namespace WS.SRM.OutboundDataService
{
    using System.Xml.Serialization;

    /// <summary>
    /// 物料退货单详情表
    /// </summary>
    [XmlRoot("DTL")]
    public class BFDAPartReturnSheetDetailInfo
    {
        public string PartNo;       /// 物料编号
		public string PartCName;    /// 物料描述
		public string PartQty;      /// 数量
		public string TargetSLCode; /// 来源库位
		public string Remark;       /// 备注

    }
}
