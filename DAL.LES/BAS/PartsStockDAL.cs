using DM.LES;
using Infrustructure.Utilities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DAL.LES
{
    public partial class PartsStockDAL
    {
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="partNo"></param>
        /// <param name="wmNo"></param>
        /// <param name="zoneNo"></param>
        /// <returns></returns>
        public PartsStockInfo GetInfo(string partNo, string wmNo, string zoneNo)
        {
            string sql = "select * from [LES].[TM_BAS_PARTS_STOCK] with(nolock) "
                + "where [VALID_FLAG] = 1 and [PART_NO] = @PART_NO and [WM_NO] = @WM_NO and [ZONE_NO] = @ZONE_NO;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PART_NO", DbType.AnsiString, partNo);
            db.AddInParameter(cmd, "@WM_NO", DbType.AnsiString, wmNo);
            db.AddInParameter(cmd, "@ZONE_NO", DbType.AnsiString, zoneNo);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                    return CreatePartsStockInfo(dr);
            }
            return null;
        }
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="partNo"></param>
        /// <param name="SupplierNum"></param>
        /// <param name="zoneNo"></param>
        /// <returns></returns>
        public PartsStockInfo GetStockInfo(string partNo, string supplierNum, string wmNo, string zoneNo)
        {
            string sql = "select * from [LES].[TM_BAS_PARTS_STOCK] with(nolock) "
                + "where [VALID_FLAG] = 1 and [PART_NO] = @PART_NO and [SUPPLIER_NUM] = @SUPPLIER_NUM and [WM_NO] = @WM_NO and [ZONE_NO] = @ZONE_NO;";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PART_NO", DbType.AnsiString, partNo);
            db.AddInParameter(cmd, "@SUPPLIER_NUM", DbType.AnsiString, supplierNum);
            db.AddInParameter(cmd, "@WM_NO", DbType.AnsiString, wmNo);
            db.AddInParameter(cmd, "@ZONE_NO", DbType.AnsiString, zoneNo);
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                if (dr.Read())
                    return CreatePartsStockInfo(dr);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="partNos"></param>
        /// <returns></returns>
        public List<PartsStockInfo> GetListForInterfaceDataSync(List<string> partNos)
        {
            string sql = "select [ID],[PART_NO],[SUPPLIER_NUM],[WM_NO],[ZONE_NO],[DLOC],[INBOUND_PACKAGE_MODEL],[INBOUND_PACKAGE] "
                + "from [LES].[TM_BAS_PARTS_STOCK] with(nolock) "
                + "where [VALID_FLAG] = 1 and [PART_NO] in ('" + string.Join("','", partNos.ToArray()) + "');";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            List<PartsStockInfo> list = new List<PartsStockInfo>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    PartsStockInfo info = new PartsStockInfo();
                    info.Id = DBConvert.GetInt64(dr, dr.GetOrdinal("ID"));
                    info.PartNo = DBConvert.GetString(dr, dr.GetOrdinal("PART_NO"));
                    info.SupplierNum = DBConvert.GetString(dr, dr.GetOrdinal("SUPPLIER_NUM"));
                    info.WmNo = DBConvert.GetString(dr, dr.GetOrdinal("WM_NO"));
                    info.ZoneNo = DBConvert.GetString(dr, dr.GetOrdinal("ZONE_NO"));
                    info.Dloc = DBConvert.GetString(dr, dr.GetOrdinal("DLOC"));
                    info.InboundPackage = DBConvert.GetInt32Nullable(dr, dr.GetOrdinal("INBOUND_PACKAGE"));
                    info.InboundPackageModel = DBConvert.GetString(dr, dr.GetOrdinal("INBOUND_PACKAGE_MODEL"));
                    list.Add(info);
                }
            }
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="partNos"></param>
        /// <param name="zoneNos"></param>
        /// <returns></returns>
        public List<PartsStockInfo> GetListForInterfaceDataSync(List<string> partNos, List<string> zoneNos)
        {
            string sql = "select [ID],[PART_NO],[SUPPLIER_NUM],[WM_NO],[ZONE_NO],[DLOC],[INBOUND_PACKAGE_MODEL],[INBOUND_PACKAGE] "
                + "from [LES].[TM_BAS_PARTS_STOCK] with(nolock) "
                + "where [VALID_FLAG] = 1 and [PART_NO] in ('" + string.Join("','", partNos.ToArray()) + "') and [ZONE_NO] in ('" + string.Join("','", zoneNos.ToArray()) + "');";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            List<PartsStockInfo> list = new List<PartsStockInfo>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    PartsStockInfo info = new PartsStockInfo();
                    info.Id = DBConvert.GetInt64(dr, dr.GetOrdinal("ID"));
                    info.PartNo = DBConvert.GetString(dr, dr.GetOrdinal("PART_NO"));
                    info.SupplierNum = DBConvert.GetString(dr, dr.GetOrdinal("SUPPLIER_NUM"));
                    info.WmNo = DBConvert.GetString(dr, dr.GetOrdinal("WM_NO"));
                    info.ZoneNo = DBConvert.GetString(dr, dr.GetOrdinal("ZONE_NO"));
                    info.Dloc = DBConvert.GetString(dr, dr.GetOrdinal("DLOC"));
                    info.InboundPackage = DBConvert.GetInt32Nullable(dr, dr.GetOrdinal("INBOUND_PACKAGE"));
                    info.InboundPackageModel = DBConvert.GetString(dr, dr.GetOrdinal("INBOUND_PACKAGE_MODEL"));
                    list.Add(info);
                }
            }
            return list;
        }
    }
}
