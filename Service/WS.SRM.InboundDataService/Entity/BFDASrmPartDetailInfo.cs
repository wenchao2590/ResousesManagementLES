using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS.SRM.InboundDataService
{

    /// <summary>
    /// 物料送货单-送货明细
    /// </summary>
    public class BFDASrmPartDetailInfo
    {

        public string SourceOrderCode;

        public string PartNo;

        public string PartCName;

        public string PartQty;

        public string TargetSLCode;

        public string PackageCode;

        public string SNP;

        public string Remark;

       /// public string DeleteFlag;

    }
}