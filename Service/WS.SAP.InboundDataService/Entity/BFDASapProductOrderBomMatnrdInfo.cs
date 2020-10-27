using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WS.SAP.InboundDataService
{
    /// <summary>
    /// SAP订单BOM 物料表
    /// </summary>
    [XmlRoot("BomMatnr")]
    public class BFDASapProductOrderBomMatnrdInfo
    {
        /// <summary>
        /// 1 物料
        /// </summary>
        public string Matnr;
        /// <summary>
        /// 2 数量	
        /// </summary>
        public string Bdmng;
        /// <summary>
        /// 3 更改单号	
        /// </summary>
        public string Aennr;
        /// <summary>
        /// 4 工位
        /// </summary>
        public string Ebort;
        /// <summary>
        /// 5 供应商
        /// </summary>
        public string Lifnr;
        /// <summary>
        /// 6 平台
        /// </summary>
        public string Platform;          	


    }
}