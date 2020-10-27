namespace BLL.SYS
{
    using DAL.SYS;
    using DM.SYS;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Linq;

    public class CommonBLL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateFields"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetFieldValues(string updateFields)
        {
            Dictionary<string, string> retdictionary = new Dictionary<string, string>();
            string[] fieldvalues = updateFields.Split(new string[] { "[", ",[", "] = N'", "] = ", ",CHARINDEX", "MODIFY_DATE] = GETDATE() ", "UPDATE_DATE] = GETDATE() ", ",1=0 " }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < fieldvalues.Length; i++)
            {
                if (fieldvalues[i].Replace(" ", string.Empty).Length == 0) continue;
                string ikey = fieldvalues[i];
                string ivalue = string.Empty;
                if (ikey.StartsWith("(N'") && ikey.EndsWith("'"))
                {
                    ivalue = ikey.Substring(0, ikey.Length - 1).Replace("(N'", string.Empty);
                    ikey = fieldvalues[++i].Replace("]) > 0", string.Empty);
                }
                else
                    ivalue = fieldvalues[++i];
                if (ivalue.EndsWith("' "))
                    ivalue = ivalue.Substring(0, ivalue.Length - 2);
                if (ivalue.EndsWith(" "))
                    ivalue = ivalue.Substring(0, ivalue.Length - 1);
                if (string.IsNullOrEmpty(ikey)||ikey== "null ") continue;
                retdictionary.Add(ikey, ivalue);
            }
            return retdictionary;
        }
        /// <summary>
        /// 从UPDATE语句中获取某字段的值
        /// </summary>
        /// <param name="updateFields">string</param>
        /// <param name="fieldName">string</param>
        /// <returns>string</returns>
        public static string GetFieldValue(string updateFields, string fieldName)
        {
            updateFields = updateFields.Replace("and ", ",").Replace("AND ", ",");
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
            ///拼接SQL
            string updateFieldSql = updateFields.Substring(0, fieldNameIndex);
            ///剩余SQL
            string remainderSql = updateFields.Substring(fieldNameIndex + fieldName.Length + 2);
            ///值INDEX
            int fieldValueIndex = remainderSql.IndexOf(",[");

            return updateFieldSql + ",[" + fieldName + "] = " + (quotesFlag ? "N'" + fieldValue + "' " : fieldValue + " ") + remainderSql.Substring(fieldValueIndex);
        }
        /// <summary>
        /// 清除某字段的更新
        /// </summary>
        /// <param name="updateFields"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static string ClearField(string updateFields, string fieldName)
        {
            ///字段INDEX
            int fieldNameIndex = updateFields.IndexOf(",[" + fieldName + "]");
            ///拼接SQL
            string updateFieldSql = updateFields.Substring(0, fieldNameIndex);
            ///剩余SQL
            string remainderSql = updateFields.Substring(fieldNameIndex + fieldName.Length + 2);
            ///值INDEX
            int fieldValueIndex = remainderSql.IndexOf(",[");

            return updateFieldSql + remainderSql.Substring(fieldValueIndex);
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
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdTimeout"></param>
        /// <returns></returns>
        public static bool ExecuteNonQueryBySql(string sql, int cmdTimeout = 60)
        {
            return CommonDAL.ExecuteNonQueryBySql(sql, cmdTimeout);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdTimeout"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, int cmdTimeout = 60)
        {
            return CommonDAL.ExecuteScalar(sql, cmdTimeout);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdTimeout"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTableBySql(string sql, int cmdTimeout = 60)
        {
            return CommonDAL.ExecuteDataTableBySql(sql, cmdTimeout);
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
            string sql = "select [" + idField + "],[" + textField + "],[" + parentId + "] from dbo.[" + tableName + "] with(nolock) where [VALID_FLAG] = 1;";
            DataTable dataTable = CommonDAL.ExecuteDataTableBySql(sql);
            GetComboTreeItems(ref keyValuePairs, dataTable, string.Empty, string.Empty, idField, textField, parentId);
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
        private static void GetComboTreeItems(ref Dictionary<string, string> treeItems, DataTable dataTable, string parentName, string parentIdValue, string idField, string textField, string parentId)
        {
            string codition = string.Empty;
            if (string.IsNullOrEmpty(parentIdValue))
                codition = parentId + " = '' or " + parentId + " is null";
            else
                codition = parentId + " = '" + parentIdValue + "'";
            foreach (DataRow dr in dataTable.Select(codition))
            {
                string strId = dr[idField].ToString();
                if (string.IsNullOrEmpty(strId)) continue;
                string strText = dr[textField].ToString();
                if (!string.IsNullOrEmpty(parentName))
                    strText = parentName + "-" + dr[textField].ToString();
                treeItems.Add(strId, strText);
                GetComboTreeItems(ref treeItems, dataTable, strText, strId, idField, textField, parentId);
            }
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
            if (tableName.StartsWith("dbo."))
                tableName = tableName.Replace("dbo.", string.Empty);
            string sql = "select [" + string.Join("],[", columns.ToArray()) + "] from [dbo].[" + tableName + "] with(nolock) where [VALID_FLAG] = 1 " + query;
            return CommonDAL.ExecuteDataTableBySql(sql);
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
            return CommonDAL.GetDataTableByPage("dbo", tableName, textWhere, textOrder, pageIndex, pageRow, out dataCnt);
        }


        public static DataSet ExecuteDataSetBySql(string sql, int cmdTimeout = 60)
        {
            return CommonDAL.ExecuteDataSetBySql(sql, cmdTimeout);
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
