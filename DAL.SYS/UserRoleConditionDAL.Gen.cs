#region Declaim
//---------------------------------------------------------------------------
// Name:		UserRoleConditionDAL
// Function: 	Expose data in table TS_SYS_USER_ROLE_CONDITION from database as business object to MES system.
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
    /// UserRoleConditionDAL对应表[TS_SYS_USER_ROLE_CONDITION]
    /// </summary>
    public partial class UserRoleConditionDAL : BusinessObjectProvider<UserRoleConditionInfo>
	{
		#region Sql Statements
		private const string TS_SYS_USER_ROLE_CONDITION_SELECT_BY_ID =
			@"SELECT CONDITION_NAME,
				TABLE_NAME,
				FIELD_NAME,
				ATTRIBUTE_NAME,
				DATA_TYPE,
				CONTROL_TYPE,
				EXTEND_ATTRIBUTE,
				CREATE_DATE,
				ID,
				VALID_FLAG,
				MODIFY_USER,
				FID,
				CREATE_USER,
				MODIFY_DATE				  
				FROM [dbo].[TS_SYS_USER_ROLE_CONDITION] WITH(NOLOCK) WHERE [VALID_FLAG] = 1 AND ID =@ID;";
			
		private const string TS_SYS_USER_ROLE_CONDITION_SELECT = 
			@"SELECT CONDITION_NAME,
				TABLE_NAME,
				FIELD_NAME,
				ATTRIBUTE_NAME,
				DATA_TYPE,
				CONTROL_TYPE,
				EXTEND_ATTRIBUTE,
				CREATE_DATE,
				ID,
				VALID_FLAG,
				MODIFY_USER,
				FID,
				CREATE_USER,
				MODIFY_DATE				 
				FROM [dbo].[TS_SYS_USER_ROLE_CONDITION] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TS_SYS_USER_ROLE_CONDITION_SELECT_COUNTS = 
			@"SELECT count(*) FROM [dbo].[TS_SYS_USER_ROLE_CONDITION]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TS_SYS_USER_ROLE_CONDITION_INSERT =
			@"INSERT INTO [dbo].[TS_SYS_USER_ROLE_CONDITION] (
				CONDITION_NAME,
				TABLE_NAME,
				FIELD_NAME,
				ATTRIBUTE_NAME,
				DATA_TYPE,
				CONTROL_TYPE,
				EXTEND_ATTRIBUTE,
				CREATE_DATE,
				VALID_FLAG,
				MODIFY_USER,
				FID,
				CREATE_USER,
				MODIFY_DATE				 
			) VALUES (
				@CONDITION_NAME,
				@TABLE_NAME,
				@FIELD_NAME,
				@ATTRIBUTE_NAME,
				@DATA_TYPE,
				@CONTROL_TYPE,
				@EXTEND_ATTRIBUTE,
				@CREATE_DATE,
				@VALID_FLAG,
				@MODIFY_USER,
				@FID,
				@CREATE_USER,
				@MODIFY_DATE				 
			);SELECT @@IDENTITY;";
		private const string TS_SYS_USER_ROLE_CONDITION_UPDATE =
			@"UPDATE [dbo].[TS_SYS_USER_ROLE_CONDITION] WITH(ROWLOCK) 
				SET CONDITION_NAME=@CONDITION_NAME,
				TABLE_NAME=@TABLE_NAME,
				FIELD_NAME=@FIELD_NAME,
				ATTRIBUTE_NAME=@ATTRIBUTE_NAME,
				DATA_TYPE=@DATA_TYPE,
				CONTROL_TYPE=@CONTROL_TYPE,
				EXTEND_ATTRIBUTE=@EXTEND_ATTRIBUTE,
				CREATE_DATE=@CREATE_DATE,
				VALID_FLAG=@VALID_FLAG,
				MODIFY_USER=@MODIFY_USER,
				FID=@FID,
				CREATE_USER=@CREATE_USER,
				MODIFY_DATE=@MODIFY_DATE				 
				WHERE [VALID_FLAG] = 1 AND ID =@ID;";

		private const string TS_SYS_USER_ROLE_CONDITION_DELETE =
			@"DELETE FROM [dbo].[TS_SYS_USER_ROLE_CONDITION] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1 AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get UserRoleConditionInfo
		/// </summary>
		/// <param name="ID">UserRoleConditionInfo Primary key </param>
		/// <returns></returns> 
		public UserRoleConditionInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_USER_ROLE_CONDITION_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateUserRoleConditionInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>UserRoleConditionInfo Collection </returns>
		public List<UserRoleConditionInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TS_SYS_USER_ROLE_CONDITION_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>UserRoleConditionInfo Collection </returns>
		public List<UserRoleConditionInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<UserRoleConditionInfo> list = new List<UserRoleConditionInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateUserRoleConditionInfo(dr));
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
		public List<UserRoleConditionInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [dbo].[TS_SYS_USER_ROLE_CONDITION]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<UserRoleConditionInfo> list = new List<UserRoleConditionInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateUserRoleConditionInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TS_SYS_USER_ROLE_CONDITION_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(UserRoleConditionInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_USER_ROLE_CONDITION_INSERT);			
			db.AddInParameter(dbCommand, "@CONDITION_NAME", DbType.String, info.ConditionName);
			db.AddInParameter(dbCommand, "@TABLE_NAME", DbType.String, info.TableName);
			db.AddInParameter(dbCommand, "@FIELD_NAME", DbType.String, info.FieldName);
			db.AddInParameter(dbCommand, "@ATTRIBUTE_NAME", DbType.String, info.AttributeName);
			db.AddInParameter(dbCommand, "@DATA_TYPE", DbType.Int32, info.DataType);
			db.AddInParameter(dbCommand, "@CONTROL_TYPE", DbType.Int32, info.ControlType);
			db.AddInParameter(dbCommand, "@EXTEND_ATTRIBUTE", DbType.String, info.ExtendAttribute);
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
		public static string GetInsertSql(UserRoleConditionInfo info)
		{
			return  
			@"insert into [dbo].[TS_SYS_USER_ROLE_CONDITION] (
				CONDITION_NAME,
				TABLE_NAME,
				FIELD_NAME,
				ATTRIBUTE_NAME,
				DATA_TYPE,
				CONTROL_TYPE,
				EXTEND_ATTRIBUTE,
				CREATE_DATE,
				VALID_FLAG,
				MODIFY_USER,
				FID,
				CREATE_USER,
				MODIFY_DATE				 
			) values ("+
				(string.IsNullOrEmpty(info.ConditionName) ? "NULL" : "N'" + info.ConditionName + "'") + ","+
				(string.IsNullOrEmpty(info.TableName) ? "NULL" : "N'" + info.TableName + "'") + ","+
				(string.IsNullOrEmpty(info.FieldName) ? "NULL" : "N'" + info.FieldName + "'") + ","+
				(string.IsNullOrEmpty(info.AttributeName) ? "NULL" : "N'" + info.AttributeName + "'") + ","+
				(info.DataType == null ? "NULL" : "" + info.DataType.GetValueOrDefault() + "") + ","+
				(info.ControlType == null ? "NULL" : "" + info.ControlType.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.ExtendAttribute) ? "NULL" : "N'" + info.ExtendAttribute + "'") + ","+
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
		public int Update(UserRoleConditionInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_USER_ROLE_CONDITION_UPDATE);				
			db.AddInParameter(dbCommand, "@CONDITION_NAME", DbType.String, info.ConditionName);
			db.AddInParameter(dbCommand, "@TABLE_NAME", DbType.String, info.TableName);
			db.AddInParameter(dbCommand, "@FIELD_NAME", DbType.String, info.FieldName);
			db.AddInParameter(dbCommand, "@ATTRIBUTE_NAME", DbType.String, info.AttributeName);
			db.AddInParameter(dbCommand, "@DATA_TYPE", DbType.Int32, info.DataType);
			db.AddInParameter(dbCommand, "@CONTROL_TYPE", DbType.Int32, info.ControlType);
			db.AddInParameter(dbCommand, "@EXTEND_ATTRIBUTE", DbType.String, info.ExtendAttribute);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
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
		/// <param name="ID">UserRoleConditionInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_USER_ROLE_CONDITION_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">UserRoleConditionInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [dbo].[TS_SYS_USER_ROLE_CONDITION] WITH(ROWLOCK) "
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
		/// <param name="ID">UserRoleConditionInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [dbo].[TS_SYS_USER_ROLE_CONDITION] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1 AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static UserRoleConditionInfo CreateUserRoleConditionInfo(IDataReader rdr)
		{
			UserRoleConditionInfo info = new UserRoleConditionInfo();
			info.ConditionName = DBConvert.GetString(rdr, rdr.GetOrdinal("CONDITION_NAME"));			
			info.TableName = DBConvert.GetString(rdr, rdr.GetOrdinal("TABLE_NAME"));			
			info.FieldName = DBConvert.GetString(rdr, rdr.GetOrdinal("FIELD_NAME"));			
			info.AttributeName = DBConvert.GetString(rdr, rdr.GetOrdinal("ATTRIBUTE_NAME"));			
			info.DataType = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("DATA_TYPE"));			
			info.ControlType = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("CONTROL_TYPE"));			
			info.ExtendAttribute = DBConvert.GetString(rdr, rdr.GetOrdinal("EXTEND_ATTRIBUTE"));			
			info.CreateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
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
