﻿
--modify by 【运维】whk 2014/8/12 【CMDB编号：CR-LES-20140807】 start
--根据“零件号，零件昵称，拉动单序号”获取单元格序号
--ALTER  Function   [LES].[Func_GetBoxListByPartNO](@part_no varchar(20),@jis_runsheet_sn int) 
CREATE FUNCTION [LES].[Func_GetBoxListByPartNOAndNickNameUsage]
(
	@PART_NO			VARCHAR(20),
	@JIS_RUNSHEET_SN	INT, 
	@PART_NICK_NAME		VARCHAR(30),
	@Usage				NUMERIC(18,2)
)   
RETURNS   VARCHAR(200)   
AS   
BEGIN   
	DECLARE @SQL VARCHAR(1000)   
	SET   @SQL=''   
	SELECT @SQL=@SQL+' '+ CAST(A.JIS_BOX_SN AS VARCHAR)
	FROM 
	(
		SELECT DISTINCT B.JIS_BOX_SN 
		FROM LES.TT_JIS_RUNSHEET_FLEX B
		INNER JOIN LES.TT_JIS_RUNSHEET_DETAIL P  ON  P.JIS_RUNSHEET_FLEX_SN=B.JIS_RUNSHEET_FLEX_SN
		INNER JOIN LES.TT_JIS_RUNSHEET D ON B.JIS_RUNSHEET_SN = D.JIS_RUNSHEET_SN 
		--WHERE P.PART_NO=@PART_NO AND D.JIS_RUNSHEET_SN=@JIS_RUNSHEET_SN) A
		WHERE P.PART_NO = @PART_NO
			AND D.JIS_RUNSHEET_SN = @JIS_RUNSHEET_SN
			AND P.PART_NICK_NAME = @PART_NICK_NAME
			AND P.USAGE = @Usage
	) A
	
	RETURN LTRIM(RTRIM(@SQL))
END
  --modify by 【运维】whk 2014/8/12 end