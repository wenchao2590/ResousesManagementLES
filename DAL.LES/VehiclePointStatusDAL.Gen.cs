#region Declaim
//---------------------------------------------------------------------------
// Name:		VehiclePointStatusDAL
// Function: 	Expose data in table TT_BAS_VEHICLE_POINT_STATUS from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月11日
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
    /// VehiclePointStatusDAL对应表[TT_BAS_VEHICLE_POINT_STATUS]
    /// </summary>
    public partial class VehiclePointStatusDAL : BusinessObjectProvider<VehiclePointStatusInfo>
	{
		#region Sql Statements
		private const string TT_BAS_VEHICLE_POINT_STATUS_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				ORDER_NO,
				STATUS_POINT_CODE,
				PLANT,
				WORKSHOP,
				ASSEMBLY_LINE,
				SPJ,
				KNR,
				PASS_TIME,
				SCHICHT,
				VEHICLE_STATUS,
				SHIFT,
				VIN,
				RUNNING_NO,
				SEQ_NO,
				MODEL_NO,
				COMMENTS,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				  
				FROM [LES].[TT_BAS_VEHICLE_POINT_STATUS] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TT_BAS_VEHICLE_POINT_STATUS_SELECT = 
			@"SELECT ID,
				FID,
				ORDER_NO,
				STATUS_POINT_CODE,
				PLANT,
				WORKSHOP,
				ASSEMBLY_LINE,
				SPJ,
				KNR,
				PASS_TIME,
				SCHICHT,
				VEHICLE_STATUS,
				SHIFT,
				VIN,
				RUNNING_NO,
				SEQ_NO,
				MODEL_NO,
				COMMENTS,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
				FROM [LES].[TT_BAS_VEHICLE_POINT_STATUS] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TT_BAS_VEHICLE_POINT_STATUS_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TT_BAS_VEHICLE_POINT_STATUS]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TT_BAS_VEHICLE_POINT_STATUS_INSERT =
			@"INSERT INTO [LES].[TT_BAS_VEHICLE_POINT_STATUS] (
				FID,
				ORDER_NO,
				STATUS_POINT_CODE,
				PLANT,
				WORKSHOP,
				ASSEMBLY_LINE,
				SPJ,
				KNR,
				PASS_TIME,
				SCHICHT,
				VEHICLE_STATUS,
				SHIFT,
				VIN,
				RUNNING_NO,
				SEQ_NO,
				MODEL_NO,
				COMMENTS,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
			) VALUES (
				@FID,
				@ORDER_NO,
				@STATUS_POINT_CODE,
				@PLANT,
				@WORKSHOP,
				@ASSEMBLY_LINE,
				@SPJ,
				@KNR,
				@PASS_TIME,
				@SCHICHT,
				@VEHICLE_STATUS,
				@SHIFT,
				@VIN,
				@RUNNING_NO,
				@SEQ_NO,
				@MODEL_NO,
				@COMMENTS,
				@PROCESS_FLAG,
				@PROCESS_TIME,
				@VALID_FLAG,
				GETDATE(),
				@CREATE_USER,
				@MODIFY_DATE,
				@MODIFY_USER				 
			);SELECT @@IDENTITY;";
		private const string TT_BAS_VEHICLE_POINT_STATUS_UPDATE =
			@"UPDATE [LES].[TT_BAS_VEHICLE_POINT_STATUS] WITH(ROWLOCK) 
				SET FID=@FID,
				ORDER_NO=@ORDER_NO,
				STATUS_POINT_CODE=@STATUS_POINT_CODE,
				PLANT=@PLANT,
				WORKSHOP=@WORKSHOP,
				ASSEMBLY_LINE=@ASSEMBLY_LINE,
				SPJ=@SPJ,
				KNR=@KNR,
				PASS_TIME=@PASS_TIME,
				SCHICHT=@SCHICHT,
				VEHICLE_STATUS=@VEHICLE_STATUS,
				SHIFT=@SHIFT,
				VIN=@VIN,
				RUNNING_NO=@RUNNING_NO,
				SEQ_NO=@SEQ_NO,
				MODEL_NO=@MODEL_NO,
				COMMENTS=@COMMENTS,
				PROCESS_FLAG=@PROCESS_FLAG,
				PROCESS_TIME=@PROCESS_TIME,
				VALID_FLAG=@VALID_FLAG,
				CREATE_DATE=@CREATE_DATE,
				CREATE_USER=@CREATE_USER,
				MODIFY_DATE=@MODIFY_DATE,
				MODIFY_USER=@MODIFY_USER				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TT_BAS_VEHICLE_POINT_STATUS_DELETE =
			@"DELETE FROM [LES].[TT_BAS_VEHICLE_POINT_STATUS] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get VehiclePointStatusInfo
		/// </summary>
		/// <param name="ID">VehiclePointStatusInfo Primary key </param>
		/// <returns></returns> 
		public VehiclePointStatusInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_BAS_VEHICLE_POINT_STATUS_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateVehiclePointStatusInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>VehiclePointStatusInfo Collection </returns>
		public List<VehiclePointStatusInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TT_BAS_VEHICLE_POINT_STATUS_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>VehiclePointStatusInfo Collection </returns>
		public List<VehiclePointStatusInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<VehiclePointStatusInfo> list = new List<VehiclePointStatusInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateVehiclePointStatusInfo(dr));
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
		public List<VehiclePointStatusInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TT_BAS_VEHICLE_POINT_STATUS]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<VehiclePointStatusInfo> list = new List<VehiclePointStatusInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateVehiclePointStatusInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TT_BAS_VEHICLE_POINT_STATUS_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(VehiclePointStatusInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TT_BAS_VEHICLE_POINT_STATUS_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@ORDER_NO", DbType.String, info.OrderNo);
			db.AddInParameter(dbCommand, "@STATUS_POINT_CODE", DbType.String, info.StatusPointCode);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@SPJ", DbType.String, info.Spj);
			db.AddInParameter(dbCommand, "@KNR", DbType.String, info.Knr);
			db.AddInParameter(dbCommand, "@PASS_TIME", DbType.DateTime, info.PassTime);
			db.AddInParameter(dbCommand, "@SCHICHT", DbType.String, info.Schicht);
			db.AddInParameter(dbCommand, "@VEHICLE_STATUS", DbType.Int32, info.VehicleStatus);
			db.AddInParameter(dbCommand, "@SHIFT", DbType.Int32, info.Shift);
			db.AddInParameter(dbCommand, "@VIN", DbType.String, info.Vin);
			db.AddInParameter(dbCommand, "@RUNNING_NO", DbType.String, info.RunningNo);
			db.AddInParameter(dbCommand, "@SEQ_NO", DbType.Int64, info.SeqNo);
			db.AddInParameter(dbCommand, "@MODEL_NO", DbType.String, info.ModelNo);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@PROCESS_FLAG", DbType.Int32, info.ProcessFlag);
			db.AddInParameter(dbCommand, "@PROCESS_TIME", DbType.DateTime, info.ProcessTime);
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
		public static string GetInsertSql(VehiclePointStatusInfo info)
		{
			return  
			@"insert into [LES].[TT_BAS_VEHICLE_POINT_STATUS] (
				FID,
				ORDER_NO,
				STATUS_POINT_CODE,
				PLANT,
				WORKSHOP,
				ASSEMBLY_LINE,
				SPJ,
				KNR,
				PASS_TIME,
				SCHICHT,
				VEHICLE_STATUS,
				SHIFT,
				VIN,
				RUNNING_NO,
				SEQ_NO,
				MODEL_NO,
				COMMENTS,
				PROCESS_FLAG,
				PROCESS_TIME,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
			) values ("+
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.OrderNo) ? "NULL" : "N'" + info.OrderNo + "'") + ","+
				(string.IsNullOrEmpty(info.StatusPointCode) ? "NULL" : "N'" + info.StatusPointCode + "'") + ","+
				(string.IsNullOrEmpty(info.Plant) ? "NULL" : "N'" + info.Plant + "'") + ","+
				(string.IsNullOrEmpty(info.Workshop) ? "NULL" : "N'" + info.Workshop + "'") + ","+
				(string.IsNullOrEmpty(info.AssemblyLine) ? "NULL" : "N'" + info.AssemblyLine + "'") + ","+
				(string.IsNullOrEmpty(info.Spj) ? "NULL" : "N'" + info.Spj + "'") + ","+
				(string.IsNullOrEmpty(info.Knr) ? "NULL" : "N'" + info.Knr + "'") + ","+
				(info.PassTime == null ? "NULL" : "N'" + info.PassTime.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.Schicht) ? "NULL" : "N'" + info.Schicht + "'") + ","+
				(info.VehicleStatus == null ? "NULL" : "" + info.VehicleStatus.GetValueOrDefault() + "") + ","+
				(info.Shift == null ? "NULL" : "" + info.Shift.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.Vin) ? "NULL" : "N'" + info.Vin + "'") + ","+
				(string.IsNullOrEmpty(info.RunningNo) ? "NULL" : "N'" + info.RunningNo + "'") + ","+
				(info.SeqNo == null ? "NULL" : "" + info.SeqNo.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.ModelNo) ? "NULL" : "N'" + info.ModelNo + "'") + ","+
				(string.IsNullOrEmpty(info.Comments) ? "NULL" : "N'" + info.Comments + "'") + ","+
				(info.ProcessFlag == null ? "NULL" : "" + info.ProcessFlag.GetValueOrDefault() + "") + ","+
				(info.ProcessTime == null ? "NULL" : "N'" + info.ProcessTime.GetValueOrDefault() + "'") + ","+
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
		public int Update(VehiclePointStatusInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_BAS_VEHICLE_POINT_STATUS_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@ORDER_NO", DbType.String, info.OrderNo);
			db.AddInParameter(dbCommand, "@STATUS_POINT_CODE", DbType.String, info.StatusPointCode);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@SPJ", DbType.String, info.Spj);
			db.AddInParameter(dbCommand, "@KNR", DbType.String, info.Knr);
			db.AddInParameter(dbCommand, "@PASS_TIME", DbType.DateTime, info.PassTime);
			db.AddInParameter(dbCommand, "@SCHICHT", DbType.String, info.Schicht);
			db.AddInParameter(dbCommand, "@VEHICLE_STATUS", DbType.Int32, info.VehicleStatus);
			db.AddInParameter(dbCommand, "@SHIFT", DbType.Int32, info.Shift);
			db.AddInParameter(dbCommand, "@VIN", DbType.String, info.Vin);
			db.AddInParameter(dbCommand, "@RUNNING_NO", DbType.String, info.RunningNo);
			db.AddInParameter(dbCommand, "@SEQ_NO", DbType.Int64, info.SeqNo);
			db.AddInParameter(dbCommand, "@MODEL_NO", DbType.String, info.ModelNo);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@PROCESS_FLAG", DbType.Int32, info.ProcessFlag);
			db.AddInParameter(dbCommand, "@PROCESS_TIME", DbType.DateTime, info.ProcessTime);
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
		/// <param name="ID">VehiclePointStatusInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_BAS_VEHICLE_POINT_STATUS_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">VehiclePointStatusInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TT_BAS_VEHICLE_POINT_STATUS] WITH(ROWLOCK) "
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
		/// <param name="ID">VehiclePointStatusInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TT_BAS_VEHICLE_POINT_STATUS] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static VehiclePointStatusInfo CreateVehiclePointStatusInfo(IDataReader rdr)
		{
			VehiclePointStatusInfo info = new VehiclePointStatusInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.OrderNo = DBConvert.GetString(rdr, rdr.GetOrdinal("ORDER_NO"));			
			info.StatusPointCode = DBConvert.GetString(rdr, rdr.GetOrdinal("STATUS_POINT_CODE"));			
			info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));			
			info.Workshop = DBConvert.GetString(rdr, rdr.GetOrdinal("WORKSHOP"));			
			info.AssemblyLine = DBConvert.GetString(rdr, rdr.GetOrdinal("ASSEMBLY_LINE"));			
			info.Spj = DBConvert.GetString(rdr, rdr.GetOrdinal("SPJ"));			
			info.Knr = DBConvert.GetString(rdr, rdr.GetOrdinal("KNR"));			
			info.PassTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("PASS_TIME"));			
			info.Schicht = DBConvert.GetString(rdr, rdr.GetOrdinal("SCHICHT"));			
			info.VehicleStatus = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("VEHICLE_STATUS"));			
			info.Shift = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("SHIFT"));			
			info.Vin = DBConvert.GetString(rdr, rdr.GetOrdinal("VIN"));			
			info.RunningNo = DBConvert.GetString(rdr, rdr.GetOrdinal("RUNNING_NO"));			
			info.SeqNo = DBConvert.GetInt64Nullable(rdr, rdr.GetOrdinal("SEQ_NO"));			
			info.ModelNo = DBConvert.GetString(rdr, rdr.GetOrdinal("MODEL_NO"));			
			info.Comments = DBConvert.GetString(rdr, rdr.GetOrdinal("COMMENTS"));			
			info.ProcessFlag = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PROCESS_FLAG"));			
			info.ProcessTime = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("PROCESS_TIME"));			
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
