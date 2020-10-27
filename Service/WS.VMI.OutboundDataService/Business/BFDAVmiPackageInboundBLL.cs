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
    /// LES-WMS-017	RDC器具入库 WL015
    /// </summary>
    public class BFDAVmiPackageInboundBLL
    {
       /// [TI_IFM_VMI_PackageInbound]
       /// 
        /// <summary>
        /// 日期格式
        /// </summary>
        private static string vmiDateFormat = "yyyy-MM-dd HH:mm:ss";
        /// <summary>
        ///  LES-WMS-017	RDC器具入库
        // <param name="logFid"></param>
        public static ExecuteResultConstants SendPackageInstorage(Guid logFid, InterfaceConfigInfo interfaceConfigInfo,
            ref string errorCode, ref string errorMsg, out string msgContent)
        {
            msgContent = string.Empty;
            List<WmsVmiPackageInboundInfo> vmiPackageInboundInfos = new WmsVmiPackageInboundBLL().GetList("[LOG_FID] = N'" + logFid + "' and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", string.Empty);
            if (vmiPackageInboundInfos.Count == 0)
            {
                errorCode = "MC:0x00000212";///经检测,传递数据为空,请确认
                return ExecuteResultConstants.Exception;
            }

            ///发送内容
            List<BFDAVmiPackageInboundInfo> bFDAVmiPackageInboundInfos = new List<BFDAVmiPackageInboundInfo>();
            foreach (var VmiPackageInboundInfo in vmiPackageInboundInfos)
            {
                bFDAVmiPackageInboundInfos.Add(GetBFDAVmiInfo(VmiPackageInboundInfo));
            }
            BFDAVMISendDataInfo<BFDAVmiPackageInboundInfo> sendDataInfo = new BFDAVMISendDataInfo<BFDAVmiPackageInboundInfo>();
            sendDataInfo.List = bFDAVmiPackageInboundInfos;
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
        /// 获取报文体
        /// </summary>
        /// <param name="inboundOrderInfos"></param>
        /// <returns></returns>
        private static string GetMsgContent(List<BFDAVmiPackageInboundInfo> inboundOrderInfos)
        {
            ///TODO:若数据量过大，其实建议取消
            return new XmlWrapper().ObjectToXml(inboundOrderInfos, false);
        }
        /// <summary>
        /// 对象转换
        /// </summary>
        /// <param name="vmiInfo"></param>
        /// <returns></returns>
        private static BFDAVmiPackageInboundInfo GetBFDAVmiInfo(WmsVmiPackageInboundInfo  wmsVmiPackageInboundInfo)
        {
            BFDAVmiPackageInboundInfo bFDAVmiPackageInboundInfo = new BFDAVmiPackageInboundInfo();
            ///TODO:此处获取时间默认值为0001-01-01，对方系统是否能够接收，待测
            bFDAVmiPackageInboundInfo.orderkey = wmsVmiPackageInboundInfo.Orderkey;     ///WMS 单号
            bFDAVmiPackageInboundInfo.orderlinenumber = wmsVmiPackageInboundInfo.Orderlinenumber;      ///行号
            bFDAVmiPackageInboundInfo.Storerkey = wmsVmiPackageInboundInfo.Storerkey;     ///供应商代码
            bFDAVmiPackageInboundInfo.Sku = wmsVmiPackageInboundInfo.Sku;      ///物料代码
            bFDAVmiPackageInboundInfo.Recqty = wmsVmiPackageInboundInfo.Recqty.GetValueOrDefault().ToString();    ///接收数量            
            bFDAVmiPackageInboundInfo.VmiWarehouseCode = wmsVmiPackageInboundInfo.Vmiwarehousecode;     ///VMI 仓库代
            bFDAVmiPackageInboundInfo.werks = wmsVmiPackageInboundInfo.Werks;     ///工厂代码
            return bFDAVmiPackageInboundInfo;
        }
    }
}
