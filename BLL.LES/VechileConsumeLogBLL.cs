using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class VechileConsumeLogBLL
    {
        #region Common
        VechileConsumeLogDAL dal = new VechileConsumeLogDAL();
        public List<VechileConsumeLogInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public VechileConsumeLogInfo SelectInfo(int consumeLogIdentity)
        {
            return dal.GetInfo(consumeLogIdentity);
        }

        public int InsertInfo(VechileConsumeLogInfo info)
        {
            return dal.Add(info);
        }

        //public bool UpdateInfo(VechileConsumeLogInfo info)
        //{
        //    return dal.Update(info) > 0 ? true : false;
        //}

        public bool DeleteInfo(int consumeLogIdentity)
        {
            return dal.Delete(consumeLogIdentity) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, int consumeLogIdentity)
        {
            return dal.UpdateInfo(fields, consumeLogIdentity) > 0 ? true : false;
        }

        public int GetCounts(string textWhere)
        {
            return dal.GetCounts(textWhere);
        }

        #endregion
    }
}

