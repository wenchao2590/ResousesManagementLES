#region Declaim
//---------------------------------------------------------------------------
// Name:		ButtonDAL
// Function: 	Expose data in table TM_EPS_BUTTON from database as business object to MES system.
// Tool:		T4
// CreateDate:	2017年12月22日
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
    /// ButtonDAL对应表[TM_EPS_BUTTON]
    /// </summary>
    public partial class ButtonDAL : BusinessObjectProvider<ButtonInfo>
	{
		#region Sql Statements
		private const string TM_EPS_BUTTON_SELECT_BY_ID =
			@"SELECT PLANT,
				ASSEMBLY_LINE,
				PLANT_ZONE,
				WORKSHOP,
				BUTTON_ID,
				BUTTON_TYPE,
				BUTTON_STATE,
				UNLOCK_TIME,
				LOCK_TIME,
				COMMENTS,
				CREATE_DATE,
				UPDATE_USER,
				CREATE_USER,
				UPDATE_DATE				  
				FROM [LES].[TM_EPS_BUTTON] WITH(NOLOCK) WHERE 1=1  AND PLANT =@PLANT AND ASSEMBLY_LINE =@ASSEMBLY_LINE AND BUTTON_ID =@BUTTON_ID;";
			
		private const string TM_EPS_BUTTON_SELECT = 
			@"SELECT PLANT,
				ASSEMBLY_LINE,
				PLANT_ZONE,
				WORKSHOP,
				BUTTON_ID,
				BUTTON_TYPE,
				BUTTON_STATE,
				UNLOCK_TIME,
				LOCK_TIME,
				COMMENTS,
				CREATE_DATE,
				UPDATE_USER,
				CREATE_USER,
				UPDATE_DATE				 
				FROM [LES].[TM_EPS_BUTTON] WITH (NOLOCK) WHERE 1=1 {0};";
		
		private const string TM_EPS_BUTTON_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TM_EPS_BUTTON]  WITH(NOLOCK) WHERE 1=1 {0};";

		private const string TM_EPS_BUTTON_INSERT =
			@"INSERT INTO [LES].[TM_EPS_BUTTON] (
				PLANT,
				ASSEMBLY_LINE,
				PLANT_ZONE,
				WORKSHOP,
				BUTTON_ID,
				BUTTON_TYPE,
				BUTTON_STATE,
				UNLOCK_TIME,
				LOCK_TIME,
				COMMENTS,
				CREATE_DATE,
				UPDATE_USER,
				CREATE_USER,
				UPDATE_DATE				 
			) VALUES (
				@PLANT,
				@ASSEMBLY_LINE,
				@PLANT_ZONE,
				@WORKSHOP,
				@BUTTON_ID,
				@BUTTON_TYPE,
				@BUTTON_STATE,
				@UNLOCK_TIME,
				@LOCK_TIME,
				@COMMENTS,
				@CREATE_DATE,
				@UPDATE_USER,
				@CREATE_USER,
				@UPDATE_DATE				 
			);";
		private const string TM_EPS_BUTTON_UPDATE =
			@"UPDATE [LES].[TM_EPS_BUTTON] WITH(ROWLOCK) 
				SET PLANT=@PLANT,
				ASSEMBLY_LINE=@ASSEMBLY_LINE,
				PLANT_ZONE=@PLANT_ZONE,
				WORKSHOP=@WORKSHOP,
				BUTTON_ID=@BUTTON_ID,
				BUTTON_TYPE=@BUTTON_TYPE,
				BUTTON_STATE=@BUTTON_STATE,
				UNLOCK_TIME=@UNLOCK_TIME,
				LOCK_TIME=@LOCK_TIME,
				COMMENTS=@COMMENTS,
				CREATE_DATE=@CREATE_DATE,
				UPDATE_USER=@UPDATE_USER,
				CREATE_USER=@CREATE_USER,
				UPDATE_DATE=@UPDATE_DATE				 
				WHERE 1=1  AND PLANT =@PLANT AND ASSEMBLY_LINE =@ASSEMBLY_LINE AND BUTTON_ID =@BUTTON_ID;";

		private const string TM_EPS_BUTTON_DELETE =
			@"DELETE FROM [LES].[TM_EPS_BUTTON] WITH(ROWLOCK)  
				WHERE 1=1  AND PLANT =@PLANT AND ASSEMBLY_LINE =@ASSEMBLY_LINE AND BUTTON_ID =@BUTTON_ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get ButtonInfo
		/// </summary>
		/// <param name="BUTTON_ID">ButtonInfo Primary key </param>
		/// <returns></returns> 
		public ButtonInfo GetInfo(string aPlant,
				string aAssemblyLine,
				string aButtonId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_EPS_BUTTON_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, aPlant);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, aAssemblyLine);
			db.AddInParameter(dbCommand, "@BUTTON_ID", DbType.String, aButtonId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateButtonInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>ButtonInfo Collection </returns>
		public List<ButtonInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TM_EPS_BUTTON_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>ButtonInfo Collection </returns>
		public List<ButtonInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<ButtonInfo> list = new List<ButtonInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateButtonInfo(dr));
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
		public List<ButtonInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                textOrder += "[BUTTON_ID] desc";
            string sql = "select top " + pageRow + " * from "
                + "(select row_number() over(order by " + textOrder + ") as rownumber"
                + ",* from [LES].[TM_EPS_BUTTON]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<ButtonInfo> list = new List<ButtonInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateButtonInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TM_EPS_BUTTON_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public bool Add(ButtonInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TM_EPS_BUTTON_INSERT);			
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@PLANT_ZONE", DbType.String, info.PlantZone);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@BUTTON_ID", DbType.String, info.ButtonId);
			db.AddInParameter(dbCommand, "@BUTTON_TYPE", DbType.Int32, info.ButtonType);
			db.AddInParameter(dbCommand, "@BUTTON_STATE", DbType.Int32, info.ButtonState);
			db.AddInParameter(dbCommand, "@UNLOCK_TIME", DbType.Int32, info.UnlockTime);
			db.AddInParameter(dbCommand, "@LOCK_TIME", DbType.DateTime, info.LockTime);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@UPDATE_USER", DbType.String, info.UpdateUser);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@UPDATE_DATE", DbType.DateTime, info.UpdateDate);
			return db.ExecuteNonQuery(dbCommand) > 0 ? true : false;		
		}		
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(ButtonInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_EPS_BUTTON_UPDATE);				
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@PLANT_ZONE", DbType.String, info.PlantZone);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@BUTTON_ID", DbType.String, info.ButtonId);
			db.AddInParameter(dbCommand, "@BUTTON_TYPE", DbType.Int32, info.ButtonType);
			db.AddInParameter(dbCommand, "@BUTTON_STATE", DbType.Int32, info.ButtonState);
			db.AddInParameter(dbCommand, "@UNLOCK_TIME", DbType.Int32, info.UnlockTime);
			db.AddInParameter(dbCommand, "@LOCK_TIME", DbType.DateTime, info.LockTime);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@UPDATE_USER", DbType.String, info.UpdateUser);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@UPDATE_DATE", DbType.DateTime, info.UpdateDate);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="BUTTON_ID">ButtonInfo Primary key </param>
		/// <returns></returns>
		public int Delete(string aPlant,
				string aAssemblyLine,
				string aButtonId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_EPS_BUTTON_DELETE);
		    db.AddInParameter(dbCommand, "@PLANT", DbType.String, aPlant);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, aAssemblyLine);
			db.AddInParameter(dbCommand, "@BUTTON_ID", DbType.String, aButtonId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}
		/// <summary>
		/// UpdateInfo
		/// </summary>
		/// <param name="BUTTON_ID">ButtonInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,string aPlant,
				string aAssemblyLine,
				string aButtonId)
		{
		    string sql = "update [LES].[TM_EPS_BUTTON] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE 1=1  AND PLANT =@PLANT AND ASSEMBLY_LINE =@ASSEMBLY_LINE AND BUTTON_ID =@BUTTON_ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, aPlant);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, aAssemblyLine);
			db.AddInParameter(dbCommand, "@BUTTON_ID", DbType.String, aButtonId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static ButtonInfo CreateButtonInfo(IDataReader rdr)
		{
			ButtonInfo info = new ButtonInfo();
			info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));			
			info.AssemblyLine = DBConvert.GetString(rdr, rdr.GetOrdinal("ASSEMBLY_LINE"));			
			info.PlantZone = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT_ZONE"));			
			info.Workshop = DBConvert.GetString(rdr, rdr.GetOrdinal("WORKSHOP"));			
			info.ButtonId = DBConvert.GetString(rdr, rdr.GetOrdinal("BUTTON_ID"));			
			info.ButtonType = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("BUTTON_TYPE"));			
			info.ButtonState = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("BUTTON_STATE"));			
			info.UnlockTime = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("UNLOCK_TIME"));			
			info.LockTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("LOCK_TIME"));			
			info.Comments = DBConvert.GetString(rdr, rdr.GetOrdinal("COMMENTS"));			
			info.CreateDate = DBConvert.GetDateTime(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.UpdateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("UPDATE_USER"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.UpdateDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("UPDATE_DATE"));			
			return info;
		}
		
		#endregion
	}
}
