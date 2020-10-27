/********************************************************************/
/*   Project Name:  SPS												*/
/*   Program Name:  [LES].[PROC_SPS_GENERATE_CLOSE]		 			*/
/*   Called By:     by web page										*/
/*   Author:        李蒙											*/
/*   Create date:	2017-10-17										*/
/*   Note:			SPS 关闭拉动单									*/
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_SPS_GENERATE_CLOSE]
(
    @SPSRUNSHEETNO NVARCHAR(50),			--拉动单号
    @LOGINNAME NVARCHAR(50),				--处理人
    @RESULT INT OUTPUT,						--返回结果，0-成功，1-失败
    @RESULTMESSAGE NVARCHAR(4000) OUTPUT	--返回信息

)
AS
BEGIN
    DECLARE @SUPPLIER_TYPE INT				--供应商类型
    DECLARE @SHEET_STATUS INT				--拉动单状态
        
    BEGIN TRY
        BEGIN TRANSACTION
        SET @RESULT = 0
        SET @RESULTMESSAGE = ''

        SELECT  @SUPPLIER_TYPE = B.[SUPPLIER_TYPE] ,
                @SHEET_STATUS = A.[SHEET_STATUS]
        FROM    [LES].[TT_SPS_RUNSHEET] A WITH ( NOLOCK )
                INNER JOIN [LES].[TM_BAS_SUPPLIER] B WITH ( NOLOCK ) ON A.[SUPPLIER_NUM] = B.[SUPPLIER_NUM]
        WHERE   A.[SPS_RUNSHEET_NO] = @SPSRUNSHEETNO

        IF @SHEET_STATUS = 1
            BEGIN
                SET @RESULT = 1
                SET @RESULTMESSAGE = '拉动单[' + @SPSRUNSHEETNO + ']已关闭！'
            END
        IF @RESULT = 0
            BEGIN
				--2 是出库单 1 是入库单
                IF @SUPPLIER_TYPE = 1
                    BEGIN
                        SET @RESULT = 1
                        SET @RESULTMESSAGE = '拉动单[' + @SPSRUNSHEETNO+ ']生成的是入库单，只能关闭生成出库单的拉动单！'
                    END
            END
        IF @RESULT = 0
            BEGIN
				--关闭拉动单
                UPDATE  [LES].[TT_SPS_RUNSHEET] WITH ( ROWLOCK )
                SET     [SHEET_STATUS] = 1 ,
                        [UPDATE_USER] = @LOGINNAME ,
                        [UPDATE_DATE] = GETDATE()
                WHERE   [SPS_RUNSHEET_NO] = @SPSRUNSHEETNO

				--关闭出库单
                UPDATE  [LES].[TT_WMM_OUTPUT] WITH ( ROWLOCK )
                SET     [CONFIRM_FLAG] = 2 ,
                        [UPDATE_USER] = @LOGINNAME ,
                        [UPDATE_DATE] = GETDATE()
                WHERE   [OUTPUT_NO] = @SPSRUNSHEETNO
            END
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
		--出错，则返回执行不成功，回滚事务
        ROLLBACK TRANSACTION
		--记录错误信息
        INSERT  INTO [LES].[TS_SYS_EXCEPTION]( [TIME_STAMP] ,[APPLICATION] ,[METHOD] ,[CLASS] ,[EXCEPTION_MESSAGE] ,[ERROR_CODE] )
        SELECT  GETDATE() ,'SPS' ,'[LES].[PROC_SPS_GENERATE_CLOSE]' ,'Procedure' ,ERROR_MESSAGE() , ERROR_LINE()
    END CATCH
END