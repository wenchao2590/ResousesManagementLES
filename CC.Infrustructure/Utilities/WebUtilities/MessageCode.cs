#region File Comment
//+-------------------------------------------------------------------+
//+ Name: 	   消息码定义
//+ Function:  可以根据消息码获取消息内容。消息主要用在界面显示层，给用户以友好提示。
//             每个消息码定义必须要加入注释。
//+ Author:    薛海军
//+ Date:      20060702       
//+-------------------------------------------------------------------+
//+ Change History:
//+ Date            Who       		Chages Made        Comments
//+-------------------------------------------------------------------+
//+ 20060702         CodeGenerator        Init Created
//+-------------------------------------------------------------------+
//+-------------------------------------------------------------------+
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace Infrustructure.Utilities.WebUtility
{
	public class MessageCode
    {
        /// <summary>
        /// 删除成功
        /// </summary>
        public const string DeleteSuccess = "M_00001";

        public const string FailureOnRow = "M_00002";

        public const string InsertSuccess = "M_00003";
        public const string UpdateSuccess = "M_00004";

        public const string FailureOnSendEPSMessage = "M_00005";
        public const string AddEPSMessageSuccess = "M_00006";

        //EPS消息输入时，没有输入消息内容
        public const string NeedEPSMessageContent = "M_00007";

        //EPS消息输入时，没有选择目的用户
        public const string NeedSelectTargetUser = "M_00008";

        //EPS任务取消成功
        public const string TaskCancelSuccess = "M_00009";

       

        #region 功能介绍消息
        /// <summary>
        /// DD进货验证功能介绍
        /// </summary>
        public const string FunctionIntro_DDIncomeVerify = "M_00010";

        #endregion

        public const string FailureOnSelectQueryUser = "M_00011";

        public const string FailureOnSelectDateTime  = "M_00012";

        public const string ButtonNotExists = "M_00013";

        public const string CardNoIsNULL = "M_00014";

        public const string CardIsNotExists = "M_00015";

        public const string ButtonCardLinkageExists = "M_00016";

        public const string ButtonCardSamePlant = "M_00099";

        public const string ProcessBarScript = "M_00017";

        public const string DDRTPMSReset = "M_00018";

        public const string DDRTPMSExport = "M_00019";

    }
}
