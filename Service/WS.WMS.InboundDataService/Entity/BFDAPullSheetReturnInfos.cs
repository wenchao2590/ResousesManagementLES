using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WS.VMI.InboundDataService
{
    [XmlRoot("PartDetails")]
    public class BFDAPullSheetReturnInfos
    {

        public List<BFDAVmiPackageOutboundInfo> PartDetails ;
    }
}

