using BLL.SYS;
using DAL.LES;
using DAL.SYS;
using DM.LES;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;

namespace BLL.LES
{
    public class KanbanPartBoxBLL
    {
        #region Common
        KanbanPartBoxDAL dal = new KanbanPartBoxDAL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<KanbanPartBoxInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public KanbanPartBoxInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(KanbanPartBoxInfo info)
        {
            ///零件类代码①与零件类名称②各自校验在对应表字段内容不能重复
            int cnt = dal.GetCounts(string.Format("[PART_BOX_CODE] = N'{0}'", info.PartBoxCode));
            if (cnt > 0)
                throw new Exception("Err_:MC:0x00000302");
            cnt = dal.GetCounts(string.Format("[PART_BOX_NAME] = N'{0}'", info.PartBoxName));
            if (cnt > 0)
                throw new Exception("Err_:MC:0x00000303");
            ///来源存储区⑤与目标存储区⑦不能为同一存储区
            if (info.SourceZoneNo == info.TargetZoneNo)
                throw new Exception("Err_:MC:0x00000304");
            ///保存时更新状态为已创建
            info.Status = (int)BasicDataStatusConstants.Created;

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
            int cnt = dal.GetCounts("[STATUS] = " + (int)BasicDataStatusConstants.Created + " and [ID] = " + id + "");
            if (cnt == 0)
                throw new Exception("Err_:MC:0x00000203");///已创建状态的零件类才能进行删除

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
            ///来源存储区⑤与目标存储区⑦不能为同一存储区
            string sourceZoneNo = CommonBLL.GetFieldValue(fields, "SOURCE_ZONE_NO");
            string targetZoneNo = CommonBLL.GetFieldValue(fields, "TARGET_ZONE_NO");
            if (sourceZoneNo == targetZoneNo)
                throw new Exception("Err_:MC:0x00000304");
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        #endregion

        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool EnableInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<KanbanPartBoxInfo> info = dal.GetList("[ID] IN (" + string.Join(",", rowsKeyValues) + ")", string.Empty);
            if (info.Count == 0)
                throw new Exception("Err_:MC:0x00000204");///看板零件类不存在
            foreach (var item in info)
            {
                if (item == null)
                    throw new Exception("Err_:MC:0x00000204");///看板零件类不存在
                                                              ///零件类必须为已创建状态
                if (item.Status != (int)BasicDataStatusConstants.Created)
                    throw new Exception("Err_:MC:0x00000205");///已创建状态的零件类才能进行启用

                ///零件类下必须有对应的已启用状态的零件拉动信息
                int cnt = new MaintainInhouseLogisticStandardDAL().GetCounts(string.Format("[INHOUSE_PART_CLASS] = N'{0}' and [STATUS] = {1} and [INHOUSE_SYSTEM_MODE] = N'{2}'"
                    , item.PartBoxCode
                    , (int)BasicDataStatusConstants.Enable
                    , (int)PullModeConstants.Kanban));
                if (cnt == 0)
                    throw new Exception("Err_:MC:0x00000206");///没有已启用的物料拉动信息，不能启用零件类

            }
            string sql = " update[LES].[TM_MPM_KANBAN_PART_BOX] WITH(ROWLOCK) set [STATUS] = " + (int)BasicDataStatusConstants.Enable + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() WHERE [VALID_FLAG] = 1  AND ID IN (" + string.Join(",", rowsKeyValues) + ") ";
            using (TransactionScope trans = new TransactionScope())
            {
                if (!string.IsNullOrEmpty(sql))
                    CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool InvalidInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<KanbanPartBoxInfo> info = dal.GetList("[ID] IN (" + string.Join(",", rowsKeyValues) + ")", string.Empty);
            if (info.Count == 0)
                throw new Exception("Err_:MC:0x00000204");///看板零件类不存在
            string sql = string.Empty;
            foreach (var item in info)
            {
                if (item == null)
                    throw new Exception("Err_:MC:0x00000204");///看板零件类不存在

                ///零件类必须不为已作废状态
                if (item.Status == (int)BasicDataStatusConstants.Disabled)
                    throw new Exception("Err_:MC:0x00000207");//此零件类已作废

                ///零件类下必须没有对应的已启用状态的零件拉动信息
                int cnt = new MaintainInhouseLogisticStandardDAL().GetCounts(string.Format("[INHOUSE_PART_CLASS] = N'{0}' and [STATUS] = {1} and [INHOUSE_SYSTEM_MODE] = N'{2}'"
                    , item.PartBoxCode
                    , (int)BasicDataStatusConstants.Enable
                    , (int)PullModeConstants.Kanban));
                if (cnt > 0)
                    throw new Exception("Err_:MC:0x00000208");//该零件类下还有已启用的物料拉动信息，不能作废零件类

                ///操作完成时更新状态为已作废，同时将对应已创建状态零件拉动信息更新为已作废状态
                sql += "update [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] "
                   + "set [STATUS] = " + (int)BasicDataStatusConstants.Disabled + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() "
                   + "where [INHOUSE_PART_CLASS] = N'" + item.PartBoxCode + "' and [INHOUSE_SYSTEM_MODE] = " + (int)PullModeConstants.Kanban + " and [STATUS] = " + (int)BasicDataStatusConstants.Created + ";";
            }
            using (TransactionScope trans = new TransactionScope())
            {
                if (!string.IsNullOrEmpty(sql))
                {
                    sql += "update [LES].[TM_MPM_KANBAN_PART_BOX] set [STATUS] = " + (int)BasicDataStatusConstants.Enable + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() where [VALID_FLAG] = 1 and [ID] IN (" + string.Join(",", rowsKeyValues) + ") ";
                    CommonDAL.ExecuteNonQueryBySql(sql);
                }
                trans.Complete();
            }
            return true;
        }
    }
}

