using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DM.LES;
using BLL.LES;
using DM.SYS;
using Infrustructure.Data;
using Infrustructure.Logging;
using DAL.LES;
using System.Transactions;

namespace WS.SRM.SyncInboundService
{
    /// <summary>
    /// 箱标签同步
    /// </summary>
    public class SyncBarcodesBLL
    {
        /// <summary>
        /// 同步
        /// </summary>
        /// <param name="loginUser"></param>
        public static void Sync(string loginUser)
        {
            ///获取没有处理的送货单数据
            List<SrmBarcodeInfo> srmBarcodeInfos = new SrmBarcodeBLL().GetList("[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", string.Empty);
            if (srmBarcodeInfos.Count == 0) return;
            ///获取对应的ASN单据，TODO:单据类型过滤？原始单据号是指？
            List<ReceiveInfo> receiveInfos = new ReceiveBLL().GetList("[ASN_NO] in ('" + string.Join("','", srmBarcodeInfos.Select(d => d.SourceOrderCode).ToArray()) + "')", string.Empty);
            if (receiveInfos.Count == 0) return;
            List<ReceiveDetailInfo> receiveDetailInfos = new ReceiveDetailBLL().GetList("[RECEIVE_FID] in ('" + string.Join("','", receiveInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", string.Empty);
            if (receiveDetailInfos.Count == 0) return;
            ///供应商
            List<SupplierInfo> supplierInfos = new SupplierBLL().GetList("[SUPPLIER_NUM] in ('" + string.Join("','", receiveInfos.Select(d => d.SupplierNum).ToArray()) + "')", string.Empty);
            if (supplierInfos.Count == 0) return;
            ///获取相关物料仓储信息
            List<PartsStockInfo> partsStockInfos = new PartsStockBLL().GetList("" +
                "[PART_NO] in ('" + string.Join("','", srmBarcodeInfos.Select(d => d.PartNo).ToArray()) + "') and " +
                "[WM_NO] in ('" + string.Join("','", receiveInfos.Select(d => d.WmNo).ToArray()) + "')", string.Empty);
            ///获取相关包装器具基础信息
            List<PackageApplianceInfo> packageApplianceInfos = new List<PackageApplianceInfo>();
            if (partsStockInfos.Count > 0)
            {
                ///标准包装
                List<string> packageModels = partsStockInfos.
                    Where(d => !string.IsNullOrEmpty(d.PackageModel)).
                    Select(d => d.PackageModel).ToList();
                ///入库包装
                packageModels.AddRange(partsStockInfos.
                    Where(d => !string.IsNullOrEmpty(d.InboundPackageModel) && !packageModels.Contains(d.InboundPackageModel)).
                    Select(d => d.InboundPackageModel).ToList());
                ///上线包装
                packageModels.AddRange(partsStockInfos.
                    Where(d => !string.IsNullOrEmpty(d.InhousePackageModel) && !packageModels.Contains(d.InhousePackageModel)).
                    Select(d => d.InhousePackageModel).ToList());
                ///
                packageApplianceInfos = new PackageApplianceBLL().GetList("[PAKCAGE_NO] in ('" + string.Join("','", packageModels.ToArray()) + "')", string.Empty);
            }
            ///
            StringBuilder @string = new StringBuilder();
            ///
            List<long> dealedIds = new List<long>();
            ///
            foreach (SrmBarcodeInfo srmBarcodeInfo in srmBarcodeInfos)
            {
                ///获取入库单。TODO:需要两个编号才能定位到唯一明细数据
                ReceiveInfo receiveInfo = receiveInfos.FirstOrDefault(d => d.AsnNo == srmBarcodeInfo.SourceOrderCode);
                ///标签数据与ASN单据数据到达LES可能存在先后顺序
                if (receiveInfo == null) continue;
                ReceiveDetailInfo receiveDetailInfo = receiveDetailInfos.FirstOrDefault(d =>
                d.PartNo == srmBarcodeInfo.PartNo &&
                d.ReceiveFid.GetValueOrDefault() == receiveInfo.Fid.GetValueOrDefault() &&
                d.RunsheetNo == srmBarcodeInfo.SourceOrderCode);
                if (receiveDetailInfo == null) continue;
                ///供应商
                SupplierInfo supplierInfo = supplierInfos.FirstOrDefault(d => d.SupplierNum == receiveInfo.SupplierNum);
                if (supplierInfo == null)
                {
                    @string.AppendLine("update [LES].[TI_IFM_SRM_BARCODE] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000229' where " +///供应商信息不存在
                        "[ID] = " + srmBarcodeInfo.Id + ";");
                    continue;
                }
                ///物料仓储信息
                PartsStockInfo partsStockInfo = partsStockInfos.FirstOrDefault(d =>
                d.PartNo == srmBarcodeInfo.PartNo &&
                d.SupplierNum == supplierInfo.SupplierNum &&
                d.WmNo == receiveInfo.WmNo &&
                d.ZoneNo == receiveInfo.ZoneNo);
                if (partsStockInfo == null)
                {
                    partsStockInfo = partsStockInfos.FirstOrDefault(d =>
                    d.PartNo == srmBarcodeInfo.PartNo &&
                    d.WmNo == receiveInfo.WmNo &&
                    d.ZoneNo == receiveInfo.ZoneNo);
                }
                if (partsStockInfo == null)
                {
                    @string.AppendLine("update [LES].[TI_IFM_SRM_BARCODE] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000370' where " +///没有相关的物料仓储信息
                        "[ID] = " + srmBarcodeInfo.Id + ";");
                    continue;
                }
                ///创建标签对象
                BarcodeInfo barcodeInfo = BarcodeBLL.CreateBarcodeInfo(loginUser);
                ///SrmBarcodeInfo -> BarcodeInfo
                BarcodeBLL.GetBarcodeInfo(srmBarcodeInfo, ref barcodeInfo);
                ///SupplierInfo -> BarcodeInfo
                BarcodeBLL.GetBarcodeInfo(supplierInfo, ref barcodeInfo);
                ///PartsStockInfo -> BarcodeInfo
                BarcodeBLL.GetBarcodeInfo(partsStockInfo, ref barcodeInfo);
                ///PackageApplianceInfo -> BarcodeInfo
                PackageApplianceInfo packageApplianceInfo = packageApplianceInfos.FirstOrDefault(d => d.PackageNo == partsStockInfo.InboundPackageModel);
                BarcodeBLL.GetBarcodeInfo(packageApplianceInfo, ref barcodeInfo);
                ///
                barcodeInfo.CreateSourceFid = receiveDetailInfo.Fid;
                ///
                @string.AppendLine(BarcodeDAL.GetInsertSql(barcodeInfo));
                dealedIds.Add(srmBarcodeInfo.Id);
            }
            if (dealedIds.Count > 0)
                ///已处理的中间表数据更新为已处理状态
                @string.AppendLine("update [LES].[TI_IFM_SRM_BARCODE] set " +
                    "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Processed + "," +
                    "[PROCESS_TIME] = GETDATE() where " +
                    "[ID] in (" + string.Join(",", dealedIds.ToArray()) + ");");
            ///执行
            using (TransactionScope trans = new TransactionScope())
            {
                if (@string.Length > 0)
                    BLL.SYS.CommonBLL.ExecuteNonQueryBySql(@string.ToString());
                trans.Complete();
            }
        }
    }
}
