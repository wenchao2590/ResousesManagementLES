namespace DAL.LES
{
    using DM.LES;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    /// <summary>
    /// BarcodeDAL
    /// </summary>
    public partial class BarcodeDAL
    {
        /// <summary>
        /// 获取更新条码记录的语句
        /// </summary>
        /// <param name="barcodeStatus"></param>
        /// <param name="wmNo"></param>
        /// <param name="zoneNo"></param>
        /// <param name="dloc"></param>
        /// <param name="barcodeId"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public static string GetBarcodeUpdateSql(int barcodeStatus, string wmNo, string zoneNo, string dloc, string asnRunsheetNo, Guid barcodeFid, string loginUser)
        {
            string sql = "update [LES].[TT_WMM_BARCODE] set " +
                "[BARCODE_STATUS] = " + barcodeStatus + "," +
                "[WM_NO] = N'" + wmNo + "'," +
                "[ZONE_NO] = N'" + zoneNo + "'," +
                "[DLOC] = N'" + dloc + "'," +
                "[ASN_RUNSHEET_NO] = N'" + asnRunsheetNo + "'," +
                "[MODIFY_USER] = N'" + loginUser + "'," +
                "[MODIFY_DATE] = GETDATE() " +
                "where [FID] = N'" + barcodeFid + "';" +
                "insert into [LES].[TT_WMM_BARCODE_STATUS] (" +
                "FID, BARCODE_FID, PART_NO, PART_CNAME, BARCODE_DATA, BARCODE_STATUS, PACKAGE_MODEL, PACKAGE, CURRENT_QTY, MEASURING_UNIT_NO, SUPPLIER_NUM, PLANT, ASSEMBLY_LINE, LOCATION, DOCK, WM_NO, ZONE_NO, DLOC, BATTH_NO, RFID_NO, ASN_RUNSHEET_NO, COMMENTS, VALID_FLAG, CREATE_USER, CREATE_DATE) select " +
                "NEWID(), FID, PART_NO, PART_CNAME, BARCODE_DATA, BARCODE_STATUS, PACKAGE_MODEL, PACKAGE, CURRENT_QTY, MEASURING_UNIT_NO, SUPPLIER_NUM, PLANT, ASSEMBLY_LINE, LOCATION, DOCK, WM_NO, ZONE_NO, DLOC, BATTH_NO, RFID_NO, ASN_RUNSHEET_NO, COMMENTS, 1, '" + loginUser + "', GETDATE() " +
                "from LES.TT_WMM_BARCODE with(nolock) where [FID] = N'" + barcodeFid + "';";
            return sql;
        }
    }
}
