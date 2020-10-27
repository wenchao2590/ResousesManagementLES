using DM.LES;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DAL.LES
{
    public partial class StocksDAL
    {
        /// <summary>
        /// 获取更新主键
        /// </summary>
        /// <param name="textWhere"></param>
        /// <returns></returns>
        public long GetStocksId(string textWhere)
        {
            string sql = "select [ID] from [LES].[TT_WMM_STOCKS] with(nolock) where [VALID_FLAG] = 1 " + textWhere;
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value) return 0;
            return Convert.ToInt64(result);
        }
        /// <summary>
        /// 获取更新主键
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public long GetStocksId(Guid fid)
        {
            string sql = "select [ID] from [LES].[TT_WMM_STOCKS] with(nolock) where [VALID_FLAG] = 1 and [FID] = @FID;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@FID", DbType.Guid, fid);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value) return 0;
            return Convert.ToInt64(result);
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
            string sql = "select sum(isnull([AVAILBLE_STOCKS],0)) from [LES].[TT_WMM_STOCKS] with(nolock) " +
                "where [VALID_FLAG] = 1 and [PART_NO] = @PART_NO and [WM_NO] = @WM_NO and [ZONE_NO] = @ZONE_NO";
            if (string.IsNullOrEmpty(supplierNum)) sql += ";";
            else sql += " and [SUPPLIER_NUM] = N'" + supplierNum + "';";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PART_NO", DbType.AnsiString, partNo);
            db.AddInParameter(cmd, "@WM_NO", DbType.AnsiString, wmNo);
            db.AddInParameter(cmd, "@ZONE_NO", DbType.AnsiString, zoneNo);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value) return 0;
            return Convert.ToDecimal(result);
        }
        /// <summary>
        /// InitStockInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InitStockInfo(StocksInfo info)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(TT_WMS_STOCKS_INSERT);
            db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Id);
            db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
            db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
            db.AddInParameter(dbCommand, "@PLANT_ZONE", DbType.String, info.PlantZone);
            db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
            db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
            db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
            db.AddInParameter(dbCommand, "@PART_CNAME", DbType.String, info.PartCname);
            db.AddInParameter(dbCommand, "@PART_ENAME", DbType.String, info.PartEname);
            db.AddInParameter(dbCommand, "@PART_NICKNAME", DbType.String, info.PartNickname);
            db.AddInParameter(dbCommand, "@PART_UNITS", DbType.String, info.PartUnits);
            db.AddInParameter(dbCommand, "@PACKAGE_MODEL", DbType.String, info.PackageModel);
            db.AddInParameter(dbCommand, "@PACKAGE", DbType.Decimal, info.Package);
            db.AddInParameter(dbCommand, "@LOGICAL_PK", DbType.String, info.LogicalPk);
            db.AddInParameter(dbCommand, "@ROUTE", DbType.String, info.Route);
            db.AddInParameter(dbCommand, "@ZONE_NO", DbType.String, info.ZoneNo);
            db.AddInParameter(dbCommand, "@WM_NO", DbType.String, info.WmNo);
            db.AddInParameter(dbCommand, "@OCCUPY_AREA", DbType.Decimal, info.OccupyArea);
            db.AddInParameter(dbCommand, "@DLOC", DbType.String, info.Dloc);
            db.AddInParameter(dbCommand, "@MAX", DbType.Decimal, info.Max);
            db.AddInParameter(dbCommand, "@MIN", DbType.Decimal, info.Min);
            db.AddInParameter(dbCommand, "@ROW_NUMBER", DbType.Int32, info.RowNumber);
            db.AddInParameter(dbCommand, "@LINE_NUMBER", DbType.Int32, info.LineNumber);
            db.AddInParameter(dbCommand, "@HIGH_NUMBER", DbType.Int32, info.HighNumber);
            db.AddInParameter(dbCommand, "@MATERIAL_GROUP", DbType.String, info.MaterialGroup);
            db.AddInParameter(dbCommand, "@KEEPER", DbType.String, info.Keeper);
            db.AddInParameter(dbCommand, "@TRANSER", DbType.String, info.Transer);
            db.AddInParameter(dbCommand, "@INFORMATIONER", DbType.String, info.Informationer);
            db.AddInParameter(dbCommand, "@ELOC", DbType.String, info.Eloc);
            db.AddInParameter(dbCommand, "@SAFE_STOCK", DbType.Decimal, info.SafeStock);
            db.AddInParameter(dbCommand, "@STOCKS", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "@FROZEN_STOCKS", DbType.Decimal, 0);
            db.AddInParameter(dbCommand, "@AVAILBLE_STOCKS", DbType.Decimal, 0);
            db.AddInParameter(dbCommand, "@IS_BATCH", DbType.Int32, info.IsBatch);
            db.AddInParameter(dbCommand, "@WMS_RULE", DbType.String, info.WmsRule);
            db.AddInParameter(dbCommand, "@COUNTER", DbType.Decimal, info.Counter);
            db.AddInParameter(dbCommand, "@FRAGMENT_NUM", DbType.Decimal, 0);
            db.AddInParameter(dbCommand, "@STOCKS_NUM", DbType.Decimal, 0);
            db.AddInParameter(dbCommand, "@PART_WEIGHT", DbType.Decimal, info.PartWeight);
            db.AddInParameter(dbCommand, "@PART_CLS", DbType.String, info.PartCls);
            db.AddInParameter(dbCommand, "@IS_REPACK", DbType.Int32, info.IsRepack);
            db.AddInParameter(dbCommand, "@REPACK_ROUTE", DbType.String, info.RepackRoute);
            db.AddInParameter(dbCommand, "@IS_TRIGGER_PULL", DbType.Int32, info.IsTriggerPull);
            db.AddInParameter(dbCommand, "@TRIGGER_WM_NO", DbType.String, info.TriggerWmNo);
            db.AddInParameter(dbCommand, "@TRIGGER_ZONE_NO", DbType.String, info.TriggerZoneNo);
            db.AddInParameter(dbCommand, "@TRIGGER_DLOC", DbType.String, info.TriggerDloc);
            db.AddInParameter(dbCommand, "@EMG_TIME", DbType.Int32, info.EmgTime);
            db.AddInParameter(dbCommand, "@SUPPER_ZONE_DLOC", DbType.String, info.SupperZoneDloc);
            db.AddInParameter(dbCommand, "@CHECK_TYPE", DbType.Int32, info.CheckType);
            db.AddInParameter(dbCommand, "@BUSINESS_PK", DbType.String, info.BusinessPk);
            db.AddInParameter(dbCommand, "@BATCH_NO", DbType.String, info.BatchNo);
            db.AddInParameter(dbCommand, "@BARCODE_DATA", DbType.String, info.BarcodeData);
            db.AddInParameter(dbCommand, "@BARCODE_TYPE", DbType.String, info.BarcodeType);
            //db.AddInParameter(dbCommand, "@ORIGIN_PLACE", DbType.String, info.OriginPlace);
            //db.AddInParameter(dbCommand, "@PURCHASE_PART_PRICE", DbType.Decimal, info.PurchasePartPrice);
            //db.AddInParameter(dbCommand, "@SALE_PART_PRICE", DbType.Decimal, info.SalePartPrice);
            //db.AddInParameter(dbCommand, "@COST_CENTER", DbType.String, info.CostCenter);
            db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
            //db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
            db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
            db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
            //db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
            //db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
            db.AddInParameter(dbCommand, "@PART_PRICE", DbType.Decimal, info.PartPrice);
            //db.AddInParameter(dbCommand, "@SETTLED_FLAG", DbType.Boolean, info.SettledFlag);
            //db.AddInParameter(dbCommand, "@PRIOR_USE_FLAG", DbType.Boolean, info.PriorUseFlag);
            return long.Parse("0" + db.ExecuteScalar(dbCommand));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stocksInfo"></param>
        /// <param name="tranDetailsId"></param>
        /// <returns></returns>
        public bool StocksUp(StocksInfo stocksInfo, long tranDetailsId, string loginUser)
        {
            string sql = "update [LES].[TT_WMM_STOCKS] "
                + "set [STOCKS] = [STOCKS] + @STOCKS"
                + ",[AVAILBLE_STOCKS] = [AVAILBLE_STOCKS] + @AVAILBLE_STOCKS"
                + ",[FRAGMENT_NUM] = [FRAGMENT_NUM] + @FRAGMENT_NUM "
                + ",[STOCKS_NUM] = [STOCKS_NUM] + @STOCKS_NUM "
                + ",[PURCHASE_PART_PRICE] = [PURCHASE_PART_PRICE] + @PURCHASE_PART_PRICE"
                + ",[MODIFY_USER] = @MODIFY_USER"
                + ",[MODIFY_DATE] = GETDATE() "
                + "where [ID] = @ID;"
                + "update [LES].[TT_WMM_TRAN_DETAILS] "
                + "set [TRAN_STATE] = @TRAN_STATE"
                + ",[MODIFY_USER] = @MODIFY_USER"
                + ",[MODIFY_DATE] = GETDATE() "
                + "where [ID] = @TRAN_DETAILS_ID;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@STOCKS", DbType.Int32, stocksInfo.Stocks);
            db.AddInParameter(cmd, "@AVAILBLE_STOCKS", DbType.Decimal, stocksInfo.AvailbleStocks);
            db.AddInParameter(cmd, "@FRAGMENT_NUM", DbType.Decimal, stocksInfo.FragmentNum);
            db.AddInParameter(cmd, "@STOCKS_NUM", DbType.Decimal, stocksInfo.StocksNum);
            //db.AddInParameter(cmd, "@PURCHASE_PART_PRICE", DbType.Decimal, stocksInfo.PurchasePartPrice);
            db.AddInParameter(cmd, "@MODIFY_USER", DbType.AnsiString, loginUser);
            db.AddInParameter(cmd, "@ID", DbType.Int64, stocksInfo.Id);
            db.AddInParameter(cmd, "@TRAN_STATE", DbType.Int32, (int)WmmTranStateConstants.Done);
            db.AddInParameter(cmd, "@TRAN_DETAILS_ID", DbType.Int64, tranDetailsId);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        public bool StocksDown(StocksInfo stocksInfo, long tranDetailsId, string loginUser)
        {
            string sql = "update [LES].[TT_WMM_STOCKS] "
                + "set [STOCKS] = [STOCKS] - @STOCKS"
                + ",[AVAILBLE_STOCKS] = [AVAILBLE_STOCKS] - @AVAILBLE_STOCKS"
                + ",[FRAGMENT_NUM] = [FRAGMENT_NUM] - @FRAGMENT_NUM "
                + ",[STOCKS_NUM] = [STOCKS_NUM] - @STOCKS_NUM "
                + ",[SALE_PART_PRICE] = [SALE_PART_PRICE] + @SALE_PART_PRICE"
                + ",[MODIFY_USER] = @MODIFY_USER"
                + ",[MODIFY_DATE] = GETDATE() "
                + "where [ID] = @ID;"
                + "update [LES].[TT_WMM_TRAN_DETAILS] "
                + "set [TRAN_STATE] = @TRAN_STATE"
                + ",[MODIFY_USER] = @MODIFY_USER"
                + ",[MODIFY_DATE] = GETDATE() "
                + "where [ID] = @TRAN_DETAILS_ID;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@STOCKS", DbType.Int32, stocksInfo.Stocks);
            db.AddInParameter(cmd, "@AVAILBLE_STOCKS", DbType.Decimal, stocksInfo.AvailbleStocks);
            db.AddInParameter(cmd, "@FRAGMENT_NUM", DbType.Decimal, stocksInfo.FragmentNum);
            db.AddInParameter(cmd, "@STOCKS_NUM", DbType.Decimal, stocksInfo.StocksNum);
            //db.AddInParameter(cmd, "@SALE_PART_PRICE", DbType.Decimal, stocksInfo.SalePartPrice);
            db.AddInParameter(cmd, "@MODIFY_USER", DbType.AnsiString, loginUser);
            db.AddInParameter(cmd, "@ID", DbType.Int64, stocksInfo.Id);
            db.AddInParameter(cmd, "@TRAN_STATE", DbType.Int32, (int)WmmTranStateConstants.Done);
            db.AddInParameter(cmd, "@TRAN_DETAILS_ID", DbType.Int64, tranDetailsId);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }
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
            string sql = "select SUM(ISNULL([STOCKS_NUM],0)) from [LES].[TT_WMM_STOCKS] with(nolock) "
                + "where [VALID_FLAG] = 1 and [PART_NO] = @PART_NO and [WM_NO] = @WM_NO and [ZONE_NO] = @ZONE_NO and [DLOC] = @DLOC;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PART_NO", DbType.AnsiString, partNo);
            db.AddInParameter(cmd, "@WM_NO", DbType.AnsiString, wmNo);
            db.AddInParameter(cmd, "@ZONE_NO", DbType.AnsiString, zoneNo);
            db.AddInParameter(cmd, "@DLOC", DbType.AnsiString, dloc);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value) return 0;
            return Convert.ToDecimal(result);
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
            //包装数、冻结数、散装数、库存数、采购金额做累加
            string sql = "update [LES].[TT_WMM_STOCKS] "
                + "set [PACKAGE] = [PACKAGE] + @PACKAGE"
                + ",[FROZEN_STOCKS] = [FROZEN_STOCKS] + @FROZEN_STOCKS"
                + ",[FRAGMENT_NUM] = [FRAGMENT_NUM] + @FRAGMENT_NUM "
                + ",[STOCKS_NUM] = [STOCKS_NUM] + @STOCKS_NUM "
                + ",[SALE_PART_PRICE] = [SALE_PART_PRICE] + @SALE_PART_PRICE"
                + ",[MODIFY_USER] = @MODIFY_USER"
                + ",[MODIFY_DATE] = GETDATE() "
                + "where [ID] = @ID;"
                + "update [LES].[TT_WMM_TRAN_DETAILS] "
                + "set [TRAN_STATE] = @TRAN_STATE"
                + ",[MODIFY_USER] = @MODIFY_USER"
                + ",[MODIFY_DATE] = GETDATE() "
                + "where [ID] = @TRAN_DETAILS_ID;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PACKAGE", DbType.Int32, stocksInfo.Package);
            db.AddInParameter(cmd, "@FROZEN_STOCKS", DbType.Decimal, stocksInfo.FrozenStocks);
            db.AddInParameter(cmd, "@FRAGMENT_NUM", DbType.Decimal, stocksInfo.FragmentNum);
            db.AddInParameter(cmd, "@STOCKS_NUM", DbType.Decimal, stocksInfo.StocksNum);
            //db.AddInParameter(cmd, "@SALE_PART_PRICE", DbType.Decimal, stocksInfo.SalePartPrice);
            db.AddInParameter(cmd, "@MODIFY_USER", DbType.AnsiString, loginUser);
            db.AddInParameter(cmd, "@ID", DbType.Int64, stocksInfo.Id);
            db.AddInParameter(cmd, "@TRAN_STATE", DbType.Int32, (int)WmmTranStateConstants.Done);
            db.AddInParameter(cmd, "@TRAN_DETAILS_ID", DbType.Int64, tranDetailsId);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
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
            //可用数做递减
            //冻结数做累加
            string sql = "update [LES].[TT_WMM_STOCKS] "
                + ",[AVAILBLE_STOCKS] = [AVAILBLE_STOCKS] - @AVAILBLE_STOCKS"
                + ",[FROZEN_STOCKS] = [FROZEN_STOCKS] + @FROZEN_STOCKS"
                + ",[MODIFY_USER] = @MODIFY_USER"
                + ",[MODIFY_DATE] = GETDATE() "
                + "where [ID] = @ID;"
                + "update [LES].[TT_WMM_TRAN_DETAILS] "
                + "set [TRAN_STATE] = @TRAN_STATE"
                + ",[MODIFY_USER] = @MODIFY_USER"
                + ",[MODIFY_DATE] = GETDATE()"
                + "where [ID] = @TRAN_DETAILS_ID;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@AVAILBLE_STOCKS", DbType.Decimal, stocksInfo.AvailbleStocks);
            db.AddInParameter(cmd, "@FROZEN_STOCKS", DbType.Decimal, stocksInfo.FrozenStocks);
            db.AddInParameter(cmd, "@MODIFY_USER", DbType.AnsiString, loginUser);
            db.AddInParameter(cmd, "@ID", DbType.Int64, stocksInfo.Id);
            db.AddInParameter(cmd, "@TRAN_STATE", DbType.Int32, (int)WmmTranStateConstants.Done);
            db.AddInParameter(cmd, "@TRAN_DETAILS_ID", DbType.Int64, tranDetailsId);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 原库冻结
        /// </summary>
        /// <param name="stocksInfo"></param>
        /// <param name="tranDetailsId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool StocksStatusThaw(StocksInfo stocksInfo, long tranDetailsId, string loginUser)
        {
            //可用数做累加
            //冻结数做递减
            string sql = "update [LES].[TT_WMM_STOCKS] "
                + ",[AVAILBLE_STOCKS] = [AVAILBLE_STOCKS] + @AVAILBLE_STOCKS"
                + ",[FROZEN_STOCKS] = [FROZEN_STOCKS] - @FROZEN_STOCKS"
                + ",[MODIFY_USER] = @MODIFY_USER"
                + ",[MODIFY_DATE] = GETDATE() "
                + "where [ID] = @ID;"
                + "update [LES].[TT_WMM_TRAN_DETAILS] "
                + "set [TRAN_STATE] = @TRAN_STATE"
                + ",[MODIFY_USER] = @MODIFY_USER"
                + ",[MODIFY_DATE] = GETDATE()"
                + "where [ID] = @TRAN_DETAILS_ID;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@AVAILBLE_STOCKS", DbType.Decimal, stocksInfo.AvailbleStocks);
            db.AddInParameter(cmd, "@FROZEN_STOCKS", DbType.Decimal, stocksInfo.FrozenStocks);
            db.AddInParameter(cmd, "@MODIFY_USER", DbType.AnsiString, loginUser);
            db.AddInParameter(cmd, "@ID", DbType.Int64, stocksInfo.Id);
            db.AddInParameter(cmd, "@TRAN_STATE", DbType.Int32, (int)WmmTranStateConstants.Done);
            db.AddInParameter(cmd, "@TRAN_DETAILS_ID", DbType.Int64, tranDetailsId);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
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
            //包装数、冻结数、散装数、库存数、采购金额做递减
            string sql = "update [LES].[TT_WMM_STOCKS] "
                + "set [PACKAGE] = [PACKAGE] - @PACKAGE"
                + ",[FROZEN_STOCKS] = [FROZEN_STOCKS] - @FROZEN_STOCKS"
                + ",[FRAGMENT_NUM] = [FRAGMENT_NUM] - @FRAGMENT_NUM "
                + ",[STOCKS_NUM] = [STOCKS_NUM] - @STOCKS_NUM "
                + ",[SALE_PART_PRICE] = [SALE_PART_PRICE] -s @SALE_PART_PRICE"
                + ",[MODIFY_USER] = @MODIFY_USER"
                + ",[MODIFY_DATE] = GETDATE() "
                + "where [ID] = @ID;"
                + "update [LES].[TT_WMM_TRAN_DETAILS] "
                + "set [TRAN_STATE] = @TRAN_STATE"
                + ",[MODIFY_USER] = @MODIFY_USER"
                + ",[MODIFY_DATE] = GETDATE() "
                + "where [ID] = @TRAN_DETAILS_ID;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PACKAGE", DbType.Int32, stocksInfo.Package);
            db.AddInParameter(cmd, "@FROZEN_STOCKS", DbType.Decimal, stocksInfo.FrozenStocks);
            db.AddInParameter(cmd, "@FRAGMENT_NUM", DbType.Decimal, stocksInfo.FragmentNum);
            db.AddInParameter(cmd, "@STOCKS_NUM", DbType.Decimal, stocksInfo.StocksNum);
            //db.AddInParameter(cmd, "@SALE_PART_PRICE", DbType.Decimal, stocksInfo.SalePartPrice);
            db.AddInParameter(cmd, "@MODIFY_USER", DbType.AnsiString, loginUser);
            db.AddInParameter(cmd, "@ID", DbType.Int64, stocksInfo.Id);
            db.AddInParameter(cmd, "@TRAN_STATE", DbType.Int32, (int)WmmTranStateConstants.Done);
            db.AddInParameter(cmd, "@TRAN_DETAILS_ID", DbType.Int64, tranDetailsId);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }
    }
}
