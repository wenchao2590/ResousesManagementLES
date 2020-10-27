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
    /// ҳ��Ļ��ࡣ ������ʾ���û���ҳ�����Ӵ˽�����м̳С�
    /// </summary>
    public class BasePage : System.Web.UI.Page
    {
        public const string SessionKey_CurrentUser = "SessionKey_CurrentUser";
        public const string PostBackControlID = "lbPostBack";

        public string ReportServerUrl = System.Configuration.ConfigurationManager.AppSettings["ReportServer"];
        public string ReportRoot = System.Configuration.ConfigurationManager.AppSettings["ReportRoot"];

        //��Լ����
        protected string ContractName = "";

        #region ��ԼĬ�Ϸ�����
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
        /// ҳ���ʼ��
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
        /// ҳ�����
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //�û�Ȩ�޼��
            IUser currentUser = UserUtil.GetCurrentUser();
            if (currentUser == null) return;

            //����û�������Դ�Ƿ���Ȩ��
            WebUtil.CheckUserRequestURL(currentUser, this.Response);
        }

        /// <summary>
        /// ��ǰ�û�
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
        /// ��ҳ�����javascript
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
        /// ����ҳ���쳣
        /// </summary>
        /// <param name="ex"></param>
        public void HandleException(Exception exp)
        {
            try
            {
                Exception ex = this.GetException(exp);

                //����ͳһ�����쳣���������Ҫ�ر�����쳣��������Ҫ���³������񣬾��������ﴦ��
                if (ex is MPSBusinessException || (ex.InnerException != null && ex.InnerException is MPSBusinessException))
                {
                    MPSBusinessException bEx = (MPSBusinessException)ex;

                    if (bEx == null)
                    {
                        bEx = (MPSBusinessException)ex.InnerException;

                    }
                    Logger.LogInfo("Infrustructure.BaseClass", "HandleException", "BasePage", "ҳ�洦���쳣" + ex.ToString());

                    this.Message = bEx.Message;
                }
                else if (ex is MESBusinessException)
                {
                    this.Message = ex.Message;
                }
                else if (ex is AuthenticationException)
                {
                    Logger.LogError("Infrustructure.BaseClass", "HandleException", "BasePage", "�û��Ự��ʧ�����û�û��Ȩ��", ex.InnerException);
                    Response.Redirect("~/default.aspx");
                }
                else if (ex is System.Threading.ThreadAbortException || ex is System.Web.HttpException)
                {
                }
                else if (ex is System.Data.SqlClient.SqlException && ex.Message.StartsWith("Invalid column name"))
                {
                    Logger.LogError("Infrustructure.BaseClass", "HandleException", "BasePage", "���ݿ��쳣", ex);
                }
                else
                {
                    Logger.LogError("Infrustructure.BaseClass", "HandleException", "BasePage", "ҳ�洦���쳣", ex);
                }

                if (ConfigurationManager.AppSettings["Trace"].ToLower() == "true")
                    this.Message = ex.Message;
                else
                {
                    //�����Ѻ���ʾ������Ϣ
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
                        // �����ҳ����תʱ��ֹ�߳��쳣���������κδ���, remove loginfo for release version
                        // Logger.LogInfo("Infrustructure.BaseClass", "HandleException", "BasePage", ex.ToString());                    
                    }
                    else
                        Logger.LogError("Infrustructure.BaseClass", "HandleException", "BasePage", "�������쳣", e);
                }
                catch { }
            }
        }

        private string _message;
        /// <summary>
        /// ���û���ʾ��Ϣ��ʾ
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
        /// ����Ƿ����ض���Ȩ��
        /// </summary>
        /// <param name="sec">��ȫѡ��</param>
        /// <returns></returns>
        public bool CheckPermissionSuccess()
        {
            if (this.CurrentUser == null)
                return false;
            return true;
        }
        ///<summary>
        /// ҳ��˵�PlaceHolder
        /// </summary>
        public System.Web.UI.WebControls.PlaceHolder plhTopHolder;
        public System.Web.UI.WebControls.PlaceHolder plhBottomHolder;

        /// <summary>
        /// �Է����������ɵĿؼ�������ȷ����֤�������Զ���renderʧ��
        /// </summary>
        /// <param name="control"></param>
        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }

        /// <summary>
        /// ҳ�淢������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BasePage_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            this.HandleException(ex);
        }

        /// <summary>
        /// ҳ��Ԥ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BasePage_PreRender(object sender, EventArgs e)
        {
            this.IsRefreshPage = false;

            //ע����Ϣ��
            this.RegisterMessageBox();
            //ע�����ؿؼ�
            this.RegisterHiddenBox();

        }

        protected override void CreateChildControls()
        {
            //ע��ҳ��ط�linkbutton
            this.RegisterPostBackLinkButton();
            base.CreateChildControls();

        }
        /// <summary>
        /// //��ÿ��ҳ��ע��һ�����صĿؼ������������Ϲ����洢�����ô洢���ڵ����ݸ�ʽ����
        ///��ѭ���¸�ʽ��key,value;key,value;
        ///��������ʹ��javascript��ÿؼ���ֵ ������SetServerValue();
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
        /// �Ƿ�����ˢ��ҳ��
        /// ��������һ���Ե�ʹ�ã�ÿ��ҳ�����֮ǰ��ֵ�ᱻ��Ϊfalse
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
        /// ��ȡ�ͻ��˸���ķ���˱���ֵ
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
        /// ͨ����̨��������web�����ϵ�html�ؼ��Ŀɼ���
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
        /// ע��ҳ����Ϣ��
        /// </summary>
        public void RegisterMessageBox()
        {
            if (this.GetMasterForm() == null) return;
            ((Panel)(this.GetMasterForm().FindControl("pnlMessage"))).Visible = false;
            if (!string.IsNullOrEmpty(Request.Form["__ShareStore"]))
            {
                this.Message = Request.Form["__ShareStore"];
            }
            //�����Ϣ��ʾ
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
        ///��ҳ��ע��һ��linkbutton������ҳ��ˢ�»ط�ʹ��
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
        /// ��postback�ؼ������ط���������Ҫ��д�÷�������ʵ��ҳ��ط�ˢ��Ч����
        /// ������Ե���search������ʵ�����²�ѯ
        /// </summary>
        protected virtual void OnReFreshPostBack()
        {
        }

        /// <summary>
        /// �رյ�ǰ���ⴰ��
        /// �����ǰ������ʹ��window.open()�ķ�ʽ������ϵͳ���ô���
        /// �����ø÷���
        /// </summary>
        /// <param name="isRefreshPage">�Ƿ���Ҫˢ�¸�ҳ��</param>
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
        /// �����ļ�
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
        /// ��ȡö�ٵ�������Ϣ
        /// </summary>
        /// <param name="en">ö��</param>
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
