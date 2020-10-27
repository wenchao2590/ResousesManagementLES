using DAL.LES;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    public class DiffanalyseReportsBLL
    {
        #region Common
        DiffanalyseReportsDAL dal = new DiffanalyseReportsDAL();
        public List<DiffanalyseReportsInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            dataCount = dal.GetCounts(textWhere);
            return dal.GetListByPage(textWhere, textOrder, pageIndex, pageRow);
        }

        public DiffanalyseReportsInfo SelectInfo(int reportId)
        {
            return dal.GetInfo(reportId);
        }

        public int InsertInfo(DiffanalyseReportsInfo info)
        {
            return dal.Add(info);
        }

        public bool DeleteInfo(int reportId)
        {
            return dal.Delete(reportId) > 0 ? true : false;
        }

        public bool UpdateInfo(string fields, int reportId)
        {
            return dal.UpdateInfo(fields, reportId) > 0 ? true : false;
        }

        public int GetCounts(string textWhere)
        {
            return dal.GetCounts(textWhere);
        }

        #endregion
    }
}

