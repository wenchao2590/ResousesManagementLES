using DM.LES;
using DM.SYS;
using Infrustructure.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WS.MES.InboundDataService
{
    /// <summary>
    /// MES-LES-003	PBS物料排查结果
    /// </summary>
    public class BFDAMESPbsMaterialInvestigationResultsBLL
    {
        /// <summary>
        /// GetKeyValue
        /// </summary>
        /// <param name="mesPbsMaterialInvestigationResultsInfo"></param>
        /// <returns></returns>
        public string GetKeyValue(BFDAMesPbsMaterialInvestigationResultsInfo mesPbsMaterialInvestigationResultsInfo)
        {
            return mesPbsMaterialInvestigationResultsInfo.AREA_NO + "|" + mesPbsMaterialInvestigationResultsInfo.DMS_NO + "|" + mesPbsMaterialInvestigationResultsInfo.SEND_TIME;
        }
        /// <summary>
        /// GetKeyValues
        /// </summary>
        /// <param name="mesPbsMaterialInvestigationResultsInfos"></param>
        /// <returns></returns>
        public string GetKeyValues(List<BFDAMesPbsMaterialInvestigationResultsInfo> mesPbsMaterialInvestigationResultsInfos)
        {
            return string.Join(",", mesPbsMaterialInvestigationResultsInfos.Select(d => d.AREA_NO + "|" + d.DMS_NO + "|" + d.SEND_TIME).ToArray());
        }
        /// <summary>
        /// InsertInfoToCentreTable
        /// </summary>
        /// <param name="mesPbsMaterialInvestigationResultsInfo"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        public void InsertInfoToCentreTable(BFDAMesPbsMaterialInvestigationResultsInfo mesPbsMaterialInvestigationResultsInfo, Guid logFid, string logSql)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// InsertListToCentreTable
        /// </summary>
        /// <param name="mesPbsMaterialInvestigationResultsInfos"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        /// <returns></returns>
        public int InsertListToCentreTable(List<BFDAMesPbsMaterialInvestigationResultsInfo> mesPbsMaterialInvestigationResultsInfos, Guid logFid, string logSql)
        {
            throw new NotImplementedException();
        }
    }
}