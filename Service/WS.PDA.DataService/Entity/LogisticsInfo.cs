using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS.PDA.BaseService
{
    public partial class LogisticsInfo
    {
        private DateTime tranTime;
        private string comments;

        public DateTime TranTime { get => tranTime; set => tranTime = value; }
        public string Comments { get => comments; set => comments = value; }
    }
}