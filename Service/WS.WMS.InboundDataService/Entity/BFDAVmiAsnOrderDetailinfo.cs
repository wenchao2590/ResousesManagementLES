

namespace WS.VMI.InboundDataService
{

    using System.Xml.Serialization;
    [XmlRoot("PartDetail")]
    public class BFDAVmiAsnOrderDetailinfo
    {
        [XmlElement("PARTNO")]
        public string PartNo;       ///物料编号
        [XmlElement("SNP")]
        public string SNP;          ///收容数
        [XmlElement("PARTQTY")]
        public string PartQty;      ///数量
        [XmlElement("PACKAGECODE")]
        public string PackageCode;  ///包装型号
        [XmlElement("REMARK")]
        public string Remark;	    ///备注
    }
}