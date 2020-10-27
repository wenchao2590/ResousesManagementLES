using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class TranHeadBLL
    {
        #region Common
        TranHeadDAL dal = new TranHeadDAL();
        public List<TranHeadInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public TranHeadInfo SelectInfo(int tranId)
        {
            return dal.GetInfo(tranId);
        }

        public int InsertInfo(TranHeadInfo info)
        {
            return dal.Add(info);
        }

        public bool DeleteInfo(int tranId)
        {
            return dal.Delete(tranId) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, int tranId)
        {
            return dal.UpdateInfo(fields, tranId) > 0 ? true : false;
        }

        #endregion
    }
}

