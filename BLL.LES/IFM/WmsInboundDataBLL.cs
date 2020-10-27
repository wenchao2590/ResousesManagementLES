namespace BLL.LES
{
    using DAL.LES;
    using DM.LES;
    using System.Collections.Generic;
    public class WmsInboundDataBLL
    {
        #region Common
        WmsInboundDataDAL dal = new WmsInboundDataDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<WmsTranOutInfo></returns>
        public List<WmsInboundDataInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public WmsInboundDataInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        public long InsertInfo(WmsInboundDataInfo info)
        {
            return dal.Add(info);
        }

        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion
    }
}
