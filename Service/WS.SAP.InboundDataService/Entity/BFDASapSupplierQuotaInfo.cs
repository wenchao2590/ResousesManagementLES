using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WS.SAP.InboundDataService
{
    /// <summary>
    /// SAP供应商零件关系数据接收
    /// </summary>
    public class BFDASupplierQuotaInfo
    {
        [XmlElement("MATNR")]
        /// <summary>
        /// 物料编号
        /// </summary>
        public string Matnr;


        [XmlElement("WERKS")]
        /// <summary>
        /// 工厂
        /// </summary>
        public string Werks;

        [XmlElement("QTYPE")]
        /// <summary>
        /// 平台系数
        /// </summary>
        public string Qtype;

        [XmlElement("I_Date")]
        /// <summary>
        /// 有效起始日期
        /// </summary>
        public string I_Date;

        [XmlElement("E_Date")]
        /// <summary>
        /// 有效截止日期
        /// </summary>
        public string E_Date;

        [XmlElement("Znrmm")]
        /// <summary>
        /// 配额协议编号
        /// </summary>
        public string Znrmm;
        /// <summary>
        /// 配额协议行项目编号
        /// </summary>
        public string Qupos;
        /// <summary>
        /// 供应商
        /// </summary>
        public string Lifnr;
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string Name1;
        /// <summary>
        /// 配额
        /// </summary>
        public string Quote;
        /// <summary>
        /// 停供标识
        /// </summary>
        public string Zstop;
        /// <summary>
        /// 状态标识
        /// </summary>
        public string Flag;
    }
}