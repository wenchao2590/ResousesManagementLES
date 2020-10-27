using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class BasPackageApplianceBLL
    {
        #region Common
        BasPackageApplianceDAL dal = new BasPackageApplianceDAL();
        public List<BasPackageApplianceInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        #endregion
    }
}

