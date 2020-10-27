namespace WS.SAP.InboundDataService
{
    using DM.LES;
    using DM.SYS;
    using Infrustructure.Logging;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using System.Text;
    /// <summary>
    /// SAP-LES-016  物料预留单
    /// </summary>
    public class BFDASAPMaterialReservationBLL : IBusiness<SapMaterialReservationInfo, BFDASapMaterialReservationInfo>
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
        string interfaceCode = "SAP-LES-016  物料预留单";
        #endregion


        /// <summary>
        /// ConversionToCentreInfo
        /// </summary>
        /// <param name="sapMaterialReservationInfo"></param>
        /// <returns></returns>
        public SapMaterialReservationInfo ConversionToCentreInfo(BFDASapMaterialReservationInfo sapMaterialReservationInfo)
        {
            SapMaterialReservationInfo materialReservationInfo = new SapMaterialReservationInfo();
            //if (string.IsNullOrEmpty(sapMaterialReservationInfo.RSPOS) || string.IsNullOrEmpty(sapMaterialReservationInfo.LGORT) || string.IsNullOrEmpty(sapMaterialReservationInfo.LIFNR) || string.IsNullOrEmpty(sapMaterialReservationInfo.MATNR))
            //    throw new Exception("MC:0x00000470");///预留号,收货方,供应商,物料号都不能为空

            materialReservationInfo.Rsnum = sapMaterialReservationInfo.RSNUM;///预留号          
            materialReservationInfo.Rspos = sapMaterialReservationInfo.RSPOS;///	预留行号
            materialReservationInfo.Lgort = sapMaterialReservationInfo.LGORT;///	收货方
            materialReservationInfo.Lifnr = sapMaterialReservationInfo.LIFNR;///	供应商
            materialReservationInfo.Matnr = sapMaterialReservationInfo.MATNR;///	物料
            materialReservationInfo.Ebeln = sapMaterialReservationInfo.EBELN;///	采购订单
            if (!int.TryParse(sapMaterialReservationInfo.EBELP, out int intEbelp))
                throw new Exception("MC:0x00000398");///采购订单行项目号错误
            materialReservationInfo.Ebelp = intEbelp;///采购订单行项目号
            if (!decimal.TryParse(sapMaterialReservationInfo.MENGE.Trim(), out decimal intMenge))
                throw new Exception("MC:0x00000395");///物料数量格式错误
            materialReservationInfo.Menge = intMenge; ///数量            
            materialReservationInfo.Bdter = BFDASapCommonBLL.TryParseDatetime(sapMaterialReservationInfo.BDTER);///需求日期
            return materialReservationInfo;
        }
        /// <summary>
        /// ConversionToCentreList
        /// </summary>
        /// <param name="sapMaterialReservationInfos"></param>
        /// <returns></returns>
        public List<SapMaterialReservationInfo> ConversionToCentreList(List<BFDASapMaterialReservationInfo> sapMaterialReservationInfos)
        {
            List<SapMaterialReservationInfo> list = new List<SapMaterialReservationInfo>();
            foreach (BFDASapMaterialReservationInfo interfaceInfo in sapMaterialReservationInfos)
            {
                list.Add(ConversionToCentreInfo(interfaceInfo));
            }
            return list;
        }
        /// <summary>
        /// GetKeyValue
        /// </summary>
        /// <param name="sapMaterialReservationInfo"></param>
        /// <returns></returns>
        public string GetKeyValue(BFDASapMaterialReservationInfo sapMaterialReservationInfo)
        {
            return sapMaterialReservationInfo.EBELN + "|" + sapMaterialReservationInfo.EBELN + "|" + sapMaterialReservationInfo.MATNR + "|" + sapMaterialReservationInfo.RSNUM + "|" + sapMaterialReservationInfo.RSPOS;
        }
        /// <summary>
        /// GetKeyValues
        /// </summary>
        /// <param name="sapMaterialReservationInfos"></param>
        /// <returns></returns>
        public string GetKeyValues(List<BFDASapMaterialReservationInfo> sapMaterialReservationInfos)
        {
            return string.Join(",", sapMaterialReservationInfos.Select(d => d.EBELN + "|" + d.EBELN + "|" + d.MATNR + "|" + d.RSNUM + "|" + d.RSPOS).ToArray());
        }
        /// <summary>
        /// InsertInfoToCentreTable
        /// </summary>
        /// <param name="sapMaterialReservationInfo"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        public void InsertInfoToCentreTable(BFDASapMaterialReservationInfo sapMaterialReservationInfo, Guid logFid, string logSql)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 物料预留单接收
        /// </summary>
        /// <param name="sapMaterialReservationInfos"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        /// <returns></returns>
        public int InsertListToCentreTable(List<BFDASapMaterialReservationInfo> sapMaterialReservationInfos, Guid logFid, string logSql)
        {
            List<SapMaterialReservationInfo> sapMaterialReservations = ConversionToCentreList(sapMaterialReservationInfos);
            StringBuilder sqlSb = new StringBuilder(logSql);
            foreach (var info in sapMaterialReservations)
            {
                sqlSb.AppendFormat("insert into [LES].[TI_IFM_SAP_MATERIAL_RESERVATION]( "
                  + "[FID] ,"
                  + "[LOG_FID] ,"
                  + "[RSNUM] ,"
                  + "[RSPOS] ,"
                  + "[LGORT] ,"
                  + "[LIFNR] ,"
                  + "[MATNR] ,"
                  + "[EBELN] ,"
                  + "[EBELP] ,"
                  + "[MENGE] ,"
                  + "[BDTER] ,"
                  + "[PROCESS_FLAG] ,"
                  + "[PROCESS_TIME] ,"
                  + "[VALID_FLAG] ,"
                  + "[CREATE_USER] ,"
                  + "[CREATE_DATE]) VALUES  ("
                  + "NEWID() ,"     //// FID - uniqueidentifier
                  + "N'{0}' ,"      //// LOG_FID - uniqueidentifier
                  + "{1} ,"         //// RSNUM - int
                  + "{2} ,"         //// RSPOS - int
                  + "N'{3}' ,"      //// LGORT - nvarchar(4)
                  + "N'{4}' ,"      //// LIFNR - nvarchar(36)
                  + "N'{5}' ,"      //// MATNR - nvarchar(36)
                  + "N'{6}' ,"      //// EBELN - nvarchar(12)
                  + "{7} ,"         //// EBELP - int
                  + "{8} ,"         //// MENGE - int
                  + "N'{9}' ,"        //// BDTER - datetime
                  + "{10},"         //// PROCESS_FLAG - int
                  + "NULL ,"   //// PROCESS_TIME - datetime
                  + "{11} ,"        //// VALID_FLAG - bit
                  + "N'{12}' ,"     //// CREATE_USER - nvarchar(32)
                  + "GETDATE()); "  /// CREATE_DATE - datetime   
                , logFid///LOG_FID - uniqueidentifier 0
                , info.Rsnum///RSNUM - int 1
                , info.Rspos///RSPOS - int 2
                , info.Lgort///LGORT - nvarchar(4) 3
                , info.Lifnr///LIFNR - nvarchar(36) 4
                , info.Matnr///MATNR - nvarchar(36) 5
                , info.Ebeln/// EBELN - nvarchar(12) 6
                , info.Ebelp///EBELP - int 7
                , info.Menge/// MENGE - int 8
                , info.Bdter/// BDTER - datetime 9
                , (int)ProcessFlagConstants.Untreated/// PROCESS_FLAG - int 10
                , 1/// VALID_FLAG - bit 11
                , loginUser/// CREATE_USER - nvarchar(32) 12
                );
            }

            if (sqlSb.Length > 0)
            {
                Log.WriteLogToFile(logFlag,sqlSb.ToString(), AppDomain.CurrentDomain.BaseDirectory + @"\Log_Script\", DateTime.Now.ToString("yyyyMMddHHmm"));
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(sqlSb.ToString());
            }            
            return sapMaterialReservations.Count;
        }
    }
}