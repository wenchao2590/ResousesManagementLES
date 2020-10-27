using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;

namespace Infrustructure.Service
{

    /// <summary>
    /// 根据暴露接口类型和Binding类型获取服务相对路径
    /// </summary>
    public interface IServiceAddressing
    {
        /// <summary>
        /// 获取服务的绝对地址
        /// </summary>
        /// <param name="baseAddress">基地址</param>
        /// <param name="serviceContractInterface">接口类型</param>
        /// <returns>服务绝对地址</returns>
        string GetAbsoluteAddress(string baseAddress, Type serviceContractInterface);

        /// <summary>
        /// 获取服务MEX的绝对地址
        /// </summary>
        /// <param name="baseAddress">基地址</param>
        /// <param name="serviceContractInterface"></param>
        /// <returns></returns>
        string GetMEXAbsoluteAddress(string baseAddress, Type serviceContractInterface);
    }

    /// <summary>
    /// 默认实现
    /// </summary>
    public class DefaultServiceAddressing : IServiceAddressing
    {

        public string GetAbsoluteAddress(string baseAddress, Type serviceContractInterface)
        {
            //return baseAddress;
            return String.Format("{0}/{1}", baseAddress, serviceContractInterface.FullName);
        }

        public string GetMEXAbsoluteAddress(string baseAddress, Type serviceContractInterface)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Host 在AppFabric中的默认实现
    /// </summary>
    public class DefaultAppFabricAddressing : IServiceAddressing
    {

        public string GetAbsoluteAddress(string baseAddress, Type serviceContractInterface)
        {

            return String.Format("{0}/{1}.svc", baseAddress, serviceContractInterface.FullName);
        }

        public string GetMEXAbsoluteAddress(string baseAddress, Type serviceContractInterface)
        {
            throw new NotImplementedException();
        }
    }
}
