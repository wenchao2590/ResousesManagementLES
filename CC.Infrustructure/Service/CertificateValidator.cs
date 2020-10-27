using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Infrustructure.Service
{
    public class CertificateValidator
    {
        /// <summary>
        /// 设置服务器证书验证方法
        /// </summary>
        public static  void SetCertificatePolicy()
        {
            ServicePointManager.ServerCertificateValidationCallback
                       += ServerCertificateValidate;
        }

        /// <summary>
        /// 验证服务器证书
        /// </summary>
        private static bool ServerCertificateValidate(
           object sender, X509Certificate cert,
            X509Chain chain, SslPolicyErrors error)
        {
           
            //TODO: 对服务器证书进行验证

            return true;
        }
    }
}
