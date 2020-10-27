using DAL.BAS;
using DM.BAS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BLL.BAS
{
    public class StockinBLL
    {
        StockinDAL dal = new StockinDAL();
        public List<StockinInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        public StockinInfo SelectInfo(int nid)
        {
            return dal.GetInfo(nid);
        }

        public int InsertInfo(YwdmkInfo info)
        {
            return 0;
        }

        public bool DeleteInfo(int nid)
        {
            return true;
        }

        public bool UpdateInfo(List<CommonField> fields, int nid)
        {
            string sql = string.Empty;
            string updatefields = string.Empty;
            string f1 = CommonBLL.GetFieldValue(fields, "Cksyk_f1");
            if (string.IsNullOrEmpty(f1)) return false;
            ///CKSYK
            updatefields = CommonBLL.GetUpdateFieldSql(fields, "Cksyk");
            if (!string.IsNullOrEmpty(updatefields))
                sql += "update dbo.[cksyk] "
                    + "set " + updatefields.Substring(1) + " "
                    + "where [f1] = '" + f1 + "';";
            ///DBJ2
            updatefields = CommonBLL.GetUpdateFieldSql(fields, "Dbj2");
            if (!string.IsNullOrEmpty(updatefields))
                sql += "update dbo.[dbj2] "
                    + "set " + updatefields.Substring(1) + " "
                    + "where [f1] = '" + f1 + "';";
            ///
            if (string.IsNullOrEmpty(sql)) return false;
            bool result = false;
            using (var trans = new TransactionScope())
            {
                result = CommonDAL.ExecuteNonQuery(sql);
                trans.Complete();
            }
            return result;
        }
    }
}
