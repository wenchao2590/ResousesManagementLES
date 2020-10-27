using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Reflection;
using System.Text;

using Microsoft.CSharp;
using Microsoft.VisualBasic;


namespace Infrustructure.Utilities
{
	public class RelectionHelper
    {
        
        public RelectionHelper()
        {
          
        }


        static private Type GetTypeFromAssembly(string strAssemblyName, string strTypeName)
        {
            ////Activator.CreateInstance((Type.GetType(strTypeName));

            Assembly assemb = Assembly.Load(strAssemblyName);
            if( assemb != null)
                return assemb.GetType(strTypeName);

            //foreach (Assembly assem in System.AppDomain.CurrentDomain.GetAssemblies())
            //{
            //    Type __ModuleType = assem.GetType(strTypeName);
            //    if (__ModuleType != null)
            //        return __ModuleType;
            //}

            return null;
        }
        static public object Invoke(string _strAssemblyName, string __strTypeName, string __strMethod, object[] __Arguments)
        {
            object result;
            Type __ModuleType = GetTypeFromAssembly(_strAssemblyName, __strTypeName);
            if (__ModuleType == null)
            {
                throw new System.Exception(string.Format("Can't Find type {0}", __strTypeName));
            }
            MethodInfo __MethodInfo = __ModuleType.GetMethod(__strMethod);
            if (__MethodInfo == null)
            {
                throw new System.Exception(string.Format("Can't Find method {1} in type {0}", __strTypeName, __strMethod));
            }
            try
            {
                result = __MethodInfo.Invoke(null, __Arguments);
            }
            catch
            {
                result = null;
            }

            return result;
         }

    }
}

