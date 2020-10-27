/********************************************************************/
/*   Project Name:  ASN												*/
/*   Program Name:  [LES].[PROC_WMS_STOCK_INPUT]					*/
/*   Called By:     web page										*/
/*   Author:        ScottHu											*/
/*   Create date:	2017-08-31										*/
/*   Note:			VMI入库存储过程								*/
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_WMS_STOCK_INPUT]
	@PLANT nvarchar(5),
    @WM_NO nvarchar(10),
	@ZONE_NO nvarchar(20),
	@DLOC nvarchar(20),
	@SUPPLIER_NUM nvarchar(12),
	@PART_NO nvarchar(20),
	@PART_CNAME nvarchar(100),
	@PACKAGE_MODEL nvarchar(30),
	@PACKAGE int,
	@NUM numeric(18,2),
	@BATCH_NO nvarchar(100),
	@BARCODE_DATA nvarchar(50),
	@BARCODE_TYPE nvarchar(10),
	@COMMENTS nvarchar(200),
	@MEASURING_UNIT_NO nvarchar(20), -- 对应PART_UNITS
	@IS_BATCH int,
	@BOX_NUM numeric(18,2)
AS
BEGIN
	SET NOCOUNT ON;

    declare @StockId int,@MAX numeric(18,2),@MIN numeric(18,2),@PART_CLS nvarchar(50),@PART_UNITS nvarchar(20)

	--更新要进行数据处理的数据对应的状态
	select
		@PACKAGE=(CASE WHEN ISNULL(@PACKAGE, 0) = 0 THEN ISNULL(S.PACKAGE,1) ELSE @PACKAGE END),
		@PACKAGE_MODEL = S.PACKAGE_MODEL, 
		@MAX = S.MAX, 
		@MIN= S.MIN, 
		@PLANT=S.PLANT, 
		@WM_NO = S.WM_NO,
		@DLOC=S.DLOC,
		@SUPPLIER_NUM=S.SUPPLIER_NUM,
		@PART_CNAME=S.PART_CNAME,
		@PART_UNITS=CASE WHEN ISNULL(@MEASURING_UNIT_NO,'')='' Then MP.PART_UNITS Else @MEASURING_UNIT_NO END,
		@IS_BATCH=S.IS_BATCH,
		@PART_CLS=S.PART_CLS,
		@NUM=ISNULL(CASE --批次管理的情况,入库和出库PDA会传递真实的件数,但Web会传NUM=0,只要是0则把包装数当成实际件数
									WHEN S.IS_BATCH = 1 AND @NUM = 0 THEN ISNULL(S.PACKAGE,1)
									--翻包操作，NUM就取package
									--WHEN T.TRAN_TYPE IN (5, 6) THEN S.PACKAGE*T.BOX_NUM
									ELSE ISNULL(@NUM,0) END,0),
		@BOX_NUM=ISNULL(CASE WHEN ISNULL(@BOX_NUM,0) < ISNULL(S.PACKAGE,1) THEN 0 ELSE ISNULL(@BOX_NUM,0) END,0)
	from 
		LES.TM_BAS_PARTS_STOCK (NOLOCK) S
		inner join 
		LES.TM_BAS_MAINTAIN_PARTS (NOLOCK) MP 
		on MP.PLANT=S.PLANT and S.PART_NO=MP.PART_NO
	where 
		S.PART_NO=@PART_NO and S.ZONE_NO=@ZONE_NO and S.PLANT=@PLANT
	
	SELECT @StockId=STOCK_IDENTITY 
			  FROM LES.TT_WMS_STOCKS S with(nolock)
			 WHERE PART_NO=@PART_NO 
			   AND S.WM_NO=@WM_NO
			   AND S.ZONE_NO=@ZONE_NO
			   AND S.DLOC=@DLOC
			   AND S.SUPPLIER_NUM=@SUPPLIER_NUM
			   AND S.DLOC is not null

	--入库
	IF ( ISNULL(@StockId, '') <> '' )
	--零件存在，修改老的库存
	BEGIN
		UPDATE LES.TT_WMS_STOCKS
			SET STOCKS_NUM = ISNULL(STOCKS_NUM,0) + ISNULL(@NUM, 0),									
				STOCKS = CASE WHEN @PACKAGE IS NULL OR @PACKAGE=0 
						THEN 0 
						ELSE 
							CASE WHEN ISNULL(STOCKS_NUM,0) + ISNULL(@NUM, 0)>=0
							THEN
								CEILING(CAST(ISNULL(STOCKS_NUM,0) + ISNULL(@NUM, 0) AS FLOAT) / CAST(ISNULL(@PACKAGE,1) AS FLOAT))
							ELSE
								FLOOR(CAST(ISNULL(STOCKS_NUM,0) + ISNULL(@NUM, 0) AS FLOAT) / CAST(ISNULL(@PACKAGE,1) AS FLOAT))
							END 
						END,                                            
				AVAILBLE_STOCKS = (ISNULL(STOCKS_NUM,0) + ISNULL(@NUM, 0) - ISNULL(FROZEN_STOCKS, 0)),		 							
				FRAGMENT_NUM = (ISNULL(STOCKS_NUM,0) + ISNULL(@NUM, 0)- ISNULL(FROZEN_STOCKS, 0)) % ISNULL(PACKAGE,1),
				UPDATE_DATE = GETDATE(),											
				COMMENTS = @COMMENTS
			Where STOCK_IDENTITY=@StockId					
	END
	ELSE
	--零件不存在，添加新的库存
	BEGIN
		INSERT  INTO [LES].[TT_WMS_STOCKS]([PLANT] ,
											[WM_NO] ,
											[ZONE_NO] ,
											[DLOC] ,
											[SUPPLIER_NUM] ,
											[PART_NO] ,
											[PART_CNAME] ,
											PART_NICKNAME,
											[PART_UNITS] ,
											[PACKAGE_MODEL] ,
											[PACKAGE] ,
											[MAX] ,
											[MIN] ,
											[SAFE_STOCK] ,
											[STOCKS] ,
											STOCKS_NUM,
											[FROZEN_STOCKS] ,
											[AVAILBLE_STOCKS] ,
											[IS_BATCH] ,
											[FRAGMENT_NUM] ,
											[PART_CLS] ,
											[IS_REPACK] ,
											[REPACK_ROUTE] ,
											[BATCH_NO] ,
											[BARCODE_DATA] ,
											[BARCODE_TYPE] ,
											[COMMENTS] ,
											[CREATE_USER] ,
											[CREATE_DATE])
								SELECT  @PLANT ,
										@WM_NO ,
										@ZONE_NO ,
										@DLOC ,
										@SUPPLIER_NUM ,
										@PART_NO ,
										@PART_CNAME ,
										(SELECT TOP 1 PART_NICKNAME FROM LES.TM_BAS_MAINTAIN_PARTS T WHERE T.PLANT=@PLANT AND T.PART_NO=@PART_NO),
										@PART_UNITS ,
										@PACKAGE_MODEL ,
										ISNULL(@PACKAGE,1) ,
										@MAX ,
										@MIN ,
										0 AS SAFE_STOCK ,
										CASE WHEN @PACKAGE IS NULL OR @PACKAGE=0 
										THEN 0 
										ELSE 
											CASE WHEN ISNULL(@NUM, 0)>=0
											THEN
												CEILING(CAST(ISNULL(@NUM, 0) AS FLOAT) / CAST(ISNULL(@PACKAGE,1) AS FLOAT))
											ELSE
												FLOOR(CAST(ISNULL(@NUM, 0) AS FLOAT) / CAST(ISNULL(@PACKAGE,1) AS FLOAT))
											END 
										END,--STOCKS
										ISNULL(@NUM,0) AS STOCKS_NUM,
										0 AS FROZEN_STOCKS ,
										AVAILBLE_STOCKS =@NUM,
										0 AS IS_BATCH ,
										ISNULL(@NUM,0) % ISNULL(@PACKAGE,1) AS [FRAGMENT_NUM] ,
										@PART_CLS ,
										'' AS [IS_REPACK] ,
										'' AS [REPACK_ROUTE] ,
										@BATCH_NO ,
										@BARCODE_DATA ,
										@BARCODE_TYPE ,
										@COMMENTS ,
										'' AS [CREATE_USER] ,
										GETDATE() AS [CREATE_DATE]
	END

END