#region Declaim
//---------------------------------------------------------------------------
// Name:		RoleUserConditionDAL
// Function: 	Expose data in table TS_SYS_ROLE_USER_CONDITION from database as business object to MES system.
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
    /// RoleUserConditionDAL对应表[TS_SYS_ROLE_USER_CONDITION]
    /// </summary>
    public partial class RoleUserConditionDAL : BusinessObjectProvider<RoleUserConditionInfo>
	{
		#region Sql Statements
		private const string TS_SYS_ROLE_USER_CONDITION_SELECT_BY_ID =
			@"SELECT USER_ROLE_FID,
				USER_FID,
				ROLE_FID,
				CONDITION_FID,
				CONDITION_CONTEXT,
				CREATE_USER,
				MODIFY_DATE,
				ID,
				CREATE_DATE,
				FID,
				MODIFY_USER,
				VALID_FLAG				  
				FROM [dbo].[TS_SYS_ROLE_USER_CONDITION] WITH(NOLOCK) WHERE [VALID_FLAG] = 1 AND ID =@ID;";
			
		private const string TS_SYS_ROLE_USER_CONDITION_SELECT = 
			@"SELECT USER_ROLE_FID,
				USER_FID,
				ROLE_FID,
				CONDITION_FID,
				CONDITION_CONTEXT,
				CREATE_USER,
				MODIFY_DATE,
				ID,
				CREATE_DATE,
				FID,
				MODIFY_USER,
				VALID_FLAG				 
				FROM [dbo].[TS_SYS_ROLE_USER_CONDITION] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TS_SYS_ROLE_USER_CONDITION_SELECT_COUNTS = 
			@"SELECT count(*) FROM [dbo].[TS_SYS_ROLE_USER_CONDITION]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TS_SYS_ROLE_USER_CONDITION_INSERT =
			@"INSERT INTO [dbo].[TS_SYS_ROLE_USER_CONDITION] (
				USER_ROLE_FID,
				USER_FID,
				ROLE_FID,
				CONDITION_FID,
				CONDITION_CONTEXT,
				CREATE_USER,
				MODIFY_DATE,
				CREATE_DATE,
				FID,
				MODIFY_USER,
				VALID_FLAG				 
			) VALUES (
				@USER_ROLE_FID,
				@USER_FID,
				@ROLE_FID,
				@CONDITION_FID,
				@CONDITION_CONTEXT,
				@CREATE_USER,
				@MODIFY_DATE,
				@CREATE_DATE,
				@FID,
				@MODIFY_USER,
				@VALID_FLAG				 
			);SELECT @@IDENTITY;";
		private const string TS_SYS_ROLE_USER_CONDITION_UPDATE =
			@"UPDATE [dbo].[TS_SYS_ROLE_USER_CONDITION] WITH(ROWLOCK) 
				SET USER_ROLE_FID=@USER_ROLE_FID,
				USER_FID=@USER_FID,
				ROLE_FID=@ROLE_FID,
				CONDITION_FID=@CONDITION_FID,
				CONDITION_CONTEXT=@CONDITION_CONTEXT,
				CREATE_USER=@CREATE_USER,
				MODIFY_DATE=@MODIFY_DATE,
				CREATE_DATE=@CREATE_DATE,
				FID=@FID,
				MODIFY_USER=@MODIFY_USER,
				VALID_FLAG=@VALID_FLAG				 
				WHERE [VALID_FLAG] = 1 AND ID =@ID;";

		private const string TS_SYS_ROLE_USER_CONDITION_DELETE =
			@"DELETE FROM [dbo].[TS_SYS_ROLE_USER_CONDITION] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1 AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get RoleUserConditionInfo
		/// </summary>
		/// <param name="ID">RoleUserConditionInfo Primary key </param>
		/// <returns></returns> 
		public RoleUserConditionInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_ROLE_USER_CONDITION_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateRoleUserConditionInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>RoleUserConditionInfo Collection </returns>
		public List<RoleUserConditionInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TS_SYS_ROLE_USER_CONDITION_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>RoleUserConditionInfo Collection </returns>
		public List<RoleUserConditionInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<RoleUserConditionInfo> list = new List<RoleUserConditionInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateRoleUserConditionInfo(dr));
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
		public List<RoleUserConditionInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [dbo].[TS_SYS_ROLE_USER_CONDITION]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<RoleUserConditionInfo> list = new List<RoleUserConditionInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateRoleUserConditionInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TS_SYS_ROLE_USER_CONDITION_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(RoleUserConditionInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_ROLE_USER_CONDITION_INSERT);			
			db.AddInParameter(dbCommand, "@USER_ROLE_FID", DbType.Guid, info.UserRoleFid);
			db.AddInParameter(dbCommand, "@USER_FID", DbType.Guid, info.UserFid);
			db.AddInParameter(dbCommand, "@ROLE_FID", DbType.Guid, info.RoleFid);
			db.AddInParameter(dbCommand, "@CONDITION_FID", DbType.Guid, info.ConditionFid);
			db.AddInParameter(dbCommand, "@CONDITION_CONTEXT", DbType.String, info.ConditionContext);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}		
		/// <summary>
		/// GetInsertSql
		/// </summary>
		/// <param name="info"> info</param>
		public static string GetInsertSql(RoleUserConditionInfo info)
		{
			return  
			@"insert into [dbo].[TS_SYS_ROLE_USER_CONDITION] (
				USER_ROLE_FID,
				USER_FID,
				ROLE_FID,
				CONDITION_FID,
				CONDITION_CONTEXT,
				CREATE_USER,
				MODIFY_DATE,
				CREATE_DATE,
				FID,
				MODIFY_USER,
				VALID_FLAG				 
			) values ("+
				(info.UserRoleFid == null ? "NULL" : "N'" + info.UserRoleFid.GetValueOrDefault() + "'") + ","+
				(info.UserFid == null ? "NULL" : "N'" + info.UserFid.GetValueOrDefault() + "'") + ","+
				(info.RoleFid == null ? "NULL" : "N'" + info.RoleFid.GetValueOrDefault() + "'") + ","+
				(info.ConditionFid == null ? "NULL" : "N'" + info.ConditionFid.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.ConditionContext) ? "NULL" : "N'" + info.ConditionContext + "'") + ","+
				"N'" + info.CreateUser + "'" + ","+		
				"NULL" + ","+			
				"GETDATE()" + ","+			
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				"NULL" + ","+			
				"1" + ");";		
				}
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(RoleUserConditionInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_ROLE_USER_CONDITION_UPDATE);				
			db.AddInParameter(dbCommand, "@USER_ROLE_FID", DbType.Guid, info.UserRoleFid);
			db.AddInParameter(dbCommand, "@USER_FID", DbType.Guid, info.UserFid);
			db.AddInParameter(dbCommand, "@ROLE_FID", DbType.Guid, info.RoleFid);
			db.AddInParameter(dbCommand, "@CONDITION_FID", DbType.Guid, info.ConditionFid);
			db.AddInParameter(dbCommand, "@CONDITION_CONTEXT", DbType.String, info.ConditionContext);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="ID">RoleUserConditionInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_ROLE_USER_CONDITION_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">RoleUserConditionInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [dbo].[TS_SYS_ROLE_USER_CONDITION] WITH(ROWLOCK) "
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
		/// <param name="ID">RoleUserConditionInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [dbo].[TS_SYS_ROLE_USER_CONDITION] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1 AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static RoleUserConditionInfo CreateRoleUserConditionInfo(IDataReader rdr)
		{
			RoleUserConditionInfo info = new RoleUserConditionInfo();
			info.UserRoleFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("USER_ROLE_FID"));			
			info.UserFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("USER_FID"));			
			info.RoleFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("ROLE_FID"));			
			info.ConditionFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("CONDITION_FID"));			
			info.ConditionContext = DBConvert.GetString(rdr, rdr.GetOrdinal("CONDITION_CONTEXT"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.CreateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			info.ValidFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			return info;
		}
		
		#endregion
	}
}
