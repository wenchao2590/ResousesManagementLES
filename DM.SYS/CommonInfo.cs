using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DM.SYS
{
    public partial class CommonInfo
    {
        /// <summary>
        /// 数据库默认时间
        /// </summary>
        public static DateTime SqlDefaultTime { get; } = new DateTime(1900, 1, 1);
    }
}
