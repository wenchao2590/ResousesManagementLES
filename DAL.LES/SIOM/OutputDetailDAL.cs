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
    public partial class OutputDetailDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textWhere"></param>
        /// <returns></returns>
        public int GetOutputDetailPageCounts(string textWhere)
        {
            string sql = "select count(1) from [LES].[TT_WMM_OUTPUT_DETAIL] with(nolock) "
                + "left join [LES].[TT_WMM_OUTPUT] with(nolock) on [LES].[TT_WMM_OUTPUT].[FID] = [LES].[TT_WMM_OUTPUT_DETAIL].[OUTPUT_FID] and [LES].[TT_WMM_OUTPUT].[VALID_FLAG] = 1 "
                + "where [LES].[TT_WMM_OUTPUT_DETAIL].[VALID_FLAG] = 1 {0};";
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
        public List<OutputDetailInfo> GetOutputDetailPageInfosByPage(string textWhere, string textOrder, int pageIndex, int pageRow)
        {
            if (pageIndex <= 0) pageIndex = 1;
            if (pageRow <= 0) pageRow = 10;
            string whereText = string.Empty;
            if (textWhere.Trim().StartsWith("and", StringComparison.OrdinalIgnoreCase))
                whereText += " where [LES].[TT_WMM_OUTPUT_DETAIL].[VALID_FLAG] = 1 " + textWhere;
            else
                whereText += " where " + textWhere + " and [LES].[TT_WMM_OUTPUT_DETAIL].[VALID_FLAG] = 1";
            if (string.IsNullOrEmpty(textOrder))
                textOrder += "[LES].[TT_WMM_OUTPUT_DETAIL].[ID] desc";
            string sql = "select top " + pageRow + " * from "
                + "(select row_number() over(order by " + textOrder + ") as rownumber"
                + ",[LES].[TT_WMM_OUTPUT_DETAIL].*"
                + ",[LES].[TT_WMM_OUTPUT].[TRAN_TIME]"
                + ",[LES].[TT_WMM_OUTPUT].[OUTPUT_NO]"
                + ",[LES].[TT_WMM_OUTPUT].[COST_CENTER]"
                + "from [LES].[TT_WMM_OUTPUT_DETAIL] with(nolock) "
                + "left join [LES].[TT_WMM_OUTPUT] with(nolock) on [LES].[TT_WMM_OUTPUT].[FID] = [LES].[TT_WMM_OUTPUT_DETAIL].[OUTPUT_FID] and [LES].[TT_WMM_OUTPUT].[VALID_FLAG] = 1 "
                + "" + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            List<OutputDetailInfo> list = new List<OutputDetailInfo>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    OutputDetailInfo info = CreateOutputDetailInfo(dr);
                    info.TranTime = DBConvert.GetDateTime(dr, dr.GetOrdinal("TRAN_TIME"));
                    info.TranNo = DBConvert.GetString(dr, dr.GetOrdinal("OUTPUT_NO"));
                    info.CostCenter = DBConvert.GetString(dr, dr.GetOrdinal("COST_CENTER"));
                    list.Add(info);
                }
            }
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public OutputDetailInfo GetInfo(Guid fid)
        {
            string sql = "select * from [LES].[TT_WMM_OUTPUT_DETAIL] with(nolock) where [FID] = @FID and [VALID_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@FID", DbType.Guid, fid);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                    return CreateOutputDetailInfo(dr);
            }
            return null;
        }
    }
}
