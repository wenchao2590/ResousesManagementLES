namespace WS.SRM.OutboundDataService
{
    using BLL.LES;
    using DM.LES;
    using DM.SYS;
    using Infrustructure.Logging;
    using Infrustructure.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Text;
    /// <summary>
    ///  LES-SRM-002 供货计划 Supply Plan
    /// </summary>
    public class BFDASupplyPlanBLL
    {
        #region Common

        /// <summary>
        /// 日期格式
        /// </summary>
        private static string srmDateFormat = "yyyy-MM-dd";
        /// <summary>
        /// 是否写入日志
        /// </summary>
        public static string logFlag = AppSettings.GetConfigString("LogFlag");
        /// <summary>
        /// 接口名称
        /// </summary>
        public static string interfaceCode = "LES-SRM-002 供货计划";

        #endregion



        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="logFid"></param>
        /// <param name="interfaceConfigInfo"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMsg"></param>
        /// <param name="msgContent"></param>
        /// <returns></returns>
        public static ExecuteResultConstants Send(Guid logFid, InterfaceConfigInfo interfaceConfigInfo, ref string errorCode, ref string errorMsg, out string msgContent)
        {
            ///
            msgContent = string.Empty;
            ///
            List<SrmSupplyPlanInfo> srmSupplyPlanInfos = new SrmSupplyPlanBLL().GetList("" +
                "[LOG_FID] = N'" + logFid + "' and " +
                "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", string.Empty);
            if (srmSupplyPlanInfos.Count == 0)
            {
                errorCode = "MC:0x00000212";///经检测,传递数据为空,请确认
                return ExecuteResultConstants.Exception;
            }
            ///发送内容
            List<BFDASupplyPlanInfo> list = new List<BFDASupplyPlanInfo>();
            foreach (var srmSupplyPlanInfo in srmSupplyPlanInfos)
            {
                list.Add(GetSrmSupplyPlanInfo(srmSupplyPlanInfo));
            }
            /////数据发送
            msgContent = new XmlWrapper().ObjectToXmlByEncoding(list, Encoding.UTF8, true);
            msgContent = msgContent.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");
            SRMSupplierPlanWSDL.SupplierPlanService_pttClient Client = new SRMSupplierPlanWSDL.SupplierPlanService_pttClient();
            Client.Endpoint.Address = new System.ServiceModel.EndpointAddress(interfaceConfigInfo.CallUrl);
            ///
            string result = Client.SupplierPlanService(msgContent, out errorCode, out errorMsg);
            Log.WriteLogToFile(logFlag, interfaceCode + "--Return:dataCnt:" + result + "Content:" + msgContent + "\r", AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMddHH"));

            if (result == Convert.ToString((int)OutboundReturnStateConstants.FAILURE))
                return ExecuteResultConstants.Error;
            return ExecuteResultConstants.Success;
        }
        /// <summary>
        /// 中间表转换成报文表
        /// </summary>
        /// <param name="srmSupplyPlanInfo"></param>
        /// <returns></returns>
        private static BFDASupplyPlanInfo GetSrmSupplyPlanInfo(SrmSupplyPlanInfo srmSupplyPlanInfo)
        {
            BFDASupplyPlanInfo info = new BFDASupplyPlanInfo();
            ///工厂
            info.Plant = srmSupplyPlanInfo.Plant;
            ///供应商编码
            info.SupplierCode = srmSupplyPlanInfo.SupplierNum;
            ///物料编号
            info.PartNo = srmSupplyPlanInfo.PartNo;
            ///需求日期
            info.RequireDate = srmSupplyPlanInfo.DeliveryDate.GetValueOrDefault().ToString(srmDateFormat);
            ///需求数量
            info.PartQty = srmSupplyPlanInfo.RequireQty.GetValueOrDefault().ToString("F0");
            ///
            return info;
        }
    }
}
