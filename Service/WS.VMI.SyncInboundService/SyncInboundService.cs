using Infrustructure.Logging;
namespace WS.VMI.SyncInboundService
{
    using System;
    using System.ServiceProcess;
    /// <summary>
    /// SyncInboundService
    /// </summary>
    public partial class SyncInboundService : ServiceBase
    {
        /// <summary>
        /// 
        /// </summary>
        private Guid serviceFid;
        /// <summary>
        /// 
        /// </summary>
        private SyncInboundThread thread;
        /// <summary>
        /// SyncInboundService
        /// </summary>
        /// <param name="serviceFid"></param>
        public SyncInboundService(Guid serviceFid)
        {
            this.serviceFid = serviceFid;
            InitializeComponent();
        }
        /// <summary>
        /// OnStart
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            try
            {
                if (thread == null)
                    thread = new SyncInboundThread(serviceFid);
                thread.Start();
            }
            catch (Exception ex)
            {
                Log.WriteLogToFile(ex.Message, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));
            }
        }
        /// <summary>
        /// OnStop
        /// </summary>
        protected override void OnStop()
        {
            try
            {
                if (thread != null) thread.Stop();
            }
            catch (Exception ex)
            {
                Log.WriteLogToFile(ex.Message, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));
            }
        }
    }
}
