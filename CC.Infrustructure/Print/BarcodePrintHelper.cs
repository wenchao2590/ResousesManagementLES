using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Infrustructure.Logging;

namespace Infrustructure.Print
{
    /// <summary>
    /// Barcode打印Helper
    /// </summary>
    public static class BarcodePrintHelper
    {
        /// <summary>
        /// 打印同步锁对象
        /// </summary>
        private static readonly Object PrintSyncObj = new Object();

        /// <summary>
        /// Bartender执行文件路径
        /// </summary>
        private static String BartenderFilePath
        {
            get { return ConfigurationManager.AppSettings["bartenderFilePath"]; }
        }

        /// <summary>
        /// Barode文件目录
        /// </summary>
        private static String BarcodeFileDir
        {
            get { return ConfigurationManager.AppSettings["barcodeFileDir"]; }
        }

        /// <summary>
        /// Barcode模板路径
        /// </summary>
        private static String BarcodeTemplateDir
        {
            get { return ConfigurationManager.AppSettings["barcodeTemplateDir"]; }
        }

        /// <summary>
        /// 同步打印
        /// </summary>
        /// <param name="templateFile"></param>
        /// <param name="barcode"></param>
        public static void PrintSync(String templateFile, String barcode)
        {
            Print(templateFile, new List<String> { barcode });
        }

        /// <summary>
        /// 异步打印
        /// </summary>
        /// <param name="templateFile"></param>
        /// <param name="barcode"></param>
        public static void PrintAsync(String templateFile, String barcode)
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                Print(templateFile, new List<String> { barcode });
            });
        }

        /// <summary>
        /// 同步打印
        /// </summary>
        /// <param name="templateFile"></param>
        /// <param name="barcodes"></param>
        public static void PrintSync(String templateFile, IEnumerable<String> barcodes)
        {
            Print(templateFile, barcodes);
        }

        /// <summary>
        /// 异步打印
        /// </summary>
        /// <param name="templateFile"></param>
        /// <param name="barcodes"></param>
        public static void PrintAsync(String templateFile, IEnumerable<String> barcodes)
        {
            ThreadPool.QueueUserWorkItem(o =>
            {
                Print(templateFile, barcodes);
            });
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="templateFile">模板文件</param>
        /// <param name="barcodes">条码集合</param>
        private static void Print(String templateFile, IEnumerable<String> barcodes)
        {
            //检查Bartender执行文件是否存在
            if (!File.Exists(BartenderFilePath))
            {
                throw new FileNotFoundException("找不到Bartender执行文件");
            }
            String templateFilePath = String.Format(@"{0}\{1}", BarcodeTemplateDir, templateFile);
            //检查打印模板文件是否存在
            if (!File.Exists(templateFilePath))
            {
                throw new FileNotFoundException("找不到打印模板文件");
            }

            //生成打印条码字串
            String barcodeStr = String.Join("\t", barcodes);
            //创建打印数据文件
            String barcodeFilePath = String.Format("{0}\\{1}.txt", BarcodeFileDir, Guid.NewGuid());
            File.Create(barcodeFilePath).Close();
            using (StreamWriter sw = new StreamWriter(barcodeFilePath, false, Encoding.UTF8))
            {
                sw.WriteLine(barcodeStr);
                sw.Flush();
                sw.Close();
            }

            //创建打印进程
            var process = new Process
            {
                StartInfo = new ProcessStartInfo(BartenderFilePath,
                                                 String.Format(" /F=\"{0}\" /D=\"{1}\" /P /X", templateFilePath, barcodeFilePath))
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardOutput = true
                }
            };
            //限制只允许一个打印作业运行
            lock (PrintSyncObj)
            {
                try
                {
                    //开始打印
                    process.Start();
                    //等待打印1分钟时间，如果未完成打印，则强制终止
                    process.WaitForExit(60 * 1000);
                }
                finally
                {
                    if (!process.HasExited)
                    {
                        process.Kill();
                        process.Dispose();
                    }
                }
            }

            //完成打印后删除数据文件
            File.Delete(barcodeFilePath);
        }
    }
}