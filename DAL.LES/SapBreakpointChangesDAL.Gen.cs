#region Declaim
//---------------------------------------------------------------------------
// Name:		SapBreakpointChangesDAL
// Function: 	Expose data in table TI_IFM_SAP_BREAKPOINT_CHANGES from database as business object to MES system.
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
    /// SapBreakpointChangesDAL对应表[TI_IFM_SAP_BREAKPOINT_CHANGES]
    /// </summary>
    public partial class SapBreakpointChangesDAL : BusinessObjectProvider<SapBreakpointChangesInfo>
	{
		#region Sql Statements
		private const string TI_IFM_SAP_BREAKPOINT_CHANGES_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				LOG_FID,
				WERKS,
				ZCJ,
				KTSCH,
				AENNR,
				MATNR,
				CHANGE_FLAG,
				OIDNRK,
				NIDNRK,
				MENGE,
				EBORT,
				DATUV,
				DATUB,
				SORTF,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				COMMENTS				  
				FROM [LES].[TI_IFM_SAP_BREAKPOINT_CHANGES] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TI_IFM_SAP_BREAKPOINT_CHANGES_SELECT = 
			@"SELECT ID,
				FID,
				LOG_FID,
				WERKS,
				ZCJ,
				KTSCH,
				AENNR,
				MATNR,
				CHANGE_FLAG,
				OIDNRK,
				NIDNRK,
				MENGE,
				EBORT,
				DATUV,
				DATUB,
				SORTF,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				COMMENTS				 
				FROM [LES].[TI_IFM_SAP_BREAKPOINT_CHANGES] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TI_IFM_SAP_BREAKPOINT_CHANGES_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TI_IFM_SAP_BREAKPOINT_CHANGES]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TI_IFM_SAP_BREAKPOINT_CHANGES_INSERT =
			@"INSERT INTO [LES].[TI_IFM_SAP_BREAKPOINT_CHANGES] (
				FID,
				LOG_FID,
				WERKS,
				ZCJ,
				KTSCH,
				AENNR,
				MATNR,
				CHANGE_FLAG,
				OIDNRK,
				NIDNRK,
				MENGE,
				EBORT,
				DATUV,
				DATUB,
				SORTF,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				COMMENTS				 
			) VALUES (
				@FID,
				@LOG_FID,
				@WERKS,
				@ZCJ,
				@KTSCH,
				@AENNR,
				@MATNR,
				@CHANGE_FLAG,
				@OIDNRK,
				@NIDNRK,
				@MENGE,
				@EBORT,
				@DATUV,
				@DATUB,
				@SORTF,
				@PROCESS_FLAG,
				@PROCESS_TIME,
				@VALID_FLAG,
				@CREATE_USER,
				GETDATE(),
				@MODIFY_USER,
				@MODIFY_DATE,
				@COMMENTS				 
			);SELECT @@IDENTITY;";
		private const string TI_IFM_SAP_BREAKPOINT_CHANGES_UPDATE =
			@"UPDATE [LES].[TI_IFM_SAP_BREAKPOINT_CHANGES] WITH(ROWLOCK) 
				SET FID=@FID,
				LOG_FID=@LOG_FID,
				WERKS=@WERKS,
				ZCJ=@ZCJ,
				KTSCH=@KTSCH,
				AENNR=@AENNR,
				MATNR=@MATNR,
				CHANGE_FLAG=@CHANGE_FLAG,
				OIDNRK=@OIDNRK,
				NIDNRK=@NIDNRK,
				MENGE=@MENGE,
				EBORT=@EBORT,
				DATUV=@DATUV,
				DATUB=@DATUB,
				SORTF=@SORTF,
				PROCESS_FLAG=@PROCESS_FLAG,
				PROCESS_TIME=@PROCESS_TIME,
				VALID_FLAG=@VALID_FLAG,
				CREATE_USER=@CREATE_USER,
				CREATE_DATE=@CREATE_DATE,
				MODIFY_USER=@MODIFY_USER,
				MODIFY_DATE=@MODIFY_DATE,
				COMMENTS=@COMMENTS				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TI_IFM_SAP_BREAKPOINT_CHANGES_DELETE =
			@"DELETE FROM [LES].[TI_IFM_SAP_BREAKPOINT_CHANGES] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get SapBreakpointChangesInfo
		/// </summary>
		/// <param name="ID">SapBreakpointChangesInfo Primary key </param>
		/// <returns></returns> 
		public SapBreakpointChangesInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SAP_BREAKPOINT_CHANGES_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateSapBreakpointChangesInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>SapBreakpointChangesInfo Collection </returns>
		public List<SapBreakpointChangesInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TI_IFM_SAP_BREAKPOINT_CHANGES_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>SapBreakpointChangesInfo Collection </returns>
		public List<SapBreakpointChangesInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<SapBreakpointChangesInfo> list = new List<SapBreakpointChangesInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateSapBreakpointChangesInfo(dr));
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
		public List<SapBreakpointChangesInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TI_IFM_SAP_BREAKPOINT_CHANGES]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<SapBreakpointChangesInfo> list = new List<SapBreakpointChangesInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateSapBreakpointChangesInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TI_IFM_SAP_BREAKPOINT_CHANGES_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(SapBreakpointChangesInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SAP_BREAKPOINT_CHANGES_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@LOG_FID", DbType.Guid, info.LogFid);
			db.AddInParameter(dbCommand, "@WERKS", DbType.String, info.Werks);
			db.AddInParameter(dbCommand, "@ZCJ", DbType.String, info.Zcj);
			db.AddInParameter(dbCommand, "@KTSCH", DbType.String, info.Ktsch);
			db.AddInParameter(dbCommand, "@AENNR", DbType.String, info.Aennr);
			db.AddInParameter(dbCommand, "@MATNR", DbType.String, info.Matnr);
			db.AddInParameter(dbCommand, "@CHANGE_FLAG", DbType.String, info.ChangeFlag);
			db.AddInParameter(dbCommand, "@OIDNRK", DbType.String, info.Oidnrk);
			db.AddInParameter(dbCommand, "@NIDNRK", DbType.String, info.Nidnrk);
			db.AddInParameter(dbCommand, "@MENGE", DbType.Decimal, info.Menge);
			db.AddInParameter(dbCommand, "@EBORT", DbType.String, info.Ebort);
			db.AddInParameter(dbCommand, "@DATUV", DbType.DateTime, info.Datuv);
			db.AddInParameter(dbCommand, "@DATUB", DbType.DateTime, info.Datub);
			db.AddInParameter(dbCommand, "@SORTF", DbType.String, info.Sortf);
			db.AddInParameter(dbCommand, "@PROCESS_FLAG", DbType.Int32, info.ProcessFlag);
			db.AddInParameter(dbCommand, "@PROCESS_TIME", DbType.DateTime, info.ProcessTime);
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
		public static string GetInsertSql(SapBreakpointChangesInfo info)
		{
			return  
			@"insert into [LES].[TI_IFM_SAP_BREAKPOINT_CHANGES] (
				FID,
				LOG_FID,
				WERKS,
				ZCJ,
				KTSCH,
				AENNR,
				MATNR,
				CHANGE_FLAG,
				OIDNRK,
				NIDNRK,
				MENGE,
				EBORT,
				DATUV,
				DATUB,
				SORTF,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				COMMENTS				 
			) values ("+
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				(info.LogFid == null ? "NULL" : "N'" + info.LogFid.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.Werks) ? "NULL" : "N'" + info.Werks + "'") + ","+
				(string.IsNullOrEmpty(info.Zcj) ? "NULL" : "N'" + info.Zcj + "'") + ","+
				(string.IsNullOrEmpty(info.Ktsch) ? "NULL" : "N'" + info.Ktsch + "'") + ","+
				(string.IsNullOrEmpty(info.Aennr) ? "NULL" : "N'" + info.Aennr + "'") + ","+
				(string.IsNullOrEmpty(info.Matnr) ? "NULL" : "N'" + info.Matnr + "'") + ","+
				(string.IsNullOrEmpty(info.ChangeFlag) ? "NULL" : "N'" + info.ChangeFlag + "'") + ","+
				(string.IsNullOrEmpty(info.Oidnrk) ? "NULL" : "N'" + info.Oidnrk + "'") + ","+
				(string.IsNullOrEmpty(info.Nidnrk) ? "NULL" : "N'" + info.Nidnrk + "'") + ","+
				(info.Menge == null ? "NULL" : "" + info.Menge.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.Ebort) ? "NULL" : "N'" + info.Ebort + "'") + ","+
				(info.Datuv == null ? "NULL" : "N'" + info.Datuv.GetValueOrDefault() + "'") + ","+
				(info.Datub == null ? "NULL" : "N'" + info.Datub.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.Sortf) ? "NULL" : "N'" + info.Sortf + "'") + ","+
				(info.ProcessFlag == null ? "NULL" : "" + info.ProcessFlag.GetValueOrDefault() + "") + ","+
				(info.ProcessTime == null ? "NULL" : "N'" + info.ProcessTime.GetValueOrDefault() + "'") + ","+
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
		public int Update(SapBreakpointChangesInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SAP_BREAKPOINT_CHANGES_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@LOG_FID", DbType.Guid, info.LogFid);
			db.AddInParameter(dbCommand, "@WERKS", DbType.String, info.Werks);
			db.AddInParameter(dbCommand, "@ZCJ", DbType.String, info.Zcj);
			db.AddInParameter(dbCommand, "@KTSCH", DbType.String, info.Ktsch);
			db.AddInParameter(dbCommand, "@AENNR", DbType.String, info.Aennr);
			db.AddInParameter(dbCommand, "@MATNR", DbType.String, info.Matnr);
			db.AddInParameter(dbCommand, "@CHANGE_FLAG", DbType.String, info.ChangeFlag);
			db.AddInParameter(dbCommand, "@OIDNRK", DbType.String, info.Oidnrk);
			db.AddInParameter(dbCommand, "@NIDNRK", DbType.String, info.Nidnrk);
			db.AddInParameter(dbCommand, "@MENGE", DbType.Decimal, info.Menge);
			db.AddInParameter(dbCommand, "@EBORT", DbType.String, info.Ebort);
			db.AddInParameter(dbCommand, "@DATUV", DbType.DateTime, info.Datuv);
			db.AddInParameter(dbCommand, "@DATUB", DbType.DateTime, info.Datub);
			db.AddInParameter(dbCommand, "@SORTF", DbType.String, info.Sortf);
			db.AddInParameter(dbCommand, "@PROCESS_FLAG", DbType.Int32, info.ProcessFlag);
			db.AddInParameter(dbCommand, "@PROCESS_TIME", DbType.DateTime, info.ProcessTime);
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
		/// <param name="ID">SapBreakpointChangesInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SAP_BREAKPOINT_CHANGES_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">SapBreakpointChangesInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TI_IFM_SAP_BREAKPOINT_CHANGES] WITH(ROWLOCK) "
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
		/// <param name="ID">SapBreakpointChangesInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TI_IFM_SAP_BREAKPOINT_CHANGES] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static SapBreakpointChangesInfo CreateSapBreakpointChangesInfo(IDataReader rdr)
		{
			SapBreakpointChangesInfo info = new SapBreakpointChangesInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.LogFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("LOG_FID"));			
			info.Werks = DBConvert.GetString(rdr, rdr.GetOrdinal("WERKS"));			
			info.Zcj = DBConvert.GetString(rdr, rdr.GetOrdinal("ZCJ"));			
			info.Ktsch = DBConvert.GetString(rdr, rdr.GetOrdinal("KTSCH"));			
			info.Aennr = DBConvert.GetString(rdr, rdr.GetOrdinal("AENNR"));			
			info.Matnr = DBConvert.GetString(rdr, rdr.GetOrdinal("MATNR"));			
			info.ChangeFlag = DBConvert.GetString(rdr, rdr.GetOrdinal("CHANGE_FLAG"));			
			info.Oidnrk = DBConvert.GetString(rdr, rdr.GetOrdinal("OIDNRK"));			
			info.Nidnrk = DBConvert.GetString(rdr, rdr.GetOrdinal("NIDNRK"));			
			info.Menge = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("MENGE"));			
			info.Ebort = DBConvert.GetString(rdr, rdr.GetOrdinal("EBORT"));			
			info.Datuv = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("DATUV"));			
			info.Datub = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("DATUB"));			
			info.Sortf = DBConvert.GetString(rdr, rdr.GetOrdinal("SORTF"));			
			info.ProcessFlag = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PROCESS_FLAG"));			
			info.ProcessTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("PROCESS_TIME"));			
			info.ValidFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.CreateDate = DBConvert.GetDateTime(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			info.Comments = DBConvert.GetString(rdr, rdr.GetOrdinal("COMMENTS"));			
			return info;
		}
		
		#endregion
	}
}