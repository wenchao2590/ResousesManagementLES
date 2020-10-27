using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS.SRM.InboundDataService
{

    /// <summary>
    /// 出入库事物数据
    /// </summary>
    public class BFDASrmTranDetailsInfo
    {

        /// <summary>
        /// 日志编号
        /// </summary>
        public  string LogFid { get; set; }

        /// <summary>
        /// 零件号
        /// </summary>
        public string PartNo { get; set; }

        /// <summary>
        /// 供应商代码
        /// </summary>
        public string SupplierCode { get; set; }

        /// <summary>
        /// 拉动单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal? Qty { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        public string TransactionType { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? Times { get; set; }

        /// <summary>
        /// 仓库代码
        /// </summary>
        public string VmiWarehouseCode { get; set; }

    }
}