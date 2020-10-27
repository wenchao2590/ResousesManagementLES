using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.SYS;
using DM.LES;
using BLL.LES;
using Infrustructure.Utilities;
using DM.SYS;

namespace WS.QMIS.SyncInboundService
{
    public class Handle
    {
        #region Common


        /// <summary>
        /// 操作用户
        /// </summary>
        private string loginUser = "WS.QMIS.SyncInboundService";

        /// <summary>
        /// 获取MESSAGE的内容
        /// </summary>
        /// <param name="messageCode"></param>
        /// <returns></returns>
        private string GetMessage(ref string messageCode)
        {
            messageCode = GetMessageCode(messageCode);

            string nowLangue = AppSettings.GetConfigString("messageLanguage");
            //string nowLangue = AppSettings.GetConfigString("messageLanguage");
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

        public void Handler()
        {


            ///BAS-029  --QMIS检验模式同步

            SyncCheckModeBLL.SyncCheckMode(loginUser);

            SyncCheckResultBLL.SyncCheckResult(loginUser);


        }



    }
}
