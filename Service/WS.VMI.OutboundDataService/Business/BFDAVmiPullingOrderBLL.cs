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
    /// LES-WMS-003-物料拉动单 LW002
    /// </summary>
    public class BFDAVmiPullingOrderBLL
    {
        /// <summary>
        /// 日期格式
        /// </summary>
        private static string vmiDateFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// LES-WMS-003-物料拉动单 LW002
        /// </summary>
        /// <param name="logFid"></param>
        public static ExecuteResultConstants SendPullingOrder(Guid logFid, InterfaceConfigInfo interfaceConfigInfo,
            ref string errorCode, ref string errorMsg, out string msgContent)
        {
            msgContent = string.Empty;
            List<WmsVmiPullingOrderInfo> wmsVmiPullingOrderInfos = new WmsVmiPullingOrderBLL().GetList("[LOG_FID] = N'" + logFid + "' and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", string.Empty);
            if (wmsVmiPullingOrderInfos.Count == 0)
            {
                errorCode = "MC:0x00000212";///经检测,传递数据为空,请确认
                return ExecuteResultConstants.Exception;
            }

            ///发送内容
            List<BFDAWmsVmiPullingOrderInfo> bFDAWmsVmiPullingOrderInfos = new List<BFDAWmsVmiPullingOrderInfo>();
            foreach (var vmiInboundOrderInfo in wmsVmiPullingOrderInfos)
            {
                bFDAWmsVmiPullingOrderInfos.Add(GetBFDAVmiInfo(vmiInboundOrderInfo));
            }
            BFDAVMISendDataInfo<BFDAWmsVmiPullingOrderInfo> sendDataInfo = new BFDAVMISendDataInfo<BFDAWmsVmiPullingOrderInfo>();
            sendDataInfo.List = bFDAWmsVmiPullingOrderInfos;
            ///
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
            Log.WriteLogToFile(msgContent, AppDomain.CurrentDomain.BaseDirectory + @"\logContent\", DateTime.Now.ToString("yyyyMMddHHmm")+ interfaceConfigInfo.SysMethodName);

            string result = client.runProcessWithAction(interfaceConfigInfo.Param1, tenantId, interfaceConfigInfo.SysMethodName, msgContent);

            BFDAVMIResultInfo resultInfo = new XmlWrapper(result, LoadType.FromString).XmlToObject("/Result", typeof(BFDAVMIResultInfo)) as BFDAVMIResultInfo;
            // throw new Exception(resultInfo.Status); TDD: delete
            ///成功后更新中间表数据处理状态
            if (resultInfo.Status.ToLower() == "error")
            {
                errorCode = resultInfo.ErrorCode;
                errorMsg = resultInfo.ErrorMsg;
                return ExecuteResultConstants.Error;
            }
            Log.WriteLogToFile("END: " + ExecuteResultConstants.Success, AppDomain.CurrentDomain.BaseDirectory + @"\log\", DateTime.Now.ToString("yyyyMMddHHmm"));

            return ExecuteResultConstants.Success;
        }

        /// <summary>
        /// 对象转换
        /// </summary>
        /// <param name="vmiInfo"></param>
        /// <returns></returns>
        private static BFDAWmsVmiPullingOrderInfo GetBFDAVmiInfo(WmsVmiPullingOrderInfo vmiInfo)
        {
            BFDAWmsVmiPullingOrderInfo BfdaPullingOrderInfo = new BFDAWmsVmiPullingOrderInfo();

            BfdaPullingOrderInfo.OrderCode = vmiInfo.OrderNo;

            BfdaPullingOrderInfo.OrderType = vmiInfo.OrderType.ToString();

            BfdaPullingOrderInfo.Dock = vmiInfo.Dock;

            BfdaPullingOrderInfo.PublishTime = vmiInfo.PublishTime.GetValueOrDefault().ToString(vmiDateFormat);

            BfdaPullingOrderInfo.PartBoxCode = vmiInfo.PartBoxCode;

            BfdaPullingOrderInfo.PartBoxName = vmiInfo.PartBoxName;

            BfdaPullingOrderInfo.SourceZoneNo = vmiInfo.SourceZoneNo;

            BfdaPullingOrderInfo.TargetZoneNo = vmiInfo.TargetZoneNo;

            BfdaPullingOrderInfo.Keeper = vmiInfo.Keeper;

            BfdaPullingOrderInfo.PlanShippingTime = vmiInfo.PlanShippingTime.GetValueOrDefault().ToString(vmiDateFormat);

            BfdaPullingOrderInfo.PlanDeliveryTime = vmiInfo.PlanDeliveryTime.GetValueOrDefault().ToString(vmiDateFormat);

            BfdaPullingOrderInfo.Remark = vmiInfo.Remark;
            /// 确认是:否 =  1:0
            BfdaPullingOrderInfo.AsnFlag = vmiInfo.AsnFlag == true ? "1" : "0";

            BfdaPullingOrderInfo.EmergencyFlag = vmiInfo.EmergencyFlag == true ? "1" : "0";

            BfdaPullingOrderInfo.WintimeCode = vmiInfo.PlanDeliveryTime.GetValueOrDefault().ToString(vmiDateFormat);

            BfdaPullingOrderInfo.WintimeDesc = vmiInfo.Remark;

            BfdaPullingOrderInfo.Werks = vmiInfo.Plant;

            BfdaPullingOrderInfo.OrderDetail = new BFDAVmiPullingOrderDetailInfos();
            BfdaPullingOrderInfo.OrderDetail.list = new List<BFDAVmiPullingOrderDetailInfo>();
            ///获取详细的物料信息
            List<WmsVmiPullingOrderDetailInfo> detailInfos = new WmsVmiPullingOrderDetailBLL().GetList(" and [ORDER_FID] = '" + vmiInfo.Fid + "' ", string.Empty);

            if (detailInfos.Count() > 0)
            {
                foreach (WmsVmiPullingOrderDetailInfo item in detailInfos)
                {
                    BfdaPullingOrderInfo.OrderDetail.list.Add(GetBFDAVMIOrderDetailInfo(item));
                }
            }


            return BfdaPullingOrderInfo;
        }

        /// <summary>
        /// 子对象对象转换
        /// </summary>
        /// <param name="wmsPullingOrderInfo"></param>
        /// <returns></returns>
        private static BFDAVmiPullingOrderDetailInfo GetBFDAVMIOrderDetailInfo(WmsVmiPullingOrderDetailInfo vmiInfo)
        {
            BFDAVmiPullingOrderDetailInfo detailInfo = new BFDAVmiPullingOrderDetailInfo();
            detailInfo.PartNo = vmiInfo.PartNo;                             ///物料编号	
            detailInfo.PartCName = vmiInfo.PartCname;                       ///物料描述	
            detailInfo.SNP = vmiInfo.Snp.GetValueOrDefault();              ///收容数	
            detailInfo.PartQty = vmiInfo.PartQty.GetValueOrDefault();       ///数量		
            detailInfo.TargetSLCode = vmiInfo.Targetslcode;                 ///目标库位	
            detailInfo.SuppermarketRepository = vmiInfo.Suppermarketrepository;       ///超市库位	
            detailInfo.PackageCode = vmiInfo.PackageCode.ToString();        ///包装型号	
            detailInfo.Remark = vmiInfo.Remark;                             ///备注		
            detailInfo.SupplierCode = vmiInfo.Suppliercode;                 ///供应商代码
            detailInfo.SupplierName = vmiInfo.Suppliername;                 ///供应商名称
            detailInfo.VerifyMode = vmiInfo.Verifymode;                     ///检验模式	
            detailInfo.EXTERNLINENO = vmiInfo.Externlineno;                 ///行序列号	
            
            return detailInfo;
        }
    }
}
