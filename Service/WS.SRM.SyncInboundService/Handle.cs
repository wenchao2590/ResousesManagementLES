using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.SYS;
using DM.LES;
using BLL.LES;
using Infrustructure.Utilities;
using DM.SYS;

namespace WS.SRM.SyncInboundService
{
    public class Handle
    {
        #region Common
        /// <summary>
        /// 
        /// </summary>
        private string targetSystem = "SRM";
        /// <summary>
        /// 执行用户
        /// </summary>
        private string loginUser = "WS.SRM.SyncInboundService";
        /// <summary>
        /// 获取MESSAGE的内容
        /// </summary>
        /// <param name="messageCode"></param>
        /// <returns></returns>
        private string GetMessage(ref string messageCode)
        {
            if (string.IsNullOrEmpty(messageCode)) return string.Empty;
            messageCode = GetMessageCode(messageCode);
            string nowLangue = AppSettings.GetConfigString("messageLanguage");
            if (!string.IsNullOrEmpty(nowLangue)) nowLangue = "zh-cn";
            return new MessageBLL().GetMessage(nowLangue, messageCode);
        }
        /// <summary>
        /// 获取错误代码
        /// </summary>
        /// <param name="messageCode"></param>
        /// <returns></returns>
        private string GetMessageCode(string messageCode)
        {
            if (string.IsNullOrEmpty(messageCode))
                return "Err_:ERROR";
            if (messageCode.StartsWith("Err_:"))
                messageCode = messageCode.Replace("Err_:", string.Empty);
            if (messageCode.StartsWith("MC:"))
                messageCode = messageCode.Replace("MC:", string.Empty);
            return messageCode;
        }
        #endregion
        /// <summary>
        /// 主函数
        /// </summary>
        public void Handler()
        {
            List<InterfaceConfigInfo> interfaceConfigInfos = new InterfaceConfigBLL().GetList("[SYS_NAME] = N'" + targetSystem + "'", string.Empty);
            if (interfaceConfigInfos.Count == 0) return;
            ///接口配置
            InterfaceConfigInfo interfaceConfigInfo = null;
            ///SRM-LES-003，物料发货单
            interfaceConfigInfo = interfaceConfigInfos.FirstOrDefault(d => d.InterfaceCode.ToUpper() == "SRM-LES-003");
            if (interfaceConfigInfo != null) SyncVmiShippingNoteBLL.Sync(loginUser);
            ///SRM-LES-006，物料送货单
            interfaceConfigInfo = interfaceConfigInfos.FirstOrDefault(d => d.InterfaceCode.ToUpper() == "SRM-LES-006");
            if (interfaceConfigInfo != null) SyncDeliveryNoteBLL.Sync(loginUser);
            ///SRM-LES-007，SRM箱标签
            interfaceConfigInfo = interfaceConfigInfos.FirstOrDefault(d => d.InterfaceCode.ToUpper() == "SRM-LES-007");
            if (interfaceConfigInfo != null) SyncBarcodesBLL.Sync(loginUser);

        }

    }
}
