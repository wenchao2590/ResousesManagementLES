using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using LES_PDA.Forms;

namespace LES_PDA
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [MTAThread]
        static void Main()
        {
            Application.Run(new FrmLogin());
        }
        /// <summary>
        /// 登录用户
        /// </summary>
        public static AuthorityUser mAppUser = null;
    }
}