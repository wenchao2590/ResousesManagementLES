CREATE TABLE [dbo].[Test_UserInfo] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (50) NULL,
    [CreateDate] DATETIME      CONSTRAINT [DF_Test_UserInfo_CreateDate] DEFAULT (getdate()) NOT NULL
);

