#region Declaim
//---------------------------------------------------------------------------
// Name:		SapProductOrderDAL
// Function: 	Expose data in table TI_IFM_SAP_PRODUCT_ORDER from database as business object to MES system.
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
    /// SapProductOrderDAL对应表[TI_IFM_SAP_PRODUCT_ORDER]
    /// </summary>
    public partial class SapProductOrderDAL : BusinessObjectProvider<SapProductOrderInfo>
	{
		#region Sql Statements
		private const string TI_IFM_SAP_PRODUCT_ORDER_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				LOG_FID,
				MATNR,
				DWERK,
				KDAUF,
				KDPOS,
				AUFNR,
				LOCK_FLAG,
				VERID,
				PSMNG,
				ONLINE_SEQ,
				ONLINE_DATE,
				OFFLINE_DATE,
				SEQ,
				NOTICE,
				CAR_COLOR,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				COMMENTS,
				ZSC				  
				FROM [LES].[TI_IFM_SAP_PRODUCT_ORDER] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TI_IFM_SAP_PRODUCT_ORDER_SELECT = 
			@"SELECT ID,
				FID,
				LOG_FID,
				MATNR,
				DWERK,
				KDAUF,
				KDPOS,
				AUFNR,
				LOCK_FLAG,
				VERID,
				PSMNG,
				ONLINE_SEQ,
				ONLINE_DATE,
				OFFLINE_DATE,
				SEQ,
				NOTICE,
				CAR_COLOR,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				COMMENTS,
				ZSC				 
				FROM [LES].[TI_IFM_SAP_PRODUCT_ORDER] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TI_IFM_SAP_PRODUCT_ORDER_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TI_IFM_SAP_PRODUCT_ORDER]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TI_IFM_SAP_PRODUCT_ORDER_INSERT =
			@"INSERT INTO [LES].[TI_IFM_SAP_PRODUCT_ORDER] (
				FID,
				LOG_FID,
				MATNR,
				DWERK,
				KDAUF,
				KDPOS,
				AUFNR,
				LOCK_FLAG,
				VERID,
				PSMNG,
				ONLINE_SEQ,
				ONLINE_DATE,
				OFFLINE_DATE,
				SEQ,
				NOTICE,
				CAR_COLOR,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				COMMENTS,
				ZSC				 
			) VALUES (
				@FID,
				@LOG_FID,
				@MATNR,
				@DWERK,
				@KDAUF,
				@KDPOS,
				@AUFNR,
				@LOCK_FLAG,
				@VERID,
				@PSMNG,
				@ONLINE_SEQ,
				@ONLINE_DATE,
				@OFFLINE_DATE,
				@SEQ,
				@NOTICE,
				@CAR_COLOR,
				@PROCESS_FLAG,
				@PROCESS_TIME,
				@VALID_FLAG,
				@CREATE_USER,
				GETDATE(),
				@MODIFY_USER,
				@MODIFY_DATE,
				@COMMENTS,
				@ZSC				 
			);SELECT @@IDENTITY;";
		private const string TI_IFM_SAP_PRODUCT_ORDER_UPDATE =
			@"UPDATE [LES].[TI_IFM_SAP_PRODUCT_ORDER] WITH(ROWLOCK) 
				SET FID=@FID,
				LOG_FID=@LOG_FID,
				MATNR=@MATNR,
				DWERK=@DWERK,
				KDAUF=@KDAUF,
				KDPOS=@KDPOS,
				AUFNR=@AUFNR,
				LOCK_FLAG=@LOCK_FLAG,
				VERID=@VERID,
				PSMNG=@PSMNG,
				ONLINE_SEQ=@ONLINE_SEQ,
				ONLINE_DATE=@ONLINE_DATE,
				OFFLINE_DATE=@OFFLINE_DATE,
				SEQ=@SEQ,
				NOTICE=@NOTICE,
				CAR_COLOR=@CAR_COLOR,
				PROCESS_FLAG=@PROCESS_FLAG,
				PROCESS_TIME=@PROCESS_TIME,
				VALID_FLAG=@VALID_FLAG,
				CREATE_USER=@CREATE_USER,
				CREATE_DATE=@CREATE_DATE,
				MODIFY_USER=@MODIFY_USER,
				MODIFY_DATE=@MODIFY_DATE,
				COMMENTS=@COMMENTS,
				ZSC=@ZSC				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TI_IFM_SAP_PRODUCT_ORDER_DELETE =
			@"DELETE FROM [LES].[TI_IFM_SAP_PRODUCT_ORDER] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get SapProductOrderInfo
		/// </summary>
		/// <param name="ID">SapProductOrderInfo Primary key </param>
		/// <returns></returns> 
		public SapProductOrderInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SAP_PRODUCT_ORDER_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateSapProductOrderInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>SapProductOrderInfo Collection </returns>
		public List<SapProductOrderInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TI_IFM_SAP_PRODUCT_ORDER_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>SapProductOrderInfo Collection </returns>
		public List<SapProductOrderInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<SapProductOrderInfo> list = new List<SapProductOrderInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateSapProductOrderInfo(dr));
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
		public List<SapProductOrderInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TI_IFM_SAP_PRODUCT_ORDER]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<SapProductOrderInfo> list = new List<SapProductOrderInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateSapProductOrderInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TI_IFM_SAP_PRODUCT_ORDER_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(SapProductOrderInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SAP_PRODUCT_ORDER_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@LOG_FID", DbType.Guid, info.LogFid);
			db.AddInParameter(dbCommand, "@MATNR", DbType.String, info.Matnr);
			db.AddInParameter(dbCommand, "@DWERK", DbType.String, info.Dwerk);
			db.AddInParameter(dbCommand, "@KDAUF", DbType.String, info.Kdauf);
			db.AddInParameter(dbCommand, "@KDPOS", DbType.String, info.Kdpos);
			db.AddInParameter(dbCommand, "@AUFNR", DbType.String, info.Aufnr);
			db.AddInParameter(dbCommand, "@LOCK_FLAG", DbType.Boolean, info.LockFlag);
			db.AddInParameter(dbCommand, "@VERID", DbType.String, info.Verid);
			db.AddInParameter(dbCommand, "@PSMNG", DbType.Int32, info.Psmng);
			db.AddInParameter(dbCommand, "@ONLINE_SEQ", DbType.String, info.OnlineSeq);
			db.AddInParameter(dbCommand, "@ONLINE_DATE", DbType.DateTime, info.OnlineDate);
			db.AddInParameter(dbCommand, "@OFFLINE_DATE", DbType.DateTime, info.OfflineDate);
			db.AddInParameter(dbCommand, "@SEQ", DbType.String, info.Seq);
			db.AddInParameter(dbCommand, "@NOTICE", DbType.String, info.Notice);
			db.AddInParameter(dbCommand, "@CAR_COLOR", DbType.String, info.CarColor);
			db.AddInParameter(dbCommand, "@PROCESS_FLAG", DbType.Int32, info.ProcessFlag);
			db.AddInParameter(dbCommand, "@PROCESS_TIME", DbType.DateTime, info.ProcessTime);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@ZSC", DbType.String, info.Zsc);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}
		/// <summary>
		/// GetInsertSql
		/// </summary>
		/// <param name="info"> info</param>
		public static string GetInsertSql(SapProductOrderInfo info)
		{
			return  
			@"insert into [LES].[TI_IFM_SAP_PRODUCT_ORDER] (
				FID,
				LOG_FID,
				MATNR,
				DWERK,
				KDAUF,
				KDPOS,
				AUFNR,
				LOCK_FLAG,
				VERID,
				PSMNG,
				ONLINE_SEQ,
				ONLINE_DATE,
				OFFLINE_DATE,
				SEQ,
				NOTICE,
				CAR_COLOR,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				COMMENTS,
				ZSC				 
			) values ("+
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				(info.LogFid == null ? "NULL" : "N'" + info.LogFid.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.Matnr) ? "NULL" : "N'" + info.Matnr + "'") + ","+
				(string.IsNullOrEmpty(info.Dwerk) ? "NULL" : "N'" + info.Dwerk + "'") + ","+
				(string.IsNullOrEmpty(info.Kdauf) ? "NULL" : "N'" + info.Kdauf + "'") + ","+
				(string.IsNullOrEmpty(info.Kdpos) ? "NULL" : "N'" + info.Kdpos + "'") + ","+
				(string.IsNullOrEmpty(info.Aufnr) ? "NULL" : "N'" + info.Aufnr + "'") + ","+
				(info.LockFlag == null ? "NULL" : "" + (info.LockFlag.GetValueOrDefault() ? "1" : "0") + "") + ","+
				(string.IsNullOrEmpty(info.Verid) ? "NULL" : "N'" + info.Verid + "'") + ","+
				(info.Psmng == null ? "NULL" : "" + info.Psmng.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.OnlineSeq) ? "NULL" : "N'" + info.OnlineSeq + "'") + ","+
				(info.OnlineDate == null ? "NULL" : "N'" + info.OnlineDate.GetValueOrDefault() + "'") + ","+
				(info.OfflineDate == null ? "NULL" : "N'" + info.OfflineDate.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.Seq) ? "NULL" : "N'" + info.Seq + "'") + ","+
				(string.IsNullOrEmpty(info.Notice) ? "NULL" : "N'" + info.Notice + "'") + ","+
				(string.IsNullOrEmpty(info.CarColor) ? "NULL" : "N'" + info.CarColor + "'") + ","+
				(info.ProcessFlag == null ? "NULL" : "" + info.ProcessFlag.GetValueOrDefault() + "") + ","+
				(info.ProcessTime == null ? "NULL" : "N'" + info.ProcessTime.GetValueOrDefault() + "'") + ","+
				"1" + ","+		
				"N'" + info.CreateUser + "'" + ","+		
				"GETDATE()" + ","+			
				"NULL" + ","+			
				"NULL" + ","+			
				(string.IsNullOrEmpty(info.Comments) ? "NULL" : "N'" + info.Comments + "'") + ","+
				(string.IsNullOrEmpty(info.Zsc) ? "NULL" : "N'" + info.Zsc + "'") + ");";				}
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(SapProductOrderInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SAP_PRODUCT_ORDER_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@LOG_FID", DbType.Guid, info.LogFid);
			db.AddInParameter(dbCommand, "@MATNR", DbType.String, info.Matnr);
			db.AddInParameter(dbCommand, "@DWERK", DbType.String, info.Dwerk);
			db.AddInParameter(dbCommand, "@KDAUF", DbType.String, info.Kdauf);
			db.AddInParameter(dbCommand, "@KDPOS", DbType.String, info.Kdpos);
			db.AddInParameter(dbCommand, "@AUFNR", DbType.String, info.Aufnr);
			db.AddInParameter(dbCommand, "@LOCK_FLAG", DbType.Boolean, info.LockFlag);
			db.AddInParameter(dbCommand, "@VERID", DbType.String, info.Verid);
			db.AddInParameter(dbCommand, "@PSMNG", DbType.Int32, info.Psmng);
			db.AddInParameter(dbCommand, "@ONLINE_SEQ", DbType.String, info.OnlineSeq);
			db.AddInParameter(dbCommand, "@ONLINE_DATE", DbType.DateTime, info.OnlineDate);
			db.AddInParameter(dbCommand, "@OFFLINE_DATE", DbType.DateTime, info.OfflineDate);
			db.AddInParameter(dbCommand, "@SEQ", DbType.String, info.Seq);
			db.AddInParameter(dbCommand, "@NOTICE", DbType.String, info.Notice);
			db.AddInParameter(dbCommand, "@CAR_COLOR", DbType.String, info.CarColor);
			db.AddInParameter(dbCommand, "@PROCESS_FLAG", DbType.Int32, info.ProcessFlag);
			db.AddInParameter(dbCommand, "@PROCESS_TIME", DbType.DateTime, info.ProcessTime);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@ZSC", DbType.String, info.Zsc);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="ID">SapProductOrderInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SAP_PRODUCT_ORDER_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">SapProductOrderInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TI_IFM_SAP_PRODUCT_ORDER] WITH(ROWLOCK) "
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
		/// <param name="ID">SapProductOrderInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TI_IFM_SAP_PRODUCT_ORDER] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static SapProductOrderInfo CreateSapProductOrderInfo(IDataReader rdr)
		{
			SapProductOrderInfo info = new SapProductOrderInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.LogFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("LOG_FID"));			
			info.Matnr = DBConvert.GetString(rdr, rdr.GetOrdinal("MATNR"));			
			info.Dwerk = DBConvert.GetString(rdr, rdr.GetOrdinal("DWERK"));			
			info.Kdauf = DBConvert.GetString(rdr, rdr.GetOrdinal("KDAUF"));			
			info.Kdpos = DBConvert.GetString(rdr, rdr.GetOrdinal("KDPOS"));			
			info.Aufnr = DBConvert.GetString(rdr, rdr.GetOrdinal("AUFNR"));			
			info.LockFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("LOCK_FLAG"));			
			info.Verid = DBConvert.GetString(rdr, rdr.GetOrdinal("VERID"));			
			info.Psmng = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PSMNG"));			
			info.OnlineSeq = DBConvert.GetString(rdr, rdr.GetOrdinal("ONLINE_SEQ"));			
			info.OnlineDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("ONLINE_DATE"));			
			info.OfflineDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("OFFLINE_DATE"));			
			info.Seq = DBConvert.GetString(rdr, rdr.GetOrdinal("SEQ"));			
			info.Notice = DBConvert.GetString(rdr, rdr.GetOrdinal("NOTICE"));			
			info.CarColor = DBConvert.GetString(rdr, rdr.GetOrdinal("CAR_COLOR"));			
			info.ProcessFlag = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PROCESS_FLAG"));			
			info.ProcessTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("PROCESS_TIME"));			
			info.ValidFlag = DBConvert.GetBool(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.CreateDate = DBConvert.GetDateTime(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			info.Comments = DBConvert.GetString(rdr, rdr.GetOrdinal("COMMENTS"));			
			info.Zsc = DBConvert.GetString(rdr, rdr.GetOrdinal("ZSC"));			
			return info;
		}
		
		#endregion
	}
}
