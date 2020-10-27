using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class CounterBLL
    {
        #region Common
        CounterDAL dal = new CounterDAL();
        public List<CounterInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public CounterInfo SelectInfo(int inhouseIdentity)
        {
            return dal.GetInfo(inhouseIdentity);
        }

        public int InsertInfo(CounterInfo info)
        {
            return dal.Add(info);
        }

        //public bool UpdateInfo(CounterInfo info)
        //{
        //    return dal.Update(info) > 0 ? true : false;
        //}

        public bool DeleteInfo(int inhouseIdentity)
        {
            return dal.Delete(inhouseIdentity) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, int inhouseIdentity)
        {
            return dal.UpdateInfo(fields, inhouseIdentity) > 0 ? true : false;
        }

        public int GetCounts(string textWhere)
        {
            return dal.GetCounts(textWhere);
        }

        #endregion
    }
}

