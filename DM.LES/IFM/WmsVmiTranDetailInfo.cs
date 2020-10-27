using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.LES
{
    public partial class WmsVmiTranDetailInfo
    {
        private string zoneNo;
        private string dloc;
        /// <summary>
        /// 存储区
        /// </summary>
        public string ZoneNo { get => zoneNo; set => zoneNo = value; }
        /// <summary>
        /// 库位
        /// </summary>
        public string Dloc { get => dloc; set => dloc = value; }

    }
}
