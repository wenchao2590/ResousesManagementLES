CREATE PROCEDURE [LES].[PROC_TT_SPM_MRP_DETAIL_SELECT]
     @WhereCond nvarchar(max), 
	 @OrderBy varchar(500),
	 @StartRowIndex int,  
	 @MaximumRows INT,
	 @Where INT
AS
BEGIN
SET DATEFIRST 1;
DECLARE @BeginWeek DATETIME;
DECLARE @EndWeek DATETIME;
--SELECT @BeginWeek=DATEPART(WEEK,GETDATE())-@Where 
--SELECT @EndWeek=datepart(week,getdate())
SELECT @BeginWeek=convert(varchar(10),getdate()-(datepart(weekday,getdate())-1),120)
SELECT @EndWeek=LES.xfn_GetDate(CONVERT(INT,DATEPART(YEAR,GETDATE())),CONVERT(INT,DATEPART(WEEK,GETDATE())+@Where),7)
DECLARE @WeekNum INT;
SELECT @WeekNum=COUNT(*) FROM LES.TT_SPM_MRP_DETAIL  WHERE REQUIRE_DATE >=@BeginWeek AND REQUIRE_DATE<=@EndWeek 
PRINT @WeekNum
IF @WeekNum>0
BEGIN

DECLARE @Str NVARCHAR(max)   
SET @Str = 'SELECT top 100 percent PART_NO,PART_CNAME'    
SELECT @Str = @Str+',['+ WEEK_ORDER + ']' from LES.TT_SPM_MRP_DETAIL WHERE REQUIRE_DATE >=@BeginWeek AND REQUIRE_DATE<=@EndWeek  group by WEEK_ORDER 
SET @Str = @Str+' FROM (SELECT PART_NO,PART_CNAME,WEEK_ORDER FROM LES.TT_SPM_MRP_DETAIL WHERE 1=1  '+@WhereCond+') AS T PIVOT(COUNT([WEEK_ORDER]) FOR WEEK_ORDER IN ('  
SELECT @Str = @Str+' ['+ WEEK_ORDER + '],' from LES.TT_SPM_MRP_DETAIL WHERE REQUIRE_DATE >=@BeginWeek AND REQUIRE_DATE<=@EndWeek group BY WEEK_ORDER    
SET @Str = left(@Str,Len(@str)-1)    
SET @Str = @Str+ ')) AS thePivot ORDER BY PART_NO ASC'   
--EXEC (@Str)  
PRINT @Str
	DECLARE @RecordCount int
	DECLARE @PageIndex int
	SET NOCOUNT ON; 
IF(@MaximumRows IS NULL OR @MaximumRows=0) SET @MaximumRows=10  
	
	Set @PageIndex = @StartRowIndex/@MaximumRows;
	Set @PageIndex = @PageIndex + 1
	
	IF(@PageIndex IS NULL OR @PageIndex<1) SET @PageIndex=1  
		
	IF(@OrderBy IS NULL ) SET @OrderBy=''
	
	
	DECLARE @IsLast int  
	
	DECLARE @SQLString nvarchar(max)
	
	PRINT convert(varchar(20),getdate(),20)
	SET @SQLString=	' SELECT @RecordCount = COUNT(*) ' + '
					from ('+@Str+') AS t'
	PRINT @SQLString
	EXECUTE sp_executesql @SQLString,N'@RecordCount int output ',@RecordCount OUTPUT  
	
	PRINT convert(varchar(20),getdate(),20)
	
	--PRINT @SQLString
	
	DECLARE @MaxPage int;  

	SET @MaxPage = CASE WHEN @RecordCount % @MaximumRows > 0 THEN @RecordCount / @MaximumRows + 1 ELSE @RecordCount / @MaximumRows END;  

	IF(@PageIndex > @MaxPage)  
	BEGIN  
		SET @IsLast = 1;   
	END   
	
	IF (@IsLast = 1)  
	BEGIN  
		SET @PageIndex = @MaxPage;   
	END  
    IF (@RecordCount>0 AND @MaximumRows>0)  
	BEGIN   
	PRINT @PageIndex * @MaximumRows
		SET @SQLString =   
		N'WITH CTE AS 
		( 
			SELECT TOP ' + CAST(@PageIndex * @MaximumRows AS VARCHAR) + ' row_number() OVER(ORDER BY '+@OrderBy+ ') AS RowNumber,*  
			FROM ( '+@Str+') AS T1 ORDER BY '+@OrderBy+'
		)
		SELECT * 
		FROM CTE
		WHERE RowNumber > ' + CAST((@PageIndex - 1) * @MaximumRows AS VARCHAR) + '
		ORDER BY RowNumber' 
		PRINT @PageIndex * @MaximumRows
		PRINT @SQLString
		PRINT  convert(varchar(20),getdate(),20)
		EXECUTE sp_executesql @SQLString,N'@PageIndex int,@MaximumRows int',@PageIndex,@MaximumRows   
		PRINT convert(varchar(20),getdate(),20)
	END  
	ELSE  
	BEGIN  
		SET @SQLString =@Str
		PRINT @SQLString
		EXECUTE sp_executesql @SQLString  
	END  
	END
	ELSE
	BEGIN
	SELECT NULL AS 'NULLCOUNT' FROM LES.TT_SPM_MRP_DETAIL WHERE 1<>1
	END
	SET NOCOUNT OFF;  
	END