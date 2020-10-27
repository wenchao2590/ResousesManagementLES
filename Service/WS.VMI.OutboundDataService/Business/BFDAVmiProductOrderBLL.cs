namespace WS.VMI.OutboundDataService
{
    using BLL.LES;
    using DM.LES;
    using DM.SYS;
    using Infrustructure.Utilities;
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using WS.VMI.OutboundDataService.VMIWSDL;
    /// <summary>
    /// LES-WMS-012 生产订单 LW008
    /// </summary>
    public class BFDAVmiProductOrderBLL
    {
        /// <summary>
        /// 日期格式
        /// </summary>
        private static string vmiDateFormat = "yyyy-MM-dd HH:mm:ss";
        /// <summary>
        /// LES-WMS-012 生产订单 LW008
        /// </summary>
        /// <param name="logFid"></param>
        public static ExecuteResultConstants Send(Guid logFid, InterfaceConfigInfo interfaceConfigInfo, ref string errorCode, ref string errorMsg, out string msgContent)
        {
            msgContent = string.Empty;
            List<WmsVmiProductOrderInfo> wmsVmiProductOrderInfos = new WmsVmiProductOrderBLL().GetList("[LOG_FID] = N'" + logFid + "' and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", string.Empty);
            if (wmsVmiProductOrderInfos.Count == 0)
            {
                errorCode = "MC:0x00000212";///经检测,传递数据为空,请确认
                return ExecuteResultConstants.Exception;
            }
            ///转换发送内容
            List<BFDAVmiProductOrderInfo> list = new List<BFDAVmiProductOrderInfo>();
            foreach (var wmsVmiProductOrderInfo in wmsVmiProductOrderInfos)
            {
                list.Add(GetWmsVmiProductOrderInfo(wmsVmiProductOrderInfo));
            }
            BFDAVMISendDataInfo<BFDAVmiProductOrderInfo> sendDataInfo = new BFDAVMISendDataInfo<BFDAVmiProductOrderInfo>();
            sendDataInfo.List = list;
            ///
            WsProcessServiceClient client = new WsProcessServiceClient();
            client.Endpoint.Address = new EndpointAddress(interfaceConfigInfo.CallUrl);
            ///1标识成功、0标识失败
            long tenantId = 89;
            if (!long.TryParse(interfaceConfigInfo.Param2, out tenantId))
            {
                errorCode = "MC:3x00000021";        ///接口配置错误
                return ExecuteResultConstants.Exception;
            }
            ///数据发送
            msgContent = new XmlWrapper().ObjectToXml(sendDataInfo, false);
            string result = client.runProcessWithAction(interfaceConfigInfo.Param1, tenantId, interfaceConfigInfo.SysMethodName, msgContent);
            BFDAVMIResultInfo resultInfo = new XmlWrapper(result, LoadType.FromString).XmlToObject("/Result", typeof(BFDAVMIResultInfo)) as BFDAVMIResultInfo;
            ///成功后更新中间表数据处理状态
            if (resultInfo.Status.ToLower() == "error")
            {
                errorCode = resultInfo.ErrorCode;
                errorMsg = resultInfo.ErrorMsg;
                return ExecuteResultConstants.Error;
            }
            return ExecuteResultConstants.Success;
        }

        /// <summary>
        /// 对象转换
        /// </summary>
        /// <param name="vmiInboundOrderInfo"></param>
        /// <returns></returns>
        public static BFDAVmiProductOrderInfo GetWmsVmiProductOrderInfo(WmsVmiProductOrderInfo wmsVmiProductOrderInfo)
        {
            BFDAVmiProductOrderInfo info = new BFDAVmiProductOrderInfo();
            ///订单编号
            info.OrderNo = wmsVmiProductOrderInfo.OrderNo;
            ///物料编号
            info.Part_No = wmsVmiProductOrderInfo.PartNo;
            ///订单日期
            info.OrderDate = wmsVmiProductOrderInfo.OrderDate.GetValueOrDefault().ToString(vmiDateFormat);
            ///生产线
            info.AssemblyLine = wmsVmiProductOrderInfo.AssemblyLine;
            ///数量
            info.Qty = wmsVmiProductOrderInfo.Qty.ToString();
            ///上线时间
            info.OnLineTime = wmsVmiProductOrderInfo.OnlineTime == null ? string.Empty : wmsVmiProductOrderInfo.OnlineTime.GetValueOrDefault().ToString(vmiDateFormat);
            ///下线时间
            info.DownLineTime = wmsVmiProductOrderInfo.DownLineTime == null ? string.Empty : wmsVmiProductOrderInfo.DownLineTime.GetValueOrDefault().ToString(vmiDateFormat);
            ///整车颜色
            info.ModelYear = wmsVmiProductOrderInfo.ModelYear;
            ///锁定标识
            info.LockFlag = wmsVmiProductOrderInfo.LockFlag.ToString();
            ///顺序
            info.SEQ = wmsVmiProductOrderInfo.Seq.GetValueOrDefault().ToString();
            ///工厂代码
            info.Werks = wmsVmiProductOrderInfo.Werks;
            return info;
        }
    }
}
