-- =============================================
-- Author:		XinPengZhang
-- Create date: 2018-1-9
-- Description:	根据过点向PTL发送BOM数据
-- =============================================
CREATE PROCEDURE [LES].[PROC_INTERFACE_LES_PTL_POINT] 
	
AS
BEGIN
	--发送BOM数据比较多，每次发送一单
	SELECT TOP 1 * INTO #POINT FROM [LES].[TT_SPS_CALCULATE_POINT] WITH(NOLOCK) WHERE PTL_SEND_FLAG = 0 AND DCP_POINT='9110_TP_2100'

	 IF EXISTS (SELECT ID FROM #POINT)
	 BEGIN		
		DECLARE @FID UNIQUEIDENTIFIER
		SET @FID = NEWID()

		BEGIN TRAN
			--插入计划接收中间表
			INSERT INTO [LES].[TI_MID_PTL_PLAN_RECEVIE]
			(
				[FID],
				[VIN],
				[SeqNo],
				[PartNo],
				[PartName],
				[Qty],
				[BoxPart],
				[CreateTime],
				[OrderType],
				[OrderNo],
				[ZordNO],
				[Farbin],
				[Zcolore_D],
				[Zcolori_D]
			)
			SELECT @FID,A.VIN,NULL,B.PART_NO,B.PART_NAME,B.QUANTITY,NULL,GETDATE(),'N',NULL,A.ORDER_ID,NULL,NULL,NULL FROM #POINT A WITH(NOLOCK)
			JOIN [LES].[TT_BAS_ORDER_PART_RESULTS] B WITH(NOLOCK) ON A.ORDER_ID = B.ORDER_NO AND A.PLANT = B.PLANT

			--插入服务中间表
			INSERT INTO [LES].[TI_SYS_OUTBOUND]
			(
				[FID],
				[TRANS_NO],
				[METHORD_NAME],
				[EXECUTE_RESULT],
				[KEY_VALUE],
				[VALID_FLAG],
				[CREATE_USER],
				[CREATE_DATE]
			)
			SELECT
				@FID,
				'007',
				'TI_MID_PTL_PLAN_RECEVIE',
				0,
				'',
				1,
				'admin',
				GETDATE()
		COMMIT
	 END
	 

END