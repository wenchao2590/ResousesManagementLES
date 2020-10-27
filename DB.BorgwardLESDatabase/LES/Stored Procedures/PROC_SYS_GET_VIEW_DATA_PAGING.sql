
-- =============================================
-- Author:		吕小望
-- Create date: 2011-6-13
-- Description:	系统模块，取得分页数据
-- =============================================

CREATE procedure [LES].[PROC_SYS_GET_VIEW_DATA_PAGING]  	 
 (    
	 @SqlString nvarchar(max),  
	 @OrderBy varchar(500),
	 @StartRowIndex int,  
	 @MaximumRows int	 )as
BEGIN
	declare @RecordCount int
	declare @PageIndex int
	SET NOCOUNT ON;  

	IF(@MaximumRows IS NULL OR @MaximumRows=0) SET @MaximumRows=10  
	
	Set @PageIndex = @StartRowIndex/@MaximumRows;
	Set @PageIndex = @PageIndex + 1
	
	IF(@PageIndex IS NULL OR @PageIndex<1) SET @PageIndex=1  
	
	DECLARE @IsLast int  
	
	DECLARE @ExecSqlString nvarchar(max)

	SET @ExecSqlString ='select @RecordCount=count(1) 
					from (' + @SqlString +') AS V_Count'
	
	EXECUTE sp_executesql @ExecSqlString,N'@RecordCount int output ',@RecordCount OUTPUT  
	--PRINT @RecordCount

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
		SET @ExecSqlString =   
		N'WITH CTE AS 
		( 
			SELECT TOP ' + CAST(@PageIndex * @MaximumRows AS VARCHAR) + ' *,row_number() OVER(ORDER BY '+@OrderBy+ N') AS RowNumber   
			FROM (  
			' + @SqlString + N'
			) AS T1
		) 
		SELECT * 
		FROM CTE
		WHERE RowNumber > ' + CAST((@PageIndex - 1) * @MaximumRows AS VARCHAR) + '
		ORDER BY RowNumber' 
		PRINT @ExecSqlString
		EXECUTE sp_executesql @ExecSqlString,N'@PageIndex int,@MaximumRows int',@PageIndex,@MaximumRows   
	END  
	ELSE  
	BEGIN  
		SET @ExecSqlString =   
		@SqlString + N'  
		ORDER BY ' + @OrderBy  
		PRINT @ExecSqlString
		EXECUTE sp_executesql @ExecSqlString  
	END  
	
	SET NOCOUNT OFF;  
END