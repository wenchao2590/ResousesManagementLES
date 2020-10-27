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
    using WS.SRM.OutboundDataService.SRMMaterialPullWSDL;
    /// <summary>
    /// LES-SRM-005-物料拉动单  
    /// </summary>
    public class BFDAPullingOrderBLL
    {
        /// <summary>
        /// 日期格式
        /// </summary>
        private static string srmDateFormat = "yyyyMMdd HHmmss";
        /// <summary>
        /// LES-SRM-005 物料拉动单  
        /// </summary>
        /// <param name="logFid"></param>
        public static ExecuteResultConstants Send(Guid logFid, InterfaceConfigInfo interfaceConfigInfo, ref string errorCode, ref string errorMsg, out string msgContent)
        {
            ///
            msgContent = string.Empty;
            ///
            List<SrmPullingOrderInfo> srmPullingOrderInfos = new SrmPullingOrderBLL().GetList("" +
                "[LOG_FID] = N'" + logFid + "' and " +
                "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", string.Empty);
            if (srmPullingOrderInfos.Count == 0)
            {
                errorCode = "MC:0x00000212";///经检测,传递数据为空,请确认
                return ExecuteResultConstants.Exception;
            }
            ///发送内容
            List<BFDAPullingOrderInfo> srmVmiPullingOrderInfos = new List<BFDAPullingOrderInfo>();
            foreach (var srmPullingOrderInfo in srmPullingOrderInfos)
            {
                srmVmiPullingOrderInfos.Add(GetSrmPullingOrderInfo(srmPullingOrderInfo));
            }
            BFDASRMSendDataInfo<BFDAPullingOrderInfo> sendDataInfo = new BFDASRMSendDataInfo<BFDAPullingOrderInfo>();
            sendDataInfo.List = srmVmiPullingOrderInfos;
            ///
            MaterialPullService_pttClient client = new MaterialPullService_pttClient();
            client.Endpoint.Address = new EndpointAddress(interfaceConfigInfo.CallUrl);
            ///数据发送
            msgContent = new XmlWrapper().ObjectToXmlByEncoding(sendDataInfo, Encoding.UTF8, false);
            msgContent = msgContent.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");
            string result = client.MaterialPullService(msgContent, out errorCode, out errorMsg);
            if (result == Convert.ToString((int)OutboundReturnStateConstants.FAILURE))
                return ExecuteResultConstants.Error;
            return ExecuteResultConstants.Success;
        }
        /// <summary>
        /// 对象转换
        /// </summary>
        /// <param name="srmPullingOrderInfo"></param>
        /// <returns></returns>
        private static BFDAPullingOrderInfo GetSrmPullingOrderInfo(SrmPullingOrderInfo srmPullingOrderInfo)
        {
            BFDAPullingOrderInfo pullingOrderInfo = new BFDAPullingOrderInfo();

            pullingOrderInfo.OrderCode = srmPullingOrderInfo.OrderNo.ToString();

            ///单据类型
            pullingOrderInfo.OrderType = srmPullingOrderInfo.OrderType.GetValueOrDefault().ToString();
            ///道口
            pullingOrderInfo.Dock = srmPullingOrderInfo.Dock;
            ///发单时间
            pullingOrderInfo.PublishTime = srmPullingOrderInfo.PublishTime.GetValueOrDefault().ToString(srmDateFormat);
            ///零件类代码
            pullingOrderInfo.PartBoxCode = srmPullingOrderInfo.PartBoxCode;
            ///零件类名称
            pullingOrderInfo.PartBoxName = srmPullingOrderInfo.PartBoxName;
            ///供应商代码
            pullingOrderInfo.SupplierCode = srmPullingOrderInfo.SupplierNum;
            ///供应商名称
            pullingOrderInfo.SupplierName = srmPullingOrderInfo.SupplierName;
            ///来源存储区代码
            pullingOrderInfo.SourceZoneNo = srmPullingOrderInfo.SourceZoneNo;
            ///目标存储区代码
            pullingOrderInfo.TargetZoneNo = srmPullingOrderInfo.TargetZoneNo;
            ///保管员
            pullingOrderInfo.Keeper = srmPullingOrderInfo.Keeper;
            ///预计发货时间
            pullingOrderInfo.PlanShippingTime = srmPullingOrderInfo.PlanShippingTime.GetValueOrDefault().ToString(srmDateFormat);
            ///预计到货时间
            pullingOrderInfo.PlanDeliveryTime = srmPullingOrderInfo.PlanDeliveryTime.GetValueOrDefault().ToString(srmDateFormat);
            ///备注
            pullingOrderInfo.Remark = srmPullingOrderInfo.Remark;
            ///是否允许编辑ASN
            pullingOrderInfo.AsnFlag = srmPullingOrderInfo.AsnFlag.GetValueOrDefault().ToString()=="true"?"1" : "0" ;
            ///是否紧急
            pullingOrderInfo.EmergencyFlag = srmPullingOrderInfo.EmergencyFlag.GetValueOrDefault().ToString()== "true" ? "1" : "0";
            ///删除标记
            pullingOrderInfo.DeleteFlag = string.Empty;
            ///工厂代码
            pullingOrderInfo.Plant = srmPullingOrderInfo.Plant;
            ///物料明细
            pullingOrderInfo.OrderDetail = new BFDAPullingOrderDetailInfos();
            pullingOrderInfo.OrderDetail.list = new List<BFDAPullingOrderDetailInfo>();
            ///获取详细的物料信息
            List<SrmPullingOrderDetailInfo> srmPullingOrderDetailInfos = new SrmPullingOrderDetailBLL().GetList("[ORDER_FID] = N'" + srmPullingOrderInfo.Fid.GetValueOrDefault() + "'", string.Empty);
            foreach (SrmPullingOrderDetailInfo srmPullingOrderDetailInfo in srmPullingOrderDetailInfos)
            {
                pullingOrderInfo.OrderDetail.list.Add(GetSrmPullingOrderDetailInfo(srmPullingOrderDetailInfo));
            }
            return pullingOrderInfo;
        }
        /// <summary>
        /// 子对象对象转换
        /// </summary>
        /// <param name="wmsPullingOrderInfo"></param>
        /// <returns></returns>
        private static BFDAPullingOrderDetailInfo GetSrmPullingOrderDetailInfo(SrmPullingOrderDetailInfo srmPullingOrderDetailInfo)
        {
            BFDAPullingOrderDetailInfo pullingOrderDetailInfo = new BFDAPullingOrderDetailInfo();
            ///物料编号	
            pullingOrderDetailInfo.PartNo = srmPullingOrderDetailInfo.PartNo;
            ///物料描述 
            pullingOrderDetailInfo.PartCName = srmPullingOrderDetailInfo.PartCname;
            ///收容数
            pullingOrderDetailInfo.SNP = srmPullingOrderDetailInfo.Snp.GetValueOrDefault();
            ///数量
            pullingOrderDetailInfo.PartQty = srmPullingOrderDetailInfo.PartQty.GetValueOrDefault();
            ///目标库位
            pullingOrderDetailInfo.TargetSLCode = srmPullingOrderDetailInfo.TargetSlcode;
            ///超市库位--新增
            pullingOrderDetailInfo.SuppermarketRepository = srmPullingOrderDetailInfo.SuppermarketRepository;
            ///包装型号
            pullingOrderDetailInfo.PackageCode = srmPullingOrderDetailInfo.PackageModel;
            ///备注
            pullingOrderDetailInfo.Remark = srmPullingOrderDetailInfo.Remark;
            ///
            return pullingOrderDetailInfo;
        }
    }
}
