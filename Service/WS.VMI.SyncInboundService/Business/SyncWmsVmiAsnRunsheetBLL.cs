using BLL.LES;
using DM.LES;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace WS.VMI.SyncInboundService
{
    public class SyncWmsVmiAsnRunsheetBLL
    {
        /// <summary>
        /// Sync
        /// </summary>
        public static void Sync(string loginUser)
        {
            ///获取未处理的中间表数据
            List<WmsVmiAsnRunsheetInfo> wmsVmiAsnRunsheetInfos = new WmsVmiAsnRunsheetBLL().GetList("[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", string.Empty);
            if (wmsVmiAsnRunsheetInfos.Count == 0) return;
            List<WmsVmiAsnRunsheetDetailInfo> wmsVmiAsnRunsheetDetailInfos = new WmsVmiAsnRunsheetDetailBLL().GetList("[LOG_FID] in ('" + string.Join("','", wmsVmiAsnRunsheetInfos.Select(d => d.LogFid.GetValueOrDefault()).ToArray()) + "')", string.Empty);
            if (wmsVmiAsnRunsheetDetailInfos.Count == 0) return;
            ///获取相关物料供应商信息
            List<SupplierInfo> supplierInfos = new SupplierBLL().GetList("" +
                "[SUPPLIER_NUM] in ('" + string.Join("','", wmsVmiAsnRunsheetInfos.Select(d => d.Suppliercode).ToArray()) + "') and " +
                "[SUPPLIER_TYPE] = " + (int)SupplierTypeConstants.MaterialSupplier + "", string.Empty);
            ///
            StringBuilder @string = new StringBuilder();
            if (supplierInfos.Count == 0)
            {
                @string.AppendLine("update [LES].[TI_IFM_WMS_VMI_ASN_RUNSHEET] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000475' where " +///送货单中供应商信息无效
                        "[ID] in (" + string.Join("','", wmsVmiAsnRunsheetInfos.Select(d => d.Id).ToArray()) + ");");
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(@string.ToString());
                return;
            }
            ///获取相关存储区信息
            List<string> zoneNos = wmsVmiAsnRunsheetInfos.Where(d => !string.IsNullOrEmpty(d.Targetzoneno)).Select(d => d.Targetzoneno).ToList();
            zoneNos.AddRange(wmsVmiAsnRunsheetInfos.Where(d => !string.IsNullOrEmpty(d.Sourcezoneno)).Select(d => d.Sourcezoneno).ToList());
            List<ZonesInfo> zonesInfos = new ZonesBLL().GetList("" +
                "[ZONE_NO] in ('" + string.Join("','", zoneNos.ToArray()) + "')  "  , string.Empty);
            if (zonesInfos.Count == 0)
            {
                @string.AppendLine("update [LES].[TI_IFM_WMS_VMI_ASN_RUNSHEET] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000485' where " +///送货单中存储区信息不存在
                        "[ID] in (" + string.Join(",", wmsVmiAsnRunsheetInfos.Select(d => d.Id).ToArray()) + ");");
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(@string.ToString());
                return;
            }
            ///获取相关仓库信息
            List<WarehouseInfo> warehouseInfos = new WarehouseBLL().GetList("[WAREHOUSE] in ('" + string.Join("','", zonesInfos.Select(d => d.WmNo).ToArray()) + "')", string.Empty);
            if (warehouseInfos.Count == 0)
            {
                @string.AppendLine("update [LES].[TI_IFM_WMS_VMI_ASN_RUNSHEET] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000230' where " +///仓库信息不存在
                        "[ID] in (" + string.Join("','", wmsVmiAsnRunsheetInfos.Select(d => d.Id).ToArray()) + ");");
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(@string.ToString());
                return;
            }
            ///供应商VMI关系
            List<VmiSupplierInfo> vmiSupplierInfos = new VmiSupplierBLL().GetList("" +
                "[SUPPLIER_NUM] in ('" + string.Join("','", supplierInfos.Select(d => d.SupplierNum).ToArray()) + "') and " +
                "[WM_NO] in ('" + string.Join("','", warehouseInfos.Select(d => d.Warehouse).ToArray()) + "')", string.Empty);
            if (vmiSupplierInfos.Count == 0)
            {
                @string.AppendLine("update [LES].[TI_IFM_WMS_VMI_ASN_RUNSHEET] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000429' where " +///VMI供应商信息未维护
                        "[ID] in (" + string.Join("','", wmsVmiAsnRunsheetInfos.Select(d => d.Id).ToArray()) + ");");
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(@string.ToString());
                return;
            }

            ///
            List<long> dealedIds = new List<long>();
            ///循环写入数据
            foreach (WmsVmiAsnRunsheetInfo wmsVmiAsnRunsheetInfo in wmsVmiAsnRunsheetInfos)
            {
                ///获取送货单明细
                List<WmsVmiAsnRunsheetDetailInfo> wmsVmiAsnRunsheetDetails = wmsVmiAsnRunsheetDetailInfos.Where(d => d.OrderFid.GetValueOrDefault() == wmsVmiAsnRunsheetInfo.LogFid.GetValueOrDefault()).ToList();
                if (wmsVmiAsnRunsheetDetails.Count == 0)
                {
                    @string.AppendLine("update [LES].[TI_IFM_WMS_VMI_ASN_RUNSHEET] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000473' where " +///送货单无物料明细
                        "[ID] = " + wmsVmiAsnRunsheetInfo.Id + ";");
                    continue;
                }
                ///供应商信息
                SupplierInfo supplierInfo = supplierInfos.FirstOrDefault(d => d.SupplierNum == wmsVmiAsnRunsheetInfo.Suppliercode);
                if (supplierInfo == null)
                {
                    @string.AppendLine("update [LES].[TI_IFM_WMS_VMI_ASN_RUNSHEET] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000484' where " +///该送货单中供应商信息不存在
                        "[ID] = " + wmsVmiAsnRunsheetInfo.Id + ";");
                    continue;
                }
               var sourceZoneInfo= zonesInfos.FirstOrDefault(fd => fd.ZoneNo == wmsVmiAsnRunsheetInfo.Sourcezoneno);
                if (sourceZoneInfo == null)
                {
                    @string.AppendLine("update [LES].[TI_IFM_WMS_VMI_ASN_RUNSHEET] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000500' where " +///存储区不存在
                        "[ID] = " + wmsVmiAsnRunsheetInfo.Id + ";");
                    continue;
                }


                ///来源仓库
                WarehouseInfo sourceWarehouseInfo = warehouseInfos.FirstOrDefault(d => d.Warehouse == sourceZoneInfo.WmNo);
                if (sourceWarehouseInfo == null)
                {
                    @string.AppendLine("update [LES].[TI_IFM_WMS_VMI_ASN_RUNSHEET] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000230' where " +///仓库信息不存在
                        "[ID] = " + wmsVmiAsnRunsheetInfo.Id + ";");
                    continue;
                }


                if (sourceWarehouseInfo.WarehouseType.GetValueOrDefault() != (int)WarehouseTypeConstants.VMI)
                {
                    @string.AppendLine("update [LES].[TI_IFM_WMS_VMI_ASN_RUNSHEET] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000219' where " +///仓库类型错误
                        "[ID] = " + wmsVmiAsnRunsheetInfo.Id + ";");
                    continue;
                }


                var targetZoneInfo = zonesInfos.FirstOrDefault(fd => fd.ZoneNo == wmsVmiAsnRunsheetInfo.Targetzoneno);
                if (targetZoneInfo == null)
                {
                    @string.AppendLine("update [LES].[TI_IFM_WMS_VMI_ASN_RUNSHEET] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000500' where " +///存储区不存在
                        "[ID] = " + wmsVmiAsnRunsheetInfo.Id + ";");
                    continue;
                }

                ///来源仓库
                WarehouseInfo targetWarehouseInfo = warehouseInfos.FirstOrDefault(d => d.Warehouse == targetZoneInfo.WmNo);
                if (targetWarehouseInfo == null)
                {
                    @string.AppendLine("update [LES].[TI_IFM_WMS_VMI_ASN_RUNSHEET] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000230' where " +///仓库信息不存在
                        "[ID] = " + wmsVmiAsnRunsheetInfo.Id + ";");
                    continue;
                }
                if (targetWarehouseInfo.WarehouseType.GetValueOrDefault() != (int)WarehouseTypeConstants.RDC)
                {
                    @string.AppendLine("update [LES].[TI_IFM_WMS_VMI_ASN_RUNSHEET] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000219' where " +///存储区不存在
                        "[ID] = " + wmsVmiAsnRunsheetInfo.Id + ";");
                    continue;
                }
                ///VMI供应商关系
                VmiSupplierInfo vmiSupplierInfo = vmiSupplierInfos.FirstOrDefault(d => d.SupplierNum == supplierInfo.SupplierNum && d.WmNo == sourceWarehouseInfo.Warehouse);
                if (vmiSupplierInfo == null)
                {
                    @string.AppendLine("update [LES].[TI_IFM_WMS_VMI_ASN_RUNSHEET] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000429' where " +///VMI供应商信息未维护
                        "[ID] = " + wmsVmiAsnRunsheetInfo.Id + ";");
                    continue;
                }
                if (!vmiSupplierInfo.AsnFlag.GetValueOrDefault())
                {
                    @string.AppendLine("update [LES].[TI_IFM_WMS_VMI_ASN_RUNSHEET] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000486' where " +///该供应商未启用ASN功能
                        "[ID] = " + wmsVmiAsnRunsheetInfo.Id + ";");
                    continue;
                }

                ///单据衔接对象创建
                MaterialPullingOrderInfo materialPullingOrderInfo = MaterialPullingCommonBLL.CreateMaterialPullingOrderInfo();
                ///WmsVmiAsnRunsheetInfo -> MaterialPullingOrderInfo
                MaterialPullingCommonBLL.GetMaterialPullingOrderInfo(wmsVmiAsnRunsheetInfo, ref materialPullingOrderInfo);
                ///
                materialPullingOrderInfo.SourceWmNo = sourceWarehouseInfo.Warehouse;
                ///
                materialPullingOrderInfo.TargetWmNo = targetWarehouseInfo.Warehouse;
                ///
                materialPullingOrderInfo.AsnFlag = true;
                ///PART_BOX_CODE,TODO:考虑由WMS增加字段？
                materialPullingOrderInfo.PartBoxCode = string.Empty;
                ///PART_BOX_NAME,11
                materialPullingOrderInfo.PartBoxName = string.Empty;
                ///
                materialPullingOrderInfo.Route = null;
                ///
                foreach (WmsVmiAsnRunsheetDetailInfo wmsVmiAsnRunsheetDetail in wmsVmiAsnRunsheetDetails)
                {
                    ///单据衔接明细对象创建
                    MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo = MaterialPullingCommonBLL.CreateMaterialPullingOrderDetailInfo();
                    ///WmsVmiAsnRunsheetDetailInfo -> MaterialPullingOrderDetailInfo
                    MaterialPullingCommonBLL.GetMaterialPullingOrderDetailInfo(wmsVmiAsnRunsheetDetail, ref materialPullingOrderDetailInfo);
                    ///WMS拉动单明细中的供应商必须为单据的供应商
                    materialPullingOrderDetailInfo.SupplierNum = wmsVmiAsnRunsheetInfo.Suppliercode;
                    ///
                    materialPullingOrderInfo.MaterialPullingOrderDetailInfos.Add(materialPullingOrderDetailInfo);
                }
                ///生成创建入库单的语句
                @string.Append(MaterialPullingCommonBLL.CreateReceiveSql(materialPullingOrderInfo, new List<PartsStockInfo>(), loginUser));
                ///执行成功的ID
                dealedIds.Add(wmsVmiAsnRunsheetInfo.Id);
            }
            if (dealedIds.Count > 0)
                ///已处理的中间表数据更新为已处理状态
                @string.AppendLine("update [LES].[TI_IFM_WMS_VMI_ASN_RUNSHEET] set " +
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
