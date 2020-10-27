using BLL.LES;
using DM.LES;
using DM.SYS;
using BLL.SYS;
using Infrustructure.Logging;
using Infrustructure.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.IO;
using System.Data;
using Infrustructure.Data;
using WS.SAP.InboundDataService;

namespace WS.SMS.InboundDataService
{
    /// <summary>
    ///	与冲压工厂接口
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class InboundDataService : WebService
    {
        #region Common
        /// <summary>
        /// 接口用户
        /// </summary>
        private string loginUser = "WS.SMS.InboundDataService";
        /// <summary>
        /// 获取MESSAGE的内容
        /// </summary>
        /// <param name="messageCode"></param>
        /// <returns></returns>
        private string GetMessage(ref string messageCode)
        {
            messageCode = GetMessageCode(messageCode);
            string nowLangue = ConfigurationManager.AppSettings["messageLanguage"];
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
        /// LES-SMS-001 供货计划  
        /// </summary>
        /// <param name="partInfos"></param>
        /// <returns></returns>
        [WebMethod(Description = "供货计划")]
        public InterfaceReturnInfo GetSapMaintainPart(List<BFDASapPartInfo> partInfos)
        {
            ///回传文本
            InterfaceReturnInfo retInfo = new InterfaceReturnInfo();
            
            return retInfo;
        }


        /// <summary>
        /// LES-SMS-002 物料送货单  
        /// </summary>
        /// <param name="partInfos"></param>
        /// <returns></returns>
        [WebMethod(Description = "物料送货单")]
        public InterfaceReturnInfo GetMaterialDeliveryList(List<BFDASapPartInfo> partInfos)
        {
            ///回传文本
            InterfaceReturnInfo retInfo = new InterfaceReturnInfo();
            ///消息体
            
            return retInfo;
        }

    }
}
