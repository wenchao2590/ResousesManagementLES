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
    public class BFDASapProductOrderBomInfo
    {
        /// <summary>
        /// 1  订单物料	
        /// </summary>
        public string Fmatnr;
        /// <summary>
        /// 2  工厂	
        /// </summary>
        public string Dwerk;
        /// <summary>
        /// 3  订单号		
        /// </summary>
        public string Aufnr;
        /// <summary>
        /// 4  生产版本	
        /// </summary>
        public string Verid;
        /// <summary>
        /// 5 上线日期	
        /// </summary>
        public string OnlineTime;
        /// <summary>
        /// 6  下线日期
        /// </summary>
        public string OfflineTime;
        /// <summary>
        /// 7子订单号
        /// </summary>
        public string Zzdd;
        /// <summary>
        ///   物料
        /// </summary>
        public string Matnr;

        #region xml形式字符串 
        /// <summary>
        ///  数量	
        /// </summary>
        public string Bdmng;
        /// <summary>
        ///  更改单号	
        /// </summary>
        public string Aennr;
        /// <summary>
        ///  工位
        /// </summary>
        public string Ebort;
        /// <summary>
        ///  供应商
        /// </summary>
        public string Lifnr;
        /// <summary>
        ///  平台
        /// </summary>
        public string Platform;

        #endregion


        /// <summary>
        /// 7 BOM 信息内容List<BFDASapProductOrderBomMatnr>
        /// </summary>
        [XmlElement("MatnrsAll")]
        public BFDASapProductOrderBomMatnrsInfo MatnrsAll;

       
    }
}