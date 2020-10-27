using BLL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace UI.WEB.COMMON
{
    public class Global : HttpApplication
    {
        public static List<HandlerInfo> handlers = new List<HandlerInfo>();
        /// <summary>
        /// 应用程序部署路径
        /// </summary>
        public static string mapPath = string.Empty;

        private void LoadHandler()
        {
            ///加载路由
            handlers = new HandlerBLL().GetHandlers();
            foreach (var handler in handlers)
            {
                handler.ClassObject = DataCommon.GetClassObject(handler.AssemblyName, handler.ClassName);
                if (handler.ClassObject == null) continue;
                handler.Method = handler.ClassObject.GetType().GetMethod(handler.ServerMethodName);
            }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            if (handlers.Count <= 0)
                LoadHandler();
            ///应用程序部署路径
            ///mapPath = new ConfigBLL().GetValueByCode("IIS_APPLICATION_PROGRAM_MAP_PATH");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            if (handlers.Count > 0)
                handlers.Clear();
        }

        private static void SaveObjToCache(string key, object value)
        {
            if (getCache(key) == null)
            {
                HttpRuntime.Cache.Insert(key, value);
            }
            else
            {
                HttpRuntime.Cache[key] = value;
            }
        }

        private static object getCache(string key)
        {
            return HttpRuntime.Cache[key];
        }

        private static void RemoveCache(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

    }
}