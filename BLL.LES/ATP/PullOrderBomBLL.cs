using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    /// <summary>
    /// PullOrderBomBLL
    /// </summary>
    public class PullOrderBomBLL
    {
        #region Common
        /// <summary>
        /// PullOrderBomDAL
        /// </summary>
        PullOrderBomDAL dal = new PullOrderBomDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<PullOrderBomInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PullOrderBomInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(PullOrderBomInfo info)
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
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <returns></returns>
        public List<PullOrderBomInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
        /// <summary>
        ///  根据窗口时间(在此窗口时间之前)、生产线、零件类
        /// 获取未生成过该零件类计划拉动单的生产订单BOM
        /// </summary>
        /// <param name="currentWindowTime">窗口时间</param>
        /// <param name="assemblyLine">生产线</param>
        /// <param name="partBoxFid">计划零件类</param>
        /// <returns></returns>
        public List<PullOrderBomInfo> GetUnPlanPullingOrders(DateTime currentWindowTime, DateTime nextWindowTime, string sqlwhere, Guid partBoxFid)
        {
            return dal.GetUnPlanPullingOrders(currentWindowTime, nextWindowTime, sqlwhere, partBoxFid);
        }


        public List<PullOrderBomInfo> GetPartList(string ProductOrderNo)
        {
            return dal.GetPartList(ProductOrderNo);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuFid"></param>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<PullOrderBomInfo> GetPullOrderBomInfos(Guid menuFid, string textWhere, string textOrder, int pageIndex, int pageRow
          , out int dataCount)
        {
            dataCount = dal.GetCounts("and [VALID_FLAG] = 1 and [ORDERFID] = N'" + menuFid + "' " + textWhere);
            return dal.GetListByPage(menuFid, textWhere, textOrder, pageIndex, pageRow);
        }
    }
}

