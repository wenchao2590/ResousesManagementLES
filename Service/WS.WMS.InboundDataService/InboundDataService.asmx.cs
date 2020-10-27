using BLL.SYS;
using DM.SYS;
using Infrustructure.Data;
using Infrustructure.Logging;
using Infrustructure.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Services;


namespace WS.VMI.InboundDataService
{
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
        private string loginUser = "WS.VMI.InboundDataService";

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
        /// WMS-LES-002-VMI.WMS.出入库事物数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [WebMethod(Description = "WMS-LES-002-VMI.WMS.出入库事物数据")]
        public InterfaceReturnInfo GetTranDetails(List<BFDAVmiTranDetailsInfo> list)
        {
            /////接口代码 
            string interfaceCode = "WMS-LES-002";
            ///开始执行时间
            DateTime executeStartTime = DateTime.Now;
            ///日志ID
            Guid logFid = Guid.NewGuid();
            ///中间表处理集合
            BFDAVmiTranDetailsBLL bll = new BFDAVmiTranDetailsBLL();
            ///回传文本
            InterfaceReturnInfo retInfo = new InterfaceReturnInfo();
            try
            {
                ///转报文
                string msgContent = JsonHelper.ToJson(list);
                ///获取传值内容,写在本地txt文本中
                Log.WriteLogToFile(msgContent, AppDomain.CurrentDomain.BaseDirectory + @"\ContentLog\", DateTime.Now.ToString("yyyyMMddHHmm"));
                ///获取关键词
                string keyValue = bll.GetKeyValues(list);
                ///LOG日志
                string logSql = BLL.LES.CommonBLL.GetCreateInboundLogSql("VMI", logFid, interfaceCode, keyValue, msgContent, string.Empty, string.Empty, loginUser, executeStartTime);
                ///添加到中间表
                int dataCnt = bll.InsertListToCentreTable(list, logFid, logSql);
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
                Log.WriteLogToFile(interfaceCode + "|" + retInfo.MessageContent + "|" + retInfo.ErrorCode, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));
                ///1成功、0失败
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure;
                ///错误代码
                string errorCode = ex.Message;
                retInfo.ErrorCode = errorCode;
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
            }
            return retInfo;
        }



        /// <summary>
        ///  WMS-LES-006 VMI.WMS.物料送货单 Finish
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [WebMethod(Description = "WMS-LES-006 VMI.WMS.物料送货单 WL011")]
        public InterfaceReturnInfo GetAsnOrder(List<BFDAVmiAsnRunsheetInfo> list)
        {
            /////接口代码 
            string interfaceCode = "WMS-LES-006";
            ///开始执行时间
            DateTime executeStartTime = DateTime.Now;
            ///日志ID
            Guid logFid = Guid.NewGuid();
            ///中间表处理集合         
            BFDAVmiAsnRunsheetBLL bll = new BFDAVmiAsnRunsheetBLL();

            InterfaceReturnInfo retInfo = new InterfaceReturnInfo();
            try
            {
                ///转报文
                string msgContent = JsonHelper.ToJson(list);
                ///获取传值内容,写在本地txt文本中
                Log.WriteLogToFile("|WMS-LES-006 VMI.WMS.物料送货单 日志记录| \r\n" + msgContent.ToString() + "\r\n|Count|" + list.Count.ToString().Length + "; End|", AppDomain.CurrentDomain.BaseDirectory + @"\Log\", DateTime.Now.ToString("yyyyMMddHHmm"));

                ///关键词
                string keyValue = bll.GetKeyValues(list);
                ///数据执行条数
                int dataCnt = 0;
                ///分布式事务 执行写入数据库操作. 
                using (TransactionScope tran = new TransactionScope())
                {
                    ///LOG日志
                    string logSql = BLL.LES.CommonBLL.GetCreateInboundLogSql("VMI", logFid, interfaceCode, keyValue, msgContent, string.Empty, string.Empty, loginUser, executeStartTime);
                    ///添加到中间表
                    dataCnt = bll.InsertListToCentreTable(list, logFid, logSql);
                    tran.Complete();
                }
                ///1成功、0失败
                retInfo.ExecuteResult = (int)ResultsDescribed.Succeed;
                ///错误编码
                retInfo.ErrorCode = string.Empty;
                string successCode = "1x00000051";///传输成功
                //描述信息
                retInfo.MessageContent = GetMessage(ref successCode);
            }
            catch (Exception ex)
            {
                Log.WriteLogToFile(interfaceCode + "|" + retInfo.MessageContent + "|" + retInfo.ErrorCode, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));

                ///1成功、0失败
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure; ;
                ///
                string errorCode = ex.Message;
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                ///错误代码
                retInfo.ErrorCode = errorCode;

            }
            return retInfo;
        }

        /// <summary>
        /// WMS-LES-007 VMI.WMS.箱标签接受  --Finish
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [WebMethod(Description = "WMS-LES-007-VMI.WMS.箱标签")]
        public InterfaceReturnInfo GetBarcodes(List<BFDAVmiBarcodeInfo> list)
        {
            /////接口代码 
            string interfaceCode = "WMS-LES-007";
            ///回传文本
            InterfaceReturnInfo retInfo = new InterfaceReturnInfo();

            ///消息体
            string msgContent = JsonHelper.ToJson(list);
            msgContent = msgContent.Replace("'", "''");     ///过滤掉容易引发错误的单引号
            Log.WriteLogToFile(msgContent + "End|", AppDomain.CurrentDomain.BaseDirectory + @"\Log\", DateTime.Now.ToString("yyyyMMddHHmm"));
            ///开始执行时间
            DateTime executeStartTime = DateTime.Now;
            try
            {
                ///LOG_FID
                Guid logFid = Guid.NewGuid();
                ///箱标签操作类
                BFDAVmiWmsBarcodeBLL bll = new BFDAVmiWmsBarcodeBLL();
                ///关键字 ,号分隔
                string keyValue = bll.GetKeyValues(list);
                ///执行SQL返回数量
                int dataCnt = 0;
                ///用分布式事务处理联动的修改
                using (TransactionScope trans = new TransactionScope())
                {
                    ///LOG日志
                    string logSql = BLL.LES.CommonBLL.GetCreateInboundLogSql("VMI", logFid, interfaceCode, keyValue, msgContent, string.Empty, string.Empty, loginUser, executeStartTime);
                    ///添加到中间表
                    dataCnt = bll.InsertListToCentreTable(list, logFid, logSql);
                    trans.Complete();
                }
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
                Log.WriteLogToFile(interfaceCode + "|" + ex.Message + "|异常来源" + ex.Source + "|异常方法:" + ex.TargetSite + "|详细:" + ex.StackTrace, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));

                ///1成功、0失败
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure; ;
                ///
                string errorCode = ex.Message;
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                ///错误代码
                retInfo.ErrorCode = errorCode;
            }
            return retInfo;
        }

        /// <summary>
        ///  WMS-LES-013 VMI.WMS.在途转可用库存    Finish
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [WebMethod(Description = "WMS-LES-013 VMI.WMS.在途转可用库存")]
        public InterfaceReturnInfo SetOnroadToStock(List<BFDAVmiTransitToInventoryInfo> list)
        {
            /////接口代码 
            string interfaceCode = "WMS-LES-013";
            ///回传文本
            InterfaceReturnInfo retInfo = new InterfaceReturnInfo();

            BFDAVmiTransitToInventoryBLL bll = new BFDAVmiTransitToInventoryBLL();

            ///开始执行时间
            DateTime executeStartTime = DateTime.Now;
            ///LOG_FID
            Guid logFid = Guid.NewGuid();

            try
            {
                ///转报文
                string msgContent = JsonHelper.ToJson(list);
                ///获取传值内容,写在本地txt文本中
                Log.WriteLogToFile(msgContent.ToString(), AppDomain.CurrentDomain.BaseDirectory + @"\Log\", DateTime.Now.ToString("yyyyMMddHHmm"));
                ///关键词
                string keyValue = bll.GetKeyValues(list);
                ///次数
                int dataCnt = 0;
                ///分布式事务 执行写入数据库操作. 
                using (TransactionScope tran = new TransactionScope())
                {
                    ///LOG日志
                    string logSql = BLL.LES.CommonBLL.GetCreateInboundLogSql("VMI", logFid, interfaceCode, keyValue, msgContent, string.Empty, string.Empty, loginUser, executeStartTime);
                    ///添加到中间表
                    dataCnt = bll.InsertListToCentreTable(list, logFid, logSql);
                    tran.Complete();
                }
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

                Log.WriteLogToFile(interfaceCode + "|" + retInfo.MessageContent + "|" + retInfo.ErrorCode, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));
                ///1成功、0失败
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure; ;
                ///
                string errorCode = ex.Message;
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                ///错误代码
                retInfo.ErrorCode = errorCode;
            }
            return retInfo;
        }

        /// <summary>
        ///  WMS-LES-014 VMI.WMS.排序拉动单回传
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [WebMethod(Description = "WMS-LES-014 VMI.WMS.排序拉动单回传")]
        public InterfaceReturnInfo GetJisAsnOrder(List<BFDAVmiJisPullOrderReturnInfo> list)
        {
            /////接口代码 
            string interfaceCode = "WMS-LES-014";
            ///回传文本
            InterfaceReturnInfo retInfo = new InterfaceReturnInfo();
            try
            {
                ///转报文
                string msgContent = JsonHelper.ToJson(list);
                ///获取传值内容,写在本地txt文本中
                Log.WriteLogToFile("|WWMS-LES-014 VMI.WMS.排序拉动单回传 日志记录| \r\n" + msgContent.ToString() + "\r\n|Count|" + list.Count.ToString().Length + "; End|", AppDomain.CurrentDomain.BaseDirectory + @"\Log\", DateTime.Now.ToString("yyyyMMddHHmm"));
                ///开始执行时间
                DateTime executeStartTime = DateTime.Now;
                ///LOG_FID
                Guid logFid = Guid.NewGuid();
                ///业务方法
                BFDAVmiJisPullOrderReturnBLL bll = new BFDAVmiJisPullOrderReturnBLL();
                ///关键词 
                string keyValue = bll.GetKeyValues(list);
                ///返回次数
                int dataCnt = 0;
                ///分布式事务 执行写入数据库操作. 
                using (TransactionScope tran = new TransactionScope())
                {
                    ///LOG日志
                    string logSql = BLL.LES.CommonBLL.GetCreateInboundLogSql("VMI", logFid, interfaceCode, keyValue, msgContent, string.Empty, string.Empty, loginUser, executeStartTime);
                    ///添加到中间表
                    dataCnt = bll.InsertListToCentreTable(list, logFid, logSql);
                    tran.Complete();
                }
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
                Log.WriteLogToFile(interfaceCode + "|" + retInfo.MessageContent + "|" + retInfo.ErrorCode, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));
                ///1成功、0失败
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure; ;
                ///
                string errorCode = ex.Message;
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                ///错误代码
                retInfo.ErrorCode = errorCode;

            }
            return retInfo;
        }

        /// <summary>
        ///  WMS-LES-016 VMI.WMS.VMI器具出库
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [WebMethod(Description = "WMS-LES-016 VMI.WMS.VMI器具出库")]
        public InterfaceReturnInfo GetVmiPackageOutbound(List<BFDAApplianceOutputInfo> list)
        {
            /////接口代码 
            string interfaceCode = "WMS-LES-016";
            ///回传文本
            InterfaceReturnInfo retInfo = new InterfaceReturnInfo();
            try
            {
                ///转报文
                string msgContent = new XmlWrapper().ObjectToXml(list);
                ///获取传值内容,写在本地txt文本中
                Log.WriteLogToFile("|WMS-LES-016 VMI.WMS.VMI器具出库 日志记录| \r\n" + msgContent.ToString() + "\r\n|Count|" + list.Count.ToString().Length + "; End|", AppDomain.CurrentDomain.BaseDirectory + @"\Log\", DateTime.Now.ToString("yyyyMMddHHmm"));
                ///
                //string keyValue = bll.GetKeyValues(list);
                ///

                ///分布式事务 执行写入数据库操作. 
                using (TransactionScope tran = new TransactionScope())
                {
                    ///LOG日志
                    // string logSql = BLL.LES.CommonBLL.GetCreateInboundLogSql("SRM",, logFid, interfaceCode, keyValue, msgContent, string.Empty, string.Empty, loginUser, executeStartTime);
                    ///添加到中间表
                    //dataCnt = bll.InsertListToCentreTable(list, logFid, logSql);
                    tran.Complete();
                }
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
                Log.WriteLogToFile(interfaceCode + "|" + retInfo.MessageContent + "|" + retInfo.ErrorCode, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));

                ///1成功、0失败
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure; ;
                ///
                string errorCode = ex.Message;
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                ///错误代码
                retInfo.ErrorCode = errorCode;
            }
            return retInfo;
        }
    }
}
