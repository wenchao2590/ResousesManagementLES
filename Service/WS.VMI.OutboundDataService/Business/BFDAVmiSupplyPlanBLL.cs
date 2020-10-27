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
using WS.VMI.OutboundDataService.VMIWSDL;

namespace WS.VMI.OutboundDataService
{
    /// <summary>
    /// LES-WMS-009 供货计划 LW006
    /// </summary>
    public class BFDAVmiSupplyPlanBLL
    {
        /// <summary>
        /// 日期格式
        /// </summary>
        private static string vmiDateFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// LES-WMS-009 供货计划 LW006
        /// </summary>
        /// <param name="logFid"></param>
        public static ExecuteResultConstants SendVmiSupplyPlan(Guid logFid, InterfaceConfigInfo interfaceConfigInfo,
            ref string errorCode, ref string errorMsg, out string msgContent)
        {
            msgContent = string.Empty;
          
                                                                        
            List<WmsVmiSupplyPlanInfo> vmiSupplyPlans = new WmsVmiSupplyPlanBLL().GetList("[LOG_FID] = N'" + logFid + "' and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", string.Empty);
         
            if (vmiSupplyPlans.Count == 0)
            {
                errorCode = "MC:0x00000212";///经检测,传递数据为空,请确认
                return ExecuteResultConstants.Exception;
            }

            ///发送内容
            List<BFDAVmiSupplyPlanInfo> bFDAVmiSupplyPlanInfos = new List<BFDAVmiSupplyPlanInfo>();
            foreach (var vmiSupplyPlan in vmiSupplyPlans)
            {
                bFDAVmiSupplyPlanInfos.Add(GetBFDAVmiInfo(vmiSupplyPlan));
            }
            BFDAVMISendDataInfo<BFDAVmiSupplyPlanInfo> sendDataInfo = new BFDAVMISendDataInfo<BFDAVmiSupplyPlanInfo>();
            sendDataInfo.List = bFDAVmiSupplyPlanInfos;

            WsProcessServiceClient client = new WsProcessServiceClient();
            client.Endpoint.Address = new EndpointAddress(interfaceConfigInfo.CallUrl);
            ///1标识成功、0标识失败
            long tenantId = 89;
            if (!long.TryParse(interfaceConfigInfo.Param2, out tenantId))
            {
                errorCode = "MC:3x00000021";///接口配置错误
                return ExecuteResultConstants.Exception;
            }
            ///数据发送
            msgContent = new XmlWrapper().ObjectToXml(sendDataInfo, false);
            Log.WriteLogToFile(msgContent, AppDomain.CurrentDomain.BaseDirectory + @"\logContent\", DateTime.Now.ToString("yyyyMMddHHmm")+ interfaceConfigInfo.SysMethodName);

            string result = client.runProcessWithAction(interfaceConfigInfo.Param1, tenantId, interfaceConfigInfo.SysMethodName, msgContent);

            BFDAVMIResultInfo resultInfo = new XmlWrapper(result, LoadType.FromString).XmlToObject("/Result", typeof(BFDAVMIResultInfo)) as BFDAVMIResultInfo;

            ///成功后更新中间表数据处理状态
            if (resultInfo.Status.ToLower() == "error")
            {
                errorCode = resultInfo.ErrorCode;
                errorMsg = resultInfo.ErrorMsg;
                return ExecuteResultConstants.Error;
            }

            Log.WriteLogToFile("END: " + ExecuteResultConstants.Success, AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMddHHmm"));

            return ExecuteResultConstants.Success;
        }

        /// <summary>
        /// 对象转换
        /// </summary>
        /// <param name="vmiInfo"></param>
        /// <returns></returns>
        private static BFDAVmiSupplyPlanInfo GetBFDAVmiInfo(WmsVmiSupplyPlanInfo vmiInfo)
        {
            BFDAVmiSupplyPlanInfo bFDAVmiSupplyPlanInfo = new BFDAVmiSupplyPlanInfo();
            bFDAVmiSupplyPlanInfo.SupplierCode = vmiInfo.SupplierNum;
            bFDAVmiSupplyPlanInfo.PartNo = vmiInfo.PartNo;
            bFDAVmiSupplyPlanInfo.RequireDate = vmiInfo.DeliveryDate.GetValueOrDefault().ToString(vmiDateFormat);
            bFDAVmiSupplyPlanInfo.PartQty = vmiInfo.RequireQty.GetValueOrDefault();
            bFDAVmiSupplyPlanInfo.VmiWarehouseCode = vmiInfo.VmiWmNo;
            bFDAVmiSupplyPlanInfo.Werks = vmiInfo.Plant;
            return bFDAVmiSupplyPlanInfo;
        }


    }
}
