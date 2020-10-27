namespace WS.MPM.CreateTwdPullOrderService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DM.LES;
    using DM.SYS;
    using BLL.LES;
    using System.Transactions;
    using DAL.LES;

    /// <summary>
    /// 生成拉动单
    /// </summary>
    public class Handle
    {
        /// <summary>
        /// 执行用户
        /// </summary>
        private string loginUser = "CreateTwdPullOrderService";
        /// <summary>
        /// Handler
        /// </summary>
        public void Handler()
        {
            ///获取发单状态⑪为10未发单，且发单时间⑨已小于等于当前时间的时间窗窗口时间数据
            ///按发单时间⑨从早到晚进行排序，若发单时间⑨相同的情况下按ID排序，如果其中有相同零件类的多条窗口时间数据则以发单时间⑨最晚的一条作为有效数据
            List<TwdWindowTimeInfo> twdWindowTimeInfos = new TwdWindowTimeBLL().GetList("" +
                "[SEND_TIME_STATUS] = " + (int)SendTimeStatusConstants.NoSend + " and " +
                "[SEND_TIME] <= GETDATE() and " +
                "[SEND_TIME] = (select max([SEND_TIME]) from [LES].[TT_MPM_TWD_WINDOW_TIME] a with(nolock) " +
                "where [LES].[TT_MPM_TWD_WINDOW_TIME].[PART_BOX_FID] = a.[PART_BOX_FID])", "[ID]");
            if (twdWindowTimeInfos.Count == 0) return;
            ///根据窗口时间中的零件类外键①获取时间窗零件类、同时获取对应的物料拉动信息、注意此处的数据都需要为已启用状态
            List<TwdPartBoxInfo> twdPartBoxInfos = new TwdPartBoxBLL().GetList("" +
                "[STATUS] =" + (int)BasicDataStatusConstants.Enable + " and " +
                "[FID] in ('" + string.Join("','", twdWindowTimeInfos.Select(d => d.PartBoxFid.GetValueOrDefault()).ToArray()) + "')", string.Empty);
            if (twdPartBoxInfos.Count == 0) return;
            ///物料拉动信息
            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandardInfos = new MaintainInhouseLogisticStandardBLL().GetList("" +
                "[STATUS] =" + (int)BasicDataStatusConstants.Enable + " and " +
                "[INHOUSE_PART_CLASS] in ('" + string.Join("','", twdPartBoxInfos.Select(d => d.PartBoxCode).ToArray()) + "') and " +
                "[INHOUSE_SYSTEM_MODE] = N'" + (int)PullModeConstants.Twd + "'", string.Empty);
            if (maintainInhouseLogisticStandardInfos.Count == 0) return;
            ///供应商信息
            List<string> supplierNums = twdPartBoxInfos.Where(d => !string.IsNullOrEmpty(d.SupplierNum)).Select(d => d.SupplierNum).ToList();
            supplierNums.AddRange(maintainInhouseLogisticStandardInfos.Where(d => !string.IsNullOrEmpty(d.SupplierNum)).Select(d => d.SupplierNum).ToList());
            List<SupplierInfo> supplierInfos = new SupplierBLL().GetList("" +
                "[SUPPLIER_NUM] in ('" + string.Join("','", supplierNums.ToArray()) + "')", string.Empty);
            ///逐条进行处理
            foreach (TwdWindowTimeInfo twdWindowTimeInfo in twdWindowTimeInfos)
            {
                ///获取未发单时间窗对应的零件类
                TwdPartBoxInfo twdPartBoxInfo = twdPartBoxInfos.FirstOrDefault(d => d.Fid.GetValueOrDefault() == twdWindowTimeInfo.PartBoxFid.GetValueOrDefault());
                if (twdPartBoxInfo == null) continue;
                ///获取零件类对应的物料拉动信息
                List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandards = maintainInhouseLogisticStandardInfos.Where(d => d.InhousePartClass == twdWindowTimeInfo.PartBoxCode).ToList();
                if (maintainInhouseLogisticStandards.Count == 0) continue;
                ///根据已获得的物料拉动信息获取对应的计数器，此处可以直接过滤出其当前计数⑮大于零的数据
                List<TwdCounterInfo> twdCounterInfos = new TwdCounterBLL().GetList("" +
                    "[STATUS] =" + (int)BasicDataStatusConstants.Enable + " and " +
                    "[PART_PULL_FID] in ('" + string.Join("','", maintainInhouseLogisticStandards.Select(d => d.Fid).ToArray()) + "')  and " +
                    "isnull([CURRENT_QTY],0) > 0", string.Empty);
                if (twdCounterInfos.Count == 0) continue;
                ///生成拉动单
                StringBuilder @string = new StringBuilder();
                @string.AppendLine(CreateTwdPullOrder(twdCounterInfos, twdPartBoxInfo, supplierInfos, twdWindowTimeInfo, maintainInhouseLogisticStandards));
                ///数据库语句执行
                using (TransactionScope trans = new TransactionScope())
                {
                    if (@string.Length > 0)
                        CommonBLL.ExecuteNonQueryBySql(@string.ToString());
                    trans.Complete();
                }
            }
        }

        /// <summary>
        /// 生成拉动单
        /// </summary>
        /// <param name="twdPartBoxInfo"></param>
        /// <param name="twdWindowTimeInfo"></param>
        /// <param name="supplierInfo"></param>
        /// <param name="twdCounterInfos"></param>
        /// <param name="maintainInhouseLogisticStandardInfos"></param>
        /// <returns></returns>
        private string CreateTwdPullOrder(List<TwdCounterInfo> twdCounterInfos, TwdPartBoxInfo twdPartBoxInfo, List<SupplierInfo> supplierInfos, TwdWindowTimeInfo twdWindowTimeInfo,
            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandardInfos)
        {
            ///
            StringBuilder @string = new StringBuilder();
            ///Create TwdPullOrderInfo
            TwdPullOrderInfo twdPullOrderInfo = null;
            ///Create PcsPullOrderInfo
            PcsPullOrderInfo pcsPullOrderInfo = null;
            ///仓储衔接主表
            MaterialPullingOrderInfo materialPulling = MaterialPullingCommonBLL.CreateMaterialPullingOrderInfo();
            if (twdPartBoxInfo.TwdPullMode.GetValueOrDefault() == (int)TwdPullModeConstants.Pcs)
            {
                pcsPullOrderInfo = PcsPullOrderBLL.CreatePcsPullOrderInfo(loginUser);
                ///TwdWindowTimeInfo -> PcsPullOrderInfo
                PcsPullOrderBLL.GetPcsPullOrderInfo(twdWindowTimeInfo, ref pcsPullOrderInfo);
                ///TwdPartBoxInfo -> PcsPullOrderInfo
                PcsPullOrderBLL.GetPcsPullOrderInfo(twdPartBoxInfo, ref pcsPullOrderInfo);
                ///SupplierInfo -> PcsPullOrderInfo
                SupplierInfo supplierInfo = supplierInfos.FirstOrDefault(d => d.SupplierNum == twdPartBoxInfo.SupplierNum);
                PcsPullOrderBLL.GetPcsPullOrderInfo(supplierInfo, ref pcsPullOrderInfo);
                ///
                @string.AppendLine(PcsPullOrderDAL.GetInsertSql(pcsPullOrderInfo));
                ///PcsPullOrderInfo -> MaterialPullingOrderInfo
                MaterialPullingCommonBLL.GetMaterialPullingOrderInfo(pcsPullOrderInfo, ref materialPulling);
            }
            else
            {
                twdPullOrderInfo = TwdPullOrderBLL.CreateTwdPullOrderInfo(loginUser);
                ///TwdWindowTimeInfo -> TwdPullOrderInfo
                TwdPullOrderBLL.GetTwdPullOrderInfo(twdWindowTimeInfo, ref twdPullOrderInfo);
                ///TwdPartBoxInfo -> TwdPullOrderInfo
                TwdPullOrderBLL.GetTwdPullOrderInfo(twdPartBoxInfo, ref twdPullOrderInfo);
                ///SupplierInfo -> TwdPullOrderInfo
                SupplierInfo supplierInfo = supplierInfos.FirstOrDefault(d => d.SupplierNum == twdPartBoxInfo.SupplierNum);
                TwdPullOrderBLL.GetTwdPullOrderInfo(supplierInfo, ref twdPullOrderInfo);
                ///
                @string.AppendLine(TwdPullOrderDAL.GetInsertSql(twdPullOrderInfo));
                ///TwdPullOrderInfo -> MaterialPullingOrderInfo
                MaterialPullingCommonBLL.GetMaterialPullingOrderInfo(twdPullOrderInfo, ref materialPulling);
            }
            ///行号
            int rowNo = 0;
            ///循环计数器
            foreach (TwdCounterInfo twdCounterInfo in twdCounterInfos)
            {
                ///物料需求数量
                decimal requiredPartQty = 0;
                ///物料需求箱数
                int packageQty = 0;

                #region 圆整方式
                ///向上圆整
                if (twdCounterInfo.RoundnessMode == (int)RoundnessModeConstants.Upward)
                {
                    if (twdCounterInfo.Package.GetValueOrDefault() > 0)
                    {
                        ///根据计数器中的圆整方式⑨，当圆整方式⑨为20.向上时以Math.Ceiling(当前计数⑮/箱内数量⑯)*箱内数量⑯作为本次的物料需求数量
                        requiredPartQty = Math.Ceiling(twdCounterInfo.CurrentQty.GetValueOrDefault() / twdCounterInfo.Package.GetValueOrDefault()) * twdCounterInfo.Package.GetValueOrDefault();
                        packageQty = Convert.ToInt32(Math.Ceiling(requiredPartQty / twdCounterInfo.Package.GetValueOrDefault()));
                    }
                }
                ///向下圆整
                if (twdCounterInfo.RoundnessMode == (int)RoundnessModeConstants.Downward)
                {
                    if (twdCounterInfo.Package.GetValueOrDefault() > 0)
                    {
                        ///根据计数器中的圆整方式⑨，当圆整方式⑨为20.向上时以Math.Ceiling(当前计数⑮/箱内数量⑯)*箱内数量⑯作为本次的物料需求数量
                        requiredPartQty = Math.Floor(twdCounterInfo.CurrentQty.GetValueOrDefault() / twdCounterInfo.Package.GetValueOrDefault()) * twdCounterInfo.Package.GetValueOrDefault();
                        packageQty = Convert.ToInt32(Math.Ceiling(requiredPartQty / twdCounterInfo.Package.GetValueOrDefault()));
                    }
                }
                ///按需求数量
                if (twdCounterInfo.RoundnessMode == (int)RoundnessModeConstants.Ondemand)
                {
                    ///当圆整方式⑨为按需时则当前计数⑮直接作为本次的物料需求数量
                    requiredPartQty = twdCounterInfo.CurrentQty.GetValueOrDefault();
                    packageQty = Convert.ToInt32(Math.Ceiling(requiredPartQty / twdCounterInfo.Package.GetValueOrDefault()));
                }
                #endregion

                ///获取物料拉动信息
                MaintainInhouseLogisticStandardInfo logisticStandardInfo = maintainInhouseLogisticStandardInfos.FirstOrDefault(d => d.Fid == twdCounterInfo.PartPullFid.GetValueOrDefault());
                if (logisticStandardInfo == null) continue;
                ///若计数器对应的物料拉动信息中最小起订包装数为一个大于零的数字，则需要在此处比对物料需求箱数是否大于等于最小起订包装数，若不满足则不产生拉动
                if (logisticStandardInfo.MinPullBox.GetValueOrDefault() > 0 && packageQty < logisticStandardInfo.MinPullBox.GetValueOrDefault()) continue;
                ///若计数器对应的物料拉动信息中批量包装数为一个大于零的数字，则需要Math.Floor(物料需求箱数 / 批量包装数) * 批量包装数，作为新的物料需求箱数，同时* 箱内数量⑯获得新的物料需求数量
                if (logisticStandardInfo.BatchPullBox.GetValueOrDefault() > 0 && twdCounterInfo.Package.GetValueOrDefault() > 0)
                {
                    packageQty = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(packageQty) / logisticStandardInfo.BatchPullBox.GetValueOrDefault())) * logisticStandardInfo.BatchPullBox.GetValueOrDefault();
                    requiredPartQty = packageQty * twdCounterInfo.Package.GetValueOrDefault();
                }

                ///仓储衔接明细表
                MaterialPullingOrderDetailInfo detailInfo = MaterialPullingCommonBLL.CreateMaterialPullingOrderDetailInfo();
                if (twdPartBoxInfo.TwdPullMode.GetValueOrDefault() == (int)TwdPullModeConstants.Pcs)
                {
                    ///
                    PcsPullOrderDetailInfo pullOrderDetailInfo = PcsPullOrderDetailBLL.CreatePcsPullOrderDetailInfo(loginUser);
                    ///MaintainInhouseLogisticStandardInfo -> TwdPullOrderDetailInfo
                    PcsPullOrderDetailBLL.GetPcsPullOrderDetailInfo(logisticStandardInfo, ref pullOrderDetailInfo);
                    ///TwdPullOrderInfo -> TwdPullOrderDetailInfo
                    PcsPullOrderDetailBLL.GetPcsPullOrderDetailInfo(pcsPullOrderInfo, ref pullOrderDetailInfo);
                    ///ROW_NO,行号
                    pullOrderDetailInfo.RowNo = ++rowNo;
                    ///REQUIRED_PACKAGE_QTY,需求包装数
                    pullOrderDetailInfo.RequiredPackageQty = packageQty;
                    ///REQUIRED_PART_QTY,需求物料数量
                    pullOrderDetailInfo.RequiredPartQty = requiredPartQty;
                    ///
                    @string.AppendLine(PcsPullOrderDetailDAL.GetInsertSql(pullOrderDetailInfo));
                    ///TwdPullOrderDetailInfo -> MaterialPullingOrderDetailInfo
                    MaterialPullingCommonBLL.GetMaterialPullingOrderDetailInfo(pullOrderDetailInfo, ref detailInfo);
                }
                else
                {
                    ///
                    TwdPullOrderDetailInfo pullOrderDetailInfo = TwdPullOrderDetailBLL.CreateTwdPullOrderDetailInfo(loginUser);
                    ///MaintainInhouseLogisticStandardInfo -> TwdPullOrderDetailInfo
                    TwdPullOrderDetailBLL.GetTwdPullOrderDetailInfo(logisticStandardInfo, ref pullOrderDetailInfo);
                    ///TwdPullOrderInfo -> TwdPullOrderDetailInfo
                    TwdPullOrderDetailBLL.GetTwdPullOrderDetailInfo(twdPullOrderInfo, ref pullOrderDetailInfo);
                    ///ROW_NO,行号
                    pullOrderDetailInfo.RowNo = ++rowNo;
                    ///REQUIRED_PACKAGE_QTY,需求包装数
                    pullOrderDetailInfo.RequiredPackageQty = packageQty;
                    ///REQUIRED_PART_QTY,需求物料数量
                    pullOrderDetailInfo.RequiredPartQty = requiredPartQty;
                    ///
                    @string.AppendLine(TwdPullOrderDetailDAL.GetInsertSql(pullOrderDetailInfo));
                    ///TwdPullOrderDetailInfo -> MaterialPullingOrderDetailInfo
                    MaterialPullingCommonBLL.GetMaterialPullingOrderDetailInfo(pullOrderDetailInfo, ref detailInfo);
                }
                ///
                materialPulling.MaterialPullingOrderDetailInfos.Add(detailInfo);

                #region 扣减计数器
                ///最后根据物料需求数量扣减对应的计数器的当前计数⑮
                @string.AppendLine("update [LES].[TT_MPM_TWD_COUNTER] " +
                    "set [CURRENT_QTY] = isnull([CURRENT_QTY],0) - " + requiredPartQty + "," +
                    "[MODIFY_DATE] = GETDATE()," +
                    "[MODIFY_USER] = N'" + loginUser + "' " +
                    "where [ID]= " + twdCounterInfo.Id + ";");
                ///
                TwdCounterLogInfo twdCounterLogInfo = TwdCounterLogBLL.CreateTwdCounterLogInfo(twdCounterInfo.Fid.GetValueOrDefault(), loginUser);
                ///TwdCounterInfo -> TwdCounterLogInfo
                TwdCounterLogBLL.GetTwdCounterLogInfo(twdCounterInfo, ref twdCounterLogInfo);
                ///PART_QTY,物料数量
                twdCounterLogInfo.PartQty = 0 - requiredPartQty;
                ///SOURCE_DATA,目视来源数据
                twdCounterLogInfo.SourceData = twdPullOrderInfo.OrderCode;
                ///SOURCE_DATA_FID,数据来源外键
                twdCounterLogInfo.SourceDataFid = twdPullOrderInfo.Fid;
                ///SOURCE_DATA_TYPE,数据来源类型
                twdCounterLogInfo.SourceDataType = (int)TwdCounterSourceDataTypeConstants.CreateRunsheet;
                ///
                @string.AppendLine(TwdCounterLogDAL.GetInsertSql(twdCounterLogInfo));
                #endregion
            }
            ///更新时间窗窗口时间为已发单状态
            @string.AppendLine("update [LES].[TT_MPM_TWD_WINDOW_TIME] " +
                "set [SEND_TIME_STATUS]= " + (int)SendTimeStatusConstants.Sent + "," +
                "[MODIFY_DATE] = GETDATE()," +
                "[MODIFY_USER] = N'" + loginUser + "' " +
                "where [ID]= " + twdWindowTimeInfo.Id + ";");
            ///拉动单生成后需要调用拉动仓储衔接函数获取语句                 
            @string.AppendLine(MaterialPullingCommonBLL.Handler(materialPulling, loginUser));
            return @string.ToString();
        }
    }
}