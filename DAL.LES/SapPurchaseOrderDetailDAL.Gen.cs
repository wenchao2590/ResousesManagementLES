#region Declaim
//---------------------------------------------------------------------------
// Name:		SapPurchaseOrderDetailDAL
// Function: 	Expose data in table TT_MPM_SAP_PURCHASE_ORDER_DETAIL from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年7月10日
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
    /// SapPurchaseOrderDetailDAL对应表[TT_MPM_SAP_PURCHASE_ORDER_DETAIL]
    /// </summary>
    public partial class SapPurchaseOrderDetailDAL : BusinessObjectProvider<SapPurchaseOrderDetailInfo>
	{
		#region Sql Statements
		private const string TT_MPM_SAP_PURCHASE_ORDER_DETAIL_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				ORDER_FID,
				PART_NO,
				PART_CNAME,
				PART_ENAME,
				PART_QTY,
				REQUIRE_PACKAGE_QTY,
				PACKAGE,
				PACKAGE_MODEL,
				PART_PURCHASE_UOM,
				PART_UOM,
				SAP_MENGE,
				SAP_RSNUM,
				SAP_RSPOS,
				SAP_EBELN,
				SAP_EBELP,
				SAP_BWART,
				SAP_KOSTL,
				SAP_LGORT,
				SAP_UMLGO,
				SAP_WEMPF,
				SAP_LIFNR,
				STATUS,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				  
				FROM [LES].[TT_MPM_SAP_PURCHASE_ORDER_DETAIL] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TT_MPM_SAP_PURCHASE_ORDER_DETAIL_SELECT = 
			@"SELECT ID,
				FID,
				ORDER_FID,
				PART_NO,
				PART_CNAME,
				PART_ENAME,
				PART_QTY,
				REQUIRE_PACKAGE_QTY,
				PACKAGE,
				PACKAGE_MODEL,
				PART_PURCHASE_UOM,
				PART_UOM,
				SAP_MENGE,
				SAP_RSNUM,
				SAP_RSPOS,
				SAP_EBELN,
				SAP_EBELP,
				SAP_BWART,
				SAP_KOSTL,
				SAP_LGORT,
				SAP_UMLGO,
				SAP_WEMPF,
				SAP_LIFNR,
				STATUS,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
				FROM [LES].[TT_MPM_SAP_PURCHASE_ORDER_DETAIL] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TT_MPM_SAP_PURCHASE_ORDER_DETAIL_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TT_MPM_SAP_PURCHASE_ORDER_DETAIL]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TT_MPM_SAP_PURCHASE_ORDER_DETAIL_INSERT =
			@"INSERT INTO [LES].[TT_MPM_SAP_PURCHASE_ORDER_DETAIL] (
				FID,
				ORDER_FID,
				PART_NO,
				PART_CNAME,
				PART_ENAME,
				PART_QTY,
				REQUIRE_PACKAGE_QTY,
				PACKAGE,
				PACKAGE_MODEL,
				PART_PURCHASE_UOM,
				PART_UOM,
				SAP_MENGE,
				SAP_RSNUM,
				SAP_RSPOS,
				SAP_EBELN,
				SAP_EBELP,
				SAP_BWART,
				SAP_KOSTL,
				SAP_LGORT,
				SAP_UMLGO,
				SAP_WEMPF,
				SAP_LIFNR,
				STATUS,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
			) VALUES (
				@FID,
				@ORDER_FID,
				@PART_NO,
				@PART_CNAME,
				@PART_ENAME,
				@PART_QTY,
				@REQUIRE_PACKAGE_QTY,
				@PACKAGE,
				@PACKAGE_MODEL,
				@PART_PURCHASE_UOM,
				@PART_UOM,
				@SAP_MENGE,
				@SAP_RSNUM,
				@SAP_RSPOS,
				@SAP_EBELN,
				@SAP_EBELP,
				@SAP_BWART,
				@SAP_KOSTL,
				@SAP_LGORT,
				@SAP_UMLGO,
				@SAP_WEMPF,
				@SAP_LIFNR,
				@STATUS,
				@COMMENTS,
				@VALID_FLAG,
				GETDATE(),
				@CREATE_USER,
				@MODIFY_DATE,
				@MODIFY_USER				 
			);SELECT @@IDENTITY;";
		private const string TT_MPM_SAP_PURCHASE_ORDER_DETAIL_UPDATE =
			@"UPDATE [LES].[TT_MPM_SAP_PURCHASE_ORDER_DETAIL] WITH(ROWLOCK) 
				SET FID=@FID,
				ORDER_FID=@ORDER_FID,
				PART_NO=@PART_NO,
				PART_CNAME=@PART_CNAME,
				PART_ENAME=@PART_ENAME,
				PART_QTY=@PART_QTY,
				REQUIRE_PACKAGE_QTY=@REQUIRE_PACKAGE_QTY,
				PACKAGE=@PACKAGE,
				PACKAGE_MODEL=@PACKAGE_MODEL,
				PART_PURCHASE_UOM=@PART_PURCHASE_UOM,
				PART_UOM=@PART_UOM,
				SAP_MENGE=@SAP_MENGE,
				SAP_RSNUM=@SAP_RSNUM,
				SAP_RSPOS=@SAP_RSPOS,
				SAP_EBELN=@SAP_EBELN,
				SAP_EBELP=@SAP_EBELP,
				SAP_BWART=@SAP_BWART,
				SAP_KOSTL=@SAP_KOSTL,
				SAP_LGORT=@SAP_LGORT,
				SAP_UMLGO=@SAP_UMLGO,
				SAP_WEMPF=@SAP_WEMPF,
				SAP_LIFNR=@SAP_LIFNR,
				STATUS=@STATUS,
				COMMENTS=@COMMENTS,
				VALID_FLAG=@VALID_FLAG,
				CREATE_DATE=@CREATE_DATE,
				CREATE_USER=@CREATE_USER,
				MODIFY_DATE=@MODIFY_DATE,
				MODIFY_USER=@MODIFY_USER				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TT_MPM_SAP_PURCHASE_ORDER_DETAIL_DELETE =
			@"DELETE FROM [LES].[TT_MPM_SAP_PURCHASE_ORDER_DETAIL] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get SapPurchaseOrderDetailInfo
		/// </summary>
		/// <param name="ID">SapPurchaseOrderDetailInfo Primary key </param>
		/// <returns></returns> 
		public SapPurchaseOrderDetailInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_MPM_SAP_PURCHASE_ORDER_DETAIL_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreateSapPurchaseOrderDetailInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>SapPurchaseOrderDetailInfo Collection </returns>
		public List<SapPurchaseOrderDetailInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TT_MPM_SAP_PURCHASE_ORDER_DETAIL_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>SapPurchaseOrderDetailInfo Collection </returns>
		public List<SapPurchaseOrderDetailInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<SapPurchaseOrderDetailInfo> list = new List<SapPurchaseOrderDetailInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreateSapPurchaseOrderDetailInfo(dr));
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
		public List<SapPurchaseOrderDetailInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TT_MPM_SAP_PURCHASE_ORDER_DETAIL]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<SapPurchaseOrderDetailInfo> list = new List<SapPurchaseOrderDetailInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateSapPurchaseOrderDetailInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TT_MPM_SAP_PURCHASE_ORDER_DETAIL_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(SapPurchaseOrderDetailInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TT_MPM_SAP_PURCHASE_ORDER_DETAIL_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@ORDER_FID", DbType.Guid, info.OrderFid);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@PART_CNAME", DbType.String, info.PartCname);
			db.AddInParameter(dbCommand, "@PART_ENAME", DbType.String, info.PartEname);
			db.AddInParameter(dbCommand, "@PART_QTY", DbType.Decimal, info.PartQty);
			db.AddInParameter(dbCommand, "@REQUIRE_PACKAGE_QTY", DbType.Int32, info.RequirePackageQty);
			db.AddInParameter(dbCommand, "@PACKAGE", DbType.Decimal, info.Package);
			db.AddInParameter(dbCommand, "@PACKAGE_MODEL", DbType.String, info.PackageModel);
			db.AddInParameter(dbCommand, "@PART_PURCHASE_UOM", DbType.String, info.PartPurchaseUom);
			db.AddInParameter(dbCommand, "@PART_UOM", DbType.String, info.PartUom);
			db.AddInParameter(dbCommand, "@SAP_MENGE", DbType.Decimal, info.SapMenge);
			db.AddInParameter(dbCommand, "@SAP_RSNUM", DbType.String, info.SapRsnum);
			db.AddInParameter(dbCommand, "@SAP_RSPOS", DbType.Int32, info.SapRspos);
			db.AddInParameter(dbCommand, "@SAP_EBELN", DbType.String, info.SapEbeln);
			db.AddInParameter(dbCommand, "@SAP_EBELP", DbType.Int32, info.SapEbelp);
			db.AddInParameter(dbCommand, "@SAP_BWART", DbType.String, info.SapBwart);
			db.AddInParameter(dbCommand, "@SAP_KOSTL", DbType.String, info.SapKostl);
			db.AddInParameter(dbCommand, "@SAP_LGORT", DbType.String, info.SapLgort);
			db.AddInParameter(dbCommand, "@SAP_UMLGO", DbType.String, info.SapUmlgo);
			db.AddInParameter(dbCommand, "@SAP_WEMPF", DbType.String, info.SapWempf);
			db.AddInParameter(dbCommand, "@SAP_LIFNR", DbType.String, info.SapLifnr);
			db.AddInParameter(dbCommand, "@STATUS", DbType.Int32, info.Status);
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
		public static string GetInsertSql(SapPurchaseOrderDetailInfo info)
		{
			return  
			@"insert into [LES].[TT_MPM_SAP_PURCHASE_ORDER_DETAIL] (
				FID,
				ORDER_FID,
				PART_NO,
				PART_CNAME,
				PART_ENAME,
				PART_QTY,
				REQUIRE_PACKAGE_QTY,
				PACKAGE,
				PACKAGE_MODEL,
				PART_PURCHASE_UOM,
				PART_UOM,
				SAP_MENGE,
				SAP_RSNUM,
				SAP_RSPOS,
				SAP_EBELN,
				SAP_EBELP,
				SAP_BWART,
				SAP_KOSTL,
				SAP_LGORT,
				SAP_UMLGO,
				SAP_WEMPF,
				SAP_LIFNR,
				STATUS,
				COMMENTS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
			) values ("+
				(info.Fid == null ? "NEWID()" : "N'" + info.Fid.GetValueOrDefault() + "'") + ","+
				(info.OrderFid == null ? "NULL" : "N'" + info.OrderFid.GetValueOrDefault() + "'") + ","+
				(string.IsNullOrEmpty(info.PartNo) ? "NULL" : "N'" + info.PartNo + "'") + ","+
				(string.IsNullOrEmpty(info.PartCname) ? "NULL" : "N'" + info.PartCname + "'") + ","+
				(string.IsNullOrEmpty(info.PartEname) ? "NULL" : "N'" + info.PartEname + "'") + ","+
				(info.PartQty == null ? "NULL" : "" + info.PartQty.GetValueOrDefault() + "") + ","+
				(info.RequirePackageQty == null ? "NULL" : "" + info.RequirePackageQty.GetValueOrDefault() + "") + ","+
				(info.Package == null ? "NULL" : "" + info.Package.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.PackageModel) ? "NULL" : "N'" + info.PackageModel + "'") + ","+
				(string.IsNullOrEmpty(info.PartPurchaseUom) ? "NULL" : "N'" + info.PartPurchaseUom + "'") + ","+
				(string.IsNullOrEmpty(info.PartUom) ? "NULL" : "N'" + info.PartUom + "'") + ","+
				(info.SapMenge == null ? "NULL" : "" + info.SapMenge.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.SapRsnum) ? "NULL" : "N'" + info.SapRsnum + "'") + ","+
				(info.SapRspos == null ? "NULL" : "" + info.SapRspos.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.SapEbeln) ? "NULL" : "N'" + info.SapEbeln + "'") + ","+
				(info.SapEbelp == null ? "NULL" : "" + info.SapEbelp.GetValueOrDefault() + "") + ","+
				(string.IsNullOrEmpty(info.SapBwart) ? "NULL" : "N'" + info.SapBwart + "'") + ","+
				(string.IsNullOrEmpty(info.SapKostl) ? "NULL" : "N'" + info.SapKostl + "'") + ","+
				(string.IsNullOrEmpty(info.SapLgort) ? "NULL" : "N'" + info.SapLgort + "'") + ","+
				(string.IsNullOrEmpty(info.SapUmlgo) ? "NULL" : "N'" + info.SapUmlgo + "'") + ","+
				(string.IsNullOrEmpty(info.SapWempf) ? "NULL" : "N'" + info.SapWempf + "'") + ","+
				(string.IsNullOrEmpty(info.SapLifnr) ? "NULL" : "N'" + info.SapLifnr + "'") + ","+
				(info.Status == null ? "NULL" : "" + info.Status.GetValueOrDefault() + "") + ","+
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
		public int Update(SapPurchaseOrderDetailInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_MPM_SAP_PURCHASE_ORDER_DETAIL_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@ORDER_FID", DbType.Guid, info.OrderFid);
			db.AddInParameter(dbCommand, "@PART_NO", DbType.String, info.PartNo);
			db.AddInParameter(dbCommand, "@PART_CNAME", DbType.String, info.PartCname);
			db.AddInParameter(dbCommand, "@PART_ENAME", DbType.String, info.PartEname);
			db.AddInParameter(dbCommand, "@PART_QTY", DbType.Decimal, info.PartQty);
			db.AddInParameter(dbCommand, "@REQUIRE_PACKAGE_QTY", DbType.Int32, info.RequirePackageQty);
			db.AddInParameter(dbCommand, "@PACKAGE", DbType.Decimal, info.Package);
			db.AddInParameter(dbCommand, "@PACKAGE_MODEL", DbType.String, info.PackageModel);
			db.AddInParameter(dbCommand, "@PART_PURCHASE_UOM", DbType.String, info.PartPurchaseUom);
			db.AddInParameter(dbCommand, "@PART_UOM", DbType.String, info.PartUom);
			db.AddInParameter(dbCommand, "@SAP_MENGE", DbType.Decimal, info.SapMenge);
			db.AddInParameter(dbCommand, "@SAP_RSNUM", DbType.String, info.SapRsnum);
			db.AddInParameter(dbCommand, "@SAP_RSPOS", DbType.Int32, info.SapRspos);
			db.AddInParameter(dbCommand, "@SAP_EBELN", DbType.String, info.SapEbeln);
			db.AddInParameter(dbCommand, "@SAP_EBELP", DbType.Int32, info.SapEbelp);
			db.AddInParameter(dbCommand, "@SAP_BWART", DbType.String, info.SapBwart);
			db.AddInParameter(dbCommand, "@SAP_KOSTL", DbType.String, info.SapKostl);
			db.AddInParameter(dbCommand, "@SAP_LGORT", DbType.String, info.SapLgort);
			db.AddInParameter(dbCommand, "@SAP_UMLGO", DbType.String, info.SapUmlgo);
			db.AddInParameter(dbCommand, "@SAP_WEMPF", DbType.String, info.SapWempf);
			db.AddInParameter(dbCommand, "@SAP_LIFNR", DbType.String, info.SapLifnr);
			db.AddInParameter(dbCommand, "@STATUS", DbType.Int32, info.Status);
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
		/// <param name="ID">SapPurchaseOrderDetailInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TT_MPM_SAP_PURCHASE_ORDER_DETAIL_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">SapPurchaseOrderDetailInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TT_MPM_SAP_PURCHASE_ORDER_DETAIL] WITH(ROWLOCK) "
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
		/// <param name="ID">SapPurchaseOrderDetailInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TT_MPM_SAP_PURCHASE_ORDER_DETAIL] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static SapPurchaseOrderDetailInfo CreateSapPurchaseOrderDetailInfo(IDataReader rdr)
		{
			SapPurchaseOrderDetailInfo info = new SapPurchaseOrderDetailInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.OrderFid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("ORDER_FID"));			
			info.PartNo = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_NO"));			
			info.PartCname = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_CNAME"));			
			info.PartEname = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_ENAME"));			
			info.PartQty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("PART_QTY"));			
			info.RequirePackageQty = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("REQUIRE_PACKAGE_QTY"));			
			info.Package = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("PACKAGE"));			
			info.PackageModel = DBConvert.GetString(rdr, rdr.GetOrdinal("PACKAGE_MODEL"));			
			info.PartPurchaseUom = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_PURCHASE_UOM"));			
			info.PartUom = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_UOM"));			
			info.SapMenge = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("SAP_MENGE"));			
			info.SapRsnum = DBConvert.GetString(rdr, rdr.GetOrdinal("SAP_RSNUM"));			
			info.SapRspos = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("SAP_RSPOS"));			
			info.SapEbeln = DBConvert.GetString(rdr, rdr.GetOrdinal("SAP_EBELN"));			
			info.SapEbelp = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("SAP_EBELP"));			
			info.SapBwart = DBConvert.GetString(rdr, rdr.GetOrdinal("SAP_BWART"));			
			info.SapKostl = DBConvert.GetString(rdr, rdr.GetOrdinal("SAP_KOSTL"));			
			info.SapLgort = DBConvert.GetString(rdr, rdr.GetOrdinal("SAP_LGORT"));			
			info.SapUmlgo = DBConvert.GetString(rdr, rdr.GetOrdinal("SAP_UMLGO"));			
			info.SapWempf = DBConvert.GetString(rdr, rdr.GetOrdinal("SAP_WEMPF"));			
			info.SapLifnr = DBConvert.GetString(rdr, rdr.GetOrdinal("SAP_LIFNR"));			
			info.Status = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("STATUS"));			
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
