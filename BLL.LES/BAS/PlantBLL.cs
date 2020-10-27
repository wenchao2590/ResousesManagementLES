using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;

namespace BLL.LES
{
    public class PlantBLL
    {
        #region Common
        PlantDAL dal = new PlantDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere">string</param>
        /// <param name="textOrder">string</param>
        /// <param name="pageIndex">int</param>
        /// <param name="pageRow">int</param>
        /// <param name="dataCount">out int</param>
        /// <returns>List<PlantInfo></returns>
        public List<PlantInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            List<PlantInfo> plantInfos= dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
            return plantInfos;
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>PlantInfo</returns>
        public PlantInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>PlantInfo Collection </returns>
		public List<PlantInfo> GetList(string textWhere, string orderText)
        {
            return dal.GetList(textWhere, orderText);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info">PlantInfo</param>
        /// <returns>int</returns>
        public long InsertInfo(PlantInfo info)
        {
            ///工厂代码①、工厂名称②不允许重复，单字段进行全表校验
            int plantCnt = dal.GetCounts("[PLANT] = N'" + info.Plant + "'");
            if (plantCnt > 0)
                throw new Exception("MC:0x00000163");
            int plantNameCnt = dal.GetCounts("[PLANT_NAME] = N'" + info.PlantName + "'");
            if (plantNameCnt > 0)
                throw new Exception("MC:0x00000164");
            info.ValidFlag = true;
            return dal.Add(info);
        }
        /// <summary>
        /// LogicDeleteInfo
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="loginUser">string</param>
        /// <returns>bool</returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            ///校验对应是否已经维护了车间TM_BAS_WORKSHOP，已逻辑删除的车间不在校验范围内
            int workshopCnt = new WorkshopDAL().GetCounts("[PLANT] in (select [PLANT] from [LES].[TM_BAS_PLANT] with(nolock) where [ID] = " + id + " and [VALID_FLAG] = 1)");
            if (workshopCnt > 0)
                throw new Exception("MC:0x00000095");///该工厂下还有车间，无法删除
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }
        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="fields">string</param>
        /// <param name="id">int</param>
        /// <returns>bool</returns>
        public bool UpdateInfo(string fields, long id)
        {
            ///工厂名称②不允许重复
            string plantName = CommonBLL.GetFieldValue(fields, "PLANT_NAME");
            if (string.IsNullOrEmpty(plantName))
                throw new Exception("MC:0x00000096");///工厂名称不能为空
            int cnt = dal.GetCounts("[ID] <> " + id + " and [PLANT_NAME] = N'" + plantName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000164");
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// GetCounts
        /// </summary>
        /// <param name="textWhere">string</param>
        /// <returns>int</returns>
        public int GetCounts(string textWhere)
        {
            return dal.GetCounts(textWhere);
        }

        #endregion

        /// <summary>
        /// 根据SAP工厂代码获取LES工厂代码
        /// </summary>
        /// <param name="sapPlantCode"></param>
        /// <returns></returns>
        public string GetPlantBySapPlantCode(string sapPlantCode)
        {
            return dal.GetPlantBySapPlantCode(sapPlantCode);
        }
        /// <summary>
        /// 获取工厂数据，用于比对SAP代码与LES代码之间关系
        /// </summary>
        /// <returns></returns>
        public List<PlantInfo> GetListForInterfaceDataSync()
        {
            return dal.GetListForInterfaceDataSync();
        }

        public string GetSapPlantByPlantCode(string PlantCode)
        {
            return dal.GetSapPlantByPlantCode(PlantCode);
        }
    }
}

