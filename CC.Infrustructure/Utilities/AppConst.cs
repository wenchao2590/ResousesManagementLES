using System;
using System.Collections.Generic;
using System.Text;

////
//XML文件的文件名
////
namespace Infrustructure.Utilities
{
    public  class AppConst
    {
        public  const string Configuration = "Configuration";

        //LES调用传真状态
        public  const string LESCallFax = "LESCallFax";

        //LES调用传真报警时间
        public  const string LESCallFaxWarningLastTime = "LESCallFaxWarningLastTime";

        //jit物料单连续性状态
        public  const string JITMaterialContinuity = "JITMaterialContinuity";

        //jit物料单连续性报警时间
        public  const string JITMaterialContinuityWarningLastTime = "JITMaterialContinuityWarningLastTime";

        //RDC终端状态
        public  const string RDCTerminal = "RDCTerminal";

        //RDC终端报警时间
        public  const string RDCTerminalWarningLastTime = "RDCTerminalWarningLastTime";

        //工作时间合法状态
        public  const string WorkTimeLegal = "WorkTimeLegal";

        //工作时间合法性报警时间
        public  const string WorkTimeLegalWarningLastTime = "WorkTimeLegalWarningLastTime";

        //EPS终端状态
        public  const string EPSTerminalStatus = "EPSTerminalStatus";

        //EPS终端报警时间
        public  const string EPSTerminalStatusWarningLastTime = "EPSTerminalStatusWarningLastTime";

        //报表上载状态
        public  const string ReportUpload = "ReportUpload";

        //报表上载报警时间
        public  const string ReportUploadWarningLastTime = "ReportUploadWarningLastTime";

        //callbutton状态
        public  const string CallButtonSingal = "CallButtonSingal";

        //callbutton报警时间
        public  const string CallButtonSingalWarningLastTime = "CallButtonSingalWarningLastTime";
         
        //MAS分拣状态
        public  const string MASSortingSystem = "MASSortingSystem";

        //MAS分拣报警时间
        public  const string MASSortingSystemWarningLastTime = "MASSortingSystemWarningLastTime";

        //打印系统状态
        public  const string PrintingSystem = "PrintingSystem";

        //打印系统报警时间
        public  const string PrintingSystemWarningLastTime = "PrintingSystemWarningLastTime";

        //MOM声光状态
        public  const string MOMSoundAndLightWarning = "MOMSoundAndLightWarning";

        //MOM声光报警时间
        public  const string MOMSoundAndLightWarningWarningLastTimeFileNmae = "MOMSoundAndLightWarningWarningLastTime";

        //RDC供应商状态
        public  const string RDCProviderResponse = "RDCProviderResponse";

        //RDC供应商报警时间
        public  const string RDCProviderResponseWarningLastTime = "RDCProviderResponseWarningLastTime";

        //
        public const string EPSMonitorWarningLastTime = "EPSMonitorWarningLastTime";

        //JITDD报警时间
        public const string JITDDMonitorWarningLastTime = "JITDDMonitorWarningLastTime";

        //WorkTime报警时间
        public const string WTMonitorWarningLastTime = "WTMonitorWarningLastTime";        

        //EPS按钮低电压报警时间
        public const string BLVMonitorWarningLastTime = "BLVMonitorWarningLastTime";

        //后台服务报警时间
        public const string BSMonitorWarningLastTime = "BSMonitorWarningLastTime";

        //按钮心跳报警时间
        public const string BAMonitorWarningLastTime = "BAMonitorWarningLastTime";

        //成对料架组织报警时间
        public const string PRMonitorWarningLastTime = "PRMonitorWarningLastTime";

        //GEPICS数据格式报警时间
        public const string FFMonitorWarningLastTime = "FFMonitorWarningLastTime";

        //传真报警时间
        public const string FLMonitorWarningLastTime = "FLMonitorWarningLastTime";

        //DD最后时间报警时间
        public const string DDMonitorWarningLastTime = "DDMonitorWarningLastTime";

        //JIT最后时间报警时间
        public const string JITMonitorWarningLastTime = "JITMonitorWarningLastTime";

        //GEPICS外部系统报警时间
        public const string FSMonitorWarningLastTime = "FSMonitorWarningLastTime";

        //AP监控报警时间
        public const string APMonitorWarningLastTime = "APMonitorWarningLastTime";

        //SAPBOM数据导入监控报警时间
        public const string SAPBOMMonitorWarningLastTime = "SAPBOMMonitorWarningLastTime";

        //监控项时间间隔
        public  const string TimerInterval = "TimerInterval";

        //工作时间合法性时间差
        public const string TimeSpanWorkTime = "TimeSpanWorkTime";

        //EPSMonitor
        public const string EPSMonitor = "EPSMonitor";

        //WorkTimeMonitor
        public const string WTMonitor = "WTMonitor";       

        //EPSButtonLowVoltageMonitor
        public const string BLVMonitor = "BLVMonitor";

        //BehindServiceStatusMonitor
        public const string BSMonitor = "BSMonitor";

        //EPSButtonAliveMonitor
        public const string BAMonitor = "BAMonitor";

        //PairRackErrorMonitor
        public const string PRMonitor = "PRMonitor";

        //FlexFormatErrorMonitor
        public const string FFMonitor = "FFMonitor";

        //FaxLastSendTimeMonitor
        public const string FLMonitor = "FLMonitor";

        //DD Last TimeMonitor
        public const string DDLMonitor = "DDLMonitor";

        //JIT Last TimeMonitor
        public const string JITMonitor = "JITMonitor";

        //FlexMonitor
        public const string FSMonitor = "FSMonitor";

        //CaLLButton
        public const string CaLLButton = "CaLLButton";

        //SupplierMonitor
        public const string SupplierMonitor = "SupplierMonitor";

        //DDMonitor
        public const string DDMonitor = "DDMonitor";

        //FaxMonitor
        public const string FaxMonitor = "FaxMonitor";

        //SPS 跳号
        public const string SPSSKIPMonitor = "SPSSkipNoMonitor";

        //SPS 车辆队列监控
        public const string SPSVechileMonitor = "SPSVechileMonitor";

        //PPS 车辆队列监控
        public const string PPSVechileMonitor = "PPSVechileMonitor";

        //AP 监控
        public const string APMonitor = "APMonitor";

        //TMSMonitor
        public const string TMSMonitor = "TMSMonitor";

        //SAP BOM 数据导入监控
        public const string SAPBOMMonitor = "SAPBOMMonitor";

        //MAS分拣失败EVENTID
        public const int MasFailEventID = 706;

        //打印系统EVENTID
        public const int PrintEventID = 10001;

        //RDC打印失败eventid
        public const int RDCPrintFailEventID = 110;

        //RDC物料单发送失败eventid
        public const int RDCSendFailEventID = 111;

        //工作时间不合法EVENTID;
        public const int WorkTimeNonlegal = 101;

        //EPS终端报警EVENTID
        public const int EPSEventID = 305;

        //打印系统时间差
        public const string TimeSpanPrint = "TimeSpanPrint";    
 
        //工厂，车间的配置文件
        public const string PlantAndWorkshop = "PmcConfig";

        //工厂，车间的配置文件
        public const string MasConfig = "MasConfig";

        //EPS终端监控检测信息记录文件名
        public const string EPSCheckTime = "EPSCheckTime"; 
   
        //时间配置文件
        public const string TimeConfig = "TimeConfig";

        //config 目录
        public const string directoryConfig = "config";

        //appConfig目录
        public const string directoryAppConfig = "appConfig";

        //APConfig文件
        public const string APConfig = "APConfig";
            
    }
}
