using DM.LES;
using Infrustructure.Data;
using Infrustructure.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace DAL.LES
{
    public partial class ReceiveAndOutputDAL
    {
        /// <summary>
        /// 根据单号获取出\入库明细列表
        /// </summary>
        /// <param name="orderNo">单号</param>
        /// <returns>返回DataTable明细列表</returns>
        public DataTable GetReceiveAndOutOrder(string orderNo)
        {
            string sql = "SELECT * FROM [LES].[V_WMM_RECEIVE_AND_OUTPUT] WHERE ORDER_NO = N'{0}';";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(string.Format(sql, orderNo));
            return db.ExecuteDataTable(cmd);
        }
    }
}
