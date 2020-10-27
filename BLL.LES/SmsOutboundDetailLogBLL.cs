using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class SmsOutboundDetailLogBLL
    {
        #region Common
        SmsOutboundDetailLogDAL dal = new SmsOutboundDetailLogDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<SmsOutboundDetailLogInfo></returns>
        public List<SmsOutboundDetailLogInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public SmsOutboundDetailLogInfo SelectInfo(int id)
        {
            return dal.GetInfo(id);
        }

        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info">对象</param>
        /// <returns></returns>
        public int InsertInfo(SmsOutboundDetailLogInfo info)
        {
            return dal.Add(info);
        }

        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="info">对象</param>
        /// <returns></returns>
        public bool UpdateInfo(SmsOutboundDetailLogInfo info)
        {
            return dal.Update(info) > 0 ? true : false;
        }

        /// <summary>
        /// DeleteInfo
        /// </summary>
        /// <returns></returns>
        public bool DeleteInfo(int id)
        {
            return dal.Delete(id) > 0 ? true : false;
        }

        /// <summary>
        /// LogicDeleteInfo
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="loginUser">用户</param>
        /// <returns></returns>
        public bool LogicDeleteInfo(int id, string loginUser)
        {
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }

        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="fields">更新字段</param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, int id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        /// <summary>
        /// GetCounts
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <returns></returns>
        public int GetCounts(string textWhere)
        {
            return dal.GetCounts(textWhere);
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <returns>List<SmsOutboundDetailLogInfo></returns>
        public List<SmsOutboundDetailLogInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }

        #endregion
    }
}

