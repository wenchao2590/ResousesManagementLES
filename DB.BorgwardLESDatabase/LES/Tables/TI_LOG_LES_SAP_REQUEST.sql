CREATE TABLE [LES].[TI_LOG_LES_SAP_REQUEST] (
    [LESSAPREQUESTID]  INT              IDENTITY (1, 1) NOT NULL,
    [INTERFACECODE]    NVARCHAR (50)    NOT NULL,
    [INTERFACEINFO]    NVARCHAR (400)   NULL,
    [TRANSACTIONID]    UNIQUEIDENTIFIER NOT NULL,
    [BIZTRANSACTIONID] NVARCHAR (200)   NULL,
    [COUNT]            INT              NULL,
    [CONSUMER]         NVARCHAR (200)   NULL,
    [SRVLEVEL]         NVARCHAR (50)    NULL,
    [ACCOUNT]          NVARCHAR (50)    NULL,
    [PASSWORD]         NVARCHAR (50)    NULL,
    [XMLDATA]          NTEXT            NULL,
    [OUTMESSAGE]       NTEXT            NULL,
    [RETURNVALUE]      NVARCHAR (400)   NULL,
    [REQUESTTIME]      DATETIME         NULL,
    [RESPONSETIME]     DATETIME         NULL,
    [RESULT]           INT              NULL,
    [ERRORMESSAGE]     NTEXT            NULL,
    [DESCRIPTION]      NTEXT            NULL,
    [CREATEDTIME]      DATETIME         CONSTRAINT [DF_TI_LOG_LES_SAP_REQUEST_CREATEDTIME] DEFAULT (getdate()) NOT NULL,
    [MODIFIEDTIME]     DATETIME         NULL,
    CONSTRAINT [PK_TI_LOG_LES_SAP_REQUEST] PRIMARY KEY CLUSTERED ([LESSAPREQUESTID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'MODIFIEDTIME', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_LES_SAP_REQUEST', @level2type = N'COLUMN', @level2name = N'MODIFIEDTIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CREATEDTIME', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_LES_SAP_REQUEST', @level2type = N'COLUMN', @level2name = N'CREATEDTIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'DESCRIPTION', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_LES_SAP_REQUEST', @level2type = N'COLUMN', @level2name = N'DESCRIPTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ERRORMESSAGE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_LES_SAP_REQUEST', @level2type = N'COLUMN', @level2name = N'ERRORMESSAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RESULT', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_LES_SAP_REQUEST', @level2type = N'COLUMN', @level2name = N'RESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RESPONSETIME', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_LES_SAP_REQUEST', @level2type = N'COLUMN', @level2name = N'RESPONSETIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'REQUESTTIME', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_LES_SAP_REQUEST', @level2type = N'COLUMN', @level2name = N'REQUESTTIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RETURNVALUE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_LES_SAP_REQUEST', @level2type = N'COLUMN', @level2name = N'RETURNVALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'OUTMESSAGE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_LES_SAP_REQUEST', @level2type = N'COLUMN', @level2name = N'OUTMESSAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'XMLDATA', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_LES_SAP_REQUEST', @level2type = N'COLUMN', @level2name = N'XMLDATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PASSWORD', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_LES_SAP_REQUEST', @level2type = N'COLUMN', @level2name = N'PASSWORD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ACCOUNT', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_LES_SAP_REQUEST', @level2type = N'COLUMN', @level2name = N'ACCOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SRVLEVEL', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_LES_SAP_REQUEST', @level2type = N'COLUMN', @level2name = N'SRVLEVEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CONSUMER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_LES_SAP_REQUEST', @level2type = N'COLUMN', @level2name = N'CONSUMER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COUNT', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_LES_SAP_REQUEST', @level2type = N'COLUMN', @level2name = N'COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'BIZTRANSACTIONID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_LES_SAP_REQUEST', @level2type = N'COLUMN', @level2name = N'BIZTRANSACTIONID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TRANSACTIONID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_LES_SAP_REQUEST', @level2type = N'COLUMN', @level2name = N'TRANSACTIONID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'INTERFACEINFO', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_LES_SAP_REQUEST', @level2type = N'COLUMN', @level2name = N'INTERFACEINFO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'INTERFACECODE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_LES_SAP_REQUEST', @level2type = N'COLUMN', @level2name = N'INTERFACECODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'LESSAPREQUESTID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_LES_SAP_REQUEST', @level2type = N'COLUMN', @level2name = N'LESSAPREQUESTID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SAP接口_LES请求SAP接口日志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_LES_SAP_REQUEST';

