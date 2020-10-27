using BLL.LES;
using BLL.SYS;
using DM.LES;
using DM.SYS;
using Infrustructure.Logging;
using Infrustructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WS.SAP.OutboundDataService.SAPWSDL;

namespace WS.SAP.OutboundDataService
{
    /// <summary>
    /// Handle
    /// </summary>
    public class Handle
    {

        #region Common

        /// <summary>
        /// 执行用户
        /// </summary>
        private string loginUser = "WS.SAP.OutboundDataService";

        /// <summary>
        /// 目标系统代码
        /// </summary>
        private string targetSystem = "SAP";

        /// <summary>
        /// 是否写入日志
        /// </summary>
        private string logFlag = AppSettings.GetConfigString("LogFlag");

        /// <summary>
        /// 获取MESSAGE的内容
        /// </summary>
        /// <param name="messageCode"></param>
        /// <returns></returns>
        private string GetMessage(ref string messageCode)
        {
            messageCode = GetMessageCode(messageCode);
            string nowLangue = AppSettings.GetConfigString("GetLanguage");
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
        #endregion



        /// <summary>
        /// Handler
        /// </summary>
        public void Handler()
        {
           
            List<InterfaceConfigInfo> interfaceConfigInfos = new InterfaceConfigBLL().GetList("[SYS_NAME] = N'" + targetSystem + "'", string.Empty);

            if (interfaceConfigInfos.Count == 0) return;
            ///发送消息前需要获取执行结果⑨为10.submit、60.submit的数据，逐条按methodCode④提交到不同的业务处理函数内
            List<SapOutboundLogInfo> sapOutboundLogInfos = new SapOutboundLogBLL().GetListForUnsend();
            if (sapOutboundLogInfos.Count == 0) return;
            foreach (SapOutboundLogInfo sapOutboundLogInfo in sapOutboundLogInfos)
            {
                

                ///业务处理函数开始处理之前需要根据logFid更新executeResult⑧为20.processing
                ///executeStartTime为当前数据库时间
                ///在BLL.LES.CommonBll提供UpdateProcessingLog函数，参数为Guid logFid
                BLL.LES.CommonBLL.UpdateProcessingLog(targetSystem, sapOutboundLogInfo.Id, loginUser);
                ///处理结果
                ExecuteResultConstants executeResult = ExecuteResultConstants.Processing;
                ///错误代码
                string errorCode = string.Empty;
                ///报文内容
                string msgContent = null;
                ///错误消息
                string errorMsg = string.Empty;
               
                InterfaceConfigInfo interfaceConfigInfo = interfaceConfigInfos.FirstOrDefault(d => d.InterfaceCode.ToLower() == sapOutboundLogInfo.MethodCode.ToLower());
                if (interfaceConfigInfo == null)
                {
                    errorCode = "MC:3x00000021";///接口配置错误
                    executeResult = ExecuteResultConstants.Exception;
                }
                else
                {
                  
                    ///
                    switch (sapOutboundLogInfo.MethodCode.ToLower().Trim())
                    {
                        ///物料移动数据发送至SAP  TI_IFM_SAP_TRAN_OUT
                        case "les-sap-009": executeResult = BFDAMaterialMoveBLL.SendMaterialMoveData(sapOutboundLogInfo.Fid, interfaceConfigInfo, ref errorCode, ref errorMsg, out msgContent); break;
                        ///LES-SAP-017 缺件影响生产订单范围 TI_IFM_SAP_PRODUCTION_ORDER_LACK_MATERIAL
                        case "les-sap-017": executeResult= BFDAProductionOrderLackMaterialBLL.SendLackMaterialData(sapOutboundLogInfo.Fid, interfaceConfigInfo, ref errorCode, ref errorMsg, out msgContent); break;
                        ///LES-SAP-015	断点替换记录  TI_IFM_SAP_BREAKPOINT_REPLACE
                        case "les-sap-015": executeResult= BFDASAPBreakpointReplaceBLL.SendBreakpointReplaceData(sapOutboundLogInfo.Fid, interfaceConfigInfo, ref errorCode, ref errorMsg, out msgContent); break;
                        ///LES-SAP-012  发送盘点报告 TI_IFM_SAP_INVENTORY_CHECK_REPORT
                        case "les-sap-012": executeResult = BFDASAPInventoryCheckReportBLL.SendInventoryCheckReportData(sapOutboundLogInfo.Fid, interfaceConfigInfo, ref errorCode, ref errorMsg, out msgContent); break;
                    }
                }
                if (!string.IsNullOrEmpty(errorCode))
                    errorMsg = GetMessage(ref errorCode);
                ///更新任务状态
                BLL.LES.CommonBLL.UpdateResultLog(targetSystem, sapOutboundLogInfo.Id, executeResult, msgContent, errorCode, errorMsg, loginUser);
            }
        }
        
    }
}
