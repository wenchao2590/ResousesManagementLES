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
            ///生产线代码②、生产线简称④不允许重复，单字段进行全表校验
            int cnt = dal.GetCounts("[ASSEMBLY_LINE] = N'" + info.AssemblyLine + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000167");///流水线代码重复

            ///生产线名称③同一车间代码下不允许重复
            cnt = dal.GetCounts("[ASSEMBLY_LINE_NAME] = N'" + info.AssemblyLineName + "' and [WORKSHOP] = N'" + info.Workshop + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000168");///流水线名称重复
            cnt = dal.GetCounts("[ASSEMBLY_LINE_NICKNAME] = N'" + info.AssemblyLineNickname + "'");
            if (cnt > 0 && !string.IsNullOrEmpty(info.AssemblyLineNickname))
                throw new Exception("MC:0x00000169");///流水线简称重复
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
            ///校验对应是否已经维护了工段TM_BAS_WORKSHOP_SECTION，已逻辑删除的工段不在校验范围内
            int workshopsectionCnt = new WorkshopSectionDAL().GetCounts("[ASSEMBLY_LINE] in (select [ASSEMBLY_LINE] from [LES].[TM_BAS_ASSEMBLY_LINE] with(nolock) where [ID] = " + id + " and [VALID_FLAG] = 1)");
            if (workshopsectionCnt > 0)
                throw new Exception("MC:0x00000074");///该生产线下还有工段，无法删除
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
            ///生产线名称③同一车间代码下不允许重复
            string assemblyLineName = CommonBLL.GetFieldValue(fields, "ASSEMBLY_LINE_NAME");
            string workshop = CommonBLL.GetFieldValue(fields, "WORKSHOP");
            if (string.IsNullOrEmpty(assemblyLineName))
                throw new Exception("MC:0x00000078");///生产线名称不能为空
            if (string.IsNullOrEmpty(workshop))
                throw new Exception("MC:0x00000079");///车间代码不能为空
            int cnt = dal.GetCounts("[ID] <> " + id + " and [ASSEMBLY_LINE_NAME] = N'" + assemblyLineName + "' and [WORKSHOP] = N'" + workshop + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000168");///流水线名称重复

            ///生产线简称④不允许重复，单字段进行全表校验
            string assembly_line_nickname = CommonBLL.GetFieldValue(fields, "ASSEMBLY_LINE_NICKNAME");
            if (assembly_line_nickname.ToUpper() == "NULL") assembly_line_nickname = string.Empty;
            cnt = dal.GetCounts("[ID] <> " + id + " and [ASSEMBLY_LINE_NICKNAME] = N'" + assembly_line_nickname + "'");
            if (cnt > 0 && !string.IsNullOrEmpty(assembly_line_nickname))
                throw new Exception("MC:0x00000169");///流水线简称重复
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
        /// 根据SAP产线代码获取LES产线代码
        /// </summary>
        /// <param name="sapAssemblyLine"></param>
        /// <returns></returns>
        public string GetAssemblyLineBySapAssemblyLine(string sapAssemblyLine)
        {
            return dal.GetAssemblyLineBySapAssemblyLine(sapAssemblyLine);
        }
        /// <summary>
        /// 根据LES产线代码获取SAP产线代码
        /// </summary>
        /// <param name="sapAssemblyLine"></param>
        /// <returns></returns>
        public string GetSapAssemblyLineByAssemblyLine(string assemblyLine)
        {
            return dal.GetSapAssemblyLineByAssemblyLine(assemblyLine);
        }
        /// <summary>
        /// 获取流水线数据，用于比对SAP代码与LES代码之间关系
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

