USE [yw_cw]
GO

/****** Object:  Table [dbo].[hxdmk]    Script Date: 02/25/2017 12:22:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[hxdmk](
	[nid] [int] IDENTITY(1,1) NOT NULL,
	[fid] [varchar](12) NOT NULL,
	[hxdm] [varchar](4) NOT NULL,
	[hx] [varchar](20) NOT NULL,
	[sm] [varchar](50) NOT NULL,
 CONSTRAINT [PK_HXDMK] PRIMARY KEY CLUSTERED 
(
	[nid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录号,记录号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'hxdmk', @level2type=N'COLUMN',@level2name=N'nid'
GO

EXEC sys.sp_addextendedproperty @name=N'US_Description', @value=N'Record No,Record No' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'hxdmk', @level2type=N'COLUMN',@level2name=N'nid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录号,记录号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'hxdmk', @level2type=N'COLUMN',@level2name=N'fid'
GO

EXEC sys.sp_addextendedproperty @name=N'US_Description', @value=N'Record No,Record No' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'hxdmk', @level2type=N'COLUMN',@level2name=N'fid'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'代码,代码长度为2位,代码可以是字母或数字,但代码中最好不要包含空格字符.代码不要有重码.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'hxdmk', @level2type=N'COLUMN',@level2name=N'hxdm'
GO

EXEC sys.sp_addextendedproperty @name=N'US_Description', @value=N'Ocean Line Code,Ocean Line Code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'hxdmk', @level2type=N'COLUMN',@level2name=N'hxdm'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称,航线名称.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'hxdmk', @level2type=N'COLUMN',@level2name=N'hx'
GO

EXEC sys.sp_addextendedproperty @name=N'US_Description', @value=N'Ocean Line,Ocean Line' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'hxdmk', @level2type=N'COLUMN',@level2name=N'hx'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'说明,其它需要标注的情况.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'hxdmk', @level2type=N'COLUMN',@level2name=N'sm'
GO

EXEC sys.sp_addextendedproperty @name=N'US_Description', @value=N'Remark,Other Information.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'hxdmk', @level2type=N'COLUMN',@level2name=N'sm'
GO


