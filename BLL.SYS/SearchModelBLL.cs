using DAL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    /// <summary>
    /// SearchModelBLL
    /// </summary>
    public class SearchModelBLL
    {
        /// <summary>
        /// SearchModelDAL
        /// </summary>
        SearchModelDAL dal = new SearchModelDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<SearchModelInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(SearchModelInfo info)
        {
            int cnt = dal.GetCounts("[SEARCH_NAME] = N'" + info.SearchName + "'");
            if (cnt > 0)
                throw new Exception("MC:1x00000027"); ///检索模型名称不允许重复
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
            string searchName = CommonBLL.GetFieldValue(fields, "SEARCH_NAME");
            int cnt = dal.GetCounts("[ID] <> " + id + " and [SEARCH_NAME] = N'" + searchName + "'");
            if (cnt > 0)
                throw new Exception("MC:1x00000027"); ///检索模型名称不允许重复
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
            int cnt = new SearchModelConditionDAL().GetCounts("[SEARCH_FID] in (select [FID] from dbo.[TS_SYS_SEARCH_MODEL] with(nolock) where [VALID_FLAG] = 1 and [ID] = " + id + ")");
            if (cnt > 0)
                throw new Exception("MC:1x00000029");///检索模型下有字段时不能删除
            return dal.LogicDelete(id, modifyUser) > 0 ? true : false;
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SearchModelInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// GetSearchConditionsByName
        /// </summary>
        /// <param name="searchName"></param>
        /// <param name="columnLength"></param>
        /// <returns></returns>
        public List<SearchModelConditionInfo> GetSearchConditionsByName(string searchName, out int columnLength)
        {
            columnLength = 0;
            SearchModelInfo info = dal.GetInfo(searchName);
            if (info == null)
                return new List<SearchModelConditionInfo>();
            columnLength = info.ColumnLength.GetValueOrDefault();
            return new SearchModelConditionDAL().GetList("[SEARCH_FID] = N'" + info.Fid.GetValueOrDefault() + "'", "[DISPLAY_ORDER]");
        }
        /// <summary>
        /// GetInfo
        /// </summary>
        /// <param name="searchName"></param>
        /// <returns></returns>
        public SearchModelInfo GetInfo(string searchName)
        {
            return dal.GetInfo(searchName);
        }
    }
}
