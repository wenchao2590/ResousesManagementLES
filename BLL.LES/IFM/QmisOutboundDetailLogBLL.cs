namespace BLL.LES
{
    using DAL.LES;
    using DM.LES;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    /// <summary>
    /// QmisOutboundDetailLogBLL
    /// </summary>
    public class QmisOutboundDetailLogBLL
    {
        #region Common
        /// <summary>
        /// QmisOutboundDetailLogDAL
        /// </summary>
        QmisOutboundDetailLogDAL dal = new QmisOutboundDetailLogDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<QmisOutboundDetailLogInfo></returns>
        public List<QmisOutboundDetailLogInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QmisOutboundDetailLogInfo SelectInfo(int id)
        {
            return dal.GetInfo(id);
        }
        #endregion
    }
}

