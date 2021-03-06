#region Declaim
//---------------------------------------------------------------------------
// Name:		TranDetailsLogDAL
// Function: 	Expose data in table TM_WMM_TRAN_DETAILS_LOG from database as business object to MES system.
// Tool:		T4
// CreateDate:	2020年10月23日
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
    /// TranDetailsLogDAL对应表[TM_WMM_TRAN_DETAILS_LOG]
    /// </summary>
    public partial class TranDetailsLogDAL : BusinessObjectProvider<TranDetailsLogInfo>
	{
		#region Sql Statements
		private const string TM_WMM_TRAN_DETAILS_LOG_SELECT_BY_ID =
			@"SELECT TRAN_NO,
				PLANT,
				Plant_NAME,
				WM_NO,
				ZONE_NO,
				PART_NO,
				PART_CNAME,
				SUPPLIER_NUM,
				SUPPLIER_NAME,
				DLOC,
				TARGET_WM,
				TARGET_ZONE,
				TARGET_DLOC,
				TARGET_DEPARTMENT,
				PACKAGE,
				NUM,
				BOX_NUM,
				SUPPLIER_SNAME,
				PACKAGE_MODEL,
				TRAN_TYPE,
				CREATE_USER,
				CREATE_DATE,
				COMMENTS,
				IS_BATCH,
				BOX_PARTS,
				PROCESS_MESSAGE,
				ACTUAL_PACKAGE_QTY,
				STORAGE_LOCATION,
				BARCODE_DATA,
				BARCODE_TYPE,
				UPDATE_FLAG,
				MIN,
				LINE_POSITION,
				PART_CLS,
				TRAN_ID,
				MEASURING_UNIT_NO,
				DOCK,
				PICKUP_SEQ_NO,
				BATCH_NO,
				TRAN_STATE,
				INNER_LOCATION,
				REQUIRED_DATE,
				VALID_FLAG,
				INHOUSE_PACKAGE_MODEL,
				RUNSHEET_NO,
				UPDATE_USER,
				TRAN_DATE,
				PART_UNITS,
				PROCESS_RESULT,
				LOCATION,
				UPDATE_DATE,
				RDC_DLOC,
				REQUIRED_PACKAGE_QTY,
				PART_NICKNAME,
				MAX,
				PACHAGE_TYPE				  
				FROM [LES].[TM_WMM_TRAN_DETAILS_LOG] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND TRAN_ID =@TRAN_ID;";
			
		private const string TM_WMM_TRAN_DETAILS_LOG_SELECT = 
			@"SELECT TRAN_NO,
				PLANT,
				Plant_NAME,
				WM_NO,
				ZONE_NO,
				PART_NO,
				PART_CNAME,
				SUPPLIER_NUM,
				SUPPLIER_NAME,
				DLOC,
				TARGET_WM,
				TARGET_ZONE,
				TARGET_DLOC,
				TARGET_DEPARTMENT,
				PACKAGE,
				NUM,
				BOX_NUM,
				SUPPLIER_SNAME,
				PACKAGE_MODEL,
				TRAN_TYPE,
				CREATE_USER,
				CREATE_DATE,
				COMMENTS,
				IS_BATCH,
				BOX_PARTS,
				PROCESS_MESSAGE,
				ACTUAL_PACKAGE_QTY,
				STORAGE_LOCATION,
				BARCODE_DATA,
				BARCODE_TYPE,
				UPDATE_FLAG,
				MIN,
				LINE_POSITION,
				PART_CLS,
				TRAN_ID,
				MEASURING_UNIT_NO,
				DOCK,
				PICKUP_SEQ_NO,
				BATCH_NO,
				TRAN_STATE,
				INNER_LOCATION,
				REQUIRED_DATE,
				VALID_FLAG,
				INHOUSE_PACKAGE_MODEL,
				RUNSHEET_NO,
				UPDATE_USER,
				TRAN_DATE,
				PART_UNITS,
				PROCESS_RESULT,
				LOCATION,
				UPDATE_DATE,
				RDC_DLOC,
				REQUIRED_PACKAGE_QTY,
				PART_NICKNAME,
				MAX,
				PACHAGE_TYPE				 
				FROM [LES].[TM_WMM_TRAN_DETAILS_LOG] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TM_WMM_TRAN_DETAILS_LOG_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TM_WMM_TRAN_DETAILS_LOG]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TM_WMM_TRAN_DETAILS_LOG_INSERT =
			@"INSERT INTO [LES].[TM_WMM_TRAN_DETAILS_LOG] (
				TRAN_NO,
				PLANT,
				Plant_NAME,
				WM_NO,
				ZONE_NO,
				PART_NO,
				PART_CNAME,
				SUPPLIER_NUM,
				SUPPLIER_NAME,
				DLOC,
				TARGET_WM,
				TARGET_ZONE,
				TARGET_DLOC,
				TARGET_DEPARTMENT,
				PACKAGE,
				NUM,
				BOX_NUM,
				SUPPLIER_SNAME,
				PACKAGE_MODEL,
				TRAN_TYPE,
				CREATE_USER,
				CREATE_DATE,
				COMMENTS,
				IS_BATCH,
				BOX_PARTS,
				PROCESS_MESSAGE,
				ACTUAL_PACKAGE_QTY,
				STORAGE_LOCATION,
				BARCODE_DATA,
				BARCODE_TYPE,
				UPDATE_FLAG,
				MIN,
				LINE_POSITION,
				PART_CLS,
				MEASURING_UNIT_NO,
				DOCK,
				PICKUP_SEQ_NO,
				BATCH_NO,
				TRAN_STATE,
				INNER_LOCATION,
				REQUIRED_DATE,
				VALID_FLAG,
				INHOUSE_PACKAGE_MODEL,
				RUNSHEET_NO,
				UPDATE_USER,
				TRAN_DATE,
				PART_UNITS,
				PROCESS_RESULT,
				LOCATION,
				UPDATE_DATE,
				RDC_DLOC,
				REQUIRED_PACKAGE_QTY,
				PART_NICKNAME,
				MAX,
				PACHAGE_TYPE				 
			) VALUES (
				@TRAN_NO,
				@PLANT,
				@Plant_NAME,
				@WM_NO,
				@ZONE_NO,
				@PART_NO,
				@PART_CNAME,
				@SUPPLIER_NUM,
				@SUPPLIER_NAME,
				@DLOC,
				@TARGET_WM,
				@TARGET_ZONE,
				@TARGET_DLOC,
				@TARGET_DEPARTMENT,
				@PACKAGE,
				@NUM,
				@BOX_NUM,
				@SUPPLIER_SNAME,
				@PACKAGE_MODEL,
				@TRAN_TYPE,
				@CREATE_USER,
				GETDATE(),
				@COMMENTS,
				@IS_BATCH,
				@BOX_PARTS,
				@PROCESS_MESSAGE,
				@ACTUAL_PACKAGE_QTY,
				@STORAGE_LOCATION,
				@BARCODE_DATA,
				@BARCODE_TYPE,
				@UPDATE_FLAG,
				@MIN,
				@LINE_POSITION,
				@PART_CLS,
				@MEASURING_UNIT_NO,
				@DOCK,
				@PICKUP_SEQ_NO,
				@BATCH_NO,
				@TRAN_STATE,
				@INNER_LOCATION,
				@REQUIRED_DATE,
				@VALID_FLAG,
				@INHOUSE_PACKAGE_MODEL,
				@RUNSHEET_NO,
				@UPDATE_USER,
				@TRAN_DATE,
				@PART_UNITS,
				@PROCESS_RESULT,
				@LOCATION,
				@UPDATE_DATE,
				@RDC_DLOC,
				@REQUIRED_PACKAGE_QTY,
				@PART_NICKNAME,
				@MAX,
				@PACHAGE_TYPE				 
			);SELECT @@IDENTITY;";
		private const string TM_WMM_TRAN_DETAILS_LOG_UPDATE =
			@"UPDATE [LES].[TM_WMM_TRAN_DETAILS_LOG] WITH(ROWLOCK) 
				SET TRAN_NO=@TRAN_NO,
				PLANT=@PLANT,
				Plant_NAME=@Plant_NAME,
				WM_NO=@WM_NO,
				ZONE_NO=@ZONE_NO,
				PART_NO=@PART_NO,
				PART_CNAME=@PART_CNAME,
				SUPPLIER_NUM=@SUPPLIER_NUM,
				SUPPLIER_NAME=@SUPPLIER_NAME,
				DLOC=@DLOC,
				TARGET_WM=@TARGET_WM,
				TARGET_ZONE=@TARGET_ZONE,
				TARGET_DLOC=@TARGET_DLOC,
				TARGET_DEPARTMENT=@TARGET_DEPARTMENT,
				PACKAGE=@PACKAGE,
				NUM=@NUM,
				BOX_NUM=@BOX_NUM,
				SUPPLIER_SNAME=@SUPPLIER_SNAME,
				PACKAGE_MODEL=@PACKAGE_MODEL,
				TRAN_TYPE=@TRAN_TYPE,
				CREATE_USER=@CREATE_USER,
				CREATE_DATE=@CREATE_DATE,
				COMMENTS=@COMMENTS,
				IS_BATCH=@IS_BATCH,
				BOX_PARTS=@BOX_PARTS,
				PROCESS_MESSAGE=@PROCESS_MESSAGE,
				ACTUAL_PACKAGE_QTY=@ACTUAL_PACKAGE_QTY,
				STORAGE_LOCATION=@STORAGE_LOCATION,
				BARCODE_DATA=@BARCODE_DATA,
				BARCODE_TYPE=@BARCODE_TYPE,
				UPDATE_FLAG=@UPDATE_FLAG,
				MIN=@MIN,
				LINE_POSITION=@LINE_POSITION,
				PART_CLS=@PART_CLS,
				MEASURING_UNIT_NO=@MEASURING_UNIT_NO,
				DOCK=@DOCK,
				PICKUP_SEQ_NO=@PICKUP_SEQ_NO,
				BATCH_NO=@BATCH_NO,
				TRAN_STATE=@TRAN_STATE,
				INNER_LOCATION=@INNER_LOCATION,
				REQUIRED_DATE=@REQUIRED_DATE,
				VALID_FLAG=@VALID_FLAG,
				INHOUSE_PACKAGE_MODEL=@INHOUSE_PACKAGE_MODEL,
				RUNSHEET_NO=@RUNSHEET_NO,
				UPDATE_USER=@UPDATE_USER,
				TRAN_DATE=@TRAN_DATE,
				PART_UNITS=@PART_UNITS,
				PROCESS_RESULT=@PROCESS_RESULT,
				LOCATION=@LOCATION,
				UPDATE_DATE=@UPDATE_DATE,
				RDC_DLOC=@RDC_DLOC,
				REQUIRED_PACKAGE_QTY=@REQUIRED_PACKAGE_QTY,
				PART_NICKNAME=@PART_NICKNAME,
				MAX=@MAX,
				PACHAGE_TYPE=@PACHAGE_TYPE				 
				WHERE [VALID_FLAG] = 1  AND TRAN_ID =@TRAN_ID;";

		private const string TM_WMM_TRAN_DETAILS_LOG_DELETE =
			@"DELETE FROM [LES].[TM_WMM_TRAN_DETAILS_LOG] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND TRAN_ID =@TRAN_ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get TranDetailsLogInfo
		/// </summary>
		/// <param name="TRAN_ID">TranDetailsLogInfo Primary key </param>
		/// <returns></returns> 
		public TranDetailsLogInfo GetInfo(int aTranId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_WMM_TRAN_DETAILS_LOG_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@TRAN_ID", DbType.Int32, aTranId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateTranDetailsLogInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>TranDetailsLogInfo Collection </returns>
		public List<TranDetailsLogInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TM_WMM_TRAN_DETAILS_LOG_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>TranDetailsLogInfo Collection </returns>
		public List<TranDetailsLogInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<TranDetailsLogInfo> list = new List<TranDetailsLogInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateTranDetailsLogInfo(dr));
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
		public List<TranDetailsLogInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                textOrder += "[TRAN_ID] desc";
            string sql = "select top " + pageRow + " * from "
                + "(select row_number() over(order by " + textOrder + ") as rownumber"
                + ",* from [LES].[TM_WMM_TRAN_DETAILS_LOG]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<TranDetailsLogInfo> list = new List<TranDetailsLogInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateTranDetailsLogInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TM_WMM_TRAN_DETAILS_LOG_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public int Add(TranDetailsLogInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TM_WMM_TRAN_DETAILS_LOG_INSERT);			
			db.AddInParameter(dbCommand, "@TRAN_NO", DbType.String, info.TranNo);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@Plant_NAME", DbType.String, info.PlantName);
			db.AddInParameter(dbCommand, "@WM_NO", DbType.String, info.WmNo);
			db.AddInParameter(dbCommand, "@ZONE_NO", DbType.String, info.ZoneNo);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@PART_CNAME", DbType.String, info.PartCname);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@SUPPLIER_NAME", DbType.String, info.SupplierName);
			db.AddInParameter(dbCommand, "@DLOC", DbType.String, info.Dloc);
			db.AddInParameter(dbCommand, "@TARGET_WM", DbType.String, info.TargetWm);
			db.AddInParameter(dbCommand, "@TARGET_ZONE", DbType.String, info.TargetZone);
			db.AddInParameter(dbCommand, "@TARGET_DLOC", DbType.String, info.TargetDloc);
			db.AddInParameter(dbCommand, "@TARGET_DEPARTMENT", DbType.String, info.TargetDepartment);
			db.AddInParameter(dbCommand, "@PACKAGE", DbType.Int32, info.Package);
			db.AddInParameter(dbCommand, "@NUM", DbType.Decimal, info.Num);
			db.AddInParameter(dbCommand, "@BOX_NUM", DbType.Decimal, info.BoxNum);
			db.AddInParameter(dbCommand, "@SUPPLIER_SNAME", DbType.String, info.SupplierSname);
			db.AddInParameter(dbCommand, "@PACKAGE_MODEL", DbType.String, info.PackageModel);
			db.AddInParameter(dbCommand, "@TRAN_TYPE", DbType.Int32, info.TranType);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@IS_BATCH", DbType.Int32, info.IsBatch);
			db.AddInParameter(dbCommand, "@BOX_PARTS", DbType.String, info.BoxParts);
			db.AddInParameter(dbCommand, "@PROCESS_MESSAGE", DbType.String, info.ProcessMessage);
			db.AddInParameter(dbCommand, "@ACTUAL_PACKAGE_QTY", DbType.Int32, info.ActualPackageQty);
			db.AddInParameter(dbCommand, "@STORAGE_LOCATION", DbType.String, info.StorageLocation);
			db.AddInParameter(dbCommand, "@BARCODE_DATA", DbType.String, info.BarcodeData);
			db.AddInParameter(dbCommand, "@BARCODE_TYPE", DbType.String, info.BarcodeType);
			db.AddInParameter(dbCommand, "@UPDATE_FLAG", DbType.Int32, info.UpdateFlag);
			db.AddInParameter(dbCommand, "@MIN", DbType.Decimal, info.Min);
			db.AddInParameter(dbCommand, "@LINE_POSITION", DbType.String, info.LinePosition);
			db.AddInParameter(dbCommand, "@PART_CLS", DbType.String, info.PartCls);
			db.AddInParameter(dbCommand, "@MEASURING_UNIT_NO", DbType.String, info.MeasuringUnitNo);
			db.AddInParameter(dbCommand, "@DOCK", DbType.String, info.Dock);
			db.AddInParameter(dbCommand, "@PICKUP_SEQ_NO", DbType.Int32, info.PickupSeqNo);
			db.AddInParameter(dbCommand, "@BATCH_NO", DbType.String, info.BatchNo);
			db.AddInParameter(dbCommand, "@TRAN_STATE", DbType.Int32, info.TranState);
			db.AddInParameter(dbCommand, "@INNER_LOCATION", DbType.String, info.InnerLocation);
			db.AddInParameter(dbCommand, "@REQUIRED_DATE", DbType.DateTime, info.RequiredDate);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@INHOUSE_PACKAGE_MODEL", DbType.String, info.InhousePackageModel);
			db.AddInParameter(dbCommand, "@RUNSHEET_NO", DbType.String, info.RunsheetNo);
			db.AddInParameter(dbCommand, "@UPDATE_USER", DbType.String, info.UpdateUser);
			db.AddInParameter(dbCommand, "@TRAN_DATE", DbType.DateTime, info.TranDate);
			db.AddInParameter(dbCommand, "@PART_UNITS", DbType.String, info.PartUnits);
			db.AddInParameter(dbCommand, "@PROCESS_RESULT", DbType.Int32, info.ProcessResult);
			db.AddInParameter(dbCommand, "@LOCATION", DbType.String, info.Location);
			db.AddInParameter(dbCommand, "@UPDATE_DATE", DbType.DateTime, info.UpdateDate);
			db.AddInParameter(dbCommand, "@RDC_DLOC", DbType.String, info.RdcDloc);
			db.AddInParameter(dbCommand, "@REQUIRED_PACKAGE_QTY", DbType.Int32, info.RequiredPackageQty);
			db.AddInParameter(dbCommand, "@PART_NICKNAME", DbType.String, info.PartNickname);
			db.AddInParameter(dbCommand, "@MAX", DbType.Decimal, info.Max);
			db.AddInParameter(dbCommand, "@PACHAGE_TYPE", DbType.String, info.PachageType);
			return int.Parse("0" + db.ExecuteScalar(dbCommand));		
		}
		/// <summary>
		/// GetInsertSql
		/// </summary>
		/// <param name="info"> info</param>
		public static string GetInsertSql(TranDetailsLogInfo info)
		{
			return  
			@"insert into [LES].[TM_WMM_TRAN_DETAILS_LOG] (
				TRAN_NO,
				PLANT,
				Plant_NAME,
				WM_NO,
				ZONE_NO,
				PART_NO,
				PART_CNAME,
				SUPPLIER_NUM,
				SUPPLIER_NAME,
				DLOC,
				TARGET_WM,
				TARGET_ZONE,
				TARGET_DLOC,
				TARGET_DEPARTMENT,
				PACKAGE,
				NUM,
				BOX_NUM,
				SUPPLIER_SNAME,
				PACKAGE_MODEL,
				TRAN_TYPE,
				CREATE_USER,
				CREATE_DATE,
				COMMENTS,
				IS_BATCH,
				BOX_PARTS,
				PROCESS_MESSAGE,
				ACTUAL_PACKAGE_QTY,
				STORAGE_LOCATION,
				BARCODE_DATA,
				BARCODE_TYPE,
				UPDATE_FLAG,
				MIN,
				LINE_POSITION,
				PART_CLS,
				MEASURING_UNIT_NO,
				DOCK,
				PICKUP_SEQ_NO,
				BATCH_NO,
				TRAN_STATE,
				INNER_LOCATION,
				REQUIRED_DATE,
				VALID_FLAG,
				INHOUSE_PACKAGE_MODEL,
				RUNSHEET_NO,
				UPDATE_USER,
				TRAN_DATE,
				PART_UNITS,
				PROCESS_RESULT,
				LOCATION,
				UPDATE_DATE,
				RDC_DLOC,
				REQUIRED_PACKAGE_QTY,
				PART_NICKNAME,
				MAX,
				PACHAGE_TYPE				 
			) values ("+
				(string.IsNullOrEmpty(info.TranNo) ? "NULL" : "N'" + info.TranNo + "'") + ","+
				(string.IsNullOrEmpty(info.Plant) ? "NULL" : "N'" + info.Plant + "'") + ","+
				(string.IsNullOrEmpty(info.PlantName) ? "NULL" : "N'" + info.PlantName + "'") + ","+
				(string.IsNullOrEmpty(info.WmNo) ? "NULL" : "N'" + info.WmNo + "'") + ","+
				(string.IsNullOrEmpty(info.ZoneNo) ? "NULL" : "N'" + info.ZoneNo + "'") + ","+
				(string.IsNullOrEmpty(info.PartNo) ? "NULL" : "N'" + info.PartNo + "'") + ","+
				(string.IsNullOrEmpty(info.PartCname) ? "NULL" : "N'" + info.PartCname + "'") + ","+
				(string.IsNullOrEmpty(info.SupplierNum) ? "NULL" : "N'" + info.SupplierNum + "'") + ","+
				(string.IsNullOrEmpty(info.SupplierName) ? "NULL" : "N'" + info.SupplierName + "'") + ","+
				(string.IsNullOrEmpty(info.Dloc) ? "NULL" : "N'" + info.Dloc + "'") + ","+
				(string.IsNullOrEmpty(info.TargetWm) ? "NULL" : "N'" + info.TargetWm + "'") + ","+
				(string.IsNullOrEmpty(info.TargetZone) ? "NULL" : "N'" + info.TargetZone + "'") + ","+
				(string.IsNullOrEmpty(info.TargetDloc) ? "NULL" : "N'" + info.TargetDloc + "'") + ","+
				(string.IsNullOrEmpty(info.TargetDepartment) ? "NULL" : "N'" + info.TargetDepartment + "'") + ","+
				(info.Package == null ? "NULL" : "" + info.Package.GetValueOrDefault() + "") + ","+
				(info.Num == null ? "NULL" : "" + info.Num.GetValueOrDefault() + "") + ","+
				(info.BoxNum == null ? "NULL" : "" + info.BoxNum.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.SupplierSname) ? "NULL" : "N'" + info.SupplierSname + "'") + ","+
				(string.IsNullOrEmpty(info.PackageModel) ? "NULL" : "N'" + info.PackageModel + "'") + ","+
				(info.TranType == null ? "NULL" : "" + info.TranType + "") + ","+
				"N'" + info.CreateUser + "'" + ","+		
				"GETDATE()" + ","+			
				(string.IsNullOrEmpty(info.Comments) ? "NULL" : "N'" + info.Comments + "'") + ","+
				(info.IsBatch == null ? "NULL" : "" + info.IsBatch.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.BoxParts) ? "NULL" : "N'" + info.BoxParts + "'") + ","+
				(string.IsNullOrEmpty(info.ProcessMessage) ? "NULL" : "N'" + info.ProcessMessage + "'") + ","+
				(info.ActualPackageQty == null ? "NULL" : "" + info.ActualPackageQty.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.StorageLocation) ? "NULL" : "N'" + info.StorageLocation + "'") + ","+
				(string.IsNullOrEmpty(info.BarcodeData) ? "NULL" : "N'" + info.BarcodeData + "'") + ","+
				(string.IsNullOrEmpty(info.BarcodeType) ? "NULL" : "N'" + info.BarcodeType + "'") + ","+
				(info.UpdateFlag == null ? "NULL" : "" + info.UpdateFlag.GetValueOrDefault() + "") + ","+
				(info.Min == null ? "NULL" : "" + info.Min.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.LinePosition) ? "NULL" : "N'" + info.LinePosition + "'") + ","+
				(string.IsNullOrEmpty(info.PartCls) ? "NULL" : "N'" + info.PartCls + "'") + ","+
				(string.IsNullOrEmpty(info.MeasuringUnitNo) ? "NULL" : "N'" + info.MeasuringUnitNo + "'") + ","+
				(string.IsNullOrEmpty(info.Dock) ? "NULL" : "N'" + info.Dock + "'") + ","+
				(info.PickupSeqNo == null ? "NULL" : "" + info.PickupSeqNo.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.BatchNo) ? "NULL" : "N'" + info.BatchNo + "'") + ","+
				(info.TranState == null ? "NULL" : "" + info.TranState.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.InnerLocation) ? "NULL" : "N'" + info.InnerLocation + "'") + ","+
				(info.RequiredDate == null ? "NULL" : "N'" + info.RequiredDate.GetValueOrDefault() + "'") + ","+
				"1" + ","+		
				(string.IsNullOrEmpty(info.InhousePackageModel) ? "NULL" : "N'" + info.InhousePackageModel + "'") + ","+
				(string.IsNullOrEmpty(info.RunsheetNo) ? "NULL" : "N'" + info.RunsheetNo + "'") + ","+
				(string.IsNullOrEmpty(info.UpdateUser) ? "NULL" : "N'" + info.UpdateUser + "'") + ","+
				(info.TranDate == null ? "NULL" : "N'" + info.TranDate.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.PartUnits) ? "NULL" : "N'" + info.PartUnits + "'") + ","+
				(info.ProcessResult == null ? "NULL" : "" + info.ProcessResult.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.Location) ? "NULL" : "N'" + info.Location + "'") + ","+
				(info.UpdateDate == null ? "NULL" : "N'" + info.UpdateDate.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.RdcDloc) ? "NULL" : "N'" + info.RdcDloc + "'") + ","+
				(info.RequiredPackageQty == null ? "NULL" : "" + info.RequiredPackageQty.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.PartNickname) ? "NULL" : "N'" + info.PartNickname + "'") + ","+
				(info.Max == null ? "NULL" : "" + info.Max.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.PachageType) ? "NULL" : "N'" + info.PachageType + "'") + ");";				}
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(TranDetailsLogInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_WMM_TRAN_DETAILS_LOG_UPDATE);				
			db.AddInParameter(dbCommand, "@TRAN_NO", DbType.String, info.TranNo);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@Plant_NAME", DbType.String, info.PlantName);
			db.AddInParameter(dbCommand, "@WM_NO", DbType.String, info.WmNo);
			db.AddInParameter(dbCommand, "@ZONE_NO", DbType.String, info.ZoneNo);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@PART_CNAME", DbType.String, info.PartCname);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@SUPPLIER_NAME", DbType.String, info.SupplierName);
			db.AddInParameter(dbCommand, "@DLOC", DbType.String, info.Dloc);
			db.AddInParameter(dbCommand, "@TARGET_WM", DbType.String, info.TargetWm);
			db.AddInParameter(dbCommand, "@TARGET_ZONE", DbType.String, info.TargetZone);
			db.AddInParameter(dbCommand, "@TARGET_DLOC", DbType.String, info.TargetDloc);
			db.AddInParameter(dbCommand, "@TARGET_DEPARTMENT", DbType.String, info.TargetDepartment);
			db.AddInParameter(dbCommand, "@PACKAGE", DbType.Int32, info.Package);
			db.AddInParameter(dbCommand, "@NUM", DbType.Decimal, info.Num);
			db.AddInParameter(dbCommand, "@BOX_NUM", DbType.Decimal, info.BoxNum);
			db.AddInParameter(dbCommand, "@SUPPLIER_SNAME", DbType.String, info.SupplierSname);
			db.AddInParameter(dbCommand, "@PACKAGE_MODEL", DbType.String, info.PackageModel);
			db.AddInParameter(dbCommand, "@TRAN_TYPE", DbType.Int32, info.TranType);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@IS_BATCH", DbType.Int32, info.IsBatch);
			db.AddInParameter(dbCommand, "@BOX_PARTS", DbType.String, info.BoxParts);
			db.AddInParameter(dbCommand, "@PROCESS_MESSAGE", DbType.String, info.ProcessMessage);
			db.AddInParameter(dbCommand, "@ACTUAL_PACKAGE_QTY", DbType.Int32, info.ActualPackageQty);
			db.AddInParameter(dbCommand, "@STORAGE_LOCATION", DbType.String, info.StorageLocation);
			db.AddInParameter(dbCommand, "@BARCODE_DATA", DbType.String, info.BarcodeData);
			db.AddInParameter(dbCommand, "@BARCODE_TYPE", DbType.String, info.BarcodeType);
			db.AddInParameter(dbCommand, "@UPDATE_FLAG", DbType.Int32, info.UpdateFlag);
			db.AddInParameter(dbCommand, "@MIN", DbType.Decimal, info.Min);
			db.AddInParameter(dbCommand, "@LINE_POSITION", DbType.String, info.LinePosition);
			db.AddInParameter(dbCommand, "@PART_CLS", DbType.String, info.PartCls);
			db.AddInParameter(dbCommand, "@TRAN_ID", DbType.Int32, info.TranId);
			db.AddInParameter(dbCommand, "@MEASURING_UNIT_NO", DbType.String, info.MeasuringUnitNo);
			db.AddInParameter(dbCommand, "@DOCK", DbType.String, info.Dock);
			db.AddInParameter(dbCommand, "@PICKUP_SEQ_NO", DbType.Int32, info.PickupSeqNo);
			db.AddInParameter(dbCommand, "@BATCH_NO", DbType.String, info.BatchNo);
			db.AddInParameter(dbCommand, "@TRAN_STATE", DbType.Int32, info.TranState);
			db.AddInParameter(dbCommand, "@INNER_LOCATION", DbType.String, info.InnerLocation);
			db.AddInParameter(dbCommand, "@REQUIRED_DATE", DbType.DateTime, info.RequiredDate);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@INHOUSE_PACKAGE_MODEL", DbType.String, info.InhousePackageModel);
			db.AddInParameter(dbCommand, "@RUNSHEET_NO", DbType.String, info.RunsheetNo);
			db.AddInParameter(dbCommand, "@UPDATE_USER", DbType.String, info.UpdateUser);
			db.AddInParameter(dbCommand, "@TRAN_DATE", DbType.DateTime, info.TranDate);
			db.AddInParameter(dbCommand, "@PART_UNITS", DbType.String, info.PartUnits);
			db.AddInParameter(dbCommand, "@PROCESS_RESULT", DbType.Int32, info.ProcessResult);
			db.AddInParameter(dbCommand, "@LOCATION", DbType.String, info.Location);
			db.AddInParameter(dbCommand, "@UPDATE_DATE", DbType.DateTime, info.UpdateDate);
			db.AddInParameter(dbCommand, "@RDC_DLOC", DbType.String, info.RdcDloc);
			db.AddInParameter(dbCommand, "@REQUIRED_PACKAGE_QTY", DbType.Int32, info.RequiredPackageQty);
			db.AddInParameter(dbCommand, "@PART_NICKNAME", DbType.String, info.PartNickname);
			db.AddInParameter(dbCommand, "@MAX", DbType.Decimal, info.Max);
			db.AddInParameter(dbCommand, "@PACHAGE_TYPE", DbType.String, info.PachageType);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="TRAN_ID">TranDetailsLogInfo Primary key </param>
		/// <returns></returns>
		public int Delete(int aTranId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_WMM_TRAN_DETAILS_LOG_DELETE);
		    db.AddInParameter(dbCommand, "@TRAN_ID", DbType.Int32, aTranId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="TRAN_ID">TranDetailsLogInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(int aTranId, string loginUser)
		{
		    string sql = "update [LES].[TM_WMM_TRAN_DETAILS_LOG] WITH(ROWLOCK) "
                + "set [VALID_FLAG] = @VALID_FLAG ,[MODIFY_USER] = @MODIFY_USER ,[MODIFY_DATE] = GETDATE() "
                + "WHERE [VALID_FLAG] = 1  AND TRAN_ID =@TRAN_ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
                                   			db.AddInParameter(dbCommand, "@TRAN_ID", DbType.Int32, aTranId);
                     db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, false);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.AnsiString, loginUser);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}
		/// <summary>
		/// UpdateInfo
		/// </summary>
		/// <param name="TRAN_ID">TranDetailsLogInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,int aTranId)
		{
		    string sql = "update [LES].[TM_WMM_TRAN_DETAILS_LOG] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND TRAN_ID =@TRAN_ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@TRAN_ID", DbType.Int32, aTranId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static TranDetailsLogInfo CreateTranDetailsLogInfo(IDataReader rdr)
		{
			TranDetailsLogInfo info = new TranDetailsLogInfo();
			info.TranNo = DBConvert.GetString(rdr, rdr.GetOrdinal("TRAN_NO"));			
			info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));			
			info.PlantName = DBConvert.GetString(rdr, rdr.GetOrdinal("Plant_NAME"));			
			info.WmNo = DBConvert.GetString(rdr, rdr.GetOrdinal("WM_NO"));			
			info.ZoneNo = DBConvert.GetString(rdr, rdr.GetOrdinal("ZONE_NO"));			
			info.PartNo = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_NO"));			
			info.PartCname = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_CNAME"));			
			info.SupplierNum = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NUM"));			
			info.SupplierName = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NAME"));			
			info.Dloc = DBConvert.GetString(rdr, rdr.GetOrdinal("DLOC"));			
			info.TargetWm = DBConvert.GetString(rdr, rdr.GetOrdinal("TARGET_WM"));			
			info.TargetZone = DBConvert.GetString(rdr, rdr.GetOrdinal("TARGET_ZONE"));			
			info.TargetDloc = DBConvert.GetString(rdr, rdr.GetOrdinal("TARGET_DLOC"));			
			info.TargetDepartment = DBConvert.GetString(rdr, rdr.GetOrdinal("TARGET_DEPARTMENT"));			
			info.Package = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PACKAGE"));			
			info.Num = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("NUM"));			
			info.BoxNum = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("BOX_NUM"));			
			info.SupplierSname = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_SNAME"));			
			info.PackageModel = DBConvert.GetString(rdr, rdr.GetOrdinal("PACKAGE_MODEL"));			
			info.TranType = DBConvert.GetInt32(rdr, rdr.GetOrdinal("TRAN_TYPE"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.CreateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.Comments = DBConvert.GetString(rdr, rdr.GetOrdinal("COMMENTS"));			
			info.IsBatch = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("IS_BATCH"));			
			info.BoxParts = DBConvert.GetString(rdr, rdr.GetOrdinal("BOX_PARTS"));			
			info.ProcessMessage = DBConvert.GetString(rdr, rdr.GetOrdinal("PROCESS_MESSAGE"));			
			info.ActualPackageQty = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("ACTUAL_PACKAGE_QTY"));			
			info.StorageLocation = DBConvert.GetString(rdr, rdr.GetOrdinal("STORAGE_LOCATION"));			
			info.BarcodeData = DBConvert.GetString(rdr, rdr.GetOrdinal("BARCODE_DATA"));			
			info.BarcodeType = DBConvert.GetString(rdr, rdr.GetOrdinal("BARCODE_TYPE"));			
			info.UpdateFlag = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("UPDATE_FLAG"));			
			info.Min = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("MIN"));			
			info.LinePosition = DBConvert.GetString(rdr, rdr.GetOrdinal("LINE_POSITION"));			
			info.PartCls = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_CLS"));			
			info.TranId = DBConvert.GetInt32(rdr, rdr.GetOrdinal("TRAN_ID"));			
			info.MeasuringUnitNo = DBConvert.GetString(rdr, rdr.GetOrdinal("MEASURING_UNIT_NO"));			
			info.Dock = DBConvert.GetString(rdr, rdr.GetOrdinal("DOCK"));			
			info.PickupSeqNo = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PICKUP_SEQ_NO"));			
			info.BatchNo = DBConvert.GetString(rdr, rdr.GetOrdinal("BATCH_NO"));			
			info.TranState = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("TRAN_STATE"));			
			info.InnerLocation = DBConvert.GetString(rdr, rdr.GetOrdinal("INNER_LOCATION"));			
			info.RequiredDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("REQUIRED_DATE"));			
			info.ValidFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			info.InhousePackageModel = DBConvert.GetString(rdr, rdr.GetOrdinal("INHOUSE_PACKAGE_MODEL"));			
			info.RunsheetNo = DBConvert.GetString(rdr, rdr.GetOrdinal("RUNSHEET_NO"));			
			info.UpdateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("UPDATE_USER"));			
			info.TranDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("TRAN_DATE"));			
			info.PartUnits = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_UNITS"));			
			info.ProcessResult = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PROCESS_RESULT"));			
			info.Location = DBConvert.GetString(rdr, rdr.GetOrdinal("LOCATION"));			
			info.UpdateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("UPDATE_DATE"));			
			info.RdcDloc = DBConvert.GetString(rdr, rdr.GetOrdinal("RDC_DLOC"));			
			info.RequiredPackageQty = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("REQUIRED_PACKAGE_QTY"));			
			info.PartNickname = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_NICKNAME"));			
			info.Max = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("MAX"));			
			info.PachageType = DBConvert.GetString(rdr, rdr.GetOrdinal("PACHAGE_TYPE"));			
			return info;
		}
		
		#endregion
	}
}
