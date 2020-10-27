using BLL.LES;
using DM.LES;
using DM.SYS;
using Infrustructure.Logging;
using Infrustructure.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace WS.SAP.InboundDataService
{
    /// <summary>
    /// SAP-LES-013	断点更改单
    /// </summary>
    public class BFDASapBreakpointChangesBLL : IBusiness<SapBreakpointChangesInfo, BFDASapBreakpointChangesInfo>
    {




        #region Common
        string[] dateTimeFormat = { "yyyyMMdd" };
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
        string interfaceCode = " SAP-LES-013 断点更改单";
        #endregion

        public SapBreakpointChangesInfo ConversionToCentreInfo(BFDASapBreakpointChangesInfo bfdaInfo)
        {
            SapBreakpointChangesInfo sapInfo = new SapBreakpointChangesInfo();

            if ( string.IsNullOrEmpty(bfdaInfo.WERKS) ||  string.IsNullOrEmpty(bfdaInfo.ZCJ)|| string.IsNullOrEmpty(bfdaInfo.AENNR) || string.IsNullOrEmpty(bfdaInfo.MATNR) || string.IsNullOrEmpty(bfdaInfo.CHANGE_FLAG) )
                throw new Exception("MC:0x00000470");///工厂,车间,变更单号,收货方,供应商,物料号都不能为空

            sapInfo.Aennr = bfdaInfo.AENNR;               ///变更单号 	CHAR
            sapInfo.Matnr = bfdaInfo.MATNR;               ///父物料 	CHAR(18)
            sapInfo.ChangeFlag = bfdaInfo.CHANGE_FLAG;    ///更改标识 	NVARCHAR(30)
            sapInfo.Oidnrk = bfdaInfo.OIDNRK;             ///旧物料 	CHAR(18)
            sapInfo.Nidnrk = bfdaInfo.NIDNRK;             ///新物料 	CHAR(18)
            sapInfo.Menge = bfdaInfo.MENGE;               ///更改数量 	decimal(18," 3)
            sapInfo.Ebort = bfdaInfo.EBORT;               ///工位 	CHAR
            sapInfo.Datuv = BFDASapCommonBLL.TryParseDatetime(bfdaInfo.DATUV);                     ///生效日期 	DATS
            sapInfo.Datub = BFDASapCommonBLL.TryParseDatetime(bfdaInfo.DATUB);                    ///有效截止日期	Nvarchar(10)
            sapInfo.Zcj = bfdaInfo.ZCJ;                  ///车间	Nvarchar(4)
            sapInfo.Ktsch = bfdaInfo.KTSCH;              ///工位	Nvarchar(7)
            sapInfo.Werks = bfdaInfo.WERKS;              ///工厂	Nvarchar(4)
            sapInfo.Sortf = bfdaInfo.SORTF;              ///排序字符串	Nvarchar(10)     
            return sapInfo;
        }

        /// <summary>
        /// 验证日期
        /// </summary>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        public DateTime TryParsDatetime(string dateStr)
        {
            DateTime Parsdate = default(DateTime); 
            string[] format = { "yyyyMMdd" };
            DateTime.TryParseExact(dateStr,
                                   format,
                                   System.Globalization.CultureInfo.InvariantCulture,
                                   System.Globalization.DateTimeStyles.None,
                                   out Parsdate);
            ///如果Out日期格式是默认最小
            if (Parsdate == DateTime.MinValue)
            {
                Parsdate = Parsdate.AddYears(1900);
            }
            return Parsdate;
        }
        public List<SapBreakpointChangesInfo> ConversionToCentreList(List<BFDASapBreakpointChangesInfo> interfaceList)
        {
            List<SapBreakpointChangesInfo> list = new List<SapBreakpointChangesInfo>();
            foreach (BFDASapBreakpointChangesInfo interfaceInfo in interfaceList)
            {
                list.Add(ConversionToCentreInfo(interfaceInfo));
            }
            return list;
        }

        public string GetKeyValue(BFDASapBreakpointChangesInfo interfaceInfo)
        {
            throw new NotImplementedException();
        }

        public string GetKeyValues(List<BFDASapBreakpointChangesInfo> interfaceList)
        {
            return string.Join(",", interfaceList.Select(d => d.AENNR ).ToArray());
        }

        public void InsertInfoToCentreTable(BFDASapBreakpointChangesInfo interfaceInfo, Guid logFid, string logSql)
        {
            throw new NotImplementedException();
        }

        public int InsertListToCentreTable(List<BFDASapBreakpointChangesInfo> interfaceList, Guid logFid, string logSql)
        {
            List<SapBreakpointChangesInfo> sapMaterialReservations = ConversionToCentreList(interfaceList);            
            StringBuilder sqlSb = new StringBuilder(logSql);

            foreach (var info in sapMaterialReservations)
            {
                sqlSb.AppendFormat(" insert into  [LES].[TI_IFM_SAP_BREAKPOINT_CHANGES]("
                 +" [FID] ,"
                 +" [LOG_FID] ,"
                 +" [WERKS] ,"
                 +" [ZCJ] ,"
                 +" [KTSCH] ,"
                 +" [AENNR] ,"
                 +" [MATNR] ,"
                 +" [CHANGE_FLAG] ,"
                 +" [OIDNRK] ,"
                 +" [NIDNRK] ,"
                 +" [MENGE] ,"
                 +" [EBORT] ,"
		         +" [SORTF] ,"
                 +" [DATUV] ,"
                 +" [DATUB] , "    
                 +" [PROCESS_FLAG] ,"
		         +" [VALID_FLAG] ,"
		         +" [CREATE_USER] ,"
                 +" [PROCESS_TIME] ,"
                 +" [CREATE_DATE] )  values  ("
                 +" NEWID() ," // FID - uniqueidentifier
                 + "N'{0}' ,"// LOG_FID - uniqueidentifier
                 +"N'{1}' , "// WERKS - nvarchar(4)
                 +"N'{2}' , "// ZCJ - nvarchar(8)
                 +"N'{3}' , "// KTSCH - nvarchar(8)
                 +"N'{4}' , "// AENNR - nvarchar(16)
                 +"N'{5}' , "// MATNR - nvarchar(32)
                 +"N'{6}' , "// CHANGE_FLAG - nvarchar(32)
                 +"N'{7}' , "// OIDNRK - nvarchar(32)
                 +"N'{8}' , "// NIDNRK - nvarchar(32)
                 +"{9} , "// MENGE - decimal(18, 3)
                 +"N'{10}' , "// EBORT - nvarchar(8)
		         +"N'{11}' , "// SORTF - nvarchar(16)
                 +"N'{12}', "// DATUV - datetime
                 +"N'{13}' , "// DATUB - datetime         
                 +"{14} , "// PROCESS_FLAG - int
		         +"{15} , "// VALID_FLAG - bit
                 +"N'{16}' , "// CREATE_USER - nvarchar(32)
                 +"GETDATE() , "// PROCESS_TIME - datetime         
                 +"GETDATE()); "//	CREATE_DATE]-datetime        
                , logFid, info.Werks,info.Zcj, info.Ktsch, info.Aennr, info.Matnr, info.ChangeFlag, info.Oidnrk, info.Nidnrk, info.Menge, info.Ebort, 
                info.Sortf, info.Datuv,info.Datub, (int)ProcessFlagConstants.Untreated,1, loginUser);
            }

            if (sqlSb.Length > 0)
            {
                Log.WriteLogToFile(logFlag, sqlSb.ToString(), AppDomain.CurrentDomain.BaseDirectory + @"\Log_Script\", DateTime.Now.ToString("yyyyMMddHHmm"));
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(sqlSb.ToString());
            }
            return sapMaterialReservations.Count;
        }
    }
}