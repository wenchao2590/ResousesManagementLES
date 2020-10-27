using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class RepackageHeadBLL
    {
        #region Common
        RepackageHeadDAL dal = new RepackageHeadDAL();
        public List<RepackageHeadInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public RepackageHeadInfo SelectInfo(int repackageId)
        {
            return dal.GetInfo(repackageId);
        }

        public int InsertInfo(RepackageHeadInfo info)
        {
            return dal.Add(info);
        }

        public bool DeleteInfo(int repackageId)
        {
            return dal.Delete(repackageId) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, int repackageId)
        {
            return dal.UpdateInfo(fields, repackageId) > 0 ? true : false;
        }

        #endregion
    }
}

