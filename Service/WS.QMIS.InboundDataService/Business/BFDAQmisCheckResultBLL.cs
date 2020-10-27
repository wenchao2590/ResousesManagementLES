using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DM.LES;
using BLL.LES;
using System.Text;
using DM.SYS;
using Infrustructure.Logging;

namespace WS.QMIS.InboundDataService
{
    /// <summary>
    /// QMIS-LES-003 检验结果回传-业务表
    /// </summary>
    public class BFDAQmisCheckResultBLL : IBusiness<QmisCheckResultInfo, BFDAQmisCheckResultInfo>
    {
        /// <summary>
        /// 操作用户
        /// </summary>
        private string loginUser = "WS.QMIS.InboundDataService";
        /// <summary>
        /// 字典表转中间表
        /// </summary>
        /// <param name="qmisCheckResultInfo"></param>
        /// <returns></returns>
        public QmisCheckResultInfo ConversionToCentreInfo(BFDAQmisCheckResultInfo qmisCheckResultInfo)
        {
            QmisCheckResultInfo checkResultInfo = new QmisCheckResultInfo();
            checkResultInfo.AsnNo = qmisCheckResultInfo.asnNo;/// ASN单号
            checkResultInfo.OrderNo = qmisCheckResultInfo.orderNo;/// 拉动单号
            checkResultInfo.PartNo = qmisCheckResultInfo.partNo;/// 物料编号
            checkResultInfo.SupplierNo = qmisCheckResultInfo.supplierNo;/// 供应商编号
            if (!int.TryParse(qmisCheckResultInfo.totalNo, out int intTotalNo))
                throw new Exception("MC:0x00000401");///送检数量数据错误
            checkResultInfo.TotalNo = intTotalNo;/// 送检数量
            if (!int.TryParse(qmisCheckResultInfo.unqualifiedNo, out int intUnqualifiedNo))
                throw new Exception("MC:0x00000409");///不合格数量数据错误
            checkResultInfo.UnqualifiedNo = intUnqualifiedNo;/// 不合格数量
            checkResultInfo.CheckStatus = qmisCheckResultInfo.checkStatus == "1" ? 1 : 0;   /// 检验状态
            return checkResultInfo;
        }
        /// <summary>
        /// 字典集合转中报表集合
        /// </summary>
        /// <param name="qmisCheckResultInfos"></param>
        /// <returns></returns>
        public List<QmisCheckResultInfo> ConversionToCentreList(List<BFDAQmisCheckResultInfo> qmisCheckResultInfos)
        {
            List<QmisCheckResultInfo> checkResultInfos = new List<QmisCheckResultInfo>();
            foreach (var qmisCheckResultInfo in qmisCheckResultInfos)
            {
                checkResultInfos.Add(ConversionToCentreInfo(qmisCheckResultInfo));
            }
            return checkResultInfos;
        }
        /// <summary>
        /// GetKeyValue
        /// </summary>
        /// <param name="interfaceInfo"></param>
        /// <returns></returns>
        public string GetKeyValue(BFDAQmisCheckResultInfo qmisCheckResultInfo)
        {
            return qmisCheckResultInfo.asnNo + "|" + qmisCheckResultInfo.orderNo + "|" + qmisCheckResultInfo.partNo + "|" + qmisCheckResultInfo.supplierNo;
        }
        /// <summary>
        /// 获取报文集合中,关键字段集合
        /// </summary>
        /// <param name="qmisCheckResultInfos"></param>
        /// <returns></returns>
        public string GetKeyValues(List<BFDAQmisCheckResultInfo> qmisCheckResultInfos)
        {
            return string.Join(",", qmisCheckResultInfos.Select(d => d.asnNo + "|" + d.orderNo + "|" + d.partNo + "|" + d.supplierNo).ToArray());
        }
        /// <summary>
        /// InsertInfoToCentreTable
        /// </summary>
        /// <param name="qmisCheckResultInfo"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        public void InsertInfoToCentreTable(BFDAQmisCheckResultInfo qmisCheckResultInfo, Guid logFid, string logSql)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 批量插入到中间表
        /// </summary>
        /// <param name="qmisCheckResultInfos"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        /// <returns></returns>
        public int InsertListToCentreTable(List<BFDAQmisCheckResultInfo> qmisCheckResultInfos, Guid logFid, string logSql)
        {
            ///把报文数据转换成中间表
            List<QmisCheckResultInfo> checkResultInfos = ConversionToCentreList(qmisCheckResultInfos);
            StringBuilder sqlSb = new StringBuilder(logSql);
            foreach (var checkResultInfo in checkResultInfos)
            {
                sqlSb.AppendFormat("insert into [LES].[TI_IFM_QMIS_CHECK_RESULT]"
                + "([FID] ,"
                + " [LOG_FID] ,"
                + " [ASN_NO] ,"
                + " [ORDER_NO] ,"
                + " [PART_NO] ,"
                + " [SUPPLIER_NO] ,"
                + " [TOTAL_NO] ,"
                + " [UNQUALIFIED_NO] ,"
                + " [CHECK_STATUS] ,"
                + " [VALID_FLAG] ,"
                + " [PROCESS_FLAG] ,"
                + " [CREATE_USER] ,"
                + " [PROCESS_TIME] ,"
                + " [CREATE_DATE]"
                + " ) values (  "
                + " NEWID() ,    "            /// FID - uniqueidentifier
                + " N'{0}' ,    "            /// LOG_FID - uniqueidentifier
                + " N'{1}' ,"        /// ASN_NO - nvarchar(64)
                + " N'{2}' ,"        /// ORDER_NO - nvarchar(32)
                + " N'{3}' ,"        /// PART_NO - nvarchar(32)
                + " N'{4}' ,"        /// SUPPLIER_NO - nvarchar(32)
                + " {5},"      /// TOTAL_NO - int
                + " {6},"      /// UNQUALIFIED_NO - int
                + " {7},"      /// CHECK_STATUS - int
                + " 1 ,"         /// VALID_FLAG - bit
                + " {8} ,"      /// PROCESS_FLAG - int
                + " N'{9}' ,"        /// CREATE_USER - nvarchar(32)
                + " NULL ,"              /// PROCESS_TIME - datetime
                + " GETDATE());",
                logFid,/// LOG_FID - uniqueidentifier,0
                checkResultInfo.AsnNo,/// ASN_NO - nvarchar(64),1
                checkResultInfo.OrderNo,/// ORDER_NO - nvarchar(32),2
                checkResultInfo.PartNo,/// PART_NO - nvarchar(32),3
                checkResultInfo.SupplierNo,/// SUPPLIER_NO - nvarchar(32),4
                checkResultInfo.TotalNo,/// TOTAL_NO - int,5
                checkResultInfo.UnqualifiedNo,/// UNQUALIFIED_NO - int,6
                checkResultInfo.CheckStatus,/// CHECK_STATUS - int,7
                (int)ProcessFlagConstants.Untreated,/// PROCESS_FLAG - int,8
                loginUser/// CREATE_USER - nvarchar(32),9
                );
            }
            if (sqlSb.Length > 0)
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(sqlSb.ToString());
            return checkResultInfos.Count;
        }
    }
}