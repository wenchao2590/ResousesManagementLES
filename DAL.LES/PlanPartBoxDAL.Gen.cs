#region Declaim
//---------------------------------------------------------------------------
// Name:		PlanPartBoxDAL
// Function: 	Expose data in table TM_MPM_PLAN_PART_BOX from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月5日
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
    /// PlanPartBoxDAL对应表[TM_MPM_PLAN_PART_BOX]
    /// </summary>
    public partial class PlanPartBoxDAL : BusinessObjectProvider<PlanPartBoxInfo>
	{
		#region Sql Statements
		private const string TM_MPM_PLAN_PART_BOX_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				PART_BOX_CODE,
				PART_BOX_NAME,
				SUPPLIER_NUM,
				SUPPLIER_NAME,
				PLANT,
				WORKSHOP,
				ASSEMBLY_LINE,
				SOURCE_ZONE_NO,
				SOURCE_WM_NO,
				TARGET_ZONE_NO,
				TARGET_WM_NO,
				PICK_UP_TIME,
				DELIVERY_TIME,
				DELAY_TIME,
				DOCK,
				STATUS,
				COMMENTS,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE				  
				FROM [LES].[TM_MPM_PLAN_PART_BOX] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TM_MPM_PLAN_PART_BOX_SELECT = 
			@"SELECT ID,
				FID,
				PART_BOX_CODE,
				PART_BOX_NAME,
				SUPPLIER_NUM,
				SUPPLIER_NAME,
				PLANT,
				WORKSHOP,
				ASSEMBLY_LINE,
				SOURCE_ZONE_NO,
				SOURCE_WM_NO,
				TARGET_ZONE_NO,
				TARGET_WM_NO,
				PICK_UP_TIME,
				DELIVERY_TIME,
				DELAY_TIME,
				DOCK,
				STATUS,
				COMMENTS,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE				 
				FROM [LES].[TM_MPM_PLAN_PART_BOX] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TM_MPM_PLAN_PART_BOX_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TM_MPM_PLAN_PART_BOX]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TM_MPM_PLAN_PART_BOX_INSERT =
			@"INSERT INTO [LES].[TM_MPM_PLAN_PART_BOX] (
				FID,
				PART_BOX_CODE,
				PART_BOX_NAME,
				SUPPLIER_NUM,
				SUPPLIER_NAME,
				PLANT,
				WORKSHOP,
				ASSEMBLY_LINE,
				SOURCE_ZONE_NO,
				SOURCE_WM_NO,
				TARGET_ZONE_NO,
				TARGET_WM_NO,
				PICK_UP_TIME,
				DELIVERY_TIME,
				DELAY_TIME,
				DOCK,
				STATUS,
				COMMENTS,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE				 
			) VALUES (
				@FID,
				@PART_BOX_CODE,
				@PART_BOX_NAME,
				@SUPPLIER_NUM,
				@SUPPLIER_NAME,
				@PLANT,
				@WORKSHOP,
				@ASSEMBLY_LINE,
				@SOURCE_ZONE_NO,
				@SOURCE_WM_NO,
				@TARGET_ZONE_NO,
				@TARGET_WM_NO,
				@PICK_UP_TIME,
				@DELIVERY_TIME,
				@DELAY_TIME,
				@DOCK,
				@STATUS,
				@COMMENTS,
				@VALID_FLAG,
				@CREATE_USER,
				GETDATE(),
				@MODIFY_USER,
				@MODIFY_DATE				 
			);SELECT @@IDENTITY;";
		private const string TM_MPM_PLAN_PART_BOX_UPDATE =
			@"UPDATE [LES].[TM_MPM_PLAN_PART_BOX] WITH(ROWLOCK) 
				SET FID=@FID,
				PART_BOX_CODE=@PART_BOX_CODE,
				PART_BOX_NAME=@PART_BOX_NAME,
				SUPPLIER_NUM=@SUPPLIER_NUM,
				SUPPLIER_NAME=@SUPPLIER_NAME,
				PLANT=@PLANT,
				WORKSHOP=@WORKSHOP,
				ASSEMBLY_LINE=@ASSEMBLY_LINE,
				SOURCE_ZONE_NO=@SOURCE_ZONE_NO,
				SOURCE_WM_NO=@SOURCE_WM_NO,
				TARGET_ZONE_NO=@TARGET_ZONE_NO,
				TARGET_WM_NO=@TARGET_WM_NO,
				PICK_UP_TIME=@PICK_UP_TIME,
				DELIVERY_TIME=@DELIVERY_TIME,
				DELAY_TIME=@DELAY_TIME,
				DOCK=@DOCK,
				STATUS=@STATUS,
				COMMENTS=@COMMENTS,
				VALID_FLAG=@VALID_FLAG,
				CREATE_USER=@CREATE_USER,
				CREATE_DATE=@CREATE_DATE,
				MODIFY_USER=@MODIFY_USER,
				MODIFY_DATE=@MODIFY_DATE				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TM_MPM_PLAN_PART_BOX_DELETE =
			@"DELETE FROM [LES].[TM_MPM_PLAN_PART_BOX] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get PlanPartBoxInfo
		/// </summary>
		/// <param name="ID">PlanPartBoxInfo Primary key </param>
		/// <returns></returns> 
		public PlanPartBoxInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_MPM_PLAN_PART_BOX_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreatePlanPartBoxInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>PlanPartBoxInfo Collection </returns>
		public List<PlanPartBoxInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TM_MPM_PLAN_PART_BOX_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>PlanPartBoxInfo Collection </returns>
		public List<PlanPartBoxInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<PlanPartBoxInfo> list = new List<PlanPartBoxInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreatePlanPartBoxInfo(dr));
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
		public List<PlanPartBoxInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TM_MPM_PLAN_PART_BOX]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<PlanPartBoxInfo> list = new List<PlanPartBoxInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreatePlanPartBoxInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TM_MPM_PLAN_PART_BOX_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(PlanPartBoxInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TM_MPM_PLAN_PART_BOX_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@PART_BOX_CODE", DbType.String, info.PartBoxCode);
			db.AddInParameter(dbCommand, "@PART_BOX_NAME", DbType.String, info.PartBoxName);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@SUPPLIER_NAME", DbType.String, info.SupplierName);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@SOURCE_ZONE_NO", DbType.String, info.SourceZoneNo);
			db.AddInParameter(dbCommand, "@SOURCE_WM_NO", DbType.String, info.SourceWmNo);
			db.AddInParameter(dbCommand, "@TARGET_ZONE_NO", DbType.String, info.TargetZoneNo);
			db.AddInParameter(dbCommand, "@TARGET_WM_NO", DbType.String, info.TargetWmNo);
			db.AddInParameter(dbCommand, "@PICK_UP_TIME", DbType.Int32, info.PickUpTime);
			db.AddInParameter(dbCommand, "@DELIVERY_TIME", DbType.Int32, info.DeliveryTime);
			db.AddInParameter(dbCommand, "@DELAY_TIME", DbType.Int32, info.DelayTime);
			db.AddInParameter(dbCommand, "@DOCK", DbType.String, info.Dock);
			db.AddInParameter(dbCommand, "@STATUS", DbType.Int32, info.Status);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
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
		public static string GetInsertSql(PlanPartBoxInfo info)
		{
			return  
			@"insert into [LES].[TM_MPM_PLAN_PART_BOX] (
				FID,
				PART_BOX_CODE,
				PART_BOX_NAME,
				SUPPLIER_NUM,
				SUPPLIER_NAME,
				PLANT,
				WORKSHOP,
				ASSEMBLY_LINE,
				SOURCE_ZONE_NO,
				SOURCE_WM_NO,
				TARGET_ZONE_NO,
				TARGET_WM_NO,
				PICK_UP_TIME,
				DELIVERY_TIME,
				DELAY_TIME,
				DOCK,
				STATUS,
				COMMENTS,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE				 
			) values ("+
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.PartBoxCode) ? "NULL" : "N'" + info.PartBoxCode + "'") + ","+
				(string.IsNullOrEmpty(info.PartBoxName) ? "NULL" : "N'" + info.PartBoxName + "'") + ","+
				(string.IsNullOrEmpty(info.SupplierNum) ? "NULL" : "N'" + info.SupplierNum + "'") + ","+
				(string.IsNullOrEmpty(info.SupplierName) ? "NULL" : "N'" + info.SupplierName + "'") + ","+
				(string.IsNullOrEmpty(info.Plant) ? "NULL" : "N'" + info.Plant + "'") + ","+
				(string.IsNullOrEmpty(info.Workshop) ? "NULL" : "N'" + info.Workshop + "'") + ","+
				(string.IsNullOrEmpty(info.AssemblyLine) ? "NULL" : "N'" + info.AssemblyLine + "'") + ","+
				(string.IsNullOrEmpty(info.SourceZoneNo) ? "NULL" : "N'" + info.SourceZoneNo + "'") + ","+
				(string.IsNullOrEmpty(info.SourceWmNo) ? "NULL" : "N'" + info.SourceWmNo + "'") + ","+
				(string.IsNullOrEmpty(info.TargetZoneNo) ? "NULL" : "N'" + info.TargetZoneNo + "'") + ","+
				(string.IsNullOrEmpty(info.TargetWmNo) ? "NULL" : "N'" + info.TargetWmNo + "'") + ","+
				(info.PickUpTime == null ? "NULL" : "" + info.PickUpTime.GetValueOrDefault() + "") + ","+
				(info.DeliveryTime == null ? "NULL" : "" + info.DeliveryTime.GetValueOrDefault() + "") + ","+
				(info.DelayTime == null ? "NULL" : "" + info.DelayTime.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.Dock) ? "NULL" : "N'" + info.Dock + "'") + ","+
				(info.Status == null ? "NULL" : "" + info.Status.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.Comments) ? "NULL" : "N'" + info.Comments + "'") + ","+
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
		public int Update(PlanPartBoxInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_MPM_PLAN_PART_BOX_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@PART_BOX_CODE", DbType.String, info.PartBoxCode);
			db.AddInParameter(dbCommand, "@PART_BOX_NAME", DbType.String, info.PartBoxName);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@SUPPLIER_NAME", DbType.String, info.SupplierName);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@SOURCE_ZONE_NO", DbType.String, info.SourceZoneNo);
			db.AddInParameter(dbCommand, "@SOURCE_WM_NO", DbType.String, info.SourceWmNo);
			db.AddInParameter(dbCommand, "@TARGET_ZONE_NO", DbType.String, info.TargetZoneNo);
			db.AddInParameter(dbCommand, "@TARGET_WM_NO", DbType.String, info.TargetWmNo);
			db.AddInParameter(dbCommand, "@PICK_UP_TIME", DbType.Int32, info.PickUpTime);
			db.AddInParameter(dbCommand, "@DELIVERY_TIME", DbType.Int32, info.DeliveryTime);
			db.AddInParameter(dbCommand, "@DELAY_TIME", DbType.Int32, info.DelayTime);
			db.AddInParameter(dbCommand, "@DOCK", DbType.String, info.Dock);
			db.AddInParameter(dbCommand, "@STATUS", DbType.Int32, info.Status);
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
		/// <param name="ID">PlanPartBoxInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_MPM_PLAN_PART_BOX_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">PlanPartBoxInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TM_MPM_PLAN_PART_BOX] WITH(ROWLOCK) "
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
		/// <param name="ID">PlanPartBoxInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TM_MPM_PLAN_PART_BOX] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static PlanPartBoxInfo CreatePlanPartBoxInfo(IDataReader rdr)
		{
			PlanPartBoxInfo info = new PlanPartBoxInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.PartBoxCode = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_BOX_CODE"));			
			info.PartBoxName = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_BOX_NAME"));			
			info.SupplierNum = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NUM"));			
			info.SupplierName = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NAME"));			
			info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));			
			info.Workshop = DBConvert.GetString(rdr, rdr.GetOrdinal("WORKSHOP"));			
			info.AssemblyLine = DBConvert.GetString(rdr, rdr.GetOrdinal("ASSEMBLY_LINE"));			
			info.SourceZoneNo = DBConvert.GetString(rdr, rdr.GetOrdinal("SOURCE_ZONE_NO"));			
			info.SourceWmNo = DBConvert.GetString(rdr, rdr.GetOrdinal("SOURCE_WM_NO"));			
			info.TargetZoneNo = DBConvert.GetString(rdr, rdr.GetOrdinal("TARGET_ZONE_NO"));			
			info.TargetWmNo = DBConvert.GetString(rdr, rdr.GetOrdinal("TARGET_WM_NO"));			
			info.PickUpTime = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PICK_UP_TIME"));			
			info.DeliveryTime = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("DELIVERY_TIME"));			
			info.DelayTime = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("DELAY_TIME"));			
			info.Dock = DBConvert.GetString(rdr, rdr.GetOrdinal("DOCK"));			
			info.Status = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("STATUS"));			
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
