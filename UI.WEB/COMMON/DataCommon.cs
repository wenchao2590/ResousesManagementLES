using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.IO;
using BLL.SYS;
using System.Text.RegularExpressions;

namespace UI.WEB.COMMON
{
    public class DataCommon
    {
        /// <summary>
        /// GRID分页查询
        /// </summary>
        /// <param name="assemblyName">例:BLL.SYS</param>
        /// <param name="className">例:ActionBLL</param>
        /// <param name="textWhere">查询条件</param>
        /// <param name="textOrder">排序字段</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageRow">单页行数</param>
        /// <param name="dataCount">out数据总数</param>
        /// <returns></returns>
        public static object GetListByPage(string assemblyName, string className
            , string textWhere, string textOrder, int pageIndex, int pageRow
            , out int dataCount)
        {
            ///构建参数
            object[] param = new object[] { textWhere, textOrder, pageIndex, pageRow, 0 };
            ///实例化BLL对象
            object classObject = GetClassObject(assemblyName, className);
            ///查找GetListByPage函数
            MethodInfo met = classObject.GetType().GetMethod("GetListByPage");
            ///执行函数
            object obj = met.Invoke(classObject, param);
            ///返回OUT总行数
            dataCount = int.Parse(param[4].ToString());
            return obj;
        }
        /// <summary>
        /// GetDataTableByPage
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="tableName"></param>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public static object GetDataTableByPage(string moduleName, string tableName, string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            ///构建参数
            object[] param = new object[] { tableName, textWhere, textOrder, pageIndex, pageRow, 0 };
            ///实例化BLL对象
            object classObject = GetClassObject("BLL." + moduleName, "CommonBLL");
            ///查找GetDataTableByPage函数
            MethodInfo met = classObject.GetType().GetMethod("GetDataTableByPage");
            ///执行函数
            object obj = met.Invoke(classObject, param);
            ///返回OUT总行数
            dataCount = Convert.ToInt32(param[5]);
            return obj;
        }
        /// <summary>
        /// 获取注脚数据源
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="tableName"></param>
        /// <param name="entityFieldInfos"></param>
        /// <param name="textWhere"></param>
        /// <returns></returns>
        public static object GetFooterData(string moduleName, string tableName, List<EntityFieldInfo> entityFieldInfos, string textWhere)
        {
            ///构建参数
            object[] param = new object[] { moduleName, tableName, entityFieldInfos, textWhere };
            ///实例化BLL对象
            object classObject = GetClassObject("BLL." + moduleName, "CommonBLL");
            ///查找GetFooterData函数
            MethodInfo met = classObject.GetType().GetMethod("GetFooterData");
            ///执行函数
            return met.Invoke(classObject, param);
        }
        /// <summary>
        /// 
        /// </summary>adm
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <param name="idField"></param>
        /// <param name="textField"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetComboxItems(string assemblyName, string entityName, string tableName, string idField, string textField)
        {
            ///实例化CommonBLL对象
            object classObject = GetClassObject(assemblyName, entityName + "BLL");
            ///查找GetDatatableForExcel函数
            MethodInfo met = classObject.GetType().GetMethod("GetComboxItems");
            if (met == null)
            {
                ///构建参数
                object[] param = new object[] { assemblyName, tableName, GetFieldName(idField), GetFieldName(textField) };
                ///实例化CommonBLL对象
                classObject = GetClassObject(assemblyName, "CommonBLL");
                ///查找GetDatatableForExcel函数
                met = classObject.GetType().GetMethod("GetComboxItems");
                ///执行函数
                object obj = met.Invoke(classObject, param);
                return obj as Dictionary<string, string>;
            }
            ///构建参数
            object[] paramBLL = new object[] { idField, textField };
            ///执行函数
            object objBLL = met.Invoke(classObject, paramBLL);
            return objBLL as Dictionary<string, string>;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="entityName"></param>
        /// <param name="tableName"></param>
        /// <param name="idField"></param>
        /// <param name="textField"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetComboTreeItems(string assemblyName, string entityName, string tableName, string idField, string textField, string parentId)
        {
            ///实例化CommonBLL对象
            object classObject = GetClassObject(assemblyName, entityName + "BLL");
            ///查找GetDatatableForExcel函数
            MethodInfo met = classObject.GetType().GetMethod("GetComboTreeItems");
            if (met == null)
            {
                ///构建参数
                object[] param = new object[] { assemblyName, tableName, GetFieldName(idField), GetFieldName(textField), GetFieldName(parentId) };
                ///实例化CommonBLL对象
                classObject = GetClassObject(assemblyName, "CommonBLL");
                ///查找GetDatatableForExcel函数
                met = classObject.GetType().GetMethod("GetComboTreeItems");
                ///执行函数
                object obj = met.Invoke(classObject, param);
                return obj as Dictionary<string, string>;
            }
            ///构建参数
            object[] paramBLL = new object[] { idField, textField, parentId };
            ///执行函数
            object objBLL = met.Invoke(classObject, paramBLL);
            return objBLL as Dictionary<string, string>;
        }

        /// <summary>
        /// 获取EXCEL导出DATATABLE
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <returns></returns>
        public static DataTable GetDatatableForExcel(string assemblyName, string entityName, string tableName, List<string> columns, string textWhere, string textOrder)
        {
            ///实例化CommonBLL对象
            object classObject = GetClassObject(assemblyName, entityName + "BLL");
            ///查找GetDatatableForExcel函数
            MethodInfo met = classObject.GetType().GetMethod("GetDatatableForExcel");
            if (met == null)
            {
                ///构建参数
                object[] param = new object[] { tableName, columns, textWhere, textOrder };
                ///实例化CommonBLL对象
                classObject = GetClassObject(assemblyName, "CommonBLL");
                ///查找GetDatatableForExcel函数
                met = classObject.GetType().GetMethod("GetDatatableForExcel");
                ///执行函数
                object obj = met.Invoke(classObject, param);
                return obj as DataTable;
            }
            ///构建参数
            object[] paramBLL = new object[] { columns, textWhere, textOrder };
            ///执行函数
            object objBLL = met.Invoke(classObject, paramBLL);
            return objBLL as DataTable;
        }
        /// <summary>
        /// 执行导入EXCEL数据
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <param name="dataTable"></param>
        /// <param name="fieldNames"></param>
        /// <returns></returns>
        public static object ImportDataByExcel(string assemblyName, string className, DataTable dataTable, Dictionary<string, string> fieldNames, string loginUser, string whereText)
        {
            ///构建参数
            object[] param = null;
            if (string.IsNullOrEmpty(whereText))
                param = new object[] { dataTable, fieldNames, loginUser };
            else
                param = new object[] { dataTable, fieldNames, loginUser, whereText };
            ///实例化BLL对象
            object classObject = GetClassObject(assemblyName, className);
            ///查找ImportDataByExcel函数
            MethodInfo met = classObject.GetType().GetMethod("ImportDataByExcel");
            ///执行函数
            return met.Invoke(classObject, param);
        }
        /// <summary>
        /// 表单提交执行函数
        /// </summary>
        /// <param name="context">HC</param>
        /// <returns>XR.L 2016.7.7</returns>
        public static object InvokeAction(HttpContext context)
        {
            ///INSERT.UPDATE.DELETE.SELECT
            ///返回大写字符串
            string actionName = HttpCommon.GetActionName(context);
            ///BLL层的对象实例化
            object bllObject = HttpCommon.GetBusinessObject(context);
            ///对于UPDATE.DELETE.SELECT方法需要ID参数支持
            object[][] rowsKeyValues = HttpCommon.GetEntityKeyValues(context);
            string loginUserName = context.Session["loginUserName"].ToString();
            ///根据不同的ACTION指向执行不同的函数
            switch (actionName.ToLower())
            {
                case "insert": return InsertInfo(bllObject, HttpCommon.GetEntityObject(context));
                case "update": return UpdateInfo(bllObject, HttpCommon.GetUpdateFields(context).Substring(1), rowsKeyValues[0]);
                case "updatefield": return UpdateInfo(bllObject, HttpCommon.GetCommonFields(context), rowsKeyValues[0]);
                case "delete": return DeleteInfo(bllObject, rowsKeyValues[0], loginUserName);
                case "select": return SelectInfo(bllObject, rowsKeyValues[0]);
                case "status":
                    string statusName = HttpCommon.GetStatusName(context);
                    if (string.IsNullOrEmpty(statusName))
                        throw new Exception("MC:0x00000188");
                    return StatusInfo(bllObject, rowsKeyValues, statusName, loginUserName);
            }
            return null;
        }
        /// <summary>
        /// 封装SETVALUE
        /// </summary>
        /// <param name="entityObject"></param>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        private static void SetPropValue(object entityObject, string propName, object propValue)
        {
            PropertyInfo p = entityObject.GetType().GetProperty(propName);
            if (p == null) return;
            p.SetValue(entityObject, propValue, null);
        }
        /// <summary>
        /// 给FID赋值
        /// </summary>
        /// <param name="entityObject"></param>
        private static void SetFid(object entityObject)
        {
            PropertyInfo p = entityObject.GetType().GetProperty("Fid");
            if (p == null) return;
            if (p.GetValue(entityObject, null) != null) return;
            ///object tableName = entityObject.GetType().GetField("_TableName").GetValue(entityObject);
            switch (p.PropertyType.Name.ToLower())
            {
                case "string": p.SetValue(entityObject, Guid.NewGuid().ToString().Replace("-", string.Empty), null); break;
                default: p.SetValue(entityObject, Guid.NewGuid(), null); break;
            }
        }
        /// <summary>
        /// 封装GETVALUE
        /// </summary>
        /// <param name="entityObject"></param>
        /// <param name="propName"></param>
        /// <returns></returns>
        public static object GetPropValue(object entityObject, string propName)
        {
            PropertyInfo p = entityObject.GetType().GetProperty(propName);
            return p.GetValue(entityObject, null);
        }
        /// <summary>
        /// 表单INSERT
        /// </summary>
        /// <param name="bllObject">BLL对象</param>
        /// <param name="entityObject">数据对象</param>
        /// <returns></returns>
        public static object InsertInfo(object bllObject, object entityObject)
        {
            MethodInfo met = bllObject.GetType().GetMethod("InsertInfo");
            SetFid(entityObject);
            object rtObject = met.Invoke(bllObject, new object[] { entityObject });
            if (rtObject.GetType().Name.ToLower() == "int64"
                || rtObject.GetType().Name.ToLower() == "int32")
            {
                SetPropValue(entityObject, "Id", rtObject);
                rtObject = entityObject;
            }
            return rtObject;
        }
        /// <summary>
        /// 表单UPDATE
        /// </summary>
        /// <param name="bllObject">BLL对象</param>
        /// <param name="fields">更新字符</param>
        /// <param name="id">ID</param>
        /// <returns></returns>
        private static bool UpdateInfo(object bllObject, string fields, object[] keyValues)
        {
            MethodInfo met = bllObject.GetType().GetMethod("UpdateInfo");
            List<object> objs = keyValues.ToList();
            objs.Insert(0, fields);
            return bool.Parse(met.Invoke(bllObject, objs.ToArray()).ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bllObject"></param>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private static bool UpdateInfo(object bllObject, List<CommonField> fields, object[] keyValues)
        {
            MethodInfo met = bllObject.GetType().GetMethod("UpdateInfo");
            List<object> objs = keyValues.ToList();
            objs.Insert(0, fields);
            return bool.Parse(met.Invoke(bllObject, objs.ToArray()).ToString());
        }
        /// <summary>
        /// 表单UPDATE
        /// </summary>
        /// <param name="bllObject">BLL对象</param>
        /// <param name="info">更新对象</param>
        /// <param name="id">ID</param>
        /// <returns>XR.L 2016.7.7</returns>
        private static bool UpdateInfo(object bllObject, object info)
        {
            MethodInfo met = bllObject.GetType().GetMethod("Update");
            return bool.Parse(met.Invoke(bllObject, new object[] { info }).ToString());
        }

        /// <summary>
        /// 表单DELETE
        /// </summary>
        /// <param name="bllObject">BLL对象</param>
        /// <param name="id">ID</param>
        /// <returns></returns>
        private static bool DeleteInfo(object bllObject, object[] keyValues, string loginUserName)
        {
            ///如果找到逻辑删除方法就按照逻辑删除处理
            MethodInfo met = bllObject.GetType().GetMethod("LogicDeleteInfo");
            if (met == null)
                met = bllObject.GetType().GetMethod("DeleteInfo");
            else
            {
                ///逻辑删除需要给于操作者
                List<object> objs = keyValues.ToList();
                objs.Add(loginUserName);
                return bool.Parse(met.Invoke(bllObject, objs.ToArray()).ToString());
            }
            return bool.Parse(met.Invoke(bllObject, keyValues).ToString());
        }
        /// <summary>
        /// 更新数据状态函数
        /// </summary>
        /// <param name="bllObject"></param>
        /// <param name="keyValues"></param>
        /// <param name="statusName"></param>
        /// <param name="loginUserName"></param>
        /// <returns></returns>
        private static bool StatusInfo(object bllObject, object[][] keyValues, string statusName, string loginUserName)
        {
            int codeValue = new CodeItemBLL().GetValueByCodeItemName("SET_STATUS_TYPE", statusName.Replace("#", string.Empty));
            ///未配置设定状态类型
            if (codeValue == 0)
                throw new Exception("MC:7x00000000");
            string methodName = statusName.Substring(0, 1).ToUpper() + statusName.Substring(1).ToLower();
            if (statusName.StartsWith("#"))
                methodName = "Undo" + statusName.Substring(1, 1).ToUpper() + statusName.Substring(2).ToLower();
            MethodInfo method = bllObject.GetType().GetMethod(methodName + "Info");
            if (method == null)
                method = bllObject.GetType().GetMethod(methodName + "Infos");
            if (method == null)
                throw new Exception("MC:0x00000188");
            List<object> objs = new List<object>();
            if (method.Name.Contains("Infos"))
            {
                List<string> rowKeys = new List<string>();
                for (int i = 0; i < keyValues.Length; i++)
                {
                    string rowKeyValues = string.Empty;
                    for (int j = 0; j < keyValues[i].Length; j++)
                    {
                        rowKeyValues += "^" + keyValues[i][j].ToString();
                    }
                    rowKeys.Add(rowKeyValues.Substring(1));
                }
                objs.Add(rowKeys);
                objs.Add(loginUserName);
            }
            else
            {
                objs = keyValues[0].ToList();
                objs.Add(loginUserName);
            }
            return bool.Parse(method.Invoke(bllObject, objs.ToArray()).ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dmObject"></param>
        /// <param name="bllObject"></param>
        /// <param name="keyValues"></param>
        /// <param name="statusName"></param>
        /// <param name="loginUserName"></param>
        /// <returns></returns>
        public static bool EntityStatusInfo(object dmObject, object bllObject, object[][] keyValues, string statusName, string loginUserName)
        {
            int codeValue = new CodeItemBLL().GetValueByCodeItemName("SET_STATUS_TYPE", statusName.Replace("#", string.Empty));
            if (codeValue == 0)
                throw new Exception("MC:7x00000000");///未配置设定状态类型
            string methodName = statusName.Substring(0, 1).ToUpper() + statusName.Substring(1).ToLower();
            MethodInfo method = bllObject.GetType().GetMethod("Entity" + methodName + "Infos");
            if (method == null)
                throw new Exception("MC:0x00000188");///后端没找到对应的方法
            List<object> objs = new List<object>();
            List<string> rowKeys = new List<string>();
            for (int i = 0; i < keyValues.Length; i++)
            {
                string rowKeyValues = string.Empty;
                for (int j = 0; j < keyValues[i].Length; j++)
                {
                    rowKeyValues += "^" + keyValues[i][j].ToString();
                }
                rowKeys.Add(rowKeyValues.Substring(1));
            }
            objs.Add(dmObject);
            objs.Add(rowKeys);
            objs.Add(loginUserName);
            return bool.Parse(method.Invoke(bllObject, objs.ToArray()).ToString());
        }


        /// <summary>
        /// 表单SELECT
        /// </summary>
        /// <param name="bllObject">BLL对象</param>
        /// <param name="id">ID</param>
        /// <returns></returns>
        private static object SelectInfo(object bllObject, object[] keyValues)
        {
            MethodInfo met = bllObject.GetType().GetMethod("SelectInfo");
            return met.Invoke(bllObject, keyValues);
        }
        /// <summary>
        /// 反射创建对象
        /// </summary>
        /// <param name="assemblyName">命名空间</param>
        /// <param name="className">类名</param>
        /// <returns></returns>
        public static object GetClassObject(string assemblyName, string className)
        {
            Assembly ass = Assembly.Load(assemblyName);
            Type classType = ass.GetTypes().FirstOrDefault(o => o.Name.Equals(className, StringComparison.OrdinalIgnoreCase));
            if (classType == null)
                classType = ass.GetTypes().FirstOrDefault(o => o.FullName.Equals(className, StringComparison.OrdinalIgnoreCase));

            return Activator.CreateInstance(classType);
        }
        /// <summary>
        /// 对象的属性名转字段名
        /// </summary>
        /// <param name="propName"></param>
        /// <returns></returns>
        public static string GetFieldName(string propName)
        {
            ///判断是否含有小写字母，如果没有小写字母则直接返回字符串，规避无_的设置
            if (!Regex.IsMatch(propName, "[a-z]"))
                return propName;
            string fieldName = propName.Substring(0, 1).ToUpper();
            for (int j = 1; j < propName.Length; j++)
            {
                if (propName[j] >= 'a' && propName[j] <= 'z')
                    fieldName += propName[j].ToString().ToUpper();
                else if (propName[j] >= 'A' && propName[j] <= 'Z')
                    fieldName += "_" + propName[j].ToString().ToUpper();
                else
                    fieldName += propName[j].ToString();
            }
            return fieldName;
        }
    }
}