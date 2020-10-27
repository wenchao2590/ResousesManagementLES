

/********************************************************************************
*规则生成主数据
*********************************************************************************/
CREATE PROCEDURE [LES].[PROC_JIS_GENERATE_ORDER_FILE]
AS
BEGIN	
begin try
	declare @supply nvarchar(200)
	declare @filePath nvarchar(200)
	select @supply=[PARAMETER9],@filePath=[PARAMETER8] FROM [LES].[TM_SYS_APPLICATION_CONFIGURATION] where [APPLICATION]='A00File'
	--truncate table [LES].[TT_JIS_UNFIT_ASSEMBLY_RULE]
	--插入明细中的主表
	INSERT INTO [LES].[TT_JIS_ORDER_FILE]
           ([PLANT]
           ,[ASSEMBLY_LINE]
           ,[SUPPLIER_NUM]
           ,[FILE_NAMES]
           ,[FILE_PATH]
           ,[CREATE_USER]
           ,[CREATE_DATE])
			SELECT distinct [PLANT]
		  ,[ASSEMBLY_LINE]
		  ,[SUPPLIER_NUM] 
		  ,replace(replace(replace(CONVERT(varchar, getdate(), 120 ),'-',''),' ',''),':','')+[PLANT]+[ASSEMBLY_LINE]+[SUPPLIER_NUM]+'.csv'
		  ,@filePath
		  ,'Oni service'
		  ,getdate()
		FROM [LES].[TM_JIS_RACK] where  CHARINDEX([SUPPLIER_NUM],@supply) > 0 
		
		select * from [LES].[TT_JIS_ORDER_FILE] where datediff(day,getdate(),[CREATE_DATE])=0
end try

begin catch
--出错，则返回执行不成功，回滚事务
rollback transaction
--记录错误信息
insert into [LES].[TS_SYS_EXCEPTION] (time_stamp, [application], [METHOD], class,  exception_message, error_code)
select getdate(),'PCS','PROC_JIS_GENERATE_ORDER_FILE','Procedure',error_message(),ERROR_LINE()
return
end catch	
END