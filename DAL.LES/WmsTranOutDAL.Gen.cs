#region Declaim
//---------------------------------------------------------------------------
// Name:		WmsTranOutDAL
// Function: 	Expose data in table TI_IFM_WMS_TRAN_OUT from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018-07-20
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
    /// WmsTranOutDAL对应表[TI_IFM_WMS_TRAN_OUT]
    /// </summary>
    public partial class WmsTranOutDAL : BusinessObjectProvider<WmsTranOutInfo>
	{
		#region Sql Statements
		private const string TI_IFM_WMS_TRAN_OUT_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				SOURCE_ORDER_CODE,
				SOURCE_ORDER_TYPE,
				PART_NO,
				SUPPLIER_NUM,
				SUPPLIER_NAME,
				DELIVERY_QTY,
				PROCESS_FLAG,
				PROCESS_TIME,
				LOG_FID,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER,
				RUNSHEET_NO,
				WM_NO,
				PLANT,
				ITEM_NUMBER				  
				FROM [LES].[TI_IFM_WMS_TRAN_OUT] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TI_IFM_WMS_TRAN_OUT_SELECT = 
			@"SELECT ID,
				FID,
				SOURCE_ORDER_CODE,
				SOURCE_ORDER_TYPE,
				PART_NO,
				SUPPLIER_NUM,
				SUPPLIER_NAME,
				DELIVERY_QTY,
				PROCESS_FLAG,
				PROCESS_TIME,
				LOG_FID,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER,
				RUNSHEET_NO,
				WM_NO,
				PLANT,
				ITEM_NUMBER				 
				FROM [LES].[TI_IFM_WMS_TRAN_OUT] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TI_IFM_WMS_TRAN_OUT_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TI_IFM_WMS_TRAN_OUT]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TI_IFM_WMS_TRAN_OUT_INSERT =
			@"INSERT INTO [LES].[TI_IFM_WMS_TRAN_OUT] (
				FID,
				SOURCE_ORDER_CODE,
				SOURCE_ORDER_TYPE,
				PART_NO,
				SUPPLIER_NUM,
				SUPPLIER_NAME,
				DELIVERY_QTY,
				PROCESS_FLAG,
				PROCESS_TIME,
				LOG_FID,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER,
				RUNSHEET_NO,
				WM_NO,
				PLANT,
				ITEM_NUMBER				 
			) VALUES (
				@FID,
				@SOURCE_ORDER_CODE,
				@SOURCE_ORDER_TYPE,
				@PART_NO,
				@SUPPLIER_NUM,
				@SUPPLIER_NAME,
				@DELIVERY_QTY,
				@PROCESS_FLAG,
				@PROCESS_TIME,
				@LOG_FID,
				@VALID_FLAG,
				GETDATE(),
				@CREATE_USER,
				@MODIFY_DATE,
				@MODIFY_USER,
				@RUNSHEET_NO,
				@WM_NO,
				@PLANT,
				@ITEM_NUMBER				 
			);SELECT @@IDENTITY;";
		private const string TI_IFM_WMS_TRAN_OUT_UPDATE =
			@"UPDATE [LES].[TI_IFM_WMS_TRAN_OUT] WITH(ROWLOCK) 
				SET FID=@FID,
				SOURCE_ORDER_CODE=@SOURCE_ORDER_CODE,
				SOURCE_ORDER_TYPE=@SOURCE_ORDER_TYPE,
				PART_NO=@PART_NO,
				SUPPLIER_NUM=@SUPPLIER_NUM,
				SUPPLIER_NAME=@SUPPLIER_NAME,
				DELIVERY_QTY=@DELIVERY_QTY,
				PROCESS_FLAG=@PROCESS_FLAG,
				PROCESS_TIME=@PROCESS_TIME,
				LOG_FID=@LOG_FID,
				VALID_FLAG=@VALID_FLAG,
				CREATE_DATE=@CREATE_DATE,
				CREATE_USER=@CREATE_USER,
				MODIFY_DATE=@MODIFY_DATE,
				MODIFY_USER=@MODIFY_USER,
				RUNSHEET_NO=@RUNSHEET_NO,
				WM_NO=@WM_NO,
				PLANT=@PLANT,
				ITEM_NUMBER=@ITEM_NUMBER				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TI_IFM_WMS_TRAN_OUT_DELETE =
			@"DELETE FROM [LES].[TI_IFM_WMS_TRAN_OUT] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get WmsTranOutInfo
		/// </summary>
		/// <param name="ID">WmsTranOutInfo Primary key </param>
		/// <returns></returns> 
		public WmsTranOutInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_WMS_TRAN_OUT_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateWmsTranOutInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>WmsTranOutInfo Collection </returns>
		public List<WmsTranOutInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TI_IFM_WMS_TRAN_OUT_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>WmsTranOutInfo Collection </returns>
		public List<WmsTranOutInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<WmsTranOutInfo> list = new List<WmsTranOutInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateWmsTranOutInfo(dr));
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
		public List<WmsTranOutInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
		{
		    if (pageIndex <= 0) pageIndex = 1;
            if (pageRow <= 0) pageRow = 10;
			string whereText = string.Empty;
            if (!string.IsNullOrEmpty(textWhere))
            {
                if (textWhere.Trim().StartsWith("and", StringComparison.OrdinalIgnoreCase))
                    whereText += " where [VALID_FLAG] = 1 " + textWhere;
                else
                    whereText += " where " + textWhere + " and [VALID_FLAG] = 1";
            }
			else
                whereText += " where [VALID_FLAG] = 1 ";
            if (string.IsNullOrEmpty(textOrder))
                textOrder += "[ID] desc";
            string sql = "select top " + pageRow + " * from "
                + "(select row_number() over(order by " + textOrder + ") as rownumber"
                + ",* from [LES].[TI_IFM_WMS_TRAN_OUT]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<WmsTranOutInfo> list = new List<WmsTranOutInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateWmsTranOutInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TI_IFM_WMS_TRAN_OUT_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(WmsTranOutInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_WMS_TRAN_OUT_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@SOURCE_ORDER_CODE", DbType.String, info.SourceOrderCode);
			db.AddInParameter(dbCommand, "@SOURCE_ORDER_TYPE", DbType.Int32, info.SourceOrderType);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@SUPPLIER_NAME", DbType.String, info.SupplierName);
			db.AddInParameter(dbCommand, "@DELIVERY_QTY", DbType.Decimal, info.DeliveryQty);
			db.AddInParameter(dbCommand, "@PROCESS_FLAG", DbType.Int32, info.ProcessFlag);
			db.AddInParameter(dbCommand, "@PROCESS_TIME", DbType.DateTime, info.ProcessTime);
			db.AddInParameter(dbCommand, "@LOG_FID", DbType.Guid, info.LogFid);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@RUNSHEET_NO", DbType.String, info.RunsheetNo);
			db.AddInParameter(dbCommand, "@WM_NO", DbType.String, info.WmNo);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@ITEM_NUMBER", DbType.String, info.ItemNumber);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}
		/// <summary>
		/// GetInsertSql
		/// </summary>
		/// <param name="info"> info</param>
		public static string GetInsertSql(WmsTranOutInfo info)
		{
			return  
			@"insert into [LES].[TI_IFM_WMS_TRAN_OUT] (
				FID,
				SOURCE_ORDER_CODE,
				SOURCE_ORDER_TYPE,
				PART_NO,
				SUPPLIER_NUM,
				SUPPLIER_NAME,
				DELIVERY_QTY,
				PROCESS_FLAG,
				PROCESS_TIME,
				LOG_FID,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER,
				RUNSHEET_NO,
				WM_NO,
				PLANT,
				ITEM_NUMBER				 
			) values ("+
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.SourceOrderCode) ? "NULL" : "N'" + info.SourceOrderCode + "'") + ","+
				(info.SourceOrderType == null ? "NULL" : "" + info.SourceOrderType.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.PartNo) ? "NULL" : "N'" + info.PartNo + "'") + ","+
				(string.IsNullOrEmpty(info.SupplierNum) ? "NULL" : "N'" + info.SupplierNum + "'") + ","+
				(string.IsNullOrEmpty(info.SupplierName) ? "NULL" : "N'" + info.SupplierName + "'") + ","+
				(info.DeliveryQty == null ? "NULL" : "" + info.DeliveryQty.GetValueOrDefault() + "") + ","+
				(info.ProcessFlag == null ? "NULL" : "" + info.ProcessFlag.GetValueOrDefault() + "") + ","+
				(info.ProcessTime == null ? "NULL" : "N'" + info.ProcessTime.GetValueOrDefault() + "'") + ","+
				(info.LogFid == null ? "NULL" : "N'" + info.LogFid.GetValueOrDefault() + "'") + ","+
				"1" + ","+		
				"GETDATE()" + ","+			
				"N'" + info.CreateUser + "'" + ","+		
				"NULL" + ","+			
				"NULL" + ","+			
				(string.IsNullOrEmpty(info.RunsheetNo) ? "NULL" : "N'" + info.RunsheetNo + "'") + ","+
				(string.IsNullOrEmpty(info.WmNo) ? "NULL" : "N'" + info.WmNo + "'") + ","+
				(string.IsNullOrEmpty(info.Plant) ? "NULL" : "N'" + info.Plant + "'") + ","+
				(string.IsNullOrEmpty(info.ItemNumber) ? "NULL" : "N'" + info.ItemNumber + "'") + ");";				}
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(WmsTranOutInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_WMS_TRAN_OUT_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@SOURCE_ORDER_CODE", DbType.String, info.SourceOrderCode);
			db.AddInParameter(dbCommand, "@SOURCE_ORDER_TYPE", DbType.Int32, info.SourceOrderType);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@SUPPLIER_NAME", DbType.String, info.SupplierName);
			db.AddInParameter(dbCommand, "@DELIVERY_QTY", DbType.Decimal, info.DeliveryQty);
			db.AddInParameter(dbCommand, "@PROCESS_FLAG", DbType.Int32, info.ProcessFlag);
			db.AddInParameter(dbCommand, "@PROCESS_TIME", DbType.DateTime, info.ProcessTime);
			db.AddInParameter(dbCommand, "@LOG_FID", DbType.Guid, info.LogFid);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@RUNSHEET_NO", DbType.String, info.RunsheetNo);
			db.AddInParameter(dbCommand, "@WM_NO", DbType.String, info.WmNo);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@ITEM_NUMBER", DbType.String, info.ItemNumber);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="ID">WmsTranOutInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_WMS_TRAN_OUT_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">WmsTranOutInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TI_IFM_WMS_TRAN_OUT] WITH(ROWLOCK) "
                + "set [VALID_FLAG] = @VALID_FLAG ,[MODIFY_USER] = @MODIFY_USER ,[MODIFY_DATE] = GETDATE() "
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
 			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
                   db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, false);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.AnsiString, loginUser);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}
		/// <summary>
		/// UpdateInfo
		/// </summary>
		/// <param name="ID">WmsTranOutInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TI_IFM_WMS_TRAN_OUT] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static WmsTranOutInfo CreateWmsTranOutInfo(IDataReader rdr)
		{
			WmsTranOutInfo info = new WmsTranOutInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.SourceOrderCode = DBConvert.GetString(rdr, rdr.GetOrdinal("SOURCE_ORDER_CODE"));			
			info.SourceOrderType = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("SOURCE_ORDER_TYPE"));			
			info.PartNo = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_NO"));			
			info.SupplierNum = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NUM"));			
			info.SupplierName = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NAME"));			
			info.DeliveryQty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("DELIVERY_QTY"));			
			info.ProcessFlag = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PROCESS_FLAG"));			
			info.ProcessTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("PROCESS_TIME"));			
			info.LogFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("LOG_FID"));			
			info.ValidFlag = DBConvert.GetBool(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			info.CreateDate = DBConvert.GetDateTime(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			info.RunsheetNo = DBConvert.GetString(rdr, rdr.GetOrdinal("RUNSHEET_NO"));			
			info.WmNo = DBConvert.GetString(rdr, rdr.GetOrdinal("WM_NO"));			
			info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));			
			info.ItemNumber = DBConvert.GetString(rdr, rdr.GetOrdinal("ITEM_NUMBER"));			
			return info;
		}
		
		#endregion
	}
}
