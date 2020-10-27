using BLL.LES;
using DM.LES;
using DM.SYS;
using Infrustructure.Logging;
using Infrustructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using WS.MES.OutboundDataService.LESMESlocal;

namespace WS.MES.OutboundDataService
{

    /// <summary>
    ///    LES-MES-005 断点替换信息
    /// </summary>
    public class BFDAMesBreakpointReplacementRecordBLL
    {
        /// <summary>
        /// 日期格式
        /// </summary>
        private static string vmiDateFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        ///  LES-MES-005 断点替换信息
        /// </summary>
        /// <param name="logFid"></param>
        public static ExecuteResultConstants SendQmisAsnPullSheet(Guid logFid, InterfaceConfigInfo interfaceConfigInfo,
                ref string errorCode, ref string errorMsg, out string msgContent)
        {
            msgContent = string.Empty;
            InterfaceReturnInfo interfaceReturnInfo = new InterfaceReturnInfo();
            Log.WriteLogToFile("The  logFid|" + logFid, AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMddHHmm"));

            List<MesBreakpointReplacementRecordInfo> qmiInfos = new MesBreakpointReplacementRecordBLL().GetList("[LOG_FID] = N'" + logFid + "' and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", string.Empty);
            if (qmiInfos.Count == 0)
            {
                errorCode = "MC:0x00000212";///经检测,传递数据为空,请确认
                return ExecuteResultConstants.Exception;
            }
            ReplacementRecord[] replacementRecord = new ReplacementRecord[qmiInfos.Count];
            for (int i = 0; i < qmiInfos.Count; i++)
            {
                ReplacementRecord replacement = new ReplacementRecord();
                replacement.ORDERNO = qmiInfos[i].Orderno;
                replacement.OLDPARTNO = qmiInfos[i].Oldpartno;
                replacement.NEWPARTNO = qmiInfos[i].Newpartno;
                replacement.OLDSUPPLIER = qmiInfos[i].Oldsupplier;
                replacement.NEWSUPPLIER = qmiInfos[i].Newsupplier;
                replacement.OLDSTATION = qmiInfos[i].Oldstation;
                replacement.NEWSTATION = qmiInfos[i].Newstation;
                replacement.OLDQTY = qmiInfos[i].Oldqty.GetValueOrDefault();
                replacement.NEWQTY = qmiInfos[i].Newqty.GetValueOrDefault();
                replacement.REPLACETIME = qmiInfos[i].Replacetime.GetValueOrDefault() ;
                replacementRecord[i] = replacement;
            }
            
            ///发送内容
            msgContent = new XmlWrapper().ObjectToXml(replacementRecord, false);
            LESMESlocal.LESWebServiceSoapClient client = new LESWebServiceSoapClient();
            client.Endpoint.Address = new EndpointAddress(  interfaceConfigInfo.CallUrl);
            InterfaceCallbackInfo backinfo= client.GetReplacementRecord(replacementRecord);
            Log.WriteLogToFile("返回结果:errorCode:" + errorCode + "; msgContent:" + errorMsg     , AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMddHHmm"));
            
            if (backinfo.RESULT == 0)
            {
                errorCode = backinfo.MSGNO;
                errorMsg = backinfo.MSG;
                return ExecuteResultConstants.Error;
            }
            return ExecuteResultConstants.Success;
        }
    }

}

