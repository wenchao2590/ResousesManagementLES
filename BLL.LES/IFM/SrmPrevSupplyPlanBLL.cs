namespace BLL.LES
{
    using DAL.LES;
    using DM.LES;
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// SrmPrevSupplyPlanBLL
    /// </summary>
    public class SrmPrevSupplyPlanBLL
    {
        #region Common
        /// <summary>
        /// SrmPrevSupplyPlanDAL
        /// </summary>
        SrmPrevSupplyPlanDAL dal = new SrmPrevSupplyPlanDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<SrmPrevSupplyPlanInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SrmPrevSupplyPlanInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(SrmPrevSupplyPlanInfo info)
        {
            return dal.Add(info);
        }
        /// <summary>
        /// LogicDeleteInfo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }
        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        #endregion
        /// <summary>
        /// 获取规定时间范围内的供货计划
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="dateEndTime"></param>
        /// <returns></returns>
        public List<SrmPrevSupplyPlanInfo> GetprevSupplyPlanInfosList(DateTime dateTime, DateTime dateEndTime)
        {
            string textWhere = string.Format(@"and [DELIVERY_DATE] between N'{0}' and N'{1}'", dateTime, dateEndTime);
            return dal.GetList(textWhere, string.Empty);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <returns></returns>
        public List<SrmPrevSupplyPlanInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateColumn"></param>
        /// <returns></returns>
        public List<SrmPrevSupplyPlanInfo> GetListByDateColumn(string dateColumn, string conditions = "")
        {
            return dal.GetListByDateColumn(dateColumn, conditions);
        }
    }
}

