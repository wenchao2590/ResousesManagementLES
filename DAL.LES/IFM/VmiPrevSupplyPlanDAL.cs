namespace DAL.LES
{
    using DM.LES;
    using Infrustructure.Utilities;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    //// <summary>
    /// PrevSupplyPlanDAL对应表[TE_ATP_SRM_PREV_SUPPLY_PLAN]
    /// </summary>
    public partial class VmiPrevSupplyPlanDAL
    {
        /// <summary>
        /// Get data collection
        /// </summary>
        /// <param name="sql">SQL Statement</param>
        /// <returns>PrevSupplyPlanInfo Collection </returns>
        public List<VmiPrevSupplyPlanInfo> GetListBySql(string dateColumn)
        {
            string sql = "select [FID],[PART_NO],[SUPPLIER_NUM],[PLANT],convert(datetime,'" + dateColumn + "') as [DELIVERY_DATE],isnull([" + dateColumn + "],0) as [REQUIRE_QTY] "
                + "from [LES].[TT_ATP_SUPPLY_PLAN] with(nolock) "
                + "where [VALID_FLAG] = 1 "
                + "and isnull([" + dateColumn + "],0) <> 0;";///将为零的数据过滤掉，否则数量巨大
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<VmiPrevSupplyPlanInfo> list = new List<VmiPrevSupplyPlanInfo>();
            using (IDataReader rdr = db.ExecuteReader(dbCommand))
            {
                while (rdr.Read())
                {
                    VmiPrevSupplyPlanInfo info = new VmiPrevSupplyPlanInfo();
                    info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));
                    info.PartNo = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_NO"));
                    info.SupplierNum = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NUM"));
                    info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));
                    info.DeliveryDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("DELIVERY_DATE"));
                    info.RequireQty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("REQUIRE_QTY"));
                    list.Add(info);
                }
            }
            return list;
        }
    }
}
