using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.LES
{
    public partial class StocksInfo
    {
        private decimal? matchedQty;
        /// <summary>
        /// 匹配数量
        /// </summary>
        public decimal? MatchedQty { get => matchedQty; set => matchedQty = value; }
    }
}
