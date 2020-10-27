using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS.VMI.InboundDataService
{
    /// <summary>
    /// 在途转库存 中间表
    /// </summary>
    public class BFDAVmiTransitToInventoryInfo
    {

        /// <summary>
        /// log日志号
        /// </summary>
        public Guid LogFid;

        /// <summary>
        /// WMS 调整单号
        /// </summary>
        public string TransferKey;

        /// <summary>
        /// WMS 调整单行号
        /// </summary>
        public string TransferLineNumber;

        /// <summary>
        /// VMI 仓库代码
        /// </summary>
        public string VmiCode;

        /// <summary>
        ///  供应商代码 
        /// </summary>          
        public string FromstorerKey;

        /// <summary>
        /// 物料代码
        /// </summary>
        public string Fromsku;

        /// <summary>
        ///   数量 
        /// </summary>
        public string Toqty;

        /// <summary>
        /// 源锁库状态 
        /// </summary>
        public string Fromlot07;

        /// <summary>
        /// 目标锁库状态
        /// </summary>
        public string Tolot07;

    }
}