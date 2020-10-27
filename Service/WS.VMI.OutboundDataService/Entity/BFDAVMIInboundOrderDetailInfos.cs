using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WS.VMI.OutboundDataService
{
    [XmlRoot("DTLS")]
    public class BFDAVMIInboundOrderDetailInfos
    {
        [XmlElement("DTL")]
        ///物料
        public List<BFDAVMIInboundOrderDetailInfo> listDetailInfo { get; set; }
    }
}
