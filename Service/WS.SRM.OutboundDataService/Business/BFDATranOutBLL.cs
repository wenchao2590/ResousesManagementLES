namespace WS.SRM.OutboundDataService
{
    using BLL.LES;
    using DM.LES;
    using DM.SYS;
    using Infrustructure.Utilities;
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Text;
    using WS.SRM.OutboundDataService.SRMInboundWSDL;
    /// <summary>
    /// LES-SRM-008	入库数据
    /// </summary>
    public class BFDATranOutBLL
    {
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
            msgContent = string.Empty;
            List<SrmTranOutInfo> srmTranOutInfos = new SrmTranOutBLL().GetList("" +
                "[LOG_FID] = N'" + logFid + "' and " +
                "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", string.Empty);
            if (srmTranOutInfos.Count == 0)
            {
                errorCode = "MC:0x00000212";///经检测,传递数据为空,请确认
                return ExecuteResultConstants.Exception;
            }
            ///发送内容
            List<BFDATranOutInfo> list = new List<BFDATranOutInfo>();
            foreach (var srmTranOutInfo in srmTranOutInfos)
            {
                list.Add(GetSrmTranOutInfo(srmTranOutInfo));
            }
            BFDASRMSendDataInfo<BFDATranOutInfo> sendDataInfo = new BFDASRMSendDataInfo<BFDATranOutInfo>();
            sendDataInfo.List = list;
            ///
            InboundService_pttClient client = new InboundService_pttClient();
            client.Endpoint.Address = new EndpointAddress(interfaceConfigInfo.CallUrl);
            ///数据发送
            msgContent = new XmlWrapper().ObjectToXmlByEncoding(sendDataInfo, Encoding.UTF8, false);
            msgContent = msgContent.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");
            string result = client.InboundService(msgContent, out errorCode, out errorMsg);
            if (result == Convert.ToString((int)OutboundReturnStateConstants.FAILURE))
                return ExecuteResultConstants.Error;
            return ExecuteResultConstants.Success;
        }
        /// <summary>
        /// 对象转换
        /// </summary>
        /// <param name="srmTranOutInfo"></param>
        /// <returns></returns>
        private static BFDATranOutInfo GetSrmTranOutInfo(SrmTranOutInfo srmTranOutInfo)
        {
            BFDATranOutInfo info = new BFDATranOutInfo();
            ///原始单据号
            info.SourceOrderCode = srmTranOutInfo.SourceOrderCode;
            ///原始单据类型
            info.SourceOrderType = srmTranOutInfo.SourceOrderType.GetValueOrDefault().ToString();
            ///物料编号
            info.PartNo = srmTranOutInfo.PartNo;
            ///实收数量
            info.DeliveryQty = srmTranOutInfo.DeliveryQty.GetValueOrDefault().ToString();
            ///
            return info;
        }
    }
}
