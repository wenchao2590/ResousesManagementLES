using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class PartsBoxBLL
    {
        #region Common
        PartsBoxDAL dal = new PartsBoxDAL();
        public List<PartsBoxInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        public PartsBoxInfo GetInfoByPartBox(string partBoxCode)
        {
            return dal.GetInfoByPartBox(partBoxCode);
        }

        public List<PartsBoxInfo> GetList(string textWhere, string orderText)
        {
            return dal.GetList(textWhere, orderText);
        }


        #endregion
    }
}


