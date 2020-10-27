using DAL.BAS;
using DM.BAS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.BAS
{
    public class Db1hyfBLL
    {
        #region Common
        Db1hyfDAL dal = new Db1hyfDAL();
        public List<Db1hyfInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            textWhere += " and db1hyf.xxbz=1 and db1hyf.sfdm='s' ";
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public Db1hyfInfo SelectInfo(int nid)
        {
            return dal.GetInfo(nid);
        }

        public int GetCounts(string textWhere)
        {
            return dal.GetCounts(textWhere);
        }

        #endregion
    }
}

