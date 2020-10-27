using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.ServiceModel.Channels;
using System.Configuration;
using System.Xml;
using System.ServiceModel;

namespace Infrustructure.Service
{
    /// <summary>
    /// 服务配置
    /// </summary>
    public class ServiceConfiguration
    {
        #region Public Attributes
        /// <summary>
        /// 调用类型
        /// </summary>
        public string CallType { get; set; }

        /// <summary>
        /// 本地程序集类型
        /// 第一个参数: 对应契约上的ServiceName
        /// 第二个参数：实现契约的类的类型，全名称
        /// </summary>
        public Dictionary<string, string> LocalService = new Dictionary<string, string>();

        /// <summary>
        /// 定位类型
        /// </summary>
        public string AddressingType { get; set; }

        /// <summary>
        /// 配置的Binding
        /// </summary>
        public List<BindingConfiguration> Bindings { get; set; }

        #endregion


        public void ResolveConfig()
        {
            var bindingsSection = ConfigurationManager.GetSection("system.serviceModel/bindings") as BindingsSection;

            foreach (var bindingConfiguration in Bindings)
            {
                bool found = false;
                foreach (var element in GetConfiguredBindingElement(bindingsSection))
                {
                    if (element.Name == bindingConfiguration.BindingElementName)
                    {
                        found = true;
                        bindingConfiguration.BindingElement = element;
                    }
                }
                if (!found)
                {
                    throw new ConfigurationErrorsException(String.Format("Address Binding:{0} is not properly configured", bindingConfiguration.Name));
                }
            }
        }

        private IEnumerable<IBindingConfigurationElement> GetConfiguredBindingElement(BindingsSection bindingsSection)
        {
            foreach (var collection in bindingsSection.BindingCollections)
            {
                foreach (var configuredElement in collection.ConfiguredBindings)
                {
                    yield return configuredElement;
                }
            }
        }

        public List<KeyValuePair<string, Binding>> GetAllBindingConfiguration(Type serivceContractInterface)
        {
            if (Bindings == null || Bindings.Count == 0)
            {
                throw new ConfigurationErrorsException("No address/binding configured");
            }
            List<KeyValuePair<string, Binding>> results = new List<KeyValuePair<string, Binding>>();
            foreach (var bindingConfiguration in Bindings)
            {
                results.Add(
                    new KeyValuePair<string, Binding>
                        (
                            GetAddressingInstance().GetAbsoluteAddress(bindingConfiguration.BaseAddress, serivceContractInterface)
                            , GetBinding(bindingConfiguration.BindingElement)
                        )
                    );
            }
            return results;
        }

        public KeyValuePair<string, Binding> GetDefaultBindingConfiguration(Type serivceContractInterface)
        {
            if (Bindings == null || Bindings.Count == 0)
            {
                throw new ConfigurationErrorsException("No address/binding configured");
            }
            return new KeyValuePair<string, Binding>
                (
                    GetAddressingInstance().GetAbsoluteAddress(Bindings[0].BaseAddress, serivceContractInterface)
                    , GetBinding(Bindings[0].BindingElement)
                );
        }

        public KeyValuePair<string, Binding> GetBindingConfiguration(string name, Type serivceContractInterface)
        {
            var bindingConfiguration = Bindings.AsQueryable().FirstOrDefault(b => b.Name == name);
            if (bindingConfiguration == null)
            {
                throw new ConfigurationErrorsException(String.Format("no address/binding with name:{0} found", name));
            }
            KeyValuePair<string, Binding> result = new KeyValuePair<string, Binding>
                (GetAddressingInstance().GetAbsoluteAddress(bindingConfiguration.BaseAddress, serivceContractInterface),
                 GetBinding(bindingConfiguration.BindingElement)) { };

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IServiceAddressing GetAddressingInstance()
        {
            if (serviceAddressInstance == null)
            {
                lock (lockSync)
                {
                    //TODO: TRY TO CATCH FAIL TO LOAD TYPE ERROR,OR MISMATCH TYPE
                    serviceAddressInstance =
                        (IServiceAddressing)Activator.CreateInstance(Type.GetType(AddressingType), null);
                }
            }
            return serviceAddressInstance;
        }

        private Binding GetBinding(IBindingConfigurationElement bindingElement)
        {
            Type type = bindingElement.GetType();
            Binding binding = null;
            if (type == typeof(BasicHttpBindingElement))
            {
                binding = new BasicHttpBinding();

            }
            if (type == typeof(WSHttpBindingElement))
            {
                binding = new WSHttpBinding();
            }

            if (type == typeof(NetTcpBindingElement))
            {
                binding = new NetTcpBinding();
            }

            if (type == typeof(NetNamedPipeBindingElement))
            {
                binding = new NetNamedPipeBinding();
            }

            //bug...
            bindingElement.ApplyConfiguration(binding);
            return binding;
        }

        private object lockSync = new object();
        private IServiceAddressing serviceAddressInstance = null;

        /// <summary>
        /// Binding 配置
        /// </summary>        
        public class BindingConfiguration
        {
            /// <summary>
            /// 名字
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 基类地址
            /// </summary>
            public string BaseAddress { get; set; }

            /// <summary>
            /// 对应配置的绑定名
            /// </summary>
            public string BindingElementName { get; set; }


            /// <summary>
            /// BindingElement
            /// </summary>
            public IBindingConfigurationElement BindingElement { get; set; }
        }
    }

    /// <summary>
    /// 服务配置Handler
    /// the hole configuration file should like
    ///<configuration>
    ///	<configSections>
    ///		<section name="ServiceConfig" type="MS.MCS.Framework.WCFClientProxy.ServiceConfigHandler, MS.MCS.Framework"/>
    ///	</configSections>
    ///	<serviceConfiguration>
    ///		<calltype value="localcall" />
    ///		<localcall>
    ///		    <service name="" type="">
    ///		</localcall>
    ///		<servicecall>
    ///		<!-- if user did not config type , use default address-->
    ///         <addressing type="MS.MCS.Framework.DefaultAddressing, MS.MCS.Framework">
    ///    <!-- when on the client only choose 1st binding is default binding , unless user specific choose by name -->
    ///             <binding type="net.tcp" name="default" baseAddress="net.tcp://hostname:3004/cc" bindingConfig="NetTcpBinding"/>
    ///  		    <binding type="basicHttp" name="bs" baseAddress="http://hostname/application" bindingConfig="basic"/>
    ///    		    <binding type="wsHttp" name="ws" baseAddress="http://hostname:800/application" bindingConfig="ws"/>
    ///    		    <binding type="net.pipe" name="pipe" baseAddress="net.pipe://hostname:9000/application" bindingConfig="basic"/>
    ///		    </addressing>
    ///		</servicecall>   
    ///	</serviceConfiguration>
    ///	<system.serviceModel>
    ///		<bindings>
    ///			<netTcpBinding>
    ///				<binding name="NetTcpBinding">
    ///					<security mode="None" />
    ///				</binding>
    ///				<binding name="Binding2" />
    ///			</netTcpBinding>
    ///			<basicHttpBinding>
    ///				<binding name="basic" maxReceivedMessageSize="2323442" />
    ///			</basicHttpBinding>
    ///			<wsHttpBinding>
    ///				<binding name="ws" maxReceivedMessageSize="3322334" />
    ///			</wsHttpBinding>
    ///			<netNamedPipeBinding>
    ///				<binding name="pipeline" />
    ///			</netNamedPipeBinding>
    ///		</bindings>
    ///	</system.serviceModel>
    ///</configuration>
    /// </summary>
    public class ServiceConfigurationHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            ServiceConfiguration configuration = new ServiceConfiguration();

            configuration.CallType =
                section.SelectSingleNode("calltype").Attributes["value"].Value;

            foreach (XmlNode node in section.SelectNodes("localcall/service"))
            {
                configuration.LocalService.Add(
                    node.Attributes["name"].Value,
                    node.Attributes["type"].Value);
            }

            configuration.AddressingType =
                section.SelectSingleNode("servicecall/addressing").Attributes["type"] == null
                    ? "MS.MCS.Framework.WCFService.WCFService.DefaultServiceAddressing, MS.MCS.Framework"
                    : section.SelectSingleNode("servicecall/addressing").Attributes["type"].Value;

            configuration.Bindings = new List<ServiceConfiguration.BindingConfiguration>();
            foreach (XmlNode node in section.SelectNodes("servicecall/addressing/binding"))
            {
                configuration.Bindings.Add(new ServiceConfiguration.BindingConfiguration()
                {
                    Name = node.Attributes["name"] == null
                            ? ""
                            : node.Attributes["name"].Value
                    ,
                    BaseAddress = node.Attributes["baseAddress"] == null
                           ? ""
                           : node.Attributes["baseAddress"].Value
                           ,
                    BindingElementName = node.Attributes["bindingConfig"] == null
                        ? ""
                        : node.Attributes["bindingConfig"].Value
                });
            }
            configuration.ResolveConfig();
            return configuration;
        }
    }

}
