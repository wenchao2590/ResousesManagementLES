using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.SYS
{
    public class InterfaceReturnInfo
    {

        /// <summary>
        /// 返回结果 0 是失败, 1是成功 (必填)
        /// </summary>
        private int executeResult;
        /// <summary>
        /// 错误信息码(可以为空)
        /// </summary>
        private string errorCode;
        /// <summary>
        /// 返回信息内容(可以空)
        /// </summary>
        private string messageContent;

        public int ExecuteResult
        {
            get { return executeResult; }
            set { executeResult = value; }
        }
        

        public string ErrorCode
        {
            get { return errorCode; }
            set { errorCode = value; }
        }
        

        public string MessageContent
        {
            get { return messageContent; }
            set { messageContent = value; }
        }
    }
}
