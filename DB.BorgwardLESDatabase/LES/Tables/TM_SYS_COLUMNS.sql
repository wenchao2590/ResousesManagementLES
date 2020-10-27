CREATE TABLE [LES].[TM_SYS_COLUMNS] (
    [ID]              INT            IDENTITY (1, 1) NOT NULL,
    [DATABASE_SCHEMA] NVARCHAR (20)  NOT NULL,
    [TABLE_NAME]      NVARCHAR (200) NOT NULL,
    [COLUMN_NAME]     NVARCHAR (50)  NOT NULL,
    [ORDER]           INT            NOT NULL,
    [TYPE]            INT            NOT NULL,
    [MAX_LENGTH]      INT            NOT NULL,
    [DESIRED_LENGTH]  INT            NULL,
    [PRECISION]       INT            NOT NULL,
    [SCALE]           INT            NOT NULL,
    [IS_NULLABLE]     BIT            NOT NULL,
    [IS_IDENTITY]     BIT            NOT NULL,
    [COLUMN_CNAME]    NVARCHAR (50)  NULL,
    CONSTRAINT [IDX_PK_COLUMNS_ID] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COLUMN_CNAME', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_COLUMNS', @level2type = N'COLUMN', @level2name = N'COLUMN_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IS_IDENTITY', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_COLUMNS', @level2type = N'COLUMN', @level2name = N'IS_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IS_NULLABLE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_COLUMNS', @level2type = N'COLUMN', @level2name = N'IS_NULLABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SCALE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_COLUMNS', @level2type = N'COLUMN', @level2name = N'SCALE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PRECISION', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_COLUMNS', @level2type = N'COLUMN', @level2name = N'PRECISION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'DESIRED_LENGTH', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_COLUMNS', @level2type = N'COLUMN', @level2name = N'DESIRED_LENGTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'MAX_LENGTH', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_COLUMNS', @level2type = N'COLUMN', @level2name = N'MAX_LENGTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TYPE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_COLUMNS', @level2type = N'COLUMN', @level2name = N'TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ORDER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_COLUMNS', @level2type = N'COLUMN', @level2name = N'ORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ICOLUMN_NAMED', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_COLUMNS', @level2type = N'COLUMN', @level2name = N'COLUMN_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TABLE_NAME', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_COLUMNS', @level2type = N'COLUMN', @level2name = N'TABLE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_COLUMNS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_系统列信息', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_COLUMNS';

