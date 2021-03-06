﻿
-- =============================================
-- Author:		luchao
-- Create date: 2012-02-03
-- Description:	复制用户设置关联的数据
-- 角色-用户关联,流水线-用户关联,产品-用户关联,JIS/PCS/TWD零件类-用户关联
-- =============================================
CREATE PROCEDURE [LES].[PROC_SYS_USER_RELATION_CONFIGS_COPY]
	@OrientUserId int,
	@NowUserId int,
	@OperateUser nvarchar(50),
	@Result int output,						--检查结果，0表示成功，-1表示失败
	@ResultMessage nvarchar(1000) output 	--结果消息
AS
BEGIN

	SET @Result = 0
	SET NOCOUNT ON;
	
	-- 1.设置角色-用户关联表数据复制
	
	BEGIN
		INSERT INTO LES.TR_SYS_USER_ROLE(ROLE_ID, USER_ID, CREATE_USER, CREATE_DATE)
		SELECT ROLE_ID,@NowUserId,@OperateUser,GETDATE() FROM LES.TR_SYS_USER_ROLE WITH(NOLOCK) WHERE USER_ID=@OrientUserId
	END
	
	-- 2.设置流水线-用户关联表,产品-用户关联
	BEGIN
		INSERT INTO LES.TS_SYS_SEARCH_FILTER(USER_ID, FILTER_NAME, FILTER_CODE, FILTER_VALUE, FILTER_VALUE_NAME, CHILD_FILTER_NAME, CHILD_FILTER_CODE, CHILD_FILTER_VALUE, CHILD_FILTER_VALUE_NAME, CREATE_USER, CREATE_DATE, UPDATE_USER, UPDATE_DATE)
		SELECT @NowUserId,FILTER_NAME, FILTER_CODE, FILTER_VALUE, FILTER_VALUE_NAME, CHILD_FILTER_NAME, CHILD_FILTER_CODE, CHILD_FILTER_VALUE, CHILD_FILTER_VALUE_NAME, @OperateUser, GETDATE(), @OperateUser, GETDATE() FROM LES.TS_SYS_SEARCH_FILTER 
		WHERE USER_ID=@OrientUserId and FILTER_CODE != 'Supplier_Num'	
	END
	
	-- 3.JIS/PCS/TWD零件类-用户关联
	BEGIN
		INSERT INTO LES.TR_SYS_USER_BOX_PARTS(USER_ID, PLANT, ASSEMBLY_LINE, PLANT_ZONE, WORKSHOP, SUPPLIER_NUM, BOX_PARTS, MODEL_TYPE, COMMENTS, CREATE_USER, CREATE_DATE, UPDATE_USER, UPDATE_DATE)
		SELECT @NowUserId, PLANT, ASSEMBLY_LINE, PLANT_ZONE, WORKSHOP, SUPPLIER_NUM, BOX_PARTS, MODEL_TYPE, COMMENTS, @OperateUser, GETDATE(), NULL, NULL FROM LES.TR_SYS_USER_BOX_PARTS WHERE USER_ID=@OrientUserId
	END
END