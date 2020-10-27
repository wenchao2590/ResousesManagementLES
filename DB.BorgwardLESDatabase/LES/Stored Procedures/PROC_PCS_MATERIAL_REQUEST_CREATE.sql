/********************************************************************/
/*   Program Name:  PROC_PCS_Material_Request_Create                */
/*                                                                  */
/*   Called By:     Automatically started by the Service            */
/*   author   :     xuehaijun                                                               */
/*   Purpose:       This is the main stored procedure to read all   */
/*                  Parts from [TT_PCS_COUNTER] whose quantity is less*/
/*                  than 0 and create a material request for each of*/
/*                  them. Then one packs will be added for each part*/	
/********************************************************************/

CREATE PROCEDURE [LES].[PROC_PCS_MATERIAL_REQUEST_CREATE]
AS --产生拉动

--TRUNCATE TABLE LES.TE_PCS_COUNTER_DELIVERY

BEGIN TRAN
BEGIN TRY

		--生成空箱指令阀值不为空的数据
      INSERT    INTO [LES].[TI_PCS_MATERIAL_REQUESTS]
                (
                  [ASSEMBLY_LINE] ,
                  [PLANT] ,
                  [LOCATION] ,
                  [REQUEST_TIME] ,
                  [INTERFACE_STATUS] ,
                  [PROCESS_TIME] ,
                  [PART_NO] ,
                  [PART_CNAME] ,
                  [PART_ENAME] ,
                  [SUPPLIER_NUM] ,
                  [DOCK] ,
                  [BOX_PARTS] ,
                  [INTERFACE_TYPE] ,
                  [PACK_COUNT] ,
                  [INHOUSE_PACKAGE_MODEL] ,
                  [INHOUSE_PACKAGE] ,
                  [MEASURING_UNIT_NO] ,
                  [EXPECTED_ARRIVAL_TIME] ,
                  [IS_ORGANIZE_SHEET] ,
                  [SEND_STATUS] ,
                  [IS_CANCEL] ,
                  [WMS_SEND_STATUS] ,
                  [CREATE_DATE] ,
                  [CREATE_USER]
                )
        SELECT  A.[ASSEMBLY_LINE] ,
                A.[PLANT] ,
                A.[LOCATION] ,
                GETDATE() AS [REQUEST_TIME] ,
                '' AS [INTERFACE_STATUS] ,
                NULL AS [PROCESS_TIME] ,
                A.[PART_NO] ,
                A.[PART_CNAME] ,
                A.[PART_ENAME] ,
                B.[SUPPLIER_NUM] ,
                A.[DOCK] ,
                A.[IN_PLANT_LOGISTIC_PART_CLASS] ,
                '' [INTERFACE_TYPE] ,
                1 AS [PACK_COUNT] ,
                A.[INHOUSE_PACKAGE_MODEL] ,
                A.[INHOUSE_PACKAGE] ,
                ISNULL(A.[MEASURING_UNIT_NO], 0) ,
                [LES].Func_Get_PCS_Expect_Arrival_Time(A.[PLANT],A.[ASSEMBLY_LINE],A.[IN_PLANT_LOGISTIC_PART_CLASS],B.[SUPPLIER_NUM],GETDATE()) AS [EXPECTED_ARRIVAL_TIME],
                B.[IS_ORGANIZE_SHEET] ,
                ISNULL(b.[BOX_PARTS_STATE], 2) ,
                0 AS [IS_CANCEL] ,
                ISNULL(b.[BOX_PARTS_STATE], 2) ,
                GETDATE() AS [CREATE_DATE] ,
                1 AS [CREATE_USER]
        FROM    [LES].[TT_PCS_COUNTER] A
        INNER JOIN LES.TM_PCS_ROUTE_BOX_PARTS B
        ON      A.[PLANT] = B.[PLANT]
        AND A.[ASSEMBLY_LINE] = B.[ASSEMBLY_LINE]
        AND A.[SUPPLIER_NUM] = B.[SUPPLIER_NUM]
        AND A.[IN_PLANT_LOGISTIC_PART_CLASS] = B.[BOX_PARTS]
        WHERE   A.CURRENT_PART_COUNT <= isnull(EMPTY_PART_COUNT, 0)
        --AND B.[BOX_PARTS_STATE] != 3
        
	  --更新库存
      UPDATE    [LES].[TT_PCS_COUNTER]
      SET       CURRENT_PART_COUNT = CURRENT_PART_COUNT + ISNULL(A.[INHOUSE_PACKAGE], 0) ,
                [UPDATE_DATE] = GETDATE()
      FROM      [LES].[TT_PCS_COUNTER] A
      INNER JOIN LES.TM_PCS_ROUTE_BOX_PARTS C
      ON A.[PLANT] = C.[PLANT]
      AND A.[ASSEMBLY_LINE] = C.[ASSEMBLY_LINE]
      AND A.[SUPPLIER_NUM] = C.[SUPPLIER_NUM]
      AND A.[IN_PLANT_LOGISTIC_PART_CLASS] = C.[BOX_PARTS]
      WHERE  A.CURRENT_PART_COUNT<=isnull(EMPTY_PART_COUNT, 0) 
      --AND C.[BOX_PARTS_STATE] != 3	  
      COMMIT TRAN 
END TRY
BEGIN CATCH
      ROLLBACK TRAN
	  --记录错误信息
      INSERT    INTO [LES].[TS_SYS_EXCEPTION]
                (
                  time_stamp ,
                  [application] ,
                  [METHOD] ,
                  class ,
                  exception_message ,
                  error_code
                )
                SELECT  GETDATE() ,
                        'PCS' ,
                        'TE_PCS_COUNTER_DELIVERY' ,
                        'Procedure' ,
                        ERROR_MESSAGE() ,
                        ERROR_LINE()
END CATCH