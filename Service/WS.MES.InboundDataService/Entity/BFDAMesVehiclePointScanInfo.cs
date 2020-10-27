using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS.MES.InboundDataService
{
    /// <summary>
    /// MES-LES-001	过点信息
    /// </summary>
    public class BFDAMesVehiclePointScanInfo
    {

        

        public string UNIT_NO; ///采集点编号
        public int? DMS_SEQ; ///过点顺序号
        public string DMS_NO; ///计划订单号
        public string VIN; ///VIN号
        public string PREVIOUS_DMS_NO; ///前车订单号
        public DateTime? SEND_TIME; ///发送时间
        public string  FALG; ///标识


        


    }
}