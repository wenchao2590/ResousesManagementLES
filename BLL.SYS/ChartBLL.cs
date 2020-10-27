using DAL.SYS;
using DM.SYS;
using System;
using System.Collections.Generic;

namespace BLL.SYS
{
    public class ChartBLL
    {
        #region Common
        ChartDAL dal = new ChartDAL();
        public List<ChartInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow
            , out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        public long InsertInfo(ChartInfo info)
        {
            info.ValidFlag = true;
            return dal.Add(info);
        }
        public bool UpdateInfo(string fields, long id)
        {
            return dal.UpdateInfo(fields, id) > 0 ? true : false;
        }
        public bool LogicDeleteInfo(long id, DateTime modifyDate, string modifyUser)
        {
            return dal.LogicDelete(id,  modifyUser) > 0 ? true : false;
        }
        public ChartInfo SelectInfo(long id)
        {
            return dal.GetInfo(id);
        }
        #endregion
        public string GetChartDataByName(string info)
        {
            string strChart = string.Empty;
            strChart = dal.GetChartDataByName(info);
            return strChart;
        }


        public string GetChartData(string filter)
        {
            string strChart = string.Empty;
            strChart = dal.GetChartData(filter);
            return strChart;

        }


        public string getDataBySql(string sql) {

            string strChart = string.Empty;
            strChart = dal.getDataBySql(sql);
            return strChart;
        }

    }
}
