#region Declaim
//---------------------------------------------------------------------------
// Name:		WmsVmiSupplyPlanDAL
// Function: 	Expose data in table TI_IFM_WMS_VMI_SUPPLY_PLAN from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月24日
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
    /// WmsVmiSupplyPlanDAL对应表[TI_IFM_WMS_VMI_SUPPLY_PLAN]
    /// </summary>
    public partial class WmsVmiSupplyPlanDAL : BusinessObjectProvider<WmsVmiSupplyPlanInfo>
	{
		#region Sql Statements
		private const string TI_IFM_WMS_VMI_SUPPLY_PLAN_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				PART_NO,
				SUPPLIER_NUM,
				PLANT,
				VMI_WM_NO,
				DELIVERY_DATE,
				REQUIRE_QTY,
				COMMENTS,
				PROCESS_FLAG,
				PROCESS_TIME,
				LOG_FID,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE				  
				FROM [LES].[TI_IFM_WMS_VMI_SUPPLY_PLAN] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TI_IFM_WMS_VMI_SUPPLY_PLAN_SELECT = 
			@"SELECT ID,
				FID,
				PART_NO,
				SUPPLIER_NUM,
				PLANT,
				VMI_WM_NO,
				DELIVERY_DATE,
				REQUIRE_QTY,
				COMMENTS,
				PROCESS_FLAG,
				PROCESS_TIME,
				LOG_FID,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE				 
				FROM [LES].[TI_IFM_WMS_VMI_SUPPLY_PLAN] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TI_IFM_WMS_VMI_SUPPLY_PLAN_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TI_IFM_WMS_VMI_SUPPLY_PLAN]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TI_IFM_WMS_VMI_SUPPLY_PLAN_INSERT =
			@"INSERT INTO [LES].[TI_IFM_WMS_VMI_SUPPLY_PLAN] (
				FID,
				PART_NO,
				SUPPLIER_NUM,
				PLANT,
				VMI_WM_NO,
				DELIVERY_DATE,
				REQUIRE_QTY,
				COMMENTS,
				PROCESS_FLAG,
				PROCESS_TIME,
				LOG_FID,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE				 
			) VALUES (
				@FID,
				@PART_NO,
				@SUPPLIER_NUM,
				@PLANT,
				@VMI_WM_NO,
				@DELIVERY_DATE,
				@REQUIRE_QTY,
				@COMMENTS,
				@PROCESS_FLAG,
				@PROCESS_TIME,
				@LOG_FID,
				@VALID_FLAG,
				@CREATE_USER,
				GETDATE(),
				@MODIFY_USER,
				@MODIFY_DATE				 
			);SELECT @@IDENTITY;";
		private const string TI_IFM_WMS_VMI_SUPPLY_PLAN_UPDATE =
			@"UPDATE [LES].[TI_IFM_WMS_VMI_SUPPLY_PLAN] WITH(ROWLOCK) 
				SET FID=@FID,
				PART_NO=@PART_NO,
				SUPPLIER_NUM=@SUPPLIER_NUM,
				PLANT=@PLANT,
				VMI_WM_NO=@VMI_WM_NO,
				DELIVERY_DATE=@DELIVERY_DATE,
				REQUIRE_QTY=@REQUIRE_QTY,
				COMMENTS=@COMMENTS,
				PROCESS_FLAG=@PROCESS_FLAG,
				PROCESS_TIME=@PROCESS_TIME,
				LOG_FID=@LOG_FID,
				VALID_FLAG=@VALID_FLAG,
				CREATE_USER=@CREATE_USER,
				CREATE_DATE=@CREATE_DATE,
				MODIFY_USER=@MODIFY_USER,
				MODIFY_DATE=@MODIFY_DATE				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TI_IFM_WMS_VMI_SUPPLY_PLAN_DELETE =
			@"DELETE FROM [LES].[TI_IFM_WMS_VMI_SUPPLY_PLAN] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get WmsVmiSupplyPlanInfo
		/// </summary>
		/// <param name="ID">WmsVmiSupplyPlanInfo Primary key </param>
		/// <returns></returns> 
		public WmsVmiSupplyPlanInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_WMS_VMI_SUPPLY_PLAN_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateWmsVmiSupplyPlanInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>WmsVmiSupplyPlanInfo Collection </returns>
		public List<WmsVmiSupplyPlanInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TI_IFM_WMS_VMI_SUPPLY_PLAN_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>WmsVmiSupplyPlanInfo Collection </returns>
		public List<WmsVmiSupplyPlanInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<WmsVmiSupplyPlanInfo> list = new List<WmsVmiSupplyPlanInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateWmsVmiSupplyPlanInfo(dr));
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
		public List<WmsVmiSupplyPlanInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TI_IFM_WMS_VMI_SUPPLY_PLAN]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<WmsVmiSupplyPlanInfo> list = new List<WmsVmiSupplyPlanInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateWmsVmiSupplyPlanInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TI_IFM_WMS_VMI_SUPPLY_PLAN_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(WmsVmiSupplyPlanInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_WMS_VMI_SUPPLY_PLAN_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@VMI_WM_NO", DbType.String, info.VmiWmNo);
			db.AddInParameter(dbCommand, "@DELIVERY_DATE", DbType.DateTime, info.DeliveryDate);
			db.AddInParameter(dbCommand, "@REQUIRE_QTY", DbType.Decimal, info.RequireQty);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@PROCESS_FLAG", DbType.Int32, info.ProcessFlag);
			db.AddInParameter(dbCommand, "@PROCESS_TIME", DbType.DateTime, info.ProcessTime);
			db.AddInParameter(dbCommand, "@LOG_FID", DbType.Guid, info.LogFid);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}
		/// <summary>
		/// GetInsertSql
		/// </summary>
		/// <param name="info"> info</param>
		public static string GetInsertSql(WmsVmiSupplyPlanInfo info)
		{
			return  
			@"insert into [LES].[TI_IFM_WMS_VMI_SUPPLY_PLAN] (
				FID,
				PART_NO,
				SUPPLIER_NUM,
				PLANT,
				VMI_WM_NO,
				DELIVERY_DATE,
				REQUIRE_QTY,
				COMMENTS,
				PROCESS_FLAG,
				PROCESS_TIME,
				LOG_FID,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE				 
			) values ("+
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.PartNo) ? "NULL" : "N'" + info.PartNo + "'") + ","+
				(string.IsNullOrEmpty(info.SupplierNum) ? "NULL" : "N'" + info.SupplierNum + "'") + ","+
				(string.IsNullOrEmpty(info.Plant) ? "NULL" : "N'" + info.Plant + "'") + ","+
				(string.IsNullOrEmpty(info.VmiWmNo) ? "NULL" : "N'" + info.VmiWmNo + "'") + ","+
				(info.DeliveryDate == null ? "NULL" : "N'" + info.DeliveryDate.GetValueOrDefault() + "'") + ","+
				(info.RequireQty == null ? "NULL" : "" + info.RequireQty.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.Comments) ? "NULL" : "N'" + info.Comments + "'") + ","+
				(info.ProcessFlag == null ? "NULL" : "" + info.ProcessFlag.GetValueOrDefault() + "") + ","+
				(info.ProcessTime == null ? "NULL" : "N'" + info.ProcessTime.GetValueOrDefault() + "'") + ","+
				(info.LogFid == null ? "NULL" : "N'" + info.LogFid.GetValueOrDefault() + "'") + ","+
				"1" + ","+		
				"N'" + info.CreateUser + "'" + ","+		
				"GETDATE()" + ","+			
				"NULL" + ","+			
				"NULL" + ");";			
				}
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(WmsVmiSupplyPlanInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_WMS_VMI_SUPPLY_PLAN_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@VMI_WM_NO", DbType.String, info.VmiWmNo);
			db.AddInParameter(dbCommand, "@DELIVERY_DATE", DbType.DateTime, info.DeliveryDate);
			db.AddInParameter(dbCommand, "@REQUIRE_QTY", DbType.Decimal, info.RequireQty);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@PROCESS_FLAG", DbType.Int32, info.ProcessFlag);
			db.AddInParameter(dbCommand, "@PROCESS_TIME", DbType.DateTime, info.ProcessTime);
			db.AddInParameter(dbCommand, "@LOG_FID", DbType.Guid, info.LogFid);
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
		/// <param name="ID">WmsVmiSupplyPlanInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_WMS_VMI_SUPPLY_PLAN_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">WmsVmiSupplyPlanInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TI_IFM_WMS_VMI_SUPPLY_PLAN] WITH(ROWLOCK) "
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
		/// <param name="ID">WmsVmiSupplyPlanInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TI_IFM_WMS_VMI_SUPPLY_PLAN] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static WmsVmiSupplyPlanInfo CreateWmsVmiSupplyPlanInfo(IDataReader rdr)
		{
			WmsVmiSupplyPlanInfo info = new WmsVmiSupplyPlanInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.PartNo = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_NO"));			
			info.SupplierNum = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NUM"));			
			info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));			
			info.VmiWmNo = DBConvert.GetString(rdr, rdr.GetOrdinal("VMI_WM_NO"));			
			info.DeliveryDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("DELIVERY_DATE"));			
			info.RequireQty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("REQUIRE_QTY"));			
			info.Comments = DBConvert.GetString(rdr, rdr.GetOrdinal("COMMENTS"));			
			info.ProcessFlag = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PROCESS_FLAG"));			
			info.ProcessTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("PROCESS_TIME"));			
			info.LogFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("LOG_FID"));			
			info.ValidFlag = DBConvert.GetBool(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.CreateDate = DBConvert.GetDateTime(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			return info;
		}
		
		#endregion
	}
}
