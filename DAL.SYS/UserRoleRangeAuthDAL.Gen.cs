#region Declaim
//---------------------------------------------------------------------------
// Name:		UserRoleRangeAuthDAL
// Function: 	Expose data in table TS_SYS_USER_ROLE_RANGE_AUTH from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年6月13日
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
    /// UserRoleRangeAuthDAL对应表[TS_SYS_USER_ROLE_RANGE_AUTH]
    /// </summary>
    public partial class UserRoleRangeAuthDAL : BusinessObjectProvider<UserRoleRangeAuthInfo>
	{
		#region Sql Statements
		private const string TS_SYS_USER_ROLE_RANGE_AUTH_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				USER_FID,
				ROLE_FID,
				CONDITION_FID,
				CONDITION_CONTEXT,
				COMMENTS,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE				  
				FROM [dbo].[TS_SYS_USER_ROLE_RANGE_AUTH] WITH(NOLOCK) WHERE [VALID_FLAG] = 1 AND ID =@ID;";
			
		private const string TS_SYS_USER_ROLE_RANGE_AUTH_SELECT = 
			@"SELECT ID,
				FID,
				USER_FID,
				ROLE_FID,
				CONDITION_FID,
				CONDITION_CONTEXT,
				COMMENTS,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE				 
				FROM [dbo].[TS_SYS_USER_ROLE_RANGE_AUTH] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TS_SYS_USER_ROLE_RANGE_AUTH_SELECT_COUNTS = 
			@"SELECT count(*) FROM [dbo].[TS_SYS_USER_ROLE_RANGE_AUTH]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TS_SYS_USER_ROLE_RANGE_AUTH_INSERT =
			@"INSERT INTO [dbo].[TS_SYS_USER_ROLE_RANGE_AUTH] (
				FID,
				USER_FID,
				ROLE_FID,
				CONDITION_FID,
				CONDITION_CONTEXT,
				COMMENTS,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE				 
			) VALUES (
				@FID,
				@USER_FID,
				@ROLE_FID,
				@CONDITION_FID,
				@CONDITION_CONTEXT,
				@COMMENTS,
				@VALID_FLAG,
				@CREATE_USER,
				@CREATE_DATE,
				@MODIFY_USER,
				@MODIFY_DATE				 
			);SELECT @@IDENTITY;";
		private const string TS_SYS_USER_ROLE_RANGE_AUTH_UPDATE =
			@"UPDATE [dbo].[TS_SYS_USER_ROLE_RANGE_AUTH] WITH(ROWLOCK) 
				SET FID=@FID,
				USER_FID=@USER_FID,
				ROLE_FID=@ROLE_FID,
				CONDITION_FID=@CONDITION_FID,
				CONDITION_CONTEXT=@CONDITION_CONTEXT,
				COMMENTS=@COMMENTS,
				VALID_FLAG=@VALID_FLAG,
				CREATE_USER=@CREATE_USER,
				CREATE_DATE=@CREATE_DATE,
				MODIFY_USER=@MODIFY_USER,
				MODIFY_DATE=@MODIFY_DATE				 
				WHERE [VALID_FLAG] = 1 AND ID =@ID;";

		private const string TS_SYS_USER_ROLE_RANGE_AUTH_DELETE =
			@"DELETE FROM [dbo].[TS_SYS_USER_ROLE_RANGE_AUTH] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1 AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get UserRoleRangeAuthInfo
		/// </summary>
		/// <param name="ID">UserRoleRangeAuthInfo Primary key </param>
		/// <returns></returns> 
		public UserRoleRangeAuthInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_USER_ROLE_RANGE_AUTH_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateUserRoleRangeAuthInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>UserRoleRangeAuthInfo Collection </returns>
		public List<UserRoleRangeAuthInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TS_SYS_USER_ROLE_RANGE_AUTH_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>UserRoleRangeAuthInfo Collection </returns>
		public List<UserRoleRangeAuthInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<UserRoleRangeAuthInfo> list = new List<UserRoleRangeAuthInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateUserRoleRangeAuthInfo(dr));
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
		public List<UserRoleRangeAuthInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [dbo].[TS_SYS_USER_ROLE_RANGE_AUTH]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<UserRoleRangeAuthInfo> list = new List<UserRoleRangeAuthInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateUserRoleRangeAuthInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TS_SYS_USER_ROLE_RANGE_AUTH_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(UserRoleRangeAuthInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_USER_ROLE_RANGE_AUTH_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@USER_FID", DbType.Guid, info.UserFid);
			db.AddInParameter(dbCommand, "@ROLE_FID", DbType.Guid, info.RoleFid);
			db.AddInParameter(dbCommand, "@CONDITION_FID", DbType.Guid, info.ConditionFid);
			db.AddInParameter(dbCommand, "@CONDITION_CONTEXT", DbType.String, info.ConditionContext);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}		
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(UserRoleRangeAuthInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_USER_ROLE_RANGE_AUTH_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@USER_FID", DbType.Guid, info.UserFid);
			db.AddInParameter(dbCommand, "@ROLE_FID", DbType.Guid, info.RoleFid);
			db.AddInParameter(dbCommand, "@CONDITION_FID", DbType.Guid, info.ConditionFid);
			db.AddInParameter(dbCommand, "@CONDITION_CONTEXT", DbType.String, info.ConditionContext);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="ID">UserRoleRangeAuthInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_USER_ROLE_RANGE_AUTH_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">UserRoleRangeAuthInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [dbo].[TS_SYS_USER_ROLE_RANGE_AUTH] WITH(ROWLOCK) "
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
		/// <param name="ID">UserRoleRangeAuthInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [dbo].[TS_SYS_USER_ROLE_RANGE_AUTH] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1 AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static UserRoleRangeAuthInfo CreateUserRoleRangeAuthInfo(IDataReader rdr)
		{
			UserRoleRangeAuthInfo info = new UserRoleRangeAuthInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.UserFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("USER_FID"));			
			info.RoleFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("ROLE_FID"));			
			info.ConditionFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("CONDITION_FID"));			
			info.ConditionContext = DBConvert.GetString(rdr, rdr.GetOrdinal("CONDITION_CONTEXT"));			
			info.Comments = DBConvert.GetString(rdr, rdr.GetOrdinal("COMMENTS"));			
			info.ValidFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.CreateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			return info;
		}
		
		#endregion
	}
}