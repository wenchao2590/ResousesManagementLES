#region Declaim
//---------------------------------------------------------------------------
// Name:		TwdCounterLogDAL
// Function: 	Expose data in table TT_MPM_TWD_COUNTER_LOG from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月19日
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
    /// TwdCounterLogDAL对应表[TT_MPM_TWD_COUNTER_LOG]
    /// </summary>
    public partial class TwdCounterLogDAL : BusinessObjectProvider<TwdCounterLogInfo>
	{
		#region Sql Statements
		private const string TT_MPM_TWD_COUNTER_LOG_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				COUNTER_FID,
				SOURCE_DATA_FID,
				SOURCE_DATA_TYPE,
				REQUIREMENT_ACCUMULATE_MODE,
				SOURCE_DATA,
				PART_QTY,
				PART_NO,
				PART_CNAME,
				PART_VERSION,
				SUPPLIER_NUM,
				PLANT,
				PLANT_ZONE,
				WORKSHOP,
				ASSEMBLY_LINE,
				WORKSHOP_SECTION,
				LOCATION,
				PACKAGE,
				PACKAGE_MODEL,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				  
				FROM [LES].[TT_MPM_TWD_COUNTER_LOG] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TT_MPM_TWD_COUNTER_LOG_SELECT = 
			@"SELECT ID,
				FID,
				COUNTER_FID,
				SOURCE_DATA_FID,
				SOURCE_DATA_TYPE,
				REQUIREMENT_ACCUMULATE_MODE,
				SOURCE_DATA,
				PART_QTY,
				PART_NO,
				PART_CNAME,
				PART_VERSION,
				SUPPLIER_NUM,
				PLANT,
				PLANT_ZONE,
				WORKSHOP,
				ASSEMBLY_LINE,
				WORKSHOP_SECTION,
				LOCATION,
				PACKAGE,
				PACKAGE_MODEL,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
				FROM [LES].[TT_MPM_TWD_COUNTER_LOG] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TT_MPM_TWD_COUNTER_LOG_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TT_MPM_TWD_COUNTER_LOG]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TT_MPM_TWD_COUNTER_LOG_INSERT =
			@"INSERT INTO [LES].[TT_MPM_TWD_COUNTER_LOG] (
				FID,
				COUNTER_FID,
				SOURCE_DATA_FID,
				SOURCE_DATA_TYPE,
				REQUIREMENT_ACCUMULATE_MODE,
				SOURCE_DATA,
				PART_QTY,
				PART_NO,
				PART_CNAME,
				PART_VERSION,
				SUPPLIER_NUM,
				PLANT,
				PLANT_ZONE,
				WORKSHOP,
				ASSEMBLY_LINE,
				WORKSHOP_SECTION,
				LOCATION,
				PACKAGE,
				PACKAGE_MODEL,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
			) VALUES (
				@FID,
				@COUNTER_FID,
				@SOURCE_DATA_FID,
				@SOURCE_DATA_TYPE,
				@REQUIREMENT_ACCUMULATE_MODE,
				@SOURCE_DATA,
				@PART_QTY,
				@PART_NO,
				@PART_CNAME,
				@PART_VERSION,
				@SUPPLIER_NUM,
				@PLANT,
				@PLANT_ZONE,
				@WORKSHOP,
				@ASSEMBLY_LINE,
				@WORKSHOP_SECTION,
				@LOCATION,
				@PACKAGE,
				@PACKAGE_MODEL,
				@COMMENTS,
				@VALID_FLAG,
				GETDATE(),
				@CREATE_USER,
				@MODIFY_DATE,
				@MODIFY_USER				 
			);SELECT @@IDENTITY;";
		private const string TT_MPM_TWD_COUNTER_LOG_UPDATE =
			@"UPDATE [LES].[TT_MPM_TWD_COUNTER_LOG] WITH(ROWLOCK) 
				SET FID=@FID,
				COUNTER_FID=@COUNTER_FID,
				SOURCE_DATA_FID=@SOURCE_DATA_FID,
				SOURCE_DATA_TYPE=@SOURCE_DATA_TYPE,
				REQUIREMENT_ACCUMULATE_MODE=@REQUIREMENT_ACCUMULATE_MODE,
				SOURCE_DATA=@SOURCE_DATA,
				PART_QTY=@PART_QTY,
				PART_NO=@PART_NO,
				PART_CNAME=@PART_CNAME,
				PART_VERSION=@PART_VERSION,
				SUPPLIER_NUM=@SUPPLIER_NUM,
				PLANT=@PLANT,
				PLANT_ZONE=@PLANT_ZONE,
				WORKSHOP=@WORKSHOP,
				ASSEMBLY_LINE=@ASSEMBLY_LINE,
				WORKSHOP_SECTION=@WORKSHOP_SECTION,
				LOCATION=@LOCATION,
				PACKAGE=@PACKAGE,
				PACKAGE_MODEL=@PACKAGE_MODEL,
				COMMENTS=@COMMENTS,
				VALID_FLAG=@VALID_FLAG,
				CREATE_DATE=@CREATE_DATE,
				CREATE_USER=@CREATE_USER,
				MODIFY_DATE=@MODIFY_DATE,
				MODIFY_USER=@MODIFY_USER				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TT_MPM_TWD_COUNTER_LOG_DELETE =
			@"DELETE FROM [LES].[TT_MPM_TWD_COUNTER_LOG] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get TwdCounterLogInfo
		/// </summary>
		/// <param name="ID">TwdCounterLogInfo Primary key </param>
		/// <returns></returns> 
		public TwdCounterLogInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_MPM_TWD_COUNTER_LOG_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateTwdCounterLogInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>TwdCounterLogInfo Collection </returns>
		public List<TwdCounterLogInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TT_MPM_TWD_COUNTER_LOG_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>TwdCounterLogInfo Collection </returns>
		public List<TwdCounterLogInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<TwdCounterLogInfo> list = new List<TwdCounterLogInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateTwdCounterLogInfo(dr));
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
		public List<TwdCounterLogInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TT_MPM_TWD_COUNTER_LOG]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<TwdCounterLogInfo> list = new List<TwdCounterLogInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateTwdCounterLogInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TT_MPM_TWD_COUNTER_LOG_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(TwdCounterLogInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TT_MPM_TWD_COUNTER_LOG_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@COUNTER_FID", DbType.Guid, info.CounterFid);
			db.AddInParameter(dbCommand, "@SOURCE_DATA_FID", DbType.Guid, info.SourceDataFid);
			db.AddInParameter(dbCommand, "@SOURCE_DATA_TYPE", DbType.Int32, info.SourceDataType);
			db.AddInParameter(dbCommand, "@REQUIREMENT_ACCUMULATE_MODE", DbType.Int32, info.RequirementAccumulateMode);
			db.AddInParameter(dbCommand, "@SOURCE_DATA", DbType.String, info.SourceData);
			db.AddInParameter(dbCommand, "@PART_QTY", DbType.Decimal, info.PartQty);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@PART_CNAME", DbType.String, info.PartCname);
			db.AddInParameter(dbCommand, "@PART_VERSION", DbType.String, info.PartVersion);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@PLANT_ZONE", DbType.String, info.PlantZone);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@WORKSHOP_SECTION", DbType.String, info.WorkshopSection);
			db.AddInParameter(dbCommand, "@LOCATION", DbType.String, info.Location);
			db.AddInParameter(dbCommand, "@PACKAGE", DbType.Decimal, info.Package);
			db.AddInParameter(dbCommand, "@PACKAGE_MODEL", DbType.String, info.PackageModel);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}
		/// <summary>
		/// GetInsertSql
		/// </summary>
		/// <param name="info"> info</param>
		public static string GetInsertSql(TwdCounterLogInfo info)
		{
			return  
			@"insert into [LES].[TT_MPM_TWD_COUNTER_LOG] (
				FID,
				COUNTER_FID,
				SOURCE_DATA_FID,
				SOURCE_DATA_TYPE,
				REQUIREMENT_ACCUMULATE_MODE,
				SOURCE_DATA,
				PART_QTY,
				PART_NO,
				PART_CNAME,
				PART_VERSION,
				SUPPLIER_NUM,
				PLANT,
				PLANT_ZONE,
				WORKSHOP,
				ASSEMBLY_LINE,
				WORKSHOP_SECTION,
				LOCATION,
				PACKAGE,
				PACKAGE_MODEL,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
			) values ("+
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				(info.CounterFid == null ? "NULL" : "N'" + info.CounterFid.GetValueOrDefault() + "'") + ","+
				(info.SourceDataFid == null ? "NULL" : "N'" + info.SourceDataFid.GetValueOrDefault() + "'") + ","+
				(info.SourceDataType == null ? "NULL" : "" + info.SourceDataType.GetValueOrDefault() + "") + ","+
				(info.RequirementAccumulateMode == null ? "NULL" : "" + info.RequirementAccumulateMode.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.SourceData) ? "NULL" : "N'" + info.SourceData + "'") + ","+
				(info.PartQty == null ? "NULL" : "" + info.PartQty.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.PartNo) ? "NULL" : "N'" + info.PartNo + "'") + ","+
				(string.IsNullOrEmpty(info.PartCname) ? "NULL" : "N'" + info.PartCname + "'") + ","+
				(string.IsNullOrEmpty(info.PartVersion) ? "NULL" : "N'" + info.PartVersion + "'") + ","+
				(string.IsNullOrEmpty(info.SupplierNum) ? "NULL" : "N'" + info.SupplierNum + "'") + ","+
				(string.IsNullOrEmpty(info.Plant) ? "NULL" : "N'" + info.Plant + "'") + ","+
				(string.IsNullOrEmpty(info.PlantZone) ? "NULL" : "N'" + info.PlantZone + "'") + ","+
				(string.IsNullOrEmpty(info.Workshop) ? "NULL" : "N'" + info.Workshop + "'") + ","+
				(string.IsNullOrEmpty(info.AssemblyLine) ? "NULL" : "N'" + info.AssemblyLine + "'") + ","+
				(string.IsNullOrEmpty(info.WorkshopSection) ? "NULL" : "N'" + info.WorkshopSection + "'") + ","+
				(string.IsNullOrEmpty(info.Location) ? "NULL" : "N'" + info.Location + "'") + ","+
				(info.Package == null ? "NULL" : "" + info.Package.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.PackageModel) ? "NULL" : "N'" + info.PackageModel + "'") + ","+
				(string.IsNullOrEmpty(info.Comments) ? "NULL" : "N'" + info.Comments + "'") + ","+
				"1" + ","+		
				"GETDATE()" + ","+			
				"N'" + info.CreateUser + "'" + ","+		
				"NULL" + ","+			
				"NULL" + ");";			
				}
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(TwdCounterLogInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_MPM_TWD_COUNTER_LOG_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@COUNTER_FID", DbType.Guid, info.CounterFid);
			db.AddInParameter(dbCommand, "@SOURCE_DATA_FID", DbType.Guid, info.SourceDataFid);
			db.AddInParameter(dbCommand, "@SOURCE_DATA_TYPE", DbType.Int32, info.SourceDataType);
			db.AddInParameter(dbCommand, "@REQUIREMENT_ACCUMULATE_MODE", DbType.Int32, info.RequirementAccumulateMode);
			db.AddInParameter(dbCommand, "@SOURCE_DATA", DbType.String, info.SourceData);
			db.AddInParameter(dbCommand, "@PART_QTY", DbType.Decimal, info.PartQty);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@PART_CNAME", DbType.String, info.PartCname);
			db.AddInParameter(dbCommand, "@PART_VERSION", DbType.String, info.PartVersion);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@PLANT_ZONE", DbType.String, info.PlantZone);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@WORKSHOP_SECTION", DbType.String, info.WorkshopSection);
			db.AddInParameter(dbCommand, "@LOCATION", DbType.String, info.Location);
			db.AddInParameter(dbCommand, "@PACKAGE", DbType.Decimal, info.Package);
			db.AddInParameter(dbCommand, "@PACKAGE_MODEL", DbType.String, info.PackageModel);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="ID">TwdCounterLogInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_MPM_TWD_COUNTER_LOG_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">TwdCounterLogInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TT_MPM_TWD_COUNTER_LOG] WITH(ROWLOCK) "
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
		/// <param name="ID">TwdCounterLogInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TT_MPM_TWD_COUNTER_LOG] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static TwdCounterLogInfo CreateTwdCounterLogInfo(IDataReader rdr)
		{
			TwdCounterLogInfo info = new TwdCounterLogInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.CounterFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("COUNTER_FID"));			
			info.SourceDataFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("SOURCE_DATA_FID"));			
			info.SourceDataType = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("SOURCE_DATA_TYPE"));			
			info.RequirementAccumulateMode = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("REQUIREMENT_ACCUMULATE_MODE"));			
			info.SourceData = DBConvert.GetString(rdr, rdr.GetOrdinal("SOURCE_DATA"));			
			info.PartQty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("PART_QTY"));			
			info.PartNo = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_NO"));			
			info.PartCname = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_CNAME"));			
			info.PartVersion = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_VERSION"));			
			info.SupplierNum = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NUM"));			
			info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));			
			info.PlantZone = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT_ZONE"));			
			info.Workshop = DBConvert.GetString(rdr, rdr.GetOrdinal("WORKSHOP"));			
			info.AssemblyLine = DBConvert.GetString(rdr, rdr.GetOrdinal("ASSEMBLY_LINE"));			
			info.WorkshopSection = DBConvert.GetString(rdr, rdr.GetOrdinal("WORKSHOP_SECTION"));			
			info.Location = DBConvert.GetString(rdr, rdr.GetOrdinal("LOCATION"));			
			info.Package = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("PACKAGE"));			
			info.PackageModel = DBConvert.GetString(rdr, rdr.GetOrdinal("PACKAGE_MODEL"));			
			info.Comments = DBConvert.GetString(rdr, rdr.GetOrdinal("COMMENTS"));			
			info.ValidFlag = DBConvert.GetBool(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			info.CreateDate = DBConvert.GetDateTime(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			return info;
		}
		
		#endregion
	}
}
