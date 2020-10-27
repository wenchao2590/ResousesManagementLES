CREATE TABLE [LES].[TM_BAS_WAITING_ZONE] (
    [WZ_ID]           BIGINT        NOT NULL,
    [EG_ID]           BIGINT        NULL,
    [WZ_CODE]         NVARCHAR (32) NULL,
    [WZ_NAME]         NVARCHAR (32) NULL,
    [WZ_CARS_CONTENT] INT           NULL,
    [IS_URGENT]       INT           NULL,
    [WZ_TYPE]         INT           NULL,
    [VALID_FLAG]      BIT           NULL,
    [CREATE_USER]     NVARCHAR (32) NULL,
    [CREATE_DATE]     DATETIME      NULL,
    [MODIFY_USER]     NVARCHAR (32) NULL,
    [MODIFY_DATE]     DATETIME      NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAITING_ZONE', @level2type = N'COLUMN', @level2name = N'MODIFY_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAITING_ZONE', @level2type = N'COLUMN', @level2name = N'MODIFY_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAITING_ZONE', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAITING_ZONE', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)有效标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAITING_ZONE', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAITING_ZONE', @level2type = N'COLUMN', @level2name = N'WZ_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)IsUrgent', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAITING_ZONE', @level2type = N'COLUMN', @level2name = N'IS_URGENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAITING_ZONE', @level2type = N'COLUMN', @level2name = N'WZ_CARS_CONTENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAITING_ZONE', @level2type = N'COLUMN', @level2name = N'WZ_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAITING_ZONE', @level2type = N'COLUMN', @level2name = N'WZ_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAITING_ZONE', @level2type = N'COLUMN', @level2name = N'EG_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAITING_ZONE', @level2type = N'COLUMN', @level2name = N'WZ_ID';

