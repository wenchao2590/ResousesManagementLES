namespace WS.SRM.OutboundDataService
{
    using System.Xml.Serialization;
    /// <summary>
    /// 排序拉动单详细表
    /// </summary>
    [XmlRoot("DetailInfo")]
    public class BFDAJisPullOrderDetailInfo
    {
        public string OrderCode;    ///订单号
        public string VehicleSeqNo;///车辆序号
		public string PartNo;///物料编号
		public string SNP;///收容数
		public string PartQty;///数量
		public string VehicleModelNo;///车型代码
		public string VINCode;///VIN号
		public string Remark;///备注
    }
}
