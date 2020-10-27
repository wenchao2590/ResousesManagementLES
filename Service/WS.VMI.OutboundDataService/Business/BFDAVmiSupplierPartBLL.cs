using BLL.LES;
using BLL.SYS;
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
    /// LES-WMS-008	VMI供应商物料关系 LW005
    /// </summary>
    public class BFDAVmiSupplierPartBLL
    {
        /// <summary>
        /// 日期格式
        /// </summary>
        private static string vmiDateFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// LES-WMS-008	VMI供应商物料关系 LW005
        /// </summary>
        /// <param name="logFid"></param>
        public static ExecuteResultConstants SendSupplierMaterialRelationship(Guid logFid, InterfaceConfigInfo interfaceConfigInfo,
            ref string errorCode, ref string errorMsg, out string msgContent)
        {
            msgContent = string.Empty;
            List<WmsVmiSupplierPartInfo>  vmiSupplierParts = new WmsVmiSupplierPartBLL().GetList("[LOG_FID] = N'" + logFid + "' and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", string.Empty);
            if (vmiSupplierParts.Count == 0)
            {
                errorCode = "MC:0x00000212";///经检测,传递数据为空,请确认
                return ExecuteResultConstants.Exception;
            }

            ///转换发送内容
            List<BFDAVmiSupplierPartInfo>  fDAVmiSupplierPartInfos = new List<BFDAVmiSupplierPartInfo>();
            foreach (var vmiSupplierPartInfo in vmiSupplierParts)
            {
                fDAVmiSupplierPartInfos.Add(GetBFDAVMIInfo(vmiSupplierPartInfo));
            }
            BFDAVMISendDataInfo<BFDAVmiSupplierPartInfo> sendDataInfo = new BFDAVMISendDataInfo<BFDAVmiSupplierPartInfo>();
            sendDataInfo.List = fDAVmiSupplierPartInfos;

            ///调用
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
        private static BFDAVmiSupplierPartInfo GetBFDAVMIInfo(WmsVmiSupplierPartInfo vmiInfo)
        {
            BFDAVmiSupplierPartInfo  bFDAVmiSupplierPartInfo = new BFDAVmiSupplierPartInfo();
            bFDAVmiSupplierPartInfo.VmiWarehouseCode = vmiInfo.VmiWarehouseCode;///VMI仓库代码
            bFDAVmiSupplierPartInfo.VmiWarehouseName = vmiInfo.VmiWarehouseName;///VMI仓库名称
            bFDAVmiSupplierPartInfo.SupplierCode = vmiInfo.SupplierCode;///供应商代码
            bFDAVmiSupplierPartInfo.SupplierName = vmiInfo.SupplierName;///供应商名称
            bFDAVmiSupplierPartInfo.PartNo = vmiInfo.PartNo;///物料编号
            bFDAVmiSupplierPartInfo.PartCName = vmiInfo.PartCname;///物料中文描述
            bFDAVmiSupplierPartInfo.DeleteFlag = vmiInfo.DeleteFlag==true?"1":"0";///删除标记
            bFDAVmiSupplierPartInfo.Cartoncode = vmiInfo.Cartoncode;///器具代码
            bFDAVmiSupplierPartInfo.Cartonqty = vmiInfo.Cartonqty.GetValueOrDefault().ToString();///分装数量
            bFDAVmiSupplierPartInfo.Werks = vmiInfo.Werks;///工厂代码

            return bFDAVmiSupplierPartInfo;
        }
    }
}
