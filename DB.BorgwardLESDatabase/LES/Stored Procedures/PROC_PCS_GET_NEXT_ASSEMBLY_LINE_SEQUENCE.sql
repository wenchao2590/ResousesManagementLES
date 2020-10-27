
-- =============================================
-- Author:		yinxuefeng
-- Create date: 2013-07-10
-- Description:	TWD 生成送货单的流水号
-- =============================================
CREATE PROCEDURE [LES].[PROC_PCS_GET_NEXT_ASSEMBLY_LINE_SEQUENCE]
    (
		@Plant NVARCHAR(5),
		@AssemblyLine NVARCHAR(10),
		@Result INT OUTPUT
    )
AS  
    BEGIN
        DECLARE @NextSequence INT
        DECLARE @Flag NVARCHAR(1)
        --从系统配置表中取出送货单标志
        /*送货单 第六位规则
				安亭环境取1
				仪征环境取2
				预留3 5 6 7 以后用与新疆 长沙 南京 宁波~
		*/
        
        BEGIN TRY
            BEGIN TRAN
            UPDATE  [LES].[TM_PCS_ASSEMBLY_LINE_SN] WITH ( ROWLOCK )
            SET     [ASSEMBLY_LINE_SN] = CASE WHEN [ASSEMBLY_LINE_SN] + 1 > 99999999
                                         THEN 1
                                         ELSE [ASSEMBLY_LINE_SN] + 1
                                    END ,
                    UPDATE_DATE = GETDATE()
            WHERE   PLANT = @Plant
            AND ASSEMBLY_LINE = @AssemblyLine
            
            
            
            IF @@ROWCOUNT = 0 
                BEGIN
                   INSERT INTO [LES].[TM_PCS_ASSEMBLY_LINE_SN]
				   ([PLANT]
				   ,[ASSEMBLY_LINE]
				   ,[PLANT_ZONE]
				   ,[WORKSHOP]
				   ,[ASSEMBLY_LINE_SN]
				   ,[COMMENTS]
				   ,[CREATE_USER]
				   ,[CREATE_DATE]
					)
                    VALUES  ( @Plant , -- PLANT - nvarchar(5)
                              @AssemblyLine , -- ASSEMBLY_LINE - nvarchar(10)
                              N'' , -- PLANT_ZONE - nvarchar(5)
                              N'' , -- WORKSHOP - nvarchar(4)
                              1 , -- ASSEMBLY_LINE_SN - int
                              N'' , -- COMMENTS - nvarchar(200)
                              N'PROC_PCS_GET_NEXT_ASSEMBLY_LINE_SEQUENCE' , -- CREATE_USER - nvarchar(50)
                              GETDATE()  -- CREATE_DATE - datetime
                            )               
                END
            
            SELECT  @NextSequence = [ASSEMBLY_LINE_SN]
            FROM   [LES].[TM_PCS_ASSEMBLY_LINE_SN]
            WHERE   PLANT =@Plant
            AND ASSEMBLY_LINE = @AssemblyLine
          
            
            COMMIT TRAN
            
            set @result =@NextSequence
			return 0

        END TRY
        BEGIN CATCH
            ROLLBACK TRAN
            set @result =''
            RETURN -1
        END CATCH   
    
    END