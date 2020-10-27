#region Declaim
//---------------------------------------------------------------------------
// Name:		JisPullOrderDetailDAL
// Function: 	Expose data in table TT_MPM_JIS_PULL_ORDER_DETAIL from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月10日
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
    /// JisPullOrderDetailDAL对应表[TT_MPM_JIS_PULL_ORDER_DETAIL]
    /// </summary>
    public partial class JisPullOrderDetailDAL : BusinessObjectProvider<JisPullOrderDetailInfo>
	{
		#region Sql Statements
		private const string TT_MPM_JIS_PULL_ORDER_DETAIL_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				ORDER_FID,
				ORDER_CODE,
				ROW_NO,
				SUPPLIER_NUM,
				PART_NO,
				PART_VERSION,
				PART_CNAME,
				PART_ENAME,
				MEASURING_UNIT_NO,
				REQUIRED_PART_QTY,
				ACTUAL_PART_QTY,
				VEHICHE_MODEL_NO,
				DAY_VEHICHE_SEQ_NO,
				PRODUCTION_NO,
				INSPECTION_MODE,
				INSPECTION_FLAG,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				  
				FROM [LES].[TT_MPM_JIS_PULL_ORDER_DETAIL] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TT_MPM_JIS_PULL_ORDER_DETAIL_SELECT = 
			@"SELECT ID,
				FID,
				ORDER_FID,
				ORDER_CODE,
				ROW_NO,
				SUPPLIER_NUM,
				PART_NO,
				PART_VERSION,
				PART_CNAME,
				PART_ENAME,
				MEASURING_UNIT_NO,
				REQUIRED_PART_QTY,
				ACTUAL_PART_QTY,
				VEHICHE_MODEL_NO,
				DAY_VEHICHE_SEQ_NO,
				PRODUCTION_NO,
				INSPECTION_MODE,
				INSPECTION_FLAG,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
				FROM [LES].[TT_MPM_JIS_PULL_ORDER_DETAIL] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TT_MPM_JIS_PULL_ORDER_DETAIL_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TT_MPM_JIS_PULL_ORDER_DETAIL]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TT_MPM_JIS_PULL_ORDER_DETAIL_INSERT =
			@"INSERT INTO [LES].[TT_MPM_JIS_PULL_ORDER_DETAIL] (
				FID,
				ORDER_FID,
				ORDER_CODE,
				ROW_NO,
				SUPPLIER_NUM,
				PART_NO,
				PART_VERSION,
				PART_CNAME,
				PART_ENAME,
				MEASURING_UNIT_NO,
				REQUIRED_PART_QTY,
				ACTUAL_PART_QTY,
				VEHICHE_MODEL_NO,
				DAY_VEHICHE_SEQ_NO,
				PRODUCTION_NO,
				INSPECTION_MODE,
				INSPECTION_FLAG,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
			) VALUES (
				@FID,
				@ORDER_FID,
				@ORDER_CODE,
				@ROW_NO,
				@SUPPLIER_NUM,
				@PART_NO,
				@PART_VERSION,
				@PART_CNAME,
				@PART_ENAME,
				@MEASURING_UNIT_NO,
				@REQUIRED_PART_QTY,
				@ACTUAL_PART_QTY,
				@VEHICHE_MODEL_NO,
				@DAY_VEHICHE_SEQ_NO,
				@PRODUCTION_NO,
				@INSPECTION_MODE,
				@INSPECTION_FLAG,
				@COMMENTS,
				@VALID_FLAG,
				GETDATE(),
				@CREATE_USER,
				@MODIFY_DATE,
				@MODIFY_USER				 
			);SELECT @@IDENTITY;";
		private const string TT_MPM_JIS_PULL_ORDER_DETAIL_UPDATE =
			@"UPDATE [LES].[TT_MPM_JIS_PULL_ORDER_DETAIL] WITH(ROWLOCK) 
				SET FID=@FID,
				ORDER_FID=@ORDER_FID,
				ORDER_CODE=@ORDER_CODE,
				ROW_NO=@ROW_NO,
				SUPPLIER_NUM=@SUPPLIER_NUM,
				PART_NO=@PART_NO,
				PART_VERSION=@PART_VERSION,
				PART_CNAME=@PART_CNAME,
				PART_ENAME=@PART_ENAME,
				MEASURING_UNIT_NO=@MEASURING_UNIT_NO,
				REQUIRED_PART_QTY=@REQUIRED_PART_QTY,
				ACTUAL_PART_QTY=@ACTUAL_PART_QTY,
				VEHICHE_MODEL_NO=@VEHICHE_MODEL_NO,
				DAY_VEHICHE_SEQ_NO=@DAY_VEHICHE_SEQ_NO,
				PRODUCTION_NO=@PRODUCTION_NO,
				INSPECTION_MODE=@INSPECTION_MODE,
				INSPECTION_FLAG=@INSPECTION_FLAG,
				COMMENTS=@COMMENTS,
				VALID_FLAG=@VALID_FLAG,
				CREATE_DATE=@CREATE_DATE,
				CREATE_USER=@CREATE_USER,
				MODIFY_DATE=@MODIFY_DATE,
				MODIFY_USER=@MODIFY_USER				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TT_MPM_JIS_PULL_ORDER_DETAIL_DELETE =
			@"DELETE FROM [LES].[TT_MPM_JIS_PULL_ORDER_DETAIL] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get JisPullOrderDetailInfo
		/// </summary>
		/// <param name="ID">JisPullOrderDetailInfo Primary key </param>
		/// <returns></returns> 
		public JisPullOrderDetailInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_MPM_JIS_PULL_ORDER_DETAIL_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateJisPullOrderDetailInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>JisPullOrderDetailInfo Collection </returns>
		public List<JisPullOrderDetailInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TT_MPM_JIS_PULL_ORDER_DETAIL_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>JisPullOrderDetailInfo Collection </returns>
		public List<JisPullOrderDetailInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<JisPullOrderDetailInfo> list = new List<JisPullOrderDetailInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateJisPullOrderDetailInfo(dr));
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
		public List<JisPullOrderDetailInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TT_MPM_JIS_PULL_ORDER_DETAIL]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<JisPullOrderDetailInfo> list = new List<JisPullOrderDetailInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateJisPullOrderDetailInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TT_MPM_JIS_PULL_ORDER_DETAIL_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(JisPullOrderDetailInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TT_MPM_JIS_PULL_ORDER_DETAIL_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@ORDER_FID", DbType.Guid, info.OrderFid);
			db.AddInParameter(dbCommand, "@ORDER_CODE", DbType.String, info.OrderCode);
			db.AddInParameter(dbCommand, "@ROW_NO", DbType.Int32, info.RowNo);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@PART_VERSION", DbType.String, info.PartVersion);
			db.AddInParameter(dbCommand, "@PART_CNAME", DbType.String, info.PartCname);
			db.AddInParameter(dbCommand, "@PART_ENAME", DbType.String, info.PartEname);
			db.AddInParameter(dbCommand, "@MEASURING_UNIT_NO", DbType.String, info.MeasuringUnitNo);
			db.AddInParameter(dbCommand, "@REQUIRED_PART_QTY", DbType.Decimal, info.RequiredPartQty);
			db.AddInParameter(dbCommand, "@ACTUAL_PART_QTY", DbType.Decimal, info.ActualPartQty);
			db.AddInParameter(dbCommand, "@VEHICHE_MODEL_NO", DbType.String, info.VehicheModelNo);
			db.AddInParameter(dbCommand, "@DAY_VEHICHE_SEQ_NO", DbType.Int32, info.DayVehicheSeqNo);
			db.AddInParameter(dbCommand, "@PRODUCTION_NO", DbType.String, info.ProductionNo);
			db.AddInParameter(dbCommand, "@INSPECTION_MODE", DbType.Int32, info.InspectionMode);
			db.AddInParameter(dbCommand, "@INSPECTION_FLAG", DbType.Boolean, info.InspectionFlag);
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
		public static string GetInsertSql(JisPullOrderDetailInfo info)
		{
			return  
			@"insert into [LES].[TT_MPM_JIS_PULL_ORDER_DETAIL] (
				FID,
				ORDER_FID,
				ORDER_CODE,
				ROW_NO,
				SUPPLIER_NUM,
				PART_NO,
				PART_VERSION,
				PART_CNAME,
				PART_ENAME,
				MEASURING_UNIT_NO,
				REQUIRED_PART_QTY,
				ACTUAL_PART_QTY,
				VEHICHE_MODEL_NO,
				DAY_VEHICHE_SEQ_NO,
				PRODUCTION_NO,
				INSPECTION_MODE,
				INSPECTION_FLAG,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
			) values ("+
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				(info.OrderFid == null ? "NULL" : "N'" + info.OrderFid.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.OrderCode) ? "NULL" : "N'" + info.OrderCode + "'") + ","+
				(info.RowNo == null ? "NULL" : "" + info.RowNo.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.SupplierNum) ? "NULL" : "N'" + info.SupplierNum + "'") + ","+
				(string.IsNullOrEmpty(info.PartNo) ? "NULL" : "N'" + info.PartNo + "'") + ","+
				(string.IsNullOrEmpty(info.PartVersion) ? "NULL" : "N'" + info.PartVersion + "'") + ","+
				(string.IsNullOrEmpty(info.PartCname) ? "NULL" : "N'" + info.PartCname + "'") + ","+
				(string.IsNullOrEmpty(info.PartEname) ? "NULL" : "N'" + info.PartEname + "'") + ","+
				(string.IsNullOrEmpty(info.MeasuringUnitNo) ? "NULL" : "N'" + info.MeasuringUnitNo + "'") + ","+
				(info.RequiredPartQty == null ? "NULL" : "" + info.RequiredPartQty.GetValueOrDefault() + "") + ","+
				(info.ActualPartQty == null ? "NULL" : "" + info.ActualPartQty.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.VehicheModelNo) ? "NULL" : "N'" + info.VehicheModelNo + "'") + ","+
				(info.DayVehicheSeqNo == null ? "NULL" : "" + info.DayVehicheSeqNo.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.ProductionNo) ? "NULL" : "N'" + info.ProductionNo + "'") + ","+
				(info.InspectionMode == null ? "NULL" : "" + info.InspectionMode.GetValueOrDefault() + "") + ","+
				(info.InspectionFlag == null ? "NULL" : "" + (info.InspectionFlag.GetValueOrDefault() ? "1" : "0") + "") + ","+
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
		public int Update(JisPullOrderDetailInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_MPM_JIS_PULL_ORDER_DETAIL_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@ORDER_FID", DbType.Guid, info.OrderFid);
			db.AddInParameter(dbCommand, "@ORDER_CODE", DbType.String, info.OrderCode);
			db.AddInParameter(dbCommand, "@ROW_NO", DbType.Int32, info.RowNo);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@PART_VERSION", DbType.String, info.PartVersion);
			db.AddInParameter(dbCommand, "@PART_CNAME", DbType.String, info.PartCname);
			db.AddInParameter(dbCommand, "@PART_ENAME", DbType.String, info.PartEname);
			db.AddInParameter(dbCommand, "@MEASURING_UNIT_NO", DbType.String, info.MeasuringUnitNo);
			db.AddInParameter(dbCommand, "@REQUIRED_PART_QTY", DbType.Decimal, info.RequiredPartQty);
			db.AddInParameter(dbCommand, "@ACTUAL_PART_QTY", DbType.Decimal, info.ActualPartQty);
			db.AddInParameter(dbCommand, "@VEHICHE_MODEL_NO", DbType.String, info.VehicheModelNo);
			db.AddInParameter(dbCommand, "@DAY_VEHICHE_SEQ_NO", DbType.Int32, info.DayVehicheSeqNo);
			db.AddInParameter(dbCommand, "@PRODUCTION_NO", DbType.String, info.ProductionNo);
			db.AddInParameter(dbCommand, "@INSPECTION_MODE", DbType.Int32, info.InspectionMode);
			db.AddInParameter(dbCommand, "@INSPECTION_FLAG", DbType.Boolean, info.InspectionFlag);
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
		/// <param name="ID">JisPullOrderDetailInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_MPM_JIS_PULL_ORDER_DETAIL_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">JisPullOrderDetailInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TT_MPM_JIS_PULL_ORDER_DETAIL] WITH(ROWLOCK) "
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
		/// <param name="ID">JisPullOrderDetailInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TT_MPM_JIS_PULL_ORDER_DETAIL] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static JisPullOrderDetailInfo CreateJisPullOrderDetailInfo(IDataReader rdr)
		{
			JisPullOrderDetailInfo info = new JisPullOrderDetailInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.OrderFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("ORDER_FID"));			
			info.OrderCode = DBConvert.GetString(rdr, rdr.GetOrdinal("ORDER_CODE"));			
			info.RowNo = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("ROW_NO"));			
			info.SupplierNum = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NUM"));			
			info.PartNo = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_NO"));			
			info.PartVersion = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_VERSION"));			
			info.PartCname = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_CNAME"));			
			info.PartEname = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_ENAME"));			
			info.MeasuringUnitNo = DBConvert.GetString(rdr, rdr.GetOrdinal("MEASURING_UNIT_NO"));			
			info.RequiredPartQty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("REQUIRED_PART_QTY"));			
			info.ActualPartQty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("ACTUAL_PART_QTY"));			
			info.VehicheModelNo = DBConvert.GetString(rdr, rdr.GetOrdinal("VEHICHE_MODEL_NO"));			
			info.DayVehicheSeqNo = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("DAY_VEHICHE_SEQ_NO"));			
			info.ProductionNo = DBConvert.GetString(rdr, rdr.GetOrdinal("PRODUCTION_NO"));			
			info.InspectionMode = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("INSPECTION_MODE"));			
			info.InspectionFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("INSPECTION_FLAG"));			
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
