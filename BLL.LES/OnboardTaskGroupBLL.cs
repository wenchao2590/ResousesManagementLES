using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class OnboardTaskGroupBLL
    {
        #region Common
        OnboardTaskGroupDAL dal = new OnboardTaskGroupDAL();
        public List<OnboardTaskGroupInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public OnboardTaskGroupInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        public long InsertInfo(OnboardTaskGroupInfo info)
        {
            return dal.Add(info);
        }

        public bool DeleteInfo(long id)
        {
            return dal.Delete(id) > 0 ? true : false;
        }

        public bool LogicDeleteInfo(long id, string loginUser)
        {
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion
    }
}

