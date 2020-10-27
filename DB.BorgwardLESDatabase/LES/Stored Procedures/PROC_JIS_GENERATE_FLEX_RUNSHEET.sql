
/********************************************************************************
*手动生成拉动单
ShenJinkui
*********************************************************************************/
CREATE PROCEDURE [LES].[PROC_JIS_GENERATE_FLEX_RUNSHEET]
(
	@WHERE nvarchar(MAX)
)
AS
BEGIN	
begin try

CREATE TABLE #TEMP_JIS_RUNSHEET_FLEX(
	[JIS_RUNSHEET_FLEX_SN] [int] NOT NULL,
	[JIS_RUNSHEET_FLEX_TIME] [datetime] NOT NULL,
	[PLANT] [nvarchar](5) NOT NULL,
	[ASSEMBLY_LINE] [nvarchar](10) NOT NULL,
	[SUPPLIER_NUM] [nvarchar](8) NOT NULL,
	[RACK] [nvarchar](20) NOT NULL,
	[BOX_NUMBER] [int] NOT NULL,
	[FORMAT] [nvarchar](6) NOT NULL,
	[MODEL] [nvarchar](10) NULL,
	[CAR_NO] [nvarchar](8) NOT NULL,
	[RUNNING_NUMBER] [nvarchar](5) NOT NULL,
	[JIS_RUNSHEET_SN] [int] NULL,
	[JIS_RUNSHEET_NO] [nvarchar](30) NULL,
	[CREATE_DATE] [datetime] NULL,
	[SAP_FLAG] [int] NULL,
	[VIN] [nvarchar](20) NULL,
	[MODEL_NO] [nvarchar](8) NULL,
	[JIS_BOX_SN] [int] NULL,
	[PROFILE1] [nvarchar](8) NULL,
	[PROFILE2] [nvarchar](8) NULL)

DECLARE @FlexSQL nvarchar(MAX)

SET @FlexSQL='INSERT INTO #TEMP_JIS_RUNSHEET_FLEX SELECT  [JIS_RUNSHEET_FLEX_SN]
      ,[JIS_RUNSHEET_FLEX_TIME]
      ,[PLANT]
      ,[ASSEMBLY_LINE]
      ,[SUPPLIER_NUM]
      ,[RACK]
      ,[BOX_NUMBER]
      ,[FORMAT]
      ,[MODEL]
      ,[CAR_NO]
      ,[RUNNING_NUMBER]
      ,[JIS_RUNSHEET_SN]
      ,[JIS_RUNSHEET_NO]
      ,[CREATE_DATE]
      ,[SAP_FLAG]
      ,[VIN]
      ,[MODEL_NO]
      ,[JIS_BOX_SN]
      ,[PROFILE1]
      ,[PROFILE2]   FROM [LES].[TT_JIS_RUNSHEET_FLEX] 
      WHERE 1=1 '+@WHERE

exec(@FlexSQL)

DECLARE @Sequence  int
EXEC  @Sequence = [LES].[PROC_JIS_GET_NEXT_SEQUENCE] 'JIS_RUNSHEET_SN'

DECLARE @PLANT  nvarchar(5)
DECLARE @ASSEMBLY_LINE nvarchar(10) 
DECLARE @RACK  nvarchar(20) 
DECLARE @SUPPLIER_NUM  nvarchar(8) 
DECLARE @JIS_RUNSHEET_FLEX_TIME DATETIME
DECLARE @FORMAT nvarchar(6) 
DECLARE @START_RUNNING_NUMBER varchar(8) 
DECLARE @END_RUNNING_NUMBER varchar(8)

SELECT TOP 1 @PLANT=PLANT,
@ASSEMBLY_LINE=ASSEMBLY_LINE, 
@RACK=RACK,
@SUPPLIER_NUM=SUPPLIER_NUM,
@JIS_RUNSHEET_FLEX_TIME=JIS_RUNSHEET_FLEX_TIME,
@FORMAT=FORMAT,
@START_RUNNING_NUMBER=RUNNING_NUMBER
FROM #TEMP_JIS_RUNSHEET_FLEX
ORDER BY JIS_RUNSHEET_FLEX_TIME

DECLARE @ASSEMBLY_LINE_NICKNAME NVARCHAR(2)=''
SELECT TOP 1 @ASSEMBLY_LINE_NICKNAME= ISNULL(ASSEMBLY_LINE_NICKNAME,'') FROM LES.TM_BAS_ASSEMBLY_LINE
WHERE PLANT=@PLANT AND ASSEMBLY_LINE=@ASSEMBLY_LINE

--开始流水号
SELECT TOP 1 
@START_RUNNING_NUMBER=RUNNING_NUMBER
FROM #TEMP_JIS_RUNSHEET_FLEX
ORDER BY RUNNING_NUMBER

--结束流水号
SELECT TOP 1 
@END_RUNNING_NUMBER=RUNNING_NUMBER
FROM #TEMP_JIS_RUNSHEET_FLEX
ORDER BY RUNNING_NUMBER DESC

DECLARE @DELIVERY_TIME int
DECLARE @UNLOADING_TIME int
DECLARE @PRINT_TYPE  varchar(2)
DECLARE @DOCK  varchar(10)
DECLARE @LOCATION  nvarchar(20)

SELECT @DELIVERY_TIME=DELIVERY_TIME,
@UNLOADING_TIME=UNLOADING_TIME ,
@PRINT_TYPE=PRINT_TYPE,
@DOCK=DOCK,
@LOCATION=LOCATION
FROM [LES].[TM_JIS_RACK] WHERE PLANT=@PLANT AND ASSEMBLY_LINE=@ASSEMBLY_LINE AND RACK=@RACK

DECLARE @JIS_RUNSHEET_NO  varchar(30)
SET @JIS_RUNSHEET_NO=@PLANT
	+ @ASSEMBLY_LINE_NICKNAME--SUBSTRING(@ASSEMBLY_LINE,1,2)
	+@SUPPLIER_NUM+'1'
	+CONVERT(varchar(1),DATEPART(yy,GETDATE())%10)
	+SUBSTRING('00000000' + CONVERT(NVARCHAR, @Sequence),LEN(CONVERT(NVARCHAR, @Sequence))+1,8)

IF @JIS_RUNSHEET_NO IS NOT NULL
BEGIN

BEGIN TRANSACTION

--插入主表
INSERT INTO [LES].[TT_JIS_RUNSHEET]
           ([JIS_RUNSHEET_SN]
           ,[JIS_RUNSHEET_NO]
           ,[JIS_RUNSHEET_TIME]
           ,[PLANT]
           ,[ASSEMBLY_LINE]
           ,[RACK]
           ,[SUPPLIER_NUM]
           ,[WORKSHOP]
           ,[PLANT_ZONE]
           ,[LOCATION]
           ,[JIS_SUPPLIER_SN]
           ,[DOCK]
           ,[FIRST_TIME]
           ,[EXPECTED_ARRIVAL_TIME]
           ,[SUPPLIER_CONFIRM_TIME]
           ,[ESTIMATED_ARRIVAL_TIME]
           ,[ACTUAL_ARRIVAL_TIME]
           ,[PRINT_TYPE]
           ,[FORMAT]
           ,[CARS]
           ,[START_RUNNING_NO]
           ,[END_RUNNING_NO]
           ,[FEEDBACK]
           ,[BOOKKEEPER]
           ,[REDO_FLAG]
           ,[JIS_RUNSHEET_STATUS]
           ,[SEND_STATUS]
           ,[SEND_TIME]
           ,[FAX_STATUS]
           ,[FAX_TIME]
           ,[SUPPLY_STATUS]
           ,[SUPPLY_TIME]
           ,[SAP_FLAG]
           ,[RETRY_TIMES]
           ,[RECKONING_NO]
           ,[OPERATION_USER]
           ,[CHECK_USER]
           ,[TRANS_SUPPLIER_NUM]
           ,[WMS_SEND_STATUS]
           ,[WMS_SEND_TIME]
           ,[COMMENTS]
           ,[UPDATE_DATE]
           ,[UPDATE_USER]
           ,[CREATE_DATE]
           ,[CREATE_USER])
     VALUES
           (@Sequence,
           @JIS_RUNSHEET_NO,--<JIS_RUNSHEET_NO, varchar(30),>
           GETDATE(),--<JIS_RUNSHEET_TIME, datetime,>
           @PLANT,
           @ASSEMBLY_LINE,
           @RACK, 
           @SUPPLIER_NUM, 
           NULL,--<WORKSHOP, nvarchar(4),>
           NULL,--<PLANT_ZONE, nvarchar(5),>
           @LOCATION,--<LOCATION, nvarchar(20),>
           @SUPPLIER_NUM,--<JIS_SUPPLIER_SN, int,>
           ISNULL(@DOCK,''),--<DOCK, nvarchar(10),>
           @JIS_RUNSHEET_FLEX_TIME,--<FIRST_TIME, datetime,>
           DATEADD(MI,@DELIVERY_TIME,@JIS_RUNSHEET_FLEX_TIME) ,--<EXPECTED_ARRIVAL_TIME, datetime,>
           NULL,--<SUPPLIER_CONFIRM_TIME, datetime,>
           DATEADD(MI,@DELIVERY_TIME-@UNLOADING_TIME,@JIS_RUNSHEET_FLEX_TIME),--<ESTIMATED_ARRIVAL_TIME, datetime,>
           NULL,--<ACTUAL_ARRIVAL_TIME, datetime,>
           @PRINT_TYPE,--<PRINT_TYPE, varchar(2),>
           @FORMAT,--<FORMAT, varchar(6),>
           '',--<CARS, varchar(200),>
           @START_RUNNING_NUMBER,--<START_RUNNING_NO, varchar(8),>
           @END_RUNNING_NUMBER,--<END_RUNNING_NO, varchar(8),>
           NULL,--<FEEDBACK, varchar(100),>
           NULL,--<BOOKKEEPER, varchar(100),>
           0,--<REDO_FLAG, bit,>
           0,--<JIS_RUNSHEET_STATUS, int,>
           NULL,--<SEND_STATUS, int,>
           NULL,--<SEND_TIME, datetime,>
           NULL,--<FAX_STATUS, int,>
           NULL,--<FAX_TIME, datetime,>
           NULL,--<SUPPLY_STATUS, int,>
           NULL,--<SUPPLY_TIME, datetime,>
           0,--<SAP_FLAG, int,>
           NULL,--<RETRY_TIMES, int,>
           NULL,--<RECKONING_NO, nvarchar(30),>
           NULL,--<OPERATION_USER, nvarchar(10),>
           NULL,--<CHECK_USER, nvarchar(10),>
           NULL,--<TRANS_SUPPLIER_NUM, nvarchar(20),>
           NULL,--<WMS_SEND_STATUS, int,>
           NULL,--<WMS_SEND_TIME, datetime,>
           NULL,--<COMMENTS, nvarchar(200),>
           NULL,--<UPDATE_DATE, datetime,>
           NULL,--<UPDATE_USER, nvarchar(50),>
           GETDATE(),--<CREATE_DATE, datetime,>
           'PROC_JIS_GENERATE_FLEX_RUNSHEET'--<CREATE_USER, nvarchar(50),>
           )

--更新FLEX拉动单号
DECLARE @UPDATE_FlexSQL nvarchar(MAX)

SET @UPDATE_FlexSQL='UPDATE  [LES].[TT_JIS_RUNSHEET_FLEX]
SET [JIS_RUNSHEET_SN]='+CONVERT(NVARCHAR,@Sequence)+',
[JIS_RUNSHEET_NO]='''+@JIS_RUNSHEET_NO+'''
 WHERE 1=1 '+@WHERE
 
exec(@UPDATE_FlexSQL)

--更新明显

UPDATE	LES.TT_JIS_RUNSHEET_DETAIL
SET		ORDER_NO = B.AGREEMENT_NO, ITEM_NO = B.PROJECT
FROM	LES.TT_JIS_RUNSHEET_DETAIL A, LES.TI_BAS_SUPPLIER_SOURCE_LIST B
WHERE	JIS_RUNSHEET_FLEX_SN IN ( SELECT JIS_RUNSHEET_FLEX_SN  FROM #TEMP_JIS_RUNSHEET_FLEX)
AND		A.PART_NO = B.PART_NO
AND		B.PLANT = @PLANT 
AND		B.SUPPLIER_NUM = @SUPPLIER_NUM
--modify by【运维】hx 2014/04/01【CMDB编号：CR-LES-20140402】start
--修改起始有效期和结束有效期只比较日期
AND		CONVERT(DATETIME,CONVERT(VARCHAR(10),GETDATE(),23)) BETWEEN START_EFFECTIVE_DATE AND END_EFFECTIVE_DATE
--modify by【运维】hx 2014/04/01 end	



commit transaction
	 
DROP TABLE 	 #TEMP_JIS_RUNSHEET_FLEX

SELECT @JIS_RUNSHEET_NO

END

end try

begin catch
--出错，则返回执行不成功，回滚事务
rollback transaction
print error_message()
--记录错误信息
insert into [LES].[TS_SYS_EXCEPTION] (time_stamp, [application], [METHOD], class,  exception_message, error_code)
select getdate(),'JIS','[PROC_JIS_GENERATE_FLEX_RUNSHEET]','Procedure',error_message(),ERROR_LINE()
return
end catch	
END