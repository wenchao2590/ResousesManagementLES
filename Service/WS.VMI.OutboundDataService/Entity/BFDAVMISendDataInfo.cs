using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WS.VMI.OutboundDataService
{
    [XmlRoot("DOCS")]
    public class BFDAVMISendDataInfo<T>
    {
        [XmlElement("DOC")]
        public List<T> List;
    }
}
