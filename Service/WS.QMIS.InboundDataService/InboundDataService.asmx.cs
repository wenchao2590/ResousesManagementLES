namespace WS.QMIS.InboundDataService
{
    using BLL.SYS;
    using Infrustructure.Data;
    using Infrustructure.Logging;
    using Infrustructure.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Transactions;
    using System.Web.Services;
    /// <summary>
    /// InboundDataService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class InboundDataService : WebService
    {
        #region Common
        /// <summary>
        /// 接口用户
        /// </summary>
        private string loginUser = "WS.QMIS.InboundDataService";
        /// <summary>
        /// 是否写入日志
        /// </summary>
        private string logFlag = ConfigurationManager.AppSettings["LogFlag"].ToLower();

        /// <summary>
        /// 获取MESSAGE的内容
        /// </summary>
        /// <param name="messageCode"></param>
        /// <returns></returns>
        private string GetMessage(ref string messageCode)
        {
            messageCode = GetMessageCode(messageCode);
            string nowLangue = System.Configuration.ConfigurationManager.AppSettings["messageLanguage"];
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
        /// QMIS-LES-001 QMIS检验模式接口  
        /// TI_IFM_QMIS_CHECK_MODE
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [WebMethod(Description = "QMIS检验模式接口")]
        public string GetQmisCheckMode(List<BFDAQmisCheckModeInfo> checkModeInfos)
        {
            ///回传文本
            InterfaceReturnInfo retInfo = new InterfaceReturnInfo();
            ///报文
            string msgContent = JsonHelper.ToJson(checkModeInfos);
            ///接口代码 
            string interfaceCode = "QMIS-LES-001";
            ///开始执行时间
            DateTime executeStartTime = DateTime.Now;
            try
            {
                
                ///LOG_FID
                Guid logFid = Guid.NewGuid();
                ///检验模式处理
                BFDAQmisCheckModeBLL bll = new BFDAQmisCheckModeBLL();
                ///关键字 ,号分隔
                string keyValue = bll.GetKeyValues(checkModeInfos);
                ///用分布式事务处理联动的修改
                using (TransactionScope trans = new TransactionScope())
                {
                    ///LOG日志
                    string logSql = BLL.LES.CommonBLL.GetCreateInboundLogSql("QMIS", logFid, interfaceCode, keyValue, msgContent, string.Empty, string.Empty, loginUser, executeStartTime);
                    ///添加到中间表
                    int dataCnt = bll.InsertListToCentreTable(checkModeInfos, logFid, logSql);
                    trans.Complete();
                }
                ///错误代码
                retInfo.MSGNO = string.Empty;
                string successCode = "1x00000051";///传输成功
                retInfo.MSG = GetMessage(ref successCode);
                retInfo.MSGNO = successCode;
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.RESULT = (int)ResultsDescribed.Succeed;
            }
            catch (Exception ex)
            {
                Log.WriteLogToFile(interfaceCode + "|" + ex.Message, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMddHHmm"));
                ///错误代码
                string errorCode = ex.Message;
                ///错误消息
                retInfo.MSG = GetMessage(ref errorCode);
                ///错误码
                retInfo.MSGNO = errorCode;
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.RESULT = (int)ResultsDescribed.Failure;
            }
            return new XmlWrapper().ObjectToXml(retInfo, false);
        }
        /// <summary>
        /// QMIS-LES-003 检验结果回传接口  
        /// </summary>
        /// <param name="checkResultInfos"></param>
        /// <returns></returns>
        [WebMethod(Description = "QMIS检验结果回传接口")]
        public string GetQmisCheckResult(List<BFDAQmisCheckResultInfo> checkResultInfos)
        {
            ///回传文本
            InterfaceReturnInfo retInfo = new InterfaceReturnInfo();
            string msgContent = JsonHelper.ToJson(checkResultInfos);
            Log.WriteLogToFile(logFlag, "QMIS-LES-003 \r\n" + msgContent.ToString() + "\r\n|Count|" + msgContent + " ;|", AppDomain.CurrentDomain.BaseDirectory + @"\Log\", DateTime.Now.ToString("yyyyMMddHHmm"));

            ///接口代码 
            string interfaceCode = "QMIS-LES-003";
            ///开始执行时间
            DateTime executeStartTime = DateTime.Now;
            try
            {
                ///LOG_FID
                Guid logFid = Guid.NewGuid();
                ///声明业务处理
                BFDAQmisCheckResultBLL bll = new BFDAQmisCheckResultBLL();
                ///关键字 ,号分隔
                string keyValue = bll.GetKeyValues(checkResultInfos);
                ///用分布式事务处理联动的修改
                using (TransactionScope trans = new TransactionScope())
                {
                    ///LOG日志
                    string logSql = BLL.LES.CommonBLL.GetCreateInboundLogSql("QMIS", logFid, interfaceCode, keyValue, msgContent, string.Empty, string.Empty, loginUser, executeStartTime);
                    ///添加到中间表
                    int dataCnt = bll.InsertListToCentreTable(checkResultInfos, logFid, logSql);
                    trans.Complete();
                }

                ///错误代码
                retInfo.MSGNO = string.Empty;
                ///传输成功
                string successCode = "1x00000051";
                ///错误消息
                retInfo.MSG = GetMessage(ref successCode);
                retInfo.MSGNO = successCode;
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.RESULT = (int)ResultsDescribed.Succeed;
            }
            catch (Exception ex)
            {
                Log.WriteLogToFile(interfaceCode + "|" + ex.Message, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));
                ///错误代码
                string errorCode = ex.Message;
                ///错误消息
                retInfo.MSG = GetMessage(ref errorCode);
                ///错误码
                retInfo.MSGNO = errorCode;
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.RESULT = (int)ResultsDescribed.Failure;
            }
            return new XmlWrapper().ObjectToXml(retInfo, false);
        }
    }
}
