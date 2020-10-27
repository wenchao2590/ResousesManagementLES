using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Infrustructure.Utilities
{
    /// <summary>
    /// 关于类型、实例的一些实用方法
    /// </summary>
    public static class TypeHelper
    {
        /// <summary>
        /// 从类型名称中创建类型
        /// </summary>
        /// <param name="typeName">程序集名.命名空间名.类型名</param>
        /// <param name="throwOnError">失败时是否抛出异常</param>
        /// <returns>Type</returns>
        public static Type CreateType(String typeName, bool throwOnError)
        {
            return Type.GetType(typeName, throwOnError, false);
        }

        /// <summary>
        /// 从类型中创建此类型的实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="expectedType">期望的类型</param>
        /// <param name="throwOnError">失败时是否抛出异常</param>
        /// <param name="parameterTypes">创建实例所需参数的类型列表</param>
        /// <param name="parameterValues">创建实例所需的参数值列表</param>
        /// <returns>类型实例</returns>
        public static object CreateObject(Type type, Type expectedType, bool throwOnError, Type[] parameterTypes, object[] parameterValues)
        {
            if (expectedType != null && !expectedType.IsAssignableFrom(type))
            {
                if (throwOnError)
                {
                    throw new System.Exception(String.Format("将要创建的类型：{0}，不是期望的类型：{1}", type.FullName, expectedType.FullName));
                }
                return null;
            }
            if (parameterTypes != null && parameterValues != null && parameterTypes.Length != parameterValues.Length)
            {
                if (throwOnError)
                {
                    throw new System.Exception("构造函数参数类型数量和参数数量不一致");
                }
            }
            object createdObject = null;
            ConstructorInfo constructor = type.GetConstructor(parameterTypes);
            if (constructor == null)
            {
                try
                {
                    createdObject = Activator.CreateInstance(type, BindingFlags.CreateInstance | (BindingFlags.NonPublic | (BindingFlags.Public | BindingFlags.Instance)), null, parameterValues, null);
                }
                catch (System.Exception e)
                {
                    if (throwOnError)
                    {
                        throw new System.Exception("即将创建的类型不支持指定的构造函数：" + e.Message, e);
                    }
                }
            }
            else
            {
                try
                {
                    createdObject = constructor.Invoke(parameterValues);
                }
                catch (System.Exception e)
                {
                    throw new System.Exception("对象创建失败：" + e.Message, e);
                }
            }
            return createdObject;
        }

        /// <summary>
        /// 从类型中创建此类型的实例（本方法不支持参数可为Null的构造函数）
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="expectedType">期望的类型</param>
        /// <param name="throwOnError">失败时是否抛出异常</param>
        /// <param name="parameters">创建实例所需的参数值列表</param>
        /// <returns>类型实例</returns>
        public static object CreateObject(Type type, Type expectedType, bool throwOnError, params object[] parameters)
        {
            int paramNum = 0;
            if (parameters != null)
            {
                paramNum = parameters.Length;
            }
            Type[] paramTypes = new Type[paramNum];
            object[] paramValues = new object[paramNum];
            for (int i = 0; i < paramNum; i++)
            {
                if (parameters[i] == null)
                {
                    if (throwOnError)
                    {
                        throw new System.Exception("不支持参数可为Null的构造函数，请使用本方法的另外重载版本");
                    }
                    else
                    {
                        return null;
                    }
                }
                paramTypes[i] = parameters[i].GetType();
                paramValues[i] = parameters[i];
            }
            return CreateObject(type, expectedType, throwOnError, paramTypes, paramValues);
        }

        public static object CreateObject(String assemblyName, String typeName, bool throwOnError, params object[] parameters)
        {
            Type aType = FindType(assemblyName, typeName);

            if (aType == null)
                return null;

            return CreateObject(aType, aType, true, parameters);
        }

        /// <summary>
        /// 从类型名中创建此类型的实例
        /// </summary>
        /// <param name="typeName">类型名</param>
        /// <param name="expectedType">期望的类型</param>
        /// <param name="throwOnError">失败时是否抛出异常</param>
        /// <param name="parameters">创建实例所需的参数值列表</param>
        /// <returns>类型实例</returns>
        public static object CreateObject(String typeName, Type expectedType, bool throwOnError, params object[] parameters)
        {
            Type type = CreateType(typeName, throwOnError);
            return CreateObject(type, expectedType, throwOnError, parameters);
        }

        /// <summary>
        /// 从类型名中创建此类型的实例
        /// </summary>
        /// <param name="typeName">类型名</param>
        /// <param name="expectedType">期望的类型</param>
        /// <param name="throwOnError">失败时是否抛出异常</param>
        /// <param name="parameterTypes">创建实例所需参数的类型列表</param>
        /// <param name="parameterValues">创建实例所需的参数值列表</param>
        /// <returns>类型实例</returns>
        public static object CreateObject(String typeName, Type expectedType, bool throwOnError, Type[] parameterTypes, object[] parameterValues)
        {
            Type type = CreateType(typeName, throwOnError);
            return CreateObject(type, expectedType, throwOnError, parameterTypes, parameterValues);
        }

        /// <summary>
        /// 在当前应用程序域中查找指定的类型
        /// </summary>
        /// <param name="typeName">类型全名（包括命名空间）</param>
        /// <returns>找到则返回指定的类型，否则返回空</returns>
        public static Type FindType(String typeName)
        {
            Type type = null;

            //取出程序集名称和类型名称
            var typeStrAry = typeName.Split(',');
            String typeStr = typeStrAry[0].Trim();
            String assemblyStr = typeStrAry.Length > 1 ? typeStrAry[1].Trim() : null;

            //如果提供了程序集，则直接从程序集中获取该类型
            if (assemblyStr != null)
            {
                //从当前已加载的程序集中获取同名程序集
                var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(ass => ass.FullName.ToLower() == assemblyStr);
                //如果已加载程序集中没有同名程序集，则从当前路径或关联路径查找程序集
                if (assembly == null)
                {
                    String assemblyPath = null;
                    if (File.Exists(String.Format("{0}\\{1}", AppDomain.CurrentDomain.BaseDirectory, assemblyStr)))
                    {
                        assemblyPath = String.Format("{0}\\{1}", AppDomain.CurrentDomain.BaseDirectory, assemblyStr);
                    }
                    else if (File.Exists(String.Format("{0}\\{1}", AppDomain.CurrentDomain.RelativeSearchPath, assemblyStr)))
                    {
                        assemblyPath = String.Format("{0}\\{1}", AppDomain.CurrentDomain.RelativeSearchPath, assemblyStr);
                    }
                    else
                    {
                        throw new FileNotFoundException("assemblyStr");
                    }
                    assembly = Assembly.LoadFrom(assemblyPath);
                }

                type = assembly.GetType(typeStr);
            }
            else
            {
                List<String> files = new List<String>();
                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (!assembly.FullName.StartsWith("BLL."))
                        continue;

                    type = assembly.GetType(typeStr, false);
                    if (type != null)
                    {
                        break;
                    }
                    else if (!assembly.GlobalAssemblyCache)
                    {
                        files.Add(assembly.ManifestModule.ScopeName.ToLower());
                    }
                }
                if (type == null)
                {
                    string path = string.Empty;
                    try
                    {
                        path = AppDomain.CurrentDomain.RelativeSearchPath;
                    }
                    catch
                    {
                    }
                    if (path == null || path.Length == 0)
                    {
                        try
                        {
                            path = AppDomain.CurrentDomain.BaseDirectory;
                        }
                        catch { }
                    }

                    String[] fileNames = Directory.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly);
                    foreach (String file in fileNames)
                    {
                        String fileName = Path.GetFileName(file);
                        if (!files.Contains(fileName.ToLower()))
                        {
                            String assemblyName = Path.GetFileNameWithoutExtension(fileName);
                            String typeFullName = typeStr + ", " + assemblyName;
                            type = CreateType(typeFullName, false);
                            if (type != null)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return type;
        }

        public static Type FindType(String assemblyName, String typeName)
        {
            if (assemblyName.Length == 0) return null;
            Assembly assemb = Assembly.Load(assemblyName);
            if (assemb != null)
                return assemb.GetType(typeName);

            return null;
        }

        public const BindingFlags FieldBindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        public static object Invoke(object obj, String methodName, params object[] parameters)
        {

            if (obj == null)
            {
                return obj;
            }

            MethodInfo methodToInvoke = obj.GetType().GetMethod(methodName, FieldBindingFlags);

            if (obj.GetType().Name.ToLower().StartsWith("servicemanager") &&
                methodName.ToLower() == "get")
            {
                object[] methodParams = { parameters };

                return methodToInvoke.Invoke(obj, methodParams);
            }
            else
            {
                return methodToInvoke.Invoke(obj, parameters);
            }

        }

        public static object Invoke(String assemblyName, String typeName, String methodName, params object[] parameters)
        {
            object obj = CreateObject(assemblyName, typeName, true, null);

            return Invoke(obj, methodName, parameters);
        }

        /// <summary>
        /// 返回对象中方法的返回类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public static Type GetReturnType(object obj, String methodName)
        {
            if (obj == null)
            {
                return null;
            }

            MethodInfo methodToInvoke = obj.GetType().GetMethod(methodName, FieldBindingFlags);

            return (methodToInvoke == null) ? null : methodToInvoke.ReturnType;
        }

        /// <summary>
        /// 从程序集中获得元属性
        /// </summary>
        /// <param name="assemblies">程序集，如果为null，则从当前应用程序域中获取所载入的所有程序集</param>
        /// <returns>找到的元属性的数组</returns>
        public static T[] GetAttributeFromAssembly<T>(Assembly[] assemblies) where T : Attribute
        {
            List<T> list = new List<T>();
            T[] attributes = null;
            if (assemblies == null)
            {
                assemblies = AppDomain.CurrentDomain.GetAssemblies();
            }
            foreach (Assembly assembly in assemblies)
            {
                attributes = (T[])assembly.GetCustomAttributes(typeof(T), false);
                if (attributes != null && attributes.Length > 0)
                {
                    list.AddRange(attributes);
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// 从运行时的堆栈中获取元属性
        /// </summary>
        /// <param name="includeAll">是否包含堆栈上所有的元属性</param>
        /// <typeparam name="T">元属性类型</typeparam>
        /// <returns>找到的元属性的数组</returns>
        public static T[] GetAttributeFromRuntimeStack<T>(bool includeAll) where T : Attribute
        {
            var list = new List<T>();
            var t = new StackTrace();
            for (var i = 0; i < t.FrameCount; i++)
            {
                var f = t.GetFrame(i);
                var m = (MethodInfo)f.GetMethod();
                var a = Attribute.GetCustomAttributes(m, typeof(T)) as T[];
                if (a != null && a.Length > 0)
                {
                    list.AddRange(a);
                    if (!includeAll)
                    {
                        break;
                    }
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// 根据字串获取程序集
        /// </summary>
        /// <param name="assemblyStr">程序集名称</param>
        /// <param name="assemblyExtFileName">程序集的扩展名</param>
        /// <returns>返回程序集</returns>
        public static Assembly GetAssembly(String assemblyStr, String assemblyExtFileName = "dll")
        {
            var assemblyFullName = assemblyExtFileName == null
                                    ? assemblyStr
                                    : String.Format("{0}.{1}", assemblyStr, assemblyExtFileName);
            //从当前已加载的程序集中获取同名程序集
            var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(ass => ass.FullName.ToLower() == assemblyFullName);
            //如果已加载程序集中没有同名程序集，则从当前路径或关联路径查找程序集
            if (assembly == null)
            {
                String assemblyPath = null;
                if (File.Exists(String.Format("{0}\\{1}", AppDomain.CurrentDomain.BaseDirectory, assemblyFullName)))
                {
                    assemblyPath = String.Format("{0}\\{1}", AppDomain.CurrentDomain.BaseDirectory, assemblyFullName);
                }
                else if (File.Exists(String.Format("{0}\\{1}", AppDomain.CurrentDomain.RelativeSearchPath, assemblyFullName)))
                {
                    assemblyPath = String.Format("{0}\\{1}", AppDomain.CurrentDomain.RelativeSearchPath, assemblyFullName);
                }
                else
                {
                    throw new FileNotFoundException("assemblyStr");
                }
                assembly = Assembly.LoadFrom(assemblyPath);
            }

            return assembly;
        }

        /// <summary>
        /// 根据字串获取类型
        /// </summary>
        /// <param name="typeName">类型名称</param>
        /// <param name="assemblyExtFileName">程序集的扩展名</param>
        /// <returns>返回类型</returns>
        public static Type GetType(String typeName, String assemblyExtFileName = "dll")
        {
            Type type = null;

            //如果指定了程序集，则从程序集中获取
            var typeNameAry = typeName.Split(',');
            if (typeNameAry.Length == 2)
            {
                var assembly = GetAssembly(typeNameAry[1], assemblyExtFileName);
                type = assembly.GetType(typeNameAry[0]);
            }
            else
            {
                type = Type.GetType(typeName);
            }

            return type;
        }

        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="assemblyExtFileName">程序集的扩展名</param>
        /// <param name="parms">构造函数参数</param>
        /// <returns></returns>
        public static Object CreateObject(String typeName, String assemblyExtFileName = "dll", Object[] parms = null)
        {
            Type type = GetType(typeName, assemblyExtFileName);

            if (parms != null)
            {
                return Activator.CreateInstance(type, parms);
            }
            else
            {
                return Activator.CreateInstance(type);
            }
        }
    }
}
