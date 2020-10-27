namespace WS.SRM.OutboundDataService
{
    using BLL.LES;
    using BLL.SYS;
    using DM.LES;
    using DM.SYS;
    using Infrustructure.Logging;
    using Infrustructure.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class Handle
    {
        #region Common
        /// <summary>
        /// 执行用户
        /// </summary>
        private string loginUser = "WS.SRM.OutboundDataService";
        /// <summary>
        /// 目标系统代码
        /// </summary>
        private string targetSystem = "SRM";

        /// <summary>
        /// 是否写入日志
        /// </summary>
        private static string logFlag = AppSettings.GetConfigString("LogFlag");
        #endregion
        

        /// <summary>
        /// Handler
        /// </summary>
        public void Handler()
        {
            List<InterfaceConfigInfo> interfaceConfigInfos = new InterfaceConfigBLL().GetList("[SYS_NAME] = N'" + targetSystem + "'", string.Empty);
            if (interfaceConfigInfos.Count == 0) return;
            ///发送消息前需要获取执行结果⑨为10.submit、60.submit的数据，逐条按methodCode④提交到不同的业务处理函数内
            List<SrmOutboundLogInfo> vmiOutboundLogInfos = new SrmOutboundLogBLL().GetListForUnsend();
            if (vmiOutboundLogInfos.Count == 0) return;
            foreach (SrmOutboundLogInfo srmOutboundInfo in vmiOutboundLogInfos)
            {
                ///业务处理函数开始处理之前需要根据logFid更新executeResult⑧为20.processing
                ///executeStartTime为当前数据库时间
                ///在BLL.LES.CommonBll提供UpdateProcessingLog函数，参数为Guid logFid
                BLL.LES.CommonBLL.UpdateProcessingLog(targetSystem, srmOutboundInfo.Id, loginUser);
                ///处理结果
                ExecuteResultConstants executeResult = ExecuteResultConstants.Processing;
                ///错误代码
                string errorCode = string.Empty;
                ///报文内容
                string msgContent = null;
                ///错误消息
                string errorMsg = string.Empty;
                InterfaceConfigInfo interfaceConfigInfo = interfaceConfigInfos.FirstOrDefault(d => d.InterfaceCode.ToLower() == srmOutboundInfo.MethodCode.ToLower());
                if (interfaceConfigInfo == null)
                {
                    executeResult = ExecuteResultConstants.Exception;
                    errorCode = "3x00000021";///接口配置错误
                    errorMsg = GetMessage(ref errorCode);
                    BLL.LES.CommonBLL.UpdateResultLog(targetSystem, srmOutboundInfo.Id, executeResult, msgContent, errorCode, errorMsg, loginUser);
                    continue;
                }
                ///
                switch (srmOutboundInfo.MethodCode.ToLower())
                {
                    ///LES-SRM-001 VMI供应商物料关系 √
                    case "les-srm-001": executeResult = BFDAVmiSupplierPartBLL.Send(srmOutboundInfo.Fid.GetValueOrDefault(), interfaceConfigInfo, ref errorCode, ref errorMsg, out msgContent); break;
                    ///LES-SRM-002	供货计划 √   TI_IFM_SRM_SUPPLY_PLAN
                    case "les-srm-002": executeResult = BFDASupplyPlanBLL.Send(srmOutboundInfo.Fid.GetValueOrDefault(), interfaceConfigInfo, ref errorCode, ref errorMsg, out msgContent); break;

                    ///LES-WMS-004 JIS 拉动单 (排序拉动单) √ TI_IFM_SRM_JIS_PULL_ORDER
                    case "les-srm-004": executeResult = BFDAJisPullOrderBLL.Send(srmOutboundInfo.Fid.GetValueOrDefault(), interfaceConfigInfo, ref errorCode, ref errorMsg, out msgContent); break;
                    
                    /// LES-SRM-005 物料拉动单  √ TI_IFM_SRM_PULLING_ORDER
                    case "les-srm-005": executeResult = BFDAPullingOrderBLL.Send(srmOutboundInfo.Fid.GetValueOrDefault(), interfaceConfigInfo, ref errorCode, ref errorMsg, out msgContent); break;

                    ///LES-SRM-008	入库数据 √ TI_IFM_SRM_TRAN_OUT
                    case "les-srm-008": executeResult = BFDATranOutBLL.Send(srmOutboundInfo.Fid.GetValueOrDefault(), interfaceConfigInfo, ref errorCode, ref errorMsg, out msgContent); break;

                    /// LES-SRM-009 物料退货单 TI_IFM_SRM_PART_RETURN_SHEET
                    case "les-srm-009": executeResult = BFDAPartReturnSheetBLL.Send(srmOutboundInfo.Fid.GetValueOrDefault(), interfaceConfigInfo, ref errorCode, ref errorMsg, out msgContent); break;
                }
                errorMsg = GetMessage(ref errorCode);

                Log.WriteLogToFile(logFlag, srmOutboundInfo.MethodCode+":Handler:msgContent:" + msgContent, AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMddHH"));

                ///更新任务状态
                BLL.LES.CommonBLL.UpdateResultLog(targetSystem, srmOutboundInfo.Id, executeResult, msgContent, errorCode, errorMsg, loginUser);
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
