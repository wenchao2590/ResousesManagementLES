using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class WmmTranReceiveDetailViewBLL
    {
        #region Common
        WmmTranReceiveDetailViewDAL dal = new WmmTranReceiveDetailViewDAL();
        public List<WmmTranReceiveDetailViewInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public int GetCounts(string textWhere)
        {
            return dal.GetCounts(textWhere);
        }

        #endregion
    }
}

