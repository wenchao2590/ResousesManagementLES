using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WS.SAP.InboundDataService
{
    /// <summary>
    /// Sap下发的生产订单中间表
    /// </summary>
   [XmlRoot("MatnrsAll")]
    public class BFDASapProductOrderBomMatnrsInfo
    {
        [XmlElement("Matnrs")]
        public List<BFDASapProductOrderBomMatnrdInfo> MatnrsAll;
    }
}