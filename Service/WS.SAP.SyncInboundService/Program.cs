namespace WS.SAP.SyncInboundService
{
    using Infrustructure.Logging;
    using Infrustructure.Utilities;
    using System;
    using System.ServiceProcess;
    using System.Threading;
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            ///Console方式运行的间隔时间（秒）
            int spanSeconds = int.Parse(AppSettings.GetConfigString("spanTime"));
            ///获取APP.Config中的consoleFlag标签值，true时以Console方式运行
            if (AppSettings.GetConfigString("consoleFlag").ToLower() == "true")
            {
                ///Console标题
                Console.Title = "WS.SAP.SyncInboundService";
                while (true)
                {
                    try
                    {
                        ///执行函数
                        new Handle().Handler();
                    }
                    catch (Exception ex)
                    {
                        Log.WriteLogToFile("ex:HelpLink|" + ex.HelpLink + "|InnerException|" + ex.InnerException + "|Message|" + ex.Message + "|Source|" + ex.Source + "|StackTrace|" + ex.StackTrace + "|TargetSite|" + ex.TargetSite
                       , AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMddHHmm"));
                        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm") + ":" + ex.Message);
                    }
                    ///每次执行完成Console显示一次DONE
                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd") + ":DONE");
                    ///Sleep
                    Thread.Sleep(spanSeconds * 1000);
                }
            }
            else
            {
                ///获取服务对应在数据库中配置的FID
                Guid serviceFid = Guid.Parse(AppSettings.GetConfigString("serviceFid"));
                ///以服务方式运行
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new SyncInboundService(serviceFid)
                };
                ServiceBase.Run(ServicesToRun);
            }

        }
    }
}
