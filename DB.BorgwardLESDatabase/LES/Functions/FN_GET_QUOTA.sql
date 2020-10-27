
-- =============================================
-- Author:		<Author,,zeno>
-- Create date: <Create Date,2013-9-25 ,>
-- Description:	<Description,获取零件拉动配额,>
-- =============================================
CREATE FUNCTION [LES].[FN_GET_QUOTA]
(
	@plant          VARCHAR(10),
	@partNo         VARCHAR(50),
	@suppliter_num  VARCHAR(50)
	-- Add the parameters for the function here
)
RETURNS DECIMAL(18,2)
AS
BEGIN
	DECLARE @result DECIMAL(18,2)
	/*
	SELECT top 1 @result = QUOTE
	FROM  LES.TM_TWD_SUPPLIER_QUOTA_RECORD T1
	WHERE  PLANT = @plant
	       AND PART_NO = @partNo
	       AND SUPPLIER_NUM = @suppliter_num
	       AND [START_EFFECTIVE_DATE] <= GETDATE()
	       AND [END_EFFECTIVE_DATE] >= GETDATE()
	       AND START_EFFECTIVE_DATE = (
	               SELECT MAX(START_EFFECTIVE_DATE)
	               FROM   LES.TM_TWD_SUPPLIER_QUOTA_RECORD T2
	               WHERE  plant = T1.plant
	                      AND part_no = T1.part_no
	                      AND CONVERT(date, create_date) = (
	                              SELECT CONVERT(date, MAX(create_Date))
	                              FROM   LES.TM_TWD_SUPPLIER_QUOTA_RECORD
	                              WHERE  plant = T2.plant
	                                     AND part_no = T2.part_no
	                          )
	           )
	*/
	SELECT TOP 1 @result=QUOTE FROM LES.TM_TWD_SUPPLIER_QUOTA_RECORD 
	WHERE PART_NO = @partNo
	AND PLANT=@plant
	AND SUPPLIER_NUM=@suppliter_num
	ORDER BY CREATE_DATE DESC,START_EFFECTIVE_DATE DESC
	RETURN isnull(@result,1)
END