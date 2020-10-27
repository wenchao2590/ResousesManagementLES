
CREATE PROCEDURE [LES].[PROC_TWD_GENERATE_SERVICE_SUPPLY]
(
	@TWD_Runsheet_Sn		INT,					--拉动单序号
	@SUPPLIER_NUM			NVARCHAR(8),			--供应商
	@Rack					varchar(20)				--料架
)
AS
SET NOCOUNT ON

declare @plant varchar(5)					--工厂
declare @assemblyLine varchar(10)			--流水线
declare @PartCname varchar(100)				--零件名称
declare @PartNickName varchar(30)			--零件名称

INSERT INTO [LES].[TT_TWD_RUNSHEET_SEND_SUPPLY]
           ([TWD_RUNSHEET_SN]
           ,[TWD_RUNSHEET_NO]
           ,[PUBLISH_TIME]
           ,[SUPPLIER_NUM]
           ,[BOX_PARTS]
           ,[SEND_TIME]
           ,[SEND_STATUS]
           ,[COMMENTS]
           ,[CREATE_DATE]
           ,[CREATE_USER])
SELECT [TWD_RUNSHEET_SN]
 
      ,A.[TWD_RUNSHEET_NO]
      ,A.[PUBLISH_TIME]
      ,B.[SUPPLIER_NUM]
      ,A.[BOX_PARTS]
      ,null
      ,1
      ,null
      ,getdate()
      ,'Multi Supply'
    
  FROM [LES].[TT_TWD_RUNSHEET] A
  inner join [LES].[TM_TWD_SUPPLY_BOX_PARTS] B
  on A.[PLANT]=b.[PLANT] And A.[ASSEMBLY_LINE]=b.[ASSEMBLY_LINE] and A.[BOX_PARTS]=B.[BOX_PARTS]
  where A.[TWD_RUNSHEET_SN]=@TWD_Runsheet_Sn




SET NOCOUNT OFF