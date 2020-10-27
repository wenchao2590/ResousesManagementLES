using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class TranDetailsBLL
    {
        #region Common
        TranDetailsDAL dal = new TranDetailsDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<TranDetailsInfo></returns>
        public List<TranDetailsInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <returns></returns>
        public List<TranDetailsInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }

        public TranDetailsInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        public long InsertInfo(TranDetailsInfo info)
        {
            return dal.Add(info);
        }

        public bool LogicDeleteInfo(long id, string loginUser)
        {
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion

        #region TranDetailsInfo 对象属性填充

        #region Receive
        /// <summary>
        /// ReceiveDetailInfo=>TranDetailsInfo
        /// </summary>
        /// <param name="receiveDetailInfo"></param>
        /// <param name="tranDetailsInfo"></param>
        /// <returns></returns>
        public static void GetTranDetailsInfo(ReceiveDetailInfo receiveDetailInfo, ref TranDetailsInfo tranDetailsInfo)
        {
            ///单据号
            tranDetailsInfo.TranNo = receiveDetailInfo.TranNo;
            ///零件号
            tranDetailsInfo.PartNo = receiveDetailInfo.PartNo;
            ///零件中文名
            tranDetailsInfo.PartCname = receiveDetailInfo.PartCname;
            ///零件类别
            tranDetailsInfo.PartCls = receiveDetailInfo.PartCls;
            ///条码
            tranDetailsInfo.BarcodeData = receiveDetailInfo.BarcodeData;
            ///供应商
            tranDetailsInfo.SupplierNum = receiveDetailInfo.SupplierNum;
            ///工厂
            tranDetailsInfo.Plant = receiveDetailInfo.Plant;
            ///源仓库编码
            tranDetailsInfo.WmNo = receiveDetailInfo.WmNo;
            ///源存贮区编码
            tranDetailsInfo.ZoneNo = receiveDetailInfo.ZoneNo;
            ///源库位
            tranDetailsInfo.Dloc = receiveDetailInfo.Dloc;
            ///目的仓库
            tranDetailsInfo.TargetWm = receiveDetailInfo.TargetWm;
            ///目的存贮区
            tranDetailsInfo.TargetZone = receiveDetailInfo.TargetZone;
            ///目的库位
            tranDetailsInfo.TargetDloc = receiveDetailInfo.TargetDloc;
            ///单位
            tranDetailsInfo.MeasuringUnitNo = receiveDetailInfo.MeasuringUnitNo;
            ///包装类型
            tranDetailsInfo.PackageModel = receiveDetailInfo.PackageModel;
            ///单包装数
            tranDetailsInfo.Package = receiveDetailInfo.Package;
            ///需求包装数量
            tranDetailsInfo.RequiredPackageQty = receiveDetailInfo.RequiredBoxNum;
            ///需求数量
            tranDetailsInfo.RequiredQty = receiveDetailInfo.RequiredQty;
            ///实收包装数
            tranDetailsInfo.ActualPackageQty = receiveDetailInfo.ActualBoxNum;
            ///实收数量
            tranDetailsInfo.ActualQty = receiveDetailInfo.ActualQty;
            ///物料金额
            tranDetailsInfo.PartPrice = receiveDetailInfo.PartPrice;
            ///零件类
            tranDetailsInfo.BoxParts = receiveDetailInfo.BoxParts;
            ///捡料顺序号
            tranDetailsInfo.PickupSeqNo = receiveDetailInfo.PickupSeqNo;
            ///供应商库位
            tranDetailsInfo.RdcDloc = receiveDetailInfo.RdcDloc;
            ///DOCK
            tranDetailsInfo.Dock = receiveDetailInfo.Dock;
            ///上线包装型号
            tranDetailsInfo.InhousePackageModel = receiveDetailInfo.InhousePackageModel;
            ///上线包装数
            tranDetailsInfo.InhousePackage = receiveDetailInfo.InhousePackage;
            ///产地
            tranDetailsInfo.OriginPlace = receiveDetailInfo.OriginPlace;
            ///拉动单号
            tranDetailsInfo.RunsheetNo = receiveDetailInfo.RunsheetNo;
            ///备注
            tranDetailsInfo.Comments = receiveDetailInfo.Comments;

        }
        /// <summary>
        /// ReceiveInfo=>TranDetailsInfo
        /// </summary>
        /// <param name="receiveInfo"></param>
        /// <param name="tranDetailsInfo"></param>
        /// <returns></returns>
        public static void GetTranDetailsInfo(ReceiveInfo receiveInfo, ref TranDetailsInfo tranDetailsInfo)
        {
            ///交易时间
            tranDetailsInfo.TranDate = receiveInfo.TranTime;
            if (tranDetailsInfo.TranDate == null)
                tranDetailsInfo.TranDate = DateTime.Now;
            ///成本中心
            //tranDetailsInfo.CostCenter = receiveInfo.CostCenter;
            ///单据号
            if (string.IsNullOrEmpty(tranDetailsInfo.TranNo))
                tranDetailsInfo.TranNo = receiveInfo.ReceiveNo;
            ///保管员
            tranDetailsInfo.Keeper = receiveInfo.BookKeeper;
            ///仓储单据类型，INBOUND+1000，OUTBOUND+2000
            tranDetailsInfo.TranOrderType = receiveInfo.ReceiveType.GetValueOrDefault() + 1000;
            ///若有入库单来源则对交易单据类型进行重新标识
            //if (receiveInfo.SourceCreateType != null)
            //    tranDetailsInfo.TranOrderType = receiveInfo.ReceiveType.GetValueOrDefault() + receiveInfo.SourceCreateType.GetValueOrDefault() * 100;
            ///拉动方式
            //tranDetailsInfo.RunsheetType = receiveInfo.PullMode;
            ///特殊入库类型处理
            switch (receiveInfo.ReceiveType.GetValueOrDefault())
            {
                ///返利入库 -> 无价值物料入库
                case (int)InboundTypeConstants.RebateInbound:
                    tranDetailsInfo.TranType = (int)WmmTranTypeConstants.Inbound;
                    tranDetailsInfo.SettledFlag = true;///无价值入库为已结算状态，无须付款而已
                    break;
                ///物料预留 -> 冻结入库
                case (int)InboundTypeConstants.ReserveInbound:
                    tranDetailsInfo.TranType = (int)WmmTranTypeConstants.FrozenInbound;
                    tranDetailsInfo.SettledFlag = true;///物料预留需要结算
                    break;
                ///采购订单 -> 物料入库
                case (int)InboundTypeConstants.PurchaseOrder:
                    tranDetailsInfo.TranType = (int)WmmTranTypeConstants.Inbound;
                    tranDetailsInfo.SettledFlag = true;///需要结算
                    break;
                default:
                    tranDetailsInfo.TranType = (int)WmmTranTypeConstants.Inbound;
                    tranDetailsInfo.SettledFlag = null;
                    break;
            }
        }
        /// <summary>
        /// 是否关注结算?
        /// </summary>
        /// <param name="settledFlag"></param>
        /// <param name="tranDetailsInfo"></param>
        public static void GetTranDetailsInfo(bool? settledFlag, ref TranDetailsInfo tranDetailsInfo)
        {
            ///如果被定义为需要结算，则不再进行变更
            if (tranDetailsInfo.SettledFlag.GetValueOrDefault()) return;
            ///是否结算
            tranDetailsInfo.SettledFlag = settledFlag;
        }

        /// <summary>
        /// 根据匹配库存数据更新交易记录
        /// </summary>
        /// <param name="stocksInfo"></param>
        /// <param name="tranDetailsInfo"></param>
        public static void GetTranDetailsInfo(StocksInfo stocksInfo, ref TranDetailsInfo tranDetailsInfo)
        {
            if (stocksInfo == null) return;
            ///是否已结算
            tranDetailsInfo.SettledFlag = stocksInfo.SettledFlag;
            ///库存的逻辑外键
            //tranDetailsInfo.StocksFid = stocksInfo.Fid;
            ///来源库位
            tranDetailsInfo.Dloc = stocksInfo.Dloc;
            ///匹配数量
            if (stocksInfo.MatchedQty != null)
            {
                tranDetailsInfo.ActualQty = stocksInfo.MatchedQty;
                ///重新计算包装数量
                if (tranDetailsInfo.Package == null || tranDetailsInfo.Package.GetValueOrDefault() == 0)
                    tranDetailsInfo.Package = 1;
                tranDetailsInfo.ActualPackageQty = Convert.ToInt32(Math.Ceiling(stocksInfo.MatchedQty.GetValueOrDefault() / tranDetailsInfo.Package.GetValueOrDefault()));
            }
            ///如果没有供应商则根据库存数据中的供应商赋值
            if (string.IsNullOrEmpty(tranDetailsInfo.SupplierNum))
                tranDetailsInfo.SupplierNum = stocksInfo.SupplierNum;
            ///SETTLED_FLAG
        }
        /// <summary>
        /// 用于单据衔接
        /// </summary>
        /// <param name="stocksInfo"></param>
        /// <param name="tranDetailsInfo"></param>
        public static void GetTranDetailsInfo2(StocksInfo stocksInfo, ref TranDetailsInfo tranDetailsInfo)
        {
            if (stocksInfo == null) return;
            tranDetailsInfo.BatchNo = stocksInfo.BatchNo;
            tranDetailsInfo.PartNo = stocksInfo.PartNo;
            tranDetailsInfo.BarcodeData = stocksInfo.BarcodeData;
            tranDetailsInfo.WmNo = stocksInfo.WmNo;
            tranDetailsInfo.ZoneNo = stocksInfo.ZoneNo;
            tranDetailsInfo.Dloc = stocksInfo.Dloc;
            tranDetailsInfo.MeasuringUnitNo = stocksInfo.PartUnits;
            tranDetailsInfo.Package = stocksInfo.Package;
            tranDetailsInfo.Max = stocksInfo.Max;
            tranDetailsInfo.Min = stocksInfo.Min;
            tranDetailsInfo.SupplierNum = stocksInfo.SupplierNum;
            tranDetailsInfo.PartCname = stocksInfo.PartCname;
            tranDetailsInfo.PartNickname = stocksInfo.PartNickname;
            tranDetailsInfo.PackageModel = stocksInfo.PackageModel;
            tranDetailsInfo.PartCls = stocksInfo.PartCls;
            tranDetailsInfo.PartUnits = stocksInfo.PartUnits;
            tranDetailsInfo.IsBatch = stocksInfo.IsBatch;
            //tranDetailsInfo.OriginPlace = stocksInfo.OriginPlace;
            tranDetailsInfo.PartPrice = stocksInfo.PartPrice;
            //tranDetailsInfo.CostCenter = stocksInfo.CostCenter;
            //tranDetailsInfo.SettledFlag = stocksInfo.SettledFlag;
            tranDetailsInfo.Keeper = stocksInfo.Keeper;
            tranDetailsInfo.StocksFid = stocksInfo.Fid;
        }
        #endregion

        #region Output
        /// <summary>
        /// OutputDetailInfo=>TranDetailsInfo
        /// </summary>
        /// <param name="outputDetailInfo"></param>
        /// <param name="tranDetailsInfo"></param>
        /// <returns></returns>
        public static void GetTranDetailsInfo(OutputDetailInfo outputDetailInfo, ref TranDetailsInfo tranDetailsInfo)
        {
            if (outputDetailInfo == null) return;
            ///单据号
            tranDetailsInfo.TranNo = outputDetailInfo.TranNo;
            ///零件号
            tranDetailsInfo.PartNo = outputDetailInfo.PartNo;
            ///零件中文名
            tranDetailsInfo.PartCname = outputDetailInfo.PartCname;
            ///零件类别
            tranDetailsInfo.PartCls = outputDetailInfo.PartCls;
            ///条码
            tranDetailsInfo.BarcodeData = outputDetailInfo.BarcodeData;
            ///供应商
            tranDetailsInfo.SupplierNum = outputDetailInfo.SupplierNum;
            ///工厂
            tranDetailsInfo.Plant = outputDetailInfo.Plant;
            ///源仓库编码
            tranDetailsInfo.WmNo = outputDetailInfo.WmNo;
            ///源存贮区编码
            tranDetailsInfo.ZoneNo = outputDetailInfo.ZoneNo;
            ///源库位
            tranDetailsInfo.Dloc = outputDetailInfo.Dloc;
            ///目的仓库
            tranDetailsInfo.TargetWm = outputDetailInfo.TargetWm;
            ///目的存贮区
            tranDetailsInfo.TargetZone = outputDetailInfo.TargetZone;
            ///目的库位
            tranDetailsInfo.TargetDloc = outputDetailInfo.TargetDloc;
            ///单位
            tranDetailsInfo.MeasuringUnitNo = outputDetailInfo.MeasuringUnitNo;
            ///包装类型
            tranDetailsInfo.PackageModel = outputDetailInfo.PackageModel;
            ///单包装数
            tranDetailsInfo.Package = outputDetailInfo.Package;
            ///需求包装数量
            tranDetailsInfo.RequiredPackageQty = outputDetailInfo.RequiredBoxNum;
            ///需求数量
            tranDetailsInfo.RequiredQty = outputDetailInfo.RequiredQty;
            ///实收包装数
            tranDetailsInfo.ActualPackageQty = outputDetailInfo.ActualBoxNum;
            ///实收数量
            tranDetailsInfo.ActualQty = outputDetailInfo.ActualQty;
            ///物料金额
            tranDetailsInfo.PartPrice = outputDetailInfo.PartPrice;
            ///零件类
            tranDetailsInfo.BoxParts = outputDetailInfo.BoxParts;
            ///捡料顺序号
            tranDetailsInfo.PickupSeqNo = outputDetailInfo.PickupSeqNo;
            ///供应商库位
            tranDetailsInfo.RdcDloc = outputDetailInfo.RdcDloc;
            ///DOCK
            tranDetailsInfo.Dock = outputDetailInfo.Dock;
            ///上线包装型号
            tranDetailsInfo.InhousePackageModel = outputDetailInfo.InhousePackageModel;
            ///上线包装数
            tranDetailsInfo.InhousePackage = outputDetailInfo.InhousePackage;
            ///产地
            tranDetailsInfo.OriginPlace = outputDetailInfo.OriginPlace;
            ///拉动单号
            tranDetailsInfo.RunsheetNo = outputDetailInfo.RunsheetNo;
            ///备注
            tranDetailsInfo.Comments = outputDetailInfo.Comments;

            ///如果当前交易类型为状态冻结，而出库单明细中已标记为冻结库存，则不能重复进行冻结返回不产生交易记录
            if (tranDetailsInfo.TranType.GetValueOrDefault() == (int)WmmTranTypeConstants.StateFreezing && outputDetailInfo.FrozenStockFlag.GetValueOrDefault())
                tranDetailsInfo.TranType = (int)WmmTranTypeConstants.None;
        }
        /// <summary>
        /// OutputInfo=>TranDetailsInfo
        /// </summary>
        /// <param name="outputInfo"></param>
        /// <param name="tranDetailsInfo"></param>
        /// <returns></returns>
        public static void GetTranDetailsInfo(OutputInfo outputInfo, ref TranDetailsInfo tranDetailsInfo)
        {
            ///交易时间
            tranDetailsInfo.TranDate = outputInfo.TranTime;
            if (tranDetailsInfo.TranDate == null)
                tranDetailsInfo.TranDate = DateTime.Now;
            ///成本中心
            //tranDetailsInfo.CostCenter = outputInfo.CostCenter;
            ///单据号
            if (string.IsNullOrEmpty(tranDetailsInfo.TranNo))
                tranDetailsInfo.TranNo = outputInfo.OutputNo;
            ///没有目标仓库存储区时为出库
            if (string.IsNullOrEmpty(outputInfo.WmNo) && string.IsNullOrEmpty(outputInfo.ZoneNo) && tranDetailsInfo.TranType.GetValueOrDefault() == (int)WmmTranTypeConstants.Movement)
                tranDetailsInfo.TranType = (int)WmmTranTypeConstants.Outbound;
            ///仓储单据类型，INBOUND+1000，OUTBOUND+2000
            tranDetailsInfo.TranOrderType = outputInfo.OutputType.GetValueOrDefault() + 2000;
            ///拉动方式
            //tranDetailsInfo.RunsheetType = outputInfo.PullMode;
        }
        #endregion

        /// <summary>
        /// MaintainInhouseLogisticStandardInfo=>TranDetailsInfo
        /// </summary>
        /// <param name="maintainInhouseLogisticStandardInfo"></param>
        /// <param name="tranDetailsInfo"></param>
        /// <returns></returns>
        public static void GetTranDetailsInfo(MaintainInhouseLogisticStandardInfo maintainInhouseLogisticStandardInfo,
            string stockCheckLockMaterialSyncUpdatePartStockFlag,
            ref TranDetailsInfo tranDetailsInfo)
        {
            if (maintainInhouseLogisticStandardInfo == null) return;
            ///工位
            tranDetailsInfo.Location = maintainInhouseLogisticStandardInfo.Location;
            ///库位
            tranDetailsInfo.StorageLocation = maintainInhouseLogisticStandardInfo.StorageLocation;
            /////是否排查锁定库存
            //if (stockCheckLockMaterialSyncUpdatePartStockFlag.ToLower() == "true"
            //    && maintainInhouseLogisticStandardInfo.CheckMaterialStockFlag.GetValueOrDefault())

                 ///是否排查锁定库存
            if (stockCheckLockMaterialSyncUpdatePartStockFlag.ToLower() == "true")
            {
                ///将物料出库交易类型变更为物料解冻
                if (tranDetailsInfo.TranType.GetValueOrDefault() == (int)WmmTranTypeConstants.Outbound)
                    tranDetailsInfo.TranType = (int)WmmTranTypeConstants.MaterialThawing;
                if (tranDetailsInfo.TranType.GetValueOrDefault() == (int)WmmTranTypeConstants.StateFreezing)
                    tranDetailsInfo.TranType = (int)WmmTranTypeConstants.None;
            }
        }

        #endregion
        /// <summary>
        /// 创建交易记录
        /// </summary>
        /// <param name="tranType"></param>
        /// <param name="tranState"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static TranDetailsInfo CreateTranDetailsInfo(int tranType, int tranState, string loginUser)
        {
            TranDetailsInfo tranDetailsInfo = new TranDetailsInfo();
            ///交易类型
            tranDetailsInfo.TranType = tranType;
            ///交易状态
            tranDetailsInfo.TranState = tranState;
            ///CreateUser
            tranDetailsInfo.CreateUser = loginUser;
            ///CreateDate
            tranDetailsInfo.CreateDate = DateTime.Now;
            ///ValidFlag
            tranDetailsInfo.ValidFlag = true;
            ///Fid
            tranDetailsInfo.Fid = Guid.NewGuid();

            return tranDetailsInfo;
        }
        /// <summary>
        /// 根据出库类型获取交易类型和结算标记
        /// </summary>
        /// <param name="outboundType"></param>
        /// <param name="wmmTranType"></param>
        /// <param name="settledFlag"></param>
        public static void CloseWmmTranType(int outboundType, int outputStatus, out int wmmTranType, out bool? settledFlag)
        {
            ///是否关注结算标记
            settledFlag = null;
            switch (outboundType)
            {
                ///余料退库,线边已结算物料移动至其它库区
                case (int)OutboundTypeConstants.ExcessStockWithDrawing:
                    ///余料退库一定是已结算的物料
                    settledFlag = true;
                    wmmTranType = (int)WmmTranTypeConstants.Movement;
                    break;
                ///料废兑换
                case (int)OutboundTypeConstants.MaterialWasteExchange:
                    wmmTranType = (int)WmmTranTypeConstants.None;
                    ///如果单据已完成，则说明事先有原地冻结的操作，此时则生成冻结移动事务
                    if (outputStatus == (int)WmmOrderStatusConstants.Completed)
                        wmmTranType = (int)WmmTranTypeConstants.FrozenMovement;
                    ///如果单据仅为已发布状态则直接产生物料冻结事务
                    if (outputStatus == (int)WmmOrderStatusConstants.Published)
                        wmmTranType = (int)WmmTranTypeConstants.MaterialFreezing;
                    break;
                ///物料退货(退供应商)
                case (int)OutboundTypeConstants.MaterialReturns:
                    ///物料退货一定是已结算的物料，否则就走线下进行替换？TODO:
                    settledFlag = true;
                    wmmTranType = (int)WmmTranTypeConstants.None;
                    if (outputStatus == (int)WmmOrderStatusConstants.Completed)
                        wmmTranType = (int)WmmTranTypeConstants.MaterialThawing;
                    if (outputStatus == (int)WmmOrderStatusConstants.Published)
                        wmmTranType = (int)WmmTranTypeConstants.Outbound;
                    break;
                ///物料冻结
                case (int)OutboundTypeConstants.MaterialFrozen:
                    wmmTranType = (int)WmmTranTypeConstants.MaterialFreezing;
                    break;
                ///物料解冻
                case (int)OutboundTypeConstants.MaterialToThaw:
                    wmmTranType = (int)WmmTranTypeConstants.MaterialThawing;
                    break;
                ///物料预留
                case (int)OutboundTypeConstants.ReserveOutbound:
                    wmmTranType = (int)WmmTranTypeConstants.FrozenOutbound;
                    settledFlag = true;
                    break;
                ///TODO:物料封存
                ///TODO:物料解封
                ///正常出库
                default:
                    wmmTranType = (int)WmmTranTypeConstants.None;
                    ///如果单据已完成，则说明事先有原地冻结的操作，此时则生成物料解冻事务
                    if (outputStatus == (int)WmmOrderStatusConstants.Completed)
                        wmmTranType = (int)WmmTranTypeConstants.MaterialThawing;
                    ///如果单据仅为已发布状态则直接产生移库(出库)事务
                    if (outputStatus == (int)WmmOrderStatusConstants.Published)
                        wmmTranType = (int)WmmTranTypeConstants.Movement;
                    break;
            }
        }
        /// <summary>
        /// 拣选完成时的交易类型
        /// </summary>
        /// <param name="outboundType"></param>
        /// <param name="wmmTranType"></param>
        /// <param name="settledFlag"></param>
        public static void CompleteWmmTranType(int outboundType, out int wmmTranType, out bool? settledFlag)
        {
            ///是否关注结算标记
            settledFlag = null;
            switch (outboundType)
            {
                ///余料退库,线边已结算物料移动至其它库区
                case (int)OutboundTypeConstants.ExcessStockWithDrawing:
                    wmmTranType = (int)WmmTranTypeConstants.None;
                    break;
                ///料废兑换，首先进行原地冻结，再可用移至其它库区冻结
                case (int)OutboundTypeConstants.MaterialWasteExchange:
                    wmmTranType = (int)WmmTranTypeConstants.StateFreezing;
                    break;
                ///物料退货(退供应商)
                case (int)OutboundTypeConstants.MaterialReturns:
                    ///物料退货一定是已结算的物料，否则就走线下进行替换？TODO:
                    wmmTranType = (int)WmmTranTypeConstants.StateFreezing;
                    break;
                ///物料冻结
                case (int)OutboundTypeConstants.MaterialFrozen:
                    wmmTranType = (int)WmmTranTypeConstants.None;
                    break;
                ///物料解冻
                case (int)OutboundTypeConstants.MaterialToThaw:
                    wmmTranType = (int)WmmTranTypeConstants.None;
                    break;
                ///物料预留
                case (int)OutboundTypeConstants.ReserveOutbound:
                    wmmTranType = (int)WmmTranTypeConstants.None;
                    break;
                ///TODO:物料封存
                ///TODO:物料解封
                ///正常出库、移库，拣选需要状态冻结
                default: wmmTranType = (int)WmmTranTypeConstants.StateFreezing; break;
            }
        }





        #region Interface
        /// <summary>
        /// Create TranDetailsInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>TranDetailsInfo</returns>
        public static TranDetailsInfo CreateTranDetailsInfo(string loginUser)
        {
            TranDetailsInfo info = new TranDetailsInfo();
            ///ID
            info.Id = 0;
            ///FID
            info.Fid = Guid.NewGuid();
            ///PLANT
            info.Plant = null;
            ///BATCH_NO
            info.BatchNo = null;
            ///BARCODE_DATA
            info.BarcodeData = null;
            ///NUM
            info.Num = null;
            ///BOX_NUM
            info.BoxNum = null;
            ///BOX_PARTS
            info.BoxParts = null;
            ///PICKUP_SEQ_NO
            info.PickupSeqNo = null;
            ///RDC_DLOC
            info.RdcDloc = null;
            ///INNER_LOCATION
            info.InnerLocation = null;
            ///LOCATION
            info.Location = null;
            ///STORAGE_LOCATION
            info.StorageLocation = null;
            ///REQUIRED_PACKAGE_QTY
            info.RequiredPackageQty = null;
            ///REQUIRED_QTY
            info.RequiredQty = null;
            ///BARCODE_TYPE
            info.BarcodeType = null;
            ///REQUIRED_DATE
            info.RequiredDate = null;
            ///PACHAGE_TYPE
            info.PachageType = null;
            ///LINE_POSITION
            info.LinePosition = null;
            ///RUNSHEET_NO
            info.RunsheetNo = null;
            ///DOCK
            info.Dock = null;
            ///PART_PRICE
            info.PartPrice = null;
            ///COST_CENTER
            info.CostCenter = null;
            ///COMMENTS
            info.Comments = null;
            ///SETTLED_FLAG
            info.SettledFlag = null;
            ///STOCKS_FID
            info.StocksFid = null;
            ///VALID_FLAG
            info.ValidFlag = true;
            ///CREATE_USER
            info.CreateUser = loginUser;
            ///CREATE_DATE
            info.CreateDate = DateTime.Now;
            ///MODIFY_USER
            info.ModifyUser = null;
            ///MODIFY_DATE
            info.ModifyDate = null;
            ///TRAN_ORDER_TYPE
            info.TranOrderType = null;
            ///RUNSHEET_TYPE
            info.RunsheetType = null;
            ///TRAN_STATE
            info.TranState = (int)WmmTranStateConstants.Created;
            return info;
        }
        /// <summary>
        /// WmsVmiTranDetailInfo -> TranDetailsInfo
        /// </summary>
        /// <param name="wmsVmiTranDetailInfo"></param>
        /// <param name="tranDetailsInfo"></param>
        public static void GetTranDetailsInfo(WmsVmiTranDetailInfo wmsVmiTranDetailInfo, ref TranDetailsInfo tranDetailsInfo)
        {
            if (wmsVmiTranDetailInfo == null) return;
            ///PART_NO
            tranDetailsInfo.PartNo = wmsVmiTranDetailInfo.PartNo;
            ///SUPPLIER_CODE
            tranDetailsInfo.SupplierNum = wmsVmiTranDetailInfo.SupplierCode;
            ///ORDER_NO
            tranDetailsInfo.TranNo = wmsVmiTranDetailInfo.OrderNo;
            ///QTY
            tranDetailsInfo.ActualQty = wmsVmiTranDetailInfo.Qty;
            ///TRAN_TYPE，DP-收货，WD-出库（负数），AJ-调整（正负数都有）
            if (tranDetailsInfo.ActualQty.GetValueOrDefault() >= 0)
            {
                tranDetailsInfo.TranType = (int)WmmTranTypeConstants.Inbound;
                ///TARGET_WM
                tranDetailsInfo.TargetWm = wmsVmiTranDetailInfo.VmiWarehouseCode;
                ///TARGET_ZONE
                tranDetailsInfo.TargetZone = wmsVmiTranDetailInfo.ZoneNo;
                ///TARGET_DLOC
                tranDetailsInfo.TargetDloc = wmsVmiTranDetailInfo.Dloc;
            }
            else
            {
                tranDetailsInfo.TranType = (int)WmmTranTypeConstants.Outbound;
                tranDetailsInfo.ActualQty = 0 - tranDetailsInfo.ActualQty.GetValueOrDefault();
                ///WM_NO
                tranDetailsInfo.WmNo = wmsVmiTranDetailInfo.VmiWarehouseCode;
                ///ZONE_NO
                tranDetailsInfo.ZoneNo = wmsVmiTranDetailInfo.ZoneNo;
                ///DLOC
                tranDetailsInfo.Dloc = wmsVmiTranDetailInfo.Dloc;
            }
            ///TIMES
            tranDetailsInfo.TranDate = wmsVmiTranDetailInfo.Times;
        }
        /// <summary>
        /// MaintainPartsInfo -> TranDetailsInfo
        /// </summary>
        /// <param name="maintainPartsInfo"></param>
        /// <param name="tranDetailsInfo"></param>
        public static void GetTranDetailsInfo(MaintainPartsInfo maintainPartsInfo, ref TranDetailsInfo tranDetailsInfo)
        {
            if (maintainPartsInfo == null) return;
            ///PART_NO
            tranDetailsInfo.PartNo = maintainPartsInfo.PartNo;
            ///MEASURING_UNIT_NO
            tranDetailsInfo.MeasuringUnitNo = maintainPartsInfo.PartUnits;
            ///PART_CNAME
            tranDetailsInfo.PartCname = maintainPartsInfo.PartCname;
            ///PART_NICKNAME
            tranDetailsInfo.PartNickname = maintainPartsInfo.PartNickname;
            ///PART_CLS
            tranDetailsInfo.PartCls = maintainPartsInfo.PartCls;
            ///PART_UNITS
            tranDetailsInfo.PartUnits = maintainPartsInfo.PartUnits;
            ///ORIGIN_PLACE
           // tranDetailsInfo.OriginPlace = maintainPartsInfo.OriginPlace;
        }
        /// <summary>
        /// PartsStockInfo -> TranDetailsInfo
        /// </summary>
        /// <param name="partsStockInfo"></param>
        /// <param name="tranDetailsInfo"></param>
        /// <returns></returns>
        public static void GetTranDetailsInfo(PartsStockInfo partsStockInfo, ref TranDetailsInfo tranDetailsInfo)
        {
            if (partsStockInfo == null) return;
            ///零件呢称
            tranDetailsInfo.PartNickname = partsStockInfo.PartNickname;
            ///是否按批次
            tranDetailsInfo.IsBatch = partsStockInfo.IsBatch;
            ///MAX
            tranDetailsInfo.Max = partsStockInfo.Max;
            ///MIN
            tranDetailsInfo.Min = partsStockInfo.Min;
            ///PACKAGE
            tranDetailsInfo.Package = partsStockInfo.InboundPackage;
            ///PACKAGE_MODEL
            tranDetailsInfo.PackageModel = partsStockInfo.InboundPackageModel;
            ///KEEPER
            tranDetailsInfo.Keeper = partsStockInfo.Keeper;
            ///INHOUSE_PACKAGE_MODEL
            tranDetailsInfo.InhousePackageModel = partsStockInfo.InhousePackageModel;
            ///INHOUSE_PACKAGE
            tranDetailsInfo.InhousePackage = partsStockInfo.InhousePackage;
        }
        /// <summary>
        /// SupplierInfo -> TranDetailsInfo
        /// </summary>
        /// <param name="supplierInfo"></param>
        /// <param name="tranDetailsInfo"></param>
        /// <returns></returns>
        public static void GetTranDetailsInfo(SupplierInfo supplierInfo, ref TranDetailsInfo tranDetailsInfo)
        {
            if (supplierInfo == null) return;
            ///供应商名称
            tranDetailsInfo.SupplierName = supplierInfo.SupplierName;
            ///供应商简称
            tranDetailsInfo.SupplierSname = supplierInfo.SupplierSname;
        }
        /// <summary>
        /// 包装数量计算
        /// </summary>
        /// <param name="tranDetailsInfo"></param>
        public static void CalculateTranDetailsInfo(ref TranDetailsInfo tranDetailsInfo)
        {
            ///ACTUAL_PACKAGE_QTY
            if (tranDetailsInfo.ActualPackageQty.GetValueOrDefault() == 0 && tranDetailsInfo.Package.GetValueOrDefault() > 0)
                tranDetailsInfo.ActualPackageQty = Convert.ToInt32(Math.Ceiling(tranDetailsInfo.ActualQty.GetValueOrDefault() / tranDetailsInfo.Package.GetValueOrDefault()));
        }
        #endregion

    }
}

