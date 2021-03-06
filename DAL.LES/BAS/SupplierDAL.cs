﻿#region Declaim
//---------------------------------------------------------------------------
// Name:		SupplierDAL
// Function: 	Expose data in table TM_BAS_SUPPLIER from database as business object to MES system.
// Tool:		T4
// CreateDate:	2018年4月10日
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
    /// SupplierDAL对应表[TM_BAS_SUPPLIER]
    /// </summary>
    public partial class SupplierDAL
    {
        /// <summary>
        /// 根据供应商代码获取供应商名称
        /// </summary>
        /// <param name="supplierNum"></param>
        /// <returns></returns>
        public string GetSupplierName(string supplierNum)
        {
            string sql = "select [SUPPLIER_NAME] from [LES].[TM_BAS_SUPPLIER] with(nolock) where [VALID_FLAG] = 1 and [SUPPLIER_NUM] = @SUPPLIER_NUM;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@SUPPLIER_NUM", DbType.AnsiString, supplierNum);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return string.Empty;
            return result.ToString();
        }
        /// <summary>
        /// 根据供应商代码获取主键
        /// </summary>
        /// <param name="supplierNum"></param>
        /// <returns></returns>
        public long GetSupplierId(string supplierNum)
        {
            string sql = "select [ID] from [LES].[TM_BAS_SUPPLIER] with(nolock) where [VALID_FLAG] = 1 and [SUPPLIER_NUM] = @SUPPLIER_NUM;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@SUPPLIER_NUM", DbType.AnsiString, supplierNum);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return 0;
            return Convert.ToInt64(result);
        }
        /// <summary>
        /// 根据供应商代码获取供应商类型
        /// </summary>
        /// <param name="supplierNum"></param>
        /// <returns></returns>
        public int GetSupplierType(string supplierNum)
        {
            string sql = "select [SUPPLIER_TYPE] from [LES].[TM_BAS_SUPPLIER] with(nolock) " +
                "where [VALID_FLAG] = 1 and [SUPPLIER_NUM] = @SUPPLIER_NUM;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@SUPPLIER_NUM", DbType.AnsiString, supplierNum);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return 0;
            return Convert.ToInt32(result);
        }

        /// <summary>
        /// 获取ASN标识
        /// </summary>
        /// <param name="supplierNum"></param>
        /// <returns></returns>
        public bool GetAsnFlag(string supplierNum)
        {
            string sql = "select [ASN_FLAG] from [LES].[TM_BAS_SUPPLIER] where [VALID_FLAG] = 1 and [SUPPLIER_NUM] = @SUPPLIER_NUM;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@SUPPLIER_NUM", DbType.AnsiString, supplierNum);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return false;
            return Convert.ToBoolean(result);
        }

        /// <summary>
        /// 根据供应商代码获取供应商信息
        /// </summary>
        /// <param name="supplierNum"></param>
        /// <returns></returns>
        public SupplierInfo GetSupplierInfo(string supplierNum)
        {
            string sql = "select * from [LES].[TM_BAS_SUPPLIER] with(nolock) where [VALID_FLAG] = 1 and [SUPPLIER_NUM] = @SUPPLIER_NUM;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@SUPPLIER_NUM", DbType.AnsiString, supplierNum);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                    return CreateSupplierInfo(dr);
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierNums"></param>
        /// <returns></returns>
        public List<SupplierInfo> GetListForInterfaceDataSync(List<string> supplierNums)
        {
            string sql = "select [ID],[FID],[SUPPLIER_NUM],[SUPPLIER_NAME],[SUPPLIER_TYPE],[SUPPLIER_SNAME] "
                + "from [LES].[TM_BAS_SUPPLIER] with(nolock) "
                + "where [VALID_FLAG] = 1 and [SUPPLIER_NUM] in ('" + string.Join("','", supplierNums.ToArray()) + "');";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            List<SupplierInfo> list = new List<SupplierInfo>();
            using (IDataReader rdr = db.ExecuteReader(cmd))
            {
                while (rdr.Read())
                {
                    SupplierInfo info = new SupplierInfo();
                    info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));
                    info.Fid = DBConvert.GetGuid(rdr, rdr.GetOrdinal("FID"));
                    info.SupplierNum = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NUM"));
                    info.SupplierSname = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_SNAME"));
                    info.SupplierName = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NAME"));
                    info.SupplierType = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("SUPPLIER_TYPE"));
                    list.Add(info);
                }
            }
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierNums"></param>
        /// <returns></returns>
        public SupplierInfo GetInfoForInterfaceDataSync(string supplierNum)
        {
            string sql = "select [ID],[FID],[SUPPLIER_NUM],[SUPPLIER_NAME],[SUPPLIER_TYPE] "
                + "from [LES].[TM_BAS_SUPPLIER] with(nolock) "
                + "where [VALID_FLAG] = 1 and [SUPPLIER_NUM] =@SUPPLIER_NUM;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@SUPPLIER_NUM", DbType.AnsiString, supplierNum);
            using (IDataReader rdr = db.ExecuteReader(cmd))
            {
                if (rdr.Read())
                {
                    SupplierInfo info = new SupplierInfo();
                    info.Id = DBConvert.GetInt64(rdr, rdr.GetOrdinal("ID"));
                    info.Fid = DBConvert.GetGuid(rdr, rdr.GetOrdinal("FID"));
                    info.SupplierNum = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NUM"));
                    info.SupplierName = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NAME"));
                    info.SupplierType = DBConvert.GetInt32Nullable(rdr, rdr.GetOrdinal("SUPPLIER_TYPE"));
                    return info;
                }
            }
            return null;
        }
    }
}
