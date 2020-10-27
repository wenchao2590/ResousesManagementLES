using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#region Imported Namespace
using DM.LES;
using Infrustructure.Data;
using Infrustructure.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

#endregion

namespace DAL.LES
{
    public partial class MesOutboundLogDAL
    {
        /// <summary>
        /// MSG_CONTENT留空
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <returns></returns>
        public List<MesOutboundLogInfo> GetListForPage(string textWhere, string textOrder, int pageIndex, int pageRow)
        {
            if (pageIndex <= 0) pageIndex = 1;
            if (pageRow <= 0) pageRow = 10;
            string whereText = string.Empty;
            if (!string.IsNullOrEmpty(textWhere))
            {
                if (textWhere.Trim().StartsWith("and", StringComparison.OrdinalIgnoreCase))
                    whereText += " where [VALID_FLAG] = 1 " + textWhere;
                else
                    whereText += " where " + textWhere + " and [VALID_FLAG] = 1";
            }
            else
                whereText += " where [VALID_FLAG] = 1 ";
            if (string.IsNullOrEmpty(textOrder))
                textOrder += "[ID] desc";
            string sql = "select top " + pageRow + " * from "
                + "(select row_number() over(order by " + textOrder + ") as rownumber"
                + ",ID, FID, TRANS_NO, SOURCE_SYSTEM, TARGET_SYSTEM, METHOD_CODE, KEY_VALUE, EXECUTE_START_TIME, EXECUTE_END_TIME, EXECUTE_RESULT, EXECUTE_TIMES, '' as MSG_CONTENT, ERROR_CODE, ERROR_MSG, VALID_FLAG, CREATE_USER, CREATE_DATE, MODIFY_USER, MODIFY_DATE " +
                "from [LES].[TI_IFM_MES_OUTBOUND_LOG] with(nolock) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            List<MesOutboundLogInfo> list = new List<MesOutboundLogInfo>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    list.Add(CreateMesOutboundLogInfo(dr));
                }
            }
            return list;
        }
        /// <summary>
        /// 获取等待发送的任务数据
        /// </summary>
        /// <returns></returns>
        public List<MesOutboundLogInfo> GetListForUnsend()
        {
            string sql = "select [ID],[FID],[METHOD_CODE] "
                + "from [LES].[TI_IFM_MES_OUTBOUND_LOG] with(nolock) "
                + "where [VALID_FLAG] = 1 "
                + "and [EXECUTE_RESULT] in (" + (int)ExecuteResultConstants.Submit + "," + (int)ExecuteResultConstants.Resend + ") "
                + "and [SOURCE_SYSTEM] = 'LES' "
                + "order by [ID];";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<MesOutboundLogInfo> list = new List<MesOutboundLogInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    MesOutboundLogInfo info = new MesOutboundLogInfo();
                    info.Id = DBConvert.GetInt64(dr, dr.GetOrdinal("ID"));
                    info.Fid = DBConvert.GetGuid(dr, dr.GetOrdinal("FID"));
                    info.MethodCode = DBConvert.GetString(dr, dr.GetOrdinal("METHOD_CODE"));
                    list.Add(info);
                }
            }
            return list;
        }
    }
}
