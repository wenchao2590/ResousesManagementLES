CREATE TABLE [LES].[TI_BAS_PART_LACATION_IN] (
    [SEQ_ID]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [ZDOCNUM]      NVARCHAR (16)  NULL,
    [ZKWERK]       NVARCHAR (4)   NULL,
    [ZIDNKD]       NVARCHAR (18)  NULL,
    [ZWS]          NVARCHAR (2)   NULL,
    [ZLINE]        NVARCHAR (8)   NULL,
    [ZLOC]         NVARCHAR (20)  NULL,
    [ZVRKME]       NVARCHAR (10)  NULL,
    [DEAL_FLAG]    INT            NULL,
    [PROCESS_FLAG] INT            NULL,
    [PROCESS_TIME] DATETIME       NULL,
    [LOGICAL_PK]   NVARCHAR (50)  NULL,
    [BUSINESS_PK]  NVARCHAR (50)  NULL,
    [COMMENTS]     NVARCHAR (200) NULL,
    [UPDATE_DATE]  DATETIME       NULL,
    [UPDATE_USER]  NVARCHAR (50)  NULL,
    [CREATE_DATE]  DATETIME       NOT NULL,
    [CREATE_USER]  NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_TI_BAS_PART_LACATION_IN_SEQ_ID] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_PART_LACATION_IN', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_PART_LACATION_IN', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_PART_LACATION_IN', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_PART_LACATION_IN', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_PART_LACATION_IN', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_PART_LACATION_IN', @level2type = N'COLUMN', @level2name = N'BUSINESS_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务主关键字', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_PART_LACATION_IN', @level2type = N'COLUMN', @level2name = N'LOGICAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_PART_LACATION_IN', @level2type = N'COLUMN', @level2name = N'PROCESS_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_PART_LACATION_IN', @level2type = N'COLUMN', @level2name = N'PROCESS_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_PART_LACATION_IN', @level2type = N'COLUMN', @level2name = N'DEAL_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_PART_LACATION_IN', @level2type = N'COLUMN', @level2name = N'ZVRKME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_PART_LACATION_IN', @level2type = N'COLUMN', @level2name = N'ZLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '生产线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_PART_LACATION_IN', @level2type = N'COLUMN', @level2name = N'ZLINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_PART_LACATION_IN', @level2type = N'COLUMN', @level2name = N'ZWS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_PART_LACATION_IN', @level2type = N'COLUMN', @level2name = N'ZIDNKD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_PART_LACATION_IN', @level2type = N'COLUMN', @level2name = N'ZKWERK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发布号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_PART_LACATION_IN', @level2type = N'COLUMN', @level2name = N'ZDOCNUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_PART_LACATION_IN', @level2type = N'COLUMN', @level2name = N'SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SAP接口_APO_零件工位信息', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_PART_LACATION_IN';

