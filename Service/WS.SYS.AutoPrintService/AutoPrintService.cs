using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using WS.SYS.AutoPrintService;

namespace WS.SYS.AutoPrintService
{
    public partial class AutoPrintService : ServiceBase
    {
        private Guid serviceFid;
        AutoPrintThread thread;
        public AutoPrintService(Guid serviceFid)
        {
            this.serviceFid = serviceFid;
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                if (thread == null)
                    thread = new AutoPrintThread(serviceFid);
                thread.Start();
            }
            catch (Exception ex)
            {

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

            }
        }
    }
}

