namespace BLL.LES
{
    using DAL.LES;
    using DM.LES;
    using System.Collections.Generic;

    /// <summary>
    /// BusinessExpenseOutPageBLL
    /// </summary>
    public partial class BusinessExpenseOutPageBLL
    {
        #region Common
        /// <summary>
        /// BusinessExpenseOutDAL
        /// </summary>
        BusinessExpenseOutDAL dal = new BusinessExpenseOutDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<BusinessExpenseOutInfo></returns>
        public List<BusinessExpenseOutInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// GetInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusinessExpenseOutInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(BusinessExpenseOutInfo info)
        {
            return new BusinessExpenseOutBLL().InsertInfo(info);
        }
        /// <summary>
        /// LogicDeleteInfo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            return new BusinessExpenseOutBLL().LogicDeleteInfo(id, loginUser);
        }
        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            return new BusinessExpenseOutBLL().UpdateInfo(fields, id);
        }
        #endregion

       
    }
}

