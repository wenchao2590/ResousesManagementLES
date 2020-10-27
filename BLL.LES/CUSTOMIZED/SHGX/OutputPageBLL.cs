using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public partial class OutputPageBLL
    {
        #region Common
        /// <summary>
        /// OutputDAL
        /// </summary>
        OutputDAL dal = new OutputDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<OutputInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 通过ID进行查询
        /// </summary>
        /// <param name="outputId"></param>
        /// <returns></returns>
        public OutputInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion
    }
}
