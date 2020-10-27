
namespace WS.VMI.SyncInboundService
{
    using BLL.SYS;
    using DM.SYS;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class Handle
    {
        /// <summary>
        /// 
        /// </summary>
        private string targetSystem = "VMI";
        /// <summary>
        /// 执行用户
        /// </summary>
        private string loginUser = "WS.VMI.SyncInboundService";
        /// <summary>
        /// Handler
        /// </summary>
        public void Handler()
        {
            List<InterfaceConfigInfo> interfaceConfigInfos = new InterfaceConfigBLL().GetList("[SYS_NAME] = N'" + targetSystem + "'", string.Empty);
            if (interfaceConfigInfos.Count == 0) return;
            ///接口配置
            InterfaceConfigInfo interfaceConfigInfo = null;
            ///WMS-LES-002，WMS库存交易记录
            interfaceConfigInfo = interfaceConfigInfos.FirstOrDefault(d => d.InterfaceCode.ToUpper() == "WMS-LES-002");
            if (interfaceConfigInfo != null) SyncWmsVmiTranDetailBLL.Sync(loginUser);
            ///WMS-LES-006，WMS送货单
            interfaceConfigInfo = interfaceConfigInfos.FirstOrDefault(d => d.InterfaceCode.ToUpper() == "WMS-LES-006");
            if (interfaceConfigInfo != null) SyncWmsVmiAsnRunsheetBLL.Sync(loginUser);
        }
    }
}
