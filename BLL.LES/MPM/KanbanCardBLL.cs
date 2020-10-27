using DAL.LES;
using DM.LES;
using DAL.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using DM.SYS;
using System.Data;

namespace BLL.LES
{
    /// <summary>
    /// KanbanCardBLL
    /// </summary>
    public class KanbanCardBLL
    {
        #region Common
        /// <summary>
        /// KanbanCardDAL
        /// </summary>
        KanbanCardDAL dal = new KanbanCardDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<KanbanCardInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public KanbanCardInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(KanbanCardInfo info)
        {
            ///����ʱУ���Ӧ���������Ϣ�п��廷����������Ѵ����ĸ����������Ϣ��Ӧ���忨�ϼ���
            int cnt = dal.GetCounts(string.Format("[STATUS] in (" + (int)BasicDataStatusConstants.Created + "," + (int)BasicDataStatusConstants.Enable + ") and [PART_NO] = N'{0}' and [PART_BOX_CODE] = N'{1}'", info.PartNo, info.PartBoxCode));
            int kanbanCircleCnt = new MaintainInhouseLogisticStandardDAL().GetKanbanCircleCnt(info.PartBoxCode, info.PartNo);
            if (kanbanCircleCnt <= cnt)
                throw new Exception("MC:0x00000310");///���������Ϣ�п��廷��������ڿ��忨�ϼ�

            ///����ʱ����Ԥ�����忨�Ţ����ɹ���Դ��ֶν�����䣬����ֻ�ṩ��ʾ���ܽ�������
            string cardType = new KanbanPartBoxDAL().GetCardTypeCodeByPartBoxCode(info.PartBoxCode);
            info.CardNo = new SeqDefineDAL().GetCurrentCode("KANBAN_CARD_NO", cardType);
            cnt = dal.GetCounts("[CARD_NO] = N'" + info.CardNo + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000055");///���忨���ظ�

            ///����ʱ����״̬��Ϊ�Ѵ���
            info.Status = (int)BasicDataStatusConstants.Created;
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
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// LogicDeleteInfo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool EnableInfos(List<string> rowsKeyValues, string loginUser)
        {
            ///���忨״̬�����Ϊ�Ѵ���
            int cnt = dal.GetCounts("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and [STATUS] = " + (int)BasicDataStatusConstants.Created + "");
            if (cnt == 0)
                throw new Exception("MC:0x00000210");///�Ѵ���״̬�Ŀ��쿨���ܽ�������

            ///�������ʱ����״̬��Ϊ������
            string sql = "update [LES].[TM_MPM_KANBAN_CARD] WITH(ROWLOCK) set [STATUS] = " + (int)BasicDataStatusConstants.Enable + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() where [VALID_FLAG] = 1  and [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool InvalidInfos(List<string> rowsKeyValues, string loginUser)
        {
            ///���忨״̬����벻Ϊ������
            int cnt = dal.GetCounts("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and [STATUS] <> " + (int)BasicDataStatusConstants.Disabled + "");
            if (cnt == 0)
                throw new Exception("MC:0x00000211");///�˿��쿨������

            ///�������ʱ����״̬��Ϊ�����ϣ�ͬʱ�����߼�ɾ�����Ϊ��
            string sql = "update [LES].[TM_MPM_KANBAN_CARD] WITH(ROWLOCK) set [STATUS] = " + (int)BasicDataStatusConstants.Disabled + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() where [VALID_FLAG] = 1  and [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }


        #endregion

        #region �Զ��巽��
        /// <summary>
        /// ���ݿ���Ż�ȡʵ�����
        /// </summary>
        /// <param name="CardNo">�����</param>
        /// <returns>���� KanbanCardInfo ����</returns>
        public KanbanCardInfo SelectInfoByCardNo(string cardNo, string loginUser)
        {
            KanbanCardInfo kanbancardinfo = dal.SelectInfoByCardNo(cardNo);
            if (kanbancardinfo == null) throw new Exception("MC:3x00000004");///��ǩ��Ϣ����
            string sqlstr = "UPDATE [LES].[TM_MPM_KANBAN_CARD] SET [USED_STATUS] =  20 ,[SCANNED_DATE] = GETDATE(),[SCANNED_USER] = N'" + loginUser + "' WHERE [CARD_NO] = N'" + cardNo + "';";
            if (!CommonDAL.ExecuteNonQueryBySql(sqlstr))
                throw new Exception("MC:0x00000276");///��ǩ��Ϣ����
            return kanbancardinfo;
        }
        /// <summary>
        /// ��ȡ��ӡ����
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public DataSet GetPrintDatas(List<string> rowsKeyValues, string loginUser)
        {
            if (rowsKeyValues.Count == 0) return null;
            string sql = "select a.[CARD_NO],a.[PART_NO],a.[PART_NAME],b.[ROUTE_CODE],c.[LINE_SITE_DLOC],c.[DLOC],a.[PART_QTY],a.[PACKAGE_CODE] "
                + "from [LES].[TM_MPM_KANBAN_CARD] a with(nolock) "
                + "left join [LES].[TM_MPM_KANBAN_PART_BOX] b on a.[PART_BOX_CODE] = b.[PART_BOX_CODE] "
                + "left join [LES].[TM_BAS_PARTS_STOCK] c on a.[PART_NO] =  c.[PART_NO] and b.[SOURCE_WM_NO] = c.[WM_NO] and b.[SOURCE_ZONE_NO] = c.[ZONE_NO] "
                + "where a.[STATUS] = " + (int)BasicDataStatusConstants.Enable + " and b.[STATUS] = " + (int)BasicDataStatusConstants.Enable + " "
                + "and a.[VALID_FLAG] = 1 and b.[VALID_FLAG] = 1 and a.[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ");";
            return CommonDAL.ExecuteDataSetBySql(sql);
        }
        /// <summary>
        /// �����������忨
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool BatchcreationInfos(List<string> rowsKeyValues, string loginUser)
        {
            ///��ѡ���Ŀ��忨
            var kanbanCardInfofirst = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", string.Empty).GroupBy(w => new { w.PartNo, w.PartBoxCode }).Select(w => new { w.Key.PartNo, w.Key.PartBoxCode });
            var kanbanCardInfosecend = dal.GetList(
                "[PART_NO] IN ('" + string.Join("','", kanbanCardInfofirst.GroupBy(w => new { w.PartNo }).Select(w => w.Key.PartNo).ToArray()) + "')"
                + "AND [PART_BOX_CODE] IN ('" + string.Join("','", kanbanCardInfofirst.GroupBy(w => new { w.PartBoxCode }).Select(w => w.Key.PartBoxCode).ToArray()) + "')"
                , string.Empty);

            List<KanbanCardInfo> kanbanCardInfos = (from a in kanbanCardInfosecend
                                                    join b in kanbanCardInfofirst on new { a.PartNo, a.PartBoxCode } equals new { b.PartNo, b.PartBoxCode }
                                                    select a).ToList<KanbanCardInfo>();

            if (kanbanCardInfos.Where(w => w.Status == (int)BasicDataStatusConstants.Disabled).Count() > 0)
                throw new Exception("MC:0x00000242");///ֻ����������Ѵ�����������״̬�Ŀ��忨

            ///ѡ�����忨��Ӧ�������
            List<KanbanPartBoxInfo> kanbanPartBoxInfos = new KanbanPartBoxDAL().GetList(
                string.Format("[PART_BOX_CODE] in ('{0}')", string.Join("','", kanbanCardInfos.GroupBy(w => new { w.PartBoxCode }).Select(w => w.Key.PartBoxCode).ToArray()))
                , string.Empty);
            if (kanbanPartBoxInfos.Count == 0)
                throw new Exception("MC:3x00000014");///������������ݴ���

            ///�ѿ��忨��Ӧ������������Ϣ
            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandardInfos
                    = new MaintainInhouseLogisticStandardDAL().GetList("[PART_NO] in ('" + string.Join("','", kanbanCardInfos.GroupBy(w => new { w.PartNo }).Select(w => w.Key.PartNo).ToArray()) + "') "
                    + "and [INHOUSE_PART_CLASS] in ('" + string.Join("','", kanbanPartBoxInfos.Select(d => d.PartBoxCode).ToArray()) + "') "
                    + "and [STATUS] = " + (int)BasicDataStatusConstants.Enable + " "
                    + "and [INHOUSE_SYSTEM_MODE] = N'" + (int)PullModeConstants.Kanban + "'", string.Empty);
            if (maintainInhouseLogisticStandardInfos.Count == 0)
                throw new Exception("MC:0x00000213");///����������Ϣ���ݴ���

            StringBuilder sqlBuilder = new StringBuilder();

            #region ���忨����ű�
            string sqlstr = @"insert into [LES].[TM_MPM_KANBAN_CARD] (
				FID,
				CARD_NO,
				PART_BOX_CODE,
				PART_BOX_NAME,
				PART_NO,
				PART_NAME,
				SUPPLIER_CODE,
				SUPPLIER_NAME,
				PART_QTY,
				PACKAGE_CODE,
				STATUS,
				PRINT_CNT,
				PRINT_TIME,
				PRINT_USER,
				USED_STATUS,
				SCANNED_USER,
				SCANNED_DATE,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
			) values ({0});";
            #endregion

            foreach (var kanbanCardInfo in kanbanCardInfos.GroupBy(w => new { w.PartNo, w.PartBoxCode }).Select(w => new { w.Key.PartBoxCode, w.Key.PartNo, SumCount = w.Count() }))
            {
                int eachcount = 0;
                MaintainInhouseLogisticStandardInfo info = maintainInhouseLogisticStandardInfos.FirstOrDefault(w => w.PartNo == kanbanCardInfo.PartNo && w.InhousePartClass == kanbanCardInfo.PartBoxCode);
                if (info != null)
                    eachcount = (info.KanbanCircleCnt ?? 0) - kanbanCardInfo.SumCount;
                if (eachcount > 0)
                {
                    for (int i = 0; i < eachcount; i++)
                    {
                        var kanbanpartboxinfo = kanbanPartBoxInfos.FirstOrDefault(w => w.PartBoxCode == kanbanCardInfo.PartBoxCode);
                        string sqlKanbanPullOrderDetailValue = "NEWID()"///FID
                                                        + ",N'" + new SeqDefineDAL().GetCurrentCode("KANBAN_CARD_NO", "HOSPITAL") + "'"
                                                        + ",N'" + kanbanCardInfo.PartBoxCode + "'"
                                                        + ",N'" + kanbanpartboxinfo.PartBoxName + "'"
                                                        + ",N'" + info.PartNo + "'"
                                                        + ",N'" + info.PartCname + "'"
                                                        + ",N'" + info.SupplierNum + "'"
                                                        + ",N'" + kanbanCardInfos.FirstOrDefault(w => w.SupplierCode == info.SupplierNum).SupplierName + "'"///��Ӧ������
                                                        + "," + info.InboundPackage.GetValueOrDefault()
                                                        + ",N'" + info.InboundPackageModel + "'"
                                                        + "," + (int)BasicDataStatusConstants.Enable
                                                        + ",null"
                                                        + ",null"
                                                        + ",null"
                                                        + ",10"
                                                        + ",null"
                                                        + ",null"
                                                        + ",1"///VALID_FLAG
                                                        + ",GETDATE()"///CREATE_DATE
                                                        + ",N'" + loginUser + "'"///CREATE_USER
                                                        + ",null"
                                                        + ",null";
                        sqlBuilder.AppendLine(string.Format(sqlstr, sqlKanbanPullOrderDetailValue));
                    }
                }
            }
            if (sqlBuilder.Length == 0)
                throw new Exception("MC:0x00000243");
            sqlBuilder.AppendLine("SELECT @@IDENTITY;");
            ///���ݱ���ʱʹ��SQLƴ�Ӷ���insert��䷽ʽһ���ύִ�У�ִ��ʧ����Ҫͬ���������ͻ���
            using (TransactionScope trans = new TransactionScope())
            {
                if (!CommonDAL.ExecuteNonQueryBySql(sqlBuilder.ToString()))
                    throw new Exception("MC:0x00000244");
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// �����������忨
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool SynchronizationKanBanCardInfos(string loginUser)
        {
            #region ׼������Դ
            StringBuilder sqlBuilder = new StringBuilder();
            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandardInfos = new MaintainInhouseLogisticStandardDAL().GetList("[STATUS] = " + (int)BasicDataStatusConstants.Enable + "and [INHOUSE_SYSTEM_MODE] = N'" + (int)PullModeConstants.Kanban + "'", string.Empty);
            if (maintainInhouseLogisticStandardInfos.Count() == 0)
                throw new Exception("MC:0x00000256");///�����ڿ�ͬ�������Ͽ���������Ϣ

            ///�����ɵĿ��忨���������ϵĿ�Ƭ֮��
            List<KanbanCardInfo> kanbanCardInfos = dal.GetList("[STATUS] <> " + (int)BasicDataStatusConstants.Disabled, string.Empty);
            ///�����ɵĿ��忨ͳ����Ϣ
            var groupKanbanCardInfos = kanbanCardInfos.GroupBy(w => new { w.PartNo, w.PartBoxCode }).Select(w => new { w.Key.PartBoxCode, w.Key.PartNo, SumCount = w.Count() }).ToList();
            ///���������
            List<KanbanPartBoxInfo> kanbanPartBoxInfos = new KanbanPartBoxDAL().GetList(string.Format("[PART_BOX_CODE] in ('{0}') and [STATUS] = " + (int)BasicDataStatusConstants.Enable + "",
                string.Join("','", maintainInhouseLogisticStandardInfos.GroupBy(w => new { w.InhousePartClass }).Select(w => w.Key.InhousePartClass).ToArray())), string.Empty);
            ///��Ӧ����Ϣ
            List<SupplierInfo> supplierinfos = new SupplierDAL().GetList(string.Format("[SUPPLIER_NUM] IN ('{0}')", maintainInhouseLogisticStandardInfos.GroupBy(w => new { w.SupplierNum }).Select(w => w.Key.SupplierNum).ToArray()), string.Empty);
            #endregion

            #region SQL�ű�
            string sqlstr = @"insert into [LES].[TM_MPM_KANBAN_CARD] (
				FID,
				CARD_NO,
				PART_BOX_CODE,
				PART_BOX_NAME,
				PART_NO,
				PART_NAME,
				SUPPLIER_CODE,
				SUPPLIER_NAME,
				PART_QTY,
				PACKAGE_CODE,
				STATUS,
				PRINT_CNT,
				PRINT_TIME,
				PRINT_USER,
				USED_STATUS,
				SCANNED_USER,
				SCANNED_DATE,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER,
				MODIFY_DATE,
				MODIFY_USER				 
			) values ({0});";
            #endregion

            #region ��������Դ���ɽű�
            foreach (var maintainInhouseLogisticStandardInfo in maintainInhouseLogisticStandardInfos)
            {
                ///��������ɴ��������ſ��忨
                int createCardCount = 0;
                var groupKanbanCardInfo = groupKanbanCardInfos.FirstOrDefault(w =>
                w.PartNo == maintainInhouseLogisticStandardInfo.PartNo &&
                w.PartBoxCode == maintainInhouseLogisticStandardInfo.InhousePartClass);
                if (groupKanbanCardInfo == null) createCardCount = maintainInhouseLogisticStandardInfo.KanbanCircleCnt.GetValueOrDefault();
                else createCardCount = maintainInhouseLogisticStandardInfo.KanbanCircleCnt.GetValueOrDefault() - groupKanbanCardInfo.SumCount;
                ///���û�пɴ����Ŀ�Ƭ�������һ��
                if (createCardCount == 0) continue;
                ///��ȡ�����������Ϣ�����������봦��������״̬
                KanbanPartBoxInfo kanbanPartBoxInfo = kanbanPartBoxInfos.FirstOrDefault(d => d.PartBoxCode == maintainInhouseLogisticStandardInfo.InhousePartClass);
                if (kanbanPartBoxInfo == null) continue;

                SupplierInfo supplierInfo = supplierinfos.FirstOrDefault(d => d.SupplierNum == maintainInhouseLogisticStandardInfo.SupplierNum);

                for (int i = 0; i < createCardCount; i++)
                {
                    string sqlKanbanPullOrderDetailValue = "NEWID()"///FID
                                                        + ",N'" + new SeqDefineDAL().GetCurrentCode("KANBAN_CARD_NO", null) + "'"
                                                        + ",N'HOSPITAL'"
                                                        //+ ",N'" + kanbanPartBoxInfo.PartBoxCode + "'"
                                                        + ",N'" + kanbanPartBoxInfo.PartBoxName + "'"
                                                        + ",N'" + maintainInhouseLogisticStandardInfo.PartNo + "'"
                                                        + ",N'" + maintainInhouseLogisticStandardInfo.PartCname + "'"
                                                        + ",N'" + maintainInhouseLogisticStandardInfo.SupplierNum + "'"
                                                        + ",N'" + (supplierInfo == null ? string.Empty : supplierInfo.SupplierName) + "'"///��Ӧ������
                                                        + "," + maintainInhouseLogisticStandardInfo.InboundPackage.GetValueOrDefault()
                                                        + ",N'" + maintainInhouseLogisticStandardInfo.InboundPackageModel + "'"
                                                        + "," + (int)BasicDataStatusConstants.Created
                                                        + ",NULL"
                                                        + ",NULL"
                                                        + ",NULL"
                                                        + ",10"
                                                        + ",NULL"
                                                        + ",NULL"
                                                        + ",1"///VALID_FLAG
                                                        + ",GETDATE()"///CREATE_DATE
                                                        + ",N'" + loginUser + "'"///CREATE_USER
                                                        + ",NULL"
                                                        + ",NULL";
                    sqlBuilder.AppendLine(string.Format(sqlstr, sqlKanbanPullOrderDetailValue));
                }
            }
            #endregion

            if (sqlBuilder.Length == 0)
                throw new Exception("MC:0x00000249");///��������Ҫ���ɵĿ��忨

            ///�����������忨ʱ�Ƿ�ͬʱ����
            string batchCreateKanbanCardEnableAtSametime = new ConfigDAL().GetValueByCode("BATCH_CREATE_KANBAN_CARD_ENABLE_AT_SAMETIME");
            if (batchCreateKanbanCardEnableAtSametime.ToLower() == "true")
                sqlBuilder.AppendLine("update [LES].[TM_MPM_KANBAN_CARD] "
                    + "set [STATUS] = " + (int)BasicDataStatusConstants.Enable + ",[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' "
                    + "where [STATUS] = " + (int)BasicDataStatusConstants.Created + ";");

            using (TransactionScope trans = new TransactionScope())
            {
                CommonDAL.ExecuteNonQueryBySql(sqlBuilder.ToString());
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// ɨ����ύ���ɿ���������
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool SubmitInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<KanbanCardInfo> kanbanCardInfos = dal.GetList("" +
                "[STATUS] = " + (int)BasicDataStatusConstants.Enable + " and " +
                "[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and " +
                "[USED_STATUS] in (" + (int)KanbanCardUseStatusConstants.NotUsed + "," + (int)KanbanCardUseStatusConstants.Reback + ")", string.Empty);
            if (kanbanCardInfos.Count == 0)
                throw new Exception("MC:0x00000399");///״̬����Ϊ�����ã���ʹ��״ֻ̬��Ϊδʹ�û��ѻؿ�
            if (kanbanCardInfos.Count < rowsKeyValues.Count)
                throw new Exception("MC:0x00000399");///״̬����Ϊ�����ã���ʹ��״ֻ̬��Ϊδʹ�û��ѻؿ�

            ///��������������
            return new KanbanPullOrderBLL().CreateKanbanPullOrder(kanbanCardInfos.Select(d => d.CardNo).ToList(), loginUser);
        }
        #endregion

    }
}

