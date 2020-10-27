namespace Infrustructure.Event
{
    /// <summary>
    /// �¼�������ţ�������Ųο�EventType, �����EventType������棬��JIS��������200, JIS�¼�IDΪ201...299
    /// </summary>
    public enum EventID
    {
        /// <summary>
        /// 1	MIN����
        /// </summary>
        MinAlarm = 1,

        /// <summary>
        /// 2	MAX����
        /// </summary>
        MaxAlarm = 2,

        /// <summary>
        /// 3	�������ն�
        /// </summary>
        NoTerminal = 3,

        /// <summary>
        /// 4	����Ԥ��
        /// </summary>
        TaskAlarm = 4,


        /// <summary>
        /// 5	����ʱ
        /// </summary>
        TaskTimeout = 5,

        /// <summary>
        /// 6	��ť�͵�ѹ
        /// </summary>
        ButtonLowV = 6,

        /// <summary>
        /// 7	�ն��쳣
        /// </summary>
        TerminalException = 7,

        /// <summary>
        /// 8	��ť�쳣
        /// </summary>
        ButtonException = 8,


        //10	��������

        /// <summary>
        /// 12	��Ҫ�������
        /// </summary>
        NeetReplace = 12,

        /// <summary>
        /// 16	�Ǳ��װ
        /// </summary>
        NotStandardPackage = 16,

        /// <summary>
        /// 20	��ť����
        /// </summary>
        ButtonAlive = 20,

        /// <summary>
        /// 101	��Ч�Ĺ���ʱ��
        /// </summary>
        ErrorWorkTime = 101,

        /// <summary>
        /// 102	SendTime���ɴ���
        /// </summary>
        WorkTimeGenError = 102,

        /// <summary>
        /// 103	���ϵ�����ʧ��
        /// </summary>
        SendFaxError = 103,

        /// <summary>
        /// 104	������֯С����С����
        /// </summary>
        LessThanMin = 104,

        /// <summary>
        /// 105	������֯�����հ����ϵ�
        /// </summary>
        BlankRunsheetCreated = 105,

        /// <summary>
        /// 106	������֯���
        /// </summary>
        OverflowStock = 106,

        /// <summary>
        /// 107	������֯����ʱ���쳣
        /// </summary>
        SendTimeException = 107,

        /// <summary>
        /// 108	������֯����ʱ��û�й���ʱ��
        /// </summary>
        NoWorkTime = 108,

        /// <summary>
        /// 109	������֯�����հ����ϵ��쳣
        /// </summary>
        BlankRunsheetCreatedException = 109,

        /// <summary>
        /// 110 RDC���ϵ���ӡ����
        /// </summary>
        RDCPrintError = 110,

        /// <summary>
        /// 111 RDC���ϵ����ʹ���
        /// </summary>
        RDCSendError = 111,

        /// <summary>
        /// 112	��֯����DD���ϵ��쳣
        /// </summary>
        DDRunsheetCreatedException = 112,

        /// <summary>
        /// 201	JISԤ�������쳣
        /// </summary>
        PreviewException = 201,

        /// <summary>
        /// 202	JISԤ���Ҳ���������
        /// </summary>
        PreviewNotFoundOrder = 202,

        /// <summary>
        /// 210	JIS�����ֽ����쳣
        /// </summary>
        PartsResolveException = 210,

        /// <summary>
        /// 211	JIS�����ֹ�����Ų�����
        /// </summary>
        RunningNoNotContinue = 211,

        /// <summary>
        /// 212	JIS�������Ҳ�������
        /// </summary>
        PartsResolveNotFoundOrder = 212,

        /// <summary>
        /// 213	JIS�������Ҳ����ϼ�
        /// </summary>
        PartsResolveNotFoundRack = 213,

        ///// <summary>
        ///// 202	�ϼ��л������ô���
        ///// </summary>
        //SetRackError = 202,

        /// <summary>
        /// 203	JIT�Զ�����
        /// </summary>
        JITAutoReceived = 203,

        /// <summary>
        /// 204	JIT�ɶ��ϼܴ���
        /// </summary>
        PairRackError = 204,

        /// <summary>
        /// 205	����JIT4λ�����8λ��������
        /// </summary>
        JITPartNo4To8Error = 205,

        /// <summary>
        /// 206	ͬ���ϼܳ���
        /// </summary>
        SynRackTry = 206,

        /// <summary>
        /// 207	Flex���ݸ�ʽ����
        /// </summary>
        FlexDataFormatError = 207,

        /// <summary>
        /// 208	Flex�������Ŵ���
        /// </summary>
        FlexSerialNoJump = 208,

        /// <summary>
        /// 301	PPS�ӿ����ݴ���
        /// </summary>
        PPSInterfaceDataError = 301,

        /// <summary>
        /// 302	PPS���ݵ����쳣
        /// </summary>
        PPS_DataImportException = 302,

        /// <summary>
        /// 401	����SAP�౨
        /// </summary>
        SAPReportGenerated = 401,

        /// <summary>
        /// 402	����SAP�౨����
        /// </summary>
        SAPReportGeneratedError = 402,

        /// <summary>
        /// 403	����WM�౨
        /// </summary>
        WMReportGenerated = 403,

        /// <summary>
        /// 404	����WM�౨����
        /// </summary>
        WMReportGeneratedError = 404,

        /// <summary>
        /// 405	�������ϵ��ձ�
        /// </summary>
        MatieralReportGenerated = 405,

        /// <summary>
        /// 406	�������ϵ��ձ�����
        /// </summary>
        MatieralReportGeneratedError = 406,

        /// <summary>
        /// 407	����PO��Ӧ���ձ�
        /// </summary>
        POSupplierReportGenerated = 407,

        /// <summary>
        /// 408	����PO��Ӧ���ձ�����
        /// </summary>
        POSupplierReportGeneratedError = 408,

        /// <summary>
        /// 409	����CC��Ӧ���ձ�
        /// </summary>
        CCSupplierReportGenerated = 409,

        /// <summary>
        /// 410	����CC��Ӧ���ձ�����
        /// </summary>
        CCSupplierReportGeneratedError = 410,

        /// <summary>
        /// 411	�����ȼӹ��ձ�
        /// </summary>
        HotSupplierReportGenerated = 411,

        /// <summary>
        /// 412	�����ȼӹ��ձ�����
        /// </summary>
        HotSupplierReportGeneratedError = 412,

        /// <summary>
        /// 413 ����FTP�ϴ����� 
        /// </summary>
        ReportFTPError = 413,

        /// <summary>
        /// 501	�ն��쳣����Ϣ��
        /// </summary>
        TerminalError = 501,

        /// <summary>
        /// 502	�����쳣����Ϣ��
        /// </summary>
        VehicleError = 502,

        /// <summary>
        /// 503	��Ҫ������أ���Ϣ��
        /// </summary>
        LowBattery = 503,

        /// <summary>
        /// 601	��Ӧ�̷��ش���
        /// </summary>
        SupplierFeedBackError = 601,

        /// <summary>
        /// 602	��Ӧ�̵�¼
        /// </summary>
        SupplierLogin = 602,

        /// <summary>
        /// 603	��Ӧ���޸�����
        /// </summary>
        SupplierPasswordModified = 603,

        /// <summary>
        /// 604	���ϵ������쳣
        /// </summary>
        MaterialReceiveException = 604,

        /// <summary>
        /// 701	��֯���ϵ��쳣
        /// </summary>
        SPSFormRunsheetException = 701,

        /// <summary>
        /// 702	�ϴ�BOM�쳣
        /// </summary>
        SPSLoadBomException = 702,

        /// <summary>
        /// 703	���������쳣
        /// </summary>
        SPSCalcConsumeException = 703,

        /// <summary>
        /// 704	�ϴ�DD�쳣
        /// </summary>
        SPSTransferDDException = 704,

        /// <summary>
        /// 705	���ص���ӡ�쳣
        /// </summary>
        SPSPrintException = 705,

        /// <summary>
        /// 706 MAS�ּ��쳣
        /// </summary>
        SPSMasException = 706,

        /// <summary>
        /// 707 ����MDM�����쳣
        /// </summary>
        MDMTransferException = 707,

        /// <summary>
        /// ����CallOff����ɾ��
        /// </summary>
        BMWCallOffDelete = 801,

        /// <summary>
        /// �����ĳ�ʱ
        /// </summary>
        BMWCallOffDelay = 802,

        /// <summary>
        /// ����������Ϣ������
        /// </summary>
        BMWCallOffCheckSequence = 803,

        /// <summary>
        /// ����CallOff����ʱ��˳�����
        /// </summary>
        BMWCallOffMessageTime = 804,
        /// <summary>
        /// ����Tod����ɾ��
        /// </summary>
        BMWTodDelete = 901,

        /// <summary>
        /// ��������鲻ƥ��
        /// </summary>
        BMWTodPartFamily = 902,

        /// <summary>
        /// ��������鲻ƥ��
        /// </summary>
        BMWTodSupplyGroup = 903,

        /// <summary>
        /// ����Tod����ʱ��˳�����
        /// </summary>
        BMWTodMessageTime = 904,

        /// <summary>
        /// ����Tod�����յ�������������
        /// </summary>
        BMWTodInsert = 905,

        /// <summary>
        /// �����ĳ�ʱ
        /// </summary>
        BMWTodDelay = 906,

        /// <summary>
        /// ����VehicleModel��ƥ��
        /// </summary>
        BMWTodVehicleModel = 907,

        /// <summary>
        /// �յ�����Reorder��Ϣ
        /// </summary>
        BMWReorder = 1000,

        /// <summary>
        /// ����Reorder�������ԭʼTod��
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
        /// 1601 ������������
        /// </summary>
        Confused_Data = 1601,

        /// <summary>
        /// 1701 SAP BOM������ʱ�������쳣
        /// </summary>
        SAPBOMImportException = 1701,

        /// <summary>
        /// 10001 ��������ӡϵͳ�쳣
        /// </summary>
        PSException = 10001,
        /// <summary>
        /// 500 ͨ�ÿͻ�����������
        /// </summary>
        CustPlantCodeNotExist = 500,
        /// <summary>
        /// 501 ͨ�ÿͻ���Ϣ�㲻���������ݱ���
        /// </summary>
        InfoPointCodeNotExist = 501,
        /// <summary>
        /// 502 ͨ�ÿͻ������ʶ�����������ݱ���
        /// </summary>
        CustParNoFlagtNotExist = 502,
        /// <summary>
        /// 503 �豸����쳣
        /// </summary>
        EquipmentMonitoring = 503,
        /// <summary>
        /// 504 �������������쳣
        /// </summary>
        PullingOrderException = 504,
        /// <summary>
        /// ����Ų�����
        /// </summary>
        PartNoNotExist = 1801,
        /// <summary>
        /// �ͻ�����Ų�����
        /// </summary>
        CustPartNoNotExist = 1802,
        /// <summary>
        /// ��������Բ����װ����
        /// </summary>
        QtyMoreThanRoundnessQty = 1803,
        /// <summary>
        /// KEPWARE�Ͽ�����
        /// </summary>
        KepwareConnectLost = 10,
    }

}
