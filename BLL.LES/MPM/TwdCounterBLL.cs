namespace BLL.LES
{
    using DAL.LES;
    using DM.LES;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data;
    using DAL.SYS;
    using DM.SYS;
    using System.Transactions;

    /// <summary>
    /// TwdCounterBLL
    /// </summary>
    public class TwdCounterBLL
    {
        #region Common
        /// <summary>
        /// TwdCounterDAL
        /// </summary>
        TwdCounterDAL dal = new TwdCounterDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<TwdCounterInfo></returns>
        public List<TwdCounterInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <returns></returns>
        public List<TwdCounterInfo> GetList(string textWhere, string textOrder)
        {
            return dal.GetList(textWhere, textOrder);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TwdCounterInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(TwdCounterInfo info)
        {
            return dal.Add(info);
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
        /// UpdateInfo
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        #endregion

        #region Private
        /// <summary>
        /// 执行导入EXCEL数据
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        public bool ImportDataByExcel(DataTable dataTable, Dictionary<string, string> fieldNames, string loginUser)
        {
            List<TwdCounterInfo> twdCounterExcelInfos = CommonDAL.DatatableConvertToList<TwdCounterInfo>(dataTable).ToList();
            if (twdCounterExcelInfos.Count == 0)
                throw new Exception("MC:1x00000043");///数据格式不符合导入规范

            List<TwdCounterInfo> twdCounterInfos = dal.GetList("" +
                "[PART_BOX_CODE] in ('" + string.Join("','", twdCounterExcelInfos.Select(d => d.PartBoxCode).ToArray()) + "') and " +
                "[PART_NO] in ('" + string.Join("','", twdCounterExcelInfos.Select(d => d.PartNo).ToArray()) + "') and " +
                "[PACKAGE_MODEL] in ('" + string.Join("','", twdCounterExcelInfos.Select(d => d.PackageModel).ToArray()) + "')", string.Empty);

            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandardInfos = new MaintainInhouseLogisticStandardDAL().GetList("" +
                "[INHOUSE_SYSTEM_MODE] = N'" + (int)PullModeConstants.Twd + "' and " +
                "[INHOUSE_PART_CLASS] in ('" + string.Join("','", twdCounterExcelInfos.Select(d => d.PartBoxCode).ToArray()) + "') and " +
                "[PART_NO] in ('" + string.Join("','", twdCounterExcelInfos.Select(d => d.PartNo).ToArray()) + "')", string.Empty);

            ///获取零件类信息
            List<TwdPartBoxInfo> twdPartBoxInfos = new TwdPartBoxDAL().GetList("" +
                "[PART_BOX_CODE] in ('" + string.Join("','", twdCounterExcelInfos.Select(d => d.PartBoxCode).ToArray()) + "')", string.Empty);


            StringBuilder @string = new StringBuilder();
            foreach (TwdCounterInfo twdCounterExcelInfo in twdCounterExcelInfos)
            {
                /// 零件类代码②、工厂③车间⑤生产线⑥工段⑬工位⑭、物料号⑩、物料版本⑫、包装容器⑰为联合主键，差异数量DiffQty作为累加当前计数⑮的依据，备注直接更新
                TwdCounterInfo twdCounterInfo = twdCounterInfos.FirstOrDefault(d =>
                d.PartBoxCode == twdCounterExcelInfo.PartBoxCode &&
                d.PartNo == twdCounterExcelInfo.PartNo &&
                d.PackageModel == twdCounterExcelInfo.PackageModel &&
                d.Plant == twdCounterExcelInfo.Plant &&
                d.Workshop == twdCounterExcelInfo.Workshop &&
                d.AssemblyLine == twdCounterExcelInfo.AssemblyLine &&
                d.WorkshopSection == twdCounterExcelInfo.WorkshopSection &&
                d.Location == twdCounterExcelInfo.Location &&
                d.PartVersion == twdCounterExcelInfo.PartVersion);

                #region 逐步减低维度处理
                if (twdCounterInfo == null)
                    twdCounterInfo = twdCounterInfos.FirstOrDefault(d =>
                    d.PartBoxCode == twdCounterExcelInfo.PartBoxCode &&
                    d.PartNo == twdCounterExcelInfo.PartNo &&
                    d.PackageModel == twdCounterExcelInfo.PackageModel &&
                    d.Plant == twdCounterExcelInfo.Plant &&
                    d.Workshop == twdCounterExcelInfo.Workshop &&
                    d.AssemblyLine == twdCounterExcelInfo.AssemblyLine &&
                    d.WorkshopSection == twdCounterExcelInfo.WorkshopSection &&
                    d.Location == twdCounterExcelInfo.Location);
                if (twdCounterInfo == null)
                    twdCounterInfo = twdCounterInfos.FirstOrDefault(d =>
                    d.PartBoxCode == twdCounterExcelInfo.PartBoxCode &&
                    d.PartNo == twdCounterExcelInfo.PartNo &&
                    d.PackageModel == twdCounterExcelInfo.PackageModel &&
                    d.Plant == twdCounterExcelInfo.Plant &&
                    d.Workshop == twdCounterExcelInfo.Workshop &&
                    d.AssemblyLine == twdCounterExcelInfo.AssemblyLine &&
                    d.WorkshopSection == twdCounterExcelInfo.WorkshopSection);
                if (twdCounterInfo == null)
                    twdCounterInfo = twdCounterInfos.FirstOrDefault(d =>
                    d.PartBoxCode == twdCounterExcelInfo.PartBoxCode &&
                    d.PartNo == twdCounterExcelInfo.PartNo &&
                    d.PackageModel == twdCounterExcelInfo.PackageModel &&
                    d.Plant == twdCounterExcelInfo.Plant &&
                    d.Workshop == twdCounterExcelInfo.Workshop &&
                    d.AssemblyLine == twdCounterExcelInfo.AssemblyLine);
                if (twdCounterInfo == null)
                    twdCounterInfo = twdCounterInfos.FirstOrDefault(d =>
                    d.PartBoxCode == twdCounterExcelInfo.PartBoxCode &&
                    d.PartNo == twdCounterExcelInfo.PartNo &&
                    d.PackageModel == twdCounterExcelInfo.PackageModel &&
                    d.Plant == twdCounterExcelInfo.Plant &&
                    d.Workshop == twdCounterExcelInfo.Workshop);
                if (twdCounterInfo == null)
                    twdCounterInfo = twdCounterInfos.FirstOrDefault(d =>
                    d.PartBoxCode == twdCounterExcelInfo.PartBoxCode &&
                    d.PartNo == twdCounterExcelInfo.PartNo &&
                    d.PackageModel == twdCounterExcelInfo.PackageModel &&
                    d.Plant == twdCounterExcelInfo.Plant);
                if (twdCounterInfo == null)
                    twdCounterInfo = twdCounterInfos.FirstOrDefault(d =>
                    d.PartBoxCode == twdCounterExcelInfo.PartBoxCode &&
                    d.PartNo == twdCounterExcelInfo.PartNo &&
                    d.PackageModel == twdCounterExcelInfo.PackageModel);
                if (twdCounterInfo == null)
                    twdCounterInfo = twdCounterInfos.FirstOrDefault(d =>
                    d.PartBoxCode == twdCounterExcelInfo.PartBoxCode &&
                    d.PartNo == twdCounterExcelInfo.PartNo);
                if (twdCounterInfo == null)
                    throw new Exception("MC:0x00000255");///数据格式不符合导入规范
                #endregion

                if (twdCounterInfo.Status.GetValueOrDefault() == (int)BasicDataStatusConstants.Disabled)
                    throw new Exception("MC:0x00000455");///计数器已作废不能修改数量

                @string.AppendLine("update [LES].[TT_MPM_TWD_COUNTER] set " +
                    "[CURRENT_QTY] = isnull([CURRENT_QTY],0) + " + twdCounterExcelInfo.DiffQty + "," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() where " +
                    "[ID] = " + twdCounterInfo.Id + ";");

                ///根据计数器的物料拉动信息外键获取物料拉动信息
                MaintainInhouseLogisticStandardInfo maintainInhouseLogisticStandardInfo = maintainInhouseLogisticStandardInfos.FirstOrDefault(d => d.Fid == twdCounterInfo.PartPullFid.GetValueOrDefault());
                if (maintainInhouseLogisticStandardInfo == null)
                    throw new Exception("MC:0x00000213");///物料拉动信息数据错误

                if (maintainInhouseLogisticStandardInfo.Status.GetValueOrDefault() != (int)BasicDataStatusConstants.Enable)
                    throw new Exception("MC:0x00000233");///没有已启用的物料拉动信息

                ///获取零件类信息
                TwdPartBoxInfo twdPartBoxInfo = twdPartBoxInfos.FirstOrDefault(d => d.PartBoxCode == maintainInhouseLogisticStandardInfo.InhousePartClass);
                ///未能成功获取零件类信息
                if (twdPartBoxInfo == null)
                    throw new Exception("MC:0x00000225");///拉动零件类数据错误

                ///零件类未启用
                if (twdPartBoxInfo.Status.GetValueOrDefault() != (int)BasicDataStatusConstants.Enable)
                    throw new Exception("MC:0x00000456");///零件类未启用

                ///创建计数器日志
                TwdCounterLogInfo twdCounterLogInfo = TwdCounterLogBLL.CreateTwdCounterLogInfo(twdCounterInfo.Fid.GetValueOrDefault(), loginUser);
                ///以物料拉动信息填充计数器日志
                TwdCounterLogBLL.GetTwdCounterLogInfo(maintainInhouseLogisticStandardInfo, ref twdCounterLogInfo);
                ///以零件类信息填充计数器日志
                TwdCounterLogBLL.GetTwdCounterLogInfo(twdPartBoxInfo, ref twdCounterLogInfo);
                ///PART_QTY 
                twdCounterLogInfo.PartQty = twdCounterExcelInfo.DiffQty;
                ///SOURCE_DATA_FID 
                twdCounterLogInfo.SourceDataFid = twdCounterInfo.Fid;
                ///SOURCE_DATA_TYPE 
                twdCounterLogInfo.SourceDataType = (int)TwdCounterSourceDataTypeConstants.Manual;
                ///SOURCE_DATA 
                twdCounterLogInfo.SourceData = twdCounterInfo.PartBoxCode;
                ///Comments
                twdCounterLogInfo.Comments = twdCounterExcelInfo.Comments;
                ///
                @string.AppendLine(TwdCounterLogDAL.GetInsertSql(twdCounterLogInfo));

                ///触发层级拉动
                @string.AppendLine(LevelPullRequirementCounter(
                    maintainInhouseLogisticStandardInfo,
                    twdCounterExcelInfo.DiffQty,
                    loginUser,
                    twdCounterInfo.Fid.GetValueOrDefault(),
                    twdCounterInfo.PartBoxCode));
            }
            ///执行
            using (var trans = new TransactionScope())
            {
                if (@string.Length == 0)
                    throw new Exception("MC:0x00000283");///没有可导入更新的数据
                CommonDAL.ExecuteNonQueryBySql(@string.ToString());
                trans.Complete();
            }
            ///
            return true;
        }
        #endregion

        /// <summary>
        /// 获取层级拉动需求累计语句
        /// </summary>
        /// <param name="maintainInhouseLogisticStandardInfo"></param>
        /// <param name="requireQty"></param>
        /// <param name="loginUser"></param>
        /// <param name="counterFid"></param>
        /// <param name="partBoxCode"></param>
        /// <returns></returns>
        public static string LevelPullRequirementCounter(MaintainInhouseLogisticStandardInfo maintainInhouseLogisticStandardInfo, decimal requireQty, string loginUser, Guid counterFid, string partBoxCode)
        {
            ///当物料拉动信息中的是否层级拉动标记 =False或未设置时函数直接返回空
            if (!maintainInhouseLogisticStandardInfo.IsTriggerPull.GetValueOrDefault())
                return string.Empty;
            ///根据物料拉动信息中的层级拉动仓库存储区以及物料号、供应商获取物料拉动信息表中目标仓库存储区对应的物料号、供应商数据
            List<MaintainInhouseLogisticStandardInfo> maintainInhouseLogisticStandardInfos = new MaintainInhouseLogisticStandardDAL().GetList("" +
                "[T_WM_NO] = N'" + maintainInhouseLogisticStandardInfo.WmNo + "' and " +
                "[T_ZONE_NO] = N'" + maintainInhouseLogisticStandardInfo.ZoneNo + "' and " +
                "[PART_NO] = N'" + maintainInhouseLogisticStandardInfo.PartNo + "' and " +
                "[STATUS] = " + (int)BasicDataStatusConstants.Enable + " and " +
                "[INHOUSE_SYSTEM_MODE] = N'" + (int)PullModeConstants.Twd + "'", string.Empty);
            ///若获取不成功则将供应商条件去除重新获取，此处在程序执行时可以先根据物料号获取，再过滤供应商
            MaintainInhouseLogisticStandardInfo maintainInhouseLogisticStandard = maintainInhouseLogisticStandardInfos.FirstOrDefault(d => d.SupplierNum == maintainInhouseLogisticStandardInfo.SupplierNum);
            if (maintainInhouseLogisticStandard == null)
                maintainInhouseLogisticStandardInfo = maintainInhouseLogisticStandardInfos.FirstOrDefault();
            ///未能成功获取物料拉动信息
            if (maintainInhouseLogisticStandard == null)
                return string.Empty;
            ///获取零件类信息
            TwdPartBoxInfo twdPartBoxInfo = new TwdPartBoxDAL().GetInfo(maintainInhouseLogisticStandard.InhousePartClass);
            ///未能成功获取零件类信息
            if (twdPartBoxInfo == null)
                return string.Empty;
            ///零件类未启用
            if (twdPartBoxInfo.Status.GetValueOrDefault() != (int)BasicDataStatusConstants.Enable)
                return string.Empty;
            ///根据物料拉动信息外键获取计数器，未能成功获取时需要创建
            TwdCounterInfo twdCounterInfo = new TwdCounterDAL().GetInfoByPartPullFid(maintainInhouseLogisticStandard.Fid.GetValueOrDefault());
            if (twdCounterInfo == null)
            {
                ///创建计数器
                twdCounterInfo = CreateTwdCounterInfo(loginUser);
                ///以物料拉动信息填充计数器
                GetTwdCounterInfo(maintainInhouseLogisticStandard, ref twdCounterInfo);
                ///以零件类信息填充计数器
                GetTwdCounterInfo(twdPartBoxInfo, ref twdCounterInfo);
                ///
                twdCounterInfo.Id = new TwdCounterDAL().Add(twdCounterInfo);
                if (twdCounterInfo.Id == 0)
                    throw new Exception("MC:0x00000453");///时间窗计数器创建失败
            }
            ///计数器状态未处于启用
            if (twdCounterInfo.Status != (int)BasicDataStatusConstants.Enable) return string.Empty;
            ///
            StringBuilder stringBuilder = new StringBuilder();
            ///
            stringBuilder.AppendLine(UpdateTwdCounter(maintainInhouseLogisticStandard, twdPartBoxInfo, requireQty, twdCounterInfo.Id, loginUser));
            ///创建计数器日志
            TwdCounterLogInfo twdCounterLogInfo = TwdCounterLogBLL.CreateTwdCounterLogInfo(twdCounterInfo.Fid.GetValueOrDefault(), loginUser);
            ///以物料拉动信息填充计数器日志
            TwdCounterLogBLL.GetTwdCounterLogInfo(maintainInhouseLogisticStandard, ref twdCounterLogInfo);
            ///以零件类信息填充计数器日志
            TwdCounterLogBLL.GetTwdCounterLogInfo(twdPartBoxInfo, ref twdCounterLogInfo);
            ///PART_QTY 
            twdCounterLogInfo.PartQty = requireQty;
            ///SOURCE_DATA_FID 
            twdCounterLogInfo.SourceDataFid = counterFid;
            ///SOURCE_DATA_TYPE 
            twdCounterLogInfo.SourceDataType = (int)TwdCounterSourceDataTypeConstants.Calculator;
            ///SOURCE_DATA 
            twdCounterLogInfo.SourceData = partBoxCode;
            ///
            stringBuilder.AppendLine(TwdCounterLogDAL.GetInsertSql(twdCounterLogInfo));
            ///触发层级拉动
            stringBuilder.AppendLine(LevelPullRequirementCounter(
                maintainInhouseLogisticStandard,
                requireQty,
                loginUser,
                twdCounterInfo.Fid.GetValueOrDefault(),
                twdCounterInfo.PartBoxCode));
            return stringBuilder.ToString();
        }
        /// <summary>
        /// 根据物料拉动信息外键获取计数器
        /// </summary>
        /// <param name="partPullFid"></param>
        /// <returns></returns>
        public static TwdCounterInfo GetInfoByPartPullFid(Guid partPullFid)
        {
            return new TwdCounterDAL().GetInfoByPartPullFid(partPullFid);
        }
        /// <summary>
        /// 获取更新计数器的语句
        /// </summary>
        /// <param name="maintainInhouseLogisticStandardInfo"></param>
        /// <param name="twdPartBoxInfo"></param>
        /// <param name="requireQty"></param>
        /// <param name="twdCounterId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static string UpdateTwdCounter(MaintainInhouseLogisticStandardInfo maintainInhouseLogisticStandardInfo, TwdPartBoxInfo twdPartBoxInfo, decimal requireQty, long twdCounterId, string loginUser)
        {
            return "update [LES].[TT_MPM_TWD_COUNTER] set " +
                "[PART_BOX_CODE] = N'" + maintainInhouseLogisticStandardInfo.InhousePartClass + "'," +
                "[PLANT] = N'" + maintainInhouseLogisticStandardInfo.Plant + "'," +
                "[PLANT_ZONE] = N'" + maintainInhouseLogisticStandardInfo.PlantZone + "'," +
                "[WORKSHOP] = N'" + maintainInhouseLogisticStandardInfo.Workshop + "'," +
                "[ASSEMBLY_LINE] = N'" + maintainInhouseLogisticStandardInfo.AssemblyLine + "'," +
                "[WORKSHOP_SECTION] = N'" + maintainInhouseLogisticStandardInfo.WorkshopSection + "'," +
                "[LOCATION] = N'" + maintainInhouseLogisticStandardInfo.Location + "'," +
                "[SUPPLIER_NUM] = N'" + maintainInhouseLogisticStandardInfo.SupplierNum + "'," +
                "[REQUIREMENT_ACCUMULATE_MODE] = " + twdPartBoxInfo.RequirementAccumulateMode.GetValueOrDefault() + "," +
                "[ROUNDNESS_MODE] = " + twdPartBoxInfo.RoundnessMode + "," +
                "[PART_NO] = N'" + maintainInhouseLogisticStandardInfo.PartNo + "'," +
                "[PART_CNAME] = N'" + maintainInhouseLogisticStandardInfo.PartCname + "'," +
                "[PACKAGE] = " + maintainInhouseLogisticStandardInfo.InboundPackage.GetValueOrDefault() + "," +
                "[PACKAGE_MODEL] = N'" + maintainInhouseLogisticStandardInfo.InboundPackageModel + "'," +
                "[CURRENT_QTY] = isnull([CURRENT_QTY],0) + " + requireQty + "," +
                "[MODIFY_DATE] = GETDATE()," +
                "[MODIFY_USER] = N'" + loginUser + "' where " +
                "[ID] = " + twdCounterId + ";";
        }

        #region TwdCounterInfo
        /// <summary>
        /// 创建 TwdCounterInfo
        /// </summary>
        /// <returns></returns>
        public static TwdCounterInfo CreateTwdCounterInfo(string loginUser)
        {
            TwdCounterInfo twdCounterInfo = new TwdCounterInfo();
            twdCounterInfo.Fid = Guid.NewGuid();
            twdCounterInfo.PartVersion = string.Empty;///TODO:物料版本
            twdCounterInfo.CurrentQty = null;
            twdCounterInfo.Status = (int)BasicDataStatusConstants.Enable;
            twdCounterInfo.Comments = string.Empty;
            twdCounterInfo.ValidFlag = true;
            twdCounterInfo.CreateDate = DateTime.Now;
            twdCounterInfo.CreateUser = loginUser;
            return twdCounterInfo;
        }
        /// <summary>
        /// MaintainInhouseLogisticStandardInfo -> TwdCounterInfo
        /// </summary>
        /// <param name="maintainInhouseLogisticStandardInfo"></param>
        /// <param name="twdCounterInfo"></param>
        public static void GetTwdCounterInfo(MaintainInhouseLogisticStandardInfo maintainInhouseLogisticStandardInfo, ref TwdCounterInfo twdCounterInfo)
        {
            if (maintainInhouseLogisticStandardInfo == null) return;
            twdCounterInfo.PartPullFid = maintainInhouseLogisticStandardInfo.Fid;
            twdCounterInfo.PartBoxCode = maintainInhouseLogisticStandardInfo.InhousePartClass;
            twdCounterInfo.Plant = maintainInhouseLogisticStandardInfo.Plant;
            twdCounterInfo.PlantZone = maintainInhouseLogisticStandardInfo.PlantZone;
            twdCounterInfo.Workshop = maintainInhouseLogisticStandardInfo.Workshop;
            twdCounterInfo.AssemblyLine = maintainInhouseLogisticStandardInfo.AssemblyLine;
            twdCounterInfo.SupplierNum = maintainInhouseLogisticStandardInfo.SupplierNum;
            twdCounterInfo.PartNo = maintainInhouseLogisticStandardInfo.PartNo;
            twdCounterInfo.PartCname = maintainInhouseLogisticStandardInfo.PartCname;
            twdCounterInfo.WorkshopSection = maintainInhouseLogisticStandardInfo.WorkshopSection;
            twdCounterInfo.Location = maintainInhouseLogisticStandardInfo.Location;
            twdCounterInfo.Package = maintainInhouseLogisticStandardInfo.InboundPackage;
            twdCounterInfo.PackageModel = maintainInhouseLogisticStandardInfo.InboundPackageModel;
        }
        /// <summary>
        /// TwdPartBoxInfo -> TwdCounterInfo
        /// </summary>
        /// <param name="twdPartBoxInfo"></param>
        /// <param name="twdCounterInfo"></param>
        public static void GetTwdCounterInfo(TwdPartBoxInfo twdPartBoxInfo, ref TwdCounterInfo twdCounterInfo)
        {
            if (twdPartBoxInfo == null) return;
            twdCounterInfo.RequirementAccumulateMode = twdPartBoxInfo.RequirementAccumulateMode;
            twdCounterInfo.RoundnessMode = twdPartBoxInfo.RoundnessMode;
        }
        #endregion







        /// <summary>
        /// 层级拉动
        /// </summary>
        /// <param name="maintainInhouseLogisticStandardInfo"></param>
        /// <param name="requireQty"></param>
        /// <param name="loginUser"></param>
        /// <param name="counterFid"></param>
        /// <param name="partBoxCode"></param>
        /// <returns></returns>
        public static string LevelPullCounter(MaintainInhouseLogisticStandardInfo maintainInhouseLogisticStandardInfo, decimal requireQty, string loginUser, Guid counterFid, TwdPartBoxInfo twdPartBoxInfo)
        {            
            ///未能成功获取零件类信息
            if (twdPartBoxInfo == null)
                return string.Empty;
            ///零件类未启用
            if (twdPartBoxInfo.Status.GetValueOrDefault() != (int)BasicDataStatusConstants.Enable)
                return string.Empty;
            ///根据物料拉动信息外键获取计数器，未能成功获取时需要创建
            TwdCounterInfo twdCounterInfo = new TwdCounterDAL().GetInfoByPartPullFid(maintainInhouseLogisticStandardInfo.Fid.GetValueOrDefault());
            if (twdCounterInfo == null)
            {
                ///创建计数器
                twdCounterInfo = CreateTwdCounterInfo(loginUser);
                ///以物料拉动信息填充计数器
                GetTwdCounterInfo(maintainInhouseLogisticStandardInfo, ref twdCounterInfo);
                ///以零件类信息填充计数器
                GetTwdCounterInfo(twdPartBoxInfo, ref twdCounterInfo);
                ///
                twdCounterInfo.Id = new TwdCounterDAL().Add(twdCounterInfo);
                if (twdCounterInfo.Id == 0)
                    throw new Exception("MC:0x00000453");///时间窗计数器创建失败
            }
            ///计数器状态未处于启用
            if (twdCounterInfo.Status != (int)BasicDataStatusConstants.Enable) return string.Empty;
            ///
            StringBuilder stringBuilder = new StringBuilder();
            ///
            stringBuilder.AppendLine(UpdateTwdCounter(maintainInhouseLogisticStandardInfo, twdPartBoxInfo, requireQty, twdCounterInfo.Id, loginUser));
            ///创建计数器日志
            TwdCounterLogInfo twdCounterLogInfo = TwdCounterLogBLL.CreateTwdCounterLogInfo(twdCounterInfo.Fid.GetValueOrDefault(), loginUser);
            ///以物料拉动信息填充计数器日志
            TwdCounterLogBLL.GetTwdCounterLogInfo(maintainInhouseLogisticStandardInfo, ref twdCounterLogInfo);
            ///以零件类信息填充计数器日志
            TwdCounterLogBLL.GetTwdCounterLogInfo(twdPartBoxInfo, ref twdCounterLogInfo);
            ///PART_QTY 
            twdCounterLogInfo.PartQty = requireQty;
            ///SOURCE_DATA_FID 
            twdCounterLogInfo.SourceDataFid = counterFid;
            ///SOURCE_DATA_TYPE 
            twdCounterLogInfo.SourceDataType = (int)TwdCounterSourceDataTypeConstants.Calculator;
            ///SOURCE_DATA 
            twdCounterLogInfo.SourceData = twdPartBoxInfo.PartBoxCode;
            ///
            stringBuilder.AppendLine(TwdCounterLogDAL.GetInsertSql(twdCounterLogInfo));
            ///触发层级拉动
            stringBuilder.AppendLine(LevelPullCounter(
                maintainInhouseLogisticStandardInfo,
                requireQty,
                loginUser,
                twdCounterInfo.Fid.GetValueOrDefault(),
                twdPartBoxInfo));
            return stringBuilder.ToString();
        }
    }
}

