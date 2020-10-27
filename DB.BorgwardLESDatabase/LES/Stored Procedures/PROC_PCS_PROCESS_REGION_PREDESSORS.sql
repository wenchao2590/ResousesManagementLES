
CREATE PROCEDURE [LES].[PROC_PCS_PROCESS_REGION_PREDESSORS]
AS
SET NOCOUNT ON

declare
	@RollBack		Integer,
	@CurrRegionId	Integer,
	@CurrRegionName varchar(20),
	@NextRegionOrder integer,
	@processdate datetime,
	@Plant varchar(5),
	@ASSEMBLY_LINE varchar(15);

Select @RollBack = 0;

CREATE TABLE #Plant_ASSEMBLY_LINE (plant_ASSEMBLY_LINE_Id INT IDENTITY(1,1),plant VARCHAR(5),[ASSEMBLY_LINE] VARCHAR(15))

INSERT INTO #Plant_ASSEMBLY_LINE(plant,[ASSEMBLY_LINE]) SELECT distinct plant,[ASSEMBLY_LINE] FROM  [LES].[TM_PCS_REGION]

DECLARE @plant_ASSEMBLY_LINE_Id INT
SET @plant_ASSEMBLY_LINE_Id = 0

-- clear RegionPredessorForConsumeBackwards
DELETE FROM [LES].[TM_PCS_REGION_PREDESSOR_FOR_CONSUME_BACKWARDS] ;

-- get this process date
select @processdate = getdate();

--遍历所有工厂车间
WHILE(@plant_ASSEMBLY_LINE_Id IS NOT NULL)
BEGIN
	SELECT @plant_ASSEMBLY_LINE_Id = min(plant_ASSEMBLY_LINE_Id)
	FROM #Plant_ASSEMBLY_LINE 
    WHERE plant_ASSEMBLY_LINE_Id>@plant_ASSEMBLY_LINE_Id 
	
	IF (@plant_ASSEMBLY_LINE_Id is not NULL)
	BEGIN
		SELECT @Plant=plant,@ASSEMBLY_LINE=[ASSEMBLY_LINE] FROM #Plant_ASSEMBLY_LINE WHERE plant_ASSEMBLY_LINE_Id=@plant_ASSEMBLY_LINE_Id 
		
		-- 循环处理region前驱后继功能
		Select @NextRegionOrder = 0
		While (@NextRegionOrder is not NULL)
		begin
			select @NextRegionOrder = min(region_order)		  
			  from [LES].[TM_PCS_REGION] (nolock)
			 where plant = @plant and [ASSEMBLY_LINE] = @ASSEMBLY_LINE and [PERMANENT_REGION]=2 and region_order>0
			   AND region_order  > @NextRegionOrder;
			
			If (@@error != 0)
			Begin 
				exec master.dbo.xp_logevent 75000,
				'Production Pull - Select @NextRegionOrder failed in region'
				Select @RollBack = 1
			End
			
			If (@NextRegionOrder is not NULL)
			begin
				Select @CurrRegionName = region_name, @currRegionId = region_identity
				from [LES].[TM_PCS_REGION] (nolock)
				where region_order = @NextRegionOrder and plant=@plant and [ASSEMBLY_LINE]=@ASSEMBLY_LINE

				If (@@error != 0)
				Begin 
					exec master.dbo.xp_logevent 75000,
						'Production Pull - Insert into #TmpPartDetail2 failed in LoadPartDetail'
					Select @RollBack = 1
				End

				insert [LES].[TM_PCS_REGION_PREDESSOR_FOR_CONSUME_BACKWARDS] with (rowlock)
					([REGION_IDENTITY], [REGION_NAME],[PREDESSOR_REGION_ID], [PREDESSOR_REGION], [LAST_REGION_CHECKTIME])
				select @currRegionId, @CurrRegionName, region_identity, region_name, @processdate
					   from [LES].[TM_PCS_REGION](nolock)
					 where plant = @plant and [ASSEMBLY_LINE]=@ASSEMBLY_LINE and [PERMANENT_REGION]=2 and region_order>0
						AND region_order  < @NextRegionOrder
					  order by region_order;

				If (@@error != 0)
				Begin 
					exec master.dbo.xp_logevent 75000,
						'Production Pull - Insert(1) into RegionPredessorForConsumeBackwards failed in region'
					Select @RollBack = 1
				End
			End
		End

	END	
	
END


--删除临时工厂车间表
DROP TABLE #Plant_ASSEMBLY_LINE

return (@RollBack)