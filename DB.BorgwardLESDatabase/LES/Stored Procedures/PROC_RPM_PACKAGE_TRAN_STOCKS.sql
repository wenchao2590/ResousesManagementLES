CREATE PROCEDURE [LES].[PROC_RPM_PACKAGE_TRAN_STOCKS]
(
	@TranId BIGINT,
    @IsInto INT = 1,						--1为入库,2为出库,3按托出库
    @Result INT OUTPUT,						--检查结果，1表示成功，-0表示失败
    @ResultMessage NVARCHAR(1000) OUTPUT	--结果消息
)
AS
BEGIN
	DECLARE @Plant NVARCHAR(5)
	DECLARE @WmNo NVARCHAR(10)
	DECLARE @ZoneNo NVARCHAR(20)
	DECLARE @PackageNo NVARCHAR(30)

	BEGIN TRY    
		--判断库存中是否存在交易明细表中记录,把不在库存中的记录提取出来
		SELECT TOP 1
			@Plant = A.[PLANT],
			@WmNo = A.[WM_NO],
			@ZoneNo = A.[ZONE_NO],
			@PackageNo = A.[PACKAGE_NO]
		FROM [LES].[TT_RPM_PACKAGE_TRAN_DETAIL] A WITH (NOLOCK)
		WHERE A.[TRAN_ID] = @TranId
		AND NOT EXISTS
		(
			SELECT
				B.[STOCK_ID]
			FROM [LES].[TM_RPM_PACKAGE_STOCKS] B WITH (NOLOCK)
			WHERE  B.[PLANT] = A.[PLANT]
			AND B.[WM_NO] = A.[WM_NO]
			AND B.[ZONE_NO] = A.[ZONE_NO]
			AND B.[PACKAGE_NO] = A.[PACKAGE_NO]
		)
		--检验操作
		IF (LEN(ISNULL(@Plant, '')) > 0)
			BEGIN
				SET @Result = 0
				SET @ResultMessage = N'在包装库存中不存在对应记录,操作失败！工厂' + @Plant + N'；仓库编码'+ @WmNo + N'；存贮区编码'+ @ZoneNo + N'；包装器具编号'+ @PackageNo +N'；'
				RETURN
			END

		--定义临时表存放以交易明细汇总记录    
		DECLARE @TemTable TABLE
		(
			[PLANT] NVARCHAR(5),
			[WM_NO] NVARCHAR(10),
			[ZONE_NO] NVARCHAR(20),
			[PACKAGE_NO] NVARCHAR(30),
			[NUM] NUMERIC(18,2)
		)
		--入库操作
		IF (@IsInto = 1)
			BEGIN
				--交易明细表添加到临时表
				INSERT INTO @TemTable
				(
					[PLANT],
					[WM_NO],
					[ZONE_NO],
					[PACKAGE_NO],
					[NUM]
				)
				SELECT
					[PLANT],
					[WM_NO],
					[ZONE_NO],
					[PACKAGE_NO],
					SUM([Current_BOX_NUM]) AS [NUM]
				FROM [LES].[TT_RPM_PACKAGE_TRAN_DETAIL] WITH (NOLOCK)
				WHERE [TRAN_ID] = @TranId
				GROUP BY [PLANT], [WM_NO], [ZONE_NO], [PACKAGE_NO]

				UPDATE S
				SET S.[STOCK] = S.[STOCK] + T.[NUM]
				FROM [LES].[TM_RPM_PACKAGE_STOCKS] S WITH (ROWLOCK)
				INNER JOIN @TemTable T
				ON S.[PLANT] = T.[PLANT] AND S.[WM_NO] = T.[WM_NO]
				AND S.[ZONE_NO] = T.[ZONE_NO] AND S.[PACKAGE_NO] = T.[PACKAGE_NO]
				WHERE T.[NUM] <> 0
			END

		--出库操作
		IF (@IsInto = 2 OR @IsInto = 3)
			BEGIN
				--交易明细表添加到临时表
				INSERT INTO @TemTable
				(
					[PLANT],
					[WM_NO],
					[ZONE_NO],
					[PACKAGE_NO],
					[NUM]
				)
				SELECT
					[PLANT],
					[WM_NO],
					[ZONE_NO],
					[PACKAGE_NO],
					SUM([NUM]) AS [NUM]
				FROM [LES].[TT_RPM_PACKAGE_TRAN_DETAIL] WITH (NOLOCK)
				WHERE [TRAN_ID] = @TranId
				GROUP BY [PLANT], [WM_NO], [ZONE_NO], [PACKAGE_NO]

				--先判断库存的数量是否大于出库数量
				SET @Plant = NULL
				SELECT TOP 1
					@Plant = S.[PLANT],
					@WmNo = S.[WM_NO],
					@ZoneNo = S.[ZONE_NO],
					@PackageNo = S.[PACKAGE_NO]
				FROM [LES].[TM_RPM_PACKAGE_STOCKS] S WITH (NOLOCK)
				INNER JOIN @TemTable T
				ON S.[PLANT] = T.[PLANT]
				AND S.[WM_NO] = T.[WM_NO]
				AND S.[ZONE_NO] = T.[ZONE_NO]
				AND S.[PACKAGE_NO] = T.[PACKAGE_NO]
				WHERE T.[NUM] > S.[STOCK]
				--检验操作
				IF (LEN(ISNULL(@Plant, '')) > 0)
					BEGIN
						SET @Result = 0
						SET @ResultMessage = N'在包装库存中数量小于出库数量,操作失败！工厂' + @Plant + N'；仓库编码' + @WmNo + N'；存贮区编码' + @ZoneNo + N'；包装器具编号' + @PackageNo + N'；'
						RETURN
					END

				--出库
				UPDATE S
				SET S.[STOCK] = S.[STOCK] - T.[NUM]
				FROM [LES].[TM_RPM_PACKAGE_STOCKS] S WITH (ROWLOCK)
				INNER JOIN @TemTable T
				ON S.[PLANT] = T.[PLANT] AND S.[WM_NO] = T.[WM_NO] AND S.[ZONE_NO] = T.[ZONE_NO] AND S.[PACKAGE_NO] = T.[PACKAGE_NO]
				WHERE T.[NUM] <> 0

				IF(@IsInto=3)
					BEGIN
						UPDATE [LES].[TT_RPM_PACKAGE_BARCODE] WITH (ROWLOCK)
						SET [BARCODE_STATUS] = 4
						WHERE [BARCODE_DATA] IN 
						(SELECT DISTINCT [BARCODE_DATA] FROM [LES].[TT_RPM_PACKAGE_TRAN_DETAIL] WITH (NOLOCK) WHERE [TRAN_ID] = @TranId)
					END
    
			END

		--更新主表字段已确认
		UPDATE [LES].[TT_RPM_PACKAGE_TRAN] WITH (ROWLOCK) SET [CONFIRM_FLAG] = 1 WHERE [TRAN_ID] = @TranId

		SET @Result = 1
		SET @ResultMessage = N'操作成功'
	END TRY
	BEGIN CATCH
	    --记录错误信息
		SET @Result = 0
		SET @ResultMessage = ERROR_MESSAGE()
	END CATCH
END