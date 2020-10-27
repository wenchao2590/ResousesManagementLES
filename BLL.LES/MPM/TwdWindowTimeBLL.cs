namespace BLL.LES
{
    using DAL.LES;
    using DAL.SYS;
    using DM.LES;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TwdWindowTimeBLL
    /// </summary>
    public class TwdWindowTimeBLL
    {
        #region Common
        /// <summary>
        /// TwdWindowTimeDAL
        /// </summary>
        TwdWindowTimeDAL dal = new TwdWindowTimeDAL();
        /// <summary>
        /// 分页获取集合
        /// </summary>
        /// <param name="textWhere">string 条件语句,无须where</param>
        /// <param name="textOrder">string 排序语句,无须order by</param>
        /// <param name="pageIndex">int 页码,从1开始</param>
        /// <param name="pageRow">int 每页行数</param>
        /// <param name="dataCount">out int 数据行数</param>
        /// <returns>List<TwdWindowTimeInfo></returns>
        public List<TwdWindowTimeInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// SelectInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TwdWindowTimeInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// InsertInfo
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public long InsertInfo(TwdWindowTimeInfo info)
        {
            if (string.IsNullOrEmpty(info.PartBoxCode))
                throw new Exception("MC:0x00000298");///零件类代码不可为空

            if (string.IsNullOrEmpty(info.PartBoxName))
                throw new Exception("MC:0x00000299");///零件类名称不可为空

            ///窗口时间
            DateTime windowTime = info.WindowTime.GetValueOrDefault();
            DateTime workDay = info.WorkDay.GetValueOrDefault();
            info.WindowTime = new DateTime(workDay.Year, workDay.Month, workDay.Day, windowTime.Hour, windowTime.Minute, 0);
            int cnt = dal.GetCounts("[PART_BOX_CODE] = N'" + info.PartBoxCode + "' and [WINDOW_TIME] = N'" + info.WindowTime.GetValueOrDefault() + "'");
            if (cnt > 0)
                throw new Exception("MC:0x00000313");///相同零件类代码②、工作日⑧、窗口时间⑩的数据不允许重复

            TwdPartBoxInfo twdPartBoxInfo = new TwdPartBoxDAL().GetInfo(info.PartBoxCode);
            if (twdPartBoxInfo == null)
                throw new Exception("MC:0x00000225");///拉动零件类数据错误
            info.PartBoxFid = twdPartBoxInfo.Fid;

            if (info.WindowTime == null)
                throw new Exception("MC:0x00000508");///窗口时间不允许为空

            if (info.WorkDay == null)
                throw new Exception("MC:0x00000509");///工作日不允许为空

            ///发单时间 = 工作日年月日 + 窗口时间时分秒 - 提前时间
            int advanceTime = twdPartBoxInfo.RequirementAccumulateTime.GetValueOrDefault() +///累积时间
                                        twdPartBoxInfo.LoadTime.GetValueOrDefault() +///装货时间
                                        twdPartBoxInfo.TransportTime.GetValueOrDefault() +///运输时间
                                        twdPartBoxInfo.UnloadTime.GetValueOrDefault();///卸货时间
            info.SendTime = info.WindowTime.GetValueOrDefault().AddMinutes(0 - advanceTime);
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
            TwdWindowTimeInfo info = dal.GetInfo(id);
            if (info == null)
                throw new Exception("MC:0x00000084");///数据错误
            if (info.SendTimeStatus != (int)SendTimeStatusConstants.NoSend)
                throw new Exception("MC:0x00000668");///发单状态为未发单才允许被删除
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
            TwdWindowTimeInfo info = dal.GetInfo(id);
            if (info == null)
                throw new Exception("MC:0x00000084");///数据错误

            if (info.SendTimeStatus.GetValueOrDefault() != (int)SendTimeStatusConstants.NoSend)
                throw new Exception("MC:0x00000311");///发单状态为未发单才允许被修改

            string partBoxCode = CommonBLL.GetFieldValue(fields, "PART_BOX_CODE");
            string workDay = CommonBLL.GetFieldValue(fields, "WORK_DAY");
            string windowTime = CommonBLL.GetFieldValue(fields, "WINDOW_TIME");
            ///窗口时间
            DateTime dateWindowTime = CommonBLL.TryParseDatetime(windowTime, "yyyy-MM-dd HH:mm:ss");
            DateTime dateWorkDay = CommonBLL.TryParseDatetime(workDay, "yyyy-MM-dd HH:mm:ss");
            dateWindowTime = new DateTime(dateWorkDay.Year, dateWorkDay.Month, dateWorkDay.Day, dateWindowTime.Hour, dateWindowTime.Minute, 0);
            int cnt = dal.GetCounts("[PART_BOX_CODE] = N'" + info.PartBoxCode + "' and [WINDOW_TIME] = N'" + info.WindowTime.GetValueOrDefault() + "' and [ID] <> " + id + "");
            if (cnt > 0)
                throw new Exception("MC:0x00000313");///相同零件类代码②、工作日⑧、窗口时间⑩的数据不允许重复
            fields = CommonBLL.SetFieldValue(fields, "WINDOW_TIME", dateWindowTime.ToString("yyyy-MM-dd HH:mm:ss"));

            TwdPartBoxInfo twdPartBoxInfo = new TwdPartBoxDAL().GetInfo(partBoxCode);
            if (twdPartBoxInfo == null)
                throw new Exception("MC:0x00000225");///拉动零件类数据错误

            ///发单时间 = 工作日年月日 + 窗口时间时分秒 - 提前时间
            int advanceTime = twdPartBoxInfo.RequirementAccumulateTime.GetValueOrDefault() +///累积时间
                                        twdPartBoxInfo.LoadTime.GetValueOrDefault() +///装货时间
                                        twdPartBoxInfo.TransportTime.GetValueOrDefault() +///运输时间
                                        twdPartBoxInfo.UnloadTime.GetValueOrDefault();///卸货时间
            DateTime sendTime = dateWindowTime.AddMinutes(0 - advanceTime);
            fields = CommonBLL.SetFieldValue(fields, "SEND_TIME", sendTime.ToString("yyyy-MM-dd HH:mm:ss"));

            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="orderText"></param>
        /// <returns></returns>
        public List<TwdWindowTimeInfo> GetList(string textWhere, string orderText)
        {
            return dal.GetList(textWhere, orderText);
        }
        #endregion
        /// <summary>
        /// 执行导入EXCEL数据
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        public bool ImportDataByExcel(DataTable dataTable, Dictionary<string, string> fieldNames, string loginUser)
        {
            DateTime dtDate;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (!DateTime.TryParse(dataTable.Rows[i]["WorkDay"].ToString(), out dtDate))
                    throw new Exception("MC:0x00000393");///日期格式不正确

                if (!DateTime.TryParse(dataTable.Rows[i]["WindowTime"].ToString(), out dtDate))
                    throw new Exception("MC:0x00000393");///日期格式不正确
            }

            ///导入数据
            List<TwdWindowTimeInfo> twdWindows = CommonDAL.DatatableConvertToList<TwdWindowTimeInfo>(dataTable).ToList();
            if (twdWindows.Count == 0)
                throw new Exception("MC:1x00000043");///数据格式不符合导入规范


            ///比对数据
            List<TwdWindowTimeInfo> timeInfos = dal.GetList("[PART_BOX_CODE] in ('" + string.Join("','", twdWindows.Select(d => d.PartBoxCode).ToArray()) + "') and [WORK_DAY] in ('" + string.Join("','", twdWindows.Select(d => d.WorkDay).ToArray()) + "') and [WINDOW_TIME] in ('" + string.Join("','", twdWindows.Select(d => d.WindowTime).ToArray()) + "')", string.Empty).ToList();
            ///零件类数据
            List<TwdPartBoxInfo> twdParts = new TwdPartBoxDAL().GetList("[PART_BOX_CODE] in ('" + string.Join("','", twdWindows.Select(d => d.PartBoxCode).ToArray()) + "')", string.Empty).ToList();
            if (twdParts.Count() != twdWindows.Count())
                throw new Exception("MC:0x00000225");///拉动零件类数据错误


            List<string> fields = new List<string>(fieldNames.Keys);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var item in twdWindows)
            {
                TwdWindowTimeInfo timeInfo = timeInfos.FirstOrDefault(d => d.PartBoxCode == item.PartBoxCode && d.WorkDay == item.WorkDay && d.WindowTime == item.WindowTime);

                TwdPartBoxInfo boxInfo = twdParts.FirstOrDefault(d => d.PartBoxCode == item.PartBoxCode);
                ///发单时间 = 工作日年月日 + 窗口时间时分秒 - 提前时间
                int advanceTime = boxInfo.RequirementAccumulateTime.GetValueOrDefault() +///需求累积时间
                                        boxInfo.LoadTime.GetValueOrDefault() +///装货时间
                                        boxInfo.TransportTime.GetValueOrDefault() +///运输时间
                                        boxInfo.UnloadTime.GetValueOrDefault();///卸货时间
                item.SendTime = item.WindowTime.GetValueOrDefault().AddMinutes(-advanceTime);
                if (timeInfo == null)
                {
                    if (item.WindowTime == null || item.WorkDay == null || item.PartBoxCode == null)
                        throw new Exception("MC:0x00000510");///零件类工作日窗口时间不可为空
                    stringBuilder.Append("insert into [LES].[TT_MPM_TWD_WINDOW_TIME]([FID],[PART_BOX_FID],[PART_BOX_CODE],[PART_BOX_NAME],[PLANT],[WORKSHOP],[ASSEMBLY_LINE],[SUPPLIER_NUM],[WORK_DAY],[SEND_TIME],[WINDOW_TIME],[SEND_TIME_STATUS],[TIME_ZONE],[COMMENTS],[VALID_FLAG],[CREATE_DATE],[CREATE_USER])values(");
                    stringBuilder.Append("newid(),N'" + boxInfo.Fid + "',N'" + item.PartBoxCode + "',N'" + boxInfo.PartBoxName + "',N'" + boxInfo.Plant + "',N'" + boxInfo.Workshop + "',N'" + boxInfo.AssemblyLine + "',N'" + boxInfo.SupplierNum + "',N'" + item.WorkDay + "',N'" + item.SendTime + "',N'" + item.WindowTime + "'," + (int)SendTimeStatusConstants.NoSend + ",N'" + item.TimeZone + "',N'" + item.Comments + "',1,GETDATE(),N'" + loginUser + "'");
                    stringBuilder.Append(");");
                    timeInfos.Add(item);
                    continue;
                }

                if (timeInfo.SendTimeStatus != (int)SendTimeStatusConstants.NoSend)
                    throw new Exception("MC:0x00000311");///发单状态⑪为10未发单才允许被修改

                if (item.WindowTime == null || item.WorkDay == null || item.PartBoxCode == null)
                    throw new Exception("MC:0x00000510");///零件类工作日窗口时间不可为空

                stringBuilder.Append("update [LES].[TT_MPM_TWD_WINDOW_TIME] set [PLANT] = N'" + boxInfo.Plant + "',[WORKSHOP] = N'" + boxInfo.Workshop + "',[ASSEMBLY_LINE] = N'" + boxInfo.AssemblyLine + "',[SUPPLIER_NUM] = N'" + boxInfo.SupplierNum + "',[SEND_TIME] = N'" + item.SendTime + "',[COMMENTS] = N'" + item.Comments + "',[MODIFY_DATE] = GETDATE(),[MODIFY_USER] = N'" + loginUser + "' where [ID] = " + timeInfo.Id + ";");
            }

            if (string.IsNullOrEmpty(stringBuilder.ToString()))
                throw new Exception("MC:0x00000283");///没有可导入更新的数据

            return CommonDAL.ExecuteNonQueryBySql(stringBuilder.ToString());
        }
    }
}

