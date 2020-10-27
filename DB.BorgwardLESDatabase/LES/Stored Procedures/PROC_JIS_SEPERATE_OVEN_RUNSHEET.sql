
/********************************************************************************
*以快速算法，

* 所以以以A00的数据做为计算基础，生成主表
*
*********************************************************************************/
CREATE PROCEDURE [LES].[PROC_JIS_SEPERATE_OVEN_RUNSHEET]
(
 
  @JisRunsheet int 

)
AS 
BEGIN	
     
    Declare @NextRecord				Integer 
	declare @jisFlexSn				int
	Declare @jisrunsheetsn			int
	declare @jisrunsheetNo			varchar(30)
	Declare @i						int
	Declare @RRack					nvarchar(20)
	Declare @LRack					nvarchar(20)
	DECLARE @PLANT					NVARCHAR(5)
	DECLARE	@ASSEMBLY_LINE			NVARCHAR(10)
	Declare @Process_status			int

	DECLARE	@return_value int
      BEGIN TRY
          
	Select @NextRecord = 0
	select @i=1

	EXEC	@jisrunsheetsn = [LES].[PROC_JIS_GET_NEXT_SEQUENCE]
			@SequenceName = N'JIS_RUNSHEET_SN'

			select @jisrunsheetNo=SUBSTRING([JIS_RUNSHEET_NO],1,11)+cast(@jisrunsheetsn as varchar(10)),@PLANT=PLANT,@ASSEMBLY_LINE=ASSEMBLY_LINE,@RRack=RACK FROM [LES].[TT_JIS_RUNSHEET]
			where [JIS_RUNSHEET_SN]=@JisRunsheet

			select @RRack=[RACK_RIGHT],@Process_status=ISNULL(PROCESS_STATUS,0) FROM [LES].[TM_JIS_ODD_EVEN] where PLANT=@PLANT and ASSEMBLY_LINE=@ASSEMBLY_LINE and RACK=@RRack
			if(@Process_status=0)
			return
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
			   ,@RRack
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

		Select @jisFlexSn = [JIS_RUNSHEET_FLEX_SN]
						from [LES].[TT_JIS_RUNSHEET_FLEX] (nolock) 
						where  [JIS_RUNSHEET_FLEX_SN] = @NextRecord 

		 
		IF (@i %2 =0) --偶数拆分到另外一张拉动单
		begin
			UPDATE [LES].[TT_JIS_RUNSHEET_FLEX] set [JIS_RUNSHEET_SN]=  @jisrunsheetsn,Rack=@RRack
			   ,[JIS_RUNSHEET_NO]=@jisrunsheetNo  where [JIS_RUNSHEET_FLEX_SN]=@jisFlexSn
		end
			   select @i=@i+1

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
                            'PROC_JIS_SEPERATE_OVEN_RUNSHEET' ,
                            'Procedure' ,
                            ERROR_MESSAGE() ,
                            ERROR_LINE()

      END CATCH
END