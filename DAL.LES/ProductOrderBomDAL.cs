#region Imported Namespace

using DM.LES;
using Infrustructure.Data;
using Infrustructure.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

#endregion

namespace DAL.LES
{
    //// <summary>
    /// ProductOrderBomDAL对应表[TI_SAP_PRODUCT_ORDER_BOM]
    /// </summary>
    public partial class SapProductOrderBomDAL
    {
        /// <summary>
        /// 根据生产订单号获取最新订单物料清单
        /// </summary>
        /// <param name="aufnr"></param>
        /// <returns></returns>
        public SapProductOrderBomInfo GetInfoByAufnr(string aufnr)
        {
            string sql = "select top 1 * from [LES].[TI_IFM_SAP_PRODUCT_ORDER_BOM] with(nolock) "
                + "where [VALID_FLAG] = 1 and [AUFNR] = @AUFNR "
                + "order by [ID] desc";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@AUFNR", DbType.AnsiString, aufnr);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                    return CreateSapProductOrderBomInfo(dr);
            }
            return null;
        }
    }
}
