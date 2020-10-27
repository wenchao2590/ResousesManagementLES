#region Declaim
//---------------------------------------------------------------------------
// Name:		PlanPullOrderDAL
// Function: 	Expose data in table TT_MPM_PLAN_PULL_ORDER from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月6日
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
    /// PlanPullOrderDAL对应表[TT_MPM_PLAN_PULL_ORDER]
    /// </summary>
    public partial class PlanPullOrderDAL : BusinessObjectProvider<PlanPullOrderInfo>
	{
		#region Sql Statements
		private const string TT_MPM_PLAN_PULL_ORDER_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				ORDER_CODE,
				PART_BOX_CODE,
				PART_BOX_NAME,
				PLANT,
				WORKSHOP,
				ASSEMBLY_LINE,
				SUPPLIER_NUM,
				SOURCE_ZONE_NO,
				SOURCE_WM_NO,
				TARGET_ZONE_NO,
				TARGET_WM_NO,
				PUBLISH_TIME,
				ORDER_TYPE,
				DOCK,
				UNLOADING_TIME,
				EXPECTED_ARRIVAL_TIME,
				SUGGEST_DELIVERY_TIME,
				ACTUAL_ARRIVAL_TIME,
				WINDOW_TIMES,
				ORDER_STATUS,
				COMMENTS,
				PRINT_TIMES,
				LAST_PRINT_DATE,
				LAST_PRINT_USER,
				ASN_FLAG,
				TIME_ZONE,
				KEEPER,
				DELIVERY_ADD,
				INSPECTION_FLAG,
				INSPECTION_MODE,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				TRUST_TIME,
				SUM_PACKAGE_QTY,
				SUM_PART_QTY,
				SUM_WEIGHT,
				SUM_VOLUME,
				ROUTE_CODE,
				ROUTE_NAME,
				CUST_CODE,
				CUST_SNAME,
				CUST_TRUST_NO,
				DELIVERY_CODE,
				DELIVERY_SNAME,
				DELIVERY_NAME,
				DELIVERY_CONTACT,
				DELIVERY_TEL,
				SHIPPING_CODE,
				SHIPPING_SNAME,
				SHIPPING_NAME,
				SHIPPING_ADD,
				SHIPPING_CONTACT,
				SHIPPING_TEL,
				ORGANIZATION_FID				  
				FROM [LES].[TT_MPM_PLAN_PULL_ORDER] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TT_MPM_PLAN_PULL_ORDER_SELECT = 
			@"SELECT ID,
				FID,
				ORDER_CODE,
				PART_BOX_CODE,
				PART_BOX_NAME,
				PLANT,
				WORKSHOP,
				ASSEMBLY_LINE,
				SUPPLIER_NUM,
				SOURCE_ZONE_NO,
				SOURCE_WM_NO,
				TARGET_ZONE_NO,
				TARGET_WM_NO,
				PUBLISH_TIME,
				ORDER_TYPE,
				DOCK,
				UNLOADING_TIME,
				EXPECTED_ARRIVAL_TIME,
				SUGGEST_DELIVERY_TIME,
				ACTUAL_ARRIVAL_TIME,
				WINDOW_TIMES,
				ORDER_STATUS,
				COMMENTS,
				PRINT_TIMES,
				LAST_PRINT_DATE,
				LAST_PRINT_USER,
				ASN_FLAG,
				TIME_ZONE,
				KEEPER,
				DELIVERY_ADD,
				INSPECTION_FLAG,
				INSPECTION_MODE,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				TRUST_TIME,
				SUM_PACKAGE_QTY,
				SUM_PART_QTY,
				SUM_WEIGHT,
				SUM_VOLUME,
				ROUTE_CODE,
				ROUTE_NAME,
				CUST_CODE,
				CUST_SNAME,
				CUST_TRUST_NO,
				DELIVERY_CODE,
				DELIVERY_SNAME,
				DELIVERY_NAME,
				DELIVERY_CONTACT,
				DELIVERY_TEL,
				SHIPPING_CODE,
				SHIPPING_SNAME,
				SHIPPING_NAME,
				SHIPPING_ADD,
				SHIPPING_CONTACT,
				SHIPPING_TEL,
				ORGANIZATION_FID				 
				FROM [LES].[TT_MPM_PLAN_PULL_ORDER] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TT_MPM_PLAN_PULL_ORDER_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TT_MPM_PLAN_PULL_ORDER]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TT_MPM_PLAN_PULL_ORDER_INSERT =
			@"INSERT INTO [LES].[TT_MPM_PLAN_PULL_ORDER] (
				FID,
				ORDER_CODE,
				PART_BOX_CODE,
				PART_BOX_NAME,
				PLANT,
				WORKSHOP,
				ASSEMBLY_LINE,
				SUPPLIER_NUM,
				SOURCE_ZONE_NO,
				SOURCE_WM_NO,
				TARGET_ZONE_NO,
				TARGET_WM_NO,
				PUBLISH_TIME,
				ORDER_TYPE,
				DOCK,
				UNLOADING_TIME,
				EXPECTED_ARRIVAL_TIME,
				SUGGEST_DELIVERY_TIME,
				ACTUAL_ARRIVAL_TIME,
				WINDOW_TIMES,
				ORDER_STATUS,
				COMMENTS,
				PRINT_TIMES,
				LAST_PRINT_DATE,
				LAST_PRINT_USER,
				ASN_FLAG,
				TIME_ZONE,
				KEEPER,
				DELIVERY_ADD,
				INSPECTION_FLAG,
				INSPECTION_MODE,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				TRUST_TIME,
				SUM_PACKAGE_QTY,
				SUM_PART_QTY,
				SUM_WEIGHT,
				SUM_VOLUME,
				ROUTE_CODE,
				ROUTE_NAME,
				CUST_CODE,
				CUST_SNAME,
				CUST_TRUST_NO,
				DELIVERY_CODE,
				DELIVERY_SNAME,
				DELIVERY_NAME,
				DELIVERY_CONTACT,
				DELIVERY_TEL,
				SHIPPING_CODE,
				SHIPPING_SNAME,
				SHIPPING_NAME,
				SHIPPING_ADD,
				SHIPPING_CONTACT,
				SHIPPING_TEL,
				ORGANIZATION_FID				 
			) VALUES (
				@FID,
				@ORDER_CODE,
				@PART_BOX_CODE,
				@PART_BOX_NAME,
				@PLANT,
				@WORKSHOP,
				@ASSEMBLY_LINE,
				@SUPPLIER_NUM,
				@SOURCE_ZONE_NO,
				@SOURCE_WM_NO,
				@TARGET_ZONE_NO,
				@TARGET_WM_NO,
				@PUBLISH_TIME,
				@ORDER_TYPE,
				@DOCK,
				@UNLOADING_TIME,
				@EXPECTED_ARRIVAL_TIME,
				@SUGGEST_DELIVERY_TIME,
				@ACTUAL_ARRIVAL_TIME,
				@WINDOW_TIMES,
				@ORDER_STATUS,
				@COMMENTS,
				@PRINT_TIMES,
				@LAST_PRINT_DATE,
				@LAST_PRINT_USER,
				@ASN_FLAG,
				@TIME_ZONE,
				@KEEPER,
				@DELIVERY_ADD,
				@INSPECTION_FLAG,
				@INSPECTION_MODE,
				@VALID_FLAG,
				@CREATE_USER,
				GETDATE(),
				@MODIFY_USER,
				@MODIFY_DATE,
				@TRUST_TIME,
				@SUM_PACKAGE_QTY,
				@SUM_PART_QTY,
				@SUM_WEIGHT,
				@SUM_VOLUME,
				@ROUTE_CODE,
				@ROUTE_NAME,
				@CUST_CODE,
				@CUST_SNAME,
				@CUST_TRUST_NO,
				@DELIVERY_CODE,
				@DELIVERY_SNAME,
				@DELIVERY_NAME,
				@DELIVERY_CONTACT,
				@DELIVERY_TEL,
				@SHIPPING_CODE,
				@SHIPPING_SNAME,
				@SHIPPING_NAME,
				@SHIPPING_ADD,
				@SHIPPING_CONTACT,
				@SHIPPING_TEL,
				@ORGANIZATION_FID				 
			);SELECT @@IDENTITY;";
		private const string TT_MPM_PLAN_PULL_ORDER_UPDATE =
			@"UPDATE [LES].[TT_MPM_PLAN_PULL_ORDER] WITH(ROWLOCK) 
				SET FID=@FID,
				ORDER_CODE=@ORDER_CODE,
				PART_BOX_CODE=@PART_BOX_CODE,
				PART_BOX_NAME=@PART_BOX_NAME,
				PLANT=@PLANT,
				WORKSHOP=@WORKSHOP,
				ASSEMBLY_LINE=@ASSEMBLY_LINE,
				SUPPLIER_NUM=@SUPPLIER_NUM,
				SOURCE_ZONE_NO=@SOURCE_ZONE_NO,
				SOURCE_WM_NO=@SOURCE_WM_NO,
				TARGET_ZONE_NO=@TARGET_ZONE_NO,
				TARGET_WM_NO=@TARGET_WM_NO,
				PUBLISH_TIME=@PUBLISH_TIME,
				ORDER_TYPE=@ORDER_TYPE,
				DOCK=@DOCK,
				UNLOADING_TIME=@UNLOADING_TIME,
				EXPECTED_ARRIVAL_TIME=@EXPECTED_ARRIVAL_TIME,
				SUGGEST_DELIVERY_TIME=@SUGGEST_DELIVERY_TIME,
				ACTUAL_ARRIVAL_TIME=@ACTUAL_ARRIVAL_TIME,
				WINDOW_TIMES=@WINDOW_TIMES,
				ORDER_STATUS=@ORDER_STATUS,
				COMMENTS=@COMMENTS,
				PRINT_TIMES=@PRINT_TIMES,
				LAST_PRINT_DATE=@LAST_PRINT_DATE,
				LAST_PRINT_USER=@LAST_PRINT_USER,
				ASN_FLAG=@ASN_FLAG,
				TIME_ZONE=@TIME_ZONE,
				KEEPER=@KEEPER,
				DELIVERY_ADD=@DELIVERY_ADD,
				INSPECTION_FLAG=@INSPECTION_FLAG,
				INSPECTION_MODE=@INSPECTION_MODE,
				VALID_FLAG=@VALID_FLAG,
				CREATE_USER=@CREATE_USER,
				CREATE_DATE=@CREATE_DATE,
				MODIFY_USER=@MODIFY_USER,
				MODIFY_DATE=@MODIFY_DATE,
				TRUST_TIME=@TRUST_TIME,
				SUM_PACKAGE_QTY=@SUM_PACKAGE_QTY,
				SUM_PART_QTY=@SUM_PART_QTY,
				SUM_WEIGHT=@SUM_WEIGHT,
				SUM_VOLUME=@SUM_VOLUME,
				ROUTE_CODE=@ROUTE_CODE,
				ROUTE_NAME=@ROUTE_NAME,
				CUST_CODE=@CUST_CODE,
				CUST_SNAME=@CUST_SNAME,
				CUST_TRUST_NO=@CUST_TRUST_NO,
				DELIVERY_CODE=@DELIVERY_CODE,
				DELIVERY_SNAME=@DELIVERY_SNAME,
				DELIVERY_NAME=@DELIVERY_NAME,
				DELIVERY_CONTACT=@DELIVERY_CONTACT,
				DELIVERY_TEL=@DELIVERY_TEL,
				SHIPPING_CODE=@SHIPPING_CODE,
				SHIPPING_SNAME=@SHIPPING_SNAME,
				SHIPPING_NAME=@SHIPPING_NAME,
				SHIPPING_ADD=@SHIPPING_ADD,
				SHIPPING_CONTACT=@SHIPPING_CONTACT,
				SHIPPING_TEL=@SHIPPING_TEL,
				ORGANIZATION_FID=@ORGANIZATION_FID				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TT_MPM_PLAN_PULL_ORDER_DELETE =
			@"DELETE FROM [LES].[TT_MPM_PLAN_PULL_ORDER] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get PlanPullOrderInfo
		/// </summary>
		/// <param name="ID">PlanPullOrderInfo Primary key </param>
		/// <returns></returns> 
		public PlanPullOrderInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_MPM_PLAN_PULL_ORDER_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreatePlanPullOrderInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>PlanPullOrderInfo Collection </returns>
		public List<PlanPullOrderInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TT_MPM_PLAN_PULL_ORDER_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>PlanPullOrderInfo Collection </returns>
		public List<PlanPullOrderInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<PlanPullOrderInfo> list = new List<PlanPullOrderInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreatePlanPullOrderInfo(dr));
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
		public List<PlanPullOrderInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TT_MPM_PLAN_PULL_ORDER]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<PlanPullOrderInfo> list = new List<PlanPullOrderInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreatePlanPullOrderInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TT_MPM_PLAN_PULL_ORDER_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(PlanPullOrderInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TT_MPM_PLAN_PULL_ORDER_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@ORDER_CODE", DbType.String, info.OrderCode);
			db.AddInParameter(dbCommand, "@PART_BOX_CODE", DbType.String, info.PartBoxCode);
			db.AddInParameter(dbCommand, "@PART_BOX_NAME", DbType.String, info.PartBoxName);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@SOURCE_ZONE_NO", DbType.String, info.SourceZoneNo);
			db.AddInParameter(dbCommand, "@SOURCE_WM_NO", DbType.String, info.SourceWmNo);
			db.AddInParameter(dbCommand, "@TARGET_ZONE_NO", DbType.String, info.TargetZoneNo);
			db.AddInParameter(dbCommand, "@TARGET_WM_NO", DbType.String, info.TargetWmNo);
			db.AddInParameter(dbCommand, "@PUBLISH_TIME", DbType.DateTime, info.PublishTime);
			db.AddInParameter(dbCommand, "@ORDER_TYPE", DbType.Int32, info.OrderType);
			db.AddInParameter(dbCommand, "@DOCK", DbType.String, info.Dock);
			db.AddInParameter(dbCommand, "@UNLOADING_TIME", DbType.Int32, info.UnloadingTime);
			db.AddInParameter(dbCommand, "@EXPECTED_ARRIVAL_TIME", DbType.DateTime, info.ExpectedArrivalTime);
			db.AddInParameter(dbCommand, "@SUGGEST_DELIVERY_TIME", DbType.DateTime, info.SuggestDeliveryTime);
			db.AddInParameter(dbCommand, "@ACTUAL_ARRIVAL_TIME", DbType.DateTime, info.ActualArrivalTime);
			db.AddInParameter(dbCommand, "@WINDOW_TIMES", DbType.String, info.WindowTimes);
			db.AddInParameter(dbCommand, "@ORDER_STATUS", DbType.Int32, info.OrderStatus);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@PRINT_TIMES", DbType.Int32, info.PrintTimes);
			db.AddInParameter(dbCommand, "@LAST_PRINT_DATE", DbType.DateTime, info.LastPrintDate);
			db.AddInParameter(dbCommand, "@LAST_PRINT_USER", DbType.String, info.LastPrintUser);
			db.AddInParameter(dbCommand, "@ASN_FLAG", DbType.Boolean, info.AsnFlag);
			db.AddInParameter(dbCommand, "@TIME_ZONE", DbType.String, info.TimeZone);
			db.AddInParameter(dbCommand, "@KEEPER", DbType.String, info.Keeper);
			db.AddInParameter(dbCommand, "@DELIVERY_ADD", DbType.String, info.DeliveryAdd);
			db.AddInParameter(dbCommand, "@INSPECTION_FLAG", DbType.Boolean, info.InspectionFlag);
			db.AddInParameter(dbCommand, "@INSPECTION_MODE", DbType.Int32, info.InspectionMode);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@TRUST_TIME", DbType.DateTime, info.TrustTime);
			db.AddInParameter(dbCommand, "@SUM_PACKAGE_QTY", DbType.Int32, info.SumPackageQty);
			db.AddInParameter(dbCommand, "@SUM_PART_QTY", DbType.Decimal, info.SumPartQty);
			db.AddInParameter(dbCommand, "@SUM_WEIGHT", DbType.Decimal, info.SumWeight);
			db.AddInParameter(dbCommand, "@SUM_VOLUME", DbType.Decimal, info.SumVolume);
			db.AddInParameter(dbCommand, "@ROUTE_CODE", DbType.String, info.RouteCode);
			db.AddInParameter(dbCommand, "@ROUTE_NAME", DbType.String, info.RouteName);
			db.AddInParameter(dbCommand, "@CUST_CODE", DbType.String, info.CustCode);
			db.AddInParameter(dbCommand, "@CUST_SNAME", DbType.String, info.CustSname);
			db.AddInParameter(dbCommand, "@CUST_TRUST_NO", DbType.String, info.CustTrustNo);
			db.AddInParameter(dbCommand, "@DELIVERY_CODE", DbType.String, info.DeliveryCode);
			db.AddInParameter(dbCommand, "@DELIVERY_SNAME", DbType.String, info.DeliverySname);
			db.AddInParameter(dbCommand, "@DELIVERY_NAME", DbType.String, info.DeliveryName);
			db.AddInParameter(dbCommand, "@DELIVERY_CONTACT", DbType.String, info.DeliveryContact);
			db.AddInParameter(dbCommand, "@DELIVERY_TEL", DbType.String, info.DeliveryTel);
			db.AddInParameter(dbCommand, "@SHIPPING_CODE", DbType.String, info.ShippingCode);
			db.AddInParameter(dbCommand, "@SHIPPING_SNAME", DbType.String, info.ShippingSname);
			db.AddInParameter(dbCommand, "@SHIPPING_NAME", DbType.String, info.ShippingName);
			db.AddInParameter(dbCommand, "@SHIPPING_ADD", DbType.String, info.ShippingAdd);
			db.AddInParameter(dbCommand, "@SHIPPING_CONTACT", DbType.String, info.ShippingContact);
			db.AddInParameter(dbCommand, "@SHIPPING_TEL", DbType.String, info.ShippingTel);
			db.AddInParameter(dbCommand, "@ORGANIZATION_FID", DbType.Guid, info.OrganizationFid);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}
		/// <summary>
		/// GetInsertSql
		/// </summary>
		/// <param name="info"> info</param>
		public static string GetInsertSql(PlanPullOrderInfo info)
		{
			return  
			@"insert into [LES].[TT_MPM_PLAN_PULL_ORDER] (
				FID,
				ORDER_CODE,
				PART_BOX_CODE,
				PART_BOX_NAME,
				PLANT,
				WORKSHOP,
				ASSEMBLY_LINE,
				SUPPLIER_NUM,
				SOURCE_ZONE_NO,
				SOURCE_WM_NO,
				TARGET_ZONE_NO,
				TARGET_WM_NO,
				PUBLISH_TIME,
				ORDER_TYPE,
				DOCK,
				UNLOADING_TIME,
				EXPECTED_ARRIVAL_TIME,
				SUGGEST_DELIVERY_TIME,
				ACTUAL_ARRIVAL_TIME,
				WINDOW_TIMES,
				ORDER_STATUS,
				COMMENTS,
				PRINT_TIMES,
				LAST_PRINT_DATE,
				LAST_PRINT_USER,
				ASN_FLAG,
				TIME_ZONE,
				KEEPER,
				DELIVERY_ADD,
				INSPECTION_FLAG,
				INSPECTION_MODE,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				TRUST_TIME,
				SUM_PACKAGE_QTY,
				SUM_PART_QTY,
				SUM_WEIGHT,
				SUM_VOLUME,
				ROUTE_CODE,
				ROUTE_NAME,
				CUST_CODE,
				CUST_SNAME,
				CUST_TRUST_NO,
				DELIVERY_CODE,
				DELIVERY_SNAME,
				DELIVERY_NAME,
				DELIVERY_CONTACT,
				DELIVERY_TEL,
				SHIPPING_CODE,
				SHIPPING_SNAME,
				SHIPPING_NAME,
				SHIPPING_ADD,
				SHIPPING_CONTACT,
				SHIPPING_TEL,
				ORGANIZATION_FID				 
			) values ("+
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.OrderCode) ? "NULL" : "N'" + info.OrderCode + "'") + ","+
				(string.IsNullOrEmpty(info.PartBoxCode) ? "NULL" : "N'" + info.PartBoxCode + "'") + ","+
				(string.IsNullOrEmpty(info.PartBoxName) ? "NULL" : "N'" + info.PartBoxName + "'") + ","+
				(string.IsNullOrEmpty(info.Plant) ? "NULL" : "N'" + info.Plant + "'") + ","+
				(string.IsNullOrEmpty(info.Workshop) ? "NULL" : "N'" + info.Workshop + "'") + ","+
				(string.IsNullOrEmpty(info.AssemblyLine) ? "NULL" : "N'" + info.AssemblyLine + "'") + ","+
				(string.IsNullOrEmpty(info.SupplierNum) ? "NULL" : "N'" + info.SupplierNum + "'") + ","+
				(string.IsNullOrEmpty(info.SourceZoneNo) ? "NULL" : "N'" + info.SourceZoneNo + "'") + ","+
				(string.IsNullOrEmpty(info.SourceWmNo) ? "NULL" : "N'" + info.SourceWmNo + "'") + ","+
				(string.IsNullOrEmpty(info.TargetZoneNo) ? "NULL" : "N'" + info.TargetZoneNo + "'") + ","+
				(string.IsNullOrEmpty(info.TargetWmNo) ? "NULL" : "N'" + info.TargetWmNo + "'") + ","+
				(info.PublishTime == null ? "NULL" : "N'" + info.PublishTime.GetValueOrDefault() + "'") + ","+
				(info.OrderType == null ? "NULL" : "" + info.OrderType.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.Dock) ? "NULL" : "N'" + info.Dock + "'") + ","+
				(info.UnloadingTime == null ? "NULL" : "" + info.UnloadingTime.GetValueOrDefault() + "") + ","+
				(info.ExpectedArrivalTime == null ? "NULL" : "N'" + info.ExpectedArrivalTime.GetValueOrDefault() + "'") + ","+
				(info.SuggestDeliveryTime == null ? "NULL" : "N'" + info.SuggestDeliveryTime.GetValueOrDefault() + "'") + ","+
				(info.ActualArrivalTime == null ? "NULL" : "N'" + info.ActualArrivalTime.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.WindowTimes) ? "NULL" : "N'" + info.WindowTimes + "'") + ","+
				(info.OrderStatus == null ? "NULL" : "" + info.OrderStatus.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.Comments) ? "NULL" : "N'" + info.Comments + "'") + ","+
				(info.PrintTimes == null ? "NULL" : "" + info.PrintTimes.GetValueOrDefault() + "") + ","+
				(info.LastPrintDate == null ? "NULL" : "N'" + info.LastPrintDate.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.LastPrintUser) ? "NULL" : "N'" + info.LastPrintUser + "'") + ","+
				(info.AsnFlag == null ? "NULL" : "" + (info.AsnFlag.GetValueOrDefault() ? "1" : "0") + "") + ","+
				(string.IsNullOrEmpty(info.TimeZone) ? "NULL" : "N'" + info.TimeZone + "'") + ","+
				(string.IsNullOrEmpty(info.Keeper) ? "NULL" : "N'" + info.Keeper + "'") + ","+
				(string.IsNullOrEmpty(info.DeliveryAdd) ? "NULL" : "N'" + info.DeliveryAdd + "'") + ","+
				(info.InspectionFlag == null ? "NULL" : "" + (info.InspectionFlag.GetValueOrDefault() ? "1" : "0") + "") + ","+
				(info.InspectionMode == null ? "NULL" : "" + info.InspectionMode.GetValueOrDefault() + "") + ","+
				"1" + ","+		
				"N'" + info.CreateUser + "'" + ","+		
				"GETDATE()" + ","+			
				"NULL" + ","+			
				"NULL" + ","+			
				(info.TrustTime == null ? "NULL" : "N'" + info.TrustTime.GetValueOrDefault() + "'") + ","+
				(info.SumPackageQty == null ? "NULL" : "" + info.SumPackageQty.GetValueOrDefault() + "") + ","+
				(info.SumPartQty == null ? "NULL" : "" + info.SumPartQty.GetValueOrDefault() + "") + ","+
				(info.SumWeight == null ? "NULL" : "" + info.SumWeight.GetValueOrDefault() + "") + ","+
				(info.SumVolume == null ? "NULL" : "" + info.SumVolume.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.RouteCode) ? "NULL" : "N'" + info.RouteCode + "'") + ","+
				(string.IsNullOrEmpty(info.RouteName) ? "NULL" : "N'" + info.RouteName + "'") + ","+
				(string.IsNullOrEmpty(info.CustCode) ? "NULL" : "N'" + info.CustCode + "'") + ","+
				(string.IsNullOrEmpty(info.CustSname) ? "NULL" : "N'" + info.CustSname + "'") + ","+
				(string.IsNullOrEmpty(info.CustTrustNo) ? "NULL" : "N'" + info.CustTrustNo + "'") + ","+
				(string.IsNullOrEmpty(info.DeliveryCode) ? "NULL" : "N'" + info.DeliveryCode + "'") + ","+
				(string.IsNullOrEmpty(info.DeliverySname) ? "NULL" : "N'" + info.DeliverySname + "'") + ","+
				(string.IsNullOrEmpty(info.DeliveryName) ? "NULL" : "N'" + info.DeliveryName + "'") + ","+
				(string.IsNullOrEmpty(info.DeliveryContact) ? "NULL" : "N'" + info.DeliveryContact + "'") + ","+
				(string.IsNullOrEmpty(info.DeliveryTel) ? "NULL" : "N'" + info.DeliveryTel + "'") + ","+
				(string.IsNullOrEmpty(info.ShippingCode) ? "NULL" : "N'" + info.ShippingCode + "'") + ","+
				(string.IsNullOrEmpty(info.ShippingSname) ? "NULL" : "N'" + info.ShippingSname + "'") + ","+
				(string.IsNullOrEmpty(info.ShippingName) ? "NULL" : "N'" + info.ShippingName + "'") + ","+
				(string.IsNullOrEmpty(info.ShippingAdd) ? "NULL" : "N'" + info.ShippingAdd + "'") + ","+
				(string.IsNullOrEmpty(info.ShippingContact) ? "NULL" : "N'" + info.ShippingContact + "'") + ","+
				(string.IsNullOrEmpty(info.ShippingTel) ? "NULL" : "N'" + info.ShippingTel + "'") + ","+
				(info.OrganizationFid == null ? "NULL" : "N'" + info.OrganizationFid.GetValueOrDefault() + "'") + ");";				}
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(PlanPullOrderInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_MPM_PLAN_PULL_ORDER_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@ORDER_CODE", DbType.String, info.OrderCode);
			db.AddInParameter(dbCommand, "@PART_BOX_CODE", DbType.String, info.PartBoxCode);
			db.AddInParameter(dbCommand, "@PART_BOX_NAME", DbType.String, info.PartBoxName);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@SOURCE_ZONE_NO", DbType.String, info.SourceZoneNo);
			db.AddInParameter(dbCommand, "@SOURCE_WM_NO", DbType.String, info.SourceWmNo);
			db.AddInParameter(dbCommand, "@TARGET_ZONE_NO", DbType.String, info.TargetZoneNo);
			db.AddInParameter(dbCommand, "@TARGET_WM_NO", DbType.String, info.TargetWmNo);
			db.AddInParameter(dbCommand, "@PUBLISH_TIME", DbType.DateTime, info.PublishTime);
			db.AddInParameter(dbCommand, "@ORDER_TYPE", DbType.Int32, info.OrderType);
			db.AddInParameter(dbCommand, "@DOCK", DbType.String, info.Dock);
			db.AddInParameter(dbCommand, "@UNLOADING_TIME", DbType.Int32, info.UnloadingTime);
			db.AddInParameter(dbCommand, "@EXPECTED_ARRIVAL_TIME", DbType.DateTime, info.ExpectedArrivalTime);
			db.AddInParameter(dbCommand, "@SUGGEST_DELIVERY_TIME", DbType.DateTime, info.SuggestDeliveryTime);
			db.AddInParameter(dbCommand, "@ACTUAL_ARRIVAL_TIME", DbType.DateTime, info.ActualArrivalTime);
			db.AddInParameter(dbCommand, "@WINDOW_TIMES", DbType.String, info.WindowTimes);
			db.AddInParameter(dbCommand, "@ORDER_STATUS", DbType.Int32, info.OrderStatus);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@PRINT_TIMES", DbType.Int32, info.PrintTimes);
			db.AddInParameter(dbCommand, "@LAST_PRINT_DATE", DbType.DateTime, info.LastPrintDate);
			db.AddInParameter(dbCommand, "@LAST_PRINT_USER", DbType.String, info.LastPrintUser);
			db.AddInParameter(dbCommand, "@ASN_FLAG", DbType.Boolean, info.AsnFlag);
			db.AddInParameter(dbCommand, "@TIME_ZONE", DbType.String, info.TimeZone);
			db.AddInParameter(dbCommand, "@KEEPER", DbType.String, info.Keeper);
			db.AddInParameter(dbCommand, "@DELIVERY_ADD", DbType.String, info.DeliveryAdd);
			db.AddInParameter(dbCommand, "@INSPECTION_FLAG", DbType.Boolean, info.InspectionFlag);
			db.AddInParameter(dbCommand, "@INSPECTION_MODE", DbType.Int32, info.InspectionMode);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@TRUST_TIME", DbType.DateTime, info.TrustTime);
			db.AddInParameter(dbCommand, "@SUM_PACKAGE_QTY", DbType.Int32, info.SumPackageQty);
			db.AddInParameter(dbCommand, "@SUM_PART_QTY", DbType.Decimal, info.SumPartQty);
			db.AddInParameter(dbCommand, "@SUM_WEIGHT", DbType.Decimal, info.SumWeight);
			db.AddInParameter(dbCommand, "@SUM_VOLUME", DbType.Decimal, info.SumVolume);
			db.AddInParameter(dbCommand, "@ROUTE_CODE", DbType.String, info.RouteCode);
			db.AddInParameter(dbCommand, "@ROUTE_NAME", DbType.String, info.RouteName);
			db.AddInParameter(dbCommand, "@CUST_CODE", DbType.String, info.CustCode);
			db.AddInParameter(dbCommand, "@CUST_SNAME", DbType.String, info.CustSname);
			db.AddInParameter(dbCommand, "@CUST_TRUST_NO", DbType.String, info.CustTrustNo);
			db.AddInParameter(dbCommand, "@DELIVERY_CODE", DbType.String, info.DeliveryCode);
			db.AddInParameter(dbCommand, "@DELIVERY_SNAME", DbType.String, info.DeliverySname);
			db.AddInParameter(dbCommand, "@DELIVERY_NAME", DbType.String, info.DeliveryName);
			db.AddInParameter(dbCommand, "@DELIVERY_CONTACT", DbType.String, info.DeliveryContact);
			db.AddInParameter(dbCommand, "@DELIVERY_TEL", DbType.String, info.DeliveryTel);
			db.AddInParameter(dbCommand, "@SHIPPING_CODE", DbType.String, info.ShippingCode);
			db.AddInParameter(dbCommand, "@SHIPPING_SNAME", DbType.String, info.ShippingSname);
			db.AddInParameter(dbCommand, "@SHIPPING_NAME", DbType.String, info.ShippingName);
			db.AddInParameter(dbCommand, "@SHIPPING_ADD", DbType.String, info.ShippingAdd);
			db.AddInParameter(dbCommand, "@SHIPPING_CONTACT", DbType.String, info.ShippingContact);
			db.AddInParameter(dbCommand, "@SHIPPING_TEL", DbType.String, info.ShippingTel);
			db.AddInParameter(dbCommand, "@ORGANIZATION_FID", DbType.Guid, info.OrganizationFid);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="ID">PlanPullOrderInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_MPM_PLAN_PULL_ORDER_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">PlanPullOrderInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TT_MPM_PLAN_PULL_ORDER] WITH(ROWLOCK) "
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
		/// <param name="ID">PlanPullOrderInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TT_MPM_PLAN_PULL_ORDER] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static PlanPullOrderInfo CreatePlanPullOrderInfo(IDataReader rdr)
		{
			PlanPullOrderInfo info = new PlanPullOrderInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.OrderCode = DBConvert.GetString(rdr, rdr.GetOrdinal("ORDER_CODE"));			
			info.PartBoxCode = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_BOX_CODE"));			
			info.PartBoxName = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_BOX_NAME"));			
			info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));			
			info.Workshop = DBConvert.GetString(rdr, rdr.GetOrdinal("WORKSHOP"));			
			info.AssemblyLine = DBConvert.GetString(rdr, rdr.GetOrdinal("ASSEMBLY_LINE"));			
			info.SupplierNum = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NUM"));			
			info.SourceZoneNo = DBConvert.GetString(rdr, rdr.GetOrdinal("SOURCE_ZONE_NO"));			
			info.SourceWmNo = DBConvert.GetString(rdr, rdr.GetOrdinal("SOURCE_WM_NO"));			
			info.TargetZoneNo = DBConvert.GetString(rdr, rdr.GetOrdinal("TARGET_ZONE_NO"));			
			info.TargetWmNo = DBConvert.GetString(rdr, rdr.GetOrdinal("TARGET_WM_NO"));			
			info.PublishTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("PUBLISH_TIME"));			
			info.OrderType = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("ORDER_TYPE"));			
			info.Dock = DBConvert.GetString(rdr, rdr.GetOrdinal("DOCK"));			
			info.UnloadingTime = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("UNLOADING_TIME"));			
			info.ExpectedArrivalTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("EXPECTED_ARRIVAL_TIME"));			
			info.SuggestDeliveryTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("SUGGEST_DELIVERY_TIME"));			
			info.ActualArrivalTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("ACTUAL_ARRIVAL_TIME"));			
			info.WindowTimes = DBConvert.GetString(rdr, rdr.GetOrdinal("WINDOW_TIMES"));			
			info.OrderStatus = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("ORDER_STATUS"));			
			info.Comments = DBConvert.GetString(rdr, rdr.GetOrdinal("COMMENTS"));			
			info.PrintTimes = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PRINT_TIMES"));			
			info.LastPrintDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("LAST_PRINT_DATE"));			
			info.LastPrintUser = DBConvert.GetString(rdr, rdr.GetOrdinal("LAST_PRINT_USER"));			
			info.AsnFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("ASN_FLAG"));			
			info.TimeZone = DBConvert.GetString(rdr, rdr.GetOrdinal("TIME_ZONE"));			
			info.Keeper = DBConvert.GetString(rdr, rdr.GetOrdinal("KEEPER"));			
			info.DeliveryAdd = DBConvert.GetString(rdr, rdr.GetOrdinal("DELIVERY_ADD"));			
			info.InspectionFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("INSPECTION_FLAG"));			
			info.InspectionMode = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("INSPECTION_MODE"));			
			info.ValidFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.CreateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			info.TrustTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("TRUST_TIME"));			
			info.SumPackageQty = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("SUM_PACKAGE_QTY"));			
			info.SumPartQty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("SUM_PART_QTY"));			
			info.SumWeight = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("SUM_WEIGHT"));			
			info.SumVolume = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("SUM_VOLUME"));			
			info.RouteCode = DBConvert.GetString(rdr, rdr.GetOrdinal("ROUTE_CODE"));			
			info.RouteName = DBConvert.GetString(rdr, rdr.GetOrdinal("ROUTE_NAME"));			
			info.CustCode = DBConvert.GetString(rdr, rdr.GetOrdinal("CUST_CODE"));			
			info.CustSname = DBConvert.GetString(rdr, rdr.GetOrdinal("CUST_SNAME"));			
			info.CustTrustNo = DBConvert.GetString(rdr, rdr.GetOrdinal("CUST_TRUST_NO"));			
			info.DeliveryCode = DBConvert.GetString(rdr, rdr.GetOrdinal("DELIVERY_CODE"));			
			info.DeliverySname = DBConvert.GetString(rdr, rdr.GetOrdinal("DELIVERY_SNAME"));			
			info.DeliveryName = DBConvert.GetString(rdr, rdr.GetOrdinal("DELIVERY_NAME"));			
			info.DeliveryContact = DBConvert.GetString(rdr, rdr.GetOrdinal("DELIVERY_CONTACT"));			
			info.DeliveryTel = DBConvert.GetString(rdr, rdr.GetOrdinal("DELIVERY_TEL"));			
			info.ShippingCode = DBConvert.GetString(rdr, rdr.GetOrdinal("SHIPPING_CODE"));			
			info.ShippingSname = DBConvert.GetString(rdr, rdr.GetOrdinal("SHIPPING_SNAME"));			
			info.ShippingName = DBConvert.GetString(rdr, rdr.GetOrdinal("SHIPPING_NAME"));			
			info.ShippingAdd = DBConvert.GetString(rdr, rdr.GetOrdinal("SHIPPING_ADD"));			
			info.ShippingContact = DBConvert.GetString(rdr, rdr.GetOrdinal("SHIPPING_CONTACT"));			
			info.ShippingTel = DBConvert.GetString(rdr, rdr.GetOrdinal("SHIPPING_TEL"));			
			info.OrganizationFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("ORGANIZATION_FID"));			
			return info;
		}
		
		#endregion
	}
}
