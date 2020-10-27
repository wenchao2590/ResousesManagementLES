
-- =============================================
-- Author:		yinxuefeng
-- Create date: 2013-07-05
-- Description:	TWD 手工生成条码
-- =============================================
CREATE PROC [LES].[PROC_TWD_GENERATE_MANUAL_BARCODE]
    (
      @PLANT_ZONE NVARCHAR(5),
      @WORKSHOP NVARCHAR(4),
      @PLANT NVARCHAR(5),
      @ASSEMBLY_LINE NVARCHAR(10),
      @SUPPLIER_NUM NVARCHAR(12) ,
      @PART_NO NVARCHAR(20) ,
      @IDENTIFY_PART_NO NVARCHAR(20) ,
      @PART_CNAME NVARCHAR(100) ,
      @BOX_PARTS NVARCHAR(10) ,
      @PICKUP_SEQ_NO INT ,
      @RDC_DLOC VARCHAR(20) ,
      @MEASURING_UNIT_NO NVARCHAR(1) ,
      @INHOUSE_PACKAGE_MODEL NVARCHAR(30) ,
      @REQUIRED_INBOUND_PACKAGE_QTY INT ,
      @REQUIRED_QTY INT ,
      @CREATE_USER NVARCHAR(50) ,
      @REQUIRED_DATE DATETIME,
      @ResumeNo NVARCHAR(20)
    )
AS 
    SET NOCOUNT ON
    DECLARE @BATCHNO NVARCHAR(30)
    DECLARE @RunsheetSN NVARCHAR(20)
    SET @RunsheetSN = 0
    SELECT  @BATCHNO = CONVERT(VARCHAR(100), GETDATE(), 112)
	DECLARE @TRANS_SUPPLIER_NUM NVARCHAR(20)
	declare @DELIVERY_LOCATION nvarchar(12)
	
    --DECLARE @REQUIRED_INBOUND_PACKAGE INT= 0
    --DECLARE @LAST_REQUIRED_INBOUND_PACKAGE_QTY INT= 0
    --DECLARE @INBOUND_PACKAGE_QTY INT= @REQUIRED_INBOUND_PACKAGE_QTY
    --DECLARE @REQUIRED_NUM DECIMAL(10, 1)
    --SET @REQUIRED_NUM = CONVERT(DECIMAL, @REQUIRED_QTY)
	--计算箱数
    --SET @REQUIRED_INBOUND_PACKAGE = CEILING(@REQUIRED_NUM / @REQUIRED_INBOUND_PACKAGE_QTY)
	--计算尾箱数量
    --SET @LAST_REQUIRED_INBOUND_PACKAGE_QTY = CEILING(@REQUIRED_NUM % @REQUIRED_INBOUND_PACKAGE_QTY)
    --如果没有余数，则好装满，则尾箱数量=包装数量
    --IF @LAST_REQUIRED_INBOUND_PACKAGE_QTY = 0 
    --    SET @LAST_REQUIRED_INBOUND_PACKAGE_QTY = @REQUIRED_INBOUND_PACKAGE_QTY
	
	SELECT  @TRANS_SUPPLIER_NUM=max([TRANS_SUPPLIER_NUM]),@DELIVERY_LOCATION=Max(WAREHOUSE)  FROM [LES].[TM_TWD_BOX_PARTS] where PLANT=@PLANT and ASSEMBLY_LINE=@ASSEMBLY_LINE and BOX_PARTS=@BOX_PARTS
			
    BEGIN
        WHILE ( @REQUIRED_QTY > 0 ) 
            BEGIN
				--修改尾箱数量
                --IF @REQUIRED_INBOUND_PACKAGE = 1 
                --    SET @INBOUND_PACKAGE_QTY = @LAST_REQUIRED_INBOUND_PACKAGE_QTY
                DECLARE @barCodeNo NVARCHAR(32)
                EXEC LES.[PROC_TWD_GET_NEXT_BARCODE_SEQUENCE] 'BARCODE',
                    @barCodeNo OUTPUT
                INSERT  INTO [LES].[TT_TWD_MANUAL_BARCODE]
                        ( 
                          [PLANT_ZONE],
                          [WORKSHOP],
                          [PLANT],
                          [ASSEMBLY_LINE],
                          [TWD_RUNSHEET_SN] ,
                          [SUPPLIER_NUM] ,
                          [PART_NO] ,
                          [BARCODE_DATA] ,
                          [IDENTIFY_PART_NO] ,
                          [PART_CNAME] ,
                          [BOX_PARTS] ,
                          [PICKUP_SEQ_NO] ,
                          [RDC_DLOC] ,
                          [MEASURING_UNIT_NO] ,
                          [INHOUSE_PACKAGE_MODEL] ,
                          [REQUIRED_INBOUND_PACKAGE_QTY] ,
                          [COMMENTS] ,
                          [BARCODE_TYPE] ,
                          BATTH_NO ,
                          [CREATE_DATE] ,
                          CREATE_USER ,
                          REQUIRED_DATE,
                          WMS_SEND_STATUS,
						  [TRANS_SUPPLIER_NUM],
						  [STORAGE_LOCATION],
						  [RESUMENO]
                        )
                VALUES  ( 
						  @PLANT_ZONE ,
						  @WORKSHOP ,
						  @PLANT ,
                          @ASSEMBLY_LINE ,
						  @RunsheetSN ,
                          @SUPPLIER_NUM ,
                          @PART_NO ,
                          @barCodeNo ,
                          @IDENTIFY_PART_NO ,
                          @PART_CNAME ,
                          @BOX_PARTS ,
                          @PICKUP_SEQ_NO ,
                          @RDC_DLOC ,
                          @MEASURING_UNIT_NO ,
                          @INHOUSE_PACKAGE_MODEL ,
                          @REQUIRED_INBOUND_PACKAGE_QTY ,
                          N'手工' ,
                          'TWD' ,
                          @BATCHNO ,
                          GETDATE() ,
                          @CREATE_USER ,
                          @REQUIRED_DATE,
                          1,--发送状态，默认为1等待发送
						  @TRANS_SUPPLIER_NUM,
						  @DELIVERY_LOCATION,
						  @ResumeNo
                        )
					
                SET @REQUIRED_QTY = @REQUIRED_QTY - 1
            END
    END
    SET NOCOUNT OFF