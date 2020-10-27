CREATE TABLE [LES].[TI_MID_PTL_PART] (
    [ID]       INT              IDENTITY (1, 1) NOT NULL,
    [FID]      UNIQUEIDENTIFIER NULL,
    [PartNo]   NVARCHAR (20)    NULL,
    [PartName] NVARCHAR (100)   NULL,
    [OpType]   INT              NULL,
    [BoxPart]  NVARCHAR (16)    NULL,
    CONSTRAINT [PK_TT_SYS_MIDDLE_PART_INFO] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件所属区域', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_PTL_PART', @level2type = N'COLUMN', @level2name = N'BoxPart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'操作类型 1新增；2修改；3删除', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_PTL_PART', @level2type = N'COLUMN', @level2name = N'OpType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_PTL_PART', @level2type = N'COLUMN', @level2name = N'PartNo';

