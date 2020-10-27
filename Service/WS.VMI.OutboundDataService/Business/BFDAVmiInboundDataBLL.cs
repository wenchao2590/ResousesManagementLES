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
    /// LES-WMS-005	入库数据 WL004
    /// </summary>
    public class BFDAVmiInboundDataBLL
    {
      
       /// 
        /// <summary>
        /// 日期格式
        /// </summary>
        private static string vmiDateFormat = "yyyy-MM-dd HH:mm:ss";
        /// <summary>
        ///  LES-WMS-005	入库数据 WL004
        // <param name="logFid"></param>
        public static ExecuteResultConstants SendInboundOrder
(Guid logFid, InterfaceConfigInfo interfaceConfigInfo,
            ref string errorCode, ref string errorMsg, out string msgContent)
        {
            msgContent = string.Empty;
            List<WmsTranOutInfo> vmiInboundOrderInfos = new WmsTranOutBLL().GetList("[LOG_FID] = N'" + logFid + "' and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", string.Empty);
            if (vmiInboundOrderInfos.Count == 0)
            {
                errorCode = "MC:0x00000212";///经检测,传递数据为空,请确认
                return ExecuteResultConstants.Exception;
            }

            ///发送内容
            List<BFDAVmiInboundDataInfo> bFDAVmiInboundDataInfos = new List<BFDAVmiInboundDataInfo>();
 
            foreach (var VmiInboundDataInfo in vmiInboundOrderInfos)
            {
                bFDAVmiInboundDataInfos.Add(GetBFDAVMIInfo(VmiInboundDataInfo));
            }
            BFDAVMISendDataInfo<BFDAVmiInboundDataInfo> sendDataInfo = new BFDAVMISendDataInfo<BFDAVmiInboundDataInfo>();
            sendDataInfo.List = bFDAVmiInboundDataInfos;

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
        /// <param name="wmsVmiInboundDataInfo"></param>
        /// <returns></returns>
        private static BFDAVmiInboundDataInfo GetBFDAVMIInfo(WmsTranOutInfo wmsTranOutInfo)
        {
            var runsheetInfo = new WmsVmiAsnRunsheetDetailBLL().GetList(string.Format(" SOURCEORDERCODE='{0}' AND WMSSOURCEKEY='{1}' AND PARTNO='{2}' AND VALID_FLAG=1 ", wmsTranOutInfo.RunsheetNo, wmsTranOutInfo.SourceOrderCode, wmsTranOutInfo.PartNo), "").FirstOrDefault();
            if (runsheetInfo != null)
            {
                wmsTranOutInfo.ItemNumber = runsheetInfo.Wmslinenumber;
            }

            BFDAVmiInboundDataInfo BfdaInfo = new BFDAVmiInboundDataInfo();
            ///TODO:此处获取时间默认值为0001-01-01，对方系统是否能够接收，待测

            BfdaInfo.SourceOrderCode = wmsTranOutInfo.RunsheetNo;   ///原始单据号

            BfdaInfo.SourceOrderType = wmsTranOutInfo.SourceOrderType.GetValueOrDefault().ToString();   ///原始单据类型
            BfdaInfo.PartNo = wmsTranOutInfo.PartNo;   ///物料编号
            BfdaInfo.SupplierCode = wmsTranOutInfo.SupplierNum;   ///供应商代码
            BfdaInfo.SupplierName = wmsTranOutInfo.SupplierName;   ///供应商名称
            BfdaInfo.DeliveryQty = wmsTranOutInfo.DeliveryQty.GetValueOrDefault().ToString();   ///实收数量

            BfdaInfo.Wmssourcekey = wmsTranOutInfo.SourceOrderCode;   ///WMS单号
            BfdaInfo.Wmslinenumber = wmsTranOutInfo.ItemNumber;   ///WMS行号

            BfdaInfo.VmiWarehouseCode = wmsTranOutInfo.WmNo;   ///VMI仓库代码
            BfdaInfo.Werks = wmsTranOutInfo.Plant;   ///工厂代码

            return BfdaInfo;
        }
    }
}
