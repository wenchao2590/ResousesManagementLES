using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WS.SAP.InboundDataService
{

    /// <summary>
    /// Sap下发的生产订单中间表
    /// </summary>    
    ///[XmlRoot("BFDASapProductOrder")]
    public class BFDASapProductOrderInfo
    {
        //[XmlElement("Matnr")]
        /// <summary>
        /// 1 物料编号
        /// </summary>       
        public string Matnr;


        //[XmlElement("DWERK")]
        /// <summary>
        /// 2 工厂
        /// </summary>
        public string Dwerk;

        //[XmlElement("KDAUF")]
        /// <summary>
        /// 3 销售订单
        /// </summary>
        public string Kdauf;

        ///[XmlElement("KDPOS")]
        /// <summary>
        /// 4 行项目
        /// </summary>
        public string Kdpos;

        ///[XmlElement("AUFNR")]
        /// <summary>
        /// 5 订单号
        /// </summary>
        public string Aufnr;

        ///[XmlElement("LOCKFLAG")]
        /// <summary>
        /// 6 锁定标识 10未锁定.20已锁定
        /// </summary>
        public string LockFlag;

        ///[XmlElement("VERID")]
        /// <summary>
        /// 7 生产版本
        /// </summary>
        public string Verid;

        ///[XmlElement("PSMNG")]
        /// <summary>
        /// 8 订单数量
        /// </summary>
        public string Psmng;

        ///[XmlElement("ONLINESEQ")]
        /// <summary>
        /// 9 上线顺序
        /// </summary>
        public string OnlineSeq;

        ///[XmlElement("ONLINEDATE")]
        /// <summary>
        /// 10 上线日期
        /// </summary>
        public string OnlineDate;

        ///[XmlElement("OFFLINEDATE")]
        /// <summary>
        /// 11 下线日期
        /// </summary>
        public string OfflineDate;


        ///// <summary>
        ///// 12 顺序号
        ///// </summary>
        //public string Seq;


        ///[XmlElement("NOTICE")]
        /// <summary>
        /// 13 公告编号
        /// </summary>
        public string Notice;

        ///[XmlElement("CARCOLOR")]
        /// <summary>
        /// 14 整车颜色
        /// </summary>
        public string CarColor;

        ///[XmlElement("LOG_FID")]
        /// <summary>
        /// 15 报文编号
        /// </summary>
        public string Log_Fid;

        /// <summary>
        /// 删除标识
        /// </summary>
        [XmlElement("ZSC")]
        public string Zsc;
    }
}