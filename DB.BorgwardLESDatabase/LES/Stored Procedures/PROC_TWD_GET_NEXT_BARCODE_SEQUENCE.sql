
-- =============================================
-- Author:		吕小望
-- Create date: 2011-10-13
-- Description:	TWD 生成下一个条码的流水号
-- =============================================
CREATE PROCEDURE [LES].[PROC_TWD_GET_NEXT_BARCODE_SEQUENCE]
(    
    @SequenceName nvarchar(32),
    @result nvarchar(32) output
)
AS
BEGIN
    DECLARE @NextSequence nvarchar(32)
    
    UPDATE  LES.TS_TWD_BARCODE_SEQUENCE WITH(ROWLOCK)
    SET     CURRENT_VALUE = CASE WHEN CURRENT_VALUE + 1 = MAX_VALUE THEN INIT_VALUE ELSE CURRENT_VALUE + 1 END,
            LAST_UPDATE_DATE = GETDATE()
    WHERE   UPPER(RTRIM(LTRIM(SEQUENCE_NAME))) = UPPER(RTRIM(LTRIM(@SequenceName)))
    
    IF @@ROWCOUNT = 0
    BEGIN
        INSERT INTO LES.TS_TWD_BARCODE_SEQUENCE(SEQUENCE_NAME,CURRENT_VALUE,INIT_VALUE,MAX_VALUE,STEP_VALUE, LAST_UPDATE_DATE)
        VALUES(@SequenceName, 1, 1, 9999999999999, 1, GETDATE())                
    END
    
    SELECT  @NextSequence = ISNULL(PREFIX_STRING,'') +right(CONVERT(varchar(8),   getdate(),112),6)+ replace(space(7 -len(convert(nvarchar(30),CURRENT_VALUE))),' ','0')
		+ convert(nvarchar(30),CURRENT_VALUE)+ ISNULL(POSTFIX_STRING,'')
    FROM    LES.TS_TWD_BARCODE_SEQUENCE
    WHERE   UPPER(RTRIM(LTRIM(SEQUENCE_NAME))) = UPPER(RTRIM(LTRIM(@SequenceName)))
    set @result =@NextSequence

    return 0

    
END