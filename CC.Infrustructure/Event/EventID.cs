namespace Infrustructure.Event
{
    /// <summary>
    /// 事件报警编号，报警编号参考EventType, 编号在EventType分类后面，如JIS分类类型200, JIS事件ID为201...299
    /// </summary>
    public enum EventID
    {
        /// <summary>
        /// 1	MIN报警
        /// </summary>
        MinAlarm = 1,

        /// <summary>
        /// 2	MAX报警
        /// </summary>
        MaxAlarm = 2,

        /// <summary>
        /// 3	任务无终端
        /// </summary>
        NoTerminal = 3,

        /// <summary>
        /// 4	任务预警
        /// </summary>
        TaskAlarm = 4,


        /// <summary>
        /// 5	任务超时
        /// </summary>
        TaskTimeout = 5,

        /// <summary>
        /// 6	按钮低电压
        /// </summary>
        ButtonLowV = 6,

        /// <summary>
        /// 7	终端异常
        /// </summary>
        TerminalException = 7,

        /// <summary>
        /// 8	按钮异常
        /// </summary>
        ButtonException = 8,


        //10	车辆故障

        /// <summary>
        /// 12	需要更换电池
        /// </summary>
        NeetReplace = 12,

        /// <summary>
        /// 16	非标包装
        /// </summary>
        NotStandardPackage = 16,

        /// <summary>
        /// 20	按钮心跳
        /// </summary>
        ButtonAlive = 20,

        /// <summary>
        /// 101	无效的工作时间
        /// </summary>
        ErrorWorkTime = 101,

        /// <summary>
        /// 102	SendTime生成错误
        /// </summary>
        WorkTimeGenError = 102,

        /// <summary>
        /// 103	物料单传真失败
        /// </summary>
        SendFaxError = 103,

        /// <summary>
        /// 104	按灯组织小于最小批量
        /// </summary>
        LessThanMin = 104,

        /// <summary>
        /// 105	按灯组织创建空白物料单
        /// </summary>
        BlankRunsheetCreated = 105,

        /// <summary>
        /// 106	按灯组织溢库
        /// </summary>
        OverflowStock = 106,

        /// <summary>
        /// 107	按灯组织发送时间异常
        /// </summary>
        SendTimeException = 107,

        /// <summary>
        /// 108	按灯组织发送时间没有工作时间
        /// </summary>
        NoWorkTime = 108,

        /// <summary>
        /// 109	按灯组织创建空白物料单异常
        /// </summary>
        BlankRunsheetCreatedException = 109,

        /// <summary>
        /// 110 RDC物料单打印错误
        /// </summary>
        RDCPrintError = 110,

        /// <summary>
        /// 111 RDC物料单发送错误
        /// </summary>
        RDCSendError = 111,

        /// <summary>
        /// 112	组织创建DD物料单异常
        /// </summary>
        DDRunsheetCreatedException = 112,

        /// <summary>
        /// 201	JIS预览进程异常
        /// </summary>
        PreviewException = 201,

        /// <summary>
        /// 202	JIS预览找不到订单号
        /// </summary>
        PreviewNotFoundOrder = 202,

        /// <summary>
        /// 210	JIS零件拆分进程异常
        /// </summary>
        PartsResolveException = 210,

        /// <summary>
        /// 211	JIS零件拆分过点序号不连续
        /// </summary>
        RunningNoNotContinue = 211,

        /// <summary>
        /// 212	JIS零件拆分找不到订单
        /// </summary>
        PartsResolveNotFoundOrder = 212,

        /// <summary>
        /// 213	JIS零件拆分找不到料架
        /// </summary>
        PartsResolveNotFoundRack = 213,

        ///// <summary>
        ///// 202	料架行或列设置错误
        ///// </summary>
        //SetRackError = 202,

        /// <summary>
        /// 203	JIT自动收料
        /// </summary>
        JITAutoReceived = 203,

        /// <summary>
        /// 204	JIT成对料架错误
        /// </summary>
        PairRackError = 204,

        /// <summary>
        /// 205	操作JIT4位零件与8位零件表错误
        /// </summary>
        JITPartNo4To8Error = 205,

        /// <summary>
        /// 206	同步料架尝试
        /// </summary>
        SynRackTry = 206,

        /// <summary>
        /// 207	Flex数据格式错误
        /// </summary>
        FlexDataFormatError = 207,

        /// <summary>
        /// 208	Flex序列跳号错误
        /// </summary>
        FlexSerialNoJump = 208,

        /// <summary>
        /// 301	PPS接口数据错误
        /// </summary>
        PPSInterfaceDataError = 301,

        /// <summary>
        /// 302	PPS数据导入异常
        /// </summary>
        PPS_DataImportException = 302,

        /// <summary>
        /// 401	生成SAP班报
        /// </summary>
        SAPReportGenerated = 401,

        /// <summary>
        /// 402	生成SAP班报错误
        /// </summary>
        SAPReportGeneratedError = 402,

        /// <summary>
        /// 403	生成WM班报
        /// </summary>
        WMReportGenerated = 403,

        /// <summary>
        /// 404	生成WM班报错误
        /// </summary>
        WMReportGeneratedError = 404,

        /// <summary>
        /// 405	生成物料单日报
        /// </summary>
        MatieralReportGenerated = 405,

        /// <summary>
        /// 406	生成物料单日报错误
        /// </summary>
        MatieralReportGeneratedError = 406,

        /// <summary>
        /// 407	生成PO供应商日报
        /// </summary>
        POSupplierReportGenerated = 407,

        /// <summary>
        /// 408	生成PO供应商日报错误
        /// </summary>
        POSupplierReportGeneratedError = 408,

        /// <summary>
        /// 409	生成CC供应商日报
        /// </summary>
        CCSupplierReportGenerated = 409,

        /// <summary>
        /// 410	生成CC供应商日报错误
        /// </summary>
        CCSupplierReportGeneratedError = 410,

        /// <summary>
        /// 411	生成热加工日报
        /// </summary>
        HotSupplierReportGenerated = 411,

        /// <summary>
        /// 412	生成热加工日报错误
        /// </summary>
        HotSupplierReportGeneratedError = 412,

        /// <summary>
        /// 413 报表FTP上传错误 
        /// </summary>
        ReportFTPError = 413,

        /// <summary>
        /// 501	终端异常（消息）
        /// </summary>
        TerminalError = 501,

        /// <summary>
        /// 502	车辆异常（消息）
        /// </summary>
        VehicleError = 502,

        /// <summary>
        /// 503	需要更换电池（消息）
        /// </summary>
        LowBattery = 503,

        /// <summary>
        /// 601	供应商返回错误
        /// </summary>
        SupplierFeedBackError = 601,

        /// <summary>
        /// 602	供应商登录
        /// </summary>
        SupplierLogin = 602,

        /// <summary>
        /// 603	供应商修改密码
        /// </summary>
        SupplierPasswordModified = 603,

        /// <summary>
        /// 604	物料单接收异常
        /// </summary>
        MaterialReceiveException = 604,

        /// <summary>
        /// 701	组织物料单异常
        /// </summary>
        SPSFormRunsheetException = 701,

        /// <summary>
        /// 702	上传BOM异常
        /// </summary>
        SPSLoadBomException = 702,

        /// <summary>
        /// 703	计算消耗异常
        /// </summary>
        SPSCalcConsumeException = 703,

        /// <summary>
        /// 704	上传DD异常
        /// </summary>
        SPSTransferDDException = 704,

        /// <summary>
        /// 705	配载单打印异常
        /// </summary>
        SPSPrintException = 705,

        /// <summary>
        /// 706 MAS分拣异常
        /// </summary>
        SPSMasException = 706,

        /// <summary>
        /// 707 下载MDM数据异常
        /// </summary>
        MDMTransferException = 707,

        /// <summary>
        /// 宝马CallOff报文删除
        /// </summary>
        BMWCallOffDelete = 801,

        /// <summary>
        /// 宝马报文超时
        /// </summary>
        BMWCallOffDelay = 802,

        /// <summary>
        /// 宝马排序信息不连续
        /// </summary>
        BMWCallOffCheckSequence = 803,

        /// <summary>
        /// 宝马CallOff报文时间顺序错误
        /// </summary>
        BMWCallOffMessageTime = 804,
        /// <summary>
        /// 宝马Tod报文删除
        /// </summary>
        BMWTodDelete = 901,

        /// <summary>
        /// 宝马零件组不匹配
        /// </summary>
        BMWTodPartFamily = 902,

        /// <summary>
        /// 宝马零件组不匹配
        /// </summary>
        BMWTodSupplyGroup = 903,

        /// <summary>
        /// 宝马Tod报文时间顺序错误
        /// </summary>
        BMWTodMessageTime = 904,

        /// <summary>
        /// 宝马Tod报文收到多条新增数据
        /// </summary>
        BMWTodInsert = 905,

        /// <summary>
        /// 宝马报文超时
        /// </summary>
        BMWTodDelay = 906,

        /// <summary>
        /// 宝马VehicleModel不匹配
        /// </summary>
        BMWTodVehicleModel = 907,

        /// <summary>
        /// 收到宝马Reorder信息
        /// </summary>
        BMWReorder = 1000,

        /// <summary>
        /// 宝马Reorder零件不在原始Tod中
        /// </summary>
        BMWReorderNoTodPart = 1001,
        /// <summary>
        /// 
        /// </summary>
        Auth_Success = 1501,

        /// <summary>
        /// 
        /// </summary>
        Auth_Failure = 1502,

        /// <summary>
        /// 
        /// </summary>
        Remove_Session_Success = 1503,

        /// <summary>
        /// 
        /// </summary>
        Remove_Session_Failure = 1504,

        /// <summary>
        /// 
        /// </summary>
        Remove_Eps_Session_Success = 1505,

        /// <summary>
        /// 
        /// </summary>
        Remove_Eps_Session_Failure = 1506,

        /// <summary>
        /// 1601 可疑垃圾数据
        /// </summary>
        Confused_Data = 1601,

        /// <summary>
        /// 1701 SAP BOM导数据时，产生异常
        /// </summary>
        SAPBOMImportException = 1701,

        /// <summary>
        /// 10001 第三方打印系统异常
        /// </summary>
        PSException = 10001,
        /// <summary>
        /// 500 通用客户工厂不存在
        /// </summary>
        CustPlantCodeNotExist = 500,
        /// <summary>
        /// 501 通用客户信息点不存在主数据表中
        /// </summary>
        InfoPointCodeNotExist = 501,
        /// <summary>
        /// 502 通用客户零件标识不存在主数据表中
        /// </summary>
        CustParNoFlagtNotExist = 502,
        /// <summary>
        /// 503 设备监控异常
        /// </summary>
        EquipmentMonitoring = 503,
        /// <summary>
        /// 504 生成拉动服务异常
        /// </summary>
        PullingOrderException = 504,
        /// <summary>
        /// 零件号不存在
        /// </summary>
        PartNoNotExist = 1801,
        /// <summary>
        /// 客户零件号不存在
        /// </summary>
        CustPartNoNotExist = 1802,
        /// <summary>
        /// 数量大于圆整包装数量
        /// </summary>
        QtyMoreThanRoundnessQty = 1803,
        /// <summary>
        /// KEPWARE断开连接
        /// </summary>
        KepwareConnectLost = 10,
    }

}
