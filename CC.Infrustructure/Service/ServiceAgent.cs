using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.Configuration;
using System.Xml;
using System.ServiceModel.Channels;

namespace Infrustructure.Service
{
    /// <summary>
    /// 简化开发者调用WCF的工作，此类继承IDisposable,在使用时
    /// 要包含在using块里，或者调用Close方法。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ServiceAgent<T> : IDisposable
    {
        #region Public Attributes
        /// <summary>
        /// 调用的服务的发布地址
        /// </summary>
        public string ServiceAddress
        {
            get
            {
                return _ServiceAddress;
            }
            set
            {
                _ServiceAddress = value;
                _ServiceEndpoint.Address = new EndpointAddress(_ServiceAddress);
            }
        }
        /// <summary>
        /// 要调用的服务
        /// </summary>
        public T Service
        {
            get
            {
                if (null == _Service)
                {
                    CreateService();
                }
                return _Service;
            }
        }

        #endregion

        /// <summary>
        /// 初始化被调用的服务点对象
        /// </summary>  
        public ServiceAgent()
            : this("")
        {
        }
        /// <summary>
        /// 初始化被调用的服务点对象
        /// </summary>
        /// <param name="bindingName">指定特定的Binding Name , 在addressing/binding下</param>
        public ServiceAgent(string bindingName)
        {
            var contractDescription = ContractDescription.GetContract(typeof(T));

            //TODO: handle exception like not found or configuration error
            KeyValuePair<string, Binding> hostService;
            if (String.IsNullOrEmpty(bindingName))
            {
                hostService = ServiceConfiguration.GetDefaultBindingConfiguration(typeof(T));
            }
            else
            {
                hostService = ServiceConfiguration.GetBindingConfiguration(bindingName, typeof(T));
            }

            _ServiceAddress = hostService.Key;
            _ServiceEndpoint = new ServiceEndpoint(contractDescription, hostService.Value, new EndpointAddress(hostService.Key));

            // Find the ContractDescription of the operation to find.
            foreach (OperationDescription operDesc in contractDescription.Operations)
            {
                // Find the serializer behavior.
                var serializerBehavior = operDesc.Behaviors.Find<DataContractSerializerOperationBehavior>();
                // If the serializer is not found, create one and add it.
                if (serializerBehavior == null)
                {
                    serializerBehavior = new DataContractSerializerOperationBehavior(operDesc);
                    operDesc.Behaviors.Add(serializerBehavior);
                }
                // Change the settings of the behavior.
                serializerBehavior.MaxItemsInObjectGraph = Int32.MaxValue - 1;
                serializerBehavior.IgnoreExtensionDataObject = true;
            }
        }

        /// <summary>
        /// 创建用户调用服务的接口
        /// </summary>
        /// <returns>返回一个服务对象，一般以接口形式返回</returns>
        private void CreateService()
        {
            _Factory = new ChannelFactory<T>(_ServiceEndpoint);

            if (this._Factory.Endpoint.Behaviors.Find<ClientMessageInspector>() == null)
            {
                this._Factory.Endpoint.Behaviors.Add(this.mClientMessageInspector);
            }

            _Service = _Factory.CreateChannel();
            IContextChannel contextChannel = (IContextChannel)_Service;

            //TODO: channel timeout
            TimeSpan ts = TimeSpan.FromMinutes(60);
            contextChannel.OperationTimeout = ts;
        }

        /// <summary>
        /// 获取要发往服务器的头信息
        /// </summary>
        public Dictionary<string, string> HeadToServer
        {
            get { return this.mClientMessageInspector.HeadToServer; }
        }


        /// <summary>
        /// 释放代理，如果客户端被包含在using块里，不需要调用；反之，需要。
        /// </summary>
        public void Close()
        {
            if (_Factory.State != CommunicationState.Closed && _Factory.State != CommunicationState.Closing)
            {
                try
                {
                    _Factory.Close();
                }
                catch
                {
                    //避免在通道错误时，拿不到正确的错误信息
                }
            }
        }

        /// <summary>
        /// 释放对象
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
        public void Dispose()
        {
            if (null != _Factory)
            {
                Close();
            }
        }


        #region Private Attributes

        private static ServiceConfiguration _ServiceConfiguration;

        private ServiceConfiguration ServiceConfiguration
        {
            get
            {
                if (_ServiceConfiguration == null)
                {
                    _ServiceConfiguration = ConfigurationManager.GetSection("ServiceConfiguration") as ServiceConfiguration;
                    _ServiceConfiguration.ResolveConfig();
                }
                return _ServiceConfiguration;
            }
        }

        //被调用的服务的发布地址
        private string _ServiceAddress;

        //被调用的服务点
        ServiceEndpoint _ServiceEndpoint;

        //本地和服务端的通道，T是要调用的服务接口的type.
        ChannelFactory<T> _Factory;

        //要调用的服务接口，如IUser       
        private T _Service = default(T);

        private ClientMessageInspector mClientMessageInspector = new ClientMessageInspector();

        #endregion
    }
}
