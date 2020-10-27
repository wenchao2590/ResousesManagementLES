namespace DAL.LES
{
    using DM.LES;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System.Data;
    using System.Data.Common;
    /// <summary>
    /// TT_ATP_PORDER_LACK_MATERIAL
    /// </summary>
    public partial class PorderLackMaterialDAL
    {
        /// <summary>
        /// GetInfo
        /// </summary>
        /// <param name="productOrderNo"></param>
        /// <returns></returns>
        public PorderLackMaterialInfo GetInfo(string productOrderNo)
        {
            string sql = "select * from [LES].[TT_ATP_PORDER_LACK_MATERIAL] with(nolock) " +
                "where [VALID_FLAG] = 1 and [PRODUCTION_ORDER_NO] = @PRODUCTION_ORDER_NO;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PRODUCTION_ORDER_NO", DbType.AnsiString, productOrderNo);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                    return CreatePorderLackMaterialInfo(dr);
            }
            return null;
        }
    }
}
