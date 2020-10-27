

namespace WS.VMI.OutboundDataService
{
    /// <summary>
    /// 排序拉动单详细表
    /// </summary>
    using System.Xml.Serialization;
    [XmlRoot("DTL")]
    public class BFDAVmiJisPullOrderDetailInfo
    {
        [XmlElement("VEHICLESEQNO")]
        public string VehicleSeqNo;///车辆序号

        [XmlElement("PARTNO")]
        public string PartNo;///物料编号

        [XmlElement("SNP")]
        public string SNP;///收容数

        [XmlElement("PARTQTY")]
        public string PartQty;///数量

        [XmlElement("VEHICLEMODELNO")]
        public string VehicleModelNo;///车型代码

        [XmlElement("VINCODE")]
        public string VINCode;///VIN号

        [XmlElement("REMARK")]
        public string Remark;///备注

        [XmlElement("SUPERMARKETREPOSITORY")]
        public string SupermarketRepository;///超市库位

        [XmlElement("EXTERNLINENO")]
        public string ExternLineNo;///行序列号
    }
}
