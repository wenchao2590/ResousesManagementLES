using BLL.LES;
using DM.LES;
using DM.SYS;
using Infrustructure.Logging;
using Infrustructure.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace WS.SAP.InboundDataService
{
    public class BFDASapPartBLL : IBusiness<SapPartsInfo, BFDASapPartInfo>
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
        string interfaceCode = "SAP-LES-001 SAP物料基础数据接收";
        #endregion

        /// <summary>
        /// 转换为中间对象
        /// </summary>
        /// <param name="interfaceInfo"></param>
        /// <returns></returns>
        public SapPartsInfo ConversionToCentreInfo(BFDASapPartInfo interfaceInfo)
        {
            SapPartsInfo info = new SapPartsInfo();
            ///工厂
            info.Werks = interfaceInfo.Werks;
            ///物料编号
            info.Matnr = interfaceInfo.Matnr;
            ///物料类型
            info.Mtart = interfaceInfo.Mtart;
            /// 物料描述(中文)    过滤掉 单引号
            if (!string.IsNullOrEmpty(interfaceInfo.Maktx))
                info.MaktxZh = interfaceInfo.Maktx.Replace("'", "''");
            /// 语言代码，中文是1，英文是E，BFDA还有D
            info.Spras = interfaceInfo.Spras;
            ///
            bool isErrorParts = false;
            bool.TryParse(interfaceInfo.Flag, out isErrorParts);
            /// 是否易错件
            info.Flag = isErrorParts;
            /// 单位
            info.Meins = interfaceInfo.Meins;
            /// MRP控制者
            info.Dispo = interfaceInfo.Dispo;
            /// MRP类型
            //info.Dismm = interfaceInfo.Dismm;
            ///ABC 只有U和D U=Update, D=Delete  
            ///MSTAE(是否删除标记) = 1
            info.Mstae = interfaceInfo.Abc == "U" ? "2" : "3";
            ///采购组
            info.Ekgrp = interfaceInfo.Ekgrp;
            ///
            return info;
        }
        /// <summary>
        /// 转换为中间集合
        /// </summary>
        /// <param name="interfaceList"></param>
        /// <returns></returns>
        public List<SapPartsInfo> ConversionToCentreList(List<BFDASapPartInfo> interfaceList)
        {
            List<SapPartsInfo> list = new List<SapPartsInfo>();
            foreach (BFDASapPartInfo interfaceInfo in interfaceList)
            {
                list.Add(ConversionToCentreInfo(interfaceInfo));
            }
            return list;
        }
        /// <summary>
        /// 获取关键字
        /// </summary>
        /// <param name="interfaceList"></param>
        /// <returns></returns>
        public string GetKeyValue(BFDASapPartInfo interfaceInfo)
        {
            return interfaceInfo.Matnr;
        }
        /// <summary>
        /// 获取关键字
        /// </summary>
        /// <param name="interfaceInfo"></param>
        /// <returns></returns>
        public string GetKeyValues(List<BFDASapPartInfo> interfaceList)
        {
            return string.Join(",", interfaceList.Select(d => d.Matnr).ToArray());
        }
        /// <summary>
        /// 中间对象添加到数据库
        /// </summary>
        /// <param name="centreInfo"></param>
        /// <param name="logFid"></param>
        /// <returns></returns>
        public void InsertInfoToCentreTable(BFDASapPartInfo interfaceInfo, Guid logFid, string logSql)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 中间集合添加到数据库
        /// </summary>
        /// <param name="centreList"></param>
        /// <param name="logFid"></param>
        /// <returns></returns>
        public int InsertListToCentreTable(List<BFDASapPartInfo> interfaceList, Guid logFid, string logSql)
        {
            List<SapPartsInfo> sapPartsInfos = ConversionToCentreList(interfaceList);
            StringBuilder sqlSb = new StringBuilder(logSql);
            foreach (var sapPartsInfo in sapPartsInfos)
            {
                sqlSb.AppendFormat("insert into [LES].[TI_IFM_SAP_PARTS] (" +
                "[FID]," +
                "[LOG_FID]," +
                "[WERKS]," +
                "[MATNR]," +
                "[MTART]," +
                "[MAKTX_ZH]," +
                "[MAKTX_EN]," +
                "[SPRAS]," +
                "[FLAG]," +
                "[MEINS]," +
                "[DISPO]," +
                "[DISMM]," +
                "[MSTAE]," +
                "[VALID_FLAG]," +
                "[CREATE_USER]," +
                "[CREATE_DATE]," +
                "[PROCESS_FLAG]," +
                "[EKGRP]," +
                "[ABC]"
                + ") values (NEWID(),'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',1,'{12}', GETDATE(),{13},N'{14}',N'{15}');",
                logFid,///LOG_FID,0
                sapPartsInfo.Werks,///WERKS,1
                sapPartsInfo.Matnr,///MATNR,2,物料编号
                sapPartsInfo.Mtart,///MTART,3,物料类型
                sapPartsInfo.MaktxZh,///MAKTX_ZH,4,物料描述(中文)
                sapPartsInfo.MaktxEn,///MAKTX_EN,5,英文描述
                sapPartsInfo.Spras,///SPRAS,6,语言代码
                sapPartsInfo.Flag.GetValueOrDefault(),///FLAG,7,是否易错件
                sapPartsInfo.Meins,///MEINS,8,单位
                sapPartsInfo.Dispo,///DISPO,9,MRP控制者
                sapPartsInfo.Dismm,///DISMM,10,MRP类型
                sapPartsInfo.Mstae,///MSTAE,11,接口标识
                loginUser,///CREATE_USER,12
                (int)ProcessFlagConstants.Untreated, ///PROCESS_FLAG,13
                sapPartsInfo.Ekgrp, ///EKGRP,14,采购组
                sapPartsInfo.Abc
                );
            }
            if (sqlSb.Length > 0)
            {
                Log.WriteLogToFile(logFlag, interfaceCode + "-EcecuteSQL:|" + sqlSb.ToString(), AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMdd"));
                BLL.SYS.CommonBLL.ExecuteNonQueryBySql(sqlSb.ToString());
            }

            return sapPartsInfos.Count;
        }
    }
}