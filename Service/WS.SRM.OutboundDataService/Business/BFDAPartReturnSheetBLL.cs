namespace WS.SRM.OutboundDataService
{
    using DAL.LES;
    using DM.LES;
    using DM.SYS;
    using Infrustructure.Logging;
    using Infrustructure.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;
    /// <summary>
    /// LES-SRM-009 物料退货单
    /// </summary>
    public class BFDAPartReturnSheetBLL
    {

        /// <summary>
        /// 日期格式
        /// </summary>
        private static string srmDateFormat = "yyyy-MM-dd";
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
            List<SrmPartReturnSheetInfo> srmPartReturnSheetInfos = new SrmPartReturnSheetDAL().GetList("[LOG_FID] = N'" + logFid + "' and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", string.Empty);
            if (srmPartReturnSheetInfos.Count == 0)
            {
                errorCode = "MC:0x00000212";///经检测,传递数据为空,请确认
                return ExecuteResultConstants.Exception;
            }
            ///转换成BFDA集合
            List<BFDAPartReturnSheetInfo> BFDAInfos = new List<BFDAPartReturnSheetInfo>();
            foreach (var srmPartReturnSheetInfo in srmPartReturnSheetInfos)
            {
                BFDAInfos.Add(GetSrmPartReturnSheetInfo(srmPartReturnSheetInfo));
            }
            ///准备把集合转成一个对象
            BFDASRMSendDataInfo<BFDAPartReturnSheetInfo> sendDataInfo = new BFDASRMSendDataInfo<BFDAPartReturnSheetInfo>();
            sendDataInfo.List = BFDAInfos;
            /// 
            SRMMaterialRefundWSDL.MaterialRefundService_pttClient client = new SRMMaterialRefundWSDL.MaterialRefundService_pttClient();
            client.Endpoint.Address = new EndpointAddress(interfaceConfigInfo.CallUrl);
            ///数据发送
            msgContent = new XmlWrapper().ObjectToXmlByEncoding(sendDataInfo, Encoding.UTF8, false);
            msgContent = msgContent.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");
            ///
            string result = client.MaterialRefundService(msgContent, out errorCode, out errorMsg);
            if (result == Convert.ToString((int)OutboundReturnStateConstants.FAILURE))
                return ExecuteResultConstants.Error;
            return ExecuteResultConstants.Success;
        }
        /// <summary>
        /// 对象转换
        /// </summary>
        /// <param name="srmPartReturnSheetInfo"></param>
        /// <returns></returns>
        private static BFDAPartReturnSheetInfo GetSrmPartReturnSheetInfo(SrmPartReturnSheetInfo srmPartReturnSheetInfo)
        {
            BFDAPartReturnSheetInfo info = new BFDAPartReturnSheetInfo();
            info.Plant = srmPartReturnSheetInfo.Plant; ///工厂
            info.OrderCode = srmPartReturnSheetInfo.Ordercode; ///退货单号
            info.Dock = srmPartReturnSheetInfo.Dock; ///道口
            info.PublishTime = srmPartReturnSheetInfo.Publishtime.GetValueOrDefault().ToString(srmDateFormat); ///发单时间
            info.SupplierCode = srmPartReturnSheetInfo.Suppliercode; ///供应商代码
            info.SupplierName = srmPartReturnSheetInfo.Suppliername; ///供应商名称
            info.SourceZoneNo = srmPartReturnSheetInfo.Sourcezoneno; ///来源存储区代码
            info.Keeper = srmPartReturnSheetInfo.Keeper; ///保管员
            info.Remark = srmPartReturnSheetInfo.Remark; ///备注
            info.DeleteFlag = srmPartReturnSheetInfo.Deleteflag.GetValueOrDefault() ? "1" : "0";        ///删除标记

            ///
            info.DetailsInfo = new BFDAPartReturnSheetDetailInfos();
            info.DetailsInfo.Parts = new List<BFDAPartReturnSheetDetailInfo>();
            ///获取详细的订单信息
            List<SrmPartReturnSheetDetailInfo> srmSheetDetailInfos = new SrmPartReturnSheetDetailDAL().GetList("[ORDER_FID] = N'" + srmPartReturnSheetInfo.Fid.GetValueOrDefault() + "'", string.Empty);
            foreach (SrmPartReturnSheetDetailInfo srmSheetDetailInfo in srmSheetDetailInfos)
            {
                info.DetailsInfo.Parts.Add(GetSrmPartReturnSheetDetailInfo(srmSheetDetailInfo));
            }
            return info;
        }
        /// <summary>
        /// 子对象对象转换
        /// </summary>
        /// <param name="wmsPullingOrderInfo"></param>
        /// <returns></returns>
        private static BFDAPartReturnSheetDetailInfo GetSrmPartReturnSheetDetailInfo(SrmPartReturnSheetDetailInfo srmPartReturnSheetDetailInfo)
        {
            BFDAPartReturnSheetDetailInfo detailInfo = new BFDAPartReturnSheetDetailInfo();
            detailInfo.PartNo = srmPartReturnSheetDetailInfo.PartNo;///物料编号
            detailInfo.PartCName = srmPartReturnSheetDetailInfo.PartCname;///物料编号
            detailInfo.PartQty = srmPartReturnSheetDetailInfo.PartQty.GetValueOrDefault().ToString();///收容数
            detailInfo.TargetSLCode = srmPartReturnSheetDetailInfo.TargetSlcode;///车型代码
            detailInfo.Remark = srmPartReturnSheetDetailInfo.Remark;///备注
            return detailInfo;
        }
    }
}
