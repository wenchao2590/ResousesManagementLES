-- =============================================
-- Author:		Andy Liu
-- Create date: 2015-07-03
-- Description:	SAP供应商信息下发
-- =============================================
CREATE PROC [LES].[PROC_INTERFACE_SAP_LES_SUPPLIER]
AS

begin try	
begin transaction	
    --更新现有供应商数据
	update A
	set	A.SUPPLIER_NAME = B.NAME1,
	A.SUPPLIER_ADDRESS = isnull(B.STRAS,'无'),
	A.SUPPLIER_GROUP = B.KTOKK,
	A.PROVINCE = B.REGIO,
	A.CITY = B.ORT00,
	A.CONTACT_NAME = B.NAME0,
	A.CONTACT_TEL = B.TELF0,
	A.CONTACT_EMAIL = B.SMTP_ADDR
	from LES.TM_BAS_SUPPLIER A,
	LES.TI_BAS_SUPPLIER_IN B 
	where A.SUPPLIER_NUM = B.LIFNR 
	and ISNULL(B.PROCESS_FLAG,0) <> 1

	--新增不存在的供应商数据
	insert into LES.TM_BAS_SUPPLIER
	(
		SUPPLIER_NUM,
		SUPPLIER_NAME,
		SUPPLIER_ADDRESS,
		SUPPLIER_GROUP,
		SUPPLIER_TYPE,
		PROVINCE,
		CITY,		
		CONTACT_NAME,
		CONTACT_TEL,
		CONTACT_EMAIL,
		CREATE_USER,
		CREATE_DATE
	)
	SELECT DISTINCT LIFNR,
	NAME1,
	isnull(STRAS,'无'),
	KTOKK,
	1,		--默认为LES供应商	
	REGIO,
	ORT00,	
	NAME0,
	TELF0,
	SMTP_ADDR,
	'admin',
	getdate()
	from LES.TI_BAS_SUPPLIER_IN
	where ISNULL(PROCESS_FLAG,0) <> 1 and
	LIFNR not in
	(
		select SUPPLIER_NUM 
		from LES.TM_BAS_SUPPLIER
	)

	--更新接口表标识
	update LES.TI_BAS_SUPPLIER_IN
	set PROCESS_FLAG = 1,
	PROCESS_TIME = getdate()
	where ISNULL(PROCESS_FLAG,0) <> 1
    
commit transaction
end try
begin catch
--出错，则返回执行不成功，回滚事务
rollback transaction
--记录错误信息
insert into [LES].[TS_SYS_EXCEPTION] (time_stamp, [application], [METHOD], class,  exception_message, error_code)
select getdate(),'INTERFACE','PROC_INTERFACE_SAP_LES_SUPPLIER','Procedure',error_message(),ERROR_LINE()

end catch