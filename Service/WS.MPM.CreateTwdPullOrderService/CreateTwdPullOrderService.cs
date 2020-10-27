using Infrustructure.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WS.MPM.CreateTwdPullOrderService
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CreateTwdPullOrderService : ServiceBase
    {
        /// <summary>
        /// 
        /// </summary>
        private Guid serviceFid;
        /// <summary>
        /// 
        /// </summary>
        CreateTwdPullOrderThread thread;
        /// <summary>
        /// CreatePlanPullOrderService
        /// </summary>
        /// <param name="serviceFid"></param>
        public CreateTwdPullOrderService(Guid serviceFid)
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
                    thread = new CreateTwdPullOrderThread(serviceFid);
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
