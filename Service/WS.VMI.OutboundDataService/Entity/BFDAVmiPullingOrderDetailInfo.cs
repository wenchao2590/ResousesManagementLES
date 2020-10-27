

namespace WS.VMI.OutboundDataService
{
    using System.Xml.Serialization;

    /// <summary>
    /// 物料拉动单中PartDetails  Item
    /// PullingOrderDetail
    /// </summary>
    [XmlRoot("DTL")]
    public class BFDAVmiPullingOrderDetailInfo
    {
        /// <summary>
        /// 物料编号
        /// </summary>
        [XmlElement("PARTNO")]
        public string PartNo;

        /// <summary>
        /// 物料描述
        /// </summary>
        [XmlElement("PARTCNAME")]
        public string PartCName;

        /// <summary>
        /// 收容数
        /// </summary>
        [XmlElement("SNP")]
        public decimal? SNP;

        /// <summary>
        /// 数量
        /// </summary>
        [XmlElement("PARTQTY")]
        public decimal? PartQty;

        /// <summary>
        /// 目标库位
        /// </summary>
        [XmlElement("TARGETSLCODE")]
        public string TargetSLCode;

        /// <summary>
        /// 超市库位
        /// </summary>
        [XmlElement("SUPPERMARKETREPOSITORY")]
        public string SuppermarketRepository;

        /// <summary>
        /// 包装型号
        /// </summary>
        [XmlElement("PACKAGECODE")]
        public string PackageCode;

        /// <summary>
        /// 备注
        /// </summary>
        [XmlElement("REMARK")]
        public string Remark;

        /// <summary>
        /// 供应商代码
        /// </summary>
        [XmlElement("SUPPLIERCODE")]
        public string SupplierCode;

        /// <summary>
        /// 供应商名称
        /// </summary>
        [XmlElement("SUPPLIERNAME")]
        public string SupplierName;

        /// <summary>
        /// 检验模式
        /// </summary>
        [XmlElement("VERIFYMODE")]
        public string VerifyMode;

        /// <summary>
        /// 行序列号
        /// </summary>
        [XmlElement("EXTERNLINENO")]
        public string EXTERNLINENO;
    }
}
