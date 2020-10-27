namespace BLL.LES
{
    using BLL.SYS;
    using DAL.LES;
    using DAL.SYS;
    using DM.LES;
    using DM.SYS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Transactions;
    /// <summary>
    /// KanbanPullOrderBLL
    /// </summary>
    public class KanbanPullOrderBLL
    {
        #region Common
        /// <summary>
        /// KanbanPullOrderDAL
        /// </summary>
        KanbanPullOrderDAL dal = new KanbanPullOrderDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<KanbanPullOrderInfo></returns>
        public List<KanbanPullOrderInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public KanbanPullOrderInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(KanbanPullOrderInfo info)
        {
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
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        #endregion

        #region 流程动作
        /// <summary>
        /// 完成
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool CompleteInfos(List<string> rowsKeyValues, string loginUser)
        {
            ///看板拉动单状态⑱必须为20.已发布
            List<KanbanPullOrderInfo> kanbanPullOrderInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues) + ")", string.Empty);
            if (kanbanPullOrderInfos.Count == 0)
                throw new Exception("MC:0x00000036");///看板拉动单不存在
            List<KanbanPullOrderDetailInfo> kanbanPullOrderDetailInfos = new KanbanPullOrderDetailDAL().GetList("" +
                "[ORDER_FID] in ('" + string.Join("','", kanbanPullOrderInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", string.Empty);
            if (kanbanPullOrderDetailInfos.Count == 0)
                throw new Exception("MC:0x00000036");///看板拉动单不存在

            string sql = string.Empty;
            foreach (var kanbanPullOrderInfo in kanbanPullOrderInfos)
            {
                if (kanbanPullOrderInfo.Status != (int)PullOrderStatusConstants.Released)
                    throw new Exception("MC:0x00000041");///只有已发布状态的看板拉动单可以执行完成
                List<KanbanPullOrderDetailInfo> kanbanPullOrderDetails = kanbanPullOrderDetailInfos.Where(d => d.OrderFid.GetValueOrDefault() == kanbanPullOrderInfo.Fid.GetValueOrDefault()).ToList();
                if (kanbanPullOrderDetails.Count == 0) continue;
                ///更新看板卡状态
                sql += "update [LES].[TM_MPM_KANBAN_CARD] set " +
                    "[USED_STATUS] = " + (int)KanbanCardUseStatusConstants.Outbound + "," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() where " +
                    "[CARD_NO] in ('" + string.Join("','", kanbanPullOrderDetails.Select(d => d.CardNo).ToArray()) + "') and " +
                    "[VALID_FLAG] = 1;";
                ///更新看板拉动单明细
                sql += "update [LES].[TT_MPM_KANBAN_PULL_ORDER_DETAIL] set " +
                    "[ORDER_STATUS] = " + (int)PullOrderStatusConstants.Pickuped + "," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() where " +
                    "[ORDER_FID] = N'" + kanbanPullOrderInfo.Fid.GetValueOrDefault() + "' and " +
                    "[VALID_FLAG] = 1;";
                ///看板拉动单状态更新
                sql += "update [LES].[TT_MPM_KANBAN_PULL_ORDER] set " +
                    "[STATUS] = " + (int)PullOrderStatusConstants.Pickuped + "," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() where " +
                    "[ID] = " + kanbanPullOrderInfo.Id + ";";
            }
            ///获取出库单主键集合
            List<string> outputIds = new OutputDAL().GetRowsKeyValues(kanbanPullOrderInfos.Select(d => d.OrderCode).ToList());
            using (TransactionScope trans = new TransactionScope())
            {
                new OutputBLL().CompleteInfos(outputIds, loginUser);
                CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool CloseInfos(List<string> rowsKeyValues, string loginUser)
        {
            ///看板拉动单状态⑱必须为20.已发布或30.已拣料            
            List<KanbanPullOrderInfo> kanbanPullOrderInfos = dal.GetList("[ID] IN (" + string.Join(",", rowsKeyValues) + ")", string.Empty);
            if (kanbanPullOrderInfos.Count == 0)
                throw new Exception("MC:0x00000036");///看板拉动单不存在
            List<KanbanPullOrderDetailInfo> kanbanPullOrderDetailInfos = new KanbanPullOrderDetailDAL().GetList("" +
                "[ORDER_FID] in ('" + string.Join("','", kanbanPullOrderInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", string.Empty);
            if (kanbanPullOrderDetailInfos.Count == 0)
                throw new Exception("MC:0x00000036");///看板拉动单不存在
            string sql = string.Empty;
            foreach (var kanbanPullOrderInfo in kanbanPullOrderInfos)
            {
                if (kanbanPullOrderInfo.Status.GetValueOrDefault() != (int)PullOrderStatusConstants.Released
                    && kanbanPullOrderInfo.Status.GetValueOrDefault() != (int)PullOrderStatusConstants.Pickuped)
                    throw new Exception("MC:0x00000039");///看板拉动单状态为已发布或已拣料才可以关闭
                List<KanbanPullOrderDetailInfo> kanbanPullOrderDetails = kanbanPullOrderDetailInfos.Where(d => d.OrderFid.GetValueOrDefault() == kanbanPullOrderInfo.Fid.GetValueOrDefault()).ToList();
                if (kanbanPullOrderDetails.Count == 0) continue;
                ///更新看板卡状态
                sql += "update [LES].[TM_MPM_KANBAN_CARD] set " +
                    "[USED_STATUS] = " + (int)KanbanCardUseStatusConstants.Reback + "," +///TODO:线边无操作直接回库状态
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() where " +
                    "[CARD_NO] in ('" + string.Join("','", kanbanPullOrderDetails.Select(d => d.CardNo).ToArray()) + "') and " +
                    "[VALID_FLAG] = 1;";
                ///更新看板拉动单明细
                sql += "update [LES].[TT_MPM_KANBAN_PULL_ORDER_DETAIL] set " +
                    "[ORDER_STATUS] = " + (int)PullOrderStatusConstants.Distribution + "," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() where " +
                    "[ORDER_FID] = N'" + kanbanPullOrderInfo.Fid.GetValueOrDefault() + "' and " +
                    "[VALID_FLAG] = 1;";
                ///看板拉动单状态更新
                sql += "update [LES].[TT_MPM_KANBAN_PULL_ORDER] set " +
                    "[STATUS] = " + (int)PullOrderStatusConstants.Distribution + "," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() where " +
                    "[ID] = " + kanbanPullOrderInfo.Id + ";";
            }
            ///获取出库单主键集合
            List<string> outputIds = new OutputDAL().GetRowsKeyValues(kanbanPullOrderInfos.Select(d => d.OrderCode).ToList());
            using (TransactionScope trans = new TransactionScope())
            {
                new OutputBLL().CloseInfos(outputIds, loginUser);
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
            ///看板拉动单状态⑱必须为20.已发布
            List<KanbanPullOrderInfo> kanbanPullOrderInfos = dal.GetList("[ID] in (" + string.Join(",", rowsKeyValues) + ")", string.Empty);
            if (kanbanPullOrderInfos.Count == 0)
                throw new Exception("MC:0x00000036");///看板拉动单不存在
            List<KanbanPullOrderDetailInfo> kanbanPullOrderDetailInfos = new KanbanPullOrderDetailDAL().GetList("" +
                "[ORDER_FID] in ('" + string.Join("','", kanbanPullOrderInfos.Select(d => d.Fid.GetValueOrDefault()).ToArray()) + "')", string.Empty);
            if (kanbanPullOrderDetailInfos.Count == 0)
                throw new Exception("MC:0x00000036");///看板拉动单不存在
            string sql = string.Empty;
            foreach (var kanbanPullOrderInfo in kanbanPullOrderInfos)
            {
                if (kanbanPullOrderInfo.Status != (int)PullOrderStatusConstants.Released)
                    throw new Exception("MC:0x00000400");///状态为已发布时可以作废

                List<KanbanPullOrderDetailInfo> kanbanPullOrderDetails = kanbanPullOrderDetailInfos.Where(d => d.OrderFid.GetValueOrDefault() == kanbanPullOrderInfo.Fid.GetValueOrDefault()).ToList();
                if (kanbanPullOrderDetails.Count == 0) continue;
                ///更新看板卡状态
                sql += "update [LES].[TM_MPM_KANBAN_CARD] set " +
                    "[USED_STATUS] = " + (int)KanbanCardUseStatusConstants.NotUsed + "," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() where " +
                    "[CARD_NO] in ('" + string.Join("','", kanbanPullOrderDetails.Select(d => d.CardNo).ToArray()) + "') and " +
                    "[VALID_FLAG] = 1;";
                ///更新看板拉动单明细
                sql += "update [LES].[TT_MPM_KANBAN_PULL_ORDER_DETAIL] set " +
                    "[ORDER_STATUS] = " + (int)PullOrderStatusConstants.Invalid + "," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() where " +
                    "[ORDER_FID] = N'" + kanbanPullOrderInfo.Fid.GetValueOrDefault() + "' and " +
                    "[VALID_FLAG] = 1;";
                ///看板拉动单状态更新
                sql += "update [LES].[TT_MPM_KANBAN_PULL_ORDER] set " +
                    "[STATUS] = " + (int)PullOrderStatusConstants.Invalid + "," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() where " +
                    "[ID] = " + kanbanPullOrderInfo.Id + ";";
            }
            ///获取出库单主键集合
            List<string> outputIds = new OutputDAL().GetRowsKeyValues(kanbanPullOrderInfos.Select(d => d.OrderCode).ToList());
            using (TransactionScope trans = new TransactionScope())
            {
                new OutputBLL().InvalidInfos(outputIds, loginUser);
                CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }

            return true;
        }
        #endregion

        #region 看板生成拉动单
        /// <summary>
        /// 看板生成拉动单
        /// </summary>
        /// <param name="cardNos"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool CreateKanbanPullOrder(List<String> cardNos, string loginUser, bool isFilterLoginUser = false)
        {
            if (cardNos.Count == 0)
                throw new Exception("MC:3x00000011");///看板卡数据不能为空

            #region 初始化SQL脚本
            ///主表字符串
            string sqlKanbanPullOrder = @"insert into [LES].[TT_MPM_KANBAN_PULL_ORDER] (
                FID,
                ORDER_CODE,
				PART_BOX_CODE,
				PART_BOX_NAME,
				ROUTE_CODE,
				ROUTE_NAME,
				SOURCE_ZONE_NO,
				SOURCE_WM_NO,
				TARGET_ZONE_NO,
				TARGET_WM_NO,
				PICK_UP_FINISH_TIME,
				PICK_UP_USER,
				DELIVERY_FINISH_TIME,
				DELIVERY_USER,
				PRINT_CNT,
				PRINT_TIME,
				PRINT_USER,
				STATUS,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER) VALUES ({0});";
            ///明细表字符串
            string sqlKanbanPullOrderDetail = @"insert into [LES].[TT_MPM_KANBAN_PULL_ORDER_DETAIL] (
                FID,
				ORDER_FID,
				ORDER_CODE,
                ORDER_STATUS,
				CARD_NO,
				PART_NO,
				PART_NAME,
				SUPPLIER_CODE,
				SUPPLIER_NAME,
				PART_QTY,
				PACKAGE_CODE,
				PACKAGE_QTY,
				VALID_FLAG,
				CREATE_DATE,
				CREATE_USER) VALUES ({0});";
            #endregion

            ///获取所有看板卡信息

            List<KanbanCardInfo> kanbanCardInfos = GetKanBanCardsListIsByLoginUser(cardNos, loginUser, isFilterLoginUser);
            if (kanbanCardInfos.Count == 0)
                throw new Exception("MC:3x00000011");///看板卡数据不能为空
            if (cardNos.Count != kanbanCardInfos.Count)
                throw new Exception("MC:0x00000257");///所提交的看板卡状态必须为已启用且不能为已扫描

            ///根据看板卡信息中的零件类代码②对物料需求进行分组
            var groupbyPartboxcode = kanbanCardInfos.GroupBy(ml => new { ml.PartBoxCode }).Select(w => w.Key).ToList();

            #region 根据零件类获取看板拉动物料拉动对应拉动零件
            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandardInfos
                    = new MaintainInhouseLogisticStandardBLL().GetList(
                    "[INHOUSE_PART_CLASS] in ('"
                    + string.Join("','", groupbyPartboxcode.Select(w => w.PartBoxCode))
                    + "') and [STATUS] = " + (int)BasicDataStatusConstants.Enable
                    + " AND [INHOUSE_SYSTEM_MODE]=N'"
                    + (int)PullModeConstants.Kanban
                    + "'", string.Empty);
            if (maintainInhouseLogisticStandardInfos.Count == 0)
                throw new Exception("0x00000233");///没有已启用的物料拉动信息 
            #endregion

            #region 获取所有看板零件类以分组方式去重
            List<KanbanPartBoxInfo> kanbanPartBoxInfos = new KanbanPartBoxDAL().GetList(
                    string.Format("[STATUS] = "
                    + (int)BasicDataStatusConstants.Enable
                    + " and [PART_BOX_CODE] in ('{0}') ", string.Join("','", groupbyPartboxcode.Select(w => w.PartBoxCode))), string.Empty);
            if (kanbanPartBoxInfos.Count == 0)
                throw new Exception("MC:3x00000014");///看板零件类数据错误 
            #endregion

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("update [LES].[TM_MPM_KANBAN_CARD] " +
                "set [USED_STATUS] = " + (int)KanbanCardUseStatusConstants.Scaned + "," +
                "[MODIFY_USER] = N'" + loginUser + "'," +
                "[MODIFY_DATE] = GETDATE() " +
                "where [ID] in (" + string.Join(",", kanbanCardInfos.Select(d => d.Id).ToArray()) + ");");

            #region 遍历看板卡信息
            foreach (var partboxcode in groupbyPartboxcode)
            {
                ///获取看板零件类信息
                KanbanPartBoxInfo kanbanPartBoxInfo = kanbanPartBoxInfos.FirstOrDefault(d => d.PartBoxCode == partboxcode.PartBoxCode);
                ///TT_MPM_KANBAN_PULL_ORDER.FID
                Guid pullOrderFid = Guid.NewGuid();
                ///看板拉动单号②依据序列号规则中的KANBAN_PULL_ORDER_CODE进行生成，明细中拉动单号③相同于主表
                string orderCode = new SeqDefineDAL().GetCurrentCode("KANBAN_PULL_ORDER_CODE", "KB");///TODO:看板拉动单号生成

                ///看板拉动单外键①为NEW.GUID，同时作为看板拉动单明细的主表外键②，明细中外键①为NEW.GUID
                ///物流路线代码⑤名称⑥、来源仓库⑧存储区⑦、目标仓库⑩存储区⑨从零件类代码③对应的看板零件类TM_MPM_KANBAN_PART_BOX信息中获取
                ///创建时间获取数据库服务器时间，创建人取自于参数操作用户
                ///创建后看板拉动单状态⑱为20.已发布，数据有效标记⑲为true，看板拉动单其它字段在生成时留空
                #region TT_MPM_KANBAN_PULL_ORDER
                string sqlKanbanPullOrderValue = "N'" + pullOrderFid.ToString() + "'"///FID
                                            + ",N'" + orderCode + "'"///ORDER_CODE
                                            + ",N'" + kanbanPartBoxInfo.PartBoxCode + "'"///PART_BOX_CODE
                                            + ",N'" + kanbanPartBoxInfo.PartBoxName + "'"///PART_BOX_NAME
                                            + ",N'" + kanbanPartBoxInfo.RouteCode + "'"///ROUTE_CODE
                                            + ",N'" + kanbanPartBoxInfo.RouteName + "'"///ROUTE_NAME
                                            + ",N'" + kanbanPartBoxInfo.SourceZoneNo + "'"///SOURCE_ZONE_NO
                                            + ",N'" + kanbanPartBoxInfo.SourceWmNo + "'"///SOURCE_WM_NO
                                            + ",N'" + kanbanPartBoxInfo.TargetZoneNo + "'"///TARGET_ZONE_NO
                                            + ",N'" + kanbanPartBoxInfo.TargetWmNo + "'"///TARGET_WM_NO
                                            + ",NULL"///PICK_UP_FINISH_TIME
                                            + ",NULL"///PICK_UP_USER
                                            + ",NULL"///DELIVERY_FINISH_TIME
                                            + ",NULL"///DELIVERY_USER
                                            + ",NULL"///PRINT_CNT
                                            + ",NULL"///PRINT_TIME
                                            + ",NULL"///PRINT_USER
                                            + "," + (int)PullOrderStatusConstants.Released///STATUS
                                            + ",1"///VALID_FLAG
                                            + ",GETDATE()"///CREATE_DATE
                                            + ",N'" + loginUser + "'";///CREATE_USER
                #endregion

                stringBuilder.AppendLine(string.Format(sqlKanbanPullOrder, sqlKanbanPullOrderValue));

                List<KanbanCardInfo> kanbanCards = kanbanCardInfos.Where(d => d.PartBoxCode == partboxcode.PartBoxCode).ToList();

                #region 根据看板卡信息获取所有零件物料拉动信息
                var maintainInhouseLogisticStandardInfosparts = from a in maintainInhouseLogisticStandardInfos
                                                                join b in kanbanCards.Where(w => w.PartBoxCode == partboxcode.PartBoxCode).GroupBy(w => new { w.PartBoxCode, w.PartNo }).Select(w => new { w.Key.PartBoxCode, w.Key.PartNo })
                                                                on new { a.InhousePartClass, a.PartNo } equals new { InhousePartClass = b.PartBoxCode, b.PartNo }
                                                                select a;
                #endregion


                foreach (var kanbanCard in kanbanCards)
                {
                    ///看板卡号①=④、物料号④=⑤、物料名称⑤=⑥、物料数量⑧=⑨、包装型号⑨=⑩
                    ///包装数量⑪默认为1，数据有效标记⑫为true
                    ///若看板卡信息中指定了供应商代码⑥名称⑦，则看板拉动单明细中供应商代码⑦名称⑧从看板卡信息中获取
                    #region TT_MPM_KANBAN_PULL_ORDER_DETAIL
                    string sqlKanbanPullOrderDetailValue = "NEWID()"///FID
                                                        + ",N'" + pullOrderFid.ToString() + "'"///ORDER_FID
                                                        + ",N'" + orderCode + "'"///ORDER_CODE
                                                        + "," + (int)PullOrderStatusConstants.Released///ORDER_STATUS
                                                        + ",N'" + kanbanCard.CardNo + "'"///CARD_NO
                                                        + ",N'" + kanbanCard.PartNo + "'"///PART_NO
                                                        + ",N'" + kanbanCard.PartName + "'"///PART_NAME
                                                        + ",N'" + kanbanCard.SupplierCode + "'"///SUPPLIER_CODE
                                                        + ",N'" + kanbanCard.SupplierName + "'"///SUPPLIER_NAME
                                                        + "," + kanbanCard.PartQty.GetValueOrDefault()///PART_QTY
                                                        + ",N'" + kanbanCard.PackageCode + "'"///PACKAGE_CODE
                                                        + ",1"///PACKAGE_QTY
                                                        + ",1"///VALID_FLAG
                                                        + ",GETDATE()"///CREATE_DATE
                                                        + ",N'" + loginUser + "'";///CREATE_USER
                    #endregion

                    stringBuilder.AppendLine(string.Format(sqlKanbanPullOrderDetail, sqlKanbanPullOrderDetailValue));
                }
                #region 单据衔接
                MaterialPullingOrderInfo mpOrder = new MaterialPullingOrderInfo();
                mpOrder.OrderNo = orderCode;
                mpOrder.PartBoxCode = kanbanPartBoxInfo.PartBoxCode;///零件类2
                mpOrder.PartBoxName = kanbanPartBoxInfo.PartBoxName; ///零件类名称3
                mpOrder.Plant = string.Empty;///工厂4
                mpOrder.Workshop = string.Empty;///车间5
                mpOrder.AssemblyLine = string.Empty;///流水线6
                mpOrder.SupplierNum = string.Empty; ///供应商代码7
                mpOrder.SupplierName = string.Empty; ///供应商名称
                mpOrder.SourceZoneNo = kanbanPartBoxInfo.SourceZoneNo;///来源存储区8
                mpOrder.SourceWmNo = kanbanPartBoxInfo.SourceWmNo;///来源仓库9
                mpOrder.TargetZoneNo = kanbanPartBoxInfo.TargetZoneNo;///目标存储区10
                mpOrder.TargetWmNo = kanbanPartBoxInfo.TargetWmNo;///目标仓库11
                mpOrder.TargetDock = string.Empty;///道口12
                mpOrder.PlanShippingTime = DateTime.Now.AddMinutes(kanbanPartBoxInfo.PickUpTime.GetValueOrDefault());///建议交货时间
                mpOrder.PlanDeliveryTime = mpOrder.PlanShippingTime.GetValueOrDefault().AddMinutes(kanbanPartBoxInfo.DeliveryTime.GetValueOrDefault());///预计到厂时间14 = 建议交货时间 – 送货时间
                mpOrder.PublishTime = DateTime.Now;
                mpOrder.AsnFlag = false;
                mpOrder.OrderType = 0;///TODO:对于看板拉动单应该的值，如果后续用到则需要定义枚举
                mpOrder.MaterialPullingOrderDetailInfos = (from m in maintainInhouseLogisticStandardInfosparts
                                                           select new MaterialPullingOrderDetailInfo
                                                           {
                                                               OrderNo = orderCode,///拉动单号1
                                                               SupplierNum = m.SupplierNum,///供应商2
                                                               PartNo = m.PartNo,///物料号3
                                                               PartCname = m.PartCname,///物料号中文名称4
                                                               PartEname = m.PartEname,///物料号英文名称5
                                                               //Uom = m.PartUnits,///计量单位6 
                                                               PackageQty = m.InboundPackage.GetValueOrDefault(),///入库单包装数量7
                                                               PackageModel = m.InboundPackageModel,///入库包装编号8
                                                               RequirePackageQty = kanbanCards.Where(w => w.PartNo == m.PartNo).Count(),///需求包装数量9
                                                               RequirePartQty = (kanbanCards.Where(w => w.PartNo == m.PartNo).Sum(w => w.PartQty) ?? 0),///需求物料数量10
                                                               TargetZoneNo = kanbanPartBoxInfo.TargetZoneNo,
                                                               TargetWmNo = kanbanPartBoxInfo.TargetWmNo
                                                           }).ToList();
                ///执行单据衔接
                stringBuilder.AppendLine(MaterialPullingCommonBLL.Handler(mpOrder, loginUser));
                #endregion

            }
            #endregion

            ///数据保存时使用SQL拼接多条insert语句方式一次提交执行，执行失败需要同步返回至客户端
            using (TransactionScope trans = new TransactionScope())
            {
                CommonDAL.ExecuteNonQueryBySql(stringBuilder.ToString());
                trans.Complete();
            }
            return true;
        }

        public List<KanbanCardInfo> GetKanBanCardsListIsByLoginUser(List<String> cardNos, string loginUser, bool isFilterLoginUser = false)
        {
            string whereSql = string.Format("[CARD_NO] in ('{0}') " + "and [STATUS] = " + (int)BasicDataStatusConstants.Enable, string.Join("','", cardNos));

            if (isFilterLoginUser)
            {
                whereSql += "and ISNULL([USED_STATUS],0) = " + (int)KanbanCardUseStatusConstants.Scaned;
                whereSql += "and [SCANNED_USER] = '" + loginUser + "'";

            }

            return new KanbanCardDAL().GetList(whereSql, string.Empty);
        }


        #endregion

        #region 打印获取数据源方法
        public DataSet GetPrintDatas(List<string> rowsKeyValues, string loginUser)
        {
            //根据预设看板拉动单格式进行打印，格式等待业务部门提供
            //打印成功后记录最后打印时间⑯、最后打印用户⑰、累计打印次数⑮
            List<KanbanPullOrderInfo> list = new KanbanPullOrderDAL().GetList(string.Format("[ID] IN ({0})", string.Join(",", rowsKeyValues.ToArray())), string.Empty);
            if (list.Count == 0)
                throw new Exception("MC:0x00000072");//没有打印文件生成
            string sql = "SELECT * FROM [LES].[TT_MPM_KANBAN_PULL_ORDER] WHERE [ID] IN({0}) AND VALID_FLAG = 1;";
            sql = string.Format(sql, string.Join(",", rowsKeyValues.ToArray()));
            sql += string.Format("SELECT * FROM [LES].[TT_MPM_KANBAN_PULL_ORDER_DETAIL] WHERE [VALID_FLAG] = 1 AND [ORDER_FID] IN('{0}');", string.Join("','", list.Select(w => w.Fid).ToArray()));
            return DAL.SYS.CommonDAL.ExecuteDataSetBySql(sql);
        }
        public void GetPrintCallBack(List<string> rowsKeyValues, string loginUser)
        {
            string sql = string.Empty;
            DataTable dt = DAL.SYS.CommonDAL.ExecuteDataTableBySql("select * from [TS_SYS_PRINT_CONFIG] where [VALID_FLAG] = 1 and [PRINT_CONFIG_CODE] = 'KANBAN_CARD'");
            sql += "update [LES].[TT_MPM_KANBAN_PULL_ORDER] set [PRINT_TIME] =  GETDATE(),[PRINT_CNT] =isnull([PRINT_CNT],0)+" + dt.Rows[0]["PRINT_COPIES"] + ",[PRINT_USER] = N'" + loginUser + "' where [ID] in (" + string.Join(",", rowsKeyValues) + ")";
            DAL.SYS.CommonDAL.ExecuteScalar(sql);
        }
        #endregion
    }
}

