namespace WS.VMI.OutboundDataService
{
    using System.Xml.Serialization;
    [XmlRoot("DTL")]
    public class BFDAVMIInboundOrderDetailInfo
    {
        [XmlElement("PARTNO")]
        ///物料号
        public string PartNo { get; set; }

        [XmlElement("SNP")]
        ///收容数
        public decimal SNP { get; set; }

        [XmlElement("PARTQTY")]
        ///物料数量
        public decimal PartQty { get; set; }

        [XmlElement("PACKAGECODE")]
        ///包装型号
        public string PackageCode { get; set; }

        [XmlElement("REMARK")]
        ///备注
        public string Remark { get; set; }
    }
}
