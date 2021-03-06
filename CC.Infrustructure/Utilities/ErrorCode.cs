#region File Comment
//+-------------------------------------------------------------------+
//+ Name: 	   业务错误码定义
//+ Function:  业务处理过程中出现的任何错误（业务级别，不是系统级别） ，都需要定义错误码
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

namespace Infrustructure.Utilities
{
    /// <summary>
    /// 系统错误码定义
    /// </summary>
    public class ErrorCode
    {
        #region 错误码概述
        //所有的错误代码的提示消息（包括异常提示和正常提示），均定义在资源文件（CommonComponents\CC.Utility\Exception\ExceptionRecs.resx）中，便于统一维护，定义规则如下：所有错误码以“E_”开头，提示码以“I_”开头，权限码以“R_”开头，后面是_错误分类 + _错误具体模块 + _错误名，如 E_PullEngine_JIT_DOCError 对应错误代码为03102
        //  系统级：        00001至 00200
        //  公共级：        00201 至 00500
        //  系统管理：      00501 至 01000
        //  子系统：        01001 至 12000
        //      外部接口管理子系统:             01001 至 02000
        //      内部业务管理应用子系统：        02001 至 03000
        //       区域管理：                     02001 至 02049
        //       DD管理：                       02050 至 02149
        //       EPS管理：                      02150 至 02199
        //       JIT管理：                      02200 至 02249
        //       零件管理：                     02250 至 02299
        //       PPS管理：                      02300 至 02399
        //       打印机管理：                   02400 至 02449
        //       报表管理：                     02500 至 02599
        //       供应商管理：                　 02600 至 02649
        //       系统管理：                     02650 至 02749
        //       监控管理：                     02750 至 02849
        //       接口查询                       02850 至 02899
        //       备用：                         02900 至 02999
        //      物流执行引擎子系统：            03001 至 04000
        //      EPS无线终端客户端应用子系统：   04001 至 05000
        //      EPS任务发布子系统：             05001 至 06000
        //      RDC供应商客户端应用子系统：     06001 至 07000
        //      外部供应商客户端应用子系统：    07001 至 08000
        //      JIT/DD发布子系统：              08001 至 09000
        //      运营监控管理子系统：            09001 至 10000
        //      运营统计分析子系统：            10001 至 11000
        //      基础服务子系统：                11001 至 12000
        #endregion

        //系统级：        00001至 00200
        #region SystemLevel
        /// <summary>
        /// 系统级错误
        /// </summary>
        public const string SystemLevelError = "E_00001";
        #endregion

        //公共级：        00201 至 00500
        #region CommonLevel

        /// <summary>
        /// 公共级错误
        /// </summary>
        public const string CommonLevelError = "E_00201";


        /// <summary>
        /// 参数不能为空
        /// </summary>
        public const string ParamCannotBeNull = "E_00202";



        /// <summary>
        /// 用户输入的应用参数名已经存在
        /// </summary>
        public const string BASAlreadyHaveApplicationName = "E_00203";

        /// <summary>
        /// 用户未登录
        /// </summary>
        public const string UserNotLogin = "E_00204";

        /// <summary>
        /// 编辑页面QueryString参数错误
        /// </summary>
        public const string PageParameterError = "02005";


        /// <summary>
        /// 用户输入的参数名已经存在
        /// </summary>
        public const string BASAlreadyHaveParameterName = "E_00206";

        /// <summary>
        /// 无法根据主键找到记录
        /// </summary>
        public const string CannotFindInfoByID = "E_00207";

        /// <summary>
        /// 没有可供导出的数据
        /// </summary>
        public const string NoDataToBeExport = "E_00208";

        /// <summary>
        /// 导出的数据量太大，请联系IT管理员从其他途径获取当前数据
        /// </summary>
        public const string TooLargeData = "E_00209";


        #endregion

        //系统管理：      00501 至 01000
        #region SysteManagement
        /// <summary>
        /// 系统管理错误
        /// </summary>
        public const string SystemError = "E_00501";
        #endregion


        #region SubSystem

        //外部接口管理子系统:             01001 至 02000
        #region Interface

        /// <summary>
        /// 外部接口管理子系统错误
        /// </summary>
        public const string InterfaceError = "E_01001";

        #endregion

        //内部业务管理应用子系统：        02001 至 03000
        #region BusinessManagementPortal

        ///区域管理：                    02001 至 02049
        #region AreaManagement

        /// <summary>
        /// 区域管理错误
        /// </summary>
        public const string AreaManagementError = "E_02001";

        /// <summary>
        /// 加载工厂列表失败
        /// </summary>
        public const string LoadPlantListError = "E_02002";

        /// <summary>
        /// 加载厂区列表失败
        /// </summary>
        public const string LoadPlantZoneListError = "E_02003";

        /// <summary>
        /// 加载流水线列表失败
        /// </summary>
        public const string LoadAssemblyLineListError = "E_02004";

        /// <summary>
        /// 加载车间列表失败
        /// </summary>
        public const string LoadWorkShopListError = "E_02005";


        /// <summary>
        /// 加载路径列表失败
        /// </summary>
        public const string LoadRouteListError = "E_02006";

        /// <summary>
        /// 加载工位列表失败
        /// </summary>
        public const string LoadLocationListError = "E_02007";

        /// <summary>
        /// 加载DCP扫描点列表失败
        /// </summary>
        public const string LoadDcpPointListError = "E_02008";

        /// <summary>
        /// 加载区域列表失败
        /// </summary>
        public const string LoadZoneListError = "E_02009";

        /// <summary>
        /// 加载状态点列表失败
        /// </summary>
        public const string LoadVehicleStatusListError = "E_02010";

        /// <summary>
        /// 加载计量单位列表失败
        /// </summary>
        public const string LoadMeasuringUnitListError = "E_02011";

        /// <summary>
        /// 加载Dock列表失败
        /// </summary>
        public const string LoadDockListError = "E_02012";

        /// <summary>
        /// 加载装配规则列表失败
        /// </summary>
        public const string LoadAssemblyRuleListError = "E_02013";

        /// <summary>   
        /// 工厂已经存在
        /// </summary>
        public const string PlantExists = "E_02014";

        /// <summary>   
        /// 厂区已经存在
        /// </summary>
        public const string PlantZoneExists = "E_02015";

        /// <summary>
        /// 车间已经存在
        /// </summary>
        public const string WorkshopExists = "E_02016";

        /// <summary>   
        /// 流水线已经存在
        /// </summary>
        public const string AssemblyLineExists = "E_02017";

        /// <summary>
        /// Dock已经存在
        /// </summary>
        public const string DockExists = "E_02018";

        /// <summary>
        /// 位置已经存在
        /// </summary>
        public const string LocationExists = "E_02019";

        /// <summary>
        /// 路径已经存在
        /// </summary>
        public const string RouteExists = "E_02020";

        /// <summary>
        /// 区域已经存在
        /// </summary>
        public const string ZoneExists = "E_02021";

        /// <summary>
        /// 导入的供应商数据格式不正确
        /// </summary>
        public const string InvalidSupplierFormat = "E_02022";

        /// <summary>
        /// 需要输入节拍
        /// </summary>
        public const string NeedFootPrint = "E_02023";

        /// <summary>
        /// 需要输入区域
        /// </summary>
        public const string NeedRegion = "E_02024";


        /// <summary>
        /// 该车间还存在用户和供应商与之关联，请先删除相关的供应商和用户关联
        /// </summary>
        public const string WorkshopRelationExists = "E_02025";


        /// <summary>
        /// 该工厂还存在车间与之关联，请先删除相关的车间
        /// </summary>
        public const string PlantRelationExists = "E_02026";


        /// <summary>
        /// 托盘设置已存在
        /// </summary>
        public const string TrayExists = "E_02027";

        /// <summary>
        /// DCP扫描点已经存在
        /// </summary>
        public const string DcpPointExists = "E_02028";

        /// <summary>
        /// 状态点已经存在
        /// </summary>
        public const string VehicleStatusExists = "E_02029";

        /// <summary>
        /// 计量单位已经存在
        /// </summary>
        public const string MeasuringUnitExists = "E_02030";

        /// <summary>
        /// 装配规则已经存在
        /// </summary>
        public const string AssemblyRuleExists = "E_02031";

        /// <summary>
        /// 加载邮件组列表失败
        /// </summary>
        public const string LoadMailGroupListError = "E_02032";

        /// <summary>
        /// 邮件组已经存在
        /// </summary>
        public const string MailGroupExists = "E_02033";

        /// <summary>
        /// 加载车型列表失败
        /// </summary>
        public const string LoadModelListError = "E_02034";

        /// <summary>
        /// 车型已经存在
        /// </summary>
        public const string ModelExists = "E_02035";

        /// <summary>
        /// 加载车型选装列表失败
        /// </summary>
        public const string LoadModelPartListError = "E_02036";

        /// <summary>
        /// 车型选装零件号已经存在
        /// </summary>
        public const string ModelPartExists = "E_02037";

        /// <summary>
        /// 加载PCS_DCP扫描点列表失败
        /// </summary>
        public const string LoadRegionListError = "E_02038";

        /// <summary>
        /// PCS_DCP扫描点已经存在
        /// </summary>
        public const string RegionExists = "E_02039";

        /// <summary>
        /// 加载PCS窗口时间列表失败
        /// </summary>
        public const string LoadDeliveryScheduleListError = "E_02040";

        /// <summary>
        /// PCS窗口时间已经存在
        /// </summary>
        public const string DeliveryScheduleExists = "E_02041";

        /// <summary>
        /// 加载PCS窗口时间列表失败
        /// </summary>
        public const string LoadRouteBoxPartsListError = "E_02042";

        /// <summary>
        /// PCS窗口时间已经存在
        /// </summary>
        public const string RouteBoxPartsExists = "E_02043";

        /// <summary>
        /// 加载工段列表失败
        /// </summary>
        public const string LoadWorkshopSectionListError = "E_02044";

        /// <summary>
        /// 工段已经存在
        /// </summary>
        public const string WorkshopSectionExists = "E_02045";


        /// <summary>
        /// 加载PCS_DCP扫描点-工位关联信息列表失败
        /// </summary>
        public const string LoadRegionLocationListError = "E_02046";

        /// <summary>
        /// 加载零件列表失败
        /// </summary>
        public const string LoadPartListError = "E_02047";

        /// <summary>
        /// 加载系统事件日志标识列表失败
        /// </summary>
        public const string LoadEventLogListError = "E_02048";
        #endregion

        //TWD管理：                       02050 至 02149
        #region TWDManagement
        /// <summary>
        /// DD管理错误
        /// </summary>
        public const string TWDManagementError = "E_02050";

        /// <summary>
        /// 非按灯卡，不能扫描
        /// </summary>
        public const string CantScanNotAnDengCard = "E_02051";


        /// <summary>
        /// 物料单数据错误，缺少零件记录
        /// </summary>
        public const string SheetHasNoDetails = "E_02052";

        /// <summary>
        /// 零件进货验证错误
        /// </summary>
        public const string IncomeVeriryError = "E_02052";

        /// <summary>
        /// 删除退货零件失败
        /// </summary>
        public const string DeleteReturnPartError = "E_02053";
        /// <summary>
        /// 不能根据退货编号找到退货信息
        /// </summary>
        public const string CannotFindReturnInfoByID = "E_02053";
        /// <summary>
        /// 没有卡组号，不能扫描
        /// </summary>
        public const string CanNotScanWithoutDDCardGroup = "E_02054";
        /// <summary>
        ///  非看板卡，不能扫描
        /// </summary>
        public const string CanNotScanNotKanBanCard = "E_02055";

        /// <summary>
        /// 加载TWD零件类列表失败
        /// </summary>
        public const string LoadTwdBoxPartsListError = "E_02056";

        /// <summary>
        /// 加载仓库代码列表失败
        /// </summary>
        public const string LoadWarehouseListError = "E_02057";

        #endregion

        //EPS管理：                      02150 至 02199
        #region EPSManagement

        /// <summary>
        /// EPS管理错误
        /// </summary>
        public const string EPSManagementError = "E_02150";

        /// <summary>
        /// 无法根据拉动关系主键找到记录
        /// </summary>
        public const string CannotFindEPSPullRelationByID = "E_02151";

        /// <summary>
        /// 无法找到按钮记录
        /// </summary>
        public const string CannotFindButton = "E_02152";

        /// <summary>
        /// 无法取消任务
        /// </summary>
        public const string CannotCancelTask = "E_02153";

        /// <summary>
        /// 终端编号已经存在
        /// </summary>
        public const string Terminal_HasExists = "E_02154";

        /// <summary>
        /// 按钮已经存在
        /// </summary>
        public const string ButtonHasExist = "E_02155";


        /// <summary>
        /// 无法找到按钮
        /// </summary>
        public const string CannotFindEPSButtonByID = "E_02156";

        /// <summary>
        /// 无法找到终端
        /// </summary>
        public const string LoadTerminalListError = "E_02157";

        /// <summary>
        /// 待删除的拉动关系对应的零件卡已经建立了与卡片的关联关系，请先删除按钮与卡片的关联关系
        /// </summary>
        public const string PullRelationHasButtonCardLink = "E_02158";

        #endregion

        //JIS管理：                      02200 至 02249
        #region JISManagement
        /// <summary>
        /// JIT管理错误
        /// </summary>
        public const string JITManagementError = "E_02200";

        /// <summary>
        /// 加载零件类(料架)列表失败
        /// </summary>
        public const string LoadRackListError = "E_02201";

        /// <summary>
        /// 加载成对料架列表失败
        /// </summary>
        public const string LoadPairRackListError = "E_02202";

        /// <summary>
        /// 零件类(料架)已经存在
        /// </summary>
        public const string RackExists = "E_02203";

        /// <summary>
        /// 成对料架已经存在
        /// </summary>
        public const string PairRackExists = "E_02204";

        /// <summary>
        /// 无法找到JIS4位零件
        /// </summary>
        public const string LoadPartJITError = "E_02205";

        /// <summary>
        /// JIT零件4位对8位对应关系已经存在
        /// </summary>
        public const string PartJITExists = "E_02206";

        /// <summary>
        /// 无法找到Runsheet特殊说明
        /// </summary>
        public const string LoadRunsheetSpecialError = "E_02207";

        /// <summary>
        /// Runsheet特殊说明已经存在
        /// </summary>
        public const string RunsheetSpecialExists = "E_02208";

        /// <summary>
        /// 导入的料架数据格式不正确
        /// </summary>
        public const string InvalidRackFormat = "E_02209";


        /// <summary>
        /// 在编辑FLEX零件号对应关系时零件号码格式不正确，零件号码的后四位应该与FLEX零件号一致
        /// </summary>
        public const string PartNoError = "E_02210";


        /// <summary>
        /// 加载托盘设置列表失败
        /// </summary>
        public const string LoadTrayListError = "E_02211";

        /// <summary>
        /// 加载JIS拉动单列表失败
        /// </summary>
        public const string LoadJisRunsheetListError = "E_02212";

        /// <summary>
        /// JIS拉动单已经存在
        /// </summary>
        public const string JisRunsheetExists = "E_02213";

        /// <summary>
        /// 更新JIS拉动单详细零件信息失败失败
        /// </summary>
        public const string UpdateJISRunsheetPartsInfoFailed = "E_02214";

        #endregion

        //零件管理：                     02250 至 02299
        #region PartManagement
        /// <summary>
        /// 零件管理错误
        /// </summary>
        public const string PartManagementError = "E_02250";


        /// <summary>
        /// 卡号位数不正确!请输入4位卡号!
        /// </summary>
        public const string InvalidCardCodeAnDeng = "E_02251";

        /// <summary>
        /// 卡号位数不正确!请输入8位卡号!
        /// </summary>
        public const string InvalidCardCodeKanban = "E_02252";


        /// <summary>
        /// 零件类信息不存在！
        /// </summary>
        public const string BoxPartsNotExist = "E_02253";

        /// <summary>
        /// 零件信息不存在！
        /// </summary>
        public const string PartNotExist = "E_02254";

        /// <summary>
        /// 该卡未启用
        /// </summary>
        public const string CantScanDisabledCard = "E_02255";

        /// <summary>
        /// 需要两级拉动的零件在RC车间无对应的卡号
        /// </summary>
        public const string PartNeedTwicePullNotExistInRC = "E_02256";

        /// <summary>
        /// 零件库存信息不存在
        /// </summary>
        public const string PartStrockNotExists = "E_02257";

        /// <summary>
        /// 零件与零件卡的关联关系不存在
        /// </summary>
        public const string PartCardLinkNotExists = "E_02258";

        /// <summary>
        /// 零件盘点记录不存在
        /// </summary>
        public const string LoadCountListError = "E_02259";

        /// <summary>
        /// 零件盘点记录已经存在
        /// </summary>
        public const string CountListExists = "E_02260";

        /// <summary>
        /// 库存信息中的路径、零件号、库位与零件卡里的对应字段匹配不上
        /// </summary>
        public const string StockInfoNotMatchCardInfo = "E_02261";

        #endregion

        //PCS管理：                      02300 至 02399
        #region PCSManagement
        /// <summary>
        /// PPS管理错误
        /// </summary>
        public const string PCSManagementError = "E_02300";
        /// <summary>
        /// 无法找到PVIRegion
        /// </summary>
        public const string CanNotFindPVIRegion = "E_02301";
        /// <summary>
        /// 无法找到DeliveryScheduleId
        /// </summary>
        public const string CanNotFindDeliveryScheduleId = "E_02302";
        /// <summary>
        /// 无法找到PartSpecId
        /// </summary>
        public const string CanNotFindPartSpecId = "E_02303";
        /// <summary>
        /// 无法找到ULocPartDetailId
        /// </summary>
        public const string CanNotFindULocPartDetailId = "E_02304";
        /// <summary>
        /// 无法找到VechicleId
        /// </summary>
        public const string CanNotFindVechicleId = "E_02305";

        /// <summary>
        /// 无法找到PartDetailID
        /// </summary>
        public const string CanNotFindPartDetailId = "E_02306";

        /// <summary>
        /// 添加PCS窗口时间信息失败
        /// </summary>
        public const string AddDeliveryScheduleWindowsTimeInfoFailed = "E_02307";

        #endregion

        //打印机管理：                   02400 至 02449
        #region PrinterManagement

        /// <summary>
        /// 打印机管理错误
        /// </summary>
        public const string PrinterManagementError = "E_02400";

        #endregion

        //报表管理：                     02500 至 02599
        #region ReportManagement
        /// <summary>
        /// 报表管理错误
        /// </summary>
        public const string ReportManagementError = "E_02500";

        #endregion

        //供应商管理：                　 02600 至 02649
        #region SupplierManagement
        /// <summary>
        /// 供应商管理错误
        /// </summary>
        public const string SupplierManagementError = "E_02600";


        /// <summary>
        /// 加载供应商列表失败
        /// </summary>
        public const string LoadSupplierListError = "E_02601";

        /// <summary>
        /// 根据车间编号找不到对应的供应商
        /// </summary>
        public const string CannotFindSupplierByWorkshop = "E_02601";

        /// <summary>
        /// 供应商已经存在
        /// </summary>
        public const string SupplierHasExist = "E_02602";


        /// <summary>
        /// 供应商工厂车间关系已经存在
        /// </summary>
        public const string SupplierWorkshopRelationExists = "E_02603";

        /// <summary>
        /// 供应商窗口时间已经存在
        /// </summary>
        public const string SupplierOnlineTimeHasExist = "E_02604";

        #endregion

        //系统管理：                     02650 至 02749
        #region SystemManagement

        /// <summary>
        /// 系统管理错误
        /// </summary>
        public const string SystemManagementError = "E_02650";


        /// <summary>
        /// 用户输入的代码值和代码名称已经存在
        /// </summary>
        public const string BASAlreadyHaveCodeNameValue = "E_02651";

        /// <summary>
        /// 用户输入的代码名称已经存在
        /// </summary>
        public const string BASAlreadyHaveCodeName = "E_02652";

        /// <summary>
        /// 用户输入的系统表示符已经存在
        /// </summary>
        public const string BASAlreadyHaveSystemId = "E_02653";

        /// <summary>
        /// 加载用户列表失败
        /// </summary>
        public const string LoadUserListError = "E_02654";

        #endregion

        //监控管理：                     02750 至 02849
        #region MonitorManagement
        /// <summary>
        /// 监控管理错误
        /// </summary>
        public const string MonitorManagementError = "E_02750";


        #endregion

        //接口查询                       02850 至 02899
        #region InterfaceMonitor
        /// <summary>
        /// 接口查询错误
        /// </summary>
        public const string InterfaceMonitorError = "E_02850";


        #endregion

        //备用：                         02900 至 02999
        #region Reserved
        /// <summary>
        /// 备用错误
        /// </summary>
        public const string ReservedError = "E_02900";

        /// <summary>
        /// 无法根据Activity找到纪录
        /// </summary>
        public const string CannotFindActivityByID = "E_02901";

        /// <summary>
        /// Activity编号已经存在
        /// </summary>
        public const string ActivityHasExists = "E_02902";

        #endregion

        #endregion


        //物流执行引擎子系统：            03001 至 04000
        #region PullEngine

        //JIT 拉动引擎          03001 至03299
        #region JITPullEngine
        /// <summary>
        /// JIT拉动引擎错误
        /// </summary>
        public const string JITPullEngineError = "E_03001";



        #endregion

        //EPS 拉动引擎         03300 至03499
        #region EPSEngine
        /// <summary>
        /// EPS拉动引擎错误
        /// </summary>
        public const string EPSPullEngineError = "E_03300";


        #endregion

        //PPS 拉动引擎         03500 至03699
        #region PPSEngine
        /// <summary>
        /// PPS拉动引擎错误
        /// </summary>
        public const string PPSPullEngineError = "E_03500";
        #endregion

        //DD 拉动引擎          03700 至 03899
        #region DDEngine

        /// <summary>
        /// PPS拉动引擎错误
        /// </summary>
        public const string DDPullEngineError = "E_03700";

        #endregion

        //备用
        #region Reserved
        #endregion


        #endregion

        //EPS任务发布子系统：             05001 至 06000
        #region EPSPublishWebService
        /// <summary>
        /// EPS任务发布子系统错误
        /// </summary>
        public const string EPSPublishWebServiceError = "E_05001";

        /// <summary>
        /// 无线按钮表中，没有指定按钮编号的配置信息
        /// </summary>
        public const string NoButtonConfigInfoError = "E_05002";
        #endregion


        //RDC供应商客户端应用子系统：     06001 至 07000
        #region RDCClient
        /// <summary>
        /// RDC供应商客户端应用子系统
        /// </summary>
        public const string RDCClientError = "E_06001";

        #endregion

        //外部供应商客户端应用子系统：    07001 至 08000
        #region SupplierClient

        /// <summary>
        /// 外部供应商客户端应用子系统
        /// </summary>
        public const string SupplierClientError = "E_07001";


        #endregion

        //运营监控管理子系统：            09001 至 10000
        #region Monitor
        /// <summary>
        /// 运营监控管理子系统
        /// </summary>
        public const string MonitorError = "E_09001";

        #endregion

        //运营统计分析子系统：            10001 至 11000
        #region Analysis
        /// <summary>
        /// 运营统计分析子系统
        /// </summary>
        const string AnalysisError = "E_10001";

        #endregion

        //基础服务子系统：                11001 至 12000
        #region BaseService
        /// <summary>
        /// 基础服务子系统
        /// </summary>
        const string BaseServiceError = "E_11001";

        #endregion


        //基础服务子系统：                12001 至 13000
        /// <summary>
        /// 无法根据Activity找到纪录
        /// </summary>

        //SPS成套供给系统:                       13001 至 14000

        /// <summary>
        /// 加载SPS列表失败
        /// </summary>
        public const string LoadPickingLineError = "E_13001";

        public const string LoadPickingSubLineError = "E_13002";


        public const string CannotFindBomInfoByID = "E_12001";



        #endregion




    }
}
