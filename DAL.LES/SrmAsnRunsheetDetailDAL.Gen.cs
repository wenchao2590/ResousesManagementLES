#region Declaim
//---------------------------------------------------------------------------
// Name:		SrmAsnRunsheetDetailDAL
// Function: 	Expose data in table TI_IFM_SRM_ASN_RUNSHEET_DETAIL from database as business object to MES system.
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
    /// SrmAsnRunsheetDetailDAL对应表[TI_IFM_SRM_ASN_RUNSHEET_DETAIL]
    /// </summary>
    public partial class SrmAsnRunsheetDetailDAL : BusinessObjectProvider<SrmAsnRunsheetDetailInfo>
	{
		#region Sql Statements
		private const string TI_IFM_SRM_ASN_RUNSHEET_DETAIL_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				LOG_FID,
				SOURCEORDERCODE,
				PARTNO,
				PARTCNAME,
				PARTQTY,
				TARGETSLCODE,
				PACKAGECODE,
				SNP,
				REMARK,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER,
				COMMENTS				  
				FROM [LES].[TI_IFM_SRM_ASN_RUNSHEET_DETAIL] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TI_IFM_SRM_ASN_RUNSHEET_DETAIL_SELECT = 
			@"SELECT ID,
				FID,
				LOG_FID,
				SOURCEORDERCODE,
				PARTNO,
				PARTCNAME,
				PARTQTY,
				TARGETSLCODE,
				PACKAGECODE,
				SNP,
				REMARK,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER,
				COMMENTS				 
				FROM [LES].[TI_IFM_SRM_ASN_RUNSHEET_DETAIL] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TI_IFM_SRM_ASN_RUNSHEET_DETAIL_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TI_IFM_SRM_ASN_RUNSHEET_DETAIL]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TI_IFM_SRM_ASN_RUNSHEET_DETAIL_INSERT =
			@"INSERT INTO [LES].[TI_IFM_SRM_ASN_RUNSHEET_DETAIL] (
				FID,
				LOG_FID,
				SOURCEORDERCODE,
				PARTNO,
				PARTCNAME,
				PARTQTY,
				TARGETSLCODE,
				PACKAGECODE,
				SNP,
				REMARK,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER,
				COMMENTS				 
			) VALUES (
				@FID,
				@LOG_FID,
				@SOURCEORDERCODE,
				@PARTNO,
				@PARTCNAME,
				@PARTQTY,
				@TARGETSLCODE,
				@PACKAGECODE,
				@SNP,
				@REMARK,
				@PROCESS_FLAG,
				@PROCESS_TIME,
				@VALID_FLAG,
				GETDATE(),
				@CREATE_USER,
				@MODIFY_DATE,
				@MODIFY_USER,
				@COMMENTS				 
			);SELECT @@IDENTITY;";
		private const string TI_IFM_SRM_ASN_RUNSHEET_DETAIL_UPDATE =
			@"UPDATE [LES].[TI_IFM_SRM_ASN_RUNSHEET_DETAIL] WITH(ROWLOCK) 
				SET FID=@FID,
				LOG_FID=@LOG_FID,
				SOURCEORDERCODE=@SOURCEORDERCODE,
				PARTNO=@PARTNO,
				PARTCNAME=@PARTCNAME,
				PARTQTY=@PARTQTY,
				TARGETSLCODE=@TARGETSLCODE,
				PACKAGECODE=@PACKAGECODE,
				SNP=@SNP,
				REMARK=@REMARK,
				PROCESS_FLAG=@PROCESS_FLAG,
				PROCESS_TIME=@PROCESS_TIME,
				VALID_FLAG=@VALID_FLAG,
				CREATE_DATE=@CREATE_DATE,
				CREATE_USER=@CREATE_USER,
				MODIFY_DATE=@MODIFY_DATE,
				MODIFY_USER=@MODIFY_USER,
				COMMENTS=@COMMENTS				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TI_IFM_SRM_ASN_RUNSHEET_DETAIL_DELETE =
			@"DELETE FROM [LES].[TI_IFM_SRM_ASN_RUNSHEET_DETAIL] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get SrmAsnRunsheetDetailInfo
		/// </summary>
		/// <param name="ID">SrmAsnRunsheetDetailInfo Primary key </param>
		/// <returns></returns> 
		public SrmAsnRunsheetDetailInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SRM_ASN_RUNSHEET_DETAIL_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateSrmAsnRunsheetDetailInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>SrmAsnRunsheetDetailInfo Collection </returns>
		public List<SrmAsnRunsheetDetailInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TI_IFM_SRM_ASN_RUNSHEET_DETAIL_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>SrmAsnRunsheetDetailInfo Collection </returns>
		public List<SrmAsnRunsheetDetailInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<SrmAsnRunsheetDetailInfo> list = new List<SrmAsnRunsheetDetailInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateSrmAsnRunsheetDetailInfo(dr));
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
		public List<SrmAsnRunsheetDetailInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TI_IFM_SRM_ASN_RUNSHEET_DETAIL]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<SrmAsnRunsheetDetailInfo> list = new List<SrmAsnRunsheetDetailInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateSrmAsnRunsheetDetailInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TI_IFM_SRM_ASN_RUNSHEET_DETAIL_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(SrmAsnRunsheetDetailInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SRM_ASN_RUNSHEET_DETAIL_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@LOG_FID", DbType.Guid, info.LogFid);
			db.AddInParameter(dbCommand, "@SOURCEORDERCODE", DbType.String, info.Sourceordercode);
			db.AddInParameter(dbCommand, "@PARTNO", DbType.String, info.Partno);
			db.AddInParameter(dbCommand, "@PARTCNAME", DbType.String, info.Partcname);
			db.AddInParameter(dbCommand, "@PARTQTY", DbType.Decimal, info.Partqty);
			db.AddInParameter(dbCommand, "@TARGETSLCODE", DbType.String, info.Targetslcode);
			db.AddInParameter(dbCommand, "@PACKAGECODE", DbType.String, info.Packagecode);
			db.AddInParameter(dbCommand, "@SNP", DbType.Decimal, info.Snp);
			db.AddInParameter(dbCommand, "@REMARK", DbType.String, info.Remark);
			db.AddInParameter(dbCommand, "@PROCESS_FLAG", DbType.Int32, info.ProcessFlag);
			db.AddInParameter(dbCommand, "@PROCESS_TIME", DbType.DateTime, info.ProcessTime);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}
		/// <summary>
		/// GetInsertSql
		/// </summary>
		/// <param name="info"> info</param>
		public static string GetInsertSql(SrmAsnRunsheetDetailInfo info)
		{
			return  
			@"insert into [LES].[TI_IFM_SRM_ASN_RUNSHEET_DETAIL] (
				FID,
				LOG_FID,
				SOURCEORDERCODE,
				PARTNO,
				PARTCNAME,
				PARTQTY,
				TARGETSLCODE,
				PACKAGECODE,
				SNP,
				REMARK,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER,
				COMMENTS				 
			) values ("+
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				(info.LogFid == null ? "NULL" : "N'" + info.LogFid.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.Sourceordercode) ? "NULL" : "N'" + info.Sourceordercode + "'") + ","+
				(string.IsNullOrEmpty(info.Partno) ? "NULL" : "N'" + info.Partno + "'") + ","+
				(string.IsNullOrEmpty(info.Partcname) ? "NULL" : "N'" + info.Partcname + "'") + ","+
				(info.Partqty == null ? "NULL" : "" + info.Partqty.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.Targetslcode) ? "NULL" : "N'" + info.Targetslcode + "'") + ","+
				(string.IsNullOrEmpty(info.Packagecode) ? "NULL" : "N'" + info.Packagecode + "'") + ","+
				(info.Snp == null ? "NULL" : "" + info.Snp.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.Remark) ? "NULL" : "N'" + info.Remark + "'") + ","+
				(info.ProcessFlag == null ? "NULL" : "" + info.ProcessFlag.GetValueOrDefault() + "") + ","+
				(info.ProcessTime == null ? "NULL" : "N'" + info.ProcessTime.GetValueOrDefault() + "'") + ","+
				"1" + ","+		
				"GETDATE()" + ","+			
				"N'" + info.CreateUser + "'" + ","+		
				"NULL" + ","+			
				"NULL" + ","+			
				(string.IsNullOrEmpty(info.Comments) ? "NULL" : "N'" + info.Comments + "'") + ");";				}
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(SrmAsnRunsheetDetailInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SRM_ASN_RUNSHEET_DETAIL_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@LOG_FID", DbType.Guid, info.LogFid);
			db.AddInParameter(dbCommand, "@SOURCEORDERCODE", DbType.String, info.Sourceordercode);
			db.AddInParameter(dbCommand, "@PARTNO", DbType.String, info.Partno);
			db.AddInParameter(dbCommand, "@PARTCNAME", DbType.String, info.Partcname);
			db.AddInParameter(dbCommand, "@PARTQTY", DbType.Decimal, info.Partqty);
			db.AddInParameter(dbCommand, "@TARGETSLCODE", DbType.String, info.Targetslcode);
			db.AddInParameter(dbCommand, "@PACKAGECODE", DbType.String, info.Packagecode);
			db.AddInParameter(dbCommand, "@SNP", DbType.Decimal, info.Snp);
			db.AddInParameter(dbCommand, "@REMARK", DbType.String, info.Remark);
			db.AddInParameter(dbCommand, "@PROCESS_FLAG", DbType.Int32, info.ProcessFlag);
			db.AddInParameter(dbCommand, "@PROCESS_TIME", DbType.DateTime, info.ProcessTime);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="ID">SrmAsnRunsheetDetailInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SRM_ASN_RUNSHEET_DETAIL_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">SrmAsnRunsheetDetailInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TI_IFM_SRM_ASN_RUNSHEET_DETAIL] WITH(ROWLOCK) "
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
		/// <param name="ID">SrmAsnRunsheetDetailInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TI_IFM_SRM_ASN_RUNSHEET_DETAIL] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static SrmAsnRunsheetDetailInfo CreateSrmAsnRunsheetDetailInfo(IDataReader rdr)
		{
			SrmAsnRunsheetDetailInfo info = new SrmAsnRunsheetDetailInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.LogFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("LOG_FID"));			
			info.Sourceordercode = DBConvert.GetString(rdr, rdr.GetOrdinal("SOURCEORDERCODE"));			
			info.Partno = DBConvert.GetString(rdr, rdr.GetOrdinal("PARTNO"));			
			info.Partcname = DBConvert.GetString(rdr, rdr.GetOrdinal("PARTCNAME"));			
			info.Partqty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("PARTQTY"));			
			info.Targetslcode = DBConvert.GetString(rdr, rdr.GetOrdinal("TARGETSLCODE"));			
			info.Packagecode = DBConvert.GetString(rdr, rdr.GetOrdinal("PACKAGECODE"));			
			info.Snp = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("SNP"));			
			info.Remark = DBConvert.GetString(rdr, rdr.GetOrdinal("REMARK"));			
			info.ProcessFlag = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PROCESS_FLAG"));			
			info.ProcessTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("PROCESS_TIME"));			
			info.ValidFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			info.CreateDate = DBConvert.GetDateTime(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			info.Comments = DBConvert.GetString(rdr, rdr.GetOrdinal("COMMENTS"));			
			return info;
		}
		
		#endregion
	}
}
