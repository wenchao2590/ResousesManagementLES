using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.LES
{
    public partial class PlanPullOrderInfo
    {
        public string RunsheetNo
        {
            get
            {
                return OrderCode;
            }
        }
    }
}
