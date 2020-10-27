--新增模块
if not exists(select * from dbo.TS_SYS_MENU with(nolock) where MENU_NAME = 'GJS_BAS') 
	insert into dbo.TS_SYS_MENU (FID, MENU_NAME, DISPLAY_ORDER, MENU_TYPE, NEED_AUTH, CREATE_USER, CREATE_DATE, VALID_FLAG, MENU_NAME_CN) values (NEWID(), 'GJS_BAS', 10, 40, 0, 'sqlinit', GETDATE(), 1, '基础数据维护');




--删除GJS模块
delete from  dbo.TS_SYS_MENU where MENU_NAME = 'GJS_BAS'



