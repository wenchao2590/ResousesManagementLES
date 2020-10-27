//-------------------------------------------------------------------
//版权所有：版权所有(C) 2006，Microsoft(China) Co.,LTD
//系统名称：GMCC-ADC
//文件名称：MPSBusinessException
//模块名称：
//模块编号：
//作　　者：YOOPAN
//完成日期：11/03/2006 14:53:56
//功能说明：
//-----------------------------------------------------------------
//修改记录：
//修改人：      
//修改时间：
//修改内容：
//-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;



namespace Infrustructure.Utilities.Exception
{
    [Serializable]
	public class MPSBusinessException : BaseException
    {
        public MPSBusinessException()
        { }

        //一个异常消息参数和一个异常错误类。
        public MPSBusinessException(string code, System.Exception innerException)
            : base(code, innerException)
        {

        }


        //一个异常编码参数和一个异常错误类。
        public MPSBusinessException(string code)
            : base(code)
        {


        }

        public MPSBusinessException(string errMsg,int exceptionLevel)
        {
            if (errMsg != string.Empty)
            {
                if (exceptionLevel == 1)
                {
                    
                }
            }
                
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override string GetMessage(string code)
        {
            string msg = base.GetMessage(code);
            if (string.IsNullOrEmpty(msg))
            {
                msg = "出现业务规则冲突";
            }

            return msg;
        }
    }
}
