namespace DAL.LES
{
    using DM.LES;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    /// <summary>
    /// VmiInboundLogDAL
    /// </summary>
    public partial class VmiInboundLogDAL
    {
        /// <summary>
        /// MSG_CONTENT留空
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <returns></returns>
        public List<VmiInboundLogInfo> GetListForPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                + ",ID, FID, TRANS_NO, SOURCE_SYSTEM, TARGET_SYSTEM, METHOD_CODE, KEY_VALUE, EXECUTE_START_TIME, EXECUTE_END_TIME, EXECUTE_RESULT, EXECUTE_TIMES, '' as MSG_CONTENT, ERROR_CODE, ERROR_MSG, VALID_FLAG, CREATE_USER, CREATE_DATE, MODIFY_USER, MODIFY_DATE from [LES].[TI_IFM_VMI_INBOUND_LOG]  with(nolock) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            List<VmiInboundLogInfo> list = new List<VmiInboundLogInfo>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    list.Add(CreateVmiInboundLogInfo(dr));
                }
            }
            return list;
        }
    }
}
