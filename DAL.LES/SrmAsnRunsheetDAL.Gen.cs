#region Declaim
//---------------------------------------------------------------------------
// Name:		SrmAsnRunsheetDAL
// Function: 	Expose data in table TI_IFM_SRM_ASN_RUNSHEET from database as business object to MES system.
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
    /// SrmAsnRunsheetDAL对应表[TI_IFM_SRM_ASN_RUNSHEET]
    /// </summary>
    public partial class SrmAsnRunsheetDAL : BusinessObjectProvider<SrmAsnRunsheetInfo>
	{
		#region Sql Statements
		private const string TI_IFM_SRM_ASN_RUNSHEET_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				LOG_FID,
				ORDERCODE,
				PLANT,
				SOURCEORDERTYPE,
				DOCK,
				PUBLISHTIME,
				SUPPLIERCODE,
				SUPPLIERNAME,
				SOURCEZONENO,
				TARGETZONENO,
				KEEPER,
				PLANSHIPPINGTIME,
				PLANDELIVERYTIME,
				REMARK,
				EMERGENCYFLAG,
				DELETEFLAG,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER,
				COMMENTS				  
				FROM [LES].[TI_IFM_SRM_ASN_RUNSHEET] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TI_IFM_SRM_ASN_RUNSHEET_SELECT = 
			@"SELECT ID,
				FID,
				LOG_FID,
				ORDERCODE,
				PLANT,
				SOURCEORDERTYPE,
				DOCK,
				PUBLISHTIME,
				SUPPLIERCODE,
				SUPPLIERNAME,
				SOURCEZONENO,
				TARGETZONENO,
				KEEPER,
				PLANSHIPPINGTIME,
				PLANDELIVERYTIME,
				REMARK,
				EMERGENCYFLAG,
				DELETEFLAG,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER,
				COMMENTS				 
				FROM [LES].[TI_IFM_SRM_ASN_RUNSHEET] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TI_IFM_SRM_ASN_RUNSHEET_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TI_IFM_SRM_ASN_RUNSHEET]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TI_IFM_SRM_ASN_RUNSHEET_INSERT =
			@"INSERT INTO [LES].[TI_IFM_SRM_ASN_RUNSHEET] (
				FID,
				LOG_FID,
				ORDERCODE,
				PLANT,
				SOURCEORDERTYPE,
				DOCK,
				PUBLISHTIME,
				SUPPLIERCODE,
				SUPPLIERNAME,
				SOURCEZONENO,
				TARGETZONENO,
				KEEPER,
				PLANSHIPPINGTIME,
				PLANDELIVERYTIME,
				REMARK,
				EMERGENCYFLAG,
				DELETEFLAG,
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
				@ORDERCODE,
				@PLANT,
				@SOURCEORDERTYPE,
				@DOCK,
				@PUBLISHTIME,
				@SUPPLIERCODE,
				@SUPPLIERNAME,
				@SOURCEZONENO,
				@TARGETZONENO,
				@KEEPER,
				@PLANSHIPPINGTIME,
				@PLANDELIVERYTIME,
				@REMARK,
				@EMERGENCYFLAG,
				@DELETEFLAG,
				@PROCESS_FLAG,
				@PROCESS_TIME,
				@VALID_FLAG,
				GETDATE(),
				@CREATE_USER,
				@MODIFY_DATE,
				@MODIFY_USER,
				@COMMENTS				 
			);SELECT @@IDENTITY;";
		private const string TI_IFM_SRM_ASN_RUNSHEET_UPDATE =
			@"UPDATE [LES].[TI_IFM_SRM_ASN_RUNSHEET] WITH(ROWLOCK) 
				SET FID=@FID,
				LOG_FID=@LOG_FID,
				ORDERCODE=@ORDERCODE,
				PLANT=@PLANT,
				SOURCEORDERTYPE=@SOURCEORDERTYPE,
				DOCK=@DOCK,
				PUBLISHTIME=@PUBLISHTIME,
				SUPPLIERCODE=@SUPPLIERCODE,
				SUPPLIERNAME=@SUPPLIERNAME,
				SOURCEZONENO=@SOURCEZONENO,
				TARGETZONENO=@TARGETZONENO,
				KEEPER=@KEEPER,
				PLANSHIPPINGTIME=@PLANSHIPPINGTIME,
				PLANDELIVERYTIME=@PLANDELIVERYTIME,
				REMARK=@REMARK,
				EMERGENCYFLAG=@EMERGENCYFLAG,
				DELETEFLAG=@DELETEFLAG,
				PROCESS_FLAG=@PROCESS_FLAG,
				PROCESS_TIME=@PROCESS_TIME,
				VALID_FLAG=@VALID_FLAG,
				CREATE_DATE=@CREATE_DATE,
				CREATE_USER=@CREATE_USER,
				MODIFY_DATE=@MODIFY_DATE,
				MODIFY_USER=@MODIFY_USER,
				COMMENTS=@COMMENTS				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TI_IFM_SRM_ASN_RUNSHEET_DELETE =
			@"DELETE FROM [LES].[TI_IFM_SRM_ASN_RUNSHEET] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get SrmAsnRunsheetInfo
		/// </summary>
		/// <param name="ID">SrmAsnRunsheetInfo Primary key </param>
		/// <returns></returns> 
		public SrmAsnRunsheetInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SRM_ASN_RUNSHEET_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateSrmAsnRunsheetInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>SrmAsnRunsheetInfo Collection </returns>
		public List<SrmAsnRunsheetInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TI_IFM_SRM_ASN_RUNSHEET_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>SrmAsnRunsheetInfo Collection </returns>
		public List<SrmAsnRunsheetInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<SrmAsnRunsheetInfo> list = new List<SrmAsnRunsheetInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateSrmAsnRunsheetInfo(dr));
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
		public List<SrmAsnRunsheetInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TI_IFM_SRM_ASN_RUNSHEET]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<SrmAsnRunsheetInfo> list = new List<SrmAsnRunsheetInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateSrmAsnRunsheetInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TI_IFM_SRM_ASN_RUNSHEET_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(SrmAsnRunsheetInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SRM_ASN_RUNSHEET_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@LOG_FID", DbType.Guid, info.LogFid);
			db.AddInParameter(dbCommand, "@ORDERCODE", DbType.String, info.Ordercode);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@SOURCEORDERTYPE", DbType.Int32, info.Sourceordertype);
			db.AddInParameter(dbCommand, "@DOCK", DbType.String, info.Dock);
			db.AddInParameter(dbCommand, "@PUBLISHTIME", DbType.DateTime, info.Publishtime);
			db.AddInParameter(dbCommand, "@SUPPLIERCODE", DbType.String, info.Suppliercode);
			db.AddInParameter(dbCommand, "@SUPPLIERNAME", DbType.String, info.Suppliername);
			db.AddInParameter(dbCommand, "@SOURCEZONENO", DbType.String, info.Sourcezoneno);
			db.AddInParameter(dbCommand, "@TARGETZONENO", DbType.String, info.Targetzoneno);
			db.AddInParameter(dbCommand, "@KEEPER", DbType.String, info.Keeper);
			db.AddInParameter(dbCommand, "@PLANSHIPPINGTIME", DbType.DateTime, info.Planshippingtime);
			db.AddInParameter(dbCommand, "@PLANDELIVERYTIME", DbType.DateTime, info.Plandeliverytime);
			db.AddInParameter(dbCommand, "@REMARK", DbType.String, info.Remark);
			db.AddInParameter(dbCommand, "@EMERGENCYFLAG", DbType.Boolean, info.Emergencyflag);
			db.AddInParameter(dbCommand, "@DELETEFLAG", DbType.Boolean, info.Deleteflag);
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
		public static string GetInsertSql(SrmAsnRunsheetInfo info)
		{
			return  
			@"insert into [LES].[TI_IFM_SRM_ASN_RUNSHEET] (
				FID,
				LOG_FID,
				ORDERCODE,
				PLANT,
				SOURCEORDERTYPE,
				DOCK,
				PUBLISHTIME,
				SUPPLIERCODE,
				SUPPLIERNAME,
				SOURCEZONENO,
				TARGETZONENO,
				KEEPER,
				PLANSHIPPINGTIME,
				PLANDELIVERYTIME,
				REMARK,
				EMERGENCYFLAG,
				DELETEFLAG,
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
				(string.IsNullOrEmpty(info.Ordercode) ? "NULL" : "N'" + info.Ordercode + "'") + ","+
				(string.IsNullOrEmpty(info.Plant) ? "NULL" : "N'" + info.Plant + "'") + ","+
				(info.Sourceordertype == null ? "NULL" : "" + info.Sourceordertype.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.Dock) ? "NULL" : "N'" + info.Dock + "'") + ","+
				(info.Publishtime == null ? "NULL" : "N'" + info.Publishtime.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.Suppliercode) ? "NULL" : "N'" + info.Suppliercode + "'") + ","+
				(string.IsNullOrEmpty(info.Suppliername) ? "NULL" : "N'" + info.Suppliername + "'") + ","+
				(string.IsNullOrEmpty(info.Sourcezoneno) ? "NULL" : "N'" + info.Sourcezoneno + "'") + ","+
				(string.IsNullOrEmpty(info.Targetzoneno) ? "NULL" : "N'" + info.Targetzoneno + "'") + ","+
				(string.IsNullOrEmpty(info.Keeper) ? "NULL" : "N'" + info.Keeper + "'") + ","+
				(info.Planshippingtime == null ? "NULL" : "N'" + info.Planshippingtime.GetValueOrDefault() + "'") + ","+
				(info.Plandeliverytime == null ? "NULL" : "N'" + info.Plandeliverytime.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.Remark) ? "NULL" : "N'" + info.Remark + "'") + ","+
				(info.Emergencyflag == null ? "NULL" : "" + (info.Emergencyflag.GetValueOrDefault() ? "1" : "0") + "") + ","+
				(info.Deleteflag == null ? "NULL" : "" + (info.Deleteflag.GetValueOrDefault() ? "1" : "0") + "") + ","+
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
		public int Update(SrmAsnRunsheetInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SRM_ASN_RUNSHEET_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@LOG_FID", DbType.Guid, info.LogFid);
			db.AddInParameter(dbCommand, "@ORDERCODE", DbType.String, info.Ordercode);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@SOURCEORDERTYPE", DbType.Int32, info.Sourceordertype);
			db.AddInParameter(dbCommand, "@DOCK", DbType.String, info.Dock);
			db.AddInParameter(dbCommand, "@PUBLISHTIME", DbType.DateTime, info.Publishtime);
			db.AddInParameter(dbCommand, "@SUPPLIERCODE", DbType.String, info.Suppliercode);
			db.AddInParameter(dbCommand, "@SUPPLIERNAME", DbType.String, info.Suppliername);
			db.AddInParameter(dbCommand, "@SOURCEZONENO", DbType.String, info.Sourcezoneno);
			db.AddInParameter(dbCommand, "@TARGETZONENO", DbType.String, info.Targetzoneno);
			db.AddInParameter(dbCommand, "@KEEPER", DbType.String, info.Keeper);
			db.AddInParameter(dbCommand, "@PLANSHIPPINGTIME", DbType.DateTime, info.Planshippingtime);
			db.AddInParameter(dbCommand, "@PLANDELIVERYTIME", DbType.DateTime, info.Plandeliverytime);
			db.AddInParameter(dbCommand, "@REMARK", DbType.String, info.Remark);
			db.AddInParameter(dbCommand, "@EMERGENCYFLAG", DbType.Boolean, info.Emergencyflag);
			db.AddInParameter(dbCommand, "@DELETEFLAG", DbType.Boolean, info.Deleteflag);
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
		/// <param name="ID">SrmAsnRunsheetInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_SRM_ASN_RUNSHEET_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">SrmAsnRunsheetInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TI_IFM_SRM_ASN_RUNSHEET] WITH(ROWLOCK) "
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
		/// <param name="ID">SrmAsnRunsheetInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TI_IFM_SRM_ASN_RUNSHEET] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static SrmAsnRunsheetInfo CreateSrmAsnRunsheetInfo(IDataReader rdr)
		{
			SrmAsnRunsheetInfo info = new SrmAsnRunsheetInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.LogFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("LOG_FID"));			
			info.Ordercode = DBConvert.GetString(rdr, rdr.GetOrdinal("ORDERCODE"));			
			info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));			
			info.Sourceordertype = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("SOURCEORDERTYPE"));			
			info.Dock = DBConvert.GetString(rdr, rdr.GetOrdinal("DOCK"));			
			info.Publishtime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("PUBLISHTIME"));			
			info.Suppliercode = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIERCODE"));			
			info.Suppliername = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIERNAME"));			
			info.Sourcezoneno = DBConvert.GetString(rdr, rdr.GetOrdinal("SOURCEZONENO"));			
			info.Targetzoneno = DBConvert.GetString(rdr, rdr.GetOrdinal("TARGETZONENO"));			
			info.Keeper = DBConvert.GetString(rdr, rdr.GetOrdinal("KEEPER"));			
			info.Planshippingtime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("PLANSHIPPINGTIME"));			
			info.Plandeliverytime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("PLANDELIVERYTIME"));			
			info.Remark = DBConvert.GetString(rdr, rdr.GetOrdinal("REMARK"));			
			info.Emergencyflag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("EMERGENCYFLAG"));			
			info.Deleteflag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("DELETEFLAG"));			
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
