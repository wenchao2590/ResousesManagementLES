
CREATE PROC [LES].[PROC_LargeScreen_Dock]
AS
BEGIN	
	DECLARE @Table1 TABLE
	(
		Dock VARCHAR(10),
		Unloading NVARCHAR(50),
		Waiting1 NVARCHAR(50),
		Waiting2 NVARCHAR(50),
		Waiting3 NVARCHAR(50)
	)

	DECLARE @Dock NVARCHAR(20);
	DECLARE SEQUENCE_CURS0  CURSOR FOR
		SELECT distinct ORIGINAL_DOCK FROM LES.TT_CMM_SUPPLIER_SEQUENCE WHERE [status] IN(0,1) 
	OPEN SEQUENCE_CURS0
	FETCH NEXT FROM SEQUENCE_CURS0 INTO @Dock
	WHILE( @@fetch_status = 0 )
	BEGIN
		--卸货供应商
		DECLARE @Unloading NVARCHAR(50);
		SET @Unloading='空闲'
		SELECT TOP 1 @Unloading =t_supplier.SUPPLIER_NAME  FROM LES.TT_CMM_SUPPLIER_SEQUENCE t_sequence 
			INNER JOIN les.TM_BAS_SUPPLIER t_supplier
			ON t_supplier.SUPPLIER_NUM = t_sequence.SUPPLIER_NUM
			 WHERE t_sequence.STATUS=1 AND t_sequence.ORIGINAL_DOCK=@Dock
		
		--等待中1
		DECLARE @Waiting1 NVARCHAR(50);
		SET @Waiting1='空闲'
		SELECT TOP 1 @Waiting1 =Table1.SUPPLIER_NAME  FROM 
		(SELECT t_supplier.SUPPLIER_NAME
				,ROW_NUMBER() OVER(ORDER BY  t_sequence.FIRST_LINE_SN,t_sequence.LINE_SN DESC) RowNUM
				FROM LES.TT_CMM_SUPPLIER_SEQUENCE t_sequence 
				INNER JOIN les.TM_BAS_SUPPLIER t_supplier
				ON t_supplier.SUPPLIER_NUM = t_sequence.SUPPLIER_NUM
				 WHERE t_sequence.status =0  AND t_sequence.ORIGINAL_DOCK=@Dock) Table1
		WHERE Table1.RowNUM=1
        
		
		--等待中2
		DECLARE @Waiting2 NVARCHAR(50);
		SET @Waiting2='空闲'
		SELECT TOP 1 @Waiting1 =Table1.SUPPLIER_NAME  FROM 
		(SELECT t_supplier.SUPPLIER_NAME
				,ROW_NUMBER() OVER(ORDER BY  t_sequence.FIRST_LINE_SN,t_sequence.LINE_SN DESC) RowNUM
				FROM LES.TT_CMM_SUPPLIER_SEQUENCE t_sequence 
				INNER JOIN les.TM_BAS_SUPPLIER t_supplier
				ON t_supplier.SUPPLIER_NUM = t_sequence.SUPPLIER_NUM
				 WHERE t_sequence.status =0  AND t_sequence.ORIGINAL_DOCK=@Dock) Table1
		WHERE Table1.RowNUM=2
		
		--等待中3
		DECLARE @Waiting3 NVARCHAR(50);
		SET @Waiting3='空闲'
		SELECT TOP 1 @Waiting1 =Table1.SUPPLIER_NAME  FROM 
		(SELECT t_supplier.SUPPLIER_NAME
				,ROW_NUMBER() OVER(ORDER BY  t_sequence.FIRST_LINE_SN,t_sequence.LINE_SN DESC) RowNUM
				FROM LES.TT_CMM_SUPPLIER_SEQUENCE t_sequence 
				INNER JOIN les.TM_BAS_SUPPLIER t_supplier
				ON t_supplier.SUPPLIER_NUM = t_sequence.SUPPLIER_NUM
				 WHERE t_sequence.status =0  AND t_sequence.ORIGINAL_DOCK=@Dock) Table1
		WHERE Table1.RowNUM=3

		--插入变量表Table1
		INSERT INTO @Table1
		        ( Dock ,
		          Unloading ,
		          Waiting1 ,
		          Waiting2 ,
		          Waiting3
		        )
		VALUES  ( @Dock , -- Dock - varchar(10)
		          @Unloading , -- Unloading - nvarchar(50)
		          @Waiting1 , -- Waiting1 - nvarchar(50)
		          @Waiting2 , -- Waiting2 - nvarchar(50)
		          @Waiting3  -- Waiting3 - nvarchar(50)
		        )
	FETCH NEXT FROM SEQUENCE_CURS0 INTO @Dock
	END
	CLOSE  SEQUENCE_CURS0
	DEALLOCATE SEQUENCE_CURS0

	SELECT 
	Dock,
	LEFT(Unloading,6) AS Unloading,
	LEFT(Waiting1,6) AS Waiting1,
	LEFT(Waiting2,6) AS Waiting2,
	LEFT(Waiting3,6) AS Waiting3 
	 FROM @Table1;

END;

--EXEC LES.PROC_LargeScreen_Dock