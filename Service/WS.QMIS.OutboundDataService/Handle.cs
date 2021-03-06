﻿using BLL.LES;
using BLL.SYS;
using DM.LES;
using DM.SYS;
using Infrustructure.Logging;
using Infrustructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WS.QMIS.OutboundDataService
{
    public class Handle
    {
        /// <summary>
        /// 执行用户
        /// </summary>
        private string loginUser = "WS.QMIS.OutboundDataService";
        /// <summary>
        /// 目标系统代码
        /// </summary>
        private string targetSystem = "QMIS";
        /// <summary>
        /// Handler
        /// </summary>
        public void Handler()
        {
            List<InterfaceConfigInfo> interfaceConfigInfos = new InterfaceConfigBLL().GetList("[SYS_NAME] = N'" + targetSystem + "'", string.Empty);

            Log.WriteLogToFile("The  interfaceConfigInfos.cout|" + interfaceConfigInfos.Count, AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMddHHmm"));

            if (interfaceConfigInfos.Count == 0) return;
            ///发送消息前需要获取执行结果⑨为10.submit、60.submit的数据，逐条按methodCode④提交到不同的业务处理函数内
            List<QmisOutboundLogInfo> qmisOutboundLogInfos = new QmisOutboundLogBLL().GetListForUnsend();
            if (qmisOutboundLogInfos.Count == 0) return;
            foreach (QmisOutboundLogInfo srmOutboundInfo in qmisOutboundLogInfos)
            {

                Log.WriteLogToFile("The  srmOutboundInfo.MethodCode|" + srmOutboundInfo.MethodCode, AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMddHHmm"));

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
                    errorCode = "MC:3x00000021";///接口配置错误
                    executeResult = ExecuteResultConstants.Exception;
                }
                else
                {
                    ///
                    Log.WriteLogToFile("The  switch|" + srmOutboundInfo.MethodCode, AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMddHHmm"));
                    switch (srmOutboundInfo.MethodCode.ToLower())
                    {

                        ///LES-SRM-001 VMI供应商物料关系 post 形式发送数据
                        case "les-qmis-002": executeResult = BFDAQmisVmiAsnPullSheetBLL.SendQmisAsnPullSheet(srmOutboundInfo.Fid, interfaceConfigInfo, ref errorCode, ref errorMsg, out msgContent); break;
                        
                    }
                }                

                if (!string.IsNullOrEmpty(errorCode))
                    errorMsg = GetMessage(ref errorCode);
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
