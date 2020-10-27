using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;

namespace BLL.LES
{
    public class AssemblyLineBLL
    {
        #region Common
        AssemblyLineDAL dal = new AssemblyLineDAL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<AssemblyLineInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AssemblyLineInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(AssemblyLineInfo info)
        {
            ///�����ߴ���ڡ������߼�Ƣܲ������ظ������ֶν���ȫ��У��
            int cnt = dal.GetCounts("[ASSEMBLY_LINE] = N'" + info.AssemblyLine + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000167");///��ˮ�ߴ����ظ�

            ///���������Ƣ�ͬһ��������²������ظ�
            cnt = dal.GetCounts("[ASSEMBLY_LINE_NAME] = N'" + info.AssemblyLineName + "' and [WORKSHOP] = N'" + info.Workshop + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000168");///��ˮ�������ظ�
            cnt = dal.GetCounts("[ASSEMBLY_LINE_NICKNAME] = N'" + info.AssemblyLineNickname + "'");
            if (cnt > 0 && !string.IsNullOrEmpty(info.AssemblyLineNickname))
                throw new Exception("MC:0x00000169");///��ˮ�߼���ظ�
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
            ///У���Ӧ�Ƿ��Ѿ�ά���˹���TM_BAS_WORKSHOP_SECTION�����߼�ɾ���Ĺ��β���У�鷶Χ��
            int workshopsectionCnt = new WorkshopSectionDAL().GetCounts("[ASSEMBLY_LINE] in (select [ASSEMBLY_LINE] from [LES].[TM_BAS_ASSEMBLY_LINE] with(nolock) where [ID] = " + id + " and [VALID_FLAG] = 1)");
            if (workshopsectionCnt > 0)
                throw new Exception("MC:0x00000074");///���������»��й��Σ��޷�ɾ��
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
            ///���������Ƣ�ͬһ��������²������ظ�
            string assemblyLineName = CommonBLL.GetFieldValue(fields, "ASSEMBLY_LINE_NAME");
            string workshop = CommonBLL.GetFieldValue(fields, "WORKSHOP");
            if (string.IsNullOrEmpty(assemblyLineName))
                throw new Exception("MC:0x00000078");///���������Ʋ���Ϊ��
            if (string.IsNullOrEmpty(workshop))
                throw new Exception("MC:0x00000079");///������벻��Ϊ��
            int cnt = dal.GetCounts("[ID] <> " + id + " and [ASSEMBLY_LINE_NAME] = N'" + assemblyLineName + "' and [WORKSHOP] = N'" + workshop + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000168");///��ˮ�������ظ�

            ///�����߼�Ƣܲ������ظ������ֶν���ȫ��У��
            string assembly_line_nickname = CommonBLL.GetFieldValue(fields, "ASSEMBLY_LINE_NICKNAME");
            if (assembly_line_nickname.ToUpper() == "NULL") assembly_line_nickname = string.Empty;
            cnt = dal.GetCounts("[ID] <> " + id + " and [ASSEMBLY_LINE_NICKNAME] = N'" + assembly_line_nickname + "'");
            if (cnt > 0 && !string.IsNullOrEmpty(assembly_line_nickname))
                throw new Exception("MC:0x00000169");///��ˮ�߼���ظ�
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
        /// <summary>
        /// ����SAP���ߴ����ȡLES���ߴ���
        /// </summary>
        /// <param name="sapAssemblyLine"></param>
        /// <returns></returns>
        public string GetAssemblyLineBySapAssemblyLine(string sapAssemblyLine)
        {
            return dal.GetAssemblyLineBySapAssemblyLine(sapAssemblyLine);
        }
        /// <summary>
        /// ����LES���ߴ����ȡSAP���ߴ���
        /// </summary>
        /// <param name="sapAssemblyLine"></param>
        /// <returns></returns>
        public string GetSapAssemblyLineByAssemblyLine(string assemblyLine)
        {
            return dal.GetSapAssemblyLineByAssemblyLine(assemblyLine);
        }
        /// <summary>
        /// ��ȡ��ˮ�����ݣ����ڱȶ�SAP������LES����֮���ϵ
        /// </summary>
        /// <returns></returns>
        public List<AssemblyLineInfo> GetListForInterfaceDataSync()
        {
            return dal.GetListForInterfaceDataSync();
        }
        /// <summary>
		/// Get data collection
		/// </summary>
		/// <param name="textWhere">Conditon</param>
		/// <param name="orderText">Sort</param>
		/// <returns>AssemblyLineInfo Collection </returns>
		public List<AssemblyLineInfo> GetList(string textWhere, string orderText)
        {
            return dal.GetList(textWhere, orderText);
        }
    }
}

