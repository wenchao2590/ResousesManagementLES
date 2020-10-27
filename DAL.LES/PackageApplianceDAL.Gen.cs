#region Declaim
//---------------------------------------------------------------------------
// Name:		PackageApplianceDAL
// Function: 	Expose data in table TM_BAS_PACKAGE_APPLIANCE from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年5月22日
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
    /// PackageApplianceDAL对应表[TM_BAS_PACKAGE_APPLIANCE]
    /// </summary>
    public partial class PackageApplianceDAL : BusinessObjectProvider<PackageApplianceInfo>
	{
		#region Sql Statements
		private const string TM_BAS_PACKAGE_APPLIANCE_SELECT_BY_ID =
			@"SELECT ID,
				FID,
				PACKAGE_NO,
				PLANT,
				ASSEMBLY_LINE,
				PLANT_ZONE,
				WORKSHOP,
				SUPPLIER_NUM,
				TRANS_SUPPLIER_NUM,
				PACKAGE_CNAME,
				PACKAGE_ENAME,
				BOX_TYPE,
				PACKAGE_TYPE,
				IS_REPEAT,
				IS_GENERAL,
				MATERIAL,
				BOX_WEIGHT,
				USE_SCHOPE,
				EFFECTIVE_DATE,
				EXPIRE_DATE,
				MAX_HIGH,
				MAX_WEIGHT,
				MAX_LOAD_WEIGHT,
				MAX_LOAD_NUM,
				LAYER_BOX_NUM,
				STACKING_NUM,
				LOGICAL_PK,
				STORAGE_LOCATION,
				IS_ACTIVE,
				PACKAGE_RESOURCE,
				TOTAL_PACKAGE_QTY,
				PACKAGE_LENGTH,
				PACKAGE_WIDTH,
				PACKAGE_HEIGHT,
				COMMENTS,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE				  
				FROM [LES].[TM_BAS_PACKAGE_APPLIANCE] WITH(NOLOCK) WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			
		private const string TM_BAS_PACKAGE_APPLIANCE_SELECT = 
			@"SELECT ID,
				FID,
				PACKAGE_NO,
				PLANT,
				ASSEMBLY_LINE,
				PLANT_ZONE,
				WORKSHOP,
				SUPPLIER_NUM,
				TRANS_SUPPLIER_NUM,
				PACKAGE_CNAME,
				PACKAGE_ENAME,
				BOX_TYPE,
				PACKAGE_TYPE,
				IS_REPEAT,
				IS_GENERAL,
				MATERIAL,
				BOX_WEIGHT,
				USE_SCHOPE,
				EFFECTIVE_DATE,
				EXPIRE_DATE,
				MAX_HIGH,
				MAX_WEIGHT,
				MAX_LOAD_WEIGHT,
				MAX_LOAD_NUM,
				LAYER_BOX_NUM,
				STACKING_NUM,
				LOGICAL_PK,
				STORAGE_LOCATION,
				IS_ACTIVE,
				PACKAGE_RESOURCE,
				TOTAL_PACKAGE_QTY,
				PACKAGE_LENGTH,
				PACKAGE_WIDTH,
				PACKAGE_HEIGHT,
				COMMENTS,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE				 
				FROM [LES].[TM_BAS_PACKAGE_APPLIANCE] WITH (NOLOCK) WHERE [VALID_FLAG] = 1 {0};";
		
		private const string TM_BAS_PACKAGE_APPLIANCE_SELECT_COUNTS = 
			@"SELECT count(*) FROM [LES].[TM_BAS_PACKAGE_APPLIANCE]  WITH(NOLOCK) WHERE [VALID_FLAG] = 1 {0};";

		private const string TM_BAS_PACKAGE_APPLIANCE_INSERT =
			@"INSERT INTO [LES].[TM_BAS_PACKAGE_APPLIANCE] (
				FID,
				PACKAGE_NO,
				PLANT,
				ASSEMBLY_LINE,
				PLANT_ZONE,
				WORKSHOP,
				SUPPLIER_NUM,
				TRANS_SUPPLIER_NUM,
				PACKAGE_CNAME,
				PACKAGE_ENAME,
				BOX_TYPE,
				PACKAGE_TYPE,
				IS_REPEAT,
				IS_GENERAL,
				MATERIAL,
				BOX_WEIGHT,
				USE_SCHOPE,
				EFFECTIVE_DATE,
				EXPIRE_DATE,
				MAX_HIGH,
				MAX_WEIGHT,
				MAX_LOAD_WEIGHT,
				MAX_LOAD_NUM,
				LAYER_BOX_NUM,
				STACKING_NUM,
				LOGICAL_PK,
				STORAGE_LOCATION,
				IS_ACTIVE,
				PACKAGE_RESOURCE,
				TOTAL_PACKAGE_QTY,
				PACKAGE_LENGTH,
				PACKAGE_WIDTH,
				PACKAGE_HEIGHT,
				COMMENTS,
				VALID_FLAG,
				CREATE_USER,
				CREATE_DATE,
				MODIFY_USER,
				MODIFY_DATE				 
			) VALUES (
				@FID,
				@PACKAGE_NO,
				@PLANT,
				@ASSEMBLY_LINE,
				@PLANT_ZONE,
				@WORKSHOP,
				@SUPPLIER_NUM,
				@TRANS_SUPPLIER_NUM,
				@PACKAGE_CNAME,
				@PACKAGE_ENAME,
				@BOX_TYPE,
				@PACKAGE_TYPE,
				@IS_REPEAT,
				@IS_GENERAL,
				@MATERIAL,
				@BOX_WEIGHT,
				@USE_SCHOPE,
				@EFFECTIVE_DATE,
				@EXPIRE_DATE,
				@MAX_HIGH,
				@MAX_WEIGHT,
				@MAX_LOAD_WEIGHT,
				@MAX_LOAD_NUM,
				@LAYER_BOX_NUM,
				@STACKING_NUM,
				@LOGICAL_PK,
				@STORAGE_LOCATION,
				@IS_ACTIVE,
				@PACKAGE_RESOURCE,
				@TOTAL_PACKAGE_QTY,
				@PACKAGE_LENGTH,
				@PACKAGE_WIDTH,
				@PACKAGE_HEIGHT,
				@COMMENTS,
				@VALID_FLAG,
				@CREATE_USER,
				GETDATE(),
				@MODIFY_USER,
				@MODIFY_DATE				 
			);SELECT @@IDENTITY;";
		private const string TM_BAS_PACKAGE_APPLIANCE_UPDATE =
			@"UPDATE [LES].[TM_BAS_PACKAGE_APPLIANCE] WITH(ROWLOCK) 
				SET FID=@FID,
				PACKAGE_NO=@PACKAGE_NO,
				PLANT=@PLANT,
				ASSEMBLY_LINE=@ASSEMBLY_LINE,
				PLANT_ZONE=@PLANT_ZONE,
				WORKSHOP=@WORKSHOP,
				SUPPLIER_NUM=@SUPPLIER_NUM,
				TRANS_SUPPLIER_NUM=@TRANS_SUPPLIER_NUM,
				PACKAGE_CNAME=@PACKAGE_CNAME,
				PACKAGE_ENAME=@PACKAGE_ENAME,
				BOX_TYPE=@BOX_TYPE,
				PACKAGE_TYPE=@PACKAGE_TYPE,
				IS_REPEAT=@IS_REPEAT,
				IS_GENERAL=@IS_GENERAL,
				MATERIAL=@MATERIAL,
				BOX_WEIGHT=@BOX_WEIGHT,
				USE_SCHOPE=@USE_SCHOPE,
				EFFECTIVE_DATE=@EFFECTIVE_DATE,
				EXPIRE_DATE=@EXPIRE_DATE,
				MAX_HIGH=@MAX_HIGH,
				MAX_WEIGHT=@MAX_WEIGHT,
				MAX_LOAD_WEIGHT=@MAX_LOAD_WEIGHT,
				MAX_LOAD_NUM=@MAX_LOAD_NUM,
				LAYER_BOX_NUM=@LAYER_BOX_NUM,
				STACKING_NUM=@STACKING_NUM,
				LOGICAL_PK=@LOGICAL_PK,
				STORAGE_LOCATION=@STORAGE_LOCATION,
				IS_ACTIVE=@IS_ACTIVE,
				PACKAGE_RESOURCE=@PACKAGE_RESOURCE,
				TOTAL_PACKAGE_QTY=@TOTAL_PACKAGE_QTY,
				PACKAGE_LENGTH=@PACKAGE_LENGTH,
				PACKAGE_WIDTH=@PACKAGE_WIDTH,
				PACKAGE_HEIGHT=@PACKAGE_HEIGHT,
				COMMENTS=@COMMENTS,
				VALID_FLAG=@VALID_FLAG,
				CREATE_USER=@CREATE_USER,
				CREATE_DATE=@CREATE_DATE,
				MODIFY_USER=@MODIFY_USER,
				MODIFY_DATE=@MODIFY_DATE				 
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";

		private const string TM_BAS_PACKAGE_APPLIANCE_DELETE =
			@"DELETE FROM [LES].[TM_BAS_PACKAGE_APPLIANCE] WITH(ROWLOCK)  
				WHERE [VALID_FLAG] = 1  AND ID =@ID;";
		#endregion
		 
		#region Access Methods
		 
		/// <summary>
		/// Get PackageApplianceInfo
		/// </summary>
		/// <param name="ID">PackageApplianceInfo Primary key </param>
		/// <returns></returns> 
		public PackageApplianceInfo GetInfo(long aId)
		{	
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_BAS_PACKAGE_APPLIANCE_SELECT_BY_ID);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				if (dr.Read())
					return CreatePackageApplianceInfo(dr);
			}
			return null;
		}
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>PackageApplianceInfo Collection </returns>
		public List<PackageApplianceInfo> GetList(string textWhere,string orderText)
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
			
			return GetList(string.Format(TM_BAS_PACKAGE_APPLIANCE_SELECT, query));
		}		
		/// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="sql">SQL Statement</param>
		/// <returns>PackageApplianceInfo Collection </returns>
		public List<PackageApplianceInfo> GetList(string sql)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			List<PackageApplianceInfo> list = new List<PackageApplianceInfo>();
			using (IDataReader dr = db.ExecuteReader(dbCommand))
			{
				while (dr.Read())
				{
					list.Add(CreatePackageApplianceInfo(dr));
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
		public List<PackageApplianceInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TM_BAS_PACKAGE_APPLIANCE]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<PackageApplianceInfo> list = new List<PackageApplianceInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreatePackageApplianceInfo(dr));
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
            DbCommand cmd = db.GetSqlStringCommand(string.Format(TM_BAS_PACKAGE_APPLIANCE_SELECT_COUNTS, textWhere));
			return Convert.ToInt32(db.ExecuteScalar(cmd));            
        }
		/// <summary>
		/// Add
		/// </summary>
		/// <param name="info"> info</param>
		public long Add(PackageApplianceInfo info)
		{ 
			Database db = DatabaseFactory.CreateDatabase();	
			DbCommand dbCommand = db.GetSqlStringCommand(TM_BAS_PACKAGE_APPLIANCE_INSERT);			
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@PACKAGE_NO", DbType.String, info.PackageNo);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@PLANT_ZONE", DbType.String, info.PlantZone);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@TRANS_SUPPLIER_NUM", DbType.String, info.TransSupplierNum);
			db.AddInParameter(dbCommand, "@PACKAGE_CNAME", DbType.String, info.PackageCname);
			db.AddInParameter(dbCommand, "@PACKAGE_ENAME", DbType.String, info.PackageEname);
			db.AddInParameter(dbCommand, "@BOX_TYPE", DbType.String, info.BoxType);
			db.AddInParameter(dbCommand, "@PACKAGE_TYPE", DbType.Int32, info.PackageType);
			db.AddInParameter(dbCommand, "@IS_REPEAT", DbType.Boolean, info.IsRepeat);
			db.AddInParameter(dbCommand, "@IS_GENERAL", DbType.Boolean, info.IsGeneral);
			db.AddInParameter(dbCommand, "@MATERIAL", DbType.Int32, info.Material);
			db.AddInParameter(dbCommand, "@BOX_WEIGHT", DbType.Decimal, info.BoxWeight);
			db.AddInParameter(dbCommand, "@USE_SCHOPE", DbType.Int32, info.UseSchope);
			db.AddInParameter(dbCommand, "@EFFECTIVE_DATE", DbType.DateTime, info.EffectiveDate);
			db.AddInParameter(dbCommand, "@EXPIRE_DATE", DbType.DateTime, info.ExpireDate);
			db.AddInParameter(dbCommand, "@MAX_HIGH", DbType.Decimal, info.MaxHigh);
			db.AddInParameter(dbCommand, "@MAX_WEIGHT", DbType.Decimal, info.MaxWeight);
			db.AddInParameter(dbCommand, "@MAX_LOAD_WEIGHT", DbType.Decimal, info.MaxLoadWeight);
			db.AddInParameter(dbCommand, "@MAX_LOAD_NUM", DbType.Decimal, info.MaxLoadNum);
			db.AddInParameter(dbCommand, "@LAYER_BOX_NUM", DbType.Int32, info.LayerBoxNum);
			db.AddInParameter(dbCommand, "@STACKING_NUM", DbType.Int32, info.StackingNum);
			db.AddInParameter(dbCommand, "@LOGICAL_PK", DbType.String, info.LogicalPk);
			db.AddInParameter(dbCommand, "@STORAGE_LOCATION", DbType.String, info.StorageLocation);
			db.AddInParameter(dbCommand, "@IS_ACTIVE", DbType.Boolean, info.IsActive);
			db.AddInParameter(dbCommand, "@PACKAGE_RESOURCE", DbType.Int32, info.PackageResource);
			db.AddInParameter(dbCommand, "@TOTAL_PACKAGE_QTY", DbType.Int32, info.TotalPackageQty);
			db.AddInParameter(dbCommand, "@PACKAGE_LENGTH", DbType.Decimal, info.PackageLength);
			db.AddInParameter(dbCommand, "@PACKAGE_WIDTH", DbType.Decimal, info.PackageWidth);
			db.AddInParameter(dbCommand, "@PACKAGE_HEIGHT", DbType.Decimal, info.PackageHeight);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			return long.Parse("0" + db.ExecuteScalar(dbCommand));		
		}		
		/// <summary>
		/// Update
		/// </summary>
		/// <param name="info">info</param>
		public int Update(PackageApplianceInfo info)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_BAS_PACKAGE_APPLIANCE_UPDATE);				
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, info.Id);
			db.AddInParameter(dbCommand, "@FID", DbType.Guid, info.Fid);
			db.AddInParameter(dbCommand, "@PACKAGE_NO", DbType.String, info.PackageNo);
			db.AddInParameter(dbCommand, "@PLANT", DbType.String, info.Plant);
			db.AddInParameter(dbCommand, "@ASSEMBLY_LINE", DbType.String, info.AssemblyLine);
			db.AddInParameter(dbCommand, "@PLANT_ZONE", DbType.String, info.PlantZone);
			db.AddInParameter(dbCommand, "@WORKSHOP", DbType.String, info.Workshop);
			db.AddInParameter(dbCommand, "@SUPPLIER_NUM", DbType.String, info.SupplierNum);
			db.AddInParameter(dbCommand, "@TRANS_SUPPLIER_NUM", DbType.String, info.TransSupplierNum);
			db.AddInParameter(dbCommand, "@PACKAGE_CNAME", DbType.String, info.PackageCname);
			db.AddInParameter(dbCommand, "@PACKAGE_ENAME", DbType.String, info.PackageEname);
			db.AddInParameter(dbCommand, "@BOX_TYPE", DbType.String, info.BoxType);
			db.AddInParameter(dbCommand, "@PACKAGE_TYPE", DbType.Int32, info.PackageType);
			db.AddInParameter(dbCommand, "@IS_REPEAT", DbType.Boolean, info.IsRepeat);
			db.AddInParameter(dbCommand, "@IS_GENERAL", DbType.Boolean, info.IsGeneral);
			db.AddInParameter(dbCommand, "@MATERIAL", DbType.Int32, info.Material);
			db.AddInParameter(dbCommand, "@BOX_WEIGHT", DbType.Decimal, info.BoxWeight);
			db.AddInParameter(dbCommand, "@USE_SCHOPE", DbType.Int32, info.UseSchope);
			db.AddInParameter(dbCommand, "@EFFECTIVE_DATE", DbType.DateTime, info.EffectiveDate);
			db.AddInParameter(dbCommand, "@EXPIRE_DATE", DbType.DateTime, info.ExpireDate);
			db.AddInParameter(dbCommand, "@MAX_HIGH", DbType.Decimal, info.MaxHigh);
			db.AddInParameter(dbCommand, "@MAX_WEIGHT", DbType.Decimal, info.MaxWeight);
			db.AddInParameter(dbCommand, "@MAX_LOAD_WEIGHT", DbType.Decimal, info.MaxLoadWeight);
			db.AddInParameter(dbCommand, "@MAX_LOAD_NUM", DbType.Decimal, info.MaxLoadNum);
			db.AddInParameter(dbCommand, "@LAYER_BOX_NUM", DbType.Int32, info.LayerBoxNum);
			db.AddInParameter(dbCommand, "@STACKING_NUM", DbType.Int32, info.StackingNum);
			db.AddInParameter(dbCommand, "@LOGICAL_PK", DbType.String, info.LogicalPk);
			db.AddInParameter(dbCommand, "@STORAGE_LOCATION", DbType.String, info.StorageLocation);
			db.AddInParameter(dbCommand, "@IS_ACTIVE", DbType.Boolean, info.IsActive);
			db.AddInParameter(dbCommand, "@PACKAGE_RESOURCE", DbType.Int32, info.PackageResource);
			db.AddInParameter(dbCommand, "@TOTAL_PACKAGE_QTY", DbType.Int32, info.TotalPackageQty);
			db.AddInParameter(dbCommand, "@PACKAGE_LENGTH", DbType.Decimal, info.PackageLength);
			db.AddInParameter(dbCommand, "@PACKAGE_WIDTH", DbType.Decimal, info.PackageWidth);
			db.AddInParameter(dbCommand, "@PACKAGE_HEIGHT", DbType.Decimal, info.PackageHeight);
			db.AddInParameter(dbCommand, "@COMMENTS", DbType.String, info.Comments);
			db.AddInParameter(dbCommand, "@VALID_FLAG", DbType.Boolean, info.ValidFlag);
			db.AddInParameter(dbCommand, "@CREATE_USER", DbType.String, info.CreateUser);
			db.AddInParameter(dbCommand, "@CREATE_DATE", DbType.DateTime, info.CreateDate);
			db.AddInParameter(dbCommand, "@MODIFY_USER", DbType.String, info.ModifyUser);
			db.AddInParameter(dbCommand, "@MODIFY_DATE", DbType.DateTime, info.ModifyDate);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="ID">PackageApplianceInfo Primary key </param>
		/// <returns></returns>
		public int Delete(long aId)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(TM_BAS_PACKAGE_APPLIANCE_DELETE);
		    db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}/// <summary>
		/// LogciDelete
		/// </summary>
		/// <param name="ID">PackageApplianceInfo Primary key </param>
		/// <returns></returns>
		public int LogicDelete(long aId, string loginUser)
		{
		    string sql = "update [LES].[TM_BAS_PACKAGE_APPLIANCE] WITH(ROWLOCK) "
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
		/// <param name="ID">PackageApplianceInfo Primary key </param>
		/// <returns></returns>
		public int UpdateInfo(string fields ,long aId)
		{
		    string sql = "update [LES].[TM_BAS_PACKAGE_APPLIANCE] WITH(ROWLOCK) "
                + "set " + fields
                + "WHERE [VALID_FLAG] = 1  AND ID =@ID;";
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "@ID", DbType.Int64, aId);
			return int.Parse("0" + db.ExecuteNonQuery(dbCommand));
		}		
		#endregion
		  
		#region Helpers   
	  
		private static PackageApplianceInfo CreatePackageApplianceInfo(IDataReader rdr)
		{
			PackageApplianceInfo info = new PackageApplianceInfo();
			info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));			
			info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));			
			info.PackageNo = DBConvert.GetString(rdr, rdr.GetOrdinal("PACKAGE_NO"));			
			info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));			
			info.AssemblyLine = DBConvert.GetString(rdr, rdr.GetOrdinal("ASSEMBLY_LINE"));			
			info.PlantZone = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT_ZONE"));			
			info.Workshop = DBConvert.GetString(rdr, rdr.GetOrdinal("WORKSHOP"));			
			info.SupplierNum = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NUM"));			
			info.TransSupplierNum = DBConvert.GetString(rdr, rdr.GetOrdinal("TRANS_SUPPLIER_NUM"));			
			info.PackageCname = DBConvert.GetString(rdr, rdr.GetOrdinal("PACKAGE_CNAME"));			
			info.PackageEname = DBConvert.GetString(rdr, rdr.GetOrdinal("PACKAGE_ENAME"));			
			info.BoxType = DBConvert.GetString(rdr, rdr.GetOrdinal("BOX_TYPE"));			
			info.PackageType = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PACKAGE_TYPE"));			
			info.IsRepeat = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("IS_REPEAT"));			
			info.IsGeneral = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("IS_GENERAL"));			
			info.Material = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("MATERIAL"));			
			info.BoxWeight = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("BOX_WEIGHT"));			
			info.UseSchope = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("USE_SCHOPE"));			
			info.EffectiveDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("EFFECTIVE_DATE"));			
			info.ExpireDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("EXPIRE_DATE"));			
			info.MaxHigh = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("MAX_HIGH"));			
			info.MaxWeight = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("MAX_WEIGHT"));			
			info.MaxLoadWeight = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("MAX_LOAD_WEIGHT"));			
			info.MaxLoadNum = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("MAX_LOAD_NUM"));			
			info.LayerBoxNum = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("LAYER_BOX_NUM"));			
			info.StackingNum = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("STACKING_NUM"));			
			info.LogicalPk = DBConvert.GetString(rdr, rdr.GetOrdinal("LOGICAL_PK"));			
			info.StorageLocation = DBConvert.GetString(rdr, rdr.GetOrdinal("STORAGE_LOCATION"));			
			info.IsActive = DBConvert.GetBoolNullable(rdr, rdr.GetOrdinal("IS_ACTIVE"));			
			info.PackageResource = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("PACKAGE_RESOURCE"));			
			info.TotalPackageQty = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("TOTAL_PACKAGE_QTY"));			
			info.PackageLength = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("PACKAGE_LENGTH"));			
			info.PackageWidth = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("PACKAGE_WIDTH"));			
			info.PackageHeight = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("PACKAGE_HEIGHT"));			
			info.Comments = DBConvert.GetString(rdr, rdr.GetOrdinal("COMMENTS"));			
			info.ValidFlag = DBConvert.GetBool(rdr, rdr.GetOrdinal("VALID_FLAG"));			
			info.CreateUser = DBConvert.GetString(rdr, rdr.GetOrdinal("CREATE_USER"));			
			info.CreateDate = DBConvert.GetDateTime(rdr, rdr.GetOrdinal("CREATE_DATE"));			
			info.ModifyUser = DBConvert.GetString(rdr, rdr.GetOrdinal("MODIFY_USER"));			
			info.ModifyDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("MODIFY_DATE"));			
			return info;
		}
		
		#endregion
	}
}
