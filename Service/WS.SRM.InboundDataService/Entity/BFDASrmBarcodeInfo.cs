using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS.SRM.InboundDataService
{
    /// <summary>
    /// 箱标签中间表 SRMBarcodeInfo
    /// </summary>
    public class BFDASrmBarcodeInfo
    {
        ///// <summary>
        ///// 日志号 
        ///// </summary>
        //public string LogFid;

        /// <summary>
        /// 箱标签号 1
        /// </summary>
        public string PackageBarcode;

        /// <summary>
        /// 物料编号 2
        /// </summary>
        public string PartNo;

        /// <summary>
        /// 物料名称 3
        /// </summary>
        public string PartCname;

        /// <summary>
        /// 数量 4
        /// </summary>
        public decimal? PartQty;

        /// <summary>
        /// 目标库位 5
        /// </summary>
        public string TargetSlcode;

        /// <summary>
        /// 超市库位-- 2018-7-25 新增
        /// </summary>
        public string SuppermarketRepository;

        /// <summary>
        /// 包装型号 6
        /// </summary>
        public string PackageCode;

        /// <summary>
        /// 原始单据号 7
        /// </summary>
        public string SourceOrderCode;

        /// <summary>
        /// 收容数 8
        /// </summary>
        public decimal? SNP;

        /// <summary>
        /// 备注 9
        /// </summary>
        public string Remark;


        /// <summary>
        /// 工厂
        /// </summary>
        public string Plant;

    }
}