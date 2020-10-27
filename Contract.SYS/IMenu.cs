using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Contract.SYS
{
    public interface IMenu
    {
        [OperationContract]
        MenuInfo GetInfo(long id);
    }
}
