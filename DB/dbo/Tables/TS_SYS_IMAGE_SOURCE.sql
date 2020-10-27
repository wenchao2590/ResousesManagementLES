CREATE TABLE [dbo].[TS_SYS_IMAGE_SOURCE] (
    [ID]            BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]           UNIQUEIDENTIFIER NULL,
    [IMAGE_NAME]    NVARCHAR (32)    NULL,
    [IMAGE_NAME_CN] NVARCHAR (32)    NULL,
    [IMAGE_URL]     NVARCHAR (512)   NULL,
    [RESOURCE]      IMAGE            NULL,
    [IMAGE_TYPE]    INT              NULL,
    [VALID_FLAG]    BIT              NULL,
    [CREATE_USER]   NVARCHAR (32)    NULL,
    [CREATE_DATE]   DATETIME         NULL,
    [MODIFY_USER]   NVARCHAR (32)    NULL,
    [MODIFY_DATE]   DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'图片类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_IMAGE_SOURCE', @level2type = N'COLUMN', @level2name = N'IMAGE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'图片资源', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_IMAGE_SOURCE', @level2type = N'COLUMN', @level2name = N'RESOURCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'图片地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_IMAGE_SOURCE', @level2type = N'COLUMN', @level2name = N'IMAGE_URL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'中文名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_IMAGE_SOURCE', @level2type = N'COLUMN', @level2name = N'IMAGE_NAME_CN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'图片名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_IMAGE_SOURCE', @level2type = N'COLUMN', @level2name = N'IMAGE_NAME';

