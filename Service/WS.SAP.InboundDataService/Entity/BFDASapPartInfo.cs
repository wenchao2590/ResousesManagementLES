using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WS.SAP.InboundDataService
{
    [XmlRoot("ITEM")]
    /// <summary>
    /// 物料主数据
    /// </summary>
    public class BFDASapPartInfo
    {
        [XmlElement("WERKS")]
        /// <summary>
        /// 工厂
        /// </summary>
        public string Werks;

        [XmlElement("MATNR")]
        /// <summary>
        /// 物料编号
        /// </summary>
        public string Matnr;

        [XmlElement("MTART")]
        /// <summary>
        /// 物料类型
        /// </summary>
        public string Mtart;

        [XmlElement("MAKTX")]
        /// <summary>
        /// 物料描述(中文)
        /// </summary>
        public string Maktx;

        [XmlElement("SPRAS")]
        /// <summary>
        /// 语言代码
        /// </summary>
        public string Spras;

        /// <summary>
        /// 英文描述
        /// </summary>
        //public string EnglishDescription;

        [XmlElement("FLAG")]
        /// <summary>
        /// 是否易错件
        /// </summary>
        public string Flag;

        [XmlElement("MEINS")]
        /// <summary>
        /// 单位
        /// </summary>
        public string Meins;

        [XmlElement("DISPO")]
        /// <summary>
        /// MRP控制者
        /// </summary>
        public string Dispo;

        /// <summary>
        /// MRP类型 作废
        /// </summary>
        //public string Dismm;

        [XmlElement("ABC")]
        /// <summary>
        /// 接口标识
        /// 1.创建 2.更改 3.删除
        /// </summary>
        public string Abc;
        /// <summary>
        /// 更新时间
        /// </summary>
        //public string UpdateTime;

        [XmlElement("EKGRP")]
        /// <summary>
        /// 采购组
        /// </summary>
        public string Ekgrp;
    }
}