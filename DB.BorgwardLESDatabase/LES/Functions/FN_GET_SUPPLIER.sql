

CREATE FUNCTION [LES].[FN_GET_SUPPLIER]
(
	@partNo nvarchar(20)
	,@plant nvarchar(20)
	,@assemblyLine nvarchar(20)
	,@boxPart nvarchar(20)
)
RETURNS nvarchar(20)
AS
BEGIN
	declare @supplier nvarchar(20)
	select @supplier = SUPPLIER_NUM from LES.TM_TWD_SUPPLIER_QUOTA_RECORD 
		where PART_NO = @partNo
			and [START_EFFECTIVE_DATE] <= getdate() and [END_EFFECTIVE_DATE] >= getdate()
			and PLANT = @plant 
			and convert(date,NEW_SEND_DATE) = convert(date, getdate())
	if(@supplier is null)
	begin
		select @supplier = SUPPLIER_NUM from LES.TM_TWD_BOX_PARTS
			where PLANT = @plant and ASSEMBLY_LINE =@assemblyLine and BOX_PARTS = @boxPart
			
	end
	return @supplier
END