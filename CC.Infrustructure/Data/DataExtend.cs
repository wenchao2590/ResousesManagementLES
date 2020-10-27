using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrustructure.Data
{
    public class DataExtend
    {
        public static string GetValueByFields(string fields, string fieldName)
        {
            string[] fieldlist = fields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            string fieldValue = string.Empty;
            foreach (var fieldinfo in fieldlist)
            {
                if (!fieldinfo.Contains(fieldName)) continue;
                fieldValue = fieldinfo.Replace(fieldName, string.Empty).Replace("=", string.Empty).Replace("'", string.Empty);
            }
            return fieldValue.Trim();
        }

        public static string GetReplaceValueByFields(string fields, string fieldName, object fieldValue)
        {
            string[] fieldlist = fields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var fieldinfo in fieldlist)
            {
                if (!fieldinfo.Contains(fieldName)) continue;
                fields = fields.Replace(fieldinfo, fieldName + "=" + fieldValue);
            }
            return fields;
        }
    }
}
