#region Declaim
//---------------------------------------------------------------------------
// Name:		VmiPrevSupplyPlanDAL
// Function: 	Expose data in table TE_ATP_VMI_PREV_SUPPLY_PLAN from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年6月28日
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
    /// VmiPrevSupplyPlanDAL对应表[TE_ATP_VMI_PREV_SUPPLY_PLAN]
    /// </summary>
    public partial class VmiPrevSupplyPlanDAL : BusinessObjectProvider<VmiPrevSupplyPlanInfo>
	{
		#region Sql Statements
		private const string TE_ATP_VMI_PREV_SUPPLY_PLAN_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				PART_NO,
				SUPPLIER_NUM,
				PLANT,
				VMI_WM_NO,
				DELIVERY_DATE,
				REQUIRE_QTY,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE				  
				FROM [LES].[TE_ATP_VMI_PREV_SUPPLY_PLAN] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TE_ATP_VMI_PREV_SUPPLY_PLAN_SELECT = 
			@"SELECT ID,
				FID,
				PART_NO,
				SUPPLIER_NUM,
				PLANT,
				VMI_WM_NO,
				DELIVERY_DATE,
				REQUIRE_QTY,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE				 
				FROM [LES].[TE_ATP_VMI_PREV_SUPPLY_PLAN] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TE_ATP_VMI_PREV_SUPPLY_PLAN_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TE_ATP_VMI_PREV_SUPPLY_PLAN]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TE_ATP_VMI_PREV_SUPPLY_PLAN_INSERT =
			@"INSERT INTO [LES].[TE_ATP_VMI_PREV_SUPPLY_PLAN] (
				FID,
				PART_NO,
				SUPPLIER_NUM,
				PLANT,
				VMI_WM_NO,
				DELIVERY_DATE,
				REQUIRE_QTY,
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
				@VALID_FLAG,
				@CREATE_USER,
				GETDATE(),
				@MODIFY_USER,
				@MODIFY_DATE				 
			);SELECT @@IDENTITY;";
		private const string TE_ATP_VMI_PREV_SUPPLY_PLAN_UPDATE =
			@"UPDATE [LES].[TE_ATP_VMI_PREV_SUPPLY_PLAN] WITH(ROWLOCK) 
				SET FID=@FID,
				PART_NO=@PART_NO,
				SUPPLIER_NUM=@SUPPLIER_NUM,
				PLANT=@PLANT,
				VMI_WM_NO=@VMI_WM_NO,
				DELIVERY_DATE=@DELIVERY_DATE,
				REQUIRE_QTY=@REQUIRE_QTY,
				VALID_FLAG=@VALID_FLAG,
				CREATE_USER=@CREATE_USER,
				CREATE_DATE=@CREATE_DATE,
				MODIFY_USER=@MODIFY_USER,
				MODIFY_DATE=@MODIFY_DATE				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TE_ATP_VMI_PREV_SUPPLY_PLAN_DELETE =
			@"DELETE FROM [LES].[TE_ATP_VMI_PREV_SUPPLY_PLAN] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get VmiPrevSupplyPlanInfo
		/// </summary>
		/// <param name="ID">VmiPrevSupplyPlanInfo Primary key </param>
		/// <returns></returns> 
		public VmiPrevSupplyPlanInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TE_ATP_VMI_PREV_SUPPLY_PLAN_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateVmiPrevSupplyPlanInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>VmiPrevSupplyPlanInfo Collection </returns>
		public List<VmiPrevSupplyPlanInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TE_ATP_VMI_PREV_SUPPLY_PLAN_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>VmiPrevSupplyPlanInfo Collection </returns>
		public List<VmiPrevSupplyPlanInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<VmiPrevSupplyPlanInfo> list = new List<VmiPrevSupplyPlanInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateVmiPrevSupplyPlanInfo(dr));
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
		public List<VmiPrevSupplyPlanInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TE_ATP_VMI_PREV_SUPPLY_PLAN]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<VmiPrevSupplyPlanInfo> list = new List<VmiPrevSupplyPlanInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateVmiPrevSupplyPlanInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TE_ATP_VMI_PREV_SUPPLY_PLAN_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(VmiPrevSupplyPlanInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TE_ATP_VMI_PREV_SUPPLY_PLAN_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@VMI_WM_NO", DbType.String, info.VmiWmNo);
			db.AddInParameter(dbCommand, "@DELIVERY_DATE", DbType.DateTime, info.DeliveryDate);
			db.AddInParameter(dbCommand, "@REQUIRE_QTY", DbType.Decimal, info.RequireQty);
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
		public int Update(VmiPrevSupplyPlanInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TE_ATP_VMI_PREV_SUPPLY_PLAN_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@VMI_WM_NO", DbType.String, info.VmiWmNo);
			db.AddInParameter(dbCommand, "@DELIVERY_DATE", DbType.DateTime, info.DeliveryDate);
			db.AddInParameter(dbCommand, "@REQUIRE_QTY", DbType.Decimal, info.RequireQty);
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
		/// <param name="ID">VmiPrevSupplyPlanInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TE_ATP_VMI_PREV_SUPPLY_PLAN_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">VmiPrevSupplyPlanInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TE_ATP_VMI_PREV_SUPPLY_PLAN] WITH(ROWLOCK) "
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
		/// <param name="ID">VmiPrevSupplyPlanInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TE_ATP_VMI_PREV_SUPPLY_PLAN] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static VmiPrevSupplyPlanInfo CreateVmiPrevSupplyPlanInfo(IDataReader rdr)
		{
			VmiPrevSupplyPlanInfo info = new VmiPrevSupplyPlanInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.PartNo = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_NO"));			
			info.SupplierNum = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NUM"));			
			info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));			
			info.VmiWmNo = DBConvert.GetString(rdr, rdr.GetOrdinal("VMI_WM_NO"));			
			info.DeliveryDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("DELIVERY_DATE"));			
			info.RequireQty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("REQUIRE_QTY"));			
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
