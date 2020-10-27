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
    ///  LES-MES-002  缺件影响订单
    /// </summary>
    public class BFDAMesMissingpartsInfluenceOrderscopeBLL
    {
        /// <summary>
        /// 日期格式
        /// </summary>
        private static string vmiDateFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        ///  LES-MES-002  缺件影响订单
        /// </summary>
        /// <param name="logFid"></param>
        public static ExecuteResultConstants SendQmisAsnPullSheet(Guid logFid, InterfaceConfigInfo interfaceConfigInfo,
                ref string errorCode, ref string errorMsg, out string msgContent)
        {
            msgContent = string.Empty;

            Log.WriteLogToFile("The  logFid|" + logFid, AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMddHHmm"));

            List<MesMissingpartsInfluenceOrderscopeInfo> mesinfo = new MesMissingpartsInfluenceOrderscopeBLL().GetList("[LOG_FID] = N'" + logFid + "' and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", string.Empty);
            if (mesinfo.Count == 0)
            {
                errorCode = "MC:0x00000212";///经检测,传递数据为空,请确认
                return ExecuteResultConstants.Exception;
            }

            AssemblePlanMissDetail[] missDetails = new AssemblePlanMissDetail[mesinfo.Count];

            for (int i = 0; i < mesinfo.Count; i++)
            {
                AssemblePlanMissDetail missDetail = new AssemblePlanMissDetail();
                missDetail.ENTERPRISE = mesinfo[i].Enterprise;  ///nvarchar(8)	Checked
                missDetail.SITE_NO = mesinfo[i].SiteNo;        ///nvarchar(8)	Checked
                missDetail.AREA_NO = mesinfo[i].AreaNo;        ///nvarchar(8)	Checked
                missDetail.DMS_NO = mesinfo[i].DmsNo;          ///nvarchar(32) Checked
                missDetail.MATERIAL_CHECK = mesinfo[i].MaterialCheck.GetValueOrDefault()==true?1 : 0;  ///bit	Checked
                missDetail.SEND_TIME = mesinfo[i].SendTime.GetValueOrDefault();  ///datetime	Checked	

                missDetails[i] = missDetail;
            }

            //    ///发送内容
            msgContent = new XmlWrapper().ObjectToXml(missDetails, false);
            LESMESlocal.LESWebServiceSoapClient client = new LESWebServiceSoapClient();
            client.Endpoint.Address = new EndpointAddress(interfaceConfigInfo.CallUrl);
            InterfaceCallbackInfo backinfo = client.GetAssemblePlanMissDetail(missDetails);
            ///记录结果
            Log.WriteLogToFile("返回结果:errorCode:" + errorCode + "; msgContent:" + errorMsg     , AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMddHHmm"));
            
            if (backinfo.RESULT ==0)
            {
                errorCode = backinfo.MSGNO;
                errorMsg = backinfo.MSG;
                return ExecuteResultConstants.Error;
               
            }
            
            return ExecuteResultConstants.Success;
        }
    }

}

