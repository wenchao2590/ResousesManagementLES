using DAL.LES;
using DAL.SYS;
using DM.LES;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BLL.LES
{
    /// <summary>
    /// OutputDetailPageBLL
    /// </summary>
    public partial class OutputDetailPageBLL
    {
        /// <summary>
        /// OutputDetailDAL
        /// </summary>
        OutputDetailDAL dal = new OutputDetailDAL();
        /// <summary>
        /// GetListByPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <param name="dataCount"></param>
        /// <returns></returns>
        public List<OutputDetailInfo> GetListByPage(string textWhere, string textOrder, int pageIndex, int pageRow, out int dataCount)
        {
            ///至关注已完成or已核销的单据
            textWhere += "and [LES].[TT_WMM_OUTPUT].[STATUS] in (" + (int)WmmOrderStatusConstants.Completed + "," + (int)WmmOrderStatusConstants.Closed + ") ";
            dataCount = dal.GetOutputDetailPageCounts(textWhere);
            return dal.GetOutputDetailPageInfosByPage(textWhere, textOrder, pageIndex, pageRow);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="whereText"></param>
        /// <param name="orderText"></param>
        /// <returns></returns>
        public DataTable GetDatatableForExcel(List<string> columns, string whereText, string orderText)
        {
            if (columns.Count == 0) return null;
            ///至关注已完成or已核销的单据
            whereText += "and [LES].[TT_WMM_OUTPUT].[STATUS] in (" + (int)WmmOrderStatusConstants.Completed + "," + (int)WmmOrderStatusConstants.Closed + ") ";
            if (!string.IsNullOrEmpty(orderText))
                whereText += "order by " + orderText;
            string sql = "select  " + string.Join(",", columns.ToArray()) + " "
                + "from [LES].[TT_WMM_OUTPUT_DETAIL] with(nolock) "
                + "left join [LES].[TT_WMM_OUTPUT] with(nolock) on [LES].[TT_WMM_OUTPUT].[FID] = [LES].[TT_WMM_OUTPUT_DETAIL].[OUTPUT_FID] and [LES].[TT_WMM_OUTPUT].[VALID_FLAG] = 1 "
                + "where [LES].[TT_WMM_OUTPUT_DETAIL].[VALID_FLAG] = 1 " + whereText;
            return CommonDAL.ExecuteDataTableBySql(sql);
        }
    }
}
