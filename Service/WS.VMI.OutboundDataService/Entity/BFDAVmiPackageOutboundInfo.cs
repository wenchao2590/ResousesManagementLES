using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WS.VMI.OutboundDataService
{
    /// <summary>
    ///  VMI器具出库
    ///  les.TI_IFM_VMI_PACKAGE_INBOUND  VmiPackageInboundInfo
    /// </summary>
    [XmlRoot("DOC")]
    public class BFDAVmiPackageOutboundInfo
    {
        [XmlElement("EXTERNRECEIPTKEY")]
        public string Externreceiptkey; ///来源单号		

        [XmlElement("EXTERNLINENO")]
        public string Externlineno; ///行序号		

        [XmlElement("STORERKEY")]
        public string Storerkey; ///供应商代码	

        [XmlElement("SKU")]
        public string Sku; ///物料代码		

        [XmlElement("QTYEXPECTED")]
        public string Qtyexpected; ///数量		

        [XmlElement("VMIWAREHOUSECODE")]
        public string Vmiwarehousecode; ///VMI 仓库代码	

        [XmlElement("WERKS")]
        public string Werks; ///工厂代码		
    }
}
