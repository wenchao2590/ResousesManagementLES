using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class InternalOrderBLL
    {
        #region Common
        InternalOrderDAL dal = new InternalOrderDAL();
        public List<InternalOrderInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public InternalOrderInfo SelectInfo(int internalId)
        {
            return dal.GetInfo(internalId);
        }

        public long InsertInfo(InternalOrderInfo info)
        {
            return dal.Add(info);
        }

        public bool DeleteInfo(int internalId)
        {
            return dal.Delete(internalId) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, int internalId)
        {
            return dal.UpdateInfo(fields, internalId) > 0 ? true : false;
        }

        #endregion
    }
}

