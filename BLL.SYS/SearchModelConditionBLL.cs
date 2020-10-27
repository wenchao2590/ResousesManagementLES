using DAL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    /// <summary>
    /// SearchModelConditionBLL
    /// </summary>
    public class SearchModelConditionBLL
    {
        #region Common
        /// <summary>
        /// SearchModelConditionDAL
        /// </summary>
        SearchModelConditionDAL dal = new SearchModelConditionDAL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<SearchModelConditionInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(SearchModelConditionInfo info)
        {
            int cnt = dal.GetCounts("[SEARCH_FID] = N'" + info.SearchFid.GetValueOrDefault() + "' and [CONTROL_ID] = N'" + info.ControlId + "'");
            if (cnt > 0)
                throw new Exception("MC:1x00000028");///同一检索模型里的字段不允许重复

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
            string searchFid = CommonBLL.GetFieldValue(fields, "SEARCH_FID");
            string controlId = CommonBLL.GetFieldValue(fields, "CONTROL_ID");
            int cnt = dal.GetCounts("[ID] <> " + id + " and [SEARCH_FID] = N'" + searchFid + "' and [CONTROL_ID] = N'" + controlId + "'");
            if (cnt > 0)
                throw new Exception("MC:1x00000028");///同一检索模型里的字段不允许重复

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
            return dal.LogicDelete(id, modifyUser) > 0 ? true : false;
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SearchModelConditionInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion
        /// <summary>
        /// GetCounts
        /// </summary>
        /// <param name="textWhere"></param>
        /// <returns></returns>
        public int GetCounts(string textWhere)
        {
            return dal.GetCounts(textWhere);
        }
    }
}
