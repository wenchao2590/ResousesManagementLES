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
using WS.VMI.OutboundDataService.VMIWSDL;

namespace WS.VMI.OutboundDataService
{

    /// <summary>
    /// LES-WMS-001 物料送货单(又名-供应商发货单)
    /// </summary>
    public class BFDAInboundOrderBLL
    {
        /// <summary>
        /// 日期格式
        /// </summary>
        private static string vmiDateFormat = "yyyy-MM-dd HH:mm:ss";
        /// <summary>
        /// LES-WMS-001 物料发货单 (又名供应商发货单) LW001
        /// </summary>
        /// <param name="logFid"></param>
        /// <param name="interfaceConfigInfo"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMsg"></param>
        /// <param name="msgContent"></param>
        /// <returns></returns>
        public static ExecuteResultConstants SendInboundOrder(Guid logFid, InterfaceConfigInfo interfaceConfigInfo, ref string errorCode, ref string errorMsg, out string msgContent)
        {
            msgContent = string.Empty;
            List<WmsVmiInboundOrderInfo> wmsVmiInboundOrderInfos = new WmsVmiInboundOrderBLL().GetList("" +
                "[LOG_FID] = N'" + logFid + "' and " +
                "[PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", string.Empty);
            if (wmsVmiInboundOrderInfos.Count == 0)
            {
                errorCode = "MC:0x00000212";///经检测,传递数据为空,请确认
                return ExecuteResultConstants.Exception;
            }
            ///发送内容
            List<BFDAVMIInboundOrderInfo> inboundOrderInfos = new List<BFDAVMIInboundOrderInfo>();
            foreach (var wmsVmiInboundOrderInfo in wmsVmiInboundOrderInfos)
            {
                inboundOrderInfos.Add(GetInboundOrderInfo(wmsVmiInboundOrderInfo));
            }
            BFDAVMISendDataInfo<BFDAVMIInboundOrderInfo> sendDataInfo = new BFDAVMISendDataInfo<BFDAVMIInboundOrderInfo>();
            sendDataInfo.List = inboundOrderInfos;
            ///
            WsProcessServiceClient client = new WsProcessServiceClient();
            client.Endpoint.Address = new EndpointAddress(interfaceConfigInfo.CallUrl);
            ///
            if (!long.TryParse(interfaceConfigInfo.Param2, out long tenantId))
            {
                errorCode = "MC:3x00000021";///接口配置错误
                return ExecuteResultConstants.Exception;
            }
            ///数据发送
            msgContent = new XmlWrapper().ObjectToXml(sendDataInfo, false);
            string result = client.runProcessWithAction(interfaceConfigInfo.Param1, tenantId, interfaceConfigInfo.SysMethodName, msgContent);
            BFDAVMIResultInfo resultInfo = new XmlWrapper(result, LoadType.FromString).XmlToObject("/Result", typeof(BFDAVMIResultInfo)) as BFDAVMIResultInfo;
            ///成功后更新中间表数据处理状态
            if (resultInfo.Status.ToLower() == "error")
            {
                errorCode = resultInfo.ErrorCode;
                errorMsg = resultInfo.ErrorMsg;
                return ExecuteResultConstants.Error;
            }
            return ExecuteResultConstants.Success;
        }
        /// <summary>
        /// 对象转换
        /// </summary>
        /// <param name="vmiInboundOrderInfo"></param>
        /// <returns></returns>
        private static BFDAVMIInboundOrderInfo GetInboundOrderInfo(WmsVmiInboundOrderInfo vmiInboundOrderInfo)
        {
            BFDAVMIInboundOrderInfo inboundOrderInfo = new BFDAVMIInboundOrderInfo();
            ///发货单号
            inboundOrderInfo.ShippingCode = vmiInboundOrderInfo.ShippingCode;
            ///供应商代码
            inboundOrderInfo.SupplierCode = vmiInboundOrderInfo.SupplierCode;
            ///计划到货时间
            inboundOrderInfo.DeliveryTime = vmiInboundOrderInfo.DeliveryTime.GetValueOrDefault().ToString(vmiDateFormat);
            ///VMI仓库代码
            inboundOrderInfo.VmiWmNo = vmiInboundOrderInfo.VmiWmNo;
            ///订单类型，固定值10
            inboundOrderInfo.OrderType = vmiInboundOrderInfo.OrderType;
            ///状态
            inboundOrderInfo.Status = vmiInboundOrderInfo.Status;
            ///工厂代码
            inboundOrderInfo.Werks = vmiInboundOrderInfo.Werks;
            inboundOrderInfo.orderDetailInfos = new BFDAVMIInboundOrderDetailInfos();
            inboundOrderInfo.orderDetailInfos.listDetailInfo = new List<BFDAVMIInboundOrderDetailInfo>();
            ///明细
            List<WmsVmiInboundOrderDetailInfo> inboundOrderDetailInfos = new WmsVmiInboundOrderDetailBLL().GetList("[ORDER_FID] = N'" + vmiInboundOrderInfo.Fid.GetValueOrDefault() + "'", string.Empty);
            foreach (WmsVmiInboundOrderDetailInfo inboundOrderDetailInfo in inboundOrderDetailInfos)
            {
                inboundOrderInfo.orderDetailInfos.listDetailInfo.Add(GetInboundOrderDetailInfo(inboundOrderDetailInfo));
            }
            return inboundOrderInfo;
        }
        /// <summary>
        /// WmsVmiInboundOrderDetailInfo -> BFDAVMIInboundOrderDetailInfo
        /// </summary>
        /// <param name="vmiInboundOrderDetailInfo"></param>
        /// <returns></returns>
        private static BFDAVMIInboundOrderDetailInfo GetInboundOrderDetailInfo(WmsVmiInboundOrderDetailInfo vmiInboundOrderDetailInfo)
        {
            BFDAVMIInboundOrderDetailInfo inboundOrderDetailInfo = new BFDAVMIInboundOrderDetailInfo();
            ///物料编号
            inboundOrderDetailInfo.PartNo = vmiInboundOrderDetailInfo.Partno;
            ///收容数
            inboundOrderDetailInfo.SNP = vmiInboundOrderDetailInfo.Snp.GetValueOrDefault();
            ///数量
            inboundOrderDetailInfo.PartQty = vmiInboundOrderDetailInfo.Partqty.GetValueOrDefault();
            ///包装类型
            inboundOrderDetailInfo.PackageCode = vmiInboundOrderDetailInfo.Packagecode;
            ///备注
            inboundOrderDetailInfo.Remark = vmiInboundOrderDetailInfo.Remark;
            return inboundOrderDetailInfo;
        }
    }
}
