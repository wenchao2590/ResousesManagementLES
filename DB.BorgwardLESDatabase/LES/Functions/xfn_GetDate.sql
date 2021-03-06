﻿CREATE Function [LES].[xfn_GetDate](@YEAR INT,@WEEK INT,@DAY INT)
RETURNS DATETIME
AS
BEGIN

 DECLARE @RST DATETIME
 SET @RST=CAST( (CAST(@YEAR AS VARCHAR(20))+'-01-01') AS DATETIME)
 WHILE @RST<CAST( (CAST(@YEAR AS VARCHAR(20))+'-12-31') AS DATETIME)
 BEGIN
  IF @WEEK=DATEPART(WW,@RST) AND @DAY=DATEPART(DW,@RST)
   RETURN @RST
  SET @RST=DATEADD(DD,1,@RST)
 END
 RETURN @RST
END