using DAL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    public class ProcessScheduleBLL
    {
        #region Common
        ProcessScheduleDAL dal = new ProcessScheduleDAL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<ProcessScheduleInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(ProcessScheduleInfo info)
        {
            ///服务描述①、服务代码②不能重复，必填项
            int cnt = dal.GetCounts("[PROCESS_CODE] = N'" + info.ProcessCode + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000023");///服务代码不允许重复
            cnt = dal.GetCounts("[PROCESS_NAME] = N'" + info.ProcessName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000024");///服务名称不允许重复 
            return dal.Add(info);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            string processName = CommonBLL.GetFieldValue(fields, "PROCESS_NAME");
            int cnt = dal.GetCounts("[ID] <> " + id + " and [PROCESS_NAME] = N'" + processName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000024");///服务名称不允许重复 
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifyUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string modifyUser)
        {
            ///仅运行状态③为10初始化的数据可以进行删除，更新逻辑删除标记⑤为0
            int cnt = dal.GetCounts("[ID] = " + id + " and [LAST_RUN_STATUS] <> " + (int)ProcessRunStatusConstants.Init + "");
            if (cnt > 0)
                throw new Exception("MC:0x00000025");///只有初始化状态的服务才允许删除 
            return dal.LogicDelete(id, modifyUser) > 0 ? true : false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProcessScheduleInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool EnableInfos(List<string> rowsKeyValues, string loginUser)
        {
            ///将运行状态③更新为20启动，需要校验状态为10初始化或30暂停
            int cnt = dal.GetCounts("[LAST_RUN_STATUS] in (" + (int)ProcessRunStatusConstants.Init + "," + (int)ProcessRunStatusConstants.Pause + ") and [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")");
            if (cnt != rowsKeyValues.Count)
                throw new Exception("Err_:MC:0x00000026");///只有初始化和暂停状态的服务可以启动
            return dal.EnableInfos(string.Join(",", rowsKeyValues.ToArray()), loginUser);
        }
        /// <summary>
        /// 暂停
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool PauseInfos(List<string> rowsKeyValues, string loginUser)
        {
            ///将运行状态③更新为30暂停，需要校验状态为20启动
            int cnt = dal.GetCounts("[LAST_RUN_STATUS] = " + (int)ProcessRunStatusConstants.Running + " and [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")");
            if (cnt != rowsKeyValues.Count)
                throw new Exception("Err_:MC:0x00000027");///只有运行中状态的服务可以暂停
            return dal.PauseInfos(string.Join(",", rowsKeyValues.ToArray()), loginUser);
        }
    }
}
