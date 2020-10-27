namespace WS.SRM.OutboundDataService
{
    using System.Xml.Serialization;
    /// <summary>
    /// 排序拉动单中间表
    /// </summary>
    public class BFDAJisPullOrderInfo
    {
        public string Plant;             ///工厂
        public string OrderCode;         ///拉动单号		
        public string Dock;              ///道口 	
        public string PublishTime;       ///发单时间 		
        public string PartBoxCode;       ///零件类代码 		
        public string PartBoxName;       ///零件类名称		
        public string SupplierCode;      ///供应商代码		
        public string SupplierName;      ///供应商名称 		
        public string SourceZoneNo;      ///来源存储区代码	
        public string TargetZoneNo;      ///目标存储区代码	
        public string StartInfoPointTime;///开始过点时间	
        public string PlanDeliveryTime; ///预计到货时间	
        public string StartVehicleSeqNo;///开始车辆序号	
        public string EndVehicleSeqNo;  ///结束车辆序号	
        public string Location;         ///工位			
        public string Remark;           ///备注			
        public string DeleteFlag;       ///删除标记	
        [XmlElement("PartDetails")]
        public BFDAJisPullOrderDetailInfos OrderDetail;      /// 排序拉动单详情表
    }
}
