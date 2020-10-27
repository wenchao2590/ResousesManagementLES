namespace WS.SRM.OutboundDataService
{
    using System.Collections.Generic;
    using System.Xml.Serialization;
    [XmlRoot("PartDetails")]
    public class BFDAPullingOrderDetailInfos
    {
        [XmlElement("DetailInfo")]
        public List<BFDAPullingOrderDetailInfo> list { get; set; }
    }
}
