using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS.QMIS.InboundDataService
{
    /// <summary>
    /// QMIS-LES-001 检验模式接口
    /// </summary>
    public class BFDAQmisCheckModeInfo
    {
        public string partNo;///物料编号	
        public string partName;///物料名称	
        public string supplierNo;///供应商代码	
        public string supplierName;///供应商名称	
        public string checkMode;///检验模式	

    }
} 