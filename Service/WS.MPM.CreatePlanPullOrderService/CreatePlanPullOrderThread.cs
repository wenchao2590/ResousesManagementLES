using Infrustructure.Logging;
using Infrustructure.Thread;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WS.MPM.CreatePlanPullOrderService
{
    /// <summary>
    /// 
    /// </summary>
    public class CreatePlanPullOrderThread : ThreadBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceFid"></param>
        public CreatePlanPullOrderThread(Guid serviceFid) : base(serviceFid) { }
        /// <summary>
        /// Process
        /// </summary>
        /// <returns></returns>
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
                Log.WriteLogToFile(ex.Message, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));
                return false;
            }
        }
    }
}
