using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS.SAP.InboundDataService
{
    /// <summary>
    ///  SAP-LES-016 物料预留单
    /// </summary>
    public class BFDASapMaterialReservationInfo
    {
        public string RSNUM;///	预留号
		public string RSPOS;///	预留行号
		public string WEMPF;///	收货方
		public string LIFNR;///	供应商 
		public string MATNR;///	物料
		public string MENGE;///	数量
		public string BDTER;///	需求日期
            
        public string BWART;    ///移动类型		Char(3)
        public string KOSTL;    ///成本中心 	Char(10)
        public string LGORT;    ///库存地点		Char(4)
        public string UMLGO;    ///转入库存地点	CHAR(4)
        
        public string EBELN;///	采购订单
		public string EBELP;///	采购订单行项目号
    }
}