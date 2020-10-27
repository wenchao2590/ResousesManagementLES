#region Declaim
//---------------------------------------------------------------------------
// Name:		LocationDAL
// Function: 	Expose data in table TM_BAS_LOCATION from database as business object to MES system.
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
    /// LocationDAL对应表[TM_BAS_LOCATION]
    /// </summary>
    public partial class LocationDAL : BusinessObjectProvider<LocationInfo>
	{
		#region Sql Statements
		private const string TM_BAS_LOCATION_SELECT_BY_ID =
			@"SELECT PLANT,
				WORKSHOP,
				ASSEMBLY_LINE,
				WORKSHOP_SECTION,
				LOCATION,
				PLANT_ZONE,
				LOCATION_TYPE,
				REGION,
				FOOTPRINT,
				FOOTPRINT_NO,
				SEQUENCE_NO,
				PICKUP_SEQ_NO,
				DCP_POINT,
				COMMENTS,
				MODIFY_USER,
				ID,
				VALID_FLAG,
				CREATE_USER,
				MODIFY_DATE,
				FID,
				LOCATION_NAME,
				CREATE_DATE				  
				FROM [LES].[TM_BAS_LOCATION] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TM_BAS_LOCATION_SELECT = 
			@"SELECT PLANT,
				WORKSHOP,
				ASSEMBLY_LINE,
				WORKSHOP_SECTION,
				LOCATION,
				PLANT_ZONE,
				LOCATION_TYPE,
				REGION,
				FOOTPRINT,
				FOOTPRINT_NO,
				SEQUENCE_NO,
				PICKUP_SEQ_NO,
				DCP_POINT,
				COMMENTS,
				MODIFY_USER,
				ID,
				VALID_FLAG,
				CREATE_USER,
				MODIFY_DATE,
				FID,
				LOCATION_NAME,
				CREATE_DATE				 
				FROM [LES].[TM_BAS_LOCATION] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TM_BAS_LOCATION_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TM_BAS_LOCATION]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TM_BAS_LOCATION_INSERT =
			@"INSERT INTO [LES].[TM_BAS_LOCATION] (
				PLANT,
				WORKSHOP,
				ASSEMBLY_LINE,
				WORKSHOP_SECTION,
				LOCATION,
				PLANT_ZONE,
				LOCATION_TYPE,
				REGION,
				FOOTPRINT,
				FOOTPRINT_NO,
				SEQUENCE_NO,
				PICKUP_SEQ_NO,
				DCP_POINT,
				COMMENTS,
				MODIFY_USER,
				VALID_FLAG,
				CREATE_USER,
				MODIFY_DATE,
				FID,
				LOCATION_NAME,
				CREATE_DATE				 
			) VALUES (
				@PLANT,
				@WORKSHOP,
				@ASSEMBLY_LINE,
				@WORKSHOP_SECTION,
				@LOCATION,
				@PLANT_ZONE,
				@LOCATION_TYPE,
				@REGION,
				@FOOTPRINT,
				@FOOTPRINT_NO,
				@SEQUENCE_NO,
				@PICKUP_SEQ_NO,
				@DCP_POINT,
				@COMMENTS,
				@MODIFY_USER,
				@VALID_FLAG,
				@CREATE_USER,
				@MODIFY_DATE,
				@FID,
				@LOCATION_NAME,
				GETDATE()				 
			);SELECT @@IDENTITY;";
		private const string TM_BAS_LOCATION_UPDATE =
			@"UPDATE [LES].[TM_BAS_LOCATION] WITH(ROWLOCK) 
				SET PLANT=@PLANT,
				WORKSHOP=@WORKSHOP,
				ASSEMBLY_LINE=@ASSEMBLY_LINE,
				WORKSHOP_SECTION=@WORKSHOP_SECTION,
				LOCATION=@LOCATION,
				PLANT_ZONE=@PLANT_ZONE,
				LOCATION_TYPE=@LOCATION_TYPE,
				REGION=@REGION,
				FOOTPRINT=@FOOTPRINT,
				FOOTPRINT_NO=@FOOTPRINT_NO,
				SEQUENCE_NO=@SEQUENCE_NO,
				PICKUP_SEQ_NO=@PICKUP_SEQ_NO,
				DCP_POINT=@DCP_POINT,
				COMMENTS=@COMMENTS,
				MODIFY_USER=@MODIFY_USER,
				VALID_FLAG=@VALID_FLAG,
				CREATE_USER=@CREATE_USER,
				MODIFY_DATE=@MODIFY_DATE,
				FID=@FID,
				LOCATION_NAME=@LOCATION_NAME,
				CREATE_DATE=@CREATE_DATE				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TM_BAS_LOCATION_DELETE =
			@"DELETE FROM [LES].[TM_BAS_LOCATION] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get LocationInfo
		/// </summary>
		/// <param name="ID">LocationInfo Primary key </param>
		/// <returns></returns> 
		public LocationInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_BAS_LOCATION_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateLocationInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>LocationInfo Collection </returns>
		public List<LocationInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TM_BAS_LOCATION_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>LocationInfo Collection </returns>
		public List<LocationInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<LocationInfo> list = new List<LocationInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateLocationInfo(dr));
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
		public List<LocationInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TM_BAS_LOCATION]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<LocationInfo> list = new List<LocationInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateLocationInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TM_BAS_LOCATION_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(LocationInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TM_BAS_LOCATION_INSERT);			
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@WORKSHOP_SECTION", DbType.String, info.WorkshopSection);
			db.AddInParameter(dbCommand, "@LOCATION", DbType.String, info.Location);
			db.AddInParameter(dbCommand, "@PLANT_ZONE", DbType.String, info.PlantZone);
			db.AddInParameter(dbCommand, "@LOCATION_TYPE", DbType.Int32, info.LocationType);
			db.AddInParameter(dbCommand, "@REGION", DbType.String, info.Region);
			db.AddInParameter(dbCommand, "@FOOTPRINT", DbType.Int32, info.Footprint);
			db.AddInParameter(dbCommand, "@FOOTPRINT_NO", DbType.Int32, info.FootprintNo);
			db.AddInParameter(dbCommand, "@SEQUENCE_NO", DbType.Int32, info.SequenceNo);
			db.AddInParameter(dbCommand, "@PICKUP_SEQ_NO", DbType.Int32, info.PickupSeqNo);
			db.AddInParameter(dbCommand, "@DCP_POINT", DbType.String, info.DcpPoint);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@LOCATION_NAME", DbType.String, info.LocationName);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}
		/// <summary>
		/// GetInsertSql
		/// </summary>
		/// <param name="info"> info</param>
		public static string GetInsertSql(LocationInfo info)
		{
			return  
			@"insert into [LES].[TM_BAS_LOCATION] (
				PLANT,
				WORKSHOP,
				ASSEMBLY_LINE,
				WORKSHOP_SECTION,
				LOCATION,
				PLANT_ZONE,
				LOCATION_TYPE,
				REGION,
				FOOTPRINT,
				FOOTPRINT_NO,
				SEQUENCE_NO,
				PICKUP_SEQ_NO,
				DCP_POINT,
				COMMENTS,
				MODIFY_USER,
				VALID_FLAG,
				CREATE_USER,
				MODIFY_DATE,
				FID,
				LOCATION_NAME,
				CREATE_DATE				 
			) values ("+
				(string.IsNullOrEmpty(info.Plant) ? "NULL" : "N'" + info.Plant + "'") + ","+
				(string.IsNullOrEmpty(info.Workshop) ? "NULL" : "N'" + info.Workshop + "'") + ","+
				(string.IsNullOrEmpty(info.AssemblyLine) ? "NULL" : "N'" + info.AssemblyLine + "'") + ","+
				(string.IsNullOrEmpty(info.WorkshopSection) ? "NULL" : "N'" + info.WorkshopSection + "'") + ","+
				(string.IsNullOrEmpty(info.Location) ? "NULL" : "N'" + info.Location + "'") + ","+
				(string.IsNullOrEmpty(info.PlantZone) ? "NULL" : "N'" + info.PlantZone + "'") + ","+
				(info.LocationType == null ? "NULL" : "" + info.LocationType.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.Region) ? "NULL" : "N'" + info.Region + "'") + ","+
				(info.Footprint == null ? "NULL" : "" + info.Footprint.GetValueOrDefault() + "") + ","+
				(info.FootprintNo == null ? "NULL" : "" + info.FootprintNo.GetValueOrDefault() + "") + ","+
				(info.SequenceNo == null ? "NULL" : "" + info.SequenceNo.GetValueOrDefault() + "") + ","+
				(info.PickupSeqNo == null ? "NULL" : "" + info.PickupSeqNo.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.DcpPoint) ? "NULL" : "N'" + info.DcpPoint + "'") + ","+
				(string.IsNullOrEmpty(info.Comments) ? "NULL" : "N'" + info.Comments + "'") + ","+
				"NULL" + ","+			
				"1" + ","+		
				"N'" + info.CreateUser + "'" + ","+		
				"NULL" + ","+			
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.LocationName) ? "NULL" : "N'" + info.LocationName + "'") + ","+
				"GETDATE()" + ");";			
				}
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(LocationInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_BAS_LOCATION_UPDATE);				
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@WORKSHOP_SECTION", DbType.String, info.WorkshopSection);
			db.AddInParameter(dbCommand, "@LOCATION", DbType.String, info.Location);
			db.AddInParameter(dbCommand, "@PLANT_ZONE", DbType.String, info.PlantZone);
			db.AddInParameter(dbCommand, "@LOCATION_TYPE", DbType.Int32, info.LocationType);
			db.AddInParameter(dbCommand, "@REGION", DbType.String, info.Region);
			db.AddInParameter(dbCommand, "@FOOTPRINT", DbType.Int32, info.Footprint);
			db.AddInParameter(dbCommand, "@FOOTPRINT_NO", DbType.Int32, info.FootprintNo);
			db.AddInParameter(dbCommand, "@SEQUENCE_NO", DbType.Int32, info.SequenceNo);
			db.AddInParameter(dbCommand, "@PICKUP_SEQ_NO", DbType.Int32, info.PickupSeqNo);
			db.AddInParameter(dbCommand, "@DCP_POINT", DbType.String, info.DcpPoint);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@LOCATION_NAME", DbType.String, info.LocationName);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="ID">LocationInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_BAS_LOCATION_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">LocationInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TM_BAS_LOCATION] WITH(ROWLOCK) "
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
		/// <param name="ID">LocationInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TM_BAS_LOCATION] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static LocationInfo CreateLocationInfo(IDataReader rdr)
		{
			LocationInfo info = new LocationInfo();
			info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));			
			info.Workshop = DBConvert.GetString(rdr, rdr.GetOrdinal("WORKSHOP"));			
			info.AssemblyLine = DBConvert.GetString(rdr, rdr.GetOrdinal("ASSEMBLY_LINE"));			
			info.WorkshopSection = DBConvert.GetString(rdr, rdr.GetOrdinal("WORKSHOP_SECTION"));			
			info.Location = DBConvert.GetString(rdr, rdr.GetOrdinal("LOCATION"));			
			info.PlantZone = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT_ZONE"));			
			info.LocationType = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("LOCATION_TYPE"));			
			info.Region = DBConvert.GetString(rdr, rdr.GetOrdinal("REGION"));			
			info.Footprint = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("FOOTPRINT"));			
			info.FootprintNo = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("FOOTPRINT_NO"));			
			info.SequenceNo = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("SEQUENCE_NO"));			
			info.PickupSeqNo = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PICKUP_SEQ_NO"));			
			info.DcpPoint = DBConvert.GetString(rdr, rdr.GetOrdinal("DCP_POINT"));			
			info.Comments = DBConvert.GetString(rdr, rdr.GetOrdinal("COMMENTS"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.ValidFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.LocationName = DBConvert.GetString(rdr, rdr.GetOrdinal("LOCATION_NAME"));			
			info.CreateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			return info;
		}
		
		#endregion
	}
}