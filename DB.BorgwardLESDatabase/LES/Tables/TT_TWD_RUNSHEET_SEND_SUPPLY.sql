CREATE TABLE [LES].[TT_TWD_RUNSHEET_SEND_SUPPLY] (
    [SEND_SEQ]        INT            IDENTITY (1, 1) NOT NULL,
    [TWD_RUNSHEET_SN] INT            NOT NULL,
    [TWD_RUNSHEET_NO] VARCHAR (22)   NULL,
    [PUBLISH_TIME]    DATETIME       NULL,
    [SUPPLIER_NUM]    NVARCHAR (12)  NULL,
    [BOX_PARTS]       NVARCHAR (10)  NULL,
    [SEND_TIME]       DATETIME       NULL,
    [SEND_STATUS]     INT            NULL,
    [FAX_TIME]        DATETIME       NULL,
    [FAX_STATUS]      INT            NULL,
    [COMMENTS]        NVARCHAR (200) NULL,
    [UPDATE_DATE]     DATETIME       NULL,
    [UPDATE_USER]     NVARCHAR (50)  NULL,
    [CREATE_DATE]     DATETIME       NULL,
    [CREATE_USER]     NVARCHAR (50)  NULL,
    CONSTRAINT [IDX_PK_RUNSHEET_SEND_SUPPLY_SEND_SEQ] PRIMARY KEY CLUSTERED ([SEND_SEQ] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_SEND_SUPPLY', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_SEND_SUPPLY', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_SEND_SUPPLY', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_SEND_SUPPLY', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_SEND_SUPPLY', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '传真状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_SEND_SUPPLY', @level2type = N'COLUMN', @level2name = N'FAX_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '传真时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_SEND_SUPPLY', @level2type = N'COLUMN', @level2name = N'FAX_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发送状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_SEND_SUPPLY', @level2type = N'COLUMN', @level2name = N'SEND_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_SEND_SUPPLY', @level2type = N'COLUMN', @level2name = N'SEND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_SEND_SUPPLY', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_SEND_SUPPLY', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '组单时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_SEND_SUPPLY', @level2type = N'COLUMN', @level2name = N'PUBLISH_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '拉动单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_SEND_SUPPLY', @level2type = N'COLUMN', @level2name = N'TWD_RUNSHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '拉动单流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_SEND_SUPPLY', @level2type = N'COLUMN', @level2name = N'TWD_RUNSHEET_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发送流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_SEND_SUPPLY', @level2type = N'COLUMN', @level2name = N'SEND_SEQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_TWD 拉动单发送服务商表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_SEND_SUPPLY';

