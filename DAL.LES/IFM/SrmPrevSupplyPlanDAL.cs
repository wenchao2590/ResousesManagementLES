namespace DAL.LES
{
    using DM.LES;
    using Infrustructure.Utilities;
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    public partial class SrmPrevSupplyPlanDAL
    {
        /// <summary>
        /// GetListByDateColumn
        /// </summary>
        /// <param name="dateColumn"></param>
        /// <returns></returns>
        public List<SrmPrevSupplyPlanInfo> GetListByDateColumn(string dateColumn, string conditions = "")
        {
            ///未能匹配到供应商信息的数据在此不生成SRM供货计划
            string sql = "select [FID],[PART_NO],[SUPPLIER_NUM],[PLANT],convert(datetime,'" + dateColumn + "') as [DELIVERY_DATE],isnull([" + dateColumn + "] , 0) as [REQUIRE_QTY],[PART_PURCHASER] "
                + "from [LES].[TT_ATP_SUPPLY_PLAN] with(nolock) "
                + "where [VALID_FLAG] = 1 "
                + "and [SUPPLIER_NUM] in (select [SUPPLIER_NUM] from LES.[TM_BAS_SUPPLIER] with(nolock) where [VALID_FLAG] = 1 and [SUPPLIER_TYPE] = " + (int)SupplierTypeConstants.MaterialSupplier + ") "
                + "and isnull([" + dateColumn + "],0) <> 0 " + conditions + ";";///将为零的数据过滤掉，否则数量巨大
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            List<SrmPrevSupplyPlanInfo> list = new List<SrmPrevSupplyPlanInfo>();
            using (IDataReader rdr = db.ExecuteReader(dbCommand))
            {
                while (rdr.Read())
                {
                    SrmPrevSupplyPlanInfo info = new SrmPrevSupplyPlanInfo();
                    info.Fid = DBConvert.GetGuidNullable(rdr, rdr.GetOrdinal("FID"));
                    info.PartNo = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_NO"));
                    info.SupplierNum = DBConvert.GetString(rdr, rdr.GetOrdinal("SUPPLIER_NUM"));
                    info.Plant = DBConvert.GetString(rdr, rdr.GetOrdinal("PLANT"));
                    info.DeliveryDate = DBConvert.GetDateTimeNullable(rdr, rdr.GetOrdinal("DELIVERY_DATE"));
                    info.RequireQty = DBConvert.GetDecimalNullable(rdr, rdr.GetOrdinal("REQUIRE_QTY"));
                    info.PartPurchaser = DBConvert.GetString(rdr, rdr.GetOrdinal("PART_PURCHASER"));
                    list.Add(info);
                }
            }
            return list;
        }
    }
}
