using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Infrustructure.Service
{
    /// <summary>
    /// 该契约用于在WCF Service时实例化一个Host, 然后从该Host读取相应配置内容<system.serviceModel>
    /// </summary>
    [ServiceContract]
    public interface IServiceModelReader
    {
        [OperationContract]
        void DoNothing();
    }
}
