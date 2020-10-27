/****************************************************/
/* Author:		孙述霄								*/
/* Create date: 2017-10-13							*/
/* Description:	SPS 生成流水号						*/
/****************************************************/
CREATE PROCEDURE [LES].[PROC_SPS_GET_NEXT_SEQUENCE]
(    
    @SequenceName NVARCHAR(32)
)
AS
BEGIN
    DECLARE @NextSequence INT
	DECLARE @TmpMaxVal INT = 99999999
    BEGIN TRY
        BEGIN TRAN
            UPDATE  [LES].[TS_SPS_SEQUENCE] WITH (ROWLOCK)
            SET     [CURRENT_VALUE] = CASE WHEN [CURRENT_VALUE] + [STEP_VALUE] > [MAX_VALUE] THEN [INIT_VALUE] ELSE [CURRENT_VALUE] + [STEP_VALUE] END,
                    [LAST_UPDATE_DATE] = GETDATE()
            WHERE   UPPER(RTRIM(LTRIM([SEQUENCE_NAME]))) = UPPER(RTRIM(LTRIM(@SequenceName)))
            
            IF @@ROWCOUNT = 0
            BEGIN
				IF(@SequenceName = 'SPS_RUNSHEET_NO_SEQ')--对SPS拉动单进行特殊处理，只能取5位
					SET @TmpMaxVal=99999
                INSERT INTO [LES].[TS_SPS_SEQUENCE] ([SEQUENCE_NAME], [CURRENT_VALUE], [INIT_VALUE], [MAX_VALUE], [STEP_VALUE], [LAST_UPDATE_DATE])
                VALUES(@SequenceName, 1, 1, @TmpMaxVal, 1, GETDATE())
            END
            
            SELECT  @NextSequence = ISNULL([PREFIX_STRING], '') + [CURRENT_VALUE] + ISNULL([POSTFIX_STRING], '')
            FROM    [LES].[TS_SPS_SEQUENCE]
            WHERE   UPPER(RTRIM(LTRIM([SEQUENCE_NAME]))) = UPPER(RTRIM(LTRIM(@SequenceName)))
            
            COMMIT TRAN
            RETURN  @NextSequence
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN
		--记录错误信息
		INSERT INTO [LES].[TS_SYS_EXCEPTION] ([TIME_STAMP], [APPLICATION], [METHOD], [CLASS], [EXCEPTION_MESSAGE], [ERROR_CODE])
		SELECT GETDATE(), 'SPS', '[LES].[PROC_SPS_GET_NEXT_SEQUENCE]', 'Procedure', ERROR_MESSAGE(), ERROR_LINE()
        RETURN ''
    END CATCH
END