using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS.QMIS.InboundDataService
{

    /// <summary>
    /// QMIS-LES-003 检验结果回传-中间表
    /// </summary>
    public class BFDAQmisCheckResultInfo
    {
        public string asnNo;///ASN单号
        public string orderNo;///拉动单号
        public string partNo;///物料编号
        public string supplierNo;///供应商编号
        public string totalNo;///送检数量
        public string unqualifiedNo;///不合格数量
        public string checkStatus;///检验状态
    }
}