using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.LES
{
    public partial class VmiOutputDetailInfo
    {
        private DateTime tranTime;
        private string costCenter;
        /// <summary>
        /// 出库时间
        /// </summary>
        public DateTime TranTime { get => tranTime; set => tranTime = value; }
        /// <summary>
        /// 核算项目
        /// </summary>
        public string CostCenter { get => costCenter; set => costCenter = value; }
    }
}
