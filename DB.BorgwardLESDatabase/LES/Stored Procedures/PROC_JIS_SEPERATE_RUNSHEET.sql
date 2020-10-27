
/********************************************************************************
*以快速算法，

* 所以以以A00的数据做为计算基础，生成主表
*
*********************************************************************************/
CREATE PROCEDURE [LES].[PROC_JIS_SEPERATE_RUNSHEET]
(
 
  @JisRunsheet int 

)
AS 
BEGIN	
     
    Declare @NextRecord				Integer 
	declare @jisFlexSn				int
	Declare @jisrunsheetsn			int
	declare @jisrunsheetNo			varchar(30)

	DECLARE	@return_value int
      BEGIN TRY
          
	Select @NextRecord = 0

	EXEC	@jisrunsheetsn = [LES].[PROC_JIS_GET_NEXT_SEQUENCE]
			@SequenceName = N'JIS_RUNSHEET_SN'

			select @jisrunsheetNo=SUBSTRING([JIS_RUNSHEET_NO],1,11)+cast(@jisrunsheetsn as varchar(10)) FROM [LES].[TT_JIS_RUNSHEET]
			where [JIS_RUNSHEET_SN]=@JisRunsheet

			
			INSERT INTO [LES].[TT_JIS_RUNSHEET]
			   ([JIS_RUNSHEET_SN]
			   ,[JIS_RUNSHEET_NO]
			   ,[JIS_RUNSHEET_TIME]
			   ,[PLANT]
			   ,[ASSEMBLY_LINE]
			   ,[RACK]
			   ,[SUPPLIER_NUM]
			   ,[WORKSHOP]
			   ,[PLANT_ZONE]
			   ,[LOCATION]
			   ,[JIS_SUPPLIER_SN]
			   ,[DOCK]
			   ,[FIRST_TIME]
			   ,[EXPECTED_ARRIVAL_TIME]
			   ,[SUPPLIER_CONFIRM_TIME]
			   ,[ESTIMATED_ARRIVAL_TIME]
			   ,[ACTUAL_ARRIVAL_TIME]
			   ,[PRINT_TYPE]
			   ,[FORMAT]
			   ,[CARS]
			   ,[START_RUNNING_NO]
			   ,[END_RUNNING_NO]
			   ,[FEEDBACK]
			   ,[BOOKKEEPER]
			   ,[REDO_FLAG]
			   ,[JIS_RUNSHEET_STATUS]
			   ,[SEND_STATUS]
			   ,[SEND_TIME]
			   ,[FAX_STATUS]
			   ,[FAX_TIME]
			   ,[SUPPLY_STATUS]
			   ,[SUPPLY_TIME]
			   ,[SAP_FLAG]
			   ,[RETRY_TIMES]
			   ,[RECKONING_NO]
			   ,[OPERATION_USER]
			   ,[CHECK_USER]
			   ,[TRANS_SUPPLIER_NUM]
			   ,[WMS_SEND_STATUS]
			   ,[WMS_SEND_TIME]
			   ,[COMMENTS]
			   ,[UPDATE_DATE]
			   ,[UPDATE_USER]
			   ,[CREATE_DATE]
			   ,[CREATE_USER])
			   select @jisrunsheetsn
			   ,@jisrunsheetNo
			   ,[JIS_RUNSHEET_TIME]
			   ,[PLANT]
			   ,[ASSEMBLY_LINE]
			   ,[RACK]
			   ,[SUPPLIER_NUM]
			   ,[WORKSHOP]
			   ,[PLANT_ZONE]
			   ,[LOCATION]
			   ,[JIS_SUPPLIER_SN]
			   ,[DOCK]
			   ,[FIRST_TIME]
			   ,[EXPECTED_ARRIVAL_TIME]
			   ,[SUPPLIER_CONFIRM_TIME]
			   ,[ESTIMATED_ARRIVAL_TIME]
			   ,[ACTUAL_ARRIVAL_TIME]
			   ,[PRINT_TYPE]
			   ,[FORMAT]
			   ,[CARS]
			   ,[START_RUNNING_NO]
			   ,[END_RUNNING_NO]
			   ,[FEEDBACK]
			   ,[BOOKKEEPER]
			   ,[REDO_FLAG]
			   ,[JIS_RUNSHEET_STATUS]
			   ,[SEND_STATUS]
			   ,[SEND_TIME]
			   ,[FAX_STATUS]
			   ,[FAX_TIME]
			   ,[SUPPLY_STATUS]
			   ,[SUPPLY_TIME]
			   ,[SAP_FLAG]
			   ,[RETRY_TIMES]
			   ,[RECKONING_NO]
			   ,[OPERATION_USER]
			   ,[CHECK_USER]
			   ,[TRANS_SUPPLIER_NUM]
			   ,[WMS_SEND_STATUS]
			   ,[WMS_SEND_TIME]
			   ,[COMMENTS]
			   ,[UPDATE_DATE]
			   ,[UPDATE_USER]
			   ,[CREATE_DATE]
			   ,[CREATE_USER]
			   from [LES].[TT_JIS_RUNSHEET] where [JIS_RUNSHEET_SN]=@JisRunsheet

	select @NextRecord = min([JIS_RUNSHEET_FLEX_SN])
			from[LES].[TT_JIS_RUNSHEET_FLEX]  where JIS_RUNSHEET_SN=@JisRunsheet  and [JIS_RUNSHEET_FLEX_SN]  > @NextRecord   
	While (@NextRecord is not NULL)

	begin

		

		print 	@NextRecord
		Select @jisFlexSn = [JIS_RUNSHEET_FLEX_SN]
						from [LES].[TT_JIS_RUNSHEET_FLEX] (nolock) 
						where  [JIS_RUNSHEET_FLEX_SN] = @NextRecord 

		 
		
		EXEC	@return_value = [LES].[PROC_JIS_GET_NEXT_SEQUENCE]
			@SequenceName = N'JIS_RUNSHEET_FLEX_SN'

			INSERT INTO [LES].[TT_JIS_RUNSHEET_FLEX]
			   ([JIS_RUNSHEET_FLEX_SN]
			   ,[JIS_RUNSHEET_FLEX_TIME]
			   ,[PLANT]
			   ,[ASSEMBLY_LINE]
			   ,[SUPPLIER_NUM]
			   ,[RACK]
			   ,[BOX_NUMBER]
			   ,[FORMAT]
			   ,[MODEL]
			   ,[CAR_NO]
			   ,[RUNNING_NUMBER]
			   ,[JIS_RUNSHEET_SN]
			   ,[JIS_RUNSHEET_NO]
			   ,[CREATE_DATE]
			   ,[SAP_FLAG]
			   ,[VIN]
			   ,[MODEL_NO]
			   ,[JIS_BOX_SN]
			   ,[PROFILE1]
			   ,[PROFILE2])
			   select 
			   @return_value
			   ,[JIS_RUNSHEET_FLEX_TIME]
			   ,[PLANT]
			   ,[ASSEMBLY_LINE]
			   ,[SUPPLIER_NUM]
			   ,[RACK]
			   ,[BOX_NUMBER]
			   ,[FORMAT]
			   ,[MODEL]
			   ,[CAR_NO]
			   ,[RUNNING_NUMBER]
			   ,@jisrunsheetsn
			   ,@jisrunsheetNo
			   ,[CREATE_DATE]
			   ,[SAP_FLAG]
			   ,[VIN]
			   ,[MODEL_NO]
			   ,[JIS_BOX_SN]
			   ,[PROFILE1]
			   ,[PROFILE2]
			   from [LES].[TT_JIS_RUNSHEET_FLEX] where [JIS_RUNSHEET_FLEX_SN]=@jisFlexSn

			  INSERT INTO [LES].[TT_JIS_RUNSHEET_DETAIL]
			   ([JIS_RUNSHEET_FLEX_SN]
			   ,[JIS_RUNSHEET_BOX_SN]
			   ,[JIS_RUNSHEET_PART_SN]
			   ,[PART_NO]
			   ,[PLANT]
			   ,[ASSEMBLY_LINE]
			   ,[PART_CNAME]
			   ,[USAGE]
			   ,[PART_NICK_NAME]
			   ,[BARCODE_DATA]
			   ,[ORDER_NO]
			   ,[ITEM_NO]
			   ,[COMMENTS])
			   select 
			   @return_value
			   ,@return_value
			   ,[JIS_RUNSHEET_PART_SN]
			   ,[PART_NO]
			   ,[PLANT]
			   ,[ASSEMBLY_LINE]
			   ,[PART_CNAME]
			   ,[USAGE]/2
			   ,[PART_NICK_NAME]
			   ,[BARCODE_DATA]
			   ,[ORDER_NO]
			   ,[ITEM_NO]
			   ,@return_value
			   from [LES].[TT_JIS_RUNSHEET_DETAIL] where  [JIS_RUNSHEET_FLEX_SN]=@jisFlexSn

			   update [LES].[TT_JIS_RUNSHEET_DETAIL] set [USAGE]=[USAGE]/2 where  [JIS_RUNSHEET_FLEX_SN]=@jisFlexSn

			   select @NextRecord = min([JIS_RUNSHEET_FLEX_SN])
			from[LES].[TT_JIS_RUNSHEET_FLEX]  where JIS_RUNSHEET_SN=@JisRunsheet  and [JIS_RUNSHEET_FLEX_SN]  > @NextRecord   
	  end
      END TRY
      BEGIN CATCH
	        --记录错误信息
            INSERT  INTO [LES].[TS_SYS_EXCEPTION]
                    (
                      time_stamp ,
                      [application] ,
                      [METHOD] ,
                      class ,
                      exception_message ,
                      error_code
                    )
                    SELECT  GETDATE() ,
                            'JIS' ,
                            'PROC_JIS_SEPERATE_RUNSHEET' ,
                            'Procedure' ,
                            ERROR_MESSAGE() ,
                            ERROR_LINE()

      END CATCH
END