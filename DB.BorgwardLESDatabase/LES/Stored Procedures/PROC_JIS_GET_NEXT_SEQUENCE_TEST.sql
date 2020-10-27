
----------------------------------------
----提取待分解的零件
----取JIS序号
----------------------------------------
CREATE PROCEDURE [LES].[PROC_JIS_GET_NEXT_SEQUENCE_TEST]
(    
    @SequenceName nvarchar(32)
)
AS
BEGIN
    DECLARE @NextSequence int
	Declare @currentValue int
	Declare @maxValue	int
    BEGIN TRY
        BEGIN TRAN

		if((UPPER(RTRIM(LTRIM(@SequenceName))))='JIS_RUNSHEET_FLEX_SN')
			begin
				INSERT INTO [LES].[TS_JIS_SEQUENCE_LOG]
			   ([CURRENT_VALUE]
			   ,[INIT_VALUE]
			   ,[MAX_VALUE]
			   ,[LAST_UPDATE_DATE])
			   select CURRENT_VALUE,CURRENT_VALUE-1,[MAX_VALUE],getdate() from LES.TS_JIS_SEQUENCE_TEST where 
				UPPER(RTRIM(LTRIM(SEQUENCE_NAME))) = UPPER(RTRIM(LTRIM(@SequenceName)))
			end

			select @currentValue=CURRENT_VALUE,@maxValue=MAX_VALUE
			FROM  LES.TS_JIS_SEQUENCE_TEST
			where UPPER(RTRIM(LTRIM(SEQUENCE_NAME))) = UPPER(RTRIM(LTRIM(@SequenceName)))
			if(@currentValue+1>@maxValue)
			begin
				 UPDATE  LES.TS_JIS_SEQUENCE_TEST WITH(ROWLOCK)
				 SET     CURRENT_VALUE =  INIT_VALUE ,
                    LAST_UPDATE_DATE = GETDATE()
					where UPPER(RTRIM(LTRIM(SEQUENCE_NAME))) = UPPER(RTRIM(LTRIM(@SequenceName)))
			end
			else
			begin
				 UPDATE  LES.TS_JIS_SEQUENCE_TEST WITH(ROWLOCK)
				 SET     CURRENT_VALUE =  CURRENT_VALUE+1 ,
                    LAST_UPDATE_DATE = GETDATE()
					where UPPER(RTRIM(LTRIM(SEQUENCE_NAME))) = UPPER(RTRIM(LTRIM(@SequenceName)))
			end
           
            
            IF @@ROWCOUNT = 0
            BEGIN
                INSERT INTO LES.TS_JIS_SEQUENCE_TEST(SEQUENCE_NAME,CURRENT_VALUE,INIT_VALUE,MAX_VALUE,STEP_VALUE, LAST_UPDATE_DATE)
                VALUES(@SequenceName, 1, 1, 99999999, 1, GETDATE())                
            END
            
            SELECT  @NextSequence = ISNULL(PREFIX_STRING,'') + CURRENT_VALUE + ISNULL(POSTFIX_STRING,'')
            FROM    LES.TS_JIS_SEQUENCE_TEST
            WHERE   UPPER(RTRIM(LTRIM(SEQUENCE_NAME))) = UPPER(RTRIM(LTRIM(@SequenceName)))
            
			
            COMMIT TRAN
            RETURN  @NextSequence
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