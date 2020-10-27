namespace WS.QMIS.InboundDataService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DM.LES;
    using DM.SYS;
    using Infrustructure.Logging;
    /// <summary>
    /// 检验模式处理
    /// </summary>
    public class BFDAQmisCheckModeBLL : IBusiness<QmisCheckModeInfo, BFDAQmisCheckModeInfo>
    {
        /// <summary>
        /// 操作用户
        /// </summary>
        private string loginUser = "WS.QMIS.InboundDataService";
        /// <summary>
        /// 把中间表转成业务表
        /// </summary>
        /// <param name="qmisCheckModeInfo"></param>
        /// <returns></returns>
        public QmisCheckModeInfo ConversionToCentreInfo(BFDAQmisCheckModeInfo qmisCheckModeInfo)
        {
            QmisCheckModeInfo checkModeInfo = new QmisCheckModeInfo();
            checkModeInfo.PartNo = qmisCheckModeInfo.partNo;    ///物料编号	
            checkModeInfo.PartName = qmisCheckModeInfo.partName;///物料名称	
            checkModeInfo.SupplierNo = qmisCheckModeInfo.supplierNo;    ///供应商代码
            checkModeInfo.SupplierName = qmisCheckModeInfo.supplierName;///供应商名称
            checkModeInfo.CheckMode = qmisCheckModeInfo.checkMode;	///检验模式	
            return checkModeInfo;
        }
        /// <summary>
        /// 中间表集合转业务表集合
        /// </summary>
        /// <param name="qmisCheckModeInfos"></param>
        /// <returns></returns>
        public List<QmisCheckModeInfo> ConversionToCentreList(List<BFDAQmisCheckModeInfo> qmisCheckModeInfos)
        {
            List<QmisCheckModeInfo> checkModeInfos = new List<QmisCheckModeInfo>();
            foreach (var qmisCheckModeInfo in qmisCheckModeInfos)
            {
                checkModeInfos.Add(ConversionToCentreInfo(qmisCheckModeInfo));
            }
            return checkModeInfos;
        }
        /// <summary>
        /// GetKeyValue
        /// </summary>
        /// <param name="qmisCheckModeInfo"></param>
        /// <returns></returns>
        public string GetKeyValue(BFDAQmisCheckModeInfo qmisCheckModeInfo)
        {
            return qmisCheckModeInfo.partNo + "|" + qmisCheckModeInfo.supplierNo + "|" + qmisCheckModeInfo.checkMode;
        }
        /// <summary>
        /// GetKeyValues
        /// </summary>
        /// <param name="interfaceList"></param>
        /// <returns></returns>
        public string GetKeyValues(List<BFDAQmisCheckModeInfo> qmisCheckModeInfos)
        {
            return string.Join(",", qmisCheckModeInfos.Select(d => d.partNo + "|" + d.supplierNo + "|" + d.checkMode).ToArray());
        }
        /// <summary>
        /// InsertInfoToCentreTable
        /// </summary>
        /// <param name="interfaceInfo"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        public void InsertInfoToCentreTable(BFDAQmisCheckModeInfo qmisCheckModeInfo, Guid logFid, string logSql)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// InsertListToCentreTable
        /// </summary>
        /// <param name="interfaceList"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        /// <returns></returns>
        public int InsertListToCentreTable(List<BFDAQmisCheckModeInfo> qmisCheckModeInfos, Guid logFid, string logSql)
        {
            List<QmisCheckModeInfo> checkModeInfos = ConversionToCentreList(qmisCheckModeInfos);
            StringBuilder sqlSb = new StringBuilder(logSql);
            foreach (var checkModeInfo in checkModeInfos)
            {
                sqlSb.AppendFormat(" insert into [LES].[TI_IFM_QMIS_CHECK_MODE] "
                + "([FID] ,"
                + "[LOG_FID] ,"
                + "[PART_NO] ,"
                + "[PART_NAME] ,"
                + "[SUPPLIER_NO] ,"
                + "[SUPPLIER_NAME] ,"
                + "[CHECK_MODE] ,"
                + "[CREATE_USER] ,"
                + "[PROCESS_FLAG] ,"
                + "[VALID_FLAG] ,"
                + "[PROCESS_TIME], "
                + "[CREATE_DATE]) "
                + "values (NEWID() , "      	/// FID - uniqueidentifier
                + "N'{0}' , "		///LOG_FID - uniqueidentifier
                + "N'{1}' , "	    /// PART_NO - nvarchar(32)
                + "N'{2}' ,"	        /// PART_NAME - nvarchar(128)
                + "N'{3}' ,"	        /// SUPPLIER_NO - nvarchar(32)
                + "N'{4}' ,"	        /// SUPPLIER_NAME - nvarchar(128)
                + "N'{5}' ,"	        /// CHECK_MODE - nvarchar(1)
		        + "N'{6}' ,"	        /// CREATE_USER - nvarchar(32)
                + "{7} ,"		        /// PROCESS_FLAG - int
                + "1 ,"		        /// VALID_FLAG - bit
		        + "NULL ," 		/// PROCESS_TIME - datetime
                + "GETDATE()"		/// CREATE_DATE - datetime
                + ");"
                , logFid///LOG_FID - uniqueidentifier 0
                , checkModeInfo.PartNo/// PART_NO - nvarchar(32) 1
                , checkModeInfo.PartName/// PART_NAME - nvarchar(128) 2
                , checkModeInfo.SupplierNo/// SUPPLIER_NO - nvarchar(32) 3
                , checkModeInfo.SupplierName/// SUPPLIER_NAME - nvarchar(128) 4
                , checkModeInfo.CheckMode/// CHECK_MODE - nvarchar(1) 5
                , loginUser/// PROCESS_FLAG - int 6
                , (int)ProcessFlagConstants.Untreated/// PROCESS_FLAG - int 7
                );
            }
            if (sqlSb.Length > 0)
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(sqlSb.ToString());
            return qmisCheckModeInfos.Count;
        }
    }
}