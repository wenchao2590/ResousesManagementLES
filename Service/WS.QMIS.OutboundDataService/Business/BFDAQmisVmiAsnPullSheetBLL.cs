using BLL.LES;
using DM.LES;
using DM.SYS;
using Infrustructure.Logging;
using Infrustructure.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Xml;
using WS.QMIS.OutboundDataService.BFDAQMISAsnPull;
namespace WS.QMIS.OutboundDataService
{


    /// <summary>
    /// LES-QMIS-002-ASN单据下发接口  
    /// </summary>
    public class BFDAQmisVmiAsnPullSheetBLL
    {
        /// <summary>
        /// 日期格式
        /// </summary>
        private static string vmiDateFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// LES-QMIS-002-ASN单据下发接口
        /// </summary>
        /// <param name="logFid"></param>
        public static ExecuteResultConstants SendQmisAsnPullSheet(Guid logFid, InterfaceConfigInfo interfaceConfigInfo,
                ref string errorCode, ref string errorMsg, out string msgContent)
        {
            msgContent = string.Empty;

            List<QmisAsnPullSheetInfo> qmiInfos = new QmisAsnPullSheetBLL().GetList("[LOG_FID] = N'" + logFid + "' and [PROCESS_FLAG] = " + (int)ProcessFlagConstants.Untreated + "", string.Empty);
            if (qmiInfos.Count == 0)
            {
                errorCode = "MC:0x00000212";///经检测,传递数据为空,请确认
                return ExecuteResultConstants.Exception;
            }

            ///转换成中间表
            List<BdAsnInfo> bdAsnInfos = new List<BdAsnInfo>();
            foreach (var qmiInfo in qmiInfos)
            {
                bdAsnInfos.Add(GetBdAsnInfo(qmiInfo));
            }
            msgContent = new XmlWrapper().ObjectToXml(bdAsnInfos, false);
            ///接口调用
            ///
            try
            {
                using (ILesToQmisService lesToQmisService = new BFDAQMISAsnPull.ILesToQmisService())
                {
                    lesToQmisService.Url = interfaceConfigInfo.CallUrl;
                    lesToQmisService.AuthenticationToken = new AuthenticationToken(interfaceConfigInfo.UserName, interfaceConfigInfo.PassWord);
                    var result = lesToQmisService.getAsninfo(bdAsnInfos.ToArray());
                    if (result.Count() > 0)
                    {
                        if (result.FirstOrDefault().status == "1")
                        {
                            return ExecuteResultConstants.Success;
                        }
                        else
                        {
                            errorMsg = result.FirstOrDefault().message;
                        }
                    } 
                }
            }
            catch (Exception ex) {
                errorMsg = ex.Message;
            }
            return ExecuteResultConstants.Error;  
        }

        public static BdAsnInfo GetBdAsnInfo(QmisAsnPullSheetInfo qmisAsnPullSheetInfo)
        {
            BdAsnInfo bdAsnInfo = new BdAsnInfo();
            bdAsnInfo.asnNo = qmisAsnPullSheetInfo.AsnNo;
            bdAsnInfo.orderNo = qmisAsnPullSheetInfo.OrderNo;
            bdAsnInfo.partNo = qmisAsnPullSheetInfo.PartNo;
            bdAsnInfo.supplierNo = qmisAsnPullSheetInfo.SupplierNo;
            bdAsnInfo.totalNo = qmisAsnPullSheetInfo.TotalNo.GetValueOrDefault().ToString();
            bdAsnInfo.checkMode = qmisAsnPullSheetInfo.CheckMode;
            bdAsnInfo.factory = qmisAsnPullSheetInfo.Plant;
            bdAsnInfo.arrivalDate = qmisAsnPullSheetInfo.ArrivalDate.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss");
            bdAsnInfo.targetPlace = qmisAsnPullSheetInfo.TargetPlace;
            bdAsnInfo.purchaseGroup = qmisAsnPullSheetInfo.PurchaseGroup;
            bdAsnInfo.id = qmisAsnPullSheetInfo.LogFid.GetValueOrDefault().ToString();

            return bdAsnInfo;
        }

            

    }

}

