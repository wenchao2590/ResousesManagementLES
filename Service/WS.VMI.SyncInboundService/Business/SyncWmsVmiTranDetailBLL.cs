namespace WS.VMI.SyncInboundService
{
    using BLL.LES;
    using DAL.LES;
    using DM.LES;
    using DM.SYS;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Transactions;

    /// <summary>
    /// 
    /// </summary>
    public class SyncWmsVmiTranDetailBLL
    {
        /// <summary>
        /// Sync
        /// </summary>
        public static void Sync(string loginUser)
        {
            ///获取未处理的检验模式中间表数据
            List<WmsVmiTranDetailInfo> wmsVmiTranDetailInfos = new WmsVmiTranDetailBLL().GetList("[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", string.Empty);
      
            if (wmsVmiTranDetailInfos.Count == 0) return;
            StringBuilder @string = new StringBuilder();

            ///获取仓库信息
            List<WarehouseInfo> warehouseInfos = new WarehouseBLL().GetList("" +
                "[WAREHOUSE] in ('" + string.Join("','", wmsVmiTranDetailInfos.Select(d => d.VmiWarehouseCode).ToArray()) + "') "
               // + " and [WAREHOUSE_TYPE] = " + (int)WarehouseTypeConstants.VMI + ""
                , string.Empty);
            if (warehouseInfos.Count == 0)
            {
                @string.AppendLine("update [LES].[TI_IFM_WMS_VMI_TRAN_DETAIL] set " +
                  "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                  "[PROCESS_TIME] = GETDATE()," +
                  "[COMMENTS] = N'0x00000230' where " + 
                  "[ID] in (" + string.Join(",", wmsVmiTranDetailInfos.Select(d => d.Id).ToArray()) + ");");
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(@string.ToString()); 
                return;
            }
            ///获取供应商信息
            List<SupplierInfo> supplierInfos = new SupplierBLL().GetList("" +
                "[SUPPLIER_NUM] in ('" + string.Join("','", wmsVmiTranDetailInfos.Select(d => d.SupplierCode).ToArray()) + "') and " +
                "[SUPPLIER_TYPE] = " + (int)SupplierTypeConstants.MaterialSupplier + "", string.Empty);
            if (supplierInfos.Count == 0)
            {
                @string.AppendLine("update [LES].[TI_IFM_WMS_VMI_TRAN_DETAIL] set " +
                  "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                  "[PROCESS_TIME] = GETDATE()," +
                  "[COMMENTS] = N'0x00000229' where " +
                  "[ID] in (" + string.Join(",", wmsVmiTranDetailInfos.Select(d => d.Id).ToArray()) + ");");
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(@string.ToString());
                return;
            }
            ///获取VMI供应商关系
            List<VmiSupplierInfo> vmiSupplierInfos = new VmiSupplierBLL().GetList("" +
                "[SUPPLIER_NUM] in ('" + string.Join("','", supplierInfos.Select(d => d.SupplierNum).ToArray()) + "') and " +
                "[WM_NO] in ('" + string.Join("','", warehouseInfos.Select(d => d.Warehouse).ToArray()) + "')", string.Empty);
            if (vmiSupplierInfos.Count == 0)
            {
                @string.AppendLine("update [LES].[TI_IFM_WMS_VMI_TRAN_DETAIL] set " +
                  "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                  "[PROCESS_TIME] = GETDATE()," +
                  "[COMMENTS] = N'0x00000429' where " +
                  "[ID] in (" + string.Join(",", wmsVmiTranDetailInfos.Select(d => d.Id).ToArray()) + ");");
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(@string.ToString());
                return;
            }

            ///获取相关物料基础信息
            List<MaintainPartsInfo> maintainPartsInfos = new MaintainPartsBLL().GetList("[PART_NO] in ('" + string.Join("','", wmsVmiTranDetailInfos.Select(d => d.PartNo).ToArray()) + "')", string.Empty);
            ///获取相关物料仓储信息
            List<PartsStockInfo> partsStockInfos = new PartsStockBLL().GetList("" +
                "[PART_NO] in ('" + string.Join("','", wmsVmiTranDetailInfos.Select(d => d.PartNo).ToArray()) + "') and " +
                "[WM_NO] in ('" + string.Join("','", warehouseInfos.Select(d => d.Warehouse).ToArray()) + "')", string.Empty);
            ///
          
            List<long> dealedIds = new List<long>();
            foreach (WmsVmiTranDetailInfo wmsVmiTranDetailInfo in wmsVmiTranDetailInfos)
            {
                var vmiWareHouseInfo = warehouseInfos.FirstOrDefault(fod => fod.Warehouse == wmsVmiTranDetailInfo.VmiWarehouseCode);
                if (vmiWareHouseInfo == null)
                {
                    @string.AppendLine("update [LES].[TI_IFM_WMS_VMI_TRAN_DETAIL] set " +
                    "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                    "[PROCESS_TIME] = GETDATE()," +
                    "[COMMENTS] = N'0x00000230' where " +///VMI供应商信息未维护
                        "[ID] = " + wmsVmiTranDetailInfo.Id + ";");
                    continue;
                }
                if (vmiWareHouseInfo.WarehouseType != (int)WarehouseTypeConstants.VMI)
                {
                    @string.AppendLine("update [LES].[TI_IFM_WMS_VMI_TRAN_DETAIL] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Processed + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000518' where " +///VMI供应商信息未维护
                        "[ID] = " + wmsVmiTranDetailInfo.Id + ";");
                    continue;
                }

                ///VMI供应商关系
                VmiSupplierInfo vmiSupplierInfo = vmiSupplierInfos.FirstOrDefault(d => d.SupplierNum == wmsVmiTranDetailInfo.SupplierCode && d.WmNo == wmsVmiTranDetailInfo.VmiWarehouseCode);
                if (vmiSupplierInfo == null)
                {
                    @string.AppendLine("update [LES].[TI_IFM_WMS_VMI_TRAN_DETAIL] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000429' where " +///VMI供应商信息未维护
                        "[ID] = " + wmsVmiTranDetailInfo.Id + ";");
                    continue;
                }
                if (!vmiSupplierInfo.VmiFlag.GetValueOrDefault())
                {
                    @string.AppendLine("update [LES].[TI_IFM_SRM_VMI_SHIPPING_NOTE] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000466' where " +///该供应商未启用WMS功能
                        "[ID] = " + wmsVmiTranDetailInfo.Id + ";");
                    continue;
                }
                if (string.IsNullOrEmpty(vmiSupplierInfo.ZoneNo))
                {
                    @string.AppendLine("update [LES].[TI_IFM_WMS_VMI_TRAN_DETAIL] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000481' where " +///VMI存储区未配置
                        "[ID] = " + wmsVmiTranDetailInfo.Id + ";");
                    continue;
                }
            
                ///指定存储区
                wmsVmiTranDetailInfo.ZoneNo = vmiSupplierInfo.ZoneNo;
                ///创建交易
                TranDetailsInfo tranDetailsInfo = TranDetailsBLL.CreateTranDetailsInfo(loginUser);
                ///PartsStockInfo -> TranDetailsInfo
                PartsStockInfo partsStockInfo = partsStockInfos.FirstOrDefault(d =>
                d.PartNo == wmsVmiTranDetailInfo.PartNo && d.WmNo == vmiSupplierInfo.WmNo && d.ZoneNo == vmiSupplierInfo.ZoneNo);
                TranDetailsBLL.GetTranDetailsInfo(partsStockInfo, ref tranDetailsInfo);
               
                ///WmsVmiTranDetailInfo -> TranDetailsInfo
                wmsVmiTranDetailInfo.Dloc = partsStockInfo.Dloc;
           
                TranDetailsBLL.GetTranDetailsInfo(wmsVmiTranDetailInfo, ref tranDetailsInfo);
                ///MaintainPartsInfo -> TranDetailsInfo
                MaintainPartsInfo maintainPartsInfo = maintainPartsInfos.FirstOrDefault(d => d.PartNo == tranDetailsInfo.PartNo);
                TranDetailsBLL.GetTranDetailsInfo(maintainPartsInfo, ref tranDetailsInfo);
             
                ///SupplierInfo -> TranDetailsInfo
                SupplierInfo supplierInfo = supplierInfos.FirstOrDefault(d => d.SupplierNum == tranDetailsInfo.SupplierNum);
                TranDetailsBLL.GetTranDetailsInfo(supplierInfo, ref tranDetailsInfo);
                ///包装数量计算
                TranDetailsBLL.CalculateTranDetailsInfo(ref tranDetailsInfo);
          
                ///获取库存交易记录的生成语句
                @string.AppendLine(TranDetailsDAL.GetInsertSql(tranDetailsInfo));
                dealedIds.Add(wmsVmiTranDetailInfo.Id);
            }
            if (dealedIds.Count > 0)
                ///已处理的中间表数据更新为已处理状态
                @string.AppendLine("update [LES].[TI_IFM_WMS_VMI_TRAN_DETAIL] set " +
                    "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Processed + "," +
                    "[PROCESS_TIME] = GETDATE() where " +
                    "[ID] in (" + string.Join(",", dealedIds.ToArray()) + ");");
            ///执行
            using (var trans = new TransactionScope())
            {
                if (@string.Length > 0)
                    BLL.SYS.CommonBLL.ExecuteNonQueryBySql(@string.ToString());
                trans.Complete();
            }


        }
    }
}
