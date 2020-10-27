namespace BLL.LES
{
    using DAL.LES;
    using DM.LES;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    /// <summary>
    /// VmiInboundLogBLL
    /// </summary>
    public class VmiInboundLogBLL
    {
        #region Common
        /// <summary>
        /// VmiInboundLogDAL
        /// </summary>
        VmiInboundLogDAL dal = new VmiInboundLogDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<VmiInboundLogInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListForPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VmiInboundLogInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion
    }
}

