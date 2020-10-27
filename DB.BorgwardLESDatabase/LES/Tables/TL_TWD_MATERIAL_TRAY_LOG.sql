CREATE TABLE [LES].[TL_TWD_MATERIAL_TRAY_LOG] (
    [ID]                    BIGINT         IDENTITY (1, 1) NOT NULL,
    [PLANT]                 NVARCHAR (5)   NULL,
    [ASSEMBLY_LINE]         NVARCHAR (10)  NULL,
    [SUPPLIER_NUM]          NVARCHAR (12)  NULL,
    [BOX_PARTS]             NVARCHAR (10)  NULL,
    [DMSNO]                 NVARCHAR (50)  NULL,
    [PART_NO]               NVARCHAR (20)  NULL,
    [PART_CNAME]            NVARCHAR (100) NULL,
    [PACK_COUNT]            INT            NULL,
    [TWD_RUNSHEET_NO]       NVARCHAR (50)  NULL,
    [FARBAU]                NVARCHAR (30)  NULL,
    [FARBIN]                NVARCHAR (30)  NULL,
    [MODEL_YEAR]            NVARCHAR (30)  NULL,
    [MODEL]                 NVARCHAR (30)  NULL,
    [ZCOLORI]               NVARCHAR (30)  NULL,
    [ZCOLORI_D]             NVARCHAR (30)  NULL,
    [IS_ASN]                INT            CONSTRAINT [DF_TL_TWD_MATERIAL_TRAY_LOG_IS_ASN] DEFAULT ((0)) NULL,
    [PROCESS_FLAG]          INT            CONSTRAINT [DF_TL_TWD_MATERIAL_TRAY_LOG_PROCESS_FLAG] DEFAULT ((0)) NULL,
    [PROCESS_DATE]          DATETIME       NULL,
    [IS_GENERATE]           INT            CONSTRAINT [DF_TL_TWD_MATERIAL_TRAY_LOG_IS_GENERATE] DEFAULT ((0)) NULL,
    [ASN_NO]                NVARCHAR (50)  NULL,
    [UPDATE_DATE]           DATETIME       NULL,
    [UPDATE_USER]           NVARCHAR (50)  NULL,
    [CREATE_DATE]           DATETIME       NOT NULL,
    [CREATE_USER]           NVARCHAR (50)  NULL,
    [PUBLISH_TIME]          DATETIME       NULL,
    [EXPECTED_ARRIVAL_TIME] DATETIME       NULL,
    [SUGGEST_DELIVERY_TIME] DATETIME       NULL,
    CONSTRAINT [PK_TL_TWD_MATERIAL_TRAY_LOG] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TL_TWD_MATERIAL_TRAY_LOG_1]
    ON [LES].[TL_TWD_MATERIAL_TRAY_LOG]([DMSNO] ASC, [ASN_NO] ASC, [PUBLISH_TIME] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TL_TWD_MATERIAL_TRAY_LOG]
    ON [LES].[TL_TWD_MATERIAL_TRAY_LOG]([PLANT] ASC, [ASSEMBLY_LINE] ASC, [SUPPLIER_NUM] ASC, [BOX_PARTS] ASC, [PART_NO] ASC, [IS_ASN] ASC, [PROCESS_FLAG] ASC, [IS_GENERATE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建议发货时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'SUGGEST_DELIVERY_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'期望到达时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'EXPECTED_ARRIVAL_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发单时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'PUBLISH_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建用户', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新用户', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ASN单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'ASN_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否生成ASN', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'IS_GENERATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'处理日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'PROCESS_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'处理标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'PROCESS_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否ASN', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'IS_ASN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'外饰颜色描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'ZCOLORI_D';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'外饰颜色代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'ZCOLORI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'内饰颜色描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'内饰颜色代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'MODEL_YEAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'特征包代码描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'FARBIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'特征包代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'FARBAU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TWD拉动单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'TWD_RUNSHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'PACK_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DMS号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'DMSNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'PLANT';

