-- =============================================
-- Author:		吕小望
-- Create date: 2011-10-13
-- Description:	TWD 生成主明细表的流水号
-- yinxuefeng：流水号由8位改为6位，所以请先执行下面SQL更新此表当前值和最大值列
-- =============================================
/*
--更新当前值和最大值
UPDATE LES.TS_TWD_SEQUENCE SET CURRENT_VALUE=999999,MAX_VALUE=999999 WHERE SEQUENCE_NAME='TWD_RUNSHEET_NO_SEQ'
*/
CREATE PROCEDURE [LES].[PROC_TWD_GET_NEXT_SEQUENCE]
(    
    @SequenceName nvarchar(32)
)
AS
BEGIN
    DECLARE @NextSequence int
	DECLARE @TmpMaxVal int=99999999
    BEGIN TRY
        BEGIN TRAN
            UPDATE  LES.TS_TWD_SEQUENCE WITH(ROWLOCK)
            SET     CURRENT_VALUE = CASE WHEN CURRENT_VALUE + STEP_VALUE > MAX_VALUE THEN INIT_VALUE ELSE CURRENT_VALUE + STEP_VALUE END,
                    LAST_UPDATE_DATE = GETDATE()
            WHERE   UPPER(RTRIM(LTRIM(SEQUENCE_NAME))) = UPPER(RTRIM(LTRIM(@SequenceName)))
            
            IF @@ROWCOUNT = 0
            BEGIN
				if(@SequenceName='TWD_RUNSHEET_NO_SEQ')--对TWD拉动单进行特殊处理，只能取4位
					SET @TmpMaxVal=9999
                INSERT INTO LES.TS_TWD_SEQUENCE(SEQUENCE_NAME,CURRENT_VALUE,INIT_VALUE,MAX_VALUE,STEP_VALUE, LAST_UPDATE_DATE)
                VALUES(@SequenceName, 1, 1, @TmpMaxVal, 1, GETDATE())                
            END
            
            SELECT  @NextSequence = ISNULL(PREFIX_STRING,'') + CURRENT_VALUE + ISNULL(POSTFIX_STRING,'')
            FROM    LES.TS_TWD_SEQUENCE
            WHERE   UPPER(RTRIM(LTRIM(SEQUENCE_NAME))) = UPPER(RTRIM(LTRIM(@SequenceName)))
            
            COMMIT TRAN
            RETURN  @NextSequence
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN
        --add by【运维】xhm 2014/08/04【CMDB编号：CR-LES-20140807】start
		--记录错误信息
		insert into [LES].[TS_SYS_EXCEPTION] (time_stamp, [application], [METHOD], class,  exception_message, error_code)
		select getdate(),'TWD','PROC_TWD_GET_NEXT_SEQUENCE','Procedure',error_message(),ERROR_LINE()
        --add by【运维】xhm 2014/08/04 end
        RETURN ''
    END CATCH   
    
END