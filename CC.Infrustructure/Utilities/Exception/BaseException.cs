using System;
using System.Collections.Generic;
using System.Text;

namespace Infrustructure.Utilities.Exception
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2240:ImplementISerializableCorrectly"), Serializable]
	public class BaseException : ApplicationException
    {
         public BaseException()
        { }

        //异常编码
        private string m_code;

        // 异常编码
        public virtual string Code
        {
            get
            {
                return m_code;
            }
            set
            {
                m_code = value;
            }
        }

       private string m_Message;
       public override string Message
       {
           get
           {
               return m_Message;
           }
       }

        //一个异常消息参数和一个异常错误类。
        public BaseException(System.Exception innerMessage)
            : base("",innerMessage)
        {


        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BaseException(string code, System.Exception innerException)
            : base("", innerException)
       {
           if (string.IsNullOrEmpty(code))
           {
               throw new ArgumentException("异常编码code不能为空");
           }

           m_code = code;
           m_Message = GetMessage(code);
         
       }

        public BaseException(string code)
            : this(code, null)
       {
          

        
       }


       protected virtual string GetMessage(string code)
       {
           //if (ExceptionRecs.ResourceManager != null)
           //{
           //    string errorMsg = ExceptionRecs.ResourceManager.GetString(code);
           //    if (string.IsNullOrEmpty(errorMsg)) return code;
           //    return errorMsg;
           //}

           if (InnerException != null)
           {
               return InnerException.Message;
           }

           //return "出现{0}异常!";
           return code;
       }

        //调用此方法可以得到异常消息页面的文件路径
        public virtual string GetMessageFile()
        {
            //IDictionary idFilePath = (IDictionary)ConfigurationSettings.GetConfig("FilePath");
            //string filePath = (string)idFilePath["settingPath"];
            string filePath = "asdfasd";
            return filePath;
        }


    }
}
