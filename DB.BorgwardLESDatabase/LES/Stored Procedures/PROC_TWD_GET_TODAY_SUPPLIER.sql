

-- =============================================
-- Author:		吕小望
-- Create date: 2011-10-13
-- Description:	TWD 取得今天的供应商
-- =============================================
CREATE proc [LES].[PROC_TWD_GET_TODAY_SUPPLIER]
(
@partNo nvarchar(20),
@plant  nvarchar(10)
,@supplierNum nvarchar(20) output
)
as
--给定一个零件号，判断今日发货供应商
begin
	declare @result nvarchar(20)
	declare @count int
    
	select @count = count(1) from [LES].[TM_TWD_SUPPLIER_QUOTA_RECORD]
			where [START_EFFECTIVE_DATE] <= getdate() and [END_EFFECTIVE_DATE] >= getdate()
			and PART_NO = @partNo and PLANT = @plant
	if( @count = 0)
	begin
	    --如果供应商配额信息中不存在该零件信息 返回NULL
    	set @result = null
	end
	else
	begin
		--如果今日已经设定供应商，则今日一直选择该供应商
        select @result = SUPPLIER_NUM  from [LES].[TM_TWD_SUPPLIER_QUOTA_RECORD] 
			where PART_NO = @partNo
			and [START_EFFECTIVE_DATE] <= getdate() and [END_EFFECTIVE_DATE] >= getdate()
            and PLANT = @plant
			and convert(date,NEW_SEND_DATE) = convert(date,getdate())
         	
		if( @result is null)
		begin
            --今日没有选定供应商则选择第一个没有满足配额的供应商
			declare @id int
			
			select top 1 @id=ID , @result =SUPPLIER_NUM from [LES].[TM_TWD_SUPPLIER_QUOTA_RECORD]
				where PART_NO = @partNo
				and [START_EFFECTIVE_DATE] <= getdate() and [END_EFFECTIVE_DATE] >= getdate()
                and PLANT = @plant
				and CURRENT_QUOTE <= QUOTE
                
			update [LES].[TM_TWD_SUPPLIER_QUOTA_RECORD]
				set NEW_SEND_DATE = getdate()
				where ID = @id
		end
	end
	set @supplierNum =@result
end