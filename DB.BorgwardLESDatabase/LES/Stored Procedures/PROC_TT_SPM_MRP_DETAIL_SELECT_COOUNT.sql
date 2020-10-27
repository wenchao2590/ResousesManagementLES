CREATE PROCEDURE [LES].[PROC_TT_SPM_MRP_DETAIL_SELECT_COOUNT]
@WhereCond nvarchar(MAX), 
	 @OrderBy varchar(500),
	 @StartRowIndex int,  
	 @MaximumRows INT,
	 @Where INT
AS
BEGIN


DECLARE @BeginWeek DATETIME;
DECLARE @EndWeek DATETIME;
SELECT @BeginWeek=convert(varchar(10),getdate()-(datepart(weekday,getdate())-1),120)
SELECT @EndWeek=LES.xfn_GetDate(CONVERT(INT,DATEPART(YEAR,GETDATE())),CONVERT(INT,DATEPART(WEEK,GETDATE())+@Where),7)
DECLARE @WeekNum INT;
SELECT @WeekNum=COUNT(*) FROM LES.TT_SPM_MRP_DETAIL  WHERE REQUIRE_DATE >=@BeginWeek AND REQUIRE_DATE<=@EndWeek 
IF @WeekNum>0
BEGIN
DECLARE @Str NVARCHAR(max)   
SET @Str = 'SELECT top 100 percent PART_NO,PART_CNAME'    
SELECT @Str = @Str+',['+ WEEK_ORDER + ']' from LES.TT_SPM_MRP_DETAIL WHERE REQUIRE_DATE >=@BeginWeek AND REQUIRE_DATE<=@EndWeek  group by WEEK_ORDER 
SET @Str = @Str+' FROM (SELECT PART_NO,PART_CNAME,WEEK_ORDER FROM LES.TT_SPM_MRP_DETAIL WHERE 1=1  '+@WhereCond+') AS T PIVOT(COUNT([WEEK_ORDER]) FOR WEEK_ORDER IN ('  
SELECT @Str = @Str+' ['+ WEEK_ORDER + '],' from LES.TT_SPM_MRP_DETAIL WHERE REQUIRE_DATE >=@BeginWeek AND REQUIRE_DATE<=@EndWeek group BY WEEK_ORDER    
SET @Str = left(@Str,Len(@str)-1)    
SET @Str = @Str+ ')) AS thePivot ORDER BY PART_NO ASC'  
DECLARE @SQLString nvarchar(max)
SET @SQLString=	' SELECT  COUNT(*) ' + '
					from ('+@Str+') AS t'
EXECUTE sp_executesql @SQLString 
END
ELSE
 SELECT 0
END