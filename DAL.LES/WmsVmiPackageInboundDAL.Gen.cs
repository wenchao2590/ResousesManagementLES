#region Declaim
//---------------------------------------------------------------------------
// Name:		WmsVmiPackageInboundDAL
// Function: 	Expose data in table TI_IFM_WMS_VMI_PACKAGE_INBOUND from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月24日
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
    /// WmsVmiPackageInboundDAL对应表[TI_IFM_WMS_VMI_PACKAGE_INBOUND]
    /// </summary>
    public partial class WmsVmiPackageInboundDAL : BusinessObjectProvider<WmsVmiPackageInboundInfo>
	{
		#region Sql Statements
		private const string TI_IFM_WMS_VMI_PACKAGE_INBOUND_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				LOG_FID,
				WERKS,
				ORDERKEY,
				ORDERLINENUMBER,
				STORERKEY,
				SKU,
				RECQTY,
				VMIWAREHOUSECODE,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				COMMENTS				  
				FROM [LES].[TI_IFM_WMS_VMI_PACKAGE_INBOUND] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TI_IFM_WMS_VMI_PACKAGE_INBOUND_SELECT = 
			@"SELECT ID,
				FID,
				LOG_FID,
				WERKS,
				ORDERKEY,
				ORDERLINENUMBER,
				STORERKEY,
				SKU,
				RECQTY,
				VMIWAREHOUSECODE,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				COMMENTS				 
				FROM [LES].[TI_IFM_WMS_VMI_PACKAGE_INBOUND] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TI_IFM_WMS_VMI_PACKAGE_INBOUND_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TI_IFM_WMS_VMI_PACKAGE_INBOUND]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TI_IFM_WMS_VMI_PACKAGE_INBOUND_INSERT =
			@"INSERT INTO [LES].[TI_IFM_WMS_VMI_PACKAGE_INBOUND] (
				FID,
				LOG_FID,
				WERKS,
				ORDERKEY,
				ORDERLINENUMBER,
				STORERKEY,
				SKU,
				RECQTY,
				VMIWAREHOUSECODE,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				COMMENTS				 
			) VALUES (
				@FID,
				@LOG_FID,
				@WERKS,
				@ORDERKEY,
				@ORDERLINENUMBER,
				@STORERKEY,
				@SKU,
				@RECQTY,
				@VMIWAREHOUSECODE,
				@PROCESS_FLAG,
				@PROCESS_TIME,
				@VALID_FLAG,
				@CREATE_USER,
				GETDATE(),
				@MODIFY_USER,
				@MODIFY_DATE,
				@COMMENTS				 
			);SELECT @@IDENTITY;";
		private const string TI_IFM_WMS_VMI_PACKAGE_INBOUND_UPDATE =
			@"UPDATE [LES].[TI_IFM_WMS_VMI_PACKAGE_INBOUND] WITH(ROWLOCK) 
				SET FID=@FID,
				LOG_FID=@LOG_FID,
				WERKS=@WERKS,
				ORDERKEY=@ORDERKEY,
				ORDERLINENUMBER=@ORDERLINENUMBER,
				STORERKEY=@STORERKEY,
				SKU=@SKU,
				RECQTY=@RECQTY,
				VMIWAREHOUSECODE=@VMIWAREHOUSECODE,
				PROCESS_FLAG=@PROCESS_FLAG,
				PROCESS_TIME=@PROCESS_TIME,
				VALID_FLAG=@VALID_FLAG,
				CREATE_USER=@CREATE_USER,
				CREATE_DATE=@CREATE_DATE,
				MODIFY_USER=@MODIFY_USER,
				MODIFY_DATE=@MODIFY_DATE,
				COMMENTS=@COMMENTS				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TI_IFM_WMS_VMI_PACKAGE_INBOUND_DELETE =
			@"DELETE FROM [LES].[TI_IFM_WMS_VMI_PACKAGE_INBOUND] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get WmsVmiPackageInboundInfo
		/// </summary>
		/// <param name="ID">WmsVmiPackageInboundInfo Primary key </param>
		/// <returns></returns> 
		public WmsVmiPackageInboundInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_WMS_VMI_PACKAGE_INBOUND_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateWmsVmiPackageInboundInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>WmsVmiPackageInboundInfo Collection </returns>
		public List<WmsVmiPackageInboundInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TI_IFM_WMS_VMI_PACKAGE_INBOUND_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>WmsVmiPackageInboundInfo Collection </returns>
		public List<WmsVmiPackageInboundInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<WmsVmiPackageInboundInfo> list = new List<WmsVmiPackageInboundInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateWmsVmiPackageInboundInfo(dr));
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
		public List<WmsVmiPackageInboundInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TI_IFM_WMS_VMI_PACKAGE_INBOUND]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<WmsVmiPackageInboundInfo> list = new List<WmsVmiPackageInboundInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateWmsVmiPackageInboundInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TI_IFM_WMS_VMI_PACKAGE_INBOUND_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(WmsVmiPackageInboundInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_WMS_VMI_PACKAGE_INBOUND_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@LOG_FID", DbType.Guid, info.LogFid);
			db.AddInParameter(dbCommand, "@WERKS", DbType.String, info.Werks);
			db.AddInParameter(dbCommand, "@ORDERKEY", DbType.String, info.Orderkey);
			db.AddInParameter(dbCommand, "@ORDERLINENUMBER", DbType.String, info.Orderlinenumber);
			db.AddInParameter(dbCommand, "@STORERKEY", DbType.String, info.Storerkey);
			db.AddInParameter(dbCommand, "@SKU", DbType.String, info.Sku);
			db.AddInParameter(dbCommand, "@RECQTY", DbType.Decimal, info.Recqty);
			db.AddInParameter(dbCommand, "@VMIWAREHOUSECODE", DbType.String, info.Vmiwarehousecode);
			db.AddInParameter(dbCommand, "@PROCESS_FLAG", DbType.Int32, info.ProcessFlag);
			db.AddInParameter(dbCommand, "@PROCESS_TIME", DbType.DateTime, info.ProcessTime);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}
		/// <summary>
		/// GetInsertSql
		/// </summary>
		/// <param name="info"> info</param>
		public static string GetInsertSql(WmsVmiPackageInboundInfo info)
		{
			return  
			@"insert into [LES].[TI_IFM_WMS_VMI_PACKAGE_INBOUND] (
				FID,
				LOG_FID,
				WERKS,
				ORDERKEY,
				ORDERLINENUMBER,
				STORERKEY,
				SKU,
				RECQTY,
				VMIWAREHOUSECODE,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				COMMENTS				 
			) values ("+
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				(info.LogFid == null ? "NULL" : "N'" + info.LogFid.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.Werks) ? "NULL" : "N'" + info.Werks + "'") + ","+
				(string.IsNullOrEmpty(info.Orderkey) ? "NULL" : "N'" + info.Orderkey + "'") + ","+
				(string.IsNullOrEmpty(info.Orderlinenumber) ? "NULL" : "N'" + info.Orderlinenumber + "'") + ","+
				(string.IsNullOrEmpty(info.Storerkey) ? "NULL" : "N'" + info.Storerkey + "'") + ","+
				(string.IsNullOrEmpty(info.Sku) ? "NULL" : "N'" + info.Sku + "'") + ","+
				(info.Recqty == null ? "NULL" : "" + info.Recqty.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.Vmiwarehousecode) ? "NULL" : "N'" + info.Vmiwarehousecode + "'") + ","+
				(info.ProcessFlag == null ? "NULL" : "" + info.ProcessFlag.GetValueOrDefault() + "") + ","+
				(info.ProcessTime == null ? "NULL" : "N'" + info.ProcessTime.GetValueOrDefault() + "'") + ","+
				"1" + ","+		
				"N'" + info.CreateUser + "'" + ","+		
				"GETDATE()" + ","+			
				"NULL" + ","+			
				"NULL" + ","+			
				(string.IsNullOrEmpty(info.Comments) ? "NULL" : "N'" + info.Comments + "'") + ");";				}
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(WmsVmiPackageInboundInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_WMS_VMI_PACKAGE_INBOUND_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@LOG_FID", DbType.Guid, info.LogFid);
			db.AddInParameter(dbCommand, "@WERKS", DbType.String, info.Werks);
			db.AddInParameter(dbCommand, "@ORDERKEY", DbType.String, info.Orderkey);
			db.AddInParameter(dbCommand, "@ORDERLINENUMBER", DbType.String, info.Orderlinenumber);
			db.AddInParameter(dbCommand, "@STORERKEY", DbType.String, info.Storerkey);
			db.AddInParameter(dbCommand, "@SKU", DbType.String, info.Sku);
			db.AddInParameter(dbCommand, "@RECQTY", DbType.Decimal, info.Recqty);
			db.AddInParameter(dbCommand, "@VMIWAREHOUSECODE", DbType.String, info.Vmiwarehousecode);
			db.AddInParameter(dbCommand, "@PROCESS_FLAG", DbType.Int32, info.ProcessFlag);
			db.AddInParameter(dbCommand, "@PROCESS_TIME", DbType.DateTime, info.ProcessTime);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="ID">WmsVmiPackageInboundInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_WMS_VMI_PACKAGE_INBOUND_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">WmsVmiPackageInboundInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TI_IFM_WMS_VMI_PACKAGE_INBOUND] WITH(ROWLOCK) "
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
		/// <param name="ID">WmsVmiPackageInboundInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TI_IFM_WMS_VMI_PACKAGE_INBOUND] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static WmsVmiPackageInboundInfo CreateWmsVmiPackageInboundInfo(IDataReader rdr)
		{
			WmsVmiPackageInboundInfo info = new WmsVmiPackageInboundInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.LogFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("LOG_FID"));			
			info.Werks = DBConvert.GetString(rdr, rdr.GetOrdinal("WERKS"));			
			info.Orderkey = DBConvert.GetString(rdr, rdr.GetOrdinal("ORDERKEY"));			
			info.Orderlinenumber = DBConvert.GetString(rdr, rdr.GetOrdinal("ORDERLINENUMBER"));			
			info.Storerkey = DBConvert.GetString(rdr, rdr.GetOrdinal("STORERKEY"));			
			info.Sku = DBConvert.GetString(rdr, rdr.GetOrdinal("SKU"));			
			info.Recqty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("RECQTY"));			
			info.Vmiwarehousecode = DBConvert.GetString(rdr, rdr.GetOrdinal("VMIWAREHOUSECODE"));			
			info.ProcessFlag = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PROCESS_FLAG"));			
			info.ProcessTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("PROCESS_TIME"));			
			info.ValidFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.CreateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			info.Comments = DBConvert.GetString(rdr, rdr.GetOrdinal("COMMENTS"));			
			return info;
		}
		
		#endregion
	}
}
