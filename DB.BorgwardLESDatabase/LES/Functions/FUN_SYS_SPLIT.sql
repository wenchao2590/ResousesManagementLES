
 Create FUNCTION [LES].[FUN_SYS_SPLIT]
  (
      @SplitString nvarchar(max),
      @Separator nvarchar(10)=' '
  )
  RETURNS @SplitStringsTable TABLE
  (
  [Id] int identity(1,1),
  [Split_Value] nvarchar(max)
 )
 AS
 BEGIN
     DECLARE @CurrentIndex int;
     DECLARE @NextIndex int;
     DECLARE @ReturnText nvarchar(max);
     SELECT @CurrentIndex=1;
     WHILE(@CurrentIndex<=len(@SplitString))
         BEGIN
             SELECT @NextIndex=charindex(@Separator,@SplitString,@CurrentIndex);
             IF(@NextIndex=0 OR @NextIndex IS NULL)
                 SELECT @NextIndex=len(@SplitString)+1;
                 SELECT @ReturnText=substring(@SplitString,@CurrentIndex,@NextIndex-@CurrentIndex);
                 IF(@ReturnText<>'')
                 BEGIN
					 INSERT INTO @SplitStringsTable(Split_Value) VALUES(@ReturnText);
                 END
                 SELECT @CurrentIndex=@NextIndex+1;
             END
     RETURN;
 END