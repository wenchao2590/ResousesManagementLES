using DAL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    public class AutoPrintTaskBLL
    {
        #region Common
        AutoPrintTaskDAL dal = new AutoPrintTaskDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<AutoPrintTaskInfo></returns>
        public List<AutoPrintTaskInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AutoPrintTaskInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(AutoPrintTaskInfo info)
        {
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
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        
    
        /// <summary>
        /// 重发
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool ResendInfo(long aId, string loginUser)
        {

            //限制状态为已打印

            int dataCnt = dal.GetCounts("and [ID] = " + aId + " and [STATUS] = N'" + (int)PrintStateConstants.HAVE_TO_PRINT + "'");
            if (dataCnt == 0)
                throw new Exception("Err_:MC:0x00000700");
            //操作完成时更新状态③为10.未打印，同时记录操作用户到最后修改用户⑧和最后修改时间
                string sql = "  [STATUS] = N'" + (int)PrintStateConstants.NOT_TO_PRINT + "' ,  [MODIFY_USER] = N'" + loginUser + "' , [MODIFY_TIME] = N'" + DateTime.Now + "'";
                return dal.UpdateInfo(sql, aId) > 0 ? true : false;
            
        }


        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool CancelInfo(long aId, string loginUser)
        {
            //限制状态为未打印

            int dataCnt = dal.GetCounts("and [ID] = " + aId + " and [STATUS] = N'" + (int)PrintStateConstants.NOT_TO_PRINT + "'");
            if (dataCnt == 0)
                throw new Exception("Err_:MC:0x00000701");
            //操作完成时更新状态③为10.未打印，同时记录操作用户到最后修改用户⑧和最后修改时间
                string sql = "  [STATUS] = N'" + (int)PrintStateConstants.CLOSED + "' ,  [MODIFY_USER] = N'" + loginUser + "' , [MODIFY_TIME] = N'" + DateTime.Now + "'";
                return dal.UpdateInfo(sql, aId) > 0 ? true : false;
           
        }

        #endregion

        public List<AutoPrintTaskInfo> GetList(string textWhere)
        {
            return dal.GetList(textWhere);
        }

        /// <summary>
        /// 排除ids 获取第一条数据
        /// </summary>
        /// <param name="idswhere"></param>
        /// <returns></returns>
        public AutoPrintTaskInfo GetTopInfo(string idswhere)
        {
            return dal.GetTopInfo(idswhere);
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <returns>List<JisPullOrderDetailInfo></returns>
        public List<AutoPrintTaskInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
    }
}

