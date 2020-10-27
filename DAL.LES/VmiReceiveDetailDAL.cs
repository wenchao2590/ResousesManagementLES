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
    public partial class VmiReceiveDetailDAL
    {
        /// <summary>
        /// 根据库存维度将入库单明细合并
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="stockDimension"></param>
        /// <returns></returns>
        public List<VmiReceiveDetailInfo> GetStockDimensionList(List<long> ids, List<string> stockDimension)
        {
            string sql = "select " +
                (stockDimension.Count > 0 ? string.Join(",", stockDimension.ToArray()) + "," : string.Empty) +
                "PART_NO, " +
                "TARGET_WM, " +
                "TARGET_ZONE, " +
                "TARGET_DLOC, " +
                "PART_CNAME, " +
                "PART_ENAME, " +
                "MEASURING_UNIT_NO, " +
                "IDENTIFY_PART_NO, " +
                "PART_TYPE, " +
                "REQUIRED_BOX_NUM = sum(REQUIRED_BOX_NUM), " +
                "REQUIRED_QTY = sum(REQUIRED_QTY), " +
                "ACTUAL_BOX_NUM = sum(ACTUAL_BOX_NUM), " +
                "ACTUAL_QTY = sum(ACTUAL_QTY), " +
                "TRAN_NO, " +
                "ORIGIN_PLACE, " +
                "PART_CLS, " +
                "PACKAGE_LENGTH, " +
                "PACKAGE_WIDTH, " +
                "PACKAGE_HEIGHT, " +
                "PERPACKAGE_GROSS_WEIGHT, " +
                "PACKAGE_VOLUME, " +
                "SUM_WEIGHT = sum(SUM_WEIGHT), " +
                "SUM_VOLUME = sum(SUM_VOLUME) " +
                "from [LES].[TT_WMM_VMI_RECEIVE_DETAIL] with(nolock) " +
                "where [VALID_FLAG] = 1 and [ID] in (" + string.Join(",", ids.ToArray()) + ") " +
                "group by [PART_NO],[PART_CNAME],[PART_ENAME],[MEASURING_UNIT_NO],[IDENTIFY_PART_NO],[PART_TYPE],[TRAN_NO],[ORIGIN_PLACE],[PART_CLS]," +
                "[PACKAGE_LENGTH],[PACKAGE_WIDTH],[PACKAGE_HEIGHT],[PERPACKAGE_GROSS_WEIGHT],[PACKAGE_VOLUME],[TARGET_DLOC],[TARGET_WM],[TARGET_ZONE]"
                + (stockDimension.Count > 0 ? "," + string.Join(",", stockDimension.ToArray()) : string.Empty) + ";";
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            List<VmiReceiveDetailInfo> list = new List<VmiReceiveDetailInfo>();
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                while (dr.Read())
                {
                    VmiReceiveDetailInfo info = new VmiReceiveDetailInfo();
                    foreach (var dimension in stockDimension)
                    {
                        if (dimension.ToUpper() == "PLANT") info.Plant = DBConvert.GetString(dr, dr.GetOrdinal("PLANT"));
                        if (dimension.ToUpper() == "SUPPLIER_NUM") info.SupplierNum = DBConvert.GetString(dr, dr.GetOrdinal("SUPPLIER_NUM"));
                        if (dimension.ToUpper() == "PACKAGE_MODEL") info.PackageModel = DBConvert.GetString(dr, dr.GetOrdinal("PACKAGE_MODEL"));
                        if (dimension.ToUpper() == "PACKAGE") info.Package = DBConvert.GetDecimalNullable(dr, dr.GetOrdinal("PACKAGE"));
                        if (dimension.ToUpper() == "ORIGIN_PLACE") info.Package = DBConvert.GetDecimalNullable(dr, dr.GetOrdinal("ORIGIN_PLACE"));
                        if (dimension.ToUpper() == "PART_CLS") info.PartCls = DBConvert.GetString(dr, dr.GetOrdinal("PART_CLS"));
                    }
                    info.PartNo = DBConvert.GetString(dr, dr.GetOrdinal("PART_NO"));
                    info.TargetWm = DBConvert.GetString(dr, dr.GetOrdinal("TARGET_WM"));
                    info.TargetZone = DBConvert.GetString(dr, dr.GetOrdinal("TARGET_ZONE"));
                    info.TargetDloc = DBConvert.GetString(dr, dr.GetOrdinal("TARGET_DLOC"));
                    info.PartCname = DBConvert.GetString(dr, dr.GetOrdinal("PART_CNAME"));
                    info.PartEname = DBConvert.GetString(dr, dr.GetOrdinal("PART_ENAME"));
                    info.MeasuringUnitNo = DBConvert.GetString(dr, dr.GetOrdinal("MEASURING_UNIT_NO"));
                    info.IdentifyPartNo = DBConvert.GetString(dr, dr.GetOrdinal("IDENTIFY_PART_NO"));
                    info.PartType = DBConvert.GetInt32Nullable(dr, dr.GetOrdinal("PART_TYPE"));
                    info.RequiredBoxNum = DBConvert.GetInt32Nullable(dr, dr.GetOrdinal("REQUIRED_BOX_NUM"));
                    info.RequiredQty = DBConvert.GetDecimalNullable(dr, dr.GetOrdinal("REQUIRED_QTY"));
                    info.ActualBoxNum = DBConvert.GetInt32Nullable(dr, dr.GetOrdinal("ACTUAL_BOX_NUM"));
                    info.ActualQty = DBConvert.GetDecimalNullable(dr, dr.GetOrdinal("ACTUAL_QTY"));
                    info.TranNo = DBConvert.GetString(dr, dr.GetOrdinal("TRAN_NO"));
                    info.PackageLength = DBConvert.GetDecimalNullable(dr, dr.GetOrdinal("PACKAGE_LENGTH"));
                    info.PackageWidth = DBConvert.GetDecimalNullable(dr, dr.GetOrdinal("PACKAGE_WIDTH"));
                    info.PackageHeight = DBConvert.GetDecimalNullable(dr, dr.GetOrdinal("PACKAGE_HEIGHT"));
                    info.PerpackageGrossWeight = DBConvert.GetDecimalNullable(dr, dr.GetOrdinal("PERPACKAGE_GROSS_WEIGHT"));
                    info.PackageVolume = DBConvert.GetDecimalNullable(dr, dr.GetOrdinal("PACKAGE_VOLUME"));
                    info.SumWeight = DBConvert.GetDecimalNullable(dr, dr.GetOrdinal("SUM_WEIGHT"));
                    info.SumVolume = DBConvert.GetDecimalNullable(dr, dr.GetOrdinal("SUM_VOLUME"));
                    list.Add(info);
                }
            }
            return list;
        }
    }
}
