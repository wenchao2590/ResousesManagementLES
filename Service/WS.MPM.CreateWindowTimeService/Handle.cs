namespace WS.MPM.CreateWindowTimeService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DM.LES;
    using BLL.LES;
    using System.Transactions;
    using BLL.SYS;
    using System;
    using DM.SYS;
    /// <summary>
    /// Handle
    /// </summary>
    public class Handle
    {
        #region 全局变量
        /// <summary>
        /// 登录用户
        /// </summary>
        private string loginUser = "CreateWindowTimeService";
        #endregion

        #region Handler
        /// <summary>
        /// Handler
        /// </summary>
        public void Handler()
        {
            TWDHandler();
            PLANHandler();
        }
        /// <summary>
        /// 生成TWD拉动窗口时间
        /// </summary>
        private void TWDHandler()
        {
            ///获取所有已启用的零件类，按零件类逐个进行逻辑处理
            List<TwdPartBoxInfo> twdPartBoxInfos = new TwdPartBoxBLL().GetList("[STATUS] = " + (int)BasicDataStatusConstants.Enable + "", "[ID]");
            if (twdPartBoxInfos.Count == 0) return;
            ///TWD窗口时间自动生成延后天数
            string twdWindowTimeAutoCreatePostponeDays = new ConfigBLL().GetValueByCode("TWD_WINDOW_TIME_AUTO_CREATE_POSTPONE_DAYS");
            if (!int.TryParse(twdWindowTimeAutoCreatePostponeDays, out int intTwdWindowTimeAutoCreatePostponeDays))
                intTwdWindowTimeAutoCreatePostponeDays = 3;///默认为3天

            ///TWD窗口时间参考前提天数
            string twdWindowTimeReferenceAdvanceDays = new ConfigBLL().GetValueByCode("TWD_WINDOW_TIME_REFERENCE_ADVANCE_DAYS");
            if (!int.TryParse(twdWindowTimeReferenceAdvanceDays, out int intTwdWindowTimeReferenceAdvanceDays))
                intTwdWindowTimeReferenceAdvanceDays = 2;///默认为2天

            ///当前时间
            DateTime currentDay = DateTime.Now.Date;
            StringBuilder sb = new StringBuilder();
            ///从提前日期一直获取到延后日期
            List<TwdWindowTimeInfo> twdWindowTimeInfos = new TwdWindowTimeBLL().GetList("" +
                "[WORK_DAY] >= N'" + currentDay.AddDays(0 - intTwdWindowTimeReferenceAdvanceDays) + "' and " +
                "[WORK_DAY] <= N'" + currentDay.AddDays(intTwdWindowTimeAutoCreatePostponeDays) + "'", string.Empty);
            if (twdWindowTimeInfos.Count == 0) return;
            foreach (TwdPartBoxInfo twdPartBoxInfo in twdPartBoxInfos)
            {
                List<TwdWindowTimeInfo> twdWindowTimes = new List<TwdWindowTimeInfo>();
                for (int j = intTwdWindowTimeReferenceAdvanceDays; j > 0; j--)
                {
                    DateTime workDay = currentDay.AddDays(0 - j);
                    twdWindowTimes = twdWindowTimeInfos.Where(d => d.PartBoxFid.GetValueOrDefault() == twdPartBoxInfo.Fid.GetValueOrDefault() && d.WorkDay.GetValueOrDefault() == workDay).ToList();
                    if (twdWindowTimes.Count > 0) break;
                }
                if (twdWindowTimes.Count == 0) continue;
                foreach (TwdWindowTimeInfo twdWindowTime in twdWindowTimes)
                {
                    for (int i = 0; i < intTwdWindowTimeAutoCreatePostponeDays; i++)
                    {
                        ///累计时间
                        DateTime createDay = currentDay.AddDays(i);
                        List<TwdWindowTimeInfo> windowTimeInfos = twdWindowTimeInfos.Where(d => d.PartBoxFid.GetValueOrDefault() == twdPartBoxInfo.Fid.GetValueOrDefault() && d.WorkDay.GetValueOrDefault() == createDay).ToList();
                        if (windowTimeInfos.Count > 0) continue;
                        ///提前时间
                        int advanceTime =
                        twdPartBoxInfo.RequirementAccumulateTime.GetValueOrDefault() +///需求累积时间
                            twdPartBoxInfo.LoadTime.GetValueOrDefault() +///装货时间
                            twdPartBoxInfo.TransportTime.GetValueOrDefault() +///运输时间
                            twdPartBoxInfo.UnloadTime.GetValueOrDefault();///卸货时间

                        ///历史窗口时间
                        DateTime windowTime = twdWindowTime.WindowTime.GetValueOrDefault();
                        ///现窗口时间
                        DateTime createWindowTime = new DateTime(createDay.Year, createDay.Month, createDay.Day, windowTime.Hour, windowTime.Minute, 0);
                        DateTime createSendTime = createWindowTime.AddMinutes(0 - advanceTime);
                        #region TT_MPM_TWD_WINDOW_TIME
                        sb.Append("insert into [LES].[TT_MPM_TWD_WINDOW_TIME] (" +
                            "[FID]," +
                            "[PART_BOX_FID]," +
                            "[PART_BOX_CODE]," +
                            "[PART_BOX_NAME]," +
                            "[PLANT]," +
                            "[WORKSHOP]," +
                            "[ASSEMBLY_LINE]," +
                            "[SUPPLIER_NUM]," +
                            "[WORK_DAY]," +
                            "[SEND_TIME]," +
                            "[WINDOW_TIME]," +
                            "[SEND_TIME_STATUS]," +
                            "[TIME_ZONE]," +
                            "[COMMENTS]," +
                            "[VALID_FLAG]," +
                            "[CREATE_DATE]," +
                            "[CREATE_USER]) values (" +
                            "NEWID()," +
                            "N'" + twdWindowTime.PartBoxFid.GetValueOrDefault() + "'," +
                            "N'" + twdWindowTime.PartBoxCode + "'," +
                            "N'" + twdPartBoxInfo.PartBoxName + "'," +
                            "N'" + twdPartBoxInfo.Plant + "'," +
                            "N'" + twdPartBoxInfo.Workshop + "'," +
                            "N'" + twdPartBoxInfo.AssemblyLine + "'," +
                            "N'" + twdPartBoxInfo.SupplierNum + "'," +
                            "N'" + createDay + "'," +
                            "N'" + createSendTime + "'," +
                            "N'" + createWindowTime + "'," +
                            "" + (int)SendTimeStatusConstants.NoSend + "," +
                            "N'" + twdWindowTime.TimeZone + "'," +
                            "NULL," +
                            "1," +
                            "GETDATE()," +
                            "N'" + loginUser + "');");
                        #endregion
                    }
                }
            }
            ///执行
            using (TransactionScope trans = new TransactionScope())
            {
                if (sb.Length > 0)
                    BLL.LES.CommonBLL.ExecuteNonQueryBySql(sb.ToString());
                trans.Complete();
            }
        }
        /// <summary>
        /// 生成计划拉动窗口时间
        /// </summary>
        private void PLANHandler()
        {
            ///获取所有已启用的零件类，按零件类逐个进行逻辑处理
            List<PlanPartBoxInfo> planPartBoxInfos = new PlanPartBoxBLL().GetList("[STATUS] = " + (int)BasicDataStatusConstants.Enable + "", "[ID]");
            if (planPartBoxInfos.Count == 0) return;
            ///计划窗口时间自动生成延后天数
            string planWindowTimeAutoCreatePostponeDays = new ConfigBLL().GetValueByCode("PLAN_WINDOW_TIME_AUTO_CREATE_POSTPONE_DAYS");
            if (!int.TryParse(planWindowTimeAutoCreatePostponeDays, out int intPlanWindowTimeAutoCreatePostponeDays))
                intPlanWindowTimeAutoCreatePostponeDays = 3;///默认为3天

            ///TWD窗口时间参考前提天数
            string planWindowTimeReferenceAdvanceDays = new ConfigBLL().GetValueByCode("PLAN_WINDOW_TIME_REFERENCE_ADVANCE_DAYS");
            if (!int.TryParse(planWindowTimeReferenceAdvanceDays, out int intPlanWindowTimeReferenceAdvanceDays))
                intPlanWindowTimeReferenceAdvanceDays = 2;///默认为2天

            ///当前时间
            DateTime currentDay = DateTime.Now.Date;
            StringBuilder sb = new StringBuilder();
            ///从提前日期一直获取到延后日期
            List<PlanWindowTimeInfo> planWindowTimeInfos = new PlanWindowTimeBLL().GetList("" +
                "[WORK_DAY] >= N'" + currentDay.AddDays(0 - intPlanWindowTimeReferenceAdvanceDays) + "' and " +
                "[WORK_DAY] <= N'" + currentDay.AddDays(intPlanWindowTimeAutoCreatePostponeDays) + "'", string.Empty);
            if (planWindowTimeInfos.Count == 0) return;
            foreach (PlanPartBoxInfo planPartBoxInfo in planPartBoxInfos)
            {
                List<PlanWindowTimeInfo> planWindowTimes = new List<PlanWindowTimeInfo>();
                for (int j = intPlanWindowTimeReferenceAdvanceDays; j > 0; j--)
                {
                    DateTime workDay = currentDay.AddDays(0 - j);
                    planWindowTimes = planWindowTimeInfos.Where(d => d.PartBoxFid.GetValueOrDefault() == planPartBoxInfo.Fid.GetValueOrDefault() && d.WorkDay.GetValueOrDefault() == workDay).ToList();
                    if (planWindowTimes.Count > 0) break;
                }
                if (planWindowTimes.Count == 0) continue;
                foreach (PlanWindowTimeInfo planWindowTime in planWindowTimes)
                {
                    for (int i = 0; i < intPlanWindowTimeAutoCreatePostponeDays; i++)
                    {
                        ///累计时间
                        DateTime createDay = currentDay.AddDays(i);
                        List<PlanWindowTimeInfo> windowTimeInfos = planWindowTimeInfos.Where(d => d.PartBoxFid.GetValueOrDefault() == planPartBoxInfo.Fid.GetValueOrDefault() && d.WorkDay.GetValueOrDefault() == createDay).ToList();
                        if (windowTimeInfos.Count > 0) continue;
                        ///提前时间
                        int advanceTime = planPartBoxInfo.PickUpTime.GetValueOrDefault() +///拣料时间
                            planPartBoxInfo.DeliveryTime.GetValueOrDefault() +///配送时间
                            planPartBoxInfo.DelayTime.GetValueOrDefault();///延迟时间

                        ///历史窗口时间
                        DateTime windowTime = planWindowTime.WindowTime.GetValueOrDefault();
                        ///现窗口时间
                        DateTime createWindowTime = new DateTime(createDay.Year, createDay.Month, createDay.Day, windowTime.Hour, windowTime.Minute, 0);
                        DateTime createSendTime = createWindowTime.AddMinutes(0 - advanceTime);
                        #region TT_MPM_PLAN_WINDOW_TIME
                        sb.Append("insert into [LES].[TT_MPM_PLAN_WINDOW_TIME] (" +
                            "[FID]," +
                            "[PART_BOX_FID]," +
                            "[PART_BOX_CODE]," +
                            "[PART_BOX_NAME]," +
                            "[PLANT]," +
                            "[WORKSHOP]," +
                            "[ASSEMBLY_LINE]," +
                            "[SUPPLIER_NUM]," +
                            "[WORK_DAY]," +
                            "[SEND_TIME]," +
                            "[WINDOW_TIME]," +
                            "[SEND_TIME_STATUS]," +
                            "[TIME_ZONE]," +
                            "[COMMENTS]," +
                            "[VALID_FLAG]," +
                            "[CREATE_DATE]," +
                            "[CREATE_USER]) values (" +
                            "NEWID()," +
                            "N'" + planWindowTime.PartBoxFid.GetValueOrDefault() + "'," +
                            "N'" + planWindowTime.PartBoxCode + "'," +
                            "N'" + planPartBoxInfo.PartBoxName + "'," +
                            "N'" + planPartBoxInfo.Plant + "'," +
                            "N'" + planPartBoxInfo.Workshop + "'," +
                            "N'" + planPartBoxInfo.AssemblyLine + "'," +
                            "N'" + planPartBoxInfo.SupplierNum + "'," +
                            "N'" + createDay + "'," +
                            "N'" + createSendTime + "'," +
                            "N'" + createWindowTime + "'," +
                            "" + (int)SendTimeStatusConstants.NoSend + "," +
                            "N'" + planWindowTime.TimeZone + "'," +
                            "NULL," +
                            "1," +
                            "GETDATE()," +
                            "N'" + loginUser + "');");
                        #endregion
                    }
                }
            }
            ///执行
            using (TransactionScope trans = new TransactionScope())
            {
                if (sb.Length > 0)
                    BLL.LES.CommonBLL.ExecuteNonQueryBySql(sb.ToString());
                trans.Complete();
            }
        }
        #endregion
    }
}