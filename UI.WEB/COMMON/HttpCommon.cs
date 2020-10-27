namespace UI.WEB.COMMON
{
    using DM.SYS;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web;
    public class HttpCommon
    {
        /// <summary>
        /// 获取对象名称
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static string GetEntityName(HttpContext context)
        {
            string methodType = GetMethodType(context);
            string[] actionArray = methodType.Split('-');
            if (actionArray.Length < 2) return string.Empty;
            ///TS_SYS_MENU_ACTION => MenuAction
            string entityName = string.Empty;
            string[] tableNameArray = actionArray[1].Split('_');
            ///说明不是表名，是直接可以启用的对象名
            if (tableNameArray.Length < 3)
                return actionArray[1];
            if (tableNameArray[0].StartsWith("T"))
            {
                for (int i = 2; i < tableNameArray.Length; i++)
                {
                    entityName += tableNameArray[i].Substring(0, 1).ToUpper() + tableNameArray[i].Substring(1).ToLower();
                }
            }
            return entityName;
        }
        /// <summary>
        /// 获取模块名称
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static string GetModuleName(HttpContext context)
        {
            string bll = context.Request["AN"];
            if (!string.IsNullOrEmpty(bll))
                bll = bll.Replace("BLL.", string.Empty);
            if (!string.IsNullOrEmpty(bll))
                return bll;
            string methodType = GetMethodType(context);
            string[] actionArray = methodType.Split('-');
            if (actionArray.Length < 2) return string.Empty;
            ///TS_SYS_MENU_ACTION => SYS
            string[] tableNameArray = actionArray[1].Split('_');
            if (tableNameArray.Length < 3) return string.Empty;
            return tableNameArray[1].ToUpper();
        }
        public static string GetMethodType(HttpContext context)
        {
            string methodType = context.Request["methods"];
            if (methodType == null || methodType == string.Empty)
            {
                methodType = context.Request["method"];
            }
            return methodType;
        }
        /// <summary>
        /// 获取动作名称
        /// 约定为:INSERT.UPDATE.DELETE.SELECT
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetActionName(HttpContext context)
        {
            string methodType = GetMethodType(context);
            string[] action = methodType.Split('-');
            if (action.Length < 1) return string.Empty;
            return action[0].ToUpper();
        }
        /// <summary>
        /// 获取更新的状态名称
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetStatusName(HttpContext context)
        {
            string methodType = GetMethodType(context);
            string[] action = methodType.Split('-');
            if (action.Length < 3) return string.Empty;
            return action[2].ToUpper();
        }
        /// <summary>
        /// 获取ID
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static object[][] GetEntityKeyValues(HttpContext context)
        {
            string keyvalue = context.Request["key"];///-|1-|2
            string keylength = context.Request["keylength"];
            if (string.IsNullOrEmpty(keyvalue)) return new object[][] { };
            if (string.IsNullOrEmpty(keylength)) keylength = "32";
            string[] rowvalues = keyvalue.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            ///字段类型
            string[] keylengths = keylength.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            ///返回值
            object[][] returnKeyValues = new object[rowvalues.Length][];
            for (int i = 0; i < rowvalues.Length; i++)
            {
                ///值
                string[] keyvalues = rowvalues[i].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                ///返回值
                object[] arrayKeyValues = new object[keyvalues.Length];
                for (int j = 0; j < keyvalues.Length; j++)
                {
                    switch (keylengths[j])
                    {
                        ///32 = int
                        case "32": arrayKeyValues[j] = Convert.ToInt32(keyvalues[j]); break;
                        ///36 = guid
                        case "36": arrayKeyValues[j] = Guid.Parse(keyvalues[j]); break;
                        ///64 = long
                        case "64": arrayKeyValues[j] = Convert.ToInt64(keyvalues[j]); break;
                        /// 512 = string
                        case "512": arrayKeyValues[j] = Convert.ToString(keyvalues[j]); break;
                    }
                }
                returnKeyValues[i] = arrayKeyValues;
            }
            return returnKeyValues;
        }
        /// <summary>
        /// 获取BLL对象
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static object GetBusinessObject(HttpContext context)
        {
            string entityName = GetEntityName(context);
            if (string.IsNullOrEmpty(entityName))
                entityName = context.Request["ENTITY_NAME"];
            string moduleName = GetModuleName(context);
            return DataCommon.GetClassObject("BLL." + moduleName, entityName + "BLL");
        }
        /// <summary>
        /// 获取数据对象
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static object GetEntityObject(HttpContext context)
        {
            string entityName = GetEntityName(context);
            if (string.IsNullOrEmpty(entityName))
                entityName = context.Request["ENTITY_NAME"];
            string moduleName = GetModuleName(context);
            object obj = DataCommon.GetClassObject("DM." + moduleName, entityName + "Info");
            return GetObject(context, obj);
        }
        /// <summary>
        /// 获取数据对象的具体属性值
        /// </summary>
        /// <param name="context"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static object GetObject(HttpContext context, object obj)
        {
            ///此处因为Keys中最后一项是method参数，所以需要向前移一位
            for (int i = 0; i < context.Request.Form.AllKeys.Length; i++)
            {
                string propName = context.Request.Form.AllKeys[i];
                if (string.IsNullOrEmpty(propName)) continue;
                if (propName.ToLower() == "method") continue;
                string propValue = context.Request[propName];
                PropertyInfo p = obj.GetType().GetProperty(propName);
                if (p == null) continue;
                switch (p.PropertyType.Name.ToLower())
                {
                    case "string": p.SetValue(obj, propValue, null); break;
                    case "nullable`1":
                        if (string.IsNullOrEmpty(propValue))
                        {
                            p.SetValue(obj, null, null);
                            break;
                        }
                        if (p.PropertyType.FullName.ToLower().Contains("system.int32"))
                            p.SetValue(obj, int.Parse(propValue), null);
                        if (p.PropertyType.FullName.ToLower().Contains("system.boolean"))
                        {
                            propValue = propValue.Trim() == "30" ? "true" : "false";//zjc.2016.5.31 //在新增的时候值为1报错。只能录入TRUE，在修改时。TRUE值又报错。所以在新增时遇到1改为true
                            p.SetValue(obj, bool.Parse(propValue), null);
                        }
                        if (p.PropertyType.FullName.ToLower().Contains("system.decimal"))
                            p.SetValue(obj, decimal.Parse(propValue), null);
                        if (p.PropertyType.FullName.ToLower().Contains("system.guid"))
                        {
                            ///TODO:firefox中replace(/'/g, "")未能执行成功，临时代码依靠c#解决
                            p.SetValue(obj, Guid.Parse(propValue.Replace("'", "").Replace("^", "")), null);
                        }
                        if (p.PropertyType.FullName.ToLower().Contains("system.datetime"))
                            p.SetValue(obj, DateTime.Parse(propValue), null);
                        break;
                    case "int32": p.SetValue(obj, string.IsNullOrEmpty(propValue) ? 0 : int.Parse(propValue), null); break;
                    case "boolean":
                    case "bool": propValue = propValue.Trim() == "30" ? "true" : "false"; p.SetValue(obj, bool.Parse(propValue), null); break;
                    case "guid": p.SetValue(obj, string.IsNullOrEmpty(propValue) ? Guid.Empty : Guid.Parse(propValue.Replace("'", "")), null); break;
                    case "datetime": p.SetValue(obj, string.IsNullOrEmpty(propValue) ? DateTime.Parse("1900-01-01") : DateTime.Parse(propValue), null); break;
                }
            }
            ///创建人
            PropertyInfo pCreate = obj.GetType().GetProperty("CreateUser");
            if (pCreate != null) pCreate.SetValue(obj, HandlerCommon.LoginUser, null);
            ///创建时间
            pCreate = obj.GetType().GetProperty("CreateDate");
            if (pCreate != null) pCreate.SetValue(obj, DateTime.Now, null);
            ///有效标记
            pCreate = obj.GetType().GetProperty("ValidFlag");
            if (pCreate != null) pCreate.SetValue(obj, true, null);
            ///逻辑主键
            pCreate = obj.GetType().GetProperty("Fid");
            if (pCreate != null) pCreate.SetValue(obj, Guid.NewGuid(), null);
            ///组织FID
            pCreate = obj.GetType().GetProperty("OrganizationFid");
            if (pCreate != null && pCreate.GetValue(obj, null) == null)
                pCreate.SetValue(obj, HandlerCommon.OrganizationFid, null);
            ///
            return obj;
        }

        /// <summary>
        /// 获取UPDATE字段的SQL语句
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetUpdateFields(HttpContext context)
        {
            string entityName = GetEntityName(context);
            string enityNameFromUI = context.Request["ENTITY_NAME"];
            if (enityNameFromUI.IndexOf("_") != -1)
            {
                enityNameFromUI = enityNameFromUI.Substring(0, enityNameFromUI.IndexOf("_"));
            }
            if (enityNameFromUI.Contains(entityName))
            {
                entityName = enityNameFromUI;
            }
            string moduleName = GetModuleName(context);
            object obj = DataCommon.GetClassObject("DM." + moduleName, entityName + "Info");
            string fields = string.Empty;
            ///此处因为Keys中最后一项是method、key参数，所以需要向前移两位
            for (int i = 0; i < context.Request.Form.AllKeys.Length; i++)
            {
                string propName = context.Request.Form.AllKeys[i];
                if (propName.ToLower() == "method") break;
                string propValue = context.Request[propName];
                PropertyInfo p = obj.GetType().GetProperty(propName);
                if (p == null) continue;
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
                switch (p.PropertyType.Name.ToLower())
                {
                    case "guid":
                        fields += ",[" + fieldName + "] = N'" + (string.IsNullOrEmpty(propValue) ? Guid.Empty : Guid.Parse(propValue)) + "' "; break;
                    case "string":
                        ///需要将单引号转换为双引号
                        fields += ",[" + fieldName + "] = N'" + propValue.Replace("'", "''") + "' "; break;
                    case "datetime":
                        fields += ",[" + fieldName + "] = N'" + (string.IsNullOrEmpty(propValue) ? DateTime.Parse("1900-01-01") : DateTime.Parse(propValue)) + "' "; break;
                    case "nullable`1":
                        if (string.IsNullOrEmpty(propValue))
                        {
                            fields += ",[" + fieldName + "] = null ";
                            break;
                        }
                        if (p.PropertyType.FullName.ToLower().Contains("system.int32"))
                        {
                            fields += ",[" + fieldName + "] = " + int.Parse(propValue) + " ";
                        }
                        if (p.PropertyType.FullName.ToLower().Contains("system.int64"))
                        {
                            fields += ",[" + fieldName + "] = " + int.Parse(propValue) + " ";
                        }
                        if (p.PropertyType.FullName.ToLower().Contains("system.boolean"))
                        {
                            fields += ",[" + fieldName + "] = " + (propValue.ToLower() == "30" ? 1 : 0) + " ";
                        }
                        if (p.PropertyType.FullName.ToLower().Contains("system.decimal"))
                        {
                            fields += ",[" + fieldName + "] = " + decimal.Parse(propValue) + " ";
                        }
                        if (p.PropertyType.FullName.ToLower().Contains("system.guid"))
                        {
                            fields += ",[" + fieldName + "] = N'" + Guid.Parse(propValue) + "' ";
                        }
                        if (p.PropertyType.FullName.ToLower().Contains("system.datetime"))
                        {
                            fields += ",[" + fieldName + "] = N'" + DateTime.Parse(propValue) + "' ";
                        }
                        break;
                    case "int32":
                    case "int64":
                        fields += ",[" + fieldName + "] = " + (string.IsNullOrEmpty(propValue) ? 0 : int.Parse(propValue)) + " "; break;
                    case "bool":
                        fields += ",[" + fieldName + "] = " + (propValue.ToLower() == "30" ? 1 : 0) + " "; break;
                    case "decimal":
                        fields += ",[" + fieldName + "] = " + (string.IsNullOrEmpty(propValue) ? 0 : decimal.Parse(propValue)) + " "; break;
                }
            }
            ///修改人
            PropertyInfo pModify = obj.GetType().GetProperty("ModifyUser");
            if (pModify != null)
                fields += ",[MODIFY_USER] = N'" + HandlerCommon.LoginUser + "' ";
            pModify = obj.GetType().GetProperty("UpdateUser");
            if (pModify != null)
                fields += ",[UPDATE_USER] = N'" + HandlerCommon.LoginUser + "' ";
            ///修改时间
            pModify = obj.GetType().GetProperty("ModifyDate");
            if (pModify != null)
                fields += ",[MODIFY_DATE] = GETDATE() ";
            pModify = obj.GetType().GetProperty("UpdateDate");
            if (pModify != null)
                fields += ",[UPDATE_DATE] = GETDATE() ";
            ///

            return fields;
        }
        /// <summary>
        /// 获取更新字段集合
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static List<CommonField> GetCommonFields(HttpContext context)
        {
            string entityName = GetEntityName(context);
            string enityNameFromUI = context.Request["ENTITY_NAME"];
            if (enityNameFromUI.IndexOf("_") != -1)
            {
                enityNameFromUI = enityNameFromUI.Substring(0, enityNameFromUI.IndexOf("_"));
            }
            if (enityNameFromUI.Contains(entityName))
            {
                entityName = enityNameFromUI;
            }
            string moduleName = GetModuleName(context);
            object obj = DataCommon.GetClassObject("DM." + moduleName, entityName + "Info");
            List<CommonField> commonfieldlist = new List<CommonField>();
            ///此处因为Keys中最后一项是method、key参数，所以需要向前移两位
            for (int i = 0; i < context.Request.Form.AllKeys.Length; i++)
            {
                string propName = context.Request.Form.AllKeys[i];
                if (propName.ToLower() == "method") break;
                string propValue = context.Request[propName];
                PropertyInfo p = obj.GetType().GetProperty(propName);
                if (p == null) continue;
                #region FIELD_NAME
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
                #endregion
                CommonField commonfield = new CommonField();
                commonfield.FieldName = fieldName;
                commonfield.PropName = propName;
                commonfield.FieldValue = propValue;
                commonfield.DataType = p.PropertyType.Name;
                if (commonfield.DataType.ToLower() == "nullable`1")
                {
                    if (p.PropertyType.FullName.ToLower().Contains("system.int32"))
                        commonfield.DataType = "int32?";
                    if (p.PropertyType.FullName.ToLower().Contains("system.int64"))
                        commonfield.DataType = "int64?";
                    if (p.PropertyType.FullName.ToLower().Contains("system.boolean"))
                        commonfield.DataType = "boolean?";
                    if (p.PropertyType.FullName.ToLower().Contains("system.decimal"))
                        commonfield.DataType = "decimal?";
                    if (p.PropertyType.FullName.ToLower().Contains("system.guid"))
                        commonfield.DataType = "guid?";
                    if (p.PropertyType.FullName.ToLower().Contains("system.datetime"))
                        commonfield.DataType = "datetime?";
                }
                commonfieldlist.Add(commonfield);
            }
            return commonfieldlist;
        }
        /// <summary>
        /// 获取UPDATE字段的SQL语句
        /// </summary>
        /// <param name="context"></param>
        /// <returns>XR.L 2016.7.7</returns>
        public static string GetUpdateFields(HttpContext context, object obj)
        {
            //string entityName = GetEntityName(context);
            //string moduleName = GetModuleName(context);
            //object obj = DataCommon.GetClassObject("DM." + moduleName, entityName + "Info");
            string fields = string.Empty;


            ///此处因为Keys中最后一项是method、key参数，所以需要向前移两位
            for (int i = 0; i < context.Request.Form.AllKeys.Length; i++)
            {
                string propName = context.Request.Form.AllKeys[i];
                if (propName.ToLower() == "method") break;
                string propValue = context.Request[propName];
                PropertyInfo p = obj.GetType().GetProperty(propName);
                if (p == null) continue;
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
                switch (p.PropertyType.Name.ToLower())
                {
                    case "guid":
                        fields += ",[" + fieldName + "] = '" + (string.IsNullOrEmpty(propValue.Replace("^", "")) ? Guid.Empty : Guid.Parse(propValue.Replace("^", ""))) + "' "; break;
                    case "string":
                        fields += ",[" + fieldName + "] = N'" + propValue + "' "; break;
                    case "datetime":
                        fields += ",[" + fieldName + "] = N'" + (string.IsNullOrEmpty(propValue) ? DateTime.Parse("1900-01-01") : DateTime.Parse(propValue)) + "' "; break;
                    case "nullable`1":
                        if (p.PropertyType.FullName.ToLower().Contains("system.int32"))
                        {
                            fields += ",[" + fieldName + "] = " + (string.IsNullOrEmpty(propValue) ? 0 : int.Parse(propValue)) + " ";
                        }
                        if (p.PropertyType.FullName.ToLower().Contains("system.boolean"))
                        {
                            fields += ",[" + fieldName + "] = " + (propValue.ToLower() == "30" ? 1 : 0) + " ";
                        }
                        if (p.PropertyType.FullName.ToLower().Contains("system.decimal"))
                        {
                            fields += ",[" + fieldName + "] = " + (string.IsNullOrEmpty(propValue) ? 0 : decimal.Parse(propValue)) + " ";
                        }
                        if (p.PropertyType.FullName.ToLower().Contains("system.guid"))
                        {
                            fields += ",[" + fieldName + "] = '" + (string.IsNullOrEmpty(propValue.Replace("^", "")) ? Guid.Empty : Guid.Parse(propValue.Replace("^", ""))) + "' ";
                        }
                        if (p.PropertyType.FullName.ToLower().Contains("system.datetime"))
                        {
                            fields += ",[" + fieldName + "] = '" + (string.IsNullOrEmpty(propValue) ? DateTime.Parse("1900-01-01") : DateTime.Parse(propValue)) + "' ";
                        }
                        break;
                    case "int32":
                    case "int64":
                        fields += ",[" + fieldName + "] = " + (string.IsNullOrEmpty(propValue) ? 0 : int.Parse(propValue)) + " "; break;
                    case "bool":
                        fields += ",[" + fieldName + "] = " + (propValue.ToLower() == "30" ? 1 : 0) + " "; break;
                }
            }
            return fields;
        }
        /// <summary>
        /// 获取UPDATE字段的SQL语句
        /// </summary>
        /// <param name="context"></param>
        /// <returns>XR.L 2016.7.7</returns>
        public static object GetUpdateObject(HttpContext context, object obj)
        {
            ///此处因为Keys中最后一项是method、key参数，所以需要向前移两位
            for (int i = 0; i < context.Request.Form.AllKeys.Length; i++)
            {
                string propName = context.Request.Form.AllKeys[i];
                if (propName.ToLower() == "method") break;
                string propValue = context.Request[propName];
                PropertyInfo p = obj.GetType().GetProperty(propName);
                if (p == null) continue;
                switch (p.PropertyType.Name.ToLower())
                {
                    case "guid":
                        p.SetValue(obj, string.IsNullOrEmpty(propValue) ? Guid.Empty : Guid.Parse(propValue), null); break;
                    case "string":
                        p.SetValue(obj, propValue, null); break;
                    case "datetime":
                        p.SetValue(obj, string.IsNullOrEmpty(propValue) ? DateTime.Parse("1900-01-01") : DateTime.Parse(propValue), null); break;
                    case "nullable`1":
                        if (p.PropertyType.FullName.ToLower().Contains("system.int32"))
                            p.SetValue(obj, string.IsNullOrEmpty(propValue) ? 0 : int.Parse(propValue), null);
                        if (p.PropertyType.FullName.ToLower().Contains("system.boolean"))
                        {
                            propValue = propValue.Trim() == "30" ? "true" : "false";//zjc.2016.5.31 //在新增的时候值为1报错。只能录入TRUE，在修改时。TRUE值又报错。所以在新增时遇到1改为true
                            p.SetValue(obj, bool.Parse(propValue), null);
                        }
                        if (p.PropertyType.FullName.ToLower().Contains("system.decimal"))
                            p.SetValue(obj, string.IsNullOrEmpty(propValue) ? 0 : decimal.Parse(propValue), null);
                        if (p.PropertyType.FullName.ToLower().Contains("system.guid"))
                            p.SetValue(obj, string.IsNullOrEmpty(propValue) ? Guid.Empty : Guid.Parse(propValue), null);
                        if (p.PropertyType.FullName.ToLower().Contains("system.datetime"))
                            p.SetValue(obj, string.IsNullOrEmpty(propValue) ? DateTime.Parse("1900-01-01") : DateTime.Parse(propValue), null);
                        break;
                    case "int32":
                    case "int64":
                        p.SetValue(obj, string.IsNullOrEmpty(propValue) ? 0 : int.Parse(propValue), null); break;
                    case "bool":
                        propValue = propValue.Trim() == "30" ? "true" : "false"; p.SetValue(obj, bool.Parse(propValue), null); break;
                }
            }
            return obj;
        }
        /// <summary>
        /// 校验是否context中传回参数为空or无效
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyOrUndefined(string text)
        {
            if (string.IsNullOrEmpty(text)) return true;
            if (text.ToLower() == "undefined") return true;
            if (text.ToLower() == "nan") return true;
            return false;
        }
        public static object GetObject(System.Data.DataRow row, System.Data.DataColumnCollection colums, object obj, string entityName)
        {
            for (var i = 0; i < colums.Count; i++)
            {
                PropertyInfo p = obj.GetType().GetProperty(colums[i].ColumnName.ToString());
                string propValue = row[colums[i].ColumnName.ToString()].ToString();
                switch (p.PropertyType.Name.ToLower())
                {
                    case "string": p.SetValue(obj, propValue, null); break;
                    case "nullable`1":
                        if (string.IsNullOrEmpty(propValue))
                        {
                            p.SetValue(obj, null, null);
                            break;
                        }
                        if (p.PropertyType.FullName.ToLower().Contains("system.int32"))
                            p.SetValue(obj, int.Parse(propValue), null);
                        if (p.PropertyType.FullName.ToLower().Contains("system.boolean"))
                        {
                            propValue = propValue.Trim() == "30" ? "true" : "false";//zjc.2016.5.31 //在新增的时候值为1报错。只能录入TRUE，在修改时。TRUE值又报错。所以在新增时遇到1改为true
                            p.SetValue(obj, bool.Parse(propValue), null);
                        }
                        if (p.PropertyType.FullName.ToLower().Contains("system.decimal"))
                            p.SetValue(obj, decimal.Parse(propValue), null);
                        if (p.PropertyType.FullName.ToLower().Contains("system.guid"))
                        {
                            ///TODO:firefox中replace(/'/g, "")未能执行成功，临时代码依靠c#解决
                            p.SetValue(obj, Guid.Parse(propValue.Replace("'", "")), null);
                        }
                        if (p.PropertyType.FullName.ToLower().Contains("system.datetime"))
                            p.SetValue(obj, DateTime.Parse(propValue), null);
                        break;
                    case "int32": p.SetValue(obj, string.IsNullOrEmpty(propValue) ? 0 : int.Parse(propValue), null); break;
                    case "bool": propValue = propValue.Trim() == "30" ? "true" : "false"; p.SetValue(obj, bool.Parse(propValue), null); break;
                    case "guid": p.SetValue(obj, string.IsNullOrEmpty(propValue) ? Guid.Empty : Guid.Parse(propValue.Replace("'", "")), null); break;
                    case "datetime": p.SetValue(obj, string.IsNullOrEmpty(propValue) ? DateTime.Parse("1900-01-01") : DateTime.Parse(propValue), null); break;
                }
            }
            if (SYSHandler.ValidateFields(obj, entityName) == false)
            {
                throw new Exception();
            }
            return obj;
        }
        /// <summary>
        /// 上传文件完成后执行的后台函数
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <param name="key"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static object UploadFileFinished(string assemblyName, string className, string methodName, string key, string loginUser)
        {
            ///构建参数
            object[] param = new object[] { key, loginUser };
            ///实例化BLL对象
            object classObject = DataCommon.GetClassObject(assemblyName, className);
            ///查找UploadFileFinished函数
            MethodInfo met = classObject.GetType().GetMethod(methodName);
            ///执行函数
            return met.Invoke(classObject, param);
        }
        /// <summary>
        /// 上传图片文件到数据库
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <param name="imageContent"></param>
        /// <param name="imageColumnName"></param>
        /// <param name="key"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static object SaveImageToDatabase(string assemblyName, string className, byte[] imageContent, string extensionName, string key, string loginUser)
        {
            ///构建参数
            object[] param = new object[] { imageContent, extensionName, key, loginUser };
            ///实例化BLL对象
            object classObject = DataCommon.GetClassObject(assemblyName, className);
            ///
            MethodInfo met = classObject.GetType().GetMethod("UpdateImage");
            ///执行函数
            return met.Invoke(classObject, param);
        }
        /// <summary>
        /// 从数据库读取图片
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object ReadImageFromDatabase(string assemblyName, string className, string key)
        {
            ///构建参数
            object[] param = new object[] { key };
            ///实例化BLL对象
            object classObject = DataCommon.GetClassObject(assemblyName, className);
            ///
            MethodInfo met = classObject.GetType().GetMethod("ReadImage");
            ///执行函数
            return met.Invoke(classObject, param);
        }
    }
}