using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class ReturnBLL
    {
        #region Common
        ReturnDAL dal = new ReturnDAL();
        public List<ReturnInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public ReturnInfo SelectInfo(int returnId)
        {
            return dal.GetInfo(returnId);
        }

        public int InsertInfo(ReturnInfo info)
        {
            return dal.Add(info);
        }

        public bool DeleteInfo(int returnId)
        {
            return dal.Delete(returnId) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, int returnId)
        {
            return dal.UpdateInfo(fields, returnId) > 0 ? true : false;
        }

        #endregion
    }
}

