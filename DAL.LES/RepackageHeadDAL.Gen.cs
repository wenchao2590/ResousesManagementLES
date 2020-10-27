#region Declaim
//---------------------------------------------------------------------------
// Name:		RepackageHeadDAL
// Function: 	Expose data in table TT_WMM_REPACKAGE_HEAD from database as business object to MES system.
// Tool:		T4
// CreateDate:	2017年12月21日
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
    /// RepackageHeadDAL对应表[TT_WMM_REPACKAGE_HEAD]
    /// </summary>
    public partial class RepackageHeadDAL : BusinessObjectProvider<RepackageHeadInfo>
	{
		#region Sql Statements
		private const string TT_WMM_REPACKAGE_HEAD_SELECT_BY_ID =
			@"SELECT REPACKAGE_ID,
				REPACKAGE_NO,
				PLANT,
				WM_NO,
				ZONE_NO,
				REPACKAGE_TIME,
				REPACKAGE_BTIME,
				REPACKAGE_ETIME,
				REPACKAGE_PICKUP_TIME,
				REPACKAGE_ROUTE,
				APPLY_KEEPER,
				BOOK_KEEPER,
				PUBLISH_KEEPER,
				PUBLISH_TIME,
				COUNT_STATUS,
				REPACKAGE_COUNT,
				EMERGENCY_TYPE,
				COUNT_REASON,
				ASSEMBLY_LINE,
				PLANT_ZONE,
				WORKSHOP,
				COMMENTS,
				CREATE_USER,
				CREATE_DATE,
				UPDATE_USER,
				UPDATE_DATE,
				REPACKAGE_PICKUP_ETIME,
				REPACKAGE_TYPE				  
				FROM [LES].[TT_WMM_REPACKAGE_HEAD] WITH(NOLOCK) WHERE 1=1  AND REPACKAGE_ID =@REPACKAGE_ID;";
			
		private const string TT_WMM_REPACKAGE_HEAD_SELECT = 
			@"SELECT REPACKAGE_ID,
				REPACKAGE_NO,
				PLANT,
				WM_NO,
				ZONE_NO,
				REPACKAGE_TIME,
				REPACKAGE_BTIME,
				REPACKAGE_ETIME,
				REPACKAGE_PICKUP_TIME,
				REPACKAGE_ROUTE,
				APPLY_KEEPER,
				BOOK_KEEPER,
				PUBLISH_KEEPER,
				PUBLISH_TIME,
				COUNT_STATUS,
				REPACKAGE_COUNT,
				EMERGENCY_TYPE,
				COUNT_REASON,
				ASSEMBLY_LINE,
				PLANT_ZONE,
				WORKSHOP,
				COMMENTS,
				CREATE_USER,
				CREATE_DATE,
				UPDATE_USER,
				UPDATE_DATE,
				REPACKAGE_PICKUP_ETIME,
				REPACKAGE_TYPE				 
				FROM [LES].[TT_WMM_REPACKAGE_HEAD] WITH (NOLOCK) WHERE 1=1 {0};";
		
		private const string TT_WMM_REPACKAGE_HEAD_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TT_WMM_REPACKAGE_HEAD]  WITH(NOLOCK) WHERE 1=1 {0};";

		private const string TT_WMM_REPACKAGE_HEAD_INSERT =
			@"INSERT INTO [LES].[TT_WMM_REPACKAGE_HEAD] (
				REPACKAGE_NO,
				PLANT,
				WM_NO,
				ZONE_NO,
				REPACKAGE_TIME,
				REPACKAGE_BTIME,
				REPACKAGE_ETIME,
				REPACKAGE_PICKUP_TIME,
				REPACKAGE_ROUTE,
				APPLY_KEEPER,
				BOOK_KEEPER,
				PUBLISH_KEEPER,
				PUBLISH_TIME,
				COUNT_STATUS,
				REPACKAGE_COUNT,
				EMERGENCY_TYPE,
				COUNT_REASON,
				ASSEMBLY_LINE,
				PLANT_ZONE,
				WORKSHOP,
				COMMENTS,
				CREATE_USER,
				CREATE_DATE,
				UPDATE_USER,
				UPDATE_DATE,
				REPACKAGE_PICKUP_ETIME,
				REPACKAGE_TYPE				 
			) VALUES (
				@REPACKAGE_NO,
				@PLANT,
				@WM_NO,
				@ZONE_NO,
				@REPACKAGE_TIME,
				@REPACKAGE_BTIME,
				@REPACKAGE_ETIME,
				@REPACKAGE_PICKUP_TIME,
				@REPACKAGE_ROUTE,
				@APPLY_KEEPER,
				@BOOK_KEEPER,
				@PUBLISH_KEEPER,
				@PUBLISH_TIME,
				@COUNT_STATUS,
				@REPACKAGE_COUNT,
				@EMERGENCY_TYPE,
				@COUNT_REASON,
				@ASSEMBLY_LINE,
				@PLANT_ZONE,
				@WORKSHOP,
				@COMMENTS,
				@CREATE_USER,
				@CREATE_DATE,
				@UPDATE_USER,
				@UPDATE_DATE,
				@REPACKAGE_PICKUP_ETIME,
				@REPACKAGE_TYPE				 
			);SELECT @@IDENTITY;";
		private const string TT_WMM_REPACKAGE_HEAD_UPDATE =
			@"UPDATE [LES].[TT_WMM_REPACKAGE_HEAD] WITH(ROWLOCK) 
				SET REPACKAGE_NO=@REPACKAGE_NO,
				PLANT=@PLANT,
				WM_NO=@WM_NO,
				ZONE_NO=@ZONE_NO,
				REPACKAGE_TIME=@REPACKAGE_TIME,
				REPACKAGE_BTIME=@REPACKAGE_BTIME,
				REPACKAGE_ETIME=@REPACKAGE_ETIME,
				REPACKAGE_PICKUP_TIME=@REPACKAGE_PICKUP_TIME,
				REPACKAGE_ROUTE=@REPACKAGE_ROUTE,
				APPLY_KEEPER=@APPLY_KEEPER,
				BOOK_KEEPER=@BOOK_KEEPER,
				PUBLISH_KEEPER=@PUBLISH_KEEPER,
				PUBLISH_TIME=@PUBLISH_TIME,
				COUNT_STATUS=@COUNT_STATUS,
				REPACKAGE_COUNT=@REPACKAGE_COUNT,
				EMERGENCY_TYPE=@EMERGENCY_TYPE,
				COUNT_REASON=@COUNT_REASON,
				ASSEMBLY_LINE=@ASSEMBLY_LINE,
				PLANT_ZONE=@PLANT_ZONE,
				WORKSHOP=@WORKSHOP,
				COMMENTS=@COMMENTS,
				CREATE_USER=@CREATE_USER,
				CREATE_DATE=@CREATE_DATE,
				UPDATE_USER=@UPDATE_USER,
				UPDATE_DATE=@UPDATE_DATE,
				REPACKAGE_PICKUP_ETIME=@REPACKAGE_PICKUP_ETIME,
				REPACKAGE_TYPE=@REPACKAGE_TYPE				 
				WHERE 1=1  AND REPACKAGE_ID =@REPACKAGE_ID;";

		private const string TT_WMM_REPACKAGE_HEAD_DELETE =
			@"DELETE FROM [LES].[TT_WMM_REPACKAGE_HEAD] WITH(ROWLOCK)  
				WHERE 1=1  AND REPACKAGE_ID =@REPACKAGE_ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get RepackageHeadInfo
		/// </summary>
		/// <param name="REPACKAGE_ID">RepackageHeadInfo Primary key </param>
		/// <returns></returns> 
		public RepackageHeadInfo GetInfo(int aRepackageId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_WMM_REPACKAGE_HEAD_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@REPACKAGE_ID", DbType.Int32, aRepackageId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateRepackageHeadInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>RepackageHeadInfo Collection </returns>
		public List<RepackageHeadInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TT_WMM_REPACKAGE_HEAD_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>RepackageHeadInfo Collection </returns>
		public List<RepackageHeadInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<RepackageHeadInfo> list = new List<RepackageHeadInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateRepackageHeadInfo(dr));
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
		public List<RepackageHeadInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
		{
		    if (pageIndex <= 0) pageIndex = 1;
            if (pageRow <= 0) pageRow = 10;
			string whereText = string.Empty;
            if (!string.IsNullOrEmpty(textWhere))
            {
                if (textWhere.Trim().StartsWith("and", StringComparison.OrdinalIgnoreCase))
                    whereText += " where 1=1 " + textWhere;
                else
                    whereText += " where " + textWhere + " and 1=1";
            }
			else
                whereText += " where 1=1 ";
            if (string.IsNullOrEmpty(textOrder))
                textOrder += "[REPACKAGE_ID] desc";
            string sql = "select top " + pageRow + " * from "
                + "(select row_number() over(order by " + textOrder + ") as rownumber"
                + ",* from [LES].[TT_WMM_REPACKAGE_HEAD]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<RepackageHeadInfo> list = new List<RepackageHeadInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateRepackageHeadInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TT_WMM_REPACKAGE_HEAD_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public int Add(RepackageHeadInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TT_WMM_REPACKAGE_HEAD_INSERT);			
			db.AddInParameter(dbCommand, "@REPACKAGE_NO", DbType.String, info.RepackageNo);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@WM_NO", DbType.String, info.WmNo);
			db.AddInParameter(dbCommand, "@ZONE_NO", DbType.String, info.ZoneNo);
			db.AddInParameter(dbCommand, "@REPACKAGE_TIME", DbType.DateTime, info.RepackageTime);
			db.AddInParameter(dbCommand, "@REPACKAGE_BTIME", DbType.DateTime, info.RepackageBtime);
			db.AddInParameter(dbCommand, "@REPACKAGE_ETIME", DbType.DateTime, info.RepackageEtime);
			db.AddInParameter(dbCommand, "@REPACKAGE_PICKUP_TIME", DbType.DateTime, info.RepackagePickupTime);
			db.AddInParameter(dbCommand, "@REPACKAGE_ROUTE", DbType.String, info.RepackageRoute);
			db.AddInParameter(dbCommand, "@APPLY_KEEPER", DbType.String, info.ApplyKeeper);
			db.AddInParameter(dbCommand, "@BOOK_KEEPER", DbType.String, info.BookKeeper);
			db.AddInParameter(dbCommand, "@PUBLISH_KEEPER", DbType.String, info.PublishKeeper);
			db.AddInParameter(dbCommand, "@PUBLISH_TIME", DbType.DateTime, info.PublishTime);
			db.AddInParameter(dbCommand, "@COUNT_STATUS", DbType.Int32, info.CountStatus);
			db.AddInParameter(dbCommand, "@REPACKAGE_COUNT", DbType.Int32, info.RepackageCount);
			db.AddInParameter(dbCommand, "@EMERGENCY_TYPE", DbType.Int32, info.EmergencyType);
			db.AddInParameter(dbCommand, "@COUNT_REASON", DbType.String, info.CountReason);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@PLANT_ZONE", DbType.String, info.PlantZone);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@UPDATE_USER", DbType.String, info.UpdateUser);
			db.AddInParameter(dbCommand, "@UPDATE_DATE", DbType.DateTime, info.UpdateDate);
			db.AddInParameter(dbCommand, "@REPACKAGE_PICKUP_ETIME", DbType.DateTime, info.RepackagePickupEtime);
			db.AddInParameter(dbCommand, "@REPACKAGE_TYPE", DbType.String, info.RepackageType);
			return int.Parse("0" + db.ExecuteScalar(dbCommand));		
		}		
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(RepackageHeadInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_WMM_REPACKAGE_HEAD_UPDATE);				
			db.AddInParameter(dbCommand, "@REPACKAGE_ID", DbType.Int32, info.RepackageId);
			db.AddInParameter(dbCommand, "@REPACKAGE_NO", DbType.String, info.RepackageNo);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@WM_NO", DbType.String, info.WmNo);
			db.AddInParameter(dbCommand, "@ZONE_NO", DbType.String, info.ZoneNo);
			db.AddInParameter(dbCommand, "@REPACKAGE_TIME", DbType.DateTime, info.RepackageTime);
			db.AddInParameter(dbCommand, "@REPACKAGE_BTIME", DbType.DateTime, info.RepackageBtime);
			db.AddInParameter(dbCommand, "@REPACKAGE_ETIME", DbType.DateTime, info.RepackageEtime);
			db.AddInParameter(dbCommand, "@REPACKAGE_PICKUP_TIME", DbType.DateTime, info.RepackagePickupTime);
			db.AddInParameter(dbCommand, "@REPACKAGE_ROUTE", DbType.String, info.RepackageRoute);
			db.AddInParameter(dbCommand, "@APPLY_KEEPER", DbType.String, info.ApplyKeeper);
			db.AddInParameter(dbCommand, "@BOOK_KEEPER", DbType.String, info.BookKeeper);
			db.AddInParameter(dbCommand, "@PUBLISH_KEEPER", DbType.String, info.PublishKeeper);
			db.AddInParameter(dbCommand, "@PUBLISH_TIME", DbType.DateTime, info.PublishTime);
			db.AddInParameter(dbCommand, "@COUNT_STATUS", DbType.Int32, info.CountStatus);
			db.AddInParameter(dbCommand, "@REPACKAGE_COUNT", DbType.Int32, info.RepackageCount);
			db.AddInParameter(dbCommand, "@EMERGENCY_TYPE", DbType.Int32, info.EmergencyType);
			db.AddInParameter(dbCommand, "@COUNT_REASON", DbType.String, info.CountReason);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@PLANT_ZONE", DbType.String, info.PlantZone);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@UPDATE_USER", DbType.String, info.UpdateUser);
			db.AddInParameter(dbCommand, "@UPDATE_DATE", DbType.DateTime, info.UpdateDate);
			db.AddInParameter(dbCommand, "@REPACKAGE_PICKUP_ETIME", DbType.DateTime, info.RepackagePickupEtime);
			db.AddInParameter(dbCommand, "@REPACKAGE_TYPE", DbType.String, info.RepackageType);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="REPACKAGE_ID">RepackageHeadInfo Primary key </param>
		/// <returns></returns>
		public int Delete(int aRepackageId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_WMM_REPACKAGE_HEAD_DELETE);
		    db.AddInParameter(dbCommand, "@REPACKAGE_ID", DbType.Int32, aRepackageId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}
		/// <summary>
		/// UpdateInfo
		/// </summary>
		/// <param name="REPACKAGE_ID">RepackageHeadInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,int aRepackageId)
		{
		    string sql = "update [LES].[TT_WMM_REPACKAGE_HEAD] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE 1=1  AND REPACKAGE_ID =@REPACKAGE_ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@REPACKAGE_ID", DbType.Int32, aRepackageId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static RepackageHeadInfo CreateRepackageHeadInfo(IDataReader rdr)
		{
			RepackageHeadInfo info = new RepackageHeadInfo();
			info.RepackageId = DBConvert.GetInt32(rdr, rdr.GetOrdinal("REPACKAGE_ID"));			
			info.RepackageNo = DBConvert.GetString(rdr, rdr.GetOrdinal("REPACKAGE_NO"));			
			info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));			
			info.WmNo = DBConvert.GetString(rdr, rdr.GetOrdinal("WM_NO"));			
			info.ZoneNo = DBConvert.GetString(rdr, rdr.GetOrdinal("ZONE_NO"));			
			info.RepackageTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("REPACKAGE_TIME"));			
			info.RepackageBtime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("REPACKAGE_BTIME"));			
			info.RepackageEtime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("REPACKAGE_ETIME"));			
			info.RepackagePickupTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("REPACKAGE_PICKUP_TIME"));			
			info.RepackageRoute = DBConvert.GetString(rdr, rdr.GetOrdinal("REPACKAGE_ROUTE"));			
			info.ApplyKeeper = DBConvert.GetString(rdr, rdr.GetOrdinal("APPLY_KEEPER"));			
			info.BookKeeper = DBConvert.GetString(rdr, rdr.GetOrdinal("BOOK_KEEPER"));			
			info.PublishKeeper = DBConvert.GetString(rdr, rdr.GetOrdinal("PUBLISH_KEEPER"));			
			info.PublishTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("PUBLISH_TIME"));			
			info.CountStatus = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("COUNT_STATUS"));			
			info.RepackageCount = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("REPACKAGE_COUNT"));			
			info.EmergencyType = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("EMERGENCY_TYPE"));			
			info.CountReason = DBConvert.GetString(rdr, rdr.GetOrdinal("COUNT_REASON"));			
			info.AssemblyLine = DBConvert.GetString(rdr, rdr.GetOrdinal("ASSEMBLY_LINE"));			
			info.PlantZone = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT_ZONE"));			
			info.Workshop = DBConvert.GetString(rdr, rdr.GetOrdinal("WORKSHOP"));			
			info.Comments = DBConvert.GetString(rdr, rdr.GetOrdinal("COMMENTS"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.CreateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.UpdateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("UPDATE_USER"));			
			info.UpdateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("UPDATE_DATE"));			
			info.RepackagePickupEtime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("REPACKAGE_PICKUP_ETIME"));			
			info.RepackageType = DBConvert.GetString(rdr, rdr.GetOrdinal("REPACKAGE_TYPE"));			
			return info;
		}
		
		#endregion
	}
}
