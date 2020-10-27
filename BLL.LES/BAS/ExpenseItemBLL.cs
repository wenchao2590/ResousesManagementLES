using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    /// <summary>
    /// ExpenseItemBLL
    /// </summary>
    public class ExpenseItemBLL
    {
        #region Common
        /// <summary>
        /// ExpenseItemDAL
        /// </summary>
        ExpenseItemDAL dal = new ExpenseItemDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<ExpenseItemInfo></returns>
        public List<ExpenseItemInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ExpenseItemInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(ExpenseItemInfo info)
        {
            int cnt = dal.GetCounts("[EXPENSE_CODE] = N'" + info.ExpenseCode + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000321"); ///费用代码不允许重复
            cnt = dal.GetCounts("[EXPENSE_NAME] = N'" + info.ExpenseName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000322"); ///费用名称不允许重复

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
            string expenseCode = CommonBLL.GetFieldValue(fields, "EXPENSE_CODE");
            int cnt = dal.GetCounts("[ID] <> " + id + " and [EXPENSE_CODE] = N'" + expenseCode + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000321"); ///费用代码不允许重复

            string expenseName = CommonBLL.GetFieldValue(fields, "EXPENSE_NAME");
            cnt = dal.GetCounts("[ID] <> " + id + " and [EXPENSE_NAME] = N'" + expenseName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000322"); ///费用名称不允许重复
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion
    }
}

