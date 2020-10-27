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
using System.Web.SessionState;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel;

using Infrustructure.BaseClass;
using Infrustructure.Utilities;

namespace Infrustructure.Service
{
    /// <summary>
    /// 用来负责从客户端向服务器端，通过头传递信息
    /// </summary>    
    public sealed class ClientMessageInspector : IClientMessageInspector, IDispatchMessageInspector, IEndpointBehavior
    {
        public readonly static string DefaultHeadNamespace = "MES.Foton.com";
        
        private static Dictionary<string, string> mHeadToServer = new Dictionary<string, string>();
        private static Dictionary<string, string> mHeadFromServer = new Dictionary<string, string>();

        //
        internal ClientMessageInspector()
        {
            string userName = "";

            if (System.Web.HttpContext.Current != null)
            {
                if (System.Web.HttpContext.Current.Session[BasePage.SessionKey_CurrentUser] != null)
                {
                    userName = ((IUser)System.Web.HttpContext.Current.Session[BasePage.SessionKey_CurrentUser]).UserLoginName;
                }
            }

            this.HeadToServer.Clear();

            //添加登录用户名到MessageHeader中，未来可根据需要将其他信息放入Header
            this.HeadToServer.Add("UserName", userName);
            this.HeadToServer.Add("IsLocal", "YES");
           
        }

        //终端客户端使用
        public ClientMessageInspector(string userName, string password)
        {          
            //在客户端发送前添加认证信息
                       
            userName = CryptTools.Encrypt(userName, CryptConstants.PassKey);
            password = CryptTools.Encrypt(password, CryptConstants.PassKey);

            this.HeadToServer.Clear();

            this.HeadToServer.Add("UserName", userName);
            this.HeadToServer.Add("Password", password);

        }

        /// <summary>
        /// 要发送到服务器端的内容
        /// </summary>
        public Dictionary<string, string> HeadToServer
        { get { return mHeadToServer; } }


        /// <summary>
        /// 在WCF服务器端获得头信息，如果没有指定的key，则返回null。
        /// </summary>
        /// <param name="name">关键字，如果没有则返回空串</param>
        /// <returns></returns>
        public static string GetHeadContent(string key)
        {
            if (OperationContext.Current == null)
                return "";

            int index = OperationContext.Current.IncomingMessageHeaders.FindHeader(key, DefaultHeadNamespace);
            if (index < 0) return null;
            string val = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>(index);
            return val;
        }


        #region IClientMessageInspector 成员
        
        
        void IClientMessageInspector.AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {

        }

        object IClientMessageInspector.BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            foreach (var item in this.HeadToServer)
            {
                MessageHeader mh = MessageHeader.CreateHeader(item.Key, DefaultHeadNamespace, item.Value);
                request.Headers.Add(mh);
            }
            return null;
        }

        #endregion


        #region IDispatchMessageInspector 成员

        /// <summary>
        /// 在服务器端接受到客户端消息派发之前截获，做安全性验证之类的处理
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="correlationState"></param>
        object IDispatchMessageInspector.AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            string userName, passWord, isLocal;

            
            userName = GetHeadContent("UserName");
            passWord = GetHeadContent("Password");
            isLocal = GetHeadContent("IsLocal");

            //LocalCall
            if (isLocal == "YES")
                return null;

            string[] strs = request.Headers.Action.Split('/');
            string requestMethod = strs[strs.Length - 1];

            //当请求为如下方法时，不预先截获消息进行验证
            if (requestMethod.ToLower() == "loginin")
                return null;

            if (string.IsNullOrEmpty(userName) ||
                string.IsNullOrEmpty(passWord))
            {
                throw new ArgumentNullException();            
            }

           
            //解密用户信息
            userName = CryptTools.Decrypt(userName, CryptConstants.PassKey);
            
            //验证用户信息
            //string message;
            //SupplierUser user = SupplierUser.Sign(userName, passWord, out message);
            //if (user == null) 
            //    throw new System.Exception(message);
            return null;
        }

        /// <summary>
        /// 在发送响应之间截获
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="correlationState"></param>
        void IDispatchMessageInspector.BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            
        }

        #endregion

        #region IEndpointBehavior 成员

        void IEndpointBehavior.AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {

        }

        void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(this);
        }

        void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(this);
        }

        void IEndpointBehavior.Validate(ServiceEndpoint endpoint)
        {

        }

        #endregion
    }
}
