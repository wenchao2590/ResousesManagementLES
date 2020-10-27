using DM.LES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.LES
{
    public partial class PackageTranDetailDAL
    {
        /// <summary>
        /// 获取新增包装交易数据的语句
        /// </summary>
        /// <param name="packageTranDetailInfo"></param>
        /// <returns></returns>
        public static string CreatePackageTranDetailSql(PackageTranDetailInfo packageTranDetailInfo)
        {
            return "insert into [LES].[TT_PCM_PACKAGE_TRAN_DETAIL] (" +
                        "FID, " +
                        "TRAN_NO, " +
                        "TRAN_TYPE, " +
                        "BARCODE_DATA, " +
                        "PART_NO, " +
                        "PLANT, " +
                        "ASSEMBLY_LINE, " +
                        "SUPPLIER_NUM, " +
                        "WM_NO, " +
                        "ZONE_NO, " +
                        "DLOC, " +
                        "TARGET_WM, " +
                        "TARGET_ZONE, " +
                        "TARGET_DLOC, " +
                        "PACKAGE_NO, " +
                        "PACKAGE_CNAME, " +
                        "PACKAGE_ENAME, " +
                        "PACKAGE, " +
                        "PACKAGE_QTY, " +
                        "STATUS, " +
                        "COMMENTS, " +
                        "VALID_FLAG, CREATE_USER, CREATE_DATE) values(" +
                        "NEWID()," +///FID
                        "N'" + packageTranDetailInfo.TranNo + "'," + ///TRAN_NO,
                        "" + packageTranDetailInfo.TranType.GetValueOrDefault() + "," +///TRAN_TYPE,
                        "N'" + packageTranDetailInfo.BarcodeData + "'," +///@BARCODE_DATA,
                        "N'" + packageTranDetailInfo.PartNo + "'," +///@PART_NO,
                        "N'" + packageTranDetailInfo.Plant + "'," +///@PLANT,
                        "N'" + packageTranDetailInfo.AssemblyLine + "'," +///@ASSEMBLY_LINE,
                        "N'" + packageTranDetailInfo.SupplierNum + "'," +///@SUPPLIER_NUM,
                        "N'" + packageTranDetailInfo.WmNo + "'," +///@WM_NO,
                        "N'" + packageTranDetailInfo.ZoneNo + "'," +///@ZONE_NO,
                        "N'" + packageTranDetailInfo.Dloc + "'," +///@DLOC,
                        "N'" + packageTranDetailInfo.TargetWm + "'," +///@TARGET_WM,
                        "N'" + packageTranDetailInfo.TargetZone + "'," +///@TARGET_ZONE,
                        "N'" + packageTranDetailInfo.TargetDloc + "'," +///@TargetDloc,
                        "N'" + packageTranDetailInfo.PackageNo + "'," +///@PACKAGE_NO,
                        "N'" + packageTranDetailInfo.PackageCname + "'," +///@PACKAGE_CNAME,
                        "N'" + packageTranDetailInfo.PackageEname + "'," +///@PACKAGE_ENAME,
                        packageTranDetailInfo.Package.GetValueOrDefault() + "," + ///@PACKAGE,
                        packageTranDetailInfo.PackageQty.GetValueOrDefault() + "," + ///@PACKAGE_QTY,
                        (int)PackageTranStateConstants.UNTREATED + "," +///@STATUS,
                        "N'" + packageTranDetailInfo.Comments + "'," +///@COMMENTS,
                        "1,N'" + packageTranDetailInfo.CreateUser + "',GETDATE()" +///VALID_FLAG, CREATE_USER, CREATE_DATE
                        ");";
        }
    }
}
