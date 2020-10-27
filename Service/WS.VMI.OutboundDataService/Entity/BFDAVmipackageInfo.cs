using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WS.VMI.OutboundDataService
{
    /// <summary>
    /// 器具基础信息
    /// </summary>
    [XmlRoot("DOC")]
    public class BFDAVmipackageInfo
    {

        [XmlElement("SKU")]
        public string Sku;          ///器具代码

        [XmlElement("SKUDESCR")]
        public string Skudescr;     ///器具描述

        [XmlElement("SKUCLS")]
        public string Skucls;       ///器具类型

        [XmlElement("SUPPLYCODE")]
        public string Supplycode;   ///供应商代码


    }
}
