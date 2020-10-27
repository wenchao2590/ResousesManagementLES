namespace WS.SAP.InboundDataService
{
    using DM.SYS;
    using BLL.SYS;
    using Infrustructure.Logging;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Transactions;
    using System.Web.Services;
    using Infrustructure.Data;
    /// <summary>
    /// InboundDataService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class InboundDataService : WebService
    {
        #region Common
        /// <summary>
        /// 接口用户
        /// </summary>
        private string loginUser = "WS.SAP.InboundDataService";
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
        /// SAP-LES-001 SAP物料基础数据接收  
        /// </summary>
        /// <param name="partInfos"></param>
        /// <returns></returns>
        [WebMethod(Description = "物料主数据接收")]
        public InterfaceReturnInfo GetSapMaintainPart(List<BFDASapPartInfo> partInfos)
        {
            ///接口代码 
            string interfaceCode = "SAP-LES-001";
            ///回传文本
            InterfaceReturnInfo retInfo = new InterfaceReturnInfo();

            if (partInfos.Count() == 0)
            {
                string errorCode = "0x00000463";
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                retInfo.ErrorCode = errorCode;
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure;
                return retInfo;
            }

            ///消息体
            string msgContent = JsonHelper.ToJson(partInfos);
            Log.WriteLogToFile(logFlag, interfaceCode + "-MsgContent:|" + msgContent, AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMdd"));

            ///开始执行时间
            DateTime executeStartTime = DateTime.Now;
            try
            {
                ///LOG_FID
                Guid logFid = Guid.NewGuid();
                ///
                BFDASapPartBLL bll = new BFDASapPartBLL();
                ///关键字 ,号分隔
                string keyValue = bll.GetKeyValues(partInfos);
                ///
                int dataCnt = 0;
                ///用分布式事务处理联动的修改
                using (TransactionScope trans = new TransactionScope())
                {
                    ///LOG日志
                    string logSql = BLL.LES.CommonBLL.GetCreateInboundLogSql("SAP", logFid, interfaceCode, keyValue, msgContent, string.Empty, string.Empty, loginUser, executeStartTime);
                    ///添加到中间表
                    dataCnt = bll.InsertListToCentreTable(partInfos, logFid, logSql);
                    trans.Complete();
                }
                ///提示代码
                retInfo.ErrorCode = string.Empty;
                ///错误消息
                string successCode = "1x00000051";///传输成功
                retInfo.MessageContent = GetMessage(ref successCode);
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.ExecuteResult = (int)ResultsDescribed.Succeed;
            }
            catch (Exception ex)
            {
                Log.WriteLogToFile(interfaceCode + "|" + ex.Message, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));
                ///错误代码
                string errorCode = ex.Message;
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                ///错误码
                retInfo.ErrorCode = errorCode;
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure;
            }
            return retInfo;
        }
        /// <summary>
        /// SAP-LES-003 SAP供应商配额数据接收  
        /// </summary>
        /// <param name="supplierQuotaInfos"></param>
        /// <returns></returns>
        [WebMethod(Description = "供应商配额数据接收")]
        public InterfaceReturnInfo GetSapSupplierQuota(List<BFDASupplierQuotaInfo> supplierQuotaInfos)
        {
            ///接口代码
            string interfaceCode = "SAP-LES-003";
            ///返回
            InterfaceReturnInfo retInfo = new InterfaceReturnInfo();
            if (supplierQuotaInfos.Count() == 0)
            {
                string errorCode = "0x00000463";
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                retInfo.ErrorCode = errorCode;
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure;
                return retInfo;
            }
            ///开始执行时间
            DateTime executeStartTime = DateTime.Now;
            ///LOG_FID
            Guid logFid = Guid.NewGuid();

            ///
            BFDASapSupplierQuotaBLL bll = new BFDASapSupplierQuotaBLL();
            try
            {
                ///消息体
                string msgContent = JsonHelper.ToJson(supplierQuotaInfos);
                Log.WriteLogToFile(logFlag, interfaceCode + "-MsgContent:|" + msgContent, AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMdd"));

                ///关键字 ,号分隔
                string keyValue = bll.GetKeyValues(supplierQuotaInfos);

                using (TransactionScope tran = new TransactionScope())
                {
                    ///LOG日志
                    string logSql = BLL.LES.CommonBLL.GetCreateInboundLogSql("SAP", logFid, interfaceCode, keyValue, msgContent, string.Empty, string.Empty, loginUser, executeStartTime);
                    ///添加到中间表
                    int dataCnt = bll.InsertListToCentreTable(supplierQuotaInfos, logFid, logSql);
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
                ///记录日志
                Log.WriteLogToFile(interfaceCode + "|" + ex.Message, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));
                ///错误代码
                string errorCode = ex.Message;
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                retInfo.ErrorCode = errorCode;
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure;
            }
            return retInfo;
        }
        /// <summary>
        /// SAP-LES-005 SAP工作日历基础数据接收
        /// </summary>
        /// <param name="workCalendarInfos"></param>
        /// <returns></returns>
        [WebMethod(Description = "工作日历基础数据接收")]
        public InterfaceReturnInfo GetSapWorkCalendar(List<BFDASapWorkCalendarInfo> workCalendarInfos)
        {
            ///接口代码
            string interfaceCode = "SAP-LES-005";
            ///返回报文体
            InterfaceReturnInfo retInfo = new InterfaceReturnInfo();
            if (workCalendarInfos.Count() == 0)
            {
                string errorCode = "0x00000463";
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                retInfo.ErrorCode = errorCode;
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure;
                return retInfo;
            }
            ///开始执行时间
            DateTime executeStartTime = DateTime.Now;
            ///LOG_FID
            Guid logFid = Guid.NewGuid();

            ///
            BFDASapWorkCalendarBLL bll = new BFDASapWorkCalendarBLL();
            try
            {
                ///消息体
                string msgContent = JsonHelper.ToJson(workCalendarInfos);
                Log.WriteLogToFile(interfaceCode + "-MsgContent:|" + msgContent, AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMdd"));

                ///关键字 ,号分隔
                string keyValue = bll.GetKeyValues(workCalendarInfos);

                using (TransactionScope tran = new TransactionScope())
                {
                    ///LOG日志
                    string logSql = BLL.LES.CommonBLL.GetCreateInboundLogSql("SAP", logFid, interfaceCode, keyValue, msgContent, string.Empty, string.Empty, loginUser, executeStartTime);
                    ///添加到中间表
                    int dataCnt = bll.InsertListToCentreTable(workCalendarInfos, logFid, logSql);
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
                Log.WriteLogToFile(logFlag, interfaceCode + "|" + ex.Message, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));
                ///错误代码
                string errorCode = ex.Message;
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                retInfo.ErrorCode = errorCode;
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure;
            }
            return retInfo;
        }
        /// <summary>
        /// SAP-LES-006 SAP生产订单接收
        /// </summary>
        /// <param name="productOrderInfos"></param>
        /// <returns></returns>
        [WebMethod(Description = "生产订单接收")]
        public InterfaceReturnInfo GetProdutionOrder(List<BFDASapProductOrderInfo> productOrderInfos)
        {
            ///返回接口内容
            InterfaceReturnInfo retInfo = new InterfaceReturnInfo();
            ///接口代码
            string interfaceCode = "SAP-LES-006";

            if (productOrderInfos.Count() == 0)
            {
                string errorCode = "0x00000463";
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                retInfo.ErrorCode = errorCode;
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure;
                return retInfo;
            }

            ///开始执行时间
            DateTime executeStartTime = DateTime.Now;
            ///LOG_FID
            Guid logFid = Guid.NewGuid();

            ///订单接收逻辑
            BFDASapProductOrderBLL bll = new BFDASapProductOrderBLL();
            try
            {
                ///消息体
                string msgContent = JsonHelper.ToJson(productOrderInfos);
                Log.WriteLogToFile(logFlag, interfaceCode + "-MsgContent:|" + msgContent, AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMdd"));

                ///关键字 ,号分隔
                string keyValue = bll.GetKeyValues(productOrderInfos);

                using (TransactionScope tran = new TransactionScope())
                {
                    ///LOG日志
                    string logSql = BLL.LES.CommonBLL.GetCreateInboundLogSql("SAP", logFid, interfaceCode, keyValue, msgContent, string.Empty, string.Empty, loginUser, executeStartTime);
                    ///添加到中间表
                    int dataCnt = bll.InsertListToCentreTable(productOrderInfos, logFid, logSql);
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
                Log.WriteLogToFile(interfaceCode + "|" + ex.Message, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));
                ///错误代码
                string errorCode = ex.Message;
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                retInfo.ErrorCode = errorCode;
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure;
            }
            return retInfo;
        }
        /// <summary>
        /// SAP-LES-007 SAP订单BOM接收
        /// </summary>
        /// <param name="productOrderBomInfos"></param>
        /// <returns></returns>
        [WebMethod(Description = "订单BOM接收")]
        public InterfaceReturnInfo GetProdutionOrderBom(List<BFDASapProductOrderBomInfo> productOrderBomInfos)
        {
            ///接口代码
            string interfaceCode = "SAP-LES-007";
            ///返回接口参数
            InterfaceReturnInfo retInfo = new InterfaceReturnInfo();
            if (productOrderBomInfos.Count() == 0)
            {
                string errorCode = "0x00000463";
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                retInfo.ErrorCode = errorCode;
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure;
                return retInfo;
            }

            ///开始执行时间
            DateTime executeStartTime = DateTime.Now;
            ///LOG_FID
            Guid logFid = Guid.NewGuid();

            ///业务处理
            BFDASapProductOrderBomBLL bll = new BFDASapProductOrderBomBLL();
            try
            {
                ///消息体
                string msgContent = JsonHelper.ToJson(productOrderBomInfos);
                Log.WriteLogToFile(logFlag, interfaceCode + "-MsgContent:|" + msgContent, AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMdd"));

                ///关键字 ,号分隔
                string keyValue = bll.GetKeyValues(productOrderBomInfos);

                using (TransactionScope tran = new TransactionScope())
                {
                    ///LOG日志
                    string logSql = BLL.LES.CommonBLL.GetCreateInboundLogSql("SAP", logFid, interfaceCode, keyValue, msgContent, string.Empty, string.Empty, loginUser, executeStartTime);
                    ///添加到中间表
                    int dataCnt = bll.InsertListToCentreTable(productOrderBomInfos, logFid, logSql);
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
                Log.WriteLogToFile(interfaceCode + "|" + ex.Message, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));
                ///错误代码
                string errorCode = ex.Message;
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                retInfo.ErrorCode = errorCode;
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure;
            }
            return retInfo;
        }

        /// <summary>
        /// SAP-LES-011 盘点计划
        /// TI_IFM_SAP_inventory_CHEK_PLAN
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "SAP-LES-011 盘点计划")]
        public InterfaceReturnInfo GetInventoryChekPlan(List<BFDASapInventoryCheckPlanInfo> bFDASapInventoryCheckPlanInfos)
        {
            ///接口代码
            string interfaceCode = "SAP-LES-011";
            ///返回报文体
            InterfaceReturnInfo retInfo = new InterfaceReturnInfo();
            if (bFDASapInventoryCheckPlanInfos.Count() == 0)
            {
                string errorCode = "0x00000463";
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                retInfo.ErrorCode = errorCode;
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure;
                return retInfo;
            }
            ///开始执行时间
            DateTime executeStartTime = DateTime.Now;
            ///LOG_FID
            Guid logFid = Guid.NewGuid();

            ///
            BFDAInventoryCheckPlanBLL bll = new BFDAInventoryCheckPlanBLL();
            try
            {
                ///消息体
                string msgContent = JsonHelper.ToJson(bFDASapInventoryCheckPlanInfos);
                Log.WriteLogToFile(logFlag, interfaceCode + "-MsgContent:|" + msgContent, AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMdd"));

                ///关键字 ,号分隔
                string keyValue = bll.GetKeyValues(bFDASapInventoryCheckPlanInfos);
                ///
                using (TransactionScope tran = new TransactionScope())
                {
                    ///LOG日志
                    string logSql = BLL.LES.CommonBLL.GetCreateInboundLogSql("SAP", logFid, interfaceCode, keyValue, msgContent, string.Empty, string.Empty, loginUser, executeStartTime);
                    ///添加到中间表
                    int dataCnt = bll.InsertListToCentreTable(bFDASapInventoryCheckPlanInfos, logFid, logSql);
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
                Log.WriteLogToFile(interfaceCode + "|" + ex.Message, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));
                ///错误代码
                string errorCode = ex.Message;
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                retInfo.ErrorCode = errorCode;
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure;
            }

            return retInfo;
        }

        /// <summary>
        /// SAP-LES-013 断点更改单 接收
        /// </summary>
        /// <param name="breakpointChangesInfos"></param>
        /// <returns></returns>
        [WebMethod(Description = "SAP-LES-013 断点更改单接收")]
        public InterfaceReturnInfo GetSapBreakpointChanges(List<BFDASapBreakpointChangesInfo> breakpointChangesInfos)
        {
            ///接口代码
            string interfaceCode = "SAP-LES-013";
            ///返回报文体
            InterfaceReturnInfo retInfo = new InterfaceReturnInfo();
            if (breakpointChangesInfos.Count() == 0)
            {
                string errorCode = "0x00000463";
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                retInfo.ErrorCode = errorCode;
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure;
                return retInfo;
            }

            ///开始执行时间
            DateTime executeStartTime = DateTime.Now;
            ///LOG_FID
            Guid logFid = Guid.NewGuid();

            ///
            BFDASapBreakpointChangesBLL bll = new BFDASapBreakpointChangesBLL();
            try
            {
                ///消息体
                string msgContent = JsonHelper.ToJson(breakpointChangesInfos);
                Log.WriteLogToFile(logFlag, interfaceCode + "-MsgContent:|" + msgContent, AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMdd"));

                ///关键字 ,号分隔
                string keyValue = bll.GetKeyValues(breakpointChangesInfos);
                ///
                using (TransactionScope tran = new TransactionScope())
                {
                    ///LOG日志
                    string logSql = BLL.LES.CommonBLL.GetCreateInboundLogSql("SAP", logFid, interfaceCode, keyValue, msgContent, string.Empty, string.Empty, loginUser, executeStartTime);
                    ///添加到中间表
                    int dataCnt = bll.InsertListToCentreTable(breakpointChangesInfos, logFid, logSql);
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
                Log.WriteLogToFile(interfaceCode + "|" + ex.Message, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));
                ///错误代码
                string errorCode = ex.Message;
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                retInfo.ErrorCode = errorCode;
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure;
            }
            return retInfo;
        }
        /// <summary>
        /// SAP-LES-014	断点供货计划
        /// </summary>
        /// <param name="breakpointSupplyPlanInfos"></param>
        /// <returns></returns>
        [WebMethod(Description = "SAP-LES-014 断点供货计划接收")]
        public InterfaceReturnInfo GetSapBreakpointSupplyPlan(List<BFDASAPBreakpointSupplyPlanInfo> breakpointSupplyPlanInfos)
        {
            ///接口代码
            string interfaceCode = "SAP-LES-014";
            ///返回报文体
            InterfaceReturnInfo retInfo = new InterfaceReturnInfo();
            if (breakpointSupplyPlanInfos.Count() == 0)
            {
                string errorCode = "0x00000463";
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                retInfo.ErrorCode = errorCode;
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure;
                return retInfo;
            }
            ///开始执行时间
            DateTime executeStartTime = DateTime.Now;
            ///LOG_FID
            Guid logFid = Guid.NewGuid();

            ///
            BFDASAPBreakpointSupplyPlanBLL bll = new BFDASAPBreakpointSupplyPlanBLL();
            try
            {
                ///消息体
                string msgContent = JsonHelper.ToJson(breakpointSupplyPlanInfos);
                Log.WriteLogToFile(logFlag, interfaceCode + "-MsgContent:|" + msgContent, AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMdd"));

                ///关键字 ,号分隔
                string keyValue = bll.GetKeyValues(breakpointSupplyPlanInfos);
                using (TransactionScope tran = new TransactionScope())
                {
                    ///LOG日志
                    string logSql = BLL.LES.CommonBLL.GetCreateInboundLogSql("SAP", logFid, interfaceCode, keyValue, msgContent, string.Empty, string.Empty, loginUser, executeStartTime);
                    ///添加到中间表
                    int dataCnt = bll.InsertListToCentreTable(breakpointSupplyPlanInfos, logFid, logSql);
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
                Log.WriteLogToFile(interfaceCode + "|" + ex.Message, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));
                ///错误代码
                string errorCode = ex.Message;
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                retInfo.ErrorCode = errorCode;
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure;
            }
            return retInfo;
        }
        /// <summary>
        /// SAP-LES-016 物料预留单
        /// </summary>
        /// <param name="materialReservationInfos"></param>
        /// <returns></returns>
        [WebMethod(Description = "物料预留单接收")]
        public InterfaceReturnInfo GetMaterialReservation(List<BFDASapMaterialReservationInfo> materialReservationInfos)
        {
            ///返回报文体
            InterfaceReturnInfo retInfo = new InterfaceReturnInfo();
            ///接口代码
            string interfaceCode = "SAP-LES-016";

            if (materialReservationInfos.Count() == 0)
            {
                string errorCode = "0x00000463";
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                retInfo.ErrorCode = errorCode;
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure;
                return retInfo;
            }

            ///开始执行时间
            DateTime executeStartTime = DateTime.Now;
            ///LOG_FID
            Guid logFid = Guid.NewGuid();

            ///
            BFDASAPMaterialReservationBLL bll = new BFDASAPMaterialReservationBLL();
            try
            {
                ///消息体
                string msgContent = JsonHelper.ToJson(materialReservationInfos);

                Log.WriteLogToFile(interfaceCode + "ListCount:" + materialReservationInfos.Count + "-MsgContent:|" + msgContent, AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMdd"));
                ///关键字 ,号分隔
                string keyValue = bll.GetKeyValues(materialReservationInfos);
                ///
                using (TransactionScope tran = new TransactionScope())
                {
                    ///LOG日志
                    string logSql = BLL.LES.CommonBLL.GetCreateInboundLogSql("SAP", logFid, interfaceCode, keyValue, msgContent, string.Empty, string.Empty, loginUser, executeStartTime);
                    ///添加到中间表
                    int dataCnt = bll.InsertListToCentreTable(materialReservationInfos, logFid, logSql);
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
                Log.WriteLogToFile(logFlag, interfaceCode + "|" + ex.Message, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));
                ///错误代码
                string errorCode = ex.Message;
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                retInfo.ErrorCode = errorCode;
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure;
            }
            return retInfo;
        }
        /// <summary>
        /// SAP-LES-018 SAP工厂布局基础数据接收
        /// </summary>
        /// <param name="plantStructureInfos"></param>
        /// <returns></returns>
        [WebMethod(Description = "工厂布局基础数据接收")]
        public InterfaceReturnInfo GetPlantLayout(List<BFDASapPlantStructureInfo> plantStructureInfos)
        {
            ///接口代码
            string interfaceCode = "SAP-LES-018";
            ///返回报文体
            InterfaceReturnInfo retInfo = new InterfaceReturnInfo();
            if (plantStructureInfos.Count() == 0)
            {
                string errorCode = "0x00000463";
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                retInfo.ErrorCode = errorCode;
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure;
                return retInfo;
            }

            ///开始执行时间
            DateTime executeStartTime = DateTime.Now;
            ///LOG_FID
            Guid logFid = Guid.NewGuid();

            BFDASapPlantStructureBLL bll = new BFDASapPlantStructureBLL();
            try
            {
                ///消息体
                string msgContent = JsonHelper.ToJson(plantStructureInfos);
                ///关键字 ,号分隔
                string keyValue = bll.GetKeyValues(plantStructureInfos);
                ///
                using (TransactionScope tran = new TransactionScope())
                {
                    ///LOG日志
                    string logSql = BLL.LES.CommonBLL.GetCreateInboundLogSql("SAP", logFid, interfaceCode, keyValue, msgContent, string.Empty, string.Empty, loginUser, executeStartTime);
                    ///添加到中间表
                    int dataCnt = bll.InsertListToCentreTable(plantStructureInfos, logFid, logSql);
                    tran.Complete();
                }
                ///1成功、0失败
                retInfo.ExecuteResult = (int)ResultsDescribed.Succeed;
                ///错误编码
                retInfo.ErrorCode = string.Empty;
                ///描述信息2
                string successCode = "1x00000051";///传输成功
                retInfo.MessageContent = GetMessage(ref successCode);
            }
            catch (Exception ex)
            {
                Log.WriteLogToFile(interfaceCode + "|" + ex.Message, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));
                ///错误代码
                string errorCode = ex.Message;
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                retInfo.ErrorCode = errorCode;
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure;
            }
            return retInfo;
        }
    }
}
