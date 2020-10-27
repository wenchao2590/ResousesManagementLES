CREATE TABLE [dbo].[TS_SYS_CODE_ITEM] (
    [ID]            BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]           UNIQUEIDENTIFIER NULL,
    [CODE_FID]      UNIQUEIDENTIFIER NULL,
    [ITEM_VALUE]    INT              NULL,
    [ITEM_NAME]     NVARCHAR (32)    NULL,
    [PARENT_FID]    UNIQUEIDENTIFIER NULL,
    [DISPLAY_ORDER] INT              NULL,
    [COMMENTS]      NVARCHAR (512)   NULL,
    [VALID_FLAG]    BIT              NULL,
    [CREATE_USER]   NVARCHAR (32)    NULL,
    [CREATE_DATE]   DATETIME         NULL,
    [MODIFY_USER]   NVARCHAR (32)    NULL,
    [MODIFY_DATE]   DATETIME         NULL,
    CONSTRAINT [PK__TS_SYS_C__3214EC2702E6DA11] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CODE_ITEM', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'顺序', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CODE_ITEM', @level2type = N'COLUMN', @level2name = N'DISPLAY_ORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'显示', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CODE_ITEM', @level2type = N'COLUMN', @level2name = N'ITEM_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CODE_ITEM', @level2type = N'COLUMN', @level2name = N'ITEM_VALUE';

