using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrustructure.Print
{
    /// <summary>
    /// JIS打印配置信息
    /// </summary>
    class JISPrintConfigInfo
    {
        public int PrintID
        {
            get;
            set;
        }

        //起始行
        public int StartRow
        {
            get;
            set;
        }

        //结束行
        public int EndRow
        {
            get;
            set;
        }

        //字体大小
        public string FontSize
        {
            get;
            set;
        }

    }
}
