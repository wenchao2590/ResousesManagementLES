#region Declaim
//---------------------------------------------------------------------------
// Name:		UserLoginDAL
// Function: 	Expose data in table TL_SYS_USER_LOGIN from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年3月27日
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
    /// UserLoginDAL对应表[TL_SYS_USER_LOGIN]
    /// </summary>
    public partial class UserLoginDAL : BusinessObjectProvider<UserLoginInfo>
	{
		#region Sql Statements
		private const string TL_SYS_USER_LOGIN_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				LOGIN_NAME,
				IP_ADDRESS,
				LOGIN_TYPE,
				SOURCE_TYPE,
				EXECUTE_TIME,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE				  
				FROM [dbo].[TL_SYS_USER_LOGIN] WITH(NOLOCK) WHERE [VALID_FLAG] = 1 AND ID =@ID;";
			
		private const string TL_SYS_USER_LOGIN_SELECT = 
			@"SELECT ID,
				FID,
				LOGIN_NAME,
				IP_ADDRESS,
				LOGIN_TYPE,
				SOURCE_TYPE,
				EXECUTE_TIME,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE				 
				FROM [dbo].[TL_SYS_USER_LOGIN] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TL_SYS_USER_LOGIN_SELECT_COUNTS = 
			@"SELECT count(*) FROM [dbo].[TL_SYS_USER_LOGIN]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TL_SYS_USER_LOGIN_INSERT =
			@"INSERT INTO [dbo].[TL_SYS_USER_LOGIN] (
				FID,
				LOGIN_NAME,
				IP_ADDRESS,
				LOGIN_TYPE,
				SOURCE_TYPE,
				EXECUTE_TIME,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE				 
			) VALUES (
				@FID,
				@LOGIN_NAME,
				@IP_ADDRESS,
				@LOGIN_TYPE,
				@SOURCE_TYPE,
				@EXECUTE_TIME,
				@VALID_FLAG,
				@CREATE_USER,
				@CREATE_DATE,
				@MODIFY_USER,
				@MODIFY_DATE				 
			);SELECT @@IDENTITY;";
		private const string TL_SYS_USER_LOGIN_UPDATE =
			@"UPDATE [dbo].[TL_SYS_USER_LOGIN] WITH(ROWLOCK) 
				SET FID=@FID,
				LOGIN_NAME=@LOGIN_NAME,
				IP_ADDRESS=@IP_ADDRESS,
				LOGIN_TYPE=@LOGIN_TYPE,
				SOURCE_TYPE=@SOURCE_TYPE,
				EXECUTE_TIME=@EXECUTE_TIME,
				VALID_FLAG=@VALID_FLAG,
				CREATE_USER=@CREATE_USER,
				CREATE_DATE=@CREATE_DATE,
				MODIFY_USER=@MODIFY_USER,
				MODIFY_DATE=@MODIFY_DATE				 
				WHERE [VALID_FLAG] = 1 AND ID =@ID;";

		private const string TL_SYS_USER_LOGIN_DELETE =
			@"DELETE FROM [dbo].[TL_SYS_USER_LOGIN] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1 AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get UserLoginInfo
		/// </summary>
		/// <param name="ID">UserLoginInfo Primary key </param>
		/// <returns></returns> 
		public UserLoginInfo GetInfo(int aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TL_SYS_USER_LOGIN_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int32, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateUserLoginInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>UserLoginInfo Collection </returns>
		public List<UserLoginInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TL_SYS_USER_LOGIN_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>UserLoginInfo Collection </returns>
		public List<UserLoginInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<UserLoginInfo> list = new List<UserLoginInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateUserLoginInfo(dr));
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
		public List<UserLoginInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [dbo].[TL_SYS_USER_LOGIN]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<UserLoginInfo> list = new List<UserLoginInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateUserLoginInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TL_SYS_USER_LOGIN_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public int Add(UserLoginInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TL_SYS_USER_LOGIN_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@LOGIN_NAME", DbType.String, info.LoginName);
			db.AddInParameter(dbCommand, "@IP_ADDRESS", DbType.String, info.IpAddress);
			db.AddInParameter(dbCommand, "@LOGIN_TYPE", DbType.String, info.LoginType);
			db.AddInParameter(dbCommand, "@SOURCE_TYPE", DbType.String, info.SourceType);
			db.AddInParameter(dbCommand, "@EXECUTE_TIME", DbType.DateTime, info.ExecuteTime);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			return int.Parse("0" + db.ExecuteScalar(dbCommand));		
		}		
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(UserLoginInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TL_SYS_USER_LOGIN_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int32, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@LOGIN_NAME", DbType.String, info.LoginName);
			db.AddInParameter(dbCommand, "@IP_ADDRESS", DbType.String, info.IpAddress);
			db.AddInParameter(dbCommand, "@LOGIN_TYPE", DbType.String, info.LoginType);
			db.AddInParameter(dbCommand, "@SOURCE_TYPE", DbType.String, info.SourceType);
			db.AddInParameter(dbCommand, "@EXECUTE_TIME", DbType.DateTime, info.ExecuteTime);
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
		/// <param name="ID">UserLoginInfo Primary key </param>
		/// <returns></returns>
		public int Delete(int aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TL_SYS_USER_LOGIN_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int32, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">UserLoginInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(int aId, string loginUser)
		{
		    string sql = "update [dbo].[TL_SYS_USER_LOGIN] WITH(ROWLOCK) "
                + "set [VALID_FLAG] = @VALID_FLAG ,[MODIFY_USER] = @MODIFY_USER ,[MODIFY_DATE] = GETDATE() "
                + "WHERE [VALID_FLAG] = 1 AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
 			db.AddInParameter(dbCommand, "@ID", DbType.Int32, aId);
           db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, false);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.AnsiString, loginUser);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}
		/// <summary>
		/// UpdateInfo
		/// </summary>
		/// <param name="ID">UserLoginInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,int aId)
		{
		    string sql = "update [dbo].[TL_SYS_USER_LOGIN] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1 AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int32, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static UserLoginInfo CreateUserLoginInfo(IDataReader rdr)
		{
			UserLoginInfo info = new UserLoginInfo();
			info.Id = DBConvert.GetInt32(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.LoginName = DBConvert.GetString(rdr, rdr.GetOrdinal("LOGIN_NAME"));			
			info.IpAddress = DBConvert.GetString(rdr, rdr.GetOrdinal("IP_ADDRESS"));			
			info.LoginType = DBConvert.GetString(rdr, rdr.GetOrdinal("LOGIN_TYPE"));			
			info.SourceType = DBConvert.GetString(rdr, rdr.GetOrdinal("SOURCE_TYPE"));			
			info.ExecuteTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("EXECUTE_TIME"));			
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