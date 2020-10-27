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
            ///�������������������Ƣڸ���У���ڶ�Ӧ���ֶ����ݲ����ظ�
            int cnt = dal.GetCounts(string.Format("[PART_BOX_CODE] = N'{0}'", info.PartBoxCode));
            if (cnt > 0)
                throw new Exception("Err_:MC:0x00000302");
            cnt = dal.GetCounts(string.Format("[PART_BOX_NAME] = N'{0}'", info.PartBoxName));
            if (cnt > 0)
                throw new Exception("Err_:MC:0x00000303");
            ///��Դ�洢������Ŀ��洢���߲���Ϊͬһ�洢��
            if (info.SourceZoneNo == info.TargetZoneNo)
                throw new Exception("Err_:MC:0x00000304");
            ///����ʱ����״̬Ϊ�Ѵ���
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
                throw new Exception("Err_:MC:0x00000203");///�Ѵ���״̬���������ܽ���ɾ��

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
            ///��Դ�洢������Ŀ��洢���߲���Ϊͬһ�洢��
            string sourceZoneNo = CommonBLL.GetFieldValue(fields, "SOURCE_ZONE_NO");
            string targetZoneNo = CommonBLL.GetFieldValue(fields, "TARGET_ZONE_NO");
            if (sourceZoneNo == targetZoneNo)
                throw new Exception("Err_:MC:0x00000304");
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        #endregion

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool EnableInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<KanbanPartBoxInfo> info = dal.GetList("[ID] IN (" + string.Join(",", rowsKeyValues) + ")", string.Empty);
            if (info.Count == 0)
                throw new Exception("Err_:MC:0x00000204");///��������಻����
            foreach (var item in info)
            {
                if (item == null)
                    throw new Exception("Err_:MC:0x00000204");///��������಻����
                                                              ///��������Ϊ�Ѵ���״̬
                if (item.Status != (int)BasicDataStatusConstants.Created)
                    throw new Exception("Err_:MC:0x00000205");///�Ѵ���״̬���������ܽ�������

                ///������±����ж�Ӧ��������״̬�����������Ϣ
                int cnt = new MaintainInhouseLogisticStandardDAL().GetCounts(string.Format("[INHOUSE_PART_CLASS] = N'{0}' and [STATUS] = {1} and [INHOUSE_SYSTEM_MODE] = N'{2}'"
                    , item.PartBoxCode
                    , (int)BasicDataStatusConstants.Enable
                    , (int)PullModeConstants.Kanban));
                if (cnt == 0)
                    throw new Exception("Err_:MC:0x00000206");///û�������õ�����������Ϣ���������������

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
        /// ����
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool InvalidInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<KanbanPartBoxInfo> info = dal.GetList("[ID] IN (" + string.Join(",", rowsKeyValues) + ")", string.Empty);
            if (info.Count == 0)
                throw new Exception("Err_:MC:0x00000204");///��������಻����
            string sql = string.Empty;
            foreach (var item in info)
            {
                if (item == null)
                    throw new Exception("Err_:MC:0x00000204");///��������಻����

                ///�������벻Ϊ������״̬
                if (item.Status == (int)BasicDataStatusConstants.Disabled)
                    throw new Exception("Err_:MC:0x00000207");//�������������

                ///������±���û�ж�Ӧ��������״̬�����������Ϣ
                int cnt = new MaintainInhouseLogisticStandardDAL().GetCounts(string.Format("[INHOUSE_PART_CLASS] = N'{0}' and [STATUS] = {1} and [INHOUSE_SYSTEM_MODE] = N'{2}'"
                    , item.PartBoxCode
                    , (int)BasicDataStatusConstants.Enable
                    , (int)PullModeConstants.Kanban));
                if (cnt > 0)
                    throw new Exception("Err_:MC:0x00000208");//��������»��������õ�����������Ϣ���������������

                ///�������ʱ����״̬Ϊ�����ϣ�ͬʱ����Ӧ�Ѵ���״̬���������Ϣ����Ϊ������״̬
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

