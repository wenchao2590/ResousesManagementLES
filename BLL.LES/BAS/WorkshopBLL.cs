using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;

namespace BLL.LES
{
    public class WorkshopBLL
    {
        #region Common
        WorkshopDAL dal = new WorkshopDAL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<WorkshopInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WorkshopInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }


        /// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>WorkshopInfo Collection </returns>
		public List<WorkshopInfo> GetList(string textWhere, string orderText)
        {
            return dal.GetList(textWhere, orderText);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(WorkshopInfo info)
        {
            ///车间代码②、车间名称③不允许重复，单字段进行全表校验
            int workshopCnt = dal.GetCounts("[WORKSHOP] = N'" + info.Workshop + "' ");
            if (workshopCnt > 0)
                throw new Exception("MC:0x00000165");///车间代码重复
            int workshopNameCnt = dal.GetCounts("[WORKSHOP_NAME] = N'" + info.WorkshopName + "' ");
            if (workshopNameCnt > 0)
                throw new Exception("MC:0x00000166");///车间名称重复
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
            ///校验对应是否已经维护了生产线TM_BAS_ASSEMBLY_LINE，已逻辑删除的生产线不在校验范围内
            int assemblylineCnt = new AssemblyLineDAL().GetCounts("[WORKSHOP] in (select [WORKSHOP] from [LES].[TM_BAS_WORKSHOP] with(nolock) where [ID] = " + id + " and [VALID_FLAG] = 1)");
            if (assemblylineCnt > 0)
                throw new Exception("MC:0x00000099");///该车间下还有生产线，无法删除
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
            ///车间名称③不允许重复
            string workshopName = CommonBLL.GetFieldValue(fields, "WORKSHOP_NAME");
            if (string.IsNullOrEmpty(workshopName))
                throw new Exception("MC:0x00000102");///车间名称不能为空
            int cnt = dal.GetCounts("[ID] <> " + id + " and [WORKSHOP_NAME] = N'" + workshopName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000166");///车间名称重复
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <returns></returns>
        public int GetCounts(string textWhere)
        {
            return dal.GetCounts(textWhere);
        }

        #endregion


        #region private 

        /// <summary>
        /// 获取工厂数据，用于比对SAP代码与LES代码之间关系
        /// </summary>
        /// <returns></returns>
        public List<WorkshopInfo> GetListForInterfaceDataSync()
        {
            return dal.GetListForInterfaceDataSync();

        }
        #endregion
    }
}

