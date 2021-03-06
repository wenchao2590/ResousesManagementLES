#region Declaim
//---------------------------------------------------------------------------
// Name:		SrmPullingOrderDetailDAL
// Function: 	Expose data in table TI_IFM_SRM_PULLING_ORDER_DETAIL from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月24日
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
    /// SrmPullingOrderDetailDAL对应表[TI_IFM_SRM_PULLING_ORDER_DETAIL]
    /// </summary>
    public partial class SrmPullingOrderDetailDAL : BusinessObjectProvider<SrmPullingOrderDetailInfo>
	{
		#region Sql Statements
		private const string TI_IFM_SRM_PULLING_ORDER_DETAIL_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				ORDER_FID,
				TARGET_SLCODE,
				PART_NO,
				PART_CNAME,
				SNP,
				PART_QTY,
				PACKAGE_MODEL,
				REMARK,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				COMMENTS				  
				FROM [LES].[TI_IFM_SRM_PULLING_ORDER_DETAIL] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TI_IFM_SRM_PULLING_ORDER_DETAIL_SELECT = 
			@"SELECT ID,
				FID,
				ORDER_FID,
				TARGET_SLCODE,
				PART_NO,
				PART_CNAME,
				SNP,
				PART_QTY,
				PACKAGE_MODEL,
				REMARK,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				COMMENTS				 
				FROM [LES].[TI_IFM_SRM_PULLING_ORDER_DETAIL] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TI_IFM_SRM_PULLING_ORDER_DETAIL_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TI_IFM_SRM_PULLING_ORDER_DETAIL]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TI_IFM_SRM_PULLING_ORDER_DETAIL_INSERT =
			@"INSERT INTO [LES].[TI_IFM_SRM_PULLING_ORDER_DETAIL] (
				FID,
				ORDER_FID,
				TARGET_SLCODE,
				PART_NO,
				PART_CNAME,
				SNP,
				PART_QTY,
				PACKAGE_MODEL,
				REMARK,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				COMMENTS				 
			) VALUES (
				@FID,
				@ORDER_FID,
				@TARGET_SLCODE,
				@PART_NO,
				@PART_CNAME,
				@SNP,
				@PART_QTY,
				@PACKAGE_MODEL,
				@REMARK,
				@VALID_FLAG,
				@CREATE_USER,
				GETDATE(),
				@MODIFY_USER,
				@MODIFY_DATE,
				@COMMENTS				 
			);SELECT @@IDENTITY;";
		private const string TI_IFM_SRM_PULLING_ORDER_DETAIL_UPDATE =
			@"UPDATE [LES].[TI_IFM_SRM_PULLING_ORDER_DETAIL] WITH(ROWLOCK) 
				SET FID=@FID,
				ORDER_FID=@ORDER_FID,
				TARGET_SLCODE=@TARGET_SLCODE,
				PART_NO=@PART_NO,
				PART_CNAME=@PART_CNAME,
				SNP=@SNP,
				PART_QTY=@PART_QTY,
				PACKAGE_MODEL=@PACKAGE_MODEL,
				REMARK=@REMARK,
				VALID_FLAG=@VALID_FLAG,
				CREATE_USER=@CREATE_USER,
				CREATE_DATE=@CREATE_DATE,
				MODIFY_USER=@MODIFY_USER,
				MODIFY_DATE=@MODIFY_DATE,
				COMMENTS=@COMMENTS				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TI_IFM_SRM_PULLING_ORDER_DETAIL_DELETE =
			@"DELETE FROM [LES].[TI_IFM_SRM_PULLING_ORDER_DETAIL] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get SrmPullingOrderDetailInfo
		/// </summary>
		/// <param name="ID">SrmPullingOrderDetailInfo Primary key </param>
		/// <returns></returns> 
		public SrmPullingOrderDetailInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SRM_PULLING_ORDER_DETAIL_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateSrmPullingOrderDetailInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>SrmPullingOrderDetailInfo Collection </returns>
		public List<SrmPullingOrderDetailInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TI_IFM_SRM_PULLING_ORDER_DETAIL_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>SrmPullingOrderDetailInfo Collection </returns>
		public List<SrmPullingOrderDetailInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<SrmPullingOrderDetailInfo> list = new List<SrmPullingOrderDetailInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateSrmPullingOrderDetailInfo(dr));
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
		public List<SrmPullingOrderDetailInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TI_IFM_SRM_PULLING_ORDER_DETAIL]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<SrmPullingOrderDetailInfo> list = new List<SrmPullingOrderDetailInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateSrmPullingOrderDetailInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TI_IFM_SRM_PULLING_ORDER_DETAIL_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(SrmPullingOrderDetailInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SRM_PULLING_ORDER_DETAIL_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@ORDER_FID", DbType.Guid, info.OrderFid);
			db.AddInParameter(dbCommand, "@TARGET_SLCODE", DbType.String, info.TargetSlcode);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@PART_CNAME", DbType.String, info.PartCname);
			db.AddInParameter(dbCommand, "@SNP", DbType.Decimal, info.Snp);
			db.AddInParameter(dbCommand, "@PART_QTY", DbType.Decimal, info.PartQty);
			db.AddInParameter(dbCommand, "@PACKAGE_MODEL", DbType.String, info.PackageModel);
			db.AddInParameter(dbCommand, "@REMARK", DbType.String, info.Remark);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}
		/// <summary>
		/// GetInsertSql
		/// </summary>
		/// <param name="info"> info</param>
		public static string GetInsertSql(SrmPullingOrderDetailInfo info)
		{
			return  
			@"insert into [LES].[TI_IFM_SRM_PULLING_ORDER_DETAIL] (
				FID,
				ORDER_FID,
				TARGET_SLCODE,
				PART_NO,
				PART_CNAME,
				SNP,
				PART_QTY,
				PACKAGE_MODEL,
				REMARK,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				COMMENTS				 
			) values ("+
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				(info.OrderFid == null ? "NULL" : "N'" + info.OrderFid.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.TargetSlcode) ? "NULL" : "N'" + info.TargetSlcode + "'") + ","+
				(string.IsNullOrEmpty(info.PartNo) ? "NULL" : "N'" + info.PartNo + "'") + ","+
				(string.IsNullOrEmpty(info.PartCname) ? "NULL" : "N'" + info.PartCname + "'") + ","+
				(info.Snp == null ? "NULL" : "" + info.Snp.GetValueOrDefault() + "") + ","+
				(info.PartQty == null ? "NULL" : "" + info.PartQty.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.PackageModel) ? "NULL" : "N'" + info.PackageModel + "'") + ","+
				(string.IsNullOrEmpty(info.Remark) ? "NULL" : "N'" + info.Remark + "'") + ","+
				"1" + ","+		
				"N'" + info.CreateUser + "'" + ","+		
				"GETDATE()" + ","+			
				"NULL" + ","+			
				"NULL" + ","+			
				(string.IsNullOrEmpty(info.Comments) ? "NULL" : "N'" + info.Comments + "'") + ");";				}
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(SrmPullingOrderDetailInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SRM_PULLING_ORDER_DETAIL_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@ORDER_FID", DbType.Guid, info.OrderFid);
			db.AddInParameter(dbCommand, "@TARGET_SLCODE", DbType.String, info.TargetSlcode);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@PART_CNAME", DbType.String, info.PartCname);
			db.AddInParameter(dbCommand, "@SNP", DbType.Decimal, info.Snp);
			db.AddInParameter(dbCommand, "@PART_QTY", DbType.Decimal, info.PartQty);
			db.AddInParameter(dbCommand, "@PACKAGE_MODEL", DbType.String, info.PackageModel);
			db.AddInParameter(dbCommand, "@REMARK", DbType.String, info.Remark);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="ID">SrmPullingOrderDetailInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SRM_PULLING_ORDER_DETAIL_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">SrmPullingOrderDetailInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TI_IFM_SRM_PULLING_ORDER_DETAIL] WITH(ROWLOCK) "
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
		/// <param name="ID">SrmPullingOrderDetailInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TI_IFM_SRM_PULLING_ORDER_DETAIL] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static SrmPullingOrderDetailInfo CreateSrmPullingOrderDetailInfo(IDataReader rdr)
		{
			SrmPullingOrderDetailInfo info = new SrmPullingOrderDetailInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.OrderFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("ORDER_FID"));			
			info.TargetSlcode = DBConvert.GetString(rdr, rdr.GetOrdinal("TARGET_SLCODE"));			
			info.PartNo = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_NO"));			
			info.PartCname = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_CNAME"));			
			info.Snp = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("SNP"));			
			info.PartQty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("PART_QTY"));			
			info.PackageModel = DBConvert.GetString(rdr, rdr.GetOrdinal("PACKAGE_MODEL"));			
			info.Remark = DBConvert.GetString(rdr, rdr.GetOrdinal("REMARK"));			
			info.ValidFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.CreateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			info.Comments = DBConvert.GetString(rdr, rdr.GetOrdinal("COMMENTS"));			
			return info;
		}
		
		#endregion
	}
}
