CREATE TABLE [LES].[TM_BAS_WAITING_AREA_LOCK] (
    [AREA_ID]     INT           NOT NULL,
    [CREATE_USER] NVARCHAR (50) NULL,
    [CREATE_DATE] DATETIME      NULL,
    [UPDATE_USER] NVARCHAR (50) NULL,
    [UPDATE_DATE] DATETIME      NULL,
    CONSTRAINT [PK_TM_BAS_WAITING_AREA_LOCK] PRIMARY KEY CLUSTERED ([AREA_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAITING_AREA_LOCK', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新用户', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAITING_AREA_LOCK', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAITING_AREA_LOCK', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建用户', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAITING_AREA_LOCK', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAITING_AREA_LOCK', @level2type = N'COLUMN', @level2name = N'AREA_ID';

