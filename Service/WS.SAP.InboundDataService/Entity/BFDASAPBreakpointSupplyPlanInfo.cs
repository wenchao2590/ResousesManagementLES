using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS.SAP.InboundDataService
{
    /// <summary>
    /// SAP-LES-014	断点供货计划
    /// </summary>
    public class BFDASAPBreakpointSupplyPlanInfo
    {
        public string ORDER_NO;///生产订单号 	NVARCHAR(36)
		public string MATNR;///物料号 		NVARCHAR(16)
		public string LIFNR;///供应商 		NVARHCAR(8)
		public string MENGE;///数量		Dec 13


    }
}