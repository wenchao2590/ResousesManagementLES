-- =============================================
-- Author:		ZheLiYang
-- Create date: 2017-08-11
-- Description:	创建车载任务
-- =============================================
CREATE PROCEDURE [LES].[PROC_CREATE_ONBOARD_TASK] 
	@TaskNo nvarchar(32) = '', 
	@TaskType int,
	@PartNo nvarchar(32),
	@BoxQty int,
	@PartQty int,
	@TrayNo nvarchar(32),
	@Plant nvarchar(8),
	@SourceWmNo nvarchar(16),
	@SourceZoneNo nvarchar(8),
	@SourceDloc nvarchar(32),
	@TargetWmNo nvarchar(8),
	@TargetZoneNo nvarchar(8),
	@TargetDloc nvarchar(32),
	@LoginUserName nvarchar(32)
AS
BEGIN
	SET NOCOUNT ON;
	declare @GroupId bigint
	declare @PartCname nvarchar(128)
	declare @PartEname nvarchar(128)
	declare @Route nvarchar(64)
	--获取零件送货路径
	select @Route = ISNULL([ROUTE],'') from LES.TM_BAS_PARTS_STOCK with(nolock) where [PART_NO] = @PartNo and [ZONE_NO] = @TargetZoneNo;
--
	select @GroupId = [ID] from LES.TM_BAS_ONBOARD_TASK_GROUP with(nolock) 
	where [VALID_FLAG] = 1 and [TASK_TYPE] = @TaskType and [PLANT] = @Plant and [WM_NO] = @TargetWmNo and [ZONE_NO] = @TargetZoneNo and ISNULL([ROUTE],'') = @Route;

	--获取零件描述信息
	select @PartCname = PART_CNAME,@PartEname = PART_ENAME from LES.TM_BAS_MAINTAIN_PARTS with(nolock) where [PART_NO] = @PartNo;

	insert into [LES].[TT_WMM_ONBOARD_TASK]
	(
	TASK_NO
	, GROUP_ID
	, TASK_TYPE
	, PART_NO
	, PART_CNAME
	, PART_ENAME
	, BOX_QTY
	, RECOMMEND_TRAY_BARCODE
	, PART_QTY
	, PLANT
	, SOURCE_WM_NO
	, SOURCE_ZONE_NO
	, RECOMMEND_SOURCE_DLOC
	, TARGET_WM_NO
	, TARGET_ZONE_NO
	, RECOMMEND_TARGET_DLOC
	, [STATUS]
	,[ROUTE]
	, VALID_FLAG
	, CREATE_DATE
	, CREATE_USER
	)
	values
	(
	@TaskNo
	,@GroupId
	,@TaskType
	,@PartNo
	,@PartCname
	,@PartEname
	,@BoxQty
	,@TrayNo
	,@PartQty
	,@Plant
	,@SourceWmNo
	,@SourceZoneNo
	,@SourceDloc
	,@TargetWmNo
	,@TargetZoneNo
	,@TargetDloc
	,10
	,@Route
	,1
	,GETDATE()
	,@LoginUserName
	)
END