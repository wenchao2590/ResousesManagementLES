using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WS.SYS.BaseService
{
    [ServiceContract]
    public interface IUser
    {
        [OperationContract]
        bool Login(string userName, string passWord);
    }
}
