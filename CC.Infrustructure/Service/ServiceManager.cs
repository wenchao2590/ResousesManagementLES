using System;
using System.Linq;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;


using Infrustructure.Utilities;

namespace Infrustructure.Service
{
    /// <summary>
    /// ServiceManager代理类
    /// 用于ServiceObjectSource, 可在ServiceObjectSource初始化时进行实例化
    /// </summary>
    public class ServiceManagerAgent
    {
        #region Public Properties

        /// <summary>
        /// 根据标识符查询数据方法名
        /// </summary>
        public string SelectMethodByID
        {
            get { return _SelectMethodByID; }
            set { if(!string.IsNullOrEmpty(value)) _SelectMethodByID = value;}
        }

        /// <summary>
        /// 根据条件查询数据方法名, 不分页
        /// </summary>
        public string SelectMethodByCondition
        {
            get { return _SelectMethodByCondition; }
            set { if(!string.IsNullOrEmpty(value)) _SelectMethodByCondition = value;}
        }

        /// <summary>
        /// 根据条件查询数据方法名, 分页
        /// </summary>
        public string SelectMethodByPageCondition
        {
            get { return _SelectMethodByPageCondition; }
            set { if(!string.IsNullOrEmpty(value)) _SelectMethodByPageCondition = value;}
        }

        /// <summary>
        /// 查询记录数方法名
        /// </summary>
        public string SelectCountMethod
        {
            get { return _SelectCountMethod; }
            set { if(!string.IsNullOrEmpty(value)) _SelectCountMethod = value;}
        }

        /// <summary>
        /// 更新方法名
        /// </summary>
        public string UpdateMethod
        {
            get { return _UpdateMethod; }
            set { if(!string.IsNullOrEmpty(value)) _UpdateMethod = value;}
        }

        /// <summary>
        /// 插入方法名
        /// </summary>
        public string InsertMethod
        {
            get { return _InsertMethod; }
            set { if(!string.IsNullOrEmpty(value)) _InsertMethod = value;}
        }

        /// <summary>
        /// 删除方法名
        /// </summary>
        public string DeleteMethod
        {
            get { return _DeleteMethod; }
            set { if(!string.IsNullOrEmpty(value)) _DeleteMethod = value;}
        }

         /// <summary>
        /// 批量删除方法名
        /// </summary>
        public string DeleteBatchMethod
        {
            get { return _DeleteBatchMethod; }
            set { if(!string.IsNullOrEmpty(value)) _DeleteBatchMethod = value;}
        }

        #endregion

        /// <summary>
        /// 构造,保存契约
        /// </summary>
        /// <param name="contractName"></param>
        /// <param name="selectMethod"></param>
        /// <param name="selectCountMethod"></param>
        public ServiceManagerAgent(string contractName)
        {
            ContractName = contractName;            
        }

        /// <summary>
        /// 根据ID获取数据
        /// </summary>
        /// <param name="ID">标识符</param>
        /// <returns></returns>
        public object Get(object[] primaryKey)
        {           
            Type contractType = TypeHelper.FindType(ContractName);
            Type serviceManagerType = typeof(Infrustructure.Service.ServiceManager<>).MakeGenericType(contractType);

            //生成ServiceManager对象
            object serviceManager = TypeHelper.CreateObject(serviceManagerType, serviceManagerType, true);
            PropertyInfo property = serviceManager.GetType().GetProperty("SelectMethodByID");
            if (property != null)
            {
                property.SetValue(serviceManager, SelectMethodByID, null);
            }

            return TypeHelper.Invoke(serviceManager, "Get", primaryKey);

        }

        /// <summary>
        /// 根据条件获取数据
        /// </summary>
        /// <param name="ID">查询条件</param>
        /// <returns></returns>
        public object GetByCondition(string condition)
        {
            Type contractType = TypeHelper.FindType(ContractName);
            Type serviceManagerType = typeof(Infrustructure.Service.ServiceManager<>).MakeGenericType(contractType);

            //生成ServiceManager对象
            object serviceManager = TypeHelper.CreateObject(serviceManagerType, serviceManagerType, true);
            PropertyInfo property = serviceManager.GetType().GetProperty("SelectMethodByCondition");
            if (property != null)
            {
                property.SetValue(serviceManager, SelectMethodByCondition, null);
            }

            return TypeHelper.Invoke(serviceManager, "SelectMethodByCondition", condition);

        }


        /// <summary>
        /// 根据条件获取数据
        /// </summary>
        /// <param name="ID">查询条件</param>
        /// <returns></returns>
        public object GetByCondition(string userID, Hashtable filterParameters)
        {
            Type contractType = TypeHelper.FindType(ContractName);
            Type serviceManagerType = typeof(Infrustructure.Service.ServiceManager<>).MakeGenericType(contractType);

            //生成ServiceManager对象
            object serviceManager = TypeHelper.CreateObject(serviceManagerType, serviceManagerType, true);
            PropertyInfo property = serviceManager.GetType().GetProperty("SelectMethodByCondition");
            if (property != null)
            {
                property.SetValue(serviceManager, SelectMethodByCondition, null);
            }

            object allParams = new object[2] { userID, filterParameters };

            return TypeHelper.Invoke(serviceManager, "GetListByCondition", allParams);

        }

        /// <summary>
        /// 根据条件获取分页数据
        /// </summary>
        /// <param name="textWhere">查询条件</param>
        /// <param name="orderText">排序</param>
        /// <param name="startRowIndex">每页第一行记录数</param>
        /// <param name="maximumRows">每页最大记录数</param>
        /// <returns></returns>
        public object GetByPageCondition(string textWhere, string orderText, int startRowIndex, int maximumRows)
        {
            //生成ServiceManager类型         
            Type contractType = TypeHelper.FindType(ContractName);
            Type serviceManagerType = typeof(Infrustructure.Service.ServiceManager<>).MakeGenericType(contractType);
                        
            //生成ServiceManager对象
            object serviceManager = TypeHelper.CreateObject(serviceManagerType, serviceManagerType, true);

            PropertyInfo property = serviceManager.GetType().GetProperty("SelectMethodByPageCondition");
            if (property != null)
            {
                property.SetValue(serviceManager, SelectMethodByPageCondition, null);
            }


            return TypeHelper.Invoke(serviceManager, "GetByPageCondition", textWhere, orderText, startRowIndex, maximumRows);

        }

        /// <summary>
        /// 根据条件获取总记录数
        /// </summary>
        /// <param name="textWhere">查询条件</param>
        /// <returns></returns>
        public int GetCounts(string textWhere)
        {
            //生成ServiceManager类型
            Type contractType = TypeHelper.FindType(ContractName);
            Type serviceManagerType = typeof(Infrustructure.Service.ServiceManager<>).MakeGenericType(contractType);
                        
            //生成ServiceManager对象
            object serviceManager = TypeHelper.CreateObject(serviceManagerType, serviceManagerType, true);
            PropertyInfo property = serviceManager.GetType().GetProperty("SelectCountMethod");
            if (property != null)
            {
                property.SetValue(serviceManager, SelectCountMethod, null);
            }

            return (int)TypeHelper.Invoke(serviceManager, "GetCounts", textWhere);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="info">业务对象</param>
        /// <returns></returns>
        public void Insert(object info)
        {
            //生成ServiceManager类型
            Type contractType = TypeHelper.FindType(ContractName);
            Type serviceManagerType = typeof(Infrustructure.Service.ServiceManager<>).MakeGenericType(contractType);

            //生成ServiceManager对象
            object serviceManager = TypeHelper.CreateObject(serviceManagerType, serviceManagerType, true);
            PropertyInfo property = serviceManager.GetType().GetProperty("InsertMethod");
            if (property != null)
            {
                property.SetValue(serviceManager, InsertMethod, null);
            }

            TypeHelper.Invoke(serviceManager, "Insert", info);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="info">业务对象</param>
        /// <returns></returns>
        public void Update(object info)
        {
            //生成ServiceManager类型
            Type contractType = TypeHelper.FindType(ContractName);
            Type serviceManagerType = typeof(Infrustructure.Service.ServiceManager<>).MakeGenericType(contractType);

            //生成ServiceManager对象
            object serviceManager = TypeHelper.CreateObject(serviceManagerType, serviceManagerType, true);
            PropertyInfo property = serviceManager.GetType().GetProperty("UpdateMethod");
            if (property != null)
            {
                property.SetValue(serviceManager, UpdateMethod, null);
            }

            TypeHelper.Invoke(serviceManager, "Update", info);
        }

        /// <summary>
        /// 根据ID删除数据
        /// </summary>
        /// <param name="ID">标识符</param>
        /// <returns></returns>
        public void Delete(string ID)
        {
            //生成ServiceManager类型
            Type contractType = TypeHelper.FindType(ContractName);
            Type serviceManagerType = typeof(Infrustructure.Service.ServiceManager<>).MakeGenericType(contractType);

            //生成ServiceManager对象
            object serviceManager = TypeHelper.CreateObject(serviceManagerType, serviceManagerType, true);
            PropertyInfo property = serviceManager.GetType().GetProperty("DeleteMethod");
            if (property != null)
            {
                property.SetValue(serviceManager, DeleteMethod, null);
            }

            TypeHelper.Invoke(serviceManager, "Delete", ID);
        }

        /// <summary>
        /// 根据ID集合删除数据
        /// </summary>
        /// <param name="ID">标识符集合</param>
        /// <returns>删除成功的记录数</returns>
        public int DeleteBatch(List<string> ids)
        {
            //生成ServiceManager类型
            Type contractType = TypeHelper.FindType(ContractName);
            Type serviceManagerType = typeof(Infrustructure.Service.ServiceManager<>).MakeGenericType(contractType);

            //生成ServiceManager对象
            object serviceManager = TypeHelper.CreateObject(serviceManagerType, serviceManagerType, true);
            PropertyInfo property = serviceManager.GetType().GetProperty("DeleteBatchMethod");
            if (property != null)
            {
                property.SetValue(serviceManager, DeleteBatchMethod, null);
            }

            return (int)TypeHelper.Invoke(serviceManager, "DeleteBatch", ids);
        }


        #region Private Properties
        /// <summary>
        /// 契约名,包括命名空间
        /// </summary>
        private string ContractName
        {
            get;
            set;
        }
        
        private string _SelectMethodByID = "Get";
        private string _SelectMethodByCondition = "GetByCondition";   
        private string _SelectMethodByPageCondition = "GetList"; 
        private string _SelectCountMethod = "GetCounts";
        private string _InsertMethod = "Add";
        private string _UpdateMethod = "Update";
        private string _DeleteMethod = "Delete";
        private string _DeleteBatchMethod = "DeleteMultRecords";
       

        #endregion
    }

    public sealed class ServiceManager<T> : IDisposable where T : class
    {
        #region Public Properties

        /// <summary>
        /// 根据标识符查询数据方法名
        /// </summary>
        public string SelectMethodByID
        {
            get { return _SelectMethodByID; }
            set { if (!string.IsNullOrEmpty(value)) _SelectMethodByID = value; }
        }

        /// <summary>
        /// 根据条件查询数据方法名, 不分页
        /// </summary>
        public string SelectMethodByCondition
        {
            get { return _SelectMethodByCondition; }
            set { if (!string.IsNullOrEmpty(value)) _SelectMethodByCondition = value; }
        }

        /// <summary>
        /// 根据条件查询数据方法名, 分页
        /// </summary>
        public string SelectMethodByPageCondition
        {
            get { return _SelectMethodByPageCondition; }
            set { if (!string.IsNullOrEmpty(value)) _SelectMethodByPageCondition = value; }
        }

        /// <summary>
        /// 查询记录数方法名
        /// </summary>
        public string SelectCountMethod
        {
            get { return _SelectCountMethod; }
            set { if (!string.IsNullOrEmpty(value)) _SelectCountMethod = value; }
        }

        /// <summary>
        /// 更新方法名
        /// </summary>
        public string UpdateMethod
        {
            get { return _UpdateMethod; }
            set { if (!string.IsNullOrEmpty(value)) _UpdateMethod = value; }
        }

        /// <summary>
        /// 插入方法名
        /// </summary>
        public string InsertMethod
        {
            get { return _InsertMethod; }
            set { if (!string.IsNullOrEmpty(value)) _InsertMethod = value; }
        }

        /// <summary>
        /// 删除方法名
        /// </summary>
        public string DeleteMethod
        {
            get { return _DeleteMethod; }
            set { if (!string.IsNullOrEmpty(value)) _DeleteMethod = value; }
        }

        /// <summary>
        /// 批量删除方法名
        /// </summary>
        public string DeleteBatchMethod
        {
            get { return _DeleteBatchMethod; }
            set { if (!string.IsNullOrEmpty(value)) _DeleteBatchMethod = value; }
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
        /// 获取契约中Get方法返回的实体类型
        /// </summary>
        /// <returns></returns>
        public Type GetEntityType()
        {
            MethodInfo methodToInvoke = typeof(T).GetMethod(SelectMethodByID, BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            return (methodToInvoke == null) ? null : methodToInvoke.ReturnType;   
        }

        /// <summary>
        /// 根据主键获取数据
        /// </summary>
        /// <param name="parmaryKey">主键</param>
        /// <returns></returns>
        public object Get(params object[] parmaryKey)
        {
            return Invoke(SelectMethodByID, parmaryKey);

        }
        /// <summary>
        /// 根据条件获取数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public object GetByCondition(string condition)
        {
            return Invoke(SelectMethodByCondition, condition);
        }

        /// <summary>
        /// 根据条件获取数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public object GetListByCondition(object[] filterParams)
        {
            return Invoke(SelectMethodByCondition, filterParams);
        }

        /// <summary>
        /// 根据条件获取分页数据
        /// </summary>
        /// <param name="textWhere">查询条件</param>
        /// <param name="orderText">排序</param>
        /// <param name="startRowIndex">每页第一行记录数</param>
        /// <param name="maximumRows">每页最大记录数</param>
        /// <returns></returns>
        public object GetByPageCondition(string textWhere, string orderText, int startRowIndex, int maximumRows)
        {
            return Invoke(SelectMethodByPageCondition, textWhere, orderText, startRowIndex, maximumRows);
        }

        /// <summary>
        /// 根据条件获取总记录数
        /// </summary>
        /// <param name="textWhere">查询条件</param>
        /// <returns></returns>
        public int GetCounts(string textWhere)
        {
            return (int)Invoke(SelectCountMethod, textWhere);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="info">业务对象</param>
        /// <returns></returns>
        public void Insert(object info)
        {
            Invoke(InsertMethod, info);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="info">业务对象</param>
        /// <returns></returns>
        public void Update(object info)
        {
            Invoke(UpdateMethod, info);
        }

        /// <summary>
        /// 根据ID删除数据
        /// </summary>
        /// <param name="ID">标识符</param>
        /// <returns></returns>
        public void Delete(string ID)
        {
            //接口中ID可能为整型，也可能为GUID
            int rID = 0;
            if (int.TryParse(ID, out rID))
                Invoke(DeleteBatchMethod, rID);
            else
                Invoke(DeleteBatchMethod, ID);
        }

        /// <summary>
        /// 根据ID集合删除数据
        /// </summary>
        /// <param name="ID">标识符集合</param>
        /// <returns>删除成功的记录数</returns>
        public int DeleteBatch(List<string> ids)
        {
            List<int> rIds = new List<int>();

            foreach(string s in ids)
            {
                //接口中ID可能为整型，也可能为GUID
                int rID = 0;
                if (int.TryParse(s, out rID))
                    rIds.Add(rID);
            }

            if(rIds.Count > 0)
                return (int)Invoke(DeleteBatchMethod, rIds);
            else
                return (int)Invoke(DeleteBatchMethod, ids);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly")]
        public void Dispose()
        {
            if (serviceAgent != null)
            {
                serviceAgent.Close();
            }
        }
        /// <summary>
        /// 创建服务对象
        /// </summary>
        /// <returns></returns>
        private void CreateService()
        {
            //本地调用
            if (IsLocalCall())
            {
                Type serviceType = CreateServiceType();

                if (serviceType == null)
                {
                    throw new System.Exception("接口契约ServiceInfo属性值配置错误!");
                }

                _Service = (T)TypeHelper.CreateObject(serviceType, typeof(T), true, null);
            }
            else
            {
                serviceAgent = new ServiceAgent<T>();
                _Service = serviceAgent.Service;
            }
           
        }        

        /// <summary>
        /// 执行接口方法
        /// </summary>
        /// <param name="methodName">参数方法名</param>
        /// <param name="dmlParams">方法参数</param>
        /// <returns></returns>        
        private object Invoke(string methodName, params object[] dmlParams)
        {   
            //本地调用
            if (IsLocalCall())
            {
                Type serviceType = CreateServiceType();

                //如果没有配置, 也可以在当前程序集所在目录下查找实现接口的类型,但消耗性能,故目前约束
                //必须在契约上指定实现类型
                if (serviceType == null)
                {
                    throw new System.Exception("接口契约ServiceInfo属性值配置错误!");
                }

                T serviceObject = (T)TypeHelper.CreateObject(serviceType, typeof(T), true, null);

                return TypeHelper.Invoke(serviceObject, methodName, dmlParams);
            }
            else
            {
                using (ServiceAgent<T> service = new ServiceAgent<T>())
                {
                    return TypeHelper.Invoke(service.Service, methodName, dmlParams);
                    //return TypeHelper.Invoke(service.Service, dmlParams[0] as string, dmlParams);
                }
            }
        }

        /// <summary>
        /// 根据配置文件中ServiceConfiguration中CallType来判断是Local Call还是Service Call
        /// </summary>
        /// <returns></returns>
        private bool IsLocalCall()
        {
            ServiceConfiguration sc = ConfigurationManager.GetSection("ServiceConfiguration") as ServiceConfiguration;

            return (sc.CallType.ToLower() == "localcall") ? true : false;
        }

        /// <summary>
        /// 创建契约实现类型, 用于Local Call
        /// </summary>
        /// <returns></returns>
        private Type CreateServiceType()
        {
            string typeName = GetTypeName();

            if (typeName == "")
                return null;

            return TypeHelper.FindType(typeName);
        }

        /// <summary>
        /// 获取契约对应的实现类型名, 用于Local Call
        /// </summary>
        /// <returns></returns>
        private string GetTypeName()
        {
            string serviceName = "", typeName = "";

            //1.根据契约获取服务信息
            Type contractType = typeof(T);           

            object[] serviceInfoAtts = contractType.GetCustomAttributes(typeof(ServiceInfoAttribute), false);
            if (serviceInfoAtts != null && serviceInfoAtts.Length > 0)
            {
                ServiceInfoAttribute sn = (ServiceInfoAttribute)serviceInfoAtts[0];

                serviceName = sn.ServiceName;
                typeName = sn.DefaultService;

            }

            ////如果没有配置服务名, 取缺省类型
            if (serviceName == "")
                return typeName;

            //2.首先根据serviceName从配置文件获取类型
            ServiceConfiguration sc = ConfigurationManager.GetSection("ServiceConfiguration") as ServiceConfiguration;
            if (sc.LocalService.ContainsKey(serviceName))
                typeName = sc.LocalService[serviceName];

            //3.如果配置文件没有类型, 则直接使用接口契约上的缺省类型，即defaultService
            return typeName;
        }



        #region Private Properties

        //服务对象
        private ServiceAgent<T> serviceAgent = null;

        private T _Service = default(T);

        private string _SelectMethodByID = "Get";
        private string _SelectMethodByCondition = "GetList";
        private string _SelectMethodByPageCondition = "GetByPageCondition";
        private string _SelectCountMethod = "GetCounts";
        private string _InsertMethod = "Add";
        private string _UpdateMethod = "Update";
        private string _DeleteMethod = "Delete";
        private string _DeleteBatchMethod = "DeleteMultRecords";

        #endregion
    }

}