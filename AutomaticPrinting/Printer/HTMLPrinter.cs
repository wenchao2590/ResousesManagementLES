using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using System.IO;
using mshtml;
using System.Threading;

namespace AutomaticPrinting
{
    #region 声明从Web Browserd COM导入的HTMLElementRender接口
    [Guid("3050f669-98b5-11cf-bb82-00aa00bdce0b"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    ComVisible(true),
    ComImport]
    public interface IHTMLElementRender
    {
        void DrawToDC([In] IntPtr hDC);
        void SetDocumentPrinter([In, MarshalAs(UnmanagedType.BStr)] string bstrPrinterName, [In] IntPtr hDC);

    };
    #endregion

    public class PrintArgs : EventArgs
    {
        private string _msg;

        public PrintArgs(string msg)
        {
            _msg = msg;
        }

        public string Message
        {
            get { return _msg; }
        }
    }


    /// <summary>
    /// HTML打印机，打印任意HTML文件
    /// </summary>
    public class HTMLPrinter : IDisposable
    {
        #region 私有字段
        private WebBrowser _webBrowser;
        private PrintDocument _printDocument;
        private string _printName;
        private string _paperSize;
        private bool _canDispose = false;
        private int _sleepMSeconds;
        private const int DefaultSeconds = 2000;

        /// <summary>
        /// 是否需要重设置浏览器尺寸，默认为true
        /// </summary>
        private bool _isResizeBrowser = true;

        public event EventHandler<PrintArgs> BeforeNavigating;
        public event EventHandler<PrintArgs> AfterNavigating;
        public event EventHandler<PrintArgs> BeforePrinting;
        public event EventHandler<PrintArgs> AfterPrinting;
        public event EventHandler<PrintArgs> BeforeRendering;
        public event EventHandler<PrintArgs> AfterRendering;
        public event EventHandler<PrintArgs> RenderMissing;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="printerName"></param>
        /// <param name="sleepMSeconds"></param>
        /// <param name="isReSizeBrowser"></param>
        public HTMLPrinter(string printerName, int sleepMSeconds, bool isReSizeBrowser)
        {
            _webBrowser = new WebBrowser();
            _canDispose = true;
            _printName = printerName;
            _sleepMSeconds = (sleepMSeconds < DefaultSeconds) ? DefaultSeconds : sleepMSeconds;
            _isResizeBrowser = isReSizeBrowser;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="printerName">打印机名</param>
        /// <param name="sleepMSeconds">打印间隔时间</param>
        /// <param name="paperSize">设置纸张大小，比如 A3， A4</param>
        /// <param name="isReSizeBrowser">是否重置浏览器大小</param>
        public HTMLPrinter(string printerName, int sleepMSeconds, string paperSize, bool isReSizeBrowser)
        {
            _webBrowser = new WebBrowser();
            _canDispose = true;
            _printName = printerName;
            _paperSize = paperSize;
            _sleepMSeconds = (sleepMSeconds < DefaultSeconds) ? DefaultSeconds : sleepMSeconds;
            _isResizeBrowser = isReSizeBrowser;
        }

        /// <summary>
        /// HTML打印对象构造函数
        /// </summary>
        /// <param name="browser">浏览器对象</param>
        /// <param name="printerName">打印机名称，如果该字段为空，则使用默认打印机</param>
        public HTMLPrinter(WebBrowser browser, string printerName)
        {
            #region 参数检查
            if (browser == null)
            {
                throw new ArgumentNullException("browser", "浏览器控件对象不能为空");
            }

            #endregion

            this._webBrowser = browser;
            this._webBrowser.ScrollBarsEnabled = false;
            _printName = printerName;

        }

        private PrintDocument GetPrintDocument(string printerName)
        {
            PrintDocument printDocument = new PrintDocument();
            if (!string.IsNullOrEmpty(printerName))
            {
                printDocument.DefaultPageSettings.PrinterSettings.PrinterName = printerName;
            }

            if (!string.IsNullOrEmpty(_paperSize))
            {
                foreach (PaperSize size in printDocument.DefaultPageSettings.PrinterSettings.PaperSizes)
                {
                    if (size.PaperName == _paperSize)
                    {
                        printDocument.DefaultPageSettings.PaperSize = size;
                    }
                }
            }

            return printDocument;
        }
        #endregion

        #region 事件处理
        /// <summary>
        /// 打印文件事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            if (BeforeRendering != null)
                BeforeRendering(this, new PrintArgs("开始渲染"));

            //获取打印句柄
            IntPtr ptr = e.Graphics.GetHdc();

            //重置浏览器窗口大小
            if (_isResizeBrowser)
                ResizeWebBrowser();

            IHTMLDocument2 document = (IHTMLDocument2)this._webBrowser.Document.DomDocument;

            if (document != null)
            {
                IHTMLElement element = (IHTMLElement)document.body;
                if (element != null)
                {
                    IHTMLElementRender RenderObject = (IHTMLElementRender)element;
                    if (RenderObject != null)
                    {
                        RenderObject.SetDocumentPrinter(_printDocument.DefaultPageSettings.PrinterSettings.PrinterName, ptr);
                        RenderObject.DrawToDC(ptr);

                        if (AfterRendering != null)
                            AfterRendering(this, new PrintArgs("结束渲染"));
                    }
                    else
                    {
                        if (RenderMissing != null)
                            RenderMissing(this, new PrintArgs("因RenderObject==null未被渲染"));
                    }
                }
                else
                {
                    if (RenderMissing != null)
                        RenderMissing(this, new PrintArgs("因element==null未被渲染"));
                }
            }
            else
            {
                if (RenderMissing != null)
                    RenderMissing(this, new PrintArgs("因document==null未被渲染"));
            }
            e.HasMorePages = false;
        }
        #endregion

        #region 方法

        private void ResizeWebBrowser()
        {
            System.Windows.Forms.HtmlDocument document = this._webBrowser.Document;
            if (document == null) return;


            HtmlElement element = document.Body;
            if (element != null)
            {
                //打印前，重设浏览器对象的高度和宽度
                Rectangle m_htmlSize = new Rectangle();
                m_htmlSize.Location = new System.Drawing.Point(0, 0);

                if (element.ScrollRectangle.Height < _printDocument.DefaultPageSettings.Bounds.Height - 25)
                    m_htmlSize.Height = _printDocument.DefaultPageSettings.Bounds.Height - 25;
                else
                    m_htmlSize.Height = element.ScrollRectangle.Height;
                if (element.ScrollRectangle.Width < _printDocument.DefaultPageSettings.Bounds.Width - 25)
                    m_htmlSize.Width = _printDocument.DefaultPageSettings.Bounds.Width - 25;
                else
                    m_htmlSize.Width = element.ScrollRectangle.Width;

                this._webBrowser.Size = new System.Drawing.Size(m_htmlSize.Width + 25, m_htmlSize.Height + 25);

            }
        }

        /// <summary>
        /// 使用指定名称打印机打印HTML文件
        /// </summary>
        /// <param name="htmlFileFullName">待打印的HTML文件名称(全名称，包含路径</param>
        /// <param name="printerName">打印机名称</param>
        /// <param name="landscape">是否横向打印</param>
        public void PrintHTMLDocument(string htmlFileFullName, bool landscape)
        {
            #region 参数检查

            if (string.IsNullOrEmpty(htmlFileFullName))
            {
                throw new ArgumentNullException("htmlFileFullName", "待打印的HTML文件路径不能为空");
            }
            if (!File.Exists(htmlFileFullName))
            {
                throw new ArgumentException("待打印的HTML文件：" + htmlFileFullName + "不存在");
            }
            #endregion

            #region 导航到HTML文件地址

            _webBrowser.Navigate(htmlFileFullName);
            if (_webBrowser.ReadyState != WebBrowserReadyState.Complete)
                Application.DoEvents();

            _webBrowser.Stop();

            #endregion

            #region 打印HTML文件
            _printDocument = GetPrintDocument(_printName);
            //_printDocument.DocumentName = fileInfo.Name;
            _printDocument.DocumentName = htmlFileFullName;
            _printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            _printDocument.DefaultPageSettings.Landscape = landscape;
            //_printDocument.DefaultPageSettings.Margins.Left = 20;
            _printDocument.Print();

            //System.Threading.Thread.Sleep(_sleepMSeconds);
            #endregion
        }

        public void Print(string htmlFileFullName, bool landscape)
        {
            #region 参数检查

            if (string.IsNullOrEmpty(htmlFileFullName))
            {
                throw new ArgumentNullException("htmlFileFullName", "待打印的HTML文件路径不能为空");
            }
            if (!File.Exists(htmlFileFullName))
            {
                throw new ArgumentException("待打印的HTML文件：" + htmlFileFullName + "不存在");
            }
            #endregion

            #region 导航到HTML文件地址
            if (BeforeNavigating != null)
                BeforeNavigating(this, new PrintArgs("开始导航" + htmlFileFullName));
            _webBrowser.Navigate(htmlFileFullName);
            while (_webBrowser.ReadyState != WebBrowserReadyState.Complete)
                Application.DoEvents();
            //System.Threading.Thread.Sleep(100);
            _webBrowser.Stop();
            if (AfterNavigating != null)
                AfterNavigating(this, new PrintArgs("结束导航" + htmlFileFullName));
            #endregion

            #region 打印HTML文件

            _printDocument = GetPrintDocument(_printName);
            _printDocument.DocumentName = htmlFileFullName;
            _printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);

            _printDocument.DefaultPageSettings.Landscape = landscape;
            _printDocument.OriginAtMargins = true;
            if (BeforePrinting != null)
                BeforePrinting(this, new PrintArgs("开始打印" + htmlFileFullName));
            _printDocument.Print();
            if (AfterPrinting != null)
                AfterPrinting(this, new PrintArgs("结束打印" + htmlFileFullName));
            //System.Threading.Thread.Sleep(_sleepMSeconds);
            #endregion
        }

        /// <summary>
        /// print via script in the web page
        /// </summary>
        /// <param name="htmlFileFullName"></param>
        public void Print2(string htmlFileFullName)
        {
            #region 参数检查

            if (string.IsNullOrEmpty(htmlFileFullName))
            {
                throw new ArgumentNullException("htmlFileFullName", "待打印的HTML文件路径不能为空");
            }
            if (!File.Exists(htmlFileFullName))
            {
                throw new ArgumentException("待打印的HTML文件：" + htmlFileFullName + "不存在");
            }
            #endregion

            #region 导航到HTML文件地址
            if (BeforeNavigating != null)
                BeforeNavigating(this, new PrintArgs("开始导航" + htmlFileFullName));
            _webBrowser.Navigate(htmlFileFullName);
            while (_webBrowser.ReadyState != WebBrowserReadyState.Complete)
                Application.DoEvents();
            _webBrowser.Stop();
            if (AfterNavigating != null)
                AfterNavigating(this, new PrintArgs("结束导航" + htmlFileFullName));
            #endregion
        }

        /// <summary>
        /// print via the web browser
        /// </summary>
        /// <param name="htmlFileFullName"></param>
        /// <param name="landscape"></param>
        public void Print3(string htmlFileFullName, bool landscape)
        {
            _webBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(PrintDocumentViaBrowser);

            _webBrowser.Url = new Uri(htmlFileFullName);
            //System.Threading.Thread.Sleep(_sleepMSeconds);
            _printDocument = GetPrintDocument(_printName);
            _printDocument.DocumentName = htmlFileFullName;
            _printDocument.DefaultPageSettings.Landscape = landscape;
        }

        private void PrintDocumentViaBrowser(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // Print the document now that it is fully loaded.
            ((WebBrowser)sender).Print();

            // Dispose the WebBrowser now that the task is complete. 
            ((WebBrowser)sender).Dispose();
        }


        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (_canDispose)
            {
                _printDocument.Dispose();
                _webBrowser.Dispose();
            }
        }

        #endregion
    }
}
