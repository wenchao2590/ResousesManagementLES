CREATE TABLE [LES].[TM_TWD_PART_RESUME] (
    [SEQID]                INT            NOT NULL,
    [PLANT]                NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]        NVARCHAR (10)  NULL,
    [PART_NO]              NVARCHAR (20)  NULL,
    [PART_CNAME]           NVARCHAR (100) NULL,
    [PLANT_ZONE]           NVARCHAR (5)   NULL,
    [WORKSHOP]             NVARCHAR (4)   NULL,
    [SUPPLIER_NUM]         NVARCHAR (12)  NULL,
    [RESUMENO]             NVARCHAR (10)  NULL,
    [RESUME_STATUS]        INT            NULL,
    [EXPIRE_DATE]          DATETIME       NULL,
    [START_EFFECTIVE_DATE] DATETIME       NULL,
    [WAREHOUSE]            NVARCHAR (50)  NULL,
    [PLACE_OF_DELIVERY]    NVARCHAR (100) NULL,
    [COMMENTS]             NVARCHAR (200) NULL,
    [CREATE_USER]          NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]          DATETIME       NOT NULL,
    [UPDATE_USER]          NVARCHAR (50)  NULL,
    [UPDATE_DATE]          DATETIME       NULL,
    CONSTRAINT [IDX_PK_PART_RESUME_SEQID] PRIMARY KEY CLUSTERED ([SEQID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_PART_RESUME', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_PART_RESUME', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_PART_RESUME', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_PART_RESUME', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_PART_RESUME', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发货地点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_PART_RESUME', @level2type = N'COLUMN', @level2name = N'PLACE_OF_DELIVERY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '收货地点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_PART_RESUME', @level2type = N'COLUMN', @level2name = N'WAREHOUSE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '开始生效日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_PART_RESUME', @level2type = N'COLUMN', @level2name = N'START_EFFECTIVE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '失效时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_PART_RESUME', @level2type = N'COLUMN', @level2name = N'EXPIRE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件履历状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_PART_RESUME', @level2type = N'COLUMN', @level2name = N'RESUME_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件履历号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_PART_RESUME', @level2type = N'COLUMN', @level2name = N'RESUMENO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_PART_RESUME', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_PART_RESUME', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_PART_RESUME', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_PART_RESUME', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_PART_RESUME', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_PART_RESUME', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_PART_RESUME', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_PART_RESUME', @level2type = N'COLUMN', @level2name = N'SEQID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_TWD 零件履历', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_PART_RESUME';

