#region Declaim
//---------------------------------------------------------------------------
// Name:		OutbounddeliveryreturnDAL
// Function: 	Expose data in table TT_WMM_OUTBOUNDDELIVERYRETURN from database as business object to MES system.
// Tool:		T4
// CreateDate:	2017年12月21日
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
    /// OutbounddeliveryreturnDAL对应表[TT_WMM_OUTBOUNDDELIVERYRETURN]
    /// </summary>
    public partial class OutbounddeliveryreturnDAL : BusinessObjectProvider<OutbounddeliveryreturnInfo>
	{
		#region Sql Statements
		private const string TT_WMM_OUTBOUNDDELIVERYRETURN_SELECT_BY_ID =
			@"SELECT OUTBOUNDDELIVERYRETURN_ID,
				OUTBOUNDDELIVERYRETURN_NO,
				RETURN_TYPE,
				PLANT,
				WM_NO,
				ZONE_NO,
				DOCK,
				SUPPLIER_NAME,
				SUPPLIER_ADDRESS,
				TRANS_SUPPLIER_NUM,
				RETURN_COMPANY_NUM,
				RETURN_COMPANY_NAME,
				RETURN_COMPANY_ADDRESS,
				RETURNER,
				PHONENUM,
				TRAN_TIME,
				EXPECT_ARRIVAL_TIME,
				ACTUAL_ARRIVAL_TIME,
				CONFIRM_FLAG,
				OPRTR,
				ERP_FLAG,
				COMMENTS,
				CREATE_USER,
				CREATE_DATE,
				UPDATE_USER,
				UPDATE_DATE,
				SUPPLIER_NUM				  
				FROM [LES].[TT_WMM_OUTBOUNDDELIVERYRETURN] WITH(NOLOCK) WHERE 1=1  AND OUTBOUNDDELIVERYRETURN_ID =@OUTBOUNDDELIVERYRETURN_ID;";
			
		private const string TT_WMM_OUTBOUNDDELIVERYRETURN_SELECT = 
			@"SELECT OUTBOUNDDELIVERYRETURN_ID,
				OUTBOUNDDELIVERYRETURN_NO,
				RETURN_TYPE,
				PLANT,
				WM_NO,
				ZONE_NO,
				DOCK,
				SUPPLIER_NAME,
				SUPPLIER_ADDRESS,
				TRANS_SUPPLIER_NUM,
				RETURN_COMPANY_NUM,
				RETURN_COMPANY_NAME,
				RETURN_COMPANY_ADDRESS,
				RETURNER,
				PHONENUM,
				TRAN_TIME,
				EXPECT_ARRIVAL_TIME,
				ACTUAL_ARRIVAL_TIME,
				CONFIRM_FLAG,
				OPRTR,
				ERP_FLAG,
				COMMENTS,
				CREATE_USER,
				CREATE_DATE,
				UPDATE_USER,
				UPDATE_DATE,
				SUPPLIER_NUM				 
				FROM [LES].[TT_WMM_OUTBOUNDDELIVERYRETURN] WITH (NOLOCK) WHERE 1=1 {0};";
		
		private const string TT_WMM_OUTBOUNDDELIVERYRETURN_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TT_WMM_OUTBOUNDDELIVERYRETURN]  WITH(NOLOCK) WHERE 1=1 {0};";

		private const string TT_WMM_OUTBOUNDDELIVERYRETURN_INSERT =
			@"INSERT INTO [LES].[TT_WMM_OUTBOUNDDELIVERYRETURN] (
				OUTBOUNDDELIVERYRETURN_NO,
				RETURN_TYPE,
				PLANT,
				WM_NO,
				ZONE_NO,
				DOCK,
				SUPPLIER_NAME,
				SUPPLIER_ADDRESS,
				TRANS_SUPPLIER_NUM,
				RETURN_COMPANY_NUM,
				RETURN_COMPANY_NAME,
				RETURN_COMPANY_ADDRESS,
				RETURNER,
				PHONENUM,
				TRAN_TIME,
				EXPECT_ARRIVAL_TIME,
				ACTUAL_ARRIVAL_TIME,
				CONFIRM_FLAG,
				OPRTR,
				ERP_FLAG,
				COMMENTS,
				CREATE_USER,
				CREATE_DATE,
				UPDATE_USER,
				UPDATE_DATE,
				SUPPLIER_NUM				 
			) VALUES (
				@OUTBOUNDDELIVERYRETURN_NO,
				@RETURN_TYPE,
				@PLANT,
				@WM_NO,
				@ZONE_NO,
				@DOCK,
				@SUPPLIER_NAME,
				@SUPPLIER_ADDRESS,
				@TRANS_SUPPLIER_NUM,
				@RETURN_COMPANY_NUM,
				@RETURN_COMPANY_NAME,
				@RETURN_COMPANY_ADDRESS,
				@RETURNER,
				@PHONENUM,
				@TRAN_TIME,
				@EXPECT_ARRIVAL_TIME,
				@ACTUAL_ARRIVAL_TIME,
				@CONFIRM_FLAG,
				@OPRTR,
				@ERP_FLAG,
				@COMMENTS,
				@CREATE_USER,
				@CREATE_DATE,
				@UPDATE_USER,
				@UPDATE_DATE,
				@SUPPLIER_NUM				 
			);SELECT @@IDENTITY;";
		private const string TT_WMM_OUTBOUNDDELIVERYRETURN_UPDATE =
			@"UPDATE [LES].[TT_WMM_OUTBOUNDDELIVERYRETURN] WITH(ROWLOCK) 
				SET OUTBOUNDDELIVERYRETURN_NO=@OUTBOUNDDELIVERYRETURN_NO,
				RETURN_TYPE=@RETURN_TYPE,
				PLANT=@PLANT,
				WM_NO=@WM_NO,
				ZONE_NO=@ZONE_NO,
				DOCK=@DOCK,
				SUPPLIER_NAME=@SUPPLIER_NAME,
				SUPPLIER_ADDRESS=@SUPPLIER_ADDRESS,
				TRANS_SUPPLIER_NUM=@TRANS_SUPPLIER_NUM,
				RETURN_COMPANY_NUM=@RETURN_COMPANY_NUM,
				RETURN_COMPANY_NAME=@RETURN_COMPANY_NAME,
				RETURN_COMPANY_ADDRESS=@RETURN_COMPANY_ADDRESS,
				RETURNER=@RETURNER,
				PHONENUM=@PHONENUM,
				TRAN_TIME=@TRAN_TIME,
				EXPECT_ARRIVAL_TIME=@EXPECT_ARRIVAL_TIME,
				ACTUAL_ARRIVAL_TIME=@ACTUAL_ARRIVAL_TIME,
				CONFIRM_FLAG=@CONFIRM_FLAG,
				OPRTR=@OPRTR,
				ERP_FLAG=@ERP_FLAG,
				COMMENTS=@COMMENTS,
				CREATE_USER=@CREATE_USER,
				CREATE_DATE=@CREATE_DATE,
				UPDATE_USER=@UPDATE_USER,
				UPDATE_DATE=@UPDATE_DATE,
				SUPPLIER_NUM=@SUPPLIER_NUM				 
				WHERE 1=1  AND OUTBOUNDDELIVERYRETURN_ID =@OUTBOUNDDELIVERYRETURN_ID;";

		private const string TT_WMM_OUTBOUNDDELIVERYRETURN_DELETE =
			@"DELETE FROM [LES].[TT_WMM_OUTBOUNDDELIVERYRETURN] WITH(ROWLOCK)  
				WHERE 1=1  AND OUTBOUNDDELIVERYRETURN_ID =@OUTBOUNDDELIVERYRETURN_ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get OutbounddeliveryreturnInfo
		/// </summary>
		/// <param name="OUTBOUNDDELIVERYRETURN_ID">OutbounddeliveryreturnInfo Primary key </param>
		/// <returns></returns> 
		public OutbounddeliveryreturnInfo GetInfo(long aOutbounddeliveryreturnId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_WMM_OUTBOUNDDELIVERYRETURN_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@OUTBOUNDDELIVERYRETURN_ID", DbType.Int64, aOutbounddeliveryreturnId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateOutbounddeliveryreturnInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>OutbounddeliveryreturnInfo Collection </returns>
		public List<OutbounddeliveryreturnInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TT_WMM_OUTBOUNDDELIVERYRETURN_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>OutbounddeliveryreturnInfo Collection </returns>
		public List<OutbounddeliveryreturnInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<OutbounddeliveryreturnInfo> list = new List<OutbounddeliveryreturnInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateOutbounddeliveryreturnInfo(dr));
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
		public List<OutbounddeliveryreturnInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                textOrder += "[OUTBOUNDDELIVERYRETURN_ID] desc";
            string sql = "select top " + pageRow + " * from "
                + "(select row_number() over(order by " + textOrder + ") as rownumber"
                + ",* from [LES].[TT_WMM_OUTBOUNDDELIVERYRETURN]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<OutbounddeliveryreturnInfo> list = new List<OutbounddeliveryreturnInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateOutbounddeliveryreturnInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TT_WMM_OUTBOUNDDELIVERYRETURN_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(OutbounddeliveryreturnInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TT_WMM_OUTBOUNDDELIVERYRETURN_INSERT);			
			db.AddInParameter(dbCommand, "@OUTBOUNDDELIVERYRETURN_NO", DbType.String, info.OutbounddeliveryreturnNo);
			db.AddInParameter(dbCommand, "@RETURN_TYPE", DbType.Int32, info.ReturnType);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@WM_NO", DbType.String, info.WmNo);
			db.AddInParameter(dbCommand, "@ZONE_NO", DbType.String, info.ZoneNo);
			db.AddInParameter(dbCommand, "@DOCK", DbType.String, info.Dock);
			db.AddInParameter(dbCommand, "@SUPPLIER_NAME", DbType.String, info.SupplierName);
			db.AddInParameter(dbCommand, "@SUPPLIER_ADDRESS", DbType.String, info.SupplierAddress);
			db.AddInParameter(dbCommand, "@TRANS_SUPPLIER_NUM", DbType.String, info.TransSupplierNum);
			db.AddInParameter(dbCommand, "@RETURN_COMPANY_NUM", DbType.String, info.ReturnCompanyNum);
			db.AddInParameter(dbCommand, "@RETURN_COMPANY_NAME", DbType.String, info.ReturnCompanyName);
			db.AddInParameter(dbCommand, "@RETURN_COMPANY_ADDRESS", DbType.String, info.ReturnCompanyAddress);
			db.AddInParameter(dbCommand, "@RETURNER", DbType.String, info.Returner);
			db.AddInParameter(dbCommand, "@PHONENUM", DbType.String, info.Phonenum);
			db.AddInParameter(dbCommand, "@TRAN_TIME", DbType.DateTime, info.TranTime);
			db.AddInParameter(dbCommand, "@EXPECT_ARRIVAL_TIME", DbType.DateTime, info.ExpectArrivalTime);
			db.AddInParameter(dbCommand, "@ACTUAL_ARRIVAL_TIME", DbType.DateTime, info.ActualArrivalTime);
			db.AddInParameter(dbCommand, "@CONFIRM_FLAG", DbType.Int32, info.ConfirmFlag);
			db.AddInParameter(dbCommand, "@OPRTR", DbType.String, info.Oprtr);
			db.AddInParameter(dbCommand, "@ERP_FLAG", DbType.Int32, info.ErpFlag);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@UPDATE_USER", DbType.String, info.UpdateUser);
			db.AddInParameter(dbCommand, "@UPDATE_DATE", DbType.DateTime, info.UpdateDate);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}		
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(OutbounddeliveryreturnInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_WMM_OUTBOUNDDELIVERYRETURN_UPDATE);				
			db.AddInParameter(dbCommand, "@OUTBOUNDDELIVERYRETURN_ID", DbType.Int64, info.OutbounddeliveryreturnId);
			db.AddInParameter(dbCommand, "@OUTBOUNDDELIVERYRETURN_NO", DbType.String, info.OutbounddeliveryreturnNo);
			db.AddInParameter(dbCommand, "@RETURN_TYPE", DbType.Int32, info.ReturnType);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@WM_NO", DbType.String, info.WmNo);
			db.AddInParameter(dbCommand, "@ZONE_NO", DbType.String, info.ZoneNo);
			db.AddInParameter(dbCommand, "@DOCK", DbType.String, info.Dock);
			db.AddInParameter(dbCommand, "@SUPPLIER_NAME", DbType.String, info.SupplierName);
			db.AddInParameter(dbCommand, "@SUPPLIER_ADDRESS", DbType.String, info.SupplierAddress);
			db.AddInParameter(dbCommand, "@TRANS_SUPPLIER_NUM", DbType.String, info.TransSupplierNum);
			db.AddInParameter(dbCommand, "@RETURN_COMPANY_NUM", DbType.String, info.ReturnCompanyNum);
			db.AddInParameter(dbCommand, "@RETURN_COMPANY_NAME", DbType.String, info.ReturnCompanyName);
			db.AddInParameter(dbCommand, "@RETURN_COMPANY_ADDRESS", DbType.String, info.ReturnCompanyAddress);
			db.AddInParameter(dbCommand, "@RETURNER", DbType.String, info.Returner);
			db.AddInParameter(dbCommand, "@PHONENUM", DbType.String, info.Phonenum);
			db.AddInParameter(dbCommand, "@TRAN_TIME", DbType.DateTime, info.TranTime);
			db.AddInParameter(dbCommand, "@EXPECT_ARRIVAL_TIME", DbType.DateTime, info.ExpectArrivalTime);
			db.AddInParameter(dbCommand, "@ACTUAL_ARRIVAL_TIME", DbType.DateTime, info.ActualArrivalTime);
			db.AddInParameter(dbCommand, "@CONFIRM_FLAG", DbType.Int32, info.ConfirmFlag);
			db.AddInParameter(dbCommand, "@OPRTR", DbType.String, info.Oprtr);
			db.AddInParameter(dbCommand, "@ERP_FLAG", DbType.Int32, info.ErpFlag);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@UPDATE_USER", DbType.String, info.UpdateUser);
			db.AddInParameter(dbCommand, "@UPDATE_DATE", DbType.DateTime, info.UpdateDate);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="OUTBOUNDDELIVERYRETURN_ID">OutbounddeliveryreturnInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aOutbounddeliveryreturnId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_WMM_OUTBOUNDDELIVERYRETURN_DELETE);
		    db.AddInParameter(dbCommand, "@OUTBOUNDDELIVERYRETURN_ID", DbType.Int64, aOutbounddeliveryreturnId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}
		/// <summary>
		/// UpdateInfo
		/// </summary>
		/// <param name="OUTBOUNDDELIVERYRETURN_ID">OutbounddeliveryreturnInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aOutbounddeliveryreturnId)
		{
		    string sql = "update [LES].[TT_WMM_OUTBOUNDDELIVERYRETURN] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE 1=1  AND OUTBOUNDDELIVERYRETURN_ID =@OUTBOUNDDELIVERYRETURN_ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@OUTBOUNDDELIVERYRETURN_ID", DbType.Int64, aOutbounddeliveryreturnId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static OutbounddeliveryreturnInfo CreateOutbounddeliveryreturnInfo(IDataReader rdr)
		{
			OutbounddeliveryreturnInfo info = new OutbounddeliveryreturnInfo();
			info.OutbounddeliveryreturnId = DBConvert.GetInt64(rdr, rdr.GetOrdinal("OUTBOUNDDELIVERYRETURN_ID"));			
			info.OutbounddeliveryreturnNo = DBConvert.GetString(rdr, rdr.GetOrdinal("OUTBOUNDDELIVERYRETURN_NO"));			
			info.ReturnType = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("RETURN_TYPE"));			
			info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));			
			info.WmNo = DBConvert.GetString(rdr, rdr.GetOrdinal("WM_NO"));			
			info.ZoneNo = DBConvert.GetString(rdr, rdr.GetOrdinal("ZONE_NO"));			
			info.Dock = DBConvert.GetString(rdr, rdr.GetOrdinal("DOCK"));			
			info.SupplierName = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NAME"));			
			info.SupplierAddress = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_ADDRESS"));			
			info.TransSupplierNum = DBConvert.GetString(rdr, rdr.GetOrdinal("TRANS_SUPPLIER_NUM"));			
			info.ReturnCompanyNum = DBConvert.GetString(rdr, rdr.GetOrdinal("RETURN_COMPANY_NUM"));			
			info.ReturnCompanyName = DBConvert.GetString(rdr, rdr.GetOrdinal("RETURN_COMPANY_NAME"));			
			info.ReturnCompanyAddress = DBConvert.GetString(rdr, rdr.GetOrdinal("RETURN_COMPANY_ADDRESS"));			
			info.Returner = DBConvert.GetString(rdr, rdr.GetOrdinal("RETURNER"));			
			info.Phonenum = DBConvert.GetString(rdr, rdr.GetOrdinal("PHONENUM"));			
			info.TranTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("TRAN_TIME"));			
			info.ExpectArrivalTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("EXPECT_ARRIVAL_TIME"));			
			info.ActualArrivalTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("ACTUAL_ARRIVAL_TIME"));			
			info.ConfirmFlag = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("CONFIRM_FLAG"));			
			info.Oprtr = DBConvert.GetString(rdr, rdr.GetOrdinal("OPRTR"));			
			info.ErpFlag = DBConvert.GetInt32(rdr, rdr.GetOrdinal("ERP_FLAG"));			
			info.Comments = DBConvert.GetString(rdr, rdr.GetOrdinal("COMMENTS"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.CreateDate = DBConvert.GetDateTime(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.UpdateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("UPDATE_USER"));			
			info.UpdateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("UPDATE_DATE"));			
			info.SupplierNum = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NUM"));			
			return info;
		}
		
		#endregion
	}
}