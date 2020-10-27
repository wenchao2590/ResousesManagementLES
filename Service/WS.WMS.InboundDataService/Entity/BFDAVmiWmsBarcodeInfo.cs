using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS.VMI.InboundDataService
{

    /// <summary>
    /// 箱标签中间表
    /// </summary>
    public class BFDAVmiBarcodeInfo
    {
        /// <summary>
        /// 日志号
        /// </summary>
        public Guid LogFid;

        /// <summary>
        /// 箱标签号
        /// </summary>
        public string PackageBarcode;

        /// <summary>
        /// 原始单据号
        /// </summary>
        public string SourceOrderCode;

        /// <summary>
        /// 物料编号
        /// </summary>
        public string PartNo;

        /// <summary>
        /// 物料名称
        /// </summary>
        public string PartCname;

        /// <summary>
        /// 收容数
        /// </summary>
        public decimal? Snp;

        /// <summary>
        /// 数量
        /// </summary>
        public decimal? PartQty;

        /// <summary>
        /// 线边工位
        /// </summary>
        public string LinePosition;

        /// <summary>
        /// 超市库位
        /// </summary>
        public string SupermarketRepository;

        /// <summary>
        /// 目标库位
        /// </summary>
        public string TargetSlcode;

        /// <summary>
        /// 包装型号
        /// </summary>
        public string PackageCode;

        /// <summary>
        /// 供应商编码
        /// </summary>
        public string SupplierCode;

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark;
    }
}