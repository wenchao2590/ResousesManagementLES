namespace WS.MES.InboundDataService
{
    using DM.LES;
    using DM.SYS;
    using Infrustructure.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    /// <summary>
    /// MES-LES-006	信息点基础数据
    /// </summary>
    public class BFDAMESInformationPointBasicBLL
    {
        /// <summary>
        /// 操作用户
        /// </summary>
        private string loginUser = "WS.MES.InboundDataService";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mesInformationPointBasicInfo"></param>
        /// <returns></returns>
        public MesInformationPointBasicInfo ConversionToCentreInfo(BFDAMesInformationPointBasicInfo mesInformationPointBasicInfo)
        {
            MesInformationPointBasicInfo informationPointBasicInfo = new MesInformationPointBasicInfo();
            informationPointBasicInfo.Enterprise = mesInformationPointBasicInfo.ENTERPRISE;
            informationPointBasicInfo.SiteNo = mesInformationPointBasicInfo.SITE_NO;
            informationPointBasicInfo.AreaNo = mesInformationPointBasicInfo.AREA_NO;
            informationPointBasicInfo.Stationcode = mesInformationPointBasicInfo.STATION_CODE;
            informationPointBasicInfo.Status = mesInformationPointBasicInfo.STATUS;
            informationPointBasicInfo.SendTime = mesInformationPointBasicInfo.SEND_TIME.GetValueOrDefault();
            return informationPointBasicInfo;
        }
        /// <summary>
        /// ConversionToCentreList
        /// </summary>
        /// <param name="mesInformationPointBasicInfos"></param>
        /// <returns></returns>
        public List<MesInformationPointBasicInfo> ConversionToCentreList(List<BFDAMesInformationPointBasicInfo> mesInformationPointBasicInfos)
        {
            List<MesInformationPointBasicInfo> informationPointBasicInfos = new List<MesInformationPointBasicInfo>();
            foreach (BFDAMesInformationPointBasicInfo mesInformationPointBasicInfo in mesInformationPointBasicInfos)
            {
                informationPointBasicInfos.Add(ConversionToCentreInfo(mesInformationPointBasicInfo));
            }
            return informationPointBasicInfos;
        }
        /// <summary>
        /// GetKeyValue
        /// </summary>
        /// <param name="mesInformationPointBasicInfo"></param>
        /// <returns></returns>
        public string GetKeyValue(BFDAMesInformationPointBasicInfo mesInformationPointBasicInfo)
        {
            return mesInformationPointBasicInfo.AREA_NO ;
        }
        /// <summary>
        /// GetKeyValues
        /// </summary>
        /// <param name="mesInformationPointBasicInfos"></param>
        /// <returns></returns>
        public string GetKeyValues(List<BFDAMesInformationPointBasicInfo> mesInformationPointBasicInfos)
        {
            return string.Join(",", mesInformationPointBasicInfos.Select(d => d.AREA_NO ).ToArray());
        }
        /// <summary>
        /// InsertInfoToCentreTable
        /// </summary>
        /// <param name="mesInformationPointBasicInfo"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        public void InsertInfoToCentreTable(BFDAMesInformationPointBasicInfo mesInformationPointBasicInfo, Guid logFid, string logSql)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// InsertListToCentreTable
        /// </summary>
        /// <param name="mesInformationPointBasicInfos"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        /// <returns></returns>
        public int InsertListToCentreTable(List<BFDAMesInformationPointBasicInfo> mesInformationPointBasicInfos, Guid logFid, string logSql)
        {
            List<MesInformationPointBasicInfo> informationPointBasicInfos = ConversionToCentreList(mesInformationPointBasicInfos);
            StringBuilder sqlSb = new StringBuilder(logSql);
            foreach (var informationPointBasicInfo in informationPointBasicInfos)
            {
                sqlSb.AppendFormat("insert into [LES].[TI_IFM_MES_INFORMATION_POINT_BASIC] ( "
                + "[FID] ,"
                + "[LOG_FID] ,"
                + "[ENTERPRISE] ,"
                + "[SITE_NO] ,"
                + "[AREA_NO] ,"
                + "[STATIONCODE] ,"
                + "[STATUS] ,"
                + "[SEND_TIME] ,"
                + "[PROCESS_FLAG] ,"
                + "[PROCESS_TIME] ,"
                + "[VALID_FLAG] ,"
                + "[CREATE_USER] ,"
                + "[CREATE_DATE]  )VALUES  ( "
                + "NEWID() ,"      /// FID - uniqueidentifier
                + "N'{0}' ,"      /// LOG_FID - uniqueidentifier
                + "N'{1}' ,"       /// ENTERPRISE - nvarchar(8)
                + "N'{2}' ,"       /// SITE_NO - nvarchar(8)
                + "N'{3}' ,"       /// AREA_NO - nvarchar(8)
                + "N'{4}' , "      /// STATIONCODE - nvarchar(256)
                + "N'{5}' , "      /// STATUS - nvarchar(4)
                + "N'{6}' ," /// SEND_TIME - datetime
                + "{7},"         /// PROCESS_FLAG - int
                + "NULL ," /// PROCESS_TIME - datetime
                + "{8} ,"      /// VALID_FLAG - bit
                + "N'{9}' ,"       /// CREATE_USER - nvarchar(32)
                + "GETDATE() ); "/// CREATE_DATE - datetime
                , logFid/// LOG_FID - uniqueidentifier,0
                , informationPointBasicInfo.Enterprise/// ENTERPRISE - nvarchar(8),1
                , informationPointBasicInfo.SiteNo /// SITE_NO - nvarchar(8),2
                , informationPointBasicInfo.AreaNo/// AREA_NO - nvarchar(8),3
                , informationPointBasicInfo.Stationcode/// STATIONCODE - nvarchar(256),4
                , informationPointBasicInfo.Status/// STATUS - nvarchar(4),5
                , informationPointBasicInfo.SendTime/// SEND_TIME - datetime,6
                , (int)ProcessFlagConstants.Untreated/// PROCESS_FLAG - int,7
                , 1/// VALID_FLAG - bit,8
                , loginUser/// CREATE_USER - nvarchar(32),9
                );
            }
            if (sqlSb.Length > 0)
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(sqlSb.ToString());
            return informationPointBasicInfos.Count;
        }
    }
}