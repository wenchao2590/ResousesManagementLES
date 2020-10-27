#region Declaim
//---------------------------------------------------------------------------
// Name:		CodeDAL
// Function: 	Expose data in table TS_SYS_CODE from database as business object to MES system.
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
    /// CodeDAL对应表[TS_SYS_CODE]
    /// </summary>
    public partial class CodeDAL : BusinessObjectProvider<CodeInfo>
	{
		#region Sql Statements
		private const string TS_SYS_CODE_SELECT_BY_ID =
			@"SELECT CODE_NAME,
				CODE_NAME_CN,
				COMMENTS,
				VALID_FLAG,
				MODIFY_USER,
				ID,
				CREATE_USER,
				MODIFY_DATE,
				FID,
				CREATE_DATE				  
				FROM [dbo].[TS_SYS_CODE] WITH(NOLOCK) WHERE [VALID_FLAG] = 1 AND ID =@ID;";
			
		private const string TS_SYS_CODE_SELECT = 
			@"SELECT CODE_NAME,
				CODE_NAME_CN,
				COMMENTS,
				VALID_FLAG,
				MODIFY_USER,
				ID,
				CREATE_USER,
				MODIFY_DATE,
				FID,
				CREATE_DATE				 
				FROM [dbo].[TS_SYS_CODE] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TS_SYS_CODE_SELECT_COUNTS = 
			@"SELECT count(*) FROM [dbo].[TS_SYS_CODE]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TS_SYS_CODE_INSERT =
			@"INSERT INTO [dbo].[TS_SYS_CODE] (
				CODE_NAME,
				CODE_NAME_CN,
				COMMENTS,
				VALID_FLAG,
				MODIFY_USER,
				CREATE_USER,
				MODIFY_DATE,
				FID,
				CREATE_DATE				 
			) VALUES (
				@CODE_NAME,
				@CODE_NAME_CN,
				@COMMENTS,
				@VALID_FLAG,
				@MODIFY_USER,
				@CREATE_USER,
				@MODIFY_DATE,
				@FID,
				@CREATE_DATE				 
			);SELECT @@IDENTITY;";
		private const string TS_SYS_CODE_UPDATE =
			@"UPDATE [dbo].[TS_SYS_CODE] WITH(ROWLOCK) 
				SET CODE_NAME=@CODE_NAME,
				CODE_NAME_CN=@CODE_NAME_CN,
				COMMENTS=@COMMENTS,
				VALID_FLAG=@VALID_FLAG,
				MODIFY_USER=@MODIFY_USER,
				CREATE_USER=@CREATE_USER,
				MODIFY_DATE=@MODIFY_DATE,
				FID=@FID,
				CREATE_DATE=@CREATE_DATE				 
				WHERE [VALID_FLAG] = 1 AND ID =@ID;";

		private const string TS_SYS_CODE_DELETE =
			@"DELETE FROM [dbo].[TS_SYS_CODE] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1 AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get CodeInfo
		/// </summary>
		/// <param name="ID">CodeInfo Primary key </param>
		/// <returns></returns> 
		public CodeInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_CODE_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateCodeInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>CodeInfo Collection </returns>
		public List<CodeInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TS_SYS_CODE_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>CodeInfo Collection </returns>
		public List<CodeInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<CodeInfo> list = new List<CodeInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateCodeInfo(dr));
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
		public List<CodeInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [dbo].[TS_SYS_CODE]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<CodeInfo> list = new List<CodeInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateCodeInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TS_SYS_CODE_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(CodeInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_CODE_INSERT);			
			db.AddInParameter(dbCommand, "@CODE_NAME", DbType.String, info.CodeName);
			db.AddInParameter(dbCommand, "@CODE_NAME_CN", DbType.String, info.CodeNameCn);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}		
		/// <summary>
		/// GetInsertSql
		/// </summary>
		/// <param name="info"> info</param>
		public static string GetInsertSql(CodeInfo info)
		{
			return  
			@"insert into [dbo].[TS_SYS_CODE] (
				CODE_NAME,
				CODE_NAME_CN,
				COMMENTS,
				VALID_FLAG,
				MODIFY_USER,
				CREATE_USER,
				MODIFY_DATE,
				FID,
				CREATE_DATE				 
			) values ("+
				(string.IsNullOrEmpty(info.CodeName) ? "NULL" : "N'" + info.CodeName + "'") + ","+
				(string.IsNullOrEmpty(info.CodeNameCn) ? "NULL" : "N'" + info.CodeNameCn + "'") + ","+
				(string.IsNullOrEmpty(info.Comments) ? "NULL" : "N'" + info.Comments + "'") + ","+
				"1" + ","+		
				"NULL" + ","+			
				"N'" + info.CreateUser + "'" + ","+		
				"NULL" + ","+			
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				"GETDATE()" + ");";			
				}
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(CodeInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_CODE_UPDATE);				
			db.AddInParameter(dbCommand, "@CODE_NAME", DbType.String, info.CodeName);
			db.AddInParameter(dbCommand, "@CODE_NAME_CN", DbType.String, info.CodeNameCn);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="ID">CodeInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_CODE_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">CodeInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [dbo].[TS_SYS_CODE] WITH(ROWLOCK) "
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
		/// <param name="ID">CodeInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [dbo].[TS_SYS_CODE] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1 AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static CodeInfo CreateCodeInfo(IDataReader rdr)
		{
			CodeInfo info = new CodeInfo();
			info.CodeName = DBConvert.GetString(rdr, rdr.GetOrdinal("CODE_NAME"));			
			info.CodeNameCn = DBConvert.GetString(rdr, rdr.GetOrdinal("CODE_NAME_CN"));			
			info.Comments = DBConvert.GetString(rdr, rdr.GetOrdinal("COMMENTS"));			
			info.ValidFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.CreateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			return info;
		}
		
		#endregion
	}
}