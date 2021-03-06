#region Declaim
//---------------------------------------------------------------------------
// Name:		EntityFieldDAL
// Function: 	Expose data in table TS_SYS_ENTITY_FIELD from database as business object to MES system.
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
    /// EntityFieldDAL对应表[TS_SYS_ENTITY_FIELD]
    /// </summary>
    public partial class EntityFieldDAL : BusinessObjectProvider<EntityFieldInfo>
	{
		#region Sql Statements
		private const string TS_SYS_ENTITY_FIELD_SELECT_BY_ID =
			@"SELECT FIELD_NAME,
				TABLE_FIELD_NAME,
				DISPLAY_NAME_CN,
				DISPLAY_NAME_EN,
				DISPLAY_ORDER,
				DATA_TYPE,
				CONTROL_TYPE,
				DATA_LENGTH,
				PRECISION,
				DEFAULT_VALUE,
				NULLENABLE,
				REGEX,
				ERROR_MSG,
				MIN_VALUE,
				MAX_VALUE,
				EDITABLE,
				EDIT_DISPLAY_WIDTH,
				LISTABLE,
				LIST_DISPLAY_WIDTH,
				EXTEND1,
				EXTEND2,
				EXTEND3,
				EDIT_READONLY,
				TAB_TITLE_CODE,
				SORTABLE,
				EXPORT_EXCEL_FLAG,
				EXPORT_EXCEL_ORDER,
				ENTITY_FID,
				VALID_FLAG,
				MODIFY_USER,
				ID,
				CREATE_USER,
				MODIFY_DATE,
				TOOLTIP_HELPER_CN,
				FID,
				CREATE_DATE,
				TOOLTIP_HELPER_EN				  
				FROM [dbo].[TS_SYS_ENTITY_FIELD] WITH(NOLOCK) WHERE [VALID_FLAG] = 1 AND ID =@ID;";
			
		private const string TS_SYS_ENTITY_FIELD_SELECT = 
			@"SELECT FIELD_NAME,
				TABLE_FIELD_NAME,
				DISPLAY_NAME_CN,
				DISPLAY_NAME_EN,
				DISPLAY_ORDER,
				DATA_TYPE,
				CONTROL_TYPE,
				DATA_LENGTH,
				PRECISION,
				DEFAULT_VALUE,
				NULLENABLE,
				REGEX,
				ERROR_MSG,
				MIN_VALUE,
				MAX_VALUE,
				EDITABLE,
				EDIT_DISPLAY_WIDTH,
				LISTABLE,
				LIST_DISPLAY_WIDTH,
				EXTEND1,
				EXTEND2,
				EXTEND3,
				EDIT_READONLY,
				TAB_TITLE_CODE,
				SORTABLE,
				EXPORT_EXCEL_FLAG,
				EXPORT_EXCEL_ORDER,
				ENTITY_FID,
				VALID_FLAG,
				MODIFY_USER,
				ID,
				CREATE_USER,
				MODIFY_DATE,
				TOOLTIP_HELPER_CN,
				FID,
				CREATE_DATE,
				TOOLTIP_HELPER_EN				 
				FROM [dbo].[TS_SYS_ENTITY_FIELD] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TS_SYS_ENTITY_FIELD_SELECT_COUNTS = 
			@"SELECT count(*) FROM [dbo].[TS_SYS_ENTITY_FIELD]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TS_SYS_ENTITY_FIELD_INSERT =
			@"INSERT INTO [dbo].[TS_SYS_ENTITY_FIELD] (
				FIELD_NAME,
				TABLE_FIELD_NAME,
				DISPLAY_NAME_CN,
				DISPLAY_NAME_EN,
				DISPLAY_ORDER,
				DATA_TYPE,
				CONTROL_TYPE,
				DATA_LENGTH,
				PRECISION,
				DEFAULT_VALUE,
				NULLENABLE,
				REGEX,
				ERROR_MSG,
				MIN_VALUE,
				MAX_VALUE,
				EDITABLE,
				EDIT_DISPLAY_WIDTH,
				LISTABLE,
				LIST_DISPLAY_WIDTH,
				EXTEND1,
				EXTEND2,
				EXTEND3,
				EDIT_READONLY,
				TAB_TITLE_CODE,
				SORTABLE,
				EXPORT_EXCEL_FLAG,
				EXPORT_EXCEL_ORDER,
				ENTITY_FID,
				VALID_FLAG,
				MODIFY_USER,
				CREATE_USER,
				MODIFY_DATE,
				TOOLTIP_HELPER_CN,
				FID,
				CREATE_DATE,
				TOOLTIP_HELPER_EN				 
			) VALUES (
				@FIELD_NAME,
				@TABLE_FIELD_NAME,
				@DISPLAY_NAME_CN,
				@DISPLAY_NAME_EN,
				@DISPLAY_ORDER,
				@DATA_TYPE,
				@CONTROL_TYPE,
				@DATA_LENGTH,
				@PRECISION,
				@DEFAULT_VALUE,
				@NULLENABLE,
				@REGEX,
				@ERROR_MSG,
				@MIN_VALUE,
				@MAX_VALUE,
				@EDITABLE,
				@EDIT_DISPLAY_WIDTH,
				@LISTABLE,
				@LIST_DISPLAY_WIDTH,
				@EXTEND1,
				@EXTEND2,
				@EXTEND3,
				@EDIT_READONLY,
				@TAB_TITLE_CODE,
				@SORTABLE,
				@EXPORT_EXCEL_FLAG,
				@EXPORT_EXCEL_ORDER,
				@ENTITY_FID,
				@VALID_FLAG,
				@MODIFY_USER,
				@CREATE_USER,
				@MODIFY_DATE,
				@TOOLTIP_HELPER_CN,
				@FID,
				@CREATE_DATE,
				@TOOLTIP_HELPER_EN				 
			);SELECT @@IDENTITY;";
		private const string TS_SYS_ENTITY_FIELD_UPDATE =
			@"UPDATE [dbo].[TS_SYS_ENTITY_FIELD] WITH(ROWLOCK) 
				SET FIELD_NAME=@FIELD_NAME,
				TABLE_FIELD_NAME=@TABLE_FIELD_NAME,
				DISPLAY_NAME_CN=@DISPLAY_NAME_CN,
				DISPLAY_NAME_EN=@DISPLAY_NAME_EN,
				DISPLAY_ORDER=@DISPLAY_ORDER,
				DATA_TYPE=@DATA_TYPE,
				CONTROL_TYPE=@CONTROL_TYPE,
				DATA_LENGTH=@DATA_LENGTH,
				PRECISION=@PRECISION,
				DEFAULT_VALUE=@DEFAULT_VALUE,
				NULLENABLE=@NULLENABLE,
				REGEX=@REGEX,
				ERROR_MSG=@ERROR_MSG,
				MIN_VALUE=@MIN_VALUE,
				MAX_VALUE=@MAX_VALUE,
				EDITABLE=@EDITABLE,
				EDIT_DISPLAY_WIDTH=@EDIT_DISPLAY_WIDTH,
				LISTABLE=@LISTABLE,
				LIST_DISPLAY_WIDTH=@LIST_DISPLAY_WIDTH,
				EXTEND1=@EXTEND1,
				EXTEND2=@EXTEND2,
				EXTEND3=@EXTEND3,
				EDIT_READONLY=@EDIT_READONLY,
				TAB_TITLE_CODE=@TAB_TITLE_CODE,
				SORTABLE=@SORTABLE,
				EXPORT_EXCEL_FLAG=@EXPORT_EXCEL_FLAG,
				EXPORT_EXCEL_ORDER=@EXPORT_EXCEL_ORDER,
				ENTITY_FID=@ENTITY_FID,
				VALID_FLAG=@VALID_FLAG,
				MODIFY_USER=@MODIFY_USER,
				CREATE_USER=@CREATE_USER,
				MODIFY_DATE=@MODIFY_DATE,
				TOOLTIP_HELPER_CN=@TOOLTIP_HELPER_CN,
				FID=@FID,
				CREATE_DATE=@CREATE_DATE,
				TOOLTIP_HELPER_EN=@TOOLTIP_HELPER_EN				 
				WHERE [VALID_FLAG] = 1 AND ID =@ID;";

		private const string TS_SYS_ENTITY_FIELD_DELETE =
			@"DELETE FROM [dbo].[TS_SYS_ENTITY_FIELD] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1 AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get EntityFieldInfo
		/// </summary>
		/// <param name="ID">EntityFieldInfo Primary key </param>
		/// <returns></returns> 
		public EntityFieldInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_ENTITY_FIELD_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateEntityFieldInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>EntityFieldInfo Collection </returns>
		public List<EntityFieldInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TS_SYS_ENTITY_FIELD_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>EntityFieldInfo Collection </returns>
		public List<EntityFieldInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<EntityFieldInfo> list = new List<EntityFieldInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateEntityFieldInfo(dr));
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
		public List<EntityFieldInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [dbo].[TS_SYS_ENTITY_FIELD]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<EntityFieldInfo> list = new List<EntityFieldInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateEntityFieldInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TS_SYS_ENTITY_FIELD_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(EntityFieldInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_ENTITY_FIELD_INSERT);			
			db.AddInParameter(dbCommand, "@FIELD_NAME", DbType.String, info.FieldName);
			db.AddInParameter(dbCommand, "@TABLE_FIELD_NAME", DbType.String, info.TableFieldName);
			db.AddInParameter(dbCommand, "@DISPLAY_NAME_CN", DbType.String, info.DisplayNameCn);
			db.AddInParameter(dbCommand, "@DISPLAY_NAME_EN", DbType.String, info.DisplayNameEn);
			db.AddInParameter(dbCommand, "@DISPLAY_ORDER", DbType.Int32, info.DisplayOrder);
			db.AddInParameter(dbCommand, "@DATA_TYPE", DbType.Int32, info.DataType);
			db.AddInParameter(dbCommand, "@CONTROL_TYPE", DbType.Int32, info.ControlType);
			db.AddInParameter(dbCommand, "@DATA_LENGTH", DbType.Int32, info.DataLength);
			db.AddInParameter(dbCommand, "@PRECISION", DbType.Int32, info.Precision);
			db.AddInParameter(dbCommand, "@DEFAULT_VALUE", DbType.String, info.DefaultValue);
			db.AddInParameter(dbCommand, "@NULLENABLE", DbType.Boolean, info.Nullenable);
			db.AddInParameter(dbCommand, "@REGEX", DbType.String, info.Regex);
			db.AddInParameter(dbCommand, "@ERROR_MSG", DbType.String, info.ErrorMsg);
			db.AddInParameter(dbCommand, "@MIN_VALUE", DbType.Int32, info.MinValue);
			db.AddInParameter(dbCommand, "@MAX_VALUE", DbType.Int32, info.MaxValue);
			db.AddInParameter(dbCommand, "@EDITABLE", DbType.Boolean, info.Editable);
			db.AddInParameter(dbCommand, "@EDIT_DISPLAY_WIDTH", DbType.String, info.EditDisplayWidth);
			db.AddInParameter(dbCommand, "@LISTABLE", DbType.Boolean, info.Listable);
			db.AddInParameter(dbCommand, "@LIST_DISPLAY_WIDTH", DbType.String, info.ListDisplayWidth);
			db.AddInParameter(dbCommand, "@EXTEND1", DbType.String, info.Extend1);
			db.AddInParameter(dbCommand, "@EXTEND2", DbType.String, info.Extend2);
			db.AddInParameter(dbCommand, "@EXTEND3", DbType.String, info.Extend3);
			db.AddInParameter(dbCommand, "@EDIT_READONLY", DbType.Int32, info.EditReadonly);
			db.AddInParameter(dbCommand, "@TAB_TITLE_CODE", DbType.String, info.TabTitleCode);
			db.AddInParameter(dbCommand, "@SORTABLE", DbType.Boolean, info.Sortable);
			db.AddInParameter(dbCommand, "@EXPORT_EXCEL_FLAG", DbType.Boolean, info.ExportExcelFlag);
			db.AddInParameter(dbCommand, "@EXPORT_EXCEL_ORDER", DbType.Int32, info.ExportExcelOrder);
			db.AddInParameter(dbCommand, "@ENTITY_FID", DbType.Guid, info.EntityFid);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@TOOLTIP_HELPER_CN", DbType.String, info.TooltipHelperCn);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@TOOLTIP_HELPER_EN", DbType.String, info.TooltipHelperEn);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}		
		/// <summary>
		/// GetInsertSql
		/// </summary>
		/// <param name="info"> info</param>
		public static string GetInsertSql(EntityFieldInfo info)
		{
			return  
			@"insert into [dbo].[TS_SYS_ENTITY_FIELD] (
				FIELD_NAME,
				TABLE_FIELD_NAME,
				DISPLAY_NAME_CN,
				DISPLAY_NAME_EN,
				DISPLAY_ORDER,
				DATA_TYPE,
				CONTROL_TYPE,
				DATA_LENGTH,
				PRECISION,
				DEFAULT_VALUE,
				NULLENABLE,
				REGEX,
				ERROR_MSG,
				MIN_VALUE,
				MAX_VALUE,
				EDITABLE,
				EDIT_DISPLAY_WIDTH,
				LISTABLE,
				LIST_DISPLAY_WIDTH,
				EXTEND1,
				EXTEND2,
				EXTEND3,
				EDIT_READONLY,
				TAB_TITLE_CODE,
				SORTABLE,
				EXPORT_EXCEL_FLAG,
				EXPORT_EXCEL_ORDER,
				ENTITY_FID,
				VALID_FLAG,
				MODIFY_USER,
				CREATE_USER,
				MODIFY_DATE,
				TOOLTIP_HELPER_CN,
				FID,
				CREATE_DATE,
				TOOLTIP_HELPER_EN				 
			) values ("+
				(string.IsNullOrEmpty(info.FieldName) ? "NULL" : "N'" + info.FieldName + "'") + ","+
				(string.IsNullOrEmpty(info.TableFieldName) ? "NULL" : "N'" + info.TableFieldName + "'") + ","+
				(string.IsNullOrEmpty(info.DisplayNameCn) ? "NULL" : "N'" + info.DisplayNameCn + "'") + ","+
				(string.IsNullOrEmpty(info.DisplayNameEn) ? "NULL" : "N'" + info.DisplayNameEn + "'") + ","+
				(info.DisplayOrder == null ? "NULL" : "" + info.DisplayOrder.GetValueOrDefault() + "") + ","+
				(info.DataType == null ? "NULL" : "" + info.DataType.GetValueOrDefault() + "") + ","+
				(info.ControlType == null ? "NULL" : "" + info.ControlType.GetValueOrDefault() + "") + ","+
				(info.DataLength == null ? "NULL" : "" + info.DataLength.GetValueOrDefault() + "") + ","+
				(info.Precision == null ? "NULL" : "" + info.Precision.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.DefaultValue) ? "NULL" : "N'" + info.DefaultValue + "'") + ","+
				(info.Nullenable == null ? "NULL" : "" + (info.Nullenable.GetValueOrDefault() ? "1" : "0") + "") + ","+
				(string.IsNullOrEmpty(info.Regex) ? "NULL" : "N'" + info.Regex + "'") + ","+
				(string.IsNullOrEmpty(info.ErrorMsg) ? "NULL" : "N'" + info.ErrorMsg + "'") + ","+
				(info.MinValue == null ? "NULL" : "" + info.MinValue.GetValueOrDefault() + "") + ","+
				(info.MaxValue == null ? "NULL" : "" + info.MaxValue.GetValueOrDefault() + "") + ","+
				(info.Editable == null ? "NULL" : "" + (info.Editable.GetValueOrDefault() ? "1" : "0") + "") + ","+
				(string.IsNullOrEmpty(info.EditDisplayWidth) ? "NULL" : "N'" + info.EditDisplayWidth + "'") + ","+
				(info.Listable == null ? "NULL" : "" + (info.Listable.GetValueOrDefault() ? "1" : "0") + "") + ","+
				(string.IsNullOrEmpty(info.ListDisplayWidth) ? "NULL" : "N'" + info.ListDisplayWidth + "'") + ","+
				(string.IsNullOrEmpty(info.Extend1) ? "NULL" : "N'" + info.Extend1 + "'") + ","+
				(string.IsNullOrEmpty(info.Extend2) ? "NULL" : "N'" + info.Extend2 + "'") + ","+
				(string.IsNullOrEmpty(info.Extend3) ? "NULL" : "N'" + info.Extend3 + "'") + ","+
				(info.EditReadonly == null ? "NULL" : "" + info.EditReadonly.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.TabTitleCode) ? "NULL" : "N'" + info.TabTitleCode + "'") + ","+
				(info.Sortable == null ? "NULL" : "" + (info.Sortable.GetValueOrDefault() ? "1" : "0") + "") + ","+
				(info.ExportExcelFlag == null ? "NULL" : "" + (info.ExportExcelFlag.GetValueOrDefault() ? "1" : "0") + "") + ","+
				(info.ExportExcelOrder == null ? "NULL" : "" + info.ExportExcelOrder.GetValueOrDefault() + "") + ","+
				(info.EntityFid == null ? "NULL" : "N'" + info.EntityFid.GetValueOrDefault() + "'") + ","+
				"1" + ","+		
				"NULL" + ","+			
				"N'" + info.CreateUser + "'" + ","+		
				"NULL" + ","+			
				(string.IsNullOrEmpty(info.TooltipHelperCn) ? "NULL" : "N'" + info.TooltipHelperCn + "'") + ","+
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				"GETDATE()" + ","+			
				(string.IsNullOrEmpty(info.TooltipHelperEn) ? "NULL" : "N'" + info.TooltipHelperEn + "'") + ");";				}
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(EntityFieldInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_ENTITY_FIELD_UPDATE);				
			db.AddInParameter(dbCommand, "@FIELD_NAME", DbType.String, info.FieldName);
			db.AddInParameter(dbCommand, "@TABLE_FIELD_NAME", DbType.String, info.TableFieldName);
			db.AddInParameter(dbCommand, "@DISPLAY_NAME_CN", DbType.String, info.DisplayNameCn);
			db.AddInParameter(dbCommand, "@DISPLAY_NAME_EN", DbType.String, info.DisplayNameEn);
			db.AddInParameter(dbCommand, "@DISPLAY_ORDER", DbType.Int32, info.DisplayOrder);
			db.AddInParameter(dbCommand, "@DATA_TYPE", DbType.Int32, info.DataType);
			db.AddInParameter(dbCommand, "@CONTROL_TYPE", DbType.Int32, info.ControlType);
			db.AddInParameter(dbCommand, "@DATA_LENGTH", DbType.Int32, info.DataLength);
			db.AddInParameter(dbCommand, "@PRECISION", DbType.Int32, info.Precision);
			db.AddInParameter(dbCommand, "@DEFAULT_VALUE", DbType.String, info.DefaultValue);
			db.AddInParameter(dbCommand, "@NULLENABLE", DbType.Boolean, info.Nullenable);
			db.AddInParameter(dbCommand, "@REGEX", DbType.String, info.Regex);
			db.AddInParameter(dbCommand, "@ERROR_MSG", DbType.String, info.ErrorMsg);
			db.AddInParameter(dbCommand, "@MIN_VALUE", DbType.Int32, info.MinValue);
			db.AddInParameter(dbCommand, "@MAX_VALUE", DbType.Int32, info.MaxValue);
			db.AddInParameter(dbCommand, "@EDITABLE", DbType.Boolean, info.Editable);
			db.AddInParameter(dbCommand, "@EDIT_DISPLAY_WIDTH", DbType.String, info.EditDisplayWidth);
			db.AddInParameter(dbCommand, "@LISTABLE", DbType.Boolean, info.Listable);
			db.AddInParameter(dbCommand, "@LIST_DISPLAY_WIDTH", DbType.String, info.ListDisplayWidth);
			db.AddInParameter(dbCommand, "@EXTEND1", DbType.String, info.Extend1);
			db.AddInParameter(dbCommand, "@EXTEND2", DbType.String, info.Extend2);
			db.AddInParameter(dbCommand, "@EXTEND3", DbType.String, info.Extend3);
			db.AddInParameter(dbCommand, "@EDIT_READONLY", DbType.Int32, info.EditReadonly);
			db.AddInParameter(dbCommand, "@TAB_TITLE_CODE", DbType.String, info.TabTitleCode);
			db.AddInParameter(dbCommand, "@SORTABLE", DbType.Boolean, info.Sortable);
			db.AddInParameter(dbCommand, "@EXPORT_EXCEL_FLAG", DbType.Boolean, info.ExportExcelFlag);
			db.AddInParameter(dbCommand, "@EXPORT_EXCEL_ORDER", DbType.Int32, info.ExportExcelOrder);
			db.AddInParameter(dbCommand, "@ENTITY_FID", DbType.Guid, info.EntityFid);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@TOOLTIP_HELPER_CN", DbType.String, info.TooltipHelperCn);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@TOOLTIP_HELPER_EN", DbType.String, info.TooltipHelperEn);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="ID">EntityFieldInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_ENTITY_FIELD_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">EntityFieldInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [dbo].[TS_SYS_ENTITY_FIELD] WITH(ROWLOCK) "
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
		/// <param name="ID">EntityFieldInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [dbo].[TS_SYS_ENTITY_FIELD] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1 AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static EntityFieldInfo CreateEntityFieldInfo(IDataReader rdr)
		{
			EntityFieldInfo info = new EntityFieldInfo();
			info.FieldName = DBConvert.GetString(rdr, rdr.GetOrdinal("FIELD_NAME"));			
			info.TableFieldName = DBConvert.GetString(rdr, rdr.GetOrdinal("TABLE_FIELD_NAME"));			
			info.DisplayNameCn = DBConvert.GetString(rdr, rdr.GetOrdinal("DISPLAY_NAME_CN"));			
			info.DisplayNameEn = DBConvert.GetString(rdr, rdr.GetOrdinal("DISPLAY_NAME_EN"));			
			info.DisplayOrder = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("DISPLAY_ORDER"));			
			info.DataType = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("DATA_TYPE"));			
			info.ControlType = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("CONTROL_TYPE"));			
			info.DataLength = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("DATA_LENGTH"));			
			info.Precision = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PRECISION"));			
			info.DefaultValue = DBConvert.GetString(rdr, rdr.GetOrdinal("DEFAULT_VALUE"));			
			info.Nullenable = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("NULLENABLE"));			
			info.Regex = DBConvert.GetString(rdr, rdr.GetOrdinal("REGEX"));			
			info.ErrorMsg = DBConvert.GetString(rdr, rdr.GetOrdinal("ERROR_MSG"));			
			info.MinValue = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("MIN_VALUE"));			
			info.MaxValue = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("MAX_VALUE"));			
			info.Editable = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("EDITABLE"));			
			info.EditDisplayWidth = DBConvert.GetString(rdr, rdr.GetOrdinal("EDIT_DISPLAY_WIDTH"));			
			info.Listable = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("LISTABLE"));			
			info.ListDisplayWidth = DBConvert.GetString(rdr, rdr.GetOrdinal("LIST_DISPLAY_WIDTH"));			
			info.Extend1 = DBConvert.GetString(rdr, rdr.GetOrdinal("EXTEND1"));			
			info.Extend2 = DBConvert.GetString(rdr, rdr.GetOrdinal("EXTEND2"));			
			info.Extend3 = DBConvert.GetString(rdr, rdr.GetOrdinal("EXTEND3"));			
			info.EditReadonly = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("EDIT_READONLY"));			
			info.TabTitleCode = DBConvert.GetString(rdr, rdr.GetOrdinal("TAB_TITLE_CODE"));			
			info.Sortable = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("SORTABLE"));			
			info.ExportExcelFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("EXPORT_EXCEL_FLAG"));			
			info.ExportExcelOrder = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("EXPORT_EXCEL_ORDER"));			
			info.EntityFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("ENTITY_FID"));			
			info.ValidFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			info.TooltipHelperCn = DBConvert.GetString(rdr, rdr.GetOrdinal("TOOLTIP_HELPER_CN"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.CreateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.TooltipHelperEn = DBConvert.GetString(rdr, rdr.GetOrdinal("TOOLTIP_HELPER_EN"));			
			return info;
		}
		
		#endregion
	}
}
