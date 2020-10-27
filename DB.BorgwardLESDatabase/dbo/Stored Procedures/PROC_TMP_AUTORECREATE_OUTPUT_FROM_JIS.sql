-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[PROC_TMP_AUTORECREATE_OUTPUT_FROM_JIS]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--日志
		
		
    
	DECLARE @SN INT
	DECLARE @OUT_ID INT

	DECLARE csCheck CURSOR
	FOR
		select JIS_RUNSHEET_SN,OUTPUT_ID from
		(
			select *,(ISNULL(JIS_COUNT,0) - ISNULL(OUT_COUNT,0)) AS DIFF_COUNT from
			(
				SELECT T_JIS.*,m.OUTPUT_ID,m.OUTPUT_NO,m.CONFIRM_FLAG,(select count(*) from [LES].[TT_WMM_OUTPUT_DETAIL] n where n.OUTPUT_ID=m.OUTPUT_ID) OUT_COUNT
					FROM
					(
						select JIS_RUNSHEET_SN,JIS_RUNSHEET_NO,SUPPLIER_NUM,count(*) JIS_COUNT
							from 
							(
								select  a.JIS_RUNSHEET_SN,a.JIS_RUNSHEET_NO,a.PLANT,a.ASSEMBLY_LINE,a.SUPPLIER_NUM,a.RACK,b.JIS_RUNSHEET_FLEX_SN,
															b.JIS_BOX_SN, b.MODEL, b.CAR_NO,b.MODEL_NO,d.JIS_RUNSHEET_BOX_SN,d.JIS_RUNSHEET_PART_SN,d.PART_NO, 
															d.PART_CNAME, d.USAGE, d.PART_NICK_NAME,b.RUNNING_NUMBER
									from    LES.TT_JIS_RUNSHEET a
											inner join LES.TT_JIS_RUNSHEET_FLEX b on a.JIS_RUNSHEET_SN = b.JIS_RUNSHEET_SN
											inner join LES.TT_JIS_RUNSHEET_DETAIL d on  b.JIS_RUNSHEET_FLEX_SN = d.JIS_RUNSHEET_FLEX_SN
									where USAGE>0
							) as T_JIS_TMP
							group by JIS_RUNSHEET_SN,JIS_RUNSHEET_NO,SUPPLIER_NUM
					) AS T_JIS
					inner join [LES].[TT_WMM_OUTPUT] m on T_JIS.JIS_RUNSHEET_NO=m.OUTPUT_NO
			) AS T_RES_TMP
		) AS T_RES
		WHERE DIFF_COUNT!=0 AND CONFIRM_FLAG=1 AND JIS_RUNSHEET_SN>25019828
		ORDER BY JIS_RUNSHEET_NO DESC
	OPEN csCheck	--打开游标
	FETCH NEXT FROM csCheck INTO @SN,@OUT_ID
	WHILE @@FETCH_STATUS=0	--判断是否成功获取数据
	BEGIN

		EXEC [dbo].[PROC_TMP_RECREATE_WMMOUTPUT_FROM_JISRUNSHEETNO] @JIS_RUNSHEET_SN = @SN,@OUTPUT_ID = @OUT_ID

		--日志
		INSERT INTO [dbo].[TEST_LOG] ([des]) values ('Recreate WMM_OUTPUT M&D records.JIS_RUNSHEET_NO:' + cast(@SN AS NVARCHAR))
		--游标下移
		FETCH NEXT FROM csCheck INTO @SN,@OUT_ID	--将游标向下移行
	END
	--关闭游标
	CLOSE csCheck
	DEALLOCATE csCheck


END