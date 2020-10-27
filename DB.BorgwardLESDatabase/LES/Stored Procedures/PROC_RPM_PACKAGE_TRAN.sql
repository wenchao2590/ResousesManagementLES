/************************************************************************************/
/*																					*/
/*   Project Name:  Foton LES System												*/
/*   Program Name:  PROC_RPM_PACKAGE_TRAN											*/
/*   Called By:     by the Page														*/
/*   Purpose:       1、根据收货入库单信息，创建包装器具入库单，并完成入库信息。		*/
/*                  2、只针对交货单入库；对于用户自行创建的收货入库单，不作处理。	*/
/*                  3、只针对循环的器具，非循环的器具不做器具入库					*/
/*   author:		Andy Liu    2015-09-09											*/
/************************************************************************************/
CREATE PROCEDURE [LES].[PROC_RPM_PACKAGE_TRAN]
(
	@RECEIVE_ID INT
)
AS
BEGIN
    DECLARE @IDENTITY INT    
    DECLARE @PART_NO NVARCHAR(20)
    DECLARE @RUNSHEET_NO NVARCHAR(50)
    DECLARE @PLANT NVARCHAR(5)
    DECLARE @ASSEMBLY_LINE NVARCHAR(10)
    DECLARE @SUPPLIER_NUM NVARCHAR(12)
    DECLARE @PACKAGE_MODEL NVARCHAR(30)
    DECLARE @PACKAGE_CNAME NVARCHAR(100)
    DECLARE @PACKAGE_ENAME NVARCHAR(100)
    DECLARE @RUNSHEET_CODE NVARCHAR(50)   --单据类型
    DECLARE @WM_NO NVARCHAR(10)
    DECLARE @Result INT
    DECLARE @ResultMessage nvarchar(1000)
    BEGIN TRY    
        BEGIN TRANSACTION
            --主表
            SELECT
				@WM_NO = [WM_NO],
				@RUNSHEET_NO = [RUNSHEET_NO],
				@RUNSHEET_CODE = [RUNSHEET_CODE]
			FROM [LES].[TT_WMM_RECEIVE] WITH (NOLOCK)
			WHERE [RECEIVE_ID] = @RECEIVE_ID

            IF @RUNSHEET_NO IS NULL 
				BEGIN
					SET @RUNSHEET_NO = ''
				END

            --当收货入库单信息是从交货单下发的情况，则进行收包装器具功能
            IF @RUNSHEET_NO <> ''
				BEGIN
					IF EXISTS
					(
						SELECT TOP 1
							A.[RECEIVE_ID]
						FROM [LES].[TT_WMM_RECEIVE_DETAIL] A WITH (NOLOCK)
						INNER JOIN [LES].[TM_RPM_PACKAGE_APPLIANCE] B WITH (NOLOCK)
						ON B.[PLANT] = A.[PLANT] AND B.[PACKAGE_NO] = A.[PACKAGE_MODEL] AND B.[IS_REPEAT] = 1
						WHERE A.[RECEIVE_ID] = @RECEIVE_ID
					)	--存在循环的器具时，创建器具入库单
						BEGIN
							--EXEC [LES].[PROC_SYS_GET_CURRENT_SEQCODE] 'PackageInput',@WM_NO,@billNo OUTPUT--交易单号，改成直接使用交货单号

							--加判断，单次收货改 多次收货 判断 器具交易主表是否存在，存在则进行修改，不存在则新增
							IF NOT EXISTS(SELECT 1 FROM [LES].[TT_RPM_PACKAGE_TRAN] WITH (NOLOCK) WHERE [ASN_NO] = @RUNSHEET_NO)    
								BEGIN
									INSERT INTO [LES].[TT_RPM_PACKAGE_TRAN]
									(
										[TRAN_NO],
										[ASN_NO],
										[WM_NO],
										[ZONE_NO],
										[RECEIVE_TYPE],
										[TRAN_TIME],
										[PLANT],
										[ASSEMBLY_LINE],
										[PLANT_ZONE],
										[WORKSHOP],
										[SUPPLIER_NUM],
										[TRANS_SUPPLIER_NUM],
										[SUPPLIER_TYPE],
										[CONFIRM_FLAG],
										[TRAN_TYPE],
										[PACKAGE_TYPE],
										[CREATE_USER],
										[CREATE_DATE]
									)
									SELECT
										@RUNSHEET_NO,
										[RECEIVE_NO],
										[WM_NO],
										[ZONE_NO],
										1,
										GETDATE(),
										[PLANT],
										[ASSEMBLY_LINE],
										[PLANT_ZONE],
										[WORKSHOP],
										[SUPPLIER_NUM],
										[TRANS_SUPPLIER_NUM],
										[SUPPLIER_TYPE],
										1,
										1,
										1,
										'admin',
										GETDATE() 
									FROM [LES].[TT_WMM_RECEIVE] WITH (NOLOCK)
									WHERE [RECEIVE_ID] = @RECEIVE_ID

									SET @IDENTITY=SCOPE_IDENTITY()

									IF @RUNSHEET_CODE = 'JIS'
										BEGIN
											--JIS收货，只插入一个器具进入子表
											INSERT INTO [LES].[TT_RPM_PACKAGE_TRAN_DETAIL]
											(
												[TRAN_ID],
												[PLANT],
												[ASSEMBLY_LINE],
												[SUPPLIER_NUM],
												[WM_NO],
												[ZONE_NO],
												[DLOC],
												[TRAN_NO],
												[TARGET_WM],
												[TARGET_ZONE],
												[TARGET_DLOC],
												[MEASURING_UNIT_NO],
												[PACKAGE],
												[NUM],
												[PACKAGE_NO],
												[PACKAGE_CNAME],
												[PACKAGE_ENAME],
												[DOCK],
												[SEQUENCE_NO],
												[PICKUP_SEQ_NO],
												[PACK_COUNT],
												[REQUIRED_PACKAGE],
												[REQUIRED_PACKAGE_QTY],
												[ACTUAL_PACKAGE],
												[ACTUAL_PACKAGE_QTY],
												[BARCODE_DATA],
												[Current_BOX_NUM],
												[PART_NO]
											)
											SELECT TOP 1
												@IDENTITY,
												A.[PLANT],
												A.[ASSEMBLY_LINE],
												A.[SUPPLIER_NUM],
												A.[WM_NO],
												A.[ZONE_NO],
												A.[DLOC],
												A.[TRAN_NO],
												A.[TARGET_WM],
												A.[TARGET_ZONE],
												A.[TARGET_DLOC],
												A.[MEASURING_UNIT_NO],
												'',
												1 AS [NUM],
												A.[PACKAGE_MODEL],
												B.[PACKAGE_CNAME],
												B.[PACKAGE_ENAME],
												A.[DOCK],
												A.[SEQUENCE_NO],
												A.[PICKUP_SEQ_NO],
												0,
												0,
												0,
												0,
												1 AS [ACTUAL_PACKAGE_QTY],
												'',
												1,
												A.[PART_NO]
											FROM [LES].[TT_WMM_RECEIVE_DETAIL] A WITH (NOLOCK)
											INNER JOIN [LES].[TM_RPM_PACKAGE_APPLIANCE] B WITH (NOLOCK)
											ON B.[PLANT] = A.[PLANT] AND B.[PACKAGE_NO] = A.[PACKAGE_MODEL] AND B.[IS_REPEAT] = 1
											WHERE A.[RECEIVE_ID] = @RECEIVE_ID
										END
									ELSE
										BEGIN
											--子表，普通收货，插入器具和托信息
											INSERT INTO [LES].[TT_RPM_PACKAGE_TRAN_DETAIL]
											(
												[TRAN_ID],
												[PLANT],
												[ASSEMBLY_LINE],
												[SUPPLIER_NUM],
												[WM_NO],
												[ZONE_NO],
												[DLOC],
												[TRAN_NO],
												[TARGET_WM],
												[TARGET_ZONE],
												[TARGET_DLOC],
												[MEASURING_UNIT_NO],
												[PACKAGE],
												[NUM],
												[PACKAGE_NO],
												[PACKAGE_CNAME],
												[PACKAGE_ENAME],
												[DOCK],
												[SEQUENCE_NO],
												[PICKUP_SEQ_NO],
												[PACK_COUNT],
												[REQUIRED_PACKAGE],
												[REQUIRED_PACKAGE_QTY],
												[ACTUAL_PACKAGE],
												[ACTUAL_PACKAGE_QTY],
												[BARCODE_DATA],
												[Current_BOX_NUM],
												[PART_NO]
											)
											SELECT
												@IDENTITY,
												A.[PLANT],
												A.[ASSEMBLY_LINE],
												A.[SUPPLIER_NUM],
												A.[WM_NO],
												A.[ZONE_NO],
												A.[DLOC],
												A.[TRAN_NO],
												A.[TARGET_WM],
												A.[TARGET_ZONE],
												A.[TARGET_DLOC],
												A.[MEASURING_UNIT_NO],
												'',
												ISNULL(A.[ACTUAL_BOX_NUM],0),
												A.[PACKAGE_MODEL],
												B.[PACKAGE_CNAME],
												B.[PACKAGE_ENAME],
												A.[DOCK],
												A.[SEQUENCE_NO],
												A.[PICKUP_SEQ_NO],
												0,
												0,
												0,
												0,
												ISNULL(A.ACTUAL_BOX_NUM,0),
												'',
												ISNULL(A.ACTUAL_BOX_NUM,0),
												A.PART_NO
											FROM [LES].[TT_WMM_RECEIVE_DETAIL] A WITH (NOLOCK)
											INNER JOIN [LES].[TM_RPM_PACKAGE_APPLIANCE] B WITH (NOLOCK)
											ON B.[PLANT] = A.[PLANT] AND B.[PACKAGE_NO] = A.[PACKAGE_MODEL] AND B.[IS_REPEAT] = 1
											WHERE A.[RECEIVE_ID] = @RECEIVE_ID

											--添加托号信息
											DECLARE @Tray_Num nvarchar(1000)                    
											SELECT @Tray_Num = [PARAMETER1] FROM [LES].[TM_SYS_APPLICATION_CONFIGURATION] WITH (NOLOCK) WHERE [APPLICATION] = 'tray_num'

											EXEC
											('
												INSERT INTO LES.TT_RPM_PACKAGE_TRAN_DETAIL
												(
													TRAN_ID,PLANT,ASSEMBLY_LINE,SUPPLIER_NUM,WM_NO,ZONE_NO,DLOC,TRAN_NO,
													TARGET_WM,TARGET_ZONE,TARGET_DLOC,MEASURING_UNIT_NO,PACKAGE,NUM,PACKAGE_NO,PACKAGE_CNAME,PACKAGE_ENAME,DOCK,
													SEQUENCE_NO,PICKUP_SEQ_NO,PACK_COUNT,REQUIRED_PACKAGE,REQUIRED_PACKAGE_QTY,ACTUAL_PACKAGE,ACTUAL_PACKAGE_QTY,BARCODE_DATA,Current_BOX_NUM
												)
												SELECT DISTINCT ' + @IDENTITY + ',
													A.PLANT,A.ASSEMBLY_LINE,A.SUPPLIER_NUM,A.WM_NO,A.ZONE_NO,A.DLOC,TRAN_NO,
													TARGET_WM,TARGET_ZONE,TARGET_DLOC,MEASURING_UNIT_NO,0,0,B.PACKAGE_NO,B.PACKAGE_CNAME,B.PACKAGE_ENAME,DOCK,
													SEQUENCE_NO,PICKUP_SEQ_NO,0,0,0,0,0,NULL,0
												FROM  LES.TM_RPM_PACKAGE_APPLIANCE B
												INNER JOIN (SELECT TOP 1 * FROM LES.[TT_WMM_RECEIVE_DETAIL] WHERE RECEIVE_ID=' + @RECEIVE_ID + ') A ON B.PLANT = A.PLANT AND B.SUPPLIER_NUM = A.SUPPLIER_NUM 
												WHERE LEFT(B.PACKAGE_NO,6) IN (' + @Tray_Num +')
											')
										END
								END
							ELSE
								BEGIN
									IF @RUNSHEET_CODE != 'JIS'
										BEGIN
											--子表，普通收货，更新 器具数量
											UPDATE D
											SET D.[Current_BOX_NUM] = ISNULL(A.[ACTUAL_BOX_NUM],0) - ISNULL(D.[NUM],0),
												D.[NUM] = ISNULL(A.[ACTUAL_BOX_NUM],0),
												D.[ACTUAL_PACKAGE_QTY] = ISNULL(A.[ACTUAL_BOX_NUM], 0)
											FROM LES.[TT_WMM_RECEIVE_DETAIL] A WITH (NOLOCK)
											INNER JOIN LES.[TT_WMM_RECEIVE] B WITH (NOLOCK) ON A.[RECEIVE_ID] = B.[RECEIVE_ID]
											INNER JOIN [LES].[TT_RPM_PACKAGE_TRAN] C WITH (NOLOCK) ON B.[RECEIVE_NO] = C.[ASN_NO]
											INNER JOIN [LES].[TT_RPM_PACKAGE_TRAN_DETAIL] D WITH (ROWLOCK) ON D.[TRAN_ID] = C.[TRAN_ID] AND D.[PART_NO] = A.[PART_NO]
											WHERE B.[RECEIVE_ID] = @RECEIVE_ID
                            
											SET @IDENTITY = (SELECT TOP 1 C.[TRAN_ID] FROM [LES].[TT_WMM_RECEIVE] B WITH (NOLOCK) INNER JOIN [LES].[TT_RPM_PACKAGE_TRAN] C WITH (NOLOCK) ON B.[RECEIVE_NO] = C.[ASN_NO] WHERE B.[RECEIVE_ID] = @RECEIVE_ID)
										END
								END

							--增加库存
							EXEC [LES].[PROC_RPM_PACKAGE_TRAN_STOCKS] @IDENTITY, 1, @Result OUTPUT, @ResultMessage OUTPUT
						END

					IF @RUNSHEET_CODE = 'JIS'
						BEGIN
							--JIS拉动单入库，更新JIS拉动单状态
							UPDATE [LES].[TT_JIS_RUNSHEET] WITH (ROWLOCK)
							SET [SEND_STATUS] = 9                --已收货
							WHERE [JIS_RUNSHEET_NO] = @RUNSHEET_NO
						END
					ELSE
						BEGIN
							--更新送货单状态，完成送货单
							UPDATE [LES].[TT_SPM_DELIVERY_RUNSHEET] WITH (ROWLOCK)
							SET [SHEET_STATUS] = 9,
								[ACTUAL_ARRIVAL_TIME] = GETDATE()
							WHERE [PLAN_RUNSHEET_NO] = @RUNSHEET_NO
						END
				END
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        --出错，则返回执行不成功，回滚事务
        ROLLBACK TRANSACTION
        --记录错误信息
        INSERT INTO [LES].[TS_SYS_EXCEPTION] ([TIME_STAMP], [APPLICATION], [METHOD], [CLASS], [EXCEPTION_MESSAGE], [ERROR_CODE])
        SELECT GETDATE(), 'INTERFACE', '[LES].[PROC_RPM_PACKAGE_TRAN]', 'Procedure', ERROR_MESSAGE(), ERROR_LINE()
        IF(@Result=0)
            BEGIN
				INSERT INTO [LES].[TS_SYS_EXCEPTION] ([TIME_STAMP], [APPLICATION], [METHOD], [CLASS], [EXCEPTION_MESSAGE], [ERROR_CODE])
				SELECT GETDATE(), 'INTERFACE', '[LES].[PROC_RPM_PACKAGE_TRAN_STOCKS]', 'Procedure', @ResultMessage, ERROR_LINE()
            END
    END CATCH
END