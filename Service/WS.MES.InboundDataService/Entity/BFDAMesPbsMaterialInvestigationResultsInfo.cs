namespace WS.MES.InboundDataService
{
    using System;
    /// <summary>
    /// MES-LES-003	PBS物料排查结果 -
    /// </summary>
    public class BFDAMesPbsMaterialInvestigationResultsInfo
    {
        public string ENTERPRISE;   ///工厂编号 		NVARCHAR(4)
		public string SITE_NO;      ///车间编号 		NVARCHAR(4)
		public string AREA_NO;      ///生产线编号 		NVARCHAR(4)
        public string DMS_NO;       ///计划订单号 		NVARCHAR(25)
		public DateTime SEND_TIME;  ///发送时间 		DateTime
        public string VERSION;      ///生产版本
        public string FORCE_PASS;   ///强制放行(0：非强制放行；1：强制放行)
        public int RESULT;          ///处理结果
        public string MSGNO;        ///消息编号
        public string MSG;          ///消息内容
        public int MATERIAL_CHECK;  ///排查结果
    }
}