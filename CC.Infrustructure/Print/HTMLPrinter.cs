using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using mshtml;
using System.Management;

namespace Infrustructure.Print
{
    //声明从Web Browserd COM导入的HTMLElementRender接口
    [Guid("3050f669-98b5-11cf-bb82-00aa00bdce0b"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), ComVisible(true), ComImport]
    public interface IHTMLElementRender
    {
        void DrawToDC([In] IntPtr hDC);
        void SetDocumentPrinter([In, MarshalAs(UnmanagedType.BStr)] String bstrPrinterName, [In] IntPtr hDC);
    };

    public class PrintArgs : EventArgs
    {
        public PrintArgs(String msg)
        {
            Message = msg;
        }

        public String Message
        {
            get;
            private set;
        }
    }


    /// <summary>
    /// HTML打印机，打印任意HTML文件
    /// </summary>
    public class HTMLPrinter : IDisposable
    {
        /// <summary>
        /// WebBrower对象
        /// </summary>
        private WebBrowser WebBrowser { get; set; }

        /// <summary>
        /// 打印文档
        /// </summary>
        private PrintDocument PrintDocument { get; set; }

        /// <summary>
        /// 打印机名称
        /// </summary>
        private String PrinterName { get; set; }

        /// <summary>
        /// 可否被Dispose
        /// </summary>
        private bool CanDispose { get; set; }

        /// <summary>
        /// 休眠时间
        /// </summary>
        private int SleepMillSeconds { get; set; }

        /// <summary>
        /// 默认Mill秒数
        /// </summary>
        private const int DefaultSeconds = 0;

        public event EventHandler<PrintArgs> BeforeNavigating;
        public event EventHandler<PrintArgs> AfterNavigating;
        public event EventHandler<PrintArgs> BeforePrinting;
        public event EventHandler<PrintArgs> AfterPrinting;
        public event EventHandler<PrintArgs> BeforeRendering;
        public event EventHandler<PrintArgs> AfterRendering;
        public event EventHandler<PrintArgs> RenderMissing;

        public static void PrintAsync(String printerName, int sleepMillSeconds, String filePath, Boolean landscape, short copies)
        {
            var thead = new System.Threading.Thread(WinformPrintAsync);
            thead.SetApartmentState(ApartmentState.STA);
            thead.Start(new PrintParams
            {
                PrinterName = printerName,
                SleepMillSeconds = sleepMillSeconds,
                HtmlFilePath = filePath,
                Landscape = landscape,
                Copies = copies
            });
        }

        private struct PrintParams
        {
            public String PrinterName { get; set; }
            public int SleepMillSeconds { get; set; }
            public String HtmlFilePath { get; set; }
            public Boolean Landscape { get; set; }
            public short Copies { get; set; }
        }

        private static void WinformPrintAsync(Object obj)
        {
            PrintParams printParams = (PrintParams)obj;

            lock (SyncObj)
            {
                HTMLPrinter htmlPrinter = new HTMLPrinter(printParams.PrinterName, printParams.SleepMillSeconds);
                htmlPrinter.WinFormPrint(printParams.HtmlFilePath, printParams.Landscape, printParams.Copies);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="printerName">打印机名称</param>
        /// <param name="sleepMillSeconds">休眠时间</param>
        public HTMLPrinter(String printerName, int sleepMillSeconds)
        {
            WebBrowser = new WebBrowser();
            CanDispose = true;
            PrinterName = printerName;
            SleepMillSeconds = (sleepMillSeconds < DefaultSeconds) ? DefaultSeconds : sleepMillSeconds;
        }

        /// <summary>
        /// HTML打印对象构造函数
        /// </summary>
        /// <param name="browser">浏览器对象</param>
        /// <param name="printerName">打印机名称，如果该字段为空，则使用默认打印机</param>
        public HTMLPrinter(WebBrowser browser, String printerName)
        {
            if (browser == null)
            {
                throw new ArgumentNullException("browser", "浏览器控件对象不能为空");
            }

            this.WebBrowser = browser;
            this.WebBrowser.ScrollBarsEnabled = false;
            PrinterName = printerName;
        }

        /// <summary>
        /// 获取PrintDocument
        /// </summary>
        /// <param name="printerName"></param>
        /// <returns></returns>
        private PrintDocument GetPrintDocument(String printerName)
        {
            PrintDocument printDocument = new PrintDocument();
            if (!String.IsNullOrEmpty(printerName))
            {
                printDocument.DefaultPageSettings.PrinterSettings.PrinterName = CheckPrinter(printerName);
            }
            return printDocument;
        }
        /// <summary>
        /// 检查打印是否存在状态是否可用，否则返回默认打印机
        /// </summary>
        /// <param name="printerName"></param>
        /// <returns></returns>
        public static string CheckPrinter(string printerName)
        {
            ManagementScope scope = new ManagementScope(@"\root\cimv2");
            scope.Connect();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");
            foreach (ManagementObject printer in searcher.Get())
            {
                if (printer["Name"].ToString().ToLower() == printerName.ToLower())
                    return printerName;
            }
            return new PrintDocument().PrinterSettings.PrinterName;
        }

        /// <summary>
        /// 打印文档页面处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            //触发打印页面开始事件
            if (BeforeRendering != null)
            {
                BeforeRendering(this, new PrintArgs("开始渲染"));
            }

            //获取打印句柄
            IntPtr ptr = e.Graphics.GetHdc();

            //重置浏览器窗口大小与打印内容一致
            ResizeWebBrowser();
            //获取Dom对象
            IHTMLDocument2 document = (IHTMLDocument2)this.WebBrowser.Document.DomDocument;
            if (document != null)
            {
                //获取Dom中的Body元素
                IHTMLElement bodyElement = document.body;
                if (bodyElement != null)
                {
                    IHTMLElementRender renderObject = (IHTMLElementRender)bodyElement;

                    //设置打印机并打印
                    renderObject.SetDocumentPrinter(PrintDocument.DefaultPageSettings.PrinterSettings.PrinterName, ptr);
                    renderObject.DrawToDC(ptr);

                    //触发本页打印结束事件
                    if (AfterRendering != null)
                    {
                        AfterRendering(this, new PrintArgs("结束渲染"));
                    }
                }
                else if (RenderMissing != null)
                {
                    RenderMissing(this, new PrintArgs("因bodyElement==null未被渲染"));
                }
            }
            else if (RenderMissing != null)
            {
                RenderMissing(this, new PrintArgs("因document==null未被渲染"));
            }

            //没有后继页面
            e.HasMorePages = false;
        }

        /// <summary>
        /// 重置WebBrower控件打印大小
        /// </summary>
        private void ResizeWebBrowser()
        {
            //获取WebBrowser的文档对象
            HtmlDocument document = this.WebBrowser.Document;
            if (document == null) return;

            //获取文档的Body元素
            HtmlElement bodyElement = document.Body;
            if (bodyElement != null)
            {
                //打印前，重设浏览器对象的高度和宽度
                Rectangle htmlSize = new Rectangle { Location = new Point(0, 0) };

                if (bodyElement.ScrollRectangle.Height < PrintDocument.DefaultPageSettings.Bounds.Height - 25)
                {
                    htmlSize.Height = PrintDocument.DefaultPageSettings.Bounds.Height - 25;
                }
                else
                {
                    htmlSize.Height = bodyElement.ScrollRectangle.Height;
                }
                if (bodyElement.ScrollRectangle.Width < PrintDocument.DefaultPageSettings.Bounds.Width - 25)
                {
                    htmlSize.Width = PrintDocument.DefaultPageSettings.Bounds.Width - 25;
                }
                else
                {
                    htmlSize.Width = bodyElement.ScrollRectangle.Width;
                }

                //重置WebBrower大小
                this.WebBrowser.Size = new Size(htmlSize.Width + 25, htmlSize.Height + 25);
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="htmlFilePath">待打印HTML文件路径</param>
        /// <param name="landscape">横向or纵向</param>
        /// <param name="copies">打印份数</param>
        public void WinFormPrint(String htmlFilePath, bool landscape, short copies)
        {
            //HTML文件检查
            if (String.IsNullOrEmpty(htmlFilePath))
            {
                throw new ArgumentNullException("htmlFilePath", "待打印的HTML文件路径不能为空");
            }
            if (!File.Exists(htmlFilePath))
            {
                throw new ArgumentException(String.Format("待打印的HTML文件{0}不存在", htmlFilePath));
            }

            //导航事件开始
            if (BeforeNavigating != null)
            {
                BeforeNavigating(this, new PrintArgs(String.Format("开始导航{0}", htmlFilePath)));
            }

            //加载HTML文档
            WebBrowser.Navigate(htmlFilePath);
            //等待加工HTML文档完成
            while (WebBrowser.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }


            //完成HTML加载
            System.Threading.Thread.Sleep(100);
            WebBrowser.Stop();
            if (AfterNavigating != null)
            {
                AfterNavigating(this, new PrintArgs(String.Format("结束导航{0}", htmlFilePath)));
            }

            //设置打印文档资料
            PrintDocument = GetPrintDocument(PrinterName);
            PrintDocument.DocumentName = htmlFilePath;
            PrintDocument.PrintPage += new PrintPageEventHandler(PrintPage);
            PrintDocument.DefaultPageSettings.Landscape = landscape;
            PrintDocument.PrinterSettings.Copies = copies;

            //触发开始打印事件
            if (BeforePrinting != null)
            {
                BeforePrinting(this, new PrintArgs("开始打印" + htmlFilePath));
            }
            PrintDocument.Print();
            //触发打印完成事件
            if (AfterPrinting != null)
            {
                AfterPrinting(this, new PrintArgs("结束打印" + htmlFilePath));
            }
            System.Threading.Thread.Sleep(SleepMillSeconds);
        }

        public void WinFormPrint(List<string> htmlFiles)
        {
            foreach (var htmlFile in htmlFiles)
            {
                WebBrowser.Navigate(htmlFile);
                while (WebBrowser.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                }
                WebBrowser.Stop();
                PrintDocument = GetPrintDocument(PrinterName);
                PrintDocument.DocumentName = htmlFile;
                PrintDocument.PrintPage += new PrintPageEventHandler(PrintPage);
                PrintDocument.DefaultPageSettings.Landscape = false;
                PrintDocument.PrinterSettings.Copies = 1;
                PrintDocument.Print();
            }
        }

        public static readonly Object SyncObj = new Object();

        /// <summary>
        /// 通过浏览器打钱
        /// </summary>
        /// <param name="url"></param>
        /// <param name="landscape"></param>
        public void WebPrint(String url, bool landscape)
        {
            WebBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(PrintDocumentViaBrowser);

            WebBrowser.Url = new Uri(url);
            System.Threading.Thread.Sleep(SleepMillSeconds);
            PrintDocument = GetPrintDocument(PrinterName);
            PrintDocument.DocumentName = url;
            PrintDocument.DefaultPageSettings.Landscape = landscape;
            WebBrowser.ShowPrintPreviewDialog();
        }

        private void PrintDocumentViaBrowser(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // Print the document now that it is fully loaded.
            // ((WebBrowser)sender).Print();

            // Dispose the WebBrowser now that the task is complete. 
            ((WebBrowser)sender).Dispose();
        }

        public void Dispose()
        {
            if (CanDispose)
            {
                PrintDocument.Dispose();
                WebBrowser.Dispose();
            }
        }
    }
}
