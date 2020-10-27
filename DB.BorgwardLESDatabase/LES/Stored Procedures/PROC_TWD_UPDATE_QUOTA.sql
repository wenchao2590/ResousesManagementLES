



CREATE proc [LES].[PROC_TWD_UPDATE_QUOTA]
(
	@partNo nvarchar(20)
    ,@plant nvarchar(10)
	,@packCount int
)
as
	begin
		--set @partNo = 'N  90656201       '
		--declare @packCount int
		--set @packCount  = 10;
		declare @id int
		declare @count int
		--declare @totalSend int
		select @id = ID from [LES].[TM_TWD_SUPPLIER_QUOTA_RECORD]
				where [START_EFFECTIVE_DATE] <= getdate() and [END_EFFECTIVE_DATE] >= getdate()
				and convert(date,NEW_SEND_DATE) = convert(date,getdate()) 
                and PLANT = @plant
				and PART_NO = @partNo
		set @count = @@rowcount
		if( @count <> 1)
		begin
			--如果供应商配额信息中不存在该零件信息 
			return
		end


		declare @sumTotalSend int
		select @sumTotalSend = sum(TOTAL_SEND) from [LES].[TM_TWD_SUPPLIER_QUOTA_RECORD]
			where  [START_EFFECTIVE_DATE] <= getdate() and [END_EFFECTIVE_DATE] >= getdate()
            and PLANT = @plant
			and PART_NO = @partNo
		--新的总数等于原总数加新发数量
		set @sumTotalSend = @sumTotalSend + @packCount

		declare  quota_cursor cursor for
			select TOTAL_SEND ,NEW_SEND_DATE from [LES].[TM_TWD_SUPPLIER_QUOTA_RECORD]
			where [START_EFFECTIVE_DATE] <= getdate() and [END_EFFECTIVE_DATE] >= getdate()
            and PLANT = @plant
			and PART_NO = @partNo for update
		open quota_cursor
		declare @totalSend int
		declare @newSendDate datetime
		declare @quota numeric(18,2)

		fetch next from quota_cursor  into @totalSend,@newSendDate 
		while(@@fetch_status = 0)
		begin
			--今日供应商
			if( convert(date,@newSendDate) = convert(date,getdate())) 
			begin
				set @totalSend = @totalSend +@packCount
				set @quota = convert(numeric(18,2),@totalSend)/@sumTotalSend
				--print @quota
				update [LES].[TM_TWD_SUPPLIER_QUOTA_RECORD] set
					TOTAL_SEND = @totalSend
					,CURRENT_QUOTE = @quota
					where current of quota_cursor
			end
			else
			begin
				set @quota = convert(numeric(18,2),@totalSend)/@sumTotalSend
				--print @quota
				update [LES].[TM_TWD_SUPPLIER_QUOTA_RECORD] set
					--TOTAL_SEND = @totalSend
					CURRENT_QUOTE = @quota
					where current of quota_cursor
			end
			fetch next from quota_cursor  into @totalSend,@newSendDate 
		end
		close quota_cursor
		deallocate quota_cursor
		
	end