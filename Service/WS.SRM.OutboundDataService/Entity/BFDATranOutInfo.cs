namespace WS.SRM.OutboundDataService
{
    /// <summary>
    /// 入库数据
    /// </summary>
    public class BFDATranOutInfo
    {
        /// <summary>
        /// 工厂代码
        /// </summary>
        public string Plant;
        /// <summary>
        /// 原始单据号
        /// </summary>
        public string SourceOrderCode;
        /// <summary>
        /// 原始单据类型
        /// </summary>
        public string SourceOrderType;
        /// <summary>
        /// 物料编号
        /// </summary>
        public string PartNo;
        /// <summary>
        /// 实收数量
        /// </summary>
        public string DeliveryQty;
    }
}
