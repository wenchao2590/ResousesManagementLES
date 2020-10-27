using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Infrustructure
{
    [DataContract]
    public class OperationResult
    {
        public OperationResult()
        {
            Result = 0;
            Message = "";
        }


        /// <summary>
        /// 操作结果代码
        /// Result=0，表示成功；<0 表示失败
        /// </summary>
        [DataMember]
        public int Result
        {
            get;
            set;
        }

        /// <summary>
        /// 操作结果信息
        /// </summary>
        [DataMember]
        public string Message
        {
            get;
            set;
        }
    }
}
