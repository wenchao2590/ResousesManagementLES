namespace WS.SRM.OutboundDataService
{
    using BLL.LES;
    using DM.LES;
    using DM.SYS;
    using Infrustructure.Logging;
    using Infrustructure.Utilities;
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Text;
    using WS.SRM.OutboundDataService.SRMSupplierMaterialWSDL;
    /// <summary>
    /// LES-SRM-001	 VMI供应商物料关系  
    /// </summary>
    public class BFDAVmiSupplierPartBLL
    {

        /// <summary>
        /// 是否写入日志
        /// </summary>
        public static string logFlag = AppSettings.GetConfigString("LogFlag");
        /// <summary>
        /// 接口名称
        /// </summary>
        public static string interfaceCode = "LES-SRM-001-VMI供应商物料关系";
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
            List<SrmVmiSupplierPartInfo> vmiSupplierParts = new SrmVmiSupplierPartBLL().GetList("" +
                "[LOG_FID] = N'" + logFid + "' and " +
                "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", string.Empty);
            if (vmiSupplierParts.Count == 0)
            {
                errorCode = "MC:0x00000212";///经检测,传递数据为空,请确认
                return ExecuteResultConstants.Exception;
            }
            ///转换发送内容
            List<BFDAVmiSupplierPartInfo> list = new List<BFDAVmiSupplierPartInfo>();
            foreach (var vmiSupplierPartInfo in vmiSupplierParts)
            {
                list.Add(GetSrmVmiSupplierPartInfo(vmiSupplierPartInfo));
            }
            ///准备把集合转成一个对象
            BFDASRMSendDataInfo<BFDAVmiSupplierPartInfo> sendDataInfo = new BFDASRMSendDataInfo<BFDAVmiSupplierPartInfo>();
            sendDataInfo.List = list;
            ///
            SupplierMaterialService_pttClient client = new SupplierMaterialService_pttClient();
            client.Endpoint.Address = new EndpointAddress(interfaceConfigInfo.CallUrl);
            ///数据发送
            msgContent = new XmlWrapper().ObjectToXmlByEncoding(sendDataInfo, Encoding.UTF8, false);
            msgContent = msgContent.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");

            ///
            string result = client.SupplierMaterialService(msgContent, out errorCode, out errorMsg);

            Log.WriteLogToFile(logFlag, interfaceCode+"--Return:dataCnt:" + result+"Content:"+ msgContent+"\r", AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMddHH"));

            if (result == Convert.ToString((int)OutboundReturnStateConstants.FAILURE))
                return ExecuteResultConstants.Error;
            return ExecuteResultConstants.Success;
        }


        /// <summary>
        /// 对象转换
        /// </summary>
        /// <param name="srmVmiSupplierPartInfo"></param>
        /// <returns></returns>
        private static BFDAVmiSupplierPartInfo GetSrmVmiSupplierPartInfo(SrmVmiSupplierPartInfo srmVmiSupplierPartInfo)
        {
            BFDAVmiSupplierPartInfo info = new BFDAVmiSupplierPartInfo();
            ///VMI仓库代码
            info.VmiWarehouseCode = srmVmiSupplierPartInfo.VmiWarehouseCode;
            ///VMI仓库名称
            info.VmiWarehouseName = srmVmiSupplierPartInfo.VmiWarehouseName;
            ///供应商代码
            info.SupplierCode = srmVmiSupplierPartInfo.SupplierCode;
            ///供应商名称
            info.SupplierName = srmVmiSupplierPartInfo.SupplierName;
            ///物料编号
            info.PartNo = srmVmiSupplierPartInfo.PartNo;
            ///物料中文描述
            info.PartCName = srmVmiSupplierPartInfo.PartCname;
            ///删除标记
            info.DeleteFlag = srmVmiSupplierPartInfo.DeleteFlag.GetValueOrDefault() ? "1" : "0";
            ///工厂代码
            info.Plant = srmVmiSupplierPartInfo.Plant;
            ///
            return info;
        }
    }
}
