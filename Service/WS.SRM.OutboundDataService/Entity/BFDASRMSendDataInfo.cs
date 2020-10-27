namespace WS.SRM.OutboundDataService
{
    using System.Collections.Generic;
    using System.Xml.Serialization;
    [XmlRoot("DOCS")]
    public class BFDASRMSendDataInfo<T>
    {
        [XmlElement("DOC")]
        public List<T> List;
    }
}
