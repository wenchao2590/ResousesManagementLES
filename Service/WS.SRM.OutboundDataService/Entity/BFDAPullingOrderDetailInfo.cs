namespace WS.SRM.OutboundDataService
{
    using System.Xml.Serialization;

    /// <summary>
    /// 物料拉动单中PartDetails  Item
    /// PullingOrderDetail
    /// </summary>
    [XmlRoot("DetailInfo")]
    public class BFDAPullingOrderDetailInfo
    {
        public string OrderCode;    ///订单号

        /// <summary>
        /// 物料编号
        /// </summary>
        public string PartNo;

        /// <summary>
        /// 物料描述
        /// </summary>
        public string PartCName;

        /// <summary>
        /// 收容数
        /// </summary>
        public decimal? SNP;

        /// <summary>
        /// 数量
        /// </summary>
        public decimal? PartQty;

        /// <summary>
        /// 目标库位
        /// </summary>
        public string TargetSLCode;

        /// <summary>
        /// 超市库位
        /// </summary>
        public string SuppermarketRepository;

        /// <summary>
        /// 包装型号
        /// </summary>
        public string PackageCode;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark;

        /// <summary>
        /// 供应商代码
        /// </summary>
        public string SupplierCode;

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName;

        /// <summary>
        /// 检验模式
        /// </summary>
        public string VerifyMode;

        /// <summary>
        /// 行序列号
        /// </summary>
        public string EXTERNLINENO;
    }
}
