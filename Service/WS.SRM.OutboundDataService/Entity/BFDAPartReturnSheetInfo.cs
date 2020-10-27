namespace WS.SRM.OutboundDataService
{
    using System.Xml.Serialization;
    /// <summary>
    /// LES-SRM-009	物料退货单 
    /// </summary>
    public class BFDAPartReturnSheetInfo
    {
        public string Plant;            ///工厂
        public string OrderCode;        ///退货单号
		public string Dock;             ///道口
		public string PublishTime;      ///发单时间
		public string SupplierCode;     ///供应商代码
		public string SupplierName;     ///供应商名称
		public string SourceZoneNo;     ///来源存储区代码
		public string Keeper;           ///保管员
		public string Remark;           ///备注
		public string DeleteFlag;       ///删除标记
        [XmlElement("DTLS")]
        public BFDAPartReturnSheetDetailInfos DetailsInfo;
    }
}
