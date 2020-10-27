#region Declaim
//---------------------------------------------------------------------------
// Name:		WarehouseLocationDAL
// Function: 	Expose data in table TM_BAS_WAREHOUSE_LOCATION from database as business object to MES system.
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
    /// WarehouseLocationDAL对应表[TM_BAS_WAREHOUSE_LOCATION]
    /// </summary>
    public partial class WarehouseLocationDAL : BusinessObjectProvider<WarehouseLocationInfo>
	{
		#region Sql Statements
		private const string TM_BAS_WAREHOUSE_LOCATION_SELECT_BY_ID =
			@"SELECT PLANT,
				WM_NO,
				ZONE_NO,
				DLOC,
				ASSEMBLY_LINE,
				PLANT_ZONE,
				WORKSHOP,
				STORAGE_LOCATION_NAME,
				LOCATION_TYPE,
				SEQUENCE_NO,
				COUNT_PARTITION,
				PART_CLASSIFY_AREA_NO,
				LANE_NO,
				SHELVES_NO,
				LAYER_NO,
				GRID_NO,
				COMMENTS,
				MODIFY_USER,
				CREATE_USER,
				ID,
				MODIFY_DATE,
				CREATE_DATE,
				FID,
				VALID_FLAG				  
				FROM [LES].[TM_BAS_WAREHOUSE_LOCATION] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TM_BAS_WAREHOUSE_LOCATION_SELECT = 
			@"SELECT PLANT,
				WM_NO,
				ZONE_NO,
				DLOC,
				ASSEMBLY_LINE,
				PLANT_ZONE,
				WORKSHOP,
				STORAGE_LOCATION_NAME,
				LOCATION_TYPE,
				SEQUENCE_NO,
				COUNT_PARTITION,
				PART_CLASSIFY_AREA_NO,
				LANE_NO,
				SHELVES_NO,
				LAYER_NO,
				GRID_NO,
				COMMENTS,
				MODIFY_USER,
				CREATE_USER,
				ID,
				MODIFY_DATE,
				CREATE_DATE,
				FID,
				VALID_FLAG				 
				FROM [LES].[TM_BAS_WAREHOUSE_LOCATION] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TM_BAS_WAREHOUSE_LOCATION_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TM_BAS_WAREHOUSE_LOCATION]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TM_BAS_WAREHOUSE_LOCATION_INSERT =
			@"INSERT INTO [LES].[TM_BAS_WAREHOUSE_LOCATION] (
				PLANT,
				WM_NO,
				ZONE_NO,
				DLOC,
				ASSEMBLY_LINE,
				PLANT_ZONE,
				WORKSHOP,
				STORAGE_LOCATION_NAME,
				LOCATION_TYPE,
				SEQUENCE_NO,
				COUNT_PARTITION,
				PART_CLASSIFY_AREA_NO,
				LANE_NO,
				SHELVES_NO,
				LAYER_NO,
				GRID_NO,
				COMMENTS,
				MODIFY_USER,
				CREATE_USER,
				MODIFY_DATE,
				CREATE_DATE,
				FID,
				VALID_FLAG				 
			) VALUES (
				@PLANT,
				@WM_NO,
				@ZONE_NO,
				@DLOC,
				@ASSEMBLY_LINE,
				@PLANT_ZONE,
				@WORKSHOP,
				@STORAGE_LOCATION_NAME,
				@LOCATION_TYPE,
				@SEQUENCE_NO,
				@COUNT_PARTITION,
				@PART_CLASSIFY_AREA_NO,
				@LANE_NO,
				@SHELVES_NO,
				@LAYER_NO,
				@GRID_NO,
				@COMMENTS,
				@MODIFY_USER,
				@CREATE_USER,
				@MODIFY_DATE,
				GETDATE(),
				@FID,
				@VALID_FLAG				 
			);SELECT @@IDENTITY;";
		private const string TM_BAS_WAREHOUSE_LOCATION_UPDATE =
			@"UPDATE [LES].[TM_BAS_WAREHOUSE_LOCATION] WITH(ROWLOCK) 
				SET PLANT=@PLANT,
				WM_NO=@WM_NO,
				ZONE_NO=@ZONE_NO,
				DLOC=@DLOC,
				ASSEMBLY_LINE=@ASSEMBLY_LINE,
				PLANT_ZONE=@PLANT_ZONE,
				WORKSHOP=@WORKSHOP,
				STORAGE_LOCATION_NAME=@STORAGE_LOCATION_NAME,
				LOCATION_TYPE=@LOCATION_TYPE,
				SEQUENCE_NO=@SEQUENCE_NO,
				COUNT_PARTITION=@COUNT_PARTITION,
				PART_CLASSIFY_AREA_NO=@PART_CLASSIFY_AREA_NO,
				LANE_NO=@LANE_NO,
				SHELVES_NO=@SHELVES_NO,
				LAYER_NO=@LAYER_NO,
				GRID_NO=@GRID_NO,
				COMMENTS=@COMMENTS,
				MODIFY_USER=@MODIFY_USER,
				CREATE_USER=@CREATE_USER,
				MODIFY_DATE=@MODIFY_DATE,
				CREATE_DATE=@CREATE_DATE,
				FID=@FID,
				VALID_FLAG=@VALID_FLAG				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TM_BAS_WAREHOUSE_LOCATION_DELETE =
			@"DELETE FROM [LES].[TM_BAS_WAREHOUSE_LOCATION] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get WarehouseLocationInfo
		/// </summary>
		/// <param name="ID">WarehouseLocationInfo Primary key </param>
		/// <returns></returns> 
		public WarehouseLocationInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_BAS_WAREHOUSE_LOCATION_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateWarehouseLocationInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>WarehouseLocationInfo Collection </returns>
		public List<WarehouseLocationInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TM_BAS_WAREHOUSE_LOCATION_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>WarehouseLocationInfo Collection </returns>
		public List<WarehouseLocationInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<WarehouseLocationInfo> list = new List<WarehouseLocationInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateWarehouseLocationInfo(dr));
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
		public List<WarehouseLocationInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TM_BAS_WAREHOUSE_LOCATION]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<WarehouseLocationInfo> list = new List<WarehouseLocationInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateWarehouseLocationInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TM_BAS_WAREHOUSE_LOCATION_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(WarehouseLocationInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TM_BAS_WAREHOUSE_LOCATION_INSERT);			
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@WM_NO", DbType.String, info.WmNo);
			db.AddInParameter(dbCommand, "@ZONE_NO", DbType.String, info.ZoneNo);
			db.AddInParameter(dbCommand, "@DLOC", DbType.String, info.Dloc);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@PLANT_ZONE", DbType.String, info.PlantZone);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@STORAGE_LOCATION_NAME", DbType.String, info.StorageLocationName);
			db.AddInParameter(dbCommand, "@LOCATION_TYPE", DbType.Int32, info.LocationType);
			db.AddInParameter(dbCommand, "@SEQUENCE_NO", DbType.Int32, info.SequenceNo);
			db.AddInParameter(dbCommand, "@COUNT_PARTITION", DbType.String, info.CountPartition);
			db.AddInParameter(dbCommand, "@PART_CLASSIFY_AREA_NO", DbType.String, info.PartClassifyAreaNo);
			db.AddInParameter(dbCommand, "@LANE_NO", DbType.Int32, info.LaneNo);
			db.AddInParameter(dbCommand, "@SHELVES_NO", DbType.Int32, info.ShelvesNo);
			db.AddInParameter(dbCommand, "@LAYER_NO", DbType.Int32, info.LayerNo);
			db.AddInParameter(dbCommand, "@GRID_NO", DbType.Int32, info.GridNo);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}
		/// <summary>
		/// GetInsertSql
		/// </summary>
		/// <param name="info"> info</param>
		public static string GetInsertSql(WarehouseLocationInfo info)
		{
			return  
			@"insert into [LES].[TM_BAS_WAREHOUSE_LOCATION] (
				PLANT,
				WM_NO,
				ZONE_NO,
				DLOC,
				ASSEMBLY_LINE,
				PLANT_ZONE,
				WORKSHOP,
				STORAGE_LOCATION_NAME,
				LOCATION_TYPE,
				SEQUENCE_NO,
				COUNT_PARTITION,
				PART_CLASSIFY_AREA_NO,
				LANE_NO,
				SHELVES_NO,
				LAYER_NO,
				GRID_NO,
				COMMENTS,
				MODIFY_USER,
				CREATE_USER,
				MODIFY_DATE,
				CREATE_DATE,
				FID,
				VALID_FLAG				 
			) values ("+
				(string.IsNullOrEmpty(info.Plant) ? "NULL" : "N'" + info.Plant + "'") + ","+
				(string.IsNullOrEmpty(info.WmNo) ? "NULL" : "N'" + info.WmNo + "'") + ","+
				(string.IsNullOrEmpty(info.ZoneNo) ? "NULL" : "N'" + info.ZoneNo + "'") + ","+
				(string.IsNullOrEmpty(info.Dloc) ? "NULL" : "N'" + info.Dloc + "'") + ","+
				(string.IsNullOrEmpty(info.AssemblyLine) ? "NULL" : "N'" + info.AssemblyLine + "'") + ","+
				(string.IsNullOrEmpty(info.PlantZone) ? "NULL" : "N'" + info.PlantZone + "'") + ","+
				(string.IsNullOrEmpty(info.Workshop) ? "NULL" : "N'" + info.Workshop + "'") + ","+
				(string.IsNullOrEmpty(info.StorageLocationName) ? "NULL" : "N'" + info.StorageLocationName + "'") + ","+
				(info.LocationType == null ? "NULL" : "" + info.LocationType.GetValueOrDefault() + "") + ","+
				(info.SequenceNo == null ? "NULL" : "" + info.SequenceNo.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.CountPartition) ? "NULL" : "N'" + info.CountPartition + "'") + ","+
				(string.IsNullOrEmpty(info.PartClassifyAreaNo) ? "NULL" : "N'" + info.PartClassifyAreaNo + "'") + ","+
				(info.LaneNo == null ? "NULL" : "" + info.LaneNo.GetValueOrDefault() + "") + ","+
				(info.ShelvesNo == null ? "NULL" : "" + info.ShelvesNo.GetValueOrDefault() + "") + ","+
				(info.LayerNo == null ? "NULL" : "" + info.LayerNo.GetValueOrDefault() + "") + ","+
				(info.GridNo == null ? "NULL" : "" + info.GridNo.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.Comments) ? "NULL" : "N'" + info.Comments + "'") + ","+
				"NULL" + ","+			
				"N'" + info.CreateUser + "'" + ","+		
				"NULL" + ","+			
				"GETDATE()" + ","+			
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				"1" + ");";		
				}
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(WarehouseLocationInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_BAS_WAREHOUSE_LOCATION_UPDATE);				
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@WM_NO", DbType.String, info.WmNo);
			db.AddInParameter(dbCommand, "@ZONE_NO", DbType.String, info.ZoneNo);
			db.AddInParameter(dbCommand, "@DLOC", DbType.String, info.Dloc);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@PLANT_ZONE", DbType.String, info.PlantZone);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@STORAGE_LOCATION_NAME", DbType.String, info.StorageLocationName);
			db.AddInParameter(dbCommand, "@LOCATION_TYPE", DbType.Int32, info.LocationType);
			db.AddInParameter(dbCommand, "@SEQUENCE_NO", DbType.Int32, info.SequenceNo);
			db.AddInParameter(dbCommand, "@COUNT_PARTITION", DbType.String, info.CountPartition);
			db.AddInParameter(dbCommand, "@PART_CLASSIFY_AREA_NO", DbType.String, info.PartClassifyAreaNo);
			db.AddInParameter(dbCommand, "@LANE_NO", DbType.Int32, info.LaneNo);
			db.AddInParameter(dbCommand, "@SHELVES_NO", DbType.Int32, info.ShelvesNo);
			db.AddInParameter(dbCommand, "@LAYER_NO", DbType.Int32, info.LayerNo);
			db.AddInParameter(dbCommand, "@GRID_NO", DbType.Int32, info.GridNo);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="ID">WarehouseLocationInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_BAS_WAREHOUSE_LOCATION_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">WarehouseLocationInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TM_BAS_WAREHOUSE_LOCATION] WITH(ROWLOCK) "
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
		/// <param name="ID">WarehouseLocationInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TM_BAS_WAREHOUSE_LOCATION] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static WarehouseLocationInfo CreateWarehouseLocationInfo(IDataReader rdr)
		{
			WarehouseLocationInfo info = new WarehouseLocationInfo();
			info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));			
			info.WmNo = DBConvert.GetString(rdr, rdr.GetOrdinal("WM_NO"));			
			info.ZoneNo = DBConvert.GetString(rdr, rdr.GetOrdinal("ZONE_NO"));			
			info.Dloc = DBConvert.GetString(rdr, rdr.GetOrdinal("DLOC"));			
			info.AssemblyLine = DBConvert.GetString(rdr, rdr.GetOrdinal("ASSEMBLY_LINE"));			
			info.PlantZone = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT_ZONE"));			
			info.Workshop = DBConvert.GetString(rdr, rdr.GetOrdinal("WORKSHOP"));			
			info.StorageLocationName = DBConvert.GetString(rdr, rdr.GetOrdinal("STORAGE_LOCATION_NAME"));			
			info.LocationType = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("LOCATION_TYPE"));			
			info.SequenceNo = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("SEQUENCE_NO"));			
			info.CountPartition = DBConvert.GetString(rdr, rdr.GetOrdinal("COUNT_PARTITION"));			
			info.PartClassifyAreaNo = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_CLASSIFY_AREA_NO"));			
			info.LaneNo = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("LANE_NO"));			
			info.ShelvesNo = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("SHELVES_NO"));			
			info.LayerNo = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("LAYER_NO"));			
			info.GridNo = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("GRID_NO"));			
			info.Comments = DBConvert.GetString(rdr, rdr.GetOrdinal("COMMENTS"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			info.CreateDate = DBConvert.GetDateTime(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.ValidFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			return info;
		}
		
		#endregion
	}
}
