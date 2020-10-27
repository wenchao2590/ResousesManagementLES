using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WS.VMI.InboundDataService
{
    /// <summary>
    /// 返回结果状态
    /// </summary>
    public enum ResultsDescribed
    {
        /// 1标识成功，0代表错误
        /// <summary>
        /// 返回成功
        /// </summary>
        [Description("返回成功")]
        Succeed = 1,
        /// <summary>
        /// 返回失败
        /// </summary>
        [Description("返回失败")]
        Failure = 0,
       
    }
}