
CREATE TABLE [dbo].[TS_SYS_HANDLER](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[FID] [uniqueidentifier] NULL,
	[AJAX_METHOD_NAME] [nvarchar](128) NULL,
	[ASSEMBLY_NAME] [nvarchar](128) NULL,
	[CLASS_NAME] [nvarchar](128) NULL,
	[SERVER_METHOD_NAME] [nvarchar](128) NULL,
	[VALID_FLAG] [bit] NULL,
	[CREATE_DATE] [datetime] NULL,
	[CREATE_USER] [nvarchar](32) NULL,
	[MODIFY_DATE] [datetime] NULL,
	[MODIFY_USER] [nvarchar](32) NULL,
	[COMMENTS] [nvarchar](512) NULL,
 CONSTRAINT [PK_TS_SYS_HANDLER] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户端函数名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TS_SYS_HANDLER', @level2type=N'COLUMN',@level2name=N'AJAX_METHOD_NAME'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'集合名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TS_SYS_HANDLER', @level2type=N'COLUMN',@level2name=N'ASSEMBLY_NAME'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TS_SYS_HANDLER', @level2type=N'COLUMN',@level2name=N'CLASS_NAME'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务端函数名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TS_SYS_HANDLER', @level2type=N'COLUMN',@level2name=N'SERVER_METHOD_NAME'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TS_SYS_HANDLER', @level2type=N'COLUMN',@level2name=N'COMMENTS'
GO

alter table dbo.TS_SYS_ENTITY add TAB_TITLES nvarchar(512)
alter table dbo.TS_SYS_ENTITY_FIELD add TAB_TITLE_CODE nvarchar(128)