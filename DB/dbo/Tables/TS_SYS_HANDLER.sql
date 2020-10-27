CREATE TABLE [dbo].[TS_SYS_HANDLER] (
    [ID]                 BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]                UNIQUEIDENTIFIER NULL,
    [AJAX_METHOD_NAME]   NVARCHAR (128)   NULL,
    [ASSEMBLY_NAME]      NVARCHAR (128)   NULL,
    [CLASS_NAME]         NVARCHAR (128)   NULL,
    [SERVER_METHOD_NAME] NVARCHAR (128)   NULL,
    [VALID_FLAG]         BIT              NULL,
    [CREATE_DATE]        DATETIME         NULL,
    [CREATE_USER]        NVARCHAR (32)    NULL,
    [MODIFY_DATE]        DATETIME         NULL,
    [MODIFY_USER]        NVARCHAR (32)    NULL,
    [COMMENTS]           NVARCHAR (512)   NULL,
    CONSTRAINT [PK_TS_SYS_HANDLER] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_HANDLER', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'服务端函数名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_HANDLER', @level2type = N'COLUMN', @level2name = N'SERVER_METHOD_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'类名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_HANDLER', @level2type = N'COLUMN', @level2name = N'CLASS_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'集合名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_HANDLER', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'客户端函数名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_HANDLER', @level2type = N'COLUMN', @level2name = N'AJAX_METHOD_NAME';

