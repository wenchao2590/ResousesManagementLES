--====================================================
--Author:        <liuhq>
--Create date:   <2015-05-08>
--Description:   <保存用户的权限>
--Modify:Andy Liu
--Note:
--====================================================
CREATE PROC [LES].[PROC_SYS_ADD_ROLE_AUTHS]
(
	@xml xml,						--菜单及动作集
	@moduleId int,					--当前模块编号
	@result int = 0 output			--执行结果
)

as


set xact_abort on
begin try
begin transaction		--开始事务


--解析xml内容
declare @idoc int
exec sp_xml_preparedocument @idoc output, @xml
--获取明细信息
select * into #TempAuthDetail	--详细信息
from openxml (@idoc, '/Auth/AuthInfo',2)
with 
(
	roleId int								'@roleId',
	menuType nvarchar(10)					'@menuType',
	resourceId int							'@resourceId'
)

declare @roleId int
select @roleId = roleId from #TempAuthDetail


--删除动作信息
delete from [LES].[TS_SYS_AUTH]
where role_id = @roleId and RESOURCE_TYPE = 'Action' and RESOURCE_ID in
(
	select Action_id from [LES].[TS_SYS_MENU_ACTION]
	where MENU_ID in
	(
		select Menu_id from  [LES].[TM_SYS_MENU]
		where PARENT_MENU_ID = @moduleId or Menu_id = @moduleId
		or PARENT_MENU_ID in (select menu_id from [LES].[TM_SYS_MENU] where PARENT_MENU_ID = @moduleId)
	)
)

--删除菜单信息
delete from [LES].[TS_SYS_AUTH]
where role_id = @roleId and RESOURCE_TYPE = 'Menu' and RESOURCE_ID in
(
	select Menu_id from  [LES].[TM_SYS_MENU]
	where PARENT_MENU_ID = @moduleId or Menu_id = @moduleId
	or PARENT_MENU_ID in (select menu_id from [LES].[TM_SYS_MENU] where PARENT_MENU_ID = @moduleId)
)


--添加菜单信息
insert into [LES].[TS_SYS_AUTH]
    ([ROLE_ID]
    ,[RESOURCE_TYPE]
    ,[RESOURCE_NAME]
    ,[RESOURCE_ID])
select distinct @roleId,
    'Menu',
    A.MENU_NAME,
    A.MENU_ID
from LES.TM_SYS_MENU A 
inner join #TempAuthDetail B on A.MENU_ID = B.resourceId 
WHERE B.menuType = 'Menu'

--添加动作信息
insert into [LES].[TS_SYS_AUTH]
    ([ROLE_ID]
    ,[RESOURCE_TYPE]
    ,[RESOURCE_NAME]
    ,[RESOURCE_ID])
select distinct @roleId,
    'Action',
    A.ACTION_NAME,
	A.ACTION_ID
from LES.TS_SYS_ACTION A 
inner join #TempAuthDetail B on A.ACTION_ID = B.resourceId 
WHERE B.menuType = 'Action'


drop table #TempAuthDetail


--执行成功，提交事务，返回执行成功
commit transaction
set @result = 1
return
end try

begin catch
--出错，则返回执行不成功，回滚事务
rollback transaction
set @result = 0

return
end catch