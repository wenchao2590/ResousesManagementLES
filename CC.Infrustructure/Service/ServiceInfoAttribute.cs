using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infrustructure.Service
{
    /// <summary>
    /// 定制属性类,用于在接口上声明缺省程序类型、服务名
    /// 缺省程序类型，指默认处理该契约的程序
    /// 服务名,指实现接口的类型名,格式: typename,assemblyname
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
    public class ServiceInfoAttribute : Attribute
    {
        public ServiceInfoAttribute(string defaultService, string serviceName)
        {
            DefaultService = defaultService;
            ServiceName = serviceName;
        }

        public ServiceInfoAttribute(string defaultService)
            : this(defaultService, "")
        {
        }

        /// <summary>
        /// 缺省类型，用于Local Call
        /// </summary>
        public string DefaultService
        {
            get;
            set;
        }

        /// <summary>
        /// 服务名，用于Local Call
        /// </summary>
        public string ServiceName
        {
            get;
            set;
        }


    }
}