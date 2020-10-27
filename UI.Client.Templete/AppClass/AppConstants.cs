using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace UI.Client.Templete.AppClass
{
    public class AppConstants
    {
        public enum MessageTypeStatus
        {
            [Description("错误信息")]
            Error=5,
            [Description("提示信息")]
            Message=10
        }
    }
}
