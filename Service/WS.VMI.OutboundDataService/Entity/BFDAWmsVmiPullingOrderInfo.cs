using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WS.VMI.OutboundDataService
{
    /// <summary>
    /// 物料拉动单-LES-WMS-003
    /// </summary>
    public class BFDAWmsVmiPullingOrderInfo
    {
        /// <summary>
        /// 拉动单号
        /// </summary>
        [XmlElement("ORDERCODE")]
        public string OrderCode;

        /// <summary>
        /// 单据类型
        /// </summary>
        [XmlElement("ORDERTYPE")]
        public string OrderType;

        /// <summary>
        /// 道口
        /// </summary>
        [XmlElement("DOCK")]
        public string Dock;

        /// <summary>
        /// 发单时间
        /// </summary>
        [XmlElement("PUBLISHTIME")]
        public string PublishTime;

        /// <summary>
        /// 零件类代码
        /// </summary>
        [XmlElement("PARTBOXCODE")]
        public string PartBoxCode;

        /// <summary>
        /// 零件类名称
        /// </summary>
        [XmlElement("PARTBOXNAME")]
        public string PartBoxName;

        /// <summary>
        /// 来源存储区代码
        /// </summary>
        [XmlElement("SOURCEZONENO")]
        public string SourceZoneNo;

        /// <summary>
        /// 目标存储区代码
        /// </summary>
        [XmlElement("TARGETZONENO")]
        public string TargetZoneNo;

        /// <summary>
        /// 保管员
        /// </summary>
        [XmlElement("KEEPER")]
        public string Keeper;

        /// <summary>
        /// 预计发货时间
        /// </summary>
        [XmlElement("PLANSHIPPINGTIME")]
        public string PlanShippingTime;

        /// <summary>
        /// 预计到货时间
        /// </summary>
        [XmlElement("PLANDELIVERYTIME")]
        public string PlanDeliveryTime;

        /// <summary>
        /// 备注
        /// </summary>
        [XmlElement("REMARK")]
        public string Remark;

        /// <summary>
        /// 是否允许编辑ASN
        /// </summary>
        [XmlElement("ASNFLAG")]
        public string AsnFlag;

        /// <summary>
        /// 是否紧急
        /// </summary>
        [XmlElement("EMERGENCYFLAG")]
        public string EmergencyFlag;

        /// <summary>
        /// 时间窗代码
        /// </summary>
        [XmlElement("WINTIMECODE")]
        public string WintimeCode;

        /// <summary>
        /// 时间窗描述
        /// </summary>
        [XmlElement("WINTIMEDESC")]
        public string WintimeDesc;

        /// <summary>
        /// 工厂代码
        /// </summary>
        [XmlElement("WERKS")]
        public string Werks;

        /// <summary>
        /// 物料明细 (该条是xml格式的List类型数据)
        /// </summary>
        [XmlElement("DTLS")]
        public BFDAVmiPullingOrderDetailInfos OrderDetail;

    }
}
