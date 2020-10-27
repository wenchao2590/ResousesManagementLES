using System;
using System.Collections.Generic;
using System.Text;

namespace Infrustructure.Utilities.Exception
{
    [Serializable]
	public class AuthenticationException : BaseException
    {
          public AuthenticationException()
        { }

        //一个异常消息参数和一个异常错误类。
        public AuthenticationException(System.Exception innerException)
            : base(innerException)
        {
          
        }

        public AuthenticationException(string code, System.Exception innerException)
            : base(code, innerException)
        { 
          
        }

        //一个异常编码参数和一个异常错误类。
        public AuthenticationException(string code)
            : base(code)
        {
           

        }

        protected override string GetMessage(string code)
        {
            string msg = base.GetMessage(code);
            if (string.IsNullOrEmpty(msg))
            {
                msg = "出现权限异常,您无权访问此功能,请与管理员联系";
            }

            return msg;
        }
    }
}
