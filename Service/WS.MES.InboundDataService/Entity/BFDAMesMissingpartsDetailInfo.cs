using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WS.MES.InboundDataService
{
    /// <summary>
    /// MES-LES-004	缺件明细详细数据
    /// </summary>
    [XmlRoot("DTC")]
    public class BFDAMesMissingpartsDetailInfo
    {
        public string DMS_NO;       ///计划订单号 	NVARCHAR(25)
        public string STATIONCODE;  ///工位编号       nvarchar(200)
        public string MATERCODE;    ///物料编号       nvarchar(200)
        public decimal? QTY;          ///数量           decimal(18,3)
    }
}