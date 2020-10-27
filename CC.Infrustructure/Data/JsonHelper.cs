namespace Infrustructure.Data
{
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Web.Script.Serialization;
    using System.Collections;
    using System.Text.RegularExpressions;
    /// <summary>
    /// JsonHelper
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetGridJson(DataTable dt)
        {
            return @"{""rows"":" + DataTableToJson(dt) + @",""total"":""" + dt.Rows.Count + @"""}";
        }
        /// <summary>
        /// DataTable To Json
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToJson(DataTable dt)
        {
            if (dt == null)
                return "[]";
            StringBuilder builder = new StringBuilder();
            builder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                builder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    builder.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + ToJson(dt.Rows[i][j]));
                    if (j < (dt.Columns.Count - 1))
                    {
                        builder.Append(",");
                    }
                }
                builder.Append("}");
                if (i < (dt.Rows.Count - 1))
                {
                    builder.Append(",");
                }
            }
            builder.Append("]");
            return builder.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="id"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public static object ArrayToTreeData(IList<Hashtable> list, string id, string pid)
        {
            var h = new Hashtable();
            var targetData = new List<Hashtable>();
            foreach (var item in list)
            {
                if (!item.ContainsKey(id)) continue;
                h[item[id].ToString()] = item;
            }
            foreach (var item in list)
            {
                if (!item.ContainsKey(id)) continue;
                if (!item.ContainsKey(pid) || item[pid] == null || !h.ContainsKey(item[pid].ToString()))
                {
                    targetData.Add(item);
                }
                else
                {
                    var pItem = h[item[pid].ToString()] as Hashtable;
                    if (!pItem.ContainsKey("children"))
                        pItem["children"] = new List<Hashtable>();
                    var children = pItem["children"] as List<Hashtable>;
                    children.Add(pItem);
                }
            }

            return ToJson(targetData);//Newtonsoft.Json.JsonConvert.SerializeObject();
        }
        /// <summary>
        /// JSON转换为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T FormJson<T>(string strJson) where T : class
        {
            return new JavaScriptSerializer().Deserialize<T>(strJson);
        }
        /// <summary>
        /// 类对像转换成json格式
        /// </summary> 
        /// <returns></returns>
        public static string ToJson(object t)
        {
            if (t == null) return string.Empty;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = int.MaxValue;
            return serializer.Serialize(t);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static DataTable JsonToDataTable(string strJson)
        {
            //转换json格式
            strJson = strJson.Replace(",\"", "*\"").Replace("\":", "\"#").ToString();
            //取出表名   
            var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
            string strName = rg.Match(strJson).Value;
            DataTable tb = null;
            //去除表名   
            strJson = strJson.Substring(strJson.IndexOf("[") + 1);
            strJson = strJson.Substring(0, strJson.IndexOf("]"));

            //获取数据   
            rg = new Regex(@"(?<={)[^}]+(?=})");
            MatchCollection mc = rg.Matches(strJson);
            for (int i = 0; i < mc.Count; i++)
            {
                string strRow = mc[i].Value;
                string[] strRows = strRow.Split('*');

                //创建表   
                if (tb == null)
                {
                    tb = new DataTable();
                    tb.TableName = strName;
                    foreach (string str in strRows)
                    {
                        var dc = new DataColumn();
                        string[] strCell = str.Split('#');

                        if (strCell[0].Substring(0, 1) == "\"")
                        {
                            int a = strCell[0].Length;
                            dc.ColumnName = strCell[0].Substring(1, a - 2);
                        }
                        else
                        {
                            dc.ColumnName = strCell[0];
                        }
                        tb.Columns.Add(dc);
                    }
                    tb.AcceptChanges();
                }

                //增加内容   
                DataRow dr = tb.NewRow();
                for (int r = 0; r < strRows.Length; r++)
                {
                    dr[r] = strRows[r].Split('#')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
                }
                tb.Rows.Add(dr);
                tb.AcceptChanges();
            }
            return tb;
        }

    }
}
