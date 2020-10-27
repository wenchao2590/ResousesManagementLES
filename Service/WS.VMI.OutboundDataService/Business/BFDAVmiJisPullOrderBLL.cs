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
    /// LES-WMS-004 VMI JIS 拉动单 (排序拉动单) LW003
    /// </summary>
    public class BFDAVmiJisPullOrderBLL
    {

        /// <summary>
        /// 日期格式
        /// </summary>
        private static string vmiDateFormat = "yyyy-MM-dd HH:mm:ss";
        /// <summary>
        /// LES-SRM-004 排序拉动单 
        // <param name="logFid"></param>
        public static ExecuteResultConstants SendJisPullOrder(Guid logFid, InterfaceConfigInfo interfaceConfigInfo,
            ref string errorCode, ref string errorMsg, out string msgContent)
        {
            msgContent = string.Empty;
            List<WmsVmiJisPullOrderInfo> vmiJisPullOrderInfos = new WmsVmiJisPullOrderBLL().GetList("[LOG_FID] = N'" + logFid + "' and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", string.Empty);
            if (vmiJisPullOrderInfos.Count == 0)
            {
                errorCode = "MC:0x00000212";///经检测,传递数据为空,请确认
                return ExecuteResultConstants.Exception;
            }

            ///发送内容
            List<BFDAVmiJisPullOrderInfo> inboundOrderInfos = new List<BFDAVmiJisPullOrderInfo>();
            foreach (var vmiJisPullOrderInfo in vmiJisPullOrderInfos)
            {
                inboundOrderInfos.Add(GetBFDAInfo(vmiJisPullOrderInfo));
            }
            BFDAVMISendDataInfo<BFDAVmiJisPullOrderInfo> sendDataInfo = new BFDAVMISendDataInfo<BFDAVmiJisPullOrderInfo>();
            sendDataInfo.List = inboundOrderInfos;

            WsProcessServiceClient client = new WsProcessServiceClient();
            client.Endpoint.Address = new EndpointAddress(interfaceConfigInfo.CallUrl);
            ///1标识成功、0标识失败
            long tenantId = 89;
            if (!long.TryParse(interfaceConfigInfo.Param2, out tenantId))
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
        /// 获取报文体
        /// </summary>
        /// <param name="inboundOrderInfos"></param>
        /// <returns></returns>
        private static string GetMsgContent(List<BFDAVmiPackageInboundInfo> inboundOrderInfos)
        {
            ///TODO:若数据量过大，其实建议取消
            return new XmlWrapper().ObjectToXml(inboundOrderInfos, false);
        }
        /// <summary>
        /// 对象转换
        /// </summary>
        /// <param name="wmsVmiJisPullOrderInfo"></param>
        /// <returns></returns>
        private static BFDAVmiJisPullOrderInfo GetBFDAInfo(WmsVmiJisPullOrderInfo  wmsVmiJisPullOrderInfo)
        {
            BFDAVmiJisPullOrderInfo bfdaInboundOrderInfo = new BFDAVmiJisPullOrderInfo();
            ///TODO:此处获取时间默认值为0001-01-01，对方系统是否能够接收，待测

            bfdaInboundOrderInfo.Werks = wmsVmiJisPullOrderInfo.Plant ;                     ///工厂
            bfdaInboundOrderInfo.Keeper = wmsVmiJisPullOrderInfo.Keeper;                   ///保管员 	 

            bfdaInboundOrderInfo.OrderCode = wmsVmiJisPullOrderInfo.OrderCode;                     ///拉动单号		
            bfdaInboundOrderInfo.Dock = wmsVmiJisPullOrderInfo.Dock;                               ///道口 	 
            bfdaInboundOrderInfo.SequenceNumbe = wmsVmiJisPullOrderInfo.Sequencenumbe;             ///当日单据顺序号	            

            bfdaInboundOrderInfo.PublishTime = Convert.ToDateTime( wmsVmiJisPullOrderInfo.PublishTime).ToString(vmiDateFormat);      ///发单时间 		
            bfdaInboundOrderInfo.PartBoxCode = wmsVmiJisPullOrderInfo.PartBoxCode;                 ///零件类代码 		
            bfdaInboundOrderInfo.PartBoxName = wmsVmiJisPullOrderInfo.PartBoxName;                 ///零件类名称		
            bfdaInboundOrderInfo.SupplierCode = wmsVmiJisPullOrderInfo.SupplierNum;               ///供应商代码		
            bfdaInboundOrderInfo.SupplierName = wmsVmiJisPullOrderInfo.SupplierName;               ///供应商名称 		
            bfdaInboundOrderInfo.SourceZoneNo = wmsVmiJisPullOrderInfo.SourceZoneNo;               ///来源存储区代码	
            bfdaInboundOrderInfo.TargetZoneNo = wmsVmiJisPullOrderInfo.TargetZoneNo;               ///目标存储区代码	
            bfdaInboundOrderInfo.StartInfoPointTime = wmsVmiJisPullOrderInfo.StartInfopoinTtime.GetValueOrDefault().ToString(vmiDateFormat);///开始过点时间	
            bfdaInboundOrderInfo.PlanDeliveryTime = wmsVmiJisPullOrderInfo.PlanDeliveryTime.GetValueOrDefault().ToString(vmiDateFormat);///预计到货时间	
            bfdaInboundOrderInfo.StartVehicleSeqNo = wmsVmiJisPullOrderInfo.StartVehicleSeqNo.GetValueOrDefault().ToString();///开始车辆序号	
            bfdaInboundOrderInfo.EndVehicleSeqNo = wmsVmiJisPullOrderInfo.EndVehicleseqNo.GetValueOrDefault().ToString();///结束车辆序号	
            bfdaInboundOrderInfo.Location = wmsVmiJisPullOrderInfo.Location;                       ///工位			
            bfdaInboundOrderInfo.Remark = wmsVmiJisPullOrderInfo.Remark;                           ///备注			
            bfdaInboundOrderInfo.DeleteFlag = wmsVmiJisPullOrderInfo.Deleteflag == true ? "1" : "0";        ///删除标记	

            bfdaInboundOrderInfo.OrderDetail = new BFDAVmiJisPullOrderDetailInfos();
            bfdaInboundOrderInfo.OrderDetail.list = new List<BFDAVmiJisPullOrderDetailInfo>();

            ///获取详细的订单信息
            List<WmsVmiJisPullOrderDetailInfo> wmsVmiPullingOrders = new WmsVmiJisPullOrderDetailBLL().GetList(" AND [ORDER_FID] = '" + wmsVmiJisPullOrderInfo.Fid+"' ", string.Empty);
            
            if (wmsVmiPullingOrders.Count() > 0)
            {
                foreach (var item in wmsVmiPullingOrders)
                {
                    bfdaInboundOrderInfo.OrderDetail.list.Add(GetBFDADetailInfo(item));
                }
            }
          
            return bfdaInboundOrderInfo;
        }


        /// <summary>
        /// 子对象对象转换
        /// </summary>
        /// <param name="wmsPullingOrderInfo"></param>
        /// <returns></returns>
        private static BFDAVmiJisPullOrderDetailInfo GetBFDADetailInfo(WmsVmiJisPullOrderDetailInfo wmsVmiJisPullOrderInfo)
        {
            BFDAVmiJisPullOrderDetailInfo detailInfo = new BFDAVmiJisPullOrderDetailInfo();
            detailInfo.VehicleSeqNo = wmsVmiJisPullOrderInfo.VehicleSeqNo.GetValueOrDefault().ToString();///车辆序号
            detailInfo.PartNo = wmsVmiJisPullOrderInfo.Partno;///物料编号
            detailInfo.SNP = wmsVmiJisPullOrderInfo.Snp.GetValueOrDefault().ToString();///收容数
            detailInfo.PartQty = wmsVmiJisPullOrderInfo.PartQty.GetValueOrDefault().ToString();///数量
            detailInfo.VehicleModelNo = wmsVmiJisPullOrderInfo.VehicleModelNo;///车型代码
            detailInfo.VINCode = wmsVmiJisPullOrderInfo.Vincode;///VIN号
            detailInfo.Remark = wmsVmiJisPullOrderInfo.Remark;///备注
            detailInfo.SupermarketRepository = wmsVmiJisPullOrderInfo.SupermarketRepository;///超市库位
            detailInfo.ExternLineNo = wmsVmiJisPullOrderInfo.ExternLineNo;///行序列号

            return detailInfo;
        }
    }
}
