namespace BLL.LES
{
    using DAL.LES;
    using DM.LES;
    using System.Collections.Generic;
    /// <summary>
    /// SapOutboundDetailLogBLL
    /// </summary>
    public partial class SapOutboundDetailLogBLL
    {
        #region Common
        /// <summary>
        /// SapOutboundDetailLogDAL
        /// </summary>
        SapOutboundDetailLogDAL dal = new SapOutboundDetailLogDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<SapOutboundDetailLogInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SapOutboundDetailLogInfo SelectInfo(int id)
        {
            return dal.GetInfo(id);
        }
        #endregion
    }
}

