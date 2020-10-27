CREATE TABLE [dbo].[T_DeductPayApply] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL,
    [OrderId]        INT             NULL,
    [OrderType]      INT             NULL,
    [PayWay]         INT             NULL,
    [ReliefAmount]   DECIMAL (18, 2) NULL,
    [RepayAmount]    DECIMAL (18, 2) NULL,
    [FileId]         INT             NULL,
    [IsDeleted]      BIT             NULL,
    [CreateOptId]    INT             NULL,
    [CreateTime]     DATETIME        NULL,
    [LastOperateId]  INT             NULL,
    [LastUpdateTime] DATETIME        NULL,
    [ApprovalStatus] INT             NULL,
    [Reason]         NVARCHAR (500)  NULL,
    CONSTRAINT [PK_T_DeductPayApply] PRIMARY KEY CLUSTERED ([Id] ASC)
);

