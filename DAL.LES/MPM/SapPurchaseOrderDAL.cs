namespace DAL.LES
{
    using DM.LES;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Text;
    public partial class SapPurchaseOrderDAL
    {
        public SapPurchaseOrderInfo GetInfo(string orderNo)
        {
            string sql = "select * from [LES].[TT_MPM_SAP_PURCHASE_ORDER] with(nolock) where [VALID_FLAG] = 1 and [ORDER_CODE] = @ORDER_CODE;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ORDER_CODE", DbType.AnsiString, orderNo);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                    return CreateSapPurchaseOrderInfo(dr);
            }
            return null;
        }
    }
}
