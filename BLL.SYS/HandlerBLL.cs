using DAL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    /// <summary>
    /// HandlerBLL
    /// </summary>
    public class HandlerBLL
    {
        #region Common
        /// <summary>
        /// HandlerDAL
        /// </summary>
        HandlerDAL dal = new HandlerDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<HandlerInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(HandlerInfo info)
        {
            int cnt = dal.GetCounts("[AJAX_METHOD_NAME] = N'" + info.AjaxMethodName + "'");
            if (cnt > 0)
                throw new Exception("MC:1x00000024");///客户端函数名不允许重复

            cnt = dal.GetCounts("[SERVER_METHOD_NAME] = N'" + info.ServerMethodName + "'");
            if (cnt > 0)
                throw new Exception("MC:1x00000023");///服务端函数名不允许重复

            ///TODO:考虑增加启用
            return dal.Add(info);
        }
        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            string ajaxMethodName = CommonBLL.GetFieldValue(fields, "AJAX_METHOD_NAME");
            int cnt = dal.GetCounts("[ID] <> " + id + " and [AJAX_METHOD_NAME] = N'" + ajaxMethodName + "'");
            if (cnt > 0)
                throw new Exception("MC:1x00000024");///客户端函数名不允许重复

            string serverMethodName = CommonBLL.GetFieldValue(fields, "SERVER_METHOD_NAME");
            cnt = dal.GetCounts("[ID] <> " + id + " and [SERVER_METHOD_NAME] = N'" + serverMethodName + "'");
            if (cnt > 0)
                throw new Exception("MC:1x00000023");///服务端函数名不允许重复

            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// LogicDeleteInfo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifyUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string modifyUser)
        {
            int cnt = dal.GetCounts("[ID] = " + id  );
            if (cnt == 0)
                throw new Exception("MC:1x00000022");///只有已创建状态的路由可以删除
            return dal.LogicDelete(id, modifyUser) > 0 ? true : false;
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HandlerInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion
        /// <summary>
        /// 获取已启用路由配置
        /// </summary>
        /// <returns></returns>
        public List<HandlerInfo> GetHandlers()
        {
            return dal.GetList( "", string.Empty);
            //return dal.GetList("[STATUS] = " + (int)BasicDataStatusConstants.Enable + "", string.Empty);
        }
        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool EnableInfo(long id, string loginUser)
        {
            int cnt = dal.GetCounts("[ID] = " + id );
            if (cnt == 0)
                throw new Exception("MC:1x00000021");///已创建或已停用状态的路由可以启用

            return dal.UpdateInfo("[STATUS] = " + (int)BasicDataStatusConstants.Enable + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE()", id) > 0 ? true : false;
        }
        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool DisableInfo(long id, string loginUser)
        {
            int cnt = dal.GetCounts("[ID] = " + id + " and [STATUS] = " + (int)BasicDataStatusConstants.Enable + "");
            if (cnt == 0)
                throw new Exception("MC:1x00000020");///只有已启用状态的路由可以停用

            return dal.UpdateInfo("[STATUS] = " + (int)BasicDataStatusConstants.Disabled + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE()", id) > 0 ? true : false;
        }
    }
}
