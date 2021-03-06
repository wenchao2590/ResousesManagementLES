#region Declaim
//---------------------------------------------------------------------------
// Name:		PartPullDAL
// Function: 	Expose data in table TM_EPS_PART_PULL from database as business object to MES system.
// Tool:		T4
// CreateDate:	2017年12月22日
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
    /// PartPullDAL对应表[TM_EPS_PART_PULL]
    /// </summary>
    public partial class PartPullDAL : BusinessObjectProvider<PartPullInfo>
	{
		#region Sql Statements
		private const string TM_EPS_PART_PULL_SELECT_BY_ID =
			@"SELECT PART_NO,
				PLANT,
				ASSEMBLY_LINE,
				LOCATION,
				D_PLANT,
				D_ASSEMBLY_LINE,
				D_LOCATION,
				E_PLANT,
				E_ASSEMBLY_LINE,
				E_LOCATION,
				USAGE,
				PACKAGE,
				SCREEN_LOCATION,
				PULL_TYPE,
				COMBINATION_TYPE,
				DELIVER_TIME,
				ALARM_TIME,
				TRIGGER_STATUS,
				BUTTON_ID,
				BAHNHOF_NO,
				ROUTE,
				DOLLY,
				COMMENTS,
				TARGET_ZONE,
				TARGET_WM,
				CREATE_DATE,
				RELATION_ID,
				CREATE_USER,
				UPDATE_DATE,
				UPDATE_USER				  
				FROM [LES].[TM_EPS_PART_PULL] WITH(NOLOCK) WHERE 1=1  AND RELATION_ID =@RELATION_ID;";
			
		private const string TM_EPS_PART_PULL_SELECT = 
			@"SELECT PART_NO,
				PLANT,
				ASSEMBLY_LINE,
				LOCATION,
				D_PLANT,
				D_ASSEMBLY_LINE,
				D_LOCATION,
				E_PLANT,
				E_ASSEMBLY_LINE,
				E_LOCATION,
				USAGE,
				PACKAGE,
				SCREEN_LOCATION,
				PULL_TYPE,
				COMBINATION_TYPE,
				DELIVER_TIME,
				ALARM_TIME,
				TRIGGER_STATUS,
				BUTTON_ID,
				BAHNHOF_NO,
				ROUTE,
				DOLLY,
				COMMENTS,
				TARGET_ZONE,
				TARGET_WM,
				CREATE_DATE,
				RELATION_ID,
				CREATE_USER,
				UPDATE_DATE,
				UPDATE_USER				 
				FROM [LES].[TM_EPS_PART_PULL] WITH (NOLOCK) WHERE 1=1 {0};";
		
		private const string TM_EPS_PART_PULL_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TM_EPS_PART_PULL]  WITH(NOLOCK) WHERE 1=1 {0};";

		private const string TM_EPS_PART_PULL_INSERT =
			@"INSERT INTO [LES].[TM_EPS_PART_PULL] (
				PART_NO,
				PLANT,
				ASSEMBLY_LINE,
				LOCATION,
				D_PLANT,
				D_ASSEMBLY_LINE,
				D_LOCATION,
				E_PLANT,
				E_ASSEMBLY_LINE,
				E_LOCATION,
				USAGE,
				PACKAGE,
				SCREEN_LOCATION,
				PULL_TYPE,
				COMBINATION_TYPE,
				DELIVER_TIME,
				ALARM_TIME,
				TRIGGER_STATUS,
				BUTTON_ID,
				BAHNHOF_NO,
				ROUTE,
				DOLLY,
				COMMENTS,
				TARGET_ZONE,
				TARGET_WM,
				CREATE_DATE,
				CREATE_USER,
				UPDATE_DATE,
				UPDATE_USER				 
			) VALUES (
				@PART_NO,
				@PLANT,
				@ASSEMBLY_LINE,
				@LOCATION,
				@D_PLANT,
				@D_ASSEMBLY_LINE,
				@D_LOCATION,
				@E_PLANT,
				@E_ASSEMBLY_LINE,
				@E_LOCATION,
				@USAGE,
				@PACKAGE,
				@SCREEN_LOCATION,
				@PULL_TYPE,
				@COMBINATION_TYPE,
				@DELIVER_TIME,
				@ALARM_TIME,
				@TRIGGER_STATUS,
				@BUTTON_ID,
				@BAHNHOF_NO,
				@ROUTE,
				@DOLLY,
				@COMMENTS,
				@TARGET_ZONE,
				@TARGET_WM,
				@CREATE_DATE,
				@CREATE_USER,
				@UPDATE_DATE,
				@UPDATE_USER				 
			);SELECT @@IDENTITY;";
		private const string TM_EPS_PART_PULL_UPDATE =
			@"UPDATE [LES].[TM_EPS_PART_PULL] WITH(ROWLOCK) 
				SET PART_NO=@PART_NO,
				PLANT=@PLANT,
				ASSEMBLY_LINE=@ASSEMBLY_LINE,
				LOCATION=@LOCATION,
				D_PLANT=@D_PLANT,
				D_ASSEMBLY_LINE=@D_ASSEMBLY_LINE,
				D_LOCATION=@D_LOCATION,
				E_PLANT=@E_PLANT,
				E_ASSEMBLY_LINE=@E_ASSEMBLY_LINE,
				E_LOCATION=@E_LOCATION,
				USAGE=@USAGE,
				PACKAGE=@PACKAGE,
				SCREEN_LOCATION=@SCREEN_LOCATION,
				PULL_TYPE=@PULL_TYPE,
				COMBINATION_TYPE=@COMBINATION_TYPE,
				DELIVER_TIME=@DELIVER_TIME,
				ALARM_TIME=@ALARM_TIME,
				TRIGGER_STATUS=@TRIGGER_STATUS,
				BUTTON_ID=@BUTTON_ID,
				BAHNHOF_NO=@BAHNHOF_NO,
				ROUTE=@ROUTE,
				DOLLY=@DOLLY,
				COMMENTS=@COMMENTS,
				TARGET_ZONE=@TARGET_ZONE,
				TARGET_WM=@TARGET_WM,
				CREATE_DATE=@CREATE_DATE,
				CREATE_USER=@CREATE_USER,
				UPDATE_DATE=@UPDATE_DATE,
				UPDATE_USER=@UPDATE_USER				 
				WHERE 1=1  AND RELATION_ID =@RELATION_ID;";

		private const string TM_EPS_PART_PULL_DELETE =
			@"DELETE FROM [LES].[TM_EPS_PART_PULL] WITH(ROWLOCK)  
				WHERE 1=1  AND RELATION_ID =@RELATION_ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get PartPullInfo
		/// </summary>
		/// <param name="RELATION_ID">PartPullInfo Primary key </param>
		/// <returns></returns> 
		public PartPullInfo GetInfo(int aRelationId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_EPS_PART_PULL_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@RELATION_ID", DbType.Int32, aRelationId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreatePartPullInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>PartPullInfo Collection </returns>
		public List<PartPullInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TM_EPS_PART_PULL_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>PartPullInfo Collection </returns>
		public List<PartPullInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<PartPullInfo> list = new List<PartPullInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreatePartPullInfo(dr));
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
		public List<PartPullInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                textOrder += "[RELATION_ID] desc";
            string sql = "select top " + pageRow + " * from "
                + "(select row_number() over(order by " + textOrder + ") as rownumber"
                + ",* from [LES].[TM_EPS_PART_PULL]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<PartPullInfo> list = new List<PartPullInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreatePartPullInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TM_EPS_PART_PULL_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public int Add(PartPullInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TM_EPS_PART_PULL_INSERT);			
			db.AddInParameter(dbCommand, "@PART_NO", DbType.AnsiString, info.PartNo);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@LOCATION", DbType.String, info.Location);
			db.AddInParameter(dbCommand, "@D_PLANT", DbType.AnsiString, info.DPlant);
			db.AddInParameter(dbCommand, "@D_ASSEMBLY_LINE", DbType.AnsiString, info.DAssemblyLine);
			db.AddInParameter(dbCommand, "@D_LOCATION", DbType.AnsiString, info.DLocation);
			db.AddInParameter(dbCommand, "@E_PLANT", DbType.AnsiString, info.EPlant);
			db.AddInParameter(dbCommand, "@E_ASSEMBLY_LINE", DbType.AnsiString, info.EAssemblyLine);
			db.AddInParameter(dbCommand, "@E_LOCATION", DbType.AnsiString, info.ELocation);
			db.AddInParameter(dbCommand, "@USAGE", DbType.Int32, info.Usage);
			db.AddInParameter(dbCommand, "@PACKAGE", DbType.Int32, info.Package);
			db.AddInParameter(dbCommand, "@SCREEN_LOCATION", DbType.String, info.ScreenLocation);
			db.AddInParameter(dbCommand, "@PULL_TYPE", DbType.Int32, info.PullType);
			db.AddInParameter(dbCommand, "@COMBINATION_TYPE", DbType.Int32, info.CombinationType);
			db.AddInParameter(dbCommand, "@DELIVER_TIME", DbType.Int32, info.DeliverTime);
			db.AddInParameter(dbCommand, "@ALARM_TIME", DbType.Int32, info.AlarmTime);
			db.AddInParameter(dbCommand, "@TRIGGER_STATUS", DbType.Int32, info.TriggerStatus);
			db.AddInParameter(dbCommand, "@BUTTON_ID", DbType.String, info.ButtonId);
			db.AddInParameter(dbCommand, "@BAHNHOF_NO", DbType.String, info.BahnhofNo);
			db.AddInParameter(dbCommand, "@ROUTE", DbType.String, info.Route);
			db.AddInParameter(dbCommand, "@DOLLY", DbType.String, info.Dolly);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@TARGET_ZONE", DbType.String, info.TargetZone);
			db.AddInParameter(dbCommand, "@TARGET_WM", DbType.String, info.TargetWm);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@UPDATE_DATE", DbType.DateTime, info.UpdateDate);
			db.AddInParameter(dbCommand, "@UPDATE_USER", DbType.String, info.UpdateUser);
			return int.Parse("0" + db.ExecuteScalar(dbCommand));		
		}		
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(PartPullInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_EPS_PART_PULL_UPDATE);				
			db.AddInParameter(dbCommand, "@PART_NO", DbType.AnsiString, info.PartNo);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@LOCATION", DbType.String, info.Location);
			db.AddInParameter(dbCommand, "@D_PLANT", DbType.AnsiString, info.DPlant);
			db.AddInParameter(dbCommand, "@D_ASSEMBLY_LINE", DbType.AnsiString, info.DAssemblyLine);
			db.AddInParameter(dbCommand, "@D_LOCATION", DbType.AnsiString, info.DLocation);
			db.AddInParameter(dbCommand, "@E_PLANT", DbType.AnsiString, info.EPlant);
			db.AddInParameter(dbCommand, "@E_ASSEMBLY_LINE", DbType.AnsiString, info.EAssemblyLine);
			db.AddInParameter(dbCommand, "@E_LOCATION", DbType.AnsiString, info.ELocation);
			db.AddInParameter(dbCommand, "@USAGE", DbType.Int32, info.Usage);
			db.AddInParameter(dbCommand, "@PACKAGE", DbType.Int32, info.Package);
			db.AddInParameter(dbCommand, "@SCREEN_LOCATION", DbType.String, info.ScreenLocation);
			db.AddInParameter(dbCommand, "@PULL_TYPE", DbType.Int32, info.PullType);
			db.AddInParameter(dbCommand, "@COMBINATION_TYPE", DbType.Int32, info.CombinationType);
			db.AddInParameter(dbCommand, "@DELIVER_TIME", DbType.Int32, info.DeliverTime);
			db.AddInParameter(dbCommand, "@ALARM_TIME", DbType.Int32, info.AlarmTime);
			db.AddInParameter(dbCommand, "@TRIGGER_STATUS", DbType.Int32, info.TriggerStatus);
			db.AddInParameter(dbCommand, "@BUTTON_ID", DbType.String, info.ButtonId);
			db.AddInParameter(dbCommand, "@BAHNHOF_NO", DbType.String, info.BahnhofNo);
			db.AddInParameter(dbCommand, "@ROUTE", DbType.String, info.Route);
			db.AddInParameter(dbCommand, "@DOLLY", DbType.String, info.Dolly);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@TARGET_ZONE", DbType.String, info.TargetZone);
			db.AddInParameter(dbCommand, "@TARGET_WM", DbType.String, info.TargetWm);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@RELATION_ID", DbType.Int32, info.RelationId);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@UPDATE_DATE", DbType.DateTime, info.UpdateDate);
			db.AddInParameter(dbCommand, "@UPDATE_USER", DbType.String, info.UpdateUser);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="RELATION_ID">PartPullInfo Primary key </param>
		/// <returns></returns>
		public int Delete(int aRelationId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_EPS_PART_PULL_DELETE);
		    db.AddInParameter(dbCommand, "@RELATION_ID", DbType.Int32, aRelationId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}
		/// <summary>
		/// UpdateInfo
		/// </summary>
		/// <param name="RELATION_ID">PartPullInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,int aRelationId)
		{
		    string sql = "update [LES].[TM_EPS_PART_PULL] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE 1=1  AND RELATION_ID =@RELATION_ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@RELATION_ID", DbType.Int32, aRelationId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static PartPullInfo CreatePartPullInfo(IDataReader rdr)
		{
			PartPullInfo info = new PartPullInfo();
			info.PartNo = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_NO"));			
			info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));			
			info.AssemblyLine = DBConvert.GetString(rdr, rdr.GetOrdinal("ASSEMBLY_LINE"));			
			info.Location = DBConvert.GetString(rdr, rdr.GetOrdinal("LOCATION"));			
			info.DPlant = DBConvert.GetString(rdr, rdr.GetOrdinal("D_PLANT"));			
			info.DAssemblyLine = DBConvert.GetString(rdr, rdr.GetOrdinal("D_ASSEMBLY_LINE"));			
			info.DLocation = DBConvert.GetString(rdr, rdr.GetOrdinal("D_LOCATION"));			
			info.EPlant = DBConvert.GetString(rdr, rdr.GetOrdinal("E_PLANT"));			
			info.EAssemblyLine = DBConvert.GetString(rdr, rdr.GetOrdinal("E_ASSEMBLY_LINE"));			
			info.ELocation = DBConvert.GetString(rdr, rdr.GetOrdinal("E_LOCATION"));			
			info.Usage = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("USAGE"));			
			info.Package = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PACKAGE"));			
			info.ScreenLocation = DBConvert.GetString(rdr, rdr.GetOrdinal("SCREEN_LOCATION"));			
			info.PullType = DBConvert.GetInt32(rdr, rdr.GetOrdinal("PULL_TYPE"));			
			info.CombinationType = DBConvert.GetInt32(rdr, rdr.GetOrdinal("COMBINATION_TYPE"));			
			info.DeliverTime = DBConvert.GetInt32(rdr, rdr.GetOrdinal("DELIVER_TIME"));			
			info.AlarmTime = DBConvert.GetInt32(rdr, rdr.GetOrdinal("ALARM_TIME"));			
			info.TriggerStatus = DBConvert.GetInt32(rdr, rdr.GetOrdinal("TRIGGER_STATUS"));			
			info.ButtonId = DBConvert.GetString(rdr, rdr.GetOrdinal("BUTTON_ID"));			
			info.BahnhofNo = DBConvert.GetString(rdr, rdr.GetOrdinal("BAHNHOF_NO"));			
			info.Route = DBConvert.GetString(rdr, rdr.GetOrdinal("ROUTE"));			
			info.Dolly = DBConvert.GetString(rdr, rdr.GetOrdinal("DOLLY"));			
			info.Comments = DBConvert.GetString(rdr, rdr.GetOrdinal("COMMENTS"));			
			info.TargetZone = DBConvert.GetString(rdr, rdr.GetOrdinal("TARGET_ZONE"));			
			info.TargetWm = DBConvert.GetString(rdr, rdr.GetOrdinal("TARGET_WM"));			
			info.CreateDate = DBConvert.GetDateTime(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.RelationId = DBConvert.GetInt32(rdr, rdr.GetOrdinal("RELATION_ID"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.UpdateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("UPDATE_DATE"));			
			info.UpdateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("UPDATE_USER"));			
			return info;
		}
		
		#endregion
	}
}
