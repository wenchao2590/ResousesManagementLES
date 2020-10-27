using Infrustructure.Logging;
using Infrustructure.Thread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WS.ATP.LackOfMaterialService
{
    class LackOfMaterialThread : ThreadBase
    {
        public LackOfMaterialThread(Guid serviceFid) : base(serviceFid) { }

        protected override bool Process()
        {
            try
            {
                new Handle().Handler();
                new Handle().CheckLackProductionOrder();
                Log.WriteLogToFile("Process", AppDomain.CurrentDomain.BaseDirectory + @"\LOG\", DateTime.Now.ToString("yyyyMMdd"));
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteLogToFile(ex.Message, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));
                return false;
            }
        }
    }
}
