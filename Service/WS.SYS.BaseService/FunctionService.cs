using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS.SYS.BaseService
{
    public class FunctionService : IUser, IMenu
    {
        public bool Login(string userName, string passWord)
        {
            return true;
        }
    }
}
