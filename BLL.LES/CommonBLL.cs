namespace BLL.LES
{
    using DAL.SYS;
    using DM.LES;
    using DM.SYS;
    using Infrustructure.Logging;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Transactions;

    public class CommonBLL
    {
        /// <summary>
        /// 自身系统简称
        /// </summary>
        private static readonly string ownerSystem = "LES";
        public static bool ExecuteNonQueryBySql(string sql, int cmdTimeout = 60)
        {
            return CommonDAL.ExecuteNonQueryBySql(sql, cmdTimeout);
        }
        /// <summary>
        /// 用于EXCEL导出数据获取
        /// </summary>
        /// <param name="whereText"></param>
        /// <param name="orderText"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public DataTable GetDatatableForExcel(string tableName, List<string> columns, string whereText, string orderText)
        {
            if (columns.Count == 0) return null;
            string query = string.Empty;
            if (string.IsNullOrEmpty(whereText))
                query = string.Empty;
            else
            {
                if (whereText.Trim().StartsWith("and", StringComparison.OrdinalIgnoreCase))
                    query = whereText;
                else
                    query = " and " + whereText;
            }
            if (!string.IsNullOrEmpty(orderText))
                query += " order by " + orderText;
            if (tableName.StartsWith("LES."))
                tableName = tableName.Replace("LES.", string.Empty);

            string sql = "select [" + string.Join("],[", columns.Where(d => !d.StartsWith("#")).ToArray()) + "] from [" + ownerSystem + "].[" + tableName + "] with(nolock) where [VALID_FLAG] = 1 " + query;
            return CommonDAL.ExecuteDataTableBySql(sql);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="tableName"></param>
        /// <param name="entityFieldInfos"></param>
        /// <param name="textWhere"></param>
        /// <returns></returns>
        public List<object> GetFooterData(string moduleName, string tableName, List<EntityFieldInfo> entityFieldInfos, string textWhere)
        {
            if (string.IsNullOrEmpty(moduleName))
                throw new Exception("0x00000425");///架构为空
            string schema = moduleName;
            if (schema.ToUpper() == "SYS")
                schema = "dbo";

            if (string.IsNullOrEmpty(tableName))
                throw new Exception("0x00000426");///数据库表名不能为空

            if (entityFieldInfos.Count == 0)
                throw new Exception("0x00000427");///统计字段为空

            ///统计字段
            string fields = string.Empty;
            foreach (var entityFieldInfo in entityFieldInfos)
            {
                if (!entityFieldInfo.StatisticsFlag.GetValueOrDefault()) continue;
                if (!string.IsNullOrEmpty(entityFieldInfo.StatisticsTitle))
                {
                    fields += ",'" + entityFieldInfo.StatisticsTitle + "' as " + entityFieldInfo.FieldName + "";
                    continue;
                }
                string statisticsType = "sum";
                switch (entityFieldInfo.StatisticsType.GetValueOrDefault())
                {
                    case (int)StatisticsTypeConstants.DataAvg: statisticsType = "avg"; break;
                    case (int)StatisticsTypeConstants.DataCount: statisticsType = "count"; break;
                    case (int)StatisticsTypeConstants.DataMax: statisticsType = "max"; break;
                    case (int)StatisticsTypeConstants.DataMin: statisticsType = "min"; break;
                    case (int)StatisticsTypeConstants.DataSum: statisticsType = "sum"; break;
                }
                fields += "," + statisticsType + "([" + entityFieldInfo.TableFieldName + "]) as " + entityFieldInfo.FieldName + "";
            }
            if (string.IsNullOrEmpty(fields))
                throw new Exception("0x00000427");///统计字段为空

            ///对象名
            string entityName = GetEntityName(tableName);
            ///
            if (string.IsNullOrEmpty(textWhere))
                textWhere = string.Empty;
            else
            {
                if (!textWhere.Trim().StartsWith("and", StringComparison.OrdinalIgnoreCase))
                    textWhere = " and " + textWhere;
            }
            string sql = "select " + fields.Substring(1) + " from [" + schema + "].[" + tableName + "] with(nolock) where [VALID_FLAG] = 1 " + textWhere;
            DataTable dataTable = CommonDAL.ExecuteDataTableBySql(sql);
            List<object> list = new List<object>();
            foreach (DataRow dr in dataTable.Rows)
            {
                object obj = GetClassObject("DM." + moduleName, entityName + "Info");
                foreach (var entityFieldInfo in entityFieldInfos)
                {
                    string propValue = dr[entityFieldInfo.FieldName].ToString();
                    PropertyInfo p = obj.GetType().GetProperty(entityFieldInfo.FieldName);
                    if (p == null) continue;
                    SetPropertyValue(propValue, p, ref obj);
                }
                list.Add(obj);
            }
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propValue"></param>
        /// <param name="propertyInfo"></param>
        /// <param name="obj"></param>
        private static void SetPropertyValue(string propValue, PropertyInfo propertyInfo, ref object obj)
        {
            switch (propertyInfo.PropertyType.Name.ToLower())
            {
                case "nullable`1":
                    if (string.IsNullOrEmpty(propValue))
                    {
                        propertyInfo.SetValue(obj, null, null);
                        break;
                    }
                    if (propertyInfo.PropertyType.FullName.ToLower().Contains("system.int32"))
                        propertyInfo.SetValue(obj, int.Parse(propValue), null);
                    if (propertyInfo.PropertyType.FullName.ToLower().Contains("system.int64"))
                        propertyInfo.SetValue(obj, long.Parse(propValue), null);
                    if (propertyInfo.PropertyType.FullName.ToLower().Contains("system.decimal"))
                        propertyInfo.SetValue(obj, decimal.Parse(propValue), null);
                    break;
                case "string": propertyInfo.SetValue(obj, propValue, null); break;
                case "int32": propertyInfo.SetValue(obj, string.IsNullOrEmpty(propValue) ? 0 : int.Parse(propValue), null); break;
                case "int64": propertyInfo.SetValue(obj, string.IsNullOrEmpty(propValue) ? 0 : long.Parse(propValue), null); break;
                case "decimal": propertyInfo.SetValue(obj, string.IsNullOrEmpty(propValue) ? 0 : decimal.Parse(propValue), null); break;
            }
        }
        /// <summary>
        /// 获取EXCEL所用选项
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="tableName"></param>
        /// <param name="idField"></param>
        /// <param name="textField"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetComboxItems(string assemblyName, string tableName, string idField, string textField)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            assemblyName = assemblyName.Replace("BLL.", string.Empty);
            string sql = "select [" + idField + "],[" + textField + "] from [" + assemblyName + "].[" + tableName + "] with(nolock) where [VALID_FLAG] = 1;";
            DataTable dataTable = CommonDAL.ExecuteDataTableBySql(sql);
            foreach (DataRow dr in dataTable.Rows)
            {
                string strId = dr[idField].ToString();
                if (string.IsNullOrEmpty(strId)) continue;
                keyValuePairs.Add(strId, dr[textField].ToString());
            }
            return keyValuePairs;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="tableName"></param>
        /// <param name="idField"></param>
        /// <param name="textField"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetComboTreeItems(string assemblyName, string tableName, string idField, string textField, string parentId)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            assemblyName = assemblyName.Replace("BLL.", string.Empty);
            string sql = "select [" + idField + "],[" + textField + "],[" + parentId + "] from [" + assemblyName + "].[" + tableName + "] with(nolock) where [VALID_FLAG] = 1;";
            DataTable dataTable = CommonDAL.ExecuteDataTableBySql(sql);
            GetComboTreeItems(ref keyValuePairs, dataTable, string.Empty, idField, textField, parentId);
            return keyValuePairs;
        }
        /// <summary>
        /// 树形递归
        /// </summary>
        /// <param name="treeItems"></param>
        /// <param name="dataTable"></param>
        /// <param name="parentIdValue"></param>
        /// <param name="idField"></param>
        /// <param name="textField"></param>
        /// <param name="parentId"></param>
        private static void GetComboTreeItems(ref Dictionary<string, string> treeItems, DataTable dataTable, string parentIdValue, string idField, string textField, string parentId)
        {
            foreach (DataRow dr in dataTable.Select(parentId + " = '" + parentIdValue + "'"))
            {
                string strId = dr[idField].ToString();
                if (string.IsNullOrEmpty(strId)) continue;
                treeItems.Add(strId, dr[textField].ToString());
                GetComboTreeItems(ref treeItems, dataTable, strId, idField, textField, parentId);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateFields"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetFieldValues(string updateFields)
        {
            Dictionary<string, string> retdictionary = new Dictionary<string, string>();
            string[] fieldvalues = updateFields.Split(new string[]
            { "["
                , ",["
                , "] = N'"
                , "] = "
                , "MODIFY_DATE] = GETDATE() "
                , "UPDATE_DATE] = GETDATE() " }
            //////AND DATEDIFF(DAY,'2018-04-20',CREATE_DATE)>=0 AND DATEDIFF(DAY,'2018-04-23',MODIFY_DATE)<=0
            , StringSplitOptions.None);
            for (int i = 1; i < fieldvalues.Length; i++)
            {
                string ikey = fieldvalues[i];
                string ivalue = fieldvalues[++i];
                if (ivalue.EndsWith("' "))
                    ivalue = ivalue.Substring(0, ivalue.Length - 2);
                if (ivalue.EndsWith(" "))
                    ivalue = ivalue.Substring(0, ivalue.Length - 1);
                if (string.IsNullOrEmpty(ikey)) continue;
                ///当NULL时返回空
                if (ivalue.Trim().ToLower() == "null")
                    ivalue = string.Empty;
                retdictionary.Add(ikey, ivalue);
            }
            return retdictionary;
        }
        /// <summary>
        /// 解析where条件字符串
        /// </summary>
        /// <param name="whereFields"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetWhereFieldValues(string whereFields)
        {
            Dictionary<string, string> retdictionary = new Dictionary<string, string>();
            string[] fieldvalues = whereFields.Split(new string[]
            { " and DATEDIFF(DAY,N'"
                ," and CHARINDEX(N'"
                ," and DATEDIFF(SS,N'"
                ," and 1=0"
                ,"',["
                ,"]) >= 0","]) <= 0","]) > 0","]) < 0"
                ," and [","] = N'","] in "
                ,"] >= ","] <= ","] > ","] < "}
            ///and [SUPPLIER_NUM] in ('A0001')
            ///AND DATEDIFF(DAY,'2018-04-20',[CREATE_DATE])>=0 AND DATEDIFF(DAY,'2018-04-23',MODIFY_DATE)<=0
            , StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < fieldvalues.Length; i++)
            {
                string ivalue = fieldvalues[i];
                string ikey = fieldvalues[++i];
                if (string.IsNullOrEmpty(ikey)) continue;
                retdictionary.Add(ikey, ivalue);
            }
            return retdictionary;
        }

        public static string GetCreateInboundLogSql(string v, Guid logFid, string interfaceCode, string keyValue, object msgContent, string empty1, string empty2, string loginUser, DateTime executeStartTime)
        {
            throw new NotImplementedException();
        }

        public static string GetWhereFieldValue(string whereFields, string fieldName)
        {
            Dictionary<string, string> retdictionary = GetWhereFieldValues(whereFields);
            string fieldValue = string.Empty;
            retdictionary.TryGetValue(fieldName, out fieldValue);
            return fieldValue;
        }
        public static string ClearWhereField(string whereFields, string fieldName, int dataType, int logicType)
        {
            if (string.IsNullOrEmpty(whereFields)) return whereFields;
            ///字段INDEX
            int fieldNameIndex = 0;
            int fieldValueIndex = 0;
            ///剩余SQL
            string remainderSql = string.Empty;
            switch (dataType)
            {
                case (int)DataTypeConstants.DATE:
                    switch (logicType)
                    {
                        case (int)SearchTypeConstants.GREATEREQUAL:
                            fieldNameIndex = whereFields.IndexOf("',[" + fieldName + "]) >= 0");
                            remainderSql = whereFields.Substring(fieldNameIndex + fieldName.Length + 10);
                            break;
                        case (int)SearchTypeConstants.GREATER:
                            fieldNameIndex = whereFields.IndexOf("',[" + fieldName + "]) > 0");
                            remainderSql = whereFields.Substring(fieldNameIndex + fieldName.Length + 9);
                            break;
                        case (int)SearchTypeConstants.LESSEQUAL:
                            fieldNameIndex = whereFields.IndexOf("',[" + fieldName + "]) <= 0");
                            remainderSql = whereFields.Substring(fieldNameIndex + fieldName.Length + 10);
                            break;
                        case (int)SearchTypeConstants.LESS:
                            fieldNameIndex = whereFields.IndexOf("',[" + fieldName + "]) > 0");
                            remainderSql = whereFields.Substring(fieldNameIndex + fieldName.Length + 9);
                            break;
                        case (int)SearchTypeConstants.EQUAL:
                            fieldNameIndex = whereFields.IndexOf(" and [" + fieldName + "] = N'");
                            whereFields = whereFields.Substring(0, fieldNameIndex);
                            remainderSql = whereFields.Substring(fieldNameIndex + fieldName.Length + 10);
                            fieldValueIndex = remainderSql.IndexOf("' and");
                            if (fieldValueIndex > -1)
                                remainderSql = remainderSql.Substring(fieldValueIndex + 1);
                            else
                                remainderSql = string.Empty;
                            return whereFields + remainderSql;
                        case (int)SearchTypeConstants.LIKE: return whereFields;
                    }
                    whereFields = whereFields.Substring(0, fieldNameIndex);
                    fieldValueIndex = whereFields.LastIndexOf(" and DATEDIFF(DAY,N'");
                    whereFields = whereFields.Substring(0, fieldValueIndex);
                    break;
            }
            return whereFields + remainderSql;
        }
        /// <summary>
        /// 从UPDATE语句中获取某字段的值
        /// </summary>
        /// <param name="updateFields">string</param>
        /// <param name="fieldName">string</param>
        /// <returns>string</returns>
        public static string GetFieldValue(string updateFields, string fieldName)
        {
            Dictionary<string, string> retdictionary = GetFieldValues(updateFields);
            string fieldValue = string.Empty;
            retdictionary.TryGetValue(fieldName, out fieldValue);
            return fieldValue;
        }
        /// <summary>
        /// 用后台处理后的字段值替换原先UPDATE语句中的值
        /// </summary>
        /// <param name="updateFields"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <param name="quotesFlag"></param>
        /// <returns></returns>
        public static string SetFieldValue(string updateFields, string fieldName, string fieldValue, bool quotesFlag = true)
        {
            ///字段INDEX
            int fieldNameIndex = updateFields.IndexOf(",[" + fieldName + "]");
            if (fieldNameIndex == -1)
                return updateFields + ",[" + fieldName + "] = " + (quotesFlag ? "N'" + fieldValue + "' " : fieldValue + " ");
            ///拼接SQL
            string updateFieldSql = updateFields.Substring(0, fieldNameIndex);
            ///剩余SQL
            string remainderSql = updateFields.Substring(fieldNameIndex + fieldName.Length + 2);
            ///值INDEX
            int fieldValueIndex = remainderSql.IndexOf(",[");

            return updateFieldSql + ",[" + fieldName + "] = " + (quotesFlag ? "N'" + fieldValue + "' " : fieldValue + " ") + remainderSql.Substring(fieldValueIndex);
        }

        public static string GetFieldValue(List<CommonField> list, string propName)
        {
            var info = list.FirstOrDefault(d => d.PropName.ToLower() == propName.ToLower());
            if (info == null) return string.Empty;
            return info.FieldValue;
        }

        public static string GetUpdateFieldSql(List<CommonField> list, string tableName)
        {
            string updateFields = string.Empty;
            foreach (var info in list)
            {
                if (info.PropName.Contains("_"))
                {
                    string tn = info.PropName.Split(new char[] { '_' })[0];
                    if (tn.ToLower() != tableName.ToLower()) continue;
                    string propName = info.PropName.Split(new char[] { '_' })[1];
                    info.FieldName = propName.Substring(0, 1).ToUpper();
                    for (int j = 1; j < propName.Length; j++)
                    {
                        if (propName[j] >= 'a' && propName[j] <= 'z')
                            info.FieldName += propName[j].ToString().ToUpper();
                        else if (propName[j] >= 'A' && propName[j] <= 'Z')
                            info.FieldName += "_" + propName[j].ToString().ToUpper();
                        else
                            info.FieldName += propName[j].ToString();
                    }
                }
                switch (info.DataType.ToLower())
                {
                    case "guid": updateFields += ",[" + info.FieldName + "] = '" + (string.IsNullOrEmpty(info.FieldValue) ? Guid.Empty : Guid.Parse(info.FieldValue)) + "' "; break;
                    case "string": updateFields += ",[" + info.FieldName + "] = N'" + info.FieldValue + "' "; break;
                    case "datetime": updateFields += ",[" + info.FieldName + "] = N'" + (string.IsNullOrEmpty(info.FieldValue) ? DateTime.Parse("1900-01-01") : DateTime.Parse(info.FieldValue)) + "' "; break;
                    case "int32": updateFields += ",[" + info.FieldName + "] = " + (string.IsNullOrEmpty(info.FieldValue) ? 0 : int.Parse(info.FieldValue)) + " "; break;
                    case "int64": updateFields += ",[" + info.FieldName + "] = " + (string.IsNullOrEmpty(info.FieldValue) ? 0 : long.Parse(info.FieldValue)) + " "; break;
                    case "bool": updateFields += ",[" + info.FieldName + "] = " + (info.FieldValue.ToLower() == "30" ? 1 : 0) + " "; break;
                    case "decimal": updateFields += ",[" + info.FieldName + "] = " + (string.IsNullOrEmpty(info.FieldValue) ? 0 : decimal.Parse(info.FieldValue)) + " "; break;
                    case "int32?": updateFields += ",[" + info.FieldName + "] = " + (string.IsNullOrEmpty(info.FieldValue) ? "NULL" : int.Parse(info.FieldValue).ToString()) + " "; break;
                    case "int64?": updateFields += ",[" + info.FieldName + "] = " + (string.IsNullOrEmpty(info.FieldValue) ? "NULL" : long.Parse(info.FieldValue).ToString()) + " "; break;
                    case "boolean?": updateFields += ",[" + info.FieldName + "] = " + (string.IsNullOrEmpty(info.FieldValue) ? "NULL" : (info.FieldValue == "30" ? "1" : "0")) + " "; break;
                    case "decimal?": updateFields += ",[" + info.FieldName + "] = " + (string.IsNullOrEmpty(info.FieldValue) ? "NULL" : decimal.Parse(info.FieldValue).ToString()) + " "; break;
                    case "guid?": updateFields += ",[" + info.FieldName + "] = " + (string.IsNullOrEmpty(info.FieldValue) ? "NULL" : ("N'" + Guid.Parse(info.FieldValue).ToString() + "'")) + " "; break;
                    case "datetime?": updateFields += ",[" + info.FieldName + "] = " + (string.IsNullOrEmpty(info.FieldValue) ? "NULL" : ("N'" + DateTime.Parse(info.FieldValue).ToString() + "'")) + " "; break;
                }
            }
            return updateFields;
        }

        public static object FieldNullToEmpty(object entity)
        {
            foreach (var item in entity.GetType().GetProperties())
            {
                if (item.PropertyType.Name.ToLower() == "string"
                    && item.GetValue(entity, null) == null)
                {
                    item.SetValue(entity, string.Empty, null);
                }
            }
            return entity;
        }


        /// <summary>
        /// IFM-004系统接口-接收
        /// </summary>
        /// <param name="sourceSystem"></param>
        /// <param name="targetSystem"></param>
        /// <param name="logFid"></param>
        /// <param name="methodCode"></param>
        /// <param name="keyValue"></param>
        /// <param name="msgContent"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMsg"></param>
        /// <param name="loginUser"></param>
        /// <param name="executeStartTime"></param>
        /// <returns></returns>
        public static bool WriteInboundLog(string sourceSystem, Guid logFid, string methodCode, string keyValue, string msgContent, string errorCode, string errorMsg, string loginUser, DateTime executeStartTime)
        {
            string sql = GetCreateInboundLogSql(sourceSystem, logFid, methodCode, keyValue, msgContent, errorCode, errorMsg, loginUser, executeStartTime);
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
        /// <summary>
        /// IFM-004系统接口-接收
        /// </summary>
        /// <param name="sourceSystem"></param>
        /// <param name="logFid"></param>
        /// <param name="methodCode"></param>
        /// <param name="keyValue"></param>
        /// <param name="msgContent"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMsg"></param>
        /// <param name="loginUser"></param>
        /// <param name="executeStartTime"></param>
        /// <returns></returns>
        public static string GetCreateInboundLogSql(string sourceSystem, Guid logFid, string methodCode, string keyValue, string msgContent, string errorCode, string errorMsg, string loginUser, DateTime executeStartTime, int executeResult = (int)ExecuteResultConstants.Success)
        {
            ///交易编号
            string transNo = new SeqDefineDAL().GetCurrentCode("INTERFACE_TRANS_NO", sourceSystem.ToUpper(), ownerSystem);
            return "insert into [LES].[TI_IFM_" + sourceSystem.ToUpper() + "_INBOUND_LOG] "
                + "(FID, TRANS_NO, SOURCE_SYSTEM, TARGET_SYSTEM, METHOD_CODE, EXECUTE_START_TIME, EXECUTE_END_TIME, EXECUTE_RESULT, EXECUTE_TIMES, KEY_VALUE, MSG_CONTENT, ERROR_CODE, ERROR_MSG, VALID_FLAG, CREATE_USER, CREATE_DATE) "
                + "values (N'" + logFid + "', N'" + transNo + "', N'" + sourceSystem + "', N'" + ownerSystem + "', N'" + methodCode + "', N'" + executeStartTime + "', GETDATE(), " + executeResult + ", 1, N'" + keyValue + "', N'" + msgContent.Replace("'", "''") + "', N'" + errorCode + "', N'" + errorMsg + "', 1, N'" + loginUser + "', GETDATE());";
        }
        /// <summary>
        /// IFM-005系统接口-发送
        /// </summary>
        /// <param name="targetSystem"></param>
        /// <param name="logFid"></param>
        /// <param name="methodCode"></param>
        /// <param name="keyValue"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static bool CreateOutboundLog(string targetSystem, Guid logFid, string methodCode, string keyValue, string loginUser)
        {
            string sql = GetCreateOutboundLogSql(targetSystem, logFid, methodCode, keyValue, loginUser);
            return CommonDAL.ExecuteNonQueryBySql(sql);
        }
        /// <summary>
        /// IFM-005系统接口-发送
        /// </summary>
        /// <param name="targetSystem"></param>
        /// <param name="logFid"></param>
        /// <param name="methodCode"></param>
        /// <param name="keyValue"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static string GetCreateOutboundLogSql(string targetSystem, Guid logFid, string methodCode, string keyValue, string loginUser)
        {
            ///交易编号
            string transNo = new SeqDefineDAL().GetCurrentCode("INTERFACE_TRANS_NO", ownerSystem, targetSystem.ToUpper());
            return "insert into [LES].[TI_IFM_" + targetSystem.ToUpper() + "_OUTBOUND_LOG] "
                + "(FID, TRANS_NO, SOURCE_SYSTEM, TARGET_SYSTEM, METHOD_CODE, KEY_VALUE, EXECUTE_RESULT, VALID_FLAG, CREATE_USER, CREATE_DATE) "
                + "values (N'" + logFid + "', N'" + transNo + "', N'" + ownerSystem + "', N'" + targetSystem.ToUpper() + "', N'" + methodCode + "', N'" + keyValue + "', " + (int)ExecuteResultConstants.Submit + ", 1, N'" + loginUser + "', GETDATE());"
                + "insert into [LES].[TI_IFM_" + targetSystem.ToUpper() + "_OUTBOUND_DETAIL_LOG] "
                + "(FID, LOG_FID, TRANS_NO, METHOD_CODE, EXECUTE_RESULT, VALID_FLAG, CREATE_USER, CREATE_DATE,EXECUTE_START_TIME) "
                + "values (NEWID(), N'" + logFid + "', N'" + transNo + "', N'" + methodCode + "', " + (int)ExecuteResultConstants.Submit + ", 1, N'" + loginUser + "', GETDATE(),GETDATE());";
        }
        /// <summary>
        /// 反射构建对象
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
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
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private static string GetEntityName(string tableName)
        {
            string entityName = string.Empty;
            string[] arrayTableName = tableName.Split('_');
            ///说明不是表名，是直接可以启用的对象名
            if (arrayTableName.Length < 3)
                throw new Exception("0x00000428");///数据库表名错误

            for (int i = 2; i < arrayTableName.Length; i++)
            {
                entityName += arrayTableName[i].Substring(0, 1).ToUpper() + arrayTableName[i].Substring(1).ToLower();
            }
            return entityName;
        }
        /// <summary>
        /// IFM-005系统接口-发送
        /// </summary>
        /// <param name="targetSystem"></param>
        /// <param name="id"></param>
        /// <param name="keyValue"></param>
        /// <param name="executeResult"></param>
        /// <param name="msgContent"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMsg"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static bool UpdateResultLog(string targetSystem, long id, ExecuteResultConstants executeResult, string msgContent, string errorCode, string errorMsg, string loginUser)
        {
            string sql = GetUpdateResultLogSql(targetSystem, id, executeResult, msgContent, errorCode, errorMsg, loginUser);
            using (var trans = new TransactionScope())
            {
                if (!string.IsNullOrEmpty(sql))
                    CommonDAL.ExecuteNonQueryBySql(sql);
                trans.Complete();
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetSystem"></param>
        /// <param name="id"></param>
        /// <param name="executeResult"></param>
        /// <param name="msgContent"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMsg"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static string GetUpdateResultLogSql(string targetSystem, long id, ExecuteResultConstants executeResult, string msgContent, string errorCode, string errorMsg, string loginUser)
        {
            ///时间更新条件
            string timeUpdateFields = string.Empty;
            if (executeResult == ExecuteResultConstants.Processing)
                timeUpdateFields = ",[EXECUTE_START_TIME] = GETDATE(),[EXECUTE_TIMES] = ISNULL([EXECUTE_TIMES],0) + 1 ";
            else if (executeResult == ExecuteResultConstants.Resend)
                timeUpdateFields = ",[EXECUTE_START_TIME] = NULL,[EXECUTE_END_TIME] = NULL ";
            else if (executeResult == ExecuteResultConstants.Cancel)
                timeUpdateFields = string.Empty;
            else
                timeUpdateFields = ",[EXECUTE_END_TIME] = GETDATE() ";

            return "update [LES].[TI_IFM_" + targetSystem.ToUpper() + "_OUTBOUND_LOG] "
                + "set [EXECUTE_RESULT] = " + (int)executeResult + ","
                + "[MSG_CONTENT] = N'" + msgContent + "',"
                + "[ERROR_CODE] = N'" + errorCode + "',"
                + "[ERROR_MSG] = N'" + errorMsg + "',"
                + "[MODIFY_USER] = N'" + loginUser + "',"
                + "[MODIFY_DATE] = GETDATE()"
                + timeUpdateFields
                + "where [ID] = " + id + ";"
                + "insert into [LES].[TI_IFM_" + targetSystem.ToUpper() + "_OUTBOUND_DETAIL_LOG] "
                + "(FID, LOG_FID, TRANS_NO, METHOD_CODE, EXECUTE_START_TIME, EXECUTE_END_TIME, EXECUTE_RESULT, ERROR_CODE, ERROR_MSG, VALID_FLAG, CREATE_USER, CREATE_DATE) "
                + "select NEWID(),[FID],[TRANS_NO],[METHOD_CODE],[EXECUTE_START_TIME],[EXECUTE_END_TIME],[EXECUTE_RESULT],[ERROR_CODE],[ERROR_MSG],1,N'" + loginUser + "',GETDATE() "
                + "from [LES].[TI_IFM_" + targetSystem.ToUpper() + "_OUTBOUND_LOG] "
                + "where [ID] = " + id + ";";
        }
        /// <summary>
        /// IFM-005系统接口-发送
        /// </summary>
        /// <param name="targetSystem"></param>
        /// <param name="id"></param>
        /// <param name="keyValue"></param>
        /// <param name="executeResult"></param>
        /// <param name="msgContent"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMsg"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static bool UpdateProcessingLog(string targetSystem, long id, string loginUser)
        {
            return UpdateResultLog(targetSystem, id, ExecuteResultConstants.Processing, string.Empty, string.Empty, string.Empty, loginUser);
        }
        /// <summary>
        /// 获取分页的DATATABLE，并获取总数据行数
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="tableName"></param>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCnt"></param>
        /// <returns></returns>
        public static DataTable GetDataTableByPage(string tableName, string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCnt)
        {
            return CommonDAL.GetDataTableByPage("LES", tableName, textWhere, textOrder, pageIndex, pageRow, out dataCnt);
        }
        /// <summary>
        /// 字符串转换为日期格式
        /// </summary>
        /// <param name="dateStr"></param>
        /// <param name="dateFormat"></param>
        /// <returns></returns>
        public static DateTime TryParseDatetime(string dateStr, string dateFormat = "yyyyMMdd")
        {
            if (string.IsNullOrEmpty(dateStr))
                throw new Exception("MC:0x00000393");///日期格式错误
            string[] format = { dateFormat };
            DateTime.TryParseExact(dateStr,
                                   format,
                                   CultureInfo.InvariantCulture,
                                   DateTimeStyles.None,
                                   out DateTime parsedate);
            ///如果Out日期格式是默认最小
            if (parsedate == DateTime.MinValue)
                throw new Exception("MC:0x00000393");///日期格式错误
            if (parsedate < new DateTime(1900, 1, 1))
                throw new Exception("MC:0x00000393");///日期格式错误
            return parsedate;
        }
    }
}
