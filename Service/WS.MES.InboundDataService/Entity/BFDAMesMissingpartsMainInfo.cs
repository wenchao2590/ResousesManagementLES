using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WS.MES.InboundDataService
{
    /// <summary>
    /// MES-LES-004	缺件明细主数据
    /// </summary>

    public class BFDAMesMissingpartsMainInfo
    {

        public string ENTERPRISE;  ///工厂编号 		NVARCHAR(4)
		public string SITE_NO;     ///车间编号 		NVARCHAR(4)
		public string AREA_NO;     ///生产线编号 		NVARCHAR(4)	 
		public string DMS_NO;      ///计划订单号 		NVARCHAR(25)
		public DateTime SEND_TIME;   ///发送时间 		DateTime

        public int RESULT;
        public string MSG;
        public string MSGNO;

        [XmlElement("DTLS")]
        public List<BFDAMesMissingpartsDetailInfo> DTLS = new List<BFDAMesMissingpartsDetailInfo>();
    }
}