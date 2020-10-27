#region Declaim
//---------------------------------------------------------------------------
// Name:		ButtonRequestDAL
// Function: 	Expose data in table TI_EPS_BUTTON_REQUEST from database as business object to MES system.
// Tool:		T4
// CreateDate:	2017年12月27日
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
    /// ButtonRequestDAL对应表[TI_EPS_BUTTON_REQUEST]
    /// </summary>
    public partial class ButtonRequestDAL : BusinessObjectProvider<ButtonRequestInfo>
	{
		#region Sql Statements
		private const string TI_EPS_BUTTON_REQUEST_SELECT_BY_ID =
			@"SELECT REQUEST_SN,
				ASSEMBLY_LINE,
				PLANT,
				REQUEST_TIME,
				REQUEST_STATE,
				BUTTON_ID,
				COMMENTS,
				UPDATE_DATE,
				UPDATE_USER,
				CREATE_DATE,
				CREATE_USER				  
				FROM [LES].[TI_EPS_BUTTON_REQUEST] WITH(NOLOCK) WHERE 1=1  AND REQUEST_SN =@REQUEST_SN;";
			
		private const string TI_EPS_BUTTON_REQUEST_SELECT = 
			@"SELECT REQUEST_SN,
				ASSEMBLY_LINE,
				PLANT,
				REQUEST_TIME,
				REQUEST_STATE,
				BUTTON_ID,
				COMMENTS,
				UPDATE_DATE,
				UPDATE_USER,
				CREATE_DATE,
				CREATE_USER				 
				FROM [LES].[TI_EPS_BUTTON_REQUEST] WITH (NOLOCK) WHERE 1=1 {0};";
		
		private const string TI_EPS_BUTTON_REQUEST_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TI_EPS_BUTTON_REQUEST]  WITH(NOLOCK) WHERE 1=1 {0};";

		private const string TI_EPS_BUTTON_REQUEST_INSERT =
			@"INSERT INTO [LES].[TI_EPS_BUTTON_REQUEST] (
				ASSEMBLY_LINE,
				PLANT,
				REQUEST_TIME,
				REQUEST_STATE,
				BUTTON_ID,
				COMMENTS,
				UPDATE_DATE,
				UPDATE_USER,
				CREATE_DATE,
				CREATE_USER				 
			) VALUES (
				@ASSEMBLY_LINE,
				@PLANT,
				@REQUEST_TIME,
				@REQUEST_STATE,
				@BUTTON_ID,
				@COMMENTS,
				@UPDATE_DATE,
				@UPDATE_USER,
				@CREATE_DATE,
				@CREATE_USER				 
			);SELECT @@IDENTITY;";
		private const string TI_EPS_BUTTON_REQUEST_UPDATE =
			@"UPDATE [LES].[TI_EPS_BUTTON_REQUEST] WITH(ROWLOCK) 
				SET ASSEMBLY_LINE=@ASSEMBLY_LINE,
				PLANT=@PLANT,
				REQUEST_TIME=@REQUEST_TIME,
				REQUEST_STATE=@REQUEST_STATE,
				BUTTON_ID=@BUTTON_ID,
				COMMENTS=@COMMENTS,
				UPDATE_DATE=@UPDATE_DATE,
				UPDATE_USER=@UPDATE_USER,
				CREATE_DATE=@CREATE_DATE,
				CREATE_USER=@CREATE_USER				 
				WHERE 1=1  AND REQUEST_SN =@REQUEST_SN;";

		private const string TI_EPS_BUTTON_REQUEST_DELETE =
			@"DELETE FROM [LES].[TI_EPS_BUTTON_REQUEST] WITH(ROWLOCK)  
				WHERE 1=1  AND REQUEST_SN =@REQUEST_SN;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get ButtonRequestInfo
		/// </summary>
		/// <param name="REQUEST_SN">ButtonRequestInfo Primary key </param>
		/// <returns></returns> 
		public ButtonRequestInfo GetInfo(int aRequestSn)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_EPS_BUTTON_REQUEST_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@REQUEST_SN", DbType.Int32, aRequestSn);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateButtonRequestInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>ButtonRequestInfo Collection </returns>
		public List<ButtonRequestInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TI_EPS_BUTTON_REQUEST_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>ButtonRequestInfo Collection </returns>
		public List<ButtonRequestInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<ButtonRequestInfo> list = new List<ButtonRequestInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateButtonRequestInfo(dr));
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
		public List<ButtonRequestInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                textOrder += "[REQUEST_SN] desc";
            string sql = "select top " + pageRow + " * from "
                + "(select row_number() over(order by " + textOrder + ") as rownumber"
                + ",* from [LES].[TI_EPS_BUTTON_REQUEST]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<ButtonRequestInfo> list = new List<ButtonRequestInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateButtonRequestInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TI_EPS_BUTTON_REQUEST_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public int Add(ButtonRequestInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TI_EPS_BUTTON_REQUEST_INSERT);			
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@REQUEST_TIME", DbType.DateTime, info.RequestTime);
			db.AddInParameter(dbCommand, "@REQUEST_STATE", DbType.Int32, info.RequestState);
			db.AddInParameter(dbCommand, "@BUTTON_ID", DbType.AnsiString, info.ButtonId);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@UPDATE_DATE", DbType.DateTime, info.UpdateDate);
			db.AddInParameter(dbCommand, "@UPDATE_USER", DbType.String, info.UpdateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			return int.Parse("0" + db.ExecuteScalar(dbCommand));		
		}		
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(ButtonRequestInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_EPS_BUTTON_REQUEST_UPDATE);				
			db.AddInParameter(dbCommand, "@REQUEST_SN", DbType.Int32, info.RequestSn);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@REQUEST_TIME", DbType.DateTime, info.RequestTime);
			db.AddInParameter(dbCommand, "@REQUEST_STATE", DbType.Int32, info.RequestState);
			db.AddInParameter(dbCommand, "@BUTTON_ID", DbType.AnsiString, info.ButtonId);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@UPDATE_DATE", DbType.DateTime, info.UpdateDate);
			db.AddInParameter(dbCommand, "@UPDATE_USER", DbType.String, info.UpdateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="REQUEST_SN">ButtonRequestInfo Primary key </param>
		/// <returns></returns>
		public int Delete(int aRequestSn)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TI_EPS_BUTTON_REQUEST_DELETE);
		    db.AddInParameter(dbCommand, "@REQUEST_SN", DbType.Int32, aRequestSn);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}
		/// <summary>
		/// UpdateInfo
		/// </summary>
		/// <param name="REQUEST_SN">ButtonRequestInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,int aRequestSn)
		{
		    string sql = "update [LES].[TI_EPS_BUTTON_REQUEST] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE 1=1  AND REQUEST_SN =@REQUEST_SN;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@REQUEST_SN", DbType.Int32, aRequestSn);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static ButtonRequestInfo CreateButtonRequestInfo(IDataReader rdr)
		{
			ButtonRequestInfo info = new ButtonRequestInfo();
			info.RequestSn = DBConvert.GetInt32(rdr, rdr.GetOrdinal("REQUEST_SN"));			
			info.AssemblyLine = DBConvert.GetString(rdr, rdr.GetOrdinal("ASSEMBLY_LINE"));			
			info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));			
			info.RequestTime = DBConvert.GetDateTime(rdr, rdr.GetOrdinal("REQUEST_TIME"));			
			info.RequestState = DBConvert.GetInt32(rdr, rdr.GetOrdinal("REQUEST_STATE"));			
			info.ButtonId = DBConvert.GetString(rdr, rdr.GetOrdinal("BUTTON_ID"));			
			info.Comments = DBConvert.GetString(rdr, rdr.GetOrdinal("COMMENTS"));			
			info.UpdateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("UPDATE_DATE"));			
			info.UpdateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("UPDATE_USER"));			
			info.CreateDate = DBConvert.GetDateTime(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			return info;
		}
		
		#endregion
	}
}