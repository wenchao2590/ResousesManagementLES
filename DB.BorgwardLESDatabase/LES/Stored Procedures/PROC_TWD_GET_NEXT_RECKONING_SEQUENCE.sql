
-- =============================================
-- Author:		yinxuefeng
-- Create date: 2013-07-10
-- Description:	TWD 生成送货单的流水号
-- =============================================
CREATE PROCEDURE [LES].[PROC_TWD_GET_NEXT_RECKONING_SEQUENCE]
    (
		--@Plant NVARCHAR(5),
		--@AssemblyLine NVARCHAR(10),
		@SupplierNum NVARCHAR(8),
		@Result NVARCHAR(12) OUTPUT
    )
AS  
    BEGIN
        DECLARE @NextSequence NVARCHAR(12)
        DECLARE @Flag NVARCHAR(1)
        --从系统配置表中取出送货单标志
        /*送货单 第六位规则
				安亭环境取1
				仪征环境取2
				预留3 5 6 7 以后用与新疆 长沙 南京 宁波~
		*/
        SELECT @Flag=ISNULL(PARAMETER_VALUE,DEFAULT_VALUE) FROM LES.TS_SYS_CONFIG WHERE PARAMETER_NAME='ReckoningNumberFlag'
        IF @Flag IS NULL
		BEGIN
			SET @Flag='1'--如果没有配置，设置为‘1’
		END
        BEGIN TRY
            BEGIN TRAN
            UPDATE  LES.TM_TWD_SUPPLIER_SN WITH ( ROWLOCK )
            SET     JIS_SUPPLIER_SN = CASE WHEN JIS_SUPPLIER_SN + 1 > 999999
                                         THEN 1
                                         ELSE JIS_SUPPLIER_SN + 1
                                    END ,
                    UPDATE_DATE = GETDATE()
            WHERE   PLANT = 'XX'
            AND ASSEMBLY_LINE = 'XXXX'
            AND UPPER(RTRIM(LTRIM(SUPPLIER_NUM))) = UPPER(RTRIM(LTRIM(@SupplierNum)))
            
            
            IF @@ROWCOUNT = 0 
                BEGIN
                    INSERT INTO LES.TM_TWD_SUPPLIER_SN
                            ( PLANT ,
                              ASSEMBLY_LINE ,
                              PLANT_ZONE ,
                              WORKSHOP ,
                              SUPPLIER_NUM ,
                              JIS_SUPPLIER_SN ,
                              COMMENTS ,
                              CREATE_USER ,
                              CREATE_DATE 
                            )
                    VALUES  ( 'XX' , -- PLANT - nvarchar(5)
                              'XXXX' , -- ASSEMBLY_LINE - nvarchar(10)
                              N'' , -- PLANT_ZONE - nvarchar(5)
                              N'' , -- WORKSHOP - nvarchar(4)
                              @SupplierNum , -- SUPPLIER_NUM - nvarchar(8)
                              1 , -- JIS_SUPPLIER_SN - int
                              N'' , -- COMMENTS - nvarchar(200)
                              N'PROC_TWD_GET_NEXT_RECKONING_SEQUENCE' , -- CREATE_USER - nvarchar(50)
                              GETDATE()  -- CREATE_DATE - datetime
                            )               
                END
            
            SELECT  @NextSequence = REPLACE(SPACE(6 - LEN(CONVERT(NVARCHAR(6), JIS_SUPPLIER_SN))),' ', '0') + CONVERT(NVARCHAR(6), JIS_SUPPLIER_SN)
            FROM    LES.TM_TWD_SUPPLIER_SN
            WHERE   PLANT = 'XX'
            AND ASSEMBLY_LINE = 'XXXX'
            AND UPPER(RTRIM(LTRIM(SUPPLIER_NUM))) = UPPER(RTRIM(LTRIM(@SupplierNum)))
            
            COMMIT TRAN
            --规则:5位供应商代码+1位送货单类型（LES定义为1）+6位流水号
            set @result =UPPER(RTRIM(LTRIM(@SupplierNum)))+RTRIM(LTRIM(@Flag))+@NextSequence
			return 0

        END TRY
        BEGIN CATCH
            ROLLBACK TRAN
            set @result =''
            RETURN -1
        END CATCH   
    
    END