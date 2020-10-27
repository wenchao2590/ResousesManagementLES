#region File Comment
//+-------------------------------------------------------------------+
//+ Name: 	   界面工具栏
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
        #region 普通List页面
        /// <summary>
        /// 新建、添加、创建命令
        /// </summary>
        public const string Create = "Create";
        /// <summary>
        /// 删除
        /// </summary>
        public const string Delete = "Delete";
        /// <summary>
        /// 取消
        /// </summary>
        public const string Cancel = "Cancel";
        /// <summary>
        /// 导出
        /// </summary>
        public const string Export = "Export";
        /// <summary>
        /// 隐藏或展开查询项
        /// </summary>
        public const string HideOrExpand = "HideOrExpand";
        /// <summary>
        /// 查询
        /// </summary>
        public const string Search = "Search";
        /// <summary>
        /// 重置
        /// </summary>
        public const string Reset = "Reset";
        /// <summary>
        /// 返回
        /// </summary>
        public const string Back = "Back";
        /// <summary>
        /// 打印
        /// </summary>
        public const string Print = "Print";
        /// <summary>
        /// 按工位打印
        /// </summary>
        public const string PrintAsULoc = "PrintAsULoc";
        /// <summary>
        /// 导入
        /// </summary>
        public const string Import = "Import";

         /// <summary>
        /// 导入
        /// </summary>
        public const string Restore = "Restore";
        /// <summary>
        /// 计算sendtime
        /// </summary>
        public const string Calculate = "Calculate";

        /// <summary>
        /// 全部重算
        /// </summary>
        public const string AllCalculate = "AllCalculate";

        /// <summary>
        /// SPS重算
        /// </summary>
        public const string SPSReCalculate = "SPSReCalculate";

        /// <summary>
        /// 比较
        /// </summary>
        public const string Compare = "Compare";

        /// <summary>
        /// 同步
        /// </summary>
        public const string Sync = "Sync";

        /// <summary>
        /// 下载模板
        /// </summary>
        public const string DownloadTemplateFile = "DownloadTemplateFile";

        /// <summary>
        /// 重发
        /// </summary>
        public const string ReSend = "ReSend";

        /// <summary>
        /// 添加PCS_DCP扫描点和工位关联
        /// </summary>
        public const string AddRegionLocations = "AddRegionLocations";

        /// <summary>
        /// 统计过M1点车辆零件
        /// </summary>
        public const string CalcStatistics = "CalcStatistics";

        #endregion

        #region 普通Edit页面
        /// <summary>
        /// 保存
        /// </summary>
        public const string Save = "Save";
        /// <summary>
        /// 确认
        /// </summary>
        public const string Submit = "Submit";
        #endregion

        #region 其他动作标识

        /// <summary>
        /// 扫描
        /// </summary>
        public const string Scan = "Scan";
        /// <summary>
        /// 扫描结束
        /// </summary>
        public const string EndScan = "EndScan";

        /// <summary>
        /// 重新打印
        /// </summary>
        public const string RePrint = "RePrint";

        /// <summary>
        /// 重发
        /// </summary>
        public const string Republish = "Republish";
        /// <summary>
        /// 手工查单
        /// </summary>
        public const string Trant="Trant";

        /// <summary>
        /// N单复制 CopyE 
        /// </summary>
        public const string CopyE = "CopyE";

        /// <summary>
        ///  N单E单转换 BillTrans 
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
        /// 起运
        /// </summary>
        public const string Shipment = "Shipment";

        public const string JIS = "JIS";
        public const string TWD = "TWD";
        public const string WindowTime = "WindowTime";

        #endregion

        #region TWD进货验证
        /// <summary>
        /// 验证
        /// </summary>
        public const string IncomeVerify = "IncomeVerify";
        /// <summary>
        /// 紧急拉动
        /// </summary>
        public const string EmergencyPull = "EmergencyPull";
        #endregion

        #region JIS管理
        public const string JISRepeatProcess = "RepeatProcess";

        public const string JISGenerateSettleSheet = "JISGenerateSettleSheet";
        #endregion 

        #region EPS管理
        public const string CreateButtonCardLink = "CreateButtonCardLink";

        public const string Close = "Close";
        #endregion

        #region Routine表导入
        public const string Load = "Load";

        #endregion

        #region  零件管理
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

        #region TWD管理

        public const string TWDGenerateSettleSheet = "TWDGenerateSettleSheet";

        #endregion 

        #region 反向查询
        public const string Search2 = "Search2";
        #endregion

        #region 生成发运单
        public const string CreateShipping="CreateShipping";
        public const string View = "View";
        #endregion
    }
}
