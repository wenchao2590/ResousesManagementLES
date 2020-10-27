CREATE TABLE [LES].[TM_BAS_INBOUND_BREAKPOINT_DETAIL] (
    [SEQ_ID]                INT            IDENTITY (1, 1) NOT NULL,
    [INBOUND_BREAKPOINT_NO] NVARCHAR (10)  NOT NULL,
    [INBOUND_IDENTITY]      INT            NOT NULL,
    [IN_PLANT_SYSTEM_MODE]  NVARCHAR (10)  NULL,
    [INBOUND_SYSTEM_MODE]   NVARCHAR (10)  NULL,
    [PART_NO]               NVARCHAR (20)  NULL,
    [PART_CNAME]            NVARCHAR (100) NULL,
    [PART_NICK_NAME]        NVARCHAR (30)  NULL,
    [LOCATION]              NVARCHAR (20)  NULL,
    CONSTRAINT [IDX_PK_INBOUND_BREAKPOINT_DETAIL_SEQ_ID] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_DETAIL', @level2type = N'COLUMN', @level2name = N'LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)零件呢称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_NICK_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_DETAIL', @level2type = N'COLUMN', @level2name = N'INBOUND_SYSTEM_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)厂内系统模块', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_DETAIL', @level2type = N'COLUMN', @level2name = N'IN_PLANT_SYSTEM_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_DETAIL', @level2type = N'COLUMN', @level2name = N'INBOUND_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_DETAIL', @level2type = N'COLUMN', @level2name = N'INBOUND_BREAKPOINT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_DETAIL', @level2type = N'COLUMN', @level2name = N'SEQ_ID';

