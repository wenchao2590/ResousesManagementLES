CREATE TABLE [LES].[TM_BAS_WAITING_AREA] (
    [AREA_ID]       INT            IDENTITY (1, 1) NOT NULL,
    [AREA_NAME]     NVARCHAR (100) NULL,
    [AREA_CAPACITY] INT            NULL,
    [IS_URGENT]     INT            NULL,
    [CREATE_USER]   NVARCHAR (50)  NULL,
    [CREATE_DATE]   DATETIME       NULL,
    [UPDATE_USER]   NVARCHAR (50)  NULL,
    [UPDATE_DATE]   DATETIME       NULL,
    [COMMENTS]      NVARCHAR (200) NULL,
    CONSTRAINT [PK_TM_BAS_WAITING_AREA] PRIMARY KEY CLUSTERED ([AREA_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAITING_AREA', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAITING_AREA', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新用户', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAITING_AREA', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAITING_AREA', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建用户', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAITING_AREA', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否紧急等待区，1是，0不是', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAITING_AREA', @level2type = N'COLUMN', @level2name = N'IS_URGENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'队列容量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAITING_AREA', @level2type = N'COLUMN', @level2name = N'AREA_CAPACITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'等待区名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAITING_AREA', @level2type = N'COLUMN', @level2name = N'AREA_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAITING_AREA', @level2type = N'COLUMN', @level2name = N'AREA_ID';

