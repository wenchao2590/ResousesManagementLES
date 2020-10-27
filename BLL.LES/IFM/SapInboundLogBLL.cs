namespace BLL.LES
{
    using DAL.LES;
    using DM.LES;
    using System.Collections.Generic;
    /// <summary>
    /// SapInboundLogBLL
    /// </summary>
    public partial class SapInboundLogBLL
    {
        #region Common
        /// <summary>
        /// SapInboundLogDAL
        /// </summary>
        SapInboundLogDAL dal = new SapInboundLogDAL();
        /// <summary>
        /// GetListByPage->GetListForPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<SapInboundLogInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListForPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SapInboundLogInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion
    }
}

