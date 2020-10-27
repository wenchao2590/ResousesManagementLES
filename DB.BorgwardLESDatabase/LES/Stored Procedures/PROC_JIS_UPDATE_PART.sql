
CREATE PROCEDURE [LES].[PROC_JIS_UPDATE_PART]
(
@Jis_Runsheet_Flex_Sn INT,					--车架序号
@Jis_Runsheet_Box_Sn INT,					--盒子序号
@OldPartNo varchar(20),						--老零件号
@NewPartNo varchar(20),						--新零件号
@NewUsage INT,								--新用量
@BatchExecFlag INT,							--标志0 批量更新，1只针对该物料单
@CarModule varchar(10),						--是否按车型匹配 
@UpdateRows INT OUTPUT
)
AS
SET NOCOUNT ON
BEGIN TRAN

declare @plant varchar(5)					--工厂
declare @assemblyLine varchar(10)			--流水线
declare @supplierNum varchar(8)				--供应商
declare @rack varchar(20)					--料架
declare @PartCname varchar(100)				--零件名称
declare @PartNickName varchar(30)			--零件名称

--取得相关信息
select @plant=plant,@assemblyLine=assembly_line,@supplierNum=supplier_num,@rack=rack 
from LES.TT_JIS_Runsheet_Flex 
where jis_runsheet_flex_sn=@Jis_Runsheet_Flex_Sn

--取得零件的中文名称 
SELECT TOP 1 @PartCname=part_cname,@PartNickName=part_nickname
from LES.TM_BAS_INHOUSE_PULL_LOGISTIC_STANDARD
where plant=@plant and assembly_line=@assemblyLine and part_no=@NewPartNo

if(@BatchExecFlag=1)						--批量更新
begin
	if(len(@CarModule)=0)					--不按车型匹配
	begin
		update B set B.part_no=@NewPartNo,B.part_cname=@PartCname,B.Part_Nick_Name = @PartNickName,USAGE = @NewUsage
		from [LES].[TT_JIS_Runsheet_Box] A,[LES].[TT_JIS_Runsheet_Detail] B 
		where A.[jis_runsheet_flex_sn]=B.[jis_runsheet_flex_sn]
				and   A.[jis_runsheet_box_sn]=B.[jis_runsheet_box_sn]
				and A.[jis_runsheet_flex_sn]>=@Jis_Runsheet_Flex_Sn
				and A.[jis_runsheet_box_sn]>=@Jis_Runsheet_Box_Sn
				and B.part_no=@OldPartNo 
				and A.[jis_runsheet_flex_sn] in (select distinct [jis_runsheet_flex_sn] from [LES].[TT_JIS_Runsheet_Flex] 
				where jis_runsheet_sn  in (select distinct jis_runsheet_sn from LES.TT_JIS_Runsheet where sap_flag=0))
	end 
	ELSE									--按车型匹配
	begin
		update B set B.part_no=@NewPartNo,B.part_cname=@PartCname,B.Part_Nick_Name = @PartNickName,USAGE = @NewUsage
		from [LES].[TT_JIS_Runsheet_Box] A,[LES].[TT_JIS_Runsheet_Detail] B 
		where A.[jis_runsheet_flex_sn]=B.[jis_runsheet_flex_sn]
				and   A.[jis_runsheet_box_sn]=B.[jis_runsheet_box_sn]
				and A.[jis_runsheet_flex_sn]>=@Jis_Runsheet_Flex_Sn
				and A.[jis_runsheet_box_sn]>=@Jis_Runsheet_Box_Sn
				and A.model_no=@CarModule
				and B.part_no=@OldPartNo
				and A.[jis_runsheet_flex_sn] in (select distinct [jis_runsheet_flex_sn] from [LES].[TT_JIS_Runsheet_Flex] 
		where jis_runsheet_sn  in (select distinct jis_runsheet_sn from LES.TT_JIS_Runsheet where Sap_flag=0))
	end
end
else										--不批量更新
begin
	if(len(@CarModule)=0)					--不按车型匹配
	begin
		update B set B.part_no=@NewPartNo,B.part_cname=@PartCname,B.Part_Nick_Name = @PartNickName,USAGE = @NewUsage
		from [LES].[TT_JIS_Runsheet_Box] A,[LES].[TT_JIS_Runsheet_Detail] B 
		where A.[jis_runsheet_flex_sn]=B.[jis_runsheet_flex_sn]
		and   A.[jis_runsheet_box_sn]=B.[jis_runsheet_box_sn]
		and A.[jis_runsheet_flex_sn]=@Jis_Runsheet_Flex_Sn
		and A.[jis_runsheet_box_sn]=@Jis_Runsheet_Box_Sn
		and B.part_no=@OldPartNo
		and A.[jis_runsheet_flex_sn] in (select distinct [jis_runsheet_flex_sn] from [LES].[TT_JIS_Runsheet_Flex] 
		where jis_runsheet_sn  in (select distinct jis_runsheet_sn from LES.TT_JIS_Runsheet where sap_flag=0))
	end
	ELSE									--按车型匹配
	begin
		update B set B.part_no=@NewPartNo,B.part_cname=@PartCname,B.Part_Nick_Name = @PartNickName,USAGE = @NewUsage
		from [LES].[TT_JIS_Runsheet_Box] A,[LES].[TT_JIS_Runsheet_Detail] B 
		where A.[jis_runsheet_flex_sn]=B.[jis_runsheet_flex_sn]
		and   A.[jis_runsheet_box_sn]=B.[jis_runsheet_box_sn]
		and A.[jis_runsheet_flex_sn]=@Jis_Runsheet_Flex_Sn
		and A.[jis_runsheet_box_sn]=@Jis_Runsheet_Box_Sn
		and A.model_no=@CarModule
		and B.part_no=@OldPartNo
		and A.[jis_runsheet_flex_sn] in (select distinct [jis_runsheet_flex_sn] from [LES].[TT_JIS_Runsheet_Flex] 
		where jis_runsheet_sn  in (select distinct jis_runsheet_sn from LES.TT_JIS_Runsheet where sap_flag=0))
	end
end
set @UpdateRows=@@ROWCOUNT
COMMIT TRAN
SET NOCOUNT OFF