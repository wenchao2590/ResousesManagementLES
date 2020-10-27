#region File Comment
/*+-------------------------------------------------------------------+
//+ Name: 	   WebService��ȫ��չ
//+ Function:   ͨ������չ����ǿWebservice�İ�ȫ��֤���ܡ��κοͻ��˱���ӵ��������
//                      ���Ӧ�����ģ����򣬽����ܵ���webservice��
//                      ʹ�÷�����1.�ڷ���˵�webmethod�ϼ����������ԣ�
//                                    [AuthExtension]
//                                    [SoapHeader ("Credentials", Required=true)]
//                                      2.�ͻ��˵��õ�ʱ��ʵ����webservice����ʱ��������֤head���磺
                                        WebService ws = new WebService ();
                                        AuthHeader Credentials = new AuthHeader ();
                                        Credentials.SecurityUP = "...��������֤������...";
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
    /// ��չ��SOAP��֤ͷ
    /// </summary>
    public class AuthHeader : SoapHeader
    {
        /// <summary>
        /// ��ȫ����֤���ģ�Ŀǰ�ݶ�"�û���+����"�ļ����ַ���
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
                        //������֤����
                        string secUP = CryptTools.Decrypt(credentials.SecurityUP, CryptConstants.PassKey);
                        //�����ȼ򵥵�ͨ���ַ�������֤
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
