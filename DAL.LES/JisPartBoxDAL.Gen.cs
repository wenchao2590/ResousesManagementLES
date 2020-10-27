#region Declaim
//---------------------------------------------------------------------------
// Name:		JisPartBoxDAL
// Function: 	Expose data in table TM_MPM_JIS_PART_BOX from database as business object to MES system.
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
    /// JisPartBoxDAL对应表[TM_MPM_JIS_PART_BOX]
    /// </summary>
    public partial class JisPartBoxDAL : BusinessObjectProvider<JisPartBoxInfo>
	{
		#region Sql Statements
		private const string TM_MPM_JIS_PART_BOX_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				PART_BOX_CODE,
				PART_BOX_NAME,
				PLANT,
				ASSEMBLY_LINE,
				PLANT_ZONE,
				WORKSHOP,
				SUPPLIER_NUM,
				PACKAGE_MODEL,
				PACKAGE_CNAME,
				PACKAGE_ENAME,
				ACCUMULATIVE_TYPE,
				ACCUMULATIVE_QTY,
				MAX_ACCUMULATIVE_TIME,
				DELIVERY_TIME,
				UNLOADING_TIME,
				DOCK,
				PACKAGE_ROW,
				PACKAGE_COLUMN,
				PACKAGE_DEPTH,
				WORKSHOP_SECTION,
				LOCATION,
				STATUS_POINT_CODE,
				S_WM_NO,
				S_ZONE_NO,
				T_WM_NO,
				T_ZONE_NO,
				JIS_PULL_MODE,
				KEEPER,
				STATUS,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				  
				FROM [LES].[TM_MPM_JIS_PART_BOX] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TM_MPM_JIS_PART_BOX_SELECT = 
			@"SELECT ID,
				FID,
				PART_BOX_CODE,
				PART_BOX_NAME,
				PLANT,
				ASSEMBLY_LINE,
				PLANT_ZONE,
				WORKSHOP,
				SUPPLIER_NUM,
				PACKAGE_MODEL,
				PACKAGE_CNAME,
				PACKAGE_ENAME,
				ACCUMULATIVE_TYPE,
				ACCUMULATIVE_QTY,
				MAX_ACCUMULATIVE_TIME,
				DELIVERY_TIME,
				UNLOADING_TIME,
				DOCK,
				PACKAGE_ROW,
				PACKAGE_COLUMN,
				PACKAGE_DEPTH,
				WORKSHOP_SECTION,
				LOCATION,
				STATUS_POINT_CODE,
				S_WM_NO,
				S_ZONE_NO,
				T_WM_NO,
				T_ZONE_NO,
				JIS_PULL_MODE,
				KEEPER,
				STATUS,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
				FROM [LES].[TM_MPM_JIS_PART_BOX] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TM_MPM_JIS_PART_BOX_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TM_MPM_JIS_PART_BOX]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TM_MPM_JIS_PART_BOX_INSERT =
			@"INSERT INTO [LES].[TM_MPM_JIS_PART_BOX] (
				FID,
				PART_BOX_CODE,
				PART_BOX_NAME,
				PLANT,
				ASSEMBLY_LINE,
				PLANT_ZONE,
				WORKSHOP,
				SUPPLIER_NUM,
				PACKAGE_MODEL,
				PACKAGE_CNAME,
				PACKAGE_ENAME,
				ACCUMULATIVE_TYPE,
				ACCUMULATIVE_QTY,
				MAX_ACCUMULATIVE_TIME,
				DELIVERY_TIME,
				UNLOADING_TIME,
				DOCK,
				PACKAGE_ROW,
				PACKAGE_COLUMN,
				PACKAGE_DEPTH,
				WORKSHOP_SECTION,
				LOCATION,
				STATUS_POINT_CODE,
				S_WM_NO,
				S_ZONE_NO,
				T_WM_NO,
				T_ZONE_NO,
				JIS_PULL_MODE,
				KEEPER,
				STATUS,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
			) VALUES (
				@FID,
				@PART_BOX_CODE,
				@PART_BOX_NAME,
				@PLANT,
				@ASSEMBLY_LINE,
				@PLANT_ZONE,
				@WORKSHOP,
				@SUPPLIER_NUM,
				@PACKAGE_MODEL,
				@PACKAGE_CNAME,
				@PACKAGE_ENAME,
				@ACCUMULATIVE_TYPE,
				@ACCUMULATIVE_QTY,
				@MAX_ACCUMULATIVE_TIME,
				@DELIVERY_TIME,
				@UNLOADING_TIME,
				@DOCK,
				@PACKAGE_ROW,
				@PACKAGE_COLUMN,
				@PACKAGE_DEPTH,
				@WORKSHOP_SECTION,
				@LOCATION,
				@STATUS_POINT_CODE,
				@S_WM_NO,
				@S_ZONE_NO,
				@T_WM_NO,
				@T_ZONE_NO,
				@JIS_PULL_MODE,
				@KEEPER,
				@STATUS,
				@COMMENTS,
				@VALID_FLAG,
				GETDATE(),
				@CREATE_USER,
				@MODIFY_DATE,
				@MODIFY_USER				 
			);SELECT @@IDENTITY;";
		private const string TM_MPM_JIS_PART_BOX_UPDATE =
			@"UPDATE [LES].[TM_MPM_JIS_PART_BOX] WITH(ROWLOCK) 
				SET FID=@FID,
				PART_BOX_CODE=@PART_BOX_CODE,
				PART_BOX_NAME=@PART_BOX_NAME,
				PLANT=@PLANT,
				ASSEMBLY_LINE=@ASSEMBLY_LINE,
				PLANT_ZONE=@PLANT_ZONE,
				WORKSHOP=@WORKSHOP,
				SUPPLIER_NUM=@SUPPLIER_NUM,
				PACKAGE_MODEL=@PACKAGE_MODEL,
				PACKAGE_CNAME=@PACKAGE_CNAME,
				PACKAGE_ENAME=@PACKAGE_ENAME,
				ACCUMULATIVE_TYPE=@ACCUMULATIVE_TYPE,
				ACCUMULATIVE_QTY=@ACCUMULATIVE_QTY,
				MAX_ACCUMULATIVE_TIME=@MAX_ACCUMULATIVE_TIME,
				DELIVERY_TIME=@DELIVERY_TIME,
				UNLOADING_TIME=@UNLOADING_TIME,
				DOCK=@DOCK,
				PACKAGE_ROW=@PACKAGE_ROW,
				PACKAGE_COLUMN=@PACKAGE_COLUMN,
				PACKAGE_DEPTH=@PACKAGE_DEPTH,
				WORKSHOP_SECTION=@WORKSHOP_SECTION,
				LOCATION=@LOCATION,
				STATUS_POINT_CODE=@STATUS_POINT_CODE,
				S_WM_NO=@S_WM_NO,
				S_ZONE_NO=@S_ZONE_NO,
				T_WM_NO=@T_WM_NO,
				T_ZONE_NO=@T_ZONE_NO,
				JIS_PULL_MODE=@JIS_PULL_MODE,
				KEEPER=@KEEPER,
				STATUS=@STATUS,
				COMMENTS=@COMMENTS,
				VALID_FLAG=@VALID_FLAG,
				CREATE_DATE=@CREATE_DATE,
				CREATE_USER=@CREATE_USER,
				MODIFY_DATE=@MODIFY_DATE,
				MODIFY_USER=@MODIFY_USER				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TM_MPM_JIS_PART_BOX_DELETE =
			@"DELETE FROM [LES].[TM_MPM_JIS_PART_BOX] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get JisPartBoxInfo
		/// </summary>
		/// <param name="ID">JisPartBoxInfo Primary key </param>
		/// <returns></returns> 
		public JisPartBoxInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_MPM_JIS_PART_BOX_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateJisPartBoxInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>JisPartBoxInfo Collection </returns>
		public List<JisPartBoxInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TM_MPM_JIS_PART_BOX_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>JisPartBoxInfo Collection </returns>
		public List<JisPartBoxInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<JisPartBoxInfo> list = new List<JisPartBoxInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateJisPartBoxInfo(dr));
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
		public List<JisPartBoxInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TM_MPM_JIS_PART_BOX]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<JisPartBoxInfo> list = new List<JisPartBoxInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateJisPartBoxInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TM_MPM_JIS_PART_BOX_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(JisPartBoxInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TM_MPM_JIS_PART_BOX_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@PART_BOX_CODE", DbType.String, info.PartBoxCode);
			db.AddInParameter(dbCommand, "@PART_BOX_NAME", DbType.String, info.PartBoxName);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@PLANT_ZONE", DbType.String, info.PlantZone);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@PACKAGE_MODEL", DbType.String, info.PackageModel);
			db.AddInParameter(dbCommand, "@PACKAGE_CNAME", DbType.String, info.PackageCname);
			db.AddInParameter(dbCommand, "@PACKAGE_ENAME", DbType.String, info.PackageEname);
			db.AddInParameter(dbCommand, "@ACCUMULATIVE_TYPE", DbType.Int32, info.AccumulativeType);
			db.AddInParameter(dbCommand, "@ACCUMULATIVE_QTY", DbType.Decimal, info.AccumulativeQty);
			db.AddInParameter(dbCommand, "@MAX_ACCUMULATIVE_TIME", DbType.Int32, info.MaxAccumulativeTime);
			db.AddInParameter(dbCommand, "@DELIVERY_TIME", DbType.Int32, info.DeliveryTime);
			db.AddInParameter(dbCommand, "@UNLOADING_TIME", DbType.Int32, info.UnloadingTime);
			db.AddInParameter(dbCommand, "@DOCK", DbType.String, info.Dock);
			db.AddInParameter(dbCommand, "@PACKAGE_ROW", DbType.Int32, info.PackageRow);
			db.AddInParameter(dbCommand, "@PACKAGE_COLUMN", DbType.Int32, info.PackageColumn);
			db.AddInParameter(dbCommand, "@PACKAGE_DEPTH", DbType.Int32, info.PackageDepth);
			db.AddInParameter(dbCommand, "@WORKSHOP_SECTION", DbType.String, info.WorkshopSection);
			db.AddInParameter(dbCommand, "@LOCATION", DbType.String, info.Location);
			db.AddInParameter(dbCommand, "@STATUS_POINT_CODE", DbType.String, info.StatusPointCode);
			db.AddInParameter(dbCommand, "@S_WM_NO", DbType.String, info.SWmNo);
			db.AddInParameter(dbCommand, "@S_ZONE_NO", DbType.String, info.SZoneNo);
			db.AddInParameter(dbCommand, "@T_WM_NO", DbType.String, info.TWmNo);
			db.AddInParameter(dbCommand, "@T_ZONE_NO", DbType.String, info.TZoneNo);
			db.AddInParameter(dbCommand, "@JIS_PULL_MODE", DbType.Int32, info.JisPullMode);
			db.AddInParameter(dbCommand, "@KEEPER", DbType.String, info.Keeper);
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
		public static string GetInsertSql(JisPartBoxInfo info)
		{
			return  
			@"insert into [LES].[TM_MPM_JIS_PART_BOX] (
				FID,
				PART_BOX_CODE,
				PART_BOX_NAME,
				PLANT,
				ASSEMBLY_LINE,
				PLANT_ZONE,
				WORKSHOP,
				SUPPLIER_NUM,
				PACKAGE_MODEL,
				PACKAGE_CNAME,
				PACKAGE_ENAME,
				ACCUMULATIVE_TYPE,
				ACCUMULATIVE_QTY,
				MAX_ACCUMULATIVE_TIME,
				DELIVERY_TIME,
				UNLOADING_TIME,
				DOCK,
				PACKAGE_ROW,
				PACKAGE_COLUMN,
				PACKAGE_DEPTH,
				WORKSHOP_SECTION,
				LOCATION,
				STATUS_POINT_CODE,
				S_WM_NO,
				S_ZONE_NO,
				T_WM_NO,
				T_ZONE_NO,
				JIS_PULL_MODE,
				KEEPER,
				STATUS,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
			) values ("+
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.PartBoxCode) ? "NULL" : "N'" + info.PartBoxCode + "'") + ","+
				(string.IsNullOrEmpty(info.PartBoxName) ? "NULL" : "N'" + info.PartBoxName + "'") + ","+
				(string.IsNullOrEmpty(info.Plant) ? "NULL" : "N'" + info.Plant + "'") + ","+
				(string.IsNullOrEmpty(info.AssemblyLine) ? "NULL" : "N'" + info.AssemblyLine + "'") + ","+
				(string.IsNullOrEmpty(info.PlantZone) ? "NULL" : "N'" + info.PlantZone + "'") + ","+
				(string.IsNullOrEmpty(info.Workshop) ? "NULL" : "N'" + info.Workshop + "'") + ","+
				(string.IsNullOrEmpty(info.SupplierNum) ? "NULL" : "N'" + info.SupplierNum + "'") + ","+
				(string.IsNullOrEmpty(info.PackageModel) ? "NULL" : "N'" + info.PackageModel + "'") + ","+
				(string.IsNullOrEmpty(info.PackageCname) ? "NULL" : "N'" + info.PackageCname + "'") + ","+
				(string.IsNullOrEmpty(info.PackageEname) ? "NULL" : "N'" + info.PackageEname + "'") + ","+
				(info.AccumulativeType == null ? "NULL" : "" + info.AccumulativeType.GetValueOrDefault() + "") + ","+
				(info.AccumulativeQty == null ? "NULL" : "" + info.AccumulativeQty.GetValueOrDefault() + "") + ","+
				(info.MaxAccumulativeTime == null ? "NULL" : "" + info.MaxAccumulativeTime.GetValueOrDefault() + "") + ","+
				(info.DeliveryTime == null ? "NULL" : "" + info.DeliveryTime.GetValueOrDefault() + "") + ","+
				(info.UnloadingTime == null ? "NULL" : "" + info.UnloadingTime.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.Dock) ? "NULL" : "N'" + info.Dock + "'") + ","+
				(info.PackageRow == null ? "NULL" : "" + info.PackageRow.GetValueOrDefault() + "") + ","+
				(info.PackageColumn == null ? "NULL" : "" + info.PackageColumn.GetValueOrDefault() + "") + ","+
				(info.PackageDepth == null ? "NULL" : "" + info.PackageDepth.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.WorkshopSection) ? "NULL" : "N'" + info.WorkshopSection + "'") + ","+
				(string.IsNullOrEmpty(info.Location) ? "NULL" : "N'" + info.Location + "'") + ","+
				(string.IsNullOrEmpty(info.StatusPointCode) ? "NULL" : "N'" + info.StatusPointCode + "'") + ","+
				(string.IsNullOrEmpty(info.SWmNo) ? "NULL" : "N'" + info.SWmNo + "'") + ","+
				(string.IsNullOrEmpty(info.SZoneNo) ? "NULL" : "N'" + info.SZoneNo + "'") + ","+
				(string.IsNullOrEmpty(info.TWmNo) ? "NULL" : "N'" + info.TWmNo + "'") + ","+
				(string.IsNullOrEmpty(info.TZoneNo) ? "NULL" : "N'" + info.TZoneNo + "'") + ","+
				(info.JisPullMode == null ? "NULL" : "" + info.JisPullMode.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.Keeper) ? "NULL" : "N'" + info.Keeper + "'") + ","+
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
		public int Update(JisPartBoxInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_MPM_JIS_PART_BOX_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@PART_BOX_CODE", DbType.String, info.PartBoxCode);
			db.AddInParameter(dbCommand, "@PART_BOX_NAME", DbType.String, info.PartBoxName);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@PLANT_ZONE", DbType.String, info.PlantZone);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@PACKAGE_MODEL", DbType.String, info.PackageModel);
			db.AddInParameter(dbCommand, "@PACKAGE_CNAME", DbType.String, info.PackageCname);
			db.AddInParameter(dbCommand, "@PACKAGE_ENAME", DbType.String, info.PackageEname);
			db.AddInParameter(dbCommand, "@ACCUMULATIVE_TYPE", DbType.Int32, info.AccumulativeType);
			db.AddInParameter(dbCommand, "@ACCUMULATIVE_QTY", DbType.Decimal, info.AccumulativeQty);
			db.AddInParameter(dbCommand, "@MAX_ACCUMULATIVE_TIME", DbType.Int32, info.MaxAccumulativeTime);
			db.AddInParameter(dbCommand, "@DELIVERY_TIME", DbType.Int32, info.DeliveryTime);
			db.AddInParameter(dbCommand, "@UNLOADING_TIME", DbType.Int32, info.UnloadingTime);
			db.AddInParameter(dbCommand, "@DOCK", DbType.String, info.Dock);
			db.AddInParameter(dbCommand, "@PACKAGE_ROW", DbType.Int32, info.PackageRow);
			db.AddInParameter(dbCommand, "@PACKAGE_COLUMN", DbType.Int32, info.PackageColumn);
			db.AddInParameter(dbCommand, "@PACKAGE_DEPTH", DbType.Int32, info.PackageDepth);
			db.AddInParameter(dbCommand, "@WORKSHOP_SECTION", DbType.String, info.WorkshopSection);
			db.AddInParameter(dbCommand, "@LOCATION", DbType.String, info.Location);
			db.AddInParameter(dbCommand, "@STATUS_POINT_CODE", DbType.String, info.StatusPointCode);
			db.AddInParameter(dbCommand, "@S_WM_NO", DbType.String, info.SWmNo);
			db.AddInParameter(dbCommand, "@S_ZONE_NO", DbType.String, info.SZoneNo);
			db.AddInParameter(dbCommand, "@T_WM_NO", DbType.String, info.TWmNo);
			db.AddInParameter(dbCommand, "@T_ZONE_NO", DbType.String, info.TZoneNo);
			db.AddInParameter(dbCommand, "@JIS_PULL_MODE", DbType.Int32, info.JisPullMode);
			db.AddInParameter(dbCommand, "@KEEPER", DbType.String, info.Keeper);
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
		/// <param name="ID">JisPartBoxInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_MPM_JIS_PART_BOX_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">JisPartBoxInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TM_MPM_JIS_PART_BOX] WITH(ROWLOCK) "
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
		/// <param name="ID">JisPartBoxInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TM_MPM_JIS_PART_BOX] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static JisPartBoxInfo CreateJisPartBoxInfo(IDataReader rdr)
		{
			JisPartBoxInfo info = new JisPartBoxInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.PartBoxCode = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_BOX_CODE"));			
			info.PartBoxName = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_BOX_NAME"));			
			info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));			
			info.AssemblyLine = DBConvert.GetString(rdr, rdr.GetOrdinal("ASSEMBLY_LINE"));			
			info.PlantZone = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT_ZONE"));			
			info.Workshop = DBConvert.GetString(rdr, rdr.GetOrdinal("WORKSHOP"));			
			info.SupplierNum = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NUM"));			
			info.PackageModel = DBConvert.GetString(rdr, rdr.GetOrdinal("PACKAGE_MODEL"));			
			info.PackageCname = DBConvert.GetString(rdr, rdr.GetOrdinal("PACKAGE_CNAME"));			
			info.PackageEname = DBConvert.GetString(rdr, rdr.GetOrdinal("PACKAGE_ENAME"));			
			info.AccumulativeType = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("ACCUMULATIVE_TYPE"));			
			info.AccumulativeQty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("ACCUMULATIVE_QTY"));			
			info.MaxAccumulativeTime = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("MAX_ACCUMULATIVE_TIME"));			
			info.DeliveryTime = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("DELIVERY_TIME"));			
			info.UnloadingTime = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("UNLOADING_TIME"));			
			info.Dock = DBConvert.GetString(rdr, rdr.GetOrdinal("DOCK"));			
			info.PackageRow = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PACKAGE_ROW"));			
			info.PackageColumn = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PACKAGE_COLUMN"));			
			info.PackageDepth = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PACKAGE_DEPTH"));			
			info.WorkshopSection = DBConvert.GetString(rdr, rdr.GetOrdinal("WORKSHOP_SECTION"));			
			info.Location = DBConvert.GetString(rdr, rdr.GetOrdinal("LOCATION"));			
			info.StatusPointCode = DBConvert.GetString(rdr, rdr.GetOrdinal("STATUS_POINT_CODE"));			
			info.SWmNo = DBConvert.GetString(rdr, rdr.GetOrdinal("S_WM_NO"));			
			info.SZoneNo = DBConvert.GetString(rdr, rdr.GetOrdinal("S_ZONE_NO"));			
			info.TWmNo = DBConvert.GetString(rdr, rdr.GetOrdinal("T_WM_NO"));			
			info.TZoneNo = DBConvert.GetString(rdr, rdr.GetOrdinal("T_ZONE_NO"));			
			info.JisPullMode = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("JIS_PULL_MODE"));			
			info.Keeper = DBConvert.GetString(rdr, rdr.GetOrdinal("KEEPER"));			
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
