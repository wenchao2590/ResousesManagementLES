using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS.SAP.InboundDataService
{
    /// <summary>
    /// SAP工作日历基础数据接收 (报文表)
    /// </summary>
    public class BFDASapWorkCalendarInfo
    {

        /// <summary>
        /// 工厂
        /// </summary>
        public string DWERK;
        /// <summary>
        /// 车间代码
        /// </summary>
        public string ZCJ;
        /// <summary>
        /// 产线代码
        /// </summary>
        public string LINENO;
        /// <summary>
        /// 日期
        /// </summary>
        public string PRODUCTIONDATE;
        /// <summary>
        /// 班次
        /// </summary>
        public string SHIFT;
        /// <summary>
        /// 开始时间
        /// </summary>
        public string BEGINTIME;
        /// <summary>
        /// 结束时间
        /// </summary>
        public string ENDTIME;

        /// <summary>
        /// 最小产能
        /// </summary>
        public int MINCAPACITY;

        /// <summary>
        /// 最小产能
        /// </summary>
        public int MAXCAPACITY;
        /// <summary>
        /// 版本
        /// </summary>
        public string VERSION;

    }
}