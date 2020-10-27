
--创建视图(因为在函数中无法直接使用newid())
create view LES.V_NEWID
as
select newid() N'MacoId';