using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Infrustructure.Access
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// Data Table to List Entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable table)
            where T : new()
        {
            List<T> result = new List<T>();
            var propertySet = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
            // .Where(o => Attribute.IsDefined(o, typeof(DataMemberAttribute))).ToList();

            if (table == null || table.Rows.Count == 0)
            {
                return result;
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                T o = Activator.CreateInstance<T>();
                for (int j = 0; j < propertySet.Count; j++)
                {
                    propertySet[j].SetValue(o, table.Rows[i].Field<object>(propertySet[j].Name), null);
                }
                result.Add(o);
            }

            return result;
        }

        /// <summary>
        /// Compare 2 Data Table 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool Compare2DataTable(this DataTable source, DataTable target)
        {
            if (source.Rows.Count != target.Rows.Count)
            {
                return false;
            }

            if (source.Rows.Count > 0)
            {
                if (source.Rows[0].ItemArray.Length != target.Rows[0].ItemArray.Length)
                {
                    return false;
                }
            }

            for (int i = 0; i < source.Rows.Count; i++)
            {
                for (int j = 0; j < source.Rows[i].ItemArray.Length; j++)
                {
                    if (!source.Rows[i][j].ToString().Equals(target.Rows[i][j].ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
