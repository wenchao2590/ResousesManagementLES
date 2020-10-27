using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Infrustructure
{
    public static class Config
    {
        /// <summary>
        /// 是否只运行一个实例
        /// </summary>
        public static Boolean RunOneInstance
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["runOneInstance"]); }
        }
    }
}
