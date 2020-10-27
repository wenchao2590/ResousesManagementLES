using DAL.LES;
using DAL.SYS;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class JisPullOrderBLL
    {
        #region Common
        JisPullOrderDAL dal = new JisPullOrderDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<JisPullOrderInfo></returns>
        public List<JisPullOrderInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public JisPullOrderInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info">对象</param>
        /// <returns></returns>
        public long InsertInfo(JisPullOrderInfo info)
        {
            return dal.Add(info);
        }


        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="fields">更新字段</param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <returns>List<JisPullOrderInfo></returns>
        public List<JisPullOrderInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }

        #endregion

        /// <summary>
        /// add sql
        /// </summary>
        /// <param name="jisPullOrderInfo"></param>
        /// <returns></returns>
        public string GetInsertSql(JisPullOrderInfo jisPullOrderInfo)
        {
            return JisPullOrderDAL.GetInsertSql(jisPullOrderInfo);
        }

        #region Print
        /// <summary>
        /// 获取打印数据
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public DataSet GetPrintDatas(List<string> rowsKeyValues, string loginUser)
        {
            string sql = "select * from [LES].[TT_MPM_JIS_PULL_ORDER] T1 with(nolock) " +
                "where [VALID_FLAG] = 1 and [ID] in (" + string.Join(",", rowsKeyValues) + ");" +
                "select * from [LES].[TT_MPM_JIS_PULL_ORDER_DETAIL] with(nolock) where [VALID_FLAG] = 1 and [ORDER_FID] in (select [FID] from [LES].[TT_MPM_JIS_PULL_ORDER] with(nolock) " +
                "where [ID] in (" + string.Join(",", rowsKeyValues) + ") and [VALID_FLAG] = 1);";
            return CommonDAL.ExecuteDataSetBySql(sql);
        }
        /// <summary>
        /// 打印后回调函数
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool GetPrintCallBack(List<string> rowsKeyValues, string loginUser)
        {
            string sql = "update [LES].[TT_MPM_JIS_PULL_ORDER] set " +
                "[LAST_PRINT_DATE] = GETDATE()," +
                "[PRINT_TIMES] = isnull([PRINT_TIMES],0) + 1," +
                "[LAST_PRINT_USER] = N'" + loginUser + "' where " +
                "[ID] in (" + string.Join(",", rowsKeyValues) + ")";
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }


        /// <summary>
        /// 获取当日车辆序号
        /// </summary>
        /// <returns></returns>
        public int GetDaySeqNo()
        {
            string sql = "select count(1)+1 from [LES].[TT_MPM_JIS_PULL_ORDER] " +
                "with(nolock) where [VALID_FLAG] = 1 " +
                "and [CREATE_DATE] between '"+DateTime.Now.Date+"' and GETDATE();";
            return Convert.ToInt32(CommonDAL.ExecuteScalar(sql));
        }
        #endregion
    }
}

