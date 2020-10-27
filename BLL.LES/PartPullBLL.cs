using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class PartPullBLL
    {
        #region Common
        PartPullDAL dal = new PartPullDAL();
        public List<PartPullInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public PartPullInfo SelectInfo(int relationId)
        {
            return dal.GetInfo(relationId);
        }

        public int InsertInfo(PartPullInfo info)
        {
            return dal.Add(info);
        }

        public bool DeleteInfo(int relationId)
        {
            return dal.Delete(relationId) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, int relationId)
        {
            return dal.UpdateInfo(fields, relationId) > 0 ? true : false;
        }

        #endregion
    }
}

