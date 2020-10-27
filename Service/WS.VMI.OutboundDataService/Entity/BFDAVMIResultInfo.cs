namespace WS.VMI.OutboundDataService
{
    using System.Xml.Serialization;
    [XmlRoot("Result")]
    public class BFDAVMIResultInfo
    {
        [XmlElement("status")]
        ///接口调用状态
        public string Status;
        [XmlElement("errorCode")]
        ///错误代码
        public string ErrorCode;
        [XmlElement("errorMsg")]
        ///错误信息
        public string ErrorMsg;

    }
}
