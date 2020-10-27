﻿
CREATE procedure [dbo].[sp_who_lock]
as
begin
    declare @spid int,@bl int,
    @intTransactionCountOnEntry int,
    @intRowcount int,
    @intCountProperties int,
    @intCounter int

	--查询被锁住的数据库表
	select   request_session_id AS  SPID,OBJECT_NAME(resource_associated_entity_id) AS LockTableName   
	from   sys.dm_tran_locks where resource_type='OBJECT'

    create table #tmp_lock_who (id int identity(1,1),spid smallint,bl smallint)
    IF @@ERROR<>0 RETURN @@ERROR
    insert into #tmp_lock_who(spid,bl) select 0 ,blocked
    from (select * from sys.sysprocesses where blocked>0 ) a
    where not exists(select * from (select * from sys.sysprocesses where blocked>0 ) b
    where a.blocked=spid)
    union select spid,blocked from sys.sysprocesses where blocked>0
    IF @@ERROR<>0 RETURN @@ERROR
        -- 找到临时表的记录数
        select @intCountProperties = Count(*),@intCounter = 1
        from #tmp_lock_who
    IF @@ERROR<>0 RETURN @@ERROR
    if @intCountProperties=0
    select '现在没有阻塞和死锁信息' as message
    -- 循环开始
    while @intCounter <= @intCountProperties
    begin
    -- 取第一条记录
    select @spid = spid,@bl = bl
    from #tmp_lock_who where id = @intCounter
    begin
    if @spid =0
        select '引起数据库死锁的是: '+ CAST(@bl AS VARCHAR(10)) + '进程号,其执行的SQL语法如下'
    else
        select '进程号SPID：'+ CAST(@spid AS VARCHAR(10))+ '被' + '进程号SPID：'+ CAST(@bl AS VARCHAR(10)) +'阻塞,其当前进程执行的SQL语法如下'
    DBCC INPUTBUFFER (@bl )
    end
    -- 循环指针下移
    set @intCounter = @intCounter + 1
    end
    drop table #tmp_lock_who
    return 0
end