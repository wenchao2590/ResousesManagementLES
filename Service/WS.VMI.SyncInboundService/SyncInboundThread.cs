namespace WS.VMI.SyncInboundService
{
    using Infrustructure.Logging;
    using Infrustructure.Thread;
    using System;

    public class SyncInboundThread : ThreadBase
    {
        /// <summary>
        /// SyncInboundThread
        /// </summary>
        /// <param name="serviceFid"></param>
        public SyncInboundThread(Guid serviceFid) : base(serviceFid) { }
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
