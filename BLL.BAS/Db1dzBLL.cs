using DAL.BAS;
using DM.BAS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.BAS
{
    public class Db1dzBLL
    {
        #region Common
        Db1dzDAL dal = new Db1dzDAL();
        public List<Db1dzInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public Db1dzInfo SelectInfo(int nid)
        {
            return dal.GetInfo(nid);
        }

        public int InsertInfo(Db1dzInfo info)
        {
            return dal.Add(info);
        }

        public bool DeleteInfo(int nid)
        {
            return dal.Delete(nid) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, int nid)
        {
            return dal.UpdateInfo(fields, nid) > 0 ? true : false;
        }

        #endregion
    }
}

