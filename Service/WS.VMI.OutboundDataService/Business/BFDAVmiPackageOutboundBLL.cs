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
using WS.VMI.OutboundDataService.VMIWSDL;

namespace WS.VMI.OutboundDataService
{

    /// <summary>
    /// LES-WMS-012	RDC器具出库 WL017
    /// </summary>
    public class BFDAVmiPackageOutboundBLL
    {
        /// <summary>
        /// 日期格式
        /// </summary>
        private static string vmiDateFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        ///  LES-WMS-017	RDC器具出库
        // <param name="logFid"></param>
        public static ExecuteResultConstants SendPackageOutBound(Guid logFid, InterfaceConfigInfo interfaceConfigInfo,
            ref string errorCode, ref string errorMsg, out string msgContent)
        {
            msgContent = string.Empty;
            List<WmsVmiPackageOutboundInfo> vmiPackageOutboundInfos = new WmsVmiPackageOutboundBLL().GetList("[LOG_FID] = N'" + logFid + "' and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", string.Empty);
            if (vmiPackageOutboundInfos.Count == 0)
            {
                errorCode = "MC:0x00000212";///经检测,传递数据为空,请确认
                return ExecuteResultConstants.Exception;
            }

            ///发送内容
            List<BFDAVmiPackageOutboundInfo> bFDAVmiPackageOutboundInfos = new List<BFDAVmiPackageOutboundInfo>();
 
            foreach (var VmiPackageOutboundInfo in vmiPackageOutboundInfos)
            {
                bFDAVmiPackageOutboundInfos.Add(GetBFDAVMIInfo(VmiPackageOutboundInfo));
            }
            BFDAVMISendDataInfo<BFDAVmiPackageOutboundInfo> sendDataInfo = new BFDAVMISendDataInfo<BFDAVmiPackageOutboundInfo>();
            sendDataInfo.List = bFDAVmiPackageOutboundInfos;
            ///
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
            // throw new Exception(resultInfo.Status); TDD: delete
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
        private static BFDAVmiPackageOutboundInfo GetBFDAVMIInfo(WmsVmiPackageOutboundInfo vmiInfo)
        {
            BFDAVmiPackageOutboundInfo BfdaInfo = new BFDAVmiPackageOutboundInfo();
            ///TODO:此处获取时间默认值为0001-01-01，对方系统是否能够接收，待测
            BfdaInfo.Externreceiptkey = vmiInfo.Externreceiptkey;  ///   来源单号		
            BfdaInfo.Externlineno = vmiInfo.Externlineno;          ///   行序号		
            BfdaInfo.Storerkey = vmiInfo.Storerkey;                ///   供应商代码	
            BfdaInfo.Sku = vmiInfo.Sku;                            ///  物料代码		
            BfdaInfo.Qtyexpected = vmiInfo.Qtyexpected.GetValueOrDefault().ToString(); ///  数量			
            BfdaInfo.Vmiwarehousecode = vmiInfo.Vmiwarehousecode;  ///   VMI 仓库代码	
            BfdaInfo.Werks = vmiInfo.Werks;                        ///   工厂代码

            return BfdaInfo;
        }
    }
}
