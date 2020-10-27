
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using BLL.LES;
    using DAL.LES;
    using DM.LES;
    using DM.SYS;
    using Infrustructure.Logging;
namespace WS.SAP.InboundDataService
{
    /// <summary>
    /// BFDASapWorkCalendarBLL
    /// </summary>
    public class BFDAInventoryCheckPlanBLL : IBusiness<SapInventoryCheckPlanInfo, BFDASapInventoryCheckPlanInfo>
    {
        

        #region Common
        /// <summary>
        /// 操作用户
        /// </summary>
        private string loginUser = "WS.SAP.InboundDataService";
        /// <summary>
        /// 是否写入日志
        /// </summary>
        string logFlag = ConfigurationManager.AppSettings["LogFlag"].ToLower();
        /// <summary>
        /// log日志名称
        /// </summary>
        string interfaceCode = "SyncPlantStructure";
        /// <summary>
        /// 时间格式
        /// </summary>
        private string dateTimeFormat = "yyyyMMdd";
        #endregion

        /// <summary>
        /// 接口数据转中间表集合
        /// </summary>
        /// <param name="bFDASapInventoryCheckPlanInfo"></param>
        /// <returns></returns>
        public SapInventoryCheckPlanInfo ConversionToCentreInfo(BFDASapInventoryCheckPlanInfo  bFDASapInventoryCheckPlanInfo)
        {
            SapInventoryCheckPlanInfo sapInventoryCheckPlanInfo = new SapInventoryCheckPlanInfo();
            sapInventoryCheckPlanInfo.Werks = bFDASapInventoryCheckPlanInfo.WERKS;  ///工厂 
            sapInventoryCheckPlanInfo.Iblnr = bFDASapInventoryCheckPlanInfo.IBLNR;  ///盘点凭证号 		
            sapInventoryCheckPlanInfo.Matnr = bFDASapInventoryCheckPlanInfo.MATNR;  ///物料
            decimal decCount = default(decimal);
            decimal.TryParse(bFDASapInventoryCheckPlanInfo.MENGE.Trim(), out decCount);             
            sapInventoryCheckPlanInfo.Menge = decCount;  ///数量 		
            sapInventoryCheckPlanInfo.Lgort = bFDASapInventoryCheckPlanInfo.LGORT;  ///库存地点 	
            sapInventoryCheckPlanInfo.Zldat = BFDASapCommonBLL.TryParseDatetime(bFDASapInventoryCheckPlanInfo.GIDAT, dateTimeFormat);  ///计划盘点日期
            sapInventoryCheckPlanInfo.ProcessFlag =(int) ProcessFlagConstants.Untreated;
            return sapInventoryCheckPlanInfo;
        }
        /// <summary>
        /// 批量接口数据转中间表集合
        /// </summary>
        /// <param name="interfaceList"></param>
        /// <returns></returns>
        public List<SapInventoryCheckPlanInfo> ConversionToCentreList(List<BFDASapInventoryCheckPlanInfo> interfaceList)
        {
            List<SapInventoryCheckPlanInfo> list = new List<SapInventoryCheckPlanInfo>();
            foreach (BFDASapInventoryCheckPlanInfo bFDASapInventoryCheckPlanInfo in interfaceList)
            {
                list.Add(ConversionToCentreInfo(bFDASapInventoryCheckPlanInfo));
            }
            return list;
        }
        /// <summary>
        /// 关键字
        /// </summary>
        /// <param name="bFDASapWorkCalendarInfo"></param>
        /// <returns></returns>
        public string GetKeyValue(BFDASapInventoryCheckPlanInfo bFDASapWorkCalendarInfo)
        {
            return bFDASapWorkCalendarInfo.IBLNR ;
        }
        /// <summary>
        /// 批量关键字
        /// </summary>
        /// <param name="bFDASapWorkCalendarInfos"></param>
        /// <returns></returns>
        public string GetKeyValues(List<BFDASapInventoryCheckPlanInfo> bFDASapWorkCalendarInfos)
        {
            return string.Join(",", bFDASapWorkCalendarInfos.Select(d => d.IBLNR ).ToArray());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bFDASapInventoryCheckPlanInfo"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        public void InsertInfoToCentreTable(BFDASapInventoryCheckPlanInfo bFDASapInventoryCheckPlanInfo, Guid logFid, string logSql)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 中间集合添加到数据库中间表
        /// </summary>
        /// <param name="bFDASapWorkCalendarInfos"></param>
        /// <param name="logFid"></param>
        /// <returns></returns>
        public int InsertListToCentreTable(List<BFDASapInventoryCheckPlanInfo> bFDASapWorkCalendarInfos, Guid logFid, string logSql)
        {
            List<SapInventoryCheckPlanInfo> sapWorkCalendarInfos = ConversionToCentreList(bFDASapWorkCalendarInfos);
            StringBuilder sqlSb = new StringBuilder(logSql);
            foreach (SapInventoryCheckPlanInfo sapWorkCalendarInfo in sapWorkCalendarInfos)
            {
                sqlSb.Append(   SapInventoryCheckPlanDAL.GetInsertSql(sapWorkCalendarInfo));
            }
            if (sqlSb.Length > 0)
            {
                Log.WriteLogToFile(logFlag, interfaceCode + "-EcecuteSQL:|" + sqlSb.ToString(), AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMdd"));
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(sqlSb.ToString());

            }
            return sapWorkCalendarInfos.Count;
        }
    }
}