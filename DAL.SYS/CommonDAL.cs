using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DAL.SYS
{
    public class CommonDAL
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdTimeout"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSetBySql(string sql, int cmdTimeout = 60)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            cmd.CommandTimeout = cmdTimeout;
            return db.ExecuteDataSet(cmd);
        }


        /// <summary>
        /// ExecuteDataTableBySql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdTimeout"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTableBySql(string sql, int cmdTimeout = 60)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            cmd.CommandTimeout = cmdTimeout;
            return db.ExecuteDataTable(cmd);
        }

        /// <summary>
        /// 返回受影响的行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdTimeout"></param>
        /// <returns></returns>
        public static bool ExecuteNonQueryBySql(string sql, int cmdTimeout = 60)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            cmd.CommandTimeout = cmdTimeout;
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }
        /// <summary>
        /// 返回第一行第一列的值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdTimeout"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, int cmdTimeout = 60)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            cmd.CommandTimeout = cmdTimeout;
            return db.ExecuteScalar(cmd);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        private static T CreateItemByDataRow<T>(DataRow row)
        {
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();
                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                    if (prop == null) continue;
                    string valueString = row[column.ColumnName].ToString();
                    switch (prop.PropertyType.Name.ToLower())
                    {
                        case "nullable`1":
                            if (string.IsNullOrEmpty(valueString))
                            {
                                prop.SetValue(obj, null, null); break;
                            }
                            if (prop.PropertyType.FullName.ToLower().Contains("system.int32"))
                            {
                                int valueInt = 0;
                                int.TryParse(valueString, out valueInt);
                                prop.SetValue(obj, valueInt, null); break;
                            }
                            if (prop.PropertyType.FullName.ToLower().Contains("system.int64"))
                            {
                                long valueLong = 0;
                                long.TryParse(valueString, out valueLong);
                                prop.SetValue(obj, valueLong, null); break;
                            }
                            if (prop.PropertyType.FullName.ToLower().Contains("system.boolean"))
                            {
                                bool valueBoolean = (valueString.ToLower() == "yes" || valueString.ToLower() == "是") ? true : false;
                                prop.SetValue(obj, valueBoolean, null); break;
                            }
                            if (prop.PropertyType.FullName.ToLower().Contains("system.decimal"))
                            {
                                decimal valueDecimal = 0;
                                decimal.TryParse(valueString, out valueDecimal);
                                prop.SetValue(obj, valueDecimal, null); break;
                            }
                            if (prop.PropertyType.FullName.ToLower().Contains("system.guid"))
                            {
                                Guid valueGuid = Guid.Empty;
                                Guid.TryParse(valueString, out valueGuid);
                                prop.SetValue(obj, valueGuid, null); break;
                            }
                            if (prop.PropertyType.FullName.ToLower().Contains("system.datetime"))
                            {
                                DateTime valueDatetime = DateTime.Parse("1900-01-01");
                                DateTime.TryParse(valueString, out valueDatetime);
                                prop.SetValue(obj, valueDatetime, null); break;
                            }
                            break;
                        case "int32":
                            int valInt = 0;
                            int.TryParse(valueString, out valInt);
                            prop.SetValue(obj, valInt, null); break;
                        case "int64":
                            int valLong = 0;
                            int.TryParse(valueString, out valLong);
                            prop.SetValue(obj, valLong, null); break;
                        case "boolean":
                        case "bool":
                            bool valBoolean = (valueString.ToLower() == "yes" || valueString.ToLower() == "是") ? true : false;
                            prop.SetValue(obj, valBoolean, null); break;
                        case "decimal":
                            decimal valDecimal = 0;
                            decimal.TryParse(valueString, out valDecimal);
                            prop.SetValue(obj, valDecimal, null); break;
                        case "guid":
                            Guid valGuid = Guid.Empty;
                            Guid.TryParse(valueString, out valGuid);
                            prop.SetValue(obj, valGuid, null); break;
                        case "datetime":
                            DateTime valDatetime = DateTime.Parse("1900-01-01");
                            DateTime.TryParse(valueString, out valDatetime);
                            prop.SetValue(obj, valDatetime, null); break;
                        default:
                            prop.SetValue(obj, valueString, null); break;
                    }
                }
            }
            return obj;
        }
        /// <summary>
        /// 获取对象中的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info"></param>
        /// <param name="propName"></param>
        /// <returns></returns>
        public static string GetFieldValueForSql<T>(T obj, string propName)
        {
            PropertyInfo prop = obj.GetType().GetProperty(propName);
            if (prop == null) return string.Empty;
            object valueObj = prop.GetValue(obj, null);
            if (valueObj == null)
            {
                if (prop.PropertyType.Name.ToLower() == "nullable`1")
                    return "NULL";
                return "N''";
            }
            string valueString = valueObj.ToString();
            switch (prop.PropertyType.Name.ToLower())
            {
                case "nullable`1":
                    string pFullName = prop.PropertyType.FullName.ToLower();
                    if (pFullName.Contains("system.int32") || pFullName.Contains("system.int64") || pFullName.Contains("system.decimal"))
                        return valueString;
                    if (pFullName.Contains("system.boolean"))
                    {
                        bool valueBoolean = (valueString.ToLower() == "yes" || valueString.ToLower() == "是") ? true : false;
                        return valueBoolean ? "1" : "0";
                    }
                    if (pFullName.Contains("system.guid") || pFullName.Contains("system.datetime"))
                        return "N'" + valueString.Replace("'", "''") + "'";
                    break;
                case "int32":
                case "int64":
                case "decimal": return valueString;
                case "boolean":
                case "bool":
                    bool valBoolean = (valueString.ToLower() == "yes" || valueString.ToLower() == "是") ? true : false;
                    return valBoolean ? "1" : "0";
                case "guid":
                case "datetime":
                    return "N'" + valueString.Replace("'", "''") + "'";
                default:
                    return "N'" + valueString.Replace("'", "''") + "'";
            }
            return string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rows"></param>
        /// <returns></returns>
        private static IList<T> DataRowsConvertToList<T>(IList<DataRow> rows)
        {
            if (rows == null) return null;
            IList<T> list = new List<T>();
            foreach (DataRow row in rows)
            {
                T item = CreateItemByDataRow<T>(row);
                list.Add(item);
            }
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IList<T> DatatableConvertToList<T>(DataTable table)
        {
            if (table == null) return null;
            List<DataRow> rows = new List<DataRow>();
            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }
            return DataRowsConvertToList<T>(rows);
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
        public static DataTable GetDataTableByPage(string schema, string tableName, string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCnt)
        {
            if (pageIndex <= 0) pageIndex = 1;
            if (pageRow <= 0) pageRow = 10;
            string whereText = string.Empty;
            if (!string.IsNullOrEmpty(textWhere))
            {
                if (textWhere.Trim().StartsWith("and", StringComparison.OrdinalIgnoreCase))
                    whereText += " where 1 = 1 " + textWhere;
                else
                    whereText += " where " + textWhere + " and 1 = 1";
            }
            else
                whereText += " where 1 = 1 ";
            if (string.IsNullOrEmpty(textOrder))
                textOrder += "[ID] desc";
            string sql = "select top " + pageRow + " * from "
                + "(select row_number() over(order by " + textOrder + ") as rownumber"
                + ",* from [" + schema + "].[" + tableName + "]  with(nolock) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + ";";
            sql += "select count(*) from [" + schema + "].[" + tableName + "]  with(nolock) " + whereText + ";";
            ///
            DataSet dataSet = ExecuteDataSetBySql(sql);
            dataCnt = Convert.ToInt32(dataSet.Tables[1].Rows[0][0]);
            return dataSet.Tables[0];
        }
    }
}
