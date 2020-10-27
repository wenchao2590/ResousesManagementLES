-- =============================================
-- Author:		Andy Liu
-- Create date: 2015-07-23
-- Description:	单据编号生成(简化版，不能支持复杂的单据生成格式)
-- Modified Info: 
-- Note:目前只支持1、一个手工传参值；2、简单的日期格式
-- =============================================
CREATE PROC [LES].[PROC_SYS_GET_CURRENT_SEQCODE]
(
	@seqCode nvarchar(32),			--序列号规则	
	@manualParam nvarchar(10),		--手工传参的值
	@billNo nvarchar(50) output		--编号（返回值)
)
as
set nocount on
declare seq_crsr cursor  fast_forward  for 
	select [ID],[IS_FIXED_LENGTH],[LENGTH],[FILL_TYPE],[FILL_CHAR],[DATA_GENERATE_TYPE],[STEP_LENGTH],[DEFAULT_VALUE],[MIN_VALUE],[MAX_VALUE],[IS_CYCLE],[IS_AUTOUP]
	from [LES].[TS_SYS_SEQ_SECTION] where [SEQ_CODE] = @seqCode and [VALID_FLAG] = 1 order by [SECTION_SEQ]

declare @ID int
declare @IS_FIXED_LENGTH  bit
declare @LENGTH  int
declare @FILL_TYPE  int
declare @FILL_CHAR  nvarchar(1)
declare @DATA_GENERATE_TYPE int 
declare @STEP_LENGTH int 
declare @DEFAULT_VALUE nvarchar(16) 
declare @MIN_VALUE int 
declare @MAX_VALUE int 
declare @IS_CYCLE bit 
declare @IS_AUTOUP bit
declare @count int
declare @currentValue int 
set @billNo = ''

open seq_crsr
fetch next from seq_crsr into @ID,@IS_FIXED_LENGTH,@LENGTH,@FILL_TYPE,@FILL_CHAR,@DATA_GENERATE_TYPE,@STEP_LENGTH,@DEFAULT_VALUE,@MIN_VALUE,@MAX_VALUE,@IS_CYCLE,@IS_AUTOUP  
while( @@fetch_status = 0 )
begin
	if (@DATA_GENERATE_TYPE = 1)
	begin
		set @billNo += @DEFAULT_VALUE;
	end
	else if (@DATA_GENERATE_TYPE = 2)
	begin
		set @billNo += @manualParam;
	end	
	else if (@DATA_GENERATE_TYPE = 4)
	begin
		if (lower(@DEFAULT_VALUE) = 'yyyymmdd')
		begin
			set @billNo += convert(varchar(8),getdate(),112);
		end
		else if (lower(@DEFAULT_VALUE) = 'mmdd')
		begin
			set @billNo += right(convert(varchar(8),getdate(),112),4);
		end
		else
		begin
			set @billNo += right(convert(varchar(8),getdate(),112),6);
		end
	end
	else if (@DATA_GENERATE_TYPE = 3)
	begin
		select top 1 @count = 1,@currentValue = CURRENT_VALUE from [LES].[TS_SYS_SEQ_CURRENT_VALUE] where [SEQ_CODE] = @seqCode and [VALID_FLAG] = 1 and [SEQ_SECTION_ID] = @Id and [QUERY_VALUE] = @billNo
		if @count > 0
		begin
			--更新
			set @currentValue = @currentValue + 1;
			update A
			set A.[CURRENT_VALUE] = @currentValue,
				A.[UPDATE_DATE] = getdate()
			from [LES].[TS_SYS_SEQ_CURRENT_VALUE] A where [SEQ_CODE] = @seqCode and [VALID_FLAG] = 1 and [SEQ_SECTION_ID] = @Id and [QUERY_VALUE] = @billNo
		end
		else
		begin
			--新增
			set @currentValue = 1;
			insert into [LES].[TS_SYS_SEQ_CURRENT_VALUE] ([SEQ_CODE] ,[SEQ_SECTION_ID],[QUERY_VALUE],[CURRENT_VALUE],[VALID_FLAG],[CREATE_USER],[CREATE_DATE])
			values(@seqCode,@ID,@billNo,1,1,'admin',getdate());
		end

		if (@LENGTH = 3)
		begin
			set @billNo += right('000'+cast(@currentValue as varchar),3)
		end
		else if (@LENGTH = 4)
		begin
			set @billNo += right('0000'+cast(@currentValue as varchar),4)
		end
		else if (@LENGTH = 5)
		begin
			set @billNo += right('00000'+cast(@currentValue as varchar),5)
		end
		else if (@LENGTH = 6)
		begin
			set @billNo += right('000000'+cast(@currentValue as varchar),6)
		end
	end
			
	fetch next from seq_crsr into @ID,@IS_FIXED_LENGTH,@LENGTH,@FILL_TYPE,@FILL_CHAR,@DATA_GENERATE_TYPE,@STEP_LENGTH,@DEFAULT_VALUE,@MIN_VALUE,@MAX_VALUE,@IS_CYCLE,@IS_AUTOUP 
end

close seq_crsr
deallocate seq_crsr
set nocount off