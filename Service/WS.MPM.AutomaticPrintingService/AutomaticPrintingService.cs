using Infrustructure.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace WS.MPM.AutomaticPrintingService
{
    public partial class AutomaticPrintingService : ServiceBase
    {
        private Guid serviceFid;
        PackageStocksSyncThread thread;
        public AutomaticPrintingService(Guid serviceFid)
        {
            this.serviceFid = serviceFid;
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                if (thread == null)
                    thread = new PackageStocksSyncThread(serviceFid);
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
