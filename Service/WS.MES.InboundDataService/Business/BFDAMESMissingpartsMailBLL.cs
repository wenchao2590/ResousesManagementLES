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
    ///  /// MES-LES-004	缺件明细主数据
    /// </summary>
    public class BFDAMESMissingpartsMailBLL
    {
        /// <summary>
        /// 操作用户
        /// </summary>
        private string loginUser = "WS.MES.InboundDataService";
        string dateTimeFormat = "YYYY-MM-DD hh:mm:ss";


        public MesMissingpartsMainInfo ConversionToCentreInfo(BFDAMesMissingpartsMainInfo bfdaInfo)
        {
            MesMissingpartsMainInfo mainInfo = new MesMissingpartsMainInfo();

            mainInfo.Enterprise = bfdaInfo.ENTERPRISE;
            mainInfo.SiteNo = bfdaInfo.SITE_NO;
            mainInfo.AreaNo = bfdaInfo.AREA_NO;
            mainInfo.DmsNo = bfdaInfo.DMS_NO;
            mainInfo.SendTime = bfdaInfo.SEND_TIME;
            mainInfo.DTLS = new List<MesMissingpartsDetailInfo>();
            if (bfdaInfo.DTLS.Count > 0)
            {
                foreach (BFDAMesMissingpartsDetailInfo item in bfdaInfo.DTLS)
                {
                    MesMissingpartsDetailInfo detailinfo = new MesMissingpartsDetailInfo();
                    detailinfo.DmsNo = item.DMS_NO;
                    detailinfo.Stationcode = item.STATIONCODE;
                    detailinfo.Matercode = item.MATERCODE;
                    detailinfo.Qty = item.QTY.GetValueOrDefault();
                    mainInfo.DTLS.Add(detailinfo);
                }
            }
            return mainInfo;
        }
        public List<MesMissingpartsMainInfo> ConversionToCentreList(List<BFDAMesMissingpartsMainInfo> interfaceList)
        {
            List<MesMissingpartsMainInfo> list = new List<MesMissingpartsMainInfo>();
            foreach (BFDAMesMissingpartsMainInfo interfaceInfo in interfaceList)
            {
                list.Add(ConversionToCentreInfo(interfaceInfo));
            }
            return list;
        }

        public string GetKeyValue(BFDAMesMissingpartsMainInfo interfaceInfo)
        {
            throw new NotImplementedException();
        }

        public string GetKeyValues(List<BFDAMesMissingpartsMainInfo> interfaceList)
        {
            return string.Join(",", interfaceList.Select(d => d.DMS_NO).ToArray());
        }

        public void InsertInfoToCentreTable(BFDAMesMissingpartsMainInfo interfaceInfo, Guid logFid, string logSql)
        {
            throw new NotImplementedException();
        }

        public int InsertListToCentreTable(List<BFDAMesMissingpartsMainInfo> interfaceList, Guid logFid, string logSql)
        {
            List<MesMissingpartsMainInfo> mesInfos = ConversionToCentreList(interfaceList);
            StringBuilder sqlSb = new StringBuilder(logSql);
            sqlSb.AppendLine();

            foreach (var info in mesInfos)
            {
                sqlSb.AppendFormat("INSERT INTO  [LES].[TI_IFM_MES_MISSINGPARTS_MAIN] ("
               + "[FID] , "
               + "[LOG_FID] , "
               + "[ENTERPRISE] , "
               + "[SITE_NO] , "
               + "[AREA_NO] , "
               + "[DMS_NO] , "
               + "[SEND_TIME] , "
               + "[VALID_FLAG] , "
               + "[PROCESS_FLAG] , "
               + "[PROCESS_TIME] , "
               + "[CREATE_USER] , "
               + "[CREATE_DATE] ) VALUES  (    "     
               + "NEWID() , "       	/// FID - uniqueidentifier
               + "N'{0}' , "       	/// LOG_FID - uniqueidentifier
               + "N'{1}' , "       		/// ENTERPRISE - nvarchar(8)
               + "N'{2}' , "       		/// SITE_NO - nvarchar(8)
               + "N'{3}' , "       		/// AREA_NO - nvarchar(8)
               + "N'{4}' , "       		/// DMS_NO - nvarchar(32)
               + "N'{5}' , "      /// SEND_TIME - datetime
               + "{6} , "       	/// VALID_FLAG - bit
               + "{7}, "       		/// PROCESS_FLAG - int
               + "GETDATE() , "      /// PROCESS_TIME - datetime
               + "N'{8}' , "       		/// CREATE_USER - nvarchar(32)
               + "GETDATE() );  "      /// CREATE_DATE - datetime   
                , logFid, info.Enterprise, info.SiteNo, info.AreaNo, info.DmsNo, info.SendTime, 1,  (int)ProcessFlagConstants.Untreated,  loginUser);
                sqlSb.AppendLine();

                if (info.DTLS.Count() > 0)
                {
                    foreach (var item in info.DTLS)
                    {
                        sqlSb.AppendFormat("INSERT INTO [LES].[TI_IFM_MES_MISSINGPARTS_DETAIL]("
                        + "[FID] ,"
                        + "[LOG_FID] ,"
                        + "[DMS_NO] ,"
                        + "[STATIONCODE] ,"
                        + "[MATERCODE] ,"
                        + "[QTY] ,"
                        + "[VALID_FLAG] ,"
                        + "[PROCESS_FLAG] ,"
                        + "[PROCESS_TIME] ,"
                        + "[CREATE_USER] ,"
                        + "[CREATE_DATE] ) values ( "
                        + "NEWID() ,"        //// FID - uniqueidentifier
                        + "N'{0}' ,"        //// LOG_FID - uniqueidentifier
                        + "N'{1}' ,"        //// DMS_NO - nvarchar(32)
                        + "N'{2}' ,"        //// STATIONCODE - nvarchar(256)
                        + "N'{3}' ,"        //// MATERCODE - nvarchar(256)
                        + "{4} ,"           //// QTY - decimal(18," 3)
                        + "{5} ,"           //// VALID_FLAG - bit
                        + "{6} ,"          //// PROCESS_FLAG - int
                        + "GETDATE() ,"     //// PROCESS_TIME - datetime
                        + "N'{7}' ,"           //// CREATE_USER - nvarchar(32)
                        + "GETDATE() );  "  //// CREATE_DATE - datetim     
                        , logFid, item.DmsNo, item.Stationcode, item.Matercode, item.Qty,   1, (int)ProcessFlagConstants.Untreated, loginUser);
                        sqlSb.AppendLine();
                    }
                }

            }

            if (sqlSb.Length > 0)
            {
                Log.WriteLogToFile(sqlSb.ToString(), AppDomain.CurrentDomain.BaseDirectory + @"\SQL-Log\", DateTime.Now.ToString("yyyyMMddHHmm"));
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(sqlSb.ToString());
            }
            return mesInfos.Count;
        }
    }
}