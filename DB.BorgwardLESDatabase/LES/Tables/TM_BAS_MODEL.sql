CREATE TABLE [LES].[TM_BAS_MODEL] (
    [MODEL_ID]           INT            IDENTITY (1, 1) NOT NULL,
    [PRODUCTION_MODEL]   NVARCHAR (10)  NULL,
    [IS_MANUAL_MAINTAIN] BIT            NULL,
    [MODEL]              NVARCHAR (10)  NULL,
    [MODEL_DESCRIPTION]  NVARCHAR (200) NULL,
    [COMMENTS]           NVARCHAR (200) NULL,
    [CREATE_USER]        NVARCHAR (50)  NULL,
    [CREATE_DATE]        DATETIME       NULL,
    [UPDATE_USER]        NVARCHAR (50)  NULL,
    [UPDATE_DATE]        DATETIME       NULL,
    CONSTRAINT [IDX_PK_MODEL_MODEL_ID] PRIMARY KEY NONCLUSTERED ([MODEL_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MODEL', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MODEL', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MODEL', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MODEL', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MODEL', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车型描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MODEL', @level2type = N'COLUMN', @level2name = N'MODEL_DESCRIPTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_产品', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MODEL', @level2type = N'COLUMN', @level2name = N'MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '是否手工维护', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MODEL', @level2type = N'COLUMN', @level2name = N'IS_MANUAL_MAINTAIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车型大类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MODEL', @level2type = N'COLUMN', @level2name = N'PRODUCTION_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车型序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MODEL', @level2type = N'COLUMN', @level2name = N'MODEL_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_车型维护', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MODEL';

