/*************************************************************/
/*   Project Name:  TWD									     */
/*   Program Name:  [LES].[PROC_WMM_UPDATE_TWD_COUNTER]  	 */
/*   Called By:     window service							 */
/*   Author:        孙述霄									 */
/*   Date:			2017-05-18								 */
/*   Note:			根据拉动明细更改TWD计数器				 */
/*************************************************************/
CREATE PROCEDURE [LES].[PROC_WMM_UPDATE_TWD_COUNTER]
AS
BEGIN
	SET XACT_ABORT ON
	SET NOCOUNT ON
	BEGIN TRY
		BEGIN TRANSACTION
			DECLARE @MinID INT
			DECLARE @plant NVARCHAR(5)
			DECLARE @assemblyLine NVARCHAR(10)
			DECLARE @partNo NVARCHAR(20)
			DECLARE @partCname NVARCHAR(100)
			DECLARE @packCount INT
			DECLARE @supplier_num NVARCHAR(12)
			DECLARE @boxparts NVARCHAR(10)
			DECLARE @dmsno NVARCHAR(50)
			DECLARE @supplier_type INT
			DECLARE @isasn INT
			DECLARE @pullmode INT
			DECLARE @istray INT

			--删除已经处理的数据
			DELETE FROM [LES].[TE_TWD_MATERIAL_CONSUME_TEMP] WITH (ROWLOCK) WHERE [PROCESS_FLAG] = 1

			--循环TWD消耗临时表，更新TWD计数器
			SET @MinID = 0
			SELECT @MinID = MIN(ID) FROM [LES].[TE_TWD_MATERIAL_CONSUME_TEMP] WITH (NOLOCK) WHERE [PROCESS_FLAG] = 0
			WHILE @MinID IS NOT NULL
				BEGIN
					--获取需要更新计数器的参数
					SELECT
						@plant = [PLANT],
						@assemblyLine = [ASSEMBLY_LINE],
						@partNo = [PART_NO],
						@partCname = [PART_CNAME],
						@packCount = [PACK_COUNT],
						@supplier_num = [SUPPLIER_NUM],
						@boxparts = [BOX_PARTS],
						@dmsno = [CARD_NO]
					FROM [LES].[TE_TWD_MATERIAL_CONSUME_TEMP] 
					WHERE ID = @MinID

					--获取供应商信息
					SELECT
						@supplier_type = [SUPPLIER_TYPE],
						@isasn = ISNULL([ASN_FLAG], 0)
					FROM [LES].[TM_BAS_SUPPLIER] WITH (NOLOCK)
					WHERE [SUPPLIER_NUM] = @supplier_num

					--获取是否按套组托
					SELECT
						@pullmode = [PULL_MODE],
						@istray = [IS_TRAY]
					FROM [LES].[TM_TWD_BOX_PARTS] WITH (NOLOCK)
					WHERE [PLANT] = @plant AND [ASSEMBLY_LINE] = @assemblyLine AND [SUPPLIER_NUM] = @supplier_num AND [BOX_PARTS] = @boxparts

					--更新计数器消耗
					UPDATE [LES].[TT_TWD_CONSUME_COUNTER] WITH (ROWLOCK) 
					SET	[CURRENT_PART_COUNT] = [CURRENT_PART_COUNT] + @packCount,
						[UPDATE_DATE] = GETDATE()
					WHERE [PLANT] = @plant AND [ASSEMBLY_LINE] = @assemblyLine AND [PART_NO] = @partNo AND [SUPPLIER_NUM] = @supplier_num AND [INBOUND_PART_CLASS] = @boxparts

					--记录TWD消耗日志
					INSERT INTO [LES].[TL_TWD_MATERIAL_CONSUME_LOG]
					(
						[PLANT_ZONE],
						[WORKSHOP],
						[ASSEMBLY_LINE],
						[PLANT],
						[LOCATION],
						[PART_NO],
						[INDENTIFY_PART_NO],
						[PART_CNAME],
						[PART_ENAME],
						[SUPPLIER_NUM],
						[DOCK],
						[BOX_PARTS],
						[PACK_COUNT],
						[INBOUND_PACKAGE_MODEL],
						[INBOUND_PACKAGE],
						[MEASURING_UNIT_NO],
						[RDC_DLOC],
						[MODEL],
						[DMSNO],
						[CREATE_DATE],
						[CREATE_USER],
						[PART_SUPPLIER_NUM],
						[PART_SUPPLIER_NAME],
						[PULL_MODE]
					)
					SELECT
						[PLANT_ZONE],
						[WORKSHOP],
						[ASSEMBLY_LINE],
						[PLANT],
						[LOCATION],
						[PART_NO],
						[INDENTIFY_PART_NO],
						[PART_CNAME],
						[PART_ENAME],
						[SUPPLIER_NUM],
						[DOCK],
						[BOX_PARTS],
						[PACK_COUNT],
						[INBOUND_PACKAGE_MODEL],
						[INBOUND_PACKAGE],
						[MEASURING_UNIT_NO],
						[RDC_DLOC],
						[MODEL],
						[CARD_NO] AS [DMSNO],
						GETDATE() AS [CREATE_DATE],
						[CREATE_USER],
						[PART_SUPPLIER_NUM],
						(SELECT [SUPPLIER_NAME] FROM [LES].[TM_BAS_SUPPLIER] WITH (NOLOCK) WHERE [SUPPLIER_NUM] = [LES].[TE_TWD_MATERIAL_CONSUME_TEMP].[PART_SUPPLIER_NUM]) AS [PART_SUPPLIER_NAME],
						@pullmode
					FROM [LES].[TE_TWD_MATERIAL_CONSUME_TEMP] WITH (NOLOCK)
					WHERE ID = @MinID

					IF @supplier_type = 1 AND @istray = 1
						BEGIN
							--记录按套组托日志表
							INSERT INTO [LES].[TL_TWD_MATERIAL_TRAY_LOG]
							(
								[PLANT],
								[ASSEMBLY_LINE],
								[SUPPLIER_NUM],
								[BOX_PARTS],
								[DMSNO],
								[PART_NO],
								[PART_CNAME],
								[PACK_COUNT],
								[FARBAU],
								[FARBIN],
								[MODEL_YEAR],
								[MODEL],
								[ZCOLORI],
								[ZCOLORI_D],
								[IS_ASN],
								[PROCESS_FLAG],
								[IS_GENERATE],
								[CREATE_DATE],
								[CREATE_USER]
							)
							SELECT TOP 1
								@plant,
								@assemblyLine,
								@supplier_num,
								@boxparts,
								@dmsno,
								@partNo,
								@partCname,
								@packCount,
								[FARBAU],
								[FARBIN],
								[MODEL_YEAR],
								[MODEL],
								[ZCOLORI],
								[ZCOLORI_D],
								@isasn,
								0,
								0,
								GETDATE(),
								'admin'
							FROM [LES].[TT_BAS_PULL_ORDERS] WITH (NOLOCK)
							WHERE [WERK] = @plant AND [ORDER_NO] = @dmsno
						END

					--更新TWD消耗临时表处理状态
					UPDATE [LES].[TE_TWD_MATERIAL_CONSUME_TEMP] WITH (ROWLOCK) SET [PROCESS_FLAG] = 1, [PROCESS_DATE] = GETDATE() WHERE [ID] = @MinID
					--获取未处理消耗的最小ID
					SELECT @MinID = MIN(ID) FROM [LES].[TE_TWD_MATERIAL_CONSUME_TEMP] WITH (NOLOCK) WHERE [PROCESS_FLAG] = 0
				END
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		--出错，则返回执行不成功，回滚事务
		ROLLBACK TRANSACTION
		--记录错误信息
		INSERT INTO [LES].[TS_SYS_EXCEPTION] ([TIME_STAMP], [APPLICATION], [METHOD], [CLASS], [EXCEPTION_MESSAGE], [ERROR_CODE])
		SELECT GETDATE(), 'TWD', '[LES].[PROC_WMM_UPDATE_TWD_COUNTER]', 'Procedure', ERROR_MESSAGE(), ERROR_LINE()
	END CATCH
END