#region File Comment
/*+-------------------------------------------------------------------+
//+ Name: 	   WebService安全扩展
//+ Function:   通过该扩展，增强Webservice的安全认证功能。任何客户端必须拥有与服务端
//                      相对应的密文，否则，将不能调用webservice。
//                      使用方法：1.在服务端的webmethod上加上两个属性：
//                                    [AuthExtension]
//                                    [SoapHeader ("Credentials", Required=true)]
//                                      2.客户端调用的时候，实例化webservice对象时，加入认证head，如：
                                        WebService ws = new WebService ();
                                        AuthHeader Credentials = new AuthHeader ();
                                        Credentials.SecurityUP = "...这里是认证的密文...";
                                        ws.AuthHeaderValue     = Credentials;
//+ Author:        xuehaijun
//+ Date:           20060702       
//+-------------------------------------------------------------------+
//+ Change History:
//+ Date            Who       		Chages Made        Comments
//+-------------------------------------------------------------------+
//+ 20060702         CodeGenerator        Init Created
//+-------------------------------------------------------------------+
//+-------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace Infrustructure.Utilities
{

    /// <summary>
    /// 扩展的SOAP认证头
    /// </summary>
    public class AuthHeader : SoapHeader
    {
        /// <summary>
        /// 安全的认证密文，目前暂定"用户名+密码"的加密字符串
        /// </summary>
        public string SecurityUP;
    }

	[Obsolete("Never be used.")]
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthExtensionAttribute : SoapExtensionAttribute
    {
        int _priority = 1;

        public override int Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        public override Type ExtensionType
        {
            get { return typeof(AuthExtension); }
        }
    }

    public class AuthExtension : SoapExtension
    {
        public override void ProcessMessage(SoapMessage message)
        {
            if (message.Stage == SoapMessageStage.AfterDeserialize)
            {
                //Check for an AuthHeader containing valid
                //credentials
                foreach (SoapHeader header in message.Headers)
                {
                    if (header is AuthHeader)
                    {
                        AuthHeader credentials = (AuthHeader)header;
                        //解密认证密文
                        string secUP = CryptTools.Decrypt(credentials.SecurityUP, CryptConstants.PassKey);
                        //这里先简单的通过字符串来认证
                        if(secUP.Contains("+"))
                            return; // Allow call to execute
                        break;
                    }
                }

                // Fail the call if we get to here. Either the header
                // isn't there or it contains invalid credentials.
                throw new SoapException("Unauthorized", SoapException.ClientFaultCode);
            }
        }

        public override Object GetInitializer(Type type)
        {
            return GetType();
        }

        public override Object GetInitializer(LogicalMethodInfo info,
            SoapExtensionAttribute attribute)
        {
            return null;
        }

        public override void Initialize(Object initializer)
        {
        }
    }
}
