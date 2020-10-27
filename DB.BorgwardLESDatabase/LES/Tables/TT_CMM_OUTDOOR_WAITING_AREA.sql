CREATE TABLE [LES].[TT_CMM_OUTDOOR_WAITING_AREA] (
    [WAITING_ID]      BIGINT        IDENTITY (1, 1) NOT NULL,
    [VEHICLE_NO]      NVARCHAR (50) NULL,
    [RUNSHEET_NO]     NVARCHAR (50) NULL,
    [RUNSHEET_TYPE]   INT           NULL,
    [REQUIRE_TIME]    DATETIME      NULL,
    [IS_URGENT_ORDER] INT           NULL,
    [CREATE_USER]     NVARCHAR (50) NULL,
    [CREATE_DATE]     DATETIME      NULL,
    [UPDATE_USER]     NVARCHAR (50) NULL,
    [UPDATE_DATE]     DATETIME      NULL,
    CONSTRAINT [PK_TT_CMM_OUTDOOR_WAITING_AREA] PRIMARY KEY CLUSTERED ([WAITING_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_OUTDOOR_WAITING_AREA', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新用户', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_OUTDOOR_WAITING_AREA', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_OUTDOOR_WAITING_AREA', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建用户', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_OUTDOOR_WAITING_AREA', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否紧急订单，1是，0不是', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_OUTDOOR_WAITING_AREA', @level2type = N'COLUMN', @level2name = N'IS_URGENT_ORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'规定到达时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_OUTDOOR_WAITING_AREA', @level2type = N'COLUMN', @level2name = N'REQUIRE_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'订单类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_OUTDOOR_WAITING_AREA', @level2type = N'COLUMN', @level2name = N'RUNSHEET_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'订单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_OUTDOOR_WAITING_AREA', @level2type = N'COLUMN', @level2name = N'RUNSHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车牌号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_OUTDOOR_WAITING_AREA', @level2type = N'COLUMN', @level2name = N'VEHICLE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_OUTDOOR_WAITING_AREA', @level2type = N'COLUMN', @level2name = N'WAITING_ID';

