namespace WS.WMM.StocksSyncService
{
    using BLL.LES;
    using BLL.SYS;
    using DAL.LES;
    using DM.LES;
    using DM.SYS;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Transactions;
    /// <summary>
    /// Handle
    /// </summary>
    public class Handle
    {
        #region 变量
        /// <summary>
        /// 供应商维度标记
        /// </summary>
        private bool supplierFlag = false;
        /// <summary>
        /// 工厂维度标记
        /// </summary>
        private bool plantFlag = false;
        /// <summary>
        /// 包装型号维度标记
        /// </summary>
        private bool packageModelFlag = false;
        /// <summary>
        /// 物料类别维度标记
        /// </summary>
        private bool partClsFlag = false;
        /// <summary>
        /// 批次号维度标记
        /// </summary>
        private bool batchFlag = false;
        /// <summary>
        /// 标签号维度标记
        /// </summary>
        private bool barcodeFlag = false;
        /// <summary>
        /// 产地维度标记
        /// </summary>
        private bool originPlaceFlag = false;
        /// <summary>
        /// 成本中心维度标记
        /// </summary>
        private bool costCenterFlag = false;
        /// <summary>
        /// 结算维度标记
        /// </summary>
        private bool settlementFlag = false;
        /// <summary>
        /// 是否启用SAP交易记录同步接口
        /// </summary>
        private string sapTranDataEnableFlag = string.Empty;
        /// <summary>
        /// 是否启用SRM交易记录同步接口
        /// </summary>
        private string srmTranDataEnableFlag = string.Empty;
        /// <summary>
        /// 是否启用WMS交易记录同步接口
        /// </summary>
        private string wmsTranDataEnableFlag = string.Empty;
        /// <summary>
        /// 是否启用VMI交易记录同步接口
        /// </summary>
        private string vmiTranDataEnableFlag = string.Empty;
        /// <summary>
        /// 返利入库单是否发布给SRM系统
        /// </summary>
        private string rebateInboundReceiveReleaseToSrm = string.Empty;
        /// <summary>
        /// 登录用户
        /// </summary>
        private string loginUser = "StocksSyncService";
        /// <summary>
        /// 
        /// </summary>
        private StocksBLL stocksBLL = new StocksBLL();
        #endregion
        /// <summary>
        /// Handler
        /// </summary>
        public void Handler()
        {
            InitConfigFlag();
            ///获取未同步的交易记录
            List<TranDetailsInfo> tranDetailsInfos = new TranDetailsBLL().GetList("[TRAN_STATE] = " + (int)WmmTranStateConstants.Created + "", "[ID]");
            if (tranDetailsInfos.Count == 0) return;
            ///仓库
            List<string> wmNos = tranDetailsInfos.Where(d => !string.IsNullOrEmpty(d.WmNo)).Select(d => d.WmNo).ToList();
            wmNos.AddRange(tranDetailsInfos.Where(d => !string.IsNullOrEmpty(d.TargetWm)).Select(d => d.TargetWm).ToList());
            List<WarehouseInfo> warehouseInfos = new WarehouseBLL().GetList("[WAREHOUSE] in ('" + string.Join("','", wmNos.ToArray()) + "')", string.Empty);
            ///存储区
            List<string> zoneNos = tranDetailsInfos.Where(d => !string.IsNullOrEmpty(d.ZoneNo)).Select(d => d.ZoneNo).ToList();
            zoneNos.AddRange(tranDetailsInfos.Where(d => !string.IsNullOrEmpty(d.TargetZone)).Select(d => d.TargetZone).ToList());
            List<ZonesInfo> zonesInfos = new ZonesBLL().GetList("[ZONE_NO] in ('" + string.Join("','", zoneNos.ToArray()) + "')", string.Empty);
            ///工厂
            List<PlantInfo> plantInfos = new PlantBLL().GetListForInterfaceDataSync();
            ///供应商
            List<SupplierInfo> supplierInfos = new SupplierBLL().GetListForInterfaceDataSync(tranDetailsInfos.Select(d => d.SupplierNum).ToList());
            ///物料
            List<MaintainPartsInfo> maintainPartsInfos = new MaintainPartsBLL().GetListForInterfaceDataSync(tranDetailsInfos.Select(d => d.PartNo).ToList());


            ///逐条处理
            foreach (var tranDetailsInfo in tranDetailsInfos)
            {
                StringBuilder stringBuilder = new StringBuilder();
                ///工厂
                PlantInfo plantInfo = plantInfos.FirstOrDefault(d => d.Plant == tranDetailsInfo.Plant);
                ///供应商
                SupplierInfo supplierInfo = supplierInfos.FirstOrDefault(d => d.SupplierNum == tranDetailsInfo.SupplierNum);
                ///目标仓库
                WarehouseInfo targetWarehouseInfo = warehouseInfos.FirstOrDefault(d => d.Warehouse == tranDetailsInfo.TargetWm);
                ///目标存储区
                ZonesInfo targetZonesInfo = zonesInfos.FirstOrDefault(d => d.ZoneNo == tranDetailsInfo.TargetZone);
                ///目标工厂
                PlantInfo targetPlantInfo = targetZonesInfo == null ? null : plantInfos.FirstOrDefault(d => d.Plant == targetZonesInfo.Plant);
                ///来源
                ZonesInfo sourceZonesInfo = zonesInfos.FirstOrDefault(d => d.ZoneNo == tranDetailsInfo.ZoneNo);
                ///来源工厂
                PlantInfo sourcePlantInfo = sourceZonesInfo == null ? null : plantInfos.FirstOrDefault(d => d.Plant == sourceZonesInfo.Plant);
                ///物料信息
                MaintainPartsInfo maintainPartsInfo = maintainPartsInfos.FirstOrDefault(d => d.PartNo == tranDetailsInfo.PartNo && d.Plant == tranDetailsInfo.Plant);
                ///创建库存对象
                StocksInfo stocksInfo = null;
                ///
                switch (tranDetailsInfo.TranType.GetValueOrDefault())
                {
                    ///物料入库
                    case (int)WmmTranTypeConstants.Inbound:
                        ///目标可用库存增加
                        stocksInfo = HandlingStocksData(tranDetailsInfo, maintainPartsInfo, targetZonesInfo, false);
                        stringBuilder.AppendFormat(stocksBLL.StocksRaiseSql(stocksInfo, tranDetailsInfo.Id, loginUser));
                        break;
                    ///撤销入库
                    case (int)WmmTranTypeConstants.UndoInbound:
                        stocksInfo = HandlingStocksData(tranDetailsInfo, maintainPartsInfo, targetZonesInfo, false);
                        UndoStock(ref stocksInfo);
                        stringBuilder.AppendFormat(stocksBLL.StocksRaiseSql(stocksInfo, tranDetailsInfo.Id, loginUser));
                        break;
                    ///物料出库
                    case (int)WmmTranTypeConstants.Outbound:
                        ///来源可用库存减少
                        stocksInfo = HandlingStocksData(tranDetailsInfo, maintainPartsInfo, sourceZonesInfo, true);
                        stringBuilder.AppendFormat(stocksBLL.StocksReduceSql(stocksInfo, tranDetailsInfo.Id, loginUser));
                        break;
                    ///撤销出库
                    case (int)WmmTranTypeConstants.UndoOutbound:
                        stocksInfo = HandlingStocksData(tranDetailsInfo, maintainPartsInfo, sourceZonesInfo, true);
                        UndoStock(ref stocksInfo);
                        stringBuilder.AppendFormat(stocksBLL.StocksReduceSql(stocksInfo, tranDetailsInfo.Id, loginUser));
                        break;
                    ///冻结入库
                    case (int)WmmTranTypeConstants.FrozenInbound:
                        ///目标冻结库存增加
                        stocksInfo = HandlingStocksData(tranDetailsInfo, maintainPartsInfo, targetZonesInfo, false);
                        stringBuilder.AppendFormat(stocksBLL.FrozenRaiseSql(stocksInfo, tranDetailsInfo.Id, loginUser));
                        break;
                    ///冻结出库
                    case (int)WmmTranTypeConstants.FrozenOutbound:
                        ///来源冻结库存减少
                        stocksInfo = HandlingStocksData(tranDetailsInfo, maintainPartsInfo, sourceZonesInfo, true);
                        stringBuilder.AppendFormat(stocksBLL.FrozenReduceSql(stocksInfo, tranDetailsInfo.Id, loginUser));
                        break;
                    ///物料冻结
                    case (int)WmmTranTypeConstants.MaterialFreezing:
                        ///来源可用库存减少
                        stocksInfo = HandlingStocksData(tranDetailsInfo, maintainPartsInfo, sourceZonesInfo, true);
                        stringBuilder.AppendFormat(stocksBLL.StocksReduceSql(stocksInfo, tranDetailsInfo.Id, loginUser));
                        ///目标冻结库存增加
                        stocksInfo = HandlingStocksData(tranDetailsInfo, maintainPartsInfo, targetZonesInfo, false);
                        stringBuilder.AppendFormat(stocksBLL.FrozenRaiseSql(stocksInfo, tranDetailsInfo.Id, loginUser));
                        break;
                    ///状态冻结
                    case (int)WmmTranTypeConstants.StateFreezing:
                        ///来源可用库存减少
                        stocksInfo = HandlingStocksData(tranDetailsInfo, maintainPartsInfo, sourceZonesInfo, true);
                        stringBuilder.AppendFormat(stocksBLL.StocksReduceSql(stocksInfo, tranDetailsInfo.Id, loginUser));
                        ///来源冻结库存增加
                        stocksInfo = HandlingStocksData(tranDetailsInfo, maintainPartsInfo, sourceZonesInfo, true);
                        stringBuilder.AppendFormat(stocksBLL.FrozenRaiseSql(stocksInfo, tranDetailsInfo.Id, loginUser));
                        break;
                    ///物料解冻
                    case (int)WmmTranTypeConstants.MaterialThawing:
                        ///来源冻结库存减少
                        stocksInfo = HandlingStocksData(tranDetailsInfo, maintainPartsInfo, sourceZonesInfo, true);
                        stringBuilder.AppendFormat(stocksBLL.FrozenReduceSql(stocksInfo, tranDetailsInfo.Id, loginUser));
                        ///目标可用库存增加
                        stocksInfo = HandlingStocksData(tranDetailsInfo, maintainPartsInfo, targetZonesInfo, false);
                        stringBuilder.AppendFormat(stocksBLL.StocksRaiseSql(stocksInfo, tranDetailsInfo.Id, loginUser));
                        break;
                    ///状态解冻
                    case (int)WmmTranTypeConstants.StateThawing:
                        ///来源可用库存增加
                        stocksInfo = HandlingStocksData(tranDetailsInfo, maintainPartsInfo, sourceZonesInfo, true);
                        stringBuilder.AppendFormat(stocksBLL.StocksRaiseSql(stocksInfo, tranDetailsInfo.Id, loginUser));
                        ///来源冻结库存减少
                        stocksInfo = HandlingStocksData(tranDetailsInfo, maintainPartsInfo, sourceZonesInfo, true);
                        stringBuilder.AppendFormat(stocksBLL.FrozenReduceSql(stocksInfo, tranDetailsInfo.Id, loginUser));
                        break;
                    ///冻结移动
                    case (int)WmmTranTypeConstants.FrozenMovement:
                        ///来源冻结库存减少
                        stocksInfo = HandlingStocksData(tranDetailsInfo, maintainPartsInfo, sourceZonesInfo, true);
                        stringBuilder.AppendFormat(stocksBLL.FrozenReduceSql(stocksInfo, tranDetailsInfo.Id, loginUser));
                        ///目标冻结库存增加
                        stocksInfo = HandlingStocksData(tranDetailsInfo, maintainPartsInfo, targetZonesInfo, false);
                        stringBuilder.AppendFormat(stocksBLL.FrozenRaiseSql(stocksInfo, tranDetailsInfo.Id, loginUser));
                        break;
                    ///物料移动
                    case (int)WmmTranTypeConstants.Movement:
                        ///来源可用库存减少
                        stocksInfo = HandlingStocksData(tranDetailsInfo, maintainPartsInfo, sourceZonesInfo, true);
                        stringBuilder.AppendFormat(stocksBLL.StocksReduceSql(stocksInfo, tranDetailsInfo.Id, loginUser));
                        ///目标可用库存增加
                        stocksInfo = HandlingStocksData(tranDetailsInfo, maintainPartsInfo, targetZonesInfo, false);
                        stringBuilder.AppendFormat(stocksBLL.StocksRaiseSql(stocksInfo, tranDetailsInfo.Id, loginUser));
                        break;
                    default: continue;
                }
                ///SAP移动数据
                stringBuilder.AppendFormat(CreateSapTranData(tranDetailsInfo, sourceZonesInfo, targetZonesInfo, sourcePlantInfo, targetPlantInfo));
                ///SRM入库数据
                stringBuilder.AppendFormat(CreateSrmTranData(tranDetailsInfo, supplierInfo, targetWarehouseInfo));
                ///WMS入库数据
                stringBuilder.AppendFormat(CreateVmiTranData(tranDetailsInfo, supplierInfo, targetWarehouseInfo));
           
                
                #region 执行
                using (TransactionScope trans = new TransactionScope())
                {
                    if (stringBuilder.Length > 0)
                        BLL.LES.CommonBLL.ExecuteNonQueryBySql(stringBuilder.ToString());
                    trans.Complete();
                }
                #endregion
                Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ":" + stocksInfo.PartNo + "|" + tranDetailsInfo.TranType.GetValueOrDefault() + "|" + stocksInfo.WmNo + "|" + stocksInfo.ZoneNo + "|" + stocksInfo.Dloc + "|");
            }
        }
        /// <summary>
        /// 初始化标记
        /// </summary>
        private void InitConfigFlag()
        {
            new StocksBLL().GetStockDimensionFlag(ref supplierFlag, ref plantFlag, ref packageModelFlag, ref partClsFlag, ref batchFlag, ref barcodeFlag, ref originPlaceFlag, ref costCenterFlag, ref settlementFlag);
            ///系统配置信息
            Dictionary<string, string> keyValues = new ConfigBLL().GetValuesByCodes(new string[] {
                "SAP_TRAN_DATA_ENABLE_FLAG",
                "SRM_TRAN_DATA_ENABLE_FLAG",
                "WMS_TRAN_DATA_ENABLE_FLAG",
                "VMI_TRAN_DATA_ENABLE_FLAG",
                "REBATE_INBOUND_RECEIVE_RELEASE_TO_SRM" });
            ///是否启用SAP交易记录同步接口
            keyValues.TryGetValue("SAP_TRAN_DATA_ENABLE_FLAG", out sapTranDataEnableFlag);
            ///是否启用SRM交易记录同步接口
            keyValues.TryGetValue("SRM_TRAN_DATA_ENABLE_FLAG", out srmTranDataEnableFlag);
            ///是否启用WMS交易记录同步接口
            keyValues.TryGetValue("WMS_TRAN_DATA_ENABLE_FLAG", out wmsTranDataEnableFlag);
            ///是否启用VMI交易记录同步接口
            keyValues.TryGetValue("VMI_TRAN_DATA_ENABLE_FLAG", out vmiTranDataEnableFlag);
            ///返利入库单是否发布给SRM系统
            keyValues.TryGetValue("REBATE_INBOUND_RECEIVE_RELEASE_TO_SRM", out rebateInboundReceiveReleaseToSrm);
        }
        /// <summary>
        /// 根据标记生成更新库存条件
        /// </summary>
        /// <param name="stocksInfo"></param>
        /// <returns></returns>
        private string GetStockUpdateConditions(ref StocksInfo stocksInfo)
        {
            string stockUpdateConditions = string.Empty;
            ///恒定条件为物料号、仓库、存储区、库位
            ///PART_NO
            stockUpdateConditions += "and [PART_NO] = N'" + stocksInfo.PartNo + "' ";
            ///WM_NO
            stockUpdateConditions += "and [WM_NO] = N'" + stocksInfo.WmNo + "' ";
            ///ZONE_NO
            stockUpdateConditions += "and [ZONE_NO] = N'" + stocksInfo.ZoneNo + "' ";
            ///DLOC
            stockUpdateConditions += "and isnull([DLOC],'') = N'" + stocksInfo.Dloc + "' ";

            ///SUPPLIER_NUM
            stockUpdateConditions += supplierFlag ? "and isnull([SUPPLIER_NUM],'') = N'" + stocksInfo.SupplierNum + "' " : string.Empty;
            stocksInfo.SupplierNum = supplierFlag ? stocksInfo.SupplierNum : string.Empty;
            ///PLANT
            stockUpdateConditions += plantFlag ? "and isnull([PLANT],'') = N'" + stocksInfo.Plant + "' " : string.Empty;
            stocksInfo.Plant = plantFlag ? stocksInfo.Plant : string.Empty;
            ///PACKAGE_MODEL
            stockUpdateConditions += packageModelFlag ? "and isnull([PACKAGE_MODEL],'') = N'" + stocksInfo.PackageModel + "' " : string.Empty;
            stocksInfo.PackageModel = packageModelFlag ? stocksInfo.PackageModel : string.Empty;
            stocksInfo.Package = packageModelFlag ? stocksInfo.Package : 0;
            ///PART_CLS
            stockUpdateConditions += partClsFlag ? "and isnull([PART_CLS],'') = N'" + stocksInfo.PartCls + "' " : string.Empty;
            stocksInfo.PartCls = partClsFlag ? stocksInfo.PartCls : string.Empty;
            ///BATCH_NO，需要系统配置与物料仓储信息批次标记同时开启时才真正启用批次
            if (batchFlag && stocksInfo.IsBatch.GetValueOrDefault() == 1)
                stockUpdateConditions += "and isnull([BATCH_NO],'') = N'" + stocksInfo.BatchNo + "' ";
            else
                stocksInfo.BatchNo = string.Empty;
            ///BARCODE_DATA
            stockUpdateConditions += barcodeFlag ? "and isnull([BARCODE_DATA],'') = N'" + stocksInfo.BarcodeData + "' " : string.Empty;
            stocksInfo.BarcodeData = barcodeFlag ? stocksInfo.BarcodeData : string.Empty;
            stocksInfo.BarcodeType = barcodeFlag ? stocksInfo.BarcodeType : string.Empty;
            ///ORIGIN_PLACE
            //stockUpdateConditions += originPlaceFlag ? "and isnull([ORIGIN_PLACE],'') = N'" + stocksInfo.OriginPlace + "' " : string.Empty;
            //stocksInfo.OriginPlace = originPlaceFlag ? stocksInfo.OriginPlace : string.Empty;
            ///COST_CENTER
            //stockUpdateConditions += costCenterFlag ? "and isnull([COST_CENTER],'') = N'" + stocksInfo.CostCenter + "' " : string.Empty;
            //stocksInfo.CostCenter = costCenterFlag ? stocksInfo.CostCenter : string.Empty;
            ///SETTLED_FLAG
            stockUpdateConditions += settlementFlag ? "and isnull([SETTLED_FLAG],0) = " + (stocksInfo.SettledFlag.GetValueOrDefault() ? 1 : 0) + " " : string.Empty;
            stocksInfo.SettledFlag = settlementFlag ? stocksInfo.SettledFlag : null;

            return stockUpdateConditions;
        }
        /// <summary>
        /// 将交易记录加工为库存投放数据
        /// </summary>
        /// <param name="tranDetailsInfo"></param>
        /// <param name="zonesInfo"></param>
        /// <param name="sourceFlag"></param>
        /// <returns></returns>
        private StocksInfo HandlingStocksData(TranDetailsInfo tranDetailsInfo, MaintainPartsInfo maintainPartsInfo, ZonesInfo zonesInfo, bool sourceFlag)
        {
            ///新建库存对象
            StocksInfo stocksInfo = stocksBLL.CreateStocksInfo(loginUser);
            ///更新物料基础信息
            stocksBLL.UpdateMaintainPartsInfo(maintainPartsInfo, ref stocksInfo);
            ///来源库存对象信息填充
            if (sourceFlag)
                stocksBLL.GetSourceStocksInfo(tranDetailsInfo, ref stocksInfo);
            ///目标库存对象信息填充
            else
                stocksBLL.GetTargetStocksInfo(tranDetailsInfo, zonesInfo, ref stocksInfo);
            ///根据库存管理维度获取更新条件
            string stockUpdateConditions = GetStockUpdateConditions(ref stocksInfo);
            ///若交易数据指定了库存数据外键，则根据外键获取主键
            if (sourceFlag && tranDetailsInfo.StocksFid != null)
                stocksInfo.Id = stocksBLL.GetStocksId(tranDetailsInfo.StocksFid.GetValueOrDefault());
            ///获取库存数据主键
            if (stocksInfo.Id == 0)
                stocksInfo.Id = stocksBLL.GetStocksId(stocksInfo, stockUpdateConditions);
            return stocksInfo;
        }
        /// <summary>
        /// 将数量及金额取得逆值
        /// </summary>
        /// <param name="stocksInfo"></param>
        private void UndoStock(ref StocksInfo stocksInfo)
        {
            ///包装数量
            stocksInfo.Stocks = 0 - stocksInfo.Stocks.GetValueOrDefault();
            ///散件数量
            stocksInfo.FragmentNum = 0 - stocksInfo.FragmentNum.GetValueOrDefault();
            ///库存件数
            stocksInfo.StocksNum = 0 - stocksInfo.StocksNum.GetValueOrDefault();
            ///采购价值
            //stocksInfo.PurchasePartPrice = 0 - stocksInfo.PurchasePartPrice.GetValueOrDefault();
            /////销售价值
            //stocksInfo.SalePartPrice = 0 - stocksInfo.SalePartPrice.GetValueOrDefault();
        }
        /// <summary>
        /// 根据交易记录以及基础数据配置数据，产生交易接口数据 Create By Xue
        /// </summary>
        private string CreateSapTranData(TranDetailsInfo tranDetailsInfo, ZonesInfo sourceZonesInfo, ZonesInfo targetZonesInfo, PlantInfo sourcePlantInfo, PlantInfo targetPlantInfo)
        {
            ///
            if (tranDetailsInfo == null) return string.Empty;
            ///是否启用SAP交易记录同步接口
            if (string.IsNullOrEmpty(sapTranDataEnableFlag) || sapTranDataEnableFlag.ToLower() != "true") return string.Empty;
            ///
            StringBuilder stringBuilder = new StringBuilder();

            #region SapTranOutInfo
            SapTranOutInfo sapTranOutInfo = new SapTranOutInfo();
            sapTranOutInfo.Matnr = tranDetailsInfo.PartNo;
            sapTranOutInfo.Menge = tranDetailsInfo.ActualQty;
            sapTranOutInfo.Budat = tranDetailsInfo.TranDate;
            sapTranOutInfo.Lifnr = tranDetailsInfo.SupplierNum;
            sapTranOutInfo.ProcessFlag = (int)ProcessFlagConstants.Untreated;
            sapTranOutInfo.CreateUser = loginUser;
            #endregion

            ///如果没有目标库区则有可能是出成本中心的业务数据
            if (targetZonesInfo == null)
            {
                ///若单据类型为物料预留、物料退货
                if (tranDetailsInfo.TranOrderType.GetValueOrDefault() == ((int)OutboundTypeConstants.ReserveOutbound + 2000)
                    || tranDetailsInfo.TranOrderType.GetValueOrDefault() == ((int)OutboundTypeConstants.MaterialReturns + 2000))
                {
                    SapPurchaseOrderDetailInfo sapPurchaseOrderDetailInfo = new SapPurchaseOrderDetailBLL().GetInfo(tranDetailsInfo.TranNo, tranDetailsInfo.PartNo);
                    if (sapPurchaseOrderDetailInfo == null) return string.Empty;///TODO:物料预留未能找到原始单据

                    ///如果没有成本中心则不具备产生SAP事务数据的条件
                    if (string.IsNullOrEmpty(sapPurchaseOrderDetailInfo.SapKostl))
                        return string.Empty;


                    ///采购订单中有SAP采购订单号的需要生成101入库交易
                    if (!string.IsNullOrEmpty(sapPurchaseOrderDetailInfo.SapEbeln))
                    {
                        #region 转入库存地点收货数据
                        SapTranOutInfo sapTranOut = sapTranOutInfo.Clone();
                        ///目标库存地点
                        sapTranOut.Lgort = sapPurchaseOrderDetailInfo.SapLgort;
                        sapTranOut.Werks = sourcePlantInfo == null ? string.Empty : sourcePlantInfo.SapPlantCode;
                        ///采购订单
                        sapTranOut.Ebeln = sapPurchaseOrderDetailInfo.SapEbeln;
                        sapTranOut.Ebelp = sapPurchaseOrderDetailInfo.SapEbelp;
                        ///101
                        sapTranOut.Bwart = ((int)SapTranTypeConstants.Inbound).ToString();
                        #endregion
                        stringBuilder.AppendLine(SapTranOutDAL.GetInsertSql(sapTranOut));
                    }
                    ///来源库存地点
                    sapTranOutInfo.Lgort = sapPurchaseOrderDetailInfo.SapLgort;
                    sapTranOutInfo.Werks = sourcePlantInfo == null ? string.Empty : sourcePlantInfo.SapPlantCode;
                    ///目标库存地点
                    sapTranOutInfo.Umlgo = sapPurchaseOrderDetailInfo.SapUmlgo;
                    sapTranOutInfo.Unwrk = targetPlantInfo == null ? string.Empty : targetPlantInfo.SapPlantCode;
                    ///成本中心
                    sapTranOutInfo.Kostl = sapPurchaseOrderDetailInfo.SapKostl;
                    ///预留单号
                    sapTranOutInfo.Rsnum = sapPurchaseOrderDetailInfo.SapRsnum;
                    ///采购订单
                    sapTranOutInfo.Ebeln = sapPurchaseOrderDetailInfo.SapEbeln;
                    sapTranOutInfo.Ebelp = sapPurchaseOrderDetailInfo.SapEbelp;
                    ///移动类型
                    sapTranOutInfo.Bwart = sapPurchaseOrderDetailInfo.SapBwart;
                    stringBuilder.AppendLine(SapTranOutDAL.GetInsertSql(sapTranOutInfo));
                    return stringBuilder.ToString();
                }
            }
            ///据交易记录中的目标存储区是否结算标记
            //todo
            if (targetZonesInfo!=null /*&& targetZonesInfo.SettlementFlag.GetValueOrDefault()*/)
            {
                ///接收工厂
                sapTranOutInfo.Unwrk = targetPlantInfo == null ? string.Empty : targetPlantInfo.SapPlantCode;
                ///接收库存地点
                sapTranOutInfo.Umlgo = targetZonesInfo.StockPlaceNo;
                ///发出工厂
                string werks = (sourcePlantInfo == null ? string.Empty : sourcePlantInfo.SapPlantCode);
                ///发出库存地点
                string lgort = (sourceZonesInfo == null ? string.Empty : sourceZonesInfo.StockPlaceNo);

                ///如果没有来源库存地点，并且物料未结算
                if (string.IsNullOrEmpty(lgort) && !tranDetailsInfo.SettledFlag.GetValueOrDefault())
                {
                    ///正常入库
                    if (tranDetailsInfo.TranOrderType.GetValueOrDefault() == ((int)InboundTypeConstants.NormalInbound + 1000))
                        sapTranOutInfo.Bwart = ((int)SapTranTypeConstants.Inbound).ToString();
                    ///返利入库单 -> 免费收货
                    if (tranDetailsInfo.TranOrderType.GetValueOrDefault() == ((int)InboundTypeConstants.RebateInbound + 1000))
                        sapTranOutInfo.Bwart = ((int)SapTranTypeConstants.RebateInbound).ToString();
                    ///采购订单 -> 正常入库
                    if (tranDetailsInfo.TranOrderType.GetValueOrDefault() == ((int)InboundTypeConstants.PurchaseOrder + 1000))
                    {
                        SapPurchaseOrderDetailInfo sapPurchaseOrderDetailInfo = new SapPurchaseOrderDetailBLL().GetInfo(tranDetailsInfo.RunsheetNo, tranDetailsInfo.PartNo);
                        if (sapPurchaseOrderDetailInfo == null) return string.Empty;///TODO:物料预留未能找到原始单据
                        sapTranOutInfo.Umlgo = sapPurchaseOrderDetailInfo.SapUmlgo;
                        sapTranOutInfo.Bwart = ((int)SapTranTypeConstants.Inbound).ToString();
                        sapTranOutInfo.Ebeln = sapPurchaseOrderDetailInfo.SapEbeln;
                        sapTranOutInfo.Ebelp = sapPurchaseOrderDetailInfo.SapEbelp;
                    }
                    return SapTranOutDAL.GetInsertSql(sapTranOutInfo);
                }
                ///如果有发出库存地点，并且物料已结算
                if (!string.IsNullOrEmpty(lgort) && tranDetailsInfo.SettledFlag.GetValueOrDefault())
                {
                    ///如果来源与目标的SAP库存地点相同则不产生移动数据
                    if (lgort == sapTranOutInfo.Umlgo)
                        return string.Empty;
                    sapTranOutInfo.Lgort = lgort;
                    sapTranOutInfo.Werks = werks;
                    sapTranOutInfo.Bwart = ((int)SapTranTypeConstants.Movement).ToString();
                    return SapTranOutDAL.GetInsertSql(sapTranOutInfo);
                }
            }
            return string.Empty;
        }
        /// <summary>
        /// CreateSrmTranData
        /// </summary>
        /// <param name="tranDetailsInfo"></param>
        private string CreateSrmTranData(TranDetailsInfo tranDetailsInfo, SupplierInfo supplierInfo, WarehouseInfo warehouseInfo)
        {
            ///若未启用外部供应商系统，TODO:考虑内部供应商模块如何处理
            if (string.IsNullOrEmpty(srmTranDataEnableFlag) || srmTranDataEnableFlag.ToLower() != "true") return string.Empty;
            ///若不是物料供应商则不需要生成记录
            if (supplierInfo.SupplierType.GetValueOrDefault() != (int)SupplierTypeConstants.MaterialSupplier) return string.Empty;
            ///交易记录NULL，肯定生成不了记录
            if (tranDetailsInfo == null) return string.Empty;
            if (warehouseInfo == null) return string.Empty;
            ///不是RDC类型的仓库的数据不发送给SRM
            if (warehouseInfo.WarehouseType.GetValueOrDefault() != (int)WarehouseTypeConstants.RDC) return string.Empty;
            ///非物料入库不传SRM
            if (tranDetailsInfo.TranType.GetValueOrDefault() != (int)WmmTranTypeConstants.Inbound) return string.Empty;
            ///SRM单据类型
            int orderType = 0;
            switch (tranDetailsInfo.TranOrderType.GetValueOrDefault())
            {
                case (int)InboundTypeConstants.RebateInbound + 1000:
                    if (string.IsNullOrEmpty(rebateInboundReceiveReleaseToSrm) || rebateInboundReceiveReleaseToSrm.ToLower() != "true") return string.Empty;
                    orderType = (int)SrmOrderTypeConstants.Reserve;///TODO:返利
                    break;
                ///采购订单
                case (int)InboundTypeConstants.PurchaseOrder + 1000: orderType = (int)SrmOrderTypeConstants.Purchase; break;
                ///预留
                case (int)InboundTypeConstants.ReserveInbound + 1000: orderType = (int)SrmOrderTypeConstants.Reserve; break;
                default:
                    switch (tranDetailsInfo.RunsheetType.GetValueOrDefault())
                    {
                        case (int)PullModeConstants.Jis: orderType = (int)SrmOrderTypeConstants.Jis; break;
                        case (int)PullModeConstants.Plan: orderType = (int)SrmOrderTypeConstants.Plan; break;
                        case (int)PullModeConstants.Twd: orderType = (int)SrmOrderTypeConstants.Twd; break;
                        default: return string.Empty;
                    }
                    break;
            }
            ///
            SrmTranOutInfo srmTranOutInfo = new SrmTranOutInfo();
            srmTranOutInfo.SourceOrderCode = tranDetailsInfo.TranNo;///TODO:SRM要哪个编号?
            srmTranOutInfo.SourceOrderType = orderType;
            srmTranOutInfo.PartNo = tranDetailsInfo.PartNo;
            srmTranOutInfo.DeliveryQty = tranDetailsInfo.ActualQty;
            srmTranOutInfo.ProcessFlag = (int)ProcessFlagConstants.Untreated;
            srmTranOutInfo.CreateUser = loginUser;
            return SrmTranOutDAL.GetInsertSql(srmTranOutInfo);
        }
        /// <summary>
        /// 036 wms Create By Xue 待商议
        /// </summary>
        /// <param name="tranDetailsInfo"></param>
        private string CreateWmsTranData(TranDetailsInfo tranDetailsInfo)
        {
            string sql = string.Empty;
            if (tranDetailsInfo == null) return string.Empty;
            int orderType = 0;
            if (tranDetailsInfo.TranNo != tranDetailsInfo.RunsheetNo)
            {
                orderType = 40;
            }
            else if (tranDetailsInfo.TranNo == tranDetailsInfo.RunsheetNo)
            {
                if (!string.IsNullOrEmpty(tranDetailsInfo.BoxParts))
                {
                    PartsBoxInfo partsBoxInfo = new PartsBoxBLL().GetInfoByPartBox(tranDetailsInfo.BoxParts);
                    orderType = partsBoxInfo.PullMode;
                }
                else
                {
                    if (string.IsNullOrEmpty(tranDetailsInfo.RunsheetNo) && string.IsNullOrEmpty(tranDetailsInfo.BoxParts))
                    {
                        orderType = 50;
                    }
                    else
                    {
                        orderType = 30;
                    }
                }
            }
            return sql = string.Format(@"insert into [LES].[TI_IFM_WMS_TRAN_OUT]([FID],[SOURCE_ORDER_CODE],[SOURCE_ORDER_TYPE],[PART_NO],[DELIVERY_QTY],[PROCESS_FLAG],[PROCESS_TIME],[LOG_FID],[VALID_FLAG],[CREATE_DATE],[CREATE_USER],[SUPPLIER_NUM],[SUPPLIER_NAME]) values(newid(),'{0}',{1},'{2}',{3},10,getdate(),null,1,getdate(),'{4}','{5}','{6}');", tranDetailsInfo.TranNo, orderType, tranDetailsInfo.PartNo, tranDetailsInfo.ActualQty, loginUser, tranDetailsInfo.SupplierNum, tranDetailsInfo.SupplierName);
        }
        
        /// <summary>
        /// 037 vmi Create By Xue 未完成
        /// </summary>
        /// <param name="tranDetailsInfo"></param>
        private string CreateVmiTranData(TranDetailsInfo tranDetailsInfo, SupplierInfo supplierInfo, WarehouseInfo warehouseInfo)
        {
            ///若未启用外部供应商系统，TODO:考虑内部供应商模块如何处理
            if (string.IsNullOrEmpty(vmiTranDataEnableFlag) || vmiTranDataEnableFlag.ToLower() != "true") return string.Empty;

            ///交易记录NULL，肯定生成不了记录
            if (tranDetailsInfo == null) return string.Empty;
            if (warehouseInfo == null) return string.Empty;
            ///不是RDC类型的仓库的数据不发送给SRM
            if (warehouseInfo.WarehouseType.GetValueOrDefault() != (int)WarehouseTypeConstants.RDC) return string.Empty;
            ///非物料入库不传SRM
            if (tranDetailsInfo.TranType.GetValueOrDefault() != (int)WmmTranTypeConstants.Inbound) return string.Empty;

            if (tranDetailsInfo.TranOrderType.GetValueOrDefault() != 4010) return string.Empty;

            WmsTranOutInfo wmsTranOutInfo = new WmsTranOutInfo();
            
            // planmod转换为ordertype
            switch ((PullModeConstants)tranDetailsInfo.RunsheetType)
            {
                case PullModeConstants.Twd:
                    wmsTranOutInfo.SourceOrderType = (int)WmsOrderTypeConstants.Twd;
                    break;
                case PullModeConstants.Plan:
                    wmsTranOutInfo.SourceOrderType = (int)WmsOrderTypeConstants.Plan;
                    break;
                case PullModeConstants.Jis:
                    wmsTranOutInfo.SourceOrderType = (int)WmsOrderTypeConstants.Jis;
                    break;
            }


            wmsTranOutInfo.SourceOrderCode = tranDetailsInfo.TranNo;
            wmsTranOutInfo.RunsheetNo = tranDetailsInfo.RunsheetNo;
            wmsTranOutInfo.WmNo= tranDetailsInfo.WmNo;
            wmsTranOutInfo.Plant= tranDetailsInfo.Plant;
            
            wmsTranOutInfo.PartNo = tranDetailsInfo.PartNo;
            wmsTranOutInfo.DeliveryQty = tranDetailsInfo.ActualQty;
            wmsTranOutInfo.ProcessFlag = (int)ProcessFlagConstants.Untreated;
            wmsTranOutInfo.CreateUser = loginUser;
            return WmsTranOutDAL.GetInsertSql(wmsTranOutInfo);

        }
    }
}
