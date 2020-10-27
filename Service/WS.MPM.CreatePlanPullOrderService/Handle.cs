using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.SYS;
using DM.LES;
using DM.SYS;
using BLL.LES;
using System.Transactions;
using DAL.LES;

namespace WS.MPM.CreatePlanPullOrderService
{
    /// <summary>
    /// 生成拉动单
    /// </summary>
    public class Handle
    {
        #region 全局变量
        /// <summary>
        /// 登录用户
        /// </summary>
        string loginUser = "CreatePlanPullOrderService";
        ///系统配置中新增PLAN_PULL_WINDOW_TIME_ENABLE参数
        string planPullWindowTimeEnable = new ConfigBLL().GetValueByCode("PLAN_PULL_WINDOW_TIME_ENABLE");
        /// <summary>
        /// 物料检验模式作为分单依据
        /// </summary>
        string inspectionModeDistinguishOrderFlag = new ConfigBLL().GetValueByCode("INSPECTION_MODE_DISTINGUISH_ORDER_FLAG");
        #endregion

        #region Handler
        /// <summary>
        /// Handler
        /// </summary>
        public void Handler()
        {
            ///获取已启用计划零件类集合
            List<PlanPartBoxInfo> planPartBoxInfos = new PlanPartBoxBLL().GetList("[STATUS] = " + (int)BasicDataStatusConstants.Enable + "", string.Empty);
            if (planPartBoxInfos.Count == 0)
                throw new Exception("3x00000023");///没有已启用的计划拉动零件类

            ///获取零件类对应物料拉动信息
            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandardInfos
                = new MaintainInhouseLogisticStandardBLL().GetList("[INHOUSE_PART_CLASS] in ('" + string.Join("','", planPartBoxInfos.Select(d => d.PartBoxCode).ToArray()) + "') and [STATUS] = " + (int)BasicDataStatusConstants.Enable + " and [INHOUSE_SYSTEM_MODE]=N'60'", string.Empty);
            if (maintainInhouseLogisticStandardInfos.Count == 0)
                throw new Exception("0x00000233");///没有已启用的物料拉动信息

            ///循环零件类集合
            foreach (PlanPartBoxInfo planPartBoxInfo in planPartBoxInfos)
            {
                #region 基础变量
                ///窗口时间
                string windowTimes = string.Empty;
                ///当前窗口时间
                DateTime currentWindowTime = DateTime.MinValue;
                ///下一窗口时间
                DateTime nextWindowTime = DateTime.MaxValue;
                ///时区
                string timeZone = string.Empty;
                List<long> planTimeIds = new List<long>();
                ///范围 工厂、车间、产线
                string sqlwhere = string.Empty;
                if (!string.IsNullOrEmpty(planPartBoxInfo.AssemblyLine))
                    sqlwhere = "and [ASSEMBLY_LINE] = N'" + planPartBoxInfo.AssemblyLine + "' ";
                else
                    sqlwhere = "and [WERK] = N'" + planPartBoxInfo.Plant + "' ";
                #endregion

                #region 获取生产订单BOM集合
                ///通过这些生产订单号①=①获取订单BOM(TT_BAS_PULL_ORDER_BOM)
                List<PullOrderBomInfo> pullOrderBomInfos = new List<PullOrderBomInfo>();
                ///生产订单集合
                List<PullOrdersInfo> pullOrdersInfos = new List<PullOrdersInfo>();
                ///获取零件类对应的最近一次未发单窗口时间

                ///当此参数为true时以计划拉动窗口时间TT_MPM_PLAN_WINDOW_TIME中发单状态⑥为10.未发单且发单时间④已早于当前时间作为当前执行依据                
                if (planPullWindowTimeEnable.ToLower() == "true")
                {
                    PlanWindowTimeInfo planWindowTimeInfo = new PlanWindowTimeBLL().GetLastNoSendTimeInfo(planPartBoxInfo.Fid.GetValueOrDefault());
                    ///并根据窗口时间⑤匹配生产订单计划执行时间同时核对未生成计划拉动单的生产订单(先以小于等于窗口时间作为条件再加上TT_MPM_PLAN_PULL_CREATE_STATUS表的SEQID not exist条件进行过滤)
                    ///如果没有符合条件的窗口时间，则执行下一个零件类
                    if (planWindowTimeInfo == null) continue;
                    if (planWindowTimeInfo.WindowTime == null) continue;
                    planTimeIds.Add(planWindowTimeInfo.Id);
                    currentWindowTime = planWindowTimeInfo.WindowTime.GetValueOrDefault();
                    ///窗口时间
                    windowTimes = currentWindowTime.ToString("MM:dd");
                    ///时区为窗口时间中设定的时间
                    timeZone = planWindowTimeInfo.TimeZone;
                    nextWindowTime = new PlanWindowTimeBLL().GetNextWindowTime(planPartBoxInfo.Fid.GetValueOrDefault(), currentWindowTime);
                    ///获取未处理生产订单对应的BOM
                    pullOrderBomInfos = new PullOrderBomBLL().GetUnPlanPullingOrders(currentWindowTime, nextWindowTime, sqlwhere, planPartBoxInfo.Fid.GetValueOrDefault());
                }
                ///若为false时以计划当日零点作为交付时间依据并根据零件类中设定的时间合计⑫+⑬+⑭扣减后作为发单时间
                ///并根据计划当日零点(大于等于)与计划次日零点(小于)之间同时核对未生成计划拉动单的生产订单
                else if (planPullWindowTimeEnable.ToLower() == "false")
                {
                    ///获取计划拉动提前期(分钟)
                    int sumLeadTime = planPartBoxInfo.DelayTime.GetValueOrDefault()
                        + planPartBoxInfo.DeliveryTime.GetValueOrDefault()
                        + planPartBoxInfo.PickUpTime.GetValueOrDefault();
                    ///根据提前期用当前时间向后推得窗口时间的零点
                    currentWindowTime = DateTime.Parse(DateTime.Now.AddMinutes(sumLeadTime).ToShortDateString());
                    ///下一窗口时间为次日零点
                    nextWindowTime = currentWindowTime.AddDays(1);
                    ///获取未处理生产订单对应的BOM
                    pullOrderBomInfos = new PullOrderBomBLL().GetUnPlanPullingOrders(currentWindowTime, nextWindowTime, sqlwhere, planPartBoxInfo.Fid.GetValueOrDefault());
                    ///拼接窗口时间
                    List<DateTime> planWindowTimes = new PlanWindowTimeBLL().GetWindowTimesByWorkDay(planPartBoxInfo.Fid.GetValueOrDefault(), currentWindowTime);
                    if (planWindowTimes.Count > 0)
                        windowTimes = string.Join(",", planWindowTimes.Select(d => d.ToString("HH:mm")).ToArray());
                    ///拼接时区
                    List<PlanWindowTimeInfo> PlanWindowTimeInfos = new PlanWindowTimeBLL().GetList("", "WINDOW_TIME");
                    if (PlanWindowTimeInfos.Count > 0)
                    {
                        foreach (var p in PlanWindowTimeInfos)
                        {
                            planTimeIds.Add(p.Id);
                        }
                        timeZone = string.Join(",", PlanWindowTimeInfos.Select(d => d.TimeZone).ToArray());
                    }
                }
                else
                    throw new Exception("1x00000045");///系统配置参数错误
                #endregion

                #region 处理集合取交集
                ///根据物料号、供应商、工厂将BOM分组并计算数量
                var pullOrderBomQuery = pullOrderBomInfos
                    .GroupBy(b => new { b.Zcomno, b.SupplierNum, b.Zkwerk })
                    .Select(p => new { PartNo = p.Key.Zcomno, p.Key.SupplierNum, Plant = p.Key.Zkwerk, Zqty = p.Sum(x => x.Zqty.GetValueOrDefault()) }).ToList();
                ///获取零件类对应的物料拉动信息
                var maintainInhouseLogisticStandardQuery = maintainInhouseLogisticStandardInfos.Where(d => d.InhousePartClass == planPartBoxInfo.PartBoxCode).ToList();
                ///校验是否全部维护了入库单包装数量，否则除数为零时程序会报错
                int cnt = maintainInhouseLogisticStandardQuery.Count(d => d.InboundPackage.GetValueOrDefault() == 0);
                if (cnt > 0)
                    throw new Exception("3x00000024");///物料拉动信息中入库单包装数量未维护

                ///并将计划零件类对应的物料拉动信息与订单BOM的交集部分作为计划拉动物料需求（比对依据为物料号②、供应商代码③、工厂）
                var materialRequirementInfos = (from m in maintainInhouseLogisticStandardQuery
                                                join b in pullOrderBomQuery
                                                on new { m.PartNo, m.SupplierNum, m.Plant }
                                                equals new { b.PartNo, b.SupplierNum, b.Plant }
                                                select new
                                                {
                                                    m.PartNo,///物料号
                                                    m.SupplierNum,///供应商代码
                                                    m.Plant,///工厂代码
                                                    m.PartCname,///物料中文描述
                                                    m.PartEname,///物料英文描述
                                                    m.PartUnits,///计量单位
                                                    m.InboundPackage,///入库单包装数量
                                                    m.InboundPackageModel,///入库包装型号
                                                    RequirePackageQty = Math.Ceiling((b.Zqty * 1.0) / m.InboundPackage.GetValueOrDefault()),///需求包装数
                                                    b.Zqty///需求物料数量
                                                }).ToList();
                #endregion

                ///生成单据时为了保障效率需要拼接整个insert语句后提交至数据库执行
                StringBuilder sql = new StringBuilder();
                ///检验模式
                if (inspectionModeDistinguishOrderFlag.ToLower() == "true")
                {
                    ///获取所有涉及的检验模式
                    List<PartInspectionModeInfo> partInspectionModeInfos
                        = new PartInspectionModeBLL().GetList("" +
                        "[PART_NO] in ('" + string.Join("','", materialRequirementInfos.Select(d => d.PartNo).ToArray()) + "') and " +
                        "[SUPPLIER_NUM] in ('" + string.Join("','", materialRequirementInfos.Select(d => d.SupplierNum).ToArray()) + "')", string.Empty);
                    ///给物料需求的集合赋予检验模式
                    var partInspectionmaterialRequirementInfos = (from m in materialRequirementInfos
                                                                  join p in partInspectionModeInfos
                                                                  on new { m.PartNo, m.SupplierNum }
                                                                  equals new { p.PartNo, p.SupplierNum } into g
                                                                  select new
                                                                  {
                                                                      m.PartNo,///物料号
                                                                      m.SupplierNum,///供应商代码
                                                                      m.Plant,///工厂代码
                                                                      m.PartCname,///物料中文描述
                                                                      m.PartEname,///物料英文描述
                                                                      m.PartUnits,///计量单位
                                                                      m.InboundPackage,///入库单包装数量
                                                                      m.InboundPackageModel,///入库包装型号
                                                                      m.RequirePackageQty,///需求包装数
                                                                      m.Zqty,///需求物料数量
                                                                      Inspection = g.Select(d => d.InspectionMode.GetValueOrDefault()).FirstOrDefault()///物料检验
                                                                  }).ToList();
                    #region 免检     
                    ///免检  
                    var exemptionInspectionMaterialRequirementInfos = partInspectionmaterialRequirementInfos.Where(d => d.Inspection == (int)InspectionModeConstants.ExemptionInspection).ToList();
                    ///生成计划拉动单同时记录该生产订单该计划零件类已生成单据
                    ///当有物料需求时，才会生成计划拉动单
                    if (exemptionInspectionMaterialRequirementInfos.Count > 0)
                    {
                        ///实例化主表Guid
                        Guid planPullOrderFid = Guid.NewGuid();
                        ///主表计划拉动单号
                        string orderCode = new SeqDefineBLL().GetCurrentCode("PLAN_PULL_ORDER_CODE", planPartBoxInfo.PartBoxCode.Substring(0, 1).ToUpper());///TODO:计划拉动单编号规则可能还会更改，待客户确认

                        MaterialPullingOrderInfo materialPullingOrderInfo = CreateMaterialPullingOrderInfo(planPartBoxInfo, orderCode, currentWindowTime, true);
                        materialPullingOrderInfo.MaterialPullingOrderDetailInfos = (from m in exemptionInspectionMaterialRequirementInfos
                                                                                    select new MaterialPullingOrderDetailInfo
                                                                                    {
                                                                                        OrderNo = orderCode,///拉动单号1
                                                                                        SupplierNum = m.SupplierNum,///供应商2
                                                                                        PartNo = m.PartNo,///物料号3
                                                                                        PartCname = m.PartCname,///物料号中文名称4
                                                                                        PartEname = m.PartEname,///物料号英文名称5
                                                                                        Uom = m.PartUnits,///计量单位6 
                                                                                        PackageQty = m.InboundPackage.GetValueOrDefault(),///入库单包装数量7
                                                                                        PackageModel = m.InboundPackageModel,///入库包装编号8
                                                                                        RequirePackageQty = Convert.ToInt32(m.RequirePackageQty),///需求包装数量9
                                                                                        RequirePartQty = m.Zqty,///需求物料数量10
                                                                                        TargetZoneNo = planPartBoxInfo.TargetZoneNo,
                                                                                        InspectMode = m.Inspection///校验模式
                                                                                    }).ToList();

                        sql.AppendLine(CreatePlanPullOrderSql(planPullOrderFid, materialPullingOrderInfo, windowTimes, timeZone, (int)InspectionFlagConstants.Exemption));
                        sql.AppendLine(CreatePlanPullOrderDetailSql(planPullOrderFid, materialPullingOrderInfo.MaterialPullingOrderDetailInfos));
                        sql.AppendFormat(MaterialPullingCommonBLL.Handler(materialPullingOrderInfo, loginUser));
                    }
                    #endregion

                    #region 不免检
                    ///不免检
                    var inspectionmaterialRequirementInfos = partInspectionmaterialRequirementInfos.Where(d => d.Inspection != (int)InspectionModeConstants.ExemptionInspection).ToList();
                    ///生成计划拉动单同时记录该生产订单该计划零件类已生成单据
                    ///当有物料需求时，才会生成计划拉动单
                    if (inspectionmaterialRequirementInfos.Count > 0)
                    {
                        ///实例化主表Guid
                        Guid planPullOrderFid = Guid.NewGuid();
                        ///主表计划拉动单号
                        string orderCode = new SeqDefineBLL().GetCurrentCode("PLAN_PULL_ORDER_CODE", planPartBoxInfo.PartBoxCode.Substring(0, 1).ToUpper());///TODO:计划拉动单编号规则可能还会更改，待客户确认

                        MaterialPullingOrderInfo materialPullingOrderInfo = CreateMaterialPullingOrderInfo(planPartBoxInfo, orderCode, currentWindowTime, false);
                        materialPullingOrderInfo.MaterialPullingOrderDetailInfos = (from m in inspectionmaterialRequirementInfos
                                                                                    select new MaterialPullingOrderDetailInfo
                                                                                    {
                                                                                        OrderNo = orderCode,///拉动单号1
                                                                                        SupplierNum = m.SupplierNum,///供应商2
                                                                                        PartNo = m.PartNo,///物料号3
                                                                                        PartCname = m.PartCname,///物料号中文名称4
                                                                                        PartEname = m.PartEname,///物料号英文名称5
                                                                                        Uom = m.PartUnits,///计量单位6 
                                                                                        PackageQty = m.InboundPackage.GetValueOrDefault(),///入库单包装数量7
                                                                                        PackageModel = m.InboundPackageModel,///入库包装编号8
                                                                                        RequirePackageQty = Convert.ToInt32(m.RequirePackageQty),///需求包装数量9
                                                                                        RequirePartQty = m.Zqty,///需求物料数量10
                                                                                        TargetZoneNo = planPartBoxInfo.TargetZoneNo,
                                                                                        InspectMode = m.Inspection///校验模式
                                                                                    }).ToList();
                        ///
                        sql.AppendLine(CreatePlanPullOrderSql(planPullOrderFid, materialPullingOrderInfo, windowTimes, timeZone, (int)InspectionFlagConstants.Inspect));
                        sql.AppendLine(CreatePlanPullOrderDetailSql(planPullOrderFid, materialPullingOrderInfo.MaterialPullingOrderDetailInfos));
                        sql.AppendFormat(MaterialPullingCommonBLL.Handler(materialPullingOrderInfo, loginUser));
                    }
                    #endregion
                }
                else
                {
                    ///生成计划拉动单同时记录该生产订单该计划零件类已生成单据
                    ///当有物料需求时，才会生成计划拉动单
                    if (materialRequirementInfos.Count > 0)
                    {
                        ///实例化主表Guid
                        Guid planPullOrderFid = Guid.NewGuid();
                        ///主表计划拉动单号
                        string orderCode = new SeqDefineBLL().GetCurrentCode("PLAN_PULL_ORDER_CODE", planPartBoxInfo.PartBoxCode.Substring(0, 1).ToUpper());///TODO:计划拉动单编号规则可能还会更改，待客户确认

                        MaterialPullingOrderInfo materialPullingOrderInfo = CreateMaterialPullingOrderInfo(planPartBoxInfo, orderCode, currentWindowTime, false);
                        materialPullingOrderInfo.MaterialPullingOrderDetailInfos = (from m in materialRequirementInfos
                                                                                    select new MaterialPullingOrderDetailInfo
                                                                                    {
                                                                                        OrderNo = orderCode,///拉动单号1
                                                                                        SupplierNum = m.SupplierNum,///供应商2
                                                                                        PartNo = m.PartNo,///物料号3
                                                                                        PartCname = m.PartCname,///物料号中文名称4
                                                                                        PartEname = m.PartEname,///物料号英文名称5
                                                                                        Uom = m.PartUnits,///计量单位6 
                                                                                        PackageQty = m.InboundPackage.GetValueOrDefault(),///入库单包装数量7
                                                                                        PackageModel = m.InboundPackageModel,///入库包装编号8
                                                                                        RequirePackageQty = Convert.ToInt32(m.RequirePackageQty),///需求包装数量9
                                                                                        RequirePartQty = m.Zqty,///需求物料数量10
                                                                                        TargetZoneNo = planPartBoxInfo.TargetZoneNo
                                                                                    }).ToList();
                        ///生成计划拉动单
                        sql.AppendLine(CreatePlanPullOrderSql(planPullOrderFid, materialPullingOrderInfo, windowTimes, timeZone, null));
                        ///生成计划拉动单明细
                        sql.AppendLine(CreatePlanPullOrderDetailSql(planPullOrderFid, materialPullingOrderInfo.MaterialPullingOrderDetailInfos));
                        ///单据衔接
                        sql.AppendLine(MaterialPullingCommonBLL.Handler(materialPullingOrderInfo, loginUser));
                    }
                }
                ///订单BOM存在时才需要更新
                if (pullOrderBomInfos.Count > 0)
                {
                    ///更新中间表处理状态 = 已处理
                    sql.AppendFormat("update [LES].[TT_MPM_PLAN_PULL_CREATE_STATUS] set " +
                        "[STATUS] = " + (int)ProcessFlagConstants.Processed + "," +
                        "[MODIFY_USER] = N'" + loginUser + "'," +
                        "[MODIFY_DATE] = GETDATE() where " +
                        "[VALID_FLAG] = 1 and " +
                        "[PART_BOX_FID] = N'{0}' and " +
                        "[ORDER_FID] in ('{1}');",
                        planPartBoxInfo.Fid.GetValueOrDefault(),
                        string.Join("','", pullOrderBomInfos.Select(d => d.Orderfid.GetValueOrDefault()).ToArray()));
                }

                #region 执行
                if (sql.Length > 0)
                {
                    ///更改窗口时间发单状态
                    if (planPullWindowTimeEnable.ToLower() == "true")
                    {
                        sql.AppendFormat("update [LES].[TT_MPM_PLAN_WINDOW_TIME] set " +
                            "[SEND_TIME_STATUS] = " + (int)SendTimeStatusConstants.Sent + "," +
                            "[MODIFY_USER] = N'" + loginUser + "'," +
                            "[MODIFY_DATE] = GETDATE() where [ID] in (" + string.Join(",", planTimeIds.ToArray()) + ");");
                    }
                    using (TransactionScope trans = new TransactionScope())
                    {
                        if (!BLL.LES.CommonBLL.ExecuteNonQueryBySql(sql.ToString()))
                            throw new Exception("0x00000736");///写入数据库失败
                        trans.Complete();
                    }
                }
                #endregion
            }
            #endregion
        }
        /// <summary>
        /// CreatePlanPullOrderSql
        /// </summary>
        /// <param name="planPullOrderFid"></param>
        /// <param name="materialPullingOrderInfo"></param>
        /// <param name="windowTimes"></param>
        /// <param name="timeZone"></param>
        /// <param name="inspectionFlag"></param>
        /// <returns></returns>
        private string CreatePlanPullOrderSql(Guid planPullOrderFid, MaterialPullingOrderInfo materialPullingOrderInfo, string windowTimes, string timeZone, int? inspectionFlag)
        {
            PlanPullOrderInfo planPullOrderInfo = new PlanPullOrderInfo();
            planPullOrderInfo.Fid = planPullOrderFid;
            planPullOrderInfo.OrderCode = materialPullingOrderInfo.OrderNo;
            planPullOrderInfo.PartBoxCode = materialPullingOrderInfo.PartBoxCode;
            planPullOrderInfo.PartBoxName = materialPullingOrderInfo.PartBoxName;
            planPullOrderInfo.Plant = materialPullingOrderInfo.Plant;
            planPullOrderInfo.Workshop = materialPullingOrderInfo.Workshop;
            planPullOrderInfo.AssemblyLine = materialPullingOrderInfo.AssemblyLine;
            planPullOrderInfo.SupplierNum = materialPullingOrderInfo.SupplierNum;
            planPullOrderInfo.SourceWmNo = materialPullingOrderInfo.SourceWmNo;
            planPullOrderInfo.SourceZoneNo = materialPullingOrderInfo.SourceZoneNo;
            planPullOrderInfo.TargetWmNo = materialPullingOrderInfo.TargetWmNo;
            planPullOrderInfo.TargetZoneNo = materialPullingOrderInfo.TargetZoneNo;
            planPullOrderInfo.PublishTime = materialPullingOrderInfo.PublishTime;
            planPullOrderInfo.OrderType = (int)PullOrderTypeConstants.Pulling;
            planPullOrderInfo.Dock = materialPullingOrderInfo.TargetDock;
            planPullOrderInfo.ExpectedArrivalTime = materialPullingOrderInfo.PlanShippingTime;
            planPullOrderInfo.SuggestDeliveryTime = materialPullingOrderInfo.PlanDeliveryTime;
            planPullOrderInfo.WindowTimes = windowTimes;
            planPullOrderInfo.OrderStatus = (int)PullOrderStatusConstants.Released;
            planPullOrderInfo.TimeZone = timeZone;
            planPullOrderInfo.DeliveryAdd = string.Empty;
            planPullOrderInfo.InspectionFlag = inspectionFlag.GetValueOrDefault() == 1 ? true : false;
            planPullOrderInfo.CreateUser = loginUser;
            return PlanPullOrderDAL.GetInsertSql(planPullOrderInfo);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="planPullOrderFid"></param>
        /// <param name="materialPullingOrderDetailInfos"></param>
        /// <returns></returns>
        private string CreatePlanPullOrderDetailSql(Guid planPullOrderFid, List<MaterialPullingOrderDetailInfo> materialPullingOrderDetailInfos)
        {
            string sql = string.Empty;
            foreach (MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo in materialPullingOrderDetailInfos)
            {
                PlanPullOrderDetailInfo planPullOrderDetailInfo = new PlanPullOrderDetailInfo();
                planPullOrderDetailInfo.OrderFid = planPullOrderFid;
                planPullOrderDetailInfo.OrderStatus = (int)PullOrderStatusConstants.Released;
                planPullOrderDetailInfo.OrderCode = materialPullingOrderDetailInfo.OrderNo;
                planPullOrderDetailInfo.SupplierNum = materialPullingOrderDetailInfo.SupplierNum;
                planPullOrderDetailInfo.PartNo = materialPullingOrderDetailInfo.PartNo;
                planPullOrderDetailInfo.PartCname = materialPullingOrderDetailInfo.PartCname;
                planPullOrderDetailInfo.PartEname = materialPullingOrderDetailInfo.PartEname;
                planPullOrderDetailInfo.MeasuringUnitNo = materialPullingOrderDetailInfo.Uom;
                planPullOrderDetailInfo.InboundPackageModel = materialPullingOrderDetailInfo.PackageModel;
                planPullOrderDetailInfo.InboundPackageQty = materialPullingOrderDetailInfo.PackageQty;
                planPullOrderDetailInfo.RequiredPackageQty = materialPullingOrderDetailInfo.RequirePackageQty;
                planPullOrderDetailInfo.RequiredPartQty = materialPullingOrderDetailInfo.RequirePartQty;
                planPullOrderDetailInfo.CreateUser = loginUser;
                sql += PlanPullOrderDetailDAL.GetInsertSql(planPullOrderDetailInfo);
            }
            return sql;
        }
        /// <summary>
        /// CreateMaterialPullingOrderInfo
        /// </summary>
        /// <param name="planPartBoxInfo"></param>
        /// <param name="orderCode"></param>
        /// <param name="currentWindowTime"></param>
        /// <param name="inspectFlag"></param>
        /// <returns></returns>
        private MaterialPullingOrderInfo CreateMaterialPullingOrderInfo(PlanPartBoxInfo planPartBoxInfo, string orderCode, DateTime currentWindowTime, bool inspectFlag)
        {
            MaterialPullingOrderInfo materialPullingOrderInfo = new MaterialPullingOrderInfo();
            materialPullingOrderInfo.OrderNo = orderCode;
            materialPullingOrderInfo.PartBoxCode = planPartBoxInfo.PartBoxCode;///零件类2
            materialPullingOrderInfo.PartBoxName = planPartBoxInfo.PartBoxName; ///零件类名称3
            materialPullingOrderInfo.Plant = planPartBoxInfo.Plant;///工厂4
            materialPullingOrderInfo.Workshop = planPartBoxInfo.Workshop;///车间5
            materialPullingOrderInfo.AssemblyLine = planPartBoxInfo.AssemblyLine;///流水线6
            materialPullingOrderInfo.SupplierNum = planPartBoxInfo.SupplierNum; ///供应商7
            materialPullingOrderInfo.SupplierName = planPartBoxInfo.SupplierName;
            materialPullingOrderInfo.SourceZoneNo = planPartBoxInfo.SourceZoneNo;///来源存储区8
            materialPullingOrderInfo.SourceWmNo = planPartBoxInfo.SourceWmNo;///来源仓库9
            materialPullingOrderInfo.TargetZoneNo = planPartBoxInfo.TargetZoneNo;///目标存储区10
            materialPullingOrderInfo.TargetWmNo = planPartBoxInfo.TargetWmNo;///目标仓库11
            materialPullingOrderInfo.TargetDock = planPartBoxInfo.Dock;///道口12
            materialPullingOrderInfo.PlanDeliveryTime = currentWindowTime;///预计到厂时间14 = 窗口时间
            materialPullingOrderInfo.PlanShippingTime = currentWindowTime.AddMinutes((-1) * planPartBoxInfo.DeliveryTime.GetValueOrDefault());
            materialPullingOrderInfo.PublishTime = DateTime.Now;
            materialPullingOrderInfo.OrderType = (int)PullOrderTypeConstants.Pulling;
            materialPullingOrderInfo.InspectFlag = inspectFlag;
            materialPullingOrderInfo.PullMode = (int)PullModeConstants.Plan;
            return materialPullingOrderInfo;
        }

    }
}