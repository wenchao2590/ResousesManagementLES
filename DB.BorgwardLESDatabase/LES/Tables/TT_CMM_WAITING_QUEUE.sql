CREATE TABLE [LES].[TT_CMM_WAITING_QUEUE] (
    [QUEUE_ID]        BIGINT        IDENTITY (1, 1) NOT NULL,
    [VEHICLE_NO]      NVARCHAR (50) NULL,
    [RUNSHEET_NO]     NVARCHAR (50) NULL,
    [AREA_ID]         INT           NULL,
    [DOCK]            NVARCHAR (10) NULL,
    [IS_URGENT_ORDER] INT           NULL,
    [SEQUENCE]        INT           NULL,
    [CREATE_USER]     NVARCHAR (50) NULL,
    [CREATE_DATE]     DATETIME      NULL,
    [UPDATE_USER]     NVARCHAR (50) NULL,
    [UPDATE_DATE]     DATETIME      NULL,
    [IS_JUMP_QUEUE]   INT           NULL,
    CONSTRAINT [PK_TT_CMM_WAITING_QUEUE] PRIMARY KEY CLUSTERED ([QUEUE_ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_AREAID_ISJUMPQUEUE]
    ON [LES].[TT_CMM_WAITING_QUEUE]([AREA_ID] ASC, [IS_JUMP_QUEUE] ASC)
    INCLUDE([QUEUE_ID], [SEQUENCE]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否插队，1是，0不是', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_WAITING_QUEUE', @level2type = N'COLUMN', @level2name = N'IS_JUMP_QUEUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_WAITING_QUEUE', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新用户', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_WAITING_QUEUE', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_WAITING_QUEUE', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建用户', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_WAITING_QUEUE', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'顺序', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_WAITING_QUEUE', @level2type = N'COLUMN', @level2name = N'SEQUENCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否紧急订单，1是，0不是', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_WAITING_QUEUE', @level2type = N'COLUMN', @level2name = N'IS_URGENT_ORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'道口编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_WAITING_QUEUE', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'等待区ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_WAITING_QUEUE', @level2type = N'COLUMN', @level2name = N'AREA_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'订单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_WAITING_QUEUE', @level2type = N'COLUMN', @level2name = N'RUNSHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车牌号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_WAITING_QUEUE', @level2type = N'COLUMN', @level2name = N'VEHICLE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_WAITING_QUEUE', @level2type = N'COLUMN', @level2name = N'QUEUE_ID';

