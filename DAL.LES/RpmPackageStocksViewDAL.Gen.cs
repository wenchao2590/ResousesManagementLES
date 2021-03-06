#region Declaim
//---------------------------------------------------------------------------
// Name:		RpmPackageStocksViewDAL
// Function: 	Expose data in table V_TM_RPM_PACKAGE_STOCKS_VIEW from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年1月8日
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------
#endregion

#region Imported Namespace

using DM.LES;
using Infrustructure.Data;
using Infrustructure.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

#endregion

namespace DAL.LES
{
    //// <summary>
    /// RpmPackageStocksViewDAL对应表[V_TM_RPM_PACKAGE_STOCKS_VIEW]
    /// </summary>
    public partial class RpmPackageStocksViewDAL : BusinessObjectProvider<RpmPackageStocksViewInfo>
    {
        #region Sql Statements
        private const string V_TM_RPM_PACKAGE_STOCKS_VIEW_SELECT_BY_ID =
            "";

        private const string V_TM_RPM_PACKAGE_STOCKS_VIEW_SELECT =
            @"SELECT TRANSER,
				PLANT,
				LOGISTIC_LATION,
				ASSEMBLY_LINE,
				PACKAGE_TYPE_Name,
				STOCK,
				TRAN_TYPE,
				WH_FEE,
				AVAILABLE_STOCK,
				LOGISTICES_LEADTIME,
				CREATE_DATE,
				HIGH_NUMBER,
				PACKAGE_STOCK,
				PACKAGE_TYPE,
				STOCK_ID,
				DOCK,
				PACKAGE_CNAME,
				ROUTE,
				PLANT_ZONE,
				INFORMATIONER,
				COUNTER,
				STOCK_TYPE,
				WM_NO,
				UPDATE_USER,
				PACKAGE_FEE,
				COMMENTS,
				PACKAGE_FREEZE_STOCK,
				OCCUPY_AREA,
				MAX,
				ELOC,
				PACKAGE_ENAME,
				ZONE_NO,
				KEEPER,
				PACKAGE_NO,
				FREEZE_STOCK,
				STOCK_STATE,
				SAGE,
				WORKSHOP,
				DLOC,
				CREATE_USER,
				PACKAGE_AVAILABLE_STOCK,
				MIN,
				TRANS_FEE,
				UPDATE_DATE				 
				FROM [LES].[V_TM_RPM_PACKAGE_STOCKS_VIEW] WITH (NOLOCK) WHERE 1=1 {0};";

        private const string V_TM_RPM_PACKAGE_STOCKS_VIEW_SELECT_COUNTS =
            @"SELECT count(*) FROM [LES].[V_TM_RPM_PACKAGE_STOCKS_VIEW]  WITH(NOLOCK) WHERE 1=1 {0};";

        private const string V_TM_RPM_PACKAGE_STOCKS_VIEW_INSERT =
            @"INSERT INTO [LES].[V_TM_RPM_PACKAGE_STOCKS_VIEW] (
				TRANSER,
				PLANT,
				LOGISTIC_LATION,
				ASSEMBLY_LINE,
				PACKAGE_TYPE_Name,
				STOCK,
				TRAN_TYPE,
				WH_FEE,
				AVAILABLE_STOCK,
				LOGISTICES_LEADTIME,
				CREATE_DATE,
				HIGH_NUMBER,
				PACKAGE_STOCK,
				PACKAGE_TYPE,
				STOCK_ID,
				DOCK,
				PACKAGE_CNAME,
				ROUTE,
				PLANT_ZONE,
				INFORMATIONER,
				COUNTER,
				STOCK_TYPE,
				WM_NO,
				UPDATE_USER,
				PACKAGE_FEE,
				COMMENTS,
				PACKAGE_FREEZE_STOCK,
				OCCUPY_AREA,
				MAX,
				ELOC,
				PACKAGE_ENAME,
				ZONE_NO,
				KEEPER,
				PACKAGE_NO,
				FREEZE_STOCK,
				STOCK_STATE,
				SAGE,
				WORKSHOP,
				DLOC,
				CREATE_USER,
				PACKAGE_AVAILABLE_STOCK,
				MIN,
				TRANS_FEE,
				UPDATE_DATE				 
			) VALUES (
				@TRANSER,
				@PLANT,
				@LOGISTIC_LATION,
				@ASSEMBLY_LINE,
				@PACKAGE_TYPE_Name,
				@STOCK,
				@TRAN_TYPE,
				@WH_FEE,
				@AVAILABLE_STOCK,
				@LOGISTICES_LEADTIME,
				@CREATE_DATE,
				@HIGH_NUMBER,
				@PACKAGE_STOCK,
				@PACKAGE_TYPE,
				@STOCK_ID,
				@DOCK,
				@PACKAGE_CNAME,
				@ROUTE,
				@PLANT_ZONE,
				@INFORMATIONER,
				@COUNTER,
				@STOCK_TYPE,
				@WM_NO,
				@UPDATE_USER,
				@PACKAGE_FEE,
				@COMMENTS,
				@PACKAGE_FREEZE_STOCK,
				@OCCUPY_AREA,
				@MAX,
				@ELOC,
				@PACKAGE_ENAME,
				@ZONE_NO,
				@KEEPER,
				@PACKAGE_NO,
				@FREEZE_STOCK,
				@STOCK_STATE,
				@SAGE,
				@WORKSHOP,
				@DLOC,
				@CREATE_USER,
				@PACKAGE_AVAILABLE_STOCK,
				@MIN,
				@TRANS_FEE,
				@UPDATE_DATE				 
			);";
        private const string V_TM_RPM_PACKAGE_STOCKS_VIEW_UPDATE =
            "";

        private const string V_TM_RPM_PACKAGE_STOCKS_VIEW_DELETE =
            "";
        #endregion

        #region Access Methods

        /// <summary>
        /// Get data collection
        /// </summary>
        /// <param name="textWhere">Conditon</param>
        /// <param name="orderText">Sort</param>
        /// <returns>RpmPackageStocksViewInfo Collection </returns>
        public List<RpmPackageStocksViewInfo> GetList(string textWhere, string orderText)
        {
            string query = string.Empty;
            if (string.IsNullOrEmpty(textWhere))
                query = string.Empty;
            else
            {
                if (textWhere.Trim().StartsWith("and", StringComparison.OrdinalIgnoreCase))
                    query = textWhere;
                else
                    query = " and " + textWhere;
            }
            if (!string.IsNullOrEmpty(orderText))
                query += " order by " + orderText;

            return GetList(string.Format(V_TM_RPM_PACKAGE_STOCKS_VIEW_SELECT, query));
        }
        /// <summary>
        /// Get data collection
        /// </summary>
        /// <param name="sql">SQL Statement</param>
        /// <returns>RpmPackageStocksViewInfo Collection </returns>
        public List<RpmPackageStocksViewInfo> GetList(string sql)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<RpmPackageStocksViewInfo> list = new List<RpmPackageStocksViewInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateRpmPackageStocksViewInfo(dr));
                }
            }
            return list;
        }
        /// <summary>
        /// 分页查询
        /// </summary>        
        /// <param name="textWhere">查询条件</param>
        /// <param name="orderText">排序字段</param>
        /// <param name="startRowIndex">当前页第一行行号</param>                    
        /// <param name="maximumRows">每页记录数</param>        
        /// <returns></returns>
        public List<RpmPackageStocksViewInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
        {
            if (pageIndex <= 0) pageIndex = 1;
            if (pageRow <= 0) pageRow = 10;
            string whereText = string.Empty;
            if (!string.IsNullOrEmpty(textWhere))
            {
                if (textWhere.Trim().StartsWith("and", StringComparison.OrdinalIgnoreCase))
                    whereText += " where 1=1 " + textWhere;
                else
                    whereText += " where " + textWhere + " and 1=1";
            }
            else
                whereText += " where 1=1 ";
            if (string.IsNullOrEmpty(textOrder))
                textOrder += "[Stock_id] desc";
            string sql = "select top " + pageRow + " * from "
                + "(select row_number() over(order by " + textOrder + ") as rownumber"
                + ",* from [LES].[V_TM_RPM_PACKAGE_STOCKS_VIEW]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<RpmPackageStocksViewInfo> list = new List<RpmPackageStocksViewInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateRpmPackageStocksViewInfo(dr));
                }
            }
            return list;
        }
        /// <summary>
        /// 分页查询
        /// </summary>        
        /// <param name="textWhere">查询条件</param>
        /// <param name="orderText">排序字段</param>            
        /// <returns></returns>
        public int GetCounts(string textWhere)
        {
            if (string.IsNullOrEmpty(textWhere))
                textWhere = string.Empty;
            else
            {
                if (!textWhere.Trim().StartsWith("and", StringComparison.OrdinalIgnoreCase))
                    textWhere = " and " + textWhere;
            }
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(string.Format(V_TM_RPM_PACKAGE_STOCKS_VIEW_SELECT_COUNTS, textWhere));
            return Convert.ToInt32(db.ExecuteScalar(cmd));
        }
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="info"> info</param>
        public bool Add(RpmPackageStocksViewInfo info)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(V_TM_RPM_PACKAGE_STOCKS_VIEW_INSERT);
            db.AddInParameter(dbCommand, "@TRANSER", DbType.String, info.Transer);
            db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
            db.AddInParameter(dbCommand, "@LOGISTIC_LATION", DbType.String, info.LogisticLation);
            db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
            db.AddInParameter(dbCommand, "@PACKAGE_TYPE_Name", DbType.String, info.PackageTypeName);
            db.AddInParameter(dbCommand, "@STOCK", DbType.Decimal, info.Stock);
            db.AddInParameter(dbCommand, "@TRAN_TYPE", DbType.Int32, info.TranType);
            db.AddInParameter(dbCommand, "@WH_FEE", DbType.Decimal, info.WhFee);
            db.AddInParameter(dbCommand, "@AVAILABLE_STOCK", DbType.Decimal, info.AvailableStock);
            db.AddInParameter(dbCommand, "@LOGISTICES_LEADTIME", DbType.Int32, info.LogisticesLeadtime);
            db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
            db.AddInParameter(dbCommand, "@HIGH_NUMBER", DbType.Int32, info.HighNumber);
            db.AddInParameter(dbCommand, "@PACKAGE_STOCK", DbType.Decimal, info.PackageStock);
            db.AddInParameter(dbCommand, "@PACKAGE_TYPE", DbType.Int32, info.PackageType);
            db.AddInParameter(dbCommand, "@STOCK_ID", DbType.Int32, info.StockId);
            db.AddInParameter(dbCommand, "@DOCK", DbType.String, info.Dock);
            db.AddInParameter(dbCommand, "@PACKAGE_CNAME", DbType.String, info.PackageCname);
            db.AddInParameter(dbCommand, "@ROUTE", DbType.String, info.Route);
            db.AddInParameter(dbCommand, "@PLANT_ZONE", DbType.String, info.PlantZone);
            db.AddInParameter(dbCommand, "@INFORMATIONER", DbType.String, info.Informationer);
            db.AddInParameter(dbCommand, "@COUNTER", DbType.Decimal, info.Counter);
            db.AddInParameter(dbCommand, "@STOCK_TYPE", DbType.Int32, info.StockType);
            db.AddInParameter(dbCommand, "@WM_NO", DbType.String, info.WmNo);
            db.AddInParameter(dbCommand, "@UPDATE_USER", DbType.String, info.UpdateUser);
            db.AddInParameter(dbCommand, "@PACKAGE_FEE", DbType.Decimal, info.PackageFee);
            db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
            db.AddInParameter(dbCommand, "@PACKAGE_FREEZE_STOCK", DbType.Decimal, info.PackageFreezeStock);
            db.AddInParameter(dbCommand, "@OCCUPY_AREA", DbType.Decimal, info.OccupyArea);
            db.AddInParameter(dbCommand, "@MAX", DbType.Decimal, info.Max);
            db.AddInParameter(dbCommand, "@ELOC", DbType.String, info.Eloc);
            db.AddInParameter(dbCommand, "@PACKAGE_ENAME", DbType.String, info.PackageEname);
            db.AddInParameter(dbCommand, "@ZONE_NO", DbType.String, info.ZoneNo);
            db.AddInParameter(dbCommand, "@KEEPER", DbType.String, info.Keeper);
            db.AddInParameter(dbCommand, "@PACKAGE_NO", DbType.String, info.PackageNo);
            db.AddInParameter(dbCommand, "@FREEZE_STOCK", DbType.Decimal, info.FreezeStock);
            db.AddInParameter(dbCommand, "@STOCK_STATE", DbType.Int32, info.StockState);
            db.AddInParameter(dbCommand, "@SAGE", DbType.Decimal, info.Sage);
            db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
            db.AddInParameter(dbCommand, "@DLOC", DbType.String, info.Dloc);
            db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
            db.AddInParameter(dbCommand, "@PACKAGE_AVAILABLE_STOCK", DbType.Decimal, info.PackageAvailableStock);
            db.AddInParameter(dbCommand, "@MIN", DbType.Decimal, info.Min);
            db.AddInParameter(dbCommand, "@TRANS_FEE", DbType.Decimal, info.TransFee);
            db.AddInParameter(dbCommand, "@UPDATE_DATE", DbType.DateTime, info.UpdateDate);
            return db.ExecuteNonQuery(dbCommand) > 0 ? true : false;
        }
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="info">info</param>
        public int Update(RpmPackageStocksViewInfo info)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(V_TM_RPM_PACKAGE_STOCKS_VIEW_UPDATE);
            db.AddInParameter(dbCommand, "@TRANSER", DbType.String, info.Transer);
            db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
            db.AddInParameter(dbCommand, "@LOGISTIC_LATION", DbType.String, info.LogisticLation);
            db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
            db.AddInParameter(dbCommand, "@PACKAGE_TYPE_Name", DbType.String, info.PackageTypeName);
            db.AddInParameter(dbCommand, "@STOCK", DbType.Decimal, info.Stock);
            db.AddInParameter(dbCommand, "@TRAN_TYPE", DbType.Int32, info.TranType);
            db.AddInParameter(dbCommand, "@WH_FEE", DbType.Decimal, info.WhFee);
            db.AddInParameter(dbCommand, "@AVAILABLE_STOCK", DbType.Decimal, info.AvailableStock);
            db.AddInParameter(dbCommand, "@LOGISTICES_LEADTIME", DbType.Int32, info.LogisticesLeadtime);
            db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
            db.AddInParameter(dbCommand, "@HIGH_NUMBER", DbType.Int32, info.HighNumber);
            db.AddInParameter(dbCommand, "@PACKAGE_STOCK", DbType.Decimal, info.PackageStock);
            db.AddInParameter(dbCommand, "@PACKAGE_TYPE", DbType.Int32, info.PackageType);
            db.AddInParameter(dbCommand, "@STOCK_ID", DbType.Int32, info.StockId);
            db.AddInParameter(dbCommand, "@DOCK", DbType.String, info.Dock);
            db.AddInParameter(dbCommand, "@PACKAGE_CNAME", DbType.String, info.PackageCname);
            db.AddInParameter(dbCommand, "@ROUTE", DbType.String, info.Route);
            db.AddInParameter(dbCommand, "@PLANT_ZONE", DbType.String, info.PlantZone);
            db.AddInParameter(dbCommand, "@INFORMATIONER", DbType.String, info.Informationer);
            db.AddInParameter(dbCommand, "@COUNTER", DbType.Decimal, info.Counter);
            db.AddInParameter(dbCommand, "@STOCK_TYPE", DbType.Int32, info.StockType);
            db.AddInParameter(dbCommand, "@WM_NO", DbType.String, info.WmNo);
            db.AddInParameter(dbCommand, "@UPDATE_USER", DbType.String, info.UpdateUser);
            db.AddInParameter(dbCommand, "@PACKAGE_FEE", DbType.Decimal, info.PackageFee);
            db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
            db.AddInParameter(dbCommand, "@PACKAGE_FREEZE_STOCK", DbType.Decimal, info.PackageFreezeStock);
            db.AddInParameter(dbCommand, "@OCCUPY_AREA", DbType.Decimal, info.OccupyArea);
            db.AddInParameter(dbCommand, "@MAX", DbType.Decimal, info.Max);
            db.AddInParameter(dbCommand, "@ELOC", DbType.String, info.Eloc);
            db.AddInParameter(dbCommand, "@PACKAGE_ENAME", DbType.String, info.PackageEname);
            db.AddInParameter(dbCommand, "@ZONE_NO", DbType.String, info.ZoneNo);
            db.AddInParameter(dbCommand, "@KEEPER", DbType.String, info.Keeper);
            db.AddInParameter(dbCommand, "@PACKAGE_NO", DbType.String, info.PackageNo);
            db.AddInParameter(dbCommand, "@FREEZE_STOCK", DbType.Decimal, info.FreezeStock);
            db.AddInParameter(dbCommand, "@STOCK_STATE", DbType.Int32, info.StockState);
            db.AddInParameter(dbCommand, "@SAGE", DbType.Decimal, info.Sage);
            db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
            db.AddInParameter(dbCommand, "@DLOC", DbType.String, info.Dloc);
            db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
            db.AddInParameter(dbCommand, "@PACKAGE_AVAILABLE_STOCK", DbType.Decimal, info.PackageAvailableStock);
            db.AddInParameter(dbCommand, "@MIN", DbType.Decimal, info.Min);
            db.AddInParameter(dbCommand, "@TRANS_FEE", DbType.Decimal, info.TransFee);
            db.AddInParameter(dbCommand, "@UPDATE_DATE", DbType.DateTime, info.UpdateDate);
            return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
        }


        #endregion

        #region Helpers

        private static RpmPackageStocksViewInfo CreateRpmPackageStocksViewInfo(IDataReader rdr)
        {
            RpmPackageStocksViewInfo info = new RpmPackageStocksViewInfo();
            info.Transer = DBConvert.GetString(rdr, rdr.GetOrdinal("TRANSER"));
            info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));
            info.LogisticLation = DBConvert.GetString(rdr, rdr.GetOrdinal("LOGISTIC_LATION"));
            info.AssemblyLine = DBConvert.GetString(rdr, rdr.GetOrdinal("ASSEMBLY_LINE"));
            info.PackageTypeName = DBConvert.GetString(rdr, rdr.GetOrdinal("PACKAGE_TYPE_Name"));
            info.Stock = DBConvert.GetDecimal(rdr, rdr.GetOrdinal("STOCK"));
            info.TranType = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("TRAN_TYPE"));
            info.WhFee = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("WH_FEE"));
            info.AvailableStock = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("AVAILABLE_STOCK"));
            info.LogisticesLeadtime = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("LOGISTICES_LEADTIME"));
            info.CreateDate = DBConvert.GetDateTime(rdr, rdr.GetOrdinal("CREATE_DATE"));
            info.HighNumber = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("HIGH_NUMBER"));
            info.PackageStock = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("PACKAGE_STOCK"));
            info.PackageType = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PACKAGE_TYPE"));
            info.StockId = DBConvert.GetInt32(rdr, rdr.GetOrdinal("STOCK_ID"));
            info.Dock = DBConvert.GetString(rdr, rdr.GetOrdinal("DOCK"));
            info.PackageCname = DBConvert.GetString(rdr, rdr.GetOrdinal("PACKAGE_CNAME"));
            info.Route = DBConvert.GetString(rdr, rdr.GetOrdinal("ROUTE"));
            info.PlantZone = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT_ZONE"));
            info.Informationer = DBConvert.GetString(rdr, rdr.GetOrdinal("INFORMATIONER"));
            info.Counter = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("COUNTER"));
            info.StockType = DBConvert.GetInt32(rdr, rdr.GetOrdinal("STOCK_TYPE"));
            info.WmNo = DBConvert.GetString(rdr, rdr.GetOrdinal("WM_NO"));
            info.UpdateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("UPDATE_USER"));
            info.PackageFee = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("PACKAGE_FEE"));
            info.Comments = DBConvert.GetString(rdr, rdr.GetOrdinal("COMMENTS"));
            info.PackageFreezeStock = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("PACKAGE_FREEZE_STOCK"));
            info.OccupyArea = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("OCCUPY_AREA"));
            info.Max = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("MAX"));
            info.Eloc = DBConvert.GetString(rdr, rdr.GetOrdinal("ELOC"));
            info.PackageEname = DBConvert.GetString(rdr, rdr.GetOrdinal("PACKAGE_ENAME"));
            info.ZoneNo = DBConvert.GetString(rdr, rdr.GetOrdinal("ZONE_NO"));
            info.Keeper = DBConvert.GetString(rdr, rdr.GetOrdinal("KEEPER"));
            info.PackageNo = DBConvert.GetString(rdr, rdr.GetOrdinal("PACKAGE_NO"));
            info.FreezeStock = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("FREEZE_STOCK"));
            info.StockState = DBConvert.GetInt32(rdr, rdr.GetOrdinal("STOCK_STATE"));
            info.Sage = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("SAGE"));
            info.Workshop = DBConvert.GetString(rdr, rdr.GetOrdinal("WORKSHOP"));
            info.Dloc = DBConvert.GetString(rdr, rdr.GetOrdinal("DLOC"));
            info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));
            info.PackageAvailableStock = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("PACKAGE_AVAILABLE_STOCK"));
            info.Min = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("MIN"));
            info.TransFee = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("TRANS_FEE"));
            info.UpdateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("UPDATE_DATE"));
            return info;
        }

        #endregion
    }
}
