using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS.SAP.InboundDataService
{
    /// <summary>
    /// 断点更改单
    /// </summary>
    public class BFDASapBreakpointChangesInfo
    {

        public string AENNR;///变更单号 	CHAR
        public string MATNR;///父物料 	CHAR(18)
        public string CHANGE_FLAG;///更改标识 	NVARCHAR(30)
        public string OIDNRK;///旧物料 	CHAR(18)
        public string NIDNRK;///新物料 	CHAR(18)
        public decimal? MENGE;///更改数量 	decimal(18," 3)
        public string EBORT;///工位 	CHAR
        public string DATUV;///有效起止日期 	DATS
        public string DATUB;///有效截止日期	Nvarchar(10)
        public string ZCJ;///车间	Nvarchar(4)
        public string KTSCH;///工位	Nvarchar(7)
        public string WERKS;///工厂	Nvarchar(4)
        public string SORTF;///排序字符串	Nvarchar(10)
   
    }
}