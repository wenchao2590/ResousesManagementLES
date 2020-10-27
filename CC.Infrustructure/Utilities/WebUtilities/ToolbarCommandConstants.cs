#region File Comment
//+-------------------------------------------------------------------+
//+ Name: 	   ���湤����
//+ Function:   
//+ Author: 
//+ Date:           20060702       
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
	public class ToolbarCommand
    {
        #region ��ͨListҳ��
        /// <summary>
        /// �½�����ӡ���������
        /// </summary>
        public const string Create = "Create";
        /// <summary>
        /// ɾ��
        /// </summary>
        public const string Delete = "Delete";
        /// <summary>
        /// ȡ��
        /// </summary>
        public const string Cancel = "Cancel";
        /// <summary>
        /// ����
        /// </summary>
        public const string Export = "Export";
        /// <summary>
        /// ���ػ�չ����ѯ��
        /// </summary>
        public const string HideOrExpand = "HideOrExpand";
        /// <summary>
        /// ��ѯ
        /// </summary>
        public const string Search = "Search";
        /// <summary>
        /// ����
        /// </summary>
        public const string Reset = "Reset";
        /// <summary>
        /// ����
        /// </summary>
        public const string Back = "Back";
        /// <summary>
        /// ��ӡ
        /// </summary>
        public const string Print = "Print";
        /// <summary>
        /// ����λ��ӡ
        /// </summary>
        public const string PrintAsULoc = "PrintAsULoc";
        /// <summary>
        /// ����
        /// </summary>
        public const string Import = "Import";

         /// <summary>
        /// ����
        /// </summary>
        public const string Restore = "Restore";
        /// <summary>
        /// ����sendtime
        /// </summary>
        public const string Calculate = "Calculate";

        /// <summary>
        /// ȫ������
        /// </summary>
        public const string AllCalculate = "AllCalculate";

        /// <summary>
        /// SPS����
        /// </summary>
        public const string SPSReCalculate = "SPSReCalculate";

        /// <summary>
        /// �Ƚ�
        /// </summary>
        public const string Compare = "Compare";

        /// <summary>
        /// ͬ��
        /// </summary>
        public const string Sync = "Sync";

        /// <summary>
        /// ����ģ��
        /// </summary>
        public const string DownloadTemplateFile = "DownloadTemplateFile";

        /// <summary>
        /// �ط�
        /// </summary>
        public const string ReSend = "ReSend";

        /// <summary>
        /// ���PCS_DCPɨ���͹�λ����
        /// </summary>
        public const string AddRegionLocations = "AddRegionLocations";

        /// <summary>
        /// ͳ�ƹ�M1�㳵�����
        /// </summary>
        public const string CalcStatistics = "CalcStatistics";

        #endregion

        #region ��ͨEditҳ��
        /// <summary>
        /// ����
        /// </summary>
        public const string Save = "Save";
        /// <summary>
        /// ȷ��
        /// </summary>
        public const string Submit = "Submit";
        #endregion

        #region ����������ʶ

        /// <summary>
        /// ɨ��
        /// </summary>
        public const string Scan = "Scan";
        /// <summary>
        /// ɨ�����
        /// </summary>
        public const string EndScan = "EndScan";

        /// <summary>
        /// ���´�ӡ
        /// </summary>
        public const string RePrint = "RePrint";

        /// <summary>
        /// �ط�
        /// </summary>
        public const string Republish = "Republish";
        /// <summary>
        /// �ֹ��鵥
        /// </summary>
        public const string Trant="Trant";

        /// <summary>
        /// N������ CopyE 
        /// </summary>
        public const string CopyE = "CopyE";

        /// <summary>
        ///  N��E��ת�� BillTrans 
        /// </summary>
        public const string BillTrans = "BillTrans";


        public const string ViewSetTimeList = "ViewSetTimeList";
        public const string ReturnManualPullPartsList = "ReturnManualPullPartsList";

        public const string UpdateTime = "UpdateTime";
        public const string ReturnViewSum = "ReturnViewSum";
        public const string Stop = "Stop";
        public const string Active = "Active";
        public const string Refresh = "Refresh";

        public const string ThirdConfirmation       = "ThirdConfirmation";
        public const string ThirdConfirmationExport = "ThirdConfirmationExport";
        public const string TaskCancel              = "TaskCancel";
        public const string Return                  = "Return";
        public const string EmerbencyPrint = "EmerPrint";
        

        public const string SearchToday = "SearchToday";

        public const string AutoRefresh = "AutoRefresh";
        public const string StopRefresh = "StopRefresh";

        public const string SearchLog = "SearchLog";

        /// <summary>
        /// ����
        /// </summary>
        public const string Shipment = "Shipment";

        public const string JIS = "JIS";
        public const string TWD = "TWD";
        public const string WindowTime = "WindowTime";

        #endregion

        #region TWD������֤
        /// <summary>
        /// ��֤
        /// </summary>
        public const string IncomeVerify = "IncomeVerify";
        /// <summary>
        /// ��������
        /// </summary>
        public const string EmergencyPull = "EmergencyPull";
        #endregion

        #region JIS����
        public const string JISRepeatProcess = "RepeatProcess";

        public const string JISGenerateSettleSheet = "JISGenerateSettleSheet";
        #endregion 

        #region EPS����
        public const string CreateButtonCardLink = "CreateButtonCardLink";

        public const string Close = "Close";
        #endregion

        #region Routine����
        public const string Load = "Load";

        #endregion

        #region  �������
        public const string RDCSave = "RDCSave";

        public const string GenerateCombineInfo = "GenerateCombineInfo";
        public const string ApproveCombineInfo = "ApproveCombineInfo";
        public const string ApproveAllCombineInfo = "ApproveAllCombineInfo";
        public const string RejectCombineInfo = "RejectCombineInfo";
        public const string RejectAllCombineInfo = "RejectAllCombineInfo";
        public const string ApproveRejectAllCombineInfo = "ApproveRejectAllCombineInfo";

        public const string GenerateInhouseDiffInfo = "GenerateInhouseDiffInfo";
        public const string ApproveInhouseDiffInfo = "ApproveInhouseDiffInfo";
        public const string ApproveAllInhouseDiffInfo = "ApproveAllInhouseDiffInfo";
        public const string RejectInhouseDiffInfo = "RejectInhouseDiffInfo";
        public const string RejectAllInhouseDiffInfo = "RejectAllInhouseDiffInfo";
        public const string ApproveRejectAllInhouseDiffInfo = "ApproveRejectAllInhouseDiffInfo";

        public const string GenerateInboundDiffInfo = "GenerateInboundDiffInfo";
        public const string ApproveInboundDiffInfo = "ApproveInboundDiffInfo";
        public const string ApproveAllInboundDiffInfo = "ApproveAllInboundDiffInfo";
        public const string RejectInboundDiffInfo = "RejectInboundDiffInfo";
        public const string RejectAllInboundDiffInfo = "RejectAllInboundDiffInfo";
        public const string ApproveRejectAllInboundDiffInfo = "ApproveRejectAllInboundDiffInfo";
        public const string InhouseCombine = "InhouseCombine";
        public const string InhouseCombineByAssemblyLine = "InhouseCombineByAssemblyLine";
        public const string InboundCombine = "InboundCombine";
        public const string InboundCombineByAssemblyLine = "InboundCombineByAssemblyLine";

        #endregion

        #region RPTPMS
        public const string RTPMS_RESET = "RTPMS_RESET";
        public const string RTPMS_EXPORT = "RTPMS_EXPORT";
        public const string RTPMS_DIRECT_SEND = "RTPMS_DIRECT_SEND";
        public const string RTPMS_LOCAL_SEND = "RTPMS_LOCAL_SEND";
        #endregion

        #region PLAN

        public const string PlanerConfirm = "PlanerConfirm";
        public const string SupplyConfirm = "SupplyConfirm";

        public const string GenerateBarCode = "GenerateBarCode";

        #endregion

        #region TWD����

        public const string TWDGenerateSettleSheet = "TWDGenerateSettleSheet";

        #endregion 

        #region �����ѯ
        public const string Search2 = "Search2";
        #endregion

        #region ���ɷ��˵�
        public const string CreateShipping="CreateShipping";
        public const string View = "View";
        #endregion
    }
}
