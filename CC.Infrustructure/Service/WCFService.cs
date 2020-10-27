using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;

namespace Infrustructure.Service
{
    /// <summary>
    /// 加载配置和创建服务
    /// </summary>
    public class WCFService
    {

        #region ====================================消息通知
        /// <summary>
        /// 向外部反应WCF启动的过程
        /// arg1:TraceLevel表示错误级别，只用到Error、Warn、Info
        /// arg2:具体信息
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly")]
        public event Action<TraceLevel, string> WcfServiceLunchNotifyEvent;



        private void OnLunchNotify(string msg, Exception ex)
        {
            if (WcfServiceLunchNotifyEvent == null) return;
            WcfServiceLunchNotifyEvent(TraceLevel.Error, msg + Environment.NewLine + ex.Message);
        }



        private void OnLunchNotify(TraceLevel traceLevel, string msg, params string[] args)
        {
            if (WcfServiceLunchNotifyEvent == null) return;
            WcfServiceLunchNotifyEvent(traceLevel, string.Format(msg, args));
        }



        private void OnLunchNotify(string msg)
        {
            if (WcfServiceLunchNotifyEvent == null) return;
            WcfServiceLunchNotifyEvent(TraceLevel.Info, msg);
        }
        #endregion



        private static WCFService _instance = null;
        static WCFService()
        {
        }



        /// <summary>
        /// 内部记录Host
        /// </summary>
        private List<ServiceHost> _hosts = new List<ServiceHost>();

        private Dictionary<Type, ServiceHost> interfaceImpMap;

        /// <summary>
        /// 停止服务
        /// </summary>
        public void Close()
        {
            foreach (ServiceHost host in _hosts)
            {
                if (host.State != CommunicationState.Opened) continue;
                try
                {
                    host.Close();
                }
                catch (System.Exception ex)
                {
                    OnLunchNotify("关闭服务" + host.Description.Name + "时出错：", ex);
                }
            }
        }



        /// <summary>
        /// 获取单一实例
        /// </summary>
        /// <returns></returns>
        public static WCFService GetInstance()
        {
            if (null == _instance)
            {
                _instance = new WCFService();
                return _instance;
            }
            else
            {
                return _instance;
            }
        }

        public static WCFService GetInstnace(string location)
        {
            if (_instance == null)
            {
                lock (syncObject)
                {
                    _instance = new WCFService(location);
                }
            }
            return _instance;
        }



        private WCFService()
        {
            _hosts = new List<ServiceHost>();
            interfaceImpMap = new Dictionary<Type, ServiceHost>();
        }

        private WCFService(string location)
        {

            _hosts = new List<ServiceHost>();
            interfaceImpMap = new Dictionary<Type, ServiceHost>();
            BuildServiceMap(location);
        }

        private void BuildServiceMap(string location)
        {
            //consider use mef to get implement type
            string[] files = Directory.GetFiles(location);
            GetServices(files, ref _hosts);
        }






        /// <summary>
        /// 启动服务，会查找当前程序域的目录下的所有 dll 文件中的类，如果类是一个实现了契约接口的类，则创建并启动一个 WCF 服务
        /// </summary>
        /// <returns>返回启动日志</returns>
        public string LunchService()
        {
            //OnLunchNotify("得到WCF地址配置文件路径为：" + GetXmlConfigLocation());

            StringBuilder serviceList = new StringBuilder();
            string hostLocation = Directory.GetParent(Assembly.GetEntryAssembly().Location).FullName;

            OnLunchNotify("得到Host所在路径为：" + hostLocation);

            GetServices(hostLocation, ref _hosts);
            OnLunchNotify("共构建Serivce Host数量：" + _hosts.Count.ToString());

            if (_hosts.Count == 0)
                return string.Empty;

            foreach (ServiceHost host in _hosts)
            {
                string hostInfo = host.Description.ServiceType.AssemblyQualifiedName;

                try
                {
                    host.Open();
                    OnLunchNotify("成功启动：" + hostInfo);
                }
                catch (Exception e)
                {
                    OnLunchNotify(TraceLevel.Warning, "启动服务时发生异常：" + hostInfo + Environment.NewLine + e.Message);
                }
            }

            string msg = "启动完成，基地址为" + Environment.NewLine;
            foreach (var bindingConfig in ServiceConfiguration.Bindings)
            {
                msg += String.Format("Binding Configuration Name:{0} BaseAddress:{1}", bindingConfig.Name,
                                     bindingConfig.BaseAddress) + Environment.NewLine;
            }



            OnLunchNotify(msg);

            return string.Empty;
        }


        /// <summary>
        /// 检查一个类定义中是否有WCF contract行为类型定义,如果有返回类型,否则返回null
        /// </summary>
        /// <param name="type">待检查的类型定义</param>
        /// <returns></returns>
        private bool CheckIsWCFContract(Type type)
        {
            object[] customAttributies = type.GetCustomAttributes(false);
            foreach (object att in customAttributies)
            {
                ServiceContractAttribute serviceContractAttribute = att as ServiceContractAttribute;
                if (null == serviceContractAttribute) continue; //it is not wcf contract

                return true;
            }
            return false;
        }


        private void GetServices(string location, ref List<ServiceHost> services)
        {
            string[] files = Directory.GetFiles(location, "*.dll", SearchOption.AllDirectories);

            GetServices(files, ref services);
        }



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private void GetServices(string[] files, ref List<ServiceHost> services)
        {
            Type[] typesInAssembly;
            foreach (string file in files)
            {
                //过滤掉框架程序集
                string fileName = Path.GetFileName(file);

                if (!fileName.StartsWith("YF.MES", StringComparison.CurrentCultureIgnoreCase) == true) continue;

                OnLunchNotify("Business DLL FileName：" + fileName);
                //只加载c#程序集合
                try
                {
                    typesInAssembly = Assembly.LoadFile(file).GetTypes();
                }
                catch (Exception ecp)
                {
                    OnLunchNotify("Exception：" + ecp.Message);
                    continue;
                }


                //建立ServiceModelReader Host
                ServiceHost rootHost = new ServiceHost(typeof(ServiceModelReader));

                #region 遍历程序集中的所有类型
                foreach (Type type in typesInAssembly)
                {
                    //不是类就不会有wcf contract接口
                    if (type.IsClass == false) continue;

                    Type[] interfacesOfType = type.GetInterfaces();
                    #region 遍历接口类型中的所有接口
                    foreach (Type wcfContractInterface in interfacesOfType)
                    {
                        try
                        {
                            if (false == CheckIsWCFContract(wcfContractInterface))
                            {
                                continue;
                            }

                            if (wcfContractInterface.FullName == "Infrustructure.Service.IServiceModelReader")
                                continue;

                            ////到此可以确定程序集中某类型是wcf contract的服务。
                            OnLunchNotify("WCFContractInterface：" + wcfContractInterface.Name);

                            ServiceHost serviceHost = BuildServiceHost(rootHost, type, wcfContractInterface);
                            services.Add(serviceHost);

                            interfaceImpMap[wcfContractInterface] = serviceHost;


                        } //end try
                        catch (Exception e)
                        {
                            string error = e.Message + Environment.NewLine + " Load '" + wcfContractInterface + "' error.";
                            Exception ex = new Exception(error, e);
                            OnLunchNotify("Exception：" + error);
                        }
                    }
                    #endregion
                }
                #endregion
            }

        }

        public ServiceHost GetAppFabricHost(Type interfaceType, Uri[] baseAddresses)
        {
            //TODO : Just ignore base address now
            if (!interfaceImpMap.ContainsKey(interfaceType))
            {
                throw new NotImplementedException("no implementation class found");
            }
            return interfaceImpMap[interfaceType];
        }



        #region Serivce Configuration
        private ServiceConfiguration serviceConfiguration = null;
        private static object syncObject = new object();

        ServiceConfiguration ServiceConfiguration
        {
            get
            {
                if (serviceConfiguration == null)
                {
                    lock (syncObject)
                    {
                        serviceConfiguration = ConfigurationManager.GetSection("ServiceConfiguration") as ServiceConfiguration;
                        if (serviceConfiguration == null)
                            throw new ConfigurationErrorsException("no serviceConfiguration configured");
                        serviceConfiguration.ResolveConfig();
                    }
                }
                return serviceConfiguration;
            }
        }
        #endregion

        /// <summary>
        /// 根据类型信息创建wcf service host
        /// </summary>
        /// <param name="hostType">实现接口的类</param>
        /// <param name="wcfContractInterface">实现的wcf contract</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private ServiceHost BuildServiceHost(ServiceHost rootHost, Type hostType, Type wcfContractInterface)
        {
            //准备服务,URI和metadataBehavior
            List<Uri> listUri = null;

            List<KeyValuePair<string, Binding>> bindingConfigs =
                ServiceConfiguration.GetAllBindingConfiguration(wcfContractInterface);


            //TODO: TRY TO CATCH BASEADDRESS CONFIG ERROR
            listUri = bindingConfigs.AsQueryable().Select(b => new Uri(b.Key)).ToList();


            //生成servviceHost实例
            ServiceHost serviceHost = new ServiceHost(hostType, listUri.ToArray());

            serviceHost.Credentials.ServiceCertificate.Certificate =
                rootHost.Credentials.ServiceCertificate.Certificate;
            serviceHost.Credentials.ClientCertificate.Certificate =
                rootHost.Credentials.ClientCertificate.Certificate;

            ServiceThrottlingBehavior stb = new ServiceThrottlingBehavior();
            stb.MaxConcurrentSessions = 1000;
            serviceHost.Description.Behaviors.Add(stb);

            //添加接口方法特性
            foreach (var binding in bindingConfigs.Select(c => c.Value))
            {
                ServiceEndpoint endpoint = null;

                if (binding is WSHttpBinding)
                {
                    WSHttpBinding wsHttpBinding = (WSHttpBinding)binding;

                    if (ConfigurationManager.AppSettings["EnableHttps"].ToLower() == "true")
                        wsHttpBinding.Security.Mode = SecurityMode.Transport;
                    else
                        wsHttpBinding.Security.Mode = SecurityMode.None;

                    wsHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;

                    endpoint = serviceHost.AddServiceEndpoint(wcfContractInterface, wsHttpBinding, "");

                    //添加元数据描述
                    ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                    if (ConfigurationManager.AppSettings["EnableHttps"].ToLower() == "true")
                    {
                        smb.HttpsGetEnabled = true;

                        //serviceHost.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpsBinding(), "mex");
                    }
                    else
                    {
                        smb.HttpGetEnabled = true;

                        //serviceHost.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");
                    }

                    //暴露元数据描述
                    AddMetadataBehavior(serviceHost, smb);
                }
                else
                {
                    endpoint = serviceHost.AddServiceEndpoint(wcfContractInterface, binding, "");
                }

                endpoint.Behaviors.Add(new ClientMessageInspector());

                AddOperationDescriptions(endpoint);
            }

            AddDebugBehavior(serviceHost);

            return serviceHost;
        }



        private void AddMetadataBehavior(ServiceHost serviceHost, ServiceMetadataBehavior serviceMetadataBehavior)
        {
            if (null == serviceMetadataBehavior) return;
            if (serviceHost.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
            {
                serviceHost.Description.Behaviors.Add(serviceMetadataBehavior);
            }
        }

        private void AddOperationDescriptions(ServiceEndpoint serviceEndpoint)
        {
            ContractDescription cd = serviceEndpoint.Contract;
            //逐一加载设置接口定义内的具体方法operDesc
            foreach (OperationDescription operDesc in cd.Operations)
            {
                // Find the serializer behavior.
                DataContractSerializerOperationBehavior serializerBehavior = operDesc.Behaviors.Find<DataContractSerializerOperationBehavior>();

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

        private void AddDebugBehavior(ServiceHost serviceHost)
        {
            string isSendErrorToClient = ConfigurationManager.AppSettings["SendErrorToClient"];

            ServiceDebugBehavior debugBehavior = serviceHost.Description.Behaviors.Find<ServiceDebugBehavior>();

            if (debugBehavior == null)
            {
                debugBehavior = new ServiceDebugBehavior();
                debugBehavior.IncludeExceptionDetailInFaults = isSendErrorToClient.ToLower() == "yes";

                serviceHost.Description.Behaviors.Add(debugBehavior);
            }
            else
            {
                debugBehavior.IncludeExceptionDetailInFaults = isSendErrorToClient.ToLower() == "yes";
            }
        }
    }
}
