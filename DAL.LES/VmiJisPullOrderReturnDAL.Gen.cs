#region Declaim
//---------------------------------------------------------------------------
// Name:		VmiJisPullOrderReturnDAL
// Function: 	Expose data in table TI_IFM_VMI_JIS_PULL_ORDER_RETURN from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年6月20日
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
    /// VmiJisPullOrderReturnDAL对应表[TI_IFM_VMI_JIS_PULL_ORDER_RETURN]
    /// </summary>
    public partial class VmiJisPullOrderReturnDAL : BusinessObjectProvider<VmiJisPullOrderReturnInfo>
	{
		#region Sql Statements
		private const string TI_IFM_VMI_JIS_PULL_ORDER_RETURN_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				LOG_FID,
				ORDERCODE,
				PARTNO,
				CARSORTSEQ,
				WMSSOURCEKEY,
				WMSLINENUMBER,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE				  
				FROM [LES].[TI_IFM_VMI_JIS_PULL_ORDER_RETURN] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TI_IFM_VMI_JIS_PULL_ORDER_RETURN_SELECT = 
			@"SELECT ID,
				FID,
				LOG_FID,
				ORDERCODE,
				PARTNO,
				CARSORTSEQ,
				WMSSOURCEKEY,
				WMSLINENUMBER,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE				 
				FROM [LES].[TI_IFM_VMI_JIS_PULL_ORDER_RETURN] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TI_IFM_VMI_JIS_PULL_ORDER_RETURN_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TI_IFM_VMI_JIS_PULL_ORDER_RETURN]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TI_IFM_VMI_JIS_PULL_ORDER_RETURN_INSERT =
			@"INSERT INTO [LES].[TI_IFM_VMI_JIS_PULL_ORDER_RETURN] (
				FID,
				LOG_FID,
				ORDERCODE,
				PARTNO,
				CARSORTSEQ,
				WMSSOURCEKEY,
				WMSLINENUMBER,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE				 
			) VALUES (
				@FID,
				@LOG_FID,
				@ORDERCODE,
				@PARTNO,
				@CARSORTSEQ,
				@WMSSOURCEKEY,
				@WMSLINENUMBER,
				@PROCESS_FLAG,
				@PROCESS_TIME,
				@VALID_FLAG,
				@CREATE_USER,
				GETDATE(),
				@MODIFY_USER,
				@MODIFY_DATE				 
			);SELECT @@IDENTITY;";
		private const string TI_IFM_VMI_JIS_PULL_ORDER_RETURN_UPDATE =
			@"UPDATE [LES].[TI_IFM_VMI_JIS_PULL_ORDER_RETURN] WITH(ROWLOCK) 
				SET FID=@FID,
				LOG_FID=@LOG_FID,
				ORDERCODE=@ORDERCODE,
				PARTNO=@PARTNO,
				CARSORTSEQ=@CARSORTSEQ,
				WMSSOURCEKEY=@WMSSOURCEKEY,
				WMSLINENUMBER=@WMSLINENUMBER,
				PROCESS_FLAG=@PROCESS_FLAG,
				PROCESS_TIME=@PROCESS_TIME,
				VALID_FLAG=@VALID_FLAG,
				CREATE_USER=@CREATE_USER,
				CREATE_DATE=@CREATE_DATE,
				MODIFY_USER=@MODIFY_USER,
				MODIFY_DATE=@MODIFY_DATE				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TI_IFM_VMI_JIS_PULL_ORDER_RETURN_DELETE =
			@"DELETE FROM [LES].[TI_IFM_VMI_JIS_PULL_ORDER_RETURN] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get VmiJisPullOrderReturnInfo
		/// </summary>
		/// <param name="ID">VmiJisPullOrderReturnInfo Primary key </param>
		/// <returns></returns> 
		public VmiJisPullOrderReturnInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_VMI_JIS_PULL_ORDER_RETURN_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateVmiJisPullOrderReturnInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>VmiJisPullOrderReturnInfo Collection </returns>
		public List<VmiJisPullOrderReturnInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TI_IFM_VMI_JIS_PULL_ORDER_RETURN_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>VmiJisPullOrderReturnInfo Collection </returns>
		public List<VmiJisPullOrderReturnInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<VmiJisPullOrderReturnInfo> list = new List<VmiJisPullOrderReturnInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateVmiJisPullOrderReturnInfo(dr));
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
		public List<VmiJisPullOrderReturnInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TI_IFM_VMI_JIS_PULL_ORDER_RETURN]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<VmiJisPullOrderReturnInfo> list = new List<VmiJisPullOrderReturnInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateVmiJisPullOrderReturnInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TI_IFM_VMI_JIS_PULL_ORDER_RETURN_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(VmiJisPullOrderReturnInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_VMI_JIS_PULL_ORDER_RETURN_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@LOG_FID", DbType.Guid, info.LogFid);
			db.AddInParameter(dbCommand, "@ORDERCODE", DbType.String, info.Ordercode);
			db.AddInParameter(dbCommand, "@PARTNO", DbType.String, info.Partno);
			db.AddInParameter(dbCommand, "@CARSORTSEQ", DbType.String, info.Carsortseq);
			db.AddInParameter(dbCommand, "@WMSSOURCEKEY", DbType.String, info.Wmssourcekey);
			db.AddInParameter(dbCommand, "@WMSLINENUMBER", DbType.String, info.Wmslinenumber);
			db.AddInParameter(dbCommand, "@PROCESS_FLAG", DbType.Int32, info.ProcessFlag);
			db.AddInParameter(dbCommand, "@PROCESS_TIME", DbType.DateTime, info.ProcessTime);
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
		public int Update(VmiJisPullOrderReturnInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_VMI_JIS_PULL_ORDER_RETURN_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@LOG_FID", DbType.Guid, info.LogFid);
			db.AddInParameter(dbCommand, "@ORDERCODE", DbType.String, info.Ordercode);
			db.AddInParameter(dbCommand, "@PARTNO", DbType.String, info.Partno);
			db.AddInParameter(dbCommand, "@CARSORTSEQ", DbType.String, info.Carsortseq);
			db.AddInParameter(dbCommand, "@WMSSOURCEKEY", DbType.String, info.Wmssourcekey);
			db.AddInParameter(dbCommand, "@WMSLINENUMBER", DbType.String, info.Wmslinenumber);
			db.AddInParameter(dbCommand, "@PROCESS_FLAG", DbType.Int32, info.ProcessFlag);
			db.AddInParameter(dbCommand, "@PROCESS_TIME", DbType.DateTime, info.ProcessTime);
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
		/// <param name="ID">VmiJisPullOrderReturnInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_VMI_JIS_PULL_ORDER_RETURN_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">VmiJisPullOrderReturnInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TI_IFM_VMI_JIS_PULL_ORDER_RETURN] WITH(ROWLOCK) "
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
		/// <param name="ID">VmiJisPullOrderReturnInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TI_IFM_VMI_JIS_PULL_ORDER_RETURN] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static VmiJisPullOrderReturnInfo CreateVmiJisPullOrderReturnInfo(IDataReader rdr)
		{
			VmiJisPullOrderReturnInfo info = new VmiJisPullOrderReturnInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.LogFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("LOG_FID"));			
			info.Ordercode = DBConvert.GetString(rdr, rdr.GetOrdinal("ORDERCODE"));			
			info.Partno = DBConvert.GetString(rdr, rdr.GetOrdinal("PARTNO"));			
			info.Carsortseq = DBConvert.GetString(rdr, rdr.GetOrdinal("CARSORTSEQ"));			
			info.Wmssourcekey = DBConvert.GetString(rdr, rdr.GetOrdinal("WMSSOURCEKEY"));			
			info.Wmslinenumber = DBConvert.GetString(rdr, rdr.GetOrdinal("WMSLINENUMBER"));			
			info.ProcessFlag = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PROCESS_FLAG"));			
			info.ProcessTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("PROCESS_TIME"));			
			info.ValidFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.CreateDate = DBConvert.GetDateTime(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			return info;
		}
		
		#endregion
	}
}
