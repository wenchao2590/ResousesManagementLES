using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class RepackageScheduleBLL
    {
        #region Common
        RepackageScheduleDAL dal = new RepackageScheduleDAL();
        public List<RepackageScheduleInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public RepackageScheduleInfo SelectInfo(int scheduleIdentity)
        {
            return dal.GetInfo(scheduleIdentity);
        }

        public int InsertInfo(RepackageScheduleInfo info)
        {
            return dal.Add(info);
        }

        public bool DeleteInfo(int scheduleIdentity)
        {
            return dal.Delete(scheduleIdentity) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, int scheduleIdentity)
        {
            return dal.UpdateInfo(fields, scheduleIdentity) > 0 ? true : false;
        }

        #endregion
    }
}

