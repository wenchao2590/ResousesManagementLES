#region Declaim
//---------------------------------------------------------------------------
// Name:		MenuActionDAL
// Function: 	Expose data in table TS_SYS_MENU_ACTION from database as business object to MES system.
// Tool:		T4
// CreateDate:	2020年3月30日
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
    /// MenuActionDAL对应表[TS_SYS_MENU_ACTION]
    /// </summary>
    public partial class MenuActionDAL : BusinessObjectProvider<MenuActionInfo>
	{
		#region Sql Statements
		private const string TS_SYS_MENU_ACTION_SELECT_BY_ID =
			@"SELECT CLIENT_JS,
				ACTION_ORDER,
				NEED_AUTH,
				MENU_FID,
				CREATE_DATE,
				ID,
				VALID_FLAG,
				MODIFY_USER,
				ACTION_FID,
				FID,
				MODIFY_DATE,
				CREATE_USER,
				Detail_Flag				  
				FROM [LES].[TS_SYS_MENU_ACTION] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TS_SYS_MENU_ACTION_SELECT = 
			@"SELECT CLIENT_JS,
				ACTION_ORDER,
				NEED_AUTH,
				MENU_FID,
				CREATE_DATE,
				ID,
				VALID_FLAG,
				MODIFY_USER,
				ACTION_FID,
				FID,
				MODIFY_DATE,
				CREATE_USER,
				Detail_Flag				 
				FROM [LES].[TS_SYS_MENU_ACTION] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TS_SYS_MENU_ACTION_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TS_SYS_MENU_ACTION]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TS_SYS_MENU_ACTION_INSERT =
			@"INSERT INTO [LES].[TS_SYS_MENU_ACTION] (
				CLIENT_JS,
				ACTION_ORDER,
				NEED_AUTH,
				MENU_FID,
				CREATE_DATE,
				VALID_FLAG,
				MODIFY_USER,
				ACTION_FID,
				FID,
				MODIFY_DATE,
				CREATE_USER,
				Detail_Flag				 
			) VALUES (
				@CLIENT_JS,
				@ACTION_ORDER,
				@NEED_AUTH,
				@MENU_FID,
				GETDATE(),
				@VALID_FLAG,
				@MODIFY_USER,
				@ACTION_FID,
				@FID,
				@MODIFY_DATE,
				@CREATE_USER,
				@Detail_Flag				 
			);SELECT @@IDENTITY;";
		private const string TS_SYS_MENU_ACTION_UPDATE =
			@"UPDATE [LES].[TS_SYS_MENU_ACTION] WITH(ROWLOCK) 
				SET CLIENT_JS=@CLIENT_JS,
				ACTION_ORDER=@ACTION_ORDER,
				NEED_AUTH=@NEED_AUTH,
				MENU_FID=@MENU_FID,
				CREATE_DATE=@CREATE_DATE,
				VALID_FLAG=@VALID_FLAG,
				MODIFY_USER=@MODIFY_USER,
				ACTION_FID=@ACTION_FID,
				FID=@FID,
				MODIFY_DATE=@MODIFY_DATE,
				CREATE_USER=@CREATE_USER,
				Detail_Flag=@Detail_Flag				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TS_SYS_MENU_ACTION_DELETE =
			@"DELETE FROM [LES].[TS_SYS_MENU_ACTION] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get MenuActionInfo
		/// </summary>
		/// <param name="ID">MenuActionInfo Primary key </param>
		/// <returns></returns> 
		public MenuActionInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_MENU_ACTION_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateMenuActionInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>MenuActionInfo Collection </returns>
		public List<MenuActionInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TS_SYS_MENU_ACTION_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>MenuActionInfo Collection </returns>
		public List<MenuActionInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<MenuActionInfo> list = new List<MenuActionInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateMenuActionInfo(dr));
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
		public List<MenuActionInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TS_SYS_MENU_ACTION]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<MenuActionInfo> list = new List<MenuActionInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateMenuActionInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TS_SYS_MENU_ACTION_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(MenuActionInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_MENU_ACTION_INSERT);			
			db.AddInParameter(dbCommand, "@CLIENT_JS", DbType.String, info.ClientJs);
			db.AddInParameter(dbCommand, "@ACTION_ORDER", DbType.Int32, info.ActionOrder);
			db.AddInParameter(dbCommand, "@NEED_AUTH", DbType.Boolean, info.NeedAuth);
			db.AddInParameter(dbCommand, "@MENU_FID", DbType.Guid, info.MenuFid);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@ACTION_FID", DbType.Guid, info.ActionFid);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@Detail_Flag", DbType.Boolean, info.DetailFlag);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}
		/// <summary>
		/// GetInsertSql
		/// </summary>
		/// <param name="info"> info</param>
		public static string GetInsertSql(MenuActionInfo info)
		{
			return  
			@"insert into [LES].[TS_SYS_MENU_ACTION] (
				CLIENT_JS,
				ACTION_ORDER,
				NEED_AUTH,
				MENU_FID,
				CREATE_DATE,
				VALID_FLAG,
				MODIFY_USER,
				ACTION_FID,
				FID,
				MODIFY_DATE,
				CREATE_USER,
				Detail_Flag				 
			) values ("+
				(string.IsNullOrEmpty(info.ClientJs) ? "NULL" : "N'" + info.ClientJs + "'") + ","+
				(info.ActionOrder == null ? "NULL" : "" + info.ActionOrder.GetValueOrDefault() + "") + ","+
				(info.NeedAuth == null ? "NULL" : "" + (info.NeedAuth.GetValueOrDefault() ? "1" : "0") + "") + ","+
				(info.MenuFid == null ? "NULL" : "N'" + info.MenuFid.GetValueOrDefault() + "'") + ","+
				"GETDATE()" + ","+			
				"1" + ","+		
				"NULL" + ","+			
				(info.ActionFid == null ? "NULL" : "N'" + info.ActionFid.GetValueOrDefault() + "'") + ","+
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				"NULL" + ","+			
				"N'" + info.CreateUser + "'" + ","+		
				(info.DetailFlag == null ? "NULL" : "" + (info.DetailFlag.GetValueOrDefault() ? "1" : "0") + "") + ");";				}
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(MenuActionInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_MENU_ACTION_UPDATE);				
			db.AddInParameter(dbCommand, "@CLIENT_JS", DbType.String, info.ClientJs);
			db.AddInParameter(dbCommand, "@ACTION_ORDER", DbType.Int32, info.ActionOrder);
			db.AddInParameter(dbCommand, "@NEED_AUTH", DbType.Boolean, info.NeedAuth);
			db.AddInParameter(dbCommand, "@MENU_FID", DbType.Guid, info.MenuFid);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@ACTION_FID", DbType.Guid, info.ActionFid);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@Detail_Flag", DbType.Boolean, info.DetailFlag);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="ID">MenuActionInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_MENU_ACTION_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">MenuActionInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TS_SYS_MENU_ACTION] WITH(ROWLOCK) "
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
		/// <param name="ID">MenuActionInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TS_SYS_MENU_ACTION] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static MenuActionInfo CreateMenuActionInfo(IDataReader rdr)
		{
			MenuActionInfo info = new MenuActionInfo();
			info.ClientJs = DBConvert.GetString(rdr, rdr.GetOrdinal("CLIENT_JS"));			
			info.ActionOrder = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("ACTION_ORDER"));			
			info.NeedAuth = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("NEED_AUTH"));			
			info.MenuFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("MENU_FID"));			
			info.CreateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.ValidFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			info.ActionFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("ACTION_FID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.DetailFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("Detail_Flag"));			
			return info;
		}
		
		#endregion
	}
}