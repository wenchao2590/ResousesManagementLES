
/********************************************************************/
/*   Project Name:  Foton LES System								*/
/*   Program Name:  [LES].[PROC_TWD_UPDATE_TIMEZONE_COUNTER]		*/
/*   Called By:     by the Page										*/
/*   Purpose:       TWD扣减时区计数器								*/
/*   author:        李蒙    2017-11-3								*/
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_TWD_UPDATE_TIMEZONE_COUNTER]
    (
      @plant NVARCHAR(5) ,
      @assemblyLine NVARCHAR(10) ,
      @dcppoint NVARCHAR(15) ,
      @deductpoints NVARCHAR(MAX) ,
      @vehiclestatusid INT
    )
AS
    BEGIN
        BEGIN TRY    
            BEGIN TRANSACTION

            CREATE TABLE [LES].[#TT_TWD_TIMEZONE_COUNTER_TEMP]
                (
                  [PLANT] [nvarchar](5) NOT NULL ,
                  [ASSEMBLY_LINE] [nvarchar](50) NULL ,
                  [DCP_POINT] [nchar](15) NULL ,
                  [PLANT_ZONE] [nchar](5) NULL ,
                  [TIME_AND] [nvarchar](50) NULL ,
                  [CREATE_DATE] [datetime] NULL
                )


            INSERT  INTO #TT_TWD_TIMEZONE_COUNTER_TEMP
                    ( PLANT ,
                      ASSEMBLY_LINE ,
                      DCP_POINT ,
                      PLANT_ZONE ,
                      TIME_AND ,
                      CREATE_DATE
			        )
                    SELECT  A.PLANT ,
                            A.ASSEMBLY_LINE ,
                            A.DCP_POINT ,
                            A.PLANT_ZONE ,
                            A.TIME_AND ,
                            MIN(A.CREATE_DATE) CREATE_DATE
                    FROM    [LES].[TT_TWD_TIMEZONE_COUNTER] A WITH ( NOLOCK )
                    WHERE   VALIDITY_FLAG = 1
                            AND A.ASSEMBLY_LINE = @assemblyLine
                            AND A.DCP_POINT IN (
                            SELECT  Split_Value
                            FROM    LES.FUN_SYS_SPLIT(@deductpoints, ',') )
                            AND A.PLANT = @plant
                    GROUP BY A.PLANT_ZONE ,
                            A.TIME_AND ,
                            A.DCP_POINT ,
                            A.ASSEMBLY_LINE ,
                            A.PLANT
            IF EXISTS ( SELECT  *
                        FROM    [LES].[TT_TWD_TIMEZONE_COUNTER] A
                                RIGHT JOIN #TT_TWD_TIMEZONE_COUNTER_TEMP B
                                WITH ( NOLOCK ) ON A.ASSEMBLY_LINE = B.ASSEMBLY_LINE
                                                   AND A.DCP_POINT = B.DCP_POINT
                                                   AND A.PLANT = B.PLANT
                                                   AND A.PLANT_ZONE = B.PLANT_ZONE
                                                   AND A.TIME_AND = B.TIME_AND
                                                   AND A.CREATE_DATE = B.CREATE_DATE )
                BEGIN
                    UPDATE  A WITH ( ROWLOCK )
                    SET     [QUANTITY] = [QUANTITY] - 1
                    FROM    [LES].[TT_TWD_TIMEZONE_COUNTER] A
                            RIGHT JOIN #TT_TWD_TIMEZONE_COUNTER_TEMP B WITH ( NOLOCK ) ON A.ASSEMBLY_LINE = B.ASSEMBLY_LINE
                                                              AND A.DCP_POINT = B.DCP_POINT
                                                              AND A.PLANT = B.PLANT
                                                              AND A.PLANT_ZONE = B.PLANT_ZONE
                                                              AND A.TIME_AND = B.TIME_AND
                                                              AND A.CREATE_DATE = B.CREATE_DATE

                    DECLARE @VEHICLE_STATUS_ID INT
                    SELECT  @VEHICLE_STATUS_ID = MAX(VEHICLE_STATUS_ID)
                    FROM    [LES].[TT_TWD_TIMEZONE_COUNTER] WITH ( NOLOCK )
                    WHERE   DCP_POINT = @dcppoint
                            AND ASSEMBLY_LINE = @assemblyLine
                            AND PLANT = @plant
                            AND [VALIDITY_FLAG] = 1
                    GROUP BY DCP_POINT ,
                            PLANT ,
                            ASSEMBLY_LINE
                    UPDATE  [LES].[TT_TWD_TIMEZONE_COUNTER] WITH ( ROWLOCK )
                    SET     VEHICLE_STATUS_ID = @vehiclestatusid
                    WHERE   DCP_POINT = @dcppoint
                            AND ASSEMBLY_LINE = @assemblyLine
                            AND PLANT = @plant
                            AND VEHICLE_STATUS_ID = @VEHICLE_STATUS_ID 
                END
			---------------------------------------扣减数量为零时,更新是否有效状态为无效
            IF EXISTS ( SELECT  *
                        FROM    [LES].[TT_TWD_TIMEZONE_COUNTER]
                        WHERE   [QUANTITY] = 0
                                AND [VALIDITY_FLAG] = 1 )
                UPDATE  [LES].[TT_TWD_TIMEZONE_COUNTER] WITH ( ROWLOCK )
                SET     [VALIDITY_FLAG] = 0
                WHERE   [QUANTITY] = 0
                        AND [VALIDITY_FLAG] = 1

            DROP TABLE #TT_TWD_TIMEZONE_COUNTER_TEMP   --删除临时表

            COMMIT TRANSACTION
        END TRY
        BEGIN CATCH
        --出错，则返回执行不成功，回滚事务
            ROLLBACK TRANSACTION
        --记录错误信息
            INSERT  INTO [LES].[TS_SYS_EXCEPTION]
                    ( [TIME_STAMP] ,
                      [APPLICATION] ,
                      [METHOD] ,
                      [CLASS] ,
                      [EXCEPTION_MESSAGE] ,
                      [ERROR_CODE]
                    )
                    SELECT  GETDATE() ,
                            'INTERFACE' ,
                            '[LES].[PROC_TWD_UPDATE_TIMEZONE_COUNTER]' ,
                            'Procedure' ,
                            ERROR_MESSAGE() ,
                            ERROR_LINE()
        END CATCH
    END