using BLL.LES;
using BLL.SYS;
using DM.LES;
using DM.SYS;
using Infrustructure.Logging;
using Infrustructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WS.SAP.OutboundDataService.SAPWSDL;

namespace WS.SAP.OutboundDataService
{
    /// <summary>
    /// MaterialMoveBLL
    /// </summary>
    public class BFDAMaterialMoveBLL
    {
        #region Common
        /// <summary>
        /// SAP日期格式
        /// </summary>
        private static string sapDateFormat = "yyyy-MM-dd";
        /// <summary>
        /// 是否写入日志
        /// </summary>
        private static  string logFlag = AppSettings.GetConfigString("LogFlag");
        #endregion


        /// <summary>
        /// 物料移动数据发送至SAP
        /// </summary>
        /// <param name="logFid"></param>
        public static ExecuteResultConstants SendMaterialMoveData(Guid logFid, InterfaceConfigInfo interfaceConfigInfo,
            ref string errorCode, ref string errorMsg, out string msgContent)
        {
            msgContent = string.Empty;
            
            List<SapTranOutInfo> sapTranOutInfos = new SapTranOutBLL().GetList("[LOG_FID] = N'" + logFid + "' and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", string.Empty);
            if (sapTranOutInfos.Count == 0)
            {
                errorCode = "MC:0x00000212";///经检测,传递数据为空,请确认
                return ExecuteResultConstants.Exception;
            }

            ///发送消息
            Zsles002[] Itab = new Zsles002[sapTranOutInfos.Count];
            for (int i = 0; i < sapTranOutInfos.Count; i++)
            {
                Zsles002 zsles002 = new Zsles002();
                ///匹配字段
                zsles002.Id = sapTranOutInfos[i].Fid.GetValueOrDefault().ToString();///唯一值
                zsles002.Matnr = sapTranOutInfos[i].Matnr;///物料
                zsles002.Menge = sapTranOutInfos[i].Menge.GetValueOrDefault();///数量 (检查传值时候, 小数的位数)
                zsles002.Rsnum = sapTranOutInfos[i].Rsnum;///预留号
                zsles002.Bwart = sapTranOutInfos[i].Bwart;///移动类型                
                zsles002.Kostl = sapTranOutInfos[i].Kostl;///成本中心

                zsles002.Budat = sapTranOutInfos[i].Budat.GetValueOrDefault().ToString(sapDateFormat);///日期
                zsles002.Lgort = sapTranOutInfos[i].Lgort;///发出库存地点
                zsles002.Umlgo = sapTranOutInfos[i].Umlgo;///接收库存地点
                zsles002.Werks = sapTranOutInfos[i].Werks;///发出工厂
                zsles002.Unwrk = sapTranOutInfos[i].Unwrk;///收货工厂
                zsles002.Ebelp = sapTranOutInfos[i].Ebelp.GetValueOrDefault().ToString();///采购订单行号
                zsles002.Ebeln = sapTranOutInfos[i].Ebeln;///采购订单号
                zsles002.Lifnr = sapTranOutInfos[i].Lifnr;///供应商

                //zsles002.Aufnr = string.Empty;  ///生产订单号  ///已经撤销
                //zsles002.Kosrt = string.Empty;  ///成本中心    ///已经撤销
                //zsles002.Rspos = string.Empty;  ///预留行号    ///已经撤销

                Itab[i] = zsles002;              
            }            
            
            ZLES002Service service = new ZLES002Service();
           
             service.Url = interfaceConfigInfo.CallUrl;
            
            ///1标识成功、0标识失败
            string sapResult;
            ///影响数据行数
            int dataCnt = service.Zles002(ref Itab, out errorMsg, out sapResult);

            Log.WriteLogToFile(logFlag, "Return:dataCnt:" + dataCnt , AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMddHHmm"));

            ///失败时的ERROR_CODE中是对方系统的ERROR_MSG
            if (sapResult == "0") return ExecuteResultConstants.Error;
            msgContent = GetMsgContent(sapTranOutInfos);
            return ExecuteResultConstants.Success;
        }
        /// <summary>
        /// 获取报文体
        /// </summary>
        /// <param name="sapTranOutInfos"></param>
        /// <returns></returns>
        private static string GetMsgContent(List<SapTranOutInfo> sapTranOutInfos)
        {
            ///TODO:若数据量过大，其实建议取消
            return new XmlWrapper().ObjectToXml(sapTranOutInfos, false);
        }
    }
}
