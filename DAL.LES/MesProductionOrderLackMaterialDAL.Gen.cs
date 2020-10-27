#region Declaim
//---------------------------------------------------------------------------
// Name:		MesProductionOrderLackMaterialDAL
// Function: 	Expose data in table TI_IFM_MES_PRODUCTION_ORDER_LACK_MATERIAL from database as business object to MES system.
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
    /// MesProductionOrderLackMaterialDAL对应表[TI_IFM_MES_PRODUCTION_ORDER_LACK_MATERIAL]
    /// </summary>
    public partial class MesProductionOrderLackMaterialDAL : BusinessObjectProvider<MesProductionOrderLackMaterialInfo>
	{
		#region Sql Statements
		private const string TI_IFM_MES_PRODUCTION_ORDER_LACK_MATERIAL_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				ENTERPRISE,
				SITE_NO,
				AREA_NO,
				DMS_NO,
				MATERIAL_CHECK,
				SEND_TIME,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				LOG_FID,
				PROCESS_FLAG,
				PROCESS_TIME				  
				FROM [LES].[TI_IFM_MES_PRODUCTION_ORDER_LACK_MATERIAL] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TI_IFM_MES_PRODUCTION_ORDER_LACK_MATERIAL_SELECT = 
			@"SELECT ID,
				FID,
				ENTERPRISE,
				SITE_NO,
				AREA_NO,
				DMS_NO,
				MATERIAL_CHECK,
				SEND_TIME,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				LOG_FID,
				PROCESS_FLAG,
				PROCESS_TIME				 
				FROM [LES].[TI_IFM_MES_PRODUCTION_ORDER_LACK_MATERIAL] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TI_IFM_MES_PRODUCTION_ORDER_LACK_MATERIAL_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TI_IFM_MES_PRODUCTION_ORDER_LACK_MATERIAL]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TI_IFM_MES_PRODUCTION_ORDER_LACK_MATERIAL_INSERT =
			@"INSERT INTO [LES].[TI_IFM_MES_PRODUCTION_ORDER_LACK_MATERIAL] (
				FID,
				ENTERPRISE,
				SITE_NO,
				AREA_NO,
				DMS_NO,
				MATERIAL_CHECK,
				SEND_TIME,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				LOG_FID,
				PROCESS_FLAG,
				PROCESS_TIME				 
			) VALUES (
				@FID,
				@ENTERPRISE,
				@SITE_NO,
				@AREA_NO,
				@DMS_NO,
				@MATERIAL_CHECK,
				@SEND_TIME,
				@VALID_FLAG,
				@CREATE_USER,
				GETDATE(),
				@MODIFY_USER,
				@MODIFY_DATE,
				@LOG_FID,
				@PROCESS_FLAG,
				@PROCESS_TIME				 
			);SELECT @@IDENTITY;";
		private const string TI_IFM_MES_PRODUCTION_ORDER_LACK_MATERIAL_UPDATE =
			@"UPDATE [LES].[TI_IFM_MES_PRODUCTION_ORDER_LACK_MATERIAL] WITH(ROWLOCK) 
				SET FID=@FID,
				ENTERPRISE=@ENTERPRISE,
				SITE_NO=@SITE_NO,
				AREA_NO=@AREA_NO,
				DMS_NO=@DMS_NO,
				MATERIAL_CHECK=@MATERIAL_CHECK,
				SEND_TIME=@SEND_TIME,
				VALID_FLAG=@VALID_FLAG,
				CREATE_USER=@CREATE_USER,
				CREATE_DATE=@CREATE_DATE,
				MODIFY_USER=@MODIFY_USER,
				MODIFY_DATE=@MODIFY_DATE,
				LOG_FID=@LOG_FID,
				PROCESS_FLAG=@PROCESS_FLAG,
				PROCESS_TIME=@PROCESS_TIME				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TI_IFM_MES_PRODUCTION_ORDER_LACK_MATERIAL_DELETE =
			@"DELETE FROM [LES].[TI_IFM_MES_PRODUCTION_ORDER_LACK_MATERIAL] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get MesProductionOrderLackMaterialInfo
		/// </summary>
		/// <param name="ID">MesProductionOrderLackMaterialInfo Primary key </param>
		/// <returns></returns> 
		public MesProductionOrderLackMaterialInfo GetInfo(int aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_MES_PRODUCTION_ORDER_LACK_MATERIAL_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int32, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateMesProductionOrderLackMaterialInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>MesProductionOrderLackMaterialInfo Collection </returns>
		public List<MesProductionOrderLackMaterialInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TI_IFM_MES_PRODUCTION_ORDER_LACK_MATERIAL_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>MesProductionOrderLackMaterialInfo Collection </returns>
		public List<MesProductionOrderLackMaterialInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<MesProductionOrderLackMaterialInfo> list = new List<MesProductionOrderLackMaterialInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateMesProductionOrderLackMaterialInfo(dr));
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
		public List<MesProductionOrderLackMaterialInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TI_IFM_MES_PRODUCTION_ORDER_LACK_MATERIAL]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<MesProductionOrderLackMaterialInfo> list = new List<MesProductionOrderLackMaterialInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateMesProductionOrderLackMaterialInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TI_IFM_MES_PRODUCTION_ORDER_LACK_MATERIAL_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public int Add(MesProductionOrderLackMaterialInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_MES_PRODUCTION_ORDER_LACK_MATERIAL_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@ENTERPRISE", DbType.String, info.Enterprise);
			db.AddInParameter(dbCommand, "@SITE_NO", DbType.String, info.SiteNo);
			db.AddInParameter(dbCommand, "@AREA_NO", DbType.String, info.AreaNo);
			db.AddInParameter(dbCommand, "@DMS_NO", DbType.String, info.DmsNo);
			db.AddInParameter(dbCommand, "@MATERIAL_CHECK", DbType.Boolean, info.MaterialCheck);
			db.AddInParameter(dbCommand, "@SEND_TIME", DbType.DateTime, info.SendTime);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@LOG_FID", DbType.Guid, info.LogFid);
			db.AddInParameter(dbCommand, "@PROCESS_FLAG", DbType.Int32, info.ProcessFlag);
			db.AddInParameter(dbCommand, "@PROCESS_TIME", DbType.DateTime, info.ProcessTime);
			return int.Parse("0" + db.ExecuteScalar(dbCommand));		
		}
		/// <summary>
		/// GetInsertSql
		/// </summary>
		/// <param name="info"> info</param>
		public static string GetInsertSql(MesProductionOrderLackMaterialInfo info)
		{
			return  
			@"insert into [LES].[TI_IFM_MES_PRODUCTION_ORDER_LACK_MATERIAL] (
				FID,
				ENTERPRISE,
				SITE_NO,
				AREA_NO,
				DMS_NO,
				MATERIAL_CHECK,
				SEND_TIME,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE,
				LOG_FID,
				PROCESS_FLAG,
				PROCESS_TIME				 
			) values ("+
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.Enterprise) ? "NULL" : "N'" + info.Enterprise + "'") + ","+
				(string.IsNullOrEmpty(info.SiteNo) ? "NULL" : "N'" + info.SiteNo + "'") + ","+
				(string.IsNullOrEmpty(info.AreaNo) ? "NULL" : "N'" + info.AreaNo + "'") + ","+
				(string.IsNullOrEmpty(info.DmsNo) ? "NULL" : "N'" + info.DmsNo + "'") + ","+
				(info.MaterialCheck == null ? "NULL" : "" + (info.MaterialCheck.GetValueOrDefault() ? "1" : "0") + "") + ","+
				(info.SendTime == null ? "NULL" : "N'" + info.SendTime.GetValueOrDefault() + "'") + ","+
				"1" + ","+		
				"N'" + info.CreateUser + "'" + ","+		
				"GETDATE()" + ","+			
				"NULL" + ","+			
				"NULL" + ","+			
				(info.LogFid == null ? "NULL" : "N'" + info.LogFid.GetValueOrDefault() + "'") + ","+
				(info.ProcessFlag == null ? "NULL" : "" + info.ProcessFlag.GetValueOrDefault() + "") + ","+
				(info.ProcessTime == null ? "NULL" : "N'" + info.ProcessTime.GetValueOrDefault() + "'") + ");";				}
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(MesProductionOrderLackMaterialInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_MES_PRODUCTION_ORDER_LACK_MATERIAL_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int32, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@ENTERPRISE", DbType.String, info.Enterprise);
			db.AddInParameter(dbCommand, "@SITE_NO", DbType.String, info.SiteNo);
			db.AddInParameter(dbCommand, "@AREA_NO", DbType.String, info.AreaNo);
			db.AddInParameter(dbCommand, "@DMS_NO", DbType.String, info.DmsNo);
			db.AddInParameter(dbCommand, "@MATERIAL_CHECK", DbType.Boolean, info.MaterialCheck);
			db.AddInParameter(dbCommand, "@SEND_TIME", DbType.DateTime, info.SendTime);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@LOG_FID", DbType.Guid, info.LogFid);
			db.AddInParameter(dbCommand, "@PROCESS_FLAG", DbType.Int32, info.ProcessFlag);
			db.AddInParameter(dbCommand, "@PROCESS_TIME", DbType.DateTime, info.ProcessTime);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="ID">MesProductionOrderLackMaterialInfo Primary key </param>
		/// <returns></returns>
		public int Delete(int aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_IFM_MES_PRODUCTION_ORDER_LACK_MATERIAL_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int32, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">MesProductionOrderLackMaterialInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(int aId, string loginUser)
		{
		    string sql = "update [LES].[TI_IFM_MES_PRODUCTION_ORDER_LACK_MATERIAL] WITH(ROWLOCK) "
                + "set [VALID_FLAG] = @VALID_FLAG ,[MODIFY_USER] = @MODIFY_USER ,[MODIFY_DATE] = GETDATE() "
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
 			db.AddInParameter(dbCommand, "@ID", DbType.Int32, aId);
               db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, false);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.AnsiString, loginUser);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}
		/// <summary>
		/// UpdateInfo
		/// </summary>
		/// <param name="ID">MesProductionOrderLackMaterialInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,int aId)
		{
		    string sql = "update [LES].[TI_IFM_MES_PRODUCTION_ORDER_LACK_MATERIAL] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int32, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static MesProductionOrderLackMaterialInfo CreateMesProductionOrderLackMaterialInfo(IDataReader rdr)
		{
			MesProductionOrderLackMaterialInfo info = new MesProductionOrderLackMaterialInfo();
			info.Id = DBConvert.GetInt32(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.Enterprise = DBConvert.GetString(rdr, rdr.GetOrdinal("ENTERPRISE"));			
			info.SiteNo = DBConvert.GetString(rdr, rdr.GetOrdinal("SITE_NO"));			
			info.AreaNo = DBConvert.GetString(rdr, rdr.GetOrdinal("AREA_NO"));			
			info.DmsNo = DBConvert.GetString(rdr, rdr.GetOrdinal("DMS_NO"));			
			info.MaterialCheck = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("MATERIAL_CHECK"));			
			info.SendTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("SEND_TIME"));			
			info.ValidFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.CreateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			info.LogFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("LOG_FID"));			
			info.ProcessFlag = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PROCESS_FLAG"));			
			info.ProcessTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("PROCESS_TIME"));			
			return info;
		}
		
		#endregion
	}
}
