using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;

namespace BLL.LES
{
    public class WorkshopSectionBLL
    {
        #region Common
        WorkshopSectionDAL dal = new WorkshopSectionDAL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<WorkshopSectionInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WorkshopSectionInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        /// <summary>
        /// Get data collection
        /// </summary>
        /// <param name="textWhere">Conditon</param>
        /// <param name="orderText">Sort</param>
        /// <returns>WorkshopSectionInfo Collection </returns>
        public List<WorkshopSectionInfo> GetList(string textWhere, string orderText)
        {
            return dal.GetList(textWhere, orderText);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(WorkshopSectionInfo info)
        {
            ///���δ���۲������ظ������ֶν���ȫ��У��
            int workshopSectionCnt = dal.GetCounts("[WORKSHOP_SECTION] = N'" + info.WorkshopSection + "'");
            if (workshopSectionCnt > 0)
                throw new Exception("MC:0x00000170");///���δ����ظ�

            ///�������Ƣ���ͬһ�����ߴ����²������ظ�
            int workshopSectionNameCnt = dal.GetCounts("[WORKSHOP_SECTION_NAME] = N'" + info.WorkshopSectionName + "' and [ASSEMBLY_LINE] = N'" + info.AssemblyLine + "'");
            if (workshopSectionNameCnt > 0)
                throw new Exception("MC:0x00000171");///���������ظ�
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
            ///У���Ӧ�Ƿ��Ѿ�ά���˹�λTM_BAS_LOCATION�����߼�ɾ���������߲���У�鷶Χ��
            int cnt = new LocationDAL().GetCounts("[WORKSHOP_SECTION] in (select [WORKSHOP_SECTION] from [LES].[TM_BAS_WORKSHOP_SECTION] with(nolock) where [ID] = " + id + " and [VALID_FLAG] = 1)");
            if (cnt > 0)
                throw new Exception("MC:0x00000103");///�ù����»��й�λ���޷�ɾ��
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
            ///�������Ƣ���ͬһ�����ߴ����²������ظ�
            string workshopSectionName = CommonBLL.GetFieldValue(fields, "WORKSHOP_SECTION_NAME");
            string assemblyLine = CommonBLL.GetFieldValue(fields, "ASSEMBLY_LINE");
            if (string.IsNullOrEmpty(workshopSectionName))
                throw new Exception("MC:0x00000104");///�������Ʋ���Ϊ��
            if (string.IsNullOrEmpty(assemblyLine))
                throw new Exception("MC:0x00000105");///�����ߴ��벻��Ϊ��
            int cnt = dal.GetCounts("[ID] <> " + id + " and [WORKSHOP_SECTION_NAME] = N'" + workshopSectionName + "' and [ASSEMBLY_LINE] = N'" + assemblyLine + "'");
            if (cnt > 0)
                throw new Exception("Err_:MC:0x00000171");
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
    }
}

