using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

using UI.Client.Templete;

namespace UI.Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Guid WorkCall = new Guid(ConfigurationManager.AppSettings["WorkCellFid"].ToString());
            FrmLogin f = new FrmLogin(WorkCall);
            var loginResult = f.ShowDialog();
            if (loginResult == DialogResult.Yes)
                Application.Run(new FrmMain(f.User));

            //Application.Run(new FrmKanbanView());
        }
}
}
