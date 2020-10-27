using Infrustructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WS.ATP.LackOfMaterialService;

namespace WS.ATP.LackOfMaterialService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ///Console方式运行的间隔时间（秒）
            int spanSeconds = int.Parse(AppSettings.GetConfigString("spanTime"));
            ///获取APP.Config中的consoleFlag标签值，true时以Console方式运行
            if (AppSettings.GetConfigString("consoleFlag").ToLower() == "true")
            {
                ///Console标题
                Console.Title = "WS.ATP.LackOfMaterialService";
                while (true)
                {
                    try
                    {
                        ///执行函数
                        new Handle().Handler();
                        new Handle().CheckLackProductionOrder();
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
                new LackOfMaterialService(serviceFid)
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
