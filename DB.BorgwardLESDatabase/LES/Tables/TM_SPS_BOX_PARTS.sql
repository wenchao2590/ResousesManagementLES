CREATE TABLE [LES].[TM_SPS_BOX_PARTS] (
    [PLANT]              NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]      NVARCHAR (10)  NOT NULL,
    [PLANT_ZONE]         NVARCHAR (5)   NULL,
    [WORKSHOP]           NVARCHAR (4)   NULL,
    [BOX_PARTS]          NVARCHAR (10)  NOT NULL,
    [BOX_PARTS_NAME]     NVARCHAR (100) NULL,
    [SUPPLIER_NUM]       NVARCHAR (12)  NOT NULL,
    [DOCK]               NVARCHAR (10)  NOT NULL,
    [ROUTE]              NVARCHAR (10)  NULL,
    [DCP_POINT]          NVARCHAR (15)  NULL,
    [BOX_PARTS_STATE]    INT            NULL,
    [TRANS_SUPPLIER_NUM] NVARCHAR (20)  NULL,
    [TRANSPORT_TIME]     INT            NULL,
    [LOAD_TIME]          INT            NULL,
    [DELAY_TIME]         INT            NULL,
    [UNLOAD_TIME]        INT            NULL,
    [WM_NO]              NVARCHAR (10)  NULL,
    [ZONE_NO]            NVARCHAR (20)  NULL,
    [S_WM_NO]            NVARCHAR (10)  NULL,
    [S_ZONE_NO]          NVARCHAR (20)  NULL,
    [COMMENTS]           NVARCHAR (200) NULL,
    [CREATE_USER]        NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]        DATETIME       NOT NULL,
    [UPDATE_USER]        NVARCHAR (50)  NULL,
    [UPDATE_DATE]        DATETIME       NULL,
    CONSTRAINT [PK_TM_SPS_BOX_PARTS] PRIMARY KEY CLUSTERED ([PLANT] ASC, [ASSEMBLY_LINE] ASC, [BOX_PARTS] ASC, [SUPPLIER_NUM] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TM_SPS_BOX_PARTS]
    ON [LES].[TM_SPS_BOX_PARTS]([BOX_PARTS_STATE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPS_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPS_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPS_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPS_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPS_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动存储区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPS_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'S_ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPS_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'S_WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'目标存储区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPS_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'目标仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPS_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'卸货时间（分）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPS_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'UNLOAD_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'延迟时间（分）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPS_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'DELAY_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'装货时间（分）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPS_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'LOAD_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'运输时间（分）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPS_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'TRANSPORT_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'承运商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPS_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态（1-活动，2-测试，3-停用）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPS_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'BOX_PARTS_STATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'MES扫描点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPS_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'DCP_POINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'送货路径', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPS_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPS_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPS_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件类名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPS_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'BOX_PARTS_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPS_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPS_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPS_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPS_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SPS_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'PLANT';

