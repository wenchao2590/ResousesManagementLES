CREATE TABLE [LES].[TM_SPM_TRAY] (
    [ID]                BIGINT         IDENTITY (1, 1) NOT NULL,
    [TRAY_NO]           NVARCHAR (20)  NOT NULL,
    [TRAY_BARCODE_TYPE] INT            NOT NULL,
    [TRAY_USE_TYPE]     INT            NOT NULL,
    [TRAY_OWNER]        NVARCHAR (20)  CONSTRAINT [DF_TM_SPM_TRAY_TRAY_OWNER] DEFAULT ('') NOT NULL,
    [VALID_FLAG]        INT            CONSTRAINT [DF_TM_SPM_TRAY_VALID_FLAG] DEFAULT ((1)) NOT NULL,
    [COMMENTS]          NVARCHAR (200) NULL,
    [CREATE_USER]       NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]       DATETIME       NOT NULL,
    [UPDATE_USER]       NVARCHAR (50)  NULL,
    [UPDATE_DATE]       DATETIME       NULL,
    CONSTRAINT [PK_TM_SPM_TRAY] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TM_SPM_TRAY_1]
    ON [LES].[TM_SPM_TRAY]([TRAY_BARCODE_TYPE] ASC, [TRAY_USE_TYPE] ASC, [TRAY_OWNER] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TM_SPM_TRAY]
    ON [LES].[TM_SPM_TRAY]([TRAY_NO] ASC, [VALID_FLAG] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPM_TRAY', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPM_TRAY', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPM_TRAY', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPM_TRAY', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPM_TRAY', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)有效标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPM_TRAY', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'托盘所有者', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPM_TRAY', @level2type = N'COLUMN', @level2name = N'TRAY_OWNER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'托盘使用类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPM_TRAY', @level2type = N'COLUMN', @level2name = N'TRAY_USE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'托盘标签类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPM_TRAY', @level2type = N'COLUMN', @level2name = N'TRAY_BARCODE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'托盘编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPM_TRAY', @level2type = N'COLUMN', @level2name = N'TRAY_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPM_TRAY', @level2type = N'COLUMN', @level2name = N'ID';

