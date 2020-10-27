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
            ///��������١��������Ƣڲ������ظ������ֶν���ȫ��У��
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
            ///У���Ӧ�Ƿ��Ѿ�ά���˳���TM_BAS_WORKSHOP�����߼�ɾ���ĳ��䲻��У�鷶Χ��
            int workshopCnt = new WorkshopDAL().GetCounts("[PLANT] in (select [PLANT] from [LES].[TM_BAS_PLANT] with(nolock) where [ID] = " + id + " and [VALID_FLAG] = 1)");
            if (workshopCnt > 0)
                throw new Exception("MC:0x00000095");///�ù����»��г��䣬�޷�ɾ��
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
            ///�������Ƣڲ������ظ�
            string plantName = CommonBLL.GetFieldValue(fields, "PLANT_NAME");
            if (string.IsNullOrEmpty(plantName))
                throw new Exception("MC:0x00000096");///�������Ʋ���Ϊ��
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
        /// ����SAP���������ȡLES��������
        /// </summary>
        /// <param name="sapPlantCode"></param>
        /// <returns></returns>
        public string GetPlantBySapPlantCode(string sapPlantCode)
        {
            return dal.GetPlantBySapPlantCode(sapPlantCode);
        }
        /// <summary>
        /// ��ȡ�������ݣ����ڱȶ�SAP������LES����֮���ϵ
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

