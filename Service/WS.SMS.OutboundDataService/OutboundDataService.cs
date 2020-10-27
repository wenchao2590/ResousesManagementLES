using Infrustructure.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace WS.SMS.OutboundDataService
{
    partial class OutboundDataService : ServiceBase
    {
        private Guid serviceFid;
        OutboundDataThread thread;
        public OutboundDataService(Guid serviceFid)
        {
            this.serviceFid = serviceFid;
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                if (thread == null)
                    thread = new OutboundDataThread(serviceFid);
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
