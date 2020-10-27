using DAL.LES;
using DAL.SYS;
using DM.LES;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BLL.LES
{
    /// <summary>
    /// StocksBLL
    /// </summary>
    public class StocksBLL
    {
        #region Common
        /// <summary>
        /// StocksDAL
        /// </summary>
        StocksDAL dal = new StocksDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<StocksInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
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
        public List<StocksInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StocksInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(StocksInfo info)
        {
            return dal.Add(info);
        }
        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion
        /// <summary>
        /// 根据根本条件获取合计库存
        /// 根本条件：物料号+库位+存储区+仓库
        /// </summary>
        /// <param name="partNo"></param>
        /// <param name="wmNo"></param>
        /// <param name="zoneNo"></param>
        /// <param name="dloc"></param>
        /// <returns></returns>
        public decimal GetPartStocks(string partNo, string wmNo, string zoneNo, string dloc)
        {
            return dal.GetPartStocks(partNo, wmNo, zoneNo, dloc);
        }
        /// <summary>
        /// 创建库存对象(初始化)
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public StocksInfo CreateStocksInfo(string loginUser)
        {
            StocksInfo stocksInfo = new StocksInfo();

            ///CreateUser
            stocksInfo.CreateUser = loginUser;
            ///CreateDate
            stocksInfo.CreateDate = DateTime.Now;
            ///ValidFlag
            stocksInfo.ValidFlag = true; 
            ///Fid
            //stocksInfo.Fid = Guid.NewGuid();
            return stocksInfo;
        }
        /// <summary>
        /// 更新物料基础数据
        /// </summary>
        /// <param name="partNo"></param>
        /// <param name="plant"></param>
        /// <param name="stocksInfo"></param>
        public void UpdateMaintainPartsInfo(MaintainPartsInfo maintainPartsInfo, ref StocksInfo stocksInfo)
        {
            if (maintainPartsInfo == null) return;
            ///信息员
            stocksInfo.Informationer = maintainPartsInfo.InfoPerson;
        }
        /// <summary>
        /// 根据交易记录获取来源库存对象
        /// TranDetailsInfo=>StocksInfo
        /// </summary>
        /// <param name="tranDetailsInfo"></param>
        /// <param name="stocksInfo"></param>
        public void GetSourceStocksInfo(TranDetailsInfo tranDetailsInfo, ref StocksInfo stocksInfo)
        {
            ///工厂
            stocksInfo.Plant = tranDetailsInfo.Plant;
            ///供应商
            stocksInfo.SupplierNum = tranDetailsInfo.SupplierNum;
            ///零件号
            stocksInfo.PartNo = tranDetailsInfo.PartNo;
            ///零件中文名
            stocksInfo.PartCname = tranDetailsInfo.PartCname;
            ///零件呢称
            stocksInfo.PartNickname = tranDetailsInfo.PartNickname;
            ///零件单位
            stocksInfo.PartUnits = tranDetailsInfo.MeasuringUnitNo;
            ///标准包装型号
            stocksInfo.PackageModel = tranDetailsInfo.PackageModel;
            ///标准包装数量
            stocksInfo.Package = Convert.ToInt32( tranDetailsInfo.Package.GetValueOrDefault());
            ///存贮区编码
            stocksInfo.ZoneNo = tranDetailsInfo.ZoneNo;
            ///仓库编码
            stocksInfo.WmNo = tranDetailsInfo.WmNo;
            ///库位
            stocksInfo.Dloc = tranDetailsInfo.Dloc;
            ///Max
            stocksInfo.Max = tranDetailsInfo.Max;
            ///Min
            stocksInfo.Min = tranDetailsInfo.Min;
            ///库存
            stocksInfo.Stocks = tranDetailsInfo.ActualPackageQty;
            ///是否批次管理
            stocksInfo.IsBatch = tranDetailsInfo.IsBatch;
            ///库存数（件）
            stocksInfo.StocksNum = tranDetailsInfo.ActualQty;
            ///零件类别
            stocksInfo.PartCls = tranDetailsInfo.PartCls;
            ///批次
            stocksInfo.BatchNo = tranDetailsInfo.BatchNo;
            ///条码
            stocksInfo.BarcodeData = tranDetailsInfo.BarcodeData;
            ///条码类型
            stocksInfo.BarcodeType = tranDetailsInfo.BarcodeType;
            ///产地
            //stocksInfo.OriginPlace = tranDetailsInfo.OriginPlace;
            /////采购物料金额
            //stocksInfo.PurchasePartPrice = tranDetailsInfo.PartPrice;
            /////销售物料金额
            //stocksInfo.SalePartPrice = tranDetailsInfo.PartPrice;
            /////成本中心
            //stocksInfo.CostCenter = tranDetailsInfo.CostCenter;
            ///保管员
            stocksInfo.Keeper = tranDetailsInfo.Keeper;
            ///是否已结算
            stocksInfo.SettledFlag = tranDetailsInfo.SettledFlag.GetValueOrDefault();
        }

        /// <summary>
        /// 根据仓储信息获取库存对象
        /// </summary>
        /// <param name="partsStockInfo"></param>
        /// <param name="stocksInfo"></param>
        public void GetPartsStocksInfo(PartsStockInfo partsStockInfo, ref StocksInfo stocksInfo)
        {
            ///工厂
            stocksInfo.Plant = partsStockInfo.Plant;
            ///供应商
            stocksInfo.SupplierNum = partsStockInfo.SupplierNum;
            ///零件号
            stocksInfo.PartNo = partsStockInfo.PartNo;
            ///零件中文名
            stocksInfo.PartCname = partsStockInfo.PartCname;
            ///零件呢称
            stocksInfo.PartNickname = partsStockInfo.PartNickname;
            ///零件单位
            stocksInfo.PartUnits = partsStockInfo.PartUnits;
            ///标准包装型号
            stocksInfo.PackageModel = partsStockInfo.PackageModel;
            ///标准包装数量
            stocksInfo.Package = partsStockInfo.Package;
            ///存贮区编码
            stocksInfo.ZoneNo = partsStockInfo.ZoneNo;
            ///仓库编码
            stocksInfo.WmNo = partsStockInfo.WmNo;
            ///库位
            stocksInfo.Dloc = partsStockInfo.Dloc;
            ///Max
            stocksInfo.Max = partsStockInfo.Max;
            ///Min
            stocksInfo.Min = partsStockInfo.Min;
            ///库存
            stocksInfo.Stocks = 0;
            ///是否批次管理
            stocksInfo.IsBatch = partsStockInfo.IsBatch;
            ///库存数（件）
            stocksInfo.StocksNum = 0;
            ///零件类别
            stocksInfo.PartCls = partsStockInfo.PartCls;
            ///批次
            stocksInfo.BatchNo = null;
            ///条码
            stocksInfo.BarcodeData = null;
            ///条码类型
            stocksInfo.BarcodeType = null;
            ///产地
            //stocksInfo.OriginPlace = null;
            ///采购物料金额
            //stocksInfo.PurchasePartPrice = null;
            /////销售物料金额
            //stocksInfo.SalePartPrice = null;
            /////成本中心
            //stocksInfo.CostCenter = null;
            ///保管员
            stocksInfo.Keeper = partsStockInfo.Keeper;
            ///是否已结算
            stocksInfo.SettledFlag = false;
        }
        /// <summary>
        /// 根据交易记录获取目标库存对象
        /// TranDetailsInfo=>StocksInfo
        /// </summary>
        /// <param name="tranDetailsInfo"></param>
        /// <param name="stocksInfo"></param>
        public void GetTargetStocksInfo(TranDetailsInfo tranDetailsInfo, ZonesInfo zonesInfo, ref StocksInfo stocksInfo)
        {
            ///工厂
            stocksInfo.Plant = tranDetailsInfo.Plant;
            ///供应商
            stocksInfo.SupplierNum = tranDetailsInfo.SupplierNum;
            ///零件号
            stocksInfo.PartNo = tranDetailsInfo.PartNo;
            ///零件中文名
            stocksInfo.PartCname = tranDetailsInfo.PartCname;
            ///零件呢称
            stocksInfo.PartNickname = tranDetailsInfo.PartNickname;
            ///零件单位
            stocksInfo.PartUnits = tranDetailsInfo.MeasuringUnitNo;
            ///标准包装型号
            stocksInfo.PackageModel = tranDetailsInfo.PackageModel;
            ///标准包装数量
            stocksInfo.Package = Convert.ToInt32( tranDetailsInfo.Package);
            ///存贮区编码
            stocksInfo.ZoneNo = tranDetailsInfo.TargetZone;
            ///仓库编码
            stocksInfo.WmNo = tranDetailsInfo.TargetWm;
            ///库位
            stocksInfo.Dloc = tranDetailsInfo.TargetDloc;
            ///Max
            stocksInfo.Max = tranDetailsInfo.Max;
            ///Min
            stocksInfo.Min = tranDetailsInfo.Min;
            ///库存
            stocksInfo.Stocks = tranDetailsInfo.ActualPackageQty;
            ///是否批次管理
            stocksInfo.IsBatch = tranDetailsInfo.IsBatch;
            ///库存数（件）
            stocksInfo.StocksNum = tranDetailsInfo.ActualQty;
            ///零件类别
            stocksInfo.PartCls = tranDetailsInfo.PartCls;
            ///批次
            stocksInfo.BatchNo = tranDetailsInfo.BatchNo;
            ///条码
            stocksInfo.BarcodeData = tranDetailsInfo.BarcodeData;
            ///条码类型
            stocksInfo.BarcodeType = tranDetailsInfo.BarcodeType;
            ///产地
            //stocksInfo.OriginPlace = tranDetailsInfo.OriginPlace;
            /////采购物料金额
            //stocksInfo.PurchasePartPrice = tranDetailsInfo.PartPrice;
            /////销售物料金额
            //stocksInfo.SalePartPrice = tranDetailsInfo.PartPrice;
            /////成本中心
            //stocksInfo.CostCenter = tranDetailsInfo.CostCenter;
            ///保管员
            stocksInfo.Keeper = tranDetailsInfo.Keeper;
            ///是否已结算
            stocksInfo.SettledFlag = tranDetailsInfo.SettledFlag.GetValueOrDefault();
            if (zonesInfo == null) return;
            //if (zonesInfo.SettlementFlag.GetValueOrDefault() && !tranDetailsInfo.SettledFlag.GetValueOrDefault())
            //    stocksInfo.SettledFlag = true;
        }
        /// <summary>
        /// 散装物料数量计算
        /// </summary>
        /// <param name="stocksInfo"></param>
        public void StockCalculation(ref StocksInfo stocksInfo)
        {
            if (stocksInfo.Stocks.GetValueOrDefault() == 0)
                stocksInfo.FragmentNum = stocksInfo.StocksNum;
            else
            {
                stocksInfo.FragmentNum///散装物料数量
                    = stocksInfo.Stocks.GetValueOrDefault()///包装数
                    * (stocksInfo.Package.GetValueOrDefault() == 0 ? 1 : stocksInfo.Package.GetValueOrDefault())///单包装数
                    - stocksInfo.StocksNum.GetValueOrDefault();///物料数量
            }
        }
        /// <summary>
        /// 获取主键
        /// </summary>
        /// <param name="stocksInfo"></param>
        /// <param name="textWhere"></param>
        /// <returns></returns>
        public long GetStocksId(StocksInfo stocksInfo, string textWhere)
        {
            long id = dal.GetStocksId(textWhere);
            if (id == 0) id = dal.InitStockInfo(stocksInfo);
            return id;
        }
        /// <summary>
        /// 获取主键
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public long GetStocksId(Guid fid)
        {
            return dal.GetStocksId(fid);
        }

        #region 库存更新
        /// <summary>
        /// 增加可用库存
        /// </summary>
        /// <param name="stocksInfo"></param>
        /// <param name="tranDetailsId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public string StocksRaiseSql(StocksInfo stocksInfo, long tranDetailsId, string loginUser)
        {
            return "update [LES].[TT_WMM_STOCKS] set " +
                "[STOCKS] = isnull([STOCKS],0) + " + stocksInfo.Stocks.GetValueOrDefault() + "," +
                "[AVAILBLE_STOCKS] = isnull([AVAILBLE_STOCKS],0) + " + stocksInfo.StocksNum.GetValueOrDefault() + "," +
                "[FRAGMENT_NUM] = isnull([FRAGMENT_NUM],0) + " + stocksInfo.FragmentNum.GetValueOrDefault() + "," +
                "[STOCKS_NUM] = isnull([STOCKS_NUM],0) + " + stocksInfo.StocksNum.GetValueOrDefault() + "," +
                "[MODIFY_USER] = N'" + loginUser + "'," +
                "[MODIFY_DATE] = GETDATE() where " +
                "[ID] = " + stocksInfo.Id + ";" +
                GetTranDetailsStatusUpdateSql(tranDetailsId, (int)WmmTranStateConstants.Done, loginUser);
        }
        /// <summary>
        /// 减少可用库存
        /// </summary>
        /// <param name="stocksInfo"></param>
        /// <param name="tranDetailsId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public string StocksReduceSql(StocksInfo stocksInfo, long tranDetailsId, string loginUser)
        {
            return "update [LES].[TT_WMM_STOCKS] set " +
                "[STOCKS] = isnull([STOCKS],0) - " + stocksInfo.Stocks.GetValueOrDefault() + "," +
                "[AVAILBLE_STOCKS] = isnull([AVAILBLE_STOCKS],0) - " + stocksInfo.StocksNum.GetValueOrDefault() + "," +
                "[FRAGMENT_NUM] = isnull([FRAGMENT_NUM],0) - " + stocksInfo.FragmentNum.GetValueOrDefault() + "," +
                "[STOCKS_NUM] = isnull([STOCKS_NUM],0) - " + stocksInfo.StocksNum.GetValueOrDefault() + "," +
                "[MODIFY_USER] = N'" + loginUser + "'," +
                "[MODIFY_DATE] = GETDATE() where " +
                "[ID] = " + stocksInfo.Id + ";" +
                GetTranDetailsStatusUpdateSql(tranDetailsId, (int)WmmTranStateConstants.Done, loginUser);
        }
        /// <summary>
        /// 增加冻结库存
        /// </summary>
        /// <param name="stocksInfo"></param>
        /// <param name="tranDetailsId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public string FrozenRaiseSql(StocksInfo stocksInfo, long tranDetailsId, string loginUser)
        {
            return "update [LES].[TT_WMM_STOCKS] set " +
                "[STOCKS] = isnull([STOCKS],0) + " + stocksInfo.Stocks.GetValueOrDefault() + "," +
                "[FROZEN_STOCKS] = isnull([FROZEN_STOCKS],0) + " + stocksInfo.StocksNum.GetValueOrDefault() + "," +
                "[FRAGMENT_NUM] = isnull([FRAGMENT_NUM],0) + " + stocksInfo.FragmentNum.GetValueOrDefault() + "," +
                "[STOCKS_NUM] = isnull([STOCKS_NUM],0) + " + stocksInfo.StocksNum.GetValueOrDefault() + "," +
                "[MODIFY_USER] = N'" + loginUser + "'," +
                "[MODIFY_DATE] = GETDATE() where " +
                "[ID] = " + stocksInfo.Id + ";" +
                GetTranDetailsStatusUpdateSql(tranDetailsId, (int)WmmTranStateConstants.Done, loginUser);
        }
        /// <summary>
        /// 减少冻结库存
        /// </summary>
        /// <param name="stocksInfo"></param>
        /// <param name="tranDetailsId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public string FrozenReduceSql(StocksInfo stocksInfo, long tranDetailsId, string loginUser)
        {
            return "update [LES].[TT_WMM_STOCKS] set " +
                "[STOCKS] = isnull([STOCKS],0) - " + stocksInfo.Stocks.GetValueOrDefault() + "," +
                "[FROZEN_STOCKS] = isnull([FROZEN_STOCKS],0) - " + stocksInfo.StocksNum.GetValueOrDefault() + "," +
                "[FRAGMENT_NUM] = isnull([FRAGMENT_NUM],0) - " + stocksInfo.FragmentNum.GetValueOrDefault() + "," +
                "[STOCKS_NUM] = isnull([STOCKS_NUM],0) - " + stocksInfo.StocksNum.GetValueOrDefault() + "," +
                "[MODIFY_USER] = N'" + loginUser + "'," +
                "[MODIFY_DATE] = GETDATE() where " +
                "[ID] = " + stocksInfo.Id + ";" +
                GetTranDetailsStatusUpdateSql(tranDetailsId, (int)WmmTranStateConstants.Done, loginUser);
        }
        #endregion

        /// <summary>
        /// 获取库存交易数据状态更新语句
        /// </summary>
        /// <param name="tranDetailsId"></param>
        /// <param name="tranState"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        private string GetTranDetailsStatusUpdateSql(long tranDetailsId, int tranState, string loginUser)
        {
            return "update [LES].[TT_WMM_TRAN_DETAILS] set " +
                "[TRAN_STATE] = " + (int)WmmTranStateConstants.Done + "," +
                "[MODIFY_USER] = N'" + loginUser + "'," +
                "[MODIFY_DATE] = GETDATE() where " +
                "[ID] = " + tranDetailsId + ";";
        }

        /// <summary>
        /// 入库冻结
        /// </summary>
        /// <param name="stocksInfo"></param>
        /// <param name="tranDetailsId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool StocksFrozen(StocksInfo stocksInfo, long tranDetailsId, string loginUser)
        {
            return dal.StocksFrozen(stocksInfo, tranDetailsId, loginUser);
        }

        /// <summary>
        /// 出库解冻
        /// </summary>
        /// <param name="stocksInfo"></param>
        /// <param name="tranDetailsId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool StocksThaw(StocksInfo stocksInfo, long tranDetailsId, string loginUser)
        {
            return dal.StocksThaw(stocksInfo, tranDetailsId, loginUser);
        }
        public string StocksThawSql(StocksInfo stocksInfo, long tranDetailsId, string loginUser)
        {
            return "update [LES].[TT_WMM_STOCKS] "
                + "set [PACKAGE] = [PACKAGE] - " + stocksInfo.Package.GetValueOrDefault() + ""
                + ",[FROZEN_STOCKS] = [FROZEN_STOCKS] - " + stocksInfo.FrozenStocks.GetValueOrDefault() + ""
                + ",[FRAGMENT_NUM] = [FRAGMENT_NUM] - " + stocksInfo.FragmentNum.GetValueOrDefault() + " "
                + ",[STOCKS_NUM] = [STOCKS_NUM] - " + stocksInfo.StocksNum.GetValueOrDefault() + " "
                //+ ",[SALE_PART_PRICE] = [SALE_PART_PRICE] -" + stocksInfo.SalePartPrice.GetValueOrDefault() + ""
               + ",[MODIFY_USER] = '" + loginUser + "'"
               + ",[MODIFY_DATE] = GETDATE() "
               + "where [ID] =" + stocksInfo.Id + ";"
               + "update [LES].[TT_WMM_TRAN_DETAILS] set " +
               "[TRAN_STATE] = " + (int)WmmTranStateConstants.Done + ""
               + ",[MODIFY_USER] = '" + loginUser + "'"
               + ",[MODIFY_DATE] = GETDATE() "
               + "where [ID] = " + tranDetailsId + ";";
        }
        /// <summary>
        /// 原库冻结
        /// </summary>
        /// <param name="stocksInfo"></param>
        /// <param name="tranDetailsId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool StocksStatusFrozen(StocksInfo stocksInfo, long tranDetailsId, string loginUser)
        {
            return dal.StocksStatusFrozen(stocksInfo, tranDetailsId, loginUser);
        }
        /// <summary>
        /// 状态冻结
        /// 原地点的可用变成冻结
        /// </summary>
        /// <param name="stocksInfo"></param>
        /// <param name="tranDetailsId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public string StocksStateFreezingSql(StocksInfo stocksInfo, long tranDetailsId, string loginUser)
        {
            return "update [LES].[TT_WMM_STOCKS] set " +
                "[AVAILBLE_STOCKS] = isnull([AVAILBLE_STOCKS],0) - " + stocksInfo.StocksNum.GetValueOrDefault() + "," +
                "[FROZEN_STOCKS] = isnull([FROZEN_STOCKS],0) + " + stocksInfo.StocksNum.GetValueOrDefault() + "," +
                "[FRAGMENT_NUM] = isnull([FRAGMENT_NUM],0) - " + stocksInfo.FragmentNum.GetValueOrDefault() + "," +
                "[MODIFY_USER] = N'" + loginUser + "'," +
                "[MODIFY_DATE] = GETDATE() where " +
                "[ID] =" + stocksInfo.Id + ";" +
                GetTranDetailsStatusUpdateSql(tranDetailsId, (int)WmmTranStateConstants.Done, loginUser);
        }
        /// <summary>
        /// 原库解冻
        /// </summary>
        /// <param name="stocksInfo"></param>
        /// <param name="tranDetailsId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool StocksStatusThaw(StocksInfo stocksInfo, long tranDetailsId, string loginUser)
        {
            return dal.StocksStatusThaw(stocksInfo, tranDetailsId, loginUser);
        }
        public string StocksStatusThawSql(StocksInfo stocksInfo, long tranDetailsId, string loginUser)
        {
            return "update [LES].[TT_WMM_STOCKS] "
                + ",[AVAILBLE_STOCKS] = [AVAILBLE_STOCKS] + " + stocksInfo.AvailbleStocks.GetValueOrDefault() + ""
                + ",[FROZEN_STOCKS] = [FROZEN_STOCKS] - " + stocksInfo.FrozenStocks.GetValueOrDefault() + ""
                + ",[MODIFY_USER] = '" + loginUser + "'"
               + ",[MODIFY_DATE] = GETDATE() "
               + "where [ID] =" + stocksInfo.Id + ";"
               + "update [LES].[TT_WMM_TRAN_DETAILS] "
               + "set [TRAN_STATE] = " + (int)WmmTranStateConstants.Done + ""
               + ",[MODIFY_USER] = '" + loginUser + "'"
               + ",[MODIFY_DATE] = GETDATE() "
               + "where [ID] = " + tranDetailsId + ";";
        }

        #region 库存锁定
        /// <summary>
        /// 提供按生产订单对物料进行排查（根据配置条件），库存满足时锁定库存
        /// </summary>
        /// <param name="productOrderNo"></param>
        /// <param name="assemblyLine"></param>
        /// <param name="loginUser"></param>
        /// <param name="forcePassFlag"></param>
        /// <returns></returns>
        public bool CheckMaterialStock(string productOrderNo, string assemblyLine, string loginUser, bool forcePassFlag = true)
        {
            ///缺件标识，默认为不缺件
            bool lackFlag = false;
            ///执行语句
            StringBuilder stringBuilder = new StringBuilder();
            ///根据参数生产订单号获取获取生产订单
            PullOrdersInfo pullOrdersInfo = new PullOrdersDAL().GetInfoByOrderNo(productOrderNo);
            if (pullOrdersInfo == null)
                throw new Exception("MC:0x00000447");///生产订单数据错误

            ///根据参数生产订单号获取TT_BAS_PULL_ORDER_BOM中的物料清单，为了加快获取数据的效率请仅获取后续逻辑所需的字段内容           
            List<PullOrderBomInfo> pullOrderBomInfos = new PullOrderBomDAL().GetPartList(productOrderNo);
            if (pullOrderBomInfos.Count == 0)
                throw new Exception("MC:0x00000448");///生产订单物料清单数据错误

            ///分组，担心同一生产订单下的物料清单中物料号+供应商会有多条数据
            var pullBoms = pullOrderBomInfos.GroupBy(p => new { p.Zcomno, p.SupplierNum }).Select(p => new { p.Key.Zcomno, p.Key.SupplierNum, Zqty = p.Sum(x => x.Zqty.GetValueOrDefault()) }).ToList();
            ///根据参数生产线获取物料拉动信息中生产线对应的信息，并以是否进行物料库存排查标记进行数据过滤
            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandardInfos = new MaintainInhouseLogisticStandardDAL().GetCheckMaterialStockList(assemblyLine, pullBoms.Select(d => d.Zcomno).ToList());
            ///没有需要检查的物料时直接返回true
            if (maintainInhouseLogisticStandardInfos.Count == 0)
                return true;
            ///以物料拉动信息与物料清单匹配其物料号、供应商，并以匹配得的数据集合对其物料拉动信息中的来源存储区的该供应商物料进行可用库存检查
            var query = (from p in pullBoms
                         join m in maintainInhouseLogisticStandardInfos
                         on new { PartNo = p.Zcomno, p.SupplierNum } equals new { m.PartNo, m.SupplierNum }
                         select new { PartNo = p.Zcomno, p.SupplierNum, p.Zqty, m.SZoneNo, m.SWmNo }).ToList();
            ///未设置需要检查的物料时，返回true
            if (query.Count == 0) return true;
            ///供应商信息
            List<SupplierInfo> supplierInfos = new SupplierDAL().GetList("[SUPPLIER_NUM] in ('" + string.Join("','", query.Select(d => d.SupplierNum).ToArray()) + "')", string.Empty);
            ///获取库存数据
            ///TODO:减少获取的字段以提高效率
            List<StocksInfo> stocksInfos = dal.GetList("" +
                "[PART_NO] in ('" + string.Join("','", query.Select(d => d.PartNo).ToArray()) + "') and " +
                "[WM_NO] in ('" + string.Join("','", query.Select(d => d.SWmNo).ToArray()) + "') and " +
                "[SUPPLIER_NUM] in ('" + string.Join("','", query.Select(d => d.SupplierNum).ToArray()) + "') and " +
                "[ZONE_NO] in ('" + string.Join("','", query.Select(d => d.SZoneNo).ToArray()) + "')", string.Empty);

            ///物料仓储信息 TM_BAS_PARTS_STOCK
            List<PartsStockInfo> partsStockInfos = new PartsStockDAL().GetList("" +
                "[PART_NO] in ('" + string.Join("','", stocksInfos.Select(d => d.PartNo).ToArray()) + "') and " +
                "[SUPPLIER_NUM] in ('" + string.Join("','", stocksInfos.Select(d => d.SupplierNum).ToArray()) + "') and " +
                "[ZONE_NO] in ('" + string.Join("','", stocksInfos.Select(d => d.ZoneNo).ToArray()) + "') and " +
                "[WM_NO] in ('" + string.Join("','", stocksInfos.Select(d => d.WmNo).ToArray()) + "')", string.Empty);

            ///回复前需要将可用库存以物料清单的消耗数量进行扣减，冻结库存进行累加
            List<StocksInfo> lockStocksInfos = new List<StocksInfo>();
            ///缺件生产订单
            PorderLackMaterialInfo porderLackMaterialInfo = new PorderLackMaterialDAL().GetInfo(productOrderNo);
            if (porderLackMaterialInfo == null)
            {
                porderLackMaterialInfo = new PorderLackMaterialInfo();
                porderLackMaterialInfo.CreateUser = loginUser;
                porderLackMaterialInfo.Fid = Guid.NewGuid();
                porderLackMaterialInfo.ValidFlag = true;
                porderLackMaterialInfo.CreateDate = DateTime.Now;
                porderLackMaterialInfo.ProductionOrderFid = pullOrdersInfo.Fid;
                porderLackMaterialInfo.ProductionOrderNo = pullOrdersInfo.OrderNo;
                porderLackMaterialInfo.Plant = pullOrdersInfo.Werk;
                porderLackMaterialInfo.AssemblyLine = pullOrdersInfo.AssemblyLine;
                porderLackMaterialInfo.LackFlag = false;
                porderLackMaterialInfo.OrderDate = pullOrdersInfo.OrderDate;
                porderLackMaterialInfo.Status = null;///TODO:状态字段的作用?
                porderLackMaterialInfo.Id = new PorderLackMaterialDAL().Add(porderLackMaterialInfo);
                if (porderLackMaterialInfo.Id == 0)
                    throw new Exception("MC:0x00000450");///缺件生产订单生成失败
            }
            ///
            foreach (var q in query)
            {
                ///对应的库存记录
                PartsStockInfo partsStockInfo = partsStockInfos.FirstOrDefault(d => d.PartNo == q.PartNo && d.SupplierNum == q.SupplierNum && d.WmNo == q.SWmNo && d.ZoneNo == q.SZoneNo);
                if (partsStockInfo == null)
                    throw new Exception("MC:0x00000451");///物料仓储信息错误
                List<StocksInfo> stocks = stocksInfos.Where(d => d.PartNo == q.PartNo && d.SupplierNum == q.SupplierNum && d.WmNo == q.SWmNo && d.ZoneNo == q.SZoneNo).ToList();
                ///若该存储区的库存发现多条可用数量大于零的数据则优先操作已结算的库存数据
                ///此逻辑需要开关设置在物料仓储信息上<是否优先使用已结算物料>
                decimal partRequireQty = q.Zqty;
                //if (partsStockInfo.PriorityUseFlag.GetValueOrDefault())
                //    stocks = stocks.OrderByDescending(d => d.SettledFlag.GetValueOrDefault()).ToList();
                //else
                //    stocks = stocks.OrderBy(d => d.SettledFlag.GetValueOrDefault()).ToList();
                ///最后一个扣减库存
                StocksInfo lastStock = new StocksInfo();
                while (partRequireQty > 0)
                {
                    StocksInfo stock = stocks.FirstOrDefault();
                    ///第一次查询 就没有库存
                    if (stock == null)
                    {
                        if (lastStock == null)
                        {
                            lastStock = CreateStocksInfo(loginUser);
                            ///供应商
                            lastStock.SupplierNum = q.SupplierNum;
                            ///零件号
                            lastStock.PartNo = q.PartNo;
                            ///存贮区编码
                            lastStock.ZoneNo = q.SZoneNo;
                            ///仓库编码
                            lastStock.WmNo = q.SWmNo;
                            ///id
                            lastStock.Id = GetStocksId(lastStock, string.Empty);
                        }
                        ///MatchedQty
                        lastStock.MatchedQty = partRequireQty;
                        lockStocksInfos.Add(lastStock);
                        break;
                    }
                    ///满足
                    if (partRequireQty <= stock.AvailbleStocks.GetValueOrDefault())
                    {
                        stock.MatchedQty = partRequireQty;
                        lockStocksInfos.Add(stock);
                        stocks.Remove(stock);
                        partRequireQty = 0;
                        break;
                    }
                    ///不满足
                    if (partRequireQty > stock.AvailbleStocks.GetValueOrDefault())
                    {
                        stock.MatchedQty = stock.AvailbleStocks.GetValueOrDefault();
                        lockStocksInfos.Add(stock);
                        stocks.Remove(stock);
                        partRequireQty -= stock.AvailbleStocks.GetValueOrDefault();
                        lastStock = stock;
                    }
                }
                ///不满足的数量
                if (partRequireQty > 0)
                {
                    PorderLackMaterialDetailInfo porderLackMaterialDetailInfo = new PorderLackMaterialDetailInfo();
                    porderLackMaterialDetailInfo.CreateUser = loginUser;
                    porderLackMaterialDetailInfo.LackPorderFid = porderLackMaterialInfo.Fid;
                    porderLackMaterialDetailInfo.ProductionOrderFid = pullOrdersInfo.Fid;
                    porderLackMaterialDetailInfo.PartNo = q.PartNo;
                    porderLackMaterialDetailInfo.SupplierNum = q.SupplierNum;
                    porderLackMaterialDetailInfo.Plant = pullOrdersInfo.Werk;
                    porderLackMaterialDetailInfo.RequireQty = q.Zqty;
                    porderLackMaterialDetailInfo.LackQty = partRequireQty;
                    porderLackMaterialDetailInfo.PartPurchaser = string.Empty;
                    porderLackMaterialDetailInfo.ProductionOrderNo = pullOrdersInfo.OrderNo;
                    porderLackMaterialDetailInfo.AssemblyLine = pullOrdersInfo.AssemblyLine;
                    porderLackMaterialDetailInfo.OrderDate = pullOrdersInfo.OrderDate;
                    stringBuilder.AppendLine(PorderLackMaterialDetailDAL.GetInsertSql(porderLackMaterialDetailInfo));
                    ///标记缺件
                    lackFlag = true;
                }
            }
            ///缺件
            if (lackFlag)
                stringBuilder.AppendLine("update [LES].[TT_ATP_PORDER_LACK_MATERIAL] set " +
                    "[LACK_FLAG] = 1," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() where " +
                    "[ID] = " + porderLackMaterialInfo.Id + ";");

            ///缺件且非强过
            if (lackFlag && !forcePassFlag)
            {
                ///库存检查全部物料满足时函数回复true
                using (var trans = new TransactionScope())
                {
                    if (stringBuilder.Length > 0)
                        CommonDAL.ExecuteNonQueryBySql(stringBuilder.ToString());
                    trans.Complete();
                }
                return false;
            }
            ///以上数据处理函数因需要外部系统接口同步调用，单次计算耗时控制在2秒内
            ///以上库存冻结若针对的仓库存储区为VMI类型，则需要同时生成VMI的库存锁定中间表数据
            ///库存锁定、交易记录

            ///强过or不缺件
            if (forcePassFlag || !lackFlag)
            {
                ///实现此逻辑需要在系统配置中增加开关STOCK_CHECK_LOCK_MATERIAL_SYNC_UPDATE_PART_STOCK_FLAG,库存排查锁定物料是否同步更新库存, 默认为true
                string stockCheckLockFlag = new ConfigDAL().GetValueByCode("STOCK_CHECK_LOCK_MATERIAL_SYNC_UPDATE_PART_STOCK_FLAG");
                ///仓库
                List<WarehouseInfo> warehouseInfos = new WarehouseDAL().GetListForInterfaceDataSync(stocksInfos.Select(d => d.WmNo).ToList());
                List<WmsVmiPartStockLockInfo> wmsVmiPartStockLockInfos = new List<WmsVmiPartStockLockInfo>();
                foreach (StocksInfo lockStocksInfo in lockStocksInfos)
                {
                    SupplierInfo supplierInfo = supplierInfos.Where(d => d.SupplierNum == lockStocksInfo.SupplierNum).FirstOrDefault();
                    WarehouseInfo warehouseInfo = warehouseInfos.Where(d => d.Warehouse == lockStocksInfo.WmNo).FirstOrDefault();
                    int tranState = (int)WmmTranStateConstants.Created;




                    ///库存锁定
                    if (!string.IsNullOrEmpty(stockCheckLockFlag) && stockCheckLockFlag.ToLower() == "true")
                    {
                        tranState = (int)WmmTranStateConstants.Done;
                        stringBuilder.AppendFormat("update [LES].[TT_WMM_STOCKS] set " +
                            "[AVAILBLE_STOCKS] = isnull([AVAILBLE_STOCKS],0) - " + lockStocksInfo.MatchedQty.GetValueOrDefault() + "," +
                            "[FROZEN_STOCKS] = isnull([FROZEN_STOCKS],0) + " + lockStocksInfo.MatchedQty.GetValueOrDefault() + "," +
                            "[PRIOR_USE_FLAG] = " + (lockStocksInfo.AvailbleStocks.GetValueOrDefault() == lockStocksInfo.MatchedQty.GetValueOrDefault() ? "0" : "[PRIOR_USE_FLAG]") + "," +
                            "[MODIFY_USER] = N'" + loginUser + "'," +
                            "[MODIFY_DATE] = GETDATE() where " +
                            "[ID] =" + lockStocksInfo.Id + ";");
                    }

                    ///是否启用WMS系统标记
                    string enable_vmi_flag = new ConfigDAL().GetValueByCode("ENABLE_VMI_FLAG");

                    ///库存锁定中间表
                    if (!warehouseInfo.VmiEnable.GetValueOrDefault() && !string.IsNullOrEmpty(enable_vmi_flag) && enable_vmi_flag.ToLower() == "true")
                    {

                        WmsVmiPartStockLockInfo wmsVmiPartStockLockInfo = new WmsVmiPartStockLockInfo();
                        wmsVmiPartStockLockInfo.Werks = lockStocksInfo.Plant;
                        wmsVmiPartStockLockInfo.Partno = lockStocksInfo.PartNo;
                        wmsVmiPartStockLockInfo.Suppliercode = lockStocksInfo.SupplierNum;
                        wmsVmiPartStockLockInfo.Vmiwarehousecode = lockStocksInfo.ZoneNo;
                        wmsVmiPartStockLockInfo.Partqty = lockStocksInfo.MatchedQty;
                        wmsVmiPartStockLockInfo.Orilockstatus = "FXZ";
                        wmsVmiPartStockLockInfo.Targetlockstatus = "XZ";
                        wmsVmiPartStockLockInfo.Invstatus = "SC";
                        wmsVmiPartStockLockInfo.ProcessFlag = (int)ProcessFlagConstants.Untreated;
                        wmsVmiPartStockLockInfo.CreateUser = loginUser;
                        wmsVmiPartStockLockInfos.Add(wmsVmiPartStockLockInfo);
                    }
                    ///构建交易
                    TranDetailsInfo tranDetailsInfo = TranDetailsBLL.CreateTranDetailsInfo((int)WmmTranTypeConstants.StateFreezing, tranState, loginUser);
                    ///以库存数据填充交易记录
                    TranDetailsBLL.GetTranDetailsInfo2(lockStocksInfo, ref tranDetailsInfo);
                    ///以供应商信息填充交易记录
                    TranDetailsBLL.GetTranDetailsInfo(supplierInfo, ref tranDetailsInfo);
                    ///交易时间
                    tranDetailsInfo.TranDate = DateTime.Now;
                    ///交易编号
                    tranDetailsInfo.TranNo = productOrderNo;
                    ///包装数量
                    tranDetailsInfo.ActualPackageQty = Convert.ToInt32(Math.Ceiling(lockStocksInfo.MatchedQty.GetValueOrDefault() / lockStocksInfo.Package.GetValueOrDefault()));
                    ///物料数量
                    tranDetailsInfo.ActualQty = lockStocksInfo.MatchedQty;
                    ///
                    stringBuilder.AppendLine(TranDetailsDAL.GetInsertSql(tranDetailsInfo));
                }
                if (wmsVmiPartStockLockInfos.Count > 0)
                {
                    Guid logFid = Guid.NewGuid();
                    string targetSystem = "VMI";
                    string methodCode = "LES-WMS-010";
                    string keyValue = productOrderNo;
                    stringBuilder.AppendLine(CommonBLL.GetCreateOutboundLogSql(targetSystem, logFid, methodCode, keyValue, loginUser));
                    foreach (var wmsVmiPartStockLockInfo in wmsVmiPartStockLockInfos)
                    {
                        wmsVmiPartStockLockInfo.LogFid = logFid;
                        stringBuilder.AppendLine(WmsVmiPartStockLockDAL.GetInsertSql(wmsVmiPartStockLockInfo));
                    }
                }
            }
            ///库存检查全部物料满足时函数回复true
            using (var trans = new TransactionScope())
            {
                if (stringBuilder.Length > 0)
                    CommonDAL.ExecuteNonQueryBySql(stringBuilder.ToString());
                trans.Complete();
            }
            return true;
        }

        #endregion

        /// <summary>
        /// 库存数据校验
        /// </summary>
        public List<StocksInfo> GetValidStocksInfo(List<StocksInfo> stocksInfos, List<BarcodeInfo> barcodeInfos,
            string partNo, string wmNo, string zoneNo, string dloc, decimal partQty, int wmmTranType,
            string supplierNum, string plant, string packageModel, string partCls, string originPlace, string costCenter, bool? settledFlag)
        {
            #region 获取库存维度
            bool supplierFlag = false;
            bool plantFlag = false;
            bool packageModelFlag = false;
            bool partClsFlag = false;
            bool batchFlag = false;
            bool barcodeFlag = false;
            bool originPlaceFlag = false;
            bool costCenterFlag = false;
            bool settlementFlag = false;
            GetStockDimensionFlag(
                ref supplierFlag,
                ref plantFlag,
                ref packageModelFlag,
                ref partClsFlag,
                ref batchFlag,
                ref barcodeFlag,
                ref originPlaceFlag,
                ref costCenterFlag,
                ref settlementFlag);
            #endregion
            ///按物料、仓库、存储区、库位进行过滤
            List<StocksInfo> stocks = new List<StocksInfo>();
            ///如没有指定库位则不需要过滤库位，在此函数返回时的库存数据所对应的库位作为本次出库的库位
            if (string.IsNullOrEmpty(dloc))
                stocks = stocksInfos.Where(d => d.PartNo == partNo && d.WmNo == wmNo && d.ZoneNo == zoneNo).ToList();
            else
                stocks = stocksInfos.Where(d => d.PartNo == partNo && d.WmNo == wmNo && d.ZoneNo == zoneNo && d.Dloc == dloc).ToList();
            ///供应商
            if (supplierFlag && !string.IsNullOrEmpty(supplierNum)) stocks = stocks.Where(d => d.SupplierNum == supplierNum).ToList();
            ///工厂
            if (plantFlag && !string.IsNullOrEmpty(plant)) stocks = stocks.Where(d => d.Plant == plant).ToList();
            ///包装型号，TODO:翻包时?
            if (packageModelFlag && !string.IsNullOrEmpty(packageModel)) stocks = stocks.Where(d => d.PackageModel == packageModel).ToList();
            ///物料分类
            if (partClsFlag && !string.IsNullOrEmpty(partCls)) stocks = stocks.Where(d => d.PartCls == partCls).ToList();
            ///批次,TODO:需要由系统进行推荐
            if (batchFlag) stocks = stocks.Where(d => d.BatchNo == string.Empty).ToList();
            ///条码
            if (barcodeFlag) stocks = stocks.Where(d => barcodeInfos.Select(b => b.BarcodeData).Contains(d.BarcodeData)).ToList();
            ///产地
            //if (originPlaceFlag && !string.IsNullOrEmpty(originPlace)) stocks = stocks.Where(d => d.OriginPlace == originPlace).ToList();
            ///成本中心
            //if (costCenterFlag && !string.IsNullOrEmpty(costCenter)) stocks = stocks.Where(d => d.CostCenter == costCenter).ToList();
            ///结算标记
            ///settledFlag为null时标识本次操作不关注是否结算
            if (settlementFlag && settledFlag != null) stocks = stocks.Where(d => d.SettledFlag == settledFlag).ToList();
            ///
            if (stocks.Count == 0) return null;
            ///返回多条有效的库存数据
            List<StocksInfo> list = new List<StocksInfo>();
            ///TODO:依据规则将stocks进行排序
            stocks = stocks.OrderByDescending(d => d.SettledFlag.GetValueOrDefault()).ToList();
            ///
            while (partQty > 0)
            {
                ///如果有多条结果记录再次确认逻辑排序第一条的记录是否满足
                StocksInfo info = stocks.FirstOrDefault();
                if (info == null) break;
                ///TODO:单条数量不满足，合计数量满足时，如果返回数据？
                ///出库、物料冻结、状态冻结
                StocksInfo sInfo = info.Clone();
                switch (wmmTranType)
                {
                    case (int)WmmTranTypeConstants.Outbound:
                    case (int)WmmTranTypeConstants.Movement:
                    case (int)WmmTranTypeConstants.MaterialFreezing:
                    case (int)WmmTranTypeConstants.StateFreezing:
                        if (info.AvailbleStocks.GetValueOrDefault() >= partQty)
                        {
                            sInfo.MatchedQty = partQty;
                            list.Add(sInfo);
                            partQty = 0;
                            break;
                        }
                        sInfo.MatchedQty = info.AvailbleStocks.GetValueOrDefault();
                        list.Add(sInfo);
                        partQty -= info.AvailbleStocks.GetValueOrDefault();
                        break;
                    case (int)WmmTranTypeConstants.MaterialThawing:
                    case (int)WmmTranTypeConstants.FrozenMovement:
                    case (int)WmmTranTypeConstants.StateThawing:
                    case (int)WmmTranTypeConstants.FrozenOutbound:
                        if (info.FrozenStocks.GetValueOrDefault() >= partQty)
                        {
                            sInfo.MatchedQty = partQty;
                            list.Add(sInfo);
                            partQty = 0;
                            break;
                        }
                        sInfo.MatchedQty = info.FrozenStocks.GetValueOrDefault();
                        list.Add(sInfo);
                        partQty -= info.FrozenStocks.GetValueOrDefault();
                        break;
                }
                stocks.Remove(info);
            }
            ///物料需求无法满足
            if (partQty > 0) return new List<StocksInfo>();
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stocksInfos"></param>
        /// <param name="outputDetailInfo"></param>
        /// <param name="batchNo"></param>
        /// <param name="settledFlag"></param>
        /// <param name="barcodeInfos"></param>
        /// <returns></returns>
        public List<StocksInfo> GetValidStocksInfo(OutputDetailInfo outputDetailInfo, int wmmTranType,
            List<StocksInfo> stocksInfos, List<OutputDetailInfo> outputDetailInfos, List<BarcodeInfo> barcodeInfos,
            bool? settledFlag)
        {
            #region 获取库存维度
            bool supplierFlag = false;
            bool plantFlag = false;///
            bool packageModelFlag = false;
            bool partClsFlag = false;///
            bool batchFlag = false;
            bool barcodeFlag = false;
            bool originPlaceFlag = false;
            bool costCenterFlag = false;
            bool settlementFlag = false;
            GetStockDimensionFlag(
                ref supplierFlag,
                ref plantFlag,
                ref packageModelFlag,
                ref partClsFlag,
                ref batchFlag,
                ref barcodeFlag,
                ref originPlaceFlag,
                ref costCenterFlag,
                ref settlementFlag);
            #endregion

            List<OutputDetailInfo> outputDetails = outputDetailInfos;
            ///供应商
            if (supplierFlag) outputDetails = outputDetails.Where(d => d.SupplierNum == outputDetailInfo.SupplierNum).ToList();
            ///工厂
            if (plantFlag) outputDetails = outputDetails.Where(d => d.Plant == outputDetailInfo.Plant).ToList();
            ///包装型号，TODO:翻包时?
            if (packageModelFlag) outputDetails = outputDetails.Where(d => d.PackageModel == outputDetailInfo.PackageModel).ToList();
            ///物料分类
            if (partClsFlag) outputDetails = outputDetails.Where(d => d.PartCls == outputDetailInfo.PartCls).ToList();
            ///产地
            if (originPlaceFlag) outputDetails = outputDetails.Where(d => d.OriginPlace == outputDetailInfo.OriginPlace).ToList();
            ///成本中心
            if (costCenterFlag) outputDetails = outputDetails.Where(d => d.CostCenter == outputDetailInfo.CostCenter).ToList();
            ///合计需求数量
            decimal partQty = outputDetails.Sum(d => d.RequiredQty.GetValueOrDefault());

            return GetValidStocksInfo(
                stocksInfos,
                barcodeInfos,
                outputDetailInfo.PartNo,
                outputDetailInfo.WmNo,
                outputDetailInfo.ZoneNo,
                outputDetailInfo.Dloc,
                partQty,
                wmmTranType,
                outputDetailInfo.SupplierNum,
                outputDetailInfo.Plant,
                outputDetailInfo.PackageModel,
                outputDetailInfo.PartCls,
                outputDetailInfo.OriginPlace,
                outputDetailInfo.CostCenter,
                settledFlag);
        }

        /// <summary>
        /// 初始化库存维度标记
        /// </summary>
        /// <param name="supplierFlag">库存供应商维度标记</param>
        /// <param name="plantFlag">库存工厂维度标记</param>
        /// <param name="packageModelFlag">库存包装型号维度标记</param>
        /// <param name="partClsFlag">库存物料类别维度标记</param>
        /// <param name="batchFlag">库存批次维度标记</param>
        /// <param name="barcodeFlag">库存标签维度标记</param>
        /// <param name="originPlaceFlag">库存产地维度标记</param>
        /// <param name="costCenterFlag">库存成本中心维度标记</param>
        /// <param name="settlementFlag">库存结算维度标记</param>
        public void GetStockDimensionFlag(ref bool supplierFlag
            , ref bool plantFlag
            , ref bool packageModelFlag
            , ref bool partClsFlag
            , ref bool batchFlag
            , ref bool barcodeFlag
            , ref bool originPlaceFlag
            , ref bool costCenterFlag
            , ref bool settlementFlag)
        {
            Dictionary<string, string> keyValues = new ConfigDAL().GetValuesByCodes(new string[] {
                "SUPPLIER_STOCKS_DIMENSION",
                "PLANT_STOCKS_DIMENSION",
                "PACKAGE_MODEL_STOCKS_DIMENSION",
                "PART_CLS_STOCKS_DIMENSION",
                "BATCH_STOCKS_DIMENSION",
                "BARCODE_STOCKS_DIMENSION",
                "ORIGIN_PLACE_STOCKS_DIMENSION",
                "COST_CENTER_STOCKS_DIMENSION",
                "SETTLEMENT_STOCKS_DIMENSION" });
            ///库存供应商维度标记
            keyValues.TryGetValue("SUPPLIER_STOCKS_DIMENSION", out string supplierStocksDimension);
            bool.TryParse(supplierStocksDimension, out supplierFlag);
            ///库存工厂维度标记
            keyValues.TryGetValue("PLANT_STOCKS_DIMENSION", out string plantStocksDimension);
            bool.TryParse(plantStocksDimension, out plantFlag);
            ///库存包装型号维度标记
            keyValues.TryGetValue("PACKAGE_MODEL_STOCKS_DIMENSION", out string packageModelStocksDimension);
            bool.TryParse(packageModelStocksDimension, out packageModelFlag);
            ///库存物料类别维度标记
            keyValues.TryGetValue("PART_CLS_STOCKS_DIMENSION", out string partClsStocksDimension);
            bool.TryParse(partClsStocksDimension, out partClsFlag);
            ///库存批次维度标记
            keyValues.TryGetValue("BATCH_STOCKS_DIMENSION", out string batchStocksDimension);
            bool.TryParse(batchStocksDimension, out batchFlag);
            ///库存标签维度标记
            keyValues.TryGetValue("BARCODE_STOCKS_DIMENSION", out string barcodeStocksDimension);
            bool.TryParse(barcodeStocksDimension, out barcodeFlag);
            ///库存产地维度标记
            keyValues.TryGetValue("ORIGIN_PLACE_STOCKS_DIMENSION", out string originPlaceStocksDimension);
            bool.TryParse(originPlaceStocksDimension, out originPlaceFlag);
            ///库存成本中心维度标记
            keyValues.TryGetValue("COST_CENTER_STOCKS_DIMENSION", out string costCenterStocksDimension);
            bool.TryParse(costCenterStocksDimension, out costCenterFlag);
            ///库存结算维度标记
            keyValues.TryGetValue("SETTLEMENT_STOCKS_DIMENSION", out string settlementStocksDimension);
            bool.TryParse(settlementStocksDimension, out settlementFlag);
        }
        /// <summary>
        /// 获取可用库存
        /// </summary>
        /// <param name="partNo"></param>
        /// <param name="wmNo"></param>
        /// <param name="zoneNo"></param>
        /// <param name="supplierNum"></param>
        /// <returns></returns>
        public decimal GetAvailbleQty(string partNo, string wmNo, string zoneNo, string supplierNum = "")
        {
            return dal.GetAvailbleQty(partNo, wmNo, zoneNo, supplierNum);
        }


        /// <summary>
        /// 优先使用
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool PrioruseInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<StocksInfo> stocksInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", string.Empty);
            if (stocksInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误
            StringBuilder @string = new StringBuilder();
            foreach (var stocksInfo in stocksInfos)
            {
                if (!stocksInfo.SettledFlag.GetValueOrDefault())
                    throw new Exception("MC:0x00000525");///非已结算物料不能标记为优先使用
                //if (stocksInfo.PriorUseFlag.GetValueOrDefault()) continue;
                int cnt = dal.GetCounts("" +
                    "[ID] <> " + stocksInfo.Id + " and " +
                    "[PRIOR_USE_FLAG] = 1 and " +
                    "[PART_NO] = N'" + stocksInfo.PartNo + "' and " +
                    "[SUPPLIER_NUM] = N'" + stocksInfo.SupplierNum + "'");
                if (cnt > 0)
                    throw new Exception("MC:0x00000526");///已结算物料只能在单一存储区内被标记为优先使用
                @string.AppendLine("update [LES].[TT_WMM_STOCKS] " +
                    "set [PRIOR_USE_FLAG] = 1," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() " +
                    "where [ID] = " + stocksInfo.Id + ";");
            }

            return true;
        }
    }
}

