

-- =============================================
-- Author:		吕小望
-- Create date: 2011-10-13
-- Description:	生成分页数据
-- =============================================
CREATE procedure [LES].[PROC_SYS_GET_DATA_PAGING]  	 
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
	
	PRINT convert(varchar(20),getdate(),20)
	
	SET @SQLString=	' SELECT @RecordCount = COUNT(*) ' + '
					from ' + @FromJoin + ' 
					WHERE 1=1 
					' + @WhereCond

	EXECUTE sp_executesql @SQLString,N'@RecordCount int output ',@RecordCount OUTPUT  
	
	PRINT convert(varchar(20),getdate(),20)
	
	PRINT @SQLString
	
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
			N'select * from (
		select ' + @SelectColumn + N',
		Row_number() over(ORDER BY '+@OrderBy+ N') as RowNumber 
		from ' + @FromJoin + N'
		where 1=1  ' + @WhereCond + N' 
		) as T1 
		WHERE RowNumber>' + CAST((@PageIndex - 1) * @MaximumRows AS VARCHAR) + ' and RowNumber<=' + CAST(@PageIndex * @MaximumRows AS VARCHAR) 
		PRINT @SQLString
		PRINT  convert(varchar(20),getdate(),20)
		EXECUTE sp_executesql @SQLString,N'@PageIndex int,@MaximumRows int',@PageIndex,@MaximumRows   
		PRINT convert(varchar(20),getdate(),20)
	END  
	ELSE  
	BEGIN  
		SET @SQLString =   
		N'SELECT ' + @SelectColumn + N'  
		FROM ' + @FromJoin + N'  
		WHERE 1=1  ' + @WhereCond + N'  
		ORDER BY ' + @OrderBy  
		--PRINT @SQLString
		EXECUTE sp_executesql @SQLString  
	END  

	SET NOCOUNT OFF;  
END