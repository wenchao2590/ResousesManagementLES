namespace BLL.LES
{
    using DAL.LES;
    using DM.LES;
    using System.Collections.Generic;
    /// <summary>
    /// SrmInboundLogBLL
    /// </summary>
    public partial class SrmInboundLogBLL
    {
        #region Common
        /// <summary>
        /// SrmInboundLogDAL
        /// </summary>
        SrmInboundLogDAL dal = new SrmInboundLogDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<SrmInboundLogInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListForPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SrmInboundLogInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion
    }
}

