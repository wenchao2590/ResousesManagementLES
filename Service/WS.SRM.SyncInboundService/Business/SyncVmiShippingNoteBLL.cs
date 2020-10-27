namespace WS.SRM.SyncInboundService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DM.LES;
    using BLL.LES;
    using DM.SYS;
    using DAL.LES;
    using BLL.SYS;
    using System.Transactions;

    /// <summary>
    /// VMI-002SRM物料发货单同步- 将SRM接收的物料发货单按VMI系统启用规则进行数据分发
    /// </summary>
    public class SyncVmiShippingNoteBLL
    {
        /// <summary>
        /// 供应商发货单同步
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static void Sync(string loginUser)
        {
            ///获取没有处理的物料发货单表
            List<SrmVmiShippingNoteInfo> srmVmiShippingNoteInfos = new SrmVmiShippingNoteBLL().GetList("[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", "[ID]");
            if (srmVmiShippingNoteInfos.Count == 0) return;
            ///获取没有处理的物料发货单详情表
            List<SrmVmiShippingNoteDetailInfo> srmVmiShippingNoteDetailInfos = new SrmVmiShippingNoteDetailBLL().GetList("[NOTE_FID] in ('" + string.Join("','", srmVmiShippingNoteInfos.Select(d => d.LogFid).ToArray()) + "')", "[ID]");
            if (srmVmiShippingNoteDetailInfos.Count == 0) return;
            StringBuilder @string = new StringBuilder();
            ///获取相关仓库信息 
            List<WarehouseInfo> warehouseInfos = new WarehouseBLL().GetList("[WAREHOUSE] in ('" + string.Join("','", srmVmiShippingNoteInfos.Select(d => d.VmiWmNo).ToArray()) + "')", string.Empty);
            if (warehouseInfos.Count == 0)
            {
                @string.AppendLine("update [LES].[TI_IFM_SRM_VMI_SHIPPING_NOTE] set " +
                            "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                            "[PROCESS_TIME] = GETDATE()," +
                            "[COMMENTS] = N'0x00000230' where " +///仓库信息不存在
                            "[ID] in (" + string.Join("','", srmVmiShippingNoteInfos.Select(d => d.Id).ToArray()) + ");");
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(@string.ToString());
                return;
            }
            ///获取相关供应商基础信息
            List<SupplierInfo> supplierInfos = new SupplierBLL().GetList("[SUPPLIER_NUM] in ('" + string.Join("','", srmVmiShippingNoteInfos.Select(d => d.SupplierCode).ToArray()) + "')", string.Empty);
            if (supplierInfos.Count == 0)
            {
                @string.AppendLine("update [LES].[TI_IFM_SRM_VMI_SHIPPING_NOTE] set " +
                            "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                            "[PROCESS_TIME] = GETDATE()," +
                            "[COMMENTS] = N'0x00000229' where " +///供应商信息不存在
                            "[ID] in (" + string.Join("','", srmVmiShippingNoteInfos.Select(d => d.Id).ToArray()) + ");");
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(@string.ToString());
                return;
            }
            ///获取VMI供应商关系
            List<VmiSupplierInfo> vmiSupplierInfos = new VmiSupplierBLL().GetList("" +
                "[SUPPLIER_NUM] in ('" + string.Join("','", srmVmiShippingNoteInfos.Select(d => d.SupplierCode).ToArray()) + "') and " +
                "[WM_NO] in ('" + string.Join("','", srmVmiShippingNoteInfos.Select(d => d.VmiWmNo).ToArray()) + "')", string.Empty);
            if (vmiSupplierInfos.Count == 0)
            {
                @string.AppendLine("update [LES].[TI_IFM_SRM_VMI_SHIPPING_NOTE] set " +
                            "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                            "[PROCESS_TIME] = GETDATE()," +
                            "[COMMENTS] = N'0x00000429' where " +///VMI供应商信息未维护
                            "[ID] in (" + string.Join("','", srmVmiShippingNoteInfos.Select(d => d.Id).ToArray()) + ");");
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(@string.ToString());
                return;
            }
            ///获取相关物料基础信息
            List<MaintainPartsInfo> maintainPartsInfos = new MaintainPartsBLL().GetList("" +
                "[PART_NO] in ('" + string.Join("','", srmVmiShippingNoteDetailInfos.Select(d => d.Partno).ToArray()) + "')", string.Empty);
            ///获取相关物料仓储信息
            List<PartsStockInfo> partsStockInfos = new PartsStockBLL().GetList("" +
                "[PART_NO] in ('" + string.Join("','", srmVmiShippingNoteDetailInfos.Select(d => d.Partno).ToArray()) + "') and " +
                "[WM_NO] in ('" + string.Join("','", warehouseInfos.Select(d => d.Warehouse).ToArray()) + "')", string.Empty);
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

            ///获取系统配置
            Dictionary<string, string> configs = new ConfigBLL().GetValuesByCodes(new string[] {
                "RELEASE_VMI_RECEIVE_ACTUAL_QTY_EQUALS_REQUIRED",
                "ENABLE_VMI_FLAG"
            });
            ///
            List<long> dealedIds = new List<long>();
            ///如果数据不为空,按照规则分发
            foreach (SrmVmiShippingNoteInfo srmVmiShippingNoteInfo in srmVmiShippingNoteInfos)
            {
                ///本单据的对应仓库
                WarehouseInfo warehouseInfo = warehouseInfos.FirstOrDefault(d => d.Warehouse == srmVmiShippingNoteInfo.VmiWmNo);
                ///如果未处理的物料发货单中仓库码不存在, 修改中间表数据为挂起状态
                if (warehouseInfo == null)
                {
                    @string.AppendLine("update [LES].[TI_IFM_SRM_VMI_SHIPPING_NOTE] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000230' where " +///仓库信息不存在
                        "[ID] = " + srmVmiShippingNoteInfo.Id + ";");
                    continue;
                }
                ///仓库类型不是VMI
                if (warehouseInfo.WarehouseType.GetValueOrDefault() != (int)WarehouseTypeConstants.VMI)
                {
                    @string.AppendLine("update [LES].[TI_IFM_SRM_VMI_SHIPPING_NOTE] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000219' where " +///仓库类型错误
                        "[ID] = " + srmVmiShippingNoteInfo.Id + ";");
                    continue;
                }
                ///本发货单的物料明细
                List<SrmVmiShippingNoteDetailInfo> srmVmiShippingNoteDetails = srmVmiShippingNoteDetailInfos.Where(d =>
                d.NoteFid.GetValueOrDefault() == srmVmiShippingNoteInfo.Fid).ToList();
                if (srmVmiShippingNoteDetails.Count == 0)
                {
                    @string.AppendLine("update [LES].[TI_IFM_SRM_VMI_SHIPPING_NOTE] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000465' where " +///发货单物料数据错误
                        "[ID] = " + srmVmiShippingNoteInfo.Id + ";");
                    continue;
                }
                ///供应商
                SupplierInfo supplierInfo = supplierInfos.FirstOrDefault(d => d.SupplierNum == srmVmiShippingNoteInfo.SupplierCode);
                if (supplierInfo == null)
                {
                    @string.AppendLine("update [LES].[TI_IFM_SRM_VMI_SHIPPING_NOTE] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000229' where " +///供应商信息不存在
                        "[ID] = " + srmVmiShippingNoteInfo.Id + ";");
                    continue;
                }
                ///VMI供应商关系
                VmiSupplierInfo vmiSupplierInfo = vmiSupplierInfos.FirstOrDefault(d => d.SupplierNum == supplierInfo.SupplierNum && d.WmNo == warehouseInfo.Warehouse);
                if (vmiSupplierInfo == null)
                {
                    @string.AppendLine("update [LES].[TI_IFM_SRM_VMI_SHIPPING_NOTE] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000429' where " +///VMI供应商信息未维护
                        "[ID] = " + srmVmiShippingNoteInfo.Id + ";");
                    continue;
                }

                ///如果该仓库启用的是VMI模块则需要将单据写入VMI入库单
                if (warehouseInfo.VmiEnable.GetValueOrDefault())
                {
                    ///发布VMI入库单时实收数量默认等于需求数量
                    configs.TryGetValue("RELEASE_VMI_RECEIVE_ACTUAL_QTY_EQUALS_REQUIRED", out string release_vmi_receive_actual_qty_equals_required);
                    ///创建VMI入库单,TODO：默认类型与状态
                    VmiReceiveInfo vmiReceiveInfo = VmiReceiveBLL.CreateVmiReceiveInfo(
                        loginUser,
                        (int)VmiReceiveTypeConstants.ProductionWarehousing,
                        (int)WmmOrderStatusConstants.Published);
                    ///SrmVmiShippingNoteInfo -> VmiReceiveInfo
                    VmiReceiveBLL.GetVmiReceiveInfo(srmVmiShippingNoteInfo, ref vmiReceiveInfo);
                    ///SupplierInfo -> VmiReceiveInfo
                    VmiReceiveBLL.GetVmiReceiveInfo(supplierInfo, ref vmiReceiveInfo);
                    ///VmiSupplierInfo -> VmiReceiveInfo
                    VmiReceiveBLL.GetVmiReceiveInfo(vmiSupplierInfo, ref vmiReceiveInfo);
                    ///生成入库单语句
                    @string.AppendLine(VmiReceiveDAL.GetInsertSql(vmiReceiveInfo));
                    ///
                    foreach (SrmVmiShippingNoteDetailInfo srmVmiShippingNoteDetail in srmVmiShippingNoteDetails)
                    {
                        ///创建VMI入库单明细
                        VmiReceiveDetailInfo vmiReceiveDetailInfo = VmiReceiveDetailBLL.CreateVmiReceiveDetailInfo(loginUser);
                        ///VmiReceiveInfo -> VmiReceiveDetailInfo
                        VmiReceiveDetailBLL.GetVmiReceiveDetailInfo(vmiReceiveInfo, ref vmiReceiveDetailInfo);
                        ///MaintainPartsInfo -> VmiReceiveDetailInfo
                        MaintainPartsInfo maintainPartsInfo = maintainPartsInfos.FirstOrDefault(d => d.PartNo == srmVmiShippingNoteDetail.Partno);
                        VmiReceiveDetailBLL.GetVmiReceiveDetailInfo(maintainPartsInfo, ref vmiReceiveDetailInfo);
                        ///PartsStockInfo -> VmiReceiveDetailInfo
                        PartsStockInfo partsStockInfo = partsStockInfos.FirstOrDefault(d => d.PartNo == srmVmiShippingNoteDetail.Partno && d.WmNo == warehouseInfo.Warehouse);
                        VmiReceiveDetailBLL.GetVmiReceiveDetailInfo(partsStockInfo, ref vmiReceiveDetailInfo);
                        ///PackageApplianceInfo -> VmiReceiveDetailInfo
                        PackageApplianceInfo packageApplianceInfo = partsStockInfo == null ? null : packageApplianceInfos.FirstOrDefault(d => d.PackageNo == partsStockInfo.InboundPackageModel);
                        VmiReceiveDetailBLL.GetVmiReceiveDetailInfo(packageApplianceInfo, ref vmiReceiveDetailInfo);
                        ///SrmVmiShippingNoteDetailInfo -> VmiReceiveDetailInfo
                        VmiReceiveDetailBLL.GetVmiReceiveDetailInfo(srmVmiShippingNoteDetail, ref vmiReceiveDetailInfo);
                        ///发布VMI入库单时实收数量默认等于需求数量
                        if (!string.IsNullOrEmpty(release_vmi_receive_actual_qty_equals_required) && release_vmi_receive_actual_qty_equals_required.ToLower() == "true")
                        {
                            ///ACTUAL_BOX_NUM
                            vmiReceiveDetailInfo.ActualBoxNum = vmiReceiveDetailInfo.RequiredBoxNum;
                            ///ACTUAL_QTY
                            vmiReceiveDetailInfo.ActualQty = vmiReceiveDetailInfo.RequiredQty;
                        }
                        ///生成入库单明细语句
                        @string.AppendLine(VmiReceiveDetailDAL.GetInsertSql(vmiReceiveDetailInfo));
                    }
                    dealedIds.Add(srmVmiShippingNoteInfo.Id);
                }
                ///如果未启用VMI模块，则根据WMS系统开关决定是否写入中间表
                else
                {
                    ///是否启用WMS系统标记
                    configs.TryGetValue("ENABLE_VMI_FLAG", out string enable_vmi_flag);
                    if (!string.IsNullOrEmpty(enable_vmi_flag) && enable_vmi_flag.ToLower() == "true" && vmiSupplierInfo.VmiFlag.GetValueOrDefault())
                    {
                        ///创建WMS入库单
                        WmsVmiInboundOrderInfo wmsVmiInboundOrderInfo = WmsVmiInboundOrderBLL.CreateWmsVmiInboundOrderInfo((int)ProcessFlagConstants.Untreated, loginUser);
                        ///SrmVmiShippingNoteInfo -> WmsVmiInboundOrderInfo
                        WmsVmiInboundOrderBLL.GetWmsVmiInboundOrderInfo(srmVmiShippingNoteInfo, ref wmsVmiInboundOrderInfo);
                        ///生成入库单语句
                        @string.AppendLine(WmsVmiInboundOrderDAL.GetInsertSql(wmsVmiInboundOrderInfo));
                        ///
                        foreach (SrmVmiShippingNoteDetailInfo srmVmiShippingNoteDetail in srmVmiShippingNoteDetails)
                        {
                            ///创建WMS入库单明细
                            WmsVmiInboundOrderDetailInfo wmsVmiInboundOrderDetailInfo = WmsVmiInboundOrderDetailBLL.CreateWmsVmiInboundOrderDetailInfo(loginUser);
                            ///WmsVmiInboundOrderInfo -> WmsVmiInboundOrderDetailInfo
                            WmsVmiInboundOrderDetailBLL.GetWmsVmiInboundOrderDetailInfo(wmsVmiInboundOrderInfo, ref wmsVmiInboundOrderDetailInfo);
                            ///
                            PartsStockInfo partsStockInfo = partsStockInfos.FirstOrDefault(d => d.PartNo == srmVmiShippingNoteDetail.Partno && d.WmNo == warehouseInfo.Warehouse);
                            WmsVmiInboundOrderDetailBLL.GetWmsVmiInboundOrderDetailInfo(partsStockInfo, ref wmsVmiInboundOrderDetailInfo);
                            ///SrmVmiShippingNoteDetailInfo -> WmsVmiInboundOrderDetailInfo
                            WmsVmiInboundOrderDetailBLL.GetWmsVmiInboundOrderDetailInfo(srmVmiShippingNoteDetail, ref wmsVmiInboundOrderDetailInfo);
                            ///生成WMS入库单明细语句
                            @string.AppendLine(WmsVmiInboundOrderDetailDAL.GetInsertSql(wmsVmiInboundOrderDetailInfo));
                        }
                        ///
                        string targetSystem = "VMI";
                        string methodCode = "LES-WMS-001";
                        @string.AppendLine(BLL.LES.CommonBLL.GetCreateOutboundLogSql(
                            targetSystem,
                            wmsVmiInboundOrderInfo.LogFid.GetValueOrDefault(),
                            methodCode,
                            srmVmiShippingNoteInfo.ShippingCode,
                            loginUser));
                        dealedIds.Add(srmVmiShippingNoteInfo.Id);
                        continue;
                    }
                    @string.AppendLine("update [LES].[TI_IFM_SRM_VMI_SHIPPING_NOTE] set " +
                        "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000466' where " +///该供应商未启用WMS功能
                        "[ID] = " + srmVmiShippingNoteInfo.Id + ";");
                    continue;
                }
            }
            if (dealedIds.Count > 0)
                ///已处理的中间表数据更新为已处理状态
                @string.AppendLine("update [LES].[TI_IFM_SRM_VMI_SHIPPING_NOTE] set " +
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
