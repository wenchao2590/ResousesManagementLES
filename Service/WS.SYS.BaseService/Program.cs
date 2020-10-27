using Infrustructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WS.SYS.BaseService
{
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
                Console.Title = "WS.SYS.BaseService";
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
                    Console.Write("OPC Service 测试程序，请勿关闭，谢谢！");
                    while (true)
                    {
                        if (Console.ReadLine() == "exit")
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                ///获取服务对应在数据库中配置的FID
                Guid serviceFid = Guid.Parse(AppSettings.GetConfigString("serviceFid"));
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new BaseService(serviceFid)
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
