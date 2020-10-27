using DAL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.SYS
{
    /// <summary>
    /// EntityFieldBLL
    /// </summary>
    public class EntityFieldBLL
    {
        #region Common
        /// <summary>
        /// EntityFieldDAL
        /// </summary>
        EntityFieldDAL dal = new EntityFieldDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<EntityFieldInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(EntityFieldInfo info)
        {
            int cnt = dal.GetCounts("[ENTITY_FID] = N'" + info.EntityFid.GetValueOrDefault() + "' and [FIELD_NAME] = N'" + info.FieldName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000045");///同一数据模型里的字段不允许重复

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
            string entityFid = CommonBLL.GetFieldValue(fields, "ENTITY_FID");
            string fieldName = CommonBLL.GetFieldValue(fields, "FIELD_NAME");
            int cnt = dal.GetCounts("[ID] <> " + id + " and [ENTITY_FID] = N'" + entityFid + "' and [FIELD_NAME] = N'" + fieldName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000045");///同一数据模型里的字段不允许重复

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
            return dal.LogicDelete(id, modifyUser) > 0 ? true : false;
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EntityFieldInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion
        /// <summary>
        /// GetCounts
        /// </summary>
        /// <param name="whereText"></param>
        /// <returns></returns>
        public int GetCounts(string whereText)
        {
            return dal.GetCounts(whereText);
        }
        /// <summary>
        /// GetInfo
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public EntityFieldInfo GetInfo(string entityName, string fieldName)
        {
            return dal.GetInfo(entityName, fieldName);
        }
        /// <summary>
        /// 获取Excel模板导出字段
        /// </summary>
        /// <param name="entityFid"></param>
        /// <returns></returns>
        public List<EntityFieldInfo> GetExcelFieldList(Guid entityFid)
        {
            return dal.GetExcelFieldList(entityFid);
        }
        /// <summary>
        /// 获取需要统计的字段
        /// </summary>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public List<EntityFieldInfo> GetStaticticsFields(string entityName)
        {
            return dal.GetStaticticsFields(entityName);
        }
    }
}
