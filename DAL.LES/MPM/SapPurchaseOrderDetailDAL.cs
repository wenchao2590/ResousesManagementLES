using DM.LES;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DAL.LES
{
    public partial class SapPurchaseOrderDetailDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sapRsnum"></param>
        /// <param name="partNo"></param>
        /// <returns></returns>
        public SapPurchaseOrderDetailInfo GetInfo(string orderCode, string partNo)
        {
            string sql = "select * from [LES].[TT_MPM_SAP_PURCHASE_ORDER_DETAIL] with(nolock) " +
                "where [VALID_FLAG] = 1 and [PART_NO] = @PART_NO and " +
                "[ORDER_FID] in (select [FID] from [LES].[TT_MPM_SAP_PURCHASE_ORDER] with(nolock) where [VALID_FLAG] = 1 and [ORDER_CODE] = @ORDER_CODE);";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PART_NO", DbType.AnsiString, partNo);
            db.AddInParameter(cmd, "@ORDER_CODE", DbType.AnsiString, orderCode);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                    return CreateSapPurchaseOrderDetailInfo(dr);
            }
            return null;
        }
    }
}
