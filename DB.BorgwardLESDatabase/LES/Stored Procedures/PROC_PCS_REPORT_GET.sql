
/********************************************************************/
/*                                                                  */
/*   Project Name:  Production Pull System                          */
/*   Program Name:  [[PROC_PCS_REPORT_GET]]             */
/*   Called By:     by the Page							*/
/*   Purpose:       This is the main stored procedure for the       */
/*   author:       wangchanghong	2011-06-20   				       */
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_PCS_REPORT_GET]

(
	@PCS_RUNSHEET_SN_LIST NVARCHAR(MAX)	
)

AS
BEGIN


SET NOCOUNT ON
DECLARE @PCS_RUNSHEET_SN nvarchar(100)
DECLARE @PCS_RUNSHEET_NO nvarchar(100)
DECLARE @PCS_COUNTS INT = 0 --总记录数
DECLARE @PCS_NEEDS INT = 0--需补充行数

CREATE TABLE LES.#TTT_PCS_RUNSHEET(
            [PCS_RUNSHEET_SN] nvarchar(100) null,[PCS_RUNSHEET_NO] nvarchar(100) null
           ,[PLANT] nvarchar(100) null,[ASSEMBLY_LINE] nvarchar(100) null
           ,DOCK nvarchar(100) null,BOX_PARTS nvarchar(100) null
           ,PUBLISH_TIME datetime null ,EXPECTED_ARRIVAL_TIME datetime null
           ,PART_NO nvarchar(500) null ,LOCATION nvarchar(100) null
           ,PART_CNAME nvarchar(100) null ,INHOUSE_PACKAGE_MODEL nvarchar(100) null
           ,INHOUSE_PACKAGE nvarchar(100) null ,PACK_COUNT int
           ,REQUIRED_INHOUSE_PACKAGE nvarchar(100))    

CREATE TABLE #Temp_PCS_RUNSHEET_SN
(
	PCS_RUNSHEET_SN	INT,
	PCS_RUNSHEET_NO NVARCHAR(30)
)
CREATE INDEX IX_#Temp_PCS_RUNSHEET_SN_1 ON #Temp_PCS_RUNSHEET_SN(PCS_RUNSHEET_SN)

DECLARE @SQLString NVARCHAR(MAX)

SET @SQLString=	' INSERT INTO #Temp_PCS_RUNSHEET_SN' + 
			    ' SELECT PCS_RUNSHEET_SN,PCS_RUNSHEET_NO FROM LES.TT_PCS_RUNSHEET WHERE PCS_RUNSHEET_SN IN (' + @PCS_RUNSHEET_SN_LIST + ')'
			
EXECUTE sp_executesql @SQLString

DECLARE PCS_Cursor CURSOR FOR SELECT PCS_RUNSHEET_SN,PCS_RUNSHEET_NO 
                              FROM #Temp_PCS_RUNSHEET_SN
           
OPEN PCS_Cursor 
FETCH NEXT FROM PCS_Cursor INTO @PCS_RUNSHEET_SN,@PCS_RUNSHEET_NO
WHILE @@FETCH_STATUS = 0
BEGIN
       
SELECT @PCS_COUNTS = COUNT(*) FROM LES.TT_PCS_RUNSHEET_DETAIL WHERE PCS_RUNSHEET_SN = @PCS_RUNSHEET_SN

IF(@PCS_COUNTS%30 <> 0)
BEGIN
	SET @PCS_NEEDS = (30- @PCS_COUNTS%30);  --获取补充空白行数量
END

INSERT INTO LES.#TTT_PCS_RUNSHEET
SELECT  R.[PCS_RUNSHEET_SN] ,[PCS_RUNSHEET_NO] 
       ,R.[PLANT] ,R.[ASSEMBLY_LINE] 
       ,R.DOCK ,R.BOX_PARTS 
       ,PUBLISH_TIME  ,EXPECTED_ARRIVAL_TIME
       ,PART_NO   ,LOCATION 
       ,PART_CNAME  ,INHOUSE_PACKAGE_MODEL 
       ,INHOUSE_PACKAGE  ,PACK_COUNT 
       ,REQUIRED_INHOUSE_PACKAGE            
FROM LES.TT_PCS_RUNSHEET AS R
LEFT JOIN  LES.TT_PCS_RUNSHEET_DETAIL  AS D ON R.PCS_RUNSHEET_SN = D.PCS_RUNSHEET_SN --where R.PCS_RUNSHEET_SN = 1
WHERE R.PCS_RUNSHEET_SN = @PCS_RUNSHEET_SN

DECLARE @i INT = 0

WHILE(@i<@PCS_NEEDS)
BEGIN
	--补空行
	INSERT INTO LES.#TTT_PCS_RUNSHEET
	SELECT  @PCS_RUNSHEET_SN,@PCS_RUNSHEET_NO
           ,NULL ,NULL
           ,NULL ,NULL
		   ,NULL ,NULL
		   ,NULL ,NULL 
		   ,NULL ,NULL 
		   ,NULL ,NULL
		   ,NULL 
    SET @i = @i + 1

END

FETCH NEXT FROM PCS_Cursor INTO  @PCS_RUNSHEET_SN,@PCS_RUNSHEET_NO
END

CLOSE PCS_Cursor
DEALLOCATE PCS_Cursor

SELECT * FROM LES.#TTT_PCS_RUNSHEET ORDER BY PCS_RUNSHEET_SN,PLANT desc

END