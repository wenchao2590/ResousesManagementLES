
CREATE PROCEDURE [LES].[PROC_JIS_GENERATE_SERVICE_SUPPLY]
(
	@Jis_Runsheet_Sn		INT,					--拉动单序号
	@SUPPLIER_NUM			NVARCHAR(8),			--供应商
	@Rack					varchar(20)				--料架
)
AS
SET NOCOUNT ON

declare @plant varchar(5)					--工厂
declare @assemblyLine varchar(10)			--流水线
declare @PartCname varchar(100)				--零件名称
declare @PartNickName varchar(30)			--零件名称

INSERT INTO [LES].[TT_JIS_RUNSHEET_SEND_SUPPLY]
           ([JIS_RUNSHEET_SN]
           ,[JIS_RUNSHEET_NO]
           ,[JIS_RUNSHEET_TIME]
           ,[SUPPLIER_NUM]
           ,[RACK]
           ,[SEND_STATUS]
           ,[SEND_TIME]
           ,[FAX_STATUS]
           ,[FAX_TIME]
           ,[CREATE_DATE]
           ,[CREATE_USER])
SELECT @Jis_Runsheet_Sn
 
      ,A.[JIS_RUNSHEET_NO]
      ,A.[JIS_RUNSHEET_TIME]
      ,B.[PLANT_ZONE]
      ,A.[RACK]
      ,1
      ,null
      ,1
      ,null
      ,getdate()
      ,'Multi Supply'
    
  FROM [LES].[TT_JIS_RUNSHEET] A
  inner join [LES].[TM_JIS_SUPPLY_RACK] B
  on A.[PLANT]=b.[PLANT] And A.[ASSEMBLY_LINE]=b.[ASSEMBLY_LINE] and A.[RACK]=B.[RACK]
  where A.[JIS_RUNSHEET_SN]=@Jis_Runsheet_Sn and isnull(B.PROCESS_STATUS,0)=0




SET NOCOUNT OFF