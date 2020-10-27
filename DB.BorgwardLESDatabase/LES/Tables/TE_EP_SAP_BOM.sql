CREATE TABLE [LES].[TE_EP_SAP_BOM] (
    [ID]                 INT             IDENTITY (1, 1) NOT NULL,
    [ERROR_MSG]          NVARCHAR (MAX)  NULL,
    [BATCH_NO]           NVARCHAR (20)   NULL,
    [CPID]               NVARCHAR (600)  NULL,
    [WERK]               NVARCHAR (600)  NULL,
    [UMFANG]             NVARCHAR (600)  NULL,
    [TAKT]               NVARCHAR (600)  NULL,
    [SGJL]               NVARCHAR (600)  NULL,
    [JSBS]               NVARCHAR (600)  NULL,
    [ZCBS]               NVARCHAR (600)  NULL,
    [CGFS]               NVARCHAR (600)  NULL,
    [ZCBJ]               NVARCHAR (600)  NULL,
    [ZCBJBC]             NVARCHAR (600)  NULL,
    [PART_NO]            NVARCHAR (600)  NULL,
    [PART_NAME]          NVARCHAR (600)  NULL,
    [PART_CNAME]         NVARCHAR (600)  NULL,
    [SB]                 NVARCHAR (600)  NULL,
    [YSMC]               NVARCHAR (600)  NULL,
    [SX]                 NVARCHAR (1000) NULL,
    [MODEL_YEAR]         NVARCHAR (600)  NULL,
    [PR_NUM]             NVARCHAR (700)  NULL,
    [JB]                 NVARCHAR (600)  NULL,
    [SL]                 NVARCHAR (600)  NULL,
    [DW]                 NVARCHAR (600)  NULL,
    [TGNTCH]             NVARCHAR (600)  NULL,
    [TGNTCRQ_STRING]     NVARCHAR (600)  NULL,
    [TGNTCRQ]            DATETIME        NULL,
    [TGNQXH]             NVARCHAR (50)   NULL,
    [TGNQXRQ_STRING]     NVARCHAR (600)  NULL,
    [TGNQXRQ]            DATETIME        NULL,
    [ESTCH]              NVARCHAR (50)   NULL,
    [ESTCRQ_STRING]      NVARCHAR (600)  NULL,
    [ESTCRQ]             DATETIME        NULL,
    [ESQXH]              NVARCHAR (50)   NULL,
    [ESQXRQ_STRING]      NVARCHAR (600)  NULL,
    [ESQXRQ]             DATETIME        NULL,
    [TGN_INFO_REST]      NVARCHAR (600)  NULL,
    [ZP]                 NVARCHAR (600)  NULL,
    [AQJ]                NVARCHAR (600)  NULL,
    [ZSJ]                NVARCHAR (600)  NULL,
    [SGJLPC]             NVARCHAR (600)  NULL,
    [GR6E]               NVARCHAR (600)  NULL,
    [BZ1]                NVARCHAR (600)  NULL,
    [BZ2]                NVARCHAR (600)  NULL,
    [BZ3]                NVARCHAR (600)  NULL,
    [BZ4]                NVARCHAR (600)  NULL,
    [AMOUNTRATIO_STRING] NVARCHAR (600)  NULL,
    [AMOUNTRATIO]        INT             NULL,
    [KTHLJ]              NVARCHAR (600)  NULL,
    [SYGC]               NVARCHAR (600)  NULL,
    [JFGC]               NVARCHAR (600)  NULL,
    [LH]                 NVARCHAR (600)  NULL,
    [VWS]                NVARCHAR (600)  NULL,
    [SAP_CODE]           NVARCHAR (600)  NULL,
    [GYS51]              NVARCHAR (600)  NULL,
    [GYSZWM1]            NVARCHAR (600)  NULL,
    [KJXYKS1_STRING]     NVARCHAR (600)  NULL,
    [KJXYKS1]            DATETIME        NULL,
    [KJXYJS1_STRING]     NVARCHAR (600)  NULL,
    [KJXYJS1]            DATETIME        NULL,
    [XYPEID1]            NVARCHAR (600)  NULL,
    [PE1]                NVARCHAR (600)  NULL,
    [CGKS1]              NVARCHAR (600)  NULL,
    [ISCROSSSTOCK1]      NVARCHAR (600)  NULL,
    [GYS52]              NVARCHAR (600)  NULL,
    [GYSZWM2]            NVARCHAR (600)  NULL,
    [KJXYKS2_STRING]     NVARCHAR (600)  NULL,
    [KJXYKS2]            DATETIME        NULL,
    [KJXYJS2_STRING]     NVARCHAR (600)  NULL,
    [KJXYJS2]            DATETIME        NULL,
    [PE2]                NVARCHAR (600)  NULL,
    [XYPEID2]            NVARCHAR (600)  NULL,
    [CGKS2]              NVARCHAR (600)  NULL,
    [ISCROSSSTOCK2]      NVARCHAR (600)  NULL,
    [ZYZ]                NVARCHAR (600)  NULL,
    [CBZ]                NVARCHAR (600)  NULL,
    [PT]                 NVARCHAR (600)  NULL,
    [LOGICAL_PK]         NVARCHAR (50)   NULL,
    [PBOM_DATE]          DATETIME        NULL,
    [CREATE_DATE]        DATETIME        CONSTRAINT [DF__TE_EP_SAP__CREAT__6991A7CB] DEFAULT (getdate()) NOT NULL,
    [VALID_FLAG]         BIT             NULL,
    [CHANGE_FLAG]        INT             NULL,
    [CHANGE_DATE]        DATETIME        NULL,
    CONSTRAINT [IDX_PK_EP_PBOM_ID] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'CHANGE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？变更标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'CHANGE_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_启用标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'PBOM_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？业务主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'LOGICAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'PT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'CBZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'ZYZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'ISCROSSSTOCK2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'CGKS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'XYPEID2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'PE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'KJXYJS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'KJXYJS2_STRING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'KJXYKS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'KJXYKS2_STRING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'GYSZWM2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'GYS52';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'ISCROSSSTOCK1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'CGKS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'PE1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'XYPEID1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'KJXYJS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'KJXYJS1_STRING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'KJXYKS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'KJXYKS1_STRING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'GYSZWM1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'GYS51';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'SAP_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？VWS', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'VWS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'LH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'JFGC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'SYGC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'KTHLJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？用量比例', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'AMOUNTRATIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'AMOUNTRATIO_STRING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'BZ4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'BZ3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'BZ2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'BZ1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'GR6E';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'SGJLPC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'ZSJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'AQJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'ZP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'TGN_INFO_REST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'ESQXRQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'ESQXRQ_STRING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'ESQXH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'ESTCRQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'ESTCRQ_STRING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'ESTCH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'TGNQXRQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'TGNQXRQ_STRING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'TGNQXH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'TGNTCRQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'TGNTCRQ_STRING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'TGNTCH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'DW';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'SL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'JB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'PR_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？年型年', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'MODEL_YEAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'SX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'YSMC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'SB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_零件名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'PART_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'ZCBJBC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'ZCBJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'CGFS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'ZCBS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'JSBS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'SGJL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'TAKT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？umfang', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'UMFANG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'WERK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'CPID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？批号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'BATCH_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？错误信息', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'ERROR_MSG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_EP_SAP_BOM', @level2type = N'COLUMN', @level2name = N'ID';

