namespace WS.SRM.InboundDataService
{
    using BLL.SYS;
    using DM.SYS;
    using Infrustructure.Logging;
    using System;
    using System.Collections.Generic;
    using System.Web.Services;
    using System.Configuration;
    using Infrustructure.Data;

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
        /// 操作用户
        /// </summary>
        private string loginUser = "WS.SRM.InboundDataService";
        /// <summary>
        /// 获取MESSAGE的内容
        /// </summary>
        /// <param name="messageCode"></param>
        /// <returns></returns>
        private string GetMessage(ref string messageCode)
        {
            if (string.IsNullOrEmpty(messageCode)) return string.Empty;
            messageCode = GetMessageCode(messageCode);
            string nowLangue = ConfigurationManager.AppSettings["messageLanguage"];
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
        /// SRM-LES-003 物料发货单 
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "SRM-LES-003 - 物料发货单")]
        public InterfaceReturnInfo GetVmiDeliveryNote(List<BFDASrmShippingNoteInfo> bFDASrmShippingNoteInfos)
        {
            ///回传报文
            InterfaceReturnInfo retInfo = new InterfaceReturnInfo();
            ///开始执行时间
            DateTime executeStartTime = DateTime.Now;
            ///接口代码
            string interfaceCode = "SRM-LES-003";
            ///中间表处理集合
            BFDASrmVmiShippingNoteBLL bll = new BFDASrmVmiShippingNoteBLL();
            try
            {
                ///转报文
                string msgContent = JsonHelper.ToJson(bFDASrmShippingNoteInfos);
                ///获取传值内容,写在本地txt文本中
                Log.WriteLogToFile("|SRM-LES-003 - 物料发货单 日志记录| \r\n" + msgContent.ToString() + "\r\n|Count|" + bFDASrmShippingNoteInfos.Count.ToString().Length + " ; End|", AppDomain.CurrentDomain.BaseDirectory + @"\Log\", DateTime.Now.ToString("yyyyMMddHHmm"));
                ///获取关键词
                string keyValue = bll.GetKeyValues(bFDASrmShippingNoteInfos);
                ///LOG日志
                ///日志ID
                Guid logFid = Guid.NewGuid();
                string logSql = BLL.LES.CommonBLL.GetCreateInboundLogSql("SRM", logFid, interfaceCode, keyValue, msgContent, string.Empty, string.Empty, loginUser, executeStartTime);
                ///添加到中间表
                int dataCnt = bll.InsertListToCentreTable(bFDASrmShippingNoteInfos, logFid, logSql);
                ///1成功、0失败
                retInfo.ExecuteResult = (int)ResultsDescribed.Succeed;
                ///错误编码
                retInfo.ErrorCode = string.Empty;
                ///错误消息
                string successCode = "1x00000051";///传输成功
                retInfo.MessageContent = GetMessage(ref successCode);
            }
            catch (Exception ex)
            {
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure;
                ///错误编码
                string errorCode = ex.Message;
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                retInfo.ErrorCode = errorCode;
                ///
                Log.WriteLogToFile(interfaceCode + "|" + retInfo.MessageContent + "|" + retInfo.ErrorCode, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));
            }
            return retInfo;
        }
        /// <summary>
        ///  LES-SRM-006 物料送货单
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [WebMethod(Description = "SRM-LES-006 物料送货单")]
        public InterfaceReturnInfo GetAsnOrder(List<BFDASrmVmiDeliveryNoteInfo>  bFDASrmVmiDeliveryNoteInfos)
        {
            /////接口代码 
            string interfaceCode = "LES-SRM-006";
            ///回传文本
            InterfaceReturnInfo retInfo = new InterfaceReturnInfo();
            ///开始执行时间
            DateTime executeStartTime = DateTime.Now;


            if (bFDASrmVmiDeliveryNoteInfos.Count == 0)
            {
                string errorCode = "0x00000463";
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                retInfo.ErrorCode = errorCode;
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure;
                return retInfo;
            }

            ///日志ID
            Guid logFid = Guid.NewGuid();
            ///中间表处理集合
            BFDASrmAsnRunsheetBLL bll = new BFDASrmAsnRunsheetBLL();
            try
            {
                ///转报文
                string msgContent = JsonHelper.ToJson(bFDASrmVmiDeliveryNoteInfos);
                ///获取传值内容,写在本地txt文本中
                Log.WriteLogToFile("|WMS-LES-006  物料送货单 日志记录| \r\n" + msgContent.ToString() + "\r\n|Count|" + bFDASrmVmiDeliveryNoteInfos.Count.ToString().Length + "; End|", AppDomain.CurrentDomain.BaseDirectory + @"\Log\", DateTime.Now.ToString("yyyyMMddHHmm"));
                ///获取关键词
                string keyValue = bll.GetKeyValues(bFDASrmVmiDeliveryNoteInfos);
                ///LOG日志
                string logSql = BLL.LES.CommonBLL.GetCreateInboundLogSql("SRM", logFid, interfaceCode, keyValue, msgContent, string.Empty, string.Empty, loginUser, executeStartTime);
                ///添加到中间表
                int dataCnt = bll.InsertListToCentreTable(bFDASrmVmiDeliveryNoteInfos, logFid, logSql);
                ///1成功、0失败
                retInfo.ExecuteResult = (int)ResultsDescribed.Succeed;
                ///错误编码
                retInfo.ErrorCode = string.Empty;
                ///描述信息
                string successCode = "1x00000051";///传输成功
                retInfo.MessageContent = GetMessage(ref successCode);
            }
            catch (Exception ex)
            {
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure;
                ///错误代码
                string errorCode = ex.Message;
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                retInfo.ErrorCode = errorCode;
                ///
                Log.WriteLogToFile(interfaceCode + "|" + retInfo.MessageContent + "|" + retInfo.ErrorCode, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));
            }
            return retInfo;
        }
        /// <summary>
        /// SRM-LES-007 箱标签 
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "SRM-LES-007 箱标签")]
        public InterfaceReturnInfo GetBarcodes(List<BFDASrmBarcodeInfo> bFDASrmBarcodeInfos)
        {
            ///返回接口
            InterfaceReturnInfo retInfo = new InterfaceReturnInfo();
            ///接口代码
            string interfaceCode = "SRM-LES-007";
            ///开始执行时间
            DateTime executeStartTime = DateTime.Now;
            ///日志ID
            Guid logFid = Guid.NewGuid();
            ///中间表处理集合
            BFDASrmBarcodeBLL bll = new BFDASrmBarcodeBLL();
            try
            {
                ///转报文
                string msgContent = JsonHelper.ToJson(bFDASrmBarcodeInfos);
                ///获取传值内容,写在本地txt文本中
                Log.WriteLogToFile("|SRM-LES-007箱标签 日志记录| \r\n" + msgContent.ToString() + "\r\n|Count|" + bFDASrmBarcodeInfos.Count.ToString().Length + "; End|", AppDomain.CurrentDomain.BaseDirectory + @"\Log\", DateTime.Now.ToString("yyyyMMddHHmm"));
                ///获取关键词
                string keyValue = bll.GetKeyValues(bFDASrmBarcodeInfos);
                ///LOG日志
                string logSql = BLL.LES.CommonBLL.GetCreateInboundLogSql("SRM", logFid, interfaceCode, keyValue, msgContent, string.Empty, string.Empty, loginUser, executeStartTime);
                ///添加到中间表
                int dataCnt = bll.InsertListToCentreTable(bFDASrmBarcodeInfos, logFid, logSql);
                ///1成功、0失败
                retInfo.ExecuteResult = (int)ResultsDescribed.Succeed;
                ///错误代码
                retInfo.ErrorCode = string.Empty;
                ///描述信息
                string successCode = "1x00000051";///传输成功
                retInfo.MessageContent = GetMessage(ref successCode);
            }
            catch (Exception ex)
            {
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure;
                ///错误代码
                string errorCode = ex.Message;
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                retInfo.ErrorCode = errorCode;
                ///
                Log.WriteLogToFile(interfaceCode + "|" + retInfo.MessageContent + "|" + retInfo.ErrorCode, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));
            }
            return retInfo;
        }
    }
}
