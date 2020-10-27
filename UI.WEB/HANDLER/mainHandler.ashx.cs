namespace UI.WEB.HANDLER
{
    using DM.SYS;
    using Infrustructure.Data;
    using System;
    using System.Reflection;
    using System.Web;
    using System.Web.SessionState;
    using UI.WEB.COMMON;
    using System.Linq;
    using BLL.SYS;

    /// <summary>
    /// Summary description for mainHandler
    /// </summary>
    public class mainHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string strReturn = string.Empty;
            HandlerInfo handlerinfo = null;
            context.Response.ContentType = "text/plain";
            ///调用的方法名
            string methodType = context.Request["method"];
            ///实体名
            string entityName = context.Request["ENTITY_NAME"];
            ///登录方法不验证SESSION
            if (methodType == "ajaxUserLogin")
            {
                try
                {
                    strReturn = HandlerCommon.Login(context);
                }
                catch (Exception ex)
                {
                    strReturn = HandlerCommon.ExceptionMessage(ex);
                    if (!strReturn.StartsWith("Err_:"))
                        strReturn = "Err_:" + strReturn;
                }
                context.Response.Write(strReturn);
                return;
            }
            else if (entityName == "InitMessage")
            {
                handlerinfo = Global.handlers.FirstOrDefault(d => d.AjaxMethodName == methodType);
                if (handlerinfo == null)
                {
                    context.Response.Write(JsonHelper.ToJson("Err_:SessionIsNull"));
                    return;
                }
                strReturn = handlerinfo.Method.Invoke(handlerinfo.ClassObject, new object[] { context }).ToString();
            }
            else if (methodType.StartsWith("public"))
            {
                methodType = methodType.Replace("public", string.Empty);
                handlerinfo = Global.handlers.FirstOrDefault(d => d.AjaxMethodName == methodType);
                if (handlerinfo == null)
                {
                    context.Response.Write(JsonHelper.ToJson("Err_:SessionIsNull"));
                    return;
                }
                #region 调用后台函数
                try
                {
                    strReturn = handlerinfo.Method.Invoke(handlerinfo.ClassObject, new object[] { context }).ToString();
                }
                catch (Exception ex)
                {
                    HandlerCommon.ExceptionMessage(ex);
                }
                #endregion
            }
            else
            {
                if (context.Session["UserFid"] == null)
                {
                    context.Response.Write(JsonHelper.ToJson("Err_:SessionIsNull"));
                    return;
                }
                if (string.IsNullOrEmpty(context.Session["UserFid"].ToString()))
                {
                    context.Response.Write(JsonHelper.ToJson("Err_:SessionIsNull"));
                    return;
                }
                try
                {
                    ///记录日志
                    HandlerCommon.WriteOperationLog(context);
                }
                catch (Exception ex)
                {
                    context.Response.Write(ex.Message);
                    return;
                }

                if (methodType.IndexOf("-") != -1)
                    methodType = "ajaxDefaultCommon";
                handlerinfo = Global.handlers.FirstOrDefault(d => d.AjaxMethodName == methodType);
                if (handlerinfo == null)
                {
                    context.Response.Write(JsonHelper.ToJson("Err_:SessionIsNull"));
                    return;
                }
                #region 调用后台函数
                try
                {
                    strReturn = handlerinfo.Method.Invoke(handlerinfo.ClassObject, new object[] { context }).ToString();
                }

                catch (Exception ex)
                {
                    strReturn = HandlerCommon.ExceptionMessage(ex);
                    if (!strReturn.StartsWith("Err_:"))
                        strReturn = "Err_:" + strReturn;
                }
                #endregion
            }
            context.Response.Write(strReturn);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}