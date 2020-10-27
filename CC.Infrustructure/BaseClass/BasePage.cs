using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Infrustructure;
using Infrustructure.Utilities;
using Infrustructure.Logging;
using Infrustructure.Utilities.Exception;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;


namespace Infrustructure.BaseClass
{
    /// <summary>
    /// 页面的基类。 所有显示给用户的页面必须从此界面进行继承。
    /// </summary>
    public class BasePage : System.Web.UI.Page
    {
        public const string SessionKey_CurrentUser = "SessionKey_CurrentUser";
        public const string PostBackControlID = "lbPostBack";

        public string ReportServerUrl = System.Configuration.ConfigurationManager.AppSettings["ReportServer"];
        public string ReportRoot = System.Configuration.ConfigurationManager.AppSettings["ReportRoot"];

        //契约名称
        protected string ContractName = "";

        #region 契约默认方法名
        protected string SelectMethodByID = "Get";
        protected string SelectMethodByCondition = "GetByCondition";
        protected string SelectMethodByPageCondition = "GetList";
        protected string SelectCountMethod = "GetCounts";
        protected string InsertMethod = "Add";
        protected string UpdateMethod = "Update";
        protected string DeleteMethod = "Delete";
        protected string DeleteBatchMethod = "DeleteMultRecords";
        #endregion

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            //event
            this.Error += new EventHandler(BasePage_Error);
            this.PreRender += new EventHandler(BasePage_PreRender);

            base.OnInit(e);
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //用户权限检查
            IUser currentUser = UserUtil.GetCurrentUser();
            if (currentUser == null) return;

            //检查用户请求资源是否有权限
            WebUtil.CheckUserRequestURL(currentUser, this.Response);
        }

        /// <summary>
        /// 当前用户
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
        public IUser CurrentUser
        {
            get
            {
                try
                {
                    if (Session[BasePage.SessionKey_CurrentUser] == null)
                    {
                        throw new AuthenticationException();
                    }
                    return (IUser)Session[SessionKey_CurrentUser];
                }
                catch (HttpException)
                {
                    return null;
                }
            }
            set
            {
                Session[SessionKey_CurrentUser] = value;
            }
        }

        /// <summary>
        /// 向页面输出javascript
        /// </summary>
        /// <param name="key"></param>
        /// <param name="script"></param>
        /// <returns></returns>
        public void RegisterJS(string key, string script)
        {
            if (!ClientScript.IsStartupScriptRegistered(key))
            {
                ClientScript.RegisterStartupScript(this.GetType(), key, script);
            }
        }

        private Exception GetException(System.Exception ex)
        {
            if (ex.InnerException != null)
            {
                ex = this.GetException(ex.InnerException);

            }

            return ex;
        }

        /// <summary>
        /// 处理页面异常
        /// </summary>
        /// <param name="ex"></param>
        public void HandleException(Exception exp)
        {
            try
            {
                Exception ex = this.GetException(exp);

                //这里统一处理异常，如果是需要特别处理的异常，例如需要重新尝试事务，均可在这里处理
                if (ex is MPSBusinessException || (ex.InnerException != null && ex.InnerException is MPSBusinessException))
                {
                    MPSBusinessException bEx = (MPSBusinessException)ex;

                    if (bEx == null)
                    {
                        bEx = (MPSBusinessException)ex.InnerException;

                    }
                    Logger.LogInfo("Infrustructure.BaseClass", "HandleException", "BasePage", "页面处理异常" + ex.ToString());

                    this.Message = bEx.Message;
                }
                else if (ex is MESBusinessException)
                {
                    this.Message = ex.Message;
                }
                else if (ex is AuthenticationException)
                {
                    Logger.LogError("Infrustructure.BaseClass", "HandleException", "BasePage", "用户会话丢失或者用户没有权限", ex.InnerException);
                    Response.Redirect("~/default.aspx");
                }
                else if (ex is System.Threading.ThreadAbortException || ex is System.Web.HttpException)
                {
                }
                else if (ex is System.Data.SqlClient.SqlException && ex.Message.StartsWith("Invalid column name"))
                {
                    Logger.LogError("Infrustructure.BaseClass", "HandleException", "BasePage", "数据库异常", ex);
                }
                else
                {
                    Logger.LogError("Infrustructure.BaseClass", "HandleException", "BasePage", "页面处理异常", ex);
                }

                if (ConfigurationManager.AppSettings["Trace"].ToLower() == "true")
                    this.Message = ex.Message;
                else
                {
                    //定制友好提示错误信息
                    if (string.IsNullOrEmpty(this.Message))
                        this.Message = ConfigurationManager.AppSettings["KindlyMessage"];
                }

            }
            catch (Exception e)
            {
                try
                {
                    if (e is System.Threading.ThreadAbortException || e is System.Web.HttpException)
                    {
                        // 如果是页面跳转时终止线程异常，则不再做任何处理, remove loginfo for release version
                        // Logger.LogInfo("Infrustructure.BaseClass", "HandleException", "BasePage", ex.ToString());                    
                    }
                    else
                        Logger.LogError("Infrustructure.BaseClass", "HandleException", "BasePage", "错误处理异常", e);
                }
                catch { }
            }
        }

        private string _message;
        /// <summary>
        /// 向用户显示信息提示
        /// </summary>
        public String Message
        {
            get { return _message; }
            set
            {
                _message = value;
            }
        }
        /// <summary>
        /// 检查是否有特定的权限
        /// </summary>
        /// <param name="sec">安全选项</param>
        /// <returns></returns>
        public bool CheckPermissionSuccess()
        {
            if (this.CurrentUser == null)
                return false;
            return true;
        }
        ///<summary>
        /// 页最顶端的PlaceHolder
        /// </summary>
        public System.Web.UI.WebControls.PlaceHolder plhTopHolder;
        public System.Web.UI.WebControls.PlaceHolder plhBottomHolder;

        /// <summary>
        /// 对服务器端生成的控件不做正确性验证，以免自定义render失败
        /// </summary>
        /// <param name="control"></param>
        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        /// <summary>
        /// 页面发生错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BasePage_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            this.HandleException(ex);
        }

        /// <summary>
        /// 页面预呈现
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BasePage_PreRender(object sender, EventArgs e)
        {
            this.IsRefreshPage = false;

            //注册消息框
            this.RegisterMessageBox();
            //注册隐藏控件
            this.RegisterHiddenBox();

        }

        protected override void CreateChildControls()
        {
            //注册页面回发linkbutton
            this.RegisterPostBackLinkButton();
            base.CreateChildControls();

        }
        /// <summary>
        /// //向每个页面注册一个隐藏的控件，用作界面上公共存储区，该存储区内的数据格式必须
        ///遵循如下格式：key,value;key,value;
        ///界面层可以使用javascript向该控件赋值 。调用SetServerValue();
        /// </summary>
        private void RegisterHiddenBox()
        {
            string js = "<script language='javascript'>"
            + "function SetShareStore(key,value)"
            + " {var serverValue = document.getElementById('__ShareStore').value;"
            + "   serverValue += key+','+value+';'}"
            + "</script>";

            ClientScript.RegisterHiddenField("__ShareStore", "");

            this.RegisterJS("SetServerValue", js);
        }

        protected void SetShareStore(string key, string value)
        {
            //to do...
        }

        /// <summary>
        /// 是否服务端刷新页面
        /// 该属性是一次性的使用，每次页面呈现之前该值会被置为false
        /// </summary>
        public bool IsRefreshPage
        {
            get
            {
                if (ViewState["BasePage_IsRefreshPage"] == null)
                    ViewState["BasePage_IsRefreshPage"] = false;
                return (bool)ViewState["BasePage_IsRefreshPage"];
            }
            set
            {
                ViewState["BasePage_IsRefreshPage"] = value;
            }
        }

        /// <summary>
        /// 获取客户端赋予的服务端变量值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string GetShareStore(string key)
        {
            string serverValue = Request.Form["__ShareStore"].ToString();
            if (string.IsNullOrEmpty(serverValue)) return "";
            if (serverValue.IndexOf(',') < 0) return "";
            if (serverValue.IndexOf(';') < 0) serverValue += ";";

            string[] keyValues = serverValue.Split(';');
            foreach (string keyValue in keyValues)
            {
                if (key == keyValue.Split(',')[0])
                    return keyValue.Split(',')[1];
            }
            return "";
        }

        /// <summary>
        /// 通过后台代码设置web界面上的html控件的可见性
        /// </summary>
        /// <param name="elementID"></param>
        /// <param name="visible"></param>
        protected void SetClientElementVisible(string elementID, bool visible)
        {
            string js = "<script language='javascript'>"
            + " SetClientElementVisible('" + elementID + "'," + visible + ");"
            + "</script>";

            this.RegisterJS("SetClientElementVisible", js);
        }

        /// <summary>
        /// 注册页面消息框
        /// </summary>
        public void RegisterMessageBox()
        {
            if (this.GetMasterForm() == null) return;
            ((Panel)(this.GetMasterForm().FindControl("pnlMessage"))).Visible = false;
            if (!string.IsNullOrEmpty(Request.Form["__ShareStore"]))
            {
                this.Message = Request.Form["__ShareStore"];
            }
            //添加信息提示
            if (!string.IsNullOrEmpty(this.Message))
            {
                ((Panel)(this.GetMasterForm().FindControl("pnlMessage"))).Visible = true;
                ((Label)(this.GetMasterForm().FindControl("lblMessage"))).Text = this.Message;
            }

        }

        public HtmlForm GetMasterForm()
        {
            if (this.Master != null)
            {
                HtmlForm form = (HtmlForm)(this.Master.FindControl("Form1"));
                return form;
            }
            return null;
        }

        /// <summary>
        ///向页面注册一个linkbutton，留作页面刷新回发使用
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private void RegisterPostBackLinkButton()
        {
            if (this.GetMasterForm() == null) return;

            PlaceHolder plhTop = (PlaceHolder)(this.GetMasterForm().FindControl("plhTopContainer"));
            if (plhTop == null) return;
            LinkButton lb = new LinkButton();
            //try
            //{

            lb.ID = BasePage.PostBackControlID;
            lb.Text = "";

            lb.Click += new EventHandler(PostBack_Click);
            plhTop.Controls.Add(lb);
            //}
            //finally
            //{
            //    lb.Dispose();
            //}

        }

        protected void PostBack_Click(object sender, EventArgs e)
        {
            OnReFreshPostBack();
        }

        /// <summary>
        /// 当postback控件触发回发后，子类需要重写该方法才能实现页面回发刷新效果。
        /// 比如可以调用search方法来实现重新查询
        /// </summary>
        protected virtual void OnReFreshPostBack()
        {
        }

        /// <summary>
        /// 关闭当前虚拟窗口
        /// 如果当前窗口是使用window.open()的方式弹出的系统内置窗口
        /// 则不适用该方法
        /// </summary>
        /// <param name="isRefreshPage">是否需要刷新父页面</param>
        protected virtual void CloseVirtualWindow(bool needRefreshPage)
        {
            if (needRefreshPage)
            {
                string strScript = "<script>"
                + "var lb=window.parent.frames['frmMain'].document.getElementById('ctl00_lbPostBack');"
                + " if(lb!=null) { "
                + " lb.click();window.parent.CloseDialogDev('" + this.Message + "'); }"
                + "</script>";
                this.RegisterJS("Base_PostBack_JS", strScript);
            }
            else
            {
                string strScript = "<script language='javascript'>"
                + "window.parent.CloseDialogDev('" + this.Message + "');"
                + "</script>";
                this.RegisterJS("Base_CloseWindow_JS", strScript);
            }
        }

        public new void SetFocus(string clientID)
        {
            string script = "<script language='javascript'>SetFocus('" + clientID + "');</script>";
            this.RegisterJS("BasePage_SetFocus", script);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="context"></param>
        public void DownLoadFile(string fileName, bool isWebPath)
        {
            if (isWebPath)
                fileName = System.IO.Path.Combine(Infrustructure.Utilities.WebUtil.GetExportFolder(), fileName);
            string realFileName = fileName.Substring(fileName.LastIndexOf('\\') + 1);
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + HttpUtility.UrlEncode(realFileName));
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            //HttpContext.Current.Response.ContentType = "text/csv";
            //HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Unicode;
            HttpContext.Current.Response.TransmitFile(fileName);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        #region async handler

        protected string OutputFilePath
        {
            set
            {
                //ScriptManager.RegisterStartupScript(this.Panel1, typeof(UpdatePanel), "downloadfile", string.Format("window.open('{0}');", value), true);
                //ScriptManager.RegisterStartupScript(this, typeof(BasePage), "downloadfile", string.Format("window.location.href='{0}';", value), true);

                DownLoadFile(value, true);
            }
        }

        protected string Information
        {
            set
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(BasePage), "showMessage",
                                                        string.Format(
                                                            "$get('InfoDiv').style.display = 'block';$get('InfoMessage').innerHTML='{0}';",
                                                            value), true);
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        protected string HtmlEncode(string text)
        {
            return this.Server.UrlEncode(text);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public string GetUrlPath(System.Uri Url)
        {
            string path = Url.AbsolutePath;
            if (path.Length > 0)
            {
                int i = path.LastIndexOf('/') + 1;
                if (i < path.Length)
                {
                    return path.Substring(i);
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取枚举的描述信息
        /// </summary>
        /// <param name="en">枚举</param>
        /// <returns></returns>
        public static string GetEnumDes(Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;


            }

            return en.ToString();

        }
        public static T XmlToObject<T>(string xml)
        {
            using (StringReader reader = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
        }


    }
}
