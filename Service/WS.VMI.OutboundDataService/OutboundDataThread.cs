using Infrustructure.Logging;
using Infrustructure.Thread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WS.VMI.OutboundDataService
{
    public class OutboundDataThread : ThreadBase
    {
        public OutboundDataThread(Guid serviceFid) : base(serviceFid) { }

        protected override bool Process()
        {
            try
            {
                new Handle().Handler();
                Log.WriteLogToFile("Process", AppDomain.CurrentDomain.BaseDirectory + @"\LOG\", DateTime.Now.ToString("yyyyMMdd"));
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLogToFile(ex.ToString(), AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));
                return false;
            }
        }
    }
}
