using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS.VMI.InboundDataService
{
    /// <summary>
    /// 排序拉动单回执
    /// </summary>
    public class BFDAVmiPackageOutboundInfo
    {

        /// <summary>
        /// log日志号
        /// </summary>
        public Guid LogFid;

        /// <summary>
        /// 来源单号
        /// </summary>
        public string OrderCode;

        /// <summary>
        /// 物料代码 
        /// </summary>
        public string PartNo;

        /// <summary>
        ///  车号
        /// </summary>
        public string CarSortseq;

        /// <summary>
        /// WMS 单号
        /// </summary>
        public string WmssourceKey;

        /// <summary>
        /// 行号
        /// </summary>
        public string WmsLineNumber;

    }
}