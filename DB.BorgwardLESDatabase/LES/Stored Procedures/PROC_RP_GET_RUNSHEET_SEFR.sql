﻿



/********************************************************************/
/*   Project Name:  [PROC_RP_GET_RUNSHEET_SEFR]						*/
/*   Program Name:													*/
/*   Called By:     by the 	Day						*/
/*   Purpose:       拉动单满足率日报表 (只限于TWD拉动单)获取拉动单满足率日报表(Day)								*/
/*   author:       吴庆超	2017-12-11   				        */
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_RP_GET_RUNSHEET_SEFR] (
    @BEGINDATETIME DATETIME,
    @ENDDATETIME DATETIME,
    @CREATE_USER VARCHAR(20),
    @COMMENTS VARCHAR(200))
AS
BEGIN
 
	---执行当天的数据
    INSERT INTO LES.RP_TWD_RUNSHEET_SEFR (PLANT, ASSEMBLY_LINE,DELIVERY_LOCATION, PLANT_ZONE, WORKSHOP, TWD_RUNSHEET_NO,SHEET_STATUS, RUNSHEET_TYPE,
                                          PART_NO, PART_CNAME, SUPPLIER_NUM, SUPPLIER_NAME, BOX_PARTS, REQUIRED_NUMBER,
                                          ACTUAL_NUMBER, SEFR, COMMENTS, BYMONTH,BYYEAR, UPDATE_DATE, UPDATE_USER,
                                          CREATE_DATE, CREATE_USER)
         (SELECT A.PLANT, A.ASSEMBLY_LINE, A.DELIVERY_LOCATION,--B.BOX_PARTS_SHEET,
                 A.PLANT_ZONE AS ZONENO, A.WORKSHOP, A.TWD_RUNSHEET_NO,a.SHEET_STATUS, A.RUNSHEET_TYPE, B.PART_NO, B.PART_CNAME,
                 A.SUPPLIER_NUM, C.SUPPLIER_NAME AS SUPPLIER_SNAME, B.BOX_PARTS,
                 B.REQUIRED_INBOUND_PACKAGE AS REQUIRED_NUMBER,
                 B.ACTUAL_INBOUND_PACKAGE AS ACTUAL_NUMBER, --(B.ACTUAL_INBOUND_PACKAGE / B.REQUIRED_INBOUND_PACKAGE) 按时,
                 CASE
                      WHEN B.REQUIRED_INBOUND_PACKAGE = 0 THEN '0'
                      WHEN B.REQUIRED_INBOUND_PACKAGE > 0 THEN
                          CONVERT(
                          VARCHAR(10),
                          ROUND(
                          CONVERT(FLOAT, B.ACTUAL_INBOUND_PACKAGE) / CONVERT(FLOAT, B.REQUIRED_INBOUND_PACKAGE) * 100,
                          2)) + '%' END AS SEFR, @COMMENTS, (SELECT DATEPART(MONTH, @ENDDATETIME) AS 'BYMONTH'),(SELECT DATEPART(YEAR, @ENDDATETIME) AS 'BYYEAR'),
                 --(round(t1.cnt/t2.totalCount*100,2))||'%' 所占总量百分比,
                 '' AS UPDATE_DATE, '' AS UPDATE_USER, A.CREATE_DATE, @CREATE_USER
            FROM LES.TT_TWD_RUNSHEET A
            LEFT JOIN LES.TT_TWD_RUNSHEET_DETAIL B
              ON A.TWD_RUNSHEET_SN = B.TWD_RUNSHEET_SN
            LEFT JOIN LES.TM_BAS_SUPPLIER C
              ON A.SUPPLIER_NUM    = C.SUPPLIER_NUM
           WHERE A.CREATE_DATE > @BEGINDATETIME              AND A.CREATE_DATE < @ENDDATETIME );
           --WHERE A.CREATE_DATE > '2017-10-11 00:00:00.000'
            -- AND A.CREATE_DATE < '2017-10-12 00:00:00.000');


	DECLARE @TmYEAR INT;
	DECLARE @TmMonth INT;
    DECLARE @TmToday INT;	
	DECLARE @TmNow  DATETIME;
	DECLARE @ByMonthCount INT;
	--DECLARE @ENDDATETIME DATETIME;
	--SET @ENDDATETIME	=GETDATE();
	
	SET @TmYEAR=(SELECT DATEPART(YEAR, @ENDDATETIME)  )
	SET @TmMonth=(SELECT DATEPART(MONTH, @ENDDATETIME)-1 );
    SET @TmToday = (SELECT DATEPART(DAY, @ENDDATETIME));

	PRINT 'TmMonth'
	PRINT @TmMonth

	PRINT 'today'
		PRINT @TmToday

	--处理跨年的1月份问题
	IF(@TmMonth=0)
	BEGIN
		PRINT '修改时间'
		PRINT @TmMonth
		PRINT @TmYEAR
		SET @TmMonth=12;
		SET @TmYEAR-=1;
	END
    
		
		PRINT 'TmYEAR+TmMonth'

		PRINT @TmMonth
		PRINT @TmYEAR
	  --每月1号计算月数据
    IF (@TmToday = 1)
      BEGIN
		SET @ByMonthCount=( SELECT  COUNT(*) FROM LES.RP_TWD_RUNSHEET_SEFR WHERE BYMONTH=@TmMonth AND BYYEAR=@TmYEAR)

		 

			PRINT 'ByMonthCount==='
			PRINT @ByMonthCount

		IF(@ByMonthCount>0)
		BEGIN
			PRINT '开始插入月数据'
			 INSERT INTO  LES.RP_TWD_RUNSHEET_SEFR_ByMonth (PLANT, ASSEMBLY_LINE,DELIVERY_LOCATION, PLANT_ZONE,   RUNSHEET_TYPE,
                                              PART_NO, PART_CNAM,BOX_PARTS, SUPPLIER_NUM, SUPPLIER_NAME,  
                                              REQUIRED_NUMBER, ACTUAL_NUMBER, SEFR, YEARLY, MONTHLY,  
                                                CREATE_DATE, CREATE_USER)
			(
			SELECT PLANT,ASSEMBLY_LINE,DELIVERY_LOCATION,PLANT_ZONE,RUNSHEET_TYPE, PART_NO,PART_CNAME,BOX_PARTS,SUPPLIER_NUM,SUPPLIER_NAME, SUM(REQUIRED_NUMBER)REQUIRED_NUMBER ,SUM(ACTUAL_NUMBER) AS ACTUAL_NUMBER ,
			CASE
								  WHEN SUM(ACTUAL_NUMBER) = 0 THEN '0'
								  WHEN  SUM(ACTUAL_NUMBER)> 0 THEN
									  CONVERT(
									  VARCHAR(10),
									  ROUND(
									  CONVERT(FLOAT,SUM(ACTUAL_NUMBER) ) / CONVERT(FLOAT, SUM(REQUIRED_NUMBER)) * 100,
									  2)) + '%' END AS SEFR ,
			@TmYEAR AS YEARLY,	@TmMonth AS MONTHLY,
			GETDATE() AS CREATE_DATE, N'admin' AS CREATE_USER
			FROM LES.RP_TWD_RUNSHEET_SEFR GROUP BY PLANT,ASSEMBLY_LINE,DELIVERY_LOCATION,PLANT_ZONE,RUNSHEET_TYPE, PART_NO,PART_CNAME,BOX_PARTS,SUPPLIER_NUM,SUPPLIER_NAME 
			)
		END
        
	  END 
END;