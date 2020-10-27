namespace BLL.LES
{
    using DAL.LES;
    using DM.LES;
    using System.Collections.Generic;
    /// <summary>
    /// QmisInboundLogBLL
    /// </summary>
    public class QmisInboundLogBLL
    {
        #region Common
        /// <summary>
        /// QmisInboundLogDAL
        /// </summary>
        QmisInboundLogDAL dal = new QmisInboundLogDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<QmisInboundLogInfo></returns>
        public List<QmisInboundLogInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListForPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QmisInboundLogInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion
    }
}

