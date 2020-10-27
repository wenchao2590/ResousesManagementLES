using DAL.BAS;
using DM.BAS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.BAS
{
    public class CommonBLL
    {
        /// <summary>
        /// GetFid
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string GetFid(string tableName)
        {
            return CommonDAL.GetFid(tableName);
        }
        ///[CKSYK_F1] = N'JOBNO.TEST' ,[CKSYK_YWDM] = N'Y' 

        public static Dictionary<string, string> GetFieldValues(string updateFields)
        {
            Dictionary<string, string> retdictionary = new Dictionary<string, string>();
            string[] fieldvalues = updateFields.Split(new string[] { "[", "] = N'", "' ,[" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < fieldvalues.Length; i++)
            {
                string ikey = fieldvalues[i];
                string ivalue = fieldvalues[++i];
                if (ivalue.EndsWith("' "))
                    ivalue = ivalue.Substring(0, ivalue.Length - 2);
                if (ivalue.EndsWith(" "))
                    ivalue = ivalue.Substring(0, ivalue.Length - 1);
                retdictionary.Add(ikey, ivalue);
            }
            return retdictionary;
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

        public static string NewJob(string ywdm, string year, string month, bool createJobFlag)
        {
            return CommonDAL.NewJob(ywdm, year, month, createJobFlag);
        }
    }
}