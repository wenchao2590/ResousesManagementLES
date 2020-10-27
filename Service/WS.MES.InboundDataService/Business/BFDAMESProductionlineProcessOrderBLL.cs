namespace WS.MES.InboundDataService
{
    using DM.LES;
    using DM.SYS;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    /// <summary>
    /// MES-LES-007	产线级工艺顺序
    /// </summary>
    public class BFDAMESProductionlineProcessOrderBLL
    {
        /// <summary>
        /// 操作用户
        /// </summary>
        private string loginUser = "WS.MES.InboundDataService";
        /// <summary>
        /// ConversionToCentreInfo
        /// </summary>
        /// <param name="mesProductionlineProcessOrderInfo"></param>
        /// <returns></returns>
        public MesProductionlineProcessOrderInfo ConversionToCentreInfo(BFDAMesProductionlineProcessOrderInfo mesProductionlineProcessOrderInfo)
        {
            MesProductionlineProcessOrderInfo productionlineProcessOrderInfo = new MesProductionlineProcessOrderInfo();
            productionlineProcessOrderInfo.Enterprise = mesProductionlineProcessOrderInfo.ENTERPRISE;
            productionlineProcessOrderInfo.SiteNo = mesProductionlineProcessOrderInfo.SITE_NO;
            productionlineProcessOrderInfo.AreaNo = mesProductionlineProcessOrderInfo.AREA_NO;
            productionlineProcessOrderInfo.Stationcode = mesProductionlineProcessOrderInfo.STATIONCODE;
            productionlineProcessOrderInfo.SeqNo = mesProductionlineProcessOrderInfo.SEQ_NO;
            productionlineProcessOrderInfo.Lineflag = mesProductionlineProcessOrderInfo.LINEFLAG;
            productionlineProcessOrderInfo.Liduiflag = mesProductionlineProcessOrderInfo.LiduiFlag;
            productionlineProcessOrderInfo.Guiduiflag = mesProductionlineProcessOrderInfo.GUIDUIFLAG;
            productionlineProcessOrderInfo.Pbsflag = mesProductionlineProcessOrderInfo.PBSFlag;
            productionlineProcessOrderInfo.Status = mesProductionlineProcessOrderInfo.Status;
            productionlineProcessOrderInfo.SendTime = mesProductionlineProcessOrderInfo.SEND_TIME;
            return productionlineProcessOrderInfo;
        }
        /// <summary>
        /// ConversionToCentreList
        /// </summary>
        /// <param name="mesProductionlineProcessOrderInfos"></param>
        /// <returns></returns>
        public List<MesProductionlineProcessOrderInfo> ConversionToCentreList(List<BFDAMesProductionlineProcessOrderInfo> mesProductionlineProcessOrderInfos)
        {
            List<MesProductionlineProcessOrderInfo> productionlineProcessOrderInfos = new List<MesProductionlineProcessOrderInfo>();
            foreach (BFDAMesProductionlineProcessOrderInfo interfaceInfo in mesProductionlineProcessOrderInfos)
            {
                productionlineProcessOrderInfos.Add(ConversionToCentreInfo(interfaceInfo));
            }
            return productionlineProcessOrderInfos;
        }
        /// <summary>
        /// GetKeyValue
        /// </summary>
        /// <param name="mesProductionlineProcessOrderInfo"></param>
        /// <returns></returns>
        public string GetKeyValue(BFDAMesProductionlineProcessOrderInfo mesProductionlineProcessOrderInfo)
        {
            return mesProductionlineProcessOrderInfo.AREA_NO + "|" + mesProductionlineProcessOrderInfo.STATIONCODE;
        }
        /// <summary>
        /// GetKeyValues
        /// </summary>
        /// <param name="mesProductionlineProcessOrderInfos"></param>
        /// <returns></returns>
        public string GetKeyValues(List<BFDAMesProductionlineProcessOrderInfo> mesProductionlineProcessOrderInfos)
        {
            return string.Join(",", mesProductionlineProcessOrderInfos.Select(d => d.AREA_NO + "|" + d.STATIONCODE).ToArray());
        }
        /// <summary>
        /// InsertInfoToCentreTable
        /// </summary>
        /// <param name="mesProductionlineProcessOrderInfo"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        public void InsertInfoToCentreTable(BFDAMesProductionlineProcessOrderInfo mesProductionlineProcessOrderInfo, Guid logFid, string logSql)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// InsertListToCentreTable
        /// </summary>
        /// <param name="mesProductionlineProcessOrderInfos"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        /// <returns></returns>
        public int InsertListToCentreTable(List<BFDAMesProductionlineProcessOrderInfo> mesProductionlineProcessOrderInfos, Guid logFid, string logSql)
        {
            List<MesProductionlineProcessOrderInfo> productionlineProcessOrderInfos = ConversionToCentreList(mesProductionlineProcessOrderInfos);
            StringBuilder sqlSb = new StringBuilder(logSql);
            foreach (var productionlineProcessOrderInfo in productionlineProcessOrderInfos)
            {
                sqlSb.AppendFormat("insert into [LES].[TI_IFM_MES_PRODUCTIONLINE_PROCESS_ORDER] ("
                + "[FID] ,"
                + "[LOG_FID] ,"
                + "[ENTERPRISE] ,"
                + "[SITE_NO] ,"
                + "[AREA_NO] ,"
                + "[STATIONCODE] ,"
                + "[LINEFLAG] ,"
                + "[LIDUIFLAG] ,"
                + "[GUIDUIFLAG] ,"
                + "[PBSFLAG] ,"
                + "[STATUS] ,"
                + "[SEND_TIME] ,"
                + "[PROCESS_FLAG] ,"
                + "[PROCESS_TIME] ,"
                + "[VALID_FLAG] ,"
                + "[CREATE_USER] ,"
                + "[CREATE_DATE],"
                + "[SEQ_NO] ) VALUES  ( "
                + "NEWID() ," //// FID - uniqueidentifier
                + "N'{0}' ," //// LOG_FID - uniqueidentifier
                + "N'{1}' ," //// ENTERPRISE - nvarchar(8)
                + "N'{2}' ," //// SITE_NO - nvarchar(8)
                + "N'{3}' ," //// AREA_NO - nvarchar(8)
                + "N'{4}' ," //// STATIONCODE - nchar(10)
                + "{5} ," //// LINEFLAG - int
                + "{6} ," //// LIDUIFLAG -int
                + "{7} ," //// GUIDUIFLAG int
                + "{8} ," //// PBSFLAG -  int
                + "N'{9}' ," //// STATUS - nchar(10)
                + "N'{10}' ," //// SEND_TIME - datetime
                + "{11} ," //// PROCESS_FLAG - int
                + "NULL ," //// PROCESS_TIME - datetime
                + "{12} ," //// VALID_FLAG - bit
                + "N'{13}' ," //// CREATE_USER - nvarchar(32)
                + "GETDATE(),"/// CREATE_USER - nvarchar(32)
                + "N'{14}'); " /// SEQ_NO         
                , logFid/// LOG_FID - uniqueidentifier,0
                , productionlineProcessOrderInfo.Enterprise/// ENTERPRISE - nvarchar(8),1
                , productionlineProcessOrderInfo.SiteNo/// SITE_NO - nvarchar(8),2
                , productionlineProcessOrderInfo.AreaNo/// AREA_NO - nvarchar(8),3
                , productionlineProcessOrderInfo.Stationcode/// STATIONCODE - nchar(10),4
                , productionlineProcessOrderInfo.Lineflag/// LINEFLAG - int,5
                , productionlineProcessOrderInfo.Liduiflag/// LIDUIFLAG -int,6
                , productionlineProcessOrderInfo.Guiduiflag/// GUIDUIFLAG int,7
                , productionlineProcessOrderInfo.Pbsflag/// PBSFLAG -  int,8
                , productionlineProcessOrderInfo.Status/// STATUS - nchar(10),9
                , productionlineProcessOrderInfo.SendTime/// SEND_TIME - datetime,10
                , (int)ProcessFlagConstants.Untreated/// PROCESS_FLAG - int,11
                , 1/// VALID_FLAG - bit,12
                , loginUser/// CREATE_USER - nvarchar(32),13
                , productionlineProcessOrderInfo.SeqNo/// SEQ_NO,14
                );
            }
            if (sqlSb.Length > 0)
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(sqlSb.ToString());
            return productionlineProcessOrderInfos.Count;
        }
    }
}