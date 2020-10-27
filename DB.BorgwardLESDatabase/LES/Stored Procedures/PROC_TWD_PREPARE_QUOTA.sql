
-- =============================================
-- Author:		吕小望
-- Create date: 2011-10-11
-- Description:	分配供应商配额并生成　拉动
-- =============================================
CREATE proc [LES].[PROC_TWD_PREPARE_QUOTA]
as
	set nocount on 
	declare @partNo nvarchar(20)
	declare @plant nvarchar(20)
	declare quota_crsr cursor for
	select PART_NO ,PLANT  from LES.TM_TWD_SUPPLIER_QUOTA_RECORD 
		where 
		[START_EFFECTIVE_DATE] <= getdate() and [END_EFFECTIVE_DATE] >= getdate()
		group by PART_NO , PLANT having (CONVERT(date,max(NEW_SEND_DATE)) <> CONVERT(date,getdate()))

	open quota_crsr
	fetch next from quota_crsr into @partNo , @plant
	while( @@fetch_status = 0 )
	begin
		update LES.TM_TWD_SUPPLIER_QUOTA_RECORD 
		set NEW_SEND_DATE = getdate()
		where ID IN (
				select top 1 ID  from [LES].[TM_TWD_SUPPLIER_QUOTA_RECORD]
					where PART_NO = @partNo
					and [START_EFFECTIVE_DATE] <= getdate() and [END_EFFECTIVE_DATE] >= getdate()
					and PLANT = @plant
					and CURRENT_QUOTE <= QUOTE)

		fetch next from quota_crsr into @partNo , @plant
	end 
	close quota_crsr
	deallocate quota_crsr
	set nocount off
	
grant execute on LES.PROC_TWD_PREPARE_QUOTA to apLES