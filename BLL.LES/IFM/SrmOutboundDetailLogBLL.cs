namespace BLL.LES
{
    using DAL.LES;
    using DM.LES;
    using System.Collections.Generic;
    /// <summary>
    /// SrmOutboundDetailLogBLL
    /// </summary>
    public partial class SrmOutboundDetailLogBLL
    {
        #region Common
        /// <summary>
        /// SrmOutboundDetailLogDAL
        /// </summary>
        SrmOutboundDetailLogDAL dal = new SrmOutboundDetailLogDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<SrmOutboundDetailLogInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SrmOutboundDetailLogInfo SelectInfo(int id)
        {
            return dal.GetInfo(id);
        }
        #endregion
    }
}

