using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class PackageNotificationHeadBLL
    {
        #region Common
        PackageNotificationHeadDAL dal = new PackageNotificationHeadDAL();
        public List<PackageNotificationHeadInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public PackageNotificationHeadInfo SelectInfo(long notificationId)
        {
            return dal.GetInfo(notificationId);
        }

        public long InsertInfo(PackageNotificationHeadInfo info)
        {
            return dal.Add(info);
        }

        //public bool UpdateInfo(PackageNotificationHeadInfo info)
        //{
        //    return dal.Update(info) > 0 ? true : false;
        //}

        public bool DeleteInfo(long notificationId)
        {
            return dal.Delete(notificationId) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, long notificationId)
        {
            return dal.UpdateInfo(fields, notificationId) > 0 ? true : false;
        }

        public int GetCounts(string textWhere)
        {
            return dal.GetCounts(textWhere);
        }

        #endregion
    }
}

