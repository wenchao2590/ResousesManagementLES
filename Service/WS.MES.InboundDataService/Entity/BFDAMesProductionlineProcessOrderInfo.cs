using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS.MES.InboundDataService
{
    /// <summary>
    /// MES-LES-007	产线级工艺顺序
    /// </summary>
    public class BFDAMesProductionlineProcessOrderInfo
    {

        public string ENTERPRISE;       ///工厂编号 		NVARCHAR(4)
		public string SITE_NO;          ///车间编号 		NVARCHAR(4)
		public string AREA_NO;          ///生产线编号 		NVARCHAR(4)
        public string STATIONCODE;      ///工位编号
        public string SEQ_NO;           ///工位顺序号
        public int LINEFLAG;         ///是否上下结点
        public int LiduiFlag;        ///是否离队点
        public int GUIDUIFLAG;       ///是否归队点
        public int PBSFlag;          ///是否pbs锁定点
        public string Status;           ///处理状态  (C：新增，U：更新，D：删除)
        public DateTime SEND_TIME;        ///发送时间

    }
}