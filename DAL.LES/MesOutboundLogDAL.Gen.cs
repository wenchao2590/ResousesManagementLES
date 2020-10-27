#region Declaim
//---------------------------------------------------------------------------
// Name:		MesOutboundLogDAL
// Function: 	Expose data in table TI_IFM_MES_OUTBOUND_LOG from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年6月23日
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
    /// MesOutboundLogDAL对应表[TI_IFM_MES_OUTBOUND_LOG]
    /// </summary>
    public partial class MesOutboundLogDAL : BusinessObjectProvider<MesOutboundLogInfo>
	{
		#region Sql Statements
		private const string TI_IFM_MES_OUTBOUND_LOG_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				TRANS_NO,
				SOURCE_SYSTEM,
				TARGET_SYSTEM,
				METHOD_CODE,
				KEY_VALUE,
				EXECUTE_START_TIME,
				EXECUTE_END_TIME,
				EXECUTE_RESULT,
				EXECUTE_TIMES,
				MSG_CONTENT,
				ERROR_CODE,
				ERROR_MSG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				VALID_FLAG				  
				FROM [LES].[TI_IFM_MES_OUTBOUND_LOG] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TI_IFM_MES_OUTBOUND_LOG_SELECT = 
			@"SELECT ID,
				FID,
				TRANS_NO,
				SOURCE_SYSTEM,
				TARGET_SYSTEM,
				METHOD_CODE,
				KEY_VALUE,
				EXECUTE_START_TIME,
				EXECUTE_END_TIME,
				EXECUTE_RESULT,
				EXECUTE_TIMES,
				MSG_CONTENT,
				ERROR_CODE,
				ERROR_MSG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				VALID_FLAG				 
				FROM [LES].[TI_IFM_MES_OUTBOUND_LOG] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TI_IFM_MES_OUTBOUND_LOG_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TI_IFM_MES_OUTBOUND_LOG]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TI_IFM_MES_OUTBOUND_LOG_INSERT =
			@"INSERT INTO [LES].[TI_IFM_MES_OUTBOUND_LOG] (
				FID,
				TRANS_NO,
				SOURCE_SYSTEM,
				TARGET_SYSTEM,
				METHOD_CODE,
				KEY_VALUE,
				EXECUTE_START_TIME,
				EXECUTE_END_TIME,
				EXECUTE_RESULT,
				EXECUTE_TIMES,
				MSG_CONTENT,
				ERROR_CODE,
				ERROR_MSG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				VALID_FLAG				 
			) VALUES (
				@FID,
				@TRANS_NO,
				@SOURCE_SYSTEM,
				@TARGET_SYSTEM,
				@METHOD_CODE,
				@KEY_VALUE,
				@EXECUTE_START_TIME,
				@EXECUTE_END_TIME,
				@EXECUTE_RESULT,
				@EXECUTE_TIMES,
				@MSG_CONTENT,
				@ERROR_CODE,
				@ERROR_MSG,
				@CREATE_USER,
				GETDATE(),
				@MODIFY_USER,
				@MODIFY_DATE,
				@VALID_FLAG				 
			);SELECT @@IDENTITY;";
		private const string TI_IFM_MES_OUTBOUND_LOG_UPDATE =
			@"UPDATE [LES].[TI_IFM_MES_OUTBOUND_LOG] WITH(ROWLOCK) 
				SET FID=@FID,
				TRANS_NO=@TRANS_NO,
				SOURCE_SYSTEM=@SOURCE_SYSTEM,
				TARGET_SYSTEM=@TARGET_SYSTEM,
				METHOD_CODE=@METHOD_CODE,
				KEY_VALUE=@KEY_VALUE,
				EXECUTE_START_TIME=@EXECUTE_START_TIME,
				EXECUTE_END_TIME=@EXECUTE_END_TIME,
				EXECUTE_RESULT=@EXECUTE_RESULT,
				EXECUTE_TIMES=@EXECUTE_TIMES,
				MSG_CONTENT=@MSG_CONTENT,
				ERROR_CODE=@ERROR_CODE,
				ERROR_MSG=@ERROR_MSG,
				CREATE_USER=@CREATE_USER,
				CREATE_DATE=@CREATE_DATE,
				MODIFY_USER=@MODIFY_USER,
				MODIFY_DATE=@MODIFY_DATE,
				VALID_FLAG=@VALID_FLAG				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TI_IFM_MES_OUTBOUND_LOG_DELETE =
			@"DELETE FROM [LES].[TI_IFM_MES_OUTBOUND_LOG] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get MesOutboundLogInfo
		/// </summary>
		/// <param name="ID">MesOutboundLogInfo Primary key </param>
		/// <returns></returns> 
		public MesOutboundLogInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_MES_OUTBOUND_LOG_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateMesOutboundLogInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>MesOutboundLogInfo Collection </returns>
		public List<MesOutboundLogInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TI_IFM_MES_OUTBOUND_LOG_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>MesOutboundLogInfo Collection </returns>
		public List<MesOutboundLogInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<MesOutboundLogInfo> list = new List<MesOutboundLogInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateMesOutboundLogInfo(dr));
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
		public List<MesOutboundLogInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TI_IFM_MES_OUTBOUND_LOG]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<MesOutboundLogInfo> list = new List<MesOutboundLogInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateMesOutboundLogInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TI_IFM_MES_OUTBOUND_LOG_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(MesOutboundLogInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_MES_OUTBOUND_LOG_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@TRANS_NO", DbType.String, info.TransNo);
			db.AddInParameter(dbCommand, "@SOURCE_SYSTEM", DbType.String, info.SourceSystem);
			db.AddInParameter(dbCommand, "@TARGET_SYSTEM", DbType.String, info.TargetSystem);
			db.AddInParameter(dbCommand, "@METHOD_CODE", DbType.String, info.MethodCode);
			db.AddInParameter(dbCommand, "@KEY_VALUE", DbType.String, info.KeyValue);
			db.AddInParameter(dbCommand, "@EXECUTE_START_TIME", DbType.DateTime, info.ExecuteStartTime);
			db.AddInParameter(dbCommand, "@EXECUTE_END_TIME", DbType.DateTime, info.ExecuteEndTime);
			db.AddInParameter(dbCommand, "@EXECUTE_RESULT", DbType.Int32, info.ExecuteResult);
			db.AddInParameter(dbCommand, "@EXECUTE_TIMES", DbType.Int32, info.ExecuteTimes);
			db.AddInParameter(dbCommand, "@MSG_CONTENT", DbType.String, info.MsgContent);
			db.AddInParameter(dbCommand, "@ERROR_CODE", DbType.String, info.ErrorCode);
			db.AddInParameter(dbCommand, "@ERROR_MSG", DbType.String, info.ErrorMsg);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Int32, info.ValidFlag);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}		
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(MesOutboundLogInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_MES_OUTBOUND_LOG_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@TRANS_NO", DbType.String, info.TransNo);
			db.AddInParameter(dbCommand, "@SOURCE_SYSTEM", DbType.String, info.SourceSystem);
			db.AddInParameter(dbCommand, "@TARGET_SYSTEM", DbType.String, info.TargetSystem);
			db.AddInParameter(dbCommand, "@METHOD_CODE", DbType.String, info.MethodCode);
			db.AddInParameter(dbCommand, "@KEY_VALUE", DbType.String, info.KeyValue);
			db.AddInParameter(dbCommand, "@EXECUTE_START_TIME", DbType.DateTime, info.ExecuteStartTime);
			db.AddInParameter(dbCommand, "@EXECUTE_END_TIME", DbType.DateTime, info.ExecuteEndTime);
			db.AddInParameter(dbCommand, "@EXECUTE_RESULT", DbType.Int32, info.ExecuteResult);
			db.AddInParameter(dbCommand, "@EXECUTE_TIMES", DbType.Int32, info.ExecuteTimes);
			db.AddInParameter(dbCommand, "@MSG_CONTENT", DbType.String, info.MsgContent);
			db.AddInParameter(dbCommand, "@ERROR_CODE", DbType.String, info.ErrorCode);
			db.AddInParameter(dbCommand, "@ERROR_MSG", DbType.String, info.ErrorMsg);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Int32, info.ValidFlag);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="ID">MesOutboundLogInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_MES_OUTBOUND_LOG_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">MesOutboundLogInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TI_IFM_MES_OUTBOUND_LOG] WITH(ROWLOCK) "
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
		/// <param name="ID">MesOutboundLogInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TI_IFM_MES_OUTBOUND_LOG] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static MesOutboundLogInfo CreateMesOutboundLogInfo(IDataReader rdr)
		{
			MesOutboundLogInfo info = new MesOutboundLogInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuid(rdr, rdr.GetOrdinal("FID"));			
			info.TransNo = DBConvert.GetString(rdr, rdr.GetOrdinal("TRANS_NO"));			
			info.SourceSystem = DBConvert.GetString(rdr, rdr.GetOrdinal("SOURCE_SYSTEM"));			
			info.TargetSystem = DBConvert.GetString(rdr, rdr.GetOrdinal("TARGET_SYSTEM"));			
			info.MethodCode = DBConvert.GetString(rdr, rdr.GetOrdinal("METHOD_CODE"));			
			info.KeyValue = DBConvert.GetString(rdr, rdr.GetOrdinal("KEY_VALUE"));			
			info.ExecuteStartTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("EXECUTE_START_TIME"));			
			info.ExecuteEndTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("EXECUTE_END_TIME"));			
			info.ExecuteResult = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("EXECUTE_RESULT"));			
			info.ExecuteTimes = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("EXECUTE_TIMES"));			
			info.MsgContent = DBConvert.GetString(rdr, rdr.GetOrdinal("MSG_CONTENT"));			
			info.ErrorCode = DBConvert.GetString(rdr, rdr.GetOrdinal("ERROR_CODE"));			
			info.ErrorMsg = DBConvert.GetString(rdr, rdr.GetOrdinal("ERROR_MSG"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.CreateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			info.ValidFlag = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			return info;
		}
		
		#endregion
	}
}
