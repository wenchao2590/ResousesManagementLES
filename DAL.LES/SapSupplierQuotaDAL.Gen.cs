#region Declaim
//---------------------------------------------------------------------------
// Name:		SapSupplierQuotaDAL
// Function: 	Expose data in table TI_IFM_SAP_SUPPLIER_QUOTA from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年5月9日
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
    /// SapSupplierQuotaDAL对应表[TI_IFM_SAP_SUPPLIER_QUOTA]
    /// </summary>
    public partial class SapSupplierQuotaDAL : BusinessObjectProvider<SapSupplierQuotaInfo>
	{
		#region Sql Statements
		private const string TI_IFM_SAP_SUPPLIER_QUOTA_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				LOG_FID,
				PART_NO,
				WERKS,
				QTYPE,
				I_DATE,
				E_DATE,
				ZNRMM,
				QUPOS,
				LIFNR,
				SUPPLIER_NAME,
				QUOTE,
				ZSTOP,
				FLAG,
				PROCESS_FLAG,
				PROCESS_TIME,
				COMMENTS,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE				  
				FROM [LES].[TI_IFM_SAP_SUPPLIER_QUOTA] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TI_IFM_SAP_SUPPLIER_QUOTA_SELECT = 
			@"SELECT ID,
				FID,
				LOG_FID,
				PART_NO,
				WERKS,
				QTYPE,
				I_DATE,
				E_DATE,
				ZNRMM,
				QUPOS,
				LIFNR,
				SUPPLIER_NAME,
				QUOTE,
				ZSTOP,
				FLAG,
				PROCESS_FLAG,
				PROCESS_TIME,
				COMMENTS,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE				 
				FROM [LES].[TI_IFM_SAP_SUPPLIER_QUOTA] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TI_IFM_SAP_SUPPLIER_QUOTA_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TI_IFM_SAP_SUPPLIER_QUOTA]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TI_IFM_SAP_SUPPLIER_QUOTA_INSERT =
			@"INSERT INTO [LES].[TI_IFM_SAP_SUPPLIER_QUOTA] (
				FID,
				LOG_FID,
				PART_NO,
				WERKS,
				QTYPE,
				I_DATE,
				E_DATE,
				ZNRMM,
				QUPOS,
				LIFNR,
				SUPPLIER_NAME,
				QUOTE,
				ZSTOP,
				FLAG,
				PROCESS_FLAG,
				PROCESS_TIME,
				COMMENTS,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE				 
			) VALUES (
				@FID,
				@LOG_FID,
				@PART_NO,
				@WERKS,
				@QTYPE,
				@I_DATE,
				@E_DATE,
				@ZNRMM,
				@QUPOS,
				@LIFNR,
				@SUPPLIER_NAME,
				@QUOTE,
				@ZSTOP,
				@FLAG,
				@PROCESS_FLAG,
				@PROCESS_TIME,
				@COMMENTS,
				@VALID_FLAG,
				@CREATE_USER,
				GETDATE(),
				@MODIFY_USER,
				@MODIFY_DATE				 
			);SELECT @@IDENTITY;";
		private const string TI_IFM_SAP_SUPPLIER_QUOTA_UPDATE =
			@"UPDATE [LES].[TI_IFM_SAP_SUPPLIER_QUOTA] WITH(ROWLOCK) 
				SET FID=@FID,
				LOG_FID=@LOG_FID,
				PART_NO=@PART_NO,
				WERKS=@WERKS,
				QTYPE=@QTYPE,
				I_DATE=@I_DATE,
				E_DATE=@E_DATE,
				ZNRMM=@ZNRMM,
				QUPOS=@QUPOS,
				LIFNR=@LIFNR,
				SUPPLIER_NAME=@SUPPLIER_NAME,
				QUOTE=@QUOTE,
				ZSTOP=@ZSTOP,
				FLAG=@FLAG,
				PROCESS_FLAG=@PROCESS_FLAG,
				PROCESS_TIME=@PROCESS_TIME,
				COMMENTS=@COMMENTS,
				VALID_FLAG=@VALID_FLAG,
				CREATE_USER=@CREATE_USER,
				CREATE_DATE=@CREATE_DATE,
				MODIFY_USER=@MODIFY_USER,
				MODIFY_DATE=@MODIFY_DATE				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TI_IFM_SAP_SUPPLIER_QUOTA_DELETE =
			@"DELETE FROM [LES].[TI_IFM_SAP_SUPPLIER_QUOTA] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get SapSupplierQuotaInfo
		/// </summary>
		/// <param name="ID">SapSupplierQuotaInfo Primary key </param>
		/// <returns></returns> 
		public SapSupplierQuotaInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SAP_SUPPLIER_QUOTA_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateSapSupplierQuotaInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>SapSupplierQuotaInfo Collection </returns>
		public List<SapSupplierQuotaInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TI_IFM_SAP_SUPPLIER_QUOTA_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>SapSupplierQuotaInfo Collection </returns>
		public List<SapSupplierQuotaInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<SapSupplierQuotaInfo> list = new List<SapSupplierQuotaInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateSapSupplierQuotaInfo(dr));
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
		public List<SapSupplierQuotaInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TI_IFM_SAP_SUPPLIER_QUOTA]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<SapSupplierQuotaInfo> list = new List<SapSupplierQuotaInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateSapSupplierQuotaInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TI_IFM_SAP_SUPPLIER_QUOTA_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(SapSupplierQuotaInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SAP_SUPPLIER_QUOTA_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@LOG_FID", DbType.Guid, info.LogFid);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@WERKS", DbType.String, info.Werks);
			db.AddInParameter(dbCommand, "@QTYPE", DbType.String, info.Qtype);
			db.AddInParameter(dbCommand, "@I_DATE", DbType.DateTime, info.IDate);
			db.AddInParameter(dbCommand, "@E_DATE", DbType.DateTime, info.EDate);
			db.AddInParameter(dbCommand, "@ZNRMM", DbType.String, info.Znrmm);
			db.AddInParameter(dbCommand, "@QUPOS", DbType.Int32, info.Qupos);
			db.AddInParameter(dbCommand, "@LIFNR", DbType.String, info.Lifnr);
			db.AddInParameter(dbCommand, "@SUPPLIER_NAME", DbType.String, info.SupplierName);
			db.AddInParameter(dbCommand, "@QUOTE", DbType.Int32, info.Quote);
			db.AddInParameter(dbCommand, "@ZSTOP", DbType.String, info.Zstop);
			db.AddInParameter(dbCommand, "@FLAG", DbType.String, info.Flag);
			db.AddInParameter(dbCommand, "@PROCESS_FLAG", DbType.Int32, info.ProcessFlag);
			db.AddInParameter(dbCommand, "@PROCESS_TIME", DbType.DateTime, info.ProcessTime);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}		
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(SapSupplierQuotaInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SAP_SUPPLIER_QUOTA_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@LOG_FID", DbType.Guid, info.LogFid);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@WERKS", DbType.String, info.Werks);
			db.AddInParameter(dbCommand, "@QTYPE", DbType.String, info.Qtype);
			db.AddInParameter(dbCommand, "@I_DATE", DbType.DateTime, info.IDate);
			db.AddInParameter(dbCommand, "@E_DATE", DbType.DateTime, info.EDate);
			db.AddInParameter(dbCommand, "@ZNRMM", DbType.String, info.Znrmm);
			db.AddInParameter(dbCommand, "@QUPOS", DbType.Int32, info.Qupos);
			db.AddInParameter(dbCommand, "@LIFNR", DbType.String, info.Lifnr);
			db.AddInParameter(dbCommand, "@SUPPLIER_NAME", DbType.String, info.SupplierName);
			db.AddInParameter(dbCommand, "@QUOTE", DbType.Int32, info.Quote);
			db.AddInParameter(dbCommand, "@ZSTOP", DbType.String, info.Zstop);
			db.AddInParameter(dbCommand, "@FLAG", DbType.String, info.Flag);
			db.AddInParameter(dbCommand, "@PROCESS_FLAG", DbType.Int32, info.ProcessFlag);
			db.AddInParameter(dbCommand, "@PROCESS_TIME", DbType.DateTime, info.ProcessTime);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="ID">SapSupplierQuotaInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SAP_SUPPLIER_QUOTA_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">SapSupplierQuotaInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TI_IFM_SAP_SUPPLIER_QUOTA] WITH(ROWLOCK) "
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
		/// <param name="ID">SapSupplierQuotaInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TI_IFM_SAP_SUPPLIER_QUOTA] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static SapSupplierQuotaInfo CreateSapSupplierQuotaInfo(IDataReader rdr)
		{
			SapSupplierQuotaInfo info = new SapSupplierQuotaInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuid(rdr, rdr.GetOrdinal("FID"));			
			info.LogFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("LOG_FID"));			
			info.PartNo = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_NO"));			
			info.Werks = DBConvert.GetString(rdr, rdr.GetOrdinal("WERKS"));			
			info.Qtype = DBConvert.GetString(rdr, rdr.GetOrdinal("QTYPE"));			
			info.IDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("I_DATE"));			
			info.EDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("E_DATE"));			
			info.Znrmm = DBConvert.GetString(rdr, rdr.GetOrdinal("ZNRMM"));			
			info.Qupos = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("QUPOS"));			
			info.Lifnr = DBConvert.GetString(rdr, rdr.GetOrdinal("LIFNR"));			
			info.SupplierName = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NAME"));			
			info.Quote = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("QUOTE"));			
			info.Zstop = DBConvert.GetString(rdr, rdr.GetOrdinal("ZSTOP"));			
			info.Flag = DBConvert.GetString(rdr, rdr.GetOrdinal("FLAG"));			
			info.ProcessFlag = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PROCESS_FLAG"));			
			info.ProcessTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("PROCESS_TIME"));			
			info.Comments = DBConvert.GetString(rdr, rdr.GetOrdinal("COMMENTS"));			
			info.ValidFlag = DBConvert.GetBool(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.CreateDate = DBConvert.GetDateTime(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			return info;
		}
		
		#endregion
	}
}
