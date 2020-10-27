/********************************************************************/
/*   Project Name:  TWD												*/
/*   Program Name:  [LES].[PROC_TWD_GENERATE_CLOSE]		 			*/
/*   Called By:     by web page										*/
/*   Author:        孙述霄											*/
/*   Create date:	2017-09-22										*/
/*   Note:			TWD 关闭拉动单									*/
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_TWD_GENERATE_CLOSE]
(
	@TWDRUNSHEETNO NVARCHAR(50),			--拉动单号
	@LOGINNAME NVARCHAR(50),				--处理人
	@RESULT INT OUTPUT,						--返回结果，0-成功，1-失败
	@RESULTMESSAGE NVARCHAR(4000) OUTPUT	--返回信息
)
AS
BEGIN
	DECLARE @SUPPLIER_TYPE INT				--供应商类型
	DECLARE @IS_ASN INT						--是否ASN单据
	DECLARE @SHEET_STATUS INT				--拉动单状态
	DECLARE @BoxState INT					--零件状态
	DECLARE @plant_zone NVARCHAR(10)		--厂区
	DECLARE @suggestTime DATETIME			--建议发货时间
	DECLARE @pickTime DATETIME				--拣选时间
	DECLARE @wmsState INT					--WMS发送状态
	DECLARE @now DATETIME					--当前时间
	DECLARE @isorganizesheet INT			--是否组织拉动单
	DECLARE @boxPartsName NVARCHAR(100)		--零件类名称
    DECLARE @dock NVARCHAR(10)				--DOCK
    DECLARE @transSupplierNum NVARCHAR(20)	--运输供应商
    DECLARE @WAREHOUSE NVARCHAR(20)			--仓库
    DECLARE @transportTime INT				--运输时间
	DECLARE @unloadtime INT					--卸货时间
	DECLARE @suppliertype INT				--供应商类型
	DECLARE @isasn INT						--是否ASN
	DECLARE @istray INT						--是否按套组托
	DECLARE @ALIAS NVARCHAR(10)				--别名
	SET @now = GETDATE()

	BEGIN TRY
		BEGIN TRANSACTION

		SET @RESULT = 0
		SET @RESULTMESSAGE = ''

		SELECT
			@SUPPLIER_TYPE = B.[SUPPLIER_TYPE], @SHEET_STATUS = A.[SHEET_STATUS], @IS_ASN = A.[IS_ASN]
		FROM [LES].[TT_TWD_RUNSHEET] A WITH (NOLOCK)
		INNER JOIN [LES].[TM_BAS_SUPPLIER] B WITH (NOLOCK) ON A.[SUPPLIER_NUM] = B.[SUPPLIER_NUM]
		WHERE A.[TWD_RUNSHEET_NO] = @TWDRUNSHEETNO

		IF @SHEET_STATUS = 1
			BEGIN
				SET @RESULT = 1
				SET @RESULTMESSAGE = '拉动单[' + @TWDRUNSHEETNO + ']已关闭！'
			END

		IF @RESULT = 0
			BEGIN
				IF @SUPPLIER_TYPE = 2
					BEGIN
						SET @RESULT = 1
						SET @RESULTMESSAGE = '拉动单[' + @TWDRUNSHEETNO + ']生成的是出库单，只能关闭生成入库单的拉动单！'
					END
			END

		IF @RESULT = 0
			BEGIN
				IF @IS_ASN = 0
					BEGIN
						--直接产生入库单
						IF EXISTS (SELECT 1 FROM [LES].[TT_WMM_RECEIVE] WITH (NOLOCK) WHERE [RECEIVE_NO] = @TWDRUNSHEETNO AND [CONFIRM_FLAG] <> 2)
							AND (EXISTS (SELECT 1 FROM [LES].[TM_WMM_TRAN_DETAILS] WITH (NOLOCK) WHERE [TRAN_NO] = @TWDRUNSHEETNO)
							OR EXISTS (SELECT 1 FROM [LES].[TM_WMM_TRAN_DETAILS_LOG] WITH (NOLOCK) WHERE [TRAN_NO] = @TWDRUNSHEETNO))
							BEGIN
								SET @RESULT = 1
								SET @RESULTMESSAGE = '拉动单[' + @TWDRUNSHEETNO + ']对应的入库单未完成入库，不能关闭！'
							END
						ELSE
							BEGIN
								--关闭拉动单
								UPDATE [LES].[TT_TWD_RUNSHEET] WITH (ROWLOCK)
								SET [SHEET_STATUS] = 1,
									[UPDATE_USER] = @LOGINNAME,
									[UPDATE_DATE] = GETDATE()
								WHERE [TWD_RUNSHEET_NO] = @TWDRUNSHEETNO
								--关闭入库单
								UPDATE [LES].[TT_WMM_RECEIVE] WITH (ROWLOCK)
								SET [CONFIRM_FLAG] = 2,
									[UPDATE_USER] = @LOGINNAME,
									[UPDATE_DATE] = GETDATE()
								WHERE [RECEIVE_NO] = @TWDRUNSHEETNO
							END
					END
				ELSE
					BEGIN
						--需创建ASN单据
						--关闭拉动单
						UPDATE [LES].[TT_TWD_RUNSHEET] WITH (ROWLOCK)
						SET [SHEET_STATUS] = 1,
							[UPDATE_USER] = @LOGINNAME,
							[UPDATE_DATE] = GETDATE()
						WHERE [TWD_RUNSHEET_NO] = @TWDRUNSHEETNO
					END
			END

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		--出错，则返回执行不成功，回滚事务
		ROLLBACK TRANSACTION
		--记录错误信息
		INSERT INTO [LES].[TS_SYS_EXCEPTION] ([TIME_STAMP], [APPLICATION], [METHOD], [CLASS], [EXCEPTION_MESSAGE], [ERROR_CODE])
		SELECT GETDATE(), 'TWD', '[LES].[PROC_TWD_GENERATE_CLOSE]', 'Procedure', ERROR_MESSAGE(), ERROR_LINE()
	END CATCH
END