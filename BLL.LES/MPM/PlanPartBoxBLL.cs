using DAL.LES;
using DAL.SYS;
using DM.LES;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BLL.LES
{
    public class PlanPartBoxBLL
    {
        #region Common
        PlanPartBoxDAL dal = new PlanPartBoxDAL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<PlanPartBoxInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="orderText"></param>
        /// <returns></returns>
        public List<PlanPartBoxInfo> GetList(string textWhere, string orderText)
        {
            return dal.GetList(textWhere, orderText);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PlanPartBoxInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(PlanPartBoxInfo info)
        {
            ///��������١���������Ƣ�Ϊ���������У���ڶ�Ӧ���ֶ����ݲ����ظ�
            int cnt = dal.GetCounts("[PART_BOX_CODE] = N'" + info.PartBoxCode + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000302");///���������ظ�
            cnt = dal.GetCounts("[PART_BOX_NAME] = N'" + info.PartBoxName + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000303");///����������ظ�

            ///��Դ�洢������Ŀ��洢���ⲻ��Ϊͬһ�洢��
            if (info.SourceZoneNo == info.TargetZoneNo)
                throw new Exception("MC:0x00000402");///��Դ�洢����Ŀ��洢������һ�£�������ѡȡ
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
            PlanPartBoxInfo info = dal.GetInfo(id);
            if (info == null)
                throw new Exception("MC:0x00000084");///���ݴ���

            ///�����û�ж�Ӧ������������Ϣ��������
            int cnt = new MaintainInhouseLogisticStandardDAL().GetCounts("[INHOUSE_PART_CLASS] = N'" + info.PartBoxCode + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000403");///��������»���������ϣ�����ɾ��

            ///���������Ѵ���״̬
            if (info.Status != (int)BasicDataStatusConstants.Created)
                throw new Exception("MC:0x00000722");///���������Ѵ���״̬
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
            int cnt = dal.GetCounts("[ID] = " + id + " and [STATUS] = " + (int)BasicDataStatusConstants.Disabled + "");
            if (cnt > 0)
                throw new Exception("MC:0x00000742");///�����ϵ�����಻�ܽ����޸�

            ///��Դ�洢������Ŀ��洢���ⲻ��Ϊͬһ�洢��
            string sourceZoneNo = CommonBLL.GetFieldValue(fields, "SOURCE_ZONE_NO");
            string targetZoneNo = CommonBLL.GetFieldValue(fields, "TARGET_ZONE_NO");
            if (sourceZoneNo == targetZoneNo)
                throw new Exception("MC:0x00000402");///��Դ�洢����Ŀ��洢������һ�£�������ѡȡ

            string partBoxCode = CommonBLL.GetFieldValue(fields, "PART_BOX_CODE");
            string plant = CommonBLL.GetFieldValue(fields, "PLANT");
            string workshop = CommonBLL.GetFieldValue(fields, "WORKSHOP");
            string assemblyLine = CommonBLL.GetFieldValue(fields, "ASSEMBLY_LINE");
            PartsBoxInfo partsBoxInfo = new PartsBoxInfo();
            partsBoxInfo.PullMode = (int)PullModeConstants.Plan;
            partsBoxInfo.BoxParts = partBoxCode;
            partsBoxInfo.Plant = plant;
            partsBoxInfo.Workshop = workshop;
            partsBoxInfo.AssemblyLine = assemblyLine;
            using (var trans = new TransactionScope())
            {
                new MaintainInhouseLogisticStandardDAL().UpdatePartsBoxInfo(partsBoxInfo);
                if (dal.UpdateInfo(fields, id) == 0) return false;
                trans.Complete();
            }
            return true;
        }
        #endregion
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool InvalidInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<PlanPartBoxInfo> planPartBoxes = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and [STATUS] <> " + (int)BasicDataStatusConstants.Disabled, "[ID]");
            if (planPartBoxes == null)
                throw new Exception("Err_:MC:0x00000084");

            ///�������벻Ϊ������״̬
            if (planPartBoxes.Count != rowsKeyValues.Count)
                throw new Exception("Err_:MC:0x00000422");///�������벻Ϊ������״̬�²ſɸ���


            List<MaintainInhouseLogisticStandardInfo> maintainInhouses = new MaintainInhouseLogisticStandardDAL().GetList("[INHOUSE_PART_CLASS] in ('" + string.Join("','", planPartBoxes.Select(d => d.PartBoxCode).ToArray()) + "') and [STATUS] = " + (int)BasicDataStatusConstants.Enable + "", "[ID]");
            if (maintainInhouses.Count > 0)
                throw new Exception("Err_:MC:0x00000423");///������±���û�ж�Ӧ��������״̬�����������Ϣ

            StringBuilder stringBuilder = new StringBuilder();
            ///�������ʱ����״̬Ϊ�����ϣ�ͬʱ����Ӧ�Ѵ���״̬���������Ϣ����Ϊ������״̬
            stringBuilder.Append("update [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] ");
            stringBuilder.Append("set [STATUS] = " + (int)BasicDataStatusConstants.Disabled + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() ");
            stringBuilder.Append("where [INHOUSE_PART_CLASS] in ('" + string.Join("','", planPartBoxes.Select(d => d.PartBoxCode).ToArray()) + "') and [STATUS] = " + (int)BasicDataStatusConstants.Created + ";");
            stringBuilder.Append("update [LES].[TM_MPM_PLAN_PART_BOX] WITH(ROWLOCK) set [STATUS] = " + (int)BasicDataStatusConstants.Disabled + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")");
            return CommonDAL.ExecuteNonQueryBySql(stringBuilder.ToString());

        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool EnableInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<PlanPartBoxInfo> planParts = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and [STATUS] = " + (int)BasicDataStatusConstants.Created, "[ID]");
            if (planParts == null)
                throw new Exception("Err_:MC:0x00000084");

            ///��������Ϊ�Ѵ���״̬
            if (planParts.Count != rowsKeyValues.Count)
                throw new Exception("Err_:MC:0x00000683");///״̬����Ϊ�Ѵ���

            List<MaintainInhouseLogisticStandardInfo> maintainInhouses = new MaintainInhouseLogisticStandardDAL().GetList("[INHOUSE_PART_CLASS] in ('" + string.Join("','", planParts.Select(d => d.PartBoxCode).ToArray()) + "') and [STATUS] = " + (int)BasicDataStatusConstants.Enable + "", "[ID]");
            if (maintainInhouses == null)
                throw new Exception("Err_:MC:0x00000084");
            foreach (var item in planParts)
            {
                if (maintainInhouses.Where(d => d.InhousePartClass == item.PartBoxCode).Count() == 0)
                    throw new Exception("Err_:MC:0x00000420");///������±����ж�Ӧ��������״̬�����������Ϣ
            }


            string sql = "update [LES].[TM_MPM_PLAN_PART_BOX] WITH(ROWLOCK) set[STATUS] = " + (int)BasicDataStatusConstants.Enable + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() where [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
            return CommonDAL.ExecuteNonQueryBySql(sql);

        }
    }
}

