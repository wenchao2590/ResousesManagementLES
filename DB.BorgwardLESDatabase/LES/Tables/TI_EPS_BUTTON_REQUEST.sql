CREATE TABLE [LES].[TI_EPS_BUTTON_REQUEST] (
    [REQUEST_SN]    INT            IDENTITY (1, 1) NOT NULL,
    [ASSEMBLY_LINE] NVARCHAR (10)  NOT NULL,
    [PLANT]         NVARCHAR (5)   NOT NULL,
    [REQUEST_TIME]  DATETIME       NOT NULL,
    [REQUEST_STATE] INT            NOT NULL,
    [BUTTON_ID]     VARCHAR (20)   NOT NULL,
    [COMMENTS]      NVARCHAR (200) NULL,
    [UPDATE_DATE]   DATETIME       NULL,
    [UPDATE_USER]   NVARCHAR (50)  NULL,
    [CREATE_DATE]   DATETIME       NOT NULL,
    [CREATE_USER]   NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_TI_EPS_BUTTON_REQUEST] PRIMARY KEY CLUSTERED ([REQUEST_SN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_EPS_BUTTON_REQUEST', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_EPS_BUTTON_REQUEST', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_EPS_BUTTON_REQUEST', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_EPS_BUTTON_REQUEST', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_EPS_BUTTON_REQUEST', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '请求按钮号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_EPS_BUTTON_REQUEST', @level2type = N'COLUMN', @level2name = N'BUTTON_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '请求状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_EPS_BUTTON_REQUEST', @level2type = N'COLUMN', @level2name = N'REQUEST_STATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '请求时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_EPS_BUTTON_REQUEST', @level2type = N'COLUMN', @level2name = N'REQUEST_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_EPS_BUTTON_REQUEST', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_EPS_BUTTON_REQUEST', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '请求序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_EPS_BUTTON_REQUEST', @level2type = N'COLUMN', @level2name = N'REQUEST_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '接口_EPS 按钮请求', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_EPS_BUTTON_REQUEST';

