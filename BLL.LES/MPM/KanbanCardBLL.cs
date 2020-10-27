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
            ///保存时校验对应零件拉动信息中看板环数必须大于已创建的该零件拉动信息对应看板卡合计数
            int cnt = dal.GetCounts(string.Format("[STATUS] in (" + (int)BasicDataStatusConstants.Created + "," + (int)BasicDataStatusConstants.Enable + ") and [PART_NO] = N'{0}' and [PART_BOX_CODE] = N'{1}'", info.PartNo, info.PartBoxCode));
            int kanbanCircleCnt = new MaintainInhouseLogisticStandardDAL().GetKanbanCircleCnt(info.PartBoxCode, info.PartNo);
            if (kanbanCircleCnt <= cnt)
                throw new Exception("MC:0x00000310");///零件拉动信息中看板环数必须大于看板卡合计

            ///保存时根据预定看板卡号①生成规则对此字段进行填充，界面只提供显示不能进行输入
            string cardType = new KanbanPartBoxDAL().GetCardTypeCodeByPartBoxCode(info.PartBoxCode);
            info.CardNo = new SeqDefineDAL().GetCurrentCode("KANBAN_CARD_NO", cardType);
            cnt = dal.GetCounts("[CARD_NO] = N'" + info.CardNo + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000055");///看板卡号重复

            ///保存时更新状态⑩为已创建
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
        /// 启用
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool EnableInfos(List<string> rowsKeyValues, string loginUser)
        {
            ///看板卡状态⑩必须为已创建
            int cnt = dal.GetCounts("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and [STATUS] = " + (int)BasicDataStatusConstants.Created + "");
            if (cnt == 0)
                throw new Exception("MC:0x00000210");///已创建状态的看办卡才能进行启用

            ///操作完成时更新状态⑩为已启用
            string sql = "update [LES].[TM_MPM_KANBAN_CARD] WITH(ROWLOCK) set [STATUS] = " + (int)BasicDataStatusConstants.Enable + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() where [VALID_FLAG] = 1  and [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool InvalidInfos(List<string> rowsKeyValues, string loginUser)
        {
            ///看板卡状态⑩必须不为已作废
            int cnt = dal.GetCounts("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ") and [STATUS] <> " + (int)BasicDataStatusConstants.Disabled + "");
            if (cnt == 0)
                throw new Exception("MC:0x00000211");///此看办卡已作废

            ///操作完成时更新状态⑩为已作废，同时更新逻辑删除标记为否
            string sql = "update [LES].[TM_MPM_KANBAN_CARD] WITH(ROWLOCK) set [STATUS] = " + (int)BasicDataStatusConstants.Disabled + ",[MODIFY_USER] = N'" + loginUser + "',[MODIFY_DATE] = GETDATE() where [VALID_FLAG] = 1  and [ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")";
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }


        #endregion

        #region 自定义方法
        /// <summary>
        /// 根据看板号获取实体对象
        /// </summary>
        /// <param name="CardNo">看板号</param>
        /// <returns>返回 KanbanCardInfo 对象</returns>
        public KanbanCardInfo SelectInfoByCardNo(string cardNo, string loginUser)
        {
            KanbanCardInfo kanbancardinfo = dal.SelectInfoByCardNo(cardNo);
            if (kanbancardinfo == null) throw new Exception("MC:3x00000004");///标签信息错误
            string sqlstr = "UPDATE [LES].[TM_MPM_KANBAN_CARD] SET [USED_STATUS] =  20 ,[SCANNED_DATE] = GETDATE(),[SCANNED_USER] = N'" + loginUser + "' WHERE [CARD_NO] = N'" + cardNo + "';";
            if (!CommonDAL.ExecuteNonQueryBySql(sqlstr))
                throw new Exception("MC:0x00000276");///标签信息错误
            return kanbancardinfo;
        }
        /// <summary>
        /// 获取打印数据
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
        /// 批量创建看板卡
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool BatchcreationInfos(List<string> rowsKeyValues, string loginUser)
        {
            ///已选定的看板卡
            var kanbanCardInfofirst = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues.ToArray()) + ")", string.Empty).GroupBy(w => new { w.PartNo, w.PartBoxCode }).Select(w => new { w.Key.PartNo, w.Key.PartBoxCode });
            var kanbanCardInfosecend = dal.GetList(
                "[PART_NO] IN ('" + string.Join("','", kanbanCardInfofirst.GroupBy(w => new { w.PartNo }).Select(w => w.Key.PartNo).ToArray()) + "')"
                + "AND [PART_BOX_CODE] IN ('" + string.Join("','", kanbanCardInfofirst.GroupBy(w => new { w.PartBoxCode }).Select(w => w.Key.PartBoxCode).ToArray()) + "')"
                , string.Empty);

            List<KanbanCardInfo> kanbanCardInfos = (from a in kanbanCardInfosecend
                                                    join b in kanbanCardInfofirst on new { a.PartNo, a.PartBoxCode } equals new { b.PartNo, b.PartBoxCode }
                                                    select a).ToList<KanbanCardInfo>();

            if (kanbanCardInfos.Where(w => w.Status == (int)BasicDataStatusConstants.Disabled).Count() > 0)
                throw new Exception("MC:0x00000242");///只能批量添加已创建或已启用状态的看板卡

            ///选定看板卡对应的零件类
            List<KanbanPartBoxInfo> kanbanPartBoxInfos = new KanbanPartBoxDAL().GetList(
                string.Format("[PART_BOX_CODE] in ('{0}')", string.Join("','", kanbanCardInfos.GroupBy(w => new { w.PartBoxCode }).Select(w => w.Key.PartBoxCode).ToArray()))
                , string.Empty);
            if (kanbanPartBoxInfos.Count == 0)
                throw new Exception("MC:3x00000014");///看板零件类数据错误

            ///已看板卡对应的物料拉动信息
            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandardInfos
                    = new MaintainInhouseLogisticStandardDAL().GetList("[PART_NO] in ('" + string.Join("','", kanbanCardInfos.GroupBy(w => new { w.PartNo }).Select(w => w.Key.PartNo).ToArray()) + "') "
                    + "and [INHOUSE_PART_CLASS] in ('" + string.Join("','", kanbanPartBoxInfos.Select(d => d.PartBoxCode).ToArray()) + "') "
                    + "and [STATUS] = " + (int)BasicDataStatusConstants.Enable + " "
                    + "and [INHOUSE_SYSTEM_MODE] = N'" + (int)PullModeConstants.Kanban + "'", string.Empty);
            if (maintainInhouseLogisticStandardInfos.Count == 0)
                throw new Exception("MC:0x00000213");///物料拉动信息数据错误

            StringBuilder sqlBuilder = new StringBuilder();

            #region 看板卡插入脚本
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
                                                        + ",N'" + kanbanCardInfos.FirstOrDefault(w => w.SupplierCode == info.SupplierNum).SupplierName + "'"///供应商名称
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
            ///数据保存时使用SQL拼接多条insert语句方式一次提交执行，执行失败需要同步返回至客户端
            using (TransactionScope trans = new TransactionScope())
            {
                if (!CommonDAL.ExecuteNonQueryBySql(sqlBuilder.ToString()))
                    throw new Exception("MC:0x00000244");
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// 批量创建看板卡
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool SynchronizationKanBanCardInfos(string loginUser)
        {
            #region 准备数据源
            StringBuilder sqlBuilder = new StringBuilder();
            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandardInfos = new MaintainInhouseLogisticStandardDAL().GetList("[STATUS] = " + (int)BasicDataStatusConstants.Enable + "and [INHOUSE_SYSTEM_MODE] = N'" + (int)PullModeConstants.Kanban + "'", string.Empty);
            if (maintainInhouseLogisticStandardInfos.Count() == 0)
                throw new Exception("MC:0x00000256");///不存在可同步的物料看板拉动信息

            ///已生成的看板卡，除了作废的卡片之外
            List<KanbanCardInfo> kanbanCardInfos = dal.GetList("[STATUS] <> " + (int)BasicDataStatusConstants.Disabled, string.Empty);
            ///已生成的看板卡统计信息
            var groupKanbanCardInfos = kanbanCardInfos.GroupBy(w => new { w.PartNo, w.PartBoxCode }).Select(w => new { w.Key.PartBoxCode, w.Key.PartNo, SumCount = w.Count() }).ToList();
            ///看板零件类
            List<KanbanPartBoxInfo> kanbanPartBoxInfos = new KanbanPartBoxDAL().GetList(string.Format("[PART_BOX_CODE] in ('{0}') and [STATUS] = " + (int)BasicDataStatusConstants.Enable + "",
                string.Join("','", maintainInhouseLogisticStandardInfos.GroupBy(w => new { w.InhousePartClass }).Select(w => w.Key.InhousePartClass).ToArray())), string.Empty);
            ///供应商信息
            List<SupplierInfo> supplierinfos = new SupplierDAL().GetList(string.Format("[SUPPLIER_NUM] IN ('{0}')", maintainInhouseLogisticStandardInfos.GroupBy(w => new { w.SupplierNum }).Select(w => w.Key.SupplierNum).ToArray()), string.Empty);
            #endregion

            #region SQL脚本
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

            #region 编列数据源生成脚本
            foreach (var maintainInhouseLogisticStandardInfo in maintainInhouseLogisticStandardInfos)
            {
                ///计算出还可创建多少张看板卡
                int createCardCount = 0;
                var groupKanbanCardInfo = groupKanbanCardInfos.FirstOrDefault(w =>
                w.PartNo == maintainInhouseLogisticStandardInfo.PartNo &&
                w.PartBoxCode == maintainInhouseLogisticStandardInfo.InhousePartClass);
                if (groupKanbanCardInfo == null) createCardCount = maintainInhouseLogisticStandardInfo.KanbanCircleCnt.GetValueOrDefault();
                else createCardCount = maintainInhouseLogisticStandardInfo.KanbanCircleCnt.GetValueOrDefault() - groupKanbanCardInfo.SumCount;
                ///如果没有可创建的卡片则继续下一个
                if (createCardCount == 0) continue;
                ///获取看板零件类信息，且零件类必须处于已启用状态
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
                                                        + ",N'" + (supplierInfo == null ? string.Empty : supplierInfo.SupplierName) + "'"///供应商名称
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
                throw new Exception("MC:0x00000249");///不存在需要生成的看板卡

            ///批量创建看板卡时是否同时启用
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
        /// 扫描后提交生成看板拉动单
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
                throw new Exception("MC:0x00000399");///状态必须为已启用，且使用状态只能为未使用或已回库
            if (kanbanCardInfos.Count < rowsKeyValues.Count)
                throw new Exception("MC:0x00000399");///状态必须为已启用，且使用状态只能为未使用或已回库

            ///创建看板拉动单
            return new KanbanPullOrderBLL().CreateKanbanPullOrder(kanbanCardInfos.Select(d => d.CardNo).ToList(), loginUser);
        }
        #endregion

    }
}

