namespace WS.VMI.OutboundDataService
{
    using System.Xml.Serialization;
    [XmlRoot("DOC")]
    public class BFDAVMIInboundOrderInfo
    {
        [XmlElement("SHIPPINGCODE")]
        ///发货单号
        public string ShippingCode { get; set; }

        [XmlElement("SUPPLIERCODE")]
        ///供应商代码
        public string SupplierCode { get; set; }

        [XmlElement("DELIVERYTIME")]
        ///预计交货时间
        public string DeliveryTime { get; set; }

        [XmlElement("VMIWAREHOUSECODE")]
        ///VMI仓库代码
        public string VmiWmNo { get; set; }

        [XmlElement("ORDERTYPE")]
        ///单据类型
        public string OrderType { get; set; }

        [XmlElement("STATUS")]
        ///单据状态
        public string Status { get; set; }

        [XmlElement("WERKS")]
        ///工厂代码
        public string Werks { get; set; }

        [XmlElement("DTLS")]
        public BFDAVMIInboundOrderDetailInfos orderDetailInfos;
    }
}
