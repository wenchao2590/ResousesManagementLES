using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS.VMI.InboundDataService
{
    /// <summary>
    /// WMS-LES-016 VMI器具出库
    /// </summary>
    public class BFDAApplianceOutputInfo
    {
        
        /// <summary>
        /// log日志号
        /// </summary>
        public Guid LogFid;

       
        /// <summary>
        /// WMS 单号
        /// </summary>
        public string orderkey;

        /// <summary>
        /// WMS行号
        /// </summary>
        public string orderlinenumber;

        /// <summary>
        /// 器具代码
        /// </summary>
        public string sku;

        /// <summary>
        /// 器具供应商
        /// </summary>
        public string storerkey;

        /// <summary>
        /// 数量
        /// </summary>
        public string shippedqty;

    }
}