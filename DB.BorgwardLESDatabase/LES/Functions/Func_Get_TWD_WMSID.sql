

/**********************************************************************************
* Purpose		: Get TWD WMSID SET BY CONDITION  
* Author		: Chen Gang
* CreateDate	: 2012-07-26
***********************************************************************************/

CREATE FUNCTION [LES].[Func_Get_TWD_WMSID](
	@PLANT			NVARCHAR(5), 
	@SUPPLIER_NUM	NVARCHAR(12), 
	@ORDER_NO		NVARCHAR(10), 
	@ITEM_NO		NVARCHAR(10), 
	@WAREHOUSE		NVARCHAR(50), 
	@PART_NO		NVARCHAR(20), 
	@CREATE_DATE	DATETIME
)  
RETURNS   NVARCHAR(MAX)   
AS   
BEGIN   

	DECLARE		@WMSID_SET   NVARCHAR(MAX)  
	SET			@WMSID_SET = ''   

	SELECT		@WMSID_SET = @WMSID_SET + ISNULL(WMSID, 0) + ','
	FROM		[LES].[TI_TWD_RECEIVED_GOODS]
	WHERE		[PLANT] = @PLANT 
	AND			[SUPPLIER_NUM] = @SUPPLIER_NUM
	AND			ISNULL([ORDER_NO], '') = ISNULL(@ORDER_NO, '')
	AND			ISNULL([ITEM_NO], '') = ISNULL(@ITEM_NO,'')
	AND			ISNULL([WAREHOUSE],'') = ISNULL(@WAREHOUSE, '')
	AND			[PART_NO] = @PART_NO
	AND			[CREATE_DATE] <= @CREATE_DATE
	AND			[INTERFACE_TYPE] = 'TWD'
	AND			[INTERFACE_STATUS] IS NULL
	

	RETURN		LTRIM(RTRIM(@WMSID_SET))
END