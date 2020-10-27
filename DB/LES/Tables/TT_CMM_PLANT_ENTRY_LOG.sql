CREATE TABLE [LES].[TT_CMM_PLANT_ENTRY_LOG] (
    [ID]                   BIGINT        IDENTITY (1, 1) NOT NULL,
    [VEHICLE_NO]           NVARCHAR (50) NULL,
    [RUNSHEET_NO]          NVARCHAR (50) NULL,
    [IS_URGENT_ORDER]      INT           NULL,
    [REQUIRE_TIME]         DATETIME      NULL,
    [ARRIVE_TIME]          INT           NULL,
    [EXPIRE_TIME]          INT           NULL,
    [ENTRY_TIME]           DATETIME      NULL,
    [DOCK_RELEASE_TIME]    DATETIME      NULL,
    [DOCK_HOLD_TIME]       DATETIME      NULL,
    [EXIT_TIME]            DATETIME      NULL,
    [WAITING_TIME]         INT           NULL,
    [DOCK_PROCESSING_TIME] INT           NULL,
    [DOCK]                 NVARCHAR (10) NULL,
    [CREATE_USER]          NVARCHAR (50) NULL,
    [CREATE_DATE]          DATETIME      NULL,
    [UPDATE_USER]          NVARCHAR (50) NULL,
    [UPDATE_DATE]          DATETIME      NULL,
    [PHONE_NO]             NVARCHAR (50) NULL,
    [STATUS]               INT           NULL,
    [IS_JUMP_QUEUE]        INT           NULL,
    CONSTRAINT [PK_TT_CMM_PLANT_ENTRY_LOG] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否插队，1是，0不是', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_PLANT_ENTRY_LOG', @level2type = N'COLUMN', @level2name = N'IS_JUMP_QUEUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'入厂日志状态，0在厂外等待区，1在场内等待区，2在道口卸货，3出厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_PLANT_ENTRY_LOG', @level2type = N'COLUMN', @level2name = N'STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'司机电话号码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_PLANT_ENTRY_LOG', @level2type = N'COLUMN', @level2name = N'PHONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_PLANT_ENTRY_LOG', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新用户', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_PLANT_ENTRY_LOG', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'实际到达时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_PLANT_ENTRY_LOG', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建用户', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_PLANT_ENTRY_LOG', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'道口编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_PLANT_ENTRY_LOG', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'实际卸货时间分钟数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_PLANT_ENTRY_LOG', @level2type = N'COLUMN', @level2name = N'DOCK_PROCESSING_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'等待时间分钟数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_PLANT_ENTRY_LOG', @level2type = N'COLUMN', @level2name = N'WAITING_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'出厂时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_PLANT_ENTRY_LOG', @level2type = N'COLUMN', @level2name = N'EXIT_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'道口占用时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_PLANT_ENTRY_LOG', @level2type = N'COLUMN', @level2name = N'DOCK_HOLD_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'道口释放时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_PLANT_ENTRY_LOG', @level2type = N'COLUMN', @level2name = N'DOCK_RELEASE_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'入厂时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_PLANT_ENTRY_LOG', @level2type = N'COLUMN', @level2name = N'ENTRY_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'超时时间分钟数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_PLANT_ENTRY_LOG', @level2type = N'COLUMN', @level2name = N'EXPIRE_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'提前时间分钟数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_PLANT_ENTRY_LOG', @level2type = N'COLUMN', @level2name = N'ARRIVE_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'规定到达时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_PLANT_ENTRY_LOG', @level2type = N'COLUMN', @level2name = N'REQUIRE_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否紧急订单，1是，0不是', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_PLANT_ENTRY_LOG', @level2type = N'COLUMN', @level2name = N'IS_URGENT_ORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'订单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_PLANT_ENTRY_LOG', @level2type = N'COLUMN', @level2name = N'RUNSHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车牌号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_PLANT_ENTRY_LOG', @level2type = N'COLUMN', @level2name = N'VEHICLE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_PLANT_ENTRY_LOG', @level2type = N'COLUMN', @level2name = N'ID';

