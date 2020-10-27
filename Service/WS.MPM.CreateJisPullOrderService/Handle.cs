using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.SYS;
using DM.LES;
using DM.SYS;
using BLL.LES;
using DAL.LES;
using System.Transactions;

namespace WS.MPM.CreateJisPullOrderService
{
    public class Handle
    {
        /// <summary>
        /// 登录用户
        /// </summary>
        string loginUser = "CreateJisPullOrderService";

        /// <summary>
        /// Handler
        /// </summary>
        public void Handler()
        {
            ///获取排序零件类中状态㉖为已启用的数据
            List<JisPartBoxInfo> jisPartBoxInfos = new JisPartBoxBLL().GetList(" and [STATUS] =" + (int)BasicDataStatusConstants.Enable + "", string.Empty);
            if (jisPartBoxInfos.Count == 0) return;
            ///获取零件类对应的计数器
            List<JisCounterInfo> jisCounterInfos = new JisCounterBLL().GetList("and [PART_BOX_FID] in ('" + string.Join("','", jisPartBoxInfos.Select(d => d.Fid).ToArray()) + "')", string.Empty);
            if (jisCounterInfos.Count == 0) return;
            ///对应的排序计数器日志
            List<JisCounterLogInfo> jisCounterLogInfos = new JisCounterLogBLL().GetList("and [COUNTER_FID] in ('" + string.Join("','", jisCounterInfos.Select(d => d.Fid).ToArray()) + "')", string.Empty);
            if (jisCounterLogInfos.Count == 0) return;
            ///计数器日志对应的生产订单
            List<PullOrdersInfo> pullOrdersInfos = new PullOrdersBLL().GetList("and [ORDER_NO] in ('" + string.Join("','", jisCounterLogInfos.Select(d => d.SourceData).ToArray()) + "')", string.Empty);
            if (pullOrdersInfos.Count == 0) return;
            ///对应的物料拉动信息
            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandardInfos = new MaintainInhouseLogisticStandardBLL().GetList("and [STATUS] =" + (int)BasicDataStatusConstants.Enable + ""
                  + " and [INHOUSE_PART_CLASS] in ('" + string.Join("','", jisPartBoxInfos.Select(d => d.PartBoxCode).ToArray()) + "')"
                  + " and [PART_NO] in ('" + string.Join("','", jisCounterLogInfos.Select(d => d.PartNo).ToArray()) + "')", string.Empty);
            if (maintainInhouseLogisticStandardInfos.Count == 0) return;
            foreach (JisPartBoxInfo jisPartBoxInfo in jisPartBoxInfos)
            {
                ///零件类对应的物料拉动信息
                List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandards = maintainInhouseLogisticStandardInfos.Where(d => d.InhousePartClass == jisPartBoxInfo.PartBoxCode).ToList();
                ///计数器
                List<JisCounterInfo> jisCounters = new List<JisCounterInfo>();
                ///获取状态为20.累计完成的排序计数器
                jisCounters = jisCounterInfos.Where(d => d.Status == (int)JisCounterStatusConstants.AccumulativeCompletion && d.PartBoxFid == jisPartBoxInfo.Fid).ToList();
                ///若此时未能成功获取到状态为20.累计完成的排序计数器时，需要获取状态为10.正在累计的排序计数器，判断其创建时间加上最大累积时间是否已超过当前时间
                if (jisCounters.Count == 0)
                    jisCounters = jisCounterInfos.Where(d => d.Status == (int)JisCounterStatusConstants.Accumulating && d.PartBoxFid == jisPartBoxInfo.Fid && d.CreateDate.AddMinutes(jisPartBoxInfo.MaxAccumulativeTime.GetValueOrDefault()) > DateTime.Now).ToList();
                if (jisCounters.Count == 0) continue;
                ///若已超过则需要首先标记排序计数器状态为20.累计完成，之后的逻辑按部就班走
                BLL.LES.CommonBLL.ExecuteNonQueryBySql(@"update[LES].[TT_MPM_JIS_COUNTER] set [STATUS]=" + (int)JisCounterStatusConstants.AccumulativeCompletion + ",[MODIFY_DATE] = getdate(),[MODIFY_USER] = '" + loginUser + "' where [ID] in (" + string.Join(",", jisCounters.Select(d => d.Id).ToArray()) + ");");
                ///根据计数器生成拉动单
                foreach (JisCounterInfo jisCounterInfo in jisCounters)
                {
                    ///sql
                    StringBuilder stringBuilder = new StringBuilder();
                    ///对应的排序计数器日志
                    List<JisCounterLogInfo> jisCounterLogs = jisCounterLogInfos.Where(d => d.CounterFid == jisCounterInfo.Fid).ToList();
                    if (jisCounterLogs.Count == 0) continue;
                    ///计数器日志物料对应的物料拉动信息
                    List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogistics = (from maintainInhouseLogisticStandard in maintainInhouseLogisticStandards
                                                                                          join jisCounterLog in jisCounterLogs
                                                                                          on maintainInhouseLogisticStandard.PartNo equals jisCounterLog.PartNo
                                                                                          select maintainInhouseLogisticStandard).ToList();
                    if (maintainInhouseLogistics.Count == 0) continue;
                    switch (jisPartBoxInfo.JisPullMode.GetValueOrDefault())
                    {
                        ///排序拉动
                        case (int)JisPullModeConstants.JisPull:
                            stringBuilder.AppendFormat(JisPull(jisPartBoxInfo, jisCounterInfo, jisCounterLogs, maintainInhouseLogistics, pullOrdersInfos, true));
                            break;
                        ///物料成套拉动
                        case (int)JisPullModeConstants.CompletePull:
                            stringBuilder.AppendFormat(JisPull(jisPartBoxInfo, jisCounterInfo, jisCounterLogs, maintainInhouseLogistics, pullOrdersInfos, false));
                            break;
                        default: continue;
                    }
                    ///数据库执行语句
                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.AppendFormat(@"update[LES].[TT_MPM_JIS_COUNTER] set [STATUS]=" + (int)JisCounterStatusConstants.GeneratedDocuments + ",[MODIFY_DATE] = getdate(),[MODIFY_USER] = '" + loginUser + "' where [ID] = " + jisCounterInfo.Id + "; ");
                        using (TransactionScope trans = new TransactionScope())
                        {
                            BLL.LES.CommonBLL.ExecuteNonQueryBySql(stringBuilder.ToString());
                            trans.Complete();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Jis拉动
        /// </summary>
        /// <param name="jisPartBoxInfo"></param>
        /// <param name="jisCounterInfo"></param>
        /// <param name="jisCounterLogs"></param>
        /// <param name="maintainInhouseLogistics"></param>
        /// <param name="JisPullGuid"></param>
        /// <param name="orderCode"></param>
        /// <param name="pullOrdersInfos"></param>
        /// <param name="isJis"></param>
        /// <returns></returns>
        public string JisPull(JisPartBoxInfo jisPartBoxInfo, JisCounterInfo jisCounterInfo, List<JisCounterLogInfo> jisCounterLogs, List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogistics,List<PullOrdersInfo> pullOrdersInfos,bool isJis)
        {
            StringBuilder stringBuilder = new StringBuilder();
            ///拉动单Guid
            Guid JisPullGuid = Guid.NewGuid();
            ///拉动单号
            string orderCode = new SeqDefineBLL().GetCurrentCode("JIS_PULL_ORDER_CODE");
            ///单据衔接
            MaterialPullingOrderInfo materialPullingOrderInfo = MaterialPullingCommonBLL.CreateMaterialPullingOrderInfo();
            ///jisPartBoxInfo -> MaterialPullingOrderInfo
            MaterialPullingCommonBLL.GetJisMaterialPullingOrderInfo(jisPartBoxInfo, ref materialPullingOrderInfo);
            ///orderCode -> MaterialPullingOrderInfo
            MaterialPullingCommonBLL.GetorderCodeMaterialPullingOrderInfo(orderCode, ref materialPullingOrderInfo);
            foreach (JisCounterLogInfo jisCounterLog in jisCounterLogs)
            {
                ///物料拉动信息
                MaintainInhouseLogisticStandardInfo maintainInhouseLogisticStandardInfo = maintainInhouseLogistics.Where(d => d.PartNo == jisCounterLog.PartNo).FirstOrDefault();
                if (maintainInhouseLogisticStandardInfo == null) continue;
                ///生产订单信息
                PullOrdersInfo pullOrdersInfo = pullOrdersInfos.FirstOrDefault(d=>d.OrderNo== jisCounterLog.SourceData);
                if(pullOrdersInfo==null) continue;
                ///仓储衔接明细表
                MaterialPullingOrderDetailInfo detailInfo = MaterialPullingCommonBLL.CreateMaterialPullingOrderDetailInfo();
                ///maintainInhouseLogisticStandardInfo -> MaterialPullingOrderDetailInfo
                MaterialPullingCommonBLL.GetMainMaterialPullingOrderDetailInfo(maintainInhouseLogisticStandardInfo, jisCounterLog.PartQty.GetValueOrDefault(), ref detailInfo);
                ///jisPartBoxInfo-> MaterialPullingOrderDetailInfo
                MaterialPullingCommonBLL.GetJisMaterialPullingOrderDetailInfo(jisPartBoxInfo, ref detailInfo);
                ///orderCode-> MaterialPullingOrderDetailInfo
                MaterialPullingCommonBLL.GetOrderCodeMaterialPullingOrderDetailInfo(orderCode, ref detailInfo);
                ///pullOrdersInfo-> MaterialPullingOrderDetailInfo
                MaterialPullingCommonBLL.GetPullOrderMaterialPullingOrderDetailInfo(pullOrdersInfo, ref detailInfo);
                ///Add
                materialPullingOrderInfo.MaterialPullingOrderDetailInfos.Add(detailInfo);
                if(isJis)
                {
                    ///排序拉动明细
                    stringBuilder.AppendFormat(CreateJisPullOrderDetailSql(JisPullGuid, materialPullingOrderInfo.MaterialPullingOrderDetailInfos));
                }
                else
                {
                    ///物料成套拉动明细
                    stringBuilder.AppendFormat(CreateCompletePullOrderDetailSql(JisPullGuid, materialPullingOrderInfo.MaterialPullingOrderDetailInfos));
                }
                if(stringBuilder.Length==0) continue;
            }
            if (isJis)
            {
                ///开始车号
                materialPullingOrderInfo.StartVehicheNo = materialPullingOrderInfo.MaterialPullingOrderDetailInfos.Min(d => d.DayVehicheSeqNo.GetValueOrDefault());
                ///结束车号
                materialPullingOrderInfo.EndVehicheNo = materialPullingOrderInfo.MaterialPullingOrderDetailInfos.Max(d => d.DayVehicheSeqNo.GetValueOrDefault());
                ///排序拉动主表
                stringBuilder.AppendFormat(CreateJisPullOrderSql(JisPullGuid, materialPullingOrderInfo));
            }
            else
            {
                ///车辆编号
                materialPullingOrderInfo.VehicheModelNo = materialPullingOrderInfo.MaterialPullingOrderDetailInfos.Select(d => d.VehicheModelNo).FirstOrDefault();
                ///生产订单号
                materialPullingOrderInfo.OrderNo = materialPullingOrderInfo.MaterialPullingOrderDetailInfos.Select(d => d.OrderNo).FirstOrDefault();
                ///物料成套拉动主表
                stringBuilder.AppendFormat(CreateCompletePullOrderSql(JisPullGuid, materialPullingOrderInfo));
            }
            ///仓储衔接
            stringBuilder.AppendFormat(MaterialPullingCommonBLL.Handler(materialPullingOrderInfo, loginUser));
            return stringBuilder.ToString();
        }
        /// <summary>
        /// 排序拉动明细
        /// </summary>
        /// <param name="JisPullGuid"></param>
        /// <param name="materialPullingOrderDetailInfos"></param>
        /// <returns></returns>
        private string CreateJisPullOrderDetailSql(Guid JisPullGuid, List<MaterialPullingOrderDetailInfo> materialPullingOrderDetailInfos)
        {
            string sql = string.Empty;
            int rowNo = 0;
            foreach (MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo in materialPullingOrderDetailInfos)
            {
                rowNo += 1;
                   JisPullOrderDetailInfo jisPullOrderDetailInfo  = new JisPullOrderDetailInfo();
                ///ORDER_FID
                jisPullOrderDetailInfo.OrderFid = JisPullGuid;
                ///ORDER_CODE
                jisPullOrderDetailInfo.OrderCode = materialPullingOrderDetailInfo.OrderNo;
                ///ROW_NO
                jisPullOrderDetailInfo.RowNo = rowNo;
                ///SUPPLIER_NUM
                jisPullOrderDetailInfo.SupplierNum = materialPullingOrderDetailInfo.SupplierNum;
                ///PART_NO
                jisPullOrderDetailInfo.PartNo = materialPullingOrderDetailInfo.PartNo;
                ///PART_CNAME
                jisPullOrderDetailInfo.PartCname = materialPullingOrderDetailInfo.PartCname;
                ///PART_VERSION TODO: 物料版本
                jisPullOrderDetailInfo.PartVersion = null;
                ///PART_ENAME
                jisPullOrderDetailInfo.PartEname = materialPullingOrderDetailInfo.PartEname;
                ///MEASURING_UNIT_NO
                jisPullOrderDetailInfo.MeasuringUnitNo = materialPullingOrderDetailInfo.Uom;
                ///VEHICHE_MODEL_NO  车型编号
                jisPullOrderDetailInfo.VehicheModelNo = materialPullingOrderDetailInfo.VehicheModelNo;
                ///DAY_VEHICHE_SEQ_NO 当日车辆序号
                jisPullOrderDetailInfo.DayVehicheSeqNo = materialPullingOrderDetailInfo.DayVehicheSeqNo.GetValueOrDefault(); 
                ///PRODUCTION_NO 生产订单号
                jisPullOrderDetailInfo.ProductionNo = materialPullingOrderDetailInfo.ProduceNo;
                ///INSPECTION_MODE 检验模式
                jisPullOrderDetailInfo.InspectionMode = null;
                ///INSPECTION_FLAG 是否检验
                jisPullOrderDetailInfo.InspectionFlag = null;
                ///REQUIRED_PART_QTY
                jisPullOrderDetailInfo.RequiredPartQty = materialPullingOrderDetailInfo.RequirePartQty;
                ///CREATE_USER
                jisPullOrderDetailInfo.CreateUser = loginUser;
                sql +=JisPullOrderDetailDAL.GetInsertSql(jisPullOrderDetailInfo);
            }
            return sql;
        }
        /// <summary>
        /// 排序拉动单主表
        /// </summary>
        /// <param name="JisPullGuid"></param>
        /// <param name="materialPullingOrderInfo"></param>
        /// <returns></returns>
        private string CreateJisPullOrderSql(Guid JisPullGuid, MaterialPullingOrderInfo materialPullingOrderInfo)
        {
            JisPullOrderInfo jisPullOrderInfo = new JisPullOrderInfo();
            jisPullOrderInfo.Fid = JisPullGuid;
            ///ORDER_CODE
            jisPullOrderInfo.OrderCode = materialPullingOrderInfo.OrderNo;
            ///PART_BOX_CODE
            jisPullOrderInfo.PartBoxCode = materialPullingOrderInfo.PartBoxCode;
            ///PART_BOX_NAME
            jisPullOrderInfo.PartBoxName = materialPullingOrderInfo.PartBoxName;
            ///PLANT
            jisPullOrderInfo.Plant = materialPullingOrderInfo.Plant;
            ///WORKSHOP
            jisPullOrderInfo.Workshop = materialPullingOrderInfo.Workshop;
            ///ASSEMBLY_LINE
            jisPullOrderInfo.AssemblyLine = materialPullingOrderInfo.AssemblyLine;
            ///WORKSHOP_SECTION
            jisPullOrderInfo.WorkshopSection = materialPullingOrderInfo.WorkshopSection;
            ///LOCATION
            jisPullOrderInfo.Location = materialPullingOrderInfo.Location;
            ///SUPPLIER_NUM
            jisPullOrderInfo.SupplierNum = materialPullingOrderInfo.SupplierNum;
            ///S_ZONE_NO
            jisPullOrderInfo.SZoneNo = materialPullingOrderInfo.SourceZoneNo;
            ///S_WM_NO
            jisPullOrderInfo.SWmNo = materialPullingOrderInfo.SourceWmNo;
            ///T_WM_NO
            jisPullOrderInfo.TWmNo = materialPullingOrderInfo.TargetWmNo;
            ///T_ZONE_NO
            jisPullOrderInfo.TZoneNo = materialPullingOrderInfo.TargetZoneNo;
            ///PUBLISH_TIME
            jisPullOrderInfo.PublishTime = materialPullingOrderInfo.PublishTime;
            ///ORDER_TYPE
            jisPullOrderInfo.OrderType = (int)PullOrderTypeConstants.Pulling;
            ///DOCK
            jisPullOrderInfo.Dock = materialPullingOrderInfo.TargetDock;
            ///PLAN_SHIPPING_TIME
            jisPullOrderInfo.PlanShippingTime = materialPullingOrderInfo.PlanShippingTime;
            ///PLAN_DELIVERY_TIME
            jisPullOrderInfo.PlanDeliveryTime = materialPullingOrderInfo.PlanDeliveryTime;
            ///START_VEHICHE_NO  开始车号
            jisPullOrderInfo.StartVehicheNo = materialPullingOrderInfo.StartVehicheNo.GetValueOrDefault().ToString();
            ///END_VEHICHE_NO 结束车号
            jisPullOrderInfo.EndVehicheNo = materialPullingOrderInfo.EndVehicheNo.GetValueOrDefault().ToString();
            ///DAY_SEQ_NO 单据当日顺序号
            jisPullOrderInfo.DaySeqNo =new JisPullOrderBLL().GetDaySeqNo();
            ///KEEPER 保管员
            jisPullOrderInfo.Keeper = null;
            ///PACKAGE_MODEL 包装编号
            jisPullOrderInfo.PackageModel = materialPullingOrderInfo.PackageModel ;
            ///PACKAGE 单包装数量
            jisPullOrderInfo.Package = materialPullingOrderInfo.Package.GetValueOrDefault();
            ///CREATE_USER
            jisPullOrderInfo.CreateUser = loginUser;
            return JisPullOrderDAL.GetInsertSql(jisPullOrderInfo);
        }
        /// <summary>
        /// 物料成套拉动明细
        /// </summary>
        /// <param name="JisPullGuid"></param>
        /// <param name="materialPullingOrderDetailInfos"></param>
        /// <returns></returns>
        private string CreateCompletePullOrderDetailSql(Guid JisPullGuid, List<MaterialPullingOrderDetailInfo> materialPullingOrderDetailInfos)
        {
            string sql = string.Empty;
            int rowNo = 0;
            foreach (MaterialPullingOrderDetailInfo materialPullingOrderDetailInfo in materialPullingOrderDetailInfos)
            {
                rowNo += 1;
                SpsPullOrderDetailInfo spsPullOrderDetailInfo = new SpsPullOrderDetailInfo();
                ///ORDER_FID
                spsPullOrderDetailInfo.OrderFid = JisPullGuid;
                ///ORDER_CODE
                spsPullOrderDetailInfo.OrderCode = materialPullingOrderDetailInfo.OrderNo;
                ///ROW_NO
                spsPullOrderDetailInfo.RowNo = rowNo;
                ///SUPPLIER_NUM
                spsPullOrderDetailInfo.SupplierNum = materialPullingOrderDetailInfo.SupplierNum;
                ///PART_NO
                spsPullOrderDetailInfo.PartNo = materialPullingOrderDetailInfo.PartNo;
                ///PART_CNAME
                spsPullOrderDetailInfo.PartCname = materialPullingOrderDetailInfo.PartCname;
                ///PART_VERSION
                spsPullOrderDetailInfo.PartVersion = null;///TODO:暂时没有物料版本
                ///PART_ENAME
                spsPullOrderDetailInfo.PartEname = materialPullingOrderDetailInfo.PartEname;
                ///MEASURING_UNIT_NO
                spsPullOrderDetailInfo.MeasuringUnitNo = materialPullingOrderDetailInfo.Uom;
                ///LIGHT_ADDRESS
                spsPullOrderDetailInfo.LightAddress = null;///TODO:暂时没有维护灯地址
                ///WORKSHOP_SECTION
                spsPullOrderDetailInfo.WorkshopSection = materialPullingOrderDetailInfo.WorkshopSection;
                ///LOCATION
                spsPullOrderDetailInfo.Location = materialPullingOrderDetailInfo.Location;
                ///REQUIRED_PART_QTY
                spsPullOrderDetailInfo.RequiredPartQty = materialPullingOrderDetailInfo.RequirePartQty;
                ///CREATE_USER
                spsPullOrderDetailInfo.CreateUser = loginUser;
                sql += SpsPullOrderDetailDAL.GetInsertSql(spsPullOrderDetailInfo);
            }
            return sql;
        }
        /// <summary>
        /// 物料成套拉动单主表
        /// </summary>
        /// <param name="JisPullGuid"></param>
        /// <param name="materialPullingOrderInfo"></param>
        /// <returns></returns>
        private string CreateCompletePullOrderSql(Guid JisPullGuid, MaterialPullingOrderInfo materialPullingOrderInfo)
        {
            SpsPullOrderInfo spsPullOrderInfo = new SpsPullOrderInfo();
            spsPullOrderInfo.Fid = JisPullGuid;
            ///ORDER_CODE
            spsPullOrderInfo.OrderCode = materialPullingOrderInfo.OrderNo;
            ///PART_BOX_CODE
            spsPullOrderInfo.PartBoxCode = materialPullingOrderInfo.PartBoxCode;
            ///PART_BOX_NAME
            spsPullOrderInfo.PartBoxName = materialPullingOrderInfo.PartBoxName;
            ///PLANT
            spsPullOrderInfo.Plant = materialPullingOrderInfo.Plant;
            ///WORKSHOP
            spsPullOrderInfo.Workshop = materialPullingOrderInfo.Workshop;
            ///ASSEMBLY_LINE
            spsPullOrderInfo.AssemblyLine = materialPullingOrderInfo.AssemblyLine;
            ///SUPPLIER_NUM
            spsPullOrderInfo.SupplierNum = materialPullingOrderInfo.SupplierNum;
            ///S_ZONE_NO
            spsPullOrderInfo.SZoneNo = materialPullingOrderInfo.SourceZoneNo;
            ///S_WM_NO
            spsPullOrderInfo.SWmNo = materialPullingOrderInfo.SourceWmNo;
            ///T_WM_NO
            spsPullOrderInfo.TWmNo = materialPullingOrderInfo.TargetWmNo;
            ///T_ZONE_NO
            spsPullOrderInfo.TZoneNo = materialPullingOrderInfo.TargetZoneNo;
            ///PUBLISH_TIME
            spsPullOrderInfo.PublishTime = materialPullingOrderInfo.PublishTime;
            ///ORDER_TYPE
            spsPullOrderInfo.OrderType = (int)PullOrderTypeConstants.Pulling;
            ///DOCK
            spsPullOrderInfo.Dock = materialPullingOrderInfo.TargetDock;
            ///PLAN_SHIPPING_TIME
            spsPullOrderInfo.PlanShippingTime = materialPullingOrderInfo.PlanShippingTime;
            ///PLAN_DELIVERY_TIME
            spsPullOrderInfo.PlanDeliveryTime = materialPullingOrderInfo.PlanDeliveryTime;
            ///DAY_VEHICHE_SEQ_NO 当日车辆顺序号 
            spsPullOrderInfo.DayVehicheSeqNo = materialPullingOrderInfo.DayVehicheSeqNo;
            ///KEEPER 保管员
            spsPullOrderInfo.Keeper = null;
            ///VEHICHE_MODEL_NO 车型编号
            spsPullOrderInfo.VehicheModelNo = materialPullingOrderInfo.VehicheModelNo;
            ///PRODUCTION_NO 生产订单号
            spsPullOrderInfo.ProductionNo = materialPullingOrderInfo.ProduceNo;
            ///PACKAGE_MODEL 包装编号
            spsPullOrderInfo.PackageModel = materialPullingOrderInfo.PackageModel;
            ///PACKAGE 单包装数量
            spsPullOrderInfo.Package = materialPullingOrderInfo.Package.GetValueOrDefault();
            ///CREATE_USER
            spsPullOrderInfo.CreateUser = loginUser;
            return SpsPullOrderDAL.GetInsertSql(spsPullOrderInfo);
        }

    }
}