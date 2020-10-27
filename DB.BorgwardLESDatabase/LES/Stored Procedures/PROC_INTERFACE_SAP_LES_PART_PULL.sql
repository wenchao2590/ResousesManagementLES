/************************************************/
/* Author:		Andy Liu						*/
/* Create date: 2015-07-03						*/
/* Description:	SAP零件拉动信息下发				*/
/* Modify Date: 2017-09-20 孙述霄				*/
/************************************************/
CREATE PROC [LES].[PROC_INTERFACE_SAP_LES_PART_PULL]
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

		--定义自增长序号临时表
		DECLARE @SelfNo TABLE
		(
			[SEQ_ID] BIGINT
		)

		INSERT INTO @SelfNo
		SELECT
			[SEQ_ID]
		FROM [LES].[TI_BAS_PART_LACATION_IN] WITH (NOLOCK)
		WHERE [PROCESS_FLAG] IS NULL OR [PROCESS_FLAG] <> 1

		IF EXISTS (SELECT TOP 1 [SEQ_ID] FROM @SelfNo)
			BEGIN
				--更新接口表标识
				UPDATE [LES].[TI_BAS_PART_LACATION_IN] WITH (ROWLOCK)
				SET [PROCESS_FLAG] = 1,
					[PROCESS_TIME] = GETDATE()
				WHERE [SEQ_ID] IN (SELECT [SEQ_ID] FROM @SelfNo)
				
				--不做更新
				--新增不存在的零件拉动信息
				INSERT INTO [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD]
				(
					[PLANT],
					[ASSEMBLY_LINE],
					[PART_NO],
					[SUPPLIER_NUM],
					[PART_ENAME],
					[PART_CNAME],
					[PART_NICKNAME],
					[LOCATION],
					[LOGICAL_PK],
					[DELETE_FLAG],
					[IS_ACTIVE],
					[BUSINESS_PK],
					[DOCK],
					[WORKSHOP],
					[CREATE_USER],
					[CREATE_DATE]
				)
				SELECT
					A.[ZKWERK],
					A.[ZLINE],
					B.[PART_NO],
					B.[SUPPLIER_NUM],
					B.[PART_ENAME],
					B.[PART_CNAME],
					B.[PART_NICKNAME],
					A.[ZLOC],
					A.[LOGICAL_PK],
					[DEAL_FLAG],
					1, --默认值：活动零件
					A.[BUSINESS_PK],
					B.[DOCK],
					A.[ZWS],
					'admin',
					GETDATE()
				FROM [LES].[TI_BAS_PART_LACATION_IN] A WITH (NOLOCK)
				INNER JOIN [LES].[TM_BAS_MAINTAIN_PARTS] B WITH (NOLOCK) ON B.[PART_NO] = A.[ZIDNKD]  --从零件基本信息或获取部分信息
				WHERE A.[SEQ_ID] IN (SELECT [SEQ_ID] FROM @SelfNo)
				AND	A.[LOGICAL_PK] NOT IN
				(
					SELECT
						[LOGICAL_PK]
					FROM [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] WITH (NOLOCK)
				)
			END
    
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		--出错，则返回执行不成功，回滚事务
		ROLLBACK TRANSACTION
		--记录错误信息
		INSERT INTO [LES].[TS_SYS_EXCEPTION] ([TIME_STAMP], [APPLICATION], [METHOD], [CLASS], [EXCEPTION_MESSAGE], [ERROR_CODE])
		SELECT GETDATE(), 'INTERFACE', '[LES].[PROC_INTERFACE_SAP_LES_PART_PULL]', 'Procedure', ERROR_MESSAGE(), ERROR_LINE()
	END CATCH
END