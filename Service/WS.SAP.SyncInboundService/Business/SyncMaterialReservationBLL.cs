namespace WS.SAP.SyncInboundService
{
    using BLL.LES;
    using DAL.LES;
    using DM.LES;
    using DM.SYS;
    using Infrustructure.Logging;
    using Infrustructure.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Transactions;

    /// <summary>
    /// SyncMaterialReservationBLL
    /// </summary>
    public class SyncMaterialReservationBLL
    {
        /// <summary>
        /// SAP物料预留单同步延迟时间
        /// </summary>
        private static string sapMaterialReservationSyncDelayMinute = "2";
        /// <summary>
        /// SyncMaterialReservation
        /// </summary>
        /// <param name="loginUser"></param>
        public static void Sync(string loginUser)
        {
            List<SapMaterialReservationInfo> sapMaterialReservationInfos = new SapMaterialReservationBLL().GetListByPage("" +
                "DATEDIFF(MINUTE,[CREATE_DATE],GETDATE()) > " + sapMaterialReservationSyncDelayMinute + " and " +
                "[PROCESS_FLAG] in (" + (int)ProcessFlagConstants.Untreated + "," + (int)ProcessFlagConstants.Resend + ")", "[ID]", 1, int.MaxValue, out int dataCnt);
            if (dataCnt == 0) return;
            ///带有库存地点信息的存储区数据
            ///TODO:存储区基础数据维护时限制SAP库存地点编号不能重复
            List<ZonesInfo> zonesInfos = new ZonesBLL().GetList("len([STOCK_PLACE_NO]) > 0", string.Empty);
            ///供应商信息
            List<SupplierInfo> supplierInfos = new SupplierBLL().GetListForInterfaceDataSync(sapMaterialReservationInfos.Select(d => d.Lifnr).ToList());
            ///物料基础信息
            List<MaintainPartsInfo> maintainPartsInfos = new MaintainPartsBLL().GetListForInterfaceDataSync(sapMaterialReservationInfos.Select(d => d.Matnr).ToList());
            ///物料仓储信息
            List<PartsStockInfo> partsStockInfos = new PartsStockBLL().GetList("[PART_NO] in ('" + string.Join("','", sapMaterialReservationInfos.Select(d => d.Matnr).ToArray()) + "')", string.Empty);
            ///已处理完成的ID
            List<long> dealedIds = new List<long>();
            StringBuilder stringBuilder = new StringBuilder();
            ///Bwart-移动类型
            ///Kostl-成本中心
            ///Lgort-中转库存地点
            ///Umlgo-接收库存地点
            ///Wempf-收货方
            ///Lifnr-供应商
            ///Rsnum-预留单号
            ///Ebeln-采购订单号
            ///Bdter-需求日期
            var gSapMaterialReservationInfos = from p in sapMaterialReservationInfos
                                               group p by new { p.Bwart, p.Kostl, p.Lgort, p.Umlgo, p.Wempf, p.Lifnr, p.Rsnum, p.Ebeln, p.Bdter } into g
                                               select new { g.Key };
            foreach (var gSapMaterialReservationInfo in gSapMaterialReservationInfos)
            {
                ///校验接收库存地点
                ZonesInfo tZone = zonesInfos.FirstOrDefault(d => d.StockPlaceNo == gSapMaterialReservationInfo.Key.Umlgo);
                if (!string.IsNullOrEmpty(gSapMaterialReservationInfo.Key.Umlgo) && tZone == null)
                {
                    stringBuilder.AppendLine("update [LES].[TI_IFM_SAP_MATERIAL_RESERVATION] " +
                        "set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000418'," +///库存地点不存在
                        "[MODIFY_USER] = N'" + loginUser + "'," +
                        "[MODIFY_DATE] = GETDATE() " +
                        "where [EBELN] = '" + gSapMaterialReservationInfo.Key.Ebeln + "';");
                    continue;
                }
                ///校验中转库存地点
                ZonesInfo sZone = zonesInfos.FirstOrDefault(d => d.StockPlaceNo == gSapMaterialReservationInfo.Key.Lgort);
                if (!string.IsNullOrEmpty(gSapMaterialReservationInfo.Key.Lgort) && sZone == null)
                {
                    stringBuilder.AppendLine("update [LES].[TI_IFM_SAP_MATERIAL_RESERVATION] " +
                        "set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000418'," +///库存地点不存在
                        "[MODIFY_USER] = N'" + loginUser + "'," +
                        "[MODIFY_DATE] = GETDATE() " +
                        "where [EBELN] = '" + gSapMaterialReservationInfo.Key.Ebeln + "';");
                    continue;
                }
                ///校验供应商
                SupplierInfo supplierInfo = supplierInfos.FirstOrDefault(d => d.SupplierNum == gSapMaterialReservationInfo.Key.Lifnr);
                if (!string.IsNullOrEmpty(gSapMaterialReservationInfo.Key.Lifnr) && supplierInfo == null)
                {
                    stringBuilder.AppendLine("update [LES].[TI_IFM_SAP_MATERIAL_RESERVATION] " +
                        "set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000239'," +///供应商数据错误
                        "[MODIFY_USER] = N'" + loginUser + "'," +
                        "[MODIFY_DATE] = GETDATE() " +
                        "where [EBELN] = '" + gSapMaterialReservationInfo.Key.Ebeln + "';");
                    continue;
                }
                ///是否存在单据明细数据
                List<SapMaterialReservationInfo> sapMaterialReservations = sapMaterialReservationInfos.Where(d => d.Ebeln == gSapMaterialReservationInfo.Key.Ebeln).ToList();
                if (sapMaterialReservations.Count == 0)
                {
                    stringBuilder.AppendLine("update [LES].[TI_IFM_SAP_MATERIAL_RESERVATION] " +
                        "set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000084'," +///数据错误
                        "[MODIFY_USER] = N'" + loginUser + "'," +
                        "[MODIFY_DATE] = GETDATE() " +
                        "where [EBELN] = '" + gSapMaterialReservationInfo.Key.Ebeln + "';");
                    continue;
                }

                #region TT_MPM_SAP_PURCHASE_ORDER
                ///采购订单号默认为SAP采购订单号
                string orderCode = gSapMaterialReservationInfo.Key.Ebeln;
                ///如果采购订单号为空，则以预留单号作为采购订单号
                ///TODO:后期考虑这种形式直接写入供应商退货单表
                if (string.IsNullOrEmpty(orderCode))
                    orderCode = gSapMaterialReservationInfo.Key.Rsnum;

                SapPurchaseOrderInfo sapPurchaseOrderInfo = new SapPurchaseOrderInfo();
                sapPurchaseOrderInfo.Fid = Guid.NewGuid();
                sapPurchaseOrderInfo.OrderCode = orderCode;
                sapPurchaseOrderInfo.SWmNo = sZone == null ? string.Empty : sZone.WmNo;
                sapPurchaseOrderInfo.SZoneNo = sZone == null ? string.Empty : sZone.ZoneNo;
                sapPurchaseOrderInfo.TWmNo = tZone == null ? string.Empty : tZone.WmNo;
                sapPurchaseOrderInfo.TZoneNo = tZone == null ? string.Empty : tZone.ZoneNo;
                sapPurchaseOrderInfo.RequireDate = gSapMaterialReservationInfo.Key.Bdter;
                sapPurchaseOrderInfo.SupplierNum = supplierInfo == null ? string.Empty : supplierInfo.SupplierNum;
                sapPurchaseOrderInfo.SupplierSname = supplierInfo == null ? string.Empty : supplierInfo.SupplierSname;
                sapPurchaseOrderInfo.SupplierName = supplierInfo == null ? string.Empty : supplierInfo.SupplierName;
                sapPurchaseOrderInfo.CustCode = gSapMaterialReservationInfo.Key.Wempf;
                sapPurchaseOrderInfo.SapBwart = gSapMaterialReservationInfo.Key.Bwart;
                sapPurchaseOrderInfo.SapKostl = gSapMaterialReservationInfo.Key.Kostl;
                sapPurchaseOrderInfo.SapLgort = gSapMaterialReservationInfo.Key.Lgort;
                sapPurchaseOrderInfo.SapUmlgo = gSapMaterialReservationInfo.Key.Umlgo;
                sapPurchaseOrderInfo.SapWempf = gSapMaterialReservationInfo.Key.Wempf;
                sapPurchaseOrderInfo.SapLifnr = gSapMaterialReservationInfo.Key.Lifnr;
                sapPurchaseOrderInfo.SapRsnum = gSapMaterialReservationInfo.Key.Rsnum;
                sapPurchaseOrderInfo.SapEbeln = gSapMaterialReservationInfo.Key.Ebeln;
                sapPurchaseOrderInfo.Status = (int)PullOrderStatusConstants.Released;
                sapPurchaseOrderInfo.CreateUser = loginUser;
                stringBuilder.AppendLine(SapPurchaseOrderDAL.GetInsertSql(sapPurchaseOrderInfo));
                #endregion

                List<SapPurchaseOrderDetailInfo> sapPurchaseOrderDetailInfos = new List<SapPurchaseOrderDetailInfo>();
                foreach (var sapMaterialReservation in sapMaterialReservations)
                {
                    ///校验物料基础信息
                    MaintainPartsInfo maintainPartsInfo = maintainPartsInfos.FirstOrDefault(d => d.PartNo == sapMaterialReservation.Matnr);
                    if (maintainPartsInfo == null)
                    {
                        stringBuilder.AppendLine("update [LES].[TI_IFM_SAP_MATERIAL_RESERVATION] " +
                        "set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000417', " +///物料信息数据错误
                        "[MODIFY_USER] = N'" + loginUser + "'," +
                        "[MODIFY_DATE] = GETDATE() " +
                        "where [ID] = '" + sapMaterialReservation.Id + "';");
                        continue;
                    }
                    ///中转库存地点的物料仓储信息
                    PartsStockInfo partsStockInfo = partsStockInfos.FirstOrDefault(d =>
                    d.PartNo == maintainPartsInfo.PartNo &&
                    d.SupplierNum == supplierInfo.SupplierNum &&
                    d.ZoneNo == sZone.ZoneNo &&
                    d.WmNo == sZone.WmNo);


                    if (!string.IsNullOrEmpty(gSapMaterialReservationInfo.Key.Lgort) && partsStockInfo == null)
                    {
                        stringBuilder.AppendLine("update [LES].[TI_IFM_SAP_MATERIAL_RESERVATION] " +
                        "set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'0x00000241'," +  ///物料仓储信息数据错误
                        "[MODIFY_USER] = N'" + loginUser + "'," +
                        "[MODIFY_DATE] = GETDATE() " +
                        "where [ID] = " + sapMaterialReservation.Id + ";");
                        continue;
                    }
                    ///接收库存地点的物料仓储信息
                    if (tZone != null)
                    {
                        PartsStockInfo tPartsStockInfo = partsStockInfos.FirstOrDefault(d =>
                        d.PartNo == maintainPartsInfo.PartNo &&
                        d.SupplierNum == supplierInfo.SupplierNum &&
                        d.ZoneNo == tZone.ZoneNo &&
                        d.WmNo == tZone.WmNo);
                        if (tPartsStockInfo == null)
                        {
                            stringBuilder.AppendLine("update [LES].[TI_IFM_SAP_MATERIAL_RESERVATION] " +
                            "set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                            "[PROCESS_TIME] = GETDATE()," +
                            "[COMMENTS] = N'0x00000241'," +///物料仓储信息数据错误
                            "[MODIFY_USER] = N'" + loginUser + "'," +
                            "[MODIFY_DATE] = GETDATE() " +
                            "where [ID] = " + sapMaterialReservation.Id + ";");
                            continue;
                        }
                    }

                    #region TT_MPM_SAP_PURCHASE_ORDER_DETAIL
                    SapPurchaseOrderDetailInfo sapPurchaseOrderDetailInfo = new SapPurchaseOrderDetailInfo();
                    sapPurchaseOrderDetailInfo.OrderFid = sapPurchaseOrderInfo.Fid;
                    sapPurchaseOrderDetailInfo.PartNo = maintainPartsInfo.PartNo;
                    sapPurchaseOrderDetailInfo.PartCname = maintainPartsInfo.PartCname;
                    sapPurchaseOrderDetailInfo.PartEname = maintainPartsInfo.PartEname;
                    sapPurchaseOrderDetailInfo.PartQty = sapMaterialReservation.Menge;
                    sapPurchaseOrderDetailInfo.PartPurchaseUom = maintainPartsInfo.PartUnits;
                    sapPurchaseOrderDetailInfo.PartUom = maintainPartsInfo.PartUnits;
                    sapPurchaseOrderDetailInfo.Package = partsStockInfo.InboundPackage;
                    sapPurchaseOrderDetailInfo.PackageModel = partsStockInfo.InboundPackageModel;
                    if (partsStockInfo.InboundPackage.GetValueOrDefault() > 0)
                        sapPurchaseOrderDetailInfo.RequirePackageQty = Convert.ToInt32(Math.Ceiling(decimal.Parse(sapMaterialReservation.Menge.GetValueOrDefault().ToString()) / partsStockInfo.InboundPackage.GetValueOrDefault()));
                    sapPurchaseOrderDetailInfo.SapMenge = sapMaterialReservation.Menge;
                    sapPurchaseOrderDetailInfo.SapRsnum = sapMaterialReservation.Rsnum;

                    if (!int.TryParse(sapMaterialReservation.Rspos, out int converintRspos))
                        throw new Exception("MC:0x00000397");///预留行号错误

                    sapPurchaseOrderDetailInfo.SapRspos = converintRspos;
                    sapPurchaseOrderDetailInfo.SapEbeln = sapMaterialReservation.Ebeln;
                    sapPurchaseOrderDetailInfo.SapEbelp = sapMaterialReservation.Ebelp;
                    sapPurchaseOrderDetailInfo.SapBwart = sapMaterialReservation.Bwart;
                    sapPurchaseOrderDetailInfo.SapKostl = sapMaterialReservation.Kostl;
                    sapPurchaseOrderDetailInfo.SapLgort = sapMaterialReservation.Lgort;
                    sapPurchaseOrderDetailInfo.SapUmlgo = sapMaterialReservation.Umlgo;
                    sapPurchaseOrderDetailInfo.SapWempf = sapMaterialReservation.Wempf;
                    sapPurchaseOrderDetailInfo.SapLifnr = sapMaterialReservation.Lifnr;
                    sapPurchaseOrderDetailInfo.Status = (int)PullOrderStatusConstants.Released;
                    sapPurchaseOrderDetailInfo.CreateUser = loginUser;
                    stringBuilder.AppendLine(SapPurchaseOrderDetailDAL.GetInsertSql(sapPurchaseOrderDetailInfo));
                    #endregion

                    sapPurchaseOrderDetailInfos.Add(sapPurchaseOrderDetailInfo);
                    dealedIds.Add(sapMaterialReservation.Id);
                }

                #region 单据衔接
                if (sapPurchaseOrderDetailInfos.Count > 0)
                {
                    int orderType = (int)SapPurchaseOrderTypeConstants.PurchaseOrder;

                    ///若SAP预留单号不为空时，系统认为是预留订单
                    //todo 0是否为空
                    if (!string.IsNullOrEmpty(sapPurchaseOrderInfo.SapRsnum) && sapPurchaseOrderInfo.SapRsnum.ToString() != "0")
                        orderType = (int)SapPurchaseOrderTypeConstants.ReservationOrder;
                    ///若SAP采购订单号为空时，系统认为是物料退货
                    if (string.IsNullOrEmpty(sapPurchaseOrderInfo.SapEbeln))
                        orderType = (int)SapPurchaseOrderTypeConstants.ReturnOrder;

                    MaterialPullingOrderInfo mpOrder = new MaterialPullingOrderInfo();
                    mpOrder.OrderNo = sapPurchaseOrderInfo.OrderCode;
                    mpOrder.PartBoxCode = string.Empty;///零件类2
                    mpOrder.PartBoxName = string.Empty; ///零件类名称3
                    mpOrder.Plant = (sZone == null ? string.Empty : sZone.Plant);///工厂4,TODO:是否增加工厂字段
                    mpOrder.Workshop = string.Empty;///车间5
                    mpOrder.AssemblyLine = string.Empty;///流水线6
                    mpOrder.SupplierNum = sapPurchaseOrderInfo.SupplierNum; ///供应商代码7
                    mpOrder.SupplierName = sapPurchaseOrderInfo.SupplierName; ///供应商名称
                    mpOrder.SourceZoneNo = sapPurchaseOrderInfo.TZoneNo;///接收存储区
                    mpOrder.SourceWmNo = sapPurchaseOrderInfo.TWmNo;///接收仓库
                    mpOrder.TargetZoneNo = sapPurchaseOrderInfo.SZoneNo;///中转存储区
                    mpOrder.TargetWmNo = sapPurchaseOrderInfo.SWmNo;///中转仓库
                    mpOrder.TargetDock = string.Empty;///道口12,TODO:是否增加道口字段
                    mpOrder.PlanShippingTime = sapPurchaseOrderInfo.RequireDate;///建议交货时间
                    mpOrder.PlanDeliveryTime = sapPurchaseOrderInfo.RequireDate;///预计到厂时间
                    mpOrder.PublishTime = DateTime.Now;
                    mpOrder.OrderType = orderType;///SAP订单类型
                    mpOrder.PullMode = (int)PullModeConstants.PurchaseOrder;
                    mpOrder.MaterialPullingOrderDetailInfos = (from m in sapPurchaseOrderDetailInfos
                                                               select new MaterialPullingOrderDetailInfo
                                                               {
                                                                   OrderNo = sapPurchaseOrderInfo.OrderCode,///拉动单号1
                                                                   SupplierNum = sapPurchaseOrderInfo.SupplierNum,///供应商2
                                                                   PartNo = m.PartNo,///物料号3
                                                                   PartCname = m.PartCname,///物料号中文名称4
                                                                   PartEname = m.PartEname,///物料号英文名称5
                                                                   Uom = m.PartUom,///计量单位6 
                                                                   PackageQty = m.Package.GetValueOrDefault(),///入库单包装数量7
                                                                   PackageModel = m.PackageModel,///入库包装编号8
                                                                   RequirePackageQty = m.RequirePackageQty.GetValueOrDefault(),///需求包装数量9
                                                                   RequirePartQty = m.PartQty.GetValueOrDefault(),///需求物料数量10
                                                                   SourceWmNo = sapPurchaseOrderInfo.TWmNo,///接收仓库
                                                                   SourceZoneNo = sapPurchaseOrderInfo.TZoneNo,///接收存储区
                                                                   TargetWmNo = sapPurchaseOrderInfo.SWmNo,///中转仓库
                                                                   TargetZoneNo = sapPurchaseOrderInfo.SZoneNo///中转存储区
                                                               }).ToList();
                    ///执行单据衔接
                    stringBuilder.AppendLine(MaterialPullingCommonBLL.Handler(mpOrder, loginUser));
                }
                #endregion
            }
            if (dealedIds.Count > 0)
                ///已处理的中间表数据更新为已处理状态
                stringBuilder.Append("update [LES].[TI_IFM_SAP_MATERIAL_RESERVATION] " +
                    "set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Processed + "," +
                    "[PROCESS_TIME] = GETDATE()," +
                    "[COMMENTS] = NULL," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() " +
                    "where [ID] in (" + string.Join(",", dealedIds.ToArray()) + ");");
            ///执行
            using (var trans = new TransactionScope())
            {
                if (stringBuilder.Length > 0)
                    BLL.SYS.CommonBLL.ExecuteNonQueryBySql(stringBuilder.ToString());
                trans.Complete();
            }
        }
    }
}
