using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using BLL.LES;
using DM.LES;
using DM.SYS;
using Infrustructure.Logging;

namespace WS.SAP.InboundDataService
{
    public class BFDASapSupplierQuotaBLL : IBusiness<SapSupplierQuotaInfo, BFDASupplierQuotaInfo>
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
        string interfaceCode = "SAP-LES-003 SAP供应商配额数据接收";
        #endregion

        /// <summary>
        /// 接口数据转中间表集合
        /// </summary>
        /// <param name="interfaceInfo"></param>
        /// <returns></returns>
        public SapSupplierQuotaInfo ConversionToCentreInfo(BFDASupplierQuotaInfo interfaceInfo)
        {
            SapSupplierQuotaInfo sapSupplierQuotaInfo = new SapSupplierQuotaInfo();
            if (string.IsNullOrEmpty(interfaceInfo.Werks)|| string.IsNullOrEmpty(interfaceInfo.Lifnr) || string.IsNullOrEmpty(interfaceInfo.Name1) || string.IsNullOrEmpty(interfaceInfo.Matnr))
                throw new Exception("MC:0x00000467");///工厂,供应商编号,供应商名称,物料号都不能为空
            sapSupplierQuotaInfo.Werks = interfaceInfo.Werks;///WERKS,2,工厂

            sapSupplierQuotaInfo.Lifnr = interfaceInfo.Lifnr;///LIFNR,3,供应商
            sapSupplierQuotaInfo.SupplierName = interfaceInfo.Name1;///SUPPLIER_NAME,4,供应商名称 过滤单引号!
            sapSupplierQuotaInfo.PartNo = interfaceInfo.Matnr;///PART_NO,5,物料编号
            sapSupplierQuotaInfo.Qtype = interfaceInfo.Qtype;///QTYPE,6,平台系数
            sapSupplierQuotaInfo.IDate = BFDASapCommonBLL.TryParseDatetime(interfaceInfo.I_Date);///I_DATE,7,有效起始日期
            sapSupplierQuotaInfo.EDate = BFDASapCommonBLL.TryParseDatetime(interfaceInfo.E_Date);///E_DATE,8,有效截止日期
            sapSupplierQuotaInfo.Znrmm = interfaceInfo.Znrmm;///ZNRMM,9,配额协议编号
            int.TryParse(interfaceInfo.Qupos, out int qupos);
            sapSupplierQuotaInfo.Qupos = qupos;///QUPOS,10,配额协议行项目编号
            int.TryParse(interfaceInfo.Quote, out int quote);
            sapSupplierQuotaInfo.Quote = quote;///QUOTE,11,配额
            sapSupplierQuotaInfo.Zstop = interfaceInfo.Zstop;///ZSTOP,12,停供标识
            sapSupplierQuotaInfo.Flag = interfaceInfo.Flag;///FLAG,13,状态标识(U:更改; D:删除)
            return sapSupplierQuotaInfo;
        }
        /// <summary>
        /// 批量接口数据转中间表集合
        /// </summary>
        /// <param name="interfaceList"></param>
        /// <returns></returns>
        public List<SapSupplierQuotaInfo> ConversionToCentreList(List<BFDASupplierQuotaInfo> interfaceList)
        {
            List<SapSupplierQuotaInfo> list = new List<SapSupplierQuotaInfo>();
            foreach (BFDASupplierQuotaInfo interfaceInfo in interfaceList)
            {
                list.Add(ConversionToCentreInfo(interfaceInfo));
            }
            return list;
        }
        /// <summary>
        /// 关键字
        /// </summary>
        /// <param name="interfaceInfo"></param>
        /// <returns></returns>
        public string GetKeyValue(BFDASupplierQuotaInfo interfaceInfo)
        {
            return interfaceInfo.Matnr + "|" + interfaceInfo.Lifnr;
        }
        /// <summary>
        /// 批量关键字
        /// </summary>
        /// <param name="interfaceList"></param>
        /// <returns></returns>
        public string GetKeyValues(List<BFDASupplierQuotaInfo> interfaceList)
        {
            return string.Join(",", interfaceList.Select(d => d.Matnr + "|" + d.Lifnr).ToArray());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="interfaceInfo"></param>
        /// <param name="logFid"></param>
        /// <param name="logSql"></param>
        public void InsertInfoToCentreTable(BFDASupplierQuotaInfo interfaceInfo, Guid logFid, string logSql)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 中间集合添加到数据库中间表
        /// </summary>
        /// <param name="centreList"></param>
        /// <param name="logFid"></param>
        /// <returns></returns>
        public int InsertListToCentreTable(List<BFDASupplierQuotaInfo> interfaceList, Guid logFid, string logSql)
        {
            List<SapSupplierQuotaInfo> sapSupplierQuotaInfos = ConversionToCentreList(interfaceList);
            StringBuilder sqlSb = new StringBuilder(logSql);
            foreach (SapSupplierQuotaInfo sapSupplierQuotaInfo in sapSupplierQuotaInfos)
            {
                sqlSb.AppendFormat("insert into [LES].[TI_IFM_SAP_SUPPLIER_QUOTA] (" +
                "[FID]," +
                "[LOG_FID], " +
                "[WERKS], " +
                "[LIFNR], " +
                "[SUPPLIER_NAME], " +
                "[PART_NO], " +
                "[QTYPE], " +
                "[I_DATE], " +
                "[E_DATE], " +
                "[ZNRMM], " +
                "[QUPOS], " +
                "[QUOTE], " +
                "[ZSTOP], " +
                "[FLAG], " +
                "[PROCESS_FLAG], " +
                "[VALID_FLAG], " +
                "[CREATE_USER], " +
                "[CREATE_DATE] " +
                ") values ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11},'{12}','{13}',{14},1,'{15}',GETDATE());",
                "NEWID()",///FID,0
                 logFid,///LOG_FID,1
                 sapSupplierQuotaInfo.Werks,///WERKS,2,工厂
                 sapSupplierQuotaInfo.Lifnr,///LIFNR,3,供应商
                 sapSupplierQuotaInfo.SupplierName.Replace("'", "\""),///SUPPLIER_NAME,4,供应商名称
                 sapSupplierQuotaInfo.PartNo,///PART_NO,5,物料编号
                 sapSupplierQuotaInfo.Qtype,///QTYPE,6,平台系数
                 sapSupplierQuotaInfo.IDate,///I_DATE,7,有效起始日期
                 sapSupplierQuotaInfo.EDate,///E_DATE,8,有效截止日期
                 sapSupplierQuotaInfo.Znrmm,///ZNRMM,9,配额协议编号
                 sapSupplierQuotaInfo.Qupos.GetValueOrDefault(),///QUPOS,10,配额协议行项目编号
                 sapSupplierQuotaInfo.Quote.GetValueOrDefault(),///QUOTE,11,配额
                 sapSupplierQuotaInfo.Zstop,///ZSTOP,12,停供标识
                 sapSupplierQuotaInfo.Flag,///FLAG,13,状态标识(U:更改; D:删除)
                 (int)ProcessFlagConstants.Untreated,///PROCESS_FLAG,14
                 loginUser///CREATE_USER,15
                );
            }
            if (sqlSb.Length > 0)
            {
                Log.WriteLogToFile(logFlag, sqlSb.ToString(), AppDomain.CurrentDomain.BaseDirectory + @"\Log_Script\", DateTime.Now.ToString("yyyyMMddHHmm"));
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(sqlSb.ToString());
            }            
            return sapSupplierQuotaInfos.Count;
        }
    }
}