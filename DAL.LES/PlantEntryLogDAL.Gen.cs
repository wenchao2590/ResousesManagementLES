#region Declaim
//---------------------------------------------------------------------------
// Name:		PlantEntryLogDAL
// Function: 	Expose data in table TT_CMM_PLANT_ENTRY_LOG from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月20日
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
    /// PlantEntryLogDAL对应表[TT_CMM_PLANT_ENTRY_LOG]
    /// </summary>
    public partial class PlantEntryLogDAL : BusinessObjectProvider<PlantEntryLogInfo>
	{
		#region Sql Statements
		private const string TT_CMM_PLANT_ENTRY_LOG_SELECT_BY_ID =
			@"SELECT ID,
				VEHICLE_NO,
				RUNSHEET_NO,
				IS_URGENT_ORDER,
				REQUIRE_TIME,
				ARRIVE_TIME,
				EXPIRE_TIME,
				ENTRY_TIME,
				DOCK_RELEASE_TIME,
				DOCK_HOLD_TIME,
				EXIT_TIME,
				WAITING_TIME,
				DOCK_PROCESSING_TIME,
				DOCK,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				PHONE_NO,
				STATUS,
				IS_JUMP_QUEUE,
				VALID_FLAG				  
				FROM [LES].[TT_CMM_PLANT_ENTRY_LOG] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TT_CMM_PLANT_ENTRY_LOG_SELECT = 
			@"SELECT ID,
				VEHICLE_NO,
				RUNSHEET_NO,
				IS_URGENT_ORDER,
				REQUIRE_TIME,
				ARRIVE_TIME,
				EXPIRE_TIME,
				ENTRY_TIME,
				DOCK_RELEASE_TIME,
				DOCK_HOLD_TIME,
				EXIT_TIME,
				WAITING_TIME,
				DOCK_PROCESSING_TIME,
				DOCK,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				PHONE_NO,
				STATUS,
				IS_JUMP_QUEUE,
				VALID_FLAG				 
				FROM [LES].[TT_CMM_PLANT_ENTRY_LOG] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TT_CMM_PLANT_ENTRY_LOG_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TT_CMM_PLANT_ENTRY_LOG]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TT_CMM_PLANT_ENTRY_LOG_INSERT =
			@"INSERT INTO [LES].[TT_CMM_PLANT_ENTRY_LOG] (
				VEHICLE_NO,
				RUNSHEET_NO,
				IS_URGENT_ORDER,
				REQUIRE_TIME,
				ARRIVE_TIME,
				EXPIRE_TIME,
				ENTRY_TIME,
				DOCK_RELEASE_TIME,
				DOCK_HOLD_TIME,
				EXIT_TIME,
				WAITING_TIME,
				DOCK_PROCESSING_TIME,
				DOCK,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				PHONE_NO,
				STATUS,
				IS_JUMP_QUEUE,
				VALID_FLAG				 
			) VALUES (
				@VEHICLE_NO,
				@RUNSHEET_NO,
				@IS_URGENT_ORDER,
				@REQUIRE_TIME,
				@ARRIVE_TIME,
				@EXPIRE_TIME,
				@ENTRY_TIME,
				@DOCK_RELEASE_TIME,
				@DOCK_HOLD_TIME,
				@EXIT_TIME,
				@WAITING_TIME,
				@DOCK_PROCESSING_TIME,
				@DOCK,
				@CREATE_USER,
				GETDATE(),
				@MODIFY_USER,
				@MODIFY_DATE,
				@PHONE_NO,
				@STATUS,
				@IS_JUMP_QUEUE,
				@VALID_FLAG				 
			);SELECT @@IDENTITY;";
		private const string TT_CMM_PLANT_ENTRY_LOG_UPDATE =
			@"UPDATE [LES].[TT_CMM_PLANT_ENTRY_LOG] WITH(ROWLOCK) 
				SET VEHICLE_NO=@VEHICLE_NO,
				RUNSHEET_NO=@RUNSHEET_NO,
				IS_URGENT_ORDER=@IS_URGENT_ORDER,
				REQUIRE_TIME=@REQUIRE_TIME,
				ARRIVE_TIME=@ARRIVE_TIME,
				EXPIRE_TIME=@EXPIRE_TIME,
				ENTRY_TIME=@ENTRY_TIME,
				DOCK_RELEASE_TIME=@DOCK_RELEASE_TIME,
				DOCK_HOLD_TIME=@DOCK_HOLD_TIME,
				EXIT_TIME=@EXIT_TIME,
				WAITING_TIME=@WAITING_TIME,
				DOCK_PROCESSING_TIME=@DOCK_PROCESSING_TIME,
				DOCK=@DOCK,
				CREATE_USER=@CREATE_USER,
				CREATE_DATE=@CREATE_DATE,
				MODIFY_USER=@MODIFY_USER,
				MODIFY_DATE=@MODIFY_DATE,
				PHONE_NO=@PHONE_NO,
				STATUS=@STATUS,
				IS_JUMP_QUEUE=@IS_JUMP_QUEUE,
				VALID_FLAG=@VALID_FLAG				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TT_CMM_PLANT_ENTRY_LOG_DELETE =
			@"DELETE FROM [LES].[TT_CMM_PLANT_ENTRY_LOG] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get PlantEntryLogInfo
		/// </summary>
		/// <param name="ID">PlantEntryLogInfo Primary key </param>
		/// <returns></returns> 
		public PlantEntryLogInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_CMM_PLANT_ENTRY_LOG_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreatePlantEntryLogInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>PlantEntryLogInfo Collection </returns>
		public List<PlantEntryLogInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TT_CMM_PLANT_ENTRY_LOG_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>PlantEntryLogInfo Collection </returns>
		public List<PlantEntryLogInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<PlantEntryLogInfo> list = new List<PlantEntryLogInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreatePlantEntryLogInfo(dr));
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
		public List<PlantEntryLogInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TT_CMM_PLANT_ENTRY_LOG]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<PlantEntryLogInfo> list = new List<PlantEntryLogInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreatePlantEntryLogInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TT_CMM_PLANT_ENTRY_LOG_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(PlantEntryLogInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TT_CMM_PLANT_ENTRY_LOG_INSERT);			
			db.AddInParameter(dbCommand, "@VEHICLE_NO", DbType.String, info.VehicleNo);
			db.AddInParameter(dbCommand, "@RUNSHEET_NO", DbType.String, info.RunsheetNo);
			db.AddInParameter(dbCommand, "@IS_URGENT_ORDER", DbType.Int32, info.IsUrgentOrder);
			db.AddInParameter(dbCommand, "@REQUIRE_TIME", DbType.DateTime, info.RequireTime);
			db.AddInParameter(dbCommand, "@ARRIVE_TIME", DbType.Int32, info.ArriveTime);
			db.AddInParameter(dbCommand, "@EXPIRE_TIME", DbType.Int32, info.ExpireTime);
			db.AddInParameter(dbCommand, "@ENTRY_TIME", DbType.DateTime, info.EntryTime);
			db.AddInParameter(dbCommand, "@DOCK_RELEASE_TIME", DbType.DateTime, info.DockReleaseTime);
			db.AddInParameter(dbCommand, "@DOCK_HOLD_TIME", DbType.DateTime, info.DockHoldTime);
			db.AddInParameter(dbCommand, "@EXIT_TIME", DbType.DateTime, info.ExitTime);
			db.AddInParameter(dbCommand, "@WAITING_TIME", DbType.Int32, info.WaitingTime);
			db.AddInParameter(dbCommand, "@DOCK_PROCESSING_TIME", DbType.Int32, info.DockProcessingTime);
			db.AddInParameter(dbCommand, "@DOCK", DbType.String, info.Dock);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@PHONE_NO", DbType.String, info.PhoneNo);
			db.AddInParameter(dbCommand, "@STATUS", DbType.Int32, info.Status);
			db.AddInParameter(dbCommand, "@IS_JUMP_QUEUE", DbType.Int32, info.IsJumpQueue);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}
		/// <summary>
		/// GetInsertSql
		/// </summary>
		/// <param name="info"> info</param>
		public static string GetInsertSql(PlantEntryLogInfo info)
		{
			return  
			@"insert into [LES].[TT_CMM_PLANT_ENTRY_LOG] (
				VEHICLE_NO,
				RUNSHEET_NO,
				IS_URGENT_ORDER,
				REQUIRE_TIME,
				ARRIVE_TIME,
				EXPIRE_TIME,
				ENTRY_TIME,
				DOCK_RELEASE_TIME,
				DOCK_HOLD_TIME,
				EXIT_TIME,
				WAITING_TIME,
				DOCK_PROCESSING_TIME,
				DOCK,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				PHONE_NO,
				STATUS,
				IS_JUMP_QUEUE,
				VALID_FLAG				 
			) values ("+
				(string.IsNullOrEmpty(info.VehicleNo) ? "NULL" : "N'" + info.VehicleNo + "'") + ","+
				(string.IsNullOrEmpty(info.RunsheetNo) ? "NULL" : "N'" + info.RunsheetNo + "'") + ","+
				(info.IsUrgentOrder == null ? "NULL" : "" + info.IsUrgentOrder.GetValueOrDefault() + "") + ","+
				(info.RequireTime == null ? "NULL" : "N'" + info.RequireTime.GetValueOrDefault() + "'") + ","+
				(info.ArriveTime == null ? "NULL" : "" + info.ArriveTime.GetValueOrDefault() + "") + ","+
				(info.ExpireTime == null ? "NULL" : "" + info.ExpireTime.GetValueOrDefault() + "") + ","+
				(info.EntryTime == null ? "NULL" : "N'" + info.EntryTime.GetValueOrDefault() + "'") + ","+
				(info.DockReleaseTime == null ? "NULL" : "N'" + info.DockReleaseTime.GetValueOrDefault() + "'") + ","+
				(info.DockHoldTime == null ? "NULL" : "N'" + info.DockHoldTime.GetValueOrDefault() + "'") + ","+
				(info.ExitTime == null ? "NULL" : "N'" + info.ExitTime.GetValueOrDefault() + "'") + ","+
				(info.WaitingTime == null ? "NULL" : "" + info.WaitingTime.GetValueOrDefault() + "") + ","+
				(info.DockProcessingTime == null ? "NULL" : "" + info.DockProcessingTime.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.Dock) ? "NULL" : "N'" + info.Dock + "'") + ","+
				"N'" + info.CreateUser + "'" + ","+		
				"GETDATE()" + ","+			
				"NULL" + ","+			
				"NULL" + ","+			
				(string.IsNullOrEmpty(info.PhoneNo) ? "NULL" : "N'" + info.PhoneNo + "'") + ","+
				(info.Status == null ? "NULL" : "" + info.Status.GetValueOrDefault() + "") + ","+
				(info.IsJumpQueue == null ? "NULL" : "" + info.IsJumpQueue.GetValueOrDefault() + "") + ","+
				"1" + ");";		
				}
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(PlantEntryLogInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_CMM_PLANT_ENTRY_LOG_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@VEHICLE_NO", DbType.String, info.VehicleNo);
			db.AddInParameter(dbCommand, "@RUNSHEET_NO", DbType.String, info.RunsheetNo);
			db.AddInParameter(dbCommand, "@IS_URGENT_ORDER", DbType.Int32, info.IsUrgentOrder);
			db.AddInParameter(dbCommand, "@REQUIRE_TIME", DbType.DateTime, info.RequireTime);
			db.AddInParameter(dbCommand, "@ARRIVE_TIME", DbType.Int32, info.ArriveTime);
			db.AddInParameter(dbCommand, "@EXPIRE_TIME", DbType.Int32, info.ExpireTime);
			db.AddInParameter(dbCommand, "@ENTRY_TIME", DbType.DateTime, info.EntryTime);
			db.AddInParameter(dbCommand, "@DOCK_RELEASE_TIME", DbType.DateTime, info.DockReleaseTime);
			db.AddInParameter(dbCommand, "@DOCK_HOLD_TIME", DbType.DateTime, info.DockHoldTime);
			db.AddInParameter(dbCommand, "@EXIT_TIME", DbType.DateTime, info.ExitTime);
			db.AddInParameter(dbCommand, "@WAITING_TIME", DbType.Int32, info.WaitingTime);
			db.AddInParameter(dbCommand, "@DOCK_PROCESSING_TIME", DbType.Int32, info.DockProcessingTime);
			db.AddInParameter(dbCommand, "@DOCK", DbType.String, info.Dock);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@PHONE_NO", DbType.String, info.PhoneNo);
			db.AddInParameter(dbCommand, "@STATUS", DbType.Int32, info.Status);
			db.AddInParameter(dbCommand, "@IS_JUMP_QUEUE", DbType.Int32, info.IsJumpQueue);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="ID">PlantEntryLogInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_CMM_PLANT_ENTRY_LOG_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">PlantEntryLogInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TT_CMM_PLANT_ENTRY_LOG] WITH(ROWLOCK) "
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
		/// <param name="ID">PlantEntryLogInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TT_CMM_PLANT_ENTRY_LOG] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static PlantEntryLogInfo CreatePlantEntryLogInfo(IDataReader rdr)
		{
			PlantEntryLogInfo info = new PlantEntryLogInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.VehicleNo = DBConvert.GetString(rdr, rdr.GetOrdinal("VEHICLE_NO"));			
			info.RunsheetNo = DBConvert.GetString(rdr, rdr.GetOrdinal("RUNSHEET_NO"));			
			info.IsUrgentOrder = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("IS_URGENT_ORDER"));			
			info.RequireTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("REQUIRE_TIME"));			
			info.ArriveTime = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("ARRIVE_TIME"));			
			info.ExpireTime = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("EXPIRE_TIME"));			
			info.EntryTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("ENTRY_TIME"));			
			info.DockReleaseTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("DOCK_RELEASE_TIME"));			
			info.DockHoldTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("DOCK_HOLD_TIME"));			
			info.ExitTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("EXIT_TIME"));			
			info.WaitingTime = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("WAITING_TIME"));			
			info.DockProcessingTime = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("DOCK_PROCESSING_TIME"));			
			info.Dock = DBConvert.GetString(rdr, rdr.GetOrdinal("DOCK"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.CreateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			info.PhoneNo = DBConvert.GetString(rdr, rdr.GetOrdinal("PHONE_NO"));			
			info.Status = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("STATUS"));			
			info.IsJumpQueue = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("IS_JUMP_QUEUE"));			
			info.ValidFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			return info;
		}
		
		#endregion
	}
}
