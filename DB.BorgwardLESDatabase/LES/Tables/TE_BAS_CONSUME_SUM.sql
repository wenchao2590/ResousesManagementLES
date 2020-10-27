CREATE TABLE [LES].[TE_BAS_CONSUME_SUM] (
    [INTERFACE_ID]          INT            NOT NULL,
    [PLANT]                 NVARCHAR (5)   NOT NULL,
    [PART_NO]               NVARCHAR (20)  NOT NULL,
    [PART_CNAME]            NVARCHAR (100) NULL,
    [SUPPLIER_NUM]          NVARCHAR (12)  NULL,
    [CONSUME_TYPE]          NVARCHAR (10)  NULL,
    [INHOUSE_PACKAGE_MODEL] NVARCHAR (30)  NULL,
    [INHOUSE_PACKAGE]       INT            NULL,
    [MEASURING_UNIT_NO]     VARCHAR (8)    NULL,
    [WMS_CONSUME]           INT            NULL,
    [OPCS_CONSUME]          INT            NULL,
    [DISPATCH_START_DATE]   DATETIME       NULL,
    [DISPATCH_END_DATE]     DATETIME       NULL,
    [COMMENTS]              NVARCHAR (200) NULL,
    [UPDATE_DATE]           DATETIME       NULL,
    [UPDATE_USER]           NVARCHAR (50)  NULL,
    [CREATE_DATE]           DATETIME       NULL,
    [CREATE_USER]           NVARCHAR (50)  NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_CONSUME_SUM', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_CONSUME_SUM', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_CONSUME_SUM', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_CONSUME_SUM', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_CONSUME_SUM', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '统计结束日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_CONSUME_SUM', @level2type = N'COLUMN', @level2name = N'DISPATCH_END_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '统计起始日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_CONSUME_SUM', @level2type = N'COLUMN', @level2name = N'DISPATCH_START_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'OPCS消耗', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_CONSUME_SUM', @level2type = N'COLUMN', @level2name = N'OPCS_CONSUME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'WMS消耗', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_CONSUME_SUM', @level2type = N'COLUMN', @level2name = N'WMS_CONSUME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_CONSUME_SUM', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_CONSUME_SUM', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_CONSUME_SUM', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_CONSUME_SUM', @level2type = N'COLUMN', @level2name = N'CONSUME_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_CONSUME_SUM', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_CONSUME_SUM', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_CONSUME_SUM', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_CONSUME_SUM', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '接口流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_CONSUME_SUM', @level2type = N'COLUMN', @level2name = N'INTERFACE_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '临时_ OPCS消耗表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_CONSUME_SUM';

