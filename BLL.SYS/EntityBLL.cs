using DAL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    /// <summary>
    /// 
    /// </summary>
    public class EntityBLL
    {
        #region Common
        /// <summary>
        /// EntityDAL
        /// </summary>
        EntityDAL dal = new EntityDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<EntityInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(EntityInfo info)
        {
            int cnt = dal.GetCounts("[ENTITY_NAME] = N'" + info.EntityName + "'");
            if (cnt > 0)
                throw new Exception("MC:1x00000025");///数据模型名称不能重复

            return dal.Add(info);
        }
        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            string entityName = CommonBLL.GetFieldValue(fields, "ENTITY_NAME");
            int cnt = dal.GetCounts("[ID] <> " + id + " and [ENTITY_NAME] = N'" + entityName.Trim('\'') + "'");
            if (cnt > 1)
                throw new Exception("MC:1x00000025");///数据模型名称不能重复


            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// LogicDeleteInfo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifyUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string modifyUser)
        {
            int cnt = new EntityFieldDAL().GetCounts("[ENTITY_FID] in (select [FID] from dbo.[TS_SYS_ENTITY] with(nolock) where [ID] = " + id + " and [VALID_FLAG] = 1)");
            if (cnt > 0)
                throw new Exception("MC:1x00000026");///数据模型下有字段时不能删除
            return dal.LogicDelete(id, modifyUser) > 0 ? true : false;
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EntityInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion
        /// <summary>
        /// GetGridFieldByEntityName
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public List<EntityFieldInfo> GetGridFieldByEntityName(string entityName, string tableName)
        {
            string fields = string.Empty;
            if (string.IsNullOrEmpty(tableName))
                fields = "and [TABLE_NAMES] = N'" + tableName + "' ";
            return new EntityFieldDAL().GetList("[ENTITY_FID] in (select [FID] from dbo.[TS_SYS_ENTITY] with(nolock) where [ENTITY_NAME] = N'" + entityName + "' " + fields + "and [VALID_FLAG] = 1)", "[DISPLAY_ORDER]");
        }
        /// <summary>
        /// GetGridFieldByEntityName
        /// </summary>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public List<EntityFieldInfo> GetGridFieldByEntityName(string entityName)
        {
            return new EntityFieldDAL().GetListByEntityName(entityName);
        }
        public List<EntityFieldInfo> GetExcelFieldList(string entityName)
        {
            return new EntityFieldDAL().GetExcelFieldList(entityName);
        }
        /// <summary>
        /// GetInfo
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public EntityInfo GetInfo(string entityName, string tableName)
        {
            return dal.GetInfo(entityName, tableName);
        }
        /// <summary>
        /// GetInfo
        /// </summary>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public EntityInfo GetInfo(string entityName)
        {
            return dal.GetInfo(entityName);
        }
        /// <summary>
        /// GetInfo
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public EntityInfo GetInfo(Guid fid)
        {
            return dal.GetInfo(fid);
        }
    }
}
