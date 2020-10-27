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
    /// WMS-LES-014	排序拉动单回传
    /// </summary>
    public class BFDAVmiJisPullOrderReturnBLL : IBusiness<VmiJisPullOrderReturnInfo, BFDAVmiJisPullOrderReturnInfo>
    {
        /// <summary>
        /// 操作用户
        /// </summary>
        private string loginUser = "WS.VMI.InboundDataService";

        /// <summary>
        /// 将报文表转换成中间表
        /// </summary>
        /// <param name="bFDAVmiJisPullOrderReturnInfo"></param>
        /// <returns></returns>
        public VmiJisPullOrderReturnInfo ConversionToCentreInfo(BFDAVmiJisPullOrderReturnInfo   bFDAVmiJisPullOrderReturnInfo)
        {
            VmiJisPullOrderReturnInfo TransitInventoryInfo = new VmiJisPullOrderReturnInfo();
            TransitInventoryInfo.Ordercode = bFDAVmiJisPullOrderReturnInfo.OrderCode;  ///来源单号
			TransitInventoryInfo.Partno = bFDAVmiJisPullOrderReturnInfo.PartNo;  ///物料代码
			TransitInventoryInfo.Carsortseq = bFDAVmiJisPullOrderReturnInfo.carsortseq;  ///车号	  
			TransitInventoryInfo.Wmssourcekey = bFDAVmiJisPullOrderReturnInfo.wmssourcekey;  ///WMS 单号
			TransitInventoryInfo.Wmslinenumber = bFDAVmiJisPullOrderReturnInfo.wmslinenumber;  ///行号	  

            return TransitInventoryInfo;
        }

        /// <summary>
        /// 将报文表集合转换成中间表集合
        /// </summary>
        /// <param name="bFDAVmiJisPullOrderReturnInfos"></param>
        /// <returns></returns>
        public List<VmiJisPullOrderReturnInfo> ConversionToCentreList(List<BFDAVmiJisPullOrderReturnInfo> bFDAVmiJisPullOrderReturnInfos)
        {
            List<VmiJisPullOrderReturnInfo> list = new List<VmiJisPullOrderReturnInfo>();
            foreach (BFDAVmiJisPullOrderReturnInfo bFDAVmiJisPullOrderReturnInfo in bFDAVmiJisPullOrderReturnInfos)
            {
                list.Add(ConversionToCentreInfo(bFDAVmiJisPullOrderReturnInfo));
            }
            return list;
        }
        /// <summary>
        /// 获取关键词
        /// </summary>
        /// <param name="bFDAVmiJisPullOrderReturnInfo"></param>
        /// <returns></returns>
        public string GetKeyValue(BFDAVmiJisPullOrderReturnInfo bFDAVmiJisPullOrderReturnInfo)
        {
            return bFDAVmiJisPullOrderReturnInfo.OrderCode;
        }
        /// <summary>
        /// 获取所有关键词集合
        /// </summary>
        /// <param name="bFDAVmiJisPullOrderReturnInfos"></param>
        /// <returns></returns>
        public string GetKeyValues(List<BFDAVmiJisPullOrderReturnInfo> bFDAVmiJisPullOrderReturnInfos)
        {
            return string.Join(",", bFDAVmiJisPullOrderReturnInfos.Select(d => d.OrderCode).ToArray());
        }

        public void InsertInfoToCentreTable(BFDAVmiJisPullOrderReturnInfo bFDAVmiJisPullOrderReturnInfo, Guid logFid, string logSql)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 插入集合信息
        /// </summary>
        /// <param name="bFDAVmiJisPullOrderReturnInfos"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        /// <returns></returns>
        public int InsertListToCentreTable(List<BFDAVmiJisPullOrderReturnInfo>   bFDAVmiJisPullOrderReturnInfos, Guid logFid, string logSql)
        {
            
            List<VmiJisPullOrderReturnInfo> vmiWmsBarcodes = ConversionToCentreList(bFDAVmiJisPullOrderReturnInfos);

            StringBuilder sqlSb = new StringBuilder(logSql);
            sqlSb.AppendLine();
            foreach (var info in vmiWmsBarcodes)
            {
                sqlSb.AppendFormat(" insert into [LES].[TI_IFM_VMI_JIS_PULL_ORDER_RETURN] (" 
                 +"[FID],"
                 +"[LOG_FID],"          
                 +"[ORDERCODE],"                  ///来源单号 1
                 +"[PARTNO],"                     ///物料代码 2
                 +"[CARSORTSEQ],"                 ///车号  3
                 +"[WMSSOURCEKEY],"               ///WMS 单号  4
                 +"[WMSLINENUMBER],"              ///行号  5
                 +"[PROCESS_FLAG],"               /// 6
                 +"[VALID_FLAG],"                 ///7
                 +"[CREATE_USER],"                ///8
                 +"[PROCESS_TIME],"               ///9
                 +"[CREATE_DATE]) values "        ///10
                 + " ( NEWID(), N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',{6},{7},N'{8}',GETDATE(),GETDATE() ); ",
                logFid,
                info.Ordercode,              ///来源单号 1
                info.Partno,                 ///物料代码 2
                info.Carsortseq,             ///车号  3
                info.Wmssourcekey,           ///WMS 单号  4
                info.Wmslinenumber,          ///行号  5
                (int)ProcessFlagConstants.Untreated,           ///  6
                1,                              ///7
                loginUser                       ///8
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