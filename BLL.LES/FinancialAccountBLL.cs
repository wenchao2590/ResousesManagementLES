using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class FinancialAccountBLL
    {
        #region Common
        FinancialAccountDAL dal = new FinancialAccountDAL();
        public List<FinancialAccountInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public FinancialAccountInfo SelectInfo(int financialId)
        {
            return dal.GetInfo(financialId);
        }

        public long InsertInfo(FinancialAccountInfo info)
        {
            return dal.Add(info);
        }
         

        public bool DeleteInfo(int financialId)
        {
            return dal.Delete(financialId) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, int financialId)
        {
            return dal.UpdateInfo(fields, financialId) > 0 ? true : false;
        }

        #endregion
    }
}

