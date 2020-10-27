CREATE TABLE [LES].[TI_MID_PTL_PLAN_RECEVIE] (
    [ID]         INT              IDENTITY (1, 1) NOT NULL,
    [FID]        UNIQUEIDENTIFIER NULL,
    [VIN]        NVARCHAR (32)    NULL,
    [SeqNo]      NVARCHAR (16)    NULL,
    [PartNo]     NVARCHAR (20)    NULL,
    [PartName]   NVARCHAR (100)   NULL,
    [Qty]        DECIMAL (18, 2)  NULL,
    [BoxPart]    NVARCHAR (16)    NULL,
    [CreateTime] DATETIME         NULL,
    [OrderType]  NVARCHAR (2)     NULL,
    [OrderNo]    NVARCHAR (32)    NULL,
    [ZordNO]     NVARCHAR (36)    NULL,
    [Farbin]     NVARCHAR (30)    NULL,
    [Zcolore_D]  NVARCHAR (30)    NULL,
    [Zcolori_D]  NVARCHAR (30)    NULL,
    CONSTRAINT [PK_TT_SYS_MIDDLE_PLAN_RECEVIE] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单据类型 N单正常、P单有替换件', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_PTL_PLAN_RECEVIE', @level2type = N'COLUMN', @level2name = N'OrderType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_PTL_PLAN_RECEVIE', @level2type = N'COLUMN', @level2name = N'CreateTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件所属区域', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_PTL_PLAN_RECEVIE', @level2type = N'COLUMN', @level2name = N'BoxPart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_PTL_PLAN_RECEVIE', @level2type = N'COLUMN', @level2name = N'Qty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_PTL_PLAN_RECEVIE', @level2type = N'COLUMN', @level2name = N'PartName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_PTL_PLAN_RECEVIE', @level2type = N'COLUMN', @level2name = N'PartNo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'序列号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_PTL_PLAN_RECEVIE', @level2type = N'COLUMN', @level2name = N'SeqNo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VIN号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_PTL_PLAN_RECEVIE', @level2type = N'COLUMN', @level2name = N'VIN';

