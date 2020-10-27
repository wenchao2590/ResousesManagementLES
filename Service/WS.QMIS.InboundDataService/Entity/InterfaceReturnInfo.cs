using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WS.QMIS.InboundDataService
{
    public class InterfaceReturnInfo
    {

        /// <summary>
        /// 返回结果 0 是失败, 1是成功 (必填)
        /// </summary>
        public int RESULT;
        /// <summary>
        /// 错误信息码(可以为空)
        /// </summary>
        public string MSGNO;
        /// <summary>
        /// 返回信息内容(可以空)
        /// </summary>
        public string MSG;

         
    }
}
