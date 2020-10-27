using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WS.SYS.BaseService
{
    public partial class BaseService : ServiceBase
    {
        private Guid serviceFid;
        BaseThread thread;
        public BaseService(Guid serviceFid)
        {
            this.serviceFid = serviceFid;
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                if (thread == null)
                    thread = new BaseThread(serviceFid);
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
