CREATE TABLE [LES].[TT_PCS_DELIVERY_SCHEDULE] (
    [SCHEDULE_IDENTITY] INT             IDENTITY (1, 1) NOT NULL,
    [PLANT]             NVARCHAR (5)    NOT NULL,
    [ASSEMBLY_LINE]     NVARCHAR (10)   NOT NULL,
    [WORKSHOP]          NVARCHAR (4)    NULL,
    [PLANT_ZONE]        NVARCHAR (5)    NULL,
    [DELIVERY_TIME]     NVARCHAR (5)    NULL,
    [SHIFT]             NVARCHAR (10)   NULL,
    [DELIVERY_DATE]     DATETIME        NULL,
    [IS_DELIVERIED]     INT             NULL,
    [BOX_PARTS]         NVARCHAR (1000) NULL,
    [WINDOWS_TIME]      NVARCHAR (5)    CONSTRAINT [DF_TT_PPS_DeliverySchedule_post_time] DEFAULT ('00:00') NOT NULL,
    [SCHEDULE_TYPE]     INT             CONSTRAINT [DF_TT_PPS_DeliverySchedule_schedule_type] DEFAULT ((1)) NOT NULL,
    [TEMPLATE_ID]       INT             NULL,
    [IS_PRINTED]        INT             NULL,
    [IS_ORGANIZE_SHEET] INT             NULL,
    [COMMENTS]          NVARCHAR (200)  NULL,
    [CREATE_USER]       NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]       DATETIME        NOT NULL,
    [UPDATE_USER]       NVARCHAR (50)   NULL,
    [UPDATE_DATE]       DATETIME        NULL,
    CONSTRAINT [IDX_PK_DELIVERY_SCHEDULE_SCHEDULE_IDENTITY] PRIMARY KEY CLUSTERED ([SCHEDULE_IDENTITY] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TT_PCS_DELIVERY_SCHEDULE]
    ON [LES].[TT_PCS_DELIVERY_SCHEDULE]([DELIVERY_DATE] ASC, [DELIVERY_TIME] ASC, [PLANT] ASC, [ASSEMBLY_LINE] ASC, [IS_DELIVERIED] ASC, [IS_ORGANIZE_SHEET] ASC, [SCHEDULE_TYPE] ASC, [TEMPLATE_ID] ASC, [SHIFT] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_DELIVERY_SCHEDULE', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_DELIVERY_SCHEDULE', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_DELIVERY_SCHEDULE', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_DELIVERY_SCHEDULE', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_DELIVERY_SCHEDULE', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '是否已经组单', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_DELIVERY_SCHEDULE', @level2type = N'COLUMN', @level2name = N'IS_ORGANIZE_SHEET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '是否已经打累计单', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_DELIVERY_SCHEDULE', @level2type = N'COLUMN', @level2name = N'IS_PRINTED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '模板编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_DELIVERY_SCHEDULE', @level2type = N'COLUMN', @level2name = N'TEMPLATE_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_DELIVERY_SCHEDULE', @level2type = N'COLUMN', @level2name = N'SCHEDULE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '窗口时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_DELIVERY_SCHEDULE', @level2type = N'COLUMN', @level2name = N'WINDOWS_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PCS 零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_DELIVERY_SCHEDULE', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '是否发送', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_DELIVERY_SCHEDULE', @level2type = N'COLUMN', @level2name = N'IS_DELIVERIED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_DELIVERY_SCHEDULE', @level2type = N'COLUMN', @level2name = N'DELIVERY_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '班次', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_DELIVERY_SCHEDULE', @level2type = N'COLUMN', @level2name = N'SHIFT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '组织拉动单时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_DELIVERY_SCHEDULE', @level2type = N'COLUMN', @level2name = N'DELIVERY_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_DELIVERY_SCHEDULE', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_DELIVERY_SCHEDULE', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_DELIVERY_SCHEDULE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_DELIVERY_SCHEDULE', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '窗口时间序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_DELIVERY_SCHEDULE', @level2type = N'COLUMN', @level2name = N'SCHEDULE_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_PCS 窗口时间维护', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_DELIVERY_SCHEDULE';

