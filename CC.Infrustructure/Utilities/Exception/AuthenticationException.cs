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

        //һ���쳣��Ϣ������һ���쳣�����ࡣ
        public AuthenticationException(System.Exception innerException)
            : base(innerException)
        {
          
        }

        public AuthenticationException(string code, System.Exception innerException)
            : base(code, innerException)
        { 
          
        }

        //һ���쳣���������һ���쳣�����ࡣ
        public AuthenticationException(string code)
            : base(code)
        {
           

        }

        protected override string GetMessage(string code)
        {
            string msg = base.GetMessage(code);
            if (string.IsNullOrEmpty(msg))
            {
                msg = "����Ȩ���쳣,����Ȩ���ʴ˹���,�������Ա��ϵ";
            }

            return msg;
        }
    }
}
