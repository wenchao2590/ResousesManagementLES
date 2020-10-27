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
    /// WMS-LES-013	在途转可用库存
    /// </summary>
    public class BFDAVmiTransitToInventoryBLL : IBusiness<WmsVmiTransitInventoryInfo, BFDAVmiTransitToInventoryInfo>
    {
        /// <summary>
        /// 操作用户
        /// </summary>
        private string loginUser = "WS.VMI.InboundDataService";

        /// <summary>
        /// 将报文表转换成中间表
        /// </summary>
        /// <param name="bFDAVmiTransitToInventoryInfo"></param>
        /// <returns></returns>
        public WmsVmiTransitInventoryInfo ConversionToCentreInfo(BFDAVmiTransitToInventoryInfo bFDAVmiTransitToInventoryInfo)
        {
            WmsVmiTransitInventoryInfo TransitInventoryInfo = new WmsVmiTransitInventoryInfo();
            TransitInventoryInfo.Transferkey = bFDAVmiTransitToInventoryInfo.TransferKey;  ///WMS 调整单号	 
            TransitInventoryInfo.Transferlinenumber = bFDAVmiTransitToInventoryInfo.TransferLineNumber;   ///WMS 调整单行号
            TransitInventoryInfo.Vmicode = bFDAVmiTransitToInventoryInfo.VmiCode;  ///VMI 仓库代码	 
            TransitInventoryInfo.Fromstorerkey = bFDAVmiTransitToInventoryInfo.FromstorerKey;  ///供应商代码  	
            TransitInventoryInfo.Fromsku = bFDAVmiTransitToInventoryInfo.Fromsku;  ///物料代码	  

            decimal qty = 0;
            decimal.TryParse(bFDAVmiTransitToInventoryInfo.Toqty, out qty);

            TransitInventoryInfo.Toqty = qty;    ///数量 	
            TransitInventoryInfo.Fromlot07 = bFDAVmiTransitToInventoryInfo.Fromlot07;  ///源锁库状态  
            TransitInventoryInfo.Tolot07 = bFDAVmiTransitToInventoryInfo.Tolot07;  ///目标锁库状态	
            return TransitInventoryInfo;
        }

        /// <summary>
        /// 将报文表集合转换成中间表集合
        /// </summary>
        /// <param name="bFDAVmiTransitToInventoryInfos"></param>
        /// <returns></returns>
        public List<WmsVmiTransitInventoryInfo> ConversionToCentreList(List<BFDAVmiTransitToInventoryInfo> bFDAVmiTransitToInventoryInfos)
        {
            List<WmsVmiTransitInventoryInfo> list = new List<WmsVmiTransitInventoryInfo>();
            foreach (BFDAVmiTransitToInventoryInfo bFDAVmiTransitToInventoryInfo in bFDAVmiTransitToInventoryInfos)
            {
                list.Add(ConversionToCentreInfo(bFDAVmiTransitToInventoryInfo));
            }
            return list;
        }
        /// <summary>
        /// 获取关键词
        /// </summary>
        /// <param name="bFDAVmiTransitToInventoryInfo"></param>
        /// <returns></returns>
        public string GetKeyValue(BFDAVmiTransitToInventoryInfo  bFDAVmiTransitToInventoryInfo)
        {
            return bFDAVmiTransitToInventoryInfo.FromstorerKey;
        }
        /// <summary>
        /// 获取所有关键词集合
        /// </summary>
        /// <param name="bFDAVmiTransitToInventoryInfos"></param>
        /// <returns></returns>
        public string GetKeyValues(List<BFDAVmiTransitToInventoryInfo> bFDAVmiTransitToInventoryInfos)
        {
            return string.Join(",", bFDAVmiTransitToInventoryInfos.Select(d => d.FromstorerKey).ToArray());
        }

        public void InsertInfoToCentreTable(BFDAVmiTransitToInventoryInfo bFDAVmiTransitToInventoryInfo, Guid logFid, string logSql)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 插入集合信息
        /// </summary>
        /// <param name="bFDAVmiTransitToInventoryInfos"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        /// <returns></returns>
        public int InsertListToCentreTable(List<BFDAVmiTransitToInventoryInfo>  bFDAVmiTransitToInventoryInfos, Guid logFid, string logSql)
        {
            List<WmsVmiTransitInventoryInfo> vmiWmsBarcodes = ConversionToCentreList(bFDAVmiTransitToInventoryInfos);
            StringBuilder sqlSb = new StringBuilder(logSql);
            sqlSb.AppendLine();
            foreach (var info in vmiWmsBarcodes)
            {
                sqlSb.AppendFormat(" INSERT INTO [LES].[TI_IFM_WMS_VMI_TRANSIT_INVENTORY] ( " 
                    +"[FID]," 
                    +"[LOG_FID],"              /// 0
                    +"[TRANSFERKEY],"          /// 1
                    +"[TRANSFERLINENUMBER],"   /// 2
                    +"[VMICODE],"              /// 3
                    +"[FROMSTORERKEY],"        /// 4
                    +"[FROMSKU],"              /// 5
                    +"[TOQTY],"                /// 6
                    +"[FROMLOT07],"            /// 7
                    +"[TOLOT07],"             /// 8
                    +"[VALID_FLAG],"           /// 9
                    +"[PROCESS_FLAG],"         /// 10
                    +"[CREATE_USER],"          /// 11
                    +"[PROCESS_TIME],"         /// 12
                    +"[CREATE_DATE]) values "  /// 13
                    +" ( NEWID(), N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',{6},N'{7}',N'{8}',{9},{10},N'{11}',GETDATE(),GETDATE() ); ",
                logFid,
                info.Transferkey,                ///WMS 调整单号 1	 
                info.Transferlinenumber,         ///WMS 调整单行号2
                info.Vmicode,                    ///VMI 仓库代码3	
                info.Fromstorerkey,              ///供应商代码  4	
                info.Fromsku,                    ///物料代码 5  
                info.Toqty,                      ///数量  6
                info.Fromlot07,                  ///源锁库状态  7	
                info.Tolot07,                    ///目标锁库状态 8	
                1,                              /// 9
                (int)ProcessFlagConstants.Untreated,           ///  10
                loginUser                       ///11
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