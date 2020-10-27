CREATE TABLE [dbo].[TM_BAS_COMPANY](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[FID] [uniqueidentifier] NULL,
	[OID] [uniqueidentifier] NULL,
	[C_CODE] [nvarchar](50) NULL,
	[C_NAME] [nvarchar](50) NULL,
	[COM_ID] [nvarchar](50) NULL,
	[DEPT_ID] [nvarchar](50) NULL,
	[PROVINCE] [nvarchar](50) NULL,
	[CITY] [nvarchar](50) NULL,
	[C_ADDRESS] [nvarchar](50) NULL,
	[ZIP_CODE] [nvarchar](50) NULL,
	[CREATE_DATE] [datetime] NULL,
	[CREATE_USER] [nvarchar](50) NULL,
	[MODIFY_DATE] [datetime] NULL,
	[MODIFY_USER] [nvarchar](50) NULL,
	[VALID_FLAG] [bit] NULL,
 CONSTRAINT [PK_TM_BAS_COMPANY] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO