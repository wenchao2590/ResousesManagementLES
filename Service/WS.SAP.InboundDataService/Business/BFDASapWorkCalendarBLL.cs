namespace WS.SAP.InboundDataService
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using DM.LES;
    using DM.SYS;
    using Infrustructure.Logging;
    using Infrustructure.Utilities;

    /// <summary>
    /// BFDASapWorkCalendarBLL
    /// </summary>
    public class BFDASapWorkCalendarBLL : IBusiness<SapWorkCalendarInfo, BFDASapWorkCalendarInfo>
    {
        #region Common
            /// <summary>
            /// 操作用户
            /// </summary>
            private string loginUser = "WS.SAP.InboundDataService";
            /// <summary>
            /// 是否写入日志
            /// </summary>
            string logFlag = ConfigurationManager.AppSettings["LogFlag"].ToLower();
            /// <summary>
            /// log日志名称
            /// </summary>
            string interfaceCode = "SAP-LES-005 SAP工作日历基础数据接收";
        #endregion


        /// <summary>
        /// 接口数据转中间表集合
        /// </summary>
        /// <param name="interfaceInfo"></param>
        /// <returns></returns>
        public SapWorkCalendarInfo ConversionToCentreInfo(BFDASapWorkCalendarInfo interfaceInfo)
        {
            SapWorkCalendarInfo sapWorkCalendarInfo = new SapWorkCalendarInfo();
            //if(string.IsNullOrEmpty(interfaceInfo.DWERK) || string.IsNullOrEmpty(interfaceInfo.ZCJ) || string.IsNullOrEmpty(interfaceInfo.LINENO) )
            //    throw new Exception("MC:0x00000468");///工厂, 车间, 生产线都不能为空
            sapWorkCalendarInfo.Dwerk = interfaceInfo.DWERK;///工厂
            sapWorkCalendarInfo.Zcj = interfaceInfo.ZCJ;///车间
            sapWorkCalendarInfo.LineNo = interfaceInfo.LINENO;///产线
            sapWorkCalendarInfo.ProductionDate = BFDASapCommonBLL.TryParseDatetime(interfaceInfo.PRODUCTIONDATE); ///工作时间
            sapWorkCalendarInfo.Shift = interfaceInfo.SHIFT;///班次 默认0,白班. 1夜班.
            sapWorkCalendarInfo.BeginTime = BFDASapCommonBLL.TryParseDatetime(interfaceInfo.PRODUCTIONDATE + interfaceInfo.BEGINTIME, "yyyyMMddHHmmss");///开始时间
            sapWorkCalendarInfo.EndTime = BFDASapCommonBLL.TryParseDatetime(interfaceInfo.PRODUCTIONDATE + interfaceInfo.ENDTIME, "yyyyMMddHHmmss");///结束时间
            sapWorkCalendarInfo.MinCapacity = interfaceInfo.MINCAPACITY;///最小产能
            sapWorkCalendarInfo.MaxCapacity = interfaceInfo.MAXCAPACITY;///最大产能
            sapWorkCalendarInfo.Version = interfaceInfo.VERSION;///版本
            return sapWorkCalendarInfo;
        }
        /// <summary>
        /// 批量接口数据转中间表集合
        /// </summary>
        /// <param name="interfaceList"></param>
        /// <returns></returns>
        public List<SapWorkCalendarInfo> ConversionToCentreList(List<BFDASapWorkCalendarInfo> interfaceList)
        {
            List<SapWorkCalendarInfo> list = new List<SapWorkCalendarInfo>();
            foreach (BFDASapWorkCalendarInfo interfaceInfo in interfaceList)
            {
                list.Add(ConversionToCentreInfo(interfaceInfo));
            }
            return list;
        }
        /// <summary>
        /// 关键字
        /// </summary>
        /// <param name="bFDASapWorkCalendarInfo"></param>
        /// <returns></returns>
        public string GetKeyValue(BFDASapWorkCalendarInfo bFDASapWorkCalendarInfo)
        {
            return bFDASapWorkCalendarInfo.DWERK + "|" + bFDASapWorkCalendarInfo.ZCJ + "|" + bFDASapWorkCalendarInfo.LINENO + "|" + bFDASapWorkCalendarInfo.PRODUCTIONDATE;
        }
        /// <summary>
        /// 批量关键字
        /// </summary>
        /// <param name="bFDASapWorkCalendarInfos"></param>
        /// <returns></returns>
        public string GetKeyValues(List<BFDASapWorkCalendarInfo> bFDASapWorkCalendarInfos)
        {
            return string.Join(",", bFDASapWorkCalendarInfos.Select(d => d.DWERK + "|" + d.ZCJ + "|" + d.LINENO + "|" + d.PRODUCTIONDATE).ToArray());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="interfaceInfo"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        public void InsertInfoToCentreTable(BFDASapWorkCalendarInfo interfaceInfo, Guid logFid, string logSql)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 中间集合添加到数据库中间表
        /// </summary>
        /// <param name="bFDASapWorkCalendarInfos"></param>
        /// <param name="logFid"></param>
        /// <returns></returns>
        public int InsertListToCentreTable(List<BFDASapWorkCalendarInfo> bFDASapWorkCalendarInfos, Guid logFid, string logSql)
        {
            List<SapWorkCalendarInfo> sapWorkCalendarInfos = ConversionToCentreList(bFDASapWorkCalendarInfos);
            StringBuilder sqlSb = new StringBuilder(logSql);
            foreach (SapWorkCalendarInfo sapWorkCalendarInfo in sapWorkCalendarInfos)
            {
                sqlSb.AppendFormat("insert into [LES].[TI_IFM_SAP_WORK_CALENDAR] ("
                + "[FID] ,"
                + "[LOG_FID] ,"
                + "[DWERK] ,"
                + "[ZCJ] ,"
                + "[LINE_NO] ,"
                + "[PRODUCTION_DATE] ,"
                + "[SHIFT] ,"
                + "[BEGIN_TIME] ,"
                + "[END_TIME] ,"
                + "[MIN_CAPACITY] ,"
                + "[MAX_CAPACITY] ,"
                + "[VERSION] ,"
                + "[PROCESS_FLAG] ,"
                + "[PROCESS_TIME] ,"
                + "[VALID_FLAG] ,"
                + "[CREATE_DATE] ,"
                + "[CREATE_USER])"
                + " values (NEWID(),'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',{8},{9},'{10}',{11},NULL,{12},GETDATE(),'{13}');",
                 logFid,///LOG_FID, 0
                 sapWorkCalendarInfo.Dwerk,///工厂 1
                 sapWorkCalendarInfo.Zcj,///车间代码 2 
                 sapWorkCalendarInfo.LineNo,///产线代码 3
                 sapWorkCalendarInfo.ProductionDate,///生产日期 4
                 sapWorkCalendarInfo.Shift, ///班次     5
                 sapWorkCalendarInfo.BeginTime,///开始时间 6
                 sapWorkCalendarInfo.EndTime,///结束时间 7
                 sapWorkCalendarInfo.MinCapacity,///最小产能 8
                 sapWorkCalendarInfo.MaxCapacity,///最大产能 9
                 sapWorkCalendarInfo.Version,///最大产能 10
                 (int)ProcessFlagConstants.Untreated,///处理状态 11
                 1,///逻辑删除状态 12
                 loginUser///创建用户 13
                );
            }
            if (sqlSb.Length > 0)
            {
                Log.WriteLogToFile(logFlag, interfaceCode + "-EcecuteSQL:|" + sqlSb.ToString(), AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMddHH"));
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(sqlSb.ToString());
            }
            return sapWorkCalendarInfos.Count;
        }
    }
}