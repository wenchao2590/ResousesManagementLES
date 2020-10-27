
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [LES].[PROC_JIS_AFTER_ARICHIVE_DATA_DELETE]
	@productiontime datetime
AS
BEGIN
	declare @max_jis_flex_SN int
	/*delete from [LES].TT_JIS_RUNSHEET where JIS_RUNSHEET_SN IN
	(SELECT JIS_RUNSHEET_SN FROM [LES].TT_JIS_RUNSHEET_FLEX where [JIS_RUNSHEET_FLEX_TIME]<= @productiontime AND SAP_FLAG=1)
	*/
	
	delete from [LES].TT_JIS_RUNSHEET where datediff(day,[JIS_RUNSHEET_TIME],@productiontime)>0 
	--delete from [LES].tt_jis_runsheet_box where [jis_runsheet_flex_sn] 
    --            IN(select JIS_RUNSHEET_FLEX_SN from LES.tt_jis_runsheet_flex where [JIS_RUNSHEET_FLEX_TIME]<= @productiontime AND SAP_FLAG=1)
    --select JIS_RUNSHEET_SN from LES.TT_JIS_Runsheet where [JIS_RUNSHEET_TIME]<= @productiontime and SAP_FLAG=1
    set @productiontime=dateadd(day,2,@productiontime)
    delete from [LES].TT_JIS_RUNSHEET_FLEX where JIS_RUNSHEET_SN not in (select JIS_RUNSHEET_SN from [LES].TT_JIS_RUNSHEET ) and datediff(day,JIS_RUNSHEET_FLEX_TIME,@productiontime)>0 
    
	select @max_jis_flex_SN=max(JIS_RUNSHEET_FLEX_SN) from  [LES].TT_JIS_RUNSHEET_FLEX where datediff(day,getdate()-1,jis_runsheet_flex_time)=0


    delete from [LES].TT_JIS_RUNSHEET_DETAIL where  [JIS_RUNSHEET_FLEX_SN] not IN
    (SELECT JIS_RUNSHEET_FLEX_SN FROM [LES].TT_JIS_RUNSHEET_FLEX )  and [JIS_RUNSHEET_FLEX_SN]<@max_jis_flex_SN

	
END