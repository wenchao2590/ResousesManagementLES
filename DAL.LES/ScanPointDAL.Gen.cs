#region Declaim
//---------------------------------------------------------------------------
// Name:		ScanPointDAL
// Function: 	Expose data in table TM_BAS_SCAN_POINT from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月19日
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
    /// ScanPointDAL对应表[TM_BAS_SCAN_POINT]
    /// </summary>
    public partial class ScanPointDAL : BusinessObjectProvider<ScanPointInfo>
	{
		#region Sql Statements
		private const string TM_BAS_SCAN_POINT_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				SCAN_POINT_CODE,
				SCAN_POINT_NAME,
				MES_SCAN_POINT_CODE,
				PLANT,
				WORKSHOP,
				ASSEMBLY_LINE,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				  
				FROM [LES].[TM_BAS_SCAN_POINT] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TM_BAS_SCAN_POINT_SELECT = 
			@"SELECT ID,
				FID,
				SCAN_POINT_CODE,
				SCAN_POINT_NAME,
				MES_SCAN_POINT_CODE,
				PLANT,
				WORKSHOP,
				ASSEMBLY_LINE,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
				FROM [LES].[TM_BAS_SCAN_POINT] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TM_BAS_SCAN_POINT_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TM_BAS_SCAN_POINT]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TM_BAS_SCAN_POINT_INSERT =
			@"INSERT INTO [LES].[TM_BAS_SCAN_POINT] (
				FID,
				SCAN_POINT_CODE,
				SCAN_POINT_NAME,
				MES_SCAN_POINT_CODE,
				PLANT,
				WORKSHOP,
				ASSEMBLY_LINE,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
			) VALUES (
				@FID,
				@SCAN_POINT_CODE,
				@SCAN_POINT_NAME,
				@MES_SCAN_POINT_CODE,
				@PLANT,
				@WORKSHOP,
				@ASSEMBLY_LINE,
				@COMMENTS,
				@VALID_FLAG,
				GETDATE(),
				@CREATE_USER,
				@MODIFY_DATE,
				@MODIFY_USER				 
			);SELECT @@IDENTITY;";
		private const string TM_BAS_SCAN_POINT_UPDATE =
			@"UPDATE [LES].[TM_BAS_SCAN_POINT] WITH(ROWLOCK) 
				SET FID=@FID,
				SCAN_POINT_CODE=@SCAN_POINT_CODE,
				SCAN_POINT_NAME=@SCAN_POINT_NAME,
				MES_SCAN_POINT_CODE=@MES_SCAN_POINT_CODE,
				PLANT=@PLANT,
				WORKSHOP=@WORKSHOP,
				ASSEMBLY_LINE=@ASSEMBLY_LINE,
				COMMENTS=@COMMENTS,
				VALID_FLAG=@VALID_FLAG,
				CREATE_DATE=@CREATE_DATE,
				CREATE_USER=@CREATE_USER,
				MODIFY_DATE=@MODIFY_DATE,
				MODIFY_USER=@MODIFY_USER				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TM_BAS_SCAN_POINT_DELETE =
			@"DELETE FROM [LES].[TM_BAS_SCAN_POINT] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get ScanPointInfo
		/// </summary>
		/// <param name="ID">ScanPointInfo Primary key </param>
		/// <returns></returns> 
		public ScanPointInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_BAS_SCAN_POINT_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateScanPointInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>ScanPointInfo Collection </returns>
		public List<ScanPointInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TM_BAS_SCAN_POINT_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>ScanPointInfo Collection </returns>
		public List<ScanPointInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<ScanPointInfo> list = new List<ScanPointInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateScanPointInfo(dr));
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
		public List<ScanPointInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TM_BAS_SCAN_POINT]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<ScanPointInfo> list = new List<ScanPointInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateScanPointInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TM_BAS_SCAN_POINT_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(ScanPointInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TM_BAS_SCAN_POINT_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@SCAN_POINT_CODE", DbType.String, info.ScanPointCode);
			db.AddInParameter(dbCommand, "@SCAN_POINT_NAME", DbType.String, info.ScanPointName);
			db.AddInParameter(dbCommand, "@MES_SCAN_POINT_CODE", DbType.String, info.MesScanPointCode);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
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
		public static string GetInsertSql(ScanPointInfo info)
		{
			return  
			@"insert into [LES].[TM_BAS_SCAN_POINT] (
				FID,
				SCAN_POINT_CODE,
				SCAN_POINT_NAME,
				MES_SCAN_POINT_CODE,
				PLANT,
				WORKSHOP,
				ASSEMBLY_LINE,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
			) values ("+
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.ScanPointCode) ? "NULL" : "N'" + info.ScanPointCode + "'") + ","+
				(string.IsNullOrEmpty(info.ScanPointName) ? "NULL" : "N'" + info.ScanPointName + "'") + ","+
				(string.IsNullOrEmpty(info.MesScanPointCode) ? "NULL" : "N'" + info.MesScanPointCode + "'") + ","+
				(string.IsNullOrEmpty(info.Plant) ? "NULL" : "N'" + info.Plant + "'") + ","+
				(string.IsNullOrEmpty(info.Workshop) ? "NULL" : "N'" + info.Workshop + "'") + ","+
				(string.IsNullOrEmpty(info.AssemblyLine) ? "NULL" : "N'" + info.AssemblyLine + "'") + ","+
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
		public int Update(ScanPointInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_BAS_SCAN_POINT_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@SCAN_POINT_CODE", DbType.String, info.ScanPointCode);
			db.AddInParameter(dbCommand, "@SCAN_POINT_NAME", DbType.String, info.ScanPointName);
			db.AddInParameter(dbCommand, "@MES_SCAN_POINT_CODE", DbType.String, info.MesScanPointCode);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
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
		/// <param name="ID">ScanPointInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_BAS_SCAN_POINT_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">ScanPointInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TM_BAS_SCAN_POINT] WITH(ROWLOCK) "
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
		/// <param name="ID">ScanPointInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TM_BAS_SCAN_POINT] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static ScanPointInfo CreateScanPointInfo(IDataReader rdr)
		{
			ScanPointInfo info = new ScanPointInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.ScanPointCode = DBConvert.GetString(rdr, rdr.GetOrdinal("SCAN_POINT_CODE"));			
			info.ScanPointName = DBConvert.GetString(rdr, rdr.GetOrdinal("SCAN_POINT_NAME"));			
			info.MesScanPointCode = DBConvert.GetString(rdr, rdr.GetOrdinal("MES_SCAN_POINT_CODE"));			
			info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));			
			info.Workshop = DBConvert.GetString(rdr, rdr.GetOrdinal("WORKSHOP"));			
			info.AssemblyLine = DBConvert.GetString(rdr, rdr.GetOrdinal("ASSEMBLY_LINE"));			
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
