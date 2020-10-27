﻿
----------------------------------------
----提取待分解的零件
----问题: 如何判断是否跳号?
----约束条件:
----1.初步假设实际最大跳号范围为1000
----2.人工处理号不连续时，要么直接设置下一个处理号，要么插入不连续号，不允许同时设置下一个号和插入断号，
----否则这些断号遗留在表中，会影响未来跳号判断
----不会发生从9999跳号后，新过点序号也超过上次最大处理号
----------------------------------------
CREATE PROCEDURE [LES].[PROC_JIS_GET_PARTS_RESOLVE_TRIGGERSTATUS_ORDERS]
(
	@PLANT	NVARCHAR(5),
	@ASSEMBLY_LINE	NVARCHAR(10)
)
AS
BEGIN
	
	--上次零件分解的最大过点号
	/*DECLARE @JisFlexSn nvarchar(8), @JisFlexTime datetime
	DECLARE @LastRunningNo nvarchar(5),@MaxRunningNo nvarchar(5), @MinRunningNo nvarchar(5)
	DECLARE @MaxRunningTime DATETIME, @MinRunningTime DATETIME
	
	CREATE TABLE #Temp_TRIGGERSTATUS_ORDERS
	(
		RID	INT identity(1,1),
		ID	INT,
		RUNNING_NO INT,
		FLAG INT
	)
	CREATE INDEX IDX_#Temp_TRIGGERSTATUS_ORDERS_1 ON #Temp_TRIGGERSTATUS_ORDERS(RID)
	CREATE INDEX IDX_#Temp_TRIGGERSTATUS_ORDERS_2 ON #Temp_TRIGGERSTATUS_ORDERS(ID)

	CREATE TABLE #Temp_TO_DO_RUNNING
	(
		PLANT				NVARCHAR(5)	,
		ASSEMBLY_LINE		NVARCHAR(10),
		MAX_RUNNING_NO		INT,
		MAX_RUNNING_TIME	DATETIME,
		MIN_RUNNING_NO		INT,
		MIN_RUNNING_TIME	DATETIME		
	)
	CREATE UNIQUE INDEX IDX_#Temp_TO_DO_RUNNING_1 ON #Temp_TO_DO_RUNNING(PLANT, ASSEMBLY_LINE) WITH IGNORE_DUP_KEY
	--根据JIS状态点表中KNR获取状态点信息和对应的订单信息
	
	IF NOT EXISTS (SELECT 1 FROM LES.TM_JIS_FLEX_SN WHERE PLANT = @PLANT AND ASSEMBLY_LINE = @ASSEMBLY_LINE)
	BEGIN
		INSERT	INTO LES.TM_JIS_FLEX_SN(PLANT, ASSEMBLY_LINE, JIS_FLEX_SN, JIS_FLEX_TIME, CREATE_USER, CREATE_DATE)
		VALUES (@PLANT, @ASSEMBLY_LINE, '0000', GETDATE(), 'JISPartsResolveService', GETDATE())
	END

	SELECT	@JisFlexSn = CONVERT(NVARCHAR(8),JIS_FLEX_SN), @JisFlexTime = JIS_FLEX_TIME
	FROM	[LES].TM_JIS_FLEX_SN
	WHERE	PLANT = @PLANT AND ASSEMBLY_LINE = @ASSEMBLY_LINE

	IF	@JisFlexSn IS NULL
		SET @JisFlexSn = '0000'
	
	
	INSERT	INTO #Temp_TO_DO_RUNNING
	SELECT  @PLANT, @ASSEMBLY_LINE, MAX(CONVERT(INT,RUNNING_NO)), MAX(PASS_TIME), NULL, NULL
	FROM	[LES].TT_JIS_TRIGGER_STATUS
	WHERE	PLANT = @PLANT AND ASSEMBLY_LINE = @ASSEMBLY_LINE
	AND		TRIGGER_TYPE = 1 --0:预览 1:组单
	AND		PROCESS_FLAG = 0 --0:表示还未处理	
	AND		RUNNING_NO > @JisFlexSn
	AND		RUNNING_NO <= '9999'	
	

	SELECT  @MinRunningNo=MIN(CONVERT(INT,RUNNING_NO)) , @MinRunningTime=MIN(PASS_TIME)
	FROM	[LES].TT_JIS_TRIGGER_STATUS
	WHERE	PLANT = @PLANT AND ASSEMBLY_LINE = @ASSEMBLY_LINE			
	AND		TRIGGER_TYPE = 1 --0:预览 1:组单
	AND		PROCESS_FLAG = 0 --0:表示还未处理			
	AND		RUNNING_NO < @JisFlexSn	
		
	UPDATE	#Temp_TO_DO_RUNNING
	SET		MIN_RUNNING_NO = @MinRunningNo, MIN_RUNNING_TIME = @MinRunningTime
	
	
	INSERT	INTO #Temp_TO_DO_RUNNING
	VALUES	(@PLANT, @ASSEMBLY_LINE, NULL, NULL, @MinRunningNo, @MinRunningTime)
		
	
	--到9999之间没有大于上次处理后的记录，则取上次处理序号和时间作为最大序号和时间
	UPDATE	#Temp_TO_DO_RUNNING
	SET		MAX_RUNNING_NO = @JisFlexSn, MAX_RUNNING_TIME = @JisFlexTime	
	WHERE	MAX_RUNNING_NO IS NULL
	
	--只保留存在跳号的记录
	DELETE	FROM #Temp_TO_DO_RUNNING 
	WHERE	MIN_RUNNING_NO IS NULL 
	OR		(MIN_RUNNING_NO + 9999 > MAX_RUNNING_NO + 1000)
	
	--插入不跳号记录
	INSERT	INTO #Temp_TRIGGERSTATUS_ORDERS
	SELECT	ID, CONVERT(INT, RUNNING_NO), '0'
	FROM	[LES].TT_JIS_TRIGGER_STATUS
	WHERE	PLANT = @PLANT AND ASSEMBLY_LINE = @ASSEMBLY_LINE
	AND		TRIGGER_TYPE = 1 --0:预览 1:组单
	AND		PROCESS_FLAG = 0 --0:表示还未处理	
	AND		RUNNING_NO > @JisFlexSn 
	AND		RUNNING_NO <= '9999'
	ORDER	BY RUNNING_NO
	
	--插入跳号记录
	
	
	INSERT	INTO #Temp_TRIGGERSTATUS_ORDERS
	SELECT	A.ID, CONVERT(INT, A.RUNNING_NO) + 9999, '1'
	FROM	[LES].TT_JIS_TRIGGER_STATUS A, #Temp_TO_DO_RUNNING B
	WHERE	A.PLANT = B.PLANT AND A.ASSEMBLY_LINE = B.ASSEMBLY_LINE
	AND		A.TRIGGER_TYPE = 1	
	AND		A.PROCESS_FLAG = 0		
	ORDER	BY A.RUNNING_NO
	*/
	SELECT	A.ID, 
			A.ORDER_ID,			
			A.KNR,
			A.PASS_TIME,			
			A.VEHICLE_STATUS,			
			A.DCP_NAME,
			A.VIN,
			A.RUNNING_NO,
			A.ASSEMBLY_LINE,
			A.PLANT,
			A.MODEL_NO,
			A.TRIGGER_TYPE,
			A.PROCESS_FLAG,
			A.UPDATE_DATE,
			A.CREATE_DATE,	
			C.FARBAU, C.FARBIN, C.MODEL_YEAR, C.MODEL, C.ORDER_DATE, C.PNR_STRING_COMPUTE, C.WERK, C.VORSERIE,	A.ASSEMBLY_LINE as BASSEMBLY_LINE,C.SPJ		
	FROM	[LES].TT_JIS_TRIGGER_STATUS A 
	LEFT JOIN [LES].TT_BAS_PULL_ORDERS C
	ON		  A.ORDER_ID = C.ORDER_NO
	where A.PLANT = @PLANT AND A.ASSEMBLY_LINE = @ASSEMBLY_LINE and A.TRIGGER_TYPE = 1	 and A.PROCESS_FLAG = 0 
	ORDER	BY A.ID,A.RUNNING_NO
END