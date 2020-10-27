namespace WS.SRM.CalculateSupplyPlanService
{
    using Infrustructure.Logging;
    using System;
    using System.ServiceProcess;
    public partial class CalculateSupplyPlanService : ServiceBase
    {
        private Guid serviceFid;
        CalculateSupplyPlanThread thread;
        public CalculateSupplyPlanService(Guid serviceFid)
        {
            this.serviceFid = serviceFid;
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                if (thread == null)
                    thread = new CalculateSupplyPlanThread(serviceFid);
                thread.Start();
            }
            catch (Exception ex)
            {
                Log.WriteLogToFile(ex.Message, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));
            }
        }

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
