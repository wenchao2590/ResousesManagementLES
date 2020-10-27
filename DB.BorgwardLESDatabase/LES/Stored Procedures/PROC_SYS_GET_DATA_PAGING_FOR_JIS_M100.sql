
-- =============================================
-- Author:		吕小望
-- Create date: 2011-10-13
-- Description:	生成分页数据
-- =============================================
CREATE procedure [LES].[PROC_SYS_GET_DATA_PAGING_FOR_JIS_M100]  	 
 (    @SelectColumn nvarchar(max),  
	 @FromJoin nvarchar(max),  
	 @WhereCond nvarchar(max),  
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
		
	IF(@OrderBy IS NULL ) SET @OrderBy=''
	IF(@WhereCond IS NULL) SET @WhereCond=''
	
	DECLARE @IsLast int  
	
	DECLARE @SQLString nvarchar(max)
	
	--PRINT convert(varchar(20),getdate(),20)
	
	SET @SQLString=	' SELECT @RecordCount = COUNT(*) ' + '
					from ' + @FromJoin + ' 
					WHERE 1=1 
					' + @WhereCond
    SET @SQLString=@SQLString+' OPTION (FORCE ORDER, LOOP JOIN)'
	EXECUTE sp_executesql @SQLString,N'@RecordCount int output ',@RecordCount OUTPUT  
	
	--PRINT convert(varchar(20),getdate(),20)
	
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
		SET @SQLString =   
		N'WITH CTE AS 
		( 
			SELECT TOP ' + CAST(@PageIndex * @MaximumRows AS VARCHAR) + ' *,row_number() OVER(ORDER BY '+@OrderBy+ N') AS RowNumber   
			FROM (  
			SELECT ' + @SelectColumn + N'  
			FROM ' + @FromJoin + N'  
			WHERE 1=1  ' + @WhereCond + N'  
			) AS T1
		) 
		SELECT * 
		FROM CTE
		WHERE RowNumber > ' + CAST((@PageIndex - 1) * @MaximumRows AS VARCHAR) + '
		ORDER BY RowNumber' 
		SET @SQLString=@SQLString+' OPTION (FORCE ORDER, LOOP JOIN)'
		--PRINT @SQLString
		--PRINT  convert(varchar(20),getdate(),20)
		EXECUTE sp_executesql @SQLString,N'@PageIndex int,@MaximumRows int',@PageIndex,@MaximumRows   
		--PRINT convert(varchar(20),getdate(),20)
	END  
	ELSE  
	BEGIN  
		SET @SQLString =   
		N'SELECT ' + @SelectColumn + N'  
		FROM ' + @FromJoin + N'  
		WHERE 1=1  ' + @WhereCond + N'  
		ORDER BY ' + @OrderBy 
		SET @SQLString=@SQLString+' OPTION (FORCE ORDER, LOOP JOIN)' 
		--PRINT @SQLString
		EXECUTE sp_executesql @SQLString  
	END  

	SET NOCOUNT OFF;  
END