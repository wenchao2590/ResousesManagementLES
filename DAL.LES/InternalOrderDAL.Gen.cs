#region Declaim
//---------------------------------------------------------------------------
// Name:		InternalOrderDAL
// Function: 	Expose data in table TM_BAS_INTERNAL_ORDER from database as business object to MES system.
// Tool:		T4
// CreateDate:	2020年3月30日
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
    /// InternalOrderDAL对应表[TM_BAS_INTERNAL_ORDER]
    /// </summary>
    public partial class InternalOrderDAL : BusinessObjectProvider<InternalOrderInfo>
	{
		#region Sql Statements
		private const string TM_BAS_INTERNAL_ORDER_SELECT_BY_ID =
			@"SELECT INTERNAL_ID,
				PLANT,
				INTERNAL_CODE,
				INTERNAL_NAME,
				COMMENTS,
				UPDATE_DATE,
				UPDATE_USER,
				CREATE_DATE,
				CREATE_USER,
				ASSEMBLY_LINE,
				PLANT_ZONE,
				WORKSHOP				  
				FROM [LES].[TM_BAS_INTERNAL_ORDER] WITH(NOLOCK) WHERE 1=1  AND INTERNAL_ID =@INTERNAL_ID;";
			
		private const string TM_BAS_INTERNAL_ORDER_SELECT = 
			@"SELECT INTERNAL_ID,
				PLANT,
				INTERNAL_CODE,
				INTERNAL_NAME,
				COMMENTS,
				UPDATE_DATE,
				UPDATE_USER,
				CREATE_DATE,
				CREATE_USER,
				ASSEMBLY_LINE,
				PLANT_ZONE,
				WORKSHOP				 
				FROM [LES].[TM_BAS_INTERNAL_ORDER] WITH (NOLOCK) WHERE 1=1 {0};";
		
		private const string TM_BAS_INTERNAL_ORDER_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TM_BAS_INTERNAL_ORDER]  WITH(NOLOCK) WHERE 1=1 {0};";

		private const string TM_BAS_INTERNAL_ORDER_INSERT =
			@"INSERT INTO [LES].[TM_BAS_INTERNAL_ORDER] (
				PLANT,
				INTERNAL_CODE,
				INTERNAL_NAME,
				COMMENTS,
				UPDATE_DATE,
				UPDATE_USER,
				CREATE_DATE,
				CREATE_USER,
				ASSEMBLY_LINE,
				PLANT_ZONE,
				WORKSHOP				 
			) VALUES (
				@PLANT,
				@INTERNAL_CODE,
				@INTERNAL_NAME,
				@COMMENTS,
				@UPDATE_DATE,
				@UPDATE_USER,
				GETDATE(),
				@CREATE_USER,
				@ASSEMBLY_LINE,
				@PLANT_ZONE,
				@WORKSHOP				 
			);SELECT @@IDENTITY;";
		private const string TM_BAS_INTERNAL_ORDER_UPDATE =
			@"UPDATE [LES].[TM_BAS_INTERNAL_ORDER] WITH(ROWLOCK) 
				SET PLANT=@PLANT,
				INTERNAL_CODE=@INTERNAL_CODE,
				INTERNAL_NAME=@INTERNAL_NAME,
				COMMENTS=@COMMENTS,
				UPDATE_DATE=@UPDATE_DATE,
				UPDATE_USER=@UPDATE_USER,
				CREATE_DATE=@CREATE_DATE,
				CREATE_USER=@CREATE_USER,
				ASSEMBLY_LINE=@ASSEMBLY_LINE,
				PLANT_ZONE=@PLANT_ZONE,
				WORKSHOP=@WORKSHOP				 
				WHERE 1=1  AND INTERNAL_ID =@INTERNAL_ID;";

		private const string TM_BAS_INTERNAL_ORDER_DELETE =
			@"DELETE FROM [LES].[TM_BAS_INTERNAL_ORDER] WITH(ROWLOCK)  
				WHERE 1=1  AND INTERNAL_ID =@INTERNAL_ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get InternalOrderInfo
		/// </summary>
		/// <param name="INTERNAL_ID">InternalOrderInfo Primary key </param>
		/// <returns></returns> 
		public InternalOrderInfo GetInfo(long aInternalId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_BAS_INTERNAL_ORDER_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@INTERNAL_ID", DbType.Int64, aInternalId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateInternalOrderInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>InternalOrderInfo Collection </returns>
		public List<InternalOrderInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TM_BAS_INTERNAL_ORDER_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>InternalOrderInfo Collection </returns>
		public List<InternalOrderInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<InternalOrderInfo> list = new List<InternalOrderInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateInternalOrderInfo(dr));
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
		public List<InternalOrderInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
		{
		    if (pageIndex <= 0) pageIndex = 1;
            if (pageRow <= 0) pageRow = 10;
			string whereText = string.Empty;
            if (!string.IsNullOrEmpty(textWhere))
            {
                if (textWhere.Trim().StartsWith("and", StringComparison.OrdinalIgnoreCase))
                    whereText += " where 1=1 " + textWhere;
                else
                    whereText += " where " + textWhere + " and 1=1";
            }
			else
                whereText += " where 1=1 ";
            if (string.IsNullOrEmpty(textOrder))
                textOrder += "[INTERNAL_ID] desc";
            string sql = "select top " + pageRow + " * from "
                + "(select row_number() over(order by " + textOrder + ") as rownumber"
                + ",* from [LES].[TM_BAS_INTERNAL_ORDER]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<InternalOrderInfo> list = new List<InternalOrderInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateInternalOrderInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TM_BAS_INTERNAL_ORDER_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(InternalOrderInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TM_BAS_INTERNAL_ORDER_INSERT);			
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@INTERNAL_CODE", DbType.String, info.InternalCode);
			db.AddInParameter(dbCommand, "@INTERNAL_NAME", DbType.String, info.InternalName);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@UPDATE_DATE", DbType.DateTime, info.UpdateDate);
			db.AddInParameter(dbCommand, "@UPDATE_USER", DbType.String, info.UpdateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@PLANT_ZONE", DbType.String, info.PlantZone);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}
		/// <summary>
		/// GetInsertSql
		/// </summary>
		/// <param name="info"> info</param>
		public static string GetInsertSql(InternalOrderInfo info)
		{
			return  
			@"insert into [LES].[TM_BAS_INTERNAL_ORDER] (
				PLANT,
				INTERNAL_CODE,
				INTERNAL_NAME,
				COMMENTS,
				UPDATE_DATE,
				UPDATE_USER,
				CREATE_DATE,
				CREATE_USER,
				ASSEMBLY_LINE,
				PLANT_ZONE,
				WORKSHOP				 
			) values ("+
				(string.IsNullOrEmpty(info.Plant) ? "NULL" : "N'" + info.Plant + "'") + ","+
				(string.IsNullOrEmpty(info.InternalCode) ? "NULL" : "N'" + info.InternalCode + "'") + ","+
				(string.IsNullOrEmpty(info.InternalName) ? "NULL" : "N'" + info.InternalName + "'") + ","+
				(string.IsNullOrEmpty(info.Comments) ? "NULL" : "N'" + info.Comments + "'") + ","+
				(info.UpdateDate == null ? "NULL" : "N'" + info.UpdateDate.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.UpdateUser) ? "NULL" : "N'" + info.UpdateUser + "'") + ","+
				"GETDATE()" + ","+			
				"N'" + info.CreateUser + "'" + ","+		
				(string.IsNullOrEmpty(info.AssemblyLine) ? "NULL" : "N'" + info.AssemblyLine + "'") + ","+
				(string.IsNullOrEmpty(info.PlantZone) ? "NULL" : "N'" + info.PlantZone + "'") + ","+
				(string.IsNullOrEmpty(info.Workshop) ? "NULL" : "N'" + info.Workshop + "'") + ");";				}
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(InternalOrderInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_BAS_INTERNAL_ORDER_UPDATE);				
			db.AddInParameter(dbCommand, "@INTERNAL_ID", DbType.Int64, info.InternalId);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@INTERNAL_CODE", DbType.String, info.InternalCode);
			db.AddInParameter(dbCommand, "@INTERNAL_NAME", DbType.String, info.InternalName);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@UPDATE_DATE", DbType.DateTime, info.UpdateDate);
			db.AddInParameter(dbCommand, "@UPDATE_USER", DbType.String, info.UpdateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@PLANT_ZONE", DbType.String, info.PlantZone);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="INTERNAL_ID">InternalOrderInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aInternalId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_BAS_INTERNAL_ORDER_DELETE);
		    db.AddInParameter(dbCommand, "@INTERNAL_ID", DbType.Int64, aInternalId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}
		/// <summary>
		/// UpdateInfo
		/// </summary>
		/// <param name="INTERNAL_ID">InternalOrderInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aInternalId)
		{
		    string sql = "update [LES].[TM_BAS_INTERNAL_ORDER] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE 1=1  AND INTERNAL_ID =@INTERNAL_ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@INTERNAL_ID", DbType.Int64, aInternalId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static InternalOrderInfo CreateInternalOrderInfo(IDataReader rdr)
		{
			InternalOrderInfo info = new InternalOrderInfo();
			info.InternalId = DBConvert.GetInt64(rdr, rdr.GetOrdinal("INTERNAL_ID"));			
			info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));			
			info.InternalCode = DBConvert.GetString(rdr, rdr.GetOrdinal("INTERNAL_CODE"));			
			info.InternalName = DBConvert.GetString(rdr, rdr.GetOrdinal("INTERNAL_NAME"));			
			info.Comments = DBConvert.GetString(rdr, rdr.GetOrdinal("COMMENTS"));			
			info.UpdateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("UPDATE_DATE"));			
			info.UpdateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("UPDATE_USER"));			
			info.CreateDate = DBConvert.GetDateTime(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.AssemblyLine = DBConvert.GetString(rdr, rdr.GetOrdinal("ASSEMBLY_LINE"));			
			info.PlantZone = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT_ZONE"));			
			info.Workshop = DBConvert.GetString(rdr, rdr.GetOrdinal("WORKSHOP"));			
			return info;
		}
		
		#endregion
	}
}
