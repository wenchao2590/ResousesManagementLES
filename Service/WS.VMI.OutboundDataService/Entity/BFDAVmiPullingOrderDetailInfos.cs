using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WS.VMI.OutboundDataService
{
    [XmlRoot("DTLS")]
    public class BFDAVmiPullingOrderDetailInfos
    {
        [XmlElement("DTL")]
        public List<BFDAVmiPullingOrderDetailInfo> list { get; set; }
    }
}
