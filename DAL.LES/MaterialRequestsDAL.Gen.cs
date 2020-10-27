#region Declaim
//---------------------------------------------------------------------------
// Name:		MaterialRequestsDAL
// Function: 	Expose data in table TI_PCS_MATERIAL_REQUESTS from database as business object to MES system.
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
    /// MaterialRequestsDAL对应表[TI_PCS_MATERIAL_REQUESTS]
    /// </summary>
    public partial class MaterialRequestsDAL : BusinessObjectProvider<MaterialRequestsInfo>
	{
		#region Sql Statements
		private const string TI_PCS_MATERIAL_REQUESTS_SELECT_BY_ID =
			@"SELECT INTERFACE_ID,
				PLANT_ZONE,
				WORKSHOP,
				ASSEMBLY_LINE,
				PLANT,
				LOCATION,
				REQUEST_TIME,
				INTERFACE_STATUS,
				PROCESS_TIME,
				PART_NO,
				PART_CNAME,
				PART_ENAME,
				SUPPLIER_NUM,
				DOCK,
				BOX_PARTS,
				INTERFACE_TYPE,
				PACK_COUNT,
				REQURIED_PACK,
				INHOUSE_PACKAGE_MODEL,
				INHOUSE_PACKAGE,
				MEASURING_UNIT_NO,
				EXPECTED_ARRIVAL_TIME,
				RDC_DLOC,
				PICKUP_SEQ_NO,
				SEQUENCE_NO,
				IS_ORGANIZE_SHEET,
				SEND_STATUS,
				SEND_TIME,
				IS_CANCEL,
				WMS_SEND_STATUS,
				WMS_SEND_TIME,
				COMMENTS,
				UPDATE_DATE,
				UPDATE_USER,
				CREATE_DATE,
				CREATE_USER				  
				FROM [LES].[TI_PCS_MATERIAL_REQUESTS] WITH(NOLOCK) WHERE 1=1  AND INTERFACE_ID =@INTERFACE_ID;";
			
		private const string TI_PCS_MATERIAL_REQUESTS_SELECT = 
			@"SELECT INTERFACE_ID,
				PLANT_ZONE,
				WORKSHOP,
				ASSEMBLY_LINE,
				PLANT,
				LOCATION,
				REQUEST_TIME,
				INTERFACE_STATUS,
				PROCESS_TIME,
				PART_NO,
				PART_CNAME,
				PART_ENAME,
				SUPPLIER_NUM,
				DOCK,
				BOX_PARTS,
				INTERFACE_TYPE,
				PACK_COUNT,
				REQURIED_PACK,
				INHOUSE_PACKAGE_MODEL,
				INHOUSE_PACKAGE,
				MEASURING_UNIT_NO,
				EXPECTED_ARRIVAL_TIME,
				RDC_DLOC,
				PICKUP_SEQ_NO,
				SEQUENCE_NO,
				IS_ORGANIZE_SHEET,
				SEND_STATUS,
				SEND_TIME,
				IS_CANCEL,
				WMS_SEND_STATUS,
				WMS_SEND_TIME,
				COMMENTS,
				UPDATE_DATE,
				UPDATE_USER,
				CREATE_DATE,
				CREATE_USER				 
				FROM [LES].[TI_PCS_MATERIAL_REQUESTS] WITH (NOLOCK) WHERE 1=1 {0};";
		
		private const string TI_PCS_MATERIAL_REQUESTS_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TI_PCS_MATERIAL_REQUESTS]  WITH(NOLOCK) WHERE 1=1 {0};";

		private const string TI_PCS_MATERIAL_REQUESTS_INSERT =
			@"INSERT INTO [LES].[TI_PCS_MATERIAL_REQUESTS] (
				PLANT_ZONE,
				WORKSHOP,
				ASSEMBLY_LINE,
				PLANT,
				LOCATION,
				REQUEST_TIME,
				INTERFACE_STATUS,
				PROCESS_TIME,
				PART_NO,
				PART_CNAME,
				PART_ENAME,
				SUPPLIER_NUM,
				DOCK,
				BOX_PARTS,
				INTERFACE_TYPE,
				PACK_COUNT,
				REQURIED_PACK,
				INHOUSE_PACKAGE_MODEL,
				INHOUSE_PACKAGE,
				MEASURING_UNIT_NO,
				EXPECTED_ARRIVAL_TIME,
				RDC_DLOC,
				PICKUP_SEQ_NO,
				SEQUENCE_NO,
				IS_ORGANIZE_SHEET,
				SEND_STATUS,
				SEND_TIME,
				IS_CANCEL,
				WMS_SEND_STATUS,
				WMS_SEND_TIME,
				COMMENTS,
				UPDATE_DATE,
				UPDATE_USER,
				CREATE_DATE,
				CREATE_USER				 
			) VALUES (
				@PLANT_ZONE,
				@WORKSHOP,
				@ASSEMBLY_LINE,
				@PLANT,
				@LOCATION,
				@REQUEST_TIME,
				@INTERFACE_STATUS,
				@PROCESS_TIME,
				@PART_NO,
				@PART_CNAME,
				@PART_ENAME,
				@SUPPLIER_NUM,
				@DOCK,
				@BOX_PARTS,
				@INTERFACE_TYPE,
				@PACK_COUNT,
				@REQURIED_PACK,
				@INHOUSE_PACKAGE_MODEL,
				@INHOUSE_PACKAGE,
				@MEASURING_UNIT_NO,
				@EXPECTED_ARRIVAL_TIME,
				@RDC_DLOC,
				@PICKUP_SEQ_NO,
				@SEQUENCE_NO,
				@IS_ORGANIZE_SHEET,
				@SEND_STATUS,
				@SEND_TIME,
				@IS_CANCEL,
				@WMS_SEND_STATUS,
				@WMS_SEND_TIME,
				@COMMENTS,
				@UPDATE_DATE,
				@UPDATE_USER,
				@CREATE_DATE,
				@CREATE_USER				 
			);SELECT @@IDENTITY;";
		private const string TI_PCS_MATERIAL_REQUESTS_UPDATE =
			@"UPDATE [LES].[TI_PCS_MATERIAL_REQUESTS] WITH(ROWLOCK) 
				SET PLANT_ZONE=@PLANT_ZONE,
				WORKSHOP=@WORKSHOP,
				ASSEMBLY_LINE=@ASSEMBLY_LINE,
				PLANT=@PLANT,
				LOCATION=@LOCATION,
				REQUEST_TIME=@REQUEST_TIME,
				INTERFACE_STATUS=@INTERFACE_STATUS,
				PROCESS_TIME=@PROCESS_TIME,
				PART_NO=@PART_NO,
				PART_CNAME=@PART_CNAME,
				PART_ENAME=@PART_ENAME,
				SUPPLIER_NUM=@SUPPLIER_NUM,
				DOCK=@DOCK,
				BOX_PARTS=@BOX_PARTS,
				INTERFACE_TYPE=@INTERFACE_TYPE,
				PACK_COUNT=@PACK_COUNT,
				REQURIED_PACK=@REQURIED_PACK,
				INHOUSE_PACKAGE_MODEL=@INHOUSE_PACKAGE_MODEL,
				INHOUSE_PACKAGE=@INHOUSE_PACKAGE,
				MEASURING_UNIT_NO=@MEASURING_UNIT_NO,
				EXPECTED_ARRIVAL_TIME=@EXPECTED_ARRIVAL_TIME,
				RDC_DLOC=@RDC_DLOC,
				PICKUP_SEQ_NO=@PICKUP_SEQ_NO,
				SEQUENCE_NO=@SEQUENCE_NO,
				IS_ORGANIZE_SHEET=@IS_ORGANIZE_SHEET,
				SEND_STATUS=@SEND_STATUS,
				SEND_TIME=@SEND_TIME,
				IS_CANCEL=@IS_CANCEL,
				WMS_SEND_STATUS=@WMS_SEND_STATUS,
				WMS_SEND_TIME=@WMS_SEND_TIME,
				COMMENTS=@COMMENTS,
				UPDATE_DATE=@UPDATE_DATE,
				UPDATE_USER=@UPDATE_USER,
				CREATE_DATE=@CREATE_DATE,
				CREATE_USER=@CREATE_USER				 
				WHERE 1=1  AND INTERFACE_ID =@INTERFACE_ID;";

		private const string TI_PCS_MATERIAL_REQUESTS_DELETE =
			@"DELETE FROM [LES].[TI_PCS_MATERIAL_REQUESTS] WITH(ROWLOCK)  
				WHERE 1=1  AND INTERFACE_ID =@INTERFACE_ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get MaterialRequestsInfo
		/// </summary>
		/// <param name="INTERFACE_ID">MaterialRequestsInfo Primary key </param>
		/// <returns></returns> 
		public MaterialRequestsInfo GetInfo(int aInterfaceId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_PCS_MATERIAL_REQUESTS_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@INTERFACE_ID", DbType.Int32, aInterfaceId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateMaterialRequestsInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>MaterialRequestsInfo Collection </returns>
		public List<MaterialRequestsInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TI_PCS_MATERIAL_REQUESTS_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>MaterialRequestsInfo Collection </returns>
		public List<MaterialRequestsInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<MaterialRequestsInfo> list = new List<MaterialRequestsInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateMaterialRequestsInfo(dr));
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
		public List<MaterialRequestsInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                textOrder += "[INTERFACE_ID] desc";
            string sql = "select top " + pageRow + " * from "
                + "(select row_number() over(order by " + textOrder + ") as rownumber"
                + ",* from [LES].[TI_PCS_MATERIAL_REQUESTS]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<MaterialRequestsInfo> list = new List<MaterialRequestsInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateMaterialRequestsInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TI_PCS_MATERIAL_REQUESTS_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public int Add(MaterialRequestsInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TI_PCS_MATERIAL_REQUESTS_INSERT);			
			db.AddInParameter(dbCommand, "@PLANT_ZONE", DbType.String, info.PlantZone);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@LOCATION", DbType.String, info.Location);
			db.AddInParameter(dbCommand, "@REQUEST_TIME", DbType.DateTime, info.RequestTime);
			db.AddInParameter(dbCommand, "@INTERFACE_STATUS", DbType.Int32, info.InterfaceStatus);
			db.AddInParameter(dbCommand, "@PROCESS_TIME", DbType.DateTime, info.ProcessTime);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@PART_CNAME", DbType.String, info.PartCname);
			db.AddInParameter(dbCommand, "@PART_ENAME", DbType.String, info.PartEname);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@DOCK", DbType.String, info.Dock);
			db.AddInParameter(dbCommand, "@BOX_PARTS", DbType.String, info.BoxParts);
			db.AddInParameter(dbCommand, "@INTERFACE_TYPE", DbType.Int32, info.InterfaceType);
			db.AddInParameter(dbCommand, "@PACK_COUNT", DbType.Int32, info.PackCount);
			db.AddInParameter(dbCommand, "@REQURIED_PACK", DbType.Int32, info.RequriedPack);
			db.AddInParameter(dbCommand, "@INHOUSE_PACKAGE_MODEL", DbType.String, info.InhousePackageModel);
			db.AddInParameter(dbCommand, "@INHOUSE_PACKAGE", DbType.Int32, info.InhousePackage);
			db.AddInParameter(dbCommand, "@MEASURING_UNIT_NO", DbType.AnsiString, info.MeasuringUnitNo);
			db.AddInParameter(dbCommand, "@EXPECTED_ARRIVAL_TIME", DbType.DateTime, info.ExpectedArrivalTime);
			db.AddInParameter(dbCommand, "@RDC_DLOC", DbType.AnsiString, info.RdcDloc);
			db.AddInParameter(dbCommand, "@PICKUP_SEQ_NO", DbType.Int32, info.PickupSeqNo);
			db.AddInParameter(dbCommand, "@SEQUENCE_NO", DbType.Int32, info.SequenceNo);
			db.AddInParameter(dbCommand, "@IS_ORGANIZE_SHEET", DbType.Int32, info.IsOrganizeSheet);
			db.AddInParameter(dbCommand, "@SEND_STATUS", DbType.Int32, info.SendStatus);
			db.AddInParameter(dbCommand, "@SEND_TIME", DbType.DateTime, info.SendTime);
			db.AddInParameter(dbCommand, "@IS_CANCEL", DbType.Boolean, info.IsCancel);
			db.AddInParameter(dbCommand, "@WMS_SEND_STATUS", DbType.Int32, info.WmsSendStatus);
			db.AddInParameter(dbCommand, "@WMS_SEND_TIME", DbType.DateTime, info.WmsSendTime);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@UPDATE_DATE", DbType.DateTime, info.UpdateDate);
			db.AddInParameter(dbCommand, "@UPDATE_USER", DbType.String, info.UpdateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			return int.Parse("0" + db.ExecuteScalar(dbCommand));		
		}		
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(MaterialRequestsInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_PCS_MATERIAL_REQUESTS_UPDATE);				
			db.AddInParameter(dbCommand, "@INTERFACE_ID", DbType.Int32, info.InterfaceId);
			db.AddInParameter(dbCommand, "@PLANT_ZONE", DbType.String, info.PlantZone);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@LOCATION", DbType.String, info.Location);
			db.AddInParameter(dbCommand, "@REQUEST_TIME", DbType.DateTime, info.RequestTime);
			db.AddInParameter(dbCommand, "@INTERFACE_STATUS", DbType.Int32, info.InterfaceStatus);
			db.AddInParameter(dbCommand, "@PROCESS_TIME", DbType.DateTime, info.ProcessTime);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@PART_CNAME", DbType.String, info.PartCname);
			db.AddInParameter(dbCommand, "@PART_ENAME", DbType.String, info.PartEname);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@DOCK", DbType.String, info.Dock);
			db.AddInParameter(dbCommand, "@BOX_PARTS", DbType.String, info.BoxParts);
			db.AddInParameter(dbCommand, "@INTERFACE_TYPE", DbType.Int32, info.InterfaceType);
			db.AddInParameter(dbCommand, "@PACK_COUNT", DbType.Int32, info.PackCount);
			db.AddInParameter(dbCommand, "@REQURIED_PACK", DbType.Int32, info.RequriedPack);
			db.AddInParameter(dbCommand, "@INHOUSE_PACKAGE_MODEL", DbType.String, info.InhousePackageModel);
			db.AddInParameter(dbCommand, "@INHOUSE_PACKAGE", DbType.Int32, info.InhousePackage);
			db.AddInParameter(dbCommand, "@MEASURING_UNIT_NO", DbType.AnsiString, info.MeasuringUnitNo);
			db.AddInParameter(dbCommand, "@EXPECTED_ARRIVAL_TIME", DbType.DateTime, info.ExpectedArrivalTime);
			db.AddInParameter(dbCommand, "@RDC_DLOC", DbType.AnsiString, info.RdcDloc);
			db.AddInParameter(dbCommand, "@PICKUP_SEQ_NO", DbType.Int32, info.PickupSeqNo);
			db.AddInParameter(dbCommand, "@SEQUENCE_NO", DbType.Int32, info.SequenceNo);
			db.AddInParameter(dbCommand, "@IS_ORGANIZE_SHEET", DbType.Int32, info.IsOrganizeSheet);
			db.AddInParameter(dbCommand, "@SEND_STATUS", DbType.Int32, info.SendStatus);
			db.AddInParameter(dbCommand, "@SEND_TIME", DbType.DateTime, info.SendTime);
			db.AddInParameter(dbCommand, "@IS_CANCEL", DbType.Boolean, info.IsCancel);
			db.AddInParameter(dbCommand, "@WMS_SEND_STATUS", DbType.Int32, info.WmsSendStatus);
			db.AddInParameter(dbCommand, "@WMS_SEND_TIME", DbType.DateTime, info.WmsSendTime);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@UPDATE_DATE", DbType.DateTime, info.UpdateDate);
			db.AddInParameter(dbCommand, "@UPDATE_USER", DbType.String, info.UpdateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="INTERFACE_ID">MaterialRequestsInfo Primary key </param>
		/// <returns></returns>
		public int Delete(int aInterfaceId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_PCS_MATERIAL_REQUESTS_DELETE);
		    db.AddInParameter(dbCommand, "@INTERFACE_ID", DbType.Int32, aInterfaceId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}
		/// <summary>
		/// UpdateInfo
		/// </summary>
		/// <param name="INTERFACE_ID">MaterialRequestsInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,int aInterfaceId)
		{
		    string sql = "update [LES].[TI_PCS_MATERIAL_REQUESTS] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE 1=1  AND INTERFACE_ID =@INTERFACE_ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@INTERFACE_ID", DbType.Int32, aInterfaceId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static MaterialRequestsInfo CreateMaterialRequestsInfo(IDataReader rdr)
		{
			MaterialRequestsInfo info = new MaterialRequestsInfo();
			info.InterfaceId = DBConvert.GetInt32(rdr, rdr.GetOrdinal("INTERFACE_ID"));			
			info.PlantZone = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT_ZONE"));			
			info.Workshop = DBConvert.GetString(rdr, rdr.GetOrdinal("WORKSHOP"));			
			info.AssemblyLine = DBConvert.GetString(rdr, rdr.GetOrdinal("ASSEMBLY_LINE"));			
			info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));			
			info.Location = DBConvert.GetString(rdr, rdr.GetOrdinal("LOCATION"));			
			info.RequestTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("REQUEST_TIME"));			
			info.InterfaceStatus = DBConvert.GetInt32(rdr, rdr.GetOrdinal("INTERFACE_STATUS"));			
			info.ProcessTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("PROCESS_TIME"));			
			info.PartNo = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_NO"));			
			info.PartCname = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_CNAME"));			
			info.PartEname = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_ENAME"));			
			info.SupplierNum = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NUM"));			
			info.Dock = DBConvert.GetString(rdr, rdr.GetOrdinal("DOCK"));			
			info.BoxParts = DBConvert.GetString(rdr, rdr.GetOrdinal("BOX_PARTS"));			
			info.InterfaceType = DBConvert.GetInt32(rdr, rdr.GetOrdinal("INTERFACE_TYPE"));			
			info.PackCount = DBConvert.GetInt32(rdr, rdr.GetOrdinal("PACK_COUNT"));			
			info.RequriedPack = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("REQURIED_PACK"));			
			info.InhousePackageModel = DBConvert.GetString(rdr, rdr.GetOrdinal("INHOUSE_PACKAGE_MODEL"));			
			info.InhousePackage = DBConvert.GetInt32(rdr, rdr.GetOrdinal("INHOUSE_PACKAGE"));			
			info.MeasuringUnitNo = DBConvert.GetString(rdr, rdr.GetOrdinal("MEASURING_UNIT_NO"));			
			info.ExpectedArrivalTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("EXPECTED_ARRIVAL_TIME"));			
			info.RdcDloc = DBConvert.GetString(rdr, rdr.GetOrdinal("RDC_DLOC"));			
			info.PickupSeqNo = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PICKUP_SEQ_NO"));			
			info.SequenceNo = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("SEQUENCE_NO"));			
			info.IsOrganizeSheet = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("IS_ORGANIZE_SHEET"));			
			info.SendStatus = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("SEND_STATUS"));			
			info.SendTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("SEND_TIME"));			
			info.IsCancel = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("IS_CANCEL"));			
			info.WmsSendStatus = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("WMS_SEND_STATUS"));			
			info.WmsSendTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("WMS_SEND_TIME"));			
			info.Comments = DBConvert.GetString(rdr, rdr.GetOrdinal("COMMENTS"));			
			info.UpdateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("UPDATE_DATE"));			
			info.UpdateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("UPDATE_USER"));			
			info.CreateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			return info;
		}
		
		#endregion
	}
}