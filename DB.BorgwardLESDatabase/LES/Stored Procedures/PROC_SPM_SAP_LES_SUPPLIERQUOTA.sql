
--
CREATE PROCEDURE [LES].[PROC_SPM_SAP_LES_SUPPLIERQUOTA] 
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
		--如果中间表有有效数据则删除业务数据表
			IF EXISTS (SELECT * FROM LES.TI_BAS_SUPPLIER_QUOTA WHERE ISNULL(PROCESS_FLAG,0) = 0 and DATEDIFF(MINUTE,[CREATE_DATE],GETDATE()) > 3)
				TRUNCATE TABLE LES.TT_SPM_SUPPLIER_PART_QUOTA;
			--插入数据
			INSERT INTO LES.TT_SPM_SUPPLIER_PART_QUOTA
				    ( PART_NO ,
				        SUPPLIER_NUM ,
				        PLANT ,
				        START_EFFECTIVE_DATE ,
				        END_EFFECTIVE_DATE ,
				        QUOTE ,
				        COMMENTS ,
				        UPDATE_DATE ,
				        UPDATE_USER ,
				        CREATE_DATE ,
				        CREATE_USER ,
				        AGREEMENT_NO ,
				        PROJECT ,
				        LOEKZ
				    )
			SELECT PART_NO ,
				        SUPPLIER_NUM ,
				        PLANT ,
				        START_EFFECTIVE_DATE ,
				        END_EFFECTIVE_DATE ,
				        QUOTE ,
				        COMMENTS ,
				        UPDATE_DATE ,
				        UPDATE_USER ,
				        CREATE_DATE ,
				        CREATE_USER ,
				        AGREEMENT_NO ,
				        PROJECT ,
				        LOEKZ
			FROM LES.TI_BAS_SUPPLIER_QUOTA
			WHERE ISNULL(PROCESS_FLAG,0) = 0
			and DATEDIFF(MINUTE,[CREATE_DATE],GETDATE()) > 3

			UPDATE LES.TI_BAS_SUPPLIER_QUOTA SET PROCESS_FLAG = 1 WHERE ISNULL(PROCESS_FLAG,0) = 0;
			COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		--出错，则返回执行不成功，回滚事务
	ROLLBACK TRANSACTION
--记录错误信息
	INSERT INTO [LES].[TS_SYS_EXCEPTION] (time_stamp, [application], [METHOD], class,  exception_message, error_code)
	select getdate(),'INTERFACE','PROC_SPM_SAP_LES_SUPPLIERQUOTA','Procedure',error_message(),ERROR_LINE()

	END CATCH
END