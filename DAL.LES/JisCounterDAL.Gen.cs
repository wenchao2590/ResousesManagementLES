#region Declaim
//---------------------------------------------------------------------------
// Name:		JisCounterDAL
// Function: 	Expose data in table TT_MPM_JIS_COUNTER from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月11日
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
    /// JisCounterDAL对应表[TT_MPM_JIS_COUNTER]
    /// </summary>
    public partial class JisCounterDAL : BusinessObjectProvider<JisCounterInfo>
	{
		#region Sql Statements
		private const string TT_MPM_JIS_COUNTER_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				PART_BOX_FID,
				PART_BOX_CODE,
				PLANT,
				PLANT_ZONE,
				WORKSHOP,
				ASSEMBLY_LINE,
				SUPPLIER_NUM,
				ACCUMULATIVE_TYPE,
				WORKSHOP_SECTION,
				LOCATION,
				CURRENT_QTY,
				ACCUMULATIVE_QTY,
				PACKAGE_MODEL,
				STATUS,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				  
				FROM [LES].[TT_MPM_JIS_COUNTER] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TT_MPM_JIS_COUNTER_SELECT = 
			@"SELECT ID,
				FID,
				PART_BOX_FID,
				PART_BOX_CODE,
				PLANT,
				PLANT_ZONE,
				WORKSHOP,
				ASSEMBLY_LINE,
				SUPPLIER_NUM,
				ACCUMULATIVE_TYPE,
				WORKSHOP_SECTION,
				LOCATION,
				CURRENT_QTY,
				ACCUMULATIVE_QTY,
				PACKAGE_MODEL,
				STATUS,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
				FROM [LES].[TT_MPM_JIS_COUNTER] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TT_MPM_JIS_COUNTER_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TT_MPM_JIS_COUNTER]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TT_MPM_JIS_COUNTER_INSERT =
			@"INSERT INTO [LES].[TT_MPM_JIS_COUNTER] (
				FID,
				PART_BOX_FID,
				PART_BOX_CODE,
				PLANT,
				PLANT_ZONE,
				WORKSHOP,
				ASSEMBLY_LINE,
				SUPPLIER_NUM,
				ACCUMULATIVE_TYPE,
				WORKSHOP_SECTION,
				LOCATION,
				CURRENT_QTY,
				ACCUMULATIVE_QTY,
				PACKAGE_MODEL,
				STATUS,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
			) VALUES (
				@FID,
				@PART_BOX_FID,
				@PART_BOX_CODE,
				@PLANT,
				@PLANT_ZONE,
				@WORKSHOP,
				@ASSEMBLY_LINE,
				@SUPPLIER_NUM,
				@ACCUMULATIVE_TYPE,
				@WORKSHOP_SECTION,
				@LOCATION,
				@CURRENT_QTY,
				@ACCUMULATIVE_QTY,
				@PACKAGE_MODEL,
				@STATUS,
				@COMMENTS,
				@VALID_FLAG,
				GETDATE(),
				@CREATE_USER,
				@MODIFY_DATE,
				@MODIFY_USER				 
			);SELECT @@IDENTITY;";
		private const string TT_MPM_JIS_COUNTER_UPDATE =
			@"UPDATE [LES].[TT_MPM_JIS_COUNTER] WITH(ROWLOCK) 
				SET FID=@FID,
				PART_BOX_FID=@PART_BOX_FID,
				PART_BOX_CODE=@PART_BOX_CODE,
				PLANT=@PLANT,
				PLANT_ZONE=@PLANT_ZONE,
				WORKSHOP=@WORKSHOP,
				ASSEMBLY_LINE=@ASSEMBLY_LINE,
				SUPPLIER_NUM=@SUPPLIER_NUM,
				ACCUMULATIVE_TYPE=@ACCUMULATIVE_TYPE,
				WORKSHOP_SECTION=@WORKSHOP_SECTION,
				LOCATION=@LOCATION,
				CURRENT_QTY=@CURRENT_QTY,
				ACCUMULATIVE_QTY=@ACCUMULATIVE_QTY,
				PACKAGE_MODEL=@PACKAGE_MODEL,
				STATUS=@STATUS,
				COMMENTS=@COMMENTS,
				VALID_FLAG=@VALID_FLAG,
				CREATE_DATE=@CREATE_DATE,
				CREATE_USER=@CREATE_USER,
				MODIFY_DATE=@MODIFY_DATE,
				MODIFY_USER=@MODIFY_USER				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TT_MPM_JIS_COUNTER_DELETE =
			@"DELETE FROM [LES].[TT_MPM_JIS_COUNTER] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get JisCounterInfo
		/// </summary>
		/// <param name="ID">JisCounterInfo Primary key </param>
		/// <returns></returns> 
		public JisCounterInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_MPM_JIS_COUNTER_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateJisCounterInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>JisCounterInfo Collection </returns>
		public List<JisCounterInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TT_MPM_JIS_COUNTER_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>JisCounterInfo Collection </returns>
		public List<JisCounterInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<JisCounterInfo> list = new List<JisCounterInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateJisCounterInfo(dr));
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
		public List<JisCounterInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TT_MPM_JIS_COUNTER]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<JisCounterInfo> list = new List<JisCounterInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateJisCounterInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TT_MPM_JIS_COUNTER_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(JisCounterInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TT_MPM_JIS_COUNTER_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@PART_BOX_FID", DbType.Guid, info.PartBoxFid);
			db.AddInParameter(dbCommand, "@PART_BOX_CODE", DbType.String, info.PartBoxCode);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@PLANT_ZONE", DbType.String, info.PlantZone);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@ACCUMULATIVE_TYPE", DbType.Int32, info.AccumulativeType);
			db.AddInParameter(dbCommand, "@WORKSHOP_SECTION", DbType.String, info.WorkshopSection);
			db.AddInParameter(dbCommand, "@LOCATION", DbType.String, info.Location);
			db.AddInParameter(dbCommand, "@CURRENT_QTY", DbType.Decimal, info.CurrentQty);
			db.AddInParameter(dbCommand, "@ACCUMULATIVE_QTY", DbType.Decimal, info.AccumulativeQty);
			db.AddInParameter(dbCommand, "@PACKAGE_MODEL", DbType.String, info.PackageModel);
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
		public static string GetInsertSql(JisCounterInfo info)
		{
			return  
			@"insert into [LES].[TT_MPM_JIS_COUNTER] (
				FID,
				PART_BOX_FID,
				PART_BOX_CODE,
				PLANT,
				PLANT_ZONE,
				WORKSHOP,
				ASSEMBLY_LINE,
				SUPPLIER_NUM,
				ACCUMULATIVE_TYPE,
				WORKSHOP_SECTION,
				LOCATION,
				CURRENT_QTY,
				ACCUMULATIVE_QTY,
				PACKAGE_MODEL,
				STATUS,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
			) values ("+
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				(info.PartBoxFid == null ? "NULL" : "N'" + info.PartBoxFid.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.PartBoxCode) ? "NULL" : "N'" + info.PartBoxCode + "'") + ","+
				(string.IsNullOrEmpty(info.Plant) ? "NULL" : "N'" + info.Plant + "'") + ","+
				(string.IsNullOrEmpty(info.PlantZone) ? "NULL" : "N'" + info.PlantZone + "'") + ","+
				(string.IsNullOrEmpty(info.Workshop) ? "NULL" : "N'" + info.Workshop + "'") + ","+
				(string.IsNullOrEmpty(info.AssemblyLine) ? "NULL" : "N'" + info.AssemblyLine + "'") + ","+
				(string.IsNullOrEmpty(info.SupplierNum) ? "NULL" : "N'" + info.SupplierNum + "'") + ","+
				(info.AccumulativeType == null ? "NULL" : "" + info.AccumulativeType.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.WorkshopSection) ? "NULL" : "N'" + info.WorkshopSection + "'") + ","+
				(string.IsNullOrEmpty(info.Location) ? "NULL" : "N'" + info.Location + "'") + ","+
				(info.CurrentQty == null ? "NULL" : "" + info.CurrentQty.GetValueOrDefault() + "") + ","+
				(info.AccumulativeQty == null ? "NULL" : "" + info.AccumulativeQty.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.PackageModel) ? "NULL" : "N'" + info.PackageModel + "'") + ","+
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
		public int Update(JisCounterInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_MPM_JIS_COUNTER_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@PART_BOX_FID", DbType.Guid, info.PartBoxFid);
			db.AddInParameter(dbCommand, "@PART_BOX_CODE", DbType.String, info.PartBoxCode);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@PLANT_ZONE", DbType.String, info.PlantZone);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@ACCUMULATIVE_TYPE", DbType.Int32, info.AccumulativeType);
			db.AddInParameter(dbCommand, "@WORKSHOP_SECTION", DbType.String, info.WorkshopSection);
			db.AddInParameter(dbCommand, "@LOCATION", DbType.String, info.Location);
			db.AddInParameter(dbCommand, "@CURRENT_QTY", DbType.Decimal, info.CurrentQty);
			db.AddInParameter(dbCommand, "@ACCUMULATIVE_QTY", DbType.Decimal, info.AccumulativeQty);
			db.AddInParameter(dbCommand, "@PACKAGE_MODEL", DbType.String, info.PackageModel);
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
		/// <param name="ID">JisCounterInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_MPM_JIS_COUNTER_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">JisCounterInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TT_MPM_JIS_COUNTER] WITH(ROWLOCK) "
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
		/// <param name="ID">JisCounterInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TT_MPM_JIS_COUNTER] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static JisCounterInfo CreateJisCounterInfo(IDataReader rdr)
		{
			JisCounterInfo info = new JisCounterInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.PartBoxFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("PART_BOX_FID"));			
			info.PartBoxCode = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_BOX_CODE"));			
			info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));			
			info.PlantZone = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT_ZONE"));			
			info.Workshop = DBConvert.GetString(rdr, rdr.GetOrdinal("WORKSHOP"));			
			info.AssemblyLine = DBConvert.GetString(rdr, rdr.GetOrdinal("ASSEMBLY_LINE"));			
			info.SupplierNum = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NUM"));			
			info.AccumulativeType = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("ACCUMULATIVE_TYPE"));			
			info.WorkshopSection = DBConvert.GetString(rdr, rdr.GetOrdinal("WORKSHOP_SECTION"));			
			info.Location = DBConvert.GetString(rdr, rdr.GetOrdinal("LOCATION"));			
			info.CurrentQty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("CURRENT_QTY"));			
			info.AccumulativeQty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("ACCUMULATIVE_QTY"));			
			info.PackageModel = DBConvert.GetString(rdr, rdr.GetOrdinal("PACKAGE_MODEL"));			
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