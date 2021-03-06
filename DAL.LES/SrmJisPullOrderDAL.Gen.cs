#region Declaim
//---------------------------------------------------------------------------
// Name:		SrmJisPullOrderDAL
// Function: 	Expose data in table TI_IFM_SRM_JIS_PULL_ORDER from database as business object to MES system.
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
    /// SrmJisPullOrderDAL对应表[TI_IFM_SRM_JIS_PULL_ORDER]
    /// </summary>
    public partial class SrmJisPullOrderDAL : BusinessObjectProvider<SrmJisPullOrderInfo>
	{
		#region Sql Statements
		private const string TI_IFM_SRM_JIS_PULL_ORDER_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				LOG_FID,
				PLANT,
				ORDER_CODE,
				DOCK,
				PUBLISH_TIME,
				PART_BOX_CODE,
				PART_BOX_NAME,
				SUPPLIER_NUM,
				SUPPLIER_NAME,
				SOURCE_ZONE_NO,
				TARGET_ZONE_NO,
				START_INFOPOINT_TIME,
				PLAN_DELIVERY_TIME,
				START_VEHICLE_SEQ_NO,
				END_VEHICLE_SEQ_NO,
				LOCATION,
				REMARK,
				DELETEFLAG,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER,
				COMMENTS				  
				FROM [LES].[TI_IFM_SRM_JIS_PULL_ORDER] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TI_IFM_SRM_JIS_PULL_ORDER_SELECT = 
			@"SELECT ID,
				FID,
				LOG_FID,
				PLANT,
				ORDER_CODE,
				DOCK,
				PUBLISH_TIME,
				PART_BOX_CODE,
				PART_BOX_NAME,
				SUPPLIER_NUM,
				SUPPLIER_NAME,
				SOURCE_ZONE_NO,
				TARGET_ZONE_NO,
				START_INFOPOINT_TIME,
				PLAN_DELIVERY_TIME,
				START_VEHICLE_SEQ_NO,
				END_VEHICLE_SEQ_NO,
				LOCATION,
				REMARK,
				DELETEFLAG,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER,
				COMMENTS				 
				FROM [LES].[TI_IFM_SRM_JIS_PULL_ORDER] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TI_IFM_SRM_JIS_PULL_ORDER_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TI_IFM_SRM_JIS_PULL_ORDER]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TI_IFM_SRM_JIS_PULL_ORDER_INSERT =
			@"INSERT INTO [LES].[TI_IFM_SRM_JIS_PULL_ORDER] (
				FID,
				LOG_FID,
				PLANT,
				ORDER_CODE,
				DOCK,
				PUBLISH_TIME,
				PART_BOX_CODE,
				PART_BOX_NAME,
				SUPPLIER_NUM,
				SUPPLIER_NAME,
				SOURCE_ZONE_NO,
				TARGET_ZONE_NO,
				START_INFOPOINT_TIME,
				PLAN_DELIVERY_TIME,
				START_VEHICLE_SEQ_NO,
				END_VEHICLE_SEQ_NO,
				LOCATION,
				REMARK,
				DELETEFLAG,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER,
				COMMENTS				 
			) VALUES (
				@FID,
				@LOG_FID,
				@PLANT,
				@ORDER_CODE,
				@DOCK,
				@PUBLISH_TIME,
				@PART_BOX_CODE,
				@PART_BOX_NAME,
				@SUPPLIER_NUM,
				@SUPPLIER_NAME,
				@SOURCE_ZONE_NO,
				@TARGET_ZONE_NO,
				@START_INFOPOINT_TIME,
				@PLAN_DELIVERY_TIME,
				@START_VEHICLE_SEQ_NO,
				@END_VEHICLE_SEQ_NO,
				@LOCATION,
				@REMARK,
				@DELETEFLAG,
				@PROCESS_FLAG,
				@PROCESS_TIME,
				@VALID_FLAG,
				GETDATE(),
				@CREATE_USER,
				@MODIFY_DATE,
				@MODIFY_USER,
				@COMMENTS				 
			);SELECT @@IDENTITY;";
		private const string TI_IFM_SRM_JIS_PULL_ORDER_UPDATE =
			@"UPDATE [LES].[TI_IFM_SRM_JIS_PULL_ORDER] WITH(ROWLOCK) 
				SET FID=@FID,
				LOG_FID=@LOG_FID,
				PLANT=@PLANT,
				ORDER_CODE=@ORDER_CODE,
				DOCK=@DOCK,
				PUBLISH_TIME=@PUBLISH_TIME,
				PART_BOX_CODE=@PART_BOX_CODE,
				PART_BOX_NAME=@PART_BOX_NAME,
				SUPPLIER_NUM=@SUPPLIER_NUM,
				SUPPLIER_NAME=@SUPPLIER_NAME,
				SOURCE_ZONE_NO=@SOURCE_ZONE_NO,
				TARGET_ZONE_NO=@TARGET_ZONE_NO,
				START_INFOPOINT_TIME=@START_INFOPOINT_TIME,
				PLAN_DELIVERY_TIME=@PLAN_DELIVERY_TIME,
				START_VEHICLE_SEQ_NO=@START_VEHICLE_SEQ_NO,
				END_VEHICLE_SEQ_NO=@END_VEHICLE_SEQ_NO,
				LOCATION=@LOCATION,
				REMARK=@REMARK,
				DELETEFLAG=@DELETEFLAG,
				PROCESS_FLAG=@PROCESS_FLAG,
				PROCESS_TIME=@PROCESS_TIME,
				VALID_FLAG=@VALID_FLAG,
				CREATE_DATE=@CREATE_DATE,
				CREATE_USER=@CREATE_USER,
				MODIFY_DATE=@MODIFY_DATE,
				MODIFY_USER=@MODIFY_USER,
				COMMENTS=@COMMENTS				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TI_IFM_SRM_JIS_PULL_ORDER_DELETE =
			@"DELETE FROM [LES].[TI_IFM_SRM_JIS_PULL_ORDER] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get SrmJisPullOrderInfo
		/// </summary>
		/// <param name="ID">SrmJisPullOrderInfo Primary key </param>
		/// <returns></returns> 
		public SrmJisPullOrderInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SRM_JIS_PULL_ORDER_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateSrmJisPullOrderInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>SrmJisPullOrderInfo Collection </returns>
		public List<SrmJisPullOrderInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TI_IFM_SRM_JIS_PULL_ORDER_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>SrmJisPullOrderInfo Collection </returns>
		public List<SrmJisPullOrderInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<SrmJisPullOrderInfo> list = new List<SrmJisPullOrderInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateSrmJisPullOrderInfo(dr));
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
		public List<SrmJisPullOrderInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TI_IFM_SRM_JIS_PULL_ORDER]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<SrmJisPullOrderInfo> list = new List<SrmJisPullOrderInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateSrmJisPullOrderInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TI_IFM_SRM_JIS_PULL_ORDER_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(SrmJisPullOrderInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SRM_JIS_PULL_ORDER_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@LOG_FID", DbType.Guid, info.LogFid);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@ORDER_CODE", DbType.String, info.OrderCode);
			db.AddInParameter(dbCommand, "@DOCK", DbType.String, info.Dock);
			db.AddInParameter(dbCommand, "@PUBLISH_TIME", DbType.DateTime, info.PublishTime);
			db.AddInParameter(dbCommand, "@PART_BOX_CODE", DbType.String, info.PartBoxCode);
			db.AddInParameter(dbCommand, "@PART_BOX_NAME", DbType.String, info.PartBoxName);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@SUPPLIER_NAME", DbType.String, info.SupplierName);
			db.AddInParameter(dbCommand, "@SOURCE_ZONE_NO", DbType.String, info.SourceZoneNo);
			db.AddInParameter(dbCommand, "@TARGET_ZONE_NO", DbType.String, info.TargetZoneNo);
			db.AddInParameter(dbCommand, "@START_INFOPOINT_TIME", DbType.DateTime, info.StartInfopointTime);
			db.AddInParameter(dbCommand, "@PLAN_DELIVERY_TIME", DbType.DateTime, info.PlanDeliveryTime);
			db.AddInParameter(dbCommand, "@START_VEHICLE_SEQ_NO", DbType.Int32, info.StartVehicleSeqNo);
			db.AddInParameter(dbCommand, "@END_VEHICLE_SEQ_NO", DbType.Int32, info.EndVehicleSeqNo);
			db.AddInParameter(dbCommand, "@LOCATION", DbType.String, info.Location);
			db.AddInParameter(dbCommand, "@REMARK", DbType.String, info.Remark);
			db.AddInParameter(dbCommand, "@DELETEFLAG", DbType.Boolean, info.Deleteflag);
			db.AddInParameter(dbCommand, "@PROCESS_FLAG", DbType.Int32, info.ProcessFlag);
			db.AddInParameter(dbCommand, "@PROCESS_TIME", DbType.DateTime, info.ProcessTime);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}
		/// <summary>
		/// GetInsertSql
		/// </summary>
		/// <param name="info"> info</param>
		public static string GetInsertSql(SrmJisPullOrderInfo info)
		{
			return  
			@"insert into [LES].[TI_IFM_SRM_JIS_PULL_ORDER] (
				FID,
				LOG_FID,
				PLANT,
				ORDER_CODE,
				DOCK,
				PUBLISH_TIME,
				PART_BOX_CODE,
				PART_BOX_NAME,
				SUPPLIER_NUM,
				SUPPLIER_NAME,
				SOURCE_ZONE_NO,
				TARGET_ZONE_NO,
				START_INFOPOINT_TIME,
				PLAN_DELIVERY_TIME,
				START_VEHICLE_SEQ_NO,
				END_VEHICLE_SEQ_NO,
				LOCATION,
				REMARK,
				DELETEFLAG,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER,
				COMMENTS				 
			) values ("+
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				(info.LogFid == null ? "NULL" : "N'" + info.LogFid.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.Plant) ? "NULL" : "N'" + info.Plant + "'") + ","+
				(string.IsNullOrEmpty(info.OrderCode) ? "NULL" : "N'" + info.OrderCode + "'") + ","+
				(string.IsNullOrEmpty(info.Dock) ? "NULL" : "N'" + info.Dock + "'") + ","+
				(info.PublishTime == null ? "NULL" : "N'" + info.PublishTime.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.PartBoxCode) ? "NULL" : "N'" + info.PartBoxCode + "'") + ","+
				(string.IsNullOrEmpty(info.PartBoxName) ? "NULL" : "N'" + info.PartBoxName + "'") + ","+
				(string.IsNullOrEmpty(info.SupplierNum) ? "NULL" : "N'" + info.SupplierNum + "'") + ","+
				(string.IsNullOrEmpty(info.SupplierName) ? "NULL" : "N'" + info.SupplierName + "'") + ","+
				(string.IsNullOrEmpty(info.SourceZoneNo) ? "NULL" : "N'" + info.SourceZoneNo + "'") + ","+
				(string.IsNullOrEmpty(info.TargetZoneNo) ? "NULL" : "N'" + info.TargetZoneNo + "'") + ","+
				(info.StartInfopointTime == null ? "NULL" : "N'" + info.StartInfopointTime.GetValueOrDefault() + "'") + ","+
				(info.PlanDeliveryTime == null ? "NULL" : "N'" + info.PlanDeliveryTime.GetValueOrDefault() + "'") + ","+
				(info.StartVehicleSeqNo == null ? "NULL" : "" + info.StartVehicleSeqNo.GetValueOrDefault() + "") + ","+
				(info.EndVehicleSeqNo == null ? "NULL" : "" + info.EndVehicleSeqNo.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.Location) ? "NULL" : "N'" + info.Location + "'") + ","+
				(string.IsNullOrEmpty(info.Remark) ? "NULL" : "N'" + info.Remark + "'") + ","+
				(info.Deleteflag == null ? "NULL" : "" + (info.Deleteflag.GetValueOrDefault() ? "1" : "0") + "") + ","+
				(info.ProcessFlag == null ? "NULL" : "" + info.ProcessFlag.GetValueOrDefault() + "") + ","+
				(info.ProcessTime == null ? "NULL" : "N'" + info.ProcessTime.GetValueOrDefault() + "'") + ","+
				"1" + ","+		
				"GETDATE()" + ","+			
				"N'" + info.CreateUser + "'" + ","+		
				"NULL" + ","+			
				"NULL" + ","+			
				(string.IsNullOrEmpty(info.Comments) ? "NULL" : "N'" + info.Comments + "'") + ");";				}
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(SrmJisPullOrderInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SRM_JIS_PULL_ORDER_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@LOG_FID", DbType.Guid, info.LogFid);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@ORDER_CODE", DbType.String, info.OrderCode);
			db.AddInParameter(dbCommand, "@DOCK", DbType.String, info.Dock);
			db.AddInParameter(dbCommand, "@PUBLISH_TIME", DbType.DateTime, info.PublishTime);
			db.AddInParameter(dbCommand, "@PART_BOX_CODE", DbType.String, info.PartBoxCode);
			db.AddInParameter(dbCommand, "@PART_BOX_NAME", DbType.String, info.PartBoxName);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@SUPPLIER_NAME", DbType.String, info.SupplierName);
			db.AddInParameter(dbCommand, "@SOURCE_ZONE_NO", DbType.String, info.SourceZoneNo);
			db.AddInParameter(dbCommand, "@TARGET_ZONE_NO", DbType.String, info.TargetZoneNo);
			db.AddInParameter(dbCommand, "@START_INFOPOINT_TIME", DbType.DateTime, info.StartInfopointTime);
			db.AddInParameter(dbCommand, "@PLAN_DELIVERY_TIME", DbType.DateTime, info.PlanDeliveryTime);
			db.AddInParameter(dbCommand, "@START_VEHICLE_SEQ_NO", DbType.Int32, info.StartVehicleSeqNo);
			db.AddInParameter(dbCommand, "@END_VEHICLE_SEQ_NO", DbType.Int32, info.EndVehicleSeqNo);
			db.AddInParameter(dbCommand, "@LOCATION", DbType.String, info.Location);
			db.AddInParameter(dbCommand, "@REMARK", DbType.String, info.Remark);
			db.AddInParameter(dbCommand, "@DELETEFLAG", DbType.Boolean, info.Deleteflag);
			db.AddInParameter(dbCommand, "@PROCESS_FLAG", DbType.Int32, info.ProcessFlag);
			db.AddInParameter(dbCommand, "@PROCESS_TIME", DbType.DateTime, info.ProcessTime);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="ID">SrmJisPullOrderInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SRM_JIS_PULL_ORDER_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">SrmJisPullOrderInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TI_IFM_SRM_JIS_PULL_ORDER] WITH(ROWLOCK) "
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
		/// <param name="ID">SrmJisPullOrderInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TI_IFM_SRM_JIS_PULL_ORDER] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static SrmJisPullOrderInfo CreateSrmJisPullOrderInfo(IDataReader rdr)
		{
			SrmJisPullOrderInfo info = new SrmJisPullOrderInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.LogFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("LOG_FID"));			
			info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));			
			info.OrderCode = DBConvert.GetString(rdr, rdr.GetOrdinal("ORDER_CODE"));			
			info.Dock = DBConvert.GetString(rdr, rdr.GetOrdinal("DOCK"));			
			info.PublishTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("PUBLISH_TIME"));			
			info.PartBoxCode = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_BOX_CODE"));			
			info.PartBoxName = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_BOX_NAME"));			
			info.SupplierNum = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NUM"));			
			info.SupplierName = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NAME"));			
			info.SourceZoneNo = DBConvert.GetString(rdr, rdr.GetOrdinal("SOURCE_ZONE_NO"));			
			info.TargetZoneNo = DBConvert.GetString(rdr, rdr.GetOrdinal("TARGET_ZONE_NO"));			
			info.StartInfopointTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("START_INFOPOINT_TIME"));			
			info.PlanDeliveryTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("PLAN_DELIVERY_TIME"));			
			info.StartVehicleSeqNo = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("START_VEHICLE_SEQ_NO"));			
			info.EndVehicleSeqNo = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("END_VEHICLE_SEQ_NO"));			
			info.Location = DBConvert.GetString(rdr, rdr.GetOrdinal("LOCATION"));			
			info.Remark = DBConvert.GetString(rdr, rdr.GetOrdinal("REMARK"));			
			info.Deleteflag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("DELETEFLAG"));			
			info.ProcessFlag = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PROCESS_FLAG"));			
			info.ProcessTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("PROCESS_TIME"));			
			info.ValidFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			info.CreateDate = DBConvert.GetDateTime(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			info.Comments = DBConvert.GetString(rdr, rdr.GetOrdinal("COMMENTS"));			
			return info;
		}
		
		#endregion
	}
}
