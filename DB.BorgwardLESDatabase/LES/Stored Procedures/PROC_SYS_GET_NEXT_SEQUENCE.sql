
-- =============================================
-- Author:		吕小望
-- Create date: 2011-10-13
-- Description:	生成序号
-- =============================================
CREATE PROCEDURE [LES].[PROC_SYS_GET_NEXT_SEQUENCE]
(    
    @SequenceName nvarchar(32)
)
AS
BEGIN
    DECLARE @NextSequence int
    BEGIN TRY
        BEGIN TRAN
            UPDATE  LES.TS_SYS_SEQUENCE WITH(ROWLOCK)
            SET     CURRENT_VALUE = CASE WHEN CURRENT_VALUE + 1 = MAX_VALUE THEN INIT_VALUE ELSE CURRENT_VALUE + 1 END,
                    LAST_UPDATE_DATE = GETDATE()
            WHERE   UPPER(RTRIM(LTRIM(SEQUENCE_NAME))) = UPPER(RTRIM(LTRIM(@SequenceName)))
            
            IF @@ROWCOUNT = 0
            BEGIN
                INSERT INTO LES.TS_SYS_SEQUENCE(SEQUENCE_NAME,CURRENT_VALUE,INIT_VALUE,MAX_VALUE,STEP_VALUE, LAST_UPDATE_DATE)
                VALUES(@SequenceName, 1, 1, 99999999, 1, GETDATE())                
            END
            
            SELECT  @NextSequence = ISNULL(PREFIX_STRING,'') + CURRENT_VALUE + ISNULL(POSTFIX_STRING,'')
            FROM    LES.TS_SYS_SEQUENCE
            WHERE   UPPER(RTRIM(LTRIM(SEQUENCE_NAME))) = UPPER(RTRIM(LTRIM(@SequenceName)))
            
            COMMIT TRAN
            RETURN  @NextSequence
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN
        RETURN ''
    END CATCH   
    
END