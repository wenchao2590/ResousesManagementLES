namespace WS.SRM.OutboundDataService
{
    using System.Collections.Generic;
    using System.Xml.Serialization;
    /// <summary>
    /// JIS排序拉动单单详情表
    /// </summary>
    [XmlRoot("PartDetails")]
    public class BFDAJisPullOrderDetailInfos
    {
        [XmlElement("DetailInfo")]
        public List<BFDAJisPullOrderDetailInfo> list { get; set; }

    }
}
