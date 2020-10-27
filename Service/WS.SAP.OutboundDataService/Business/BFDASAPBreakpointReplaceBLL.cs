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
using WS.SAP.OutboundDataService.SAPWSDL15;

namespace WS.SAP.OutboundDataService
{
    /// <summary>
    /// LES-SAP-015	断点替换记录
    /// </summary>
    public class BFDASAPBreakpointReplaceBLL
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
        ///  LES-SAP-015	断点替换记录
        /// </summary>
        /// <param name="logFid"></param>
        public static ExecuteResultConstants SendBreakpointReplaceData(Guid logFid, InterfaceConfigInfo interfaceConfigInfo,
            ref string errorCode, ref string errorMsg, out string msgContent)
        {
            msgContent = string.Empty;
            List<SapBreakpointReplaceInfo> sapSendInfos = new SapBreakpointReplaceBLL().GetList("[LOG_FID] = N'" + logFid + "' and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", string.Empty);
            if (sapSendInfos.Count == 0)
            {
                errorCode = "MC:0x00000212";///经检测,传递数据为空,请确认
                return ExecuteResultConstants.Exception;
            }

            ///发送消息
            Zsles006[] Itab = new Zsles006[sapSendInfos.Count];
            for (int i = 0; i < sapSendInfos.Count; i++)
            {
                Zsles006 zles006 = new Zsles006();                
                zles006.Aufnr = sapSendInfos[i].Aufnr;	                    ///生产订单
				zles006.Nmatnr = sapSendInfos[i].Nmatnr;	                ///新物料号
				zles006.Omatnr = sapSendInfos[i].Omatnr;	                ///旧物料号
				zles006.Menge = sapSendInfos[i].Menge.GetValueOrDefault();	///数量
				zles006.Vlsch = sapSendInfos[i].Vlsch;	                    ///工位
				zles006.Rdate = sapSendInfos[i].Rdate.GetValueOrDefault().ToString(sapDateFormat);	///替换日期
                Itab[i]= zles006;
            }
           
            ZLES006Client client = new ZLES006Client();
            client.Endpoint.Address = new EndpointAddress(interfaceConfigInfo.CallUrl);
            
            ///1标识成功、0标识失败
            //long tenantId = 89;
            //if (!long.TryParse(interfaceConfigInfo.Param2, out tenantId))
            //{
            //    errorCode = "MC:3x00000021";///接口配置错误
            //  return ExecuteResultConstants.Exception;
            //}
            ///返回对象
            Zfles006[] retu006 = new Zfles006[0];
            ///1标识成功、0标识失败
            string result;
            ///返回信息提示
            string msg;
            ///影响数据行数
            int dataCnt = client.Zles006(ref Itab, ref retu006, out result, out msg);
            Log.WriteLogToFile("LES-SAP-015	断点替换记录.return:dataCnt:" + dataCnt, AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMddHH"));

            ///失败时的ERROR_CODE中是对方系统的ERROR_MSG
            if (result == "0") {
                errorMsg = msg;
                for (int i = 0; i < retu006.Count(); i++)
                {
                    errorMsg += "; ";
                    errorMsg += retu006[i].Nmatnr+":"+retu006[i].Mess;
                }

                Log.WriteLogToFile(errorMsg, AppDomain.CurrentDomain.BaseDirectory + @"\Error_Log\", DateTime.Now.ToString("yyyyMMddHH"));
                return ExecuteResultConstants.Error;
            }
            msgContent = GetMsgContent(sapSendInfos);

            Log.WriteLogToFile("result:"+result+ " | Content:" + msgContent , AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMddHH"));

            return ExecuteResultConstants.Success;
        }
        /// <summary>
        /// 获取报文体
        /// </summary>
        /// <param name="sapSendInfos"></param>
        /// <returns></returns>
        private static string GetMsgContent(List<SapBreakpointReplaceInfo> sapSendInfos)
        {
            ///TODO:若数据量过大，其实建议取消
            return new XmlWrapper().ObjectToXml(sapSendInfos, false);
        }
    }
}
