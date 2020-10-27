
----------------------------------------
----提取待分解的零件
----取JIS序号
--modify by xhm 2014/05/20 【CMDB编号：CR-LES-20140520】 
--1,获取JIS拆分序号的存储过程【PROC_JIS_GET_NEXT_SEQUENCE】出现异常时记录日志到表《TS_SYS_EXCEPTION》，
--最大值判断等于"="改为大于等于">="99999999
----------------------------------------
CREATE PROCEDURE [LES].[PROC_JIS_GET_NEXT_SEQUENCE]
(    
    @SequenceName nvarchar(32)
)
AS
BEGIN
    DECLARE @NextSequence int
    BEGIN TRY
        BEGIN TRAN
            UPDATE  LES.TS_JIS_SEQUENCE WITH(ROWLOCK)
            SET     CURRENT_VALUE = CASE WHEN CURRENT_VALUE + 1 >= MAX_VALUE THEN INIT_VALUE ELSE CURRENT_VALUE + 1 END,
                    LAST_UPDATE_DATE = GETDATE()
            WHERE   UPPER(RTRIM(LTRIM(SEQUENCE_NAME))) = UPPER(RTRIM(LTRIM(@SequenceName)))
            
            IF @@ROWCOUNT = 0
            BEGIN
                INSERT INTO LES.TS_JIS_SEQUENCE(SEQUENCE_NAME,CURRENT_VALUE,INIT_VALUE,MAX_VALUE,STEP_VALUE, LAST_UPDATE_DATE)
                VALUES(@SequenceName, 1, 1, 99999999, 1, GETDATE())                
            END
            
            SELECT  @NextSequence = ISNULL(PREFIX_STRING,'') + CURRENT_VALUE + ISNULL(POSTFIX_STRING,'')
            FROM    LES.TS_JIS_SEQUENCE
            WHERE   UPPER(RTRIM(LTRIM(SEQUENCE_NAME))) = UPPER(RTRIM(LTRIM(@SequenceName)))
            
            COMMIT TRAN
            RETURN @NextSequence 
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN
		 INSERT    INTO [LES].[TS_SYS_EXCEPTION]
        (
          time_stamp ,
          [application] ,
          [METHOD] ,
          class ,
          exception_message ,
          error_code
        )
        SELECT  GETDATE() ,
                'JIS' ,
                'PROC_JIS_GET_NEXT_SEQUENCE' ,
                'Procedure' ,
                ERROR_MESSAGE() ,
                ERROR_LINE()
        RETURN ''
    END CATCH   
    
END