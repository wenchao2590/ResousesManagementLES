


namespace WS.SRM.OutboundDataService
{
    using System.Xml.Serialization;
    using System.Collections.Generic;
    [XmlRoot("DTLS")]
    public class BFDAPartReturnSheetDetailInfos
    {
        [XmlElement("DTL")]
        public List<BFDAPartReturnSheetDetailInfo> Parts;

    }
}


