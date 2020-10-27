namespace WS.SRM.OutboundDataService
{
    using System.Xml.Serialization;
    /// <summary>
    /// LES-SRM-005-物料拉动单 
    /// </summary>
    public class BFDAPullingOrderInfo
    {
        public string OrderCode;///拉动单号
        public string OrderType;///单据类型
        public string Dock;///道口
        public string PublishTime;///发单时间
        public string PartBoxCode;///零件类代码
        public string PartBoxName;///零件类名称
        public string SupplierCode;///供应商代码
        public string SupplierName;///供应商名称
        public string SourceZoneNo;///来源存储区代码
        public string TargetZoneNo;///目标存储区代码
        public string Keeper;///保管员
        public string PlanShippingTime;///预计发货时间
        public string PlanDeliveryTime;///预计到货时间
        public string Remark;///备注
        public string AsnFlag;///是否允许编辑ASN
        public string EmergencyFlag;///是否紧急
        public string DeleteFlag;///删除标记
        public string Plant;///工厂代码

        /// <summary>
        /// 物料明细 (该条是xml格式的List类型数据)
        /// </summary>
        [XmlElement("PartDetails")]
        public BFDAPullingOrderDetailInfos OrderDetail;

    }
}
