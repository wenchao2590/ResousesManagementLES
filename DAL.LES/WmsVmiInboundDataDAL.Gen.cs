#region Declaim
//---------------------------------------------------------------------------
// Name:		WmsVmiInboundDataDAL
// Function: 	Expose data in table TI_IFM_WMS_VMI_INBOUND_DATA from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月5日
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
    /// WmsVmiInboundDataDAL对应表[TI_IFM_WMS_VMI_INBOUND_DATA]
    /// </summary>
    public partial class WmsVmiInboundDataDAL : BusinessObjectProvider<WmsVmiInboundDataInfo>
	{
		#region Sql Statements
		private const string TI_IFM_WMS_VMI_INBOUND_DATA_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				LOG_FID,
				WERKS,
				SOURCE_ORDER_CODE,
				SOURCE_ORDER_TYPE,
				PART_NO,
				SUPPLIER_NUM,
				SUPPLIER_NAME,
				DELIVERY_QTY,
				WMSSOURCEKEY,
				WMSLINENUMBER,
				VMIWAREHOUSECODE,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER,
				COMMENTS				  
				FROM [LES].[TI_IFM_WMS_VMI_INBOUND_DATA] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TI_IFM_WMS_VMI_INBOUND_DATA_SELECT = 
			@"SELECT ID,
				FID,
				LOG_FID,
				WERKS,
				SOURCE_ORDER_CODE,
				SOURCE_ORDER_TYPE,
				PART_NO,
				SUPPLIER_NUM,
				SUPPLIER_NAME,
				DELIVERY_QTY,
				WMSSOURCEKEY,
				WMSLINENUMBER,
				VMIWAREHOUSECODE,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER,
				COMMENTS				 
				FROM [LES].[TI_IFM_WMS_VMI_INBOUND_DATA] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TI_IFM_WMS_VMI_INBOUND_DATA_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TI_IFM_WMS_VMI_INBOUND_DATA]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TI_IFM_WMS_VMI_INBOUND_DATA_INSERT =
			@"INSERT INTO [LES].[TI_IFM_WMS_VMI_INBOUND_DATA] (
				FID,
				LOG_FID,
				WERKS,
				SOURCE_ORDER_CODE,
				SOURCE_ORDER_TYPE,
				PART_NO,
				SUPPLIER_NUM,
				SUPPLIER_NAME,
				DELIVERY_QTY,
				WMSSOURCEKEY,
				WMSLINENUMBER,
				VMIWAREHOUSECODE,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER,
				COMMENTS				 
			) VALUES (
				@FID,
				@LOG_FID,
				@WERKS,
				@SOURCE_ORDER_CODE,
				@SOURCE_ORDER_TYPE,
				@PART_NO,
				@SUPPLIER_NUM,
				@SUPPLIER_NAME,
				@DELIVERY_QTY,
				@WMSSOURCEKEY,
				@WMSLINENUMBER,
				@VMIWAREHOUSECODE,
				@PROCESS_FLAG,
				@PROCESS_TIME,
				@VALID_FLAG,
				GETDATE(),
				@CREATE_USER,
				@MODIFY_DATE,
				@MODIFY_USER,
				@COMMENTS				 
			);SELECT @@IDENTITY;";
		private const string TI_IFM_WMS_VMI_INBOUND_DATA_UPDATE =
			@"UPDATE [LES].[TI_IFM_WMS_VMI_INBOUND_DATA] WITH(ROWLOCK) 
				SET FID=@FID,
				LOG_FID=@LOG_FID,
				WERKS=@WERKS,
				SOURCE_ORDER_CODE=@SOURCE_ORDER_CODE,
				SOURCE_ORDER_TYPE=@SOURCE_ORDER_TYPE,
				PART_NO=@PART_NO,
				SUPPLIER_NUM=@SUPPLIER_NUM,
				SUPPLIER_NAME=@SUPPLIER_NAME,
				DELIVERY_QTY=@DELIVERY_QTY,
				WMSSOURCEKEY=@WMSSOURCEKEY,
				WMSLINENUMBER=@WMSLINENUMBER,
				VMIWAREHOUSECODE=@VMIWAREHOUSECODE,
				PROCESS_FLAG=@PROCESS_FLAG,
				PROCESS_TIME=@PROCESS_TIME,
				VALID_FLAG=@VALID_FLAG,
				CREATE_DATE=@CREATE_DATE,
				CREATE_USER=@CREATE_USER,
				MODIFY_DATE=@MODIFY_DATE,
				MODIFY_USER=@MODIFY_USER,
				COMMENTS=@COMMENTS				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TI_IFM_WMS_VMI_INBOUND_DATA_DELETE =
			@"DELETE FROM [LES].[TI_IFM_WMS_VMI_INBOUND_DATA] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get WmsVmiInboundDataInfo
		/// </summary>
		/// <param name="ID">WmsVmiInboundDataInfo Primary key </param>
		/// <returns></returns> 
		public WmsVmiInboundDataInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_WMS_VMI_INBOUND_DATA_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateWmsVmiInboundDataInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>WmsVmiInboundDataInfo Collection </returns>
		public List<WmsVmiInboundDataInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TI_IFM_WMS_VMI_INBOUND_DATA_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>WmsVmiInboundDataInfo Collection </returns>
		public List<WmsVmiInboundDataInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<WmsVmiInboundDataInfo> list = new List<WmsVmiInboundDataInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateWmsVmiInboundDataInfo(dr));
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
		public List<WmsVmiInboundDataInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TI_IFM_WMS_VMI_INBOUND_DATA]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<WmsVmiInboundDataInfo> list = new List<WmsVmiInboundDataInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateWmsVmiInboundDataInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TI_IFM_WMS_VMI_INBOUND_DATA_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(WmsVmiInboundDataInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_WMS_VMI_INBOUND_DATA_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@LOG_FID", DbType.Guid, info.LogFid);
			db.AddInParameter(dbCommand, "@WERKS", DbType.String, info.Werks);
			db.AddInParameter(dbCommand, "@SOURCE_ORDER_CODE", DbType.String, info.SourceOrderCode);
			db.AddInParameter(dbCommand, "@SOURCE_ORDER_TYPE", DbType.Int32, info.SourceOrderType);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@SUPPLIER_NAME", DbType.String, info.SupplierName);
			db.AddInParameter(dbCommand, "@DELIVERY_QTY", DbType.Decimal, info.DeliveryQty);
			db.AddInParameter(dbCommand, "@WMSSOURCEKEY", DbType.String, info.Wmssourcekey);
			db.AddInParameter(dbCommand, "@WMSLINENUMBER", DbType.String, info.Wmslinenumber);
			db.AddInParameter(dbCommand, "@VMIWAREHOUSECODE", DbType.String, info.Vmiwarehousecode);
			db.AddInParameter(dbCommand, "@PROCESS_FLAG", DbType.Int32, info.ProcessFlag);
			db.AddInParameter(dbCommand, "@PROCESS_TIME", DbType.DateTime, info.ProcessTime);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}
		/// <summary>
		/// GetInsertSql
		/// </summary>
		/// <param name="info"> info</param>
		public static string GetInsertSql(WmsVmiInboundDataInfo info)
		{
			return  
			@"insert into [LES].[TI_IFM_WMS_VMI_INBOUND_DATA] (
				FID,
				LOG_FID,
				WERKS,
				SOURCE_ORDER_CODE,
				SOURCE_ORDER_TYPE,
				PART_NO,
				SUPPLIER_NUM,
				SUPPLIER_NAME,
				DELIVERY_QTY,
				WMSSOURCEKEY,
				WMSLINENUMBER,
				VMIWAREHOUSECODE,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER,
				COMMENTS				 
			) values ("+
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				(info.LogFid == null ? "NULL" : "N'" + info.LogFid.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.Werks) ? "NULL" : "N'" + info.Werks + "'") + ","+
				(string.IsNullOrEmpty(info.SourceOrderCode) ? "NULL" : "N'" + info.SourceOrderCode + "'") + ","+
				(info.SourceOrderType == null ? "NULL" : "" + info.SourceOrderType.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.PartNo) ? "NULL" : "N'" + info.PartNo + "'") + ","+
				(string.IsNullOrEmpty(info.SupplierNum) ? "NULL" : "N'" + info.SupplierNum + "'") + ","+
				(string.IsNullOrEmpty(info.SupplierName) ? "NULL" : "N'" + info.SupplierName + "'") + ","+
				(info.DeliveryQty == null ? "NULL" : "" + info.DeliveryQty.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.Wmssourcekey) ? "NULL" : "N'" + info.Wmssourcekey + "'") + ","+
				(string.IsNullOrEmpty(info.Wmslinenumber) ? "NULL" : "N'" + info.Wmslinenumber + "'") + ","+
				(string.IsNullOrEmpty(info.Vmiwarehousecode) ? "NULL" : "N'" + info.Vmiwarehousecode + "'") + ","+
				(info.ProcessFlag == null ? "NULL" : "" + info.ProcessFlag.GetValueOrDefault() + "") + ","+
				(info.ProcessTime == null ? "NULL" : "N'" + info.ProcessTime.GetValueOrDefault() + "'") + ","+
				"1" + ","+		
				"GETDATE()" + ","+			
				"N'" + info.CreateUser + "'" + ","+		
				"NULL" + ","+			
				"NULL" + ","+			
				(string.IsNullOrEmpty(info.Comments) ? "NULL" : "N'" + info.Comments + "'") + ");";				}
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(WmsVmiInboundDataInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_WMS_VMI_INBOUND_DATA_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@LOG_FID", DbType.Guid, info.LogFid);
			db.AddInParameter(dbCommand, "@WERKS", DbType.String, info.Werks);
			db.AddInParameter(dbCommand, "@SOURCE_ORDER_CODE", DbType.String, info.SourceOrderCode);
			db.AddInParameter(dbCommand, "@SOURCE_ORDER_TYPE", DbType.Int32, info.SourceOrderType);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@SUPPLIER_NAME", DbType.String, info.SupplierName);
			db.AddInParameter(dbCommand, "@DELIVERY_QTY", DbType.Decimal, info.DeliveryQty);
			db.AddInParameter(dbCommand, "@WMSSOURCEKEY", DbType.String, info.Wmssourcekey);
			db.AddInParameter(dbCommand, "@WMSLINENUMBER", DbType.String, info.Wmslinenumber);
			db.AddInParameter(dbCommand, "@VMIWAREHOUSECODE", DbType.String, info.Vmiwarehousecode);
			db.AddInParameter(dbCommand, "@PROCESS_FLAG", DbType.Int32, info.ProcessFlag);
			db.AddInParameter(dbCommand, "@PROCESS_TIME", DbType.DateTime, info.ProcessTime);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="ID">WmsVmiInboundDataInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_WMS_VMI_INBOUND_DATA_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">WmsVmiInboundDataInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TI_IFM_WMS_VMI_INBOUND_DATA] WITH(ROWLOCK) "
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
		/// <param name="ID">WmsVmiInboundDataInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TI_IFM_WMS_VMI_INBOUND_DATA] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static WmsVmiInboundDataInfo CreateWmsVmiInboundDataInfo(IDataReader rdr)
		{
			WmsVmiInboundDataInfo info = new WmsVmiInboundDataInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.LogFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("LOG_FID"));			
			info.Werks = DBConvert.GetString(rdr, rdr.GetOrdinal("WERKS"));			
			info.SourceOrderCode = DBConvert.GetString(rdr, rdr.GetOrdinal("SOURCE_ORDER_CODE"));			
			info.SourceOrderType = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("SOURCE_ORDER_TYPE"));			
			info.PartNo = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_NO"));			
			info.SupplierNum = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NUM"));			
			info.SupplierName = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NAME"));			
			info.DeliveryQty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("DELIVERY_QTY"));			
			info.Wmssourcekey = DBConvert.GetString(rdr, rdr.GetOrdinal("WMSSOURCEKEY"));			
			info.Wmslinenumber = DBConvert.GetString(rdr, rdr.GetOrdinal("WMSLINENUMBER"));			
			info.Vmiwarehousecode = DBConvert.GetString(rdr, rdr.GetOrdinal("VMIWAREHOUSECODE"));			
			info.ProcessFlag = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PROCESS_FLAG"));			
			info.ProcessTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("PROCESS_TIME"));			
			info.ValidFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			info.CreateDate = DBConvert.GetDateTime(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			info.Comments = DBConvert.GetString(rdr, rdr.GetOrdinal("COMMENTS"));			
			return info;
		}
		
		#endregion
	}
}
