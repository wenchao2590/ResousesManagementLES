#region Declaim
//---------------------------------------------------------------------------
// Name:		ReportColumnDAL
// Function: 	Expose data in table TS_SYS_REPORT_COLUMN from database as business object to MES system.
// Tool:		T4
// CreateDate:	2020年3月9日
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
    /// ReportColumnDAL对应表[TS_SYS_REPORT_COLUMN]
    /// </summary>
    public partial class ReportColumnDAL : BusinessObjectProvider<ReportColumnInfo>
	{
		#region Sql Statements
		private const string TS_SYS_REPORT_COLUMN_SELECT_BY_ID =
			@"SELECT FIELD_NAME,
				DISPLAY_NAME_CN,
				DISPLAY_NAME_EN,
				FIELD_TYPE,
				DISPLAY_ORDER,
				DISPLAY_WIDTH,
				PID,
				CREATE_DATE,
				Id,
				VALID_FLAG,
				MODIFY_USER,
				FID,
				CREATE_USER,
				MODIFY_DATE				  
				FROM [dbo].[TS_SYS_REPORT_COLUMN] WITH(NOLOCK) WHERE [VALID_FLAG] = 1 AND Id =@Id;";
			
		private const string TS_SYS_REPORT_COLUMN_SELECT = 
			@"SELECT FIELD_NAME,
				DISPLAY_NAME_CN,
				DISPLAY_NAME_EN,
				FIELD_TYPE,
				DISPLAY_ORDER,
				DISPLAY_WIDTH,
				PID,
				CREATE_DATE,
				Id,
				VALID_FLAG,
				MODIFY_USER,
				FID,
				CREATE_USER,
				MODIFY_DATE				 
				FROM [dbo].[TS_SYS_REPORT_COLUMN] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TS_SYS_REPORT_COLUMN_SELECT_COUNTS = 
			@"SELECT count(*) FROM [dbo].[TS_SYS_REPORT_COLUMN]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TS_SYS_REPORT_COLUMN_INSERT =
			@"INSERT INTO [dbo].[TS_SYS_REPORT_COLUMN] (
				FIELD_NAME,
				DISPLAY_NAME_CN,
				DISPLAY_NAME_EN,
				FIELD_TYPE,
				DISPLAY_ORDER,
				DISPLAY_WIDTH,
				PID,
				CREATE_DATE,
				VALID_FLAG,
				MODIFY_USER,
				FID,
				CREATE_USER,
				MODIFY_DATE				 
			) VALUES (
				@FIELD_NAME,
				@DISPLAY_NAME_CN,
				@DISPLAY_NAME_EN,
				@FIELD_TYPE,
				@DISPLAY_ORDER,
				@DISPLAY_WIDTH,
				@PID,
				@CREATE_DATE,
				@VALID_FLAG,
				@MODIFY_USER,
				@FID,
				@CREATE_USER,
				@MODIFY_DATE				 
			);SELECT @@IDENTITY;";
		private const string TS_SYS_REPORT_COLUMN_UPDATE =
			@"UPDATE [dbo].[TS_SYS_REPORT_COLUMN] WITH(ROWLOCK) 
				SET FIELD_NAME=@FIELD_NAME,
				DISPLAY_NAME_CN=@DISPLAY_NAME_CN,
				DISPLAY_NAME_EN=@DISPLAY_NAME_EN,
				FIELD_TYPE=@FIELD_TYPE,
				DISPLAY_ORDER=@DISPLAY_ORDER,
				DISPLAY_WIDTH=@DISPLAY_WIDTH,
				PID=@PID,
				CREATE_DATE=@CREATE_DATE,
				VALID_FLAG=@VALID_FLAG,
				MODIFY_USER=@MODIFY_USER,
				FID=@FID,
				CREATE_USER=@CREATE_USER,
				MODIFY_DATE=@MODIFY_DATE				 
				WHERE [VALID_FLAG] = 1 AND Id =@Id;";

		private const string TS_SYS_REPORT_COLUMN_DELETE =
			@"DELETE FROM [dbo].[TS_SYS_REPORT_COLUMN] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1 AND Id =@Id;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get ReportColumnInfo
		/// </summary>
		/// <param name="Id">ReportColumnInfo Primary key </param>
		/// <returns></returns> 
		public ReportColumnInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_REPORT_COLUMN_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@Id", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateReportColumnInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>ReportColumnInfo Collection </returns>
		public List<ReportColumnInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TS_SYS_REPORT_COLUMN_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>ReportColumnInfo Collection </returns>
		public List<ReportColumnInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<ReportColumnInfo> list = new List<ReportColumnInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateReportColumnInfo(dr));
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
		public List<ReportColumnInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                textOrder += "[Id] desc";
            string sql = "select top " + pageRow + " * from "
                + "(select row_number() over(order by " + textOrder + ") as rownumber"
                + ",* from [dbo].[TS_SYS_REPORT_COLUMN]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<ReportColumnInfo> list = new List<ReportColumnInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateReportColumnInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TS_SYS_REPORT_COLUMN_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(ReportColumnInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_REPORT_COLUMN_INSERT);			
			db.AddInParameter(dbCommand, "@FIELD_NAME", DbType.String, info.FieldName);
			db.AddInParameter(dbCommand, "@DISPLAY_NAME_CN", DbType.String, info.DisplayNameCn);
			db.AddInParameter(dbCommand, "@DISPLAY_NAME_EN", DbType.String, info.DisplayNameEn);
			db.AddInParameter(dbCommand, "@FIELD_TYPE", DbType.Int32, info.FieldType);
			db.AddInParameter(dbCommand, "@DISPLAY_ORDER", DbType.Int32, info.DisplayOrder);
			db.AddInParameter(dbCommand, "@DISPLAY_WIDTH", DbType.Int32, info.DisplayWidth);
			db.AddInParameter(dbCommand, "@PID", DbType.Guid, info.Pid);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}		
		/// <summary>
		/// GetInsertSql
		/// </summary>
		/// <param name="info"> info</param>
		public static string GetInsertSql(ReportColumnInfo info)
		{
			return  
			@"insert into [dbo].[TS_SYS_REPORT_COLUMN] (
				FIELD_NAME,
				DISPLAY_NAME_CN,
				DISPLAY_NAME_EN,
				FIELD_TYPE,
				DISPLAY_ORDER,
				DISPLAY_WIDTH,
				PID,
				CREATE_DATE,
				VALID_FLAG,
				MODIFY_USER,
				FID,
				CREATE_USER,
				MODIFY_DATE				 
			) values ("+
				(string.IsNullOrEmpty(info.FieldName) ? "NULL" : "N'" + info.FieldName + "'") + ","+
				(string.IsNullOrEmpty(info.DisplayNameCn) ? "NULL" : "N'" + info.DisplayNameCn + "'") + ","+
				(string.IsNullOrEmpty(info.DisplayNameEn) ? "NULL" : "N'" + info.DisplayNameEn + "'") + ","+
				(info.FieldType == null ? "NULL" : "" + info.FieldType.GetValueOrDefault() + "") + ","+
				(info.DisplayOrder == null ? "NULL" : "" + info.DisplayOrder.GetValueOrDefault() + "") + ","+
				(info.DisplayWidth == null ? "NULL" : "" + info.DisplayWidth.GetValueOrDefault() + "") + ","+
				(info.Pid == null ? "NULL" : "N'" + info.Pid.GetValueOrDefault() + "'") + ","+
				"GETDATE()" + ","+			
				"1" + ","+		
				"NULL" + ","+			
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				"N'" + info.CreateUser + "'" + ","+		
				"NULL" + ");";			
				}
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(ReportColumnInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_REPORT_COLUMN_UPDATE);				
			db.AddInParameter(dbCommand, "@FIELD_NAME", DbType.String, info.FieldName);
			db.AddInParameter(dbCommand, "@DISPLAY_NAME_CN", DbType.String, info.DisplayNameCn);
			db.AddInParameter(dbCommand, "@DISPLAY_NAME_EN", DbType.String, info.DisplayNameEn);
			db.AddInParameter(dbCommand, "@FIELD_TYPE", DbType.Int32, info.FieldType);
			db.AddInParameter(dbCommand, "@DISPLAY_ORDER", DbType.Int32, info.DisplayOrder);
			db.AddInParameter(dbCommand, "@DISPLAY_WIDTH", DbType.Int32, info.DisplayWidth);
			db.AddInParameter(dbCommand, "@PID", DbType.Guid, info.Pid);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@Id", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="Id">ReportColumnInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_REPORT_COLUMN_DELETE);
		    db.AddInParameter(dbCommand, "@Id", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="Id">ReportColumnInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [dbo].[TS_SYS_REPORT_COLUMN] WITH(ROWLOCK) "
                + "set [VALID_FLAG] = @VALID_FLAG ,[MODIFY_USER] = @MODIFY_USER ,[MODIFY_DATE] = GETDATE() "
                + "WHERE [VALID_FLAG] = 1 AND Id =@Id;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
         			db.AddInParameter(dbCommand, "@Id", DbType.Int64, aId);
     db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, false);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.AnsiString, loginUser);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}
		/// <summary>
		/// UpdateInfo
		/// </summary>
		/// <param name="Id">ReportColumnInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [dbo].[TS_SYS_REPORT_COLUMN] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1 AND Id =@Id;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@Id", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static ReportColumnInfo CreateReportColumnInfo(IDataReader rdr)
		{
			ReportColumnInfo info = new ReportColumnInfo();
			info.FieldName = DBConvert.GetString(rdr, rdr.GetOrdinal("FIELD_NAME"));			
			info.DisplayNameCn = DBConvert.GetString(rdr, rdr.GetOrdinal("DISPLAY_NAME_CN"));			
			info.DisplayNameEn = DBConvert.GetString(rdr, rdr.GetOrdinal("DISPLAY_NAME_EN"));			
			info.FieldType = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("FIELD_TYPE"));			
			info.DisplayOrder = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("DISPLAY_ORDER"));			
			info.DisplayWidth = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("DISPLAY_WIDTH"));			
			info.Pid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("PID"));			
			info.CreateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("Id"));			
			info.ValidFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			return info;
		}
		
		#endregion
	}
}
