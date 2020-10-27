namespace DAL.LES
{
    using System;
    using System.Collections.Generic;
    using DM.LES;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System.Data;
    using System.Data.Common;
    /// <summary>
    /// QmisOutboundLogDAL
    /// </summary>
    public partial class QmisOutboundLogDAL
    {
        /// <summary>
        /// GetListForPage
        /// </summary>
        /// <param name="textWhere"></param>
        /// <param name="textOrder"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageRow"></param>
        /// <returns></returns>
        public List<QmisOutboundLogInfo> GetListForPage(string textWhere, string textOrder, int pageIndex, int pageRow)
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
                "from [LES].[TI_IFM_QMIS_OUTBOUND_LOG] with(nolock) " + whereText + ") T "
                + "where rownumber > " + (pageIndex - 1) * pageRow + " ";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<QmisOutboundLogInfo> list = new List<QmisOutboundLogInfo>();
            using (IDataReader dr = db.ExecuteReader(dbCommand))
            {
                while (dr.Read())
                {
                    list.Add(CreateQmisOutboundLogInfo(dr));
                }
            }
            return list;
        }
    }
}
