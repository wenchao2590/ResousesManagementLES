using DAL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    public class ConfigBLL
    {
        #region Common
        ConfigDAL dal = new ConfigDAL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<ConfigInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow
            , out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(ConfigInfo info)
        {
            ///名称①、代码②不能重复，必填项
            int cnt = dal.GetCounts("[CODE] = N'" + info.Code + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000005");///系统配置代码不允许重复
            cnt = dal.GetCounts("[NAME] = N'" + info.Name + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000006");///系统配置名称不允许重复
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
            ///名称①不能重复，必填项
            string name = CommonBLL.GetFieldValue(fields, "NAME");
            int cnt = dal.GetCounts("[ID] <> " + id + " and [NAME] = N'" + name + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000006");///系统配置名称不允许重复
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
            return dal.LogicDelete(id, modifyUser) > 0 ? true : false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ConfigInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="orderText"></param>
        /// <returns></returns>
        public List<ConfigInfo> GetList(string textWhere, string orderText)
        {
            return dal.GetList(textWhere, orderText);
        }
        #endregion
        /// <summary>
        /// 根据代码获取值
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetValueByCode(string code)
        {
            return dal.GetValueByCode(code);
        }
        /// <summary>
        /// 批量根据代码获取值
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetValuesByCodes(string[] codes)
        {
            return dal.GetValuesByCodes(codes);
        }
    }
}
