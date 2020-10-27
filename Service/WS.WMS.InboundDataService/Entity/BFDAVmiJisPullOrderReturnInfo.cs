using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS.VMI.InboundDataService
{
    /// <summary>
    /// wms 排序拉动单回传 WMS-LES-014	排序拉动单回传
    /// TI_IFM_VMI_JIS_PULL_ORDER_RETURN
    /// </summary>
    public class BFDAVmiJisPullOrderReturnInfo
    {
        public string OrderCode;	///来源单号	  Nvarchar2(10)  
		public string PartNo;	///物料代码 	  Nvarchar2(10)
		public string carsortseq;	///车号	  Nvarchar2(20)  
		public string wmssourcekey;	///WMS 单号	  Nvarchar2(20) 
		public string wmslinenumber;	///行号	  Nvarchar2(30)  


    }
}