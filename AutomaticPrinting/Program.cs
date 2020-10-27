using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutomaticPrinting
{


    class Program
    {
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool SetDefaultPrinter(string Name);

        [STAThread]
        static void Main(string[] args)
        {
            //System.Diagnostics.Process p = new System.Diagnostics.Process();
            ////不现实调用程序窗口,但是对于某些应用无效
            //p.StartInfo.CreateNoWindow = true;
            //p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            ////采用操作系统自动识别的模式
            //p.StartInfo.UseShellExecute = true;
            ////要打印的文件路径
            //p.StartInfo.FileName = @"E:\TFS\Daimler\LES\CODE\AutomaticPrinting\Flies\FFFFFFF.pdf";
            ////指定执行的动作，是打印，即print，打开是 open
            //p.StartInfo.Verb = "print";
            ////将指定的打印机设为默认打印机
            //SetDefaultPrinter("Microsoft Print to PDF");
            //p.Start();


            string printerName = Cprinter.DefaultPrinter;
            string runsheetFileName = @"C:\Users\55338\Desktop\KanbnaRunSheet.html";

            using (HTMLPrinter htmlPrinter = new HTMLPrinter(printerName, 100, "A4", true))
            {
                htmlPrinter.Print(runsheetFileName, false);
            }
        }
    }
    /// <summary>
    /// 获取所有打印机
    /// </summary>
    public class Cprinter
    {
        private static PrintDocument fPrintDocument = new PrintDocument();
        ///<summary>
        ///获取本地默认打印机名称
        ///</summary>
        public static string DefaultPrinter
        {
            get { return fPrintDocument.PrinterSettings.PrinterName; }
        }


        /// <summary>
        ///  获取本地打印机的列表，第一项就是默认打印机
        /// </summary>
        public static List<string> GetLocalPrinter()
        {
            List<string> fPrinters = new List<string>();
            fPrinters.Add(DefaultPrinter);  //默认打印机出现在列表的第一项
            foreach (string fPrinterName in PrinterSettings.InstalledPrinters)
            {
                if (!fPrinters.Contains(fPrinterName))
                    fPrinters.Add(fPrinterName);
            }
            return fPrinters;
        }

    }
}
