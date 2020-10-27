using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class DeliveryScheduleBLL
    {
        #region Common
        DeliveryScheduleDAL dal = new DeliveryScheduleDAL();
        public List<DeliveryScheduleInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public DeliveryScheduleInfo SelectInfo(int scheduleIdentity)
        {
            return dal.GetInfo(scheduleIdentity);
        }

        public int InsertInfo(DeliveryScheduleInfo info)
        {
            return dal.Add(info);
        }

        //public bool UpdateInfo(DeliveryScheduleInfo info)
        //{
        //    return dal.Update(info) > 0 ? true : false;
        //}

        public bool DeleteInfo(int scheduleIdentity)
        {
            return dal.Delete(scheduleIdentity) > 0 ? true : false;
        }


        public bool UpdateInfo(string fields, int scheduleIdentity)
        {
            return dal.UpdateInfo(fields, scheduleIdentity) > 0 ? true : false;
        }

        public int GetCounts(string textWhere)
        {
            return dal.GetCounts(textWhere);
        }

        #endregion
    }
}

