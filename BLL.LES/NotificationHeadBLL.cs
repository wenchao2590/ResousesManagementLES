using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class NotificationHeadBLL
    {
        #region Common
        NotificationHeadDAL dal = new NotificationHeadDAL();
        public List<NotificationHeadInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public NotificationHeadInfo SelectInfo(int notificationId)
        {
            return dal.GetInfo(notificationId);
        }

        public int InsertInfo(NotificationHeadInfo info)
        {
            return dal.Add(info);
        }

        public bool DeleteInfo(int notificationId)
        {
            return dal.Delete(notificationId) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, int notificationId)
        {
            return dal.UpdateInfo(fields, notificationId) > 0 ? true : false;
        }

        #endregion
    }
}

