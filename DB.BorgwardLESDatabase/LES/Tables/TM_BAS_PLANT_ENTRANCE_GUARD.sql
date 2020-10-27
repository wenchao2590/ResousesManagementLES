CREATE TABLE [LES].[TM_BAS_PLANT_ENTRANCE_GUARD] (
    [EG_ID]       BIGINT         NOT NULL,
    [PLANT]       NVARCHAR (8)   NULL,
    [EG_CODE]     NVARCHAR (32)  NULL,
    [EG_NAME]     NVARCHAR (32)  NULL,
    [EG_ADDRESS]  NVARCHAR (100) NULL,
    [EG_TYPE]     INT            NULL,
    [VALID_FLAG]  BIT            NULL,
    [CREATE_USER] NVARCHAR (32)  NULL,
    [CREATE_DATE] DATETIME       NULL,
    [MODIFY_USER] NVARCHAR (32)  NULL,
    [MODIFY_DATE] DATETIME       NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PLANT_ENTRANCE_GUARD', @level2type = N'COLUMN', @level2name = N'MODIFY_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PLANT_ENTRANCE_GUARD', @level2type = N'COLUMN', @level2name = N'MODIFY_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PLANT_ENTRANCE_GUARD', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PLANT_ENTRANCE_GUARD', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)有效标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PLANT_ENTRANCE_GUARD', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PLANT_ENTRANCE_GUARD', @level2type = N'COLUMN', @level2name = N'EG_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PLANT_ENTRANCE_GUARD', @level2type = N'COLUMN', @level2name = N'EG_ADDRESS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PLANT_ENTRANCE_GUARD', @level2type = N'COLUMN', @level2name = N'EG_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PLANT_ENTRANCE_GUARD', @level2type = N'COLUMN', @level2name = N'EG_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PLANT_ENTRANCE_GUARD', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PLANT_ENTRANCE_GUARD', @level2type = N'COLUMN', @level2name = N'EG_ID';

