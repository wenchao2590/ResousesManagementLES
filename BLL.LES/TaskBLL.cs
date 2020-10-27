using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class TaskBLL
    {
        #region Common
        TaskDAL dal = new TaskDAL();
        public List<TaskInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public TaskInfo SelectInfo(int taskSn)
        {
            return dal.GetInfo(taskSn);
        }

        public bool InsertInfo(TaskInfo info)
        {
            return dal.Add(info);
        }

        public bool DeleteInfo(int taskSn)
        {
            return dal.Delete(taskSn) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, int taskSn)
        {
            return dal.UpdateInfo(fields, taskSn) > 0 ? true : false;
        }

        #endregion
    }
}

