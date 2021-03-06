#region Declaim
//---------------------------------------------------------------------------
// Name:		ReceiveDetailDAL
// Function: 	Expose data in table TT_WMM_RECEIVE_DETAIL from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月9日
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
    /// ReceiveDetailDAL对应表[TT_WMM_RECEIVE_DETAIL]
    /// </summary>
    public partial class ReceiveDetailDAL : BusinessObjectProvider<ReceiveDetailInfo>
	{
		#region Sql Statements
		private const string TT_WMM_RECEIVE_DETAIL_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				RECEIVE_FID,
				PLANT,
				SUPPLIER_NUM,
				WM_NO,
				ZONE_NO,
				DLOC,
				TARGET_WM,
				TARGET_ZONE,
				TARGET_DLOC,
				PART_NO,
				PART_CNAME,
				PART_ENAME,
				MEASURING_UNIT_NO,
				IDENTIFY_PART_NO,
				PACKAGE_MODEL,
				PACKAGE,
				PART_TYPE,
				REQUIRED_BOX_NUM,
				REQUIRED_QTY,
				ACTUAL_BOX_NUM,
				ACTUAL_QTY,
				BARCODE_DATA,
				TRAN_NO,
				DOCK,
				ASSEMBLY_LINE,
				BOX_PARTS,
				SEQUENCE_NO,
				PICKUP_SEQ_NO,
				RDC_DLOC,
				INHOUSE_PACKAGE,
				INHOUSE_PACKAGE_MODEL,
				RUNSHEET_NO,
				SUPPLIER_NUM_SHEET,
				BOX_PARTS_SHEET,
				RETURN_REPORT_FLAG,
				ORDER_NO,
				ITEM_NO,
				CURRENT_BOX_NUM,
				CURRENT_QTY,
				FINAL_WM,
				FINAL_ZONE,
				FINAL_DLOC,
				IS_SCAN_BOX,
				INSPECTION_MODE,
				ROW_NO,
				ORIGIN_PLACE,
				PURCHASE_UNIT_PRICE,
				PART_PRICE,
				PART_CLS,
				PACKAGE_LENGTH,
				PACKAGE_WIDTH,
				PACKAGE_HEIGHT,
				PERPACKAGE_GROSS_WEIGHT,
				COMMENTS,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				PACKAGE_VOLUME,
				SUM_WEIGHT,
				SUM_VOLUME,
				QUALIFIED_QTY,
				INSPECTION_STATUS,
				FROZEN_STOCK_FLAG				  
				FROM [LES].[TT_WMM_RECEIVE_DETAIL] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TT_WMM_RECEIVE_DETAIL_SELECT = 
			@"SELECT ID,
				FID,
				RECEIVE_FID,
				PLANT,
				SUPPLIER_NUM,
				WM_NO,
				ZONE_NO,
				DLOC,
				TARGET_WM,
				TARGET_ZONE,
				TARGET_DLOC,
				PART_NO,
				PART_CNAME,
				PART_ENAME,
				MEASURING_UNIT_NO,
				IDENTIFY_PART_NO,
				PACKAGE_MODEL,
				PACKAGE,
				PART_TYPE,
				REQUIRED_BOX_NUM,
				REQUIRED_QTY,
				ACTUAL_BOX_NUM,
				ACTUAL_QTY,
				BARCODE_DATA,
				TRAN_NO,
				DOCK,
				ASSEMBLY_LINE,
				BOX_PARTS,
				SEQUENCE_NO,
				PICKUP_SEQ_NO,
				RDC_DLOC,
				INHOUSE_PACKAGE,
				INHOUSE_PACKAGE_MODEL,
				RUNSHEET_NO,
				SUPPLIER_NUM_SHEET,
				BOX_PARTS_SHEET,
				RETURN_REPORT_FLAG,
				ORDER_NO,
				ITEM_NO,
				CURRENT_BOX_NUM,
				CURRENT_QTY,
				FINAL_WM,
				FINAL_ZONE,
				FINAL_DLOC,
				IS_SCAN_BOX,
				INSPECTION_MODE,
				ROW_NO,
				ORIGIN_PLACE,
				PURCHASE_UNIT_PRICE,
				PART_PRICE,
				PART_CLS,
				PACKAGE_LENGTH,
				PACKAGE_WIDTH,
				PACKAGE_HEIGHT,
				PERPACKAGE_GROSS_WEIGHT,
				COMMENTS,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				PACKAGE_VOLUME,
				SUM_WEIGHT,
				SUM_VOLUME,
				QUALIFIED_QTY,
				INSPECTION_STATUS,
				FROZEN_STOCK_FLAG				 
				FROM [LES].[TT_WMM_RECEIVE_DETAIL] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TT_WMM_RECEIVE_DETAIL_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TT_WMM_RECEIVE_DETAIL]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TT_WMM_RECEIVE_DETAIL_INSERT =
			@"INSERT INTO [LES].[TT_WMM_RECEIVE_DETAIL] (
				FID,
				RECEIVE_FID,
				PLANT,
				SUPPLIER_NUM,
				WM_NO,
				ZONE_NO,
				DLOC,
				TARGET_WM,
				TARGET_ZONE,
				TARGET_DLOC,
				PART_NO,
				PART_CNAME,
				PART_ENAME,
				MEASURING_UNIT_NO,
				IDENTIFY_PART_NO,
				PACKAGE_MODEL,
				PACKAGE,
				PART_TYPE,
				REQUIRED_BOX_NUM,
				REQUIRED_QTY,
				ACTUAL_BOX_NUM,
				ACTUAL_QTY,
				BARCODE_DATA,
				TRAN_NO,
				DOCK,
				ASSEMBLY_LINE,
				BOX_PARTS,
				SEQUENCE_NO,
				PICKUP_SEQ_NO,
				RDC_DLOC,
				INHOUSE_PACKAGE,
				INHOUSE_PACKAGE_MODEL,
				RUNSHEET_NO,
				SUPPLIER_NUM_SHEET,
				BOX_PARTS_SHEET,
				RETURN_REPORT_FLAG,
				ORDER_NO,
				ITEM_NO,
				CURRENT_BOX_NUM,
				CURRENT_QTY,
				FINAL_WM,
				FINAL_ZONE,
				FINAL_DLOC,
				IS_SCAN_BOX,
				INSPECTION_MODE,
				ROW_NO,
				ORIGIN_PLACE,
				PURCHASE_UNIT_PRICE,
				PART_PRICE,
				PART_CLS,
				PACKAGE_LENGTH,
				PACKAGE_WIDTH,
				PACKAGE_HEIGHT,
				PERPACKAGE_GROSS_WEIGHT,
				COMMENTS,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				PACKAGE_VOLUME,
				SUM_WEIGHT,
				SUM_VOLUME,
				QUALIFIED_QTY,
				INSPECTION_STATUS,
				FROZEN_STOCK_FLAG				 
			) VALUES (
				@FID,
				@RECEIVE_FID,
				@PLANT,
				@SUPPLIER_NUM,
				@WM_NO,
				@ZONE_NO,
				@DLOC,
				@TARGET_WM,
				@TARGET_ZONE,
				@TARGET_DLOC,
				@PART_NO,
				@PART_CNAME,
				@PART_ENAME,
				@MEASURING_UNIT_NO,
				@IDENTIFY_PART_NO,
				@PACKAGE_MODEL,
				@PACKAGE,
				@PART_TYPE,
				@REQUIRED_BOX_NUM,
				@REQUIRED_QTY,
				@ACTUAL_BOX_NUM,
				@ACTUAL_QTY,
				@BARCODE_DATA,
				@TRAN_NO,
				@DOCK,
				@ASSEMBLY_LINE,
				@BOX_PARTS,
				@SEQUENCE_NO,
				@PICKUP_SEQ_NO,
				@RDC_DLOC,
				@INHOUSE_PACKAGE,
				@INHOUSE_PACKAGE_MODEL,
				@RUNSHEET_NO,
				@SUPPLIER_NUM_SHEET,
				@BOX_PARTS_SHEET,
				@RETURN_REPORT_FLAG,
				@ORDER_NO,
				@ITEM_NO,
				@CURRENT_BOX_NUM,
				@CURRENT_QTY,
				@FINAL_WM,
				@FINAL_ZONE,
				@FINAL_DLOC,
				@IS_SCAN_BOX,
				@INSPECTION_MODE,
				@ROW_NO,
				@ORIGIN_PLACE,
				@PURCHASE_UNIT_PRICE,
				@PART_PRICE,
				@PART_CLS,
				@PACKAGE_LENGTH,
				@PACKAGE_WIDTH,
				@PACKAGE_HEIGHT,
				@PERPACKAGE_GROSS_WEIGHT,
				@COMMENTS,
				@VALID_FLAG,
				@CREATE_USER,
				GETDATE(),
				@MODIFY_USER,
				@MODIFY_DATE,
				@PACKAGE_VOLUME,
				@SUM_WEIGHT,
				@SUM_VOLUME,
				@QUALIFIED_QTY,
				@INSPECTION_STATUS,
				@FROZEN_STOCK_FLAG				 
			);SELECT @@IDENTITY;";
		private const string TT_WMM_RECEIVE_DETAIL_UPDATE =
			@"UPDATE [LES].[TT_WMM_RECEIVE_DETAIL] WITH(ROWLOCK) 
				SET FID=@FID,
				RECEIVE_FID=@RECEIVE_FID,
				PLANT=@PLANT,
				SUPPLIER_NUM=@SUPPLIER_NUM,
				WM_NO=@WM_NO,
				ZONE_NO=@ZONE_NO,
				DLOC=@DLOC,
				TARGET_WM=@TARGET_WM,
				TARGET_ZONE=@TARGET_ZONE,
				TARGET_DLOC=@TARGET_DLOC,
				PART_NO=@PART_NO,
				PART_CNAME=@PART_CNAME,
				PART_ENAME=@PART_ENAME,
				MEASURING_UNIT_NO=@MEASURING_UNIT_NO,
				IDENTIFY_PART_NO=@IDENTIFY_PART_NO,
				PACKAGE_MODEL=@PACKAGE_MODEL,
				PACKAGE=@PACKAGE,
				PART_TYPE=@PART_TYPE,
				REQUIRED_BOX_NUM=@REQUIRED_BOX_NUM,
				REQUIRED_QTY=@REQUIRED_QTY,
				ACTUAL_BOX_NUM=@ACTUAL_BOX_NUM,
				ACTUAL_QTY=@ACTUAL_QTY,
				BARCODE_DATA=@BARCODE_DATA,
				TRAN_NO=@TRAN_NO,
				DOCK=@DOCK,
				ASSEMBLY_LINE=@ASSEMBLY_LINE,
				BOX_PARTS=@BOX_PARTS,
				SEQUENCE_NO=@SEQUENCE_NO,
				PICKUP_SEQ_NO=@PICKUP_SEQ_NO,
				RDC_DLOC=@RDC_DLOC,
				INHOUSE_PACKAGE=@INHOUSE_PACKAGE,
				INHOUSE_PACKAGE_MODEL=@INHOUSE_PACKAGE_MODEL,
				RUNSHEET_NO=@RUNSHEET_NO,
				SUPPLIER_NUM_SHEET=@SUPPLIER_NUM_SHEET,
				BOX_PARTS_SHEET=@BOX_PARTS_SHEET,
				RETURN_REPORT_FLAG=@RETURN_REPORT_FLAG,
				ORDER_NO=@ORDER_NO,
				ITEM_NO=@ITEM_NO,
				CURRENT_BOX_NUM=@CURRENT_BOX_NUM,
				CURRENT_QTY=@CURRENT_QTY,
				FINAL_WM=@FINAL_WM,
				FINAL_ZONE=@FINAL_ZONE,
				FINAL_DLOC=@FINAL_DLOC,
				IS_SCAN_BOX=@IS_SCAN_BOX,
				INSPECTION_MODE=@INSPECTION_MODE,
				ROW_NO=@ROW_NO,
				ORIGIN_PLACE=@ORIGIN_PLACE,
				PURCHASE_UNIT_PRICE=@PURCHASE_UNIT_PRICE,
				PART_PRICE=@PART_PRICE,
				PART_CLS=@PART_CLS,
				PACKAGE_LENGTH=@PACKAGE_LENGTH,
				PACKAGE_WIDTH=@PACKAGE_WIDTH,
				PACKAGE_HEIGHT=@PACKAGE_HEIGHT,
				PERPACKAGE_GROSS_WEIGHT=@PERPACKAGE_GROSS_WEIGHT,
				COMMENTS=@COMMENTS,
				VALID_FLAG=@VALID_FLAG,
				CREATE_USER=@CREATE_USER,
				CREATE_DATE=@CREATE_DATE,
				MODIFY_USER=@MODIFY_USER,
				MODIFY_DATE=@MODIFY_DATE,
				PACKAGE_VOLUME=@PACKAGE_VOLUME,
				SUM_WEIGHT=@SUM_WEIGHT,
				SUM_VOLUME=@SUM_VOLUME,
				QUALIFIED_QTY=@QUALIFIED_QTY,
				INSPECTION_STATUS=@INSPECTION_STATUS,
				FROZEN_STOCK_FLAG=@FROZEN_STOCK_FLAG				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TT_WMM_RECEIVE_DETAIL_DELETE =
			@"DELETE FROM [LES].[TT_WMM_RECEIVE_DETAIL] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get ReceiveDetailInfo
		/// </summary>
		/// <param name="ID">ReceiveDetailInfo Primary key </param>
		/// <returns></returns> 
		public ReceiveDetailInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_WMM_RECEIVE_DETAIL_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateReceiveDetailInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>ReceiveDetailInfo Collection </returns>
		public List<ReceiveDetailInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TT_WMM_RECEIVE_DETAIL_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>ReceiveDetailInfo Collection </returns>
		public List<ReceiveDetailInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<ReceiveDetailInfo> list = new List<ReceiveDetailInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateReceiveDetailInfo(dr));
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
		public List<ReceiveDetailInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TT_WMM_RECEIVE_DETAIL]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<ReceiveDetailInfo> list = new List<ReceiveDetailInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateReceiveDetailInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TT_WMM_RECEIVE_DETAIL_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(ReceiveDetailInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TT_WMM_RECEIVE_DETAIL_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@RECEIVE_FID", DbType.Guid, info.ReceiveFid);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@WM_NO", DbType.String, info.WmNo);
			db.AddInParameter(dbCommand, "@ZONE_NO", DbType.String, info.ZoneNo);
			db.AddInParameter(dbCommand, "@DLOC", DbType.String, info.Dloc);
			db.AddInParameter(dbCommand, "@TARGET_WM", DbType.String, info.TargetWm);
			db.AddInParameter(dbCommand, "@TARGET_ZONE", DbType.String, info.TargetZone);
			db.AddInParameter(dbCommand, "@TARGET_DLOC", DbType.String, info.TargetDloc);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@PART_CNAME", DbType.String, info.PartCname);
			db.AddInParameter(dbCommand, "@PART_ENAME", DbType.String, info.PartEname);
			db.AddInParameter(dbCommand, "@MEASURING_UNIT_NO", DbType.String, info.MeasuringUnitNo);
			db.AddInParameter(dbCommand, "@IDENTIFY_PART_NO", DbType.String, info.IdentifyPartNo);
			db.AddInParameter(dbCommand, "@PACKAGE_MODEL", DbType.String, info.PackageModel);
			db.AddInParameter(dbCommand, "@PACKAGE", DbType.Decimal, info.Package);
			db.AddInParameter(dbCommand, "@PART_TYPE", DbType.Int32, info.PartType);
			db.AddInParameter(dbCommand, "@REQUIRED_BOX_NUM", DbType.Int32, info.RequiredBoxNum);
			db.AddInParameter(dbCommand, "@REQUIRED_QTY", DbType.Decimal, info.RequiredQty);
			db.AddInParameter(dbCommand, "@ACTUAL_BOX_NUM", DbType.Int32, info.ActualBoxNum);
			db.AddInParameter(dbCommand, "@ACTUAL_QTY", DbType.Decimal, info.ActualQty);
			db.AddInParameter(dbCommand, "@BARCODE_DATA", DbType.String, info.BarcodeData);
			db.AddInParameter(dbCommand, "@TRAN_NO", DbType.String, info.TranNo);
			db.AddInParameter(dbCommand, "@DOCK", DbType.String, info.Dock);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@BOX_PARTS", DbType.String, info.BoxParts);
			db.AddInParameter(dbCommand, "@SEQUENCE_NO", DbType.Int32, info.SequenceNo);
			db.AddInParameter(dbCommand, "@PICKUP_SEQ_NO", DbType.Int32, info.PickupSeqNo);
			db.AddInParameter(dbCommand, "@RDC_DLOC", DbType.String, info.RdcDloc);
			db.AddInParameter(dbCommand, "@INHOUSE_PACKAGE", DbType.Decimal, info.InhousePackage);
			db.AddInParameter(dbCommand, "@INHOUSE_PACKAGE_MODEL", DbType.String, info.InhousePackageModel);
			db.AddInParameter(dbCommand, "@RUNSHEET_NO", DbType.String, info.RunsheetNo);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM_SHEET", DbType.String, info.SupplierNumSheet);
			db.AddInParameter(dbCommand, "@BOX_PARTS_SHEET", DbType.String, info.BoxPartsSheet);
			db.AddInParameter(dbCommand, "@RETURN_REPORT_FLAG", DbType.Int32, info.ReturnReportFlag);
			db.AddInParameter(dbCommand, "@ORDER_NO", DbType.String, info.OrderNo);
			db.AddInParameter(dbCommand, "@ITEM_NO", DbType.String, info.ItemNo);
			db.AddInParameter(dbCommand, "@CURRENT_BOX_NUM", DbType.Int32, info.CurrentBoxNum);
			db.AddInParameter(dbCommand, "@CURRENT_QTY", DbType.Decimal, info.CurrentQty);
			db.AddInParameter(dbCommand, "@FINAL_WM", DbType.String, info.FinalWm);
			db.AddInParameter(dbCommand, "@FINAL_ZONE", DbType.String, info.FinalZone);
			db.AddInParameter(dbCommand, "@FINAL_DLOC", DbType.String, info.FinalDloc);
			db.AddInParameter(dbCommand, "@IS_SCAN_BOX", DbType.Boolean, info.IsScanBox);
			db.AddInParameter(dbCommand, "@INSPECTION_MODE", DbType.Int32, info.InspectionMode);
			db.AddInParameter(dbCommand, "@ROW_NO", DbType.Int32, info.RowNo);
			db.AddInParameter(dbCommand, "@ORIGIN_PLACE", DbType.String, info.OriginPlace);
			db.AddInParameter(dbCommand, "@PURCHASE_UNIT_PRICE", DbType.Decimal, info.PurchaseUnitPrice);
			db.AddInParameter(dbCommand, "@PART_PRICE", DbType.Decimal, info.PartPrice);
			db.AddInParameter(dbCommand, "@PART_CLS", DbType.String, info.PartCls);
			db.AddInParameter(dbCommand, "@PACKAGE_LENGTH", DbType.Decimal, info.PackageLength);
			db.AddInParameter(dbCommand, "@PACKAGE_WIDTH", DbType.Decimal, info.PackageWidth);
			db.AddInParameter(dbCommand, "@PACKAGE_HEIGHT", DbType.Decimal, info.PackageHeight);
			db.AddInParameter(dbCommand, "@PERPACKAGE_GROSS_WEIGHT", DbType.Decimal, info.PerpackageGrossWeight);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@PACKAGE_VOLUME", DbType.Decimal, info.PackageVolume);
			db.AddInParameter(dbCommand, "@SUM_WEIGHT", DbType.Decimal, info.SumWeight);
			db.AddInParameter(dbCommand, "@SUM_VOLUME", DbType.Decimal, info.SumVolume);
			db.AddInParameter(dbCommand, "@QUALIFIED_QTY", DbType.Decimal, info.QualifiedQty);
			db.AddInParameter(dbCommand, "@INSPECTION_STATUS", DbType.Int32, info.InspectionStatus);
			db.AddInParameter(dbCommand, "@FROZEN_STOCK_FLAG", DbType.Boolean, info.FrozenStockFlag);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}
		/// <summary>
		/// GetInsertSql
		/// </summary>
		/// <param name="info"> info</param>
		public static string GetInsertSql(ReceiveDetailInfo info)
		{
			return  
			@"insert into [LES].[TT_WMM_RECEIVE_DETAIL] (
				FID,
				RECEIVE_FID,
				PLANT,
				SUPPLIER_NUM,
				WM_NO,
				ZONE_NO,
				DLOC,
				TARGET_WM,
				TARGET_ZONE,
				TARGET_DLOC,
				PART_NO,
				PART_CNAME,
				PART_ENAME,
				MEASURING_UNIT_NO,
				IDENTIFY_PART_NO,
				PACKAGE_MODEL,
				PACKAGE,
				PART_TYPE,
				REQUIRED_BOX_NUM,
				REQUIRED_QTY,
				ACTUAL_BOX_NUM,
				ACTUAL_QTY,
				BARCODE_DATA,
				TRAN_NO,
				DOCK,
				ASSEMBLY_LINE,
				BOX_PARTS,
				SEQUENCE_NO,
				PICKUP_SEQ_NO,
				RDC_DLOC,
				INHOUSE_PACKAGE,
				INHOUSE_PACKAGE_MODEL,
				RUNSHEET_NO,
				SUPPLIER_NUM_SHEET,
				BOX_PARTS_SHEET,
				RETURN_REPORT_FLAG,
				ORDER_NO,
				ITEM_NO,
				CURRENT_BOX_NUM,
				CURRENT_QTY,
				FINAL_WM,
				FINAL_ZONE,
				FINAL_DLOC,
				IS_SCAN_BOX,
				INSPECTION_MODE,
				ROW_NO,
				ORIGIN_PLACE,
				PURCHASE_UNIT_PRICE,
				PART_PRICE,
				PART_CLS,
				PACKAGE_LENGTH,
				PACKAGE_WIDTH,
				PACKAGE_HEIGHT,
				PERPACKAGE_GROSS_WEIGHT,
				COMMENTS,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				PACKAGE_VOLUME,
				SUM_WEIGHT,
				SUM_VOLUME,
				QUALIFIED_QTY,
				INSPECTION_STATUS,
				FROZEN_STOCK_FLAG				 
			) values ("+
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				(info.ReceiveFid == null ? "NULL" : "N'" + info.ReceiveFid.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.Plant) ? "NULL" : "N'" + info.Plant + "'") + ","+
				(string.IsNullOrEmpty(info.SupplierNum) ? "NULL" : "N'" + info.SupplierNum + "'") + ","+
				(string.IsNullOrEmpty(info.WmNo) ? "NULL" : "N'" + info.WmNo + "'") + ","+
				(string.IsNullOrEmpty(info.ZoneNo) ? "NULL" : "N'" + info.ZoneNo + "'") + ","+
				(string.IsNullOrEmpty(info.Dloc) ? "NULL" : "N'" + info.Dloc + "'") + ","+
				(string.IsNullOrEmpty(info.TargetWm) ? "NULL" : "N'" + info.TargetWm + "'") + ","+
				(string.IsNullOrEmpty(info.TargetZone) ? "NULL" : "N'" + info.TargetZone + "'") + ","+
				(string.IsNullOrEmpty(info.TargetDloc) ? "NULL" : "N'" + info.TargetDloc + "'") + ","+
				(string.IsNullOrEmpty(info.PartNo) ? "NULL" : "N'" + info.PartNo + "'") + ","+
				(string.IsNullOrEmpty(info.PartCname) ? "NULL" : "N'" + info.PartCname + "'") + ","+
				(string.IsNullOrEmpty(info.PartEname) ? "NULL" : "N'" + info.PartEname + "'") + ","+
				(string.IsNullOrEmpty(info.MeasuringUnitNo) ? "NULL" : "N'" + info.MeasuringUnitNo + "'") + ","+
				(string.IsNullOrEmpty(info.IdentifyPartNo) ? "NULL" : "N'" + info.IdentifyPartNo + "'") + ","+
				(string.IsNullOrEmpty(info.PackageModel) ? "NULL" : "N'" + info.PackageModel + "'") + ","+
				(info.Package == null ? "NULL" : "" + info.Package.GetValueOrDefault() + "") + ","+
				(info.PartType == null ? "NULL" : "" + info.PartType.GetValueOrDefault() + "") + ","+
				(info.RequiredBoxNum == null ? "NULL" : "" + info.RequiredBoxNum.GetValueOrDefault() + "") + ","+
				(info.RequiredQty == null ? "NULL" : "" + info.RequiredQty.GetValueOrDefault() + "") + ","+
				(info.ActualBoxNum == null ? "NULL" : "" + info.ActualBoxNum.GetValueOrDefault() + "") + ","+
				(info.ActualQty == null ? "NULL" : "" + info.ActualQty.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.BarcodeData) ? "NULL" : "N'" + info.BarcodeData + "'") + ","+
				(string.IsNullOrEmpty(info.TranNo) ? "NULL" : "N'" + info.TranNo + "'") + ","+
				(string.IsNullOrEmpty(info.Dock) ? "NULL" : "N'" + info.Dock + "'") + ","+
				(string.IsNullOrEmpty(info.AssemblyLine) ? "NULL" : "N'" + info.AssemblyLine + "'") + ","+
				(string.IsNullOrEmpty(info.BoxParts) ? "NULL" : "N'" + info.BoxParts + "'") + ","+
				(info.SequenceNo == null ? "NULL" : "" + info.SequenceNo.GetValueOrDefault() + "") + ","+
				(info.PickupSeqNo == null ? "NULL" : "" + info.PickupSeqNo.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.RdcDloc) ? "NULL" : "N'" + info.RdcDloc + "'") + ","+
				(info.InhousePackage == null ? "NULL" : "" + info.InhousePackage.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.InhousePackageModel) ? "NULL" : "N'" + info.InhousePackageModel + "'") + ","+
				(string.IsNullOrEmpty(info.RunsheetNo) ? "NULL" : "N'" + info.RunsheetNo + "'") + ","+
				(string.IsNullOrEmpty(info.SupplierNumSheet) ? "NULL" : "N'" + info.SupplierNumSheet + "'") + ","+
				(string.IsNullOrEmpty(info.BoxPartsSheet) ? "NULL" : "N'" + info.BoxPartsSheet + "'") + ","+
				(info.ReturnReportFlag == null ? "NULL" : "" + info.ReturnReportFlag.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.OrderNo) ? "NULL" : "N'" + info.OrderNo + "'") + ","+
				(string.IsNullOrEmpty(info.ItemNo) ? "NULL" : "N'" + info.ItemNo + "'") + ","+
				(info.CurrentBoxNum == null ? "NULL" : "" + info.CurrentBoxNum.GetValueOrDefault() + "") + ","+
				(info.CurrentQty == null ? "NULL" : "" + info.CurrentQty.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.FinalWm) ? "NULL" : "N'" + info.FinalWm + "'") + ","+
				(string.IsNullOrEmpty(info.FinalZone) ? "NULL" : "N'" + info.FinalZone + "'") + ","+
				(string.IsNullOrEmpty(info.FinalDloc) ? "NULL" : "N'" + info.FinalDloc + "'") + ","+
				(info.IsScanBox == null ? "NULL" : "" + (info.IsScanBox.GetValueOrDefault() ? "1" : "0") + "") + ","+
				(info.InspectionMode == null ? "NULL" : "" + info.InspectionMode.GetValueOrDefault() + "") + ","+
				(info.RowNo == null ? "NULL" : "" + info.RowNo.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.OriginPlace) ? "NULL" : "N'" + info.OriginPlace + "'") + ","+
				(info.PurchaseUnitPrice == null ? "NULL" : "" + info.PurchaseUnitPrice.GetValueOrDefault() + "") + ","+
				(info.PartPrice == null ? "NULL" : "" + info.PartPrice.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.PartCls) ? "NULL" : "N'" + info.PartCls + "'") + ","+
				(info.PackageLength == null ? "NULL" : "" + info.PackageLength.GetValueOrDefault() + "") + ","+
				(info.PackageWidth == null ? "NULL" : "" + info.PackageWidth.GetValueOrDefault() + "") + ","+
				(info.PackageHeight == null ? "NULL" : "" + info.PackageHeight.GetValueOrDefault() + "") + ","+
				(info.PerpackageGrossWeight == null ? "NULL" : "" + info.PerpackageGrossWeight.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.Comments) ? "NULL" : "N'" + info.Comments + "'") + ","+
				"1" + ","+		
				"N'" + info.CreateUser + "'" + ","+		
				"GETDATE()" + ","+			
				"NULL" + ","+			
				"NULL" + ","+			
				(info.PackageVolume == null ? "NULL" : "" + info.PackageVolume.GetValueOrDefault() + "") + ","+
				(info.SumWeight == null ? "NULL" : "" + info.SumWeight.GetValueOrDefault() + "") + ","+
				(info.SumVolume == null ? "NULL" : "" + info.SumVolume.GetValueOrDefault() + "") + ","+
				(info.QualifiedQty == null ? "NULL" : "" + info.QualifiedQty.GetValueOrDefault() + "") + ","+
				(info.InspectionStatus == null ? "NULL" : "" + info.InspectionStatus.GetValueOrDefault() + "") + ","+
				(info.FrozenStockFlag == null ? "NULL" : "" + (info.FrozenStockFlag.GetValueOrDefault() ? "1" : "0") + "") + ");";				}
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(ReceiveDetailInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_WMM_RECEIVE_DETAIL_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@RECEIVE_FID", DbType.Guid, info.ReceiveFid);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@WM_NO", DbType.String, info.WmNo);
			db.AddInParameter(dbCommand, "@ZONE_NO", DbType.String, info.ZoneNo);
			db.AddInParameter(dbCommand, "@DLOC", DbType.String, info.Dloc);
			db.AddInParameter(dbCommand, "@TARGET_WM", DbType.String, info.TargetWm);
			db.AddInParameter(dbCommand, "@TARGET_ZONE", DbType.String, info.TargetZone);
			db.AddInParameter(dbCommand, "@TARGET_DLOC", DbType.String, info.TargetDloc);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@PART_CNAME", DbType.String, info.PartCname);
			db.AddInParameter(dbCommand, "@PART_ENAME", DbType.String, info.PartEname);
			db.AddInParameter(dbCommand, "@MEASURING_UNIT_NO", DbType.String, info.MeasuringUnitNo);
			db.AddInParameter(dbCommand, "@IDENTIFY_PART_NO", DbType.String, info.IdentifyPartNo);
			db.AddInParameter(dbCommand, "@PACKAGE_MODEL", DbType.String, info.PackageModel);
			db.AddInParameter(dbCommand, "@PACKAGE", DbType.Decimal, info.Package);
			db.AddInParameter(dbCommand, "@PART_TYPE", DbType.Int32, info.PartType);
			db.AddInParameter(dbCommand, "@REQUIRED_BOX_NUM", DbType.Int32, info.RequiredBoxNum);
			db.AddInParameter(dbCommand, "@REQUIRED_QTY", DbType.Decimal, info.RequiredQty);
			db.AddInParameter(dbCommand, "@ACTUAL_BOX_NUM", DbType.Int32, info.ActualBoxNum);
			db.AddInParameter(dbCommand, "@ACTUAL_QTY", DbType.Decimal, info.ActualQty);
			db.AddInParameter(dbCommand, "@BARCODE_DATA", DbType.String, info.BarcodeData);
			db.AddInParameter(dbCommand, "@TRAN_NO", DbType.String, info.TranNo);
			db.AddInParameter(dbCommand, "@DOCK", DbType.String, info.Dock);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@BOX_PARTS", DbType.String, info.BoxParts);
			db.AddInParameter(dbCommand, "@SEQUENCE_NO", DbType.Int32, info.SequenceNo);
			db.AddInParameter(dbCommand, "@PICKUP_SEQ_NO", DbType.Int32, info.PickupSeqNo);
			db.AddInParameter(dbCommand, "@RDC_DLOC", DbType.String, info.RdcDloc);
			db.AddInParameter(dbCommand, "@INHOUSE_PACKAGE", DbType.Decimal, info.InhousePackage);
			db.AddInParameter(dbCommand, "@INHOUSE_PACKAGE_MODEL", DbType.String, info.InhousePackageModel);
			db.AddInParameter(dbCommand, "@RUNSHEET_NO", DbType.String, info.RunsheetNo);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM_SHEET", DbType.String, info.SupplierNumSheet);
			db.AddInParameter(dbCommand, "@BOX_PARTS_SHEET", DbType.String, info.BoxPartsSheet);
			db.AddInParameter(dbCommand, "@RETURN_REPORT_FLAG", DbType.Int32, info.ReturnReportFlag);
			db.AddInParameter(dbCommand, "@ORDER_NO", DbType.String, info.OrderNo);
			db.AddInParameter(dbCommand, "@ITEM_NO", DbType.String, info.ItemNo);
			db.AddInParameter(dbCommand, "@CURRENT_BOX_NUM", DbType.Int32, info.CurrentBoxNum);
			db.AddInParameter(dbCommand, "@CURRENT_QTY", DbType.Decimal, info.CurrentQty);
			db.AddInParameter(dbCommand, "@FINAL_WM", DbType.String, info.FinalWm);
			db.AddInParameter(dbCommand, "@FINAL_ZONE", DbType.String, info.FinalZone);
			db.AddInParameter(dbCommand, "@FINAL_DLOC", DbType.String, info.FinalDloc);
			db.AddInParameter(dbCommand, "@IS_SCAN_BOX", DbType.Boolean, info.IsScanBox);
			db.AddInParameter(dbCommand, "@INSPECTION_MODE", DbType.Int32, info.InspectionMode);
			db.AddInParameter(dbCommand, "@ROW_NO", DbType.Int32, info.RowNo);
			db.AddInParameter(dbCommand, "@ORIGIN_PLACE", DbType.String, info.OriginPlace);
			db.AddInParameter(dbCommand, "@PURCHASE_UNIT_PRICE", DbType.Decimal, info.PurchaseUnitPrice);
			db.AddInParameter(dbCommand, "@PART_PRICE", DbType.Decimal, info.PartPrice);
			db.AddInParameter(dbCommand, "@PART_CLS", DbType.String, info.PartCls);
			db.AddInParameter(dbCommand, "@PACKAGE_LENGTH", DbType.Decimal, info.PackageLength);
			db.AddInParameter(dbCommand, "@PACKAGE_WIDTH", DbType.Decimal, info.PackageWidth);
			db.AddInParameter(dbCommand, "@PACKAGE_HEIGHT", DbType.Decimal, info.PackageHeight);
			db.AddInParameter(dbCommand, "@PERPACKAGE_GROSS_WEIGHT", DbType.Decimal, info.PerpackageGrossWeight);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@PACKAGE_VOLUME", DbType.Decimal, info.PackageVolume);
			db.AddInParameter(dbCommand, "@SUM_WEIGHT", DbType.Decimal, info.SumWeight);
			db.AddInParameter(dbCommand, "@SUM_VOLUME", DbType.Decimal, info.SumVolume);
			db.AddInParameter(dbCommand, "@QUALIFIED_QTY", DbType.Decimal, info.QualifiedQty);
			db.AddInParameter(dbCommand, "@INSPECTION_STATUS", DbType.Int32, info.InspectionStatus);
			db.AddInParameter(dbCommand, "@FROZEN_STOCK_FLAG", DbType.Boolean, info.FrozenStockFlag);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="ID">ReceiveDetailInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_WMM_RECEIVE_DETAIL_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">ReceiveDetailInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TT_WMM_RECEIVE_DETAIL] WITH(ROWLOCK) "
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
		/// <param name="ID">ReceiveDetailInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TT_WMM_RECEIVE_DETAIL] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static ReceiveDetailInfo CreateReceiveDetailInfo(IDataReader rdr)
		{
			ReceiveDetailInfo info = new ReceiveDetailInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.ReceiveFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("RECEIVE_FID"));			
			info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));			
			info.SupplierNum = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NUM"));			
			info.WmNo = DBConvert.GetString(rdr, rdr.GetOrdinal("WM_NO"));			
			info.ZoneNo = DBConvert.GetString(rdr, rdr.GetOrdinal("ZONE_NO"));			
			info.Dloc = DBConvert.GetString(rdr, rdr.GetOrdinal("DLOC"));			
			info.TargetWm = DBConvert.GetString(rdr, rdr.GetOrdinal("TARGET_WM"));			
			info.TargetZone = DBConvert.GetString(rdr, rdr.GetOrdinal("TARGET_ZONE"));			
			info.TargetDloc = DBConvert.GetString(rdr, rdr.GetOrdinal("TARGET_DLOC"));			
			info.PartNo = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_NO"));			
			info.PartCname = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_CNAME"));			
			info.PartEname = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_ENAME"));			
			info.MeasuringUnitNo = DBConvert.GetString(rdr, rdr.GetOrdinal("MEASURING_UNIT_NO"));			
			info.IdentifyPartNo = DBConvert.GetString(rdr, rdr.GetOrdinal("IDENTIFY_PART_NO"));			
			info.PackageModel = DBConvert.GetString(rdr, rdr.GetOrdinal("PACKAGE_MODEL"));			
			info.Package = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("PACKAGE"));			
			info.PartType = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PART_TYPE"));			
			info.RequiredBoxNum = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("REQUIRED_BOX_NUM"));			
			info.RequiredQty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("REQUIRED_QTY"));			
			info.ActualBoxNum = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("ACTUAL_BOX_NUM"));			
			info.ActualQty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("ACTUAL_QTY"));			
			info.BarcodeData = DBConvert.GetString(rdr, rdr.GetOrdinal("BARCODE_DATA"));			
			info.TranNo = DBConvert.GetString(rdr, rdr.GetOrdinal("TRAN_NO"));			
			info.Dock = DBConvert.GetString(rdr, rdr.GetOrdinal("DOCK"));			
			info.AssemblyLine = DBConvert.GetString(rdr, rdr.GetOrdinal("ASSEMBLY_LINE"));			
			info.BoxParts = DBConvert.GetString(rdr, rdr.GetOrdinal("BOX_PARTS"));			
			info.SequenceNo = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("SEQUENCE_NO"));			
			info.PickupSeqNo = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PICKUP_SEQ_NO"));			
			info.RdcDloc = DBConvert.GetString(rdr, rdr.GetOrdinal("RDC_DLOC"));			
			info.InhousePackage = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("INHOUSE_PACKAGE"));			
			info.InhousePackageModel = DBConvert.GetString(rdr, rdr.GetOrdinal("INHOUSE_PACKAGE_MODEL"));			
			info.RunsheetNo = DBConvert.GetString(rdr, rdr.GetOrdinal("RUNSHEET_NO"));			
			info.SupplierNumSheet = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NUM_SHEET"));			
			info.BoxPartsSheet = DBConvert.GetString(rdr, rdr.GetOrdinal("BOX_PARTS_SHEET"));			
			info.ReturnReportFlag = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("RETURN_REPORT_FLAG"));			
			info.OrderNo = DBConvert.GetString(rdr, rdr.GetOrdinal("ORDER_NO"));			
			info.ItemNo = DBConvert.GetString(rdr, rdr.GetOrdinal("ITEM_NO"));			
			info.CurrentBoxNum = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("CURRENT_BOX_NUM"));			
			info.CurrentQty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("CURRENT_QTY"));			
			info.FinalWm = DBConvert.GetString(rdr, rdr.GetOrdinal("FINAL_WM"));			
			info.FinalZone = DBConvert.GetString(rdr, rdr.GetOrdinal("FINAL_ZONE"));			
			info.FinalDloc = DBConvert.GetString(rdr, rdr.GetOrdinal("FINAL_DLOC"));			
			info.IsScanBox = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("IS_SCAN_BOX"));			
			info.InspectionMode = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("INSPECTION_MODE"));			
			info.RowNo = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("ROW_NO"));			
			info.OriginPlace = DBConvert.GetString(rdr, rdr.GetOrdinal("ORIGIN_PLACE"));			
			info.PurchaseUnitPrice = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("PURCHASE_UNIT_PRICE"));			
			info.PartPrice = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("PART_PRICE"));			
			info.PartCls = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_CLS"));			
			info.PackageLength = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("PACKAGE_LENGTH"));			
			info.PackageWidth = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("PACKAGE_WIDTH"));			
			info.PackageHeight = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("PACKAGE_HEIGHT"));			
			info.PerpackageGrossWeight = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("PERPACKAGE_GROSS_WEIGHT"));			
			info.Comments = DBConvert.GetString(rdr, rdr.GetOrdinal("COMMENTS"));			
			info.ValidFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.CreateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			info.PackageVolume = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("PACKAGE_VOLUME"));			
			info.SumWeight = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("SUM_WEIGHT"));			
			info.SumVolume = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("SUM_VOLUME"));			
			info.QualifiedQty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("QUALIFIED_QTY"));			
			info.InspectionStatus = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("INSPECTION_STATUS"));			
			info.FrozenStockFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("FROZEN_STOCK_FLAG"));			
			return info;
		}
		
		#endregion
	}
}
