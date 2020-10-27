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
    /// LES-WMS-010	库存锁定 LW007
    /// </summary>
    public class BFDAVmiPartStockLockBLL
    {

        /// <summary>
        /// 日期格式
        /// </summary>
        private static string vmiDateFormat = "yyyy-MM-dd HH:mm:ss";
        /// <summary>
        /// LES-WMS-010	库存锁定
        // <param name="logFid"></param>
        public static ExecuteResultConstants SendInventorLock(Guid logFid, InterfaceConfigInfo interfaceConfigInfo,
            ref string errorCode, ref string errorMsg, out string msgContent)
        {
            msgContent = string.Empty;
            List<WmsVmiPartStockLockInfo> vmiPartStockLockInfos = new WmsVmiPartStockLockBLL().GetList("[LOG_FID] = N'" + logFid + "' and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", string.Empty);
            if (vmiPartStockLockInfos.Count == 0)
            {
                errorCode = "MC:0x00000212";///经检测,传递数据为空,请确认
                return ExecuteResultConstants.Exception;
            }

            ///发送内容
            List<BFDAVmiPartStockLockInfo> bFDAVmiPartStockLockInfos = new List<BFDAVmiPartStockLockInfo>();
            foreach (var vmiPartStockLockInfo in vmiPartStockLockInfos)
            {
                bFDAVmiPartStockLockInfos.Add(GetBFDAVMIInfo(vmiPartStockLockInfo));
            }


            BFDAVMISendDataInfo<BFDAVmiPartStockLockInfo> sendDataInfo = new BFDAVMISendDataInfo<BFDAVmiPartStockLockInfo>();
            sendDataInfo.List = bFDAVmiPartStockLockInfos;
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
        /// <param name="BFDAInfo"></param>
        /// <returns></returns>
        private static string GetMsgContent(List<BFDAVmiPackageInboundInfo> BFDAInfo)
        {
            ///TODO:若数据量过大，其实建议取消
            return new XmlWrapper().ObjectToXml(BFDAInfo, false);
        }
        /// <summary>
        /// 对象转换
        /// </summary>
        /// <param name="wmsvmiInfo"></param>
        /// <returns></returns>
        private static BFDAVmiPartStockLockInfo GetBFDAVMIInfo(WmsVmiPartStockLockInfo wmsvmiInfo)
        {
            BFDAVmiPartStockLockInfo  BFDAInfo = new BFDAVmiPartStockLockInfo();
            ///TODO:此处获取时间默认值为0001-01-01，对方系统是否能够接收，待测
            BFDAInfo.PartNo = wmsvmiInfo.Partno;                           ///物料编号
            BFDAInfo.SupplierCode = wmsvmiInfo.Suppliercode;               ///供应商代码
            BFDAInfo.VmiWarehouseCode = wmsvmiInfo.Vmiwarehousecode;       ///VMI仓库代码
            BFDAInfo.PartQty = wmsvmiInfo.Partqty.GetValueOrDefault().ToString();              ///数量
            BFDAInfo.Orilockstatus = wmsvmiInfo.Orilockstatus;             ///源锁库状态
            BFDAInfo.Targetlockstatus = wmsvmiInfo.Targetlockstatus;       ///目标锁库状态
            BFDAInfo.Invstatus = wmsvmiInfo.Invstatus;                     ///库存状态
            BFDAInfo.Werks = wmsvmiInfo.Werks;                             ///工厂代码
            return BFDAInfo;
        }
    }
}
