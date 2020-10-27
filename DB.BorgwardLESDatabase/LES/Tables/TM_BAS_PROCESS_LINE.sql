CREATE TABLE [LES].[TM_BAS_PROCESS_LINE] (
    [PROCESS_LINE_SN]          INT            IDENTITY (1, 1) NOT NULL,
    [PLANT]                    NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]            NVARCHAR (10)  NULL,
    [PROCESS_LINE_NO]          NVARCHAR (20)  NULL,
    [MODEL]                    NVARCHAR (10)  NULL,
    [BS_PLANT]                 NVARCHAR (5)   NULL,
    [BS_ASSEMBLY_LINE]         NVARCHAR (10)  NULL,
    [PS_PLANT]                 NVARCHAR (5)   NULL,
    [PS_ASSEMBLY_LINE]         NVARCHAR (10)  NULL,
    [GA_PLANT]                 NVARCHAR (5)   NULL,
    [GA_ASSEMBLY_LINE]         NVARCHAR (10)  NULL,
    [STATUS1]                  NVARCHAR (30)  NULL,
    [STATUS2]                  NVARCHAR (30)  NULL,
    [SK]                       NVARCHAR (50)  NULL,
    [PROCESS_LINE_DESCRIPTION] NVARCHAR (200) NULL,
    [COMMENTS]                 NVARCHAR (200) NULL,
    [CREATE_USER]              NVARCHAR (50)  NULL,
    [CREATE_DATE]              DATETIME       NULL,
    [UPDATE_USER]              NVARCHAR (50)  NULL,
    [UPDATE_DATE]              DATETIME       NULL,
    CONSTRAINT [IDX_PK_PROCESS_LINE_PROCESS_LINE_SN] PRIMARY KEY CLUSTERED ([PROCESS_LINE_SN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PROCESS_LINE', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PROCESS_LINE', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PROCESS_LINE', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PROCESS_LINE', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PROCESS_LINE', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工艺路线描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PROCESS_LINE', @level2type = N'COLUMN', @level2name = N'PROCESS_LINE_DESCRIPTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PROCESS_LINE', @level2type = N'COLUMN', @level2name = N'SK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态2', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PROCESS_LINE', @level2type = N'COLUMN', @level2name = N'STATUS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态1', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PROCESS_LINE', @level2type = N'COLUMN', @level2name = N'STATUS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'GA流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PROCESS_LINE', @level2type = N'COLUMN', @level2name = N'GA_ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'GA工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PROCESS_LINE', @level2type = N'COLUMN', @level2name = N'GA_PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PS流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PROCESS_LINE', @level2type = N'COLUMN', @level2name = N'PS_ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PS工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PROCESS_LINE', @level2type = N'COLUMN', @level2name = N'PS_PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'BS流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PROCESS_LINE', @level2type = N'COLUMN', @level2name = N'BS_ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'BS工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PROCESS_LINE', @level2type = N'COLUMN', @level2name = N'BS_PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_车型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PROCESS_LINE', @level2type = N'COLUMN', @level2name = N'MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工艺路线编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PROCESS_LINE', @level2type = N'COLUMN', @level2name = N'PROCESS_LINE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PROCESS_LINE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PROCESS_LINE', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工艺路线流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PROCESS_LINE', @level2type = N'COLUMN', @level2name = N'PROCESS_LINE_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_FIS 工艺路线定义', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PROCESS_LINE';

