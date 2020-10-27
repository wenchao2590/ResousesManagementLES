using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace WS.SYS.SAMPLE
{
    public partial class SampleService : ServiceBase
    {
        private Guid serviceFid;
        SampleThread thread;
        public SampleService(Guid serviceFid)
        {
            this.serviceFid = serviceFid;
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                if (thread == null)
                    thread = new SampleThread(serviceFid);
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
