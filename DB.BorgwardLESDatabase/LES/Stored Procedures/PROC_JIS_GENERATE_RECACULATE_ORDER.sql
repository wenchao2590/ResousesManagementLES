

/********************************************************************************
*规则生成主数据
*********************************************************************************/
CREATE PROCEDURE [LES].[PROC_JIS_GENERATE_RECACULATE_ORDER]
AS
BEGIN	

	truncate table LES.TE_JIS_RECACULATE_ORDER
begin try
begin transaction

	--插入明细中的主表
	INSERT INTO [LES].TE_JIS_RECACULATE_ORDER
     SELECT distinct [SIGNATURE]
  FROM [LES].[TT_BAS_PULL_ORDERS] A where -- A.WERK!='C9' and 
  not exists(select  [SIGNATURE] from LES.TT_ODS_INIT_PREVIEW_DATA B where A.[SIGNATURE]=b.[SIGNATURE])
  
  INSERT INTO [LES].[TE_ODS_MANUAL_RECACULATE_ORDER]
           ([ORDER_NO]
           ,[WERK]
           ,[KNR]
           ,[RECALCULATE_FLAG]
           ,[CREATE_USER]
           ,[CREATE_DATE])
	select min([ORDER_NO])
           ,min([WERK])
           ,min([KNR]),1,'auto Service',getdate()    
           from [LES].[TT_BAS_PULL_ORDERS] A
           where exists(select [ORDER_NO] from  LES.TE_JIS_RECACULATE_ORDER  B where A.[SIGNATURE]=b.[SIGNATURE])  
           --modify by 【运维】xhm 【CMDB编号：CR-LES-20140605】 2014/6/6 start
           --and not exists(select  KNR from [LES].[TE_ODS_MANUAL_RECACULATE_ORDER] C where A.[ORDER_NO]=C.[ORDER_NO]) 
           and not exists(select  KNR from [LES].[TE_ODS_MANUAL_RECACULATE_ORDER] C where A.[SIGNATURE]=C.[SIGNATURE]) 
           --modify by 【运维】xhm  end
           group by [signature]   
			having min([KNR]) is not null 		 
	
commit transaction

return
end try

begin catch
--出错，则返回执行不成功，回滚事务
rollback transaction
--记录错误信息
insert into [LES].[TS_SYS_EXCEPTION] (time_stamp, [application], [METHOD], class,  exception_message, error_code)
select getdate(),'JIS','PROC_JIS_GENERATE_RECACULATE_ORDER','Procedure',error_message(),ERROR_LINE()
return
end catch	
END