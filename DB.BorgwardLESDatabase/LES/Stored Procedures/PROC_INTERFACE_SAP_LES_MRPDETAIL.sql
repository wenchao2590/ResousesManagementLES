


--SAP接口_APO_采购预测需求
CREATE PROCEDURE [LES].[PROC_INTERFACE_SAP_LES_MRPDETAIL]
AS
BEGIN
    --业务逻辑,每次下发采购预测，版本批次号格式为000000000016-001，其中000000000016为版本号，001为批次号码
	--LES.TT_SPM_MRP_DETAIL表中只保留最大的版本号（@INVERSION），其余版本号删除，同一版本号不同批次号（@INSUBVERSION）做合并
	BEGIN TRANSACTION
	BEGIN TRY
		--取最后插入接口表LES.TI_SPM_MRP_IN中的未处理数据
		DECLARE @INSEQ_ID AS INT = NULL
		DECLARE @INZDOCNUM AS NVARCHAR(16)
		DECLARE @INVERSION AS INT
		DECLARE @INSUBVERSION AS INT

		SELECT TOP 1 @INSEQ_ID=SEQ_ID
			        ,@INZDOCNUM=ZDOCNUM
			    FROM LES.TI_SPM_MRP_IN
			   WHERE ISNULL(PROCESS_FLAG,0)=0
			   ORDER BY SEQ_ID DESC

		--如果接口表存在未处理记录
		--IF EXISTS(SELECT TOP 1 * FROM LES.TI_SPM_MRP_IN WHERE ISNULL(PROCESS_FLAG,0)=0)
		IF (@INSEQ_ID IS NOT NULL)
		BEGIN
			--000000000016-001
			SET @INVERSION=CAST(SUBSTRING(@INZDOCNUM,1,CHARINDEX('-',@INZDOCNUM)-1) AS INT)
			SET @INSUBVERSION=CAST(SUBSTRING(@INZDOCNUM,CHARINDEX('-',@INZDOCNUM)+1,3) AS INT)

			--print('INVERSION: ' + cast(@INVERSION as nvarchar(50)))

			--取最后进入业务表LES.TT_SPM_MRP_DETAIL数据
			DECLARE @SEQ_ID AS INT
			DECLARE @ZDOCNUM AS NVARCHAR(16)
			DECLARE @VERSION AS INT
			DECLARE @SUBVERSION AS INT

			SELECT TOP 1 @SEQ_ID=SEQ_ID
			            ,@ZDOCNUM=PLAN_NO
			        FROM LES.TT_SPM_MRP_DETAIL
			       ORDER BY SEQ_ID DESC

			 --print('ZDOCNUM: ' + @ZDOCNUM)
			 --如果不是首次下发
			 IF(IsNull(@ZDOCNUM,'')!='')
			 Begin
			 	SET @VERSION=CAST(SUBSTRING(@ZDOCNUM,1,CHARINDEX('-',@ZDOCNUM)-1) AS INT)
			    SET @SUBVERSION=CAST(SUBSTRING(@ZDOCNUM,CHARINDEX('-',@ZDOCNUM)+1,3) AS INT)				
			 End

			 --print('VERSION: ' + cast(@VERSION as nvarchar(50)))

			--如果是首次下发或新版本号码大，则删除现有业务数据，将接口中数据插入业务表
			IF(IsNull(@ZDOCNUM,'')='' OR @INVERSION>@VERSION)
			Begin
				Delete LES.TT_SPM_MRP_DETAIL Where SEQ_ID<=@SEQ_ID
				--print('SEQ_ID: ' + cast(@SEQ_ID as nvarchar(50)))
				--插入数据
				INSERT INTO LES.TT_SPM_MRP_DETAIL (
						PLAN_NO ,
						PLANT ,
						SUPPLIER_NUM ,
						PART_NO ,
						PART_CNAME ,
						INBOUND_PACKAGE_MODEL ,
						INBOUND_PACKAGE ,
						REQUIRE_DATE ,
						REQUIRE_WEEK,
						IS_MEET ,
						CREATE_USER ,
						CREATE_DATE
					)
					SELECT	ZDOCNUM , -- PLAN_NO - varchar(20)
							ZKWERK , -- PLANT - nvarchar(5)
							ZPARTN , -- SUPPLIER_NUM - nvarchar(8)
							ZIDNKD , -- PART_NO - nvarchar(20)
							b.PART_CNAME , -- PART_CNAME - nvarchar(100)
							ZVRKME , -- INBOUND_PACKAGE_MODEL - nvarchar(30)
							CAST(ZWMENG AS int) , -- INBOUND_PACKAGE - int
							ZEDATUV , -- REQUIRE_DATE - datetime
							datename(week,ZEDATUV) , -- REQUIRE_WEEK - nvarchar(20)
							3 , -- IS_MEET - int
							'admin' , -- CREATE_USER - nvarchar(50)
							GETDATE()  -- CREATE_DATE - datetime
					   FROM LES.TI_SPM_MRP_IN a
				 LEFT JOIN (SELECT DISTINCT PART_NO,part_cName FROM Les.TM_BAS_MAINTAIN_PARTS) b ON (b.PART_NO = a.ZIDNKD)
					  Where ISNULL(a.PROCESS_FLAG,0)=0
						And SEQ_ID<=@INSEQ_ID
						And CAST(SUBSTRING(ZDOCNUM,1,CHARINDEX('-',ZDOCNUM)-1) AS INT)=@INVERSION

						Update LES.TI_SPM_MRP_IN 
						   SET PROCESS_FLAG=1
							  ,PROCESS_TIME=Getdate() 
						 Where ISNULL(PROCESS_FLAG,0)=0
						   And SEQ_ID<=@INSEQ_ID
						   And CAST(SUBSTRING(ZDOCNUM,1,CHARINDEX('-',ZDOCNUM)-1) AS INT)=@INVERSION

			End
			Else IF(@INVERSION=@VERSION)--如果版本号相同
			Begin

			  --插入数据
				INSERT INTO LES.TT_SPM_MRP_DETAIL
						  ( PLAN_NO ,
							PLANT ,
							SUPPLIER_NUM ,
							PART_NO ,
							PART_CNAME ,
							INBOUND_PACKAGE_MODEL ,
							INBOUND_PACKAGE ,
							REQUIRE_DATE ,
							REQUIRE_WEEK,
							IS_MEET ,
							CREATE_USER ,
							CREATE_DATE)
				  SELECT	ZDOCNUM , -- PLAN_NO - varchar(20)
							ZKWERK , -- PLANT - nvarchar(5)
							ZPARTN , -- SUPPLIER_NUM - nvarchar(8)
							ZIDNKD , -- PART_NO - nvarchar(20)
							b.PART_CNAME , -- PART_CNAME - nvarchar(100)
							ZVRKME , -- INBOUND_PACKAGE_MODEL - nvarchar(30)
							CAST(ZWMENG AS int) , -- INBOUND_PACKAGE - int
							ZEDATUV , -- REQUIRE_DATE - datetime
							datename(week,ZEDATUV) , -- REQUIRE_WEEK - nvarchar(20)
							3 , -- IS_MEET - int
							'admin' , -- CREATE_USER - nvarchar(50)
							GETDATE()  -- CREATE_DATE - datetime
				FROM LES.TI_SPM_MRP_IN a
		   LEFT JOIN (SELECT DISTINCT PART_NO,part_cName FROM Les.TM_BAS_MAINTAIN_PARTS) b ON (b.PART_NO = a.ZIDNKD)
			   Where ISNULL(a.PROCESS_FLAG,0)=0
				 And SEQ_ID<=@INSEQ_ID
				 And CAST(SUBSTRING(ZDOCNUM,1,CHARINDEX('-',ZDOCNUM)-1) AS INT)=@INVERSION

				Update LES.TI_SPM_MRP_IN 
				   SET PROCESS_FLAG=1
				      ,PROCESS_TIME=Getdate() 
				 Where ISNULL(PROCESS_FLAG,0)=0
				    And SEQ_ID<=@INSEQ_ID 
					And CAST(SUBSTRING(ZDOCNUM,1,CHARINDEX('-',ZDOCNUM)-1) AS INT)=@INVERSION
			End
			Else IF(@INVERSION<@VERSION)--如果新版本号小于业务表版本号
			Begin
				Update LES.TI_SPM_MRP_IN 
					SET PROCESS_FLAG=1
						,PROCESS_TIME=Getdate() 
					Where ISNULL(PROCESS_FLAG,0)=0
					And SEQ_ID<=@INSEQ_ID 
					And CAST(SUBSTRING(ZDOCNUM,1,CHARINDEX('-',ZDOCNUM)-1) AS INT)=@INVERSION
			End

			--删除重复的数据，以零件号、供应商、日期做参照
			--Delete A 
			--from LES.TT_SPM_MRP_DETAIL A 
			--inner join LES.TI_SPM_MRP_IN B 
			--on B.ZPARTN = A.SUPPLIER_NUM and B.ZIDNKD = A.PART_NO and B.ZEDATUV = convert(nvarchar(8),A.REQUIRE_DATE,112)
			--where B.PROCESS_FLAG = 0

		END

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		--出错，则返回执行不成功，回滚事务
		ROLLBACK TRANSACTION

		--记录错误信息
		INSERT INTO [LES].[TS_SYS_EXCEPTION] (time_stamp, [application], [METHOD], class,  exception_message, error_code)
			SELECT getdate(),'INTERFACE','PROC_INTERFACE_SAP_LES_MRPDETAIL','Procedure',error_message(),ERROR_LINE()

		--避免错误数据重复执行
		 Update LES.TI_SPM_MRP_IN 
			SET PROCESS_FLAG=9
			   ,PROCESS_TIME=Getdate() 
		  Where ISNULL(PROCESS_FLAG,0)=0
		    And SEQ_ID<=@INSEQ_ID 
			And CAST(SUBSTRING(ZDOCNUM,1,CHARINDEX('-',ZDOCNUM)-1) AS INT)=@INVERSION

		Print(error_message())
	END CATCH
END