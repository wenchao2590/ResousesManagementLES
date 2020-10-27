using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WS.SAP.InboundDataService
{
    [XmlRoot("DATA")]
    public class ESBDataInfo
    {
        [XmlElement("HEAD")]
        public ESBHeadInfo eSBHead;
    }
}