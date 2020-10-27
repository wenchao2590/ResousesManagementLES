namespace WS.SAP.SyncInboundService
{
    using BLL.SYS;
    using DM.SYS;
    using System.Collections.Generic;
    using System.Linq;
    public class Handle
    {
        /// <summary>
        /// 
        /// </summary>
        private string targetSystem = "SAP";
        /// <summary>
        /// 
        /// </summary>
        private string loginUser = "WS.SAP.SyncInboundService";
        /// <summary>
        /// 
        /// </summary>
        public void Handler()
        {
            List<InterfaceConfigInfo> interfaceConfigInfos = new InterfaceConfigBLL().GetList("[SYS_NAME] = N'" + targetSystem + "'", string.Empty);
            if (interfaceConfigInfos.Count == 0) return;
            ///接口配置
            InterfaceConfigInfo interfaceConfigInfo = null;
            ///SAP-LES-018，工厂布局基础数据
            interfaceConfigInfo = interfaceConfigInfos.FirstOrDefault(d => d.InterfaceCode.ToUpper() == "SAP-LES-018");
            if (interfaceConfigInfo != null) SyncPlantStructureBLL.Sync(loginUser);

            ///SAP-LES-001，SAP物料基础数据同步
            interfaceConfigInfo = interfaceConfigInfos.FirstOrDefault(d => d.InterfaceCode.ToUpper() == "SAP-LES-001");
            if (interfaceConfigInfo != null) SyncMaintainPartsBLL.Sync(loginUser);

            ///SAP-LES-003，SAP供应商配额基础数据同步
            interfaceConfigInfo = interfaceConfigInfos.FirstOrDefault(d => d.InterfaceCode.ToUpper() == "SAP-LES-003");
            if (interfaceConfigInfo != null) SyncSupplierPartQuotaBLL.Sync(loginUser);

            ///SAP-LES-005，工作日历基础数据
            interfaceConfigInfo = interfaceConfigInfos.FirstOrDefault(d => d.InterfaceCode.ToUpper() == "SAP-LES-005");
            if (interfaceConfigInfo != null) SyncWorkScheduleBLL.Sync(loginUser);

            ///SAP-LES-016，物料预留单接收
            interfaceConfigInfo = interfaceConfigInfos.FirstOrDefault(d => d.InterfaceCode.ToUpper() == "SAP-LES-016");
            if (interfaceConfigInfo != null) SyncMaterialReservationBLL.Sync(loginUser);
        }
    }
}
