namespace BLL.LES
{
    using DAL.LES;
    using DM.LES;
    using DAL.SYS;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DM.SYS;
    using System.Linq.Expressions;
    using Infrustructure.Linq;
    using System.Reflection;

    /// <summary>
    /// MaterialPullingCommonBLL
    /// </summary>
    public class MaterialPullingCommonBLL
    {
        #region Handler
        /// <summary>
        /// 拉动仓储单据衔接
        /// </summary>
        /// <param name="materialPullingOrderInfo"></param>
        /// <param name="loginUser"></param>
        /// <param name="splitFlag"></param>
        /// <returns></returns>
        public static string Handler(MaterialPullingOrderInfo materialPullingOrderInfo, string loginUser, bool splitFlag = true)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (materialPullingOrderInfo == null)
                throw new Exception("MC:0x00000084");///数据错误

            if (materialPullingOrderInfo.MaterialPullingOrderDetailInfos == null)
                throw new Exception("MC:0x00000084");///数据错误

            if (materialPullingOrderInfo.MaterialPullingOrderDetailInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            ///是否启用WMS系统标记
            string enable_vmi_flag = new ConfigDAL().GetValueByCode("ENABLE_VMI_FLAG");
            ///是否启用SRM系统标记、ENABLE_SRM_FLAG、默认为false
            string enable_srm_flag = new ConfigDAL().GetValueByCode("ENABLE_SRM_FLAG");

            ///根据拉动单中的供应商代码获取供应商信息
            SupplierInfo supplierInfo = new SupplierDAL().GetInfoForInterfaceDataSync(materialPullingOrderInfo.SupplierNum);
            ///物料仓储信息
            List<PartsStockInfo> partsStockInfos = new PartsStockDAL().GetListForInterfaceDataSync(materialPullingOrderInfo.MaterialPullingOrderDetailInfos.Select(d => d.PartNo).ToList());
            ///供应商类型
            int supplierType = supplierInfo == null ? 0 : supplierInfo.SupplierType.GetValueOrDefault();

            ///在此收集分单规则，例如：检验模式、积压库存、物流路线等
            ///根据传入的单个MaterialPullingOrderInfo，拆分为多个MaterialPullingOrderInfo

            ///优先使用标记库存是否作为仓储分单原则
            string prior_use_stocks_groupby_wmm_order_flag = new ConfigDAL().GetValueByCode("PRIOR_USE_STOCKS_GROUPBY_WMM_ORDER_FLAG");
            if (!string.IsNullOrEmpty(prior_use_stocks_groupby_wmm_order_flag) && prior_use_stocks_groupby_wmm_order_flag.ToLower() == "true" && splitFlag)
            {
                ///在拉动单明细范围内获取需要优先使用的物料库存数据
                ///且不能是拉动单对应的目标仓库存储区
                ///同时可用库存需要大于零
                List<StocksInfo> stocksInfos = new StocksDAL().GetList("" +
                    "[PART_NO] in ('" + string.Join("','", materialPullingOrderInfo.MaterialPullingOrderDetailInfos.Select(d => d.PartNo).ToArray()) + "') and " +
                    "[PRIOR_USE_FLAG] = 1 and " +
                    "[AVAILBLE_STOCKS] > 0 and " +
                    "[WM_NO] <> N'" + materialPullingOrderInfo.TargetWmNo + "' and " +
                    "[ZONE_NO] <> N'" + materialPullingOrderInfo.TargetZoneNo + "'", string.Empty);
                List<MaterialPullingOrderDetailInfo> materialPullingOrderDetails = new List<MaterialPullingOrderDetailInfo>();
                List<WmsVmiPartStockLockInfo> wmsVmiPartStockLockInfos = new List<WmsVmiPartStockLockInfo>();
                foreach (var stocksInfo in stocksInfos)
                {
                    MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo = materialPullingOrderInfo.MaterialPullingOrderDetailInfos.FirstOrDefault(d => d.PartNo == stocksInfo.PartNo && d.SupplierNum == stocksInfo.SupplierNum);
                    if (materialPullingOrderDetailInfo == null) continue;
                    ///匹配物料数量
                    decimal matchedQty = 0;
                    ///标记为优先使用的库存数据满足物料需求
                    if (stocksInfo.AvailbleStocks.GetValueOrDefault() >= materialPullingOrderDetailInfo.RequirePartQty)
                        matchedQty = materialPullingOrderDetailInfo.RequirePartQty;
                    else
                        matchedQty = stocksInfo.AvailbleStocks.GetValueOrDefault();
                    ///
                    materialPullingOrderDetailInfo.RequirePartQty = matchedQty;
                    materialPullingOrderDetailInfo.FrozenStockFlag = true;
                    ///
                    GetMaterialPullingOrderDetailInfo(stocksInfo, ref materialPullingOrderDetailInfo);
                    ///加入分单集合中
                    materialPullingOrderDetails.Add(materialPullingOrderDetailInfo);
                    ///从源集合中扣除
                    foreach (MaterialPullingOrderDetailInfo m in materialPullingOrderInfo.MaterialPullingOrderDetailInfos)
                    {
                        if (m.PartNo != stocksInfo.PartNo) continue;
                        if (m.SupplierNum != stocksInfo.SupplierNum) continue;
                        m.RequirePartQty -= matchedQty;
                    }
                    ///库存排查锁定物料是否同步更新库存
                    string stock_check_lock_material_sync_update_part_stock_flag = new ConfigDAL().GetValueByCode("STOCK_CHECK_LOCK_MATERIAL_SYNC_UPDATE_PART_STOCK_FLAG");
                    ///交易记录状态默认为未处理
                    int tranState = (int)WmmTranStateConstants.Created;
                    if (!string.IsNullOrEmpty(stock_check_lock_material_sync_update_part_stock_flag) && stock_check_lock_material_sync_update_part_stock_flag.ToLower() == "true")
                    {
                        ///标记交易记录状态为已处理
                        tranState = (int)WmmTranStateConstants.Done;
                        stringBuilder.AppendLine("update [LES].[TT_WMM_STOCKS] set " +
                            "[AVAILBLE_STOCKS] = isnull([AVAILBLE_STOCKS],0) - " + matchedQty + "," +
                            "[FROZEN_STOCKS] = isnull([FROZEN_STOCKS],0) + " + matchedQty + "," +
                            "[PRIOR_USE_FLAG] = " + (stocksInfo.AvailbleStocks.GetValueOrDefault() == matchedQty ? "0" : "[PRIOR_USE_FLAG]") + "," +
                            "[MODIFY_USER] = N'" + loginUser + "'," +
                            "[MODIFY_DATE] = GETDATE() where " +
                            "[ID] =" + stocksInfo.Id + ";");
                    }
                    ///来源仓库，在没有供应商的情况下就得有来源仓库否则报错
                    WarehouseInfo warehouseInfo = new WarehouseDAL().GetWarehouseInfo(stocksInfo.WmNo);
                    if (warehouseInfo == null)
                        throw new Exception("MC:0x00000230");///仓库信息不存在

                    ///未启用VMI模块
                    if (!warehouseInfo.VmiEnable.GetValueOrDefault() && !string.IsNullOrEmpty(enable_vmi_flag) && enable_vmi_flag.ToLower() == "true")
                    {
                        WmsVmiPartStockLockInfo wmsVmiPartStockLockInfo = new WmsVmiPartStockLockInfo
                        {
                            Werks = materialPullingOrderInfo.Plant,
                            Partno = stocksInfo.PartNo,
                            Suppliercode = stocksInfo.SupplierNum,
                            Vmiwarehousecode = stocksInfo.ZoneNo,
                            Partqty = materialPullingOrderDetailInfo.RequirePartQty,
                            Orilockstatus = "FXZ",
                            Targetlockstatus = "XZ",
                            Invstatus = "SC",
                            ProcessFlag = (int)ProcessFlagConstants.Untreated,
                            CreateUser = loginUser
                        };
                        wmsVmiPartStockLockInfos.Add(wmsVmiPartStockLockInfo);
                    }
                    ///构建交易
                    TranDetailsInfo tranDetailsInfo = TranDetailsBLL.CreateTranDetailsInfo((int)WmmTranTypeConstants.StateFreezing, tranState, loginUser);
                    ///以库存数据填充交易记录
                    TranDetailsBLL.GetTranDetailsInfo2(stocksInfo, ref tranDetailsInfo);
                    ///以拉动对象填充交易记录
                    GetTranDetailsInfo(materialPullingOrderInfo, ref tranDetailsInfo);
                    ///以拉动物料对象填充交易记录
                    GetTranDetailsInfo(matchedQty, ref tranDetailsInfo);
                    ///以供应商信息填充交易记录
                    TranDetailsBLL.GetTranDetailsInfo(supplierInfo, ref tranDetailsInfo);
                    ///交易时间
                    tranDetailsInfo.TranDate = DateTime.Now;
                    ///
                    stringBuilder.AppendLine(TranDetailsDAL.GetInsertSql(tranDetailsInfo));
                }
                ///                
                if (wmsVmiPartStockLockInfos.Count > 0)
                {
                    Guid logFid = Guid.NewGuid();
                    string targetSystem = "VMI";
                    string methodCode = "LES-WMS-010";
                    string keyValue = materialPullingOrderInfo.OrderNo;
                    stringBuilder.AppendLine(CommonBLL.GetCreateOutboundLogSql(targetSystem, logFid, methodCode, keyValue, loginUser));
                    foreach (var wmsVmiPartStockLockInfo in wmsVmiPartStockLockInfos)
                    {
                        wmsVmiPartStockLockInfo.LogFid = logFid;
                        stringBuilder.AppendLine(WmsVmiPartStockLockDAL.GetInsertSql(wmsVmiPartStockLockInfo));
                    }
                }
                var qs = from p in materialPullingOrderDetails group p by new { p.SourceWmNo, p.SourceZoneNo } into g select new { g.Key };
                foreach (var q in qs)
                {
                    MaterialPullingOrderInfo materialPullingOrder = new MaterialPullingOrderInfo
                    {
                        AssemblyLine = materialPullingOrderInfo.AssemblyLine,
                        Comments = materialPullingOrderInfo.Comments,
                        Keeper = materialPullingOrderInfo.Keeper,
                        OrderNo = materialPullingOrderInfo.OrderNo,
                        OrderType = 0,
                        PlanDeliveryTime = materialPullingOrderInfo.PlanDeliveryTime,
                        PlanShippingTime = materialPullingOrderInfo.PlanShippingTime,
                        Plant = materialPullingOrderInfo.Plant,
                        SourceWmNo = q.Key.SourceWmNo,
                        SourceZoneNo = q.Key.SourceZoneNo,
                        TargetDock = materialPullingOrderInfo.TargetDock,
                        TargetWmNo = materialPullingOrderInfo.TargetWmNo,
                        TargetZoneNo = materialPullingOrderInfo.TargetZoneNo,
                        Workshop = materialPullingOrderInfo.Workshop,
                        ///
                        MaterialPullingOrderDetailInfos = materialPullingOrderDetails.Where(d => d.SourceWmNo == q.Key.SourceWmNo && d.SourceZoneNo == q.Key.SourceZoneNo).ToList()
                    };
                    ///
                    stringBuilder.AppendLine(Handler(materialPullingOrder, loginUser, false));
                }
            }
            ///没有明细时直接返回已生成的语句
            materialPullingOrderInfo.MaterialPullingOrderDetailInfos = materialPullingOrderInfo.MaterialPullingOrderDetailInfos.Where(d => d.RequirePartQty > 0).ToList();
            if (materialPullingOrderInfo.MaterialPullingOrderDetailInfos.Count == 0)
                return stringBuilder.ToString();
            ///储运供应商or没有指定供应商
            if (supplierType == (int)SupplierTypeConstants.LogisticsSupplier || supplierType == 0)
            {
                ///来源仓库，在没有供应商的情况下就得有来源仓库否则报错
                WarehouseInfo warehouseInfo = new WarehouseDAL().GetWarehouseInfo(materialPullingOrderInfo.SourceWmNo);
                if (warehouseInfo == null)
                    throw new Exception("MC:0x00000230");///仓库信息不存在

                /////是否启用ASN
                //materialPullingOrderInfo.AsnFlag = warehouseInfo.AsnFlag.GetValueOrDefault();
                ///若来源仓库为RDC类型才会创建--TT_WMM_OUTPUT
                if (warehouseInfo.WarehouseType == (int)WarehouseTypeConstants.RDC)
                    ///生成出库单
                    stringBuilder.AppendLine(CreateOutputSql(materialPullingOrderInfo, loginUser));
                ///如果来源仓库为VMI类型则需要创建入库单，同时创建VMI出库单
                if (warehouseInfo.WarehouseType == (int)WarehouseTypeConstants.VMI)
                {
                    ///如果启用了LES的VMI功能，则需要生成入库单，同时生成VMI的出库单
                    if (warehouseInfo.VmiEnable.GetValueOrDefault())
                        ///生成VMI拉动单
                        stringBuilder.AppendLine(CreateVmiPullOrderSql(materialPullingOrderInfo, partsStockInfos, loginUser));
                    else
                    {
                        ///生成WMS拉动单 且是jis拉动单
                        if (enable_vmi_flag.ToLower() == "true" && materialPullingOrderInfo.PullMode == (int)PullModeConstants.Jis)
                            stringBuilder.AppendLine(CreateJisWmsPullOrderSql(materialPullingOrderInfo,loginUser));
                        else if (enable_vmi_flag.ToLower() == "true")
                            stringBuilder.AppendLine(CreateWmsPullOrderSql(materialPullingOrderInfo, partsStockInfos, loginUser));
                    }
                    //if (!warehouseInfo.AsnFlag.GetValueOrDefault())
                    //    ///生成入库单
                    //    stringBuilder.AppendLine(CreateReceiveSql(materialPullingOrderInfo, partsStockInfos, loginUser));
                }
                ///内部移库直接返回语句，无须其它逻辑
                return stringBuilder.ToString();
            }

            ///当供应商信息中的供应商类型为10.物料供应商时执行WMM-001-01逻辑
            if (supplierInfo.SupplierType.GetValueOrDefault() == (int)SupplierTypeConstants.MaterialSupplier)
            {
                ///如果有来源仓库存储区
                if (!string.IsNullOrEmpty(materialPullingOrderInfo.SourceWmNo))
                {
                    ///TODO:需要标记单据不允许SRM编辑，通过orderType体现

                    ///来源仓库，在没有供应商的情况下就得有来源仓库否则报错
                    WarehouseInfo warehouseInfo = new WarehouseDAL().GetWarehouseInfo(materialPullingOrderInfo.SourceWmNo);
                    if (warehouseInfo == null)
                        throw new Exception("MC:0x00000230");///仓库信息不存在

                    ///若来源仓库为RDC类型才会创建--TT_WMM_OUTPUT
                    if (warehouseInfo.WarehouseType == (int)WarehouseTypeConstants.RDC)
                        ///生成出库单
                        stringBuilder.AppendLine(CreateOutputSql(materialPullingOrderInfo, loginUser));
                    ///如果来源仓库为VMI类型则需要创建入库单，同时创建VMI出库单
                    if (warehouseInfo.WarehouseType == (int)WarehouseTypeConstants.VMI)
                    {
                        VmiSupplierInfo vmiSupplierInfo = new VmiSupplierDAL().GetInfo(supplierInfo.SupplierNum, materialPullingOrderInfo.SourceWmNo, materialPullingOrderInfo.SourceZoneNo);
                        if (vmiSupplierInfo == null)
                            throw new Exception("MC:0x00000429");///VMI供应商信息未维护

                        ///是否启用ASN
                        materialPullingOrderInfo.AsnFlag = vmiSupplierInfo.AsnFlag.GetValueOrDefault();

                        ///如果启用了LES的VMI功能，则需要生成入库单，同时生成VMI的出库单
                        if (vmiSupplierInfo.VmiFlag.GetValueOrDefault())
                            ///生成VMI拉动单
                            stringBuilder.AppendLine(CreateVmiPullOrderSql(materialPullingOrderInfo, partsStockInfos, loginUser));
                        else
                        {
                            ///生成WMS拉动单 且是jis拉动单
                            if (enable_vmi_flag.ToLower() == "true" && materialPullingOrderInfo.PullMode == (int)PullModeConstants.Jis)
                                stringBuilder.AppendLine(CreateJisWmsPullOrderSql(materialPullingOrderInfo, loginUser));
                            else if (enable_vmi_flag.ToLower() == "true")
                                stringBuilder.AppendLine(CreateWmsPullOrderSql(materialPullingOrderInfo, partsStockInfos, loginUser));
                        }
                        if (!vmiSupplierInfo.AsnFlag.GetValueOrDefault())
                            ///生成入库单
                            stringBuilder.AppendLine(CreateReceiveSql(materialPullingOrderInfo, partsStockInfos, loginUser));
                    }
                    return stringBuilder.ToString();
                }
                ///关注供应商是否启用ASN
                materialPullingOrderInfo.AsnFlag = supplierInfo.AsnFlag.GetValueOrDefault();
                ///若启用了SRM 且是Jis拉动单
                if (enable_srm_flag.ToLower() == "true" && materialPullingOrderInfo.PullMode==(int)PullModeConstants.Jis)
                    stringBuilder.AppendLine(CreateJisSrmPullOrderSql(materialPullingOrderInfo, loginUser)); 
                ///不是Jis拉动单
                else if (enable_srm_flag.ToLower() == "true")
                    stringBuilder.AppendLine(CreateSrmPullOrderSql(materialPullingOrderInfo, partsStockInfos, loginUser));

                ///不启用ASN的供应商直接生成入库单
                if (!materialPullingOrderInfo.AsnFlag)
                    stringBuilder.AppendLine(CreateReceiveSql(materialPullingOrderInfo, partsStockInfos, loginUser));

                return stringBuilder.ToString();
            }
            return string.Empty;
        }
        #endregion

        #region Output
        /// <summary>
        /// MaterialPullingOrderInfo -> OutputInfo
        /// </summary>
        /// <param name="materialPullingOrderInfo"></param>
        /// <param name="outputInfo"></param>
        private static void GetOutputInfo(MaterialPullingOrderInfo materialPullingOrderInfo, ref OutputInfo outputInfo)
        {
            if (materialPullingOrderInfo == null) return;
            ///OUTPUT_NO,出库单号
            outputInfo.OutputNo = materialPullingOrderInfo.OrderNo;
            ///PLANT,工厂模型_工厂
            outputInfo.Plant = materialPullingOrderInfo.Plant;
            ///WORKSHOP,工厂模型_车间
            outputInfo.Workshop = materialPullingOrderInfo.Workshop;
            ///ASSEMBLY_LINE,工厂模型_流水线
            outputInfo.AssemblyLine = materialPullingOrderInfo.AssemblyLine;
            ///WM_NO,仓库编码
            outputInfo.WmNo = materialPullingOrderInfo.SourceWmNo;
            ///ZONE_NO,存贮区编码
            outputInfo.ZoneNo = materialPullingOrderInfo.SourceZoneNo;
            ///T_WM_NO,目标仓库代码
            outputInfo.WmNo = materialPullingOrderInfo.TargetWmNo;
            ///T_ZONE_NO,目标存储区代码
            outputInfo.ZoneNo = materialPullingOrderInfo.TargetZoneNo;
            ///T_DOCK,目标道口代码
            //outputInfo.Dock = materialPullingOrderInfo.TargetDock;
            ///PART_BOX_CODE,零件类代码
            //outputInfo.PartBoxCode = materialPullingOrderInfo.PartBoxCode;
            ///BOOK_KEEPER,收货员
            outputInfo.BookKeeper = materialPullingOrderInfo.Keeper;
            ///ROUTE,送货路径
            outputInfo.Route = materialPullingOrderInfo.Route;
            ///SEND_TIME,发送时间
            outputInfo.SendTime = DateTime.Now;
            ///PULL_MODE,拉动方式
            //outputInfo.PullMode = materialPullingOrderInfo.PullMode;
            ///OUTPUT_TYPE,出库类型
            switch (materialPullingOrderInfo.PullMode)
            {
                ///采购订单
                case (int)PullModeConstants.PurchaseOrder:
                    switch (materialPullingOrderInfo.OrderType)
                    {
                        ///采购订单类型为物料退货时，需要生成出库单的类型为物料退货
                        case (int)SapPurchaseOrderTypeConstants.ReturnOrder:
                            outputInfo.OutputType = (int)OutboundTypeConstants.MaterialReturns;
                            break;
                        default: outputInfo.OutputType = (int)OutboundTypeConstants.NormalOutbound; break;
                    }
                    break;
                default: outputInfo.OutputType = (int)OutboundTypeConstants.NormalOutbound; break;
            }
            ///STATUS,出库单状态
            //outputInfo.Status = (int)WmmOrderStatusConstants.Published;
            ///COMMENTS,备注
            outputInfo.Comments = materialPullingOrderInfo.Comments;
            ///PLAN_SHIPPING_TIME,计划发货时间
            //outputInfo.PlanShippingTime = materialPullingOrderInfo.PlanShippingTime;
            /////PLAN_DELIVERY_TIME,计划到达时间
            //outputInfo.PlanDeliveryTime = materialPullingOrderInfo.PlanDeliveryTime;
        }
        /// <summary>
        /// MaterialPullingOrderDetailInfo -> OutputDetailInfo
        /// </summary>
        /// <param name="materialPullingOrderDetailInfo"></param>
        /// <param name="outputDetailInfo"></param>
        private static void GetOutputDetailInfo(MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo, ref OutputDetailInfo outputDetailInfo)
        {
            if (materialPullingOrderDetailInfo == null) return;
            ///COMMENTS,备注
            outputDetailInfo.Comments = materialPullingOrderDetailInfo.Comments;
            ///SUPPLIER_NUM,基础数据_供应商
            outputDetailInfo.SupplierNum = materialPullingOrderDetailInfo.SupplierNum;
            ///PART_NO,车辆模型_零件号
            outputDetailInfo.PartNo = materialPullingOrderDetailInfo.PartNo;
            ///RUNSHEET_NO,拉动单号
            outputDetailInfo.RunsheetNo = materialPullingOrderDetailInfo.RunsheetNo;
            ///REQUIRED_QTY,需求数量
            outputDetailInfo.RequiredQty = materialPullingOrderDetailInfo.RequirePartQty;
            ///FROZEN_STOCK_FLAG,已冻结库存标记
            outputDetailInfo.FrozenStockFlag = materialPullingOrderDetailInfo.FrozenStockFlag;
        }
        /// <summary>
        /// 生成出库单的语句
        /// </summary>
        /// <param name="materialPullingOrderInfo"></param>
        /// <param name="partsStockInfos">可空</param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static string CreateOutputSql(MaterialPullingOrderInfo materialPullingOrderInfo, string loginUser)
        {
            ///
            StringBuilder @string = new StringBuilder();
            ///创建出库单对象
            OutputInfo outputInfo = OutputBLL.CreateOutputInfo(loginUser);
            ///MaterialPullingOrderInfo -> OutputInfo
            GetOutputInfo(materialPullingOrderInfo, ref outputInfo);
            ///获取目标仓库存储区的物料仓储信息
            List<PartsStockInfo> partsStocks = new PartsStockDAL().GetList("" +
                "[PART_NO] in ('" + string.Join("','", materialPullingOrderInfo.MaterialPullingOrderDetailInfos.Select(d => d.PartNo).ToArray()) + "') and " +
                "[WM_NO] = N'" + materialPullingOrderInfo.TargetWmNo + "' and " +
                "[ZONE_NO] = N'" + materialPullingOrderInfo.TargetZoneNo + "'", string.Empty);
            ///行号
            int rowno = 0;
            decimal sumPartQty = 0;
            int sumPackageQty = 0;
            decimal sumOfPrice = 0;
            ///
            List<MaterialPullingOrderDetailInfo> materialPullingOrderDetailInfos = materialPullingOrderInfo.MaterialPullingOrderDetailInfos;
            ///排序拉动单转换出库单时是否合并
            string jis_pull_order_convert_to_output_merge = new ConfigDAL().GetValueByCode("JIS_PULL_ORDER_CONVERT_TO_OUTPUT_MERGE");
            if (!string.IsNullOrEmpty(jis_pull_order_convert_to_output_merge) && jis_pull_order_convert_to_output_merge.ToLower() == "true")
            {
                ///JIS与SPS都是排序，根据系统配置将其按库存维度进行合并
                if (materialPullingOrderInfo.PullMode == (int)PullModeConstants.Jis || materialPullingOrderInfo.PullMode == (int)PullModeConstants.Sps)
                {
                    List<string> gFields = new List<string>() { "PartNo", "SupplierNum", "RunsheetNo", "FrozenStockFlag" };
                    IQueryable<MaterialPullingOrderDetailInfo> orderDetailInfos = materialPullingOrderInfo.MaterialPullingOrderDetailInfos.AsQueryable();
                    var qGroups = DynamicQueryable.GroupBy(orderDetailInfos, "new(" + string.Join(",", gFields.ToArray()) + ")", "new(" + string.Join(",", gFields.ToArray()) + ")");
                    materialPullingOrderDetailInfos = new List<MaterialPullingOrderDetailInfo>();
                    foreach (var qGroup in qGroups)
                    {
                        PropertyInfo propertyInfo = qGroup.GetType().GetProperty("Key");
                        object keyValues = propertyInfo.GetValue(qGroup, null);
                        MaterialPullingOrderDetailInfo orderDetailInfo = new MaterialPullingOrderDetailInfo();
                        orderDetailInfo.PartNo = propertyInfo.GetType().GetProperty("PartNo").GetValue(keyValues, null).ToString();
                        orderDetailInfo.SupplierNum = propertyInfo.GetType().GetProperty("SupplierNum").GetValue(keyValues, null).ToString();
                        orderDetailInfo.RunsheetNo = propertyInfo.GetType().GetProperty("RunsheetNo").GetValue(keyValues, null).ToString();
                        orderDetailInfo.FrozenStockFlag = Convert.ToBoolean(propertyInfo.GetType().GetProperty("FrozenStockFlag").GetValue(keyValues, null));
                        orderDetailInfo.RequirePartQty = orderDetailInfos.Where(d =>
                        d.PartNo == orderDetailInfo.PartNo &&
                        d.SupplierNum == orderDetailInfo.SupplierNum &&
                        d.RunsheetNo == orderDetailInfo.RunsheetNo &&
                        d.FrozenStockFlag == orderDetailInfo.FrozenStockFlag).Sum(d => d.RequirePartQty);
                        materialPullingOrderDetailInfos.Add(orderDetailInfo);
                    }
                }
            }

            foreach (MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo in materialPullingOrderDetailInfos)
            {
                ///
                OutputDetailInfo outputDetailInfo = OutputDetailBLL.CreateOutputDetailInfo(loginUser);
                ///PartsStockInfo -> OutputDetailInfo
                PartsStockInfo partsStockInfo = partsStocks.FirstOrDefault(d =>
                d.PartNo == materialPullingOrderDetailInfo.PartNo &&
                d.SupplierNum == materialPullingOrderDetailInfo.SupplierNum);
                OutputDetailBLL.GetOutputDetailInfo(partsStockInfo, ref outputDetailInfo);
                ///OutputInfo -> OutputDetailInfo
                OutputDetailBLL.GetOutputDetailInfo(outputInfo, ref outputDetailInfo);
                ///MaterialPullingOrderDetailInfo -> OutputDetailInfo
                GetOutputDetailInfo(materialPullingOrderDetailInfo, ref outputDetailInfo);
                ///
                OutputDetailBLL.GetOutputDetailInfo(ref outputDetailInfo);
                ///
                outputDetailInfo.RowNo = ++rowno;
                ///发布出库单时实发数量等于需求数量
                string release_output_actual_qty_equals_required = new ConfigDAL().GetValueByCode("RELEASE_OUTPUT_ACTUAL_QTY_EQUALS_REQUIRED");
                if (!string.IsNullOrEmpty(release_output_actual_qty_equals_required) && release_output_actual_qty_equals_required.ToLower() == "true")
                {
                    outputDetailInfo.ActualBoxNum = outputDetailInfo.RequiredBoxNum;
                    outputDetailInfo.ActualQty = outputDetailInfo.RequiredQty;
                }
                ///
                @string.AppendFormat(OutputDetailDAL.GetInsertSql(outputDetailInfo));
                ///
                sumPartQty += outputDetailInfo.RequiredQty.GetValueOrDefault();
                sumPackageQty += outputDetailInfo.RequiredBoxNum.GetValueOrDefault();
                sumOfPrice += outputDetailInfo.PartPrice.GetValueOrDefault();
            }
            ///SUM_PART_QTY,合计物料数量
            //outputInfo.SumPartQty = sumPartQty;
            /////SUM_PACKAGE_QTY,合计箱数
            //outputInfo.SumPackageQty = sumPackageQty;
            /////
            //outputInfo.SumOfPrice = sumOfPrice;
            ///
            @string.AppendLine(OutputDAL.GetInsertSql(outputInfo));
            return @string.ToString();
        }
        #endregion

        #region Receive
        /// <summary>
        /// 获取创建入库单的语句
        /// </summary>
        /// <param name="materialPullingOrderInfo"></param>
        /// <param name="partsStockInfos"></param>
        /// <param name="zonesInfo"></param>
        /// <param name="supplierInfo"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static string CreateReceiveSql(MaterialPullingOrderInfo materialPullingOrderInfo, List<PartsStockInfo> partsStockInfos, string loginUser)
        {
            StringBuilder stringBuilder = new StringBuilder();
            ///创建入库单
            ReceiveInfo receiveInfo = new ReceiveInfo();
            GetReceiveInfo(materialPullingOrderInfo, ref receiveInfo);
            ///如果没有物料仓储信息传入则
            if (partsStockInfos.Count == 0)
                partsStockInfos = new PartsStockDAL().GetListForInterfaceDataSync(
                    materialPullingOrderInfo.MaterialPullingOrderDetailInfos.Select(d => d.PartNo).ToList(),
                    new List<string>() { materialPullingOrderInfo.TargetZoneNo });
            ///根据对象加载入库单及明细
            List<ReceiveDetailInfo> receiveDetailInfos = GetReceiveDetailInfos(materialPullingOrderInfo, partsStockInfos, receiveInfo.Fid.GetValueOrDefault());
            ///
            stringBuilder.AppendFormat(PartInspectionModeBLL.LoadInspectionMode(ref receiveInfo, ref receiveDetailInfos, loginUser));
            ///将拉动单数据写入入库单及入库单明细
            ///发布入库单时实收数量等于需求数量
            string release_receive_actual_qty_equals_required = new ConfigDAL().GetValueByCode("RELEASE_RECEIVE_ACTUAL_QTY_EQUALS_REQUIRED");
            foreach (ReceiveDetailInfo receiveDetailInfo in receiveDetailInfos)
            {
                ///根据物料号、供应商、仓库、存储区获取物料仓储信息，并赋予单据明细中的目标库位
                PartsStockInfo partsStockInfo = partsStockInfos.FirstOrDefault(d =>
                   d.PartNo == receiveDetailInfo.PartNo &&
                   d.SupplierNum == receiveDetailInfo.SupplierNum &&
                   d.WmNo == receiveDetailInfo.TargetWm &&
                   d.ZoneNo == receiveDetailInfo.TargetZone);
                ///
                if (!string.IsNullOrEmpty(release_receive_actual_qty_equals_required) && release_receive_actual_qty_equals_required.ToLower() == "true")
                {
                    receiveDetailInfo.ActualBoxNum = receiveDetailInfo.RequiredBoxNum;
                    receiveDetailInfo.ActualQty = receiveDetailInfo.RequiredQty;
                }
                stringBuilder.AppendFormat(ReceiveDetailDAL.GetInsertSql(receiveDetailInfo));

                ///如果不是来自于ASN，则需要生成标签
                if (!materialPullingOrderInfo.AsnFlag)///TODO:需要改到物料标签统一生成函数
                    stringBuilder.AppendFormat(GetCreateBarcodeSql(receiveDetailInfo, new List<BarcodeInfo>(), partsStockInfo, null, loginUser));
            }
            ///主表
            stringBuilder.AppendFormat(ReceiveDAL.GetInsertSql(receiveInfo));
            return stringBuilder.ToString();
        }
        #endregion

        #region WmsPullOrder
        /// <summary>
        /// 生成WMS接口拉动单
        /// </summary>
        /// <param name="materialPullingOrderInfo"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static string CreateWmsPullOrderSql(MaterialPullingOrderInfo materialPullingOrderInfo, List<PartsStockInfo> partsStockInfos, string loginUser)
        {
            StringBuilder stringBuilder = new StringBuilder();

            #region VmiPullOrderInfo
            WmsVmiPullingOrderInfo wmsVmiPullingOrderInfo = new WmsVmiPullingOrderInfo
            {
                Fid = Guid.NewGuid(),
                LogFid = Guid.NewGuid(),
                OrderNo = materialPullingOrderInfo.OrderNo,
                PartBoxCode = materialPullingOrderInfo.PartBoxCode,
                PartBoxName = materialPullingOrderInfo.PartBoxName,
                Plant = materialPullingOrderInfo.Plant,
                SupplierNum = materialPullingOrderInfo.SupplierNum,
                SupplierName = materialPullingOrderInfo.SupplierName,
                SourceZoneNo = materialPullingOrderInfo.SourceWmNo,
                TargetZoneNo = materialPullingOrderInfo.TargetWmNo,
                PublishTime = materialPullingOrderInfo.PublishTime,
                OrderType = materialPullingOrderInfo.OrderType,
                Dock = materialPullingOrderInfo.TargetDock,
                PlanShippingTime = materialPullingOrderInfo.PlanShippingTime,
                PlanDeliveryTime = materialPullingOrderInfo.PlanDeliveryTime,
                AsnFlag = materialPullingOrderInfo.AsnFlag,
                EmergencyFlag = materialPullingOrderInfo.EmergencyFlag,
                Wintimecode = string.Empty,
                Keeper = materialPullingOrderInfo.Keeper,
                Remark = materialPullingOrderInfo.Comments,
                InspectFlag = materialPullingOrderInfo.InspectFlag,
                ProcessFlag = (int)ProcessFlagConstants.Untreated,
                Wintimedesc = string.Empty,
                CreateUser = loginUser
            };
            #endregion

            // planmod转换为ordertype
            switch ((PullModeConstants)materialPullingOrderInfo.PullMode)
            {
                case PullModeConstants.Twd:
                    wmsVmiPullingOrderInfo.OrderType = (int)WmsOrderTypeConstants.Twd;
                    break;
                case PullModeConstants.Plan:
                    wmsVmiPullingOrderInfo.OrderType = (int)WmsOrderTypeConstants.Plan;
                    break;
                case PullModeConstants.Jis:
                    wmsVmiPullingOrderInfo.OrderType = (int)WmsOrderTypeConstants.Jis;
                    break; 
            }
            stringBuilder.AppendFormat(WmsVmiPullingOrderDAL.GetInsertSql(wmsVmiPullingOrderInfo));
            int rowNo = 0;
            ///如果没有物料仓储信息传入则
            if (partsStockInfos.Count == 0)
                partsStockInfos = new PartsStockDAL().GetListForInterfaceDataSync(
                    materialPullingOrderInfo.MaterialPullingOrderDetailInfos.Select(d => d.PartNo).ToList(),
                    new List<string>() { materialPullingOrderInfo.TargetZoneNo });
            foreach (MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo in materialPullingOrderInfo.MaterialPullingOrderDetailInfos)
            {
                ///根据物料号、供应商、仓库、存储区获取物料仓储信息
                ///用于加载包装信息
                PartsStockInfo partsStockInfo = partsStockInfos.FirstOrDefault(d =>
                   d.PartNo == materialPullingOrderDetailInfo.PartNo &&
                   d.SupplierNum == materialPullingOrderDetailInfo.SupplierNum &&
                   d.WmNo == materialPullingOrderInfo.TargetWmNo &&
                   d.ZoneNo == materialPullingOrderInfo.TargetZoneNo);
                ///
                #region WmsVmiPullingOrderDetailInfo
                WmsVmiPullingOrderDetailInfo wmsVmiPullingOrderDetailInfo = new WmsVmiPullingOrderDetailInfo
                {
                    OrderFid = wmsVmiPullingOrderInfo.Fid,
                    Targetslcode = partsStockInfo == null ? string.Empty : partsStockInfo.Dloc,
                    Externlineno = (++rowNo).ToString(),
                    Suppliercode = materialPullingOrderDetailInfo.SupplierNum,
                    Suppliername = materialPullingOrderDetailInfo.SupplierName,
                    PartNo = materialPullingOrderDetailInfo.PartNo,
                    PartCname = materialPullingOrderDetailInfo.PartCname,
                    Suppermarketrepository = partsStockInfo == null ? string.Empty : partsStockInfo.SupperZoneDloc,
                    Verifymode = materialPullingOrderDetailInfo.InspectMode.ToString()
                };
                ///TODO:
                ///单包装数
                if (materialPullingOrderDetailInfo.PackageQty == 0)
                    wmsVmiPullingOrderDetailInfo.Snp = partsStockInfo?.InboundPackage;
                else
                    wmsVmiPullingOrderDetailInfo.Snp = materialPullingOrderDetailInfo.PackageQty;
                ///包装型号
                if (string.IsNullOrEmpty(materialPullingOrderDetailInfo.PackageModel))
                    wmsVmiPullingOrderDetailInfo.PackageModel = partsStockInfo == null ? string.Empty : partsStockInfo.InboundPackageModel;
                else
                    wmsVmiPullingOrderDetailInfo.PackageModel = materialPullingOrderDetailInfo.PackageModel;
                ///需求包装数量
                if (materialPullingOrderDetailInfo.RequirePackageQty == 0 && wmsVmiPullingOrderDetailInfo.Snp > 0)
                    wmsVmiPullingOrderDetailInfo.PackageCode = Convert.ToInt32(Math.Ceiling(materialPullingOrderDetailInfo.RequirePartQty / wmsVmiPullingOrderDetailInfo.Snp.GetValueOrDefault()));
                else
                    wmsVmiPullingOrderDetailInfo.PackageCode = materialPullingOrderDetailInfo.RequirePackageQty;
                ///
                wmsVmiPullingOrderDetailInfo.PartQty = materialPullingOrderDetailInfo.RequirePartQty;
                wmsVmiPullingOrderDetailInfo.Remark = materialPullingOrderDetailInfo.Comments;
                wmsVmiPullingOrderDetailInfo.CreateUser = loginUser;
                #endregion

                stringBuilder.AppendFormat(WmsVmiPullingOrderDetailDAL.GetInsertSql(wmsVmiPullingOrderDetailInfo));
            }
            ///并生成对SRM的数据发送任务
            string targetSystem = "VMI";
            string methodCode = "LES-WMS-003";
            string keyValue = materialPullingOrderInfo.OrderNo;
            stringBuilder.AppendFormat(CommonBLL.GetCreateOutboundLogSql(targetSystem, wmsVmiPullingOrderInfo.LogFid.GetValueOrDefault(), methodCode, keyValue, loginUser));
            return stringBuilder.ToString();
        }
        #endregion

        #region WmsVmiJisPullOrder
        public static string CreateJisWmsPullOrderSql(MaterialPullingOrderInfo materialPullingOrderInfo, string loginUser)
        {
            StringBuilder @string = new StringBuilder();

            #region WmsVmiJisPullOrderInfo
            ///WmsVmiJisPullOrderInfo对象
            WmsVmiJisPullOrderInfo wmsVmiJisPullOrderInfo = WmsVmiJisPullOrderBLL.CreateWmsVmiJisPullOrderInfo(loginUser);
            ///MaterialPullingOrderInfo-->WmsVmiJisPullOrderInfo
            WmsVmiJisPullOrderBLL.GetWmsVmiJisPullOrderByMaterial(materialPullingOrderInfo, ref wmsVmiJisPullOrderInfo);
            ///CreateWmsVmiJisPullOrderSql
            @string.AppendLine(WmsVmiJisPullOrderDAL.GetInsertSql(wmsVmiJisPullOrderInfo));
            #endregion

            #region  WmsVmiJisPullOrderDetails     
            ///行号
            int rowNo = 0;
            foreach (MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo in materialPullingOrderInfo.MaterialPullingOrderDetailInfos)
            {
                rowNo += 1;
                #region WmsVmiJisPullOrderDetailInfo
                ///WmsVmiJisPullOrderDetailInfo 对象
                WmsVmiJisPullOrderDetailInfo wmsVmiJisPullOrderDetailInfo = WmsVmiJisPullOrderDetailBLL.CreateWmsVmiJisPullOrderDetailInfo(loginUser);
                ///JisWmsPullOrderInfo-->wmsVmiJisPullOrderDetailInfo
                WmsVmiJisPullOrderDetailBLL.GetWmsVmiJisPullOrderDetailByOrder(wmsVmiJisPullOrderInfo, ref wmsVmiJisPullOrderDetailInfo);
                ///ExternLineNo,行号
                wmsVmiJisPullOrderDetailInfo.ExternLineNo = rowNo.ToString();
                ///JisWmsPullOrderInfo-->wmsVmiJisPullOrderDetailInfo
                WmsVmiJisPullOrderDetailBLL.GetWmsVmiJisPullOrderDetailByMaterial(materialPullingOrderDetailInfo, ref wmsVmiJisPullOrderDetailInfo);
                ///CreateWmsVmiJisPullOrderDetailSql
                @string.AppendLine(WmsVmiJisPullOrderDetailDAL.GetInsertSql(wmsVmiJisPullOrderDetailInfo));
                #endregion
            }
            #endregion
            #region Send Data
            ///并生成对WMS的数据发送任务
            string targetSystem = "VMI";
            string methodCode = "LES-WMS-004";
            string keyValue = materialPullingOrderInfo.OrderNo;
            @string.AppendFormat(CommonBLL.GetCreateOutboundLogSql(targetSystem, wmsVmiJisPullOrderInfo.LogFid.GetValueOrDefault(), methodCode, keyValue, loginUser));
            #endregion

            return @string.ToString();
        }
        #endregion

        #region VmiPullOrder
        /// <summary>
        /// 生成LES-VMI模块拉动单
        /// </summary>
        /// <param name="materialPullingOrderInfo"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static string CreateVmiPullOrderSql(MaterialPullingOrderInfo materialPullingOrderInfo, List<PartsStockInfo> partsStockInfos, string loginUser)
        {
            StringBuilder stringBuilder = new StringBuilder();

            #region VmiPullOrderInfo
            VmiPullOrderInfo vmiPullOrderInfo = new VmiPullOrderInfo
            {
                Fid = Guid.NewGuid(),
                OrderCode = materialPullingOrderInfo.OrderNo,
                PartBoxCode = materialPullingOrderInfo.PartBoxCode,
                PartBoxName = materialPullingOrderInfo.PartBoxName,
                Plant = materialPullingOrderInfo.Plant,
                Workshop = materialPullingOrderInfo.Workshop,
                AssemblyLine = materialPullingOrderInfo.AssemblyLine,
                RouteCode = materialPullingOrderInfo.Route,
                SupplierNum = materialPullingOrderInfo.SupplierNum,
                SWmNo = materialPullingOrderInfo.SourceWmNo,
                SZoneNo = materialPullingOrderInfo.SourceZoneNo,
                TWmNo = materialPullingOrderInfo.TargetWmNo,
                TZoneNo = materialPullingOrderInfo.TargetZoneNo,
                PublishTime = materialPullingOrderInfo.PublishTime,
                OrderType = materialPullingOrderInfo.OrderType,
                Dock = materialPullingOrderInfo.TargetDock,
                PlanShippingTime = materialPullingOrderInfo.PlanShippingTime,
                PlanDeliveryTime = materialPullingOrderInfo.PlanDeliveryTime,
                OrderStatus = (int)PullOrderStatusConstants.Released,
                AsnFlag = materialPullingOrderInfo.AsnFlag,
                TimeZones = string.Empty,
                Keeper = materialPullingOrderInfo.Keeper,
                Comments = materialPullingOrderInfo.Comments,
                PullMode = materialPullingOrderInfo.PullMode,
                InspectionFlag = materialPullingOrderInfo.InspectFlag,
                InspectionMode = null,
                WindowTimes = string.Empty,
                CreateUser = loginUser
            };
            #endregion

            stringBuilder.AppendFormat(VmiPullOrderDAL.GetInsertSql(vmiPullOrderInfo));
            int rowNo = 0;
            ///如果没有物料仓储信息传入则
            if (partsStockInfos.Count == 0)
                partsStockInfos = new PartsStockDAL().GetListForInterfaceDataSync(
                    materialPullingOrderInfo.MaterialPullingOrderDetailInfos.Select(d => d.PartNo).ToList(),
                    new List<string>() { materialPullingOrderInfo.TargetZoneNo });
            foreach (MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo in materialPullingOrderInfo.MaterialPullingOrderDetailInfos)
            {
                ///根据物料号、供应商、仓库、存储区获取物料仓储信息
                ///用于加载包装信息
                PartsStockInfo partsStockInfo = partsStockInfos.FirstOrDefault(d =>
                   d.PartNo == materialPullingOrderDetailInfo.PartNo &&
                   d.SupplierNum == materialPullingOrderDetailInfo.SupplierNum &&
                   d.WmNo == materialPullingOrderInfo.TargetWmNo &&
                   d.ZoneNo == materialPullingOrderInfo.TargetZoneNo);
                ///
                #region VmiPullOrderDetailInfo
                VmiPullOrderDetailInfo vmiPullOrderDetailInfo = new VmiPullOrderDetailInfo
                {
                    OrderFid = vmiPullOrderInfo.Fid,
                    OrderCode = vmiPullOrderInfo.OrderCode,
                    RowNo = ++rowNo,
                    SupplierNum = materialPullingOrderDetailInfo.SupplierNum,
                    WorkshopSection = string.Empty,
                    Location = string.Empty,
                    PartNo = materialPullingOrderDetailInfo.PartNo,
                    PartVersion = string.Empty,
                    PartCname = materialPullingOrderDetailInfo.PartCname,
                    PartEname = materialPullingOrderDetailInfo.PartEname,
                    MeasuringUnitNo = partsStockInfo == null ? string.Empty : partsStockInfo.PartUnits
                };
                ///单包装数
                if (materialPullingOrderDetailInfo.PackageQty == 0)
                    vmiPullOrderDetailInfo.Package = partsStockInfo?.InboundPackage;
                else
                    vmiPullOrderDetailInfo.Package = materialPullingOrderDetailInfo.PackageQty;
                ///包装型号
                if (string.IsNullOrEmpty(materialPullingOrderDetailInfo.PackageModel))
                    vmiPullOrderDetailInfo.PackageModel = partsStockInfo == null ? string.Empty : partsStockInfo.InboundPackageModel;
                else
                    vmiPullOrderDetailInfo.PackageModel = materialPullingOrderDetailInfo.PackageModel;
                ///需求包装数量
                if (materialPullingOrderDetailInfo.RequirePackageQty == 0 && vmiPullOrderDetailInfo.Package > 0)
                    vmiPullOrderDetailInfo.RequiredPackageQty = Convert.ToInt32(Math.Ceiling(materialPullingOrderDetailInfo.RequirePartQty / vmiPullOrderDetailInfo.Package.GetValueOrDefault()));
                else
                    vmiPullOrderDetailInfo.RequiredPackageQty = materialPullingOrderDetailInfo.RequirePackageQty;
                ///
                vmiPullOrderDetailInfo.RequiredPartQty = materialPullingOrderDetailInfo.RequirePartQty;
                vmiPullOrderDetailInfo.Comments = materialPullingOrderDetailInfo.Comments;
                vmiPullOrderDetailInfo.CreateUser = loginUser;
                #endregion

                stringBuilder.AppendFormat(VmiPullOrderDetailDAL.GetInsertSql(vmiPullOrderDetailInfo));
            }
            ///如果未启用ASN的情况下同时生成VMI出库单
            if (!materialPullingOrderInfo.AsnFlag)
                stringBuilder.AppendFormat(CreateVmiOutputSql(materialPullingOrderInfo, partsStockInfos, loginUser));
            return stringBuilder.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="materialPullingOrderInfo"></param>
        /// <param name="partsStockInfos"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static string CreateVmiOutputSql(MaterialPullingOrderInfo materialPullingOrderInfo, List<PartsStockInfo> partsStockInfos, string loginUser)
        {
            ///
            StringBuilder stringBuilder = new StringBuilder();

            #region VmiOutputInfo
            VmiOutputInfo vmiOutputInfo = new VmiOutputInfo
            {
                Fid = Guid.NewGuid(),
                OutputNo = materialPullingOrderInfo.OrderNo,
                Plant = materialPullingOrderInfo.Plant,
                WmNo = materialPullingOrderInfo.SourceWmNo,
                ZoneNo = materialPullingOrderInfo.SourceZoneNo,
                BookKeeper = materialPullingOrderInfo.Keeper,
                AssemblyLine = materialPullingOrderInfo.AssemblyLine,
                Workshop = materialPullingOrderInfo.Workshop,
                Route = materialPullingOrderInfo.Route,
                TWmNo = materialPullingOrderInfo.TargetWmNo,
                TZoneNo = materialPullingOrderInfo.TargetZoneNo,
                TDock = materialPullingOrderInfo.TargetDock,
                PartBoxCode = materialPullingOrderInfo.PartBoxCode,
                OutputType = (int)OutboundTypeConstants.NormalOutbound,
                CreateUser = loginUser,
                Status = (int)WmmOrderStatusConstants.Published,
                SendTime = DateTime.Now,
                PullMode = materialPullingOrderInfo.PullMode
            };
            #endregion

            stringBuilder.AppendFormat(VmiOutputDAL.GetInsertSql(vmiOutputInfo));
            int rowno = 0;
            ///如果没有物料仓储信息传入则
            if (partsStockInfos.Count == 0)
                partsStockInfos = new PartsStockDAL().GetListForInterfaceDataSync(
                    materialPullingOrderInfo.MaterialPullingOrderDetailInfos.Select(d => d.PartNo).ToList(),
                    new List<string>() { materialPullingOrderInfo.TargetZoneNo });
            foreach (MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo in materialPullingOrderInfo.MaterialPullingOrderDetailInfos)
            {
                ///根据物料号、供应商、仓库、存储区获取物料仓储信息，并赋予单据明细中的目标库位
                PartsStockInfo partsStockInfo = partsStockInfos.FirstOrDefault(d =>
                   d.PartNo == materialPullingOrderDetailInfo.PartNo &&
                   d.SupplierNum == materialPullingOrderDetailInfo.SupplierNum &&
                   d.WmNo == materialPullingOrderDetailInfo.TargetWmNo &&
                   d.ZoneNo == materialPullingOrderDetailInfo.TargetZoneNo);
                if (string.IsNullOrEmpty(materialPullingOrderDetailInfo.TargetDloc) && partsStockInfo != null)
                    materialPullingOrderDetailInfo.TargetDloc = partsStockInfo.Dloc;

                #region VmiOutputDetailInfo
                VmiOutputDetailInfo vmiOutputDetailInfo = new VmiOutputDetailInfo
                {
                    OutputFid = vmiOutputInfo.Fid,
                    Plant = materialPullingOrderInfo.Plant,
                    SupplierNum = materialPullingOrderDetailInfo.SupplierNum,
                    WmNo = materialPullingOrderInfo.SourceWmNo,
                    ZoneNo = materialPullingOrderInfo.SourceZoneNo,
                    Dloc = materialPullingOrderDetailInfo.SourceDloc,
                    TargetWm = materialPullingOrderInfo.TargetWmNo,
                    TargetZone = materialPullingOrderInfo.TargetZoneNo,
                    TargetDloc = materialPullingOrderDetailInfo.TargetDloc,
                    PartNo = materialPullingOrderDetailInfo.PartNo,
                    PartCname = materialPullingOrderDetailInfo.PartCname,
                    Package = materialPullingOrderDetailInfo.PackageQty,
                    RequiredBoxNum = materialPullingOrderDetailInfo.RequirePackageQty,
                    RequiredQty = materialPullingOrderDetailInfo.RequirePartQty,
                    PackageModel = materialPullingOrderDetailInfo.PackageModel,
                    MeasuringUnitNo = materialPullingOrderDetailInfo.Uom,
                    PartEname = materialPullingOrderDetailInfo.PartEname,
                    AssemblyLine = materialPullingOrderInfo.AssemblyLine,
                    BoxParts = materialPullingOrderInfo.PartBoxCode,
                    Comments = materialPullingOrderInfo.Comments,
                    CreateUser = loginUser,
                    RowNo = ++rowno
                };
                ///发布出库单时实发数量等于需求数量
                string release_output_actual_qty_equals_required = new ConfigDAL().GetValueByCode("RELEASE_OUTPUT_ACTUAL_QTY_EQUALS_REQUIRED");
                if (release_output_actual_qty_equals_required.ToLower() == "true")
                {
                    vmiOutputDetailInfo.ActualBoxNum = materialPullingOrderDetailInfo.RequirePackageQty;
                    vmiOutputDetailInfo.ActualQty = materialPullingOrderDetailInfo.RequirePartQty;
                }
                #endregion

                stringBuilder.AppendFormat(VmiOutputDetailDAL.GetInsertSql(vmiOutputDetailInfo));
            }
            return stringBuilder.ToString();
        }
        #endregion

        #region SrmPullOrder
        /// <summary>
        /// SRM-MaterialSupplier
        /// </summary>
        /// <param name="materialPullingOrderInfo"></param>
        /// <param name="supplierInfo"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static string CreateSrmPullOrderSql(MaterialPullingOrderInfo materialPullingOrderInfo, List<PartsStockInfo> partsStockInfos, string loginUser)
        {
            StringBuilder stringBuilder = new StringBuilder();

            #region SrmPullingOrderInfo
            SrmPullingOrderInfo srmPullingOrderInfo = new SrmPullingOrderInfo
            {
                Fid = Guid.NewGuid(),
                LogFid = Guid.NewGuid(),
                OrderNo = materialPullingOrderInfo.OrderNo,
                PartBoxCode = materialPullingOrderInfo.PartBoxCode,
                PartBoxName = materialPullingOrderInfo.PartBoxName,
                Plant = materialPullingOrderInfo.Plant,
                SupplierNum = materialPullingOrderInfo.SupplierNum,
                SupplierName = materialPullingOrderInfo.SupplierName,
                SourceZoneNo = materialPullingOrderInfo.SourceZoneNo,
                TargetZoneNo = materialPullingOrderInfo.TargetZoneNo,
                PublishTime = materialPullingOrderInfo.PublishTime,
                OrderType = materialPullingOrderInfo.OrderType,///TODO:
                Dock = materialPullingOrderInfo.TargetDock,
                PlanShippingTime = materialPullingOrderInfo.PlanShippingTime,
                PlanDeliveryTime = materialPullingOrderInfo.PlanDeliveryTime,
                AsnFlag = materialPullingOrderInfo.AsnFlag,
                Keeper = materialPullingOrderInfo.Keeper,
                Remark = materialPullingOrderInfo.Comments,
                EmergencyFlag = materialPullingOrderInfo.EmergencyFlag,
                InspectFlag = materialPullingOrderInfo.InspectFlag,
                ProcessFlag = (int)ProcessFlagConstants.Untreated,
                CreateUser = loginUser
            };
            #endregion

            stringBuilder.AppendFormat(SrmPullingOrderDAL.GetInsertSql(srmPullingOrderInfo));
            ///如果没有物料仓储信息传入则
            if (partsStockInfos.Count == 0)
                partsStockInfos = new PartsStockDAL().GetListForInterfaceDataSync(
                    materialPullingOrderInfo.MaterialPullingOrderDetailInfos.Select(d => d.PartNo).ToList(),
                    new List<string>() { materialPullingOrderInfo.TargetZoneNo });
            foreach (MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo in materialPullingOrderInfo.MaterialPullingOrderDetailInfos)
            {
                ///根据物料号、供应商、仓库、存储区获取物料仓储信息
                ///用于加载包装信息
                PartsStockInfo partsStockInfo = partsStockInfos.FirstOrDefault(d =>
                   d.PartNo == materialPullingOrderDetailInfo.PartNo &&
                   d.SupplierNum == materialPullingOrderDetailInfo.SupplierNum &&
                   d.WmNo == materialPullingOrderInfo.TargetWmNo &&
                   d.ZoneNo == materialPullingOrderInfo.TargetZoneNo);
                ///
                #region SrmPullingOrderDetailInfo
                SrmPullingOrderDetailInfo srmPullingOrderDetailInfo = new SrmPullingOrderDetailInfo
                {
                    OrderFid = srmPullingOrderInfo.Fid,
                    TargetSlcode = partsStockInfo == null ? string.Empty : partsStockInfo.Dloc,
                    PartNo = materialPullingOrderDetailInfo.PartNo,
                    PartCname = materialPullingOrderDetailInfo.PartCname
                };
                ///单包装数
                if (materialPullingOrderDetailInfo.PackageQty == 0)
                    srmPullingOrderDetailInfo.Snp = partsStockInfo?.InboundPackage;
                else
                    srmPullingOrderDetailInfo.Snp = materialPullingOrderDetailInfo.PackageQty;
                ///包装型号
                if (string.IsNullOrEmpty(materialPullingOrderDetailInfo.PackageModel))
                    srmPullingOrderDetailInfo.PackageModel = partsStockInfo == null ? string.Empty : partsStockInfo.InboundPackageModel;
                else
                    srmPullingOrderDetailInfo.PackageModel = materialPullingOrderDetailInfo.PackageModel;
                ///
                srmPullingOrderDetailInfo.PartQty = materialPullingOrderDetailInfo.RequirePartQty;
                srmPullingOrderDetailInfo.Remark = materialPullingOrderDetailInfo.Comments;
                srmPullingOrderDetailInfo.CreateUser = loginUser;
                #endregion

                stringBuilder.AppendFormat(SrmPullingOrderDetailDAL.GetInsertSql(srmPullingOrderDetailInfo));
            }
            ///并生成对SRM的数据发送任务
            string targetSystem = "SRM";
            string methodCode = "LES-SRM-005";
            string keyValue = materialPullingOrderInfo.OrderNo;
            stringBuilder.AppendFormat(CommonBLL.GetCreateOutboundLogSql(targetSystem, srmPullingOrderInfo.LogFid.GetValueOrDefault(), methodCode, keyValue, loginUser));
            return stringBuilder.ToString();
        }
        #endregion

        #region JisSrmPullOrder
        /// <summary>
        /// /JisSrmPullOrder
        /// </summary>
        /// <param name="materialPullingOrderInfo"></param>
        /// <param name="partsStockInfos"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static string CreateJisSrmPullOrderSql(MaterialPullingOrderInfo materialPullingOrderInfo, string loginUser)
        {
            StringBuilder @string = new StringBuilder();

            #region SrmJisPullOrderInfo
            ///SrmJisPullOrderInfo对象
            SrmJisPullOrderInfo srmJisPullOrderInfo = SrmJisPullOrderBLL.CreateSrmJisPullOrderInfo(loginUser);
            ///MaterialPullingOrderInfo-->SrmJisPullOrderInfo
            SrmJisPullOrderBLL.GetSrmJisPullOrderByMaterial(materialPullingOrderInfo, ref srmJisPullOrderInfo);
            ///CreateSrmJisPullOrderSql
            @string.AppendLine(SrmJisPullOrderDAL.GetInsertSql(srmJisPullOrderInfo));
            #endregion

            #region  SrmJisPullOrderDetails         
            ///行号
            int rowNo = 0;
            foreach (MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo in materialPullingOrderInfo.MaterialPullingOrderDetailInfos)
            {
                rowNo += 1;
                #region SrmPullingOrderDetailInfo
                ///SrmJisPullOrderDetailInfo 对象
                SrmJisPullOrderDetailInfo srmJisPullOrderDetailInfo = SrmJisPullOrderDetailBLL.CreateSrmJisPullOrderDetailInfo(loginUser);
                ///SrmJisPullOrderInfo-->SrmJisPullOrderDetailInfo
                SrmJisPullOrderDetailBLL.GetSrmJisPullOrderDetailByOrder(srmJisPullOrderInfo,ref srmJisPullOrderDetailInfo);
                ///ROW_NO
                srmJisPullOrderDetailInfo.RowNo = rowNo;
                ///SrmJisPullOrderInfo-->SrmJisPullOrderDetailInfo
                SrmJisPullOrderDetailBLL.GetSrmJisPullOrderDetailByMaterial(materialPullingOrderDetailInfo, ref srmJisPullOrderDetailInfo);              
                ///CreateSrmJisPullOrderDetailSql
                @string.AppendLine(SrmJisPullOrderDetailDAL.GetInsertSql(srmJisPullOrderDetailInfo));
                #endregion
            }
            #endregion
            #region Send Data
            ///并生成对SRM的数据发送任务
            string targetSystem = "SRM";
            string methodCode = "LES-SRM-004";
            string keyValue = materialPullingOrderInfo.OrderNo;
            @string.AppendFormat(CommonBLL.GetCreateOutboundLogSql(targetSystem, srmJisPullOrderInfo.LogFid.GetValueOrDefault(), methodCode, keyValue, loginUser));
            #endregion
            return @string.ToString();
        }
        #endregion

        #region Create Info
        /// <summary>
        /// CreateMaterialPullingOrderInfo
        /// </summary>
        /// <returns></returns>
        public static MaterialPullingOrderInfo CreateMaterialPullingOrderInfo()
        {
            MaterialPullingOrderInfo materialPullingOrderInfo = new MaterialPullingOrderInfo
            {
                MaterialPullingOrderDetailInfos = new List<MaterialPullingOrderDetailInfo>()
            };
            return materialPullingOrderInfo;
        }
        /// <summary>
        /// CreateMaterialPullingOrderDetailInfo
        /// </summary>
        /// <returns></returns>
        public static MaterialPullingOrderDetailInfo CreateMaterialPullingOrderDetailInfo()
        {
            return new MaterialPullingOrderDetailInfo();
        }
        /// <summary>
        /// MaterialPullingOrderInfo -> TranDetailsInfo
        /// </summary>
        /// <param name="materialPullingOrderInfo"></param>
        /// <param name="tranDetailsInfo"></param>
        private static void GetTranDetailsInfo(MaterialPullingOrderInfo materialPullingOrderInfo, ref TranDetailsInfo tranDetailsInfo)
        {
            if (materialPullingOrderInfo == null) return;
            tranDetailsInfo.TranNo = materialPullingOrderInfo.OrderNo;
            tranDetailsInfo.Plant = materialPullingOrderInfo.Plant;
            tranDetailsInfo.BoxParts = materialPullingOrderInfo.PartBoxCode;
            tranDetailsInfo.RequiredDate = materialPullingOrderInfo.PlanDeliveryTime;
            tranDetailsInfo.RunsheetNo = materialPullingOrderInfo.OrderNo;
        }
        /// <summary>
        /// MaterialPullingOrderDetailInfo -> TranDetailsInfo
        /// </summary>
        /// <param name="materialPullingOrderDetailInfo"></param>
        /// <param name="tranDetailsInfo"></param>
        private static void GetTranDetailsInfo(decimal matchedQty, ref TranDetailsInfo tranDetailsInfo)
        {
            if (tranDetailsInfo.Package.GetValueOrDefault() == 0) return;
            tranDetailsInfo.BoxNum = Math.Ceiling(matchedQty / tranDetailsInfo.Package.GetValueOrDefault());
            tranDetailsInfo.Num = matchedQty;
            tranDetailsInfo.ActualPackageQty = Convert.ToInt32(Math.Ceiling(matchedQty / tranDetailsInfo.Package.GetValueOrDefault()));
            tranDetailsInfo.ActualQty = matchedQty;
            tranDetailsInfo.RequiredPackageQty = Convert.ToInt32(Math.Ceiling(matchedQty / tranDetailsInfo.Package.GetValueOrDefault()));
            tranDetailsInfo.RequiredQty = matchedQty;
        }
        /// <summary>
        /// StocksInfo -> MaterialPullingOrderDetailInfo
        /// </summary>
        /// <param name="stocksInfo"></param>
        /// <param name="materialPullingOrderDetailInfo"></param>
        private static void GetMaterialPullingOrderDetailInfo(StocksInfo stocksInfo, ref MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo)
        {
            if (stocksInfo == null) return;
            materialPullingOrderDetailInfo.PackageModel = stocksInfo.PackageModel;
            materialPullingOrderDetailInfo.PackageQty = stocksInfo.Package.GetValueOrDefault();
            materialPullingOrderDetailInfo.SourceDloc = stocksInfo.Dloc;
            materialPullingOrderDetailInfo.SourceWmNo = stocksInfo.WmNo;
            materialPullingOrderDetailInfo.SourceZoneNo = stocksInfo.ZoneNo;
        }
        #endregion

        #region Receive
        /// <summary>
        /// MaterialPullingOrderInfo -> ReceiveInfo
        /// </summary>
        /// <param name="materialPullingOrderInfo"></param>
        /// <returns></returns>
        private static void GetReceiveInfo(MaterialPullingOrderInfo materialPullingOrderInfo, ref ReceiveInfo receiveInfo)
        {
            ///
            receiveInfo.Fid = Guid.NewGuid();
            ///ORDER_NO,1
            receiveInfo.ReceiveNo = materialPullingOrderInfo.OrderNo;
            ///PLANT,2
            receiveInfo.Plant = materialPullingOrderInfo.Plant;
            ///SUPPLIER_NUM,3
            receiveInfo.SupplierNum = materialPullingOrderInfo.SupplierNum;
            ///
            receiveInfo.SupplierType = materialPullingOrderInfo.SupplierType;
            ///
            //receiveInfo.SourceWmNo = materialPullingOrderInfo.SourceWmNo;
            /////SOURCE_ZONE_NO,4
            //receiveInfo.SourceZoneNo = materialPullingOrderInfo.SourceZoneNo;
            ///
            receiveInfo.WmNo = materialPullingOrderInfo.TargetWmNo;
            ///
            receiveInfo.ZoneNo = materialPullingOrderInfo.TargetZoneNo;
            ///KEEPER,5
            receiveInfo.BookKeeper = materialPullingOrderInfo.Keeper;
            ///DOCK,7
            receiveInfo.Dock = materialPullingOrderInfo.TargetDock;
            ///PUBLISH_TIME,9
            receiveInfo.SendTime = materialPullingOrderInfo.PublishTime;
            ///PART_BOX_CODE,10
            //receiveInfo.PartBoxCode = materialPullingOrderInfo.PartBoxCode;
            /////PART_BOX_NAME,11
            //receiveInfo.PartBoxName = materialPullingOrderInfo.PartBoxName;
            /////PULL_MODE
            //receiveInfo.PullMode = materialPullingOrderInfo.PullMode;
            ///RECEIVE_TYPE，入库类型
            switch (materialPullingOrderInfo.PullMode)
            {
                case (int)PullModeConstants.PurchaseOrder:
                    switch (materialPullingOrderInfo.OrderType)
                    {
                        case (int)SapPurchaseOrderTypeConstants.ReservationOrder: receiveInfo.ReceiveType = (int)InboundTypeConstants.ReserveInbound; break;
                        default: receiveInfo.ReceiveType = (int)InboundTypeConstants.PurchaseOrder; break;
                    }
                    break;
                default: receiveInfo.ReceiveType = (int)InboundTypeConstants.NormalInbound; break;
            }
            ///
            //receiveInfo.Status = (int)WmmOrderStatusConstants.Published;
            ///
            //receiveInfo.Route = materialPullingOrderInfo.Route;
            /////
            //receiveInfo.InspectionFlag = materialPullingOrderInfo.InspectFlag;
            /////PLAN_SHIPPING_TIME,16
            //receiveInfo.PlanShippingTime = materialPullingOrderInfo.PlanShippingTime;
            /////PLAN_DELIVERY_TIME,17
            //receiveInfo.PlanDeliveryTime = materialPullingOrderInfo.PlanDeliveryTime;
            ///REMARK,18
            receiveInfo.Comments = materialPullingOrderInfo.Comments;
            ///
            receiveInfo.AsnNo = materialPullingOrderInfo.OrderNo;
            ///拉动单号
            receiveInfo.RunsheetNo = materialPullingOrderInfo.RunsheetNo;
            ///
            //receiveInfo.PullMode = materialPullingOrderInfo.PullMode;
            /////入库单创建来源类型
            //receiveInfo.SourceCreateType = materialPullingOrderInfo.SourceCreateType;
        }
        /// <summary>
        /// ReceiveInfo -> MaterialPullingOrderInfo
        /// </summary>
        /// <param name="receiveInfo"></param>
        /// <param name="materialPullingOrderInfo"></param>
        public static void GetMaterialPullingOrderInfo(ReceiveInfo receiveInfo, ref MaterialPullingOrderInfo materialPullingOrderInfo)
        {
            ///ORDER_NO,1
            materialPullingOrderInfo.OrderNo = receiveInfo.ReceiveNo;
            ///PLANT,2
            materialPullingOrderInfo.Plant = receiveInfo.Plant;
            ///SUPPLIER_NUM,3
            materialPullingOrderInfo.SupplierNum = receiveInfo.SupplierNum;
            ///
            materialPullingOrderInfo.SupplierType = receiveInfo.SupplierType.GetValueOrDefault();
            ///
            //materialPullingOrderInfo.SourceWmNo = receiveInfo.SourceWmNo;
            /////SOURCE_ZONE_NO,4
            //materialPullingOrderInfo.SourceZoneNo = receiveInfo.SourceZoneNo;
            ///
            materialPullingOrderInfo.TargetWmNo = receiveInfo.WmNo;
            ///TARGET_ZONE_NO,6
            materialPullingOrderInfo.TargetZoneNo = receiveInfo.ZoneNo;
            ///KEEPER,5
            materialPullingOrderInfo.Keeper = receiveInfo.BookKeeper;
            ///PUBLISH_TIME,9
            materialPullingOrderInfo.PublishTime = receiveInfo.SendTime;
            ///PART_BOX_CODE,10
            //materialPullingOrderInfo.PartBoxCode = receiveInfo.PartBoxCode;
            /////PART_BOX_NAME,11
            //materialPullingOrderInfo.PartBoxName = receiveInfo.PartBoxName;
            /////
            //materialPullingOrderInfo.Route = receiveInfo.Route;
            /////
            //materialPullingOrderInfo.InspectFlag = receiveInfo.InspectionFlag;
            /////PLAN_SHIPPING_TIME,16
            //materialPullingOrderInfo.PlanShippingTime = receiveInfo.PlanShippingTime;
            /////PLAN_DELIVERY_TIME,17
            //materialPullingOrderInfo.PlanDeliveryTime = receiveInfo.PlanDeliveryTime;
            ///REMARK,18
            materialPullingOrderInfo.Comments = receiveInfo.Comments;
            ///
            //materialPullingOrderInfo.PullMode = receiveInfo.PullMode.GetValueOrDefault();
            ///DOCK,7
            materialPullingOrderInfo.TargetDock = receiveInfo.Dock;
        }
        /// <summary>
        /// MaterialPullingOrderInfo -> ReceiveDetailInfo
        /// </summary>
        /// <param name="materialPullingOrderInfo"></param>
        /// <returns></returns>
        private static List<ReceiveDetailInfo> GetReceiveDetailInfos(MaterialPullingOrderInfo materialPullingOrderInfo, List<PartsStockInfo> partsStockInfos, Guid receiveFid)
        {
            List<ReceiveDetailInfo> receiveDetailInfos = new List<ReceiveDetailInfo>();
            int rowno = 0;
            foreach (MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo in materialPullingOrderInfo.MaterialPullingOrderDetailInfos)
            {
                ///根据物料号、供应商、仓库、存储区获取物料仓储信息，并赋予单据明细中的目标库位
                PartsStockInfo partsStockInfo = partsStockInfos.FirstOrDefault(d =>
                   d.PartNo == materialPullingOrderDetailInfo.PartNo &&
                   d.SupplierNum == materialPullingOrderDetailInfo.SupplierNum &&
                   d.WmNo == materialPullingOrderDetailInfo.TargetWmNo &&
                   d.ZoneNo == materialPullingOrderDetailInfo.TargetZoneNo);

                ReceiveDetailInfo receiveDetailInfo = new ReceiveDetailInfo();
                ///
                GetReceiveDetailInfo(materialPullingOrderInfo, ref receiveDetailInfo);
                ///
                GetReceiveDetailInfo(materialPullingOrderDetailInfo, ref receiveDetailInfo);
                ///
                GetReceiveDetailInfo(partsStockInfo, ref receiveDetailInfo);
                ///
                receiveDetailInfo.ReceiveFid = receiveFid;
                ///行号
                receiveDetailInfo.RowNo = ++rowno;
                ///
                receiveDetailInfos.Add(receiveDetailInfo);
            }
            return receiveDetailInfos;
        }
        /// <summary>
        /// 转换-BackMaterialPullingOrderDetailInfos
        /// </summary>
        /// <param name="receiveDetailInfos"></param>
        /// <returns></returns>
        public static List<MaterialPullingOrderDetailInfo> GetMaterialPullingOrderDetailInfos(List<ReceiveDetailInfo> receiveDetailInfos)
        {
            List<MaterialPullingOrderDetailInfo> materialPullingOrderDetailInfos = new List<MaterialPullingOrderDetailInfo>();
            foreach (ReceiveDetailInfo receiveDetailInfo in receiveDetailInfos)
            {
                ///
                MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo = CreateMaterialPullingOrderDetailInfo();
                ///
                GetMaterialPullingOrderDetailInfo(receiveDetailInfo, ref materialPullingOrderDetailInfo);
                ///
                materialPullingOrderDetailInfos.Add(materialPullingOrderDetailInfo);
            }
            return materialPullingOrderDetailInfos;
        }
        /// <summary>
        /// MaterialPullingOrderInfo->ReceiveDetailInfo
        /// </summary>
        /// <param name="materialPullingOrderInfo"></param>
        /// <param name="receiveDetailInfo"></param>
        private static void GetReceiveDetailInfo(MaterialPullingOrderInfo materialPullingOrderInfo, ref ReceiveDetailInfo receiveDetailInfo)
        {
            ///
            receiveDetailInfo.TranNo = materialPullingOrderInfo.OrderNo;
            ///
            receiveDetailInfo.Plant = materialPullingOrderInfo.Plant;
            ///
            receiveDetailInfo.AssemblyLine = materialPullingOrderInfo.AssemblyLine;
            ///
            receiveDetailInfo.WmNo = materialPullingOrderInfo.TargetWmNo;
            ///
            receiveDetailInfo.ZoneNo = materialPullingOrderInfo.TargetZoneNo;
            ///
            receiveDetailInfo.TargetWm = materialPullingOrderInfo.TargetWmNo;
            ///
            receiveDetailInfo.TargetZone = materialPullingOrderInfo.TargetZoneNo;
            ///
            receiveDetailInfo.Dock = materialPullingOrderInfo.TargetDock;
            ///
            receiveDetailInfo.BoxParts = materialPullingOrderInfo.PartBoxCode;
        }
        /// <summary>
        /// MaterialPullingOrderDetailInfo->ReceiveDetailInfo
        /// </summary>
        /// <param name="materialPullingOrderDetailInfo"></param>
        /// <param name="receiveDetailInfo"></param>
        private static void GetReceiveDetailInfo(MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo, ref ReceiveDetailInfo receiveDetailInfo)
        {
            ///
            receiveDetailInfo.PartNo = materialPullingOrderDetailInfo.PartNo;
            ///
            receiveDetailInfo.PartCname = materialPullingOrderDetailInfo.PartCname;
            ///
            receiveDetailInfo.PartEname = materialPullingOrderDetailInfo.PartEname;
            ///
            receiveDetailInfo.PackageModel = materialPullingOrderDetailInfo.PackageModel;
            ///
            receiveDetailInfo.Package = materialPullingOrderDetailInfo.PackageQty;
            ///
            receiveDetailInfo.RequiredBoxNum = materialPullingOrderDetailInfo.RequirePackageQty;
            ///
            receiveDetailInfo.RequiredQty = materialPullingOrderDetailInfo.RequirePartQty;
            ///
            receiveDetailInfo.MeasuringUnitNo = materialPullingOrderDetailInfo.Uom;
            ///
            receiveDetailInfo.SupplierNum = materialPullingOrderDetailInfo.SupplierNum;
            ///
            if (string.IsNullOrEmpty(receiveDetailInfo.Plant))
                receiveDetailInfo.Plant = materialPullingOrderDetailInfo.Plant;
            ///
            receiveDetailInfo.Dock = materialPullingOrderDetailInfo.TargetDock;
            ///
            receiveDetailInfo.Dloc = materialPullingOrderDetailInfo.TargetDloc;
            ///
            receiveDetailInfo.BoxParts = materialPullingOrderDetailInfo.PartBoxCode;
            ///
            receiveDetailInfo.RunsheetNo = materialPullingOrderDetailInfo.RunsheetNo;
            ///
            receiveDetailInfo.Comments = materialPullingOrderDetailInfo.Comments;
            ///
            receiveDetailInfo.TargetDloc = materialPullingOrderDetailInfo.TargetDloc;
        }
        /// <summary>
        /// ReceiveDetailInfo -> MaterialPullingOrderDetailInfo
        /// </summary>
        /// <param name="receiveDetailInfo"></param>
        /// <param name="materialPullingOrderDetailInfo"></param>
        private static void GetMaterialPullingOrderDetailInfo(ReceiveDetailInfo receiveDetailInfo, ref MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo)
        {
            ///物料号
            materialPullingOrderDetailInfo.PartNo = receiveDetailInfo.PartNo;
            ///物料描述
            materialPullingOrderDetailInfo.PartCname = receiveDetailInfo.PartCname;
            ///包装型号
            materialPullingOrderDetailInfo.PackageModel = receiveDetailInfo.PackageModel;
            ///单包装数量
            materialPullingOrderDetailInfo.PackageQty = receiveDetailInfo.Package.GetValueOrDefault();
            ///需求数量
            materialPullingOrderDetailInfo.RequirePartQty = receiveDetailInfo.RequiredQty.GetValueOrDefault();
            ///
            materialPullingOrderDetailInfo.RequirePackageQty = receiveDetailInfo.RequiredBoxNum.GetValueOrDefault();
            ///计量单位
            materialPullingOrderDetailInfo.Uom = receiveDetailInfo.MeasuringUnitNo;
            ///供应商
            materialPullingOrderDetailInfo.SupplierNum = receiveDetailInfo.SupplierNum;
            ///工厂
            materialPullingOrderDetailInfo.Plant = receiveDetailInfo.Plant;
            ///道口
            materialPullingOrderDetailInfo.TargetDock = receiveDetailInfo.Dock;
            ///仓库
            materialPullingOrderDetailInfo.TargetWmNo = receiveDetailInfo.TargetWm;
            ///存储区
            materialPullingOrderDetailInfo.TargetZoneNo = receiveDetailInfo.TargetZone;
            ///库位
            materialPullingOrderDetailInfo.TargetDloc = receiveDetailInfo.TargetDloc;
            ///零件类
            materialPullingOrderDetailInfo.PartBoxCode = receiveDetailInfo.BoxParts;
            ///入库单号
            materialPullingOrderDetailInfo.OrderNo = receiveDetailInfo.TranNo;
            ///备注
            materialPullingOrderDetailInfo.Comments = receiveDetailInfo.Comments;
            ///
            materialPullingOrderDetailInfo.PartEname = receiveDetailInfo.PartEname;
        }
        /// <summary>
        /// PartsStockInfo->ReceiveDetailInfo
        /// </summary>
        /// <param name="partsStockInfo"></param>
        /// <param name="receiveDetailInfo"></param>
        private static void GetReceiveDetailInfo(PartsStockInfo partsStockInfo, ref ReceiveDetailInfo receiveDetailInfo)
        {
            ///优先从参数获取，在参数传入的属性为空时从物料仓储信息中获取
            if (partsStockInfo == null) return;
            ///中文描述
            receiveDetailInfo.PartCname = string.IsNullOrEmpty(receiveDetailInfo.PartCname) ? partsStockInfo.PartCname : receiveDetailInfo.PartCname;
            ///英文描述
            receiveDetailInfo.PartEname = string.IsNullOrEmpty(receiveDetailInfo.PartEname) ? partsStockInfo.PartEname : receiveDetailInfo.PartEname;
            ///单包装数
            receiveDetailInfo.Package = receiveDetailInfo.Package == 0 ? partsStockInfo.Package.GetValueOrDefault() : receiveDetailInfo.Package;
            ///包装型号
            receiveDetailInfo.PackageModel = string.IsNullOrEmpty(receiveDetailInfo.PackageModel) ? partsStockInfo.PackageModel : receiveDetailInfo.PackageModel;
            ///目标库位
            receiveDetailInfo.TargetDloc = string.IsNullOrEmpty(receiveDetailInfo.TargetDloc) ? partsStockInfo.Dloc : receiveDetailInfo.TargetDloc;
            ///英文描述
            receiveDetailInfo.MeasuringUnitNo = string.IsNullOrEmpty(receiveDetailInfo.MeasuringUnitNo) ? partsStockInfo.PartUnits : receiveDetailInfo.MeasuringUnitNo;
        }
        #endregion

        #region Barcode
        /// <summary>
        /// 拉动仓储单据衔接-标签信息
        /// </summary>
        /// <param name="PartNo"></param>
        /// <param name="PackageModel"></param>
        /// <param name="Package"></param>
        /// <param name="AsnRunsheetNo"></param>
        private static string GetCreateBarcodeSql(ReceiveDetailInfo receiveDetailInfo, List<BarcodeInfo> barcodeInfos, PartsStockInfo partsStockInfo, SupplierInfo supplierInfo, string loginUser)
        {
            ///如果之前已经生成过标签，后来单据被取消，则需要以差异数量生成标签
            int pPackageQty = barcodeInfos.Count;
            ///
            StringBuilder stringBuilder = new StringBuilder();
            ///包装数
            int packageQty = receiveDetailInfo.RequiredBoxNum.GetValueOrDefault() - pPackageQty;
            ///单包装数据为空或为零时无法生成条码
            if (receiveDetailInfo.Package.GetValueOrDefault() == 0)
            {
                if (receiveDetailInfo.RequiredQty.GetValueOrDefault() == 0)
                {
                    receiveDetailInfo.RequiredQty = packageQty;
                    receiveDetailInfo.Package = 1;
                }
                else
                    receiveDetailInfo.Package = Math.Ceiling(receiveDetailInfo.RequiredQty.GetValueOrDefault() / receiveDetailInfo.RequiredBoxNum.GetValueOrDefault());
            }

            ///最后一包装物料数量
            decimal lastPackagePartQty = receiveDetailInfo.RequiredQty.GetValueOrDefault() % receiveDetailInfo.Package.GetValueOrDefault();
            ///当有散件包装时整包数量应该少一箱
            if (lastPackagePartQty > 0)
                packageQty--;
            ///如果当前需求包装数量比零小，说明需要将之前已生成的标签进行作废
            if (packageQty < 0)
            {
                ///将多余标签作废
                for (int i = 0; i < 0 - packageQty; i++)
                {
                    BarcodeInfo barcodeInfo = barcodeInfos.FirstOrDefault();
                    if (barcodeInfo == null) continue;
                    stringBuilder.AppendLine("update [LES].[TT_WMM_BARCODE] set " +
                                                            "[BARCODE_STATUS] = " + (int)BarcodeStatusConstants.Invalid + "," +
                                                            "[MODIFY_USER] = N'" + loginUser + "'," +
                                                            "[MODIFY_DATE] = GETDATE() where " +
                                                            "[ID] = " + barcodeInfo.Id + ";");
                    ///
                    BarcodeStatusInfo barcodeStatusInfo = BarcodeStatusBLL.CreateBarcodeStatusInfo(loginUser);
                    ///
                    BarcodeStatusBLL.GetBarcodeStatusInfo(barcodeInfo, ref barcodeStatusInfo);
                    ///
                    stringBuilder.AppendLine(BarcodeStatusDAL.GetInsertSql(barcodeStatusInfo));
                    barcodeInfos.Remove(barcodeInfo);
                }
                ///用此控制不生成新的标签
                packageQty = 0;
            }
            ///如果没有拉动单号，则说明是手工创建的入库单，为了后续处理方便将拉动单号以入库单号替代
            if (string.IsNullOrEmpty(receiveDetailInfo.RunsheetNo))
                receiveDetailInfo.RunsheetNo = receiveDetailInfo.TranNo;

            ///若入库单明细上的来源仓库存储区库位不为空时，则标签上的库存地点信息从来源进行获取
            if (!string.IsNullOrEmpty(receiveDetailInfo.WmNo))
                receiveDetailInfo.TargetWm = receiveDetailInfo.WmNo;
            if (!string.IsNullOrEmpty(receiveDetailInfo.ZoneNo))
                receiveDetailInfo.TargetZone = receiveDetailInfo.ZoneNo;
            if (!string.IsNullOrEmpty(receiveDetailInfo.Dloc))
                receiveDetailInfo.TargetDloc = receiveDetailInfo.Dloc;

            for (int i = 0; i < packageQty; i++)
            {
                BarcodeInfo barcodeInfo = BarcodeBLL.CreateBarcodeInfo(loginUser);
                ///
                BarcodeBLL.GetBarcodeInfo(receiveDetailInfo, ref barcodeInfo);
                ///PartsStockInfo -> BarcodeInfo
                BarcodeBLL.GetBarcodeInfo(partsStockInfo, ref barcodeInfo);
                ///SupplierInfo -> BarcodeInfo
                BarcodeBLL.GetBarcodeInfo(supplierInfo, ref barcodeInfo);
                ///
                barcodeInfo.BarcodeData = new SeqDefineDAL().GetCurrentCode("BARCODE_DATA", receiveDetailInfo.TranNo);
                barcodeInfo.CurrentQty = receiveDetailInfo.Package;
                ///
                stringBuilder.AppendLine(BarcodeDAL.GetInsertSql(barcodeInfo));
                ///
                BarcodeStatusInfo barcodeStatusInfo = BarcodeStatusBLL.CreateBarcodeStatusInfo(loginUser);
                ///
                BarcodeStatusBLL.GetBarcodeStatusInfo(barcodeInfo, ref barcodeStatusInfo);
                ///
                stringBuilder.AppendLine(BarcodeStatusDAL.GetInsertSql(barcodeStatusInfo));
            }
            ///最后一包装物料数量大于零
            if (lastPackagePartQty > 0)
            {
                BarcodeInfo barcodeInfo = BarcodeBLL.CreateBarcodeInfo(loginUser);
                ///
                BarcodeBLL.GetBarcodeInfo(receiveDetailInfo, ref barcodeInfo);
                ///PartsStockInfo -> BarcodeInfo
                BarcodeBLL.GetBarcodeInfo(partsStockInfo, ref barcodeInfo);
                ///SupplierInfo -> BarcodeInfo
                BarcodeBLL.GetBarcodeInfo(supplierInfo, ref barcodeInfo);
                ///
                barcodeInfo.BarcodeData = new SeqDefineDAL().GetCurrentCode("BARCODE_DATA", receiveDetailInfo.TranNo);
                barcodeInfo.CurrentQty = lastPackagePartQty;
                ///
                stringBuilder.AppendLine(BarcodeDAL.GetInsertSql(barcodeInfo));
                ///
                BarcodeStatusInfo barcodeStatusInfo = BarcodeStatusBLL.CreateBarcodeStatusInfo(loginUser);
                ///
                BarcodeStatusBLL.GetBarcodeStatusInfo(barcodeInfo, ref barcodeStatusInfo);
                ///
                stringBuilder.AppendLine(BarcodeStatusDAL.GetInsertSql(barcodeStatusInfo));
            }
            ///由于发布后取消再发布，需要更新已生成的标签信息
            foreach (var barcodeInfo in barcodeInfos)
            {
                stringBuilder.AppendLine("update [LES].[TT_WMM_BARCODE] set " +
                    "[PART_NO] = N'" + receiveDetailInfo.PartNo + "'," +
                    "[PART_CNAME] = N'" + receiveDetailInfo.PartCname + "'," +
                    "[PART_NICKNAME] = " + (partsStockInfo == null ? "NULL" : "N'" + partsStockInfo.PartNickname + "'") + "," +
                    "[PACKAGE_MODEL] = N'" + receiveDetailInfo.PackageModel + "'," +
                    "[PACKAGE] = " + receiveDetailInfo.Package + "," +
                    "[CURRENT_QTY] = " + receiveDetailInfo.Package + "," +
                    "[IDENTIFY_PART_NO] = N'" + receiveDetailInfo.IdentifyPartNo + "'," +
                    "[MEASURING_UNIT_NO] = N'" + receiveDetailInfo.MeasuringUnitNo + "'," +
                    "[SUPPLIER_NUM] = N'" + receiveDetailInfo.SupplierNum + "'," +
                    "[SUPPLIER_NAME] = " + (supplierInfo == null ? "NULL" : "N'" + supplierInfo.SupplierName + "'") + "," +
                    "[SUPPLIER_SNAME] = " + (supplierInfo == null ? "NULL" : "N'" + supplierInfo.SupplierSname + "'") + "," +
                    "[PLANT] = N'" + receiveDetailInfo.Plant + "'," +
                    "[ASSEMBLY_LINE] = N'" + receiveDetailInfo.AssemblyLine + "'," +
                    "[LOCATION] = N'" + string.Empty + "'," +
                    "[DOCK] = N'" + receiveDetailInfo.Dock + "'," +
                    "[WM_NO] = N'" + receiveDetailInfo.TargetWm + "'," +
                    "[ZONE_NO] = N'" + receiveDetailInfo.TargetZone + "'," +
                    "[DLOC] = N'" + receiveDetailInfo.TargetDloc + "'," +
                    "[BOX_PARTS] = N'" + receiveDetailInfo.BoxParts + "'," +
                    "[RUNSHEET_NO] = N'" + receiveDetailInfo.RunsheetNo + "'," +
                    "[ASN_RUNSHEET_NO] = N'" + receiveDetailInfo.TranNo + "'," +
                    "[PICKUP_SEQ_NO] = " + (receiveDetailInfo.PickupSeqNo == null ? "NULL" : "" + receiveDetailInfo.PickupSeqNo.GetValueOrDefault() + "") + "," +
                    "[RDC_DLOC] = N'" + receiveDetailInfo.RdcDloc + "'," +
                    "[COMMENTS] = N'" + receiveDetailInfo.Comments + "'," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE()," +
                    "[PACKAGE_LENGTH] = " + receiveDetailInfo.PackageLength.GetValueOrDefault() + "," +
                    "[PACKAGE_WIDTH] = " + receiveDetailInfo.PackageWidth.GetValueOrDefault() + "," +
                    "[PACKAGE_HEIGHT] = " + receiveDetailInfo.PackageHeight.GetValueOrDefault() + "," +
                    "[PERPACKAGE_GROSS_WEIGHT] = " + receiveDetailInfo.PerpackageGrossWeight.GetValueOrDefault() + "," +
                    "[SUPERMARKET_ADDRESS] = " + (partsStockInfo == null ? "NULL" : "N'" + partsStockInfo.SupperZoneDloc + "'") + "," +
                    "[LINE_POSITION] = " + (partsStockInfo == null ? "NULL" : "N'" + partsStockInfo.LineSiteDloc + "'") + "," +
                    "[PACKAGE_VOLUME] = " + (receiveDetailInfo.PackageVolume == null ? "NULL" : "" + receiveDetailInfo.PackageVolume.GetValueOrDefault() + "") + " where " +
                    "[ID] = " + barcodeInfo.Id + ";");
                ///
                BarcodeStatusInfo barcodeStatusInfo = BarcodeStatusBLL.CreateBarcodeStatusInfo(loginUser);
                ///
                BarcodeStatusBLL.GetBarcodeStatusInfo(barcodeInfo, ref barcodeStatusInfo);
                ///
                stringBuilder.AppendLine(BarcodeStatusDAL.GetInsertSql(barcodeStatusInfo));
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// 拉动仓储单据衔接-标签信息
        /// </summary>
        /// <param name="outputDetailInfo"></param>
        /// <param name="partsStockInfo"></param>
        /// <param name="supplierInfo"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static string GetCreateBarcodeSql(OutputDetailInfo outputDetailInfo, PartsStockInfo partsStockInfo, SupplierInfo supplierInfo, string loginUser)
        {
            StringBuilder @string = new StringBuilder();
            ///单包装数据为空或为零时无法生成条码
            if (outputDetailInfo.Package.GetValueOrDefault() == 0) return string.Empty;
            ///包装数
            decimal packageQty = outputDetailInfo.RequiredQty.GetValueOrDefault() / outputDetailInfo.Package.GetValueOrDefault();
            ///最后一包装物料数量
            decimal lastPackagePartQty = outputDetailInfo.RequiredQty.GetValueOrDefault() % outputDetailInfo.Package.GetValueOrDefault();
            ///如果没有拉动单号，则说明是手工创建的入库单，为了后续处理方便将拉动单号以入库单号替代
            if (string.IsNullOrEmpty(outputDetailInfo.RunsheetNo))
                outputDetailInfo.RunsheetNo = outputDetailInfo.TranNo;

            for (int i = 0; i < packageQty; i++)
            {
                BarcodeInfo barcodeInfo = BarcodeBLL.CreateBarcodeInfo(loginUser);
                ///
                BarcodeBLL.GetBarcodeInfo(outputDetailInfo, ref barcodeInfo);
                ///
                BarcodeBLL.GetBarcodeInfo(partsStockInfo, ref barcodeInfo);
                ///
                BarcodeBLL.GetBarcodeInfo(supplierInfo, ref barcodeInfo);
                ///
                barcodeInfo.BarcodeData = new SeqDefineDAL().GetCurrentCode("BARCODE_DATA", outputDetailInfo.TranNo);
                barcodeInfo.CurrentQty = outputDetailInfo.Package;
                @string.AppendLine(BarcodeDAL.GetInsertSql(barcodeInfo));
            }
            ///最后一包装物料数量大于零
            if (lastPackagePartQty > 0)
            {
                BarcodeInfo barcodeInfo = BarcodeBLL.CreateBarcodeInfo(loginUser);
                ///
                BarcodeBLL.GetBarcodeInfo(outputDetailInfo, ref barcodeInfo);
                ///
                BarcodeBLL.GetBarcodeInfo(partsStockInfo, ref barcodeInfo);
                ///
                BarcodeBLL.GetBarcodeInfo(supplierInfo, ref barcodeInfo);
                ///
                barcodeInfo.BarcodeData = new SeqDefineDAL().GetCurrentCode("BARCODE_DATA", outputDetailInfo.TranNo);
                barcodeInfo.CurrentQty = lastPackagePartQty;
                @string.AppendLine(BarcodeDAL.GetInsertSql(barcodeInfo));
            }
            return @string.ToString();
        }
        /// <summary>
        /// 拉动仓储单据衔接-标签信息
        /// </summary>
        /// <param name="receiveDetailInfos"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static string GetCreateBarcodesSql(List<ReceiveDetailInfo> receiveDetailInfos, List<BarcodeInfo> barcodeInfos, string loginUser)
        {
            StringBuilder stringBuilder = new StringBuilder();
            ///物料仓储信息 
            List<PartsStockInfo> partsStockInfos = new PartsStockDAL().GetListForInterfaceDataSync(receiveDetailInfos.Select(d => d.PartNo).ToList());
            ///供应商信息
            List<SupplierInfo> supplierInfos = new SupplierDAL().GetListForInterfaceDataSync(receiveDetailInfos.Select(d => d.SupplierNum).ToList());
            ///
            foreach (var receiveDetailInfo in receiveDetailInfos)
            {
                ///PartsStockInfo
                PartsStockInfo partsStockInfo = partsStockInfos.FirstOrDefault(d => d.PartNo == receiveDetailInfo.PartNo
                && d.SupplierNum == receiveDetailInfo.SupplierNum
                && d.WmNo == receiveDetailInfo.WmNo
                && d.ZoneNo == receiveDetailInfo.ZoneNo);
                ///SupplierInfo
                SupplierInfo supplierInfo = supplierInfos.FirstOrDefault(d => d.SupplierNum == receiveDetailInfo.SupplierNum);
                ///
                List<BarcodeInfo> barcodes = barcodeInfos.Where(d => d.CreateSourceFid.GetValueOrDefault() == receiveDetailInfo.Fid.GetValueOrDefault()).ToList();
                ///
                stringBuilder.AppendFormat(GetCreateBarcodeSql(receiveDetailInfo, barcodes, partsStockInfo, supplierInfo, loginUser));
            }
            return stringBuilder.ToString();
        }
        #endregion

        #region Interface
        /// <summary>
        /// SrmDeliveryNoteInfo -> MaterialPullingOrderInfo
        /// </summary>
        /// <param name="srmDeliveryNoteInfo"></param>
        /// <param name="materialPullingOrderInfo"></param>
        public static void GetMaterialPullingOrderInfo(SrmDeliveryNoteInfo srmDeliveryNoteInfo, ref MaterialPullingOrderInfo materialPullingOrderInfo)
        {
            if (srmDeliveryNoteInfo == null) return;
            ///ORDERCODE
            materialPullingOrderInfo.OrderNo = srmDeliveryNoteInfo.Ordercode;
            ///PLANT
            materialPullingOrderInfo.Plant = srmDeliveryNoteInfo.Plant;
            ///SOURCEORDERTYPE
            ///
            switch (srmDeliveryNoteInfo.Sourceordertype.GetValueOrDefault())
            {
                ///时间窗拉动
                case (int)SrmOrderTypeConstants.Twd:
                    materialPullingOrderInfo.OrderType = (int)PullOrderTypeConstants.Pulling;
                    materialPullingOrderInfo.PullMode = (int)PullModeConstants.Twd;
                    break;
                ///计划拉动
                case (int)SrmOrderTypeConstants.Plan:
                    materialPullingOrderInfo.OrderType = (int)PullOrderTypeConstants.Pulling;
                    materialPullingOrderInfo.PullMode = (int)PullModeConstants.Plan;
                    break;
                ///预留单据
                case (int)SrmOrderTypeConstants.Reserve:
                    materialPullingOrderInfo.OrderType = (int)SapPurchaseOrderTypeConstants.ReservationOrder;
                    materialPullingOrderInfo.PullMode = (int)PullModeConstants.PurchaseOrder;
                    break;
                ///采购订单
                case (int)SrmOrderTypeConstants.Purchase:
                    materialPullingOrderInfo.OrderType = (int)SapPurchaseOrderTypeConstants.PurchaseOrder;
                    materialPullingOrderInfo.PullMode = (int)PullModeConstants.PurchaseOrder;
                    break;
            }
            ///DOCK
            materialPullingOrderInfo.TargetDock = srmDeliveryNoteInfo.Dock;
            ///PUBLISHTIME
            materialPullingOrderInfo.PublishTime = srmDeliveryNoteInfo.Publishtime;
            ///SUPPLIERCODE
            materialPullingOrderInfo.SupplierNum = srmDeliveryNoteInfo.Suppliercode;
            ///SUPPLIERNAME
            materialPullingOrderInfo.SupplierName = srmDeliveryNoteInfo.Suppliername;
            ///SOURCEZONENO，TODO:对于供应商ASN单据来说不应该有来源存储区
            materialPullingOrderInfo.SourceZoneNo = srmDeliveryNoteInfo.Sourcezoneno;
            ///TARGETZONENO
            materialPullingOrderInfo.TargetZoneNo = srmDeliveryNoteInfo.Targetzoneno;
            ///TARGETZONENO
            materialPullingOrderInfo.Keeper = srmDeliveryNoteInfo.Keeper;
            ///PLANSHIPPINGTIME
            materialPullingOrderInfo.PlanShippingTime = srmDeliveryNoteInfo.Planshippingtime;
            ///PLANDELIVERYTIME
            materialPullingOrderInfo.PlanDeliveryTime = srmDeliveryNoteInfo.Plandeliverytime;
            ///REMARK
            materialPullingOrderInfo.Comments = srmDeliveryNoteInfo.Remark;
            ///EMERGENCYFLAG
            materialPullingOrderInfo.EmergencyFlag = srmDeliveryNoteInfo.Emergencyflag;
            ///零件类代码
            materialPullingOrderInfo.PartBoxCode = null;
            ///零件类名称
            materialPullingOrderInfo.PartBoxName = null;
            ///
            materialPullingOrderInfo.Route = null;
            ///
            materialPullingOrderInfo.SourceCreateType = (int)ReceiveSourceCreateTypeConstants.Srm;
        }
        /// <summary>
        /// SrmDeliveryNoteDetailInfo -> MaterialPullingOrderDetailInfo
        /// </summary>
        /// <param name="srmDeliveryNoteDetailInfo"></param>
        /// <param name="materialPullingOrderDetailInfo"></param>
        public static void GetMaterialPullingOrderDetailInfo(SrmDeliveryNoteDetailInfo srmDeliveryNoteDetailInfo, ref MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo)
        {
            if (srmDeliveryNoteDetailInfo == null) return;
            ///SOURCEORDERCODE
            materialPullingOrderDetailInfo.RunsheetNo = srmDeliveryNoteDetailInfo.Sourceordercode;
            ///PARTNO
            materialPullingOrderDetailInfo.PartNo = srmDeliveryNoteDetailInfo.Partno;
            ///PARTCNAME
            materialPullingOrderDetailInfo.PartCname = srmDeliveryNoteDetailInfo.Partcname;
            ///PARTQTY
            materialPullingOrderDetailInfo.RequirePartQty = srmDeliveryNoteDetailInfo.Partqty.GetValueOrDefault();
            ///TARGETSLCODE
            materialPullingOrderDetailInfo.TargetDloc = srmDeliveryNoteDetailInfo.Targetslcode;
            ///PACKAGECODE
            materialPullingOrderDetailInfo.PackageModel = srmDeliveryNoteDetailInfo.Packagecode;
            ///SNP
            materialPullingOrderDetailInfo.PackageQty = srmDeliveryNoteDetailInfo.Snp.GetValueOrDefault();
            ///REMARK
            materialPullingOrderDetailInfo.Comments = srmDeliveryNoteDetailInfo.Remark;
            ///PackageQty
            if (materialPullingOrderDetailInfo.PackageQty > 0)
                materialPullingOrderDetailInfo.RequirePackageQty = Convert.ToInt32(Math.Ceiling(materialPullingOrderDetailInfo.RequirePartQty / materialPullingOrderDetailInfo.PackageQty));
        }
        /// <summary>
        /// WmsVmiAsnRunsheetInfo -> MaterialPullingOrderInfo
        /// </summary>
        /// <param name="wmsVmiAsnRunsheetInfo"></param>
        /// <param name="materialPullingOrderInfo"></param>
        public static void GetMaterialPullingOrderInfo(WmsVmiAsnRunsheetInfo wmsVmiAsnRunsheetInfo, ref MaterialPullingOrderInfo materialPullingOrderInfo)
        {
            if (wmsVmiAsnRunsheetInfo == null) return;
            ///ORDERCODE
            materialPullingOrderInfo.OrderNo = wmsVmiAsnRunsheetInfo.Ordercode;
            ///PLANT
            materialPullingOrderInfo.Plant = wmsVmiAsnRunsheetInfo.Plant;
            ///SOURCEORDERTYPE
            ///
            switch (wmsVmiAsnRunsheetInfo.Sourceordertype.GetValueOrDefault())
            {
                ///时间窗拉动
                case (int)WmsOrderTypeConstants.Twd:
                    materialPullingOrderInfo.OrderType = (int)PullOrderTypeConstants.Pulling;
                    materialPullingOrderInfo.PullMode = (int)PullModeConstants.Twd;
                    break;
                ///计划拉动
                case (int)WmsOrderTypeConstants.Plan:
                    materialPullingOrderInfo.OrderType = (int)PullOrderTypeConstants.Pulling;
                    materialPullingOrderInfo.PullMode = (int)PullModeConstants.Plan;
                    break;
                ///预留单据
                case (int)WmsOrderTypeConstants.Reserve:
                    materialPullingOrderInfo.OrderType = (int)SapPurchaseOrderTypeConstants.ReservationOrder;
                    materialPullingOrderInfo.PullMode = (int)PullModeConstants.PurchaseOrder;
                    break;
            }
            ///DOCK
            materialPullingOrderInfo.TargetDock = wmsVmiAsnRunsheetInfo.Dock;
            ///PUBLISHTIME
            materialPullingOrderInfo.PublishTime = wmsVmiAsnRunsheetInfo.Publishtime;
            ///SUPPLIERCODE
            materialPullingOrderInfo.SupplierNum = wmsVmiAsnRunsheetInfo.Suppliercode;
            ///SUPPLIERNAME
            materialPullingOrderInfo.SupplierName = wmsVmiAsnRunsheetInfo.Suppliername;
            ///SOURCEZONENO
            materialPullingOrderInfo.SourceZoneNo = wmsVmiAsnRunsheetInfo.Sourcezoneno;
            ///TARGETZONENO
            materialPullingOrderInfo.TargetZoneNo = wmsVmiAsnRunsheetInfo.Targetzoneno;
            ///TARGETZONENO
            materialPullingOrderInfo.Keeper = wmsVmiAsnRunsheetInfo.Keeper;
            ///PLANSHIPPINGTIME
            materialPullingOrderInfo.PlanShippingTime = wmsVmiAsnRunsheetInfo.Planshippingtime;
            ///PLANDELIVERYTIME
            materialPullingOrderInfo.PlanDeliveryTime = wmsVmiAsnRunsheetInfo.Plandeliverytime;
            ///REMARK
            materialPullingOrderInfo.Comments = wmsVmiAsnRunsheetInfo.Remark;
            ///EMERGENCYFLAG
            materialPullingOrderInfo.EmergencyFlag = wmsVmiAsnRunsheetInfo.Emergencyflag;
            ///零件类代码
            materialPullingOrderInfo.PartBoxCode = null;
            ///零件类名称
            materialPullingOrderInfo.PartBoxName = null;
            ///
            materialPullingOrderInfo.Route = null;
            ///
            materialPullingOrderInfo.SourceCreateType = (int)ReceiveSourceCreateTypeConstants.Wms;
        }
        /// <summary>
        /// WmsVmiAsnRunsheetDetailInfo -> MaterialPullingOrderDetailInfo
        /// </summary>
        /// <param name="wmsVmiAsnRunsheetDetailInfo"></param>
        /// <param name="materialPullingOrderDetailInfo"></param>
        public static void GetMaterialPullingOrderDetailInfo(WmsVmiAsnRunsheetDetailInfo wmsVmiAsnRunsheetDetailInfo, ref MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo)
        {
            if (wmsVmiAsnRunsheetDetailInfo == null) return;
            ///SOURCEORDERCODE
            materialPullingOrderDetailInfo.RunsheetNo = wmsVmiAsnRunsheetDetailInfo.Sourceordercode;
            ///PARTNO
            materialPullingOrderDetailInfo.PartNo = wmsVmiAsnRunsheetDetailInfo.Partno;
            ///PARTCNAME
            materialPullingOrderDetailInfo.PartCname = wmsVmiAsnRunsheetDetailInfo.Partcname;
            ///PARTQTY
            materialPullingOrderDetailInfo.RequirePartQty = wmsVmiAsnRunsheetDetailInfo.Partqty.GetValueOrDefault();
            ///TARGETSLCODE
            materialPullingOrderDetailInfo.TargetDloc = wmsVmiAsnRunsheetDetailInfo.Targetslcode;
            ///PACKAGECODE
            materialPullingOrderDetailInfo.PackageModel = wmsVmiAsnRunsheetDetailInfo.Packagecode;
            ///SNP
            materialPullingOrderDetailInfo.PackageQty = wmsVmiAsnRunsheetDetailInfo.Snp.GetValueOrDefault();
            ///REMARK
            materialPullingOrderDetailInfo.Comments = wmsVmiAsnRunsheetDetailInfo.Remark;
            ///PackageQty
            if (materialPullingOrderDetailInfo.PackageQty > 0)
                materialPullingOrderDetailInfo.RequirePackageQty = Convert.ToInt32(Math.Ceiling(materialPullingOrderDetailInfo.RequirePartQty / materialPullingOrderDetailInfo.PackageQty));
        }
        /// <summary>
        /// SupplierInfo -> MaterialPullingOrderInfo
        /// </summary>
        /// <param name="supplierInfo"></param>
        /// <param name="materialPullingOrderInfo"></param>
        public static void GetMaterialPullingOrderInfo(SupplierInfo supplierInfo, ref MaterialPullingOrderInfo materialPullingOrderInfo)
        {
            if (supplierInfo == null) return;
            ///SUPPLIER_NAME
            materialPullingOrderInfo.SupplierName = supplierInfo.SupplierName;
            ///SUPPLIER_TYPE
            materialPullingOrderInfo.SupplierType = supplierInfo.SupplierType.GetValueOrDefault();
            ///ASN_FLAG
            materialPullingOrderInfo.AsnFlag = supplierInfo.AsnFlag.GetValueOrDefault();
        }
        /// <summary>
        /// TwdPullOrderInfo -> MaterialPullingOrderInfo
        /// </summary>
        /// <param name="twdPullOrderInfo"></param>
        /// <param name="materialPullingOrderInfo"></param>
        public static void GetMaterialPullingOrderInfo(TwdPullOrderInfo twdPullOrderInfo, ref MaterialPullingOrderInfo materialPullingOrderInfo)
        {
            if (twdPullOrderInfo == null) return;
            ///拉动单号
            materialPullingOrderInfo.OrderNo = twdPullOrderInfo.OrderCode;
            ///零件类代码
            materialPullingOrderInfo.PartBoxCode = twdPullOrderInfo.PartBoxCode;
            ///零件类名称
            materialPullingOrderInfo.PartBoxName = twdPullOrderInfo.PartBoxName;
            ///工厂
            materialPullingOrderInfo.Plant = twdPullOrderInfo.Plant;
            ///车间
            materialPullingOrderInfo.Workshop = twdPullOrderInfo.Workshop;
            ///生产线
            materialPullingOrderInfo.AssemblyLine = twdPullOrderInfo.AssemblyLine;
            ///供应商
            materialPullingOrderInfo.SupplierNum = twdPullOrderInfo.SupplierNum;
            ///来源存储区
            materialPullingOrderInfo.SourceZoneNo = twdPullOrderInfo.SZoneNo;
            ///来源仓库
            materialPullingOrderInfo.SourceWmNo = twdPullOrderInfo.SWmNo;
            ///目标存储区
            materialPullingOrderInfo.TargetZoneNo = twdPullOrderInfo.TZoneNo;
            ///目标仓库
            materialPullingOrderInfo.TargetWmNo = twdPullOrderInfo.TWmNo;
            ///道口
            materialPullingOrderInfo.TargetDock = twdPullOrderInfo.Dock;
            ///送货路径
            materialPullingOrderInfo.Route = twdPullOrderInfo.RouteCode;
            ///预计到厂时间
            materialPullingOrderInfo.PlanDeliveryTime = twdPullOrderInfo.PlanDeliveryTime;
            ///发货时间
            materialPullingOrderInfo.PlanShippingTime = twdPullOrderInfo.PlanShippingTime;
            ///发布时间
            materialPullingOrderInfo.PublishTime = twdPullOrderInfo.PublishTime;
            ///ASN标识
            materialPullingOrderInfo.AsnFlag = twdPullOrderInfo.AsnFlag.GetValueOrDefault();
            ///单据类型
            materialPullingOrderInfo.OrderType = twdPullOrderInfo.OrderType.GetValueOrDefault();
            ///保管员
            materialPullingOrderInfo.Keeper = twdPullOrderInfo.Keeper;
            ///拉动方式
            materialPullingOrderInfo.PullMode = (int)PullModeConstants.Twd;
            ///拉动单号
            materialPullingOrderInfo.RunsheetNo = twdPullOrderInfo.OrderCode;
        }
        /// <summary>
        /// PcsPullOrderInfo -> MaterialPullingOrderInfo
        /// </summary>
        /// <param name="pcsPullOrderInfo"></param>
        /// <param name="materialPullingOrderInfo"></param>
        public static void GetMaterialPullingOrderInfo(PcsPullOrderInfo pcsPullOrderInfo, ref MaterialPullingOrderInfo materialPullingOrderInfo)
        {
            if (pcsPullOrderInfo == null) return;
            ///拉动单号
            materialPullingOrderInfo.OrderNo = pcsPullOrderInfo.OrderCode;
            ///零件类代码
            materialPullingOrderInfo.PartBoxCode = pcsPullOrderInfo.PartBoxCode;
            ///零件类名称
            materialPullingOrderInfo.PartBoxName = pcsPullOrderInfo.PartBoxName;
            ///工厂
            materialPullingOrderInfo.Plant = pcsPullOrderInfo.Plant;
            ///车间
            materialPullingOrderInfo.Workshop = pcsPullOrderInfo.Workshop;
            ///生产线
            materialPullingOrderInfo.AssemblyLine = pcsPullOrderInfo.AssemblyLine;
            ///供应商
            materialPullingOrderInfo.SupplierNum = pcsPullOrderInfo.SupplierNum;
            ///来源存储区
            materialPullingOrderInfo.SourceZoneNo = pcsPullOrderInfo.SZoneNo;
            ///来源仓库
            materialPullingOrderInfo.SourceWmNo = pcsPullOrderInfo.SWmNo;
            ///目标存储区
            materialPullingOrderInfo.TargetZoneNo = pcsPullOrderInfo.TZoneNo;
            ///目标仓库
            materialPullingOrderInfo.TargetWmNo = pcsPullOrderInfo.TWmNo;
            ///道口
            materialPullingOrderInfo.TargetDock = pcsPullOrderInfo.Dock;
            ///送货路径
            materialPullingOrderInfo.Route = pcsPullOrderInfo.RouteCode;
            ///预计到厂时间
            materialPullingOrderInfo.PlanDeliveryTime = pcsPullOrderInfo.PlanDeliveryTime;
            ///发货时间
            materialPullingOrderInfo.PlanShippingTime = pcsPullOrderInfo.PlanShippingTime;
            ///发布时间
            materialPullingOrderInfo.PublishTime = pcsPullOrderInfo.PublishTime;
            ///ASN标识
            materialPullingOrderInfo.AsnFlag = pcsPullOrderInfo.AsnFlag.GetValueOrDefault();
            ///单据类型
            materialPullingOrderInfo.OrderType = pcsPullOrderInfo.OrderType.GetValueOrDefault();
            ///保管员
            materialPullingOrderInfo.Keeper = pcsPullOrderInfo.Keeper;
            ///拉动方式
            materialPullingOrderInfo.PullMode = (int)PullModeConstants.Pcs;
            ///拉动单号
            materialPullingOrderInfo.RunsheetNo = pcsPullOrderInfo.OrderCode;
        }
        /// <summary>
        /// TwdPullOrderDetailInfo -> MaterialPullingOrderDetailInfo
        /// </summary>
        /// <param name="twdPullOrderDetailInfo"></param>
        /// <param name="materialPullingOrderDetailInfo"></param>
        public static void GetMaterialPullingOrderDetailInfo(TwdPullOrderDetailInfo twdPullOrderDetailInfo, ref MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo)
        {
            if (twdPullOrderDetailInfo == null) return;
            ///拉动单号
            materialPullingOrderDetailInfo.OrderNo = twdPullOrderDetailInfo.OrderCode;
            ///供应商
            materialPullingOrderDetailInfo.SupplierNum = twdPullOrderDetailInfo.SupplierNum;
            ///物料号
            materialPullingOrderDetailInfo.PartNo = twdPullOrderDetailInfo.PartNo;
            ///物料号中文名称
            materialPullingOrderDetailInfo.PartCname = twdPullOrderDetailInfo.PartCname;
            ///物料号英文名称
            materialPullingOrderDetailInfo.PartEname = twdPullOrderDetailInfo.PartEname;
            ///计量单位
            materialPullingOrderDetailInfo.Uom = twdPullOrderDetailInfo.MeasuringUnitNo;
            ///入库单包装数量
            materialPullingOrderDetailInfo.PackageQty = twdPullOrderDetailInfo.Package.GetValueOrDefault();
            ///入库包装编号
            materialPullingOrderDetailInfo.PackageModel = twdPullOrderDetailInfo.PackageModel;
            ///需求包装数量
            materialPullingOrderDetailInfo.RequirePackageQty = twdPullOrderDetailInfo.RequiredPackageQty.GetValueOrDefault();
            ///需求物料数量
            materialPullingOrderDetailInfo.RequirePartQty = twdPullOrderDetailInfo.RequiredPartQty.GetValueOrDefault();
        }
        /// <summary>
        /// PcsPullOrderDetailInfo -> MaterialPullingOrderDetailInfo
        /// </summary>
        /// <param name="pcsPullOrderDetailInfo"></param>
        /// <param name="materialPullingOrderDetailInfo"></param>
        public static void GetMaterialPullingOrderDetailInfo(PcsPullOrderDetailInfo pcsPullOrderDetailInfo, ref MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo)
        {
            if (pcsPullOrderDetailInfo == null) return;
            ///拉动单号
            materialPullingOrderDetailInfo.OrderNo = pcsPullOrderDetailInfo.OrderCode;
            ///供应商
            materialPullingOrderDetailInfo.SupplierNum = pcsPullOrderDetailInfo.SupplierNum;
            ///物料号
            materialPullingOrderDetailInfo.PartNo = pcsPullOrderDetailInfo.PartNo;
            ///物料号中文名称
            materialPullingOrderDetailInfo.PartCname = pcsPullOrderDetailInfo.PartCname;
            ///物料号英文名称
            materialPullingOrderDetailInfo.PartEname = pcsPullOrderDetailInfo.PartEname;
            ///计量单位
            materialPullingOrderDetailInfo.Uom = pcsPullOrderDetailInfo.MeasuringUnitNo;
            ///入库单包装数量
            materialPullingOrderDetailInfo.PackageQty = pcsPullOrderDetailInfo.Package.GetValueOrDefault();
            ///入库包装编号
            materialPullingOrderDetailInfo.PackageModel = pcsPullOrderDetailInfo.PackageModel;
            ///需求包装数量
            materialPullingOrderDetailInfo.RequirePackageQty = pcsPullOrderDetailInfo.RequiredPackageQty.GetValueOrDefault();
            ///需求物料数量
            materialPullingOrderDetailInfo.RequirePartQty = pcsPullOrderDetailInfo.RequiredPartQty.GetValueOrDefault();
        }
        /// <summary>
        /// VmiOutputInfo -> MaterialPullingOrderInfo
        /// </summary>
        /// <param name="twdPullOrderInfo"></param>
        /// <param name="materialPullingOrderInfo"></param>
        public static void GetMaterialPullingOrderInfo(VmiOutputInfo vmiOutputInfo, ref MaterialPullingOrderInfo info)
        {
            if (vmiOutputInfo == null) return;
            ///拉动单号
            info.OrderNo = vmiOutputInfo.OutputNo;
            ///零件类代码
            info.PartBoxCode = vmiOutputInfo.PartBoxCode;
            ///工厂
            info.Plant = vmiOutputInfo.Plant;
            ///车间
            info.Workshop = vmiOutputInfo.Workshop;
            ///生产线
            info.AssemblyLine = vmiOutputInfo.AssemblyLine;
            ///供应商
            info.SupplierNum = vmiOutputInfo.SupplierNum;
            ///来源存储区
            info.SourceZoneNo = vmiOutputInfo.ZoneNo;
            ///来源仓库
            info.SourceWmNo = vmiOutputInfo.WmNo;
            ///目标存储区
            info.TargetZoneNo = vmiOutputInfo.TZoneNo;
            ///目标仓库
            info.TargetWmNo = vmiOutputInfo.TWmNo;
            ///道口
            info.TargetDock = vmiOutputInfo.TDock;
            ///送货路径
            info.Route = vmiOutputInfo.Route;
            ///预计到厂时间
            info.PlanDeliveryTime = vmiOutputInfo.PlanDeliveryTime;
            ///发货时间
            info.PlanShippingTime = vmiOutputInfo.PlanShippingTime;
            ///发布时间
            info.PublishTime = vmiOutputInfo.SendTime;
            ///ASN标识
            info.AsnFlag = false;
            ///单据类型
            info.OrderType = vmiOutputInfo.OutputType.GetValueOrDefault();
            ///保管员
            info.Keeper = vmiOutputInfo.BookKeeper;
            ///拉动方式
            info.PullMode = vmiOutputInfo.PullMode.GetValueOrDefault();
            ///拉动单号
            info.RunsheetNo = vmiOutputInfo.RunsheetNo;
        }
        /// <summary>
        /// VmiOutputDetailInfo -> MaterialPullingOrderDetailInfo
        /// </summary>
        /// <param name="vmiOutputDetailInfo"></param>
        /// <param name="info"></param>
        public static void GetMaterialPullingOrderDetailInfo(VmiOutputDetailInfo vmiOutputDetailInfo, ref MaterialPullingOrderDetailInfo info)
        {
            if (vmiOutputDetailInfo == null) return;
            ///拉动单号
            info.OrderNo = vmiOutputDetailInfo.OrderNo;
            ///供应商
            info.SupplierNum = vmiOutputDetailInfo.SupplierNum;
            ///物料号
            info.PartNo = vmiOutputDetailInfo.PartNo;
            ///物料号中文名称
            info.PartCname = vmiOutputDetailInfo.PartCname;
            ///物料号英文名称
            info.PartEname = vmiOutputDetailInfo.PartEname;
            ///计量单位
            info.Uom = vmiOutputDetailInfo.MeasuringUnitNo;
            ///入库单包装数量
            info.PackageQty = vmiOutputDetailInfo.Package.GetValueOrDefault();
            ///入库包装编号
            info.PackageModel = vmiOutputDetailInfo.PackageModel;
            ///需求包装数量 = 外仓的实发
            info.RequirePackageQty = vmiOutputDetailInfo.ActualBoxNum.GetValueOrDefault();
            ///需求物料数量
            info.RequirePartQty = vmiOutputDetailInfo.ActualQty.GetValueOrDefault();
        }
        #endregion

        #region Jis
        /// <summary>
        /// jisPartBoxInfo -> MaterialPullingOrderInfo
        /// </summary>
        /// <param name="jisPartBoxInfo"></param>
        /// <param name="materialPullingOrderInfo"></param>
        public static void GetJisMaterialPullingOrderInfo(JisPartBoxInfo jisPartBoxInfo, ref MaterialPullingOrderInfo materialPullingOrderInfo)
        {
            materialPullingOrderInfo.PartBoxCode = jisPartBoxInfo.PartBoxCode;///零件类2
            materialPullingOrderInfo.PartBoxName = jisPartBoxInfo.PartBoxName; ///零件类名称3
            materialPullingOrderInfo.Plant = jisPartBoxInfo.Plant;///工厂4
            materialPullingOrderInfo.Workshop = jisPartBoxInfo.Workshop;///车间5
            materialPullingOrderInfo.AssemblyLine = jisPartBoxInfo.AssemblyLine;///流水线6
            materialPullingOrderInfo.SupplierNum = jisPartBoxInfo.SupplierNum; ///供应商7
            materialPullingOrderInfo.SourceZoneNo = jisPartBoxInfo.SZoneNo;///来源存储区8
            materialPullingOrderInfo.SourceWmNo = jisPartBoxInfo.SWmNo;///来源仓库9
            materialPullingOrderInfo.TargetZoneNo = jisPartBoxInfo.TZoneNo;///目标存储区10
            materialPullingOrderInfo.TargetWmNo = jisPartBoxInfo.TWmNo;///目标仓库11
            materialPullingOrderInfo.TargetDock = jisPartBoxInfo.Dock;///道口12
            materialPullingOrderInfo.PlanShippingTime = DateTime.Now.AddMinutes(jisPartBoxInfo.DeliveryTime.GetValueOrDefault());///发货时间 15
            materialPullingOrderInfo.PlanDeliveryTime = materialPullingOrderInfo.PlanShippingTime.GetValueOrDefault().AddMinutes(jisPartBoxInfo.UnloadingTime.GetValueOrDefault());///预计到厂时间14
            materialPullingOrderInfo.PublishTime = DateTime.Now; ///发布时间 16
            materialPullingOrderInfo.OrderType = (int)PullOrderTypeConstants.Pulling; ///单据类型 18
            materialPullingOrderInfo.WorkshopSection = jisPartBoxInfo.WorkshopSection;///工段  19
            materialPullingOrderInfo.Location = jisPartBoxInfo.Location;///工位 20
            materialPullingOrderInfo.StartVehicheNo = null;///开始车号
            materialPullingOrderInfo.EndVehicheNo = null;///结束车号
            materialPullingOrderInfo.PackageModel = jisPartBoxInfo.PackageModel;///包装编号
            materialPullingOrderInfo.Package = jisPartBoxInfo.AccumulativeQty.GetValueOrDefault(); ///单包装数量 TODO:没有此字段
            materialPullingOrderInfo.PullMode = (int)PullModeConstants.Jis;
        }
        /// <summary>
        /// orderCode -> MaterialPullingOrderInfo
        /// </summary>
        /// <param name="orderCode"></param>
        /// <param name="materialPullingOrderInfo"></param>
        public static void GetorderCodeMaterialPullingOrderInfo(string orderCode, ref MaterialPullingOrderInfo materialPullingOrderInfo)
        {
            materialPullingOrderInfo.OrderNo = orderCode; ///拉动单号 1
        }

        /// <summary>
        /// pullOrdersInfo-> MaterialPullingOrderDetailInfo
        /// </summary>
        /// <param name="pullOrdersInfo"></param>
        /// <param name="materialPullingOrderDetailInfo"></param>
        public static void GetPullOrderMaterialPullingOrderDetailInfo(PullOrdersInfo pullOrdersInfo, ref MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo)
        {
            materialPullingOrderDetailInfo.ProduceNo = pullOrdersInfo.OrderNo;///生产订单号
            materialPullingOrderDetailInfo.VehicheModelNo = pullOrdersInfo.PartNo;///车型编号
            materialPullingOrderDetailInfo.DayVehicheSeqNo = null;///当日车辆顺序号  TODO:从生产订单中取        
            materialPullingOrderDetailInfo.Vin = pullOrdersInfo.Vin;///Vin
        }

        /// <summary>
        /// orderCode-> MaterialPullingOrderDetailInfo
        /// </summary>
        /// <param name="orderCode"></param>
        /// <param name="materialPullingOrderDetailInfo"></param>
        public static void GetOrderCodeMaterialPullingOrderDetailInfo(string orderCode, ref MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo)
        {
            materialPullingOrderDetailInfo.OrderNo = orderCode;///拉动单号1   
        }
        /// <summary>
        /// jisPartBoxInfo-> MaterialPullingOrderDetailInfo
        /// </summary>
        /// <param name="jisPartBoxInfo"></param>
        /// <param name="materialPullingOrderDetailInfo"></param>
        public static void GetJisMaterialPullingOrderDetailInfo(JisPartBoxInfo jisPartBoxInfo, ref MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo)
        {
            materialPullingOrderDetailInfo.TargetZoneNo = jisPartBoxInfo.TZoneNo; ///目标存储区
        }
        /// <summary>
        /// maintainInhouseLogisticStandardInfo -> MaterialPullingOrderDetailInfo
        /// </summary>
        /// <param name="maintainInhouseLogisticStandardInfo"></param>
        /// <param name="PartQty"></param>
        /// <param name="materialPullingOrderDetailInfo"></param>
        public static void GetMainMaterialPullingOrderDetailInfo(MaintainInhouseLogisticStandardInfo maintainInhouseLogisticStandardInfo, Decimal PartQty, ref MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo)
        {
            materialPullingOrderDetailInfo.SupplierNum = maintainInhouseLogisticStandardInfo.SupplierNum;///供应商2
            materialPullingOrderDetailInfo.PartNo = maintainInhouseLogisticStandardInfo.PartNo;///物料号3
            materialPullingOrderDetailInfo.PartCname = maintainInhouseLogisticStandardInfo.PartCname;///物料号中文名称4
            materialPullingOrderDetailInfo.PartEname = maintainInhouseLogisticStandardInfo.PartEname;///物料号英文名称5
            //materialPullingOrderDetailInfo.Uom = maintainInhouseLogisticStandardInfo.PartUnits;///计量单位6 
            materialPullingOrderDetailInfo.PackageQty = maintainInhouseLogisticStandardInfo.InboundPackage.GetValueOrDefault();///入库单包装数量7
            materialPullingOrderDetailInfo.PackageModel = maintainInhouseLogisticStandardInfo.InboundPackageModel;///入库包装编号8
            materialPullingOrderDetailInfo.RequirePackageQty = Convert.ToInt32(Math.Ceiling(PartQty / maintainInhouseLogisticStandardInfo.InboundPackage.GetValueOrDefault()));///需求包装数量9
            materialPullingOrderDetailInfo.RequirePartQty = PartQty;///需求物料数量10
            materialPullingOrderDetailInfo.WorkshopSection = maintainInhouseLogisticStandardInfo.WorkshopSection;///工段
            materialPullingOrderDetailInfo.Location = maintainInhouseLogisticStandardInfo.Location;///工位
            materialPullingOrderDetailInfo.DayVehicheSeqNo = null;///当日车辆顺序号  TODO:从生产订单中取     
        }
        #endregion

        #region Plan
        /// <summary>
        /// planPullOrderInfo-->materialPullingOrderInfo
        /// </summary>
        /// <param name="planPullOrderInfo"></param>
        /// <param name="materialPullingOrderInfo"></param>
        public static void GetMaterialPlanPullingOrderInfo(PlanPullOrderInfo planPullOrderInfo, ref MaterialPullingOrderInfo materialPullingOrderInfo)
        {
            if (planPullOrderInfo == null) return;
            ///OrderCode
            materialPullingOrderInfo.OrderNo = planPullOrderInfo.OrderCode;
            ///PartBoxCode
            materialPullingOrderInfo.PartBoxCode = planPullOrderInfo.PartBoxCode;
            ///PartBoxName
            materialPullingOrderInfo.PartBoxName = planPullOrderInfo.PartBoxName;
            ///Plant
            materialPullingOrderInfo.Plant = planPullOrderInfo.Plant;
            ///Workshop
            materialPullingOrderInfo.Workshop = planPullOrderInfo.Workshop;
            ///AssemblyLine
            materialPullingOrderInfo.AssemblyLine = planPullOrderInfo.AssemblyLine;
            ///SupplierNum
            materialPullingOrderInfo.SupplierNum = planPullOrderInfo.SupplierNum;
            ///SourceZoneNo
            materialPullingOrderInfo.SourceZoneNo = planPullOrderInfo.SourceZoneNo;
            ///SourceWmNo
            materialPullingOrderInfo.SourceWmNo = planPullOrderInfo.SourceWmNo;
            ///TargetZoneNo
            materialPullingOrderInfo.TargetZoneNo = planPullOrderInfo.TargetZoneNo;
            ///TargetWmNo
            materialPullingOrderInfo.TargetWmNo = planPullOrderInfo.TargetWmNo;
            ///TargetDock
            materialPullingOrderInfo.TargetDock = planPullOrderInfo.Dock;
            ///PlanDeliveryTime
            materialPullingOrderInfo.PlanDeliveryTime = planPullOrderInfo.SuggestDeliveryTime;
            ///PlanShippingTime
            materialPullingOrderInfo.PlanShippingTime = planPullOrderInfo.ExpectedArrivalTime;
            ///PublishTime
            materialPullingOrderInfo.PublishTime = planPullOrderInfo.PublishTime;
            ///OrderType
            materialPullingOrderInfo.OrderType = planPullOrderInfo.OrderType.GetValueOrDefault();
            ///PullMode
            materialPullingOrderInfo.PullMode = (int)PullModeConstants.Plan;
            ///RunsheetNo
            materialPullingOrderInfo.RunsheetNo = planPullOrderInfo.OrderCode;
        }


        /// <summary>
        /// TwdPullOrderDetailInfo -> MaterialPullingOrderDetailInfo
        /// </summary>
        /// <param name="twdPullOrderDetailInfo"></param>
        /// <param name="materialPullingOrderDetailInfo"></param>
        public static void GetMaterialPullingOrderDetail(PlanPullOrderDetailInfo planPullOrderDetailInfo, ref MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo)
        {
            if (planPullOrderDetailInfo == null) return;
            ///拉动单号
            materialPullingOrderDetailInfo.OrderNo = planPullOrderDetailInfo.OrderCode;
            ///供应商
            materialPullingOrderDetailInfo.SupplierNum = planPullOrderDetailInfo.SupplierNum;
            ///物料号
            materialPullingOrderDetailInfo.PartNo = planPullOrderDetailInfo.PartNo;
            ///物料号中文名称
            materialPullingOrderDetailInfo.PartCname = planPullOrderDetailInfo.PartCname;
            ///物料号英文名称
            materialPullingOrderDetailInfo.PartEname = planPullOrderDetailInfo.PartEname;
            ///计量单位
            materialPullingOrderDetailInfo.Uom = planPullOrderDetailInfo.MeasuringUnitNo;
            ///入库单包装数量
            materialPullingOrderDetailInfo.PackageQty = planPullOrderDetailInfo.InboundPackageQty.GetValueOrDefault();
            ///入库包装编号
            materialPullingOrderDetailInfo.PackageModel = planPullOrderDetailInfo.InboundPackageModel;
            ///需求包装数量
            materialPullingOrderDetailInfo.RequirePackageQty = planPullOrderDetailInfo.RequiredPackageQty.GetValueOrDefault();
            ///需求物料数量
            materialPullingOrderDetailInfo.RequirePartQty = planPullOrderDetailInfo.RequiredPartQty.GetValueOrDefault();
        }
        #endregion
    }
}
