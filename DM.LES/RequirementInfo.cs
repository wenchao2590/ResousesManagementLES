using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.LES
{
    public class RequirementInfo
    {
        public long id;
        public string boxCode;
        public int seqNo;
        public bool kitFlag;
        public int pullType;
        public string partNo;
        public decimal partQty;
        public string supplierNo;
        public string locationNo;
        public string fromStockLoc;
        public string toStockLoc;
        public string vehicheNo;
        public DateTime requirementTime;
        public string sourceNo;
    }
}
