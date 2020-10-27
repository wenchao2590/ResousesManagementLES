CREATE TABLE [LES].[TM_BAS_INHOUSE_BREAKPOINT_DETAIL] (
    [SEQ_ID]                INT            IDENTITY (1, 1) NOT NULL,
    [INHOUSE_BREAKPOINT_NO] NVARCHAR (10)  NOT NULL,
    [INHOUSE_IDENTITY]      INT            NOT NULL,
    [IN_PLANT_SYSTEM_MODE]  NVARCHAR (10)  NULL,
    [INHOUSE_SYSTEM_MODE]   NVARCHAR (10)  NULL,
    [PART_NO]               NVARCHAR (20)  NULL,
    [PART_CNAME]            NVARCHAR (100) NULL,
    [PART_NICK_NAME]        NVARCHAR (30)  NULL,
    [LOCATION]              NVARCHAR (20)  NULL,
    CONSTRAINT [IDX_PK_INHOUSE_BREAKPOINT_DETAIL_SEQ_ID] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_DETAIL', @level2type = N'COLUMN', @level2name = N'LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件昵称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_NICK_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线系统模块', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_DETAIL', @level2type = N'COLUMN', @level2name = N'INHOUSE_SYSTEM_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '厂内系统模块', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_DETAIL', @level2type = N'COLUMN', @level2name = N'IN_PLANT_SYSTEM_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'INHOUSE标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_DETAIL', @level2type = N'COLUMN', @level2name = N'INHOUSE_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'INHOUSE断点标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_DETAIL', @level2type = N'COLUMN', @level2name = N'INHOUSE_BREAKPOINT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_DETAIL', @level2type = N'COLUMN', @level2name = N'SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_零件断点明细', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_DETAIL';

