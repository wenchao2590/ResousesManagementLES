using DM.LES;
using Infrustructure.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Dynamic;
using Infrustructure.Utilities;
using DM.SYS;

namespace DAL.LES
{
    public partial class SupplyPlanDAL
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
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <returns></returns>
        public DataTable GetSupplyPlanDataTableByPage(string textWhere, string textOrder, int pageIndex, int pageRow, string columns)
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
                + "," + columns + " from [LES].[TT_ATP_SUPPLY_PLAN]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteDataTable(cmd);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateColumns"></param>
        /// <returns></returns>
        public List<string> GetDatabaseExistsDateColumns(List<string> dateColumns)
        {
            string sql = "select d.name from sys.syscolumns d left join sys.sysobjects m on m.id = d.id "
                + "where m.name = 'TT_ATP_SUPPLY_PLAN' "
                + "and d.name in ('" + string.Join("','", dateColumns) + "') "
                + "order by d.name";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            List<string> list = new List<string>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    list.Add(DBConvert.GetString(dr, dr.GetOrdinal("name")));
                }
            }
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateColumn"></param>
        /// <returns></returns>
        public string GetDatabaseExistsDateColumn(string dateColumn)
        {
            string sql = "select d.name from sys.syscolumns d left join sys.sysobjects m on m.id = d.id "
                + "where m.name = 'TT_ATP_SUPPLY_PLAN' "
                + "and d.name = N'" + dateColumn + "'"
                + "order by d.name";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value) return string.Empty;
            return result.ToString();
        }


        public DataTable GetListBySql(List<string> dateColumns)
        {
            string sql = "select [FID], [PART_NO],[SUPPLIER_NUM],[PLANT],[" + string.Join("],[", dateColumns) + "] from [LES].[TT_ATP_SUPPLY_PLAN] where VALID_FLAG = 1";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            return db.ExecuteDataTable(dbCommand);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <returns></returns>
        public DataTable GetSupplyPlanCheckDataTableByPage(string textWhere, string textOrder, int pageIndex, int pageRow, string columns)
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
                + "," + columns + " from [LES].[TT_ATP_SUPPLY_PLAN]  T1 "
                + "left join ("
                + "select  [T3].[PART_NO],[T3].[PLANT],[T3].[SUPPLIER_NUM],"
                + "MAX(CASE [WAREHOUSE_TYPE] WHEN '10' THEN 库存 ELSE 0 END ) RDC,"
                + "MAX(CASE [WAREHOUSE_TYPE] WHEN '20' THEN 库存 ELSE 0 END ) VMI  from "
                + "(select [A].[PART_NO],[A].[PLANT],[A].[SUPPLIER_NUM],[A].[WAREHOUSE_TYPE],sum([B].[AVAILBLE_STOCKS]) 库存 from ("
                + " select  [W].[WAREHOUSE_TYPE],[P].[PART_NO],[P].[SUPPLIER_NUM],[P].[PLANT],[P].[WM_NO] from "
                + "[LES].[TM_BAS_PARTS_STOCK] P  left join  [LES].[TM_BAS_WAREHOUSE]  W on  [P].[WM_NO]=[W].[WAREHOUSE]"
                + "where [P].[LACK_OF_INSPECTION_FLAG] = 1 and [P].[VALID_FLAG] = 1 and [W].[VALID_FLAG] = 1 )"
                + "A  left join [LES].[TT_WMM_STOCKS] B on [A].[PART_NO] = [B].[PART_NO] and [A].[PLANT] = [B].[PLANT] and [A].[SUPPLIER_NUM] = [B].[SUPPLIER_NUM] and [A].[WM_NO] = [B].[WM_NO]"
                + " group by [A].[PART_NO],[A].[PLANT],[A].[WAREHOUSE_TYPE],[A].[SUPPLIER_NUM]) T3"
                + " group by  [T3].[PART_NO],[T3].[PLANT],[T3].[SUPPLIER_NUM]) T2  "
                + "on [T1].[PART_NO] = [T2].[PART_NO] and [T1].[PLANT] = [T2].[PLANT] and [T1].[SUPPLIER_NUM] = [T2].[SUPPLIER_NUM]"
                + " " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteDataTable(cmd);
        }

    }
}
