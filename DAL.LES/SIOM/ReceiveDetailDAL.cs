using DM.LES;
using Infrustructure.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DAL.LES
{
    public partial class ReceiveDetailDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <returns></returns>
        public int GetReceiveDetailPageCounts(string textWhere)
        {
            string sql = "select count(1) from [LES].[TT_WMM_RECEIVE_DETAIL] with(nolock) "
                + "left join [LES].[TT_WMM_RECEIVE] with(nolock) on [LES].[TT_WMM_RECEIVE].[FID] = [LES].[TT_WMM_RECEIVE_DETAIL].[RECEIVE_FID] and [LES].[TT_WMM_RECEIVE].[VALID_FLAG] = 1 "
                + "where [LES].[TT_WMM_RECEIVE_DETAIL].[VALID_FLAG] = 1 {0};";
            if (string.IsNullOrEmpty(textWhere))
                textWhere = string.Empty;
            else
            {
                if (!textWhere.Trim().StartsWith("and", StringComparison.OrdinalIgnoreCase))
                    textWhere = " and " + textWhere;
            }
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(string.Format(sql, textWhere));
            return Convert.ToInt32(db.ExecuteScalar(cmd));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <returns></returns>
        public List<ReceiveDetailInfo> GetReceiveDetailPageInfosByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
        {
            if (pageIndex <= 0) pageIndex = 1;
            if (pageRow <= 0) pageRow = 10;
            string whereText = string.Empty;
            if (!string.IsNullOrEmpty(textWhere))
            {
                if (textWhere.Trim().StartsWith("and", StringComparison.OrdinalIgnoreCase))
                    whereText += " where [LES].[TT_WMM_RECEIVE_DETAIL].[VALID_FLAG] = 1 " + textWhere;
                else
                    whereText += " where " + textWhere + " and [LES].[TT_WMM_RECEIVE_DETAIL].[VALID_FLAG] = 1";
            }
            else
                whereText += " where [LES].[TT_WMM_RECEIVE_DETAIL].[VALID_FLAG] = 1 ";
            if (string.IsNullOrEmpty(textOrder))
                textOrder += "[LES].[TT_WMM_RECEIVE_DETAIL].[ID] desc";
            string sql = "select top " + pageRow + " * from "
                + "(select row_number() over(order by " + textOrder + ") as rownumber"
                + ",[LES].[TT_WMM_RECEIVE_DETAIL].*"
                + ",[LES].[TT_WMM_RECEIVE].[TRAN_TIME]"
                + ",[LES].[TT_WMM_RECEIVE].[RECEIVE_NO]"
                + ",[LES].[TT_WMM_RECEIVE].[COST_CENTER]"
                + ",[LES].[TT_WMM_RECEIVE].[BOOK_KEEPER] "
                + "from [LES].[TT_WMM_RECEIVE_DETAIL] with(nolock) "
                + "left join [LES].[TT_WMM_RECEIVE] with(nolock) on [LES].[TT_WMM_RECEIVE].[FID] = [LES].[TT_WMM_RECEIVE_DETAIL].[RECEIVE_FID] and [LES].[TT_WMM_RECEIVE].[VALID_FLAG] = 1 "
                + "" + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            List<ReceiveDetailInfo> list = new List<ReceiveDetailInfo>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    ReceiveDetailInfo info = CreateReceiveDetailInfo(dr);
                    info.TranTime = DBConvert.GetDateTime(dr, dr.GetOrdinal("TRAN_TIME"));
                    info.TranNo = DBConvert.GetString(dr, dr.GetOrdinal("RECEIVE_NO"));
                    info.CostCenter = DBConvert.GetString(dr, dr.GetOrdinal("COST_CENTER"));
                    info.BookKeeper = DBConvert.GetString(dr, dr.GetOrdinal("BOOK_KEEPER"));
                    list.Add(info);
                }
            }
            return list;
        }
    }
}
