CREATE TABLE [LES].[TI_LOG_MES_LES_REQUEST] (
    [MESLESREQUESTID]  INT              IDENTITY (1, 1) NOT NULL,
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
    [CREATEDTIME]      DATETIME         CONSTRAINT [DF_TI_LOG_MES_LES_REQUEST_CREATEDTIME] DEFAULT (getdate()) NOT NULL,
    [MODIFIEDTIME]     DATETIME         NULL,
    CONSTRAINT [PK_TI_LOG_MES_LES_REQUEST] PRIMARY KEY CLUSTERED ([MESLESREQUESTID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)MODIFIEDTIME', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_MES_LES_REQUEST', @level2type = N'COLUMN', @level2name = N'MODIFIEDTIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)CREATEDTIME', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_MES_LES_REQUEST', @level2type = N'COLUMN', @level2name = N'CREATEDTIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)DESCRIPTION', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_MES_LES_REQUEST', @level2type = N'COLUMN', @level2name = N'DESCRIPTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)ERRORMESSAGE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_MES_LES_REQUEST', @level2type = N'COLUMN', @level2name = N'ERRORMESSAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)RESULT', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_MES_LES_REQUEST', @level2type = N'COLUMN', @level2name = N'RESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)RESPONSETIME', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_MES_LES_REQUEST', @level2type = N'COLUMN', @level2name = N'RESPONSETIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)REQUESTTIME', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_MES_LES_REQUEST', @level2type = N'COLUMN', @level2name = N'REQUESTTIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)RETURNVALUE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_MES_LES_REQUEST', @level2type = N'COLUMN', @level2name = N'RETURNVALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)OUTMESSAGE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_MES_LES_REQUEST', @level2type = N'COLUMN', @level2name = N'OUTMESSAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)XMLDATA', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_MES_LES_REQUEST', @level2type = N'COLUMN', @level2name = N'XMLDATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)PASSWORD', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_MES_LES_REQUEST', @level2type = N'COLUMN', @level2name = N'PASSWORD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)ACCOUNT', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_MES_LES_REQUEST', @level2type = N'COLUMN', @level2name = N'ACCOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)SRVLEVEL', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_MES_LES_REQUEST', @level2type = N'COLUMN', @level2name = N'SRVLEVEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)CONSUMER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_MES_LES_REQUEST', @level2type = N'COLUMN', @level2name = N'CONSUMER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COUNT', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_MES_LES_REQUEST', @level2type = N'COLUMN', @level2name = N'COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)BIZTRANSACTIONID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_MES_LES_REQUEST', @level2type = N'COLUMN', @level2name = N'BIZTRANSACTIONID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)TRANSACTIONID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_MES_LES_REQUEST', @level2type = N'COLUMN', @level2name = N'TRANSACTIONID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)INTERFACEINFO', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_MES_LES_REQUEST', @level2type = N'COLUMN', @level2name = N'INTERFACEINFO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)INTERFACECODE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_MES_LES_REQUEST', @level2type = N'COLUMN', @level2name = N'INTERFACECODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)MESLESREQUESTID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_LOG_MES_LES_REQUEST', @level2type = N'COLUMN', @level2name = N'MESLESREQUESTID';

