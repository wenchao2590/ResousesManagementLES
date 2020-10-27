using BLL.LES;
using DM.LES;
using DM.SYS;
using Infrustructure.Data;
using Infrustructure.Logging;
using Infrustructure.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Serialization;

namespace WS.SAP.InboundDataService
{
    /// <summary>
    /// Sap下发的生产订单接收
    /// </summary>
    public class BFDASapProductOrderBomBLL : IBusiness<SapProductOrderBomInfo, BFDASapProductOrderBomInfo>
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
        string interfaceCode = "";
        #endregion

        /// <summary>
        /// 转换为中间对象
        /// </summary>
        /// <param name="sapProductOrderBomInfo"></param>
        /// <returns></returns>
        public SapProductOrderBomInfo ConversionToCentreInfo(BFDASapProductOrderBomInfo sapProductOrderBomInfo)
        {
            SapProductOrderBomInfo productOrderInfo = new SapProductOrderBomInfo();
       

            ///1 总成物料编号
            productOrderInfo.Fmatnr = sapProductOrderBomInfo.Fmatnr;
            ///2 工厂
            productOrderInfo.Dwerk = sapProductOrderBomInfo.Dwerk;
            ///3 生产订单
            productOrderInfo.Aufnr = sapProductOrderBomInfo.Aufnr;
            ///4 生产版本
            productOrderInfo.Verid = sapProductOrderBomInfo.Verid;
            ///5 上线日期         
            productOrderInfo.OnlineDate = BFDASapCommonBLL.TryParseDatetime(sapProductOrderBomInfo.OnlineTime);
            ///6 下线日期 
            productOrderInfo.OfflineDate = BFDASapCommonBLL.TryParseDatetime(sapProductOrderBomInfo.OfflineTime);
            ///子订单号 char15
            productOrderInfo.Zzdd = sapProductOrderBomInfo.Zzdd;
            ///当子订单不为空时，LES认为子订单号即为生产订单号
            if (!string.IsNullOrEmpty(productOrderInfo.Zzdd))
                productOrderInfo.Aufnr = productOrderInfo.Zzdd;
            if (!decimal.TryParse(sapProductOrderBomInfo.Bdmng, out decimal bdmng))
                throw new Exception("0x00000395");///物料数量格式错误

            ///物料图号 CHAR
            productOrderInfo.Matnrs = sapProductOrderBomInfo.Matnr;
            ///数量
            productOrderInfo.Bdmng = bdmng;
            ///更改单号  CHAR
            productOrderInfo.Aennr = sapProductOrderBomInfo.Aennr;
            ///工位 	CHAR
            productOrderInfo.Ebort = sapProductOrderBomInfo.Ebort;
            ///供应商 NVARCHAR(8)
            productOrderInfo.Lifnr = sapProductOrderBomInfo.Lifnr;
            ///平台 NVARCHAR(20)
            productOrderInfo.Platform = sapProductOrderBomInfo.Platform;

            return productOrderInfo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productOrderBomInfo"></param>
        /// <returns></returns>
        public BFDASapProductOrderBomMatnrdInfo ConversionToCentreDetailInfo(SapProductOrderBomInfo productOrderBomInfo)
        {
            BFDASapProductOrderBomMatnrdInfo productOrderBomMatnrdInfo = new BFDASapProductOrderBomMatnrdInfo();
            ///物料图号 CHAR
            productOrderBomMatnrdInfo.Matnr = productOrderBomInfo.Matnrs;
            ///数量
            productOrderBomMatnrdInfo.Bdmng = productOrderBomInfo.Bdmng.ToString();
            ///更改单号  CHAR
            productOrderBomMatnrdInfo.Aennr = productOrderBomInfo.Aennr;
            ///工位 	CHAR
            productOrderBomMatnrdInfo.Ebort = productOrderBomInfo.Ebort;
            ///供应商 NVARCHAR(8)
            productOrderBomMatnrdInfo.Lifnr = productOrderBomInfo.Lifnr;
            ///平台 NVARCHAR(20)
            productOrderBomMatnrdInfo.Platform = productOrderBomInfo.Platform;
            ///
            return productOrderBomMatnrdInfo;
        }
        /// <summary>       
        /// 转换为中间集合
        /// </summary>
        /// <param name="sapProductOrderBomInfos"></param>
        /// <returns></returns>
        public List<SapProductOrderBomInfo> ConversionToCentreList(List<BFDASapProductOrderBomInfo> sapProductOrderBomInfos)
        {
            ///订单BOM
            List<SapProductOrderBomInfo> productOrderBomInfos = new List<SapProductOrderBomInfo>();
            foreach (BFDASapProductOrderBomInfo sapProductOrderBomInfo in sapProductOrderBomInfos)
            {
                SapProductOrderBomInfo productOrderBomInfo = ConversionToCentreInfo(sapProductOrderBomInfo);
                productOrderBomInfos.Add(productOrderBomInfo);
            }
            var qProductOrderBomInfos =
                from u in productOrderBomInfos
                orderby u.Aufnr
                group u by new { u.Fmatnr, u.Dwerk, u.Aufnr, u.Verid, u.OnlineDate, u.OfflineDate, u.Zzdd }
                into g
                select new { g.Key };
            List<SapProductOrderBomInfo> list = new List<SapProductOrderBomInfo>();
            foreach (var qProductOrderBomInfo in qProductOrderBomInfos)
            {
                ///接口单车物料清单临时集合
                List<SapProductOrderBomInfo> orderBomInfos = productOrderBomInfos.Where(d =>
                d.Fmatnr == qProductOrderBomInfo.Key.Fmatnr &&
                d.Dwerk == qProductOrderBomInfo.Key.Dwerk &&
                d.Aufnr == qProductOrderBomInfo.Key.Aufnr &&
                d.Verid == qProductOrderBomInfo.Key.Verid &&
                d.Zzdd == qProductOrderBomInfo.Key.Zzdd &&
                d.OnlineDate == qProductOrderBomInfo.Key.OnlineDate &&
                d.OfflineDate == qProductOrderBomInfo.Key.OfflineDate).ToList();
                List<BFDASapProductOrderBomMatnrdInfo> sapProductOrderBomMatnrdInfos = new List<BFDASapProductOrderBomMatnrdInfo>();
                foreach (var orderBomInfo in orderBomInfos)
                {
                    sapProductOrderBomMatnrdInfos.Add(ConversionToCentreDetailInfo(orderBomInfo));
                }
                BFDASapProductOrderBomMatnrsInfo matnrsInfo = new BFDASapProductOrderBomMatnrsInfo();
                matnrsInfo.MatnrsAll = sapProductOrderBomMatnrdInfos;
                string matnrs = new XmlWrapper().ObjectToXmlByEncoding(matnrsInfo, Encoding.UTF8, false);
                matnrs = matnrs.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");
                matnrs= matnrs.Replace("\r\n", "").Trim();
                matnrs = matnrs.Replace(" ", "").Trim();
               
                ///
                SapProductOrderBomInfo info = new SapProductOrderBomInfo();
                info.Fmatnr = qProductOrderBomInfo.Key.Fmatnr;
                info.Dwerk = qProductOrderBomInfo.Key.Dwerk;
                info.Aufnr = qProductOrderBomInfo.Key.Aufnr;
                info.Verid = qProductOrderBomInfo.Key.Verid;
                info.Zzdd = qProductOrderBomInfo.Key.Zzdd;
                info.OnlineDate = qProductOrderBomInfo.Key.OnlineDate;
                info.OfflineDate = qProductOrderBomInfo.Key.OfflineDate;
                info.Matnrs = matnrs;
                list.Add(info);
            }
            return list;
        }
        /// <summary>
        /// 获取关键字
        /// </summary>
        /// <param name="sapProductOrderBomInfo"></param>
        /// <returns></returns>
        public string GetKeyValue(BFDASapProductOrderBomInfo sapProductOrderBomInfo)
        {
            return sapProductOrderBomInfo.Aufnr + "|" +
                sapProductOrderBomInfo.Dwerk + "|" +
                sapProductOrderBomInfo.Verid + "|" +
                sapProductOrderBomInfo.Zzdd + "|" +
                sapProductOrderBomInfo.Fmatnr;
        }
        /// <summary>
        /// 获取关键字
        /// </summary>
        /// <param name="sapProductOrderBomInfos"></param>
        /// <returns></returns>
        public string GetKeyValues(List<BFDASapProductOrderBomInfo> sapProductOrderBomInfos)
        {
            return string.Join(",", sapProductOrderBomInfos.Select(d => d.Aufnr + "|" + d.Dwerk + "|" + d.Verid + "|" + d.Zzdd + "|" + d.Fmatnr).ToArray());
        }
        /// <summary>
        /// 中间对象添加到数据库
        /// </summary>
        /// <param name="centreInfo"></param>
        /// <param name="logFid"></param>
        /// <returns></returns>
        public void InsertInfoToCentreTable(BFDASapProductOrderBomInfo interfaceInfo, Guid logFid, string logSql)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 中间集合添加到数据库
        /// </summary>
        /// <param name="sapProductOrderBomInfos"></param>
        /// <param name="logFid"></param>
        /// <returns></returns>
        public int InsertListToCentreTable(List<BFDASapProductOrderBomInfo> sapProductOrderBomInfos, Guid logFid, string logSql)
        {
            List<SapProductOrderBomInfo> productOrderBomInfos = ConversionToCentreList(sapProductOrderBomInfos);
            StringBuilder sqlSb = new StringBuilder(logSql);
            foreach (var productOrderBomInfo in productOrderBomInfos)
            {
                sqlSb.AppendFormat("insert into [LES].[TI_IFM_SAP_PRODUCT_ORDER_BOM] ("
                + "[FID] ,"
                + "[FMATNR] ,"
                + "[DWERK] ,"
                + "[AUFNR] ,"
                + "[VERID] ,"
                + "[ONLINE_DATE] ,"
                + "[OFFLINE_DATE], "
                + "[MATNRS] ,"
                + "[PROCESS_FLAG],"
                + "[VALID_FLAG] ,"
                + "[CREATE_USER] ,"
                + "[PROCESS_TIME], "
                + "[CREATE_DATE], "
                + "[LOG_FID]," +
                "[ZZDD]" +
                ") values (NEWID(),N'{0}',N'{1}',N'{2}',N'{3}','{4}','{5}',N'{6}',{7},{8},N'{9}', GETDATE(),GETDATE(),N'{10}',N'{11}');",
                productOrderBomInfo.Fmatnr,        ///0 订单物料                     
                productOrderBomInfo.Dwerk,         ///1 工厂
                productOrderBomInfo.Aufnr,         ///2 订单号
                productOrderBomInfo.Verid,         ///3 生产版本
                productOrderBomInfo.OnlineDate,    ///4 上线日期
                productOrderBomInfo.OfflineDate,   ///5 下线日期
                productOrderBomInfo.Matnrs,        ///6  物料⑦、数量⑧、更改单号⑨、工位⑩、供应商⑪、平台⑫需要以XML格式写入中间表MATNRS字段中.
                (int)ProcessFlagConstants.Untreated,///7 PROCESS_FLAG
                1,///8,VALID_FLAG
                loginUser,///9,CREATE_USER
                logFid,///10,LOG_FID
                productOrderBomInfo.Zzdd///11,ZZDD
                );
            }
            if (sqlSb.Length > 0)
            {
                Log.WriteLogToFile(logFlag, sqlSb.ToString(), AppDomain.CurrentDomain.BaseDirectory + @"\Log_Script\", DateTime.Now.ToString("yyyyMMddHHmm"));
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(sqlSb.ToString());
            }
            return productOrderBomInfos.Count;
        }
    }
}