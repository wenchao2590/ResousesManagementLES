using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace WS.SAP.SyncVmiSupplierPartService
{
    partial class SyncVmiSupplierPartService : ServiceBase
    {
        private Guid serviceFid;
        SyncVmiSupplierPartThread thread;
        public SyncVmiSupplierPartService(Guid serviceFid)
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: 在此处添加代码以启动服务。
            try
            {
                if (thread == null)
                    thread = new SyncVmiSupplierPartThread(serviceFid);
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
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
        }
    }
}
