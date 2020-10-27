/************************************************/
/* Author:		孙述霄							*/
/* Create date: 2017-08-29						*/
/* Description:	同步动态库位					*/
/************************************************/
CREATE PROCEDURE [LES].[PROC_BAS_SYNCHRONIZE_DYNAMIC_DLOC]
AS
BEGIN
    BEGIN TRY
        BEGIN TRAN
			INSERT INTO [LES].[TR_BAS_PART_TRAY_STOCK]
			(
				[PLANT],
				[WM_NO],
				[ZONE_NO],
				[DLOC],
				[PART_NO],
				[TRAY_NO],
				[BATCH_NO],
				[NUM],
				[BOX_NUM],
				[DLOC_STATUS],
				[VALID_FLAG],
				[CREATE_USER],
				[CREATE_DATE],
				[MODIFY_USER],
				[MODIFY_DATE]
			)
			SELECT
				A.[PLANT],
				A.[WM_NO],
				A.[ZONE_NO],
				A.[DLOC],
				'',
				'',
				'',
				NULL,
				NULL,
				0,
				1,
				'admin',
				GETDATE(),
				'admin',
				GETDATE()
			FROM [LES].[TM_BAS_WAREHOUSE_LOCATION] A WITH (NOLOCK)
			LEFT JOIN [LES].[TR_BAS_PART_TRAY_STOCK] B WITH (NOLOCK)
			ON A.[PLANT] = B.[PLANT] AND A.[WM_NO] = B.[WM_NO] AND A.[ZONE_NO] = B.[ZONE_NO] AND A.[DLOC] = B.[DLOC] AND B.[VALID_FLAG] = 1
			WHERE A.[LOCATION_TYPE] = 1 AND B.[ID] IS NULL

			COMMIT TRAN
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION

		--记录错误信息
		INSERT INTO [LES].[TS_SYS_EXCEPTION] ([TIME_STAMP], [APPLICATION], [METHOD], [CLASS],  [EXCEPTION_MESSAGE], [ERROR_CODE])
		SELECT GETDATE(), 'INTERFACE', '[LES].[PROC_BAS_SYNCHRONIZE_DYNAMIC_DLOC]', 'Procedure', ERROR_MESSAGE(), ERROR_LINE()
    END CATCH
END