using DAL.SYS;
using DM.LES;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DAL.LES
{
    public partial class SupplierPartQuotaDAL
    {
        public List<SupplierPartQuotaInfo> GetListForInterfaceDataSync(List<string> partNos)
        {
            string sql = "select * from [LES].[TM_BAS_SUPPLIER_PART_QUOTA] with(nolock) "
                + "where [VALID_FLAG] = 1 and [PART_NO] in ('" + string.Join("','", partNos.ToArray()) + "');";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            List<SupplierPartQuotaInfo> list = new List<SupplierPartQuotaInfo>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    list.Add(CreateSupplierPartQuotaInfo(dr));
                }
            }
            return list;
        }
        public List<SupplierPartQuotaInfo> GetListByPages(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",* from [LES].[TM_BAS_SUPPLIER_PART_QUOTA]  WITH(NOLOCK) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<SupplierPartQuotaInfo> list = new List<SupplierPartQuotaInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateSupplierPartQuotaInfo(dr));
                }
            }
            return list;


        }
    }
}
