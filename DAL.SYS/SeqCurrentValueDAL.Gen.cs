#region Declaim
//---------------------------------------------------------------------------
// Name:		SeqCurrentValueDAL
// Function: 	Expose data in table TS_SYS_SEQ_CURRENT_VALUE from database as business object to MES system.
// Tool:		T4
// CreateDate:	2020年3月9日
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------
#endregion

#region Imported Namespace

using DM.SYS;
using Infrustructure.Data;
using Infrustructure.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
          
#endregion

namespace DAL.SYS 
{     
	//// <summary>
    /// SeqCurrentValueDAL对应表[TS_SYS_SEQ_CURRENT_VALUE]
    /// </summary>
    public partial class SeqCurrentValueDAL : BusinessObjectProvider<SeqCurrentValueInfo>
	{
		#region Sql Statements
		private const string TS_SYS_SEQ_CURRENT_VALUE_SELECT_BY_ID =
			@"SELECT QUERY_VALUE,
				CURRENT_VALUE,
				SEQ_CODE,
				CREATE_DATE,
				ID,
				VALID_FLAG,
				MODIFY_USER,
				SEQ_SECTION_FID,
				FID,
				MODIFY_DATE,
				CREATE_USER				  
				FROM [dbo].[TS_SYS_SEQ_CURRENT_VALUE] WITH(NOLOCK) WHERE [VALID_FLAG] = 1 AND ID =@ID;";
			
		private const string TS_SYS_SEQ_CURRENT_VALUE_SELECT = 
			@"SELECT QUERY_VALUE,
				CURRENT_VALUE,
				SEQ_CODE,
				CREATE_DATE,
				ID,
				VALID_FLAG,
				MODIFY_USER,
				SEQ_SECTION_FID,
				FID,
				MODIFY_DATE,
				CREATE_USER				 
				FROM [dbo].[TS_SYS_SEQ_CURRENT_VALUE] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TS_SYS_SEQ_CURRENT_VALUE_SELECT_COUNTS = 
			@"SELECT count(*) FROM [dbo].[TS_SYS_SEQ_CURRENT_VALUE]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TS_SYS_SEQ_CURRENT_VALUE_INSERT =
			@"INSERT INTO [dbo].[TS_SYS_SEQ_CURRENT_VALUE] (
				QUERY_VALUE,
				CURRENT_VALUE,
				SEQ_CODE,
				CREATE_DATE,
				VALID_FLAG,
				MODIFY_USER,
				SEQ_SECTION_FID,
				FID,
				MODIFY_DATE,
				CREATE_USER				 
			) VALUES (
				@QUERY_VALUE,
				@CURRENT_VALUE,
				@SEQ_CODE,
				@CREATE_DATE,
				@VALID_FLAG,
				@MODIFY_USER,
				@SEQ_SECTION_FID,
				@FID,
				@MODIFY_DATE,
				@CREATE_USER				 
			);SELECT @@IDENTITY;";
		private const string TS_SYS_SEQ_CURRENT_VALUE_UPDATE =
			@"UPDATE [dbo].[TS_SYS_SEQ_CURRENT_VALUE] WITH(ROWLOCK) 
				SET QUERY_VALUE=@QUERY_VALUE,
				CURRENT_VALUE=@CURRENT_VALUE,
				SEQ_CODE=@SEQ_CODE,
				CREATE_DATE=@CREATE_DATE,
				VALID_FLAG=@VALID_FLAG,
				MODIFY_USER=@MODIFY_USER,
				SEQ_SECTION_FID=@SEQ_SECTION_FID,
				FID=@FID,
				MODIFY_DATE=@MODIFY_DATE,
				CREATE_USER=@CREATE_USER				 
				WHERE [VALID_FLAG] = 1 AND ID =@ID;";

		private const string TS_SYS_SEQ_CURRENT_VALUE_DELETE =
			@"DELETE FROM [dbo].[TS_SYS_SEQ_CURRENT_VALUE] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1 AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get SeqCurrentValueInfo
		/// </summary>
		/// <param name="ID">SeqCurrentValueInfo Primary key </param>
		/// <returns></returns> 
		public SeqCurrentValueInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_SEQ_CURRENT_VALUE_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateSeqCurrentValueInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>SeqCurrentValueInfo Collection </returns>
		public List<SeqCurrentValueInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TS_SYS_SEQ_CURRENT_VALUE_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>SeqCurrentValueInfo Collection </returns>
		public List<SeqCurrentValueInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<SeqCurrentValueInfo> list = new List<SeqCurrentValueInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateSeqCurrentValueInfo(dr));
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
		public List<SeqCurrentValueInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [dbo].[TS_SYS_SEQ_CURRENT_VALUE]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<SeqCurrentValueInfo> list = new List<SeqCurrentValueInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateSeqCurrentValueInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TS_SYS_SEQ_CURRENT_VALUE_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(SeqCurrentValueInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_SEQ_CURRENT_VALUE_INSERT);			
			db.AddInParameter(dbCommand, "@QUERY_VALUE", DbType.String, info.QueryValue);
			db.AddInParameter(dbCommand, "@CURRENT_VALUE", DbType.Int32, info.CurrentValue);
			db.AddInParameter(dbCommand, "@SEQ_CODE", DbType.String, info.SeqCode);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@SEQ_SECTION_FID", DbType.Guid, info.SeqSectionFid);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}		
		/// <summary>
		/// GetInsertSql
		/// </summary>
		/// <param name="info"> info</param>
		public static string GetInsertSql(SeqCurrentValueInfo info)
		{
			return  
			@"insert into [dbo].[TS_SYS_SEQ_CURRENT_VALUE] (
				QUERY_VALUE,
				CURRENT_VALUE,
				SEQ_CODE,
				CREATE_DATE,
				VALID_FLAG,
				MODIFY_USER,
				SEQ_SECTION_FID,
				FID,
				MODIFY_DATE,
				CREATE_USER				 
			) values ("+
				(string.IsNullOrEmpty(info.QueryValue) ? "NULL" : "N'" + info.QueryValue + "'") + ","+
				(info.CurrentValue == null ? "NULL" : "" + info.CurrentValue.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.SeqCode) ? "NULL" : "N'" + info.SeqCode + "'") + ","+
				"GETDATE()" + ","+			
				"1" + ","+		
				"NULL" + ","+			
				(info.SeqSectionFid == null ? "NULL" : "N'" + info.SeqSectionFid.GetValueOrDefault() + "'") + ","+
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				"NULL" + ","+			
				"N'" + info.CreateUser + "'" + ");";		
				}
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(SeqCurrentValueInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_SEQ_CURRENT_VALUE_UPDATE);				
			db.AddInParameter(dbCommand, "@QUERY_VALUE", DbType.String, info.QueryValue);
			db.AddInParameter(dbCommand, "@CURRENT_VALUE", DbType.Int32, info.CurrentValue);
			db.AddInParameter(dbCommand, "@SEQ_CODE", DbType.String, info.SeqCode);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@SEQ_SECTION_FID", DbType.Guid, info.SeqSectionFid);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="ID">SeqCurrentValueInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TS_SYS_SEQ_CURRENT_VALUE_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">SeqCurrentValueInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [dbo].[TS_SYS_SEQ_CURRENT_VALUE] WITH(ROWLOCK) "
                + "set [VALID_FLAG] = @VALID_FLAG ,[MODIFY_USER] = @MODIFY_USER ,[MODIFY_DATE] = GETDATE() "
                + "WHERE [VALID_FLAG] = 1 AND ID =@ID;";
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
		/// <param name="ID">SeqCurrentValueInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [dbo].[TS_SYS_SEQ_CURRENT_VALUE] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1 AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static SeqCurrentValueInfo CreateSeqCurrentValueInfo(IDataReader rdr)
		{
			SeqCurrentValueInfo info = new SeqCurrentValueInfo();
			info.QueryValue = DBConvert.GetString(rdr, rdr.GetOrdinal("QUERY_VALUE"));			
			info.CurrentValue = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("CURRENT_VALUE"));			
			info.SeqCode = DBConvert.GetString(rdr, rdr.GetOrdinal("SEQ_CODE"));			
			info.CreateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.ValidFlag = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			info.SeqSectionFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("SEQ_SECTION_FID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			return info;
		}
		
		#endregion
	}
}