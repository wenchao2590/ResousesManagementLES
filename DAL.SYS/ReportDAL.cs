using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace DAL.SYS
{
    public partial class ReportDAL
    {


        public string GetReportData(string filter)
        {
            string result = string.Empty;
            Database db = DatabaseFactory.CreateDatabase();
            string sql = "select * from TS_SYS_REPORT where VALID_FLAG=1   " + filter;
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            DataTable dt = db.ExecuteDataTable(dbCommand);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                result += "{";
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    string key = dt.Columns[k].ToString();
                    result += "\"" + key + "\"" + ":" + "\"" + dt.Rows[i][key].ToString() + "\",";
                }
                result = result.Substring(0, result.Length - 1);
                result += "},";
            }
            result = result.Length > 0 ? result.Substring(0, result.Length - 1) : result;
            result = " {'total':" + dt.Rows.Count + ",'rows':[" + result + "]}";

            return result;
        }

        public string GetTableData(string sql, string isCol)
        {
            string col = string.Empty;
            // { field: 'code', title: 'Code', width: 100 }
            string result = string.Empty;
            Database db = DatabaseFactory.CreateDatabase();
            if (isCol == "update")
            {

                DbCommand cmd = db.GetSqlStringCommand(sql);
                result = db.ExecuteNonQuery(cmd) > 0 ? "操作成功" : "操作失败";
                return result;
            }
            else
            {
                DbCommand dbCommand = db.GetSqlStringCommand(sql);
                DataTable dt = db.ExecuteDataTable(dbCommand);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    result += "{";
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {

                        string key = dt.Columns[k].ToString();
                        result += "\"" + key + "\"" + ":" + "\"" + dt.Rows[i][key].ToString() + "\",";
                        if (i == 0)
                        {
                            col += "{field:'" + key + "',title:'" + key + "',width:150},";
                        }
                    }
                    result = result.Substring(0, result.Length - 1);
                    result += "},";
                }
                col = col.Length > 0 ? col.Substring(0, col.Length - 1) : col;
                col = "[" + col + "]";
                result = result.Length > 0 ? result.Substring(0, result.Length - 1) : result;
                if (isCol == "false")
                {
                    result = " {'total':" + dt.Rows.Count + ",'rows':[" + result + "]}";
                }
                else
                {
                    result = " {'total':" + dt.Rows.Count + ",'rows':[" + result + "]}" + "⊙" + col;
                }
                return result;
            }
        }
    }
}
