using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WS.VMI.OutboundDataService
{
    /// <summary>
    /// 入库数据
    /// </summary>
    [XmlRoot("DOC")]
    public class BFDAVmiInboundDataInfo
    {
        [XmlElement("SOURCEORDERCODE")]
        public string SourceOrderCode;///		原始单据号

        [XmlElement("SOURCEORDERTYPE")]
        public string SourceOrderType;///		原始单据类型

        [XmlElement("PARTNO")]
        public string PartNo;///		物料编号

        [XmlElement("SUPPLIERCODE")]
        public string SupplierCode;///		供应商代码

        [XmlElement("SUPPLIERNAME")]
        public string SupplierName;///		供应商名称

        [XmlElement("DELIVERYQTY")]
        public string DeliveryQty;///		实收数量

        [XmlElement("WMSSOURCEKEY")]
        public string Wmssourcekey;///		WMS单号

        [XmlElement("WMSLINENUMBER")]
        public string Wmslinenumber;///		WMS行号

        [XmlElement("VMIWAREHOUSECODE")]
        public string VmiWarehouseCode;///		VMI仓库代码

        [XmlElement("WERKS")]
        public string Werks;///		工厂代码
    }
}
