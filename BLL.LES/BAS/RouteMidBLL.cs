namespace BLL.LES
{
    using DAL.LES;
    using DAL.SYS;
    using DM.LES;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    /// <summary>
    /// RouteMidBLL
    /// </summary>
    public class RouteMidBLL
    {

        #region Common
        /// <summary>
        /// RouteMidDAL
        /// </summary>
        RouteMidDAL dal = new RouteMidDAL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<RouteMidInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RouteMidInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(RouteMidInfo info)
        {
            int cnt = dal.GetCounts("[ROUTE_FID] = N'" + info.RouteFid.GetValueOrDefault() + "' and [WM_NO] = N'" + info.WmNo + "' and [ZONE_NO] = N'" + info.ZoneNo + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000325");///同一路径下地点不能重复
            return dal.Add(info);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            string routeFid = CommonBLL.GetFieldValue(fields, "ROUTE_FID");
            string wmNo = CommonBLL.GetFieldValue(fields, "WM_NO");
            string zoneNo = CommonBLL.GetFieldValue(fields, "ZONE_NO");
            int cnt = dal.GetCounts("[ROUTE_FID] = N'" + routeFid + "' and [WM_NO] = N'" + wmNo + "' and [ZONE_NO] = N'" + zoneNo + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000325");///同一路径下地点不能重复
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        #endregion
    }
}
