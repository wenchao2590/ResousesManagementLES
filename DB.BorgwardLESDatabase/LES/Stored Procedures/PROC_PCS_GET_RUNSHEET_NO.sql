/********************************************************************/
/*   Project Name:  [PROC_PCS_GET_RUNSHEET_NO]						*/
/*   Program Name:													*/
/*   Called By:     by the 	generate runsheet						*/
/*   Purpose:       get request data								*/
/*   author:       xuehaijun	2011-07-2   				        */
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_PCS_GET_RUNSHEET_NO]
(
	@plant NVARCHAR(5),
	@assemblyLine NVARCHAR(10),
	@supplierNum NVARCHAR(5),
	@result NVARCHAR(30) OUTPUT,
	@billType NVARCHAR(1) = 'E'
)
AS 
BEGIN
	DECLARE @seqNo INT
	EXEC @seqNo = [LES].[PROC_PCS_GET_NEXT_SEQUENCE] 'pcs_pull_runsheet_no'
	DECLARE @seqNoString NVARCHAR(4)
	SET @seqNoString = RIGHT(CONVERT(NVARCHAR(10), @seqNo), 4)
	SET @seqNoString = REPLACE(SPACE(4 - LEN(@seqNoString)), ' ', '0') + @seqNoString
	
	--declare @assemblyNickName nvarchar(10)
	--select @assemblyNickName  = [ASSEMBLY_LINE_NICKNAME] from LES.[TM_BAS_ASSEMBLY_LINE] where [ASSEMBLY_LINE] = @assemblyLine and plant = @plant
	
	--declare @result nvarchar(30)
	DECLARE @runsheetNo NVARCHAR(30)
	--规则：改为“两位车间（78）+六位年份(yyMMdd)+四位流水号（1234）”
    SET @runsheetNo = @assemblyLine + CONVERT(VARCHAR(6),GETDATE(),12) + @billType + @seqNoString
	--print @runsheetNo
	SET @result =@runsheetNo
END