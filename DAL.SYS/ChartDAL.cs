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
    public partial class ChartDAL
    {
        public string GetChartConfigBySql(string sql, string chartType)
        {
            return chartType;
        }
        public string GetChartDataByName(string chartName)
        {
            #region 配置表  如果CHART_COLUMN_NAME为空 ，那么就去数据表取列名（写在前台）
            Database db = DatabaseFactory.CreateDatabase();
            string chartType = string.Empty;
            string sqlType = string.Empty;
            string sql = "select NAME,NAME_EN,RECEIVER_LAYER,CHART_TYPE,MIX_CONDITION,CHART_LABEL_NAME,";
            sql += "CHART_STYLE,TIP_FORMAT,CHART_WIDTH,CHART_HIGHT,CHART_COLUMN_NAME,CHART_ROW_NAME,SQL_STRING From TS_SYS_CHART where name=N'" + chartName + "'";
            string strConfig = "chartConfigJson=[{";
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            DataTable dt = db.ExecuteDataTable(dbCommand);
            if (dt.Rows.Count > 0)
            {
                chartType = dt.Rows[0]["CHART_TYPE"].ToString();
                //SQL_STRING里数据如果是SQL开头那么就是从数据库去取值,否则就不做动作
                sql = dt.Rows[0]["SQL_STRING"].ToString();
                sqlType = sql.Substring(0, sql.IndexOf(":"));
                sql = sql.Substring(sql.IndexOf(":") + 1);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    strConfig = strConfig + "\"" + dt.Columns[i] + "\":";
                    strConfig = strConfig + "\"" + dt.Rows[0].ItemArray[i] + "\",";
                }
                strConfig = strConfig.ToString().Substring(0, strConfig.Length - 1);
                strConfig = strConfig + "}];";
            }
            #endregion 
            //sql = "select CHART_WIDTH,CHART_HIGHT,CHART_COLUMN_NAME,CHART_ROW_NAME,SQL_STRING From TS_SYS_CHART where ";
            #region 数据表  
            DbCommand dbComd = db.GetSqlStringCommand(sql);
            string strChart = "DataJson={\"xAxis\":\"";
            //如果不是混合图表
            if (sqlType.ToUpper() == "SQL" && (chartType == "column" || chartType == "spline"))
            {
                using (DataTable dt1 = db.ExecuteDataTable(dbComd))
                {

                    for (int i = 0; i < dt1.Columns.Count; i++)
                    {
                        string colname = dt1.Columns[i].ColumnName.ToString();
                        strChart = strChart + "'" + colname + "',";
                    }
                    strChart = strChart.ToString().Substring(0, strChart.Length - 1);
                    strChart = strChart + "\",\"dataItem\":[";
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        strChart = strChart + "{\"name\":\"" + chartName + "\",\"data\":\"";
                        for (int k = 0; k < dt1.Columns.Count; k++)
                        {
                            string colval = dt1.Rows[j].ItemArray[k].ToString();
                            strChart = strChart + colval + ",";
                        }
                        strChart = strChart.ToString().Substring(0, strChart.Length - 1);
                        strChart = strChart + "\",\"rowID\":\"1\",\"type\":\"" + chartType + "\"},";
                    }
                    strChart = strChart.ToString().Substring(0, strChart.Length - 1);
                    strChart = strChart + "]};setChart(chartConfigJson);";
                }
            }
            else  //如果是混合图表
            {

            }
            #endregion
            return strConfig + strChart;
        }

        //        {"total":28,"rows":[
        //	{"productid":"FI-SW-01","productname":"Koi","unitcost":10.00,"status":"P","listprice":36.50,"attr1":"Large","itemid":"EST-1"},
        //	{"productid":"K9-DL-01","productname":"Dalmation","unitcost":12.00,"status":"P","listprice":18.50,"attr1":"Spotted Adult Female","itemid":"EST-10"},
        //	{"productid":"RP-SN-01","productname":"Rattlesnake","unitcost":12.00,"status":"P","listprice":38.50,"attr1":"Venomless","itemid":"EST-11"},
        //	{"productid":"RP-SN-01","productname":"Rattlesnake","unitcost":12.00,"status":"P","listprice":26.50,"attr1":"Rattleless","itemid":"EST-12"},
        //	{"selected":true,"productid":"RP-LI-02","productname":"Iguana","unitcost":12.00,"status":"P","listprice":35.50,"attr1":"Green Adult","itemid":"EST-13"},
        //	{"productid":"FL-DSH-01","productname":"Manx","unitcost":12.00,"status":"P","listprice":158.50,"attr1":"Tailless","itemid":"EST-14"},
        //	{"productid":"FL-DSH-01","productname":"Manx","unitcost":12.00,"status":"P","listprice":83.50,"attr1":"With tail","itemid":"EST-15"},
        //	{"productid":"FL-DLH-02","productname":"Persian","unitcost":12.00,"status":"P","listprice":23.50,"attr1":"Adult Female","itemid":"EST-16"},
        //	{"productid":"FL-DLH-02","productname":"Persian","unitcost":12.00,"status":"P","listprice":89.50,"attr1":"Adult Male","itemid":"EST-17"},
        //	{"productid":"AV-CB-01","productname":"Amazon Parrot","unitcost":92.00,"status":"P","listprice":63.50,"attr1":"Adult Male","itemid":"EST-18"}
        //]}



        public string GetChartData(string filter)
        {
            string result = string.Empty;
            Database db = DatabaseFactory.CreateDatabase();
            string sql = "select * from TS_SYS_CHART where VALID_FLAG=1   " + filter;
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



        public string getDataBySql(string sql)
        {

            //var chartData = [
            //     { "时间": "1", "合格": "20", "不合格": "3" },
            //     { "时间": "2", "合格": "33", "不合格": "1" },
            //     { "时间": "3", "合格": "43", "不合格": "4" },
            //     { "时间": "4", "合格": "25", "不合格": "11" },
            //     { "时间": "5", "合格": "13", "不合格": "2" },
            //     { "时间": "6", "合格": "63", "不合格": "6" },
            //     { "时间": "7", "合格": "90", "不合格": "8" }
            //];

            string result = string.Empty;
            Database db = DatabaseFactory.CreateDatabase();
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
            result = "[" + result + "]";

            return result;
        }


    }
}
