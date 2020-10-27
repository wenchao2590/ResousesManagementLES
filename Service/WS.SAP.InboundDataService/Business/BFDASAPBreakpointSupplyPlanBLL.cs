using DM.LES;
using DM.SYS;
using Infrustructure.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace WS.SAP.InboundDataService
{
    /// <summary>
    /// SAP-LES-014	断点供货计划
    /// </summary>
    public class BFDASAPBreakpointSupplyPlanBLL
    {


        #region Common

       /// <summary>
       /// 操作用户
       /// </summary> 
        private string loginUser = "WS.SAP.InboundDataService";
        /// <summary>
        /// 时间格式
        /// </summary>
        private string dateTimeFormat = "yyyyMMdd";
        /// <summary>
        /// 是否写入日志
        /// </summary>
        string logFlag = ConfigurationManager.AppSettings["LogFlag"].ToLower();
        /// <summary>
        /// log日志名称
        /// </summary>
        string interfaceCode = "SAP-LES-014	断点供货计划";

        #endregion

        public SapBreakpointSupplyPlanInfo ConversionToCentreInfo(BFDASAPBreakpointSupplyPlanInfo bfdaInfo)
        {
            SapBreakpointSupplyPlanInfo sapInfo = new SapBreakpointSupplyPlanInfo();
            sapInfo.OrderNo = bfdaInfo.ORDER_NO;///生产订单号 	NVARCHAR(36)
            sapInfo.Matnr = bfdaInfo.MATNR;///物料号 		NVARCHAR(16)
            sapInfo.Lifnr = bfdaInfo.LIFNR;///供应商 		NVARHCAR(8)

            decimal bfCount = default(decimal);
            decimal.TryParse(bfdaInfo.MENGE.Trim(), out bfCount);
            sapInfo.Menge = bfCount;///数量		Dec 13

            return sapInfo;
        }
        public List<SapBreakpointSupplyPlanInfo> ConversionToCentreList(List<BFDASAPBreakpointSupplyPlanInfo> interfaceList)
        {
            List<SapBreakpointSupplyPlanInfo> list = new List<SapBreakpointSupplyPlanInfo>();
            foreach (BFDASAPBreakpointSupplyPlanInfo interfaceInfo in interfaceList)
            {
                list.Add(ConversionToCentreInfo(interfaceInfo));
            }
            return list;
        }

        public string GetKeyValue(BFDASAPBreakpointSupplyPlanInfo interfaceInfo)
        {
            throw new NotImplementedException();
        }

        public string GetKeyValues(List<BFDASAPBreakpointSupplyPlanInfo> interfaceList)
        {
            return string.Join(",", interfaceList.Select(d => d.ORDER_NO).ToArray());
        }

        public void InsertInfoToCentreTable(BFDASAPBreakpointSupplyPlanInfo interfaceInfo, Guid logFid, string logSql)
        {
            throw new NotImplementedException();
        }

        public int InsertListToCentreTable(List<BFDASAPBreakpointSupplyPlanInfo> interfaceList, Guid logFid, string logSql)
        {
            List<SapBreakpointSupplyPlanInfo> sapBreakpointSupplyPlanInfos = ConversionToCentreList(interfaceList);
            StringBuilder sqlSb = new StringBuilder(logSql);
            sqlSb.AppendLine();

            foreach (var info in sapBreakpointSupplyPlanInfos)
            {
                sqlSb.AppendFormat(" insert into [LES].[TI_IFM_SAP_BREAKPOINT_SUPPLY_PLAN] ("
                  + "[FID] ,"
                  + "[LOG_FID] ,"
                  + "[ORDER_NO] ,"
                  + "[MATNR] ,"
                  + "[LIFNR] ,"
                  + "[MENGE] ,"
                  + "[PROCESS_FLAG] ,"
                  + "[VALID_FLAG] ,"
                  + "[CREATE_USER] ,"
                  + "[PROCESS_TIME] ,"
                  + "[CREATE_DATE] ) values ( "
                  + "NEWID() ," /// FID - uniqueidentifier
                  + "N'{0}' ," /// LOG_FID - uniqueidentifier
                  + "N'{1}' ," /// ORDER_NO - nvarchar(64)
                  + "N'{2}' ," /// MATNR - nvarchar(32)
                  + "N'{3}' ," /// LIFNR - nvarchar(32)
                  + "{4} ," /// MENGE - decimal(18, 3)
                  + "{5} ," /// PROCESS_FLAG - int
                  + "{6} ," /// VALID_FLAG - bit
                  + "N'{7}' ," /// CREATE_USER - nvarchar(32)
                  + "GETDATE() ," /// PROCESS_TIME - datetime
                  + "GETDATE() ); "  /// CREATE_DATE datetime
                , logFid, info.OrderNo,info.Matnr,info.Lifnr, info.Menge, (int)ProcessFlagConstants.Untreated, 1, loginUser);
            }

            if (sqlSb.Length > 0)
            {
                Log.WriteLogToFile(logFlag ,sqlSb.ToString(), AppDomain.CurrentDomain.BaseDirectory + @"\Log_Script\", DateTime.Now.ToString("yyyyMMddHHmm"));
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(sqlSb.ToString());
            }
            return sapBreakpointSupplyPlanInfos.Count;
        }
    }
}