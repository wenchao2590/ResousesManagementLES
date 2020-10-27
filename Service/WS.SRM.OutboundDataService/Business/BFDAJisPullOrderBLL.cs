using BLL.LES;
using DM.LES;
using DM.SYS;
using Infrustructure.Logging;
using Infrustructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using WS.SRM.OutboundDataService.SRMJisPullWSDL;

namespace WS.SRM.OutboundDataService
{

    /// <summary>
    /// LES-WMS-004 VMI JIS 拉动单 (排序拉动单) 
    /// </summary>
    public class BFDAJisPullOrderBLL
    {
        /// <summary>
        /// 日期格式
        /// </summary>
        private static readonly string srmDateFormat = "yyyyMMdd HHmmss";
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
            List<SrmJisPullOrderInfo> srmJisPullOrderInfos = new SrmJisPullOrderBLL().GetList("[LOG_FID] = N'" + logFid + "' and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", string.Empty);
            if (srmJisPullOrderInfos.Count == 0)
            {
                errorCode = "MC:0x00000212";///经检测,传递数据为空,请确认
                return ExecuteResultConstants.Exception;
            }
            ///转换成BFDA集合
            List<BFDAJisPullOrderInfo> list = new List<BFDAJisPullOrderInfo>();
            foreach (var srmJisPullOrderInfo in srmJisPullOrderInfos)
            {
                list.Add(GetSrmJisPullOrderInfo(srmJisPullOrderInfo));
            }
            ///准备把集合转成一个对象
            BFDASRMSendDataInfo<BFDAJisPullOrderInfo> sendDataInfo = new BFDASRMSendDataInfo<BFDAJisPullOrderInfo>
            {
                List = list
            };
            ///
            msgContent = new XmlWrapper().ObjectToXmlByEncoding(sendDataInfo, Encoding.UTF8, false);
            msgContent = msgContent.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");
            ///
            JISPullService_pttClient client = new JISPullService_pttClient();
            client.Endpoint.Address = new EndpointAddress(interfaceConfigInfo.CallUrl);
            ///数据发送
            string result = client.JISPullService(msgContent, out errorCode, out errorMsg);
            if (result == Convert.ToString((int)OutboundReturnStateConstants.FAILURE))
                return ExecuteResultConstants.Error;
            return ExecuteResultConstants.Success;
        }
        /// <summary>
        /// 对象转换
        /// </summary>
        /// <param name="srmJisPullOrderInfo"></param>
        /// <returns></returns>
        private static BFDAJisPullOrderInfo GetSrmJisPullOrderInfo(SrmJisPullOrderInfo srmJisPullOrderInfo)
        {
            BFDAJisPullOrderInfo info = new BFDAJisPullOrderInfo
            {
                ///工厂
                Plant = srmJisPullOrderInfo.Plant,
                ///拉动单号
                OrderCode = srmJisPullOrderInfo.OrderCode,
                ///道口
                Dock = srmJisPullOrderInfo.Dock,
                ///发单时间 
                PublishTime = srmJisPullOrderInfo.PublishTime.GetValueOrDefault().ToString(srmDateFormat),
                ///零件类代码
                PartBoxCode = srmJisPullOrderInfo.PartBoxCode,
                ///零件类名称
                PartBoxName = srmJisPullOrderInfo.PartBoxName,
                ///供应商代码
                SupplierCode = srmJisPullOrderInfo.SupplierNum,
                ///供应商名称
                SupplierName = srmJisPullOrderInfo.SupplierName,
                ///来源存储区代码
                SourceZoneNo = srmJisPullOrderInfo.SourceZoneNo,
                ///目标存储区代码
                TargetZoneNo = srmJisPullOrderInfo.TargetZoneNo,
                ///开始过点时间
                StartInfoPointTime = srmJisPullOrderInfo.StartInfopointTime.GetValueOrDefault().ToString(srmDateFormat),
                ///预计到货时间
                PlanDeliveryTime = srmJisPullOrderInfo.PlanDeliveryTime.GetValueOrDefault().ToString(srmDateFormat),
                ///开始车辆序号
                StartVehicleSeqNo = srmJisPullOrderInfo.StartVehicleSeqNo.GetValueOrDefault().ToString(),
                ///结束车辆序号
                EndVehicleSeqNo = srmJisPullOrderInfo.EndVehicleSeqNo.GetValueOrDefault().ToString(),
                ///工位
                Location = srmJisPullOrderInfo.Location,
                ///备注
                Remark = srmJisPullOrderInfo.Remark,
                ///删除标记
                DeleteFlag = srmJisPullOrderInfo.Deleteflag.GetValueOrDefault() ? "1" : "0",
                ///明细
                OrderDetail = new BFDAJisPullOrderDetailInfos()
            };
            info.OrderDetail.list = new List<BFDAJisPullOrderDetailInfo>();
            ///获取详细的订单信息
            List<SrmJisPullOrderDetailInfo> srmJisPullOrderDetailInfos = new SrmJisPullOrderDetailBLL().GetList("[ORDER_FID] = N'" + srmJisPullOrderInfo.Fid.GetValueOrDefault() + "'", string.Empty);
            foreach (SrmJisPullOrderDetailInfo srmJisPullOrderDetailInfo in srmJisPullOrderDetailInfos)
            {
                info.OrderDetail.list.Add(GetSrmJisPullOrderDetailInfo(srmJisPullOrderDetailInfo));
            }
            return info;
        }
        /// <summary>
        /// 子对象对象转换
        /// </summary>
        /// <param name="wmsPullingOrderInfo"></param>
        /// <returns></returns>
        private static BFDAJisPullOrderDetailInfo GetSrmJisPullOrderDetailInfo(SrmJisPullOrderDetailInfo srmJisPullOrderDetailInfo)
        {
            BFDAJisPullOrderDetailInfo detailInfo = new BFDAJisPullOrderDetailInfo
            {
                ///订单号
                OrderCode = srmJisPullOrderDetailInfo.OrderCode,
                ///车辆序号
                VehicleSeqNo = srmJisPullOrderDetailInfo.VehicleSeqNo.GetValueOrDefault().ToString(),
                ///物料编号
                PartNo = srmJisPullOrderDetailInfo.PartNo,
                ///收容数
                SNP = srmJisPullOrderDetailInfo.PartQty.GetValueOrDefault().ToString(),
                ///数量
                PartQty = srmJisPullOrderDetailInfo.PartQty.GetValueOrDefault().ToString(),
                ///车型代码
                VehicleModelNo = srmJisPullOrderDetailInfo.VehicleModelNo,
                ///VIN号
                VINCode = srmJisPullOrderDetailInfo.Vincode,
                ///备注
                Remark = srmJisPullOrderDetailInfo.Remark
            };
            ///
            return detailInfo;
        }
    }
}
