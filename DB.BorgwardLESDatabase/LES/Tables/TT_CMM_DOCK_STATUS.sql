CREATE TABLE [LES].[TT_CMM_DOCK_STATUS] (
    [PLANT]         NVARCHAR (5)  NOT NULL,
    [ASSEMBLY_LINE] NVARCHAR (10) NOT NULL,
    [DOCK]          NVARCHAR (10) NOT NULL,
    [STATUS]        INT           NULL,
    [CREATE_USER]   NVARCHAR (50) NULL,
    [CREATE_DATE]   DATETIME      NULL,
    [UPDATE_USER]   NVARCHAR (50) NULL,
    [UPDATE_DATE]   DATETIME      NULL,
    [VEHICLE_NO]    NVARCHAR (50) NULL,
    [SHEET_NO]      NVARCHAR (50) NULL,
    CONSTRAINT [PK_TT_CMM_DOCK_STATUS] PRIMARY KEY CLUSTERED ([PLANT] ASC, [ASSEMBLY_LINE] ASC, [DOCK] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'占用道口订单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_DOCK_STATUS', @level2type = N'COLUMN', @level2name = N'SHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'占用道口车牌号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_DOCK_STATUS', @level2type = N'COLUMN', @level2name = N'VEHICLE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_DOCK_STATUS', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新用户', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_DOCK_STATUS', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_DOCK_STATUS', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建用户', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_DOCK_STATUS', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'道口状态，1占用，0释放', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_DOCK_STATUS', @level2type = N'COLUMN', @level2name = N'STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'道口编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_DOCK_STATUS', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_DOCK_STATUS', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_DOCK_STATUS', @level2type = N'COLUMN', @level2name = N'PLANT';

