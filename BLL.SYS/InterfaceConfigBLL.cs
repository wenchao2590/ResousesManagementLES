using DAL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    public class InterfaceConfigBLL
    {
        #region Common
        InterfaceConfigDAL dal = new InterfaceConfigDAL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<InterfaceConfigInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public InterfaceConfigInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(InterfaceConfigInfo info)
        {
            ///接口代码①不能重复
            int interfaceCodeCnt = dal.GetCounts("[INTERFACE_CODE] = N'" + info.InterfaceCode + "'");
            if (interfaceCodeCnt > 0)
                throw new Exception("Err_:MC:0x00000015");///接口代码不允许重复

            ///接口名称②、外部系统名③组合不能重复
            int interfaceMethodSysNameCnt = dal.GetCounts("[METHOD_NAME] = N'" + info.MethrodName + "' and [SYS_NAME] = N'" + info.SysName + "'");
            if (interfaceMethodSysNameCnt > 0)
                throw new Exception("Err_:MC:0x00000016");///针对同一外部系统接口名称不允许重复
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
            ///接口名称②、外部系统名③组合不能重复
            string methodName = CommonBLL.GetFieldValue(fields, "METHROD_NAME");
            string sysName = CommonBLL.GetFieldValue(fields, "SYS_NAME");
            int interfaceMethodSysNameCnt = dal.GetCounts("[ID] <> " + id + " and [METHOD_NAME] = N'" + methodName + "' and [SYS_NAME] = N'" + sysName + "'");
            if (interfaceMethodSysNameCnt > 0)
                throw new Exception("Err_:MC:0x00000016");///针对同一外部系统接口名称不允许重复
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion

        /// <summary>
        /// 根据接口代码获取
        /// </summary>
        /// <param name="interfaceCode"></param>
        /// <returns></returns>
        public InterfaceConfigInfo GetInfoByInterfaceCode(string interfaceCode)
        {
            return dal.GetInfoByInterfaceCode(interfaceCode);
        }
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="whereText"></param>
        /// <param name="orderText"></param>
        /// <returns></returns>
        public List<InterfaceConfigInfo> GetList(string whereText, string orderText)
        {
            return dal.GetList(whereText, orderText);
        }
    }
}

