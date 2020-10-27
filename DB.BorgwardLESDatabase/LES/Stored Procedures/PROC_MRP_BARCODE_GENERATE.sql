
CREATE PROCEDURE [LES].[PROC_MRP_BARCODE_GENERATE]
(  
	@LOGICAL_PK  nvarchar(100),
	@PLAN_AMOUNT Decimal ,
	@CLIENT  nvarchar(50),
	@SEND_PORT  nvarchar(50),
	@IDOC  nvarchar(50),
	@LINE_NO  nvarchar(50),
	@DELIVERY_DATE  datetime,
	@PART_NO nvarchar(50),
	@PLANT nvarchar(50),
	@CREATE_USER nvarchar(50),
	@CREATE_DATE datetime,
	@UPDATE_USER nvarchar(50),
	@UPDATE_DATE datetime
)
AS
BEGIN
    DECLARE @NextSequence int
    DECLARE @BARCODE nvarchar(20)
    DECLARE @SequenceName nvarchar(32)
    DECLARE @InboundPackage int --入场包装数
    DECLARE @InboundPackageMode nvarchar(20)
    DECLARE @PackageCount int = 0
    DECLARE @Surplus_PackageCount int --剩余
    DECLARE @SUPPLIER_NUM nvarchar(20)
    DECLARE @PART_NOt nvarchar(20)
    DECLARE @PART_CNAME nvarchar(300)
	DECLARE @INBOUND_PACKAGE	int
	DECLARE @INBOUND_PACKAGE_MODEL nvarchar(30)
    
    DECLARE @i INT --循环变量    
    
    --BEGIN TRY
       -- BEGIN TRAN
        
            SELECT  @InboundPackage = Inbound_Package,
                    @InboundPackageMode = Inbound_Package_Model,@SUPPLIER_NUM=SUPPLIER_NUM,@PART_CNAME=PART_CNAME
            FROM LES.TM_BAS_MAINTAIN_INBOUND_LOGISTIC_STANDARD  
			WHERE PLANT = @PLANT AND PART_NO = @PART_NO --AND INBOUND_SYSTEM_MODE <> 'TWD'
            
            IF(ISNULL(@InboundPackage,0) <= 0)
            BEGIN
				RETURN -1
            END  
           
            
            SET @PackageCount = ceiling(@PLAN_AMOUNT/@InboundPackage); --取整数量
 
			IF(@PackageCount = 0) SET @PackageCount = 1
             
            --SET @Surplus_PackageCount = @PLAN_AMOUNT%@InboundPackage ;  --取余数量
                        
            SET @i = 1 
            
            PRINT '@PackageCount'
            PRINT @PackageCount
             
            WHILE(@i <= @PackageCount)
            BEGIN           
            
				EXECUTE @NextSequence = LES.PROC_SYS_GET_NEXT_SEQUENCE 'SAPMRPBARCODE'
			
				SET @BARCODE = 'TW' + RIGHT('000000000000000000000000'+convert(varchar(12),@NextSequence),12) ; --计算BarCode,补位
				
				/*
				IF(@i = @PackageCount AND @Surplus_PackageCount<>0) 
				BEGIN
					--余数不等于零时，最后一箱用余数
					SET @PLAN_AMOUNT = @Surplus_PackageCount;
				END	
				*/		
				
				INSERT INTO [LES].[TT_MRP_REQUEST_LIST_BARCODE]
			               (LOGICAL_PK,[CLIENT],[SEND_PORT],[IDOC],[LINE_NO]
			               ,[DELIVERY_DATE],[BARCODE],[COMMENTS],[SUPPLIER_NUM],[PART_NO]
							,[PART_CNAME],[BOX_PARTS],[MEASURING_UNIT_NO],[INBOUND_PACKAGE],[INBOUND_PACKAGE_MODEL]
			               ,[CREATE_USER] ,[CREATE_DATE],[UPDATE_USER],[UPDATE_DATE])
			          VALUES(@LOGICAL_PK,@CLIENT,@SEND_PORT,@IDOC,@LINE_NO
			               ,@DELIVERY_DATE,@BARCODE,'',@SUPPLIER_NUM,@PART_NO,@PART_CNAME,'',1,@InboundPackage,@InboundPackageMode
			               ,@CREATE_USER,@CREATE_DATE,@UPDATE_USER,@UPDATE_DATE)
			               
			    SET @i = @i + 1
            END
          
            --COMMIT TRAN
           
    --END TRY
    --BEGIN CATCH
    --    ROLLBACK TRAN
    --    RETURN 0
    --END CATCH   
    
END