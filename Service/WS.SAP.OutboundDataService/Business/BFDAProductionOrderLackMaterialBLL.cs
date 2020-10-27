using BLL.LES;
using DM.LES;
using DM.SYS;
using Infrustructure.Logging;
using Infrustructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WS.SAP.OutboundDataService.SAPLACKMATERIALWSDL17;

namespace WS.SAP.OutboundDataService
{
    /// <summary>
    /// LES-SAP-017	缺件影响生产订单范围
    /// </summary>
    public class BFDAProductionOrderLackMaterialBLL
    {
        #region Common
        /// <summary>
        /// SAP日期格式
        /// </summary>
        private static string sapDateFormat = "yyyy-MM-dd";
        /// <summary>
        /// 是否写入日志
        /// </summary>
        private static string logFlag = AppSettings.GetConfigString("LogFlag");
        #endregion
        /// <summary>
        /// 物料移动数据发送至SAP
        /// </summary>
        /// <param name="logFid"></param>
        public static ExecuteResultConstants SendLackMaterialData(Guid logFid, InterfaceConfigInfo interfaceConfigInfo,
            ref string errorCode, ref string errorMsg, out string msgContent)
        {

            msgContent = string.Empty;
            List<SapProductionOrderLackMaterialInfo> sapProductionOrderLacks = new SapProductionOrderLackMaterialBLL().GetList("[LOG_FID] = N'" + logFid + "' and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", string.Empty);
            if (sapProductionOrderLacks.Count == 0)
            {
                errorCode = "MC:0x00000212";///经检测,传递数据为空,请确认
                return ExecuteResultConstants.Exception;
            }

            
            Zsles003[] Itab = new Zsles003[sapProductionOrderLacks.Count];
            for (int i = 0; i < sapProductionOrderLacks.Count; i++)
            {
                Zsles003 Zsles003 = new Zsles003();
                ///匹配字段
                Zsles003.Id = sapProductionOrderLacks[i].Fid.ToString();
                Zsles003.Enterprise = sapProductionOrderLacks[i].Enterprise;      ///工厂编号 	
                Zsles003.SiteNo = sapProductionOrderLacks[i].SiteNo;  ///车间编号  	
                Zsles003.AreaNo = sapProductionOrderLacks[i].AreaNo;  ///生产线编号 	
                Zsles003.DmsNo = sapProductionOrderLacks[i].DmsNo;      ///计划订单号 	
                Zsles003.MaterialCheck = sapProductionOrderLacks[i].MaterialCheck==true?"1":"0";      ///排查结果  	
                Zsles003.SendTime = sapProductionOrderLacks[i].SendTime.GetValueOrDefault().ToString(sapDateFormat);      ///发送时间 	
                Zsles003.Datuv = sapProductionOrderLacks[i].Datuv.GetValueOrDefault().ToString(sapDateFormat);      ///替换日期 
                Itab[i] = Zsles003;
               
            }
            ZLES003Service service = new ZLES003Service();

            service.Url = interfaceConfigInfo.CallUrl;
             


            // 1标识成功、0标识失败
            Zfles003[] sapResult = new Zfles003[0];
            ///影响数据行数
            int dataCnt = service.Zles003(ref Itab,   ref sapResult);
            Log.WriteLogToFile(logFlag, "LES-SAP-017-缺件影响生产订单范围Return:dataCnt:" + dataCnt , AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMddHHmm"));

            //  Id 是错误的数据ID.
            if (sapResult[0].Id.Length > 0)
            {
                Log.WriteLogToFile("LES-SAP-017	缺件影响生产订单范围:" + sapResult[0].Id+" |"
              , AppDomain.CurrentDomain.BaseDirectory + @"\Error\", DateTime.Now.ToString("yyyyMMddHHmm"));
                return ExecuteResultConstants.Error;
            } 
            msgContent = sapResult[0].Msg;
            return ExecuteResultConstants.Success;
        }
        /// <summary>
        /// 获取报文体
        /// </summary>
        /// <param name="sapProductionOrderLacks"></param>
        /// <returns></returns>
        private static string GetMsgContent(List<SapTranOutInfo> sapProductionOrderLacks)
        {
            ///TODO:若数据量过大，其实建议取消
            return new XmlWrapper().ObjectToXml(sapProductionOrderLacks, false);
        }
    }
}
