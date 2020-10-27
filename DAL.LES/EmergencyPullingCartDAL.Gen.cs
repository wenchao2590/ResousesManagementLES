#region Declaim
//---------------------------------------------------------------------------
// Name:		EmergencyPullingCartDAL
// Function: 	Expose data in table TE_MPM_EMERGENCY_PULLING_CART from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月23日
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
    /// EmergencyPullingCartDAL对应表[TE_MPM_EMERGENCY_PULLING_CART]
    /// </summary>
    public partial class EmergencyPullingCartDAL : BusinessObjectProvider<EmergencyPullingCartInfo>
	{
		#region Sql Statements
		private const string TE_MPM_EMERGENCY_PULLING_CART_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				LOGISTIC_STANDARD_FID,
				PART_NO,
				REQUIRED_PART_QTY,
				REQUIRED_BOX_QTY,
				PART_CNAME,
				PULL_PACKAGE_QTY,
				PULL_PACKAGE_MODEL,
				SUPPLIER_NUM,
				PULL_MODE,
				PART_BOX_CODE,
				S_WM_NO,
				S_ZONE_NO,
				T_WM_NO,
				T_ZONE_NO,
				PLANT,
				WORKSHOP,
				ASSEMBLY_LINE,
				WORKSHOP_SECTION,
				LOCATION,
				EMERGENCY_PULL_MODE,
				TRIGGER_PULL_FLAG,
				TRIGGER_WM_NO,
				TRIGGER_ZONE_NO,
				STATUS,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				  
				FROM [LES].[TE_MPM_EMERGENCY_PULLING_CART] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TE_MPM_EMERGENCY_PULLING_CART_SELECT = 
			@"SELECT ID,
				FID,
				LOGISTIC_STANDARD_FID,
				PART_NO,
				REQUIRED_PART_QTY,
				REQUIRED_BOX_QTY,
				PART_CNAME,
				PULL_PACKAGE_QTY,
				PULL_PACKAGE_MODEL,
				SUPPLIER_NUM,
				PULL_MODE,
				PART_BOX_CODE,
				S_WM_NO,
				S_ZONE_NO,
				T_WM_NO,
				T_ZONE_NO,
				PLANT,
				WORKSHOP,
				ASSEMBLY_LINE,
				WORKSHOP_SECTION,
				LOCATION,
				EMERGENCY_PULL_MODE,
				TRIGGER_PULL_FLAG,
				TRIGGER_WM_NO,
				TRIGGER_ZONE_NO,
				STATUS,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
				FROM [LES].[TE_MPM_EMERGENCY_PULLING_CART] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TE_MPM_EMERGENCY_PULLING_CART_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TE_MPM_EMERGENCY_PULLING_CART]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TE_MPM_EMERGENCY_PULLING_CART_INSERT =
			@"INSERT INTO [LES].[TE_MPM_EMERGENCY_PULLING_CART] (
				FID,
				LOGISTIC_STANDARD_FID,
				PART_NO,
				REQUIRED_PART_QTY,
				REQUIRED_BOX_QTY,
				PART_CNAME,
				PULL_PACKAGE_QTY,
				PULL_PACKAGE_MODEL,
				SUPPLIER_NUM,
				PULL_MODE,
				PART_BOX_CODE,
				S_WM_NO,
				S_ZONE_NO,
				T_WM_NO,
				T_ZONE_NO,
				PLANT,
				WORKSHOP,
				ASSEMBLY_LINE,
				WORKSHOP_SECTION,
				LOCATION,
				EMERGENCY_PULL_MODE,
				TRIGGER_PULL_FLAG,
				TRIGGER_WM_NO,
				TRIGGER_ZONE_NO,
				STATUS,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
			) VALUES (
				@FID,
				@LOGISTIC_STANDARD_FID,
				@PART_NO,
				@REQUIRED_PART_QTY,
				@REQUIRED_BOX_QTY,
				@PART_CNAME,
				@PULL_PACKAGE_QTY,
				@PULL_PACKAGE_MODEL,
				@SUPPLIER_NUM,
				@PULL_MODE,
				@PART_BOX_CODE,
				@S_WM_NO,
				@S_ZONE_NO,
				@T_WM_NO,
				@T_ZONE_NO,
				@PLANT,
				@WORKSHOP,
				@ASSEMBLY_LINE,
				@WORKSHOP_SECTION,
				@LOCATION,
				@EMERGENCY_PULL_MODE,
				@TRIGGER_PULL_FLAG,
				@TRIGGER_WM_NO,
				@TRIGGER_ZONE_NO,
				@STATUS,
				@COMMENTS,
				@VALID_FLAG,
				GETDATE(),
				@CREATE_USER,
				@MODIFY_DATE,
				@MODIFY_USER				 
			);SELECT @@IDENTITY;";
		private const string TE_MPM_EMERGENCY_PULLING_CART_UPDATE =
			@"UPDATE [LES].[TE_MPM_EMERGENCY_PULLING_CART] WITH(ROWLOCK) 
				SET FID=@FID,
				LOGISTIC_STANDARD_FID=@LOGISTIC_STANDARD_FID,
				PART_NO=@PART_NO,
				REQUIRED_PART_QTY=@REQUIRED_PART_QTY,
				REQUIRED_BOX_QTY=@REQUIRED_BOX_QTY,
				PART_CNAME=@PART_CNAME,
				PULL_PACKAGE_QTY=@PULL_PACKAGE_QTY,
				PULL_PACKAGE_MODEL=@PULL_PACKAGE_MODEL,
				SUPPLIER_NUM=@SUPPLIER_NUM,
				PULL_MODE=@PULL_MODE,
				PART_BOX_CODE=@PART_BOX_CODE,
				S_WM_NO=@S_WM_NO,
				S_ZONE_NO=@S_ZONE_NO,
				T_WM_NO=@T_WM_NO,
				T_ZONE_NO=@T_ZONE_NO,
				PLANT=@PLANT,
				WORKSHOP=@WORKSHOP,
				ASSEMBLY_LINE=@ASSEMBLY_LINE,
				WORKSHOP_SECTION=@WORKSHOP_SECTION,
				LOCATION=@LOCATION,
				EMERGENCY_PULL_MODE=@EMERGENCY_PULL_MODE,
				TRIGGER_PULL_FLAG=@TRIGGER_PULL_FLAG,
				TRIGGER_WM_NO=@TRIGGER_WM_NO,
				TRIGGER_ZONE_NO=@TRIGGER_ZONE_NO,
				STATUS=@STATUS,
				COMMENTS=@COMMENTS,
				VALID_FLAG=@VALID_FLAG,
				CREATE_DATE=@CREATE_DATE,
				CREATE_USER=@CREATE_USER,
				MODIFY_DATE=@MODIFY_DATE,
				MODIFY_USER=@MODIFY_USER				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TE_MPM_EMERGENCY_PULLING_CART_DELETE =
			@"DELETE FROM [LES].[TE_MPM_EMERGENCY_PULLING_CART] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get EmergencyPullingCartInfo
		/// </summary>
		/// <param name="ID">EmergencyPullingCartInfo Primary key </param>
		/// <returns></returns> 
		public EmergencyPullingCartInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TE_MPM_EMERGENCY_PULLING_CART_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateEmergencyPullingCartInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>EmergencyPullingCartInfo Collection </returns>
		public List<EmergencyPullingCartInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TE_MPM_EMERGENCY_PULLING_CART_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>EmergencyPullingCartInfo Collection </returns>
		public List<EmergencyPullingCartInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<EmergencyPullingCartInfo> list = new List<EmergencyPullingCartInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateEmergencyPullingCartInfo(dr));
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
		public List<EmergencyPullingCartInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TE_MPM_EMERGENCY_PULLING_CART]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<EmergencyPullingCartInfo> list = new List<EmergencyPullingCartInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateEmergencyPullingCartInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TE_MPM_EMERGENCY_PULLING_CART_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(EmergencyPullingCartInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TE_MPM_EMERGENCY_PULLING_CART_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@LOGISTIC_STANDARD_FID", DbType.Guid, info.LogisticStandardFid);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@REQUIRED_PART_QTY", DbType.Decimal, info.RequiredPartQty);
			db.AddInParameter(dbCommand, "@REQUIRED_BOX_QTY", DbType.Int32, info.RequiredBoxQty);
			db.AddInParameter(dbCommand, "@PART_CNAME", DbType.String, info.PartCname);
			db.AddInParameter(dbCommand, "@PULL_PACKAGE_QTY", DbType.Decimal, info.PullPackageQty);
			db.AddInParameter(dbCommand, "@PULL_PACKAGE_MODEL", DbType.String, info.PullPackageModel);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@PULL_MODE", DbType.Int32, info.PullMode);
			db.AddInParameter(dbCommand, "@PART_BOX_CODE", DbType.String, info.PartBoxCode);
			db.AddInParameter(dbCommand, "@S_WM_NO", DbType.String, info.SWmNo);
			db.AddInParameter(dbCommand, "@S_ZONE_NO", DbType.String, info.SZoneNo);
			db.AddInParameter(dbCommand, "@T_WM_NO", DbType.String, info.TWmNo);
			db.AddInParameter(dbCommand, "@T_ZONE_NO", DbType.String, info.TZoneNo);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@WORKSHOP_SECTION", DbType.String, info.WorkshopSection);
			db.AddInParameter(dbCommand, "@LOCATION", DbType.String, info.Location);
			db.AddInParameter(dbCommand, "@EMERGENCY_PULL_MODE", DbType.Int32, info.EmergencyPullMode);
			db.AddInParameter(dbCommand, "@TRIGGER_PULL_FLAG", DbType.Boolean, info.TriggerPullFlag);
			db.AddInParameter(dbCommand, "@TRIGGER_WM_NO", DbType.String, info.TriggerWmNo);
			db.AddInParameter(dbCommand, "@TRIGGER_ZONE_NO", DbType.String, info.TriggerZoneNo);
			db.AddInParameter(dbCommand, "@STATUS", DbType.Int32, info.Status);
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
		public static string GetInsertSql(EmergencyPullingCartInfo info)
		{
			return  
			@"insert into [LES].[TE_MPM_EMERGENCY_PULLING_CART] (
				FID,
				LOGISTIC_STANDARD_FID,
				PART_NO,
				REQUIRED_PART_QTY,
				REQUIRED_BOX_QTY,
				PART_CNAME,
				PULL_PACKAGE_QTY,
				PULL_PACKAGE_MODEL,
				SUPPLIER_NUM,
				PULL_MODE,
				PART_BOX_CODE,
				S_WM_NO,
				S_ZONE_NO,
				T_WM_NO,
				T_ZONE_NO,
				PLANT,
				WORKSHOP,
				ASSEMBLY_LINE,
				WORKSHOP_SECTION,
				LOCATION,
				EMERGENCY_PULL_MODE,
				TRIGGER_PULL_FLAG,
				TRIGGER_WM_NO,
				TRIGGER_ZONE_NO,
				STATUS,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
			) values ("+
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				(info.LogisticStandardFid == null ? "NULL" : "N'" + info.LogisticStandardFid.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.PartNo) ? "NULL" : "N'" + info.PartNo + "'") + ","+
				(info.RequiredPartQty == null ? "NULL" : "" + info.RequiredPartQty.GetValueOrDefault() + "") + ","+
				(info.RequiredBoxQty == null ? "NULL" : "" + info.RequiredBoxQty.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.PartCname) ? "NULL" : "N'" + info.PartCname + "'") + ","+
				(info.PullPackageQty == null ? "NULL" : "" + info.PullPackageQty.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.PullPackageModel) ? "NULL" : "N'" + info.PullPackageModel + "'") + ","+
				(string.IsNullOrEmpty(info.SupplierNum) ? "NULL" : "N'" + info.SupplierNum + "'") + ","+
				(info.PullMode == null ? "NULL" : "" + info.PullMode.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.PartBoxCode) ? "NULL" : "N'" + info.PartBoxCode + "'") + ","+
				(string.IsNullOrEmpty(info.SWmNo) ? "NULL" : "N'" + info.SWmNo + "'") + ","+
				(string.IsNullOrEmpty(info.SZoneNo) ? "NULL" : "N'" + info.SZoneNo + "'") + ","+
				(string.IsNullOrEmpty(info.TWmNo) ? "NULL" : "N'" + info.TWmNo + "'") + ","+
				(string.IsNullOrEmpty(info.TZoneNo) ? "NULL" : "N'" + info.TZoneNo + "'") + ","+
				(string.IsNullOrEmpty(info.Plant) ? "NULL" : "N'" + info.Plant + "'") + ","+
				(string.IsNullOrEmpty(info.Workshop) ? "NULL" : "N'" + info.Workshop + "'") + ","+
				(string.IsNullOrEmpty(info.AssemblyLine) ? "NULL" : "N'" + info.AssemblyLine + "'") + ","+
				(string.IsNullOrEmpty(info.WorkshopSection) ? "NULL" : "N'" + info.WorkshopSection + "'") + ","+
				(string.IsNullOrEmpty(info.Location) ? "NULL" : "N'" + info.Location + "'") + ","+
				(info.EmergencyPullMode == null ? "NULL" : "" + info.EmergencyPullMode.GetValueOrDefault() + "") + ","+
				(info.TriggerPullFlag == null ? "NULL" : "" + (info.TriggerPullFlag.GetValueOrDefault() ? "1" : "0") + "") + ","+
				(string.IsNullOrEmpty(info.TriggerWmNo) ? "NULL" : "N'" + info.TriggerWmNo + "'") + ","+
				(string.IsNullOrEmpty(info.TriggerZoneNo) ? "NULL" : "N'" + info.TriggerZoneNo + "'") + ","+
				(info.Status == null ? "NULL" : "" + info.Status.GetValueOrDefault() + "") + ","+
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
		public int Update(EmergencyPullingCartInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TE_MPM_EMERGENCY_PULLING_CART_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@LOGISTIC_STANDARD_FID", DbType.Guid, info.LogisticStandardFid);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@REQUIRED_PART_QTY", DbType.Decimal, info.RequiredPartQty);
			db.AddInParameter(dbCommand, "@REQUIRED_BOX_QTY", DbType.Int32, info.RequiredBoxQty);
			db.AddInParameter(dbCommand, "@PART_CNAME", DbType.String, info.PartCname);
			db.AddInParameter(dbCommand, "@PULL_PACKAGE_QTY", DbType.Decimal, info.PullPackageQty);
			db.AddInParameter(dbCommand, "@PULL_PACKAGE_MODEL", DbType.String, info.PullPackageModel);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@PULL_MODE", DbType.Int32, info.PullMode);
			db.AddInParameter(dbCommand, "@PART_BOX_CODE", DbType.String, info.PartBoxCode);
			db.AddInParameter(dbCommand, "@S_WM_NO", DbType.String, info.SWmNo);
			db.AddInParameter(dbCommand, "@S_ZONE_NO", DbType.String, info.SZoneNo);
			db.AddInParameter(dbCommand, "@T_WM_NO", DbType.String, info.TWmNo);
			db.AddInParameter(dbCommand, "@T_ZONE_NO", DbType.String, info.TZoneNo);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@WORKSHOP_SECTION", DbType.String, info.WorkshopSection);
			db.AddInParameter(dbCommand, "@LOCATION", DbType.String, info.Location);
			db.AddInParameter(dbCommand, "@EMERGENCY_PULL_MODE", DbType.Int32, info.EmergencyPullMode);
			db.AddInParameter(dbCommand, "@TRIGGER_PULL_FLAG", DbType.Boolean, info.TriggerPullFlag);
			db.AddInParameter(dbCommand, "@TRIGGER_WM_NO", DbType.String, info.TriggerWmNo);
			db.AddInParameter(dbCommand, "@TRIGGER_ZONE_NO", DbType.String, info.TriggerZoneNo);
			db.AddInParameter(dbCommand, "@STATUS", DbType.Int32, info.Status);
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
		/// <param name="ID">EmergencyPullingCartInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TE_MPM_EMERGENCY_PULLING_CART_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">EmergencyPullingCartInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TE_MPM_EMERGENCY_PULLING_CART] WITH(ROWLOCK) "
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
		/// <param name="ID">EmergencyPullingCartInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TE_MPM_EMERGENCY_PULLING_CART] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static EmergencyPullingCartInfo CreateEmergencyPullingCartInfo(IDataReader rdr)
		{
			EmergencyPullingCartInfo info = new EmergencyPullingCartInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.LogisticStandardFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("LOGISTIC_STANDARD_FID"));			
			info.PartNo = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_NO"));			
			info.RequiredPartQty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("REQUIRED_PART_QTY"));			
			info.RequiredBoxQty = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("REQUIRED_BOX_QTY"));			
			info.PartCname = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_CNAME"));			
			info.PullPackageQty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("PULL_PACKAGE_QTY"));			
			info.PullPackageModel = DBConvert.GetString(rdr, rdr.GetOrdinal("PULL_PACKAGE_MODEL"));			
			info.SupplierNum = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NUM"));			
			info.PullMode = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PULL_MODE"));			
			info.PartBoxCode = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_BOX_CODE"));			
			info.SWmNo = DBConvert.GetString(rdr, rdr.GetOrdinal("S_WM_NO"));			
			info.SZoneNo = DBConvert.GetString(rdr, rdr.GetOrdinal("S_ZONE_NO"));			
			info.TWmNo = DBConvert.GetString(rdr, rdr.GetOrdinal("T_WM_NO"));			
			info.TZoneNo = DBConvert.GetString(rdr, rdr.GetOrdinal("T_ZONE_NO"));			
			info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));			
			info.Workshop = DBConvert.GetString(rdr, rdr.GetOrdinal("WORKSHOP"));			
			info.AssemblyLine = DBConvert.GetString(rdr, rdr.GetOrdinal("ASSEMBLY_LINE"));			
			info.WorkshopSection = DBConvert.GetString(rdr, rdr.GetOrdinal("WORKSHOP_SECTION"));			
			info.Location = DBConvert.GetString(rdr, rdr.GetOrdinal("LOCATION"));			
			info.EmergencyPullMode = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("EMERGENCY_PULL_MODE"));			
			info.TriggerPullFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("TRIGGER_PULL_FLAG"));			
			info.TriggerWmNo = DBConvert.GetString(rdr, rdr.GetOrdinal("TRIGGER_WM_NO"));			
			info.TriggerZoneNo = DBConvert.GetString(rdr, rdr.GetOrdinal("TRIGGER_ZONE_NO"));			
			info.Status = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("STATUS"));			
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