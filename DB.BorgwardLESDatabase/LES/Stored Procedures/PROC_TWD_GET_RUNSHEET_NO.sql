-- =============================================
-- Author:		吕小望
-- Create date: 2011-10-13
-- Description:	TWD 生成拉动单号
-- Changed by yinxuefeng 2013-08-13 修改拉动单规则
-- =============================================
CREATE PROC [LES].[PROC_TWD_GET_RUNSHEET_NO]
    (
      @plant NVARCHAR(5) ,
	  @assemblyLine nvarchar(10) ,
      @supplierNum NVARCHAR(5) ,
      @result NVARCHAR(30) OUTPUT,
	  @billType NVARCHAR(1) = 'K'
    )
AS 
    DECLARE @seqNo INT
    EXEC @seqNo = [LES].[PROC_TWD_GET_NEXT_SEQUENCE] 'TWD_RUNSHEET_NO_SEQ'
    DECLARE @seqNoString NVARCHAR(4)
    SET @seqNoString = CONVERT(NVARCHAR(4), @seqNo)
    SET @seqNoString = REPLACE(SPACE(4 - LEN(@seqNoString)), ' ', '0') + @seqNoString
	
    DECLARE @runsheetNo NVARCHAR(30)
	--规则：改为“两位车间（78）+六位年份(yyMMdd)+四位流水号（1234）”
    SET @runsheetNo = @assemblyLine + convert(varchar(6),getdate(),12) + @billType + @seqNoString
    --PRINT @runsheetNo
    SET @result = @runsheetNo