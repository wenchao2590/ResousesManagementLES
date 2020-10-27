using DAL.LES;
using DM.LES;
using DM.SYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;
using WS.SRM.InboundDataService;

namespace WS.SRM.InboundDataService
{

    /// <summary>
    /// 箱标签业务处理
    /// </summary>
    public class BFDASrmBarcodeBLL : IBusiness<SrmBarcodeInfo, BFDASrmBarcodeInfo>
    {
        /// <summary>
        /// 操作用户
        /// </summary>
        private string loginUser = "WS.SRM.InboundDataService";
        /// <summary>
        /// 转换实体
        /// </summary>
        /// <param name="barcodeInfo"></param>
        /// <returns></returns>
        public SrmBarcodeInfo ConversionToCentreInfo(BFDASrmBarcodeInfo barcodeInfo)
        {
            SrmBarcodeInfo info = new SrmBarcodeInfo();
            ///箱标签号
            info.PackageBarcode = barcodeInfo.PackageBarcode;
            ///原始单据号
            info.SourceOrderCode = barcodeInfo.SourceOrderCode;
            ///物料编号
            info.PartNo = barcodeInfo.PartNo;
            ///物料名称
            info.PartCname = barcodeInfo.PartCname;
            ///数量
            info.PartQty = barcodeInfo.PartQty;
            ///目标库位
            info.TargetSlcode = barcodeInfo.TargetSlcode;
            ///包装型号
            info.PackageCode = barcodeInfo.PackageCode;
            ///收容数
            info.Snp = barcodeInfo.SNP;
            ///备注
            info.Remark = barcodeInfo.Remark;
            ///
            return info;
        }
        /// <summary>
        /// 将报文表集合转换成中间表集合
        /// </summary>
        /// <param name="barcodeInfos"></param>
        /// <returns></returns>
        public List<SrmBarcodeInfo> ConversionToCentreList(List<BFDASrmBarcodeInfo> barcodeInfos)
        {
            List<SrmBarcodeInfo> list = new List<SrmBarcodeInfo>();
            foreach (BFDASrmBarcodeInfo barcodeInfo in barcodeInfos)
            {
                list.Add(ConversionToCentreInfo(barcodeInfo));
            }
            return list;
        }
        /// <summary>
        /// GetKeyValue
        /// </summary>
        /// <param name="barcodeInfo"></param>
        /// <returns></returns>
        public string GetKeyValue(BFDASrmBarcodeInfo barcodeInfo)
        {
            return barcodeInfo.PackageBarcode;
        }
        /// <summary>
        /// GetKeyValues
        /// </summary>
        /// <param name="barcodeInfos"></param>
        /// <returns></returns>
        public string GetKeyValues(List<BFDASrmBarcodeInfo> barcodeInfos)
        {
            return string.Join(",", barcodeInfos.Select(d => d.PackageBarcode).ToArray());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="barcodeInfo"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        public void InsertInfoToCentreTable(BFDASrmBarcodeInfo barcodeInfo, Guid logFid, string logSql)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 插入集合信息
        /// </summary>
        /// <param name="barcodeInfos"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        /// <returns></returns>
        public int InsertListToCentreTable(List<BFDASrmBarcodeInfo> barcodeInfos, Guid logFid, string logSql)
        {
            List<SrmBarcodeInfo> list = ConversionToCentreList(barcodeInfos);
            StringBuilder @string = new StringBuilder(logSql);
            foreach (SrmBarcodeInfo info in list)
            {
                ///LOG_FID
                info.LogFid = logFid;
                ///
                info.ProcessFlag = (int)ProcessFlagConstants.Untreated;
                ///CREATE_USER
                info.CreateUser = loginUser;

                @string.AppendLine(SrmBarcodeDAL.GetInsertSql(info));
            }
            using (TransactionScope tran = new TransactionScope())
            {
                if (@string.Length > 0)
                    BLL.SYS.CommonBLL.ExecuteNonQueryBySql(@string.ToString());
                tran.Complete();
            }
            return list.Count;
        }
    }
}