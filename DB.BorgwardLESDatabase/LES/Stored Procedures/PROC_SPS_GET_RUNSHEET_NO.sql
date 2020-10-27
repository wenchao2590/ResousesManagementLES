/****************************************************/
/* Author:		孙述霄								*/
/* Create date: 2017-10-13							*/
/* Description:	SPS 生成拉动单号					*/
/****************************************************/
CREATE PROCEDURE [LES].[PROC_SPS_GET_RUNSHEET_NO]
(
    @plant NVARCHAR(5),
	@assemblyLine NVARCHAR(10),
    @supplierNum NVARCHAR(5),
    @result NVARCHAR(30) OUTPUT,
	@billType NVARCHAR(3) = 'K'
)
AS
BEGIN
    DECLARE @seqNo INT
    EXEC @seqNo = [LES].[PROC_SPS_GET_NEXT_SEQUENCE] 'SPS_RUNSHEET_NO_SEQ'
    DECLARE @seqNoString NVARCHAR(5)
    SET @seqNoString = CONVERT(NVARCHAR(5), @seqNo)
    SET @seqNoString = REPLACE(SPACE(5 - LEN(@seqNoString)), ' ', '0') + @seqNoString
	
    DECLARE @runsheetNo NVARCHAR(30)
	--规则：改为“两位车间（78）+六位年份(yyMMdd)+5位流水号（1234）”
    SET @runsheetNo = @billType + CONVERT(VARCHAR(6),GETDATE(),12) + @seqNoString
    SET @result = @runsheetNo
END