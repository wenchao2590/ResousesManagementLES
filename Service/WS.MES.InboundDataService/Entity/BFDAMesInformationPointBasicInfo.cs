using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS.MES.InboundDataService
{
    /// <summary>
    /// MES-LES-006	信息点基础数据
    /// </summary>
    public class BFDAMesInformationPointBasicInfo
    {

        public string ENTERPRISE;           ///工厂编号 		NVARCHAR(4)
		public string SITE_NO;              ///车间编号 		NVARCHAR(4)
		public string AREA_NO;              ///生产线编号 		NVARCHAR(4)
		public string STATION_CODE;         ///工位编号 		NVARCHAR(4)

        
        public string UNIT_NO;       ///信息点编号
        public string UNIT_NAME;     ///信息点名称
        public string STATUS;        ///处理状态
        public DateTime? SEND_TIME;  ///发送时间 		DateTime


    }
}