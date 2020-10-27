using BLL.LES;
using BLL.SYS;
using DM.LES;
using DM.SYS;
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
    /// LES-WMS-011 供应商基础数据中间表 (暂缓)
    /// </summary>
    public class BFDAVmiBasSupplierBLL
    {
        /// <summary>
        /// 日期格式
        /// </summary>
        private static string vmiDateFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// LES-WMS-009 供货计划 LW006
        /// </summary>
        /// <param name="logFid"></param>
        public static ExecuteResultConstants SendVmiSupplyPlan(Guid logFid, InterfaceConfigInfo interfaceConfigInfo,
            ref string errorCode, ref string errorMsg, out string msgContent)
        {
            msgContent = string.Empty;

            

             
            return ExecuteResultConstants.Success;
        }
 
        /// <summary>
        /// 对象转换
        /// </summary>
        /// <param name="planinfo"></param>
        /// <returns></returns>
        private static BFDAVmiSupplyPlanInfo GetBFDAVmiSupplyPlanInfo(WmsVmiSupplyPlanInfo planinfo)
        {
            ///TDD 未完成
            BFDAVmiSupplyPlanInfo bFDAVmiSupplyPlanInfo = new BFDAVmiSupplyPlanInfo();
            return bFDAVmiSupplyPlanInfo;
        }

         
    }
}
