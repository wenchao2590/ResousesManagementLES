namespace WS.MPM.CounterDealWithStocksService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BLL.LES;
    using DM.LES;
    using DM.SYS;
    using BLL.SYS;
    using System.Transactions;
    using System;
    using DAL.LES;

    public class Handle
    {
        /// <summary>
        /// 执行用户
        /// </summary>
        private string loginUser = "CounterDealWithStocksService";
        /// <summary>
        /// 供应商库存维度
        /// </summary>
        private string supplier_stocks_dimension = new ConfigBLL().GetValueByCode("SUPPLIER_STOCKS_DIMENSION");
        /// <summary>
        /// 主函数
        /// </summary>
        public void Handler()
        {
            ///获取需求累计方式㉒为库存当量的零件类TM_MPM_TWD_PART_BOX
            List<TwdPartBoxInfo> twdPartBoxInfos = new TwdPartBoxBLL().GetList("" +
                "[REQUIREMENT_ACCUMULATE_MODE] = " + (int)RequirementAccumulateModeConstants.InventoryEquivalent + " and " +
                "[STATUS] = " + (int)BasicDataStatusConstants.Enable + "", string.Empty);
            if (twdPartBoxInfos.Count == 0) return;
            ///同时获取对应的物料拉动信息
            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandardInfos = new MaintainInhouseLogisticStandardBLL().GetList("" +
                "[STATUS] = " + (int)BasicDataStatusConstants.Enable + " and " +
                "[INHOUSE_SYSTEM_MODE] = N'" + (int)PullModeConstants.Twd + "' and " +
                "[INHOUSE_PART_CLASS] in ('" + string.Join("','", twdPartBoxInfos.Select(d => d.PartBoxCode).ToArray()) + "')", string.Empty);
            if (maintainInhouseLogisticStandardInfos.Count == 0) return;
            ///根据物料拉动信息中的工厂③目标仓库⑩存储区⑪和物料号获取库存数据
            List<StocksInfo> stocksInfos = new StocksBLL().GetList("" +
                "[PLANT] in ('" + string.Join("','", maintainInhouseLogisticStandardInfos.Select(d => d.Plant).ToArray()) + "') and " +
                "[WM_NO] in ('" + string.Join("','", maintainInhouseLogisticStandardInfos.Select(d => d.TWmNo).ToArray()) + "') and " +
                "[ZONE_NO] in ('" + string.Join("','", maintainInhouseLogisticStandardInfos.Select(d => d.TZoneNo).ToArray()) + "') and " +
                "[PART_NO] in ('" + string.Join("','", maintainInhouseLogisticStandardInfos.Select(d => d.PartNo).ToArray()) + "')", string.Empty);

            StringBuilder stringBuilder = new StringBuilder();
            ///以零件类进行循环
            foreach (TwdPartBoxInfo twdPartBoxInfo in twdPartBoxInfos)
            {
                ///获取零件类对应的物料拉动信息
                List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandards = maintainInhouseLogisticStandardInfos.Where(d => d.InhousePartClass == twdPartBoxInfo.PartBoxCode).ToList();
                if (maintainInhouseLogisticStandards.Count == 0) continue;
                ///物料拉动信息
                foreach (MaintainInhouseLogisticStandardInfo maintainInhouseLogisticStandard in maintainInhouseLogisticStandards)
                {
                    ///当系统配置中库存供应商维度标记 = true时，在过滤库存数据时需要考虑供应商
                    ///若物料拉动信息中未维护供应商信息则不考虑，因为两个标记同时符合时才会考虑
                    string supplierNum = string.Empty;
                    if (supplier_stocks_dimension.ToLower() == "true" && !string.IsNullOrEmpty(maintainInhouseLogisticStandard.SupplierNum))
                        supplierNum = maintainInhouseLogisticStandard.SupplierNum;
                    ///将工厂③目标仓库⑩存储区⑪物料号或增加供应商维度的过滤完成的库存数据集合中的可用数量进行汇总
                    ///当前库存
                    decimal avaibleQty = new StocksBLL().GetAvailbleQty(
                        maintainInhouseLogisticStandard.PartNo,
                        maintainInhouseLogisticStandard.TWmNo,
                        maintainInhouseLogisticStandard.TZoneNo,
                        supplierNum);
                    ///根据获得的物料拉动信息外键获取计数器
                    TwdCounterInfo twdCounterInfo = TwdCounterBLL.GetInfoByPartPullFid(maintainInhouseLogisticStandard.Fid);
                    if (twdCounterInfo == null)
                    {
                        ///创建计数器
                        twdCounterInfo = TwdCounterBLL.CreateTwdCounterInfo(loginUser);
                        ///以物料拉动信息填充计数器
                        TwdCounterBLL.GetTwdCounterInfo(maintainInhouseLogisticStandard, ref twdCounterInfo);
                        ///以零件类信息填充计数器
                        TwdCounterBLL.GetTwdCounterInfo(twdPartBoxInfo, ref twdCounterInfo);
                        ///
                        twdCounterInfo.Id = new TwdCounterBLL().InsertInfo(twdCounterInfo);
                        if (twdCounterInfo.Id == 0)
                            throw new Exception("MC:0x00000453");///时间窗计数器创建失败
                    }
                    ///计数器状态未处于启用
                    if (twdCounterInfo.Status != (int)BasicDataStatusConstants.Enable) continue;
                    ///在途库存，已累积 + 已生成未完成TODO:
                    decimal onroadQty = twdCounterInfo.CurrentQty.GetValueOrDefault();
                    ///当可用数量小于等于物料拉动信息中的拉动最小值时，将拉动最大值减去汇总数量得到的则为本次需求数量
                    if (avaibleQty + onroadQty > maintainInhouseLogisticStandard.Min.GetValueOrDefault()) continue;
                    ///本次需求数量
                    decimal requireQty = maintainInhouseLogisticStandard.Max.GetValueOrDefault() - avaibleQty - onroadQty;
                    ///
                    stringBuilder.AppendLine(TwdCounterBLL.UpdateTwdCounter(maintainInhouseLogisticStandard, twdPartBoxInfo, requireQty, twdCounterInfo.Id, loginUser));
                    ///创建计数器日志
                    TwdCounterLogInfo twdCounterLogInfo = TwdCounterLogBLL.CreateTwdCounterLogInfo(twdCounterInfo.Fid.GetValueOrDefault(), loginUser);
                    ///以物料拉动信息填充计数器日志
                    TwdCounterLogBLL.GetTwdCounterLogInfo(maintainInhouseLogisticStandard, ref twdCounterLogInfo);
                    ///以零件类信息填充计数器日志
                    TwdCounterLogBLL.GetTwdCounterLogInfo(twdPartBoxInfo, ref twdCounterLogInfo);
                    ///PART_QTY 
                    twdCounterLogInfo.PartQty = requireQty;
                    ///SOURCE_DATA_FID 
                    twdCounterLogInfo.SourceDataFid = Guid.Empty;
                    ///SOURCE_DATA_TYPE 
                    twdCounterLogInfo.SourceDataType = (int)TwdCounterSourceDataTypeConstants.Inventory;
                    ///SOURCE_DATA ，供应商|当前可用|在途数量
                    twdCounterLogInfo.SourceData = supplierNum + "|" + avaibleQty + "|" + onroadQty;
                    ///
                    stringBuilder.AppendLine(TwdCounterLogDAL.GetInsertSql(twdCounterLogInfo));
                    ///触发层级拉动
                    stringBuilder.AppendLine(TwdCounterBLL.LevelPullRequirementCounter(
                        maintainInhouseLogisticStandard,
                        requireQty,
                        loginUser,
                        twdCounterInfo.Fid.GetValueOrDefault(),
                        twdCounterInfo.PartBoxCode));
                }
                ///数据库执行
                using (TransactionScope trans = new TransactionScope())
                {
                    if (stringBuilder.Length > 0)
                        BLL.LES.CommonBLL.ExecuteNonQueryBySql(stringBuilder.ToString());
                    trans.Complete();
                }
                stringBuilder.Clear();
            }
        }
    }
}