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
    public class EmergencyPullingCartBLL
    {
        #region Common
        EmergencyPullingCartDAL dal = new EmergencyPullingCartDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<EmergencyPullingCartInfo></returns>
        public List<EmergencyPullingCartInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public EmergencyPullingCartInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }

        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info">对象</param>
        /// <returns></returns>
        public long InsertInfo(EmergencyPullingCartInfo info)
        {
            if (info.LogisticStandardFid == null)
                throw new Exception("MC:0x00000084");///数据错误
            MaintainInhouseLogisticStandardInfo standardInfo = new MaintainInhouseLogisticStandardDAL().GetInfoByFid(info.LogisticStandardFid.GetValueOrDefault());
            if (standardInfo == null)
                throw new Exception("MC:0x00000084");///数据错误

            ///
            GetEmergencyPullingCartInfo(standardInfo, ref info);
            ///REQUIRED_BOX_QTY
            if (info.PullPackageQty.GetValueOrDefault() > 0)
                info.RequiredBoxQty = Convert.ToInt32(Math.Ceiling(info.RequiredPartQty.GetValueOrDefault() / info.PullPackageQty.GetValueOrDefault()));
            ///EMERGENCY_PULL_MODE
            if (info.EmergencyPullMode == null)
                throw new Exception("MC:0x00000084");///数据错误

            ///STATUS
            info.Status = 10;

            return dal.Add(info);
        }

        /// <summary>
        /// LogicDeleteInfo
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="loginUser">用户</param>
        /// <returns></returns>
        public bool LogicDeleteInfo(long id, string loginUser)
        {
            return dal.LogicDelete(id, loginUser) > 0 ? true : false;
        }

        /// <summary>
        /// UpdateInfo
        /// </summary>
        /// <param name="fields">更新字段</param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <returns>List<EmergencyPullingCartInfo></returns>
        public List<EmergencyPullingCartInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }

        #endregion

        #region Private
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool BatchDeletingInfos(List<string> rowsKeyValues, string loginUser)
        {
            StringBuilder @string = new StringBuilder();
            foreach (var rowsKeyValue in rowsKeyValues)
            {
                string[] keyValues = rowsKeyValue.Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
                if (keyValues.Length == 0)
                    throw new Exception("MC:0x00000084");///数据错误 

                @string.AppendLine("update [LES].[TE_MPM_EMERGENCY_PULLING_CART] " +
                 "set [VALID_FLAG] = 0," +
                 "[MODIFY_USER] = N'" + loginUser + "'," +
                 "[MODIFY_DATE] = GETDATE() " +
                 "where [ID] = " + Convert.ToInt64(keyValues[0]) + ";");
            }
            ///执行
            using (var trans = new TransactionScope())
            {
                if (@string.Length > 0)
                    CommonDAL.ExecuteNonQueryBySql(@string.ToString());
                trans.Complete();
            }
            return true;
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="rowsKeyValues"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool ReleaseInfos(List<string> rowsKeyValues, string loginUser)
        {
            List<EmergencyPullingCartInfo> emergencyPullingCarts = new List<EmergencyPullingCartInfo>();
            foreach (var rowsKeyValue in rowsKeyValues)
            {
                string[] keyValues = rowsKeyValue.Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
                if (keyValues.Length == 0)
                    throw new Exception("MC:0x00000084");///数据错误 
                if (keyValues.Length == 1)
                    throw new Exception("MC:0x00000502");///物料需求数量不能为空

                EmergencyPullingCartInfo emergencyPullingCart = new EmergencyPullingCartInfo();
                emergencyPullingCart.Id = Convert.ToInt64(keyValues[0]);
                emergencyPullingCart.RequiredPartQty = Convert.ToDecimal(keyValues[1]);
                if (emergencyPullingCart.RequiredPartQty <= 0)
                    throw new Exception("MC:0x00000507");///物料需求数量不能小于等于零
                emergencyPullingCarts.Add(emergencyPullingCart);
            }

            List<EmergencyPullingCartInfo> emergencyPullingCartInfos = dal.GetList("[ID] in (" + string.Join(",", emergencyPullingCarts.Select(d => d.Id).ToArray()) + ")", string.Empty);
            if (emergencyPullingCartInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            StringBuilder @string = new StringBuilder();
            foreach (EmergencyPullingCartInfo emergencyPullingCartInfo in emergencyPullingCartInfos)
            {
                EmergencyPullingCartInfo cartInfo = emergencyPullingCarts.FirstOrDefault(d => d.Id == emergencyPullingCartInfo.Id);
                if (cartInfo == null) continue;
                emergencyPullingCartInfo.RequiredPartQty = cartInfo.RequiredPartQty;
                ///REQUIRED_BOX_QTY
                if (emergencyPullingCartInfo.PullPackageQty.GetValueOrDefault() > 0)
                    emergencyPullingCartInfo.RequiredBoxQty = Convert.ToInt32(Math.Ceiling(emergencyPullingCartInfo.RequiredPartQty.GetValueOrDefault() / emergencyPullingCartInfo.PullPackageQty.GetValueOrDefault()));

                @string.AppendLine("update [LES].[TE_MPM_EMERGENCY_PULLING_CART] " +
                "set [VALID_FLAG] = 0," +
                "[REQUIRED_PART_QTY] = " + emergencyPullingCartInfo.RequiredPartQty.GetValueOrDefault() + "," +
                "[REQUIRED_BOX_QTY] = " + emergencyPullingCartInfo.RequiredBoxQty.GetValueOrDefault() + "," +
                "[MODIFY_USER] = N'" + loginUser + "'," +
                "[MODIFY_DATE] = GETDATE() " +
                "where [ID] = " + emergencyPullingCartInfo.Id + ";");
            }
            @string.AppendLine(Handler(emergencyPullingCartInfos, loginUser));
            ///执行
            using (var trans = new TransactionScope())
            {
                if (@string.Length > 0)
                    CommonDAL.ExecuteNonQueryBySql(@string.ToString());
                trans.Complete();
            }
            return true;
        }
        #endregion

        #region Interface
        /// <summary>
        /// Create EmergencyPullingCartInfo
        /// </summary>
        /// <param name="loginUser"></param>
        /// <returns>EmergencyPullingCartInfo</returns>
        public static EmergencyPullingCartInfo CreateEmergencyPullingCartInfo(string loginUser)
        {
            EmergencyPullingCartInfo info = new EmergencyPullingCartInfo();
            ///FID
            info.Fid = Guid.NewGuid();
            ///LOGISTIC_STANDARD_FID
            info.LogisticStandardFid = null;
            ///REQUIRED_PART_QTY
            info.RequiredPartQty = null;
            ///EMERGENCY_PULL_MODE
            info.EmergencyPullMode = null;
            ///TRIGGER_PULL_FLAG
            info.TriggerPullFlag = null;
            ///TRIGGER_WM_NO
            info.TriggerWmNo = null;
            ///TRIGGER_ZONE_NO
            info.TriggerZoneNo = null;
            ///STATUS
            info.Status = null;
            ///COMMENTS
            info.Comments = null;
            ///VALID_FLAG
            info.ValidFlag = true;
            ///CREATE_DATE
            info.CreateDate = DateTime.Now;
            ///CREATE_USER
            info.CreateUser = loginUser;
            ///MODIFY_DATE
            info.ModifyDate = null;
            ///MODIFY_USER
            info.ModifyUser = null;
            return info;
        }
        /// <summary>
        /// MaintainInhouseLogisticStandardInfo -> EmergencyPullingCartInfo
        /// </summary>
        /// <param name="maintainInhouseLogisticStandardInfo"></param>
        /// <param name="info"></param>
        public static void GetEmergencyPullingCartInfo(MaintainInhouseLogisticStandardInfo maintainInhouseLogisticStandardInfo, ref EmergencyPullingCartInfo info)
        {
            if (maintainInhouseLogisticStandardInfo == null) return;
            ///LOGISTICSTANDARD_FID
            info.LogisticStandardFid = maintainInhouseLogisticStandardInfo.Fid;
            ///PART_NO
            info.PartNo = maintainInhouseLogisticStandardInfo.PartNo;
            ///PART_CNAME
            info.PartCname = maintainInhouseLogisticStandardInfo.PartCname;
            ///PULL_PACKAGE_QTY
            info.PullPackageQty = maintainInhouseLogisticStandardInfo.InboundPackage;
            ///PULL_PACKAGE_MODEL
            info.PullPackageModel = maintainInhouseLogisticStandardInfo.InboundPackageModel;
            ///SUPPLIER_NUM
            info.SupplierNum = maintainInhouseLogisticStandardInfo.SupplierNum;
            ///PULL_MODE
            info.PullMode = Convert.ToInt32(maintainInhouseLogisticStandardInfo.InhouseSystemMode);
            ///PART_BOX_CODE
            info.PartBoxCode = maintainInhouseLogisticStandardInfo.InhousePartClass;
            ///S_WM_NO
            info.SWmNo = maintainInhouseLogisticStandardInfo.SWmNo;
            ///S_ZONE_NO
            info.SZoneNo = maintainInhouseLogisticStandardInfo.SZoneNo;
            ///T_WM_NO
            info.TWmNo = maintainInhouseLogisticStandardInfo.TWmNo;
            ///T_ZONE_NO
            info.TZoneNo = maintainInhouseLogisticStandardInfo.TZoneNo;
            ///PLANT
            info.Plant = maintainInhouseLogisticStandardInfo.Plant;
            ///WORKSHOP
            info.Workshop = maintainInhouseLogisticStandardInfo.Workshop;
            ///ASSEMBLY_LINE
            info.AssemblyLine = maintainInhouseLogisticStandardInfo.AssemblyLine;
            ///WORKSHOP_SECTION
            info.WorkshopSection = maintainInhouseLogisticStandardInfo.WorkshopSection;
            ///LOCATION
            info.Location = maintainInhouseLogisticStandardInfo.Location;

        }

        /// <summary>
        /// ReceiveInfo-->EmergencyPullingCartInfo
        /// </summary>
        /// <param name="receiveInfo"></param>
        /// <param name="info"></param>
        public static void GetEmergencyPullingCartByReceive(ReceiveInfo receiveInfo , ref EmergencyPullingCartInfo info)
        {
            if (receiveInfo == null) return;
            ///PULL_MODE,拉动方式
            //info.PullMode = receiveInfo.PullMode.GetValueOrDefault();
            /////PART_BOX_CODE,零件类代码
            //info.PartBoxCode = receiveInfo.PartBoxCode;

        }
        /// <summary>
        /// ReceiveDetailInfo-->EmergencyPullingCartInfo
        /// </summary>
        /// <param name="receiveDetailInfo"></param>
        /// <param name="info"></param>
        public static void GetEmergencyPullingCartByReceiveDetail(ReceiveDetailInfo receiveDetailInfo, ref EmergencyPullingCartInfo info)
        {
            if (receiveDetailInfo == null) return;
            ///PART_NO,物料图号
            info.PartNo = receiveDetailInfo.PartNo;
            ///REQUIRED_PART_QTY,物料需求数量
            info.RequiredPartQty = receiveDetailInfo.RequiredQty.GetValueOrDefault();
            ///REQUIRED_BOX_QTY,包装需求数量
            info.RequiredBoxQty = receiveDetailInfo.RequiredBoxNum.GetValueOrDefault();
            ///PART_CNAME,物料中文描述
            info.PartCname = receiveDetailInfo.PartCname;
            ///PULL_PACKAGE_QTY,拉动包装数 ///TODO: Package和InhousePackage 那个有效？
            info.PullPackageQty = receiveDetailInfo.Package.GetValueOrDefault();
            ///PULL_PACKAGE_MODEL,拉动包装型号 ///TODO:PackageModel和InhousePackageModel 那个有效？
            info.PullPackageModel = receiveDetailInfo.PackageModel;
            ///SUPPLIER_NUM,供应商代码
            info.SupplierNum = receiveDetailInfo.SupplierNum;
            ///S_WM_NO,来源仓库
            info.SWmNo = receiveDetailInfo.WmNo;
            ///S_ZONE_NO,来源存储区
            info.SZoneNo = receiveDetailInfo.ZoneNo;
            ///T_WM_NO,目标仓库
            info.TWmNo = receiveDetailInfo.TargetWm;
            ///T_ZONE_NO,目标存储区
            info.TZoneNo = receiveDetailInfo.TargetZone;
            ///PLANT,工厂代码
            info.Plant = receiveDetailInfo.Plant;
            ///ASSEMBLY_LINE,产线代码
            info.AssemblyLine = receiveDetailInfo.AssemblyLine;
            ///EMERGENCY_PULL_MODE,紧急拉动模式
            info.EmergencyPullMode = (int)EmergencyPullModeConstants.ManualPull;
            ///STATUS,状态
            info.Status = 10;///已提交 TODO:没有枚举项

        }
        #endregion

        #region Handler
        /// <summary>
        /// 对于MPM-036逻辑进行函数封装
        /// </summary>
        /// <param name="emergencyPullingCartInfos"></param>
        /// <param name="loginUser"></param>
        public static string Handler(List<EmergencyPullingCartInfo> emergencyPullingCartInfos, string loginUser)
        {
            ///执行语句
            StringBuilder @string = new StringBuilder();
            ///获取系统配置是否允许过量提前拉动
            string allowedFlag = new ConfigDAL().GetValueByCode("ENABLE_EXCEED_COUNTER_QTY_WHEN_ADVANCE_PULL");
            ///接收紧急拉动购物车对象集合进行处理
            if (emergencyPullingCartInfos.Count == 0)
                throw new Exception("MC:0x00000084");///数据错误

            ///对应的物料拉动信息
            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandardInfos = new MaintainInhouseLogisticStandardBLL().GetList("" +
                " and [STATUS] =" + (int)BasicDataStatusConstants.Enable + "" +
                " and [FID] in ('" + string.Join("','", emergencyPullingCartInfos.Select(d => d.LogisticStandardFid).ToArray()) + "')", string.Empty);
            if (maintainInhouseLogisticStandardInfos.Count == 0)
                throw new Exception("MC:0x00000213");///物料拉动信息数据错误

            ///首先需要将其按照拉动方式、零件类进行分组，每个分组产生一张紧急拉动单
            var cartGroups = emergencyPullingCartInfos.GroupBy(d => new { d.PullMode, d.PartBoxCode }).ToList();

            #region 手工拉动 PLAN
            var planCartGroups = cartGroups.Where(d => d.Key.PullMode == (int)PullModeConstants.Plan).ToList();
            ///计划拉动零件类
            List<PlanPartBoxInfo> planPartBoxInfos = new PlanPartBoxBLL().GetList("" +
                " and [STATUS] = " + (int)BasicDataStatusConstants.Enable + "" +
                " and [PART_BOX_CODE] in ('" + string.Join("','", planCartGroups.Select(d => d.Key.PartBoxCode).ToArray()) + "')", string.Empty);
            foreach (var planCartGroup in planCartGroups)
            {
                ///从参数集合从提取对应的紧急拉动购物车集合
                List<EmergencyPullingCartInfo> pullingCartInfos = emergencyPullingCartInfos.Where(d =>
                 d.PullMode == planCartGroup.Key.PullMode && d.PartBoxCode == planCartGroup.Key.PartBoxCode).ToList();
                if (pullingCartInfos.Count == 0) continue;
                ///计划零件类
                PlanPartBoxInfo planPartBoxInfo = planPartBoxInfos.FirstOrDefault(d => d.PartBoxCode == planCartGroup.Key.PartBoxCode);
                if (planPartBoxInfo == null) continue;
                ///计划拉动-->仓储衔接主表
                MaterialPullingOrderInfo materialPulling = MaterialPullingCommonBLL.CreateMaterialPullingOrderInfo();
                ///计划拉动单主表
                PlanPullOrderInfo planPullOrderInfo = PlanPullOrderBLL.CreatePlanPullOrder(loginUser);
                ///OrderType 拉动单类型
                planPullOrderInfo.OrderType = (int)PullOrderTypeConstants.Emergency;
                ///PlanPartBoxInfo-->PlanPullOrderInfo
                PlanPullOrderBLL.GetPlanPullOrder(planPartBoxInfo, ref planPullOrderInfo);
                ///计划拉动单主表sql
                @string.AppendLine(PlanPullOrderDAL.GetInsertSql(planPullOrderInfo));
                ///PlanPullOrderInfo -> MaterialPullingOrderInfo
                MaterialPullingCommonBLL.GetMaterialPlanPullingOrderInfo(planPullOrderInfo, ref materialPulling);
                foreach (EmergencyPullingCartInfo planCartInfo in pullingCartInfos)
                {
                    ///物料拉动信息
                    MaintainInhouseLogisticStandardInfo maintainInhouseLogisticStandardInfo = maintainInhouseLogisticStandardInfos.FirstOrDefault(d => d.Fid == planCartInfo.LogisticStandardFid);
                    if (maintainInhouseLogisticStandardInfo == null) continue;
                    ///物料包装数量在此需要根据单包装数量以及物料需求数量进行向上圆整计算
                    ///REQUIRED_BOX_QTY:物料包装数量  PULL_PACKAGE_QTY:单包装数量 REQUIRED_PART_QTY:物料需求数量
                    if (planCartInfo.PullPackageQty.GetValueOrDefault() == 0) continue;///O不能做被除数
                    planCartInfo.RequiredBoxQty = Convert.ToInt32(Math.Ceiling(planCartInfo.RequiredPartQty.GetValueOrDefault() / planCartInfo.PullPackageQty.GetValueOrDefault()));
                    ///仓储衔接明细表
                    MaterialPullingOrderDetailInfo detailInfo = MaterialPullingCommonBLL.CreateMaterialPullingOrderDetailInfo();
                    ///计划拉动单明细
                    PlanPullOrderDetailInfo planPullOrderDetailInfo = PlanPullOrderDetailBLL.CreatePlanPullOrderDetail(loginUser);
                    ///MaintainInhouseLogisticStandardInfo -> PlanPullOrderDetailInfo
                    PlanPullOrderDetailBLL.GetPlanPullOrderDetail(maintainInhouseLogisticStandardInfo, ref planPullOrderDetailInfo);
                    ///TwdPullOrderInfo -> TwdPullOrderDetailInfo
                    PlanPullOrderDetailBLL.GetPlanPullOrderDetailInfo(planPullOrderInfo, ref planPullOrderDetailInfo);
                    ///RequiredPackageQty
                    planPullOrderDetailInfo.RequiredPackageQty = planCartInfo.RequiredBoxQty.GetValueOrDefault();
                    ///RequiredPartQty
                    planPullOrderDetailInfo.RequiredPartQty = planCartInfo.RequiredPartQty.GetValueOrDefault();
                    ///计划拉动明细sql
                    @string.AppendLine(PlanPullOrderDetailDAL.GetInsertSql(planPullOrderDetailInfo));
                    ///PlanPullOrderDetailInfo -> MaterialPullingOrderDetailInfo
                    MaterialPullingCommonBLL.GetMaterialPullingOrderDetail(planPullOrderDetailInfo, ref detailInfo);
                    ///仓储明细集合Add
                    materialPulling.MaterialPullingOrderDetailInfos.Add(detailInfo);
                }
                ///拉动单生成后需要调用拉动仓储衔接函数获取语句                 
                @string.AppendLine(MaterialPullingCommonBLL.Handler(materialPulling, loginUser));
            }

            #endregion

            #region 提前拉动 TWD
            var twdCartGroups = cartGroups.Where(d => d.Key.PullMode == (int)PullModeConstants.Pcs || d.Key.PullMode == (int)PullModeConstants.Twd).ToList();
            ///TWD计数器
            List<TwdCounterInfo> twdCounterInfos = new TwdCounterBLL().GetList("" +
                    "[STATUS] =" + (int)BasicDataStatusConstants.Enable + " and " +
                    "[PART_PULL_FID] in ('" + string.Join("','", maintainInhouseLogisticStandardInfos.Select(d => d.Fid).ToArray()) + "')  and " +
                    "isnull([CURRENT_QTY],0) > 0", string.Empty);
            ///TWD零件类
            List<TwdPartBoxInfo> twdPartBoxInfos = new TwdPartBoxBLL().GetList("" +
               "[STATUS] =" + (int)BasicDataStatusConstants.Enable + " and " +
               "[PART_BOX_CODE] in ('" + string.Join("','", twdCartGroups.Select(d => d.Key.PartBoxCode).ToArray()) + "')", string.Empty);
            foreach (var twdCartGroup in twdCartGroups)
            {
                ///从参数集合从提取对应的紧急拉动购物车集合
                List<EmergencyPullingCartInfo> pullingCartInfos = emergencyPullingCartInfos.Where(d =>
                 d.PullMode == twdCartGroup.Key.PullMode && d.PartBoxCode == twdCartGroup.Key.PartBoxCode).ToList();
                if (pullingCartInfos.Count == 0) continue;
                ///TWD零件类
                TwdPartBoxInfo twdPartBoxInfo = twdPartBoxInfos.FirstOrDefault(d => d.PartBoxCode == twdCartGroup.Key.PartBoxCode);
                if (twdPartBoxInfo == null) continue;
                ///触发层级拉动的集合
                List<EmergencyPullingCartInfo> pullingLevelCartInfos = pullingCartInfos.Where(d => d.TriggerPullFlag == true).ToList();
                foreach (EmergencyPullingCartInfo pullingLevelCartInfo in pullingLevelCartInfos)
                {
                    ///需要根据其物料拉动信息外键获取对应的计数器数据（状态必须为已启用）           
                    TwdCounterInfo twdCounterInfo = twdCounterInfos.FirstOrDefault(d =>
                    d.PartPullFid == pullingLevelCartInfo.LogisticStandardFid.GetValueOrDefault() &&
                    d.Status == (int)BasicDataStatusConstants.Enable);
                    if (twdCounterInfo == null) continue;
                    ///在此之前需要根据物料图号、供应商、层级仓库、层级存储区在物料拉动信息中获取匹配的目标仓库、目标存储区数据
                    MaintainInhouseLogisticStandardInfo maintainInhouseLogisticStandardInfo = maintainInhouseLogisticStandardInfos.FirstOrDefault(d =>
                     d.PartNo == pullingLevelCartInfo.PartNo &&
                     d.SupplierNum == pullingLevelCartInfo.SupplierNum &&
                     d.WmNo == pullingLevelCartInfo.TriggerWmNo &&
                     d.ZoneNo == pullingLevelCartInfo.TriggerZoneNo);
                    ///触发层级拉动
                    @string.AppendFormat(TwdCounterBLL.LevelPullCounter(maintainInhouseLogisticStandardInfo, pullingLevelCartInfo.RequiredPartQty.GetValueOrDefault(), loginUser, twdCounterInfo.Fid.GetValueOrDefault(), twdPartBoxInfo));
                }
                ///提前拉动的集合
                List<EmergencyPullingCartInfo> advancePullCartInfos = pullingCartInfos.Where(d => 
                d.TriggerPullFlag == false && d.EmergencyPullMode==(int)EmergencyPullModeConstants.AdvancePull).ToList();
                if (advancePullCartInfos.Count == 0) continue;
                ///仓储衔接主表
                MaterialPullingOrderInfo materialPulling = MaterialPullingCommonBLL.CreateMaterialPullingOrderInfo();
                ///TWD拉动单主表
                TwdPullOrderInfo twdPullOrderInfo = TwdPullOrderBLL.CreateTwdPullOrderInfo(loginUser);
                ///TwdPartBoxInfo -> TwdPullOrderInfo
                TwdPullOrderBLL.GetTwdPullOrderInfo(twdPartBoxInfo, ref twdPullOrderInfo);
                ///TWD拉动单主表sql
                @string.AppendLine(TwdPullOrderDAL.GetInsertSql(twdPullOrderInfo));
                ///TwdPullOrderInfo-->MaterialPullingOrderInfo
                MaterialPullingCommonBLL.GetMaterialPullingOrderInfo(twdPullOrderInfo, ref materialPulling);
                int rowNo = 0;///行号
                              ///逐条循环每个购物车
                foreach (EmergencyPullingCartInfo advancePullCartInfo in advancePullCartInfos)
                {
                    ///需要根据其物料拉动信息外键获取对应的计数器数据（状态必须为已启用）           
                    ///TT_MPM_TWD_COUNTER TWD计数器
                    TwdCounterInfo twdCounterInfo = twdCounterInfos.FirstOrDefault(d =>
                    d.PartPullFid == advancePullCartInfo.LogisticStandardFid.GetValueOrDefault() &&
                    d.Status == (int)BasicDataStatusConstants.Enable);
                    if (twdCounterInfo == null) continue;
                    ///物料拉动信息
                    MaintainInhouseLogisticStandardInfo maintainInhouseLogisticStandardInfo = maintainInhouseLogisticStandardInfos.FirstOrDefault(d => d.Fid == advancePullCartInfo.LogisticStandardFid);
                    if (maintainInhouseLogisticStandardInfo == null) continue;
                    ///物料包装数量在此需要根据单包装数量以及物料需求数量进行向上圆整计算
                    ///REQUIRED_BOX_QTY:物料包装数量  PULL_PACKAGE_QTY:单包装数量 REQUIRED_PART_QTY:物料需求数量
                    if (advancePullCartInfo.PullPackageQty.GetValueOrDefault() == 0) continue;///O不能做被除数
                    advancePullCartInfo.RequiredBoxQty = Convert.ToInt32(Math.Ceiling(advancePullCartInfo.RequiredPartQty.GetValueOrDefault() / advancePullCartInfo.PullPackageQty.GetValueOrDefault()));
                    ///若该系统配置标记为false时、计数器当前累计数量不允许小于购物车物料需求数量，
                    if (allowedFlag.ToLower() == "false" && twdCounterInfo.CurrentQty.GetValueOrDefault() < advancePullCartInfo.RequiredPartQty.GetValueOrDefault()) continue;
                    ///仓储衔接明细表
                    MaterialPullingOrderDetailInfo detailInfo = MaterialPullingCommonBLL.CreateMaterialPullingOrderDetailInfo();
                    ///TWD明细表
                    TwdPullOrderDetailInfo pullOrderDetailInfo = TwdPullOrderDetailBLL.CreateTwdPullOrderDetailInfo(loginUser);
                    ///MaintainInhouseLogisticStandardInfo -> TwdPullOrderDetailInfo
                    TwdPullOrderDetailBLL.GetTwdPullOrderDetailInfo(maintainInhouseLogisticStandardInfo, ref pullOrderDetailInfo);
                    ///TwdPullOrderInfo -> TwdPullOrderDetailInfo
                    TwdPullOrderDetailBLL.GetTwdPullOrderDetailInfo(twdPullOrderInfo, ref pullOrderDetailInfo);
                    ///ROW_NO,行号
                    pullOrderDetailInfo.RowNo = ++rowNo;
                    ///REQUIRED_PACKAGE_QTY,需求包装数
                    pullOrderDetailInfo.RequiredPackageQty = advancePullCartInfo.RequiredBoxQty.GetValueOrDefault();
                    ///REQUIRED_PART_QTY,需求物料数量
                    pullOrderDetailInfo.RequiredPartQty = advancePullCartInfo.RequiredPartQty.GetValueOrDefault();
                    ///TWD明细表sql
                    @string.AppendLine(TwdPullOrderDetailDAL.GetInsertSql(pullOrderDetailInfo));
                    ///TwdPullOrderDetailInfo -> MaterialPullingOrderDetailInfo
                    MaterialPullingCommonBLL.GetMaterialPullingOrderDetailInfo(pullOrderDetailInfo, ref detailInfo);
                    ///仓储衔接集合Add
                    materialPulling.MaterialPullingOrderDetailInfos.Add(detailInfo);
                    ///否则直接对计数器的当前累计数量按购物车物料需求数量进行扣减
                    @string.AppendLine("update [LES].[TT_MPM_TWD_COUNTER] " +
                        "set [CURRENT_QTY] = isnull([CURRENT_QTY],0) - " + advancePullCartInfo.RequiredPartQty.GetValueOrDefault() + "," +
                        "[MODIFY_DATE] = GETDATE()," +
                        "[MODIFY_USER] = N'" + loginUser + "' " +
                        "where [ID]= " + twdCounterInfo.Id + ";");
                    ///同时记录计数器日志并标记其类型为提前拉动
                    TwdCounterLogInfo twdCounterLogInfo = TwdCounterLogBLL.CreateTwdCounterLogInfo(twdCounterInfo.Fid.GetValueOrDefault(), loginUser);
                    ///TwdCounterInfo -> TwdCounterLogInfo
                    TwdCounterLogBLL.GetTwdCounterLogInfo(twdCounterInfo, ref twdCounterLogInfo);
                    ///PART_QTY,物料数量
                    twdCounterLogInfo.PartQty = 0 - advancePullCartInfo.RequiredPartQty.GetValueOrDefault();
                    ///SOURCE_DATA,目视来源数据
                    twdCounterLogInfo.SourceData = twdPullOrderInfo.OrderCode;
                    ///SOURCE_DATA_FID,数据来源外键
                    twdCounterLogInfo.SourceDataFid = twdPullOrderInfo.Fid;
                    ///SOURCE_DATA_TYPE,数据来源类型
                    twdCounterLogInfo.SourceDataType = (int)TwdCounterSourceDataTypeConstants.AdvancePull;
                    ///计数器日志sql
                    @string.AppendLine(TwdCounterLogDAL.GetInsertSql(twdCounterLogInfo));
                }
                ///拉动单生成后需要调用拉动仓储衔接函数获取语句                 
                @string.AppendLine(MaterialPullingCommonBLL.Handler(materialPulling, loginUser));
            }

            #endregion

            return @string.ToString();
        }
        #endregion
    }
}

