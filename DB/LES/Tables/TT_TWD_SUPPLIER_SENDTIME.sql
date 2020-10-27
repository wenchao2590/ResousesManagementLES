CREATE TABLE [LES].[TT_TWD_SUPPLIER_SENDTIME] (
    [SUPPLIER_SEND_TIME_SN] INT            IDENTITY (1, 1) NOT NULL,
    [WORKSHOP]              NVARCHAR (4)   NULL,
    [PLANT]                 NVARCHAR (5)   NOT NULL,
    [PLANT_ZONE]            NVARCHAR (5)   NULL,
    [ASSEMBLY_LINE]         NVARCHAR (10)  NOT NULL,
    [SUPPLIER_NUM]          NVARCHAR (12)  NOT NULL,
    [PART_TYPE]             INT            NOT NULL,
    [BOX_PARTS]             VARCHAR (10)   NOT NULL,
    [WORK_DAY]              DATETIME       NULL,
    [SEND_TIME]             DATETIME       NOT NULL,
    [WINDOW_TIME]           DATETIME       NOT NULL,
    [LAST_SEND_TIME]        DATETIME       NULL,
    [SEND_TIME_STATUS]      INT            NULL,
    [IS_OVERRIDE]           INT            NULL,
    [TIME_TYPE]             INT            NULL,
    [COMMENTS]              NVARCHAR (200) NULL,
    [UPDATE_DATE]           DATETIME       NULL,
    [UPDATE_USER]           NVARCHAR (50)  NULL,
    [CREATE_DATE]           DATETIME       NOT NULL,
    [CREATE_USER]           NVARCHAR (50)  NOT NULL,
    [TIME_AND]              NVARCHAR (16)  NULL,
    CONSTRAINT [IDX_PK_SUPPLIER_SENDTIME_SUPPLIER_SEND_TIME_SN] PRIMARY KEY CLUSTERED ([SUPPLIER_SEND_TIME_SN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'时区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'TIME_AND';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'时间类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'TIME_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否保留', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'IS_OVERRIDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发单状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'SEND_TIME_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最后发单时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'LAST_SEND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'窗口时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'WINDOW_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发单时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'SEND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工作日', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'WORK_DAY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TWD 零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'PART_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商窗口时间序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'SUPPLIER_SEND_TIME_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'业务_TWD 窗口时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_SUPPLIER_SENDTIME';

