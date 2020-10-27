#region Declaim
//---------------------------------------------------------------------------
// Name:		OperationLogDAL
// Function: 	Expose data in table TL_SYS_OPERATION_LOG from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月25日
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------
#endregion

#region Imported Namespace

using DM.SYS;
using Infrustructure.Data;
using Infrustructure.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
          
#endregion

namespace DAL.SYS 
{     
	//// <summary>
    /// OperationLogDAL对应表[TL_SYS_OPERATION_LOG]
    /// </summary>
    public partial class OperationLogDAL : BusinessObjectProvider<OperationLogInfo>
	{
		#region Sql Statements
		private const string TL_SYS_OPERATION_LOG_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				TABLE_NAME,
				OPERATION_CONTEXT,
				IP_ADDRESS,
				PAGE_URL,
				BROWSER_INFO,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				OPERATION_TYPE				  
				FROM [dbo].[TL_SYS_OPERATION_LOG] WITH(NOLOCK) WHERE [VALID_FLAG] = 1 AND ID =@ID;";
			
		private const string TL_SYS_OPERATION_LOG_SELECT = 
			@"SELECT ID,
				FID,
				TABLE_NAME,
				OPERATION_CONTEXT,
				IP_ADDRESS,
				PAGE_URL,
				BROWSER_INFO,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				OPERATION_TYPE				 
				FROM [dbo].[TL_SYS_OPERATION_LOG] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TL_SYS_OPERATION_LOG_SELECT_COUNTS = 
			@"SELECT count(*) FROM [dbo].[TL_SYS_OPERATION_LOG]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TL_SYS_OPERATION_LOG_INSERT =
			@"INSERT INTO [dbo].[TL_SYS_OPERATION_LOG] (
				FID,
				TABLE_NAME,
				OPERATION_CONTEXT,
				IP_ADDRESS,
				PAGE_URL,
				BROWSER_INFO,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				OPERATION_TYPE				 
			) VALUES (
				@FID,
				@TABLE_NAME,
				@OPERATION_CONTEXT,
				@IP_ADDRESS,
				@PAGE_URL,
				@BROWSER_INFO,
				@VALID_FLAG,
				@CREATE_USER,
				@CREATE_DATE,
				@OPERATION_TYPE				 
			);SELECT @@IDENTITY;";
		private const string TL_SYS_OPERATION_LOG_UPDATE =
			@"UPDATE [dbo].[TL_SYS_OPERATION_LOG] WITH(ROWLOCK) 
				SET FID=@FID,
				TABLE_NAME=@TABLE_NAME,
				OPERATION_CONTEXT=@OPERATION_CONTEXT,
				IP_ADDRESS=@IP_ADDRESS,
				PAGE_URL=@PAGE_URL,
				BROWSER_INFO=@BROWSER_INFO,
				VALID_FLAG=@VALID_FLAG,
				CREATE_USER=@CREATE_USER,
				CREATE_DATE=@CREATE_DATE,
				OPERATION_TYPE=@OPERATION_TYPE				 
				WHERE [VALID_FLAG] = 1 AND ID =@ID;";

		private const string TL_SYS_OPERATION_LOG_DELETE =
			@"DELETE FROM [dbo].[TL_SYS_OPERATION_LOG] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1 AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get OperationLogInfo
		/// </summary>
		/// <param name="ID">OperationLogInfo Primary key </param>
		/// <returns></returns> 
		public OperationLogInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TL_SYS_OPERATION_LOG_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateOperationLogInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>OperationLogInfo Collection </returns>
		public List<OperationLogInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TL_SYS_OPERATION_LOG_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>OperationLogInfo Collection </returns>
		public List<OperationLogInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<OperationLogInfo> list = new List<OperationLogInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateOperationLogInfo(dr));
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
		public List<OperationLogInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [dbo].[TL_SYS_OPERATION_LOG]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<OperationLogInfo> list = new List<OperationLogInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateOperationLogInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TL_SYS_OPERATION_LOG_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(OperationLogInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TL_SYS_OPERATION_LOG_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@TABLE_NAME", DbType.String, info.TableName);
			db.AddInParameter(dbCommand, "@OPERATION_CONTEXT", DbType.String, info.OperationContext);
			db.AddInParameter(dbCommand, "@IP_ADDRESS", DbType.String, info.IpAddress);
			db.AddInParameter(dbCommand, "@PAGE_URL", DbType.String, info.PageUrl);
			db.AddInParameter(dbCommand, "@BROWSER_INFO", DbType.String, info.BrowserInfo);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@OPERATION_TYPE", DbType.Int32, info.OperationType);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}		
		/// <summary>
		/// GetInsertSql
		/// </summary>
		/// <param name="info"> info</param>
		public static string GetInsertSql(OperationLogInfo info)
		{
			return  
			@"insert into [dbo].[TL_SYS_OPERATION_LOG] (
				FID,
				TABLE_NAME,
				OPERATION_CONTEXT,
				IP_ADDRESS,
				PAGE_URL,
				BROWSER_INFO,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				OPERATION_TYPE				 
			) values ("+
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.TableName) ? "NULL" : "N'" + info.TableName + "'") + ","+
				(string.IsNullOrEmpty(info.OperationContext) ? "NULL" : "N'" + info.OperationContext + "'") + ","+
				(string.IsNullOrEmpty(info.IpAddress) ? "NULL" : "N'" + info.IpAddress + "'") + ","+
				(string.IsNullOrEmpty(info.PageUrl) ? "NULL" : "N'" + info.PageUrl + "'") + ","+
				(string.IsNullOrEmpty(info.BrowserInfo) ? "NULL" : "N'" + info.BrowserInfo + "'") + ","+
				"1" + ","+		
				"N'" + info.CreateUser + "'" + ","+		
				"GETDATE()" + ","+			
				(info.OperationType == null ? "NULL" : "" + info.OperationType.GetValueOrDefault() + "") + ");";				}
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(OperationLogInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TL_SYS_OPERATION_LOG_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@TABLE_NAME", DbType.String, info.TableName);
			db.AddInParameter(dbCommand, "@OPERATION_CONTEXT", DbType.String, info.OperationContext);
			db.AddInParameter(dbCommand, "@IP_ADDRESS", DbType.String, info.IpAddress);
			db.AddInParameter(dbCommand, "@PAGE_URL", DbType.String, info.PageUrl);
			db.AddInParameter(dbCommand, "@BROWSER_INFO", DbType.String, info.BrowserInfo);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@OPERATION_TYPE", DbType.Int32, info.OperationType);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="ID">OperationLogInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TL_SYS_OPERATION_LOG_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">OperationLogInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [dbo].[TL_SYS_OPERATION_LOG] WITH(ROWLOCK) "
                + "set [VALID_FLAG] = @VALID_FLAG ,[MODIFY_USER] = @MODIFY_USER ,[MODIFY_DATE] = GETDATE() "
                + "WHERE [VALID_FLAG] = 1 AND ID =@ID;";
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
		/// <param name="ID">OperationLogInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [dbo].[TL_SYS_OPERATION_LOG] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1 AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static OperationLogInfo CreateOperationLogInfo(IDataReader rdr)
		{
			OperationLogInfo info = new OperationLogInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.TableName = DBConvert.GetString(rdr, rdr.GetOrdinal("TABLE_NAME"));			
			info.OperationContext = DBConvert.GetString(rdr, rdr.GetOrdinal("OPERATION_CONTEXT"));			
			info.IpAddress = DBConvert.GetString(rdr, rdr.GetOrdinal("IP_ADDRESS"));			
			info.PageUrl = DBConvert.GetString(rdr, rdr.GetOrdinal("PAGE_URL"));			
			info.BrowserInfo = DBConvert.GetString(rdr, rdr.GetOrdinal("BROWSER_INFO"));			
			info.ValidFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.CreateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.OperationType = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("OPERATION_TYPE"));			
			return info;
		}
		
		#endregion
	}
}
