namespace WS.MPM.CounterDealWithVehicleStatusService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BLL.LES;
    using DM.LES;
    using DM.SYS;
    using System.Transactions;
    using DAL.LES;

    /// <summary>
    /// Handle
    /// </summary>
    public class Handle
    {
        /// <summary>
        /// 执行用户
        /// </summary>
        private string loginUser = "CounterDealWithVehicleStatus";
        /// <summary>
        /// Handler
        /// </summary>
        public void Handler()
        {
            ///获取状态为待处理的车辆状态信息，其中类型为正常过点、校验补入、车辆归队
            List<VehiclePointStatusInfo> vehiclePointStatusInfos = new VehiclePointStatusBLL().GetList("" +
                "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + " and " +
                "[VEHICLE_STATUS] in (" + (int)VehicleStatusTypeConstants.NormalPoint + "," + (int)VehicleStatusTypeConstants.CheckAndFill + "," + (int)VehicleStatusTypeConstants.VehicleReturn + ")", "[ID]");
            if (vehiclePointStatusInfos.Count == 0) return;

            #region TWD 计数器及基础数据获取
            ///根据状态点代码获取时间窗零件类中需求累计方式㉒为过点且状态为已启用⑭的数据
            List<TwdPartBoxInfo> twdPartBoxInfos = new TwdPartBoxBLL().GetList("" +
                "[REQUIREMENT_ACCUMULATE_MODE] = " + (int)RequirementAccumulateModeConstants.PassSpot + " and " +
                "[STATUS] =" + (int)BasicDataStatusConstants.Enable + " and " +
                "[STATUS_POINT_CODE] in ('" + string.Join("','", vehiclePointStatusInfos.Select(d => d.StatusPointCode).ToArray()) + "')", string.Empty);
            ///物料拉动信息
            List<MaintainInhouseLogisticStandardInfo> twdMaintainInhouseLogisticStandardInfos = new List<MaintainInhouseLogisticStandardInfo>();
            if (twdPartBoxInfos.Count > 0)
            {
                ///同时获取这些零件类下对应的已启用的物料拉动信息
                twdMaintainInhouseLogisticStandardInfos = new MaintainInhouseLogisticStandardBLL().GetList("" +
                    "[STATUS] =" + (int)BasicDataStatusConstants.Enable + " and " +
                    "[INHOUSE_SYSTEM_MODE] = N'" + (int)PullModeConstants.Twd + "' and " +
                    "[INHOUSE_PART_CLASS] in ('" + string.Join("','", twdPartBoxInfos.Select(d => d.PartBoxCode).ToArray()) + "')", string.Empty);
            }
            #endregion

            #region JIS 计数器及基础数据获取
            ///根据状态点代码获取排序零件类中状态㉖为已启用的数据
            List<JisPartBoxInfo> jisPartBoxInfos = new JisPartBoxBLL().GetList("" +
                "[STATUS] =" + (int)BasicDataStatusConstants.Enable + " and " +
                "[STATUS_POINT_CODE] in ('" + string.Join("','", vehiclePointStatusInfos.Select(d => d.StatusPointCode).ToArray()) + "')", string.Empty);
            ///物料拉动信息
            List<MaintainInhouseLogisticStandardInfo> jisMaintainInhouseLogisticStandardInfos = new List<MaintainInhouseLogisticStandardInfo>();
            if (jisPartBoxInfos.Count > 0)
            {
                ///同时获取这些零件类下对应的已启用的物料拉动信息
                jisMaintainInhouseLogisticStandardInfos = new MaintainInhouseLogisticStandardBLL().GetList("" +
                    "[STATUS] =" + (int)BasicDataStatusConstants.Enable + " and " +
                    "[INHOUSE_SYSTEM_MODE] = N'" + (int)PullModeConstants.Jis + "' and " +
                    "[INHOUSE_PART_CLASS] in ('" + string.Join("','", jisPartBoxInfos.Select(d => d.PartBoxCode).ToArray()) + "')", string.Empty);
            }
            #endregion

            foreach (VehiclePointStatusInfo vehiclePointStatusInfo in vehiclePointStatusInfos)
            {
                ///执行语句
                StringBuilder stringBuilder = new StringBuilder();
                ///TWD
                if (twdPartBoxInfos.Count > 0 && twdMaintainInhouseLogisticStandardInfos.Count > 0)
                    stringBuilder.AppendLine(TwdCounterDeal(vehiclePointStatusInfo, twdPartBoxInfos, twdMaintainInhouseLogisticStandardInfos));
                ///JIS
                if (jisPartBoxInfos.Count > 0 && jisMaintainInhouseLogisticStandardInfos.Count > 0)
                    stringBuilder.AppendLine(JisCounterDeal(vehiclePointStatusInfo, jisPartBoxInfos, jisMaintainInhouseLogisticStandardInfos));
                ///单条车辆状态全部类型计数器更新完成后标记其状态为已处理
                stringBuilder.AppendLine("update [LES].[TT_BAS_VEHICLE_POINT_STATUS] set " +
                    "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Processed + "," +
                    "[MODIFY_DATE] = GETDATE()," +
                    "[MODIFY_USER] = N'" + loginUser + "' where " +
                    "[ID]= " + vehiclePointStatusInfo.Id + ";");
                ///数据库语句执行
                using (TransactionScope trans = new TransactionScope())
                {
                    CommonBLL.ExecuteNonQueryBySql(stringBuilder.ToString());
                    trans.Complete();
                }
            }
        }
        /// <summary>
        /// TWD
        /// </summary>
        /// <param name="vehiclePointStatusInfo"></param>
        /// <param name="twdCounterInfos"></param>
        /// <param name="twdMaintainInhouseLogisticStandardInfos"></param>
        /// <returns></returns>
        private string TwdCounterDeal(VehiclePointStatusInfo vehiclePointStatusInfo, List<TwdPartBoxInfo> twdPartBoxInfos, List<MaintainInhouseLogisticStandardInfo> twdMaintainInhouseLogisticStandardInfos)
        {
            ///
            StringBuilder stringBuilder = new StringBuilder();
            ///根据车辆状态信息中的生产订单号①获取到对应的生产订单物料清单、此处为了保障执行效率，需要根据已获取的计数器的物料号⑩过滤获取物料清单
            ///ZORDNO = 生产订单号
            ///ZCOMNO = 物料图号
            List<PullOrderBomInfo> pullOrderBomInfos = new PullOrderBomBLL().GetList("" +
                "[ZORDNO] = N'" + vehiclePointStatusInfo.OrderNo + "' and " +
                "[ZCOMNO] in ('" + string.Join("','", twdMaintainInhouseLogisticStandardInfos.Select(d => d.PartNo).ToArray()) + "')", string.Empty);
            if (pullOrderBomInfos.Count == 0) return string.Empty;
            ///
            foreach (TwdPartBoxInfo twdPartBoxInfo in twdPartBoxInfos)
            {
                ///不是这个信息点的零件类忽略
                if (vehiclePointStatusInfo.StatusPointCode != twdPartBoxInfo.StatusPointCode) continue;
                ///本零件类对应的物料拉动信息
                List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandards = twdMaintainInhouseLogisticStandardInfos.Where(d => d.InhousePartClass == twdPartBoxInfo.PartBoxCode).ToList();
                ///若本零件类无物料拉动信息则返回
                if (twdMaintainInhouseLogisticStandardInfos.Count == 0) continue;
                ///零件类过滤后的物料拉动信息对应的物料清单
                List<PullOrderBomInfo> pullOrderBoms = pullOrderBomInfos.Where(d => maintainInhouseLogisticStandards.Select(m => m.PartNo).Contains(d.Zcomno)).ToList();
                ///循环过滤后的物料清单
                foreach (PullOrderBomInfo pullOrderBom in pullOrderBoms)
                {
                    ///匹配物料拉动信息的最小维度是 物料图号+供应商+工位，依次降低维度来获取唯一的物料拉动信息
                    MaintainInhouseLogisticStandardInfo maintainInhouseLogisticStandard = maintainInhouseLogisticStandards.FirstOrDefault(d =>
                    d.PartNo == pullOrderBom.Zcomno && d.SupplierNum == pullOrderBom.SupplierNum && d.Location == pullOrderBom.Zloc);
                    ///物料图号+供应商
                    if (maintainInhouseLogisticStandard == null)
                        maintainInhouseLogisticStandard = maintainInhouseLogisticStandards.FirstOrDefault(d => d.PartNo == pullOrderBom.Zcomno && d.SupplierNum == pullOrderBom.SupplierNum);
                    ///物料图号
                    if (maintainInhouseLogisticStandard == null)
                        maintainInhouseLogisticStandard = maintainInhouseLogisticStandards.FirstOrDefault(d => d.PartNo == pullOrderBom.Zcomno);
                    ///未能成功获取到正确的物料拉动信息
                    if (maintainInhouseLogisticStandard == null) continue;
                    ///根据物料拉动信息外键获取计数器，未能成功获取时需要创建
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
                    stringBuilder.AppendLine(TwdCounterBLL.UpdateTwdCounter(maintainInhouseLogisticStandard, twdPartBoxInfo, pullOrderBom.Zqty.GetValueOrDefault(), twdCounterInfo.Id, loginUser));
                    ///创建计数器日志
                    TwdCounterLogInfo twdCounterLogInfo = TwdCounterLogBLL.CreateTwdCounterLogInfo(twdCounterInfo.Fid.GetValueOrDefault(), loginUser);
                    ///以车辆过点信息填充计数器日志
                    TwdCounterLogBLL.GetTwdCounterLogInfo(vehiclePointStatusInfo, ref twdCounterLogInfo);
                    ///以物料拉动信息填充计数器日志
                    TwdCounterLogBLL.GetTwdCounterLogInfo(maintainInhouseLogisticStandard, ref twdCounterLogInfo);
                    ///以零件类信息填充计数器日志
                    TwdCounterLogBLL.GetTwdCounterLogInfo(twdPartBoxInfo, ref twdCounterLogInfo);
                    ///PART_QTY 
                    twdCounterLogInfo.PartQty = pullOrderBom.Zqty.GetValueOrDefault();
                    ///
                    stringBuilder.AppendLine(TwdCounterLogDAL.GetInsertSql(twdCounterLogInfo));
                    ///触发层级拉动
                    stringBuilder.AppendLine(TwdCounterBLL.LevelPullRequirementCounter(
                        maintainInhouseLogisticStandard,
                        pullOrderBom.Zqty.GetValueOrDefault(),
                        loginUser,
                        twdCounterInfo.Fid.GetValueOrDefault(),
                        twdCounterInfo.PartBoxCode));
                }
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// JIS
        /// </summary>
        /// <param name="vehiclePointStatusInfo"></param>
        /// <param name="jisPartBoxInfos"></param>
        /// <param name="jisCounterInfos"></param>
        /// <param name="jisMaintainInhouseLogisticStandardInfos"></param>
        /// <returns></returns>
        private string JisCounterDeal(VehiclePointStatusInfo vehiclePointStatusInfo, List<JisPartBoxInfo> jisPartBoxInfos, List<MaintainInhouseLogisticStandardInfo> jisMaintainInhouseLogisticStandardInfos)
        {
            ///
            StringBuilder stringBuilder = new StringBuilder();
            ///根据车辆状态信息中的生产订单号①获取到对应的生产订单物料清单、此处为了保障执行效率，需要根据已获取的物料拉动信息过滤获取物料清单
            List<PullOrderBomInfo> pullOrderBomInfos = new PullOrderBomBLL().GetList("" +
                "[ZORDNO] = N'" + vehiclePointStatusInfo.OrderNo + "' and " +
                "[ZCOMNO] in ('" + string.Join("','", jisMaintainInhouseLogisticStandardInfos.Select(d => d.PartNo).ToArray()) + "')", string.Empty);
            if (pullOrderBomInfos.Count == 0) return string.Empty;
            ///
            foreach (JisPartBoxInfo jisPartBoxInfo in jisPartBoxInfos)
            {
                ///不是这个信息点的零件类忽略
                if (vehiclePointStatusInfo.StatusPointCode != jisPartBoxInfo.StatusPointCode) continue;
                ///根据物料拉动信息外键获取计数器，未能成功获取时需要创建
                JisCounterInfo jisCounterInfo = new JisCounterBLL().GetInfoByPartBoxFid(jisPartBoxInfo.Fid.GetValueOrDefault());
                if (jisCounterInfo == null)
                    jisCounterInfo = CreateJisCounterInfo(jisPartBoxInfo);
                ///零件类对应的物料拉动信息
                List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandards = jisMaintainInhouseLogisticStandardInfos.Where(d => d.InhousePartClass == jisPartBoxInfo.PartBoxCode).ToList();
                if (maintainInhouseLogisticStandards.Count == 0) continue;
                ///零件类过滤后的物料拉动信息对应的物料清单
                List<PullOrderBomInfo> pullOrderBoms = pullOrderBomInfos.Where(d => maintainInhouseLogisticStandards.Select(m => m.PartNo).Contains(d.Zcomno)).ToList();
                if (pullOrderBoms.Count == 0) continue;
                ///当排序计数器的累计方式⑧为按车辆累计时
                if (jisPartBoxInfo.AccumulativeType.GetValueOrDefault() == (int)JisAccumulativeTypeConstants.VehicleAccumulative)
                {
                    ///本次可累计车辆数量
                    decimal vehicleQty = jisCounterInfo.AccumulativeQty.GetValueOrDefault() - jisCounterInfo.CurrentQty.GetValueOrDefault();
                    int vehicleCounterStatus = (int)JisCounterStatusConstants.Accumulating;
                    if (vehicleQty == 1)
                        vehicleCounterStatus = (int)JisCounterStatusConstants.AccumulativeCompletion;
                    ///
                    stringBuilder.AppendLine(UpdateJisCounter(jisPartBoxInfo, jisCounterInfo, 1, vehicleCounterStatus));
                }
                ///
                foreach (PullOrderBomInfo pullOrderBom in pullOrderBoms)
                {
                    ///匹配物料拉动信息的最小维度是 物料图号+供应商+工位，依次降低维度来获取唯一的物料拉动信息
                    MaintainInhouseLogisticStandardInfo maintainInhouseLogisticStandard = maintainInhouseLogisticStandards.FirstOrDefault(d =>
                    d.PartNo == pullOrderBom.Zcomno && d.SupplierNum == pullOrderBom.SupplierNum && d.Location == pullOrderBom.Zloc);
                    ///物料图号+供应商
                    if (maintainInhouseLogisticStandard == null)
                        maintainInhouseLogisticStandard = maintainInhouseLogisticStandards.FirstOrDefault(d => d.PartNo == pullOrderBom.Zcomno && d.SupplierNum == pullOrderBom.SupplierNum);
                    ///物料图号
                    if (maintainInhouseLogisticStandard == null)
                        maintainInhouseLogisticStandard = maintainInhouseLogisticStandards.FirstOrDefault(d => d.PartNo == pullOrderBom.Zcomno);
                    ///未能成功获取到正确的物料拉动信息
                    if (maintainInhouseLogisticStandard == null) continue;
                    ///当排序计数器的累计方式⑧为按车辆累计时
                    if (jisPartBoxInfo.AccumulativeType.GetValueOrDefault() == (int)JisAccumulativeTypeConstants.VehicleAccumulative)
                        stringBuilder.AppendLine(UpdateJisCounter(jisPartBoxInfo, jisCounterInfo, vehiclePointStatusInfo, maintainInhouseLogisticStandard, pullOrderBom.Zqty.GetValueOrDefault()));
                    ///当排序计数器的累计方式⑧为按器具累计时
                    if (jisPartBoxInfo.AccumulativeType.GetValueOrDefault() == (int)JisAccumulativeTypeConstants.UtensilAccumulative)
                    {
                        ///物料需求数量
                        decimal requireQty = pullOrderBom.Zqty.GetValueOrDefault();
                        while (requireQty > 0)
                        {
                            int jisCounterStatus = (int)JisCounterStatusConstants.Accumulating;
                            ///本次可累计数量
                            decimal currentQty = jisCounterInfo.AccumulativeQty.GetValueOrDefault() - jisCounterInfo.CurrentQty.GetValueOrDefault();
                            if (currentQty == 0) break;
                            ///需求大于本次可累计
                            if (currentQty <= requireQty)
                            {
                                ///剩余需累计数量
                                requireQty -= currentQty;
                                ///累计完成
                                jisCounterStatus = (int)JisCounterStatusConstants.AccumulativeCompletion;
                            }
                            else
                            {
                                ///将需求赋予本次
                                currentQty = requireQty;
                                ///需求清空
                                requireQty = 0;
                            }
                            ///
                            stringBuilder.AppendLine(UpdateJisCounter(jisPartBoxInfo, jisCounterInfo, currentQty, jisCounterStatus));
                            ///
                            stringBuilder.AppendLine(UpdateJisCounter(jisPartBoxInfo, jisCounterInfo, vehiclePointStatusInfo, maintainInhouseLogisticStandard, currentQty));
                            ///当剩余需求数量大于零时，需要创建计数器
                            if (requireQty > 0)
                                jisCounterInfo = CreateJisCounterInfo(jisPartBoxInfo);
                        }
                    }
                    ///触发层级拉动
                    stringBuilder.AppendLine(TwdCounterBLL.LevelPullRequirementCounter(
                        maintainInhouseLogisticStandard,
                        pullOrderBom.Zqty.GetValueOrDefault(),
                        loginUser,
                        jisCounterInfo.Fid.GetValueOrDefault(),
                        jisCounterInfo.PartBoxCode));
                }

            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// JisPartBoxInfo - > jisCounterInfo
        /// </summary>
        /// <param name="jisPartBoxInfo"></param>
        /// <param name="jisCounterInfo"></param>
        private JisCounterInfo CreateJisCounterInfo(JisPartBoxInfo jisPartBoxInfo)
        {
            ///创建计数器
            JisCounterInfo jisCounterInfo = JisCounterBLL.CreateJisCounterInfo(loginUser);
            ///以物料拉动信息填充计数器
            JisCounterBLL.GetJisCounterInfo(jisPartBoxInfo, ref jisCounterInfo);
            ///
            jisCounterInfo.Id = new JisCounterBLL().InsertInfo(jisCounterInfo);
            if (jisCounterInfo.Id == 0)
                throw new Exception("MC:0x00000454");///排序计数器创建失败
            return jisCounterInfo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jisPartBoxInfo"></param>
        /// <param name="jisCounterInfo"></param>
        /// <param name="vehiclePointStatusInfo"></param>
        /// <param name="maintainInhouseLogisticStandardInfo"></param>
        /// <param name="currentQty"></param>
        /// <param name="jisCounterStatus"></param>
        /// <returns></returns>
        private string UpdateJisCounter(JisPartBoxInfo jisPartBoxInfo, JisCounterInfo jisCounterInfo, decimal currentQty, int jisCounterStatus)
        {
            return "update [LES].[TT_MPM_JIS_COUNTER] set " +
                "[PART_BOX_CODE] = N'" + jisPartBoxInfo.PartBoxCode + "'," +
                "[PLANT] = N'" + jisPartBoxInfo.Plant + "'," +
                "[PLANT_ZONE] = N'" + jisPartBoxInfo.PlantZone + "'," +
                "[WORKSHOP] = N'" + jisPartBoxInfo.Workshop + "'," +
                "[ASSEMBLY_LINE] = N'" + jisPartBoxInfo.AssemblyLine + "'," +
                "[WORKSHOP_SECTION] = N'" + jisPartBoxInfo.WorkshopSection + "'," +
                "[LOCATION] = N'" + jisPartBoxInfo.Location + "'," +
                "[SUPPLIER_NUM] = N'" + jisPartBoxInfo.SupplierNum + "'," +
                "[ACCUMULATIVE_TYPE] = " + jisPartBoxInfo.AccumulativeType.GetValueOrDefault() + "," +
                "[ACCUMULATIVE_QTY] = " + jisPartBoxInfo.AccumulativeQty.GetValueOrDefault() + "," +
                "[PACKAGE_MODEL] = N'" + jisPartBoxInfo.PackageModel + "'," +
                "[CURRENT_QTY] = isnull([CURRENT_QTY],0) + " + currentQty + "," +
                "[STATUS] = " + jisCounterStatus + "," +
                "[MODIFY_DATE] = GETDATE()," +
                "[MODIFY_USER] = N'" + loginUser + "' where " +
                "[ID] = " + jisCounterInfo.Id + ";";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jisPartBoxInfo"></param>
        /// <param name="jisCounterInfo"></param>
        /// <param name="vehiclePointStatusInfo"></param>
        /// <param name="maintainInhouseLogisticStandardInfo"></param>
        /// <param name="currentQty"></param>
        /// <returns></returns>
        private string UpdateJisCounter(JisPartBoxInfo jisPartBoxInfo, JisCounterInfo jisCounterInfo, VehiclePointStatusInfo vehiclePointStatusInfo, MaintainInhouseLogisticStandardInfo maintainInhouseLogisticStandardInfo, decimal currentQty)
        {
            ///创建计数器日志
            JisCounterLogInfo jisCounterLogInfo = JisCounterLogBLL.CreateJisCounterLogInfo(jisCounterInfo.Fid.GetValueOrDefault(), loginUser);
            ///以车辆过点信息填充计数器日志
            JisCounterLogBLL.GetJisCounterLogInfo(vehiclePointStatusInfo, ref jisCounterLogInfo);
            ///以零件类信息填充计数器日志
            JisCounterLogBLL.GetJisCounterLogInfo(jisPartBoxInfo, ref jisCounterLogInfo);
            ///以物料拉动信息填充计数器日志
            JisCounterLogBLL.GetJisCounterLogInfo(maintainInhouseLogisticStandardInfo, ref jisCounterLogInfo);
            ///PART_QTY 
            jisCounterLogInfo.PartQty = currentQty;
            ///
            return JisCounterLogDAL.GetInsertSql(jisCounterLogInfo);
        }
    }
}