CREATE TABLE [LES].[TT_WMM_RECEIVE_DETAIL_LOG] (
    [RECEIVE_DETAIL_LOG_ID] INT             IDENTITY (1, 1) NOT NULL,
    [RECEIVE_ID]            INT             NULL,
    [RECEIVE_NO]            NVARCHAR (50)   NULL,
    [PART_NO]               NVARCHAR (20)   NULL,
    [NUM]                   NUMERIC (18, 2) NULL,
    [BOX_NUM]               NUMERIC (18, 2) NULL,
    [SEQUENCE]              INT             NULL,
    [ERP_FLAG]              INT             NULL,
    [SEND_TIME]             DATETIME        NULL,
    [CREATE_USER]           NVARCHAR (50)   NULL,
    [CREATE_DATE]           DATETIME        NULL,
    [UPDATE_USER]           NVARCHAR (50)   NULL,
    [UPDATE_DATE]           DATETIME        NULL,
    CONSTRAINT [PK_TT_WMM_RECEIVE_DETAIL_LOG] PRIMARY KEY CLUSTERED ([RECEIVE_DETAIL_LOG_ID] ASC),
    CONSTRAINT [FK_TT_WMM_RECEIVE_DETAIL_LOG_TT_WMM_RECEIVE] FOREIGN KEY ([RECEIVE_ID]) REFERENCES [LES].[TT_WMM_RECEIVE] ([RECEIVE_ID]) ON DELETE CASCADE
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE_DETAIL_LOG', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE_DETAIL_LOG', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE_DETAIL_LOG', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE_DETAIL_LOG', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)发送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE_DETAIL_LOG', @level2type = N'COLUMN', @level2name = N'SEND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)ERP标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE_DETAIL_LOG', @level2type = N'COLUMN', @level2name = N'ERP_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE_DETAIL_LOG', @level2type = N'COLUMN', @level2name = N'SEQUENCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE_DETAIL_LOG', @level2type = N'COLUMN', @level2name = N'BOX_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE_DETAIL_LOG', @level2type = N'COLUMN', @level2name = N'NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE_DETAIL_LOG', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)入库单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE_DETAIL_LOG', @level2type = N'COLUMN', @level2name = N'RECEIVE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)入库流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE_DETAIL_LOG', @level2type = N'COLUMN', @level2name = N'RECEIVE_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE_DETAIL_LOG', @level2type = N'COLUMN', @level2name = N'RECEIVE_DETAIL_LOG_ID';

