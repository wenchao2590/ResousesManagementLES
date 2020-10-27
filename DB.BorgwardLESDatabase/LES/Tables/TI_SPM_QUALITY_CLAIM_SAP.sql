CREATE TABLE [LES].[TI_SPM_QUALITY_CLAIM_SAP] (
    [ID]             INT             IDENTITY (1, 1) NOT NULL,
    [NAME1_D]        NVARCHAR (40)   NULL,
    [CLANO]          NVARCHAR (30)   NOT NULL,
    [VHVIN]          NVARCHAR (35)   NULL,
    [CMILE]          DECIMAL (13, 3) NULL,
    [CLAMT]          DECIMAL (13, 2) NULL,
    [DUCODE]         NVARCHAR (18)   NULL,
    [DUCODE_NAME]    NVARCHAR (40)   NULL,
    [MONAM]          NVARCHAR (100)  NULL,
    [PRODAT]         NVARCHAR (8)    NULL,
    [REPDAT]         NVARCHAR (8)    NULL,
    [TRDESC]         NVARCHAR (300)  NULL,
    [CUDESC]         NVARCHAR (300)  NULL,
    [LAAMT]          DECIMAL (13, 2) NULL,
    [PAAMT]          DECIMAL (13, 2) NULL,
    [MAAMT]          DECIMAL (13, 2) NULL,
    [FRAMT]          NVARCHAR (15)   NULL,
    [OTAMT]          DECIMAL (13, 2) NULL,
    [MSDESC]         NVARCHAR (300)  NULL,
    [PANUM]          DECIMAL (13, 3) NULL,
    [LIFNR]          NVARCHAR (10)   NULL,
    [NAME1_K]        NVARCHAR (40)   NULL,
    [BW_PERSON]      NVARCHAR (30)   NULL,
    [BW_PHONE]       NVARCHAR (30)   NULL,
    [BW_EMAIL]       NVARCHAR (300)  NULL,
    [CLAIM_BATCH]    NVARCHAR (30)   NULL,
    [PROCESS_STATUS] INT             CONSTRAINT [DF_TI_SPM_QUALITY_CLAIM_SAP_PROCESS_STATUS] DEFAULT ((0)) NULL,
    [PROCESS_DATE]   DATETIME        NULL,
    [CREATE_USER]    NVARCHAR (50)   NULL,
    [CREATE_DATE]    DATETIME        NULL,
    [UPDATE_USER]    NVARCHAR (50)   NULL,
    [UPDATE_DATE]    DATETIME        NULL,
    CONSTRAINT [PK_TI_SPM_QUALITY_CLAIM_SAP] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'处理时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'PROCESS_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'处理状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'PROCESS_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'索赔批次', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'CLAIM_BATCH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'邮箱', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'BW_EMAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'电话', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'BW_PHONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'联系人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'BW_PERSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'NAME1_K';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'LIFNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'配件数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'PANUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'维修项目名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'MSDESC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'其他总费用', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'OTAMT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'配件运费', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'FRAMT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'配件总管理费', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'MAAMT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'配件总费用', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'PAAMT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工时总费用', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'LAAMT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'顾客描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'CUDESC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'故障主题', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'TRDESC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'维修日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'REPDAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'生产日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'PRODAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车型年', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'MONAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主损配件名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'DUCODE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主损配件代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'DUCODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'索赔金额', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'CLAMT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'行驶里程', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'CMILE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车辆标识编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'VHVIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'索赔单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'CLANO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'经销商名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_QUALITY_CLAIM_SAP', @level2type = N'COLUMN', @level2name = N'NAME1_D';

