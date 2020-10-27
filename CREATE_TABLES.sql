/****** Object:  Schema [GJS]    Script Date: 2017-09-25 14:22:43 ******/
CREATE SCHEMA [GJS]
GO

/****** Object:  Table [GJS].[TM_BAS_CKDMK]    Script Date: 2017-09-25 14:23:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [GJS].[TM_BAS_CKDMK](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CODE] [nvarchar](8) NULL,
	[NAME] [nvarchar](64) NULL,
	[ADDR] [nvarchar](128) NULL,
	[TEL] [nvarchar](32) NULL,
	[FAX] [nvarchar](32) NULL,
	[LXR] [nvarchar](16) NULL,
	[BZ] [nvarchar](256) NULL,
	[VALID_FLAG] [bit] NULL,
	[CREATE_DATE] [datetime] NULL,
	[CREATE_USER] [nvarchar](32) NULL,
	[MODIFY_DATE] [datetime] NULL,
	[MODIFY_USER] [nvarchar](32) NULL,
 CONSTRAINT [PK_TM_BAS_CKDMK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代码' , @level0type=N'SCHEMA',@level0name=N'GJS', @level1type=N'TABLE',@level1name=N'TM_BAS_CKDMK', @level2type=N'COLUMN',@level2name=N'CODE'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'GJS', @level1type=N'TABLE',@level1name=N'TM_BAS_CKDMK', @level2type=N'COLUMN',@level2name=N'NAME'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地址' , @level0type=N'SCHEMA',@level0name=N'GJS', @level1type=N'TABLE',@level1name=N'TM_BAS_CKDMK', @level2type=N'COLUMN',@level2name=N'ADDR'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电话' , @level0type=N'SCHEMA',@level0name=N'GJS', @level1type=N'TABLE',@level1name=N'TM_BAS_CKDMK', @level2type=N'COLUMN',@level2name=N'TEL'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'传真' , @level0type=N'SCHEMA',@level0name=N'GJS', @level1type=N'TABLE',@level1name=N'TM_BAS_CKDMK', @level2type=N'COLUMN',@level2name=N'FAX'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系人' , @level0type=N'SCHEMA',@level0name=N'GJS', @level1type=N'TABLE',@level1name=N'TM_BAS_CKDMK', @level2type=N'COLUMN',@level2name=N'LXR'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'GJS', @level1type=N'TABLE',@level1name=N'TM_BAS_CKDMK', @level2type=N'COLUMN',@level2name=N'BZ'
GO



