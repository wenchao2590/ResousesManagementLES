﻿
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
    /// InventoryOrderPartDAL对应表[TT_PCM_INVENTORY_ORDER_PART]
    /// </summary>
    public partial class PCMInventoryOrderPartDAL : BusinessObjectProvider<PCMInventoryOrderPartInfo>
    {
        #region Sql Statements
        private const string TT_PCM_INVENTORY_ORDER_PART_SELECT_BY_ID =
            @"SELECT ID,
				FID,
				ORDER_CODE,
				ORDER_FID,
				WM_NO,
				ZONE_NO,
				DLOC,
				PART_NO,
				SUPPLIER_NUM,
				PACKAGE_MODEL,
				PACKAGE,
				PART_CNAME,
				PACKAGE_QTY,
				PART_QTY,
				REFERENCE_QTY,
				DIFFERENCE_QTY,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				  
				FROM [LES].[TT_PCM_INVENTORY_ORDER_PART] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";

        private const string TT_PCM_INVENTORY_ORDER_PART_SELECT =
            @"SELECT ID,
				FID,
				ORDER_CODE,
				ORDER_FID,
				WM_NO,
				ZONE_NO,
				DLOC,
				PART_NO,
				SUPPLIER_NUM,
				PACKAGE_MODEL,
				PACKAGE,
				PART_CNAME,
				PACKAGE_QTY,
				PART_QTY,
				REFERENCE_QTY,
				DIFFERENCE_QTY,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
				FROM [LES].[TT_PCM_INVENTORY_ORDER_PART] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

        private const string TT_PCM_INVENTORY_ORDER_PART_SELECT_COUNTS =
            @"SELECT count(*) FROM [LES].[TT_PCM_INVENTORY_ORDER_PART]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

        private const string TT_PCM_INVENTORY_ORDER_PART_INSERT =
            @"INSERT INTO [LES].[TT_PCM_INVENTORY_ORDER_PART] (
				FID,
				ORDER_CODE,
				ORDER_FID,
				WM_NO,
				ZONE_NO,
				DLOC,
				PART_NO,
				SUPPLIER_NUM,
				PACKAGE_MODEL,
				PACKAGE,
				PART_CNAME,
				PACKAGE_QTY,
				PART_QTY,
				REFERENCE_QTY,
				DIFFERENCE_QTY,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
			) VALUES (
				@FID,
				@ORDER_CODE,
				@ORDER_FID,
				@WM_NO,
				@ZONE_NO,
				@DLOC,
				@PART_NO,
				@SUPPLIER_NUM,
				@PACKAGE_MODEL,
				@PACKAGE,
				@PART_CNAME,
				@PACKAGE_QTY,
				@PART_QTY,
				@REFERENCE_QTY,
				@DIFFERENCE_QTY,
				@COMMENTS,
				@VALID_FLAG,
				GETDATE(),
				@CREATE_USER,
				@MODIFY_DATE,
				@MODIFY_USER				 
			);SELECT @@IDENTITY;";
        private const string TT_PCM_INVENTORY_ORDER_PART_UPDATE =
            @"UPDATE [LES].[TT_PCM_INVENTORY_ORDER_PART] WITH(ROWLOCK) 
				SET FID=@FID,
				ORDER_CODE=@ORDER_CODE,
				ORDER_FID=@ORDER_FID,
				WM_NO=@WM_NO,
				ZONE_NO=@ZONE_NO,
				DLOC=@DLOC,
				PART_NO=@PART_NO,
				SUPPLIER_NUM=@SUPPLIER_NUM,
				PACKAGE_MODEL=@PACKAGE_MODEL,
				PACKAGE=@PACKAGE,
				PART_CNAME=@PART_CNAME,
				PACKAGE_QTY=@PACKAGE_QTY,
				PART_QTY=@PART_QTY,
				REFERENCE_QTY=@REFERENCE_QTY,
				DIFFERENCE_QTY=@DIFFERENCE_QTY,
				COMMENTS=@COMMENTS,
				VALID_FLAG=@VALID_FLAG,
				CREATE_DATE=@CREATE_DATE,
				CREATE_USER=@CREATE_USER,
				MODIFY_DATE=@MODIFY_DATE,
				MODIFY_USER=@MODIFY_USER				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

        private const string TT_PCM_INVENTORY_ORDER_PART_DELETE =
            @"DELETE FROM [LES].[TT_PCM_INVENTORY_ORDER_PART] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
        #endregion

        #region Access Methods

        /// <summary>
        /// Get PCMInventoryOrderPartInfo
        /// </summary>
        /// <param name="ID">PCMInventoryOrderPartInfo Primary key </param>
        /// <returns></returns> 
        public PCMInventoryOrderPartInfo GetInfo(long aId)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(TT_PCM_INVENTORY_ORDER_PART_SELECT_BY_ID);
            db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                if (dr.Read())
                    return CreatePCMInventoryOrderPartInfo(dr);
            }
            return null;
        }
        /// <summary>
        /// Get data collection
        /// </summary>
        /// <param name="textWhere">Conditon</param>
        /// <param name="orderText">Sort</param>
        /// <returns>PCMInventoryOrderPartInfo Collection </returns>
        public List<PCMInventoryOrderPartInfo> GetList(string textWhere, string orderText)
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

            return GetList(string.Format(TT_PCM_INVENTORY_ORDER_PART_SELECT, query));
        }
        /// <summary>
        /// Get data collection
        /// </summary>
        /// <param name="sql">SQL Statement</param>
        /// <returns>PCMInventoryOrderPartInfo Collection </returns>
        public List<PCMInventoryOrderPartInfo> GetList(string sql)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<PCMInventoryOrderPartInfo> list = new List<PCMInventoryOrderPartInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreatePCMInventoryOrderPartInfo(dr));
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
        public List<PCMInventoryOrderPartInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TT_PCM_INVENTORY_ORDER_PART]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<PCMInventoryOrderPartInfo> list = new List<PCMInventoryOrderPartInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreatePCMInventoryOrderPartInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TT_PCM_INVENTORY_ORDER_PART_SELECT_COUNTS, textWhere));
            return Convert.ToInt32(db.ExecuteScalar(cmd));
        }
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="info"> info</param>
        public long Add(PCMInventoryOrderPartInfo info)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(TT_PCM_INVENTORY_ORDER_PART_INSERT);
            db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
            db.AddInParameter(dbCommand, "@ORDER_CODE", DbType.String, info.OrderCode);
            db.AddInParameter(dbCommand, "@ORDER_FID", DbType.Guid, info.OrderFid);
            db.AddInParameter(dbCommand, "@WM_NO", DbType.String, info.WmNo);
            db.AddInParameter(dbCommand, "@ZONE_NO", DbType.String, info.ZoneNo);
            db.AddInParameter(dbCommand, "@DLOC", DbType.String, info.Dloc);
            db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
            db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
            db.AddInParameter(dbCommand, "@PACKAGE_MODEL", DbType.String, info.PackageModel);
            db.AddInParameter(dbCommand, "@PACKAGE", DbType.Decimal, info.Package);
            db.AddInParameter(dbCommand, "@PART_CNAME", DbType.String, info.PartCname);
            db.AddInParameter(dbCommand, "@PACKAGE_QTY", DbType.Int32, info.PackageQty);
            db.AddInParameter(dbCommand, "@PART_QTY", DbType.Decimal, info.PartQty);
            db.AddInParameter(dbCommand, "@REFERENCE_QTY", DbType.Decimal, info.ReferenceQty);
            db.AddInParameter(dbCommand, "@DIFFERENCE_QTY", DbType.Decimal, info.DifferenceQty);
            db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
            db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
            db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
            db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
            db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
            db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
            return long.Parse("0" + db.ExecuteScalar(dbCommand));
        }
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="info">info</param>
        public int Update(PCMInventoryOrderPartInfo info)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(TT_PCM_INVENTORY_ORDER_PART_UPDATE);
            db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
            db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
            db.AddInParameter(dbCommand, "@ORDER_CODE", DbType.String, info.OrderCode);
            db.AddInParameter(dbCommand, "@ORDER_FID", DbType.Guid, info.OrderFid);
            db.AddInParameter(dbCommand, "@WM_NO", DbType.String, info.WmNo);
            db.AddInParameter(dbCommand, "@ZONE_NO", DbType.String, info.ZoneNo);
            db.AddInParameter(dbCommand, "@DLOC", DbType.String, info.Dloc);
            db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
            db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
            db.AddInParameter(dbCommand, "@PACKAGE_MODEL", DbType.String, info.PackageModel);
            db.AddInParameter(dbCommand, "@PACKAGE", DbType.Decimal, info.Package);
            db.AddInParameter(dbCommand, "@PART_CNAME", DbType.String, info.PartCname);
            db.AddInParameter(dbCommand, "@PACKAGE_QTY", DbType.Int32, info.PackageQty);
            db.AddInParameter(dbCommand, "@PART_QTY", DbType.Decimal, info.PartQty);
            db.AddInParameter(dbCommand, "@REFERENCE_QTY", DbType.Decimal, info.ReferenceQty);
            db.AddInParameter(dbCommand, "@DIFFERENCE_QTY", DbType.Decimal, info.DifferenceQty);
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
        /// <param name="ID">PCMInventoryOrderPartInfo Primary key </param>
        /// <returns></returns>
        public int Delete(long aId)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(TT_PCM_INVENTORY_ORDER_PART_DELETE);
            db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
            return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
        }/// <summary>
         /// LogciDelete
         /// </summary>
         /// <param name="ID">PCMInventoryOrderPartInfo Primary key </param>
         /// <returns></returns>
        public int LogicDelete(long aId, string loginUser)
        {
            string sql = "update [LES].[TT_PCM_INVENTORY_ORDER_PART] WITH(ROWLOCK) "
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
        /// <param name="ID">PCMInventoryOrderPartInfo Primary key </param>
        /// <returns></returns>
        public int UpdateInfo(string fields, long aId)
        {
            string sql = "update [LES].[TT_PCM_INVENTORY_ORDER_PART] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
            return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
        }
        #endregion

        #region Helpers   

        private static PCMInventoryOrderPartInfo CreatePCMInventoryOrderPartInfo(IDataReader rdr)
        {
            PCMInventoryOrderPartInfo info = new PCMInventoryOrderPartInfo();
            info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));
            info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));
            info.OrderCode = DBConvert.GetString(rdr, rdr.GetOrdinal("ORDER_CODE"));
            info.OrderFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("ORDER_FID"));
            info.WmNo = DBConvert.GetString(rdr, rdr.GetOrdinal("WM_NO"));
            info.ZoneNo = DBConvert.GetString(rdr, rdr.GetOrdinal("ZONE_NO"));
            info.Dloc = DBConvert.GetString(rdr, rdr.GetOrdinal("DLOC"));
            info.PartNo = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_NO"));
            info.SupplierNum = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NUM"));
            info.PackageModel = DBConvert.GetString(rdr, rdr.GetOrdinal("PACKAGE_MODEL"));
            info.Package = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("PACKAGE"));
            info.PartCname = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_CNAME"));
            info.PackageQty = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PACKAGE_QTY"));
            info.PartQty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("PART_QTY"));
            info.ReferenceQty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("REFERENCE_QTY"));
            info.DifferenceQty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("DIFFERENCE_QTY"));
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
