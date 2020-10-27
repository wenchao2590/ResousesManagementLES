using BLL.LES;
using BLL.SYS;
using DM.LES;
using DM.SYS;
using Infrustructure.Logging;
using Infrustructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using WS.SAP.OutboundDataService.LesSap012;
using WS.SAP.OutboundDataService.SAPWSDL15;

namespace WS.SAP.OutboundDataService
{
    /// <summary>
    /// LES-SAP-012 盘点计划报告
    /// </summary>
    public class BFDASAPInventoryCheckReportBLL
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
        ///  LES-SAP-012 盘点计划报告
        /// </summary>
        /// <param name="logFid"></param>
        public static ExecuteResultConstants SendInventoryCheckReportData(Guid logFid, InterfaceConfigInfo interfaceConfigInfo,
            ref string errorCode, ref string errorMsg, out string msgContent)
        {
            msgContent = string.Empty;
            List<SapInventoryCheckReportInfo>  sapInventoryCheckReportInfos = new SapInventoryCheckReportBLL().GetList("[LOG_FID] = N'" + logFid + "' and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", string.Empty);
            if (sapInventoryCheckReportInfos.Count == 0)
            {
                errorCode = "MC:0x00000212";///经检测,传递数据为空,请确认
                return ExecuteResultConstants.Exception;
            }

            ///发送消息
            Zsles008[] Itab = new Zsles008[sapInventoryCheckReportInfos.Count];
            for (int i = 0; i < sapInventoryCheckReportInfos.Count; i++)
            {
                Zsles008 zfles008 = new Zsles008();
                zfles008.Iblnr = sapInventoryCheckReportInfos[i].Iblnr;	                    ///库存盘点凭证
				zfles008.Matnr = sapInventoryCheckReportInfos[i].Matnr;	                    ///物料号
				///zfles008.Werks = sapInventoryCheckReportInfos[i].;	                    ///工厂
				zfles008.Lgort = sapInventoryCheckReportInfos[i].Lgort;	                    ///库存地点
				zfles008.Menge = Convert.ToDecimal( sapInventoryCheckReportInfos[i].Menge.GetValueOrDefault());	                ///数量
				zfles008.Aqty = Convert.ToDecimal(sapInventoryCheckReportInfos[i].Aqty.GetValueOrDefault()); 	///实盘数量
                zfles008.Dqty = Convert.ToDecimal(sapInventoryCheckReportInfos[i].Dqty.GetValueOrDefault());   ///差异数量
                zfles008.Gidat =  sapInventoryCheckReportInfos[i].Zldat.GetValueOrDefault().ToString(sapDateFormat); ///库存盘点计划日期

                Itab[i]= zfles008;
            }
            msgContent= new XmlWrapper().ObjectToXml(Itab, false);
            Log.WriteLogToFile("LES-SAP-012	盘点报告.return:Count:" + sapInventoryCheckReportInfos.Count+"/r"+ msgContent, AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMddHH"));

            ZLES008Client client = new ZLES008Client();
            client.Endpoint.Address = new EndpointAddress(interfaceConfigInfo.CallUrl);

            ///返回对象
            Zfles008[] retu008 = new Zfles008[0];
            ///1标识成功、0标识失败
            int result;
            ///返回信息提示
            string msg;
            ///影响数据行数
            string dataCnt = client.Zles008(ref Itab, ref retu008, out result, out msg);
            Log.WriteLogToFile("LES-SAP-012	盘点报告.return:dataCnt:" + dataCnt, AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMddHH"));

            ///失败时的ERROR_CODE中是对方系统的ERROR_MSG
            if (result == 0) {
                errorMsg = msg;
                for (int i = 0; i < retu008.Count(); i++)
                {
                    errorMsg += "; ";
                    errorMsg += retu008[i].Msg+":"+ retu008[i].Iblnr;
                }

                Log.WriteLogToFile(errorMsg, AppDomain.CurrentDomain.BaseDirectory + @"\Error_Log\", DateTime.Now.ToString("yyyyMMddHH"));
                return ExecuteResultConstants.Error;
            }
            msgContent = GetMsgContent(sapInventoryCheckReportInfos);

            Log.WriteLogToFile("result:"+result+ " | Content:" + msgContent , AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMddHH"));

            return ExecuteResultConstants.Success;
        }
        /// <summary>
        /// 获取报文体
        /// </summary>
        /// <param name="sapInventoryCheckReportInfos"></param>
        /// <returns></returns>
        private static string GetMsgContent(List<SapInventoryCheckReportInfo> sapInventoryCheckReportInfos)
        {
            ///TODO:若数据量过大，其实建议取消
            return new XmlWrapper().ObjectToXml(sapInventoryCheckReportInfos, false);
        }
    }
}
