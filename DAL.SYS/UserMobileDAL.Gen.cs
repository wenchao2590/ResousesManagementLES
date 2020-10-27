#region Declaim
//---------------------------------------------------------------------------
// Name:		UserMobileDAL
// Function: 	Expose data in table TS_SYS_USER_MOBILE from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年6月21日
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
    /// UserMobileDAL对应表[TS_SYS_USER_MOBILE]
    /// </summary>
    public partial class UserMobileDAL : BusinessObjectProvider<UserMobileInfo>
	{
		#region Sql Statements
		private const string TS_SYS_USER_MOBILE_SELECT_BY_ID =
			@"SELECT USER_FID,
				USER_NAME,
				IMEI,
				IMSI,
				MODEL,
				VENDOR,
				UUID,
				OS_LANGUAGE,
				OS_VERSION,
				OS_NAME,
				OS_VENDOR,
				VALID_FLAG,
				ID,
				MODIFY_USER,
				CREATE_USER,
				MODIFY_DATE,
				FID,
				CREATE_DATE,
				STATUS				  
				FROM [dbo].[TS_SYS_USER_MOBILE] WITH(NOLOCK) WHERE [VALID_FLAG] = 1 AND ID =@ID;";
			
		private const string TS_SYS_USER_MOBILE_SELECT = 
			@"SELECT USER_FID,
				USER_NAME,
				IMEI,
				IMSI,
				MODEL,
				VENDOR,
				UUID,
				OS_LANGUAGE,
				OS_VERSION,
				OS_NAME,
				OS_VENDOR,
				VALID_FLAG,
				ID,
				MODIFY_USER,
				CREATE_USER,
				MODIFY_DATE,
				FID,
				CREATE_DATE,
				STATUS				 
				FROM [dbo].[TS_SYS_USER_MOBILE] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TS_SYS_USER_MOBILE_SELECT_COUNTS = 
			@"SELECT count(*) FROM [dbo].[TS_SYS_USER_MOBILE]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TS_SYS_USER_MOBILE_INSERT =
			@"INSERT INTO [dbo].[TS_SYS_USER_MOBILE] (
				USER_FID,
				USER_NAME,
				IMEI,
				IMSI,
				MODEL,
				VENDOR,
				UUID,
				OS_LANGUAGE,
				OS_VERSION,
				OS_NAME,
				OS_VENDOR,
				VALID_FLAG,
				MODIFY_USER,
				CREATE_USER,
				MODIFY_DATE,
				FID,
				CREATE_DATE,
				STATUS				 
			) VALUES (
				@USER_FID,
				@USER_NAME,
				@IMEI,
				@IMSI,
				@MODEL,
				@VENDOR,
				@UUID,
				@OS_LANGUAGE,
				@OS_VERSION,
				@OS_NAME,
				@OS_VENDOR,
				@VALID_FLAG,
				@MODIFY_USER,
				@CREATE_USER,
				@MODIFY_DATE,
				@FID,
				@CREATE_DATE,
				@STATUS				 
			);SELECT @@IDENTITY;";
		private const string TS_SYS_USER_MOBILE_UPDATE =
			@"UPDATE [dbo].[TS_SYS_USER_MOBILE] WITH(ROWLOCK) 
				SET USER_FID=@USER_FID,
				USER_NAME=@USER_NAME,
				IMEI=@IMEI,
				IMSI=@IMSI,
				MODEL=@MODEL,
				VENDOR=@VENDOR,
				UUID=@UUID,
				OS_LANGUAGE=@OS_LANGUAGE,
				OS_VERSION=@OS_VERSION,
				OS_NAME=@OS_NAME,
				OS_VENDOR=@OS_VENDOR,
				VALID_FLAG=@VALID_FLAG,
				MODIFY_USER=@MODIFY_USER,
				CREATE_USER=@CREATE_USER,
				MODIFY_DATE=@MODIFY_DATE,
				FID=@FID,
				CREATE_DATE=@CREATE_DATE,
				STATUS=@STATUS				 
				WHERE [VALID_FLAG] = 1 AND ID =@ID;";

		private const string TS_SYS_USER_MOBILE_DELETE =
			@"DELETE FROM [dbo].[TS_SYS_USER_MOBILE] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1 AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get UserMobileInfo
		/// </summary>
		/// <param name="ID">UserMobileInfo Primary key </param>
		/// <returns></returns> 
		public UserMobileInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_USER_MOBILE_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateUserMobileInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>UserMobileInfo Collection </returns>
		public List<UserMobileInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TS_SYS_USER_MOBILE_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>UserMobileInfo Collection </returns>
		public List<UserMobileInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<UserMobileInfo> list = new List<UserMobileInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateUserMobileInfo(dr));
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
		public List<UserMobileInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [dbo].[TS_SYS_USER_MOBILE]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<UserMobileInfo> list = new List<UserMobileInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateUserMobileInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TS_SYS_USER_MOBILE_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(UserMobileInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_USER_MOBILE_INSERT);			
			db.AddInParameter(dbCommand, "@USER_FID", DbType.Guid, info.UserFid);
			db.AddInParameter(dbCommand, "@USER_NAME", DbType.String, info.UserName);
			db.AddInParameter(dbCommand, "@IMEI", DbType.String, info.Imei);
			db.AddInParameter(dbCommand, "@IMSI", DbType.String, info.Imsi);
			db.AddInParameter(dbCommand, "@MODEL", DbType.String, info.Model);
			db.AddInParameter(dbCommand, "@VENDOR", DbType.String, info.Vendor);
			db.AddInParameter(dbCommand, "@UUID", DbType.String, info.Uuid);
			db.AddInParameter(dbCommand, "@OS_LANGUAGE", DbType.String, info.OsLanguage);
			db.AddInParameter(dbCommand, "@OS_VERSION", DbType.String, info.OsVersion);
			db.AddInParameter(dbCommand, "@OS_NAME", DbType.String, info.OsName);
			db.AddInParameter(dbCommand, "@OS_VENDOR", DbType.String, info.OsVendor);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@STATUS", DbType.Int32, info.Status);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}		
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(UserMobileInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_USER_MOBILE_UPDATE);				
			db.AddInParameter(dbCommand, "@USER_FID", DbType.Guid, info.UserFid);
			db.AddInParameter(dbCommand, "@USER_NAME", DbType.String, info.UserName);
			db.AddInParameter(dbCommand, "@IMEI", DbType.String, info.Imei);
			db.AddInParameter(dbCommand, "@IMSI", DbType.String, info.Imsi);
			db.AddInParameter(dbCommand, "@MODEL", DbType.String, info.Model);
			db.AddInParameter(dbCommand, "@VENDOR", DbType.String, info.Vendor);
			db.AddInParameter(dbCommand, "@UUID", DbType.String, info.Uuid);
			db.AddInParameter(dbCommand, "@OS_LANGUAGE", DbType.String, info.OsLanguage);
			db.AddInParameter(dbCommand, "@OS_VERSION", DbType.String, info.OsVersion);
			db.AddInParameter(dbCommand, "@OS_NAME", DbType.String, info.OsName);
			db.AddInParameter(dbCommand, "@OS_VENDOR", DbType.String, info.OsVendor);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@STATUS", DbType.Int32, info.Status);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="ID">UserMobileInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_USER_MOBILE_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">UserMobileInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [dbo].[TS_SYS_USER_MOBILE] WITH(ROWLOCK) "
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
		/// <param name="ID">UserMobileInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [dbo].[TS_SYS_USER_MOBILE] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1 AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static UserMobileInfo CreateUserMobileInfo(IDataReader rdr)
		{
			UserMobileInfo info = new UserMobileInfo();
			info.UserFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("USER_FID"));			
			info.UserName = DBConvert.GetString(rdr, rdr.GetOrdinal("USER_NAME"));			
			info.Imei = DBConvert.GetString(rdr, rdr.GetOrdinal("IMEI"));			
			info.Imsi = DBConvert.GetString(rdr, rdr.GetOrdinal("IMSI"));			
			info.Model = DBConvert.GetString(rdr, rdr.GetOrdinal("MODEL"));			
			info.Vendor = DBConvert.GetString(rdr, rdr.GetOrdinal("VENDOR"));			
			info.Uuid = DBConvert.GetString(rdr, rdr.GetOrdinal("UUID"));			
			info.OsLanguage = DBConvert.GetString(rdr, rdr.GetOrdinal("OS_LANGUAGE"));			
			info.OsVersion = DBConvert.GetString(rdr, rdr.GetOrdinal("OS_VERSION"));			
			info.OsName = DBConvert.GetString(rdr, rdr.GetOrdinal("OS_NAME"));			
			info.OsVendor = DBConvert.GetString(rdr, rdr.GetOrdinal("OS_VENDOR"));			
			info.ValidFlag = DBConvert.GetBool(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.CreateDate = DBConvert.GetDateTime(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.Status = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("STATUS"));			
			return info;
		}
		
		#endregion
	}
}
