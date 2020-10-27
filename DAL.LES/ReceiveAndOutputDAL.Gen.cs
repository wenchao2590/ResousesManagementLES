#region Declaim
//---------------------------------------------------------------------------
// Name:		ReceiveAndOutputDAL
// Function: 	Expose data in table V_WMM_RECEIVE_AND_OUTPUT from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年5月21日
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
    /// ReceiveAndOutputDAL对应表[V_WMM_RECEIVE_AND_OUTPUT]
    /// </summary>
    public partial class ReceiveAndOutputDAL : BusinessObjectProvider<ReceiveAndOutputInfo>
	{
		#region Sql Statements
		private const string V_WMM_RECEIVE_AND_OUTPUT_SELECT_BY_ID =
			"";
			
		private const string V_WMM_RECEIVE_AND_OUTPUT_SELECT = 
			@"SELECT ORDER_ID,
				DETAIL_ID,
				ORDER_NO,
				RUNSHEET_NO,
				PLANT,
				WM_NO,
				ZONE_NO,
				TARGET_WM,
				TARGET_ZONE,
				TARGET_DLOC,
				PART_NO,
				PART_CNAME,
				SUPPLIER_NUM,
				REQUIRED_BOX_NUM,
				ACTUAL_BOX_NUM,
				REQUIRED_QTY,
				ACTUAL_QTY,
				IS_OUTPUT,
				FINAL_WM,
				FINAL_ZONE,
				IS_SCAN_BOX,
				UNQUALIFIED_QTY,
				CHECK_MODE,
				CHECK_STATUS,
				OPERABLE_NUMBER,
				OPERATION_TYPE,
				PACKAGE,
				PACKAGE_MODEL				 
				FROM [LES].[V_WMM_RECEIVE_AND_OUTPUT] WITH (NOLOCK) WHERE 1=1 {0};";
		
		private const string V_WMM_RECEIVE_AND_OUTPUT_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[V_WMM_RECEIVE_AND_OUTPUT]  WITH(NOLOCK) WHERE 1=1 {0};";

		private const string V_WMM_RECEIVE_AND_OUTPUT_INSERT =
			@"INSERT INTO [LES].[V_WMM_RECEIVE_AND_OUTPUT] (
				ORDER_ID,
				DETAIL_ID,
				ORDER_NO,
				RUNSHEET_NO,
				PLANT,
				WM_NO,
				ZONE_NO,
				TARGET_WM,
				TARGET_ZONE,
				TARGET_DLOC,
				PART_NO,
				PART_CNAME,
				SUPPLIER_NUM,
				REQUIRED_BOX_NUM,
				ACTUAL_BOX_NUM,
				REQUIRED_QTY,
				ACTUAL_QTY,
				IS_OUTPUT,
				FINAL_WM,
				FINAL_ZONE,
				IS_SCAN_BOX,
				UNQUALIFIED_QTY,
				CHECK_MODE,
				CHECK_STATUS,
				OPERABLE_NUMBER,
				OPERATION_TYPE,
				PACKAGE,
				PACKAGE_MODEL				 
			) VALUES (
				@ORDER_ID,
				@DETAIL_ID,
				@ORDER_NO,
				@RUNSHEET_NO,
				@PLANT,
				@WM_NO,
				@ZONE_NO,
				@TARGET_WM,
				@TARGET_ZONE,
				@TARGET_DLOC,
				@PART_NO,
				@PART_CNAME,
				@SUPPLIER_NUM,
				@REQUIRED_BOX_NUM,
				@ACTUAL_BOX_NUM,
				@REQUIRED_QTY,
				@ACTUAL_QTY,
				@IS_OUTPUT,
				@FINAL_WM,
				@FINAL_ZONE,
				@IS_SCAN_BOX,
				@UNQUALIFIED_QTY,
				@CHECK_MODE,
				@CHECK_STATUS,
				@OPERABLE_NUMBER,
				@OPERATION_TYPE,
				@PACKAGE,
				@PACKAGE_MODEL				 
			);";
		private const string V_WMM_RECEIVE_AND_OUTPUT_UPDATE =
			"";

		private const string V_WMM_RECEIVE_AND_OUTPUT_DELETE =
			"";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>ReceiveAndOutputInfo Collection </returns>
		public List<ReceiveAndOutputInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(V_WMM_RECEIVE_AND_OUTPUT_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>ReceiveAndOutputInfo Collection </returns>
		public List<ReceiveAndOutputInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<ReceiveAndOutputInfo> list = new List<ReceiveAndOutputInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateReceiveAndOutputInfo(dr));
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
		public List<ReceiveAndOutputInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                textOrder += "[nid] desc";
            string sql = "select top " + pageRow + " * from "
                + "(select row_number() over(order by " + textOrder + ") as rownumber"
                + ",* from [LES].[V_WMM_RECEIVE_AND_OUTPUT]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<ReceiveAndOutputInfo> list = new List<ReceiveAndOutputInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateReceiveAndOutputInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(V_WMM_RECEIVE_AND_OUTPUT_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public bool Add(ReceiveAndOutputInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(V_WMM_RECEIVE_AND_OUTPUT_INSERT);			
			db.AddInParameter(dbCommand, "@ORDER_ID", DbType.Int64, info.OrderId);
			db.AddInParameter(dbCommand, "@DETAIL_ID", DbType.Int64, info.DetailId);
			db.AddInParameter(dbCommand, "@ORDER_NO", DbType.String, info.OrderNo);
			db.AddInParameter(dbCommand, "@RUNSHEET_NO", DbType.String, info.RunsheetNo);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@WM_NO", DbType.String, info.WmNo);
			db.AddInParameter(dbCommand, "@ZONE_NO", DbType.String, info.ZoneNo);
			db.AddInParameter(dbCommand, "@TARGET_WM", DbType.String, info.TargetWm);
			db.AddInParameter(dbCommand, "@TARGET_ZONE", DbType.String, info.TargetZone);
			db.AddInParameter(dbCommand, "@TARGET_DLOC", DbType.String, info.TargetDloc);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@PART_CNAME", DbType.String, info.PartCname);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@REQUIRED_BOX_NUM", DbType.Int32, info.RequiredBoxNum);
			db.AddInParameter(dbCommand, "@ACTUAL_BOX_NUM", DbType.Int32, info.ActualBoxNum);
			db.AddInParameter(dbCommand, "@REQUIRED_QTY", DbType.Decimal, info.RequiredQty);
			db.AddInParameter(dbCommand, "@ACTUAL_QTY", DbType.Decimal, info.ActualQty);
			db.AddInParameter(dbCommand, "@IS_OUTPUT", DbType.Int32, info.IsOutput);
			db.AddInParameter(dbCommand, "@FINAL_WM", DbType.String, info.FinalWm);
			db.AddInParameter(dbCommand, "@FINAL_ZONE", DbType.String, info.FinalZone);
			db.AddInParameter(dbCommand, "@IS_SCAN_BOX", DbType.Boolean, info.IsScanBox);
			db.AddInParameter(dbCommand, "@UNQUALIFIED_QTY", DbType.Decimal, info.UnqualifiedQty);
			db.AddInParameter(dbCommand, "@CHECK_MODE", DbType.Int32, info.CheckMode);
			db.AddInParameter(dbCommand, "@CHECK_STATUS", DbType.Int32, info.CheckStatus);
			db.AddInParameter(dbCommand, "@OPERABLE_NUMBER", DbType.Decimal, info.OperableNumber);
			db.AddInParameter(dbCommand, "@OPERATION_TYPE", DbType.Int32, info.OperationType);
			db.AddInParameter(dbCommand, "@PACKAGE", DbType.Decimal, info.Package);
			db.AddInParameter(dbCommand, "@PACKAGE_MODEL", DbType.String, info.PackageModel);
			return db.ExecuteNonQuery(dbCommand) > 0 ? true : false;		
		}		
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(ReceiveAndOutputInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(V_WMM_RECEIVE_AND_OUTPUT_UPDATE);				
			db.AddInParameter(dbCommand, "@ORDER_ID", DbType.Int64, info.OrderId);
			db.AddInParameter(dbCommand, "@DETAIL_ID", DbType.Int64, info.DetailId);
			db.AddInParameter(dbCommand, "@ORDER_NO", DbType.String, info.OrderNo);
			db.AddInParameter(dbCommand, "@RUNSHEET_NO", DbType.String, info.RunsheetNo);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@WM_NO", DbType.String, info.WmNo);
			db.AddInParameter(dbCommand, "@ZONE_NO", DbType.String, info.ZoneNo);
			db.AddInParameter(dbCommand, "@TARGET_WM", DbType.String, info.TargetWm);
			db.AddInParameter(dbCommand, "@TARGET_ZONE", DbType.String, info.TargetZone);
			db.AddInParameter(dbCommand, "@TARGET_DLOC", DbType.String, info.TargetDloc);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@PART_CNAME", DbType.String, info.PartCname);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@REQUIRED_BOX_NUM", DbType.Int32, info.RequiredBoxNum);
			db.AddInParameter(dbCommand, "@ACTUAL_BOX_NUM", DbType.Int32, info.ActualBoxNum);
			db.AddInParameter(dbCommand, "@REQUIRED_QTY", DbType.Decimal, info.RequiredQty);
			db.AddInParameter(dbCommand, "@ACTUAL_QTY", DbType.Decimal, info.ActualQty);
			db.AddInParameter(dbCommand, "@IS_OUTPUT", DbType.Int32, info.IsOutput);
			db.AddInParameter(dbCommand, "@FINAL_WM", DbType.String, info.FinalWm);
			db.AddInParameter(dbCommand, "@FINAL_ZONE", DbType.String, info.FinalZone);
			db.AddInParameter(dbCommand, "@IS_SCAN_BOX", DbType.Boolean, info.IsScanBox);
			db.AddInParameter(dbCommand, "@UNQUALIFIED_QTY", DbType.Decimal, info.UnqualifiedQty);
			db.AddInParameter(dbCommand, "@CHECK_MODE", DbType.Int32, info.CheckMode);
			db.AddInParameter(dbCommand, "@CHECK_STATUS", DbType.Int32, info.CheckStatus);
			db.AddInParameter(dbCommand, "@OPERABLE_NUMBER", DbType.Decimal, info.OperableNumber);
			db.AddInParameter(dbCommand, "@OPERATION_TYPE", DbType.Int32, info.OperationType);
			db.AddInParameter(dbCommand, "@PACKAGE", DbType.Decimal, info.Package);
			db.AddInParameter(dbCommand, "@PACKAGE_MODEL", DbType.String, info.PackageModel);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		
		
		#endregion
		  
		#region Helpers   
	  
		private static ReceiveAndOutputInfo CreateReceiveAndOutputInfo(IDataReader rdr)
		{
			ReceiveAndOutputInfo info = new ReceiveAndOutputInfo();
			info.OrderId = DBConvert.GetInt64Nullable(rdr, rdr.GetOrdinal("ORDER_ID"));			
			info.DetailId = DBConvert.GetInt64(rdr, rdr.GetOrdinal("DETAIL_ID"));			
			info.OrderNo = DBConvert.GetString(rdr, rdr.GetOrdinal("ORDER_NO"));			
			info.RunsheetNo = DBConvert.GetString(rdr, rdr.GetOrdinal("RUNSHEET_NO"));			
			info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));			
			info.WmNo = DBConvert.GetString(rdr, rdr.GetOrdinal("WM_NO"));			
			info.ZoneNo = DBConvert.GetString(rdr, rdr.GetOrdinal("ZONE_NO"));			
			info.TargetWm = DBConvert.GetString(rdr, rdr.GetOrdinal("TARGET_WM"));			
			info.TargetZone = DBConvert.GetString(rdr, rdr.GetOrdinal("TARGET_ZONE"));			
			info.TargetDloc = DBConvert.GetString(rdr, rdr.GetOrdinal("TARGET_DLOC"));			
			info.PartNo = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_NO"));			
			info.PartCname = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_CNAME"));			
			info.SupplierNum = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NUM"));			
			info.RequiredBoxNum = DBConvert.GetInt32(rdr, rdr.GetOrdinal("REQUIRED_BOX_NUM"));			
			info.ActualBoxNum = DBConvert.GetInt32(rdr, rdr.GetOrdinal("ACTUAL_BOX_NUM"));			
			info.RequiredQty = DBConvert.GetDecimal(rdr, rdr.GetOrdinal("REQUIRED_QTY"));			
			info.ActualQty = DBConvert.GetDecimal(rdr, rdr.GetOrdinal("ACTUAL_QTY"));			
			info.IsOutput = DBConvert.GetInt32(rdr, rdr.GetOrdinal("IS_OUTPUT"));			
			info.FinalWm = DBConvert.GetString(rdr, rdr.GetOrdinal("FINAL_WM"));			
			info.FinalZone = DBConvert.GetString(rdr, rdr.GetOrdinal("FINAL_ZONE"));			
			info.IsScanBox = DBConvert.GetBool(rdr, rdr.GetOrdinal("IS_SCAN_BOX"));			
			info.UnqualifiedQty = DBConvert.GetDecimal(rdr, rdr.GetOrdinal("UNQUALIFIED_QTY"));			
			info.CheckMode = DBConvert.GetInt32(rdr, rdr.GetOrdinal("CHECK_MODE"));			
			info.CheckStatus = DBConvert.GetInt32(rdr, rdr.GetOrdinal("CHECK_STATUS"));			
			info.OperableNumber = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("OPERABLE_NUMBER"));			
			info.OperationType = DBConvert.GetInt32(rdr, rdr.GetOrdinal("OPERATION_TYPE"));			
			info.Package = DBConvert.GetDecimal(rdr, rdr.GetOrdinal("PACKAGE"));			
			info.PackageModel = DBConvert.GetString(rdr, rdr.GetOrdinal("PACKAGE_MODEL"));			
			return info;
		}
		
		#endregion
	}
}