using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WS.VMI.OutboundDataService
{
    /// <summary>
    ///  VMI器具入库
    ///  les.TI_IFM_VMI_PACKAGE_INBOUND  VmiPackageInboundInfo
    /// </summary>
    [XmlRoot("DOC")]
    public class BFDAVmiPackageInboundInfo
    {
        [XmlElement("ORDERKEY")]
        public string orderkey;///WMS 单号

        [XmlElement("ORDERLINENUMBER")]
        public string orderlinenumber;///行号

        [XmlElement("STORERKEY")]
        public string Storerkey;///供应商代码

        [XmlElement("SKU")]
        public string Sku;///物料代码

        [XmlElement("RECQTY")]
        public string Recqty;///接收数量

        [XmlElement("VMIWAREHOUSECODE")]
        public string VmiWarehouseCode;///VMI 仓库代码

        [XmlElement("WERKS")]
        public string werks;///工厂代码
    }
}
