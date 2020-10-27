using BLL.LES;
using DM.LES;
using DM.SYS;
using BLL.SYS;
using Infrustructure.Logging;
using Infrustructure.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.IO;
using System.Data;
using Infrustructure.Data;

namespace WS.MES.InboundDataService
{
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
        private string loginUser = "WS.MES.InboundDataService";
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
        /// MES-LES-001	过点信息--采集点信息
        /// </summary>
        /// <param name="vehiclePointScanInfos"></param>
        /// <returns></returns>
        [WebMethod(Description = "过点信息")]
        public InterfaceReturnInfo GetVehiclePointScan(List<BFDAMesVehiclePointScanInfo> vehiclePointScanInfos)
        {
            ///回传文本
            InterfaceReturnInfo retInfo = new InterfaceReturnInfo();
            ///接口代码 
            string interfaceCode = "MES-LES-001";
            ///消息体
            string msgContent = JsonHelper.ToJson(vehiclePointScanInfos);
            ///写入日志
            Log.WriteLogToFile(logFlag, interfaceCode + "-MsgContent:|" + msgContent, AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMdd"));
           
            ///开始执行时间
            DateTime executeStartTime = DateTime.Now;
            try
            {
                ///LOG_FID
                Guid logFid = Guid.NewGuid();
                ///业务表
                BFDAMESVehiclePointScanBLL bll = new BFDAMESVehiclePointScanBLL();
                ///关键字 ,号分隔
                string keyValue = bll.GetKeyValues(vehiclePointScanInfos);
                ///用分布式事务处理联动的修改
                using (TransactionScope trans = new TransactionScope())
                {
                    ///LOG日志
                    string logSql = BLL.LES.CommonBLL.GetCreateInboundLogSql("MES", logFid, interfaceCode, keyValue, msgContent, string.Empty, string.Empty, loginUser, executeStartTime);
                    ///添加到中间表
                    int dataCnt = bll.InsertListToCentreTable(vehiclePointScanInfos, logFid, logSql);
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
                Log.WriteLogToFile(interfaceCode + "|" + ex.Message, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));
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
        /// PBS物料排查结果
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [WebMethod(Description = "PBS车辆物料排查")]
        public BFDAMesPbsMaterialInvestigationResultsInfo CheckMaterialStock(BFDAMesPbsMaterialInvestigationResultsInfo pbsMaterialInvestigationResultsInfo)
        {
            ///接口代码
            string interfaceCode = "MES-LES-003";
            ///开始执行时间
            DateTime executeStartTime = DateTime.Now;
            ///LOG_FID
            Guid logFid = Guid.NewGuid();
            ///业务表
            BFDAMESPbsMaterialInvestigationResultsBLL bll = new BFDAMESPbsMaterialInvestigationResultsBLL();
            try
            {
                ///关键字 ,号分隔
                string keyValue = bll.GetKeyValue(pbsMaterialInvestigationResultsInfo);
                using (TransactionScope tran = new TransactionScope())
                {
                    ///同步调用结果
                    bool result = new StocksBLL().CheckMaterialStock(pbsMaterialInvestigationResultsInfo.DMS_NO, pbsMaterialInvestigationResultsInfo.AREA_NO, loginUser);
                    int executeResult = (int)ExecuteResultConstants.Success;
                    pbsMaterialInvestigationResultsInfo.RESULT = (int)ResultsDescribed.Succeed;
                    string errorCode = "0x00000410";///车辆物料排查通过
                    pbsMaterialInvestigationResultsInfo.MATERIAL_CHECK = 1;
                    if (!result)
                    {
                        executeResult = (int)ExecuteResultConstants.Error;
                        pbsMaterialInvestigationResultsInfo.RESULT = (int)ResultsDescribed.Failure;
                        errorCode = "0x00000411";///车辆物料排查未通过                   
                        pbsMaterialInvestigationResultsInfo.MATERIAL_CHECK = 0;
                    }
                    pbsMaterialInvestigationResultsInfo.MSG = GetMessage(ref errorCode);
                    ///错误编码
                    pbsMaterialInvestigationResultsInfo.MSGNO = errorCode;
                    ///LOG日志
                    string logSql = BLL.LES.CommonBLL.GetCreateInboundLogSql("MES",
                        logFid,
                        interfaceCode,
                        keyValue,
                        JsonHelper.ToJson(pbsMaterialInvestigationResultsInfo),
                        pbsMaterialInvestigationResultsInfo.MSGNO,
                        pbsMaterialInvestigationResultsInfo.MSG,
                        loginUser,
                        executeStartTime,
                        executeResult);
                    BLL.SYS.CommonBLL.ExecuteNonQueryBySql(logSql);
                    tran.Complete();
                }
            }
            catch (Exception ex)
            {
                ///记录日志
                Log.WriteLogToFile(interfaceCode + "|" + ex.Message, AppDomain.CurrentDomain.BaseDirectory + @"\ERROR\", DateTime.Now.ToString("yyyyMMdd"));
                ///错误代码
                string errorCode = ex.Message;
                ///错误消息
                pbsMaterialInvestigationResultsInfo.MSG = GetMessage(ref errorCode);
                pbsMaterialInvestigationResultsInfo.MSGNO = errorCode;
                ///
                pbsMaterialInvestigationResultsInfo.RESULT = (int)ResultsDescribed.Failure;
            }
            return pbsMaterialInvestigationResultsInfo;
        }

        /// <summary>
        /// MES-LES-004	缺件明细主数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [WebMethod(Description = "PBS订单缺件明细")]
        public BFDAMesMissingpartsMainInfo GetProductionOrderLackMaterial(BFDAMesMissingpartsMainInfo mesMissingpartsMainInfo)
        {
            ///接口代码
            string interfaceCode = "MES-LES-004";
            ///开始执行时间
            DateTime executeStartTime = DateTime.Now;
            ///LOG_FID
            Guid logFid = Guid.NewGuid();
            ///返回接口内容
            InterfaceReturnInfo retInfo = new InterfaceReturnInfo();
            ///逻辑
            BFDAMESMissingpartsMailBLL bll = new BFDAMESMissingpartsMailBLL();
            try
            {
                ///消息体
                string msgContent = JsonHelper.ToJson(mesMissingpartsMainInfo);
                ///关键字 ,号分隔
                string keyValue = bll.GetKeyValue(mesMissingpartsMainInfo);
                using (TransactionScope tran = new TransactionScope())
                {
                    ///LOG日志
                    string logSql = BLL.LES.CommonBLL.GetCreateInboundLogSql("MES", logFid, interfaceCode, keyValue, msgContent, string.Empty, string.Empty, loginUser, executeStartTime);

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
                string errorCode = ex.Message;
                ///错误消息
                retInfo.MessageContent = GetMessage(ref errorCode);
                retInfo.ErrorCode = errorCode;
                ///ESB.RESULT 0标识成功，1代表错误
                retInfo.ExecuteResult = (int)ResultsDescribed.Failure;
            }
            return mesMissingpartsMainInfo;
        }

        /// <summary>
        /// MES-LES-006	信息点基础数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [WebMethod(Description = "信息点基础数据-扫描点基础数据")]
        public InterfaceReturnInfo GetInformationPointBasic(List<BFDAMesInformationPointBasicInfo> informationPointBasicInfos)
        {
            ///接口代码
            string interfaceCode = "MES-LES-006";
            ///开始执行时间
            DateTime executeStartTime = DateTime.Now;
            ///LOG_FID
            Guid logFid = Guid.NewGuid();
            ///返回接口内容
            InterfaceReturnInfo retInfo = new InterfaceReturnInfo();
            ///逻辑
            BFDAMESInformationPointBasicBLL bll = new BFDAMESInformationPointBasicBLL();
            try
            {
                ///消息体
                string msgContent = JsonHelper.ToJson(informationPointBasicInfos);
                Log.WriteLogToFile(logFlag, interfaceCode + "-MsgContent:|" + msgContent, AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMdd"));

                ///关键字 ,号分隔
                string keyValue = bll.GetKeyValues(informationPointBasicInfos);

                using (TransactionScope tran = new TransactionScope())
                {
                    ///LOG日志
                    string logSql = BLL.LES.CommonBLL.GetCreateInboundLogSql("MES", logFid, interfaceCode, keyValue, msgContent, string.Empty, string.Empty, loginUser, executeStartTime);
                    ///添加到中间表
                    int dataCnt = bll.InsertListToCentreTable(informationPointBasicInfos, logFid, logSql);
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
        /// MES-LES-007	产线级工艺顺序
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [WebMethod(Description = "产线级工艺顺序--LES工位的顺序")]
        public InterfaceReturnInfo GetProductionlineProcessOrder(List<BFDAMesProductionlineProcessOrderInfo> productionlineProcessOrderInfos)
        {
            ///接口代码
            string interfaceCode = "MES-LES-007";
            ///开始执行时间
            DateTime executeStartTime = DateTime.Now;
            ///LOG_FID
            Guid logFid = Guid.NewGuid();
            ///返回接口内容
            InterfaceReturnInfo retInfo = new InterfaceReturnInfo();
            ///逻辑
            BFDAMESProductionlineProcessOrderBLL bll = new BFDAMESProductionlineProcessOrderBLL();
            try
            {
                ///消息体
                string msgContent = JsonHelper.ToJson(productionlineProcessOrderInfos);
                ///写入日志
                Log.WriteLogToFile(logFlag, interfaceCode + "-MsgContent:|" + msgContent, AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMdd"));
                ///关键字 ,号分隔
                string keyValue = bll.GetKeyValues(productionlineProcessOrderInfos);

                using (TransactionScope tran = new TransactionScope())
                {
                    ///LOG日志
                    string logSql = BLL.LES.CommonBLL.GetCreateInboundLogSql("MES", logFid, interfaceCode, keyValue, msgContent, string.Empty, string.Empty, loginUser, executeStartTime);
                    ///添加到中间表
                    int dataCnt = bll.InsertListToCentreTable(productionlineProcessOrderInfos, logFid, logSql);
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
