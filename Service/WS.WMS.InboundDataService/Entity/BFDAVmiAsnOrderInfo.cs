

namespace WS.VMI.InboundDataService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Xml.Serialization;
    [XmlRoot("AsnOrderInfo")]
    /// <summary>
    /// WMS-LES-006 物料送货单 WL011 
    /// </summary>
    public class BFDAVmiAsnOrderInfo
    {

        public string OrderCode;            ///送货单号
        public string SourceOrderType;      ///原始单据类型
        public string Dock;                 ///道口
        public string PublishTime;          ///发单时间
        public string SupplierCode;         ///供应商代码
        public string SupplierName;         ///供应商名称
        public string SourceZoneNo;         ///来源存储区代码
        public string TargetZoneNo;         ///目标存储区代码
        public string Keeper;               ///保管员
        public string PlanShippingTime;     ///预计发货时间
        public string PlanDeliveryTime;     ///预计到货时间
        public string Remark;               ///备注
        public string EmergencyFlag;        ///是否紧急
        public string DeleteFlag;		    ///删除标记

        [XmlElement("PartDetailList")]         
        public BFDAVmiAsnOrderDetailinfos PartDetails;  ///物料明细

         
    }
}