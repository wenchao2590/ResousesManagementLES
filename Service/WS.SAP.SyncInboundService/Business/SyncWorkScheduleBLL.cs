namespace WS.SAP.SyncInboundService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Transactions;
    using BLL.LES;
    using DM.LES;
    using DM.SYS;

    using Infrustructure.Logging;
    using Infrustructure.Utilities;

    /// <summary>
    /// 同步工作日历
    /// </summary>
    public class SyncWorkScheduleBLL
    {

        /// <summary>
        /// SyncWorkSchedule
        /// </summary>
        /// <param name="loginUser"></param>
        public static void Sync(string loginUser)
        {
            List<SapWorkCalendarInfo> sapWorkCalendarInfos = new SapWorkCalendarBLL().GetListByPage("" +
                "[PROCESS_FLAG] in (" + (int)ProcessFlagConstants.Untreated + "," + (int)ProcessFlagConstants.Resend + ")", "[ID]", 1, 1000, out int dataCnt);
            if (dataCnt == 0) return;
            ///获取业务表中需要修改的数据
            List<WorkScheduleInfo> workScheduleInfos = new WorkScheduleBLL().GetListForInterfaceDataSync(sapWorkCalendarInfos.Select(d => d.ProductionDate.GetValueOrDefault()).ToList());

            StringBuilder stringBuilder = new StringBuilder();
            ///获取工厂信息,准备对比
            List<PlantInfo> plantInfos = new PlantBLL().GetListForInterfaceDataSync();
            ///获取车间信息,准备对比
            List<WorkshopInfo> workshopInfos = new WorkshopBLL().GetListForInterfaceDataSync();
            ///获取生产线信息,准备对比
            List<AssemblyLineInfo> assemblyLineInfos = new AssemblyLineBLL().GetListForInterfaceDataSync();
            ///已处理完成的ID
            List<long> dealedIds = new List<long>();
            ///逐条处理中间表数据
            foreach (var sapWorkCalendarInfo in sapWorkCalendarInfos)
            {
                ///处理工厂不对等的数据
                PlantInfo plantInfo = plantInfos.FirstOrDefault(d => d.SapPlantCode == sapWorkCalendarInfo.Dwerk);
                if (plantInfo == null)
                {
                    ///将这样的数据更新为挂起状态
                    stringBuilder.AppendLine("update [LES].[TI_IFM_SAP_WORK_CALENDAR] " +
                        "set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'3x00000019'," +///工厂信息不存在
                        "[MODIFY_USER] = N'" + loginUser + "'," +
                        "[MODIFY_DATE] = GETDATE() " +
                        "where [ID] = " + sapWorkCalendarInfo.Id + ";");
                    continue;
                }
                ///处理车间不对等的数据
                WorkshopInfo workshopInfo = workshopInfos.FirstOrDefault(d => d.Plant == plantInfo.Plant && d.Workshop == sapWorkCalendarInfo.Zcj);
                if (workshopInfo == null)
                {
                    ///将这样的数据更新为挂起状态
                    stringBuilder.AppendLine("update [LES].[TI_IFM_SAP_WORK_CALENDAR] " +
                        "set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'7x00000015'," +///车间信息不存在
                        "[MODIFY_USER] = N'" + loginUser + "'," +
                        "[MODIFY_DATE] = GETDATE() " +
                        "where [ID] = " + sapWorkCalendarInfo.Id + ";");
                    continue;
                }
                ///处理生产线不对等的数据
                AssemblyLineInfo assemblyLineInfo = assemblyLineInfos.FirstOrDefault(d =>
                d.Plant == plantInfo.Plant &&
                d.Workshop == workshopInfo.Workshop &&
                d.AssemblyLine == sapWorkCalendarInfo.LineNo);
                if (assemblyLineInfo == null)
                {
                    ///将这样的数据更新为挂起状态
                    stringBuilder.AppendLine("update [LES].[TI_IFM_SAP_WORK_CALENDAR] " +
                        "set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                        "[PROCESS_TIME] = GETDATE()," +
                        "[COMMENTS] = N'7x00000016'," +///流水线信息不存在
                        "[MODIFY_USER] = N'" + loginUser + "'," +
                        "[MODIFY_DATE] = GETDATE() " +
                        "where [ID] = " + sapWorkCalendarInfo.Id + ";");
                    continue;
                }
                ///当前业务数据表中无此工厂代码+车间+生产线+日期+班次  即为新增
                ///TODO: SAP班次的枚举？
                int lesShift = Convert.ToInt32(sapWorkCalendarInfo.Shift);
                ///
                WorkScheduleInfo workScheduleInfo = workScheduleInfos.FirstOrDefault(d =>
                d.Plant == plantInfo.Plant &&
                d.Workshop == sapWorkCalendarInfo.Zcj &&
                d.AssemblyLine == sapWorkCalendarInfo.LineNo &&
                d.Date == sapWorkCalendarInfo.ProductionDate &&
                d.Shift == lesShift);

                ///如果为空 进行新增
                if (workScheduleInfo == null)
                {
                    /// 工厂, 日期,班次是必填项
                    if (string.IsNullOrEmpty(sapWorkCalendarInfo.Dwerk) || sapWorkCalendarInfo.ProductionDate == null || string.IsNullOrEmpty(sapWorkCalendarInfo.Shift))
                    {
                        ///将这样的数据更新为挂起状态
                        stringBuilder.Append("update [LES].[TI_IFM_SAP_WORK_CALENDAR] " +
                            "set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Suspend + "," +
                            "[PROCESS_TIME] = GETDATE()," +
                            "[COMMENTS] = N'7x00000014'," +///工厂, 日期,班次是必填项
                            "[MODIFY_USER] = N'" + loginUser + "'," +
                            "[MODIFY_DATE] = GETDATE() " +
                            "where [ID] = " + sapWorkCalendarInfo.Id.ToString() + ";");
                        continue;
                    }
                    /// 新增
                    stringBuilder.AppendFormat("insert into [LES].[TM_BAS_WORK_SCHEDULE] ("
                     + "[FID]"
                     + "[PLANT] ,"
                     + "[WORKSHOP] ,"
                     + "[ASSEMBLY_LINE] ,"
                     + "[DATE] ,"
                     + "[SHIFT] ,"
                     + "[BEGIN_TIME] ,"
                     + "[END_TIME] ,"
                     + "[VALID_FLAG] ,"
                     + "[CREATE_USER] ,"
                     + "[CREATE_DATE]) values ("
                     + "N'{0}' ," /// 工厂 - nvarchar(8)
                     + "N'{1}' ," /// 车间 - nvarchar(32)
                     + "N'{2}' ," /// 生产线 - nvarchar(32)
                     + "N'{3}' ," /// 日期 - datetime          
                     + "{4} ," /// 班次 - int
                     + "N'{5}' ," /// 开始时间 - datetime
                     + "N'{6}' ," /// 结束时间 - datetime                  
                     + "{7} ," /// VALID_FLAG - bit
                     + "N'{8}' ," /// CREATE_USER - nvarchar(32)
                     + "GETDATE() ) ;", /// CREATE_DATE - datetime
                     plantInfo.Plant,/// 工厂 - nvarchar(8),0
                     workshopInfo.Workshop,/// 车间 - nvarchar(32),1
                     assemblyLineInfo.AssemblyLine,/// 生产线 - nvarchar(32),2
                     sapWorkCalendarInfo.ProductionDate,/// 日期 - datetime,3
                     lesShift,  /// 班次 - int,4，
                     sapWorkCalendarInfo.BeginTime,/// 开始时间 - datetime,5
                     sapWorkCalendarInfo.EndTime,/// 结束时间 - datetime,6
                     1, /// VALID_FLAG - bit,7
                     loginUser);/// CREATE_USER - nvarchar(32),8

                    workScheduleInfos.Add(new WorkScheduleInfo()
                    {
                        Plant = plantInfo.Plant,
                        Workshop = sapWorkCalendarInfo.Zcj,
                        AssemblyLine = sapWorkCalendarInfo.LineNo,
                        Date = sapWorkCalendarInfo.ProductionDate,
                        Shift = lesShift
                    });
                }
                else
                {
                    ///此次循环的数据在业务表中存在,进行修改
                    stringBuilder.AppendLine("update [LES].[TM_BAS_WORK_SCHEDULE] " +
                        "set [BEGIN_TIME] = N'" + workScheduleInfo.BeginTime.GetValueOrDefault() + "'," +
                        "[END_TIME] =N'" + workScheduleInfo.EndTime.GetValueOrDefault() + "'," +
                        "[MODIFY_USER] = N'" + loginUser + "'," +
                        "[MODIFY_DATE] = GETDATE() " +
                        "where [PLANT] = N'" + plantInfo.Plant + "' and " +
                       "[WORKSHOP] = N'" + workshopInfo.Workshop + "' and " +
                       "[ASSEMBLY_LINE] = N'" + assemblyLineInfo.AssemblyLine + "' and " +
                       "[DATE] = N'" + sapWorkCalendarInfo.ProductionDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' and " +
                       "[SHIFT] = N'" + lesShift + "';");

                }
                dealedIds.Add(sapWorkCalendarInfo.Id);
            }
            if (dealedIds.Count > 0)
                ///已处理的中间表数据更新为已处理状态
                stringBuilder.Append("update [LES].[TI_IFM_SAP_WORK_CALENDAR] " +
                    "set [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Processed + "," +
                    "[PROCESS_TIME] = GETDATE()," +
                    "[COMMENTS] = NULL," +
                    "[MODIFY_USER] = N'" + loginUser + "'," +
                    "[MODIFY_DATE] = GETDATE() " +
                    "where [ID] in (" + string.Join(",", dealedIds.ToArray()) + ");");

            using (var trans = new TransactionScope())
            {
                if (stringBuilder.Length > 0)
                    BLL.SYS.CommonBLL.ExecuteNonQueryBySql(stringBuilder.ToString());
                trans.Complete();
            }
        }

    }
}
