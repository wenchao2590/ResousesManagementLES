#region File Comment
//+-------------------------------------------------------------------+
//+ Name: 	   ��Ϣ�붨��
//+ Function:  ���Ը�����Ϣ���ȡ��Ϣ���ݡ���Ϣ��Ҫ���ڽ�����ʾ�㣬���û����Ѻ���ʾ��
//             ÿ����Ϣ�붨�����Ҫ����ע�͡�
//+ Author:    Ѧ����
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
        /// ɾ���ɹ�
        /// </summary>
        public const string DeleteSuccess = "M_00001";

        public const string FailureOnRow = "M_00002";

        public const string InsertSuccess = "M_00003";
        public const string UpdateSuccess = "M_00004";

        public const string FailureOnSendEPSMessage = "M_00005";
        public const string AddEPSMessageSuccess = "M_00006";

        //EPS��Ϣ����ʱ��û��������Ϣ����
        public const string NeedEPSMessageContent = "M_00007";

        //EPS��Ϣ����ʱ��û��ѡ��Ŀ���û�
        public const string NeedSelectTargetUser = "M_00008";

        //EPS����ȡ���ɹ�
        public const string TaskCancelSuccess = "M_00009";

       

        #region ���ܽ�����Ϣ
        /// <summary>
        /// DD������֤���ܽ���
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
