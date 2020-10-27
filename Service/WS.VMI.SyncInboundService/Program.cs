namespace WS.VMI.SyncInboundService
{
    using Infrustructure.Utilities;
    using System;
    using System.ServiceProcess;
    using System.Threading;
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            ///Console方式运行的间隔时间（秒）
            int spanSeconds = int.Parse(AppSettings.GetConfigString("spanTime"));
            ///获取APP.Config中的consoleFlag标签值，true时以Console方式运行
            if (AppSettings.GetConfigString("consoleFlag").ToLower() == "true")
            {
                ///Console标题
                Console.Title = "WS.VMI.SyncInboundService";
                while (true)
                {
                    try
                    {
                        ///执行函数
                        new Handle().Handler();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ":" + ex.Message);
                    }
                    ///每次执行完成Console显示一次DONE
                    Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ":DONE");
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
