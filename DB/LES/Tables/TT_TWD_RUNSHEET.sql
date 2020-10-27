CREATE TABLE [LES].[TT_TWD_RUNSHEET] (
    [TWD_RUNSHEET_SN]       INT            IDENTITY (1, 1) NOT NULL,
    [TWD_RUNSHEET_NO]       VARCHAR (22)   NOT NULL,
    [PLANT]                 NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]         NVARCHAR (10)  NOT NULL,
    [WORKSHOP]              NVARCHAR (4)   NULL,
    [PLANT_ZONE]            NVARCHAR (10)  NULL,
    [PUBLISH_TIME]          DATETIME       NOT NULL,
    [RUNSHEET_TYPE]         INT            NOT NULL,
    [SUPPLIER_NUM]          NVARCHAR (12)  NOT NULL,
    [SUPPLIER_SN]           INT            NOT NULL,
    [DOCK]                  NVARCHAR (10)  NULL,
    [DELIVERY_LOCATION]     NVARCHAR (50)  NULL,
    [BOX_PARTS]             NVARCHAR (10)  NOT NULL,
    [PART_TYPE]             INT            NULL,
    [UNLOADING_TIME]        INT            NULL,
    [EXPECTED_ARRIVAL_TIME] DATETIME       NOT NULL,
    [SUGGEST_DELIVERY_TIME] DATETIME       NULL,
    [ACTUAL_ARRIVAL_TIME]   DATETIME       NULL,
    [VERIFY_TIME]           DATETIME       NULL,
    [REJECT_REASON]         NVARCHAR (200) NULL,
    [TRANS_SUPPLIER_NUM]    NVARCHAR (20)  NULL,
    [FEEDBACK]              NVARCHAR (100) NULL,
    [SHEET_STATUS]          INT            NOT NULL,
    [SEND_TIME]             DATETIME       NULL,
    [SEND_STATUS]           INT            NULL,
    [OPERATON_USER]         NVARCHAR (10)  NULL,
    [CHECK_USER]            NVARCHAR (10)  NULL,
    [RETRY_TIMES]           INT            NULL,
    [SUPPLY_TIME]           DATETIME       NULL,
    [SUPPLY_STATUS]         INT            NULL,
    [FAX_TIME]              DATETIME       NULL,
    [FAX_STATUS]            INT            NULL,
    [SAP_FLAG]              INT            NOT NULL,
    [SAP_FLAG2]             INT            NOT NULL,
    [RECKONING_NO]          NVARCHAR (30)  NULL,
    [WMS_SEND_TIME]         DATETIME       NULL,
    [WMS_SEND_STATUS]       INT            NULL,
    [COMMENTS]              NVARCHAR (200) NULL,
    [UPDATE_DATE]           DATETIME       NULL,
    [UPDATE_USER]           NVARCHAR (50)  NULL,
    [CREATE_DATE]           DATETIME       NOT NULL,
    [CREATE_USER]           NVARCHAR (50)  NOT NULL,
    [GENERATE_TYPE]         INT            NULL,
    [IS_GENERATE_REC]       INT            NULL,
    [PRINT_TIMES]           INT            DEFAULT ((0)) NOT NULL,
    [PRINT_STATE]           INT            DEFAULT ((0)) NOT NULL,
    [INHOUSE_SYSTEM_MODE]   NVARCHAR (10)  NULL,
    [IS_ASN]                INT            CONSTRAINT [DF_TT_TWD_RUNSHEET_IS_ASN] DEFAULT ((0)) NULL,
    [IS_TRAY]               INT            CONSTRAINT [DF_TT_TWD_RUNSHEET_IS_TRAY] DEFAULT ((0)) NULL,
    [TIME_AND]              NVARCHAR (16)  NULL,
    CONSTRAINT [IDX_PK_RUNSHEET_TWD_RUNSHEET_SN] PRIMARY KEY CLUSTERED ([TWD_RUNSHEET_SN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'时区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET', @level2type = N'COLUMN', @level2name = N'TIME_AND';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否按套组托', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET', @level2type = N'COLUMN', @level2name = N'IS_TRAY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否ASN', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET', @level2type = N'COLUMN', @level2name = N'IS_ASN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否生成送货单', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET', @level2type = N'COLUMN', @level2name = N'TWD_RUNSHEET_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'业务_TWD 拉动单', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET';

