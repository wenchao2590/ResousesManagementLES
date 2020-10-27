using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DAL.LES
{
    public partial class PlanPullOrderDAL
    {
        /// <summary>
        /// 根据委托编号获取业务编号
        /// </summary>
        /// <param name="custTrustNo"></param>
        /// <returns></returns>
        public string GetOrderCode(string custTrustNo)
        {
            string sql = "select [ORDER_CODE] from [LES].[TT_MPM_PLAN_PULL_ORDER] with(nolock) " +
                "where [CUST_TRUST_NO] = @CUST_TRUST_NO and [VALID_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@CUST_TRUST_NO", DbType.AnsiString, custTrustNo);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return string.Empty;
            return result.ToString();
        }
        /// <summary>
        /// 根据业务编号获取FID
        /// </summary>
        /// <param name="orderCode"></param>
        /// <returns></returns>
        public Guid GetFid(string orderCode)
        {
            string sql = "select [FID] from [LES].[TT_MPM_PLAN_PULL_ORDER] with(nolock) " +
                "where [ORDER_CODE] = @ORDER_CODE and [VALID_FLAG] = 1;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ORDER_CODE", DbType.AnsiString, orderCode);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value)
                return Guid.Empty;
            return Guid.Parse(result.ToString());
        }

    }
}
