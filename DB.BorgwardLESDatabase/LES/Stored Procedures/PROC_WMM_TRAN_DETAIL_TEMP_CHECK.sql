﻿
/********************************************************************/
/*                                                                  */
/*   Project Name:  LES System                          */
/*   Program Name:  [PROC_WMM_TRAN_DETAIL_TEMP_CHECK]                       */
/*   Called By:     by the Page							*/
/*   Purpose:       This is the main stored procedure for the       */
/*   author:       andy	2015-06-09   				       */
/********************************************************************/
CREATE PROC [LES].[PROC_WMM_TRAN_DETAIL_TEMP_CHECK]
AS
	BEGIN TRANSACTION;

	
	BEGIN TRY

	UPDATE [LES].TE_WMM_TRAN_DETAIL_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '交易单号为空;'
	WHERE TRAN_NO IS NULL or LEN([TRAN_NO]) < 1
		
	UPDATE [LES].TE_WMM_TRAN_DETAIL_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '需求箱数格式错误;'
	WHERE BOX_NUM_TEXT='' or ISNUMERIC(BOX_NUM_TEXT)=0 or CAST(BOX_NUM_TEXT AS INT)<0
	
	UPDATE [LES].TE_WMM_TRAN_DETAIL_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '需求件数格式错误;'
	WHERE NUM_TEXT='' or ISNUMERIC(NUM_TEXT)=0 or CAST(NUM_TEXT AS INT)<0

	UPDATE [LES].TE_WMM_TRAN_DETAIL_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '交易单已被确认不可再修改;'
	WHERE EXISTS
	(
		SELECT 1 FROM LES.TT_WMM_TRAN_HEAD R (NOLOCK)
		WHERE R.TRAN_NO = [LES].TE_WMM_TRAN_DETAIL_TEMP.TRAN_NO 
		AND R.TRAN_STATUS IN (1,2)
	)	

	UPDATE [LES].TE_WMM_TRAN_DETAIL_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '零件号长度超出;'
	WHERE LEN([PART_NO]) > 20

	UPDATE [LES].TE_WMM_TRAN_DETAIL_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '源仓库/存储区不存在指定零件信息;'
	WHERE 
		[PART_NO] NOT IN 
		(SELECT st.[PART_NO] FROM 
			[LES].[TM_BAS_PARTS_STOCK] st, [LES].[TT_WMM_TRAN_HEAD] th
		 WHERE 
			[LES].TE_WMM_TRAN_DETAIL_TEMP.TRAN_NO=th.TRAN_NO 
			AND st.PLANT=th.PLANT AND st.WM_NO=th.S_WM_NO AND st.ZONE_NO=th.S_ZONE_NO)
		OR 
		[PART_NO] NOT IN 
		(SELECT st.[PART_NO] FROM 
			[LES].[TT_WMS_STOCKS] st, [LES].[TT_WMM_TRAN_HEAD] th
		 WHERE 
			[LES].TE_WMM_TRAN_DETAIL_TEMP.TRAN_NO=th.TRAN_NO 
			AND st.PLANT=th.PLANT AND st.WM_NO=th.S_WM_NO AND st.ZONE_NO=th.S_ZONE_NO)
			
	UPDATE [LES].TE_WMM_TRAN_DETAIL_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '目的仓库/存储区不存在指定零件的仓库信息;'
	WHERE 
		[PART_NO] NOT IN 
		(SELECT st.[PART_NO] FROM 
			[LES].[TM_BAS_PARTS_STOCK] st, [LES].[TT_WMM_TRAN_HEAD] th
		 WHERE th.TRAN_TYPE NOT IN ('307','55') AND
			[LES].TE_WMM_TRAN_DETAIL_TEMP.TRAN_NO=th.TRAN_NO 
			AND st.PLANT=th.PLANT AND st.WM_NO=th.O_WM_NO AND st.ZONE_NO=th.O_ZONE_NO)	
			

	UPDATE [LES].TE_WMM_TRAN_DETAIL_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '零件号重复;'
	WHERE PART_NO IN (
		SELECT tmp.PART_NO FROM 
			(SELECT TRAN_NO, PART_NO, COUNT(*) AS NUM FROM [LES].[TE_WMM_TRAN_DETAIL_TEMP] WHERE PART_NO is not NULL GROUP BY TRAN_NO, PART_NO) tmp 
		WHERE tmp.NUM > 1)

	DECLARE @TranType INT,@ErrCount int
	DECLARE @Plant NVARCHAR(20),@WmNo  NVARCHAR(20),@ZoneNo NVARCHAR(20)

	SELECT @ErrCount=COUNT(1) FROM [LES].TE_WMM_TRAN_DETAIL_TEMP WHERE VALID_FLAG = 0

	IF(@ErrCount=0)
	BEGIN
	UPDATE [LES].TE_WMM_TRAN_DETAIL_TEMP SET NUM=NUM_TEXT
	UPDATE [LES].TE_WMM_TRAN_DETAIL_TEMP SET BOX_NUM=BOX_NUM_TEXT


	SELECT TOP 1 @TranType=TRAN_TYPE,@Plant=PLANT,@WmNo=S_WM_NO,@ZoneNo=S_ZONE_NO FROM LES.TT_WMM_TRAN_HEAD WHERE 
	TRAN_NO IN (SELECT DISTINCT tran_no FROM [LES].TE_WMM_TRAN_DETAIL_TEMP)  


	UPDATE t_detail
	SET t_detail.VALID_FLAG = 0, t_detail.ERROR_MSG = ERROR_MSG + '可用库存量不足;'
	 FROM  [LES].TE_WMM_TRAN_DETAIL_TEMP t_detail 
	left JOIN LES.TT_WMS_STOCKS t_STOCK
	  ON t_STOCK.PART_NO = t_detail.PART_NO
	   AND t_STOCK.PLANT=@Plant
	   AND t_STOCK.WM_NO=@WmNo
	   AND t_STOCK.ZONE_NO=@ZoneNo
	WHERE  t_STOCK.IS_BATCH!=1 AND @TranType IN (261,304,307,55,301)
	 AND (t_STOCK.Stocks_Num-t_STOCK.FROZEN_STOCKS)<t_detail.NUM
	
	
	UPDATE t_detail
	SET t_detail.VALID_FLAG = 0, t_detail.ERROR_MSG = ERROR_MSG + '冻结件数不足;'
	 FROM  [LES].TE_WMM_TRAN_DETAIL_TEMP t_detail 
	left JOIN LES.TT_WMS_STOCKS t_STOCK
	  ON t_STOCK.PART_NO = t_detail.PART_NO
	   AND t_STOCK.PLANT=@Plant
	   AND t_STOCK.WM_NO=@WmNo
	   AND t_STOCK.ZONE_NO=@ZoneNo
	WHERE  t_STOCK.IS_BATCH!=1 AND @TranType=302
	 AND (t_STOCK.FROZEN_STOCKS)<t_detail.NUM	



	END;
    
	END TRY
	BEGIN CATCH
		SELECT 
			ERROR_NUMBER() AS ErrorNumber
			,ERROR_SEVERITY() AS ErrorSeverity
			,ERROR_STATE() AS ErrorState
			,ERROR_PROCEDURE() AS ErrorProcedure
			,ERROR_LINE() AS ErrorLine
			,ERROR_MESSAGE() AS ErrorMessage;

		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
	END CATCH;

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;