using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WS.VMI.OutboundDataService
{
    /// <summary>
    ///  LES-WMS-010	库存锁定
    /// </summary>
    [XmlRoot("DOC")]
    public class BFDAVmiPartStockLockInfo
    {
        [XmlElement("WERKS")]
        public string Werks;            ///工厂代码

        [XmlElement("PARTNO")]
        public string PartNo;           ///物料编号

        [XmlElement("SUPPLIERCODE")]
        public string SupplierCode;     ///供应商代码

        [XmlElement("VMIWAREHOUSECODE")]
        public string VmiWarehouseCode; ///VMI仓库代码

        [XmlElement("PARTQTY")]
        public string PartQty;          ///数量

        [XmlElement("ORILOCKSTATUS")]
        public string Orilockstatus;    ///源锁库状态

        [XmlElement("TARGETLOCKSTATUS")]
        public string Targetlockstatus; ///目标锁库状态

        [XmlElement("INVSTATUS")]
        public string Invstatus;        ///库存状态
    }
}
