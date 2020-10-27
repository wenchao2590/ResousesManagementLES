namespace WS.VMI.OutboundDataService
{
    using BLL.LES;
    using BLL.SYS;
    using DM.LES;
    using DM.SYS;
    using Infrustructure.Utilities;
    using System.Collections.Generic;
    using System.Linq;
    public class Handle
    {
        /// <summary>
        /// 执行用户
        /// </summary>
        private string loginUser = "WS.VMI.OutboundDataService";
        /// <summary>
        /// 目标系统代码
        /// </summary>
        private string targetSystem = "VMI";
        /// <summary>
        /// Handler
        /// </summary>
        public void Handler()
        {
            List<InterfaceConfigInfo> interfaceConfigInfos = new InterfaceConfigBLL().GetList("[SYS_NAME] = N'" + targetSystem + "'", string.Empty);
            if (interfaceConfigInfos.Count == 0) return;
            ///发送消息前需要获取执行结果⑨为10.submit、60.submit的数据，逐条按methodCode④提交到不同的业务处理函数内
            List<VmiOutboundLogInfo> vmiOutboundLogInfos = new VmiOutboundLogBLL().GetListForUnsend();
            if (vmiOutboundLogInfos.Count == 0) return;
            foreach (VmiOutboundLogInfo vmiOutboundLogInfo in vmiOutboundLogInfos)
            {
                ///业务处理函数开始处理之前需要根据logFid更新executeResult⑧为20.processing
                ///executeStartTime为当前数据库时间
                ///在BLL.LES.CommonBll提供UpdateProcessingLog函数，参数为Guid logFid
                BLL.LES.CommonBLL.UpdateProcessingLog(targetSystem, vmiOutboundLogInfo.Id, loginUser);
                ///处理结果
                ExecuteResultConstants executeResult = ExecuteResultConstants.Processing;
                ///错误代码
                string errorCode = string.Empty;
                ///错误消息
                string errorMsg = string.Empty;
                ///报文内容
                string msgContent = string.Empty;
                InterfaceConfigInfo interfaceConfigInfo = interfaceConfigInfos.FirstOrDefault(d => d.InterfaceCode.Trim().ToLower() == vmiOutboundLogInfo.MethodCode.Trim().ToLower());
                if (interfaceConfigInfo == null)
                {
                    errorCode = "MC:3x00000021";///接口配置错误
                    errorMsg = GetMessage(ref errorCode);
                    executeResult = ExecuteResultConstants.Exception;
                    BLL.LES.CommonBLL.UpdateResultLog(targetSystem, vmiOutboundLogInfo.Id, executeResult, msgContent, errorCode, errorMsg, loginUser);
                    continue;
                }
                switch (vmiOutboundLogInfo.MethodCode.ToLower())
                {
                    ///les-wms-001 物料发货单 (又名供应商发货单) LW001
                    case "les-wms-001": executeResult = BFDAInboundOrderBLL.SendInboundOrder(vmiOutboundLogInfo.Fid, interfaceConfigInfo, ref errorCode, ref errorMsg, out msgContent); break;

                    ///les-wms-003-物料拉动单 LW002
                    case "les-wms-003": executeResult = BFDAVmiPullingOrderBLL.SendPullingOrder(vmiOutboundLogInfo.Fid, interfaceConfigInfo, ref errorCode, ref errorMsg, out msgContent); break;

                    ///les-wms-004 VMI JIS 拉动单 (排序拉动单) LW003
                    case "les-wms-004": executeResult = BFDAVmiJisPullOrderBLL.SendJisPullOrder(vmiOutboundLogInfo.Fid, interfaceConfigInfo, ref errorCode, ref errorMsg, out msgContent); break;

                    ///LES-WMS-005	入库数据 WL004
                    case "les-wms-005": executeResult = BFDAVmiInboundDataBLL.SendInboundOrder(vmiOutboundLogInfo.Fid, interfaceConfigInfo, ref errorCode, ref errorMsg, out msgContent); break;

                    ///les-wms-008	VMI供应商物料关系 LW005
                    case "les-wms-008": executeResult = BFDAVmiSupplierPartBLL.SendSupplierMaterialRelationship(vmiOutboundLogInfo.Fid, interfaceConfigInfo, ref errorCode, ref errorMsg, out msgContent); break;

                    ///les-wms-009 供货计划 LW006
                    case "les-wms-009": executeResult = BFDAVmiSupplyPlanBLL.SendVmiSupplyPlan(vmiOutboundLogInfo.Fid, interfaceConfigInfo, ref errorCode, ref errorMsg, out msgContent); break;

                    ///les-wms-010	库存锁定 LW007
                    case "les-wms-010": executeResult = BFDAVmiPartStockLockBLL.SendInventorLock(vmiOutboundLogInfo.Fid, interfaceConfigInfo, ref errorCode, ref errorMsg, out msgContent); break;

                    ///les-wms-011	供应商基础数据(可能不做)

                    ///les-wms-012 生产订单 LW008 √
                    case "les-wms-012": executeResult = BFDAVmiProductOrderBLL.Send(vmiOutboundLogInfo.Fid, interfaceConfigInfo, ref errorCode, ref errorMsg, out msgContent); break;

                    ///les-wms-015 器具基础数据-WL014
                    case "les-wms-015": executeResult = BFDAVmiBasicSpackageBLL.SendBasicSpackage(vmiOutboundLogInfo.Fid, interfaceConfigInfo, ref errorCode, ref errorMsg, out msgContent); break;

                    ///les-wms-017	RDC器具入库 LW016
                    case "les-wms-017": executeResult = BFDAVmiPackageInboundBLL.SendPackageInstorage(vmiOutboundLogInfo.Fid, interfaceConfigInfo, ref errorCode, ref errorMsg, out msgContent); break;

                    ///les-wms-018	RDC器具出库 LW017
                    case "les-wms-018": executeResult = BFDAVmiPackageOutboundBLL.SendPackageOutBound(vmiOutboundLogInfo.Fid, interfaceConfigInfo, ref errorCode, ref errorMsg, out msgContent); break;

                }
                errorMsg = GetMessage(ref errorCode);
                ///更新任务状态
                BLL.LES.CommonBLL.UpdateResultLog(targetSystem, vmiOutboundLogInfo.Id, executeResult, msgContent, errorCode, errorMsg, loginUser);
            }
        }
        /// <summary>
        /// 获取MESSAGE的内容
        /// </summary>
        /// <param name="messageCode"></param>
        /// <returns></returns>
        private string GetMessage(ref string messageCode)
        {
            if (string.IsNullOrEmpty(messageCode)) return string.Empty;
            messageCode = GetMessageCode(messageCode);
            string nowLangue = AppSettings.GetConfigString("messageLanguage");
            if (!string.IsNullOrEmpty(nowLangue)) nowLangue = "zh-cn";
            return new MessageBLL().GetMessage(nowLangue, messageCode);
        }
        /// <summary>
        /// 获取错误代码
        /// </summary>
        /// <param name="messageCode"></param>
        /// <returns></returns>
        private string GetMessageCode(string messageCode)
        {
            if (string.IsNullOrEmpty(messageCode))
                return "Err_:ERROR";
            if (messageCode.StartsWith("Err_:"))
                messageCode = messageCode.Replace("Err_:", string.Empty);
            if (messageCode.StartsWith("MC:"))
                messageCode = messageCode.Replace("MC:", string.Empty);
            return messageCode;
        }
    }
}
