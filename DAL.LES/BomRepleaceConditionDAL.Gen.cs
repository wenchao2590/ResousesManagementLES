#region Declaim
//---------------------------------------------------------------------------
// Name:		BomRepleaceConditionDAL
// Function: 	Expose data in table TT_BPM_BOM_REPLEACE_CONDITION from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月17日
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
    /// BomRepleaceConditionDAL对应表[TT_BPM_BOM_REPLEACE_CONDITION]
    /// </summary>
    public partial class BomRepleaceConditionDAL : BusinessObjectProvider<BomRepleaceConditionInfo>
	{
		#region Sql Statements
		private const string TT_BPM_BOM_REPLEACE_CONDITION_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				CONDITION_CODE,
				OLD_PART_NO,
				NEW_PART_NO,
				OLD_SUPPLIER_NUM,
				NEW_SUPPLIER_NUM,
				OLD_LOCATION,
				NEW_LOCATION,
				OLD_PART_VERSION,
				NEW_PART_VERSION,
				OLD_PART_QTY,
				NEW_PART_QTY,
				START_PORDER_CODE,
				PORDER_START_TIME,
				PORDER_END_TIME,
				EXECUTE_START_TIME,
				EXECUTE_END_TIME,
				STATUS,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				  
				FROM [LES].[TT_BPM_BOM_REPLEACE_CONDITION] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TT_BPM_BOM_REPLEACE_CONDITION_SELECT = 
			@"SELECT ID,
				FID,
				CONDITION_CODE,
				OLD_PART_NO,
				NEW_PART_NO,
				OLD_SUPPLIER_NUM,
				NEW_SUPPLIER_NUM,
				OLD_LOCATION,
				NEW_LOCATION,
				OLD_PART_VERSION,
				NEW_PART_VERSION,
				OLD_PART_QTY,
				NEW_PART_QTY,
				START_PORDER_CODE,
				PORDER_START_TIME,
				PORDER_END_TIME,
				EXECUTE_START_TIME,
				EXECUTE_END_TIME,
				STATUS,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
				FROM [LES].[TT_BPM_BOM_REPLEACE_CONDITION] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TT_BPM_BOM_REPLEACE_CONDITION_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TT_BPM_BOM_REPLEACE_CONDITION]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TT_BPM_BOM_REPLEACE_CONDITION_INSERT =
			@"INSERT INTO [LES].[TT_BPM_BOM_REPLEACE_CONDITION] (
				FID,
				CONDITION_CODE,
				OLD_PART_NO,
				NEW_PART_NO,
				OLD_SUPPLIER_NUM,
				NEW_SUPPLIER_NUM,
				OLD_LOCATION,
				NEW_LOCATION,
				OLD_PART_VERSION,
				NEW_PART_VERSION,
				OLD_PART_QTY,
				NEW_PART_QTY,
				START_PORDER_CODE,
				PORDER_START_TIME,
				PORDER_END_TIME,
				EXECUTE_START_TIME,
				EXECUTE_END_TIME,
				STATUS,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
			) VALUES (
				@FID,
				@CONDITION_CODE,
				@OLD_PART_NO,
				@NEW_PART_NO,
				@OLD_SUPPLIER_NUM,
				@NEW_SUPPLIER_NUM,
				@OLD_LOCATION,
				@NEW_LOCATION,
				@OLD_PART_VERSION,
				@NEW_PART_VERSION,
				@OLD_PART_QTY,
				@NEW_PART_QTY,
				@START_PORDER_CODE,
				@PORDER_START_TIME,
				@PORDER_END_TIME,
				@EXECUTE_START_TIME,
				@EXECUTE_END_TIME,
				@STATUS,
				@COMMENTS,
				@VALID_FLAG,
				GETDATE(),
				@CREATE_USER,
				@MODIFY_DATE,
				@MODIFY_USER				 
			);SELECT @@IDENTITY;";
		private const string TT_BPM_BOM_REPLEACE_CONDITION_UPDATE =
			@"UPDATE [LES].[TT_BPM_BOM_REPLEACE_CONDITION] WITH(ROWLOCK) 
				SET FID=@FID,
				CONDITION_CODE=@CONDITION_CODE,
				OLD_PART_NO=@OLD_PART_NO,
				NEW_PART_NO=@NEW_PART_NO,
				OLD_SUPPLIER_NUM=@OLD_SUPPLIER_NUM,
				NEW_SUPPLIER_NUM=@NEW_SUPPLIER_NUM,
				OLD_LOCATION=@OLD_LOCATION,
				NEW_LOCATION=@NEW_LOCATION,
				OLD_PART_VERSION=@OLD_PART_VERSION,
				NEW_PART_VERSION=@NEW_PART_VERSION,
				OLD_PART_QTY=@OLD_PART_QTY,
				NEW_PART_QTY=@NEW_PART_QTY,
				START_PORDER_CODE=@START_PORDER_CODE,
				PORDER_START_TIME=@PORDER_START_TIME,
				PORDER_END_TIME=@PORDER_END_TIME,
				EXECUTE_START_TIME=@EXECUTE_START_TIME,
				EXECUTE_END_TIME=@EXECUTE_END_TIME,
				STATUS=@STATUS,
				COMMENTS=@COMMENTS,
				VALID_FLAG=@VALID_FLAG,
				CREATE_DATE=@CREATE_DATE,
				CREATE_USER=@CREATE_USER,
				MODIFY_DATE=@MODIFY_DATE,
				MODIFY_USER=@MODIFY_USER				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TT_BPM_BOM_REPLEACE_CONDITION_DELETE =
			@"DELETE FROM [LES].[TT_BPM_BOM_REPLEACE_CONDITION] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get BomRepleaceConditionInfo
		/// </summary>
		/// <param name="ID">BomRepleaceConditionInfo Primary key </param>
		/// <returns></returns> 
		public BomRepleaceConditionInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_BPM_BOM_REPLEACE_CONDITION_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateBomRepleaceConditionInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>BomRepleaceConditionInfo Collection </returns>
		public List<BomRepleaceConditionInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TT_BPM_BOM_REPLEACE_CONDITION_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>BomRepleaceConditionInfo Collection </returns>
		public List<BomRepleaceConditionInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<BomRepleaceConditionInfo> list = new List<BomRepleaceConditionInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateBomRepleaceConditionInfo(dr));
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
		public List<BomRepleaceConditionInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TT_BPM_BOM_REPLEACE_CONDITION]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<BomRepleaceConditionInfo> list = new List<BomRepleaceConditionInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateBomRepleaceConditionInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TT_BPM_BOM_REPLEACE_CONDITION_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(BomRepleaceConditionInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TT_BPM_BOM_REPLEACE_CONDITION_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@CONDITION_CODE", DbType.String, info.ConditionCode);
			db.AddInParameter(dbCommand, "@OLD_PART_NO", DbType.String, info.OldPartNo);
			db.AddInParameter(dbCommand, "@NEW_PART_NO", DbType.String, info.NewPartNo);
			db.AddInParameter(dbCommand, "@OLD_SUPPLIER_NUM", DbType.String, info.OldSupplierNum);
			db.AddInParameter(dbCommand, "@NEW_SUPPLIER_NUM", DbType.String, info.NewSupplierNum);
			db.AddInParameter(dbCommand, "@OLD_LOCATION", DbType.String, info.OldLocation);
			db.AddInParameter(dbCommand, "@NEW_LOCATION", DbType.String, info.NewLocation);
			db.AddInParameter(dbCommand, "@OLD_PART_VERSION", DbType.String, info.OldPartVersion);
			db.AddInParameter(dbCommand, "@NEW_PART_VERSION", DbType.String, info.NewPartVersion);
			db.AddInParameter(dbCommand, "@OLD_PART_QTY", DbType.Decimal, info.OldPartQty);
			db.AddInParameter(dbCommand, "@NEW_PART_QTY", DbType.Decimal, info.NewPartQty);
			db.AddInParameter(dbCommand, "@START_PORDER_CODE", DbType.String, info.StartPorderCode);
			db.AddInParameter(dbCommand, "@PORDER_START_TIME", DbType.DateTime, info.PorderStartTime);
			db.AddInParameter(dbCommand, "@PORDER_END_TIME", DbType.DateTime, info.PorderEndTime);
			db.AddInParameter(dbCommand, "@EXECUTE_START_TIME", DbType.DateTime, info.ExecuteStartTime);
			db.AddInParameter(dbCommand, "@EXECUTE_END_TIME", DbType.DateTime, info.ExecuteEndTime);
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
		public static string GetInsertSql(BomRepleaceConditionInfo info)
		{
			return  
			@"insert into [LES].[TT_BPM_BOM_REPLEACE_CONDITION] (
				FID,
				CONDITION_CODE,
				OLD_PART_NO,
				NEW_PART_NO,
				OLD_SUPPLIER_NUM,
				NEW_SUPPLIER_NUM,
				OLD_LOCATION,
				NEW_LOCATION,
				OLD_PART_VERSION,
				NEW_PART_VERSION,
				OLD_PART_QTY,
				NEW_PART_QTY,
				START_PORDER_CODE,
				PORDER_START_TIME,
				PORDER_END_TIME,
				EXECUTE_START_TIME,
				EXECUTE_END_TIME,
				STATUS,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
			) values ("+
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.ConditionCode) ? "NULL" : "N'" + info.ConditionCode + "'") + ","+
				(string.IsNullOrEmpty(info.OldPartNo) ? "NULL" : "N'" + info.OldPartNo + "'") + ","+
				(string.IsNullOrEmpty(info.NewPartNo) ? "NULL" : "N'" + info.NewPartNo + "'") + ","+
				(string.IsNullOrEmpty(info.OldSupplierNum) ? "NULL" : "N'" + info.OldSupplierNum + "'") + ","+
				(string.IsNullOrEmpty(info.NewSupplierNum) ? "NULL" : "N'" + info.NewSupplierNum + "'") + ","+
				(string.IsNullOrEmpty(info.OldLocation) ? "NULL" : "N'" + info.OldLocation + "'") + ","+
				(string.IsNullOrEmpty(info.NewLocation) ? "NULL" : "N'" + info.NewLocation + "'") + ","+
				(string.IsNullOrEmpty(info.OldPartVersion) ? "NULL" : "N'" + info.OldPartVersion + "'") + ","+
				(string.IsNullOrEmpty(info.NewPartVersion) ? "NULL" : "N'" + info.NewPartVersion + "'") + ","+
				(info.OldPartQty == null ? "NULL" : "" + info.OldPartQty.GetValueOrDefault() + "") + ","+
				(info.NewPartQty == null ? "NULL" : "" + info.NewPartQty.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.StartPorderCode) ? "NULL" : "N'" + info.StartPorderCode + "'") + ","+
				(info.PorderStartTime == null ? "NULL" : "N'" + info.PorderStartTime.GetValueOrDefault() + "'") + ","+
				(info.PorderEndTime == null ? "NULL" : "N'" + info.PorderEndTime.GetValueOrDefault() + "'") + ","+
				(info.ExecuteStartTime == null ? "NULL" : "N'" + info.ExecuteStartTime.GetValueOrDefault() + "'") + ","+
				(info.ExecuteEndTime == null ? "NULL" : "N'" + info.ExecuteEndTime.GetValueOrDefault() + "'") + ","+
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
		public int Update(BomRepleaceConditionInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_BPM_BOM_REPLEACE_CONDITION_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@CONDITION_CODE", DbType.String, info.ConditionCode);
			db.AddInParameter(dbCommand, "@OLD_PART_NO", DbType.String, info.OldPartNo);
			db.AddInParameter(dbCommand, "@NEW_PART_NO", DbType.String, info.NewPartNo);
			db.AddInParameter(dbCommand, "@OLD_SUPPLIER_NUM", DbType.String, info.OldSupplierNum);
			db.AddInParameter(dbCommand, "@NEW_SUPPLIER_NUM", DbType.String, info.NewSupplierNum);
			db.AddInParameter(dbCommand, "@OLD_LOCATION", DbType.String, info.OldLocation);
			db.AddInParameter(dbCommand, "@NEW_LOCATION", DbType.String, info.NewLocation);
			db.AddInParameter(dbCommand, "@OLD_PART_VERSION", DbType.String, info.OldPartVersion);
			db.AddInParameter(dbCommand, "@NEW_PART_VERSION", DbType.String, info.NewPartVersion);
			db.AddInParameter(dbCommand, "@OLD_PART_QTY", DbType.Decimal, info.OldPartQty);
			db.AddInParameter(dbCommand, "@NEW_PART_QTY", DbType.Decimal, info.NewPartQty);
			db.AddInParameter(dbCommand, "@START_PORDER_CODE", DbType.String, info.StartPorderCode);
			db.AddInParameter(dbCommand, "@PORDER_START_TIME", DbType.DateTime, info.PorderStartTime);
			db.AddInParameter(dbCommand, "@PORDER_END_TIME", DbType.DateTime, info.PorderEndTime);
			db.AddInParameter(dbCommand, "@EXECUTE_START_TIME", DbType.DateTime, info.ExecuteStartTime);
			db.AddInParameter(dbCommand, "@EXECUTE_END_TIME", DbType.DateTime, info.ExecuteEndTime);
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
		/// <param name="ID">BomRepleaceConditionInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_BPM_BOM_REPLEACE_CONDITION_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">BomRepleaceConditionInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TT_BPM_BOM_REPLEACE_CONDITION] WITH(ROWLOCK) "
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
		/// <param name="ID">BomRepleaceConditionInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TT_BPM_BOM_REPLEACE_CONDITION] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static BomRepleaceConditionInfo CreateBomRepleaceConditionInfo(IDataReader rdr)
		{
			BomRepleaceConditionInfo info = new BomRepleaceConditionInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.ConditionCode = DBConvert.GetString(rdr, rdr.GetOrdinal("CONDITION_CODE"));			
			info.OldPartNo = DBConvert.GetString(rdr, rdr.GetOrdinal("OLD_PART_NO"));			
			info.NewPartNo = DBConvert.GetString(rdr, rdr.GetOrdinal("NEW_PART_NO"));			
			info.OldSupplierNum = DBConvert.GetString(rdr, rdr.GetOrdinal("OLD_SUPPLIER_NUM"));			
			info.NewSupplierNum = DBConvert.GetString(rdr, rdr.GetOrdinal("NEW_SUPPLIER_NUM"));			
			info.OldLocation = DBConvert.GetString(rdr, rdr.GetOrdinal("OLD_LOCATION"));			
			info.NewLocation = DBConvert.GetString(rdr, rdr.GetOrdinal("NEW_LOCATION"));			
			info.OldPartVersion = DBConvert.GetString(rdr, rdr.GetOrdinal("OLD_PART_VERSION"));			
			info.NewPartVersion = DBConvert.GetString(rdr, rdr.GetOrdinal("NEW_PART_VERSION"));			
			info.OldPartQty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("OLD_PART_QTY"));			
			info.NewPartQty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("NEW_PART_QTY"));			
			info.StartPorderCode = DBConvert.GetString(rdr, rdr.GetOrdinal("START_PORDER_CODE"));			
			info.PorderStartTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("PORDER_START_TIME"));			
			info.PorderEndTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("PORDER_END_TIME"));			
			info.ExecuteStartTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("EXECUTE_START_TIME"));			
			info.ExecuteEndTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("EXECUTE_END_TIME"));			
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