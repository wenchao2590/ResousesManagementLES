using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WS.VMI.OutboundDataService
{
    /// <summary>
    /// 生产订单
    /// </summary>
    [XmlRoot("DOC")]
    public class BFDAVmiProductOrderInfo
    {
        [XmlElement("ORDERNO")]
        public string OrderNo;      ///	订单编号

        [XmlElement("PART_NO")]
        public string Part_No;      ///	物料编号

        [XmlElement("ORDERDATE")]
        public string OrderDate;    ///	订单日期

        [XmlElement("ASSEMBLYLINE")]
        public string AssemblyLine; ///	生产线

        [XmlElement("QTY")]
        public string Qty;          ///	数量

        [XmlElement("ONLINETIME")]
        public string OnLineTime;   ///	上线时间

        [XmlElement("DOWNLINETIME")]
        public string DownLineTime; ///	下线时间

        [XmlElement("MODELYEAR")]
        public string ModelYear;    ///	整车颜色

        [XmlElement("LOCKFLAG")]
        public string LockFlag;     ///	锁定标识

        [XmlElement("SEQ")]
        public string SEQ;          ///	顺序

        [XmlElement("WERKS")]
        public string Werks;	    ///	工厂代码


    }
}
