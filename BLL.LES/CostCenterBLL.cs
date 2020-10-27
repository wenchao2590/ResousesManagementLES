using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class CostCenterBLL
    {
        #region Common
        CostCenterDAL dal = new CostCenterDAL();
        public List<CostCenterInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public CostCenterInfo SelectInfo(int costId)
        {
            return dal.GetInfo(costId);
        }

        public long InsertInfo(CostCenterInfo info)
        {
            return dal.Add(info);
        }

        public bool DeleteInfo(int costId)
        {
            return dal.Delete(costId) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, int costId)
        {
            return dal.UpdateInfo(fields, costId) > 0 ? true : false;
        }

        #endregion
    }
}

