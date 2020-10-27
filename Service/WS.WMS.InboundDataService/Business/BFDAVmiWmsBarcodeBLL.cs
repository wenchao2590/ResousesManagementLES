using DM.LES;
using DM.SYS;
using Infrustructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
 

namespace WS.VMI.InboundDataService
{

    /// <summary>
    /// WMS-LES-007	箱标签
    /// </summary>
    public class BFDAVmiWmsBarcodeBLL : IBusiness<WmsVmiBarcodeInfo, BFDAVmiBarcodeInfo>
    {
        /// <summary>
        /// 操作用户
        /// </summary>
        private string loginUser = "WS.VMI.InboundDataService";

        /// <summary>
        /// 将报文表转换成中间表
        /// </summary>
        /// <param name="bFDAVmiBarcodeInfo"></param>
        /// <returns></returns>
        public WmsVmiBarcodeInfo ConversionToCentreInfo(BFDAVmiBarcodeInfo  bFDAVmiBarcodeInfo)
        {
            WmsVmiBarcodeInfo wmsBarcodeInfo = new WmsVmiBarcodeInfo();
            
            wmsBarcodeInfo.PackageBarcode = bFDAVmiBarcodeInfo.PackageBarcode;           ///箱标签号

            wmsBarcodeInfo.SourceOrderCode = bFDAVmiBarcodeInfo.SourceOrderCode;         ///原始单据号

            wmsBarcodeInfo.PartNo = bFDAVmiBarcodeInfo.PartNo;                           ///物料编号

            wmsBarcodeInfo.PartCname = bFDAVmiBarcodeInfo.PartCname;                     ///物料名称

            wmsBarcodeInfo.Snp = bFDAVmiBarcodeInfo.Snp;                                 ///收容数

            wmsBarcodeInfo.PartQty = bFDAVmiBarcodeInfo.PartQty;                         ///数量

            wmsBarcodeInfo.LinePosition = bFDAVmiBarcodeInfo.LinePosition;               ///线边工位

            wmsBarcodeInfo.SupermarketRepository = bFDAVmiBarcodeInfo.SupermarketRepository; ///超市库位

            wmsBarcodeInfo.TargetSlcode = bFDAVmiBarcodeInfo.TargetSlcode;               ///目标库存

            wmsBarcodeInfo.PackageCode = bFDAVmiBarcodeInfo.PackageCode;                 ///包装型号

            wmsBarcodeInfo.SupplierCode = bFDAVmiBarcodeInfo.SupplierCode;               ///供应商编号

            wmsBarcodeInfo.SupplierName = bFDAVmiBarcodeInfo.SupplierName;               ///供应商名称

            wmsBarcodeInfo.Remark = bFDAVmiBarcodeInfo.Remark;                           ///备注

            return wmsBarcodeInfo;
        }

        /// <summary>
        /// 将报文表集合转换成中间表集合
        /// </summary>
        /// <param name="bFDAVmiBarcodeInfos"></param>
        /// <returns></returns>
        public List<WmsVmiBarcodeInfo> ConversionToCentreList(List<BFDAVmiBarcodeInfo>  bFDAVmiBarcodeInfos)
        {
            List<WmsVmiBarcodeInfo> list = new List<WmsVmiBarcodeInfo>();
            foreach (BFDAVmiBarcodeInfo bFDAVmiBarcodeInfo in bFDAVmiBarcodeInfos)
            {
                list.Add(ConversionToCentreInfo(bFDAVmiBarcodeInfo));
            }
            return list;
        }
        /// <summary>
        /// 获取关键词-箱标签号
        /// </summary>
        /// <param name="bFDAVmiBarcodeInfo"></param>
        /// <returns></returns>
        public string GetKeyValue(BFDAVmiBarcodeInfo bFDAVmiBarcodeInfo)
        {
            return bFDAVmiBarcodeInfo.PackageBarcode;
        }
        /// <summary>
        /// 获取所有关键词集合-箱标签号
        /// </summary>
        /// <param name="bFDAVmiBarcodeInfos"></param>
        /// <returns></returns>
        public string GetKeyValues(List<BFDAVmiBarcodeInfo>  bFDAVmiBarcodeInfos)
        {
            return string.Join(",", bFDAVmiBarcodeInfos.Select(d => d.PackageBarcode).ToArray());
        }

        public void InsertInfoToCentreTable(BFDAVmiBarcodeInfo bFDAVmiBarcodeInfo, Guid logFid, string logSql)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 插入集合信息
        /// </summary>
        /// <param name="bFDAVmiBarcodeInfos"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        /// <returns></returns>
        public int InsertListToCentreTable(List<BFDAVmiBarcodeInfo>  bFDAVmiBarcodeInfos, Guid logFid, string logSql)
        {
            
            List<WmsVmiBarcodeInfo> vmiWmsBarcodes = ConversionToCentreList(bFDAVmiBarcodeInfos);

            StringBuilder sqlSb = new StringBuilder(logSql);
            sqlSb.AppendLine();
            foreach (var info in vmiWmsBarcodes)
            {
                sqlSb.AppendFormat("INSERT INTO [LES].[TI_IFM_WMS_VMI_BARCODE]  ( "
                + "[FID] ,"
                + "[LOG_FID] ,"
                + "[PACKAGE_BARCODE] ,"     ///箱标签号 1
                + "[SOURCE_ORDER_CODE] ,"   ///原始单据号 2
                + "[PART_NO] ,"             ///物料编号 3
                + "[PART_CNAME] ,"          ///物料名称 4
                + "[SNP] ,"                 ///收容数 5 (数字)
                + "[PART_QTY] ,"            ///数量   6 (数字)
                + "[LINE_POSITION] ,"       ///线边工位 7
                + "[SUPERMARKET_REPOSITORY] ," ///超市库位 8
                + "[TARGET_SLCODE] ,"       ///目标库位 9
                + "[PACKAGE_CODE] ,"        ///包装型号 10
                + "[SUPPLIER_CODE] ,"       ///供应商代码 11
                + "[SUPPLIER_NAME] ,"       ///供应商名称 12
                + "[REMARK] ,"              ///备注   13
                + "[PROCESS_FLAG] ,"        ///处理状态  
                + "[VALID_FLAG] ,"          ///逻辑删除状态 
                + "[PROCESS_TIME] ,"        ///处理时间
                + "[CREATE_DATE] ,"         ///创建时间
                + "[CREATE_USER] "          ///创建人
                + " )values ( NEWID(), '{0}','{1}','{2}','{3}','{4}',{5},{6},'{7}','{8}','{9}','{10}','{11}','{12}','{13}',{14},{15},GETDATE(),GETDATE(),'{16}' ); ",
                logFid,
                info.PackageBarcode,        ///箱标签号 1
                info.SourceOrderCode,       ///原始单据号 2
                info.PartNo,                ///物料编号 3
                info.PartCname,             ///物料名称 4
                info.Snp,                   ///收容数 5 (数字)
                info.PartQty,               ///数量   6 (数字)
                info.LinePosition,          ///线边工位 7
                info.SupermarketRepository, ///超市库位 8
                info.TargetSlcode,          ///目标库位 9
                info.PackageCode,           ///包装型号 10
                info.SupplierCode,          ///供应商代码 11
                info.SupplierName,          ///供应商名称 12
                info.Remark,                ///备注   13
                (int)ProcessFlagConstants.Untreated,    ///处理状态 14
                1,                          ///逻辑删除   15
                loginUser
                );
                sqlSb.AppendLine();
            }
         
            if (sqlSb.Length > 0)
            {
                Log.WriteLogToFile(sqlSb.ToString(), AppDomain.CurrentDomain.BaseDirectory + @"\SQL-Log\", DateTime.Now.ToString("yyyyMMddHHmm"));
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(sqlSb.ToString());
            }
            return vmiWmsBarcodes.Count;
           
        }
    }
}