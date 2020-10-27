
-- =============================================
-- Author:		yinxuefeng
-- Create date: 2013-01-25
-- Description:	为V000车辆创建空报文
-- =============================================
CREATE PROCEDURE [LES].[PROC_JIS_CREATE_EMPTY_PREVIEW_RESULT_FOR_V000] (
     @KNR NVARCHAR(10) ,
     @PLANT NVARCHAR(5),
     @ASSEMBLY_LINE NVARCHAR(10) ,
     @VEHICLE_STATUS NVARCHAR(10) ,
     @RUNNING_NUMBER NVARCHAR(10) ,
     @SIGNATURE NVARCHAR(50) ,
     @DCP_POINT NVARCHAR(50) ,
     @VIN NVARCHAR(50) ,
     @PASS_TIME DATETIME,
     @ORDER_ID NVARCHAR(50),
     @MODEL_NO NVARCHAR(8)
    )
AS 
    BEGIN	
        DECLARE @CHANGE_TYPE NVARCHAR(50)
        DECLARE @SEQUENCE_NUMBER NVARCHAR(10)
        DECLARE @EXTERIOR_COLOR NVARCHAR(4)
        DECLARE @INTERNAL_COLOR NVARCHAR(2)
        DECLARE @SPJ NVARCHAR(10)
        DECLARE @MODEL_YEAR NVARCHAR(10)
        DECLARE @MODEL NVARCHAR(10)
	
        DECLARE @SOP_FLAG BIT
        SELECT  @SOP_FLAG = [VORSERIE], @EXTERIOR_COLOR = [FARBAU],
                @INTERNAL_COLOR = [FARBIN], @SPJ = RIGHT(spj, 2),
                @MODEL_YEAR = RIGHT(model_year, 2)
        FROM    [LES].[TT_BAS_PULL_ORDERS]
        WHERE   KNR = @KNR
        SELECT  @SEQUENCE_NUMBER = '0' + RIGHT(@DCP_POINT, 1) + @RUNNING_NUMBER
        SELECT  @MODEL = [MODEL] FROM    [LES].[TM_BAS_MODEL]
        WHERE   [PRODUCTION_MODEL] = SUBSTRING(@MODEL_NO, 1, 2)
        BEGIN TRY
            BEGIN
                SET @CHANGE_TYPE = 'STC'
                IF ( SUBSTRING(RTRIM(LTRIM(@KNR)), 3, 1) != '9' ) 
                    BEGIN
                        INSERT  INTO [LES].[TT_JIS_PREVIEW_HEAD_DATA] ( [PREVIEW_DATA_TIME],
                                                              [PREVIEW_TYPE],
                                                              [RUNNING_NUMBER],
                                                              [SOP_FLAG],
                                                              [PLANT],
                                                              [ASSEMBLY_LINE],
                                                              [SUPPLIER_NUM],
                                                              [RACKS],
                                                              [VEHICLE_STATUS],
                                                              [ORDER_ID],
                                                              [CAR_NO],
                                                              [SEND_STATUS],
                                                              [WMS_SEND_STATUS],
                                                              [SIGNATURE],
                                                              [DCP_NAME],
                                                              [SEQUENCE_NUMBER],
                                                              [COMMENTS], VIN,
                                                              [CHANGE_TYPE],
                                                              [CREATE_DATE],
                                                              [CREATE_USER],
                                                              [DCP_POINT],
                                                              [BOX_PARTS_NAME],
                                                              [EXTERIOR_COLOR],
                                                              [INTERNAL_COLOR],
                                                              [SPJ],
                                                              [MODEL_YEAR],
                                                              [MODEL],
                                                              [MODEL_NO] )
                                SELECT  @PASS_TIME, '1', @RUNNING_NUMBER,
                                        @SOP_FLAG, @PLANT, @ASSEMBLY_LINE,
                                        A.[SUPPLIER_NUM], a.RACKS AS [RACKS],
                                        'M100', @ORDER_ID, @KNR, 1, 1,--状态写成M100,由于此车为报废，不关注状态，写成M100是为了能生成空报文明细
                                        @SIGNATURE, RIGHT(@DCP_POINT, 4),
                                        @SEQUENCE_NUMBER, '', @VIN,
                                        @CHANGE_TYPE, GETDATE(),
                                        'FISDispatchVehicleService_V000',
                                        RIGHT(@DCP_POINT, 4),
                                        '' AS [RACKNAMES], @EXTERIOR_COLOR,
                                        @INTERNAL_COLOR, @SPJ, @MODEL_YEAR,
                                        @MODEL, @MODEL_NO
                                FROM    LES.TT_JIS_PREVIEW_HEAD_DATA A
                                WHERE   CAR_NO = @KNR
                                AND PREVIEW_DATA_SN IN
								(
									SELECT PREVIEW_DATA_SN FROM 
									(
										SELECT PREVIEW_DATA_SN,ROW_NUMBER() OVER ( PARTITION BY SUPPLIER_NUM ORDER BY PREVIEW_DATA_SN DESC ) AS RN   
										FROM LES.TT_JIS_PREVIEW_HEAD_DATA B
										WHERE CAR_NO=@KNR
									) C
									WHERE C.RN=1
								)
                    END
            END
        END TRY
        BEGIN CATCH
			--记录错误信息
            INSERT  INTO [LES].[TS_SYS_EXCEPTION] ( time_stamp, [application],
                                                     [METHOD], class,
                                                     exception_message,
                                                     error_code )
                    SELECT  GETDATE(), 'JIS',
                            'PROC_JIS_CREATE_EMPTY_PREVIEW_RESULT_FOR_V000',
                            'Procedure', ERROR_MESSAGE(), ERROR_LINE()

        END CATCH
    END