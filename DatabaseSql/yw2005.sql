CREATE DATABASE [yw2005]  
	 ON  PRIMARY (NAME = N'yw2005', FILENAME = N'E:\DATABASE\yw2005.mdf' , SIZE = 100, FILEGROWTH = 10%),
	 FILEGROUP [fjk] ( NAME = N'fjk', FILENAME = N'E:\DATABASE\yw2005_data_fjk.ndf' , SIZE = 100 , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
	LOG ON (NAME = N'yw2005_log', FILENAME = N'E:\DATABASE\yw2005_log.LDF' , SIZE = 100, FILEGROWTH = 10%)
 COLLATE Chinese_PRC_CI_AS
GO


--<auto_option> 
if cast(left(cast(SERVERPROPERTY('ProductVersion') as varchar(128)),charindex('.',cast(SERVERPROPERTY('ProductVersion') as varchar(128))) - 1) as int) < 11
	exec sp_dboption N'yw2005', N'autoclose', N'false'
else
	alter database [yw2005] set AUTO_CLOSE off
GO

if cast(left(cast(SERVERPROPERTY('ProductVersion') as varchar(128)),charindex('.',cast(SERVERPROPERTY('ProductVersion') as varchar(128))) - 1) as int) < 11
	exec sp_dboption N'yw2005', N'auto create statistics', N'true'
else
	alter database [yw2005] set AUTO_CREATE_STATISTICS on
GO

if cast(left(cast(SERVERPROPERTY('ProductVersion') as varchar(128)),charindex('.',cast(SERVERPROPERTY('ProductVersion') as varchar(128))) - 1) as int) < 11
	exec sp_dboption N'yw2005', N'auto update statistics', N'true'
else
	alter database [yw2005] set AUTO_UPDATE_STATISTICS on
GO

if cast(left(cast(SERVERPROPERTY('ProductVersion') as varchar(128)),charindex('.',cast(SERVERPROPERTY('ProductVersion') as varchar(128))) - 1) as int) < 11
	exec sp_dboption N'yw2005', N'autoshrink', N'false'
else
	alter database [yw2005] set AUTO_SHRINK off
GO

--2012中无此选项 
if cast(left(cast(SERVERPROPERTY('ProductVersion') as varchar(128)),charindex('.',cast(SERVERPROPERTY('ProductVersion') as varchar(128))) - 1) as int) < 11
	exec sp_dboption N'yw2005', N'bulkcopy', N'false'
GO

--<db_update_option> 
if cast(left(cast(SERVERPROPERTY('ProductVersion') as varchar(128)),charindex('.',cast(SERVERPROPERTY('ProductVersion') as varchar(128))) - 1) as int) < 11
	exec sp_dboption N'yw2005', N'read only', N'false'
else
	alter database [yw2005] set READ_WRITE
GO

--<db_user_access_option>
if cast(left(cast(SERVERPROPERTY('ProductVersion') as varchar(128)),charindex('.',cast(SERVERPROPERTY('ProductVersion') as varchar(128))) - 1) as int) < 11
	exec sp_dboption N'yw2005', N'dbo use', N'false'
else
	alter database [yw2005] set MULTI_USER 
GO

--<recovery_option> 
if cast(left(cast(SERVERPROPERTY('ProductVersion') as varchar(128)),charindex('.',cast(SERVERPROPERTY('ProductVersion') as varchar(128))) - 1) as int) < 11
	exec sp_dboption N'yw2005', N'single', N'false'
else
	alter database [yw2005] set RECOVERY SIMPLE 
GO

--以后将停用此功能
--alter database [yw2005] set TORN_PAGE_DETECTION on 
--替换功能为
if cast(left(cast(SERVERPROPERTY('ProductVersion') as varchar(128)),charindex('.',cast(SERVERPROPERTY('ProductVersion') as varchar(128))) - 1) as int) < 11
	exec sp_dboption N'yw2005', N'torn page detection', N'true'
else
begin
	declare @sql varchar(250)
	select @sql = 'alter database [yw2005] set PAGE_VERIFY TORN_PAGE_DETECTION'
	execute (@sql)
end
	
GO

--2012中无此选项 
if cast(left(cast(SERVERPROPERTY('ProductVersion') as varchar(128)),charindex('.',cast(SERVERPROPERTY('ProductVersion') as varchar(128))) - 1) as int) < 11
	exec sp_dboption N'yw2005', N'trunc. log', N'false'
GO

--<sql_option> 
if cast(left(cast(SERVERPROPERTY('ProductVersion') as varchar(128)),charindex('.',cast(SERVERPROPERTY('ProductVersion') as varchar(128))) - 1) as int) < 11
	exec sp_dboption N'yw2005', N'ANSI null default', N'false'
else
	alter database [yw2005] set ANSI_NULL_DEFAULT off
GO

if cast(left(cast(SERVERPROPERTY('ProductVersion') as varchar(128)),charindex('.',cast(SERVERPROPERTY('ProductVersion') as varchar(128))) - 1) as int) < 11
	exec sp_dboption N'yw2005', N'recursive triggers', N'false'
else
	alter database [yw2005] set RECURSIVE_TRIGGERS off
GO

if cast(left(cast(SERVERPROPERTY('ProductVersion') as varchar(128)),charindex('.',cast(SERVERPROPERTY('ProductVersion') as varchar(128))) - 1) as int) < 11
	exec sp_dboption N'yw2005', N'ANSI nulls', N'false'
else
	alter database [yw2005] set ANSI_NULLS off
GO

if cast(left(cast(SERVERPROPERTY('ProductVersion') as varchar(128)),charindex('.',cast(SERVERPROPERTY('ProductVersion') as varchar(128))) - 1) as int) < 11
	exec sp_dboption N'yw2005', N'concat null yields null', N'false'
else
	alter database [yw2005] set CONCAT_NULL_YIELDS_NULL off
GO

if cast(left(cast(SERVERPROPERTY('ProductVersion') as varchar(128)),charindex('.',cast(SERVERPROPERTY('ProductVersion') as varchar(128))) - 1) as int) < 11
	exec sp_dboption N'yw2005', N'quoted identifier', N'false'
else
	alter database [yw2005] set QUOTED_IDENTIFIER off
GO

if cast(left(cast(SERVERPROPERTY('ProductVersion') as varchar(128)),charindex('.',cast(SERVERPROPERTY('ProductVersion') as varchar(128))) - 1) as int) < 11
	exec sp_dboption N'yw2005', N'ANSI warnings', N'false'
else
	alter database [yw2005] set ANSI_WARNINGS off
GO

--<cursor_option> 
if cast(left(cast(SERVERPROPERTY('ProductVersion') as varchar(128)),charindex('.',cast(SERVERPROPERTY('ProductVersion') as varchar(128))) - 1) as int) < 11
	exec sp_dboption N'yw2005', N'cursor close on commit', N'false'
else
	alter database [yw2005] set CURSOR_CLOSE_ON_COMMIT off
GO

if cast(left(cast(SERVERPROPERTY('ProductVersion') as varchar(128)),charindex('.',cast(SERVERPROPERTY('ProductVersion') as varchar(128))) - 1) as int) < 11
	exec sp_dboption N'yw2005', N'default to local cursor', N'false'
else
	alter database [yw2005] set CURSOR_DEFAULT GLOBAL
GO

--<recovery_option>
alter database [yw2005] SET RECOVERY SIMPLE
GO

--<external_access_option>
if cast(left(cast(SERVERPROPERTY('ProductVersion') as varchar(128)),charindex('.',cast(SERVERPROPERTY('ProductVersion') as varchar(128))) - 1) as int) < 11
	exec sp_dboption N'yw2005', N'db chaining', N'false'
else
begin
	declare @sql varchar(250)
	select @sql = 'alter database [yw2005] SET DB_CHAINING off'
	execute (@sql)
end
GO

use [yw2005]
GO

SET ANSI_PADDING off  --default:ON:尾随空格，Off:尾去空格
GO

CREATE DEFAULT con_empty AS ''
GO

CREATE DEFAULT con_zero AS 0
GO


CREATE TABLE [_bm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bm] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzwm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xj] [numeric](1, 0) NOT NULL ,
	[jz] [numeric](1, 0) NOT NULL ,
	[st] [numeric](1, 0) NOT NULL ,
	[ty] [numeric](1, 0) NOT NULL ,
	[cs] [numeric](1, 0) NOT NULL ,
	[wh] [numeric](1, 0) NOT NULL ,
	[bzwm2] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzwm3] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK__bm] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [a_act] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_A_ACT] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [a_bd] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjbm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_A_BD] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [a_bdlx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_a_bdlx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [a_ck] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jdxh] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jdmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[th] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tj] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckjd] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cksm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_A_CK] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [a_dl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fqb] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fdm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fmc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fldm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[flmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wt] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wta] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dl] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dlid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dlg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dlgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dla] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dlaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjqb] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ksrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kssj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ksrqsj] [varchar] (17) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jssj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsrqsj] [varchar] (17) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cj] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cja] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_A_DL] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [a_duty] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_A_DUTY] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [a_f] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fldm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[flmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bddm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bdmc] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dyl] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[new_bef] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[new_aft] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cncl_aft] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[form_env] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mklx] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mkv] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mf1] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_a_f] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [a_fk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sw_fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[swh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ffrdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ffr] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ffrmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[biz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsyh] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nszh] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yshj] [numeric](19, 2) NOT NULL ,
	[sssj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sshj] [numeric](19, 2) NOT NULL ,
	[fkdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fkmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsyhdm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsyhmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsxjdm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsxjmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xz] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pzh] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wpxz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yw] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzbz] [varchar] (254) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_a_fk] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [a_fkmx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[swsf_fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmh1] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfnr1] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yshj] [numeric](19, 2) NOT NULL ,
	[ysfph] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzl] [numeric](2, 0) NOT NULL ,
	[fpdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fpmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dzh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bkzzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_a_fkmx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [a_flx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_a_flx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [a_fp] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[swh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysfph] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[expid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bkzzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fpdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fpmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ffrdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nstt] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsid] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsjydz] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsyh] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfnr1] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfnr2] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfnr3] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfnr4] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfnr5] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfnr6] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfnr7] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfnr8] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gg1] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gg2] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gg3] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gg4] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gg5] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gg6] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gg7] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gg8] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc1] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc2] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc3] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc4] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc5] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc6] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc7] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc8] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sl1] [numeric](19, 4) NOT NULL ,
	[sl2] [numeric](19, 4) NOT NULL ,
	[sl3] [numeric](19, 4) NOT NULL ,
	[sl4] [numeric](19, 4) NOT NULL ,
	[sl5] [numeric](19, 4) NOT NULL ,
	[sl6] [numeric](19, 4) NOT NULL ,
	[sl7] [numeric](19, 4) NOT NULL ,
	[sl8] [numeric](19, 4) NOT NULL ,
	[dk1] [numeric](19, 11) NOT NULL ,
	[dk2] [numeric](19, 11) NOT NULL ,
	[dk3] [numeric](19, 11) NOT NULL ,
	[dk4] [numeric](19, 11) NOT NULL ,
	[dk5] [numeric](19, 11) NOT NULL ,
	[dk6] [numeric](19, 11) NOT NULL ,
	[dk7] [numeric](19, 11) NOT NULL ,
	[dk8] [numeric](19, 11) NOT NULL ,
	[dj1] [numeric](19, 11) NOT NULL ,
	[dj2] [numeric](19, 11) NOT NULL ,
	[dj3] [numeric](19, 11) NOT NULL ,
	[dj4] [numeric](19, 11) NOT NULL ,
	[dj5] [numeric](19, 11) NOT NULL ,
	[dj6] [numeric](19, 11) NOT NULL ,
	[dj7] [numeric](19, 11) NOT NULL ,
	[dj8] [numeric](19, 11) NOT NULL ,
	[jk1] [numeric](19, 2) NOT NULL ,
	[jk2] [numeric](19, 2) NOT NULL ,
	[jk3] [numeric](19, 2) NOT NULL ,
	[jk4] [numeric](19, 2) NOT NULL ,
	[jk5] [numeric](19, 2) NOT NULL ,
	[jk6] [numeric](19, 2) NOT NULL ,
	[jk7] [numeric](19, 2) NOT NULL ,
	[jk8] [numeric](19, 2) NOT NULL ,
	[je1] [numeric](19, 2) NOT NULL ,
	[je2] [numeric](19, 2) NOT NULL ,
	[je3] [numeric](19, 2) NOT NULL ,
	[je4] [numeric](19, 2) NOT NULL ,
	[je5] [numeric](19, 2) NOT NULL ,
	[je6] [numeric](19, 2) NOT NULL ,
	[je7] [numeric](19, 2) NOT NULL ,
	[je8] [numeric](19, 2) NOT NULL ,
	[tax1] [numeric](19, 2) NOT NULL ,
	[tax2] [numeric](19, 2) NOT NULL ,
	[tax3] [numeric](19, 2) NOT NULL ,
	[tax4] [numeric](19, 2) NOT NULL ,
	[tax5] [numeric](19, 2) NOT NULL ,
	[tax6] [numeric](19, 2) NOT NULL ,
	[tax7] [numeric](19, 2) NOT NULL ,
	[tax8] [numeric](19, 2) NOT NULL ,
	[zzl] [numeric](2, 0) NOT NULL ,
	[zzj] [numeric](19, 2) NOT NULL ,
	[zzs] [numeric](19, 2) NOT NULL ,
	[kpex] [numeric](19, 2) NOT NULL ,
	[dx] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nstt2] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsid2] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsjydz2] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsyh2] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fidlst] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1lst] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sqbz] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsrid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzj2] [numeric](19, 2) NOT NULL ,
	[zzs2] [numeric](19, 2) NOT NULL ,
	[kpex2] [numeric](19, 2) NOT NULL ,
	[xkp] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sw_fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsjg] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_A_FP] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [a_gd] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sw_fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[swh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[swsf_fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fl] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ly] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ffrdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ffr] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glrid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glzid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glz] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glraid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glra] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[biz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzl] [numeric](2, 0) NOT NULL ,
	[fpdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fpmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (254) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hcbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhxgrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywmc] [numeric](1, 0) NOT NULL ,
	[dd] [numeric](1, 0) NOT NULL ,
	[sd] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sdr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sds] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fyqr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bll] [numeric](1, 0) NOT NULL ,
	[xmh1] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmh2] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmh3] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmh4] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmh5] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmh6] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmh7] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmh8] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfnr1] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfnr2] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfnr3] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfnr4] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfnr5] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfnr6] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfnr7] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfnr8] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dj1] [numeric](19, 3) NOT NULL ,
	[dj2] [numeric](19, 3) NOT NULL ,
	[dj3] [numeric](19, 3) NOT NULL ,
	[dj4] [numeric](19, 3) NOT NULL ,
	[dj5] [numeric](19, 3) NOT NULL ,
	[dj6] [numeric](19, 3) NOT NULL ,
	[dj7] [numeric](19, 3) NOT NULL ,
	[dj8] [numeric](19, 3) NOT NULL ,
	[sl1] [numeric](19, 4) NOT NULL ,
	[sl2] [numeric](19, 4) NOT NULL ,
	[sl3] [numeric](19, 4) NOT NULL ,
	[sl4] [numeric](19, 4) NOT NULL ,
	[sl5] [numeric](19, 4) NOT NULL ,
	[sl6] [numeric](19, 4) NOT NULL ,
	[sl7] [numeric](19, 4) NOT NULL ,
	[sl8] [numeric](19, 4) NOT NULL ,
	[dwdm1] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwdm2] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwdm3] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwdm4] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwdm5] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwdm6] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwdm7] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwdm8] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc1] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc2] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc3] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc4] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc5] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc6] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc7] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc8] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfje1] [numeric](19, 2) NOT NULL ,
	[sfje2] [numeric](19, 2) NOT NULL ,
	[sfje3] [numeric](19, 2) NOT NULL ,
	[sfje4] [numeric](19, 2) NOT NULL ,
	[sfje5] [numeric](19, 2) NOT NULL ,
	[sfje6] [numeric](19, 2) NOT NULL ,
	[sfje7] [numeric](19, 2) NOT NULL ,
	[sfje8] [numeric](19, 2) NOT NULL ,
	[fl1] [numeric](19, 3) NOT NULL ,
	[fl2] [numeric](19, 3) NOT NULL ,
	[fl3] [numeric](19, 3) NOT NULL ,
	[fl4] [numeric](19, 3) NOT NULL ,
	[fl5] [numeric](19, 3) NOT NULL ,
	[fl6] [numeric](19, 3) NOT NULL ,
	[fl7] [numeric](19, 3) NOT NULL ,
	[fl8] [numeric](19, 3) NOT NULL ,
	[fj1] [numeric](19, 2) NOT NULL ,
	[fj2] [numeric](19, 2) NOT NULL ,
	[fj3] [numeric](19, 2) NOT NULL ,
	[fj4] [numeric](19, 2) NOT NULL ,
	[fj5] [numeric](19, 2) NOT NULL ,
	[fj6] [numeric](19, 2) NOT NULL ,
	[fj7] [numeric](19, 2) NOT NULL ,
	[fj8] [numeric](19, 2) NOT NULL ,
	[ys1] [numeric](19, 2) NOT NULL ,
	[ys2] [numeric](19, 2) NOT NULL ,
	[ys3] [numeric](19, 2) NOT NULL ,
	[ys4] [numeric](19, 2) NOT NULL ,
	[ys5] [numeric](19, 2) NOT NULL ,
	[ys6] [numeric](19, 2) NOT NULL ,
	[ys7] [numeric](19, 2) NOT NULL ,
	[ys8] [numeric](19, 2) NOT NULL ,
	[bz1] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz2] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz3] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz4] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz5] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz6] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz7] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz8] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yshj] [numeric](19, 2) NOT NULL ,
	[ddfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ffrmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_a_gd] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [a_jd] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jddm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jdmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dutydm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dutymc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bef] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[aft] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bd] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bdmc] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bddm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kzf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsrexp] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_a_jd] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [a_jdlx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_a_jdlx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [a_jdzt] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_A_JDZT] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [a_jjlx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_A_JJLX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [a_op] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[op] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opa] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_a_op] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [a_opg] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_a_opg] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [a_oplx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_A_OPLX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [a_sj] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jdxh] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jdmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jd] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[op] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opa] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ztdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ztmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jssj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsrqsj] [varchar] (17) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[oprq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opsj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[oprqsj] [varchar] (17) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opsc] [varchar] (17) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opyj] [varchar] (254) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_A_SJ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [a_sw] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[swh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[klx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kv] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fdm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fmc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fldm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[flmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjsj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjrqsj] [varchar] (17) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cj] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cja] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[swbt] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jjdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jjmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[swztdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[swztmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mklx] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mkv] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mf1] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckn] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckp] [int] NOT NULL ,
	[lckt] [varchar] (23) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_A_SW] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [a_swzt] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_A_SWZT] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [agtcost] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fyfldm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fyflmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmmc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dj] [numeric](19, 3) NOT NULL ,
	[sl] [numeric](19, 3) NOT NULL ,
	[dwdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[je] [numeric](19, 2) NOT NULL ,
	[biz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hl] [numeric](19, 6) NOT NULL ,
	[hlje] [numeric](19, 2) NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gg] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dj1] [numeric](19, 3) NOT NULL ,
	CONSTRAINT [PK_AGTCOST] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [autosend] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tid] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tname] [varchar] (28) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tstatus] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ttype] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cname] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cdate] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ctime] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[oname] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[odate] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[otime] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_autosend] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [batchg] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwmc] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mclst] [varchar] (240) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fldlst] [varchar] (240) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dtj] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwmc2] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ty] [numeric](5, 0) NOT NULL ,
	[zwmc3] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mclst2] [varchar] (240) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zd] [numeric](1, 0) NOT NULL ,
	CONSTRAINT [PK_BATCHG] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [bbhead] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bblx] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bbmc] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[head1] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[head2] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[head3] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[head4] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jj1] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jj2] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jj3] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jj4] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jj5] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_bbhead] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [bblx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bblx] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_bblx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [bd] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fm2] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40js1] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40mz1] [varchar] (13) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40tj1] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f4031] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f4032] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f402] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mz] [numeric](19, 3) NOT NULL ,
	[tj] [numeric](19, 3) NOT NULL ,
	[js] [numeric](19, 0) NOT NULL ,
	[f31] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40mt] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40hm] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40jsdw] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cbhsbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hgjs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pc1] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhfh] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_BD] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [bgd] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ylrbh] [varchar] (18) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hgbh] [varchar] (18) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bah] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sbrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysfs] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhdw] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zmxz] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jhfs] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xkzh] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jrhyd] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bf] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zf] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jz] [numeric](19, 3) NOT NULL ,
	[sfdj] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sccj] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mark] [varchar] (2000) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh1] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh3] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh4] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh5] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spbh1] [varchar] (13) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spbh2] [varchar] (13) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spbh3] [varchar] (13) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spbh4] [varchar] (13) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spbh5] [varchar] (13) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spmz1] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spmz2] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spmz3] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spmz4] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spmz5] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sljdw1] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sljdw2] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sljdw3] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sljdw4] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sljdw5] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzmdm1] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzmdg1] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzmdm2] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzmdg2] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzmdm3] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzmdg3] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzmdm4] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzmdg4] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzmdm5] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzmdg5] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dja1] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dja2] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dja3] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dja4] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dja5] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[djb1] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[djb2] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[djb3] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[djb4] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[djb5] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zja1] [numeric](19, 2) NOT NULL ,
	[zja2] [numeric](19, 2) NOT NULL ,
	[zja3] [numeric](19, 2) NOT NULL ,
	[zja4] [numeric](19, 2) NOT NULL ,
	[zja5] [numeric](19, 2) NOT NULL ,
	[zjb1] [numeric](19, 2) NOT NULL ,
	[zjb2] [numeric](19, 2) NOT NULL ,
	[zjb3] [numeric](19, 2) NOT NULL ,
	[zjb4] [numeric](19, 2) NOT NULL ,
	[zjb5] [numeric](19, 2) NOT NULL ,
	[bza1] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bza2] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bza3] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bza4] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bza5] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzb1] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzb2] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzb3] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzb4] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzb5] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zm1] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zm2] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zm3] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zm4] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zm5] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dyjydw] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[myfs] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[myfn] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bgks] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysgj] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tydh] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zs] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zyg] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[js] [numeric](20, 0) NOT NULL ,
	[mz] [numeric](20, 3) NOT NULL ,
	[spbhm1] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spbhm2] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spbhm3] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spbhm4] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spbhm5] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lry] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lrdw] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ys] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxzt] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sbqk] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hgbh_rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hgbh_sj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hgdm] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zyhm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dzlxdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dzlxmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fxrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fxsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tgxzdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tgxzmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgfs1] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgfs1mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgfs2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgfs2mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tddm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tdmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cyfsdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cyfsmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhjddm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhjdmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rkrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rksj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cntztdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cntztmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ieflagdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ieflagmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_bgd] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [bggd] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gzyy] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gzq] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gzh] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_bggd] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [bggs] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsbm] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spmc] [varchar] (253) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjmc] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sl] [numeric](19, 4) NOT NULL ,
	[je] [numeric](19, 2) NOT NULL ,
	[biz] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hl] [numeric](19, 6) NOT NULL ,
	[yj] [numeric](5, 0) NOT NULL ,
	[gsl] [numeric](19, 2) NOT NULL ,
	[gsy] [numeric](19, 2) NOT NULL ,
	[zzl] [numeric](19, 2) NOT NULL ,
	[zzy] [numeric](19, 2) NOT NULL ,
	[xfl] [numeric](19, 2) NOT NULL ,
	[xfy] [numeric](19, 2) NOT NULL ,
	[fql] [numeric](19, 2) NOT NULL ,
	[fqy] [numeric](19, 2) NOT NULL ,
	[hjy] [numeric](19, 2) NOT NULL ,
	[slxs] [numeric](19, 4) NOT NULL ,
	CONSTRAINT [PK_bggs] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [bgjgtj] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_bgjgtj] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [bgjldw] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_BGJLDW] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [bgsp] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spdm] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spmc] [varchar] (253) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhgsl] [numeric](19, 2) NOT NULL ,
	[jldw] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgtjdm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgtj] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sbys] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sbyq] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xfs] [numeric](19, 2) NOT NULL ,
	[zzs] [numeric](19, 2) NOT NULL ,
	[bz] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jks] [numeric](19, 2) NOT NULL ,
	[cks] [numeric](19, 2) NOT NULL ,
	[jldw2] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zdjksl] [numeric](19, 2) NOT NULL ,
	[cktsl] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jyjy] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_bgsp] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [bjlx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_bjlx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [bsdy] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fydm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fymc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fyemc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shv] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfldm] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sflmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sffrdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sffr] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sbiz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sdj] [numeric](19, 3) NOT NULL ,
	[ssl] [numeric](19, 3) NOT NULL ,
	[sje] [numeric](19, 2) NOT NULL ,
	[fhv] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ffldm] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fflmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fffrdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fffr] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fbiz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fdj] [numeric](19, 3) NOT NULL ,
	[fsl] [numeric](19, 3) NOT NULL ,
	[fje] [numeric](19, 2) NOT NULL ,
	[bz] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_bsdy] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [bsdyw] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fydm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fymc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fyemc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shv] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfldm] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sflmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sffrdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sffr] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sbiz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sdj] [numeric](19, 3) NOT NULL ,
	[ssl] [numeric](19, 3) NOT NULL ,
	[sje] [numeric](19, 2) NOT NULL ,
	[fhv] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ffldm] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fflmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fffrdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fffr] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fbiz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fdj] [numeric](19, 3) NOT NULL ,
	[fsl] [numeric](19, 3) NOT NULL ,
	[fje] [numeric](19, 2) NOT NULL ,
	[bz] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_bsdyw] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [bwlog] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wjm] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_bwlog] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [bwlx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_bwlx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [bxfl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxtk] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_BXFL] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [bxfs] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_bxfs] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cbdtk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f32] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cmdm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cm] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f29] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgs] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxq] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gg] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eta] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cbzq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[srrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11a] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eta1] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11b] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eta2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11c] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eta3] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11d] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eta4] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11e] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eta5] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm2] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx2] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm3] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx3] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm4] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx4] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm5] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx5] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cqfldm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cqflmc] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[src] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cwdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cwmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kgsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ggdm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cbhh] [varchar] (9) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f32sj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[atd] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[atdsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cq] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jh] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jxzt] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pod] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ei] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[etasj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ata] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[atasj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kbzt] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nxz] [numeric](1, 0) NOT NULL ,
	[jdr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jdsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cunno] [varchar] (9) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sg] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CBDTK] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cbhc] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f29] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hcjs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_cbhc] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cbhs] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zt] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_cbhs] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cbst] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (13) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (13) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f32] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eta] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kmtr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lay1] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lay2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtdcdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtdcmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jmr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CBST] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cbsy] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (13) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f32] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cmdm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cm] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f29] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgs] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxq] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gg] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eta] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cbzq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[srrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11a] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eta1] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11b] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eta2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11c] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eta3] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11d] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eta4] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11e] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eta5] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm2] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx2] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm3] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx3] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm4] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx4] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm5] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx5] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cqfldm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cqflmc] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[src] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nxz] [numeric](1, 0) NOT NULL ,
	CONSTRAINT [PK_CBSY] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ccbc] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ccbc] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cccw] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_cccw] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cccx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_cccx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ccdw] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ccdw] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ccfjk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tjid] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f3] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxdm] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (13) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dx] [numeric](19, 0) NOT NULL ,
	[sgrq] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wjm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxbm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fjsj] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fj] [image] NOT NULL ,
	[sy] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yslx] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[klx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tjsj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cl] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[clrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[clsj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ccfjk] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ccfp] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysfph] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dj] [numeric](19, 0) NOT NULL ,
	[sl] [numeric](19, 0) NOT NULL ,
	[je] [numeric](19, 0) NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ccfp] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cchw] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f3] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hh] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwhm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mt] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[js] [numeric](19, 0) NOT NULL ,
	[hwc] [numeric](19, 2) NOT NULL ,
	[hwk] [numeric](19, 2) NOT NULL ,
	[hwg] [numeric](19, 2) NOT NULL ,
	[dwtj] [numeric](19, 4) NOT NULL ,
	[jstj] [numeric](19, 4) NOT NULL ,
	[dwmz] [numeric](19, 3) NOT NULL ,
	[jsmz] [numeric](19, 3) NOT NULL ,
	[hwcw] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hwps] [numeric](5, 0) NOT NULL ,
	[ysxcf] [numeric](19, 2) NOT NULL ,
	[jcfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysccf] [numeric](19, 2) NOT NULL ,
	[f31] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[js0] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mz] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tj] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yhbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yhrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yhsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jljs] [numeric](19, 0) NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f4] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mobkey] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ccjs] [numeric](19, 0) NOT NULL ,
	[ccjljs] [numeric](19, 0) NOT NULL ,
	[zcjs] [numeric](19, 0) NOT NULL ,
	[zcjljs] [numeric](19, 0) NOT NULL ,
	[po] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wxpfl] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[unno] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ym] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_cchw] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ccjs] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ccjs] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ccjx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ccjx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ccsend] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tjid] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f3] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwhm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mt] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhfmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jcjs] [numeric](19, 0) NOT NULL ,
	[jcmz] [numeric](19, 3) NOT NULL ,
	[jctj] [numeric](19, 4) NOT NULL ,
	[yhjl] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[psjs] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tqmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rkrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rksj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tjsj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cl] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[clrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[clsj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CCSEND] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ccsendmx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tjid] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[js] [numeric](19, 0) NOT NULL ,
	[jljs] [numeric](19, 0) NOT NULL ,
	[hwc] [numeric](19, 2) NOT NULL ,
	[hwk] [numeric](19, 2) NOT NULL ,
	[hwg] [numeric](19, 2) NOT NULL ,
	[jstj] [numeric](19, 4) NOT NULL ,
	[jsmz] [numeric](19, 3) NOT NULL ,
	[hwcw] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CCSENDMX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cctj] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc2] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc3] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CCTJ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cctq] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CCTQ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cctqyb] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_cctqyb] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ccyw] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xz] [numeric](1, 0) NOT NULL ,
	[ty] [numeric](1, 0) NOT NULL ,
	[zwmc] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwmc2] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fld] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[chld] [numeric](1, 0) NOT NULL ,
	[bz] [varchar] (180) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jy] [numeric](1, 0) NOT NULL ,
	[zwmc3] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dyl] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ccyw] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ccywlx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ccywlx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cczj] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_cczj] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cddmk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_cddmk] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cddmlx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CDDMLX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [chgitem] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xm] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zdm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zdmm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cd] [numeric](19, 0) NOT NULL ,
	[xsw] [numeric](19, 0) NOT NULL ,
	[ts] [varchar] (254) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_chgitem] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [chgrec] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[khbh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f32] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[op] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sqrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sqsj] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sqr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sqdw] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ggxm] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmbm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmzdm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ggq] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ggh] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ggyy] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sqzdm] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sqip] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zt] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[slyj] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[slrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[slsj] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[slr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opa] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[chgpc] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sl] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nbts] [varchar] (240) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xtts] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nxz] [numeric](1, 0) NOT NULL ,
	[bkmks] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_chgrec] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ckdm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CKDM] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cksyk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fm1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f29] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f31] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtf2dm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtf2] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f46] [numeric](19, 0) NOT NULL ,
	[f47] [numeric](19, 0) NOT NULL ,
	[f48] [numeric](19, 0) NOT NULL ,
	[teu] [numeric](19, 1) NOT NULL ,
	[mz] [numeric](19, 3) NOT NULL ,
	[hkmz] [numeric](19, 3) NOT NULL ,
	[tj] [numeric](19, 3) NOT NULL ,
	[js] [numeric](19, 0) NOT NULL ,
	[jsdw] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yw] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[agent] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[agentmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtdcdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtdcmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pc1] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pc2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dl1] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pcdlmc1] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f32] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwhm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tgbz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[swqr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywqr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm3] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cbhsbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f31h] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[khbh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fkdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fkmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pc4] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pc3] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jpcl] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jz1] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jz2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jz3] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jz4] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jz5] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jz6] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jz7] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jz8] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jz9] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dl2] [numeric](19, 0) NOT NULL ,
	[dl3] [numeric](19, 3) NOT NULL ,
	[wtf1dm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtf1] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcbh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tdlx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tdmc] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xz] [numeric](1, 0) NOT NULL ,
	[hylx] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f33] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hdrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pc5] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pc6] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[op] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hgcy] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hgcyr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjcy] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjcyr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bgbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cdbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dl4] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f45] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ft40xy] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pcdlmc2] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[refno] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywdma] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjjs] [numeric](19, 0) NOT NULL ,
	[sjmz] [numeric](19, 3) NOT NULL ,
	[sjtj] [numeric](19, 3) NOT NULL ,
	[hl] [numeric](19, 6) NOT NULL ,
	[hlyf] [numeric](19, 6) NOT NULL ,
	[lx2dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx2mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fm2] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bk] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bkg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glrid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glra] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opa] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bkid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bka] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bmid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glraid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bkgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bkaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[doc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[docg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[doca] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[docid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[docgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[docaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ctj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cl] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ctj1] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cl1] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yw2] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[atd] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywlxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywlxmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cklxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cklxmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jmyw] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcqr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzbz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cs] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csa] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hwfldm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hwflmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rqfldm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rqflmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhdh] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xsdh] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgdh] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qshd] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dbtb] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ygbx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxr] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lay1] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lay2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fm3] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsbm] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f49] [numeric](19, 0) NOT NULL ,
	[dzf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lzf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsdldm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsdlmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxgsdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxgsmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxdldm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxdlmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kmtr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dgr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sbhm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtfkdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtfkmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gpf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yzf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zyfjs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ctj2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cl2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[atdsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[czbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[swbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bkqr1] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bkqr2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bkqr3] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bfqr1] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bfqr2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bfqr3] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dxzt] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sghz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xsts] [numeric](19, 0) NOT NULL ,
	[xszx] [numeric](19, 0) NOT NULL ,
	[zjbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cyjs] [numeric](19, 0) NOT NULL ,
	[cymz] [numeric](19, 3) NOT NULL ,
	[cytj] [numeric](19, 3) NOT NULL ,
	[tdyqr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckqr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[khdcqr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[swrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckn] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckp] [int] NOT NULL ,
	[lckt] [varchar] (23) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cwdldm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cwdlmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fyjs] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fxzlbh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qydm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qymc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dbjs] [numeric](19, 2) NOT NULL ,
	[opm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjs] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rkdz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckdz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fxyp] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cdyp] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckbgbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckbgrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckbgsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gqfg] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zccy] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tdqf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bksh1] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bksh2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bksh3] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bfsh1] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bfsh2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bfsh3] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zdbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f32sj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pol] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[s_dz] [numeric](5, 0) NOT NULL ,
	[s_sj] [numeric](5, 0) NOT NULL ,
	[s_fj] [numeric](5, 0) NOT NULL ,
	[s_bgd] [numeric](5, 0) NOT NULL ,
	[ywqr2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyhid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyhz] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyhzid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyha] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyhaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f34] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjcq] [numeric](19, 0) NOT NULL ,
	[shfdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shfmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gdtj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gdsl] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bfdm] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_cksyk] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ckzxl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fm1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f45] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f46] [numeric](19, 0) NOT NULL ,
	[ft40xy] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f47] [numeric](19, 0) NOT NULL ,
	[tj] [numeric](19, 3) NOT NULL ,
	[mz] [numeric](19, 3) NOT NULL ,
	[js] [numeric](19, 0) NOT NULL ,
	[tgbz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fxh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ffh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[usdyf] [numeric](19, 2) NOT NULL ,
	[usdsf] [numeric](19, 2) NOT NULL ,
	[usdfkbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rmbyf] [numeric](19, 2) NOT NULL ,
	[rmbsf] [numeric](19, 2) NOT NULL ,
	[rmbfkbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtdcdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtdcmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fh] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ckzxl] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [clbb] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsyxm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zz1] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zz2] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fsrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fssj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjnr] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjyy] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fsdd] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jssj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[clbf] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cldwdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cldw] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[clfy] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zgyj] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zgcs] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfbb] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CLBB] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [clbx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxje] [numeric](19, 2) NOT NULL ,
	[bxfy] [numeric](19, 2) NOT NULL ,
	[bxgsdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxgs] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxqxq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxqxz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bdh] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jb] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[blrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CLBX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [clbxmx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cbxzdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cbxz] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxje] [numeric](19, 2) NOT NULL ,
	[bxfy] [numeric](19, 2) NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CLBXMX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [clby] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gcrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsyxm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bydwdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bydw] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xcfzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jcrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[njcrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjgls] [numeric](19, 2) NOT NULL ,
	[njhgls] [numeric](19, 2) NOT NULL ,
	[jsyqr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jlqr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[byje] [numeric](19, 2) NOT NULL ,
	[ckgs] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[scyf] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ycyf] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CLBY] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [clbymx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bynr] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bydj] [numeric](19, 2) NOT NULL ,
	[bysl] [numeric](19, 2) NOT NULL ,
	[byje] [numeric](19, 2) NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CLBYMX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cldmbx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CLDMBX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cldmby] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CLDMBY] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cldmcz] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CLDMCZ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cldmgc] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CLDMGC] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cldmjf] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CLDMJF] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cldmlx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CLDMLX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cldmrl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CLDMRL] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cldmzt] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CLDMZT] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cljf] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jfqq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jfqz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jd] [numeric](19, 2) NOT NULL ,
	[hj] [numeric](19, 2) NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jb] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[blrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fyx1] [numeric](19, 2) NOT NULL ,
	[fyx2] [numeric](19, 2) NOT NULL ,
	[fyx3] [numeric](19, 2) NOT NULL ,
	[fyx4] [numeric](19, 2) NOT NULL ,
	[xmdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CLJF] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cljsy] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tgch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsy] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsydh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxqxz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hdyh] [numeric](19, 2) NOT NULL ,
	CONSTRAINT [PK_CLJSY] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cllt] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsyxm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pgr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ltdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ltmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ltgg] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ltbh] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yxsgl] [numeric](19, 2) NOT NULL ,
	[jhxsgl] [numeric](19, 2) NOT NULL ,
	[sjtw] [numeric](19, 2) NOT NULL ,
	[jhbfsj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CLLT] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [clxx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[clxl] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[syr] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dz] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ppxh] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[syxz] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fdjhm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[clsbdm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zcrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[czrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fzjg] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzl] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zbzl] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hdzzl] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zqyzzl] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hdzk] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jssgc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxnbcc] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hzgbthps] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wlcc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjyxrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bdh] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxqxq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxqxz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxfy] [numeric](19, 2) NOT NULL ,
	[bxgsdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxgs] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxje] [numeric](19, 2) NOT NULL ,
	[clbh] [varchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pzys] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csys] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yyzh] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pzwh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dxm] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsyxm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gcdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gcmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[czdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[czmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rldm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rlmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ztdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ztmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kcyh] [numeric](19, 3) NOT NULL ,
	[kxyh] [numeric](19, 3) NOT NULL ,
	[zxyh] [numeric](19, 3) NOT NULL ,
	[tzbl] [numeric](19, 3) NOT NULL ,
	[ccrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ajxqz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ebxqz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jfxqz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ylfz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ygfz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ccsz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsyxm1] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ct] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckn] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckp] [int] NOT NULL ,
	[lckt] [varchar] (23) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CLXX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cmdmk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[emc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgs] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qy] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cbhh] [varchar] (9) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cwdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cwmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[unno] [varchar] (9) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ebm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjmc] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CMDMK] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [counter] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[name] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[counter] [int] NOT NULL ,
	CONSTRAINT [PK_COUNTER] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cqfl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_cqfl] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [crfl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_crfl] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [crxm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fldm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[flmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zb] [numeric](19, 2) NOT NULL ,
	[kpexp] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dyl] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_crxm] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [csdm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (22) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_csdm] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [csk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[name] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[value] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_csk] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [csl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[name] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[value] [varchar] (8000) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_csl] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cslx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CSLX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [csv] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ifcdd] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sdlx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tdlx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qqxdm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qqxmc] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[srzdm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[srzmc] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mbl] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hbl] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[invno] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[refno] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hdlx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xtdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xtmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[etd] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cm] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsmc] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sbdm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gqdm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shddm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjdm] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cj] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eta] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ctnnumb] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pkg] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mz] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tj] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f402] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shipper] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[send1] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[send2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40hm] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ctn] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40mt] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz1] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz2] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CSV] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cu] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ty] [numeric](5, 0) NOT NULL ,
	[fl] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwmc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwmc2] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cdlk] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CU] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [culink] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jy] [int] NOT NULL ,
	[lx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mk] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fdm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[url] [varchar] (254) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pic] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qx] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[op] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nw] [numeric](9, 0) NOT NULL ,
	CONSTRAINT [PK_culink] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cupg] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ty] [numeric](5, 0) NOT NULL ,
	[zwmc] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwmc2] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tjexp] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[collst] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fyzd] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dmbm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dmzd] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mczd] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwmc3] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xstj] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_CUPG] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [custep] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[stp] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xz] [numeric](1, 0) NOT NULL ,
	[ty] [numeric](1, 0) NOT NULL ,
	[zwmc] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwmc2] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fld] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[chkprg] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (180) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwmc3] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dyl] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_custep] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [custp] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[stp] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzm] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzm2] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzjc] [varchar] (27) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bj] [numeric](1, 0) NOT NULL ,
	[dyxs] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_custp] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cuyy] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xz] [numeric](1, 0) NOT NULL ,
	[zwmc] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bbcd] [numeric](19, 0) NOT NULL ,
	[sqlcol] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sqlas] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bbbds] [varchar] (180) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zd] [numeric](1, 0) NOT NULL ,
	[mczd] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dmbm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dmzd] [varchar] (21) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dmmczd] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dmsp] [numeric](1, 0) NOT NULL ,
	[chk] [numeric](1, 0) NOT NULL ,
	[sparse] [numeric](1, 0) NOT NULL ,
	[kjm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dtct] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dtxt] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dtbj] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bx] [numeric](1, 0) NOT NULL ,
	[ct] [numeric](1, 0) NOT NULL ,
	[xt] [numeric](1, 0) NOT NULL ,
	[ys] [numeric](1, 0) NOT NULL ,
	[kz] [numeric](1, 0) NOT NULL ,
	[lwk] [numeric](1, 0) NOT NULL ,
	[bz] [varchar] (180) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ty] [numeric](1, 0) NOT NULL ,
	[qid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gxid] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwmc2] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dtj] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fz] [numeric](5, 0) NOT NULL ,
	[webbz] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dtqj] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dh] [numeric](1, 0) NOT NULL ,
	[lj] [numeric](1, 0) NOT NULL ,
	[cs] [numeric](1, 0) NOT NULL ,
	[zwmc3] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cktj] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xstj] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dyl] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zrq] [numeric](5, 0) NOT NULL ,
	CONSTRAINT [PK_cuyy] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cuyyg] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zz] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jlh] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xz] [numeric](1, 0) NOT NULL ,
	[sjkd] [numeric](19, 0) NOT NULL ,
	[sjsx] [numeric](19, 0) NOT NULL ,
	CONSTRAINT [PK_cuyyg] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cwbbii] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzlx] [numeric](1, 0) NOT NULL ,
	[mc] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[idxbds] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fzbds] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zb] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (180) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ty] [numeric](1, 0) NOT NULL ,
	[sqlcol] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sqlas] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gbexp] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc2] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc3] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cktj] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dyl] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hv] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_cwbbii] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cwywbb] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xz] [numeric](1, 0) NOT NULL ,
	[zwmc] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bbcd] [numeric](19, 0) NOT NULL ,
	[sqlcol] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sqlas] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bbbds] [varchar] (180) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ct] [numeric](1, 0) NOT NULL ,
	[xt] [numeric](1, 0) NOT NULL ,
	[ys] [numeric](1, 0) NOT NULL ,
	[dt] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kz] [numeric](1, 0) NOT NULL ,
	[lwk] [numeric](1, 0) NOT NULL ,
	[bqh] [numeric](1, 0) NOT NULL ,
	[bz] [varchar] (180) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ty] [numeric](1, 0) NOT NULL ,
	[qid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwmc2] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwmc3] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cktj] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dyl] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zrq] [numeric](5, 0) NOT NULL ,
	CONSTRAINT [PK_cwywbb] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cwywbbb] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xz] [numeric](1, 0) NOT NULL ,
	[fz] [numeric](1, 0) NOT NULL ,
	[qh] [numeric](1, 0) NOT NULL ,
	[zwmc] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bbcd] [numeric](19, 0) NOT NULL ,
	[sqlcol] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sqlas] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bbbds] [varchar] (180) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lwk] [numeric](1, 0) NOT NULL ,
	[bqh] [numeric](1, 0) NOT NULL ,
	[bz] [varchar] (180) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ty] [numeric](1, 0) NOT NULL ,
	[qid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwmc2] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwmc3] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cktj] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dyl] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_cwywbbb] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [cxdbf] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fh] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwmc] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zdlx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zdcd] [numeric](19, 0) NOT NULL ,
	[xsw] [numeric](19, 0) NOT NULL ,
	[qhsx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xz] [numeric](1, 0) NOT NULL ,
	[dbfmc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zdmc] [varchar] (180) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dmbm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dmzd] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mczd] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zh] [numeric](1, 0) NOT NULL ,
	[zhbm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhzd] [varchar] (180) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (180) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ty] [numeric](1, 0) NOT NULL ,
	[uv] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ks] [numeric](1, 0) NOT NULL ,
	[rqxz] [numeric](1, 0) NOT NULL ,
	[bjk] [numeric](1, 0) NOT NULL ,
	[zwmc2] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dxm] [numeric](1, 0) NOT NULL ,
	[zwmc3] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cktj] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dyl] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_cxdbf] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ver] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[last_day] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[last_month] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[last_year] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[last_back] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bv] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckn] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckp] [int] NOT NULL ,
	[lckt] [varchar] (23) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_db] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[khqc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxr] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dh] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[twcz] [varchar] (28) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[khdz] [varchar] (240) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yzbm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dh1] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[twcz1] [varchar] (28) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz1] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[flbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f15] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f17] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f19] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f22] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bp] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eml] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yjbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qcbj] [numeric](1, 0) NOT NULL ,
	[gxrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fkdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fkmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fkts] [numeric](19, 0) NOT NULL ,
	[fkje] [numeric](19, 2) NOT NULL ,
	[lxdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx2dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx2mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[usdzh] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[usdkhh] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rmbzh] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rmbkhh] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[khbgbm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dybj] [numeric](1, 0) NOT NULL ,
	[cpbj] [numeric](1, 0) NOT NULL ,
	[hth] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mdf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm3] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[syf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ewm] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kl] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jl] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjz] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bkg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bk] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[op] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[czyq] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glrid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glra] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opa] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bkid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bka] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bmid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glraid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bkgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bkaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ys] [numeric](19, 0) NOT NULL ,
	[fjrq] [numeric](2, 0) NOT NULL ,
	[jlsj] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jleml] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gb] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dep] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zw] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cs] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csa] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sopno] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dl] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hs1] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hs2] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsyq] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mk] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[doc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[docid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[docg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[docgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[doca] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[docaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckn] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckp] [int] NOT NULL ,
	[lckt] [varchar] (23) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kjhl] [numeric](19, 2) NOT NULL ,
	[xdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sja] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjrid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjzid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nbdw] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kpyq] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fjsl] [numeric](19, 4) NOT NULL ,
	[kmx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cqyj] [numeric](1, 0) NOT NULL ,
	[zpqr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jp24] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yjdh] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jqpjts] [numeric](19, 1) NOT NULL ,
	[fjslf] [numeric](19, 4) NOT NULL ,
	[lkdm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hmdbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nzbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wlfw] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zdfjr] [numeric](2, 0) NOT NULL ,
	[bfdm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qq_lxr] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qq_jl] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_db1] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1_kjqy] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DB1_KJQY] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1c2] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gp20] [numeric](19, 2) NOT NULL ,
	[gp40] [numeric](19, 2) NOT NULL ,
	[hq40] [numeric](19, 2) NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DB1C2] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1cc] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f3] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjxz] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cbm] [numeric](19, 2) NOT NULL ,
	[plt] [numeric](19, 2) NOT NULL ,
	[ton] [numeric](19, 2) NOT NULL ,
	[min] [numeric](19, 2) NOT NULL ,
	[xsj] [numeric](19, 2) NOT NULL ,
	[ybj] [numeric](19, 2) NOT NULL ,
	[dbj] [numeric](19, 2) NOT NULL ,
	[jmf] [numeric](19, 2) NOT NULL ,
	[bxf] [numeric](19, 2) NOT NULL ,
	[mfdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfmc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nbts] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wbts] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjsdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjsmc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ys] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[djf] [numeric](19, 2) NOT NULL ,
	[djd] [numeric](19, 2) NOT NULL ,
	[djm] [numeric](19, 2) NOT NULL ,
	[djj] [numeric](19, 2) NOT NULL ,
	[djjsdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[djjsmc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyhid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyhz] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyhzid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyha] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyhaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ybsdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ybsmc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_db1cc] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1cr] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fldm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[flmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mf] [numeric](19, 2) NOT NULL ,
	[fs] [numeric](19, 2) NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_db1cr] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1cs] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bb] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dz] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[usr] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pwd] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[my] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[km] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pingzt] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[telnetzt] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ljzt] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[prq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[trq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lj] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxbz] [numeric](1, 0) NOT NULL ,
	CONSTRAINT [PK_DB1CS] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1cx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fldm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[flmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mf] [numeric](19, 2) NOT NULL ,
	[fs] [numeric](19, 2) NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jg] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_db1cx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1d1] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hwfldm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hwflmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cddm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cycd] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhdbh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhjc] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhd] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhdgsmc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxgq] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dxdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sxqks] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sxqjs] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jffs] [numeric](1, 0) NOT NULL ,
	[jfmc] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dj] [numeric](19, 2) NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dj2] [numeric](19, 2) NOT NULL ,
	CONSTRAINT [PK_DB1D1] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1d2] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtdcdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtdcmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtf2dm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtf2mc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gp20] [numeric](19, 2) NOT NULL ,
	[gp40] [numeric](19, 2) NOT NULL ,
	[hq40] [numeric](19, 2) NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DB1D2] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1d3] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hwfldm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hwflmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysfs] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysfn] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cddm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cycd] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxdm2] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxgq2] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shdbh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shjc] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shdd] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shdgsmc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dxdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sxqks] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sxqjs] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jffs] [numeric](1, 0) NOT NULL ,
	[jfmc] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dj] [numeric](19, 2) NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dj2] [numeric](19, 2) NOT NULL ,
	[bt] [numeric](19, 2) NOT NULL ,
	CONSTRAINT [PK_DB1D3] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1dqlx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_db1dqlx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1dz] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[khdz] [varchar] (240) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxr] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dh] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fax] [varchar] (28) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eml] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DB1DZ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1fj] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dxdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcfts] [numeric](9, 0) NOT NULL ,
	[dcfdj] [numeric](19, 2) NOT NULL ,
	[zxfts] [numeric](9, 0) NOT NULL ,
	[zxfdj] [numeric](19, 2) NOT NULL ,
	[sxqks] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sxqjs] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtf2dm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtf2] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zcf] [numeric](19, 2) NOT NULL ,
	[cgsdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DB1FJ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1fwlx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_db1fwlx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1gd] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rjje] [numeric](19, 4) NOT NULL ,
	[rjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rjfs] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjje] [numeric](19, 4) NOT NULL ,
	[sjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjfs] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_db1gd] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1glr] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xsid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xs] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xsgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xsg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xsaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xsa] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DB1GLR] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1gmlx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_db1gmlx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1hx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fldm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[flmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cyrdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cyrmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DB1HX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1hyf] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxbz] [numeric](1, 0) NOT NULL ,
	[sfdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sxrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f20] [numeric](19, 2) NOT NULL ,
	[g20] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40] [numeric](19, 2) NOT NULL ,
	[g40] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f42] [numeric](19, 2) NOT NULL ,
	[g42] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ts] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cqdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cq] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mbl] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxgq] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zz] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mdmt] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[d20] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[d40] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[d42] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[z20] [numeric](19, 2) NOT NULL ,
	[z40] [numeric](19, 2) NOT NULL ,
	[j20] [numeric](19, 2) NOT NULL ,
	[j40] [numeric](19, 2) NOT NULL ,
	[j42] [numeric](19, 2) NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[x20] [numeric](19, 2) NOT NULL ,
	[x40] [numeric](19, 2) NOT NULL ,
	[x42] [numeric](19, 2) NOT NULL ,
	[ty] [numeric](19, 2) NOT NULL ,
	[zfdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zfmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[s20] [numeric](19, 2) NOT NULL ,
	[s40] [numeric](19, 2) NOT NULL ,
	[s42] [numeric](19, 2) NOT NULL ,
	[biz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[c20] [numeric](19, 2) NOT NULL ,
	[c40] [numeric](19, 2) NOT NULL ,
	[c42] [numeric](19, 2) NOT NULL ,
	[b20] [numeric](19, 2) NOT NULL ,
	[b40] [numeric](19, 2) NOT NULL ,
	[b42] [numeric](19, 2) NOT NULL ,
	[cw] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[stp] [numeric](1, 0) NOT NULL ,
	[m20] [numeric](19, 2) NOT NULL ,
	[m40] [numeric](19, 2) NOT NULL ,
	[m42] [numeric](19, 2) NOT NULL ,
	[ffid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cshx] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sxr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wxpfl] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_db1hyf] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1hylx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_db1hylx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1jjd] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dz] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dh] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cz] [varchar] (28) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DB1JJD] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1khzk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qydm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qymc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qszl] [numeric](19, 3) NOT NULL ,
	[jszl] [numeric](19, 3) NOT NULL ,
	[zk] [numeric](19, 2) NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DB1KHZK] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1kptt] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kptt] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsid] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsjydz] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsyh] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nszh] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[usdyh] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[usdzh] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fpdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fpmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DB1KPTT] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1ld] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfmc] [varchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lc] [numeric](19, 0) NOT NULL ,
	[qj1] [numeric](19, 2) NOT NULL ,
	[dj1] [numeric](19, 2) NOT NULL ,
	[ts1] [numeric](19, 2) NOT NULL ,
	[cd2] [numeric](19, 2) NOT NULL ,
	[dw2] [numeric](19, 2) NOT NULL ,
	[dj2] [numeric](19, 2) NOT NULL ,
	[ts2] [numeric](19, 2) NOT NULL ,
	[sxqks] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sxqjs] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DB1LD] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1lxr] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zw] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxr] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dho] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[twcz] [varchar] (28) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eml] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xb] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dhh] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bp] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yzbm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dz] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dep] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[msn] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qq] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[msnzl] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[msnbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DB1LXR] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1lxr2] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[db1fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xm] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kl] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mk] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[czfw] [varchar] (244) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zt] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zw1] [image] NOT NULL ,
	[zw2] [image] NOT NULL ,
	[fg] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sp] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[counter] [int] NOT NULL ,
	[sqsj] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sqr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spsj] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yxq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dho] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[twcz] [varchar] (28) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eml] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[msn] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qq] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (169) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_db1lxr2] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1lxrlx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_db1lxrlx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1ly] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhdbh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhjc] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhd] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhdgsmc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shdbh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shjc] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shdd] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shdgsmc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jffs] [numeric](1, 0) NOT NULL ,
	[jfmc] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qs] [numeric](19, 2) NOT NULL ,
	[dj] [numeric](19, 2) NOT NULL ,
	[yssj] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sxqks] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sxqjs] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cddm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cycd] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DB1LY] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1lyf] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxbz] [numeric](1, 0) NOT NULL ,
	[sfdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jcdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jcmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11dm] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11mc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cyrdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cyrmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dt20] [numeric](19, 2) NOT NULL ,
	[st20] [numeric](19, 2) NOT NULL ,
	[st40] [numeric](19, 2) NOT NULL ,
	[sxrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DB1LYF] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1lylx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_db1lylx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1pro] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sect] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mon] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tmode] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[vol] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tos] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cb] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[comm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[comp] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_db1pro] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1sf] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fydm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fymc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[biz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[s20gp] [numeric](19, 3) NOT NULL ,
	[s40gp] [numeric](19, 3) NOT NULL ,
	[s40hc] [numeric](19, 3) NOT NULL ,
	[f20gp] [numeric](19, 3) NOT NULL ,
	[f40gp] [numeric](19, 3) NOT NULL ,
	[f40hc] [numeric](19, 3) NOT NULL ,
	[sxqks] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sxqjs] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dfdd] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lcls] [numeric](19, 2) NOT NULL ,
	[lclf] [numeric](19, 2) NOT NULL ,
	CONSTRAINT [PK_DB1SF] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1shdd] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jc] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shdbh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shdd] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxr] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dh] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[twcz] [varchar] (28) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gsmc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (28) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shmdgls] [numeric](19, 2) NOT NULL ,
	[qxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qxmc] [varchar] (32) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qxdm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csmc] [varchar] (22) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csdm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfdm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfmc] [varchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqdm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xz] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xza] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mdqydm] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mdqymc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lqf] [numeric](19, 2) NOT NULL ,
	[hdyf] [numeric](19, 2) NOT NULL ,
	CONSTRAINT [PK_DB1SHDD] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1shyq] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shyq] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DB1SHYQ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1sj] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zt] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ms] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kssj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jssj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pfyj] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pfr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pfrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jbr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kssj2] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jssj2] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sc] [numeric](19, 2) NOT NULL ,
	CONSTRAINT [PK_DB1SJ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1sl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sl] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc15] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc17] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dz15] [varchar] (240) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dz17] [varchar] (240) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxr15] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxr17] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dh15] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dh17] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cz15] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cz17] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[po1] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[po2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[doc1] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[doc2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[war1] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[war2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pick1] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pick2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cust1] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cust2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qc1] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qc2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pac1] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cha1] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cha2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_db1sl] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1svc] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmmc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dj] [numeric](19, 3) NOT NULL ,
	[sl] [numeric](19, 3) NOT NULL ,
	[dwdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[je] [numeric](19, 2) NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gg] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjzdm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxbz] [numeric](1, 0) NOT NULL ,
	[ksrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fyfldm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fyflmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[biz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hl] [numeric](19, 6) NOT NULL ,
	[dx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtqydm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtqymc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[slks] [numeric](19, 2) NOT NULL ,
	[sljs] [numeric](19, 2) NOT NULL ,
	[slmin] [numeric](19, 3) NOT NULL ,
	[slqzdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[slqzmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jeqzdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jeqzmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jemin] [numeric](19, 2) NOT NULL ,
	[xh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xyh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mdqydm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mdqymc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fjtjdm] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fjtjmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fpdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fpmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzl] [numeric](2, 0) NOT NULL ,
	CONSTRAINT [PK_db1svc] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1uf] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[khqc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zqbj] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhd] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhdlxr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhdtx] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[uf812bm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sbjjd] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gysbm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gysqc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10dma] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10a] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11dma] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11a] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f12dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f12dma] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f12] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f12a] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tdqfd] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pc1] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f4032] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pc2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f4032h] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pc5] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pc6] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsid] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsjydz] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsyh] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nszh] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[khdddm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gysddm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sino_khdm] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[swglr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxfsdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxfsmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxfldm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxflmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jtfs] [numeric](1, 0) NOT NULL ,
	[jtbl] [numeric](19, 2) NOT NULL ,
	[jtzx] [numeric](19, 2) NOT NULL ,
	[jtpx] [numeric](19, 2) NOT NULL ,
	[jtky] [numeric](19, 2) NOT NULL ,
	[nstt] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gldm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[prod] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[orga] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pers] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[deci] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[obje] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[posi] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[prop] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[send] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sale] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pass] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[othe] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fpdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fpmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mdqydm] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mdqymc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fpdm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fpmc2] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzl] [numeric](2, 0) NOT NULL ,
	[jxper] [numeric](19, 2) NOT NULL ,
	[jxks] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jxjs] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckfs] [numeric](19, 2) NOT NULL ,
	[ckrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jkfs] [numeric](19, 2) NOT NULL ,
	[jkrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hwfs] [numeric](19, 2) NOT NULL ,
	[hwrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[regmc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eml] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jhzjm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qyzcdz] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[szdq] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[reglxr] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[regdh] [varchar] (18) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzjgdm] [varchar] (9) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[regfjm] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wsbg] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mxts] [numeric](19, 0) NOT NULL ,
	[jf] [numeric](19, 2) NOT NULL ,
	[xqrh] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_db1uf] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1xy] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xyh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xydm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xymc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xyfm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xyje] [numeric](19, 2) NOT NULL ,
	[xybd] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fkdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fkmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dfqz] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dfrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wfqz] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wfrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wyzr] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xyqs] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xyzz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxbz] [numeric](1, 0) NOT NULL ,
	[qdjh] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzjh] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xyqc] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjzdm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zlts] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sbjy] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sbfy] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xydj] [numeric](19, 3) NOT NULL ,
	[xysl] [numeric](19, 3) NOT NULL ,
	[dwdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zht] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DB1XY] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1xy2] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qdjh] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzjh] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[scgx] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wczt] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wcb] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzwc] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wjwc] [varchar] (2000) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzwc] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjwc] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jhsjjh] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DB1XY2] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1xy3] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sbwh] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ghnr] [varchar] (240) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sl] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjzdm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xtdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xtmc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DB1XY3] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1xylx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_db1xylx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1xzlx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_db1xzlx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1yf] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysfs] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysfn] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hwfldm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hwflmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhdbh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhjc] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csmc] [varchar] (22) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csdm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfdm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfmc] [varchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shdbh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shjc] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f4031h] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dxdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sxqks] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sxqjs] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qd] [numeric](19, 2) NOT NULL ,
	[zd] [numeric](19, 2) NOT NULL ,
	[hd] [numeric](19, 2) NOT NULL ,
	[glf] [numeric](19, 2) NOT NULL ,
	[hj] [numeric](19, 2) NOT NULL ,
	[mzbc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qcsj] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yssj] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jffs] [numeric](1, 0) NOT NULL ,
	[jfmc] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhd] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhdgsmc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shdd] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shdgsmc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DB1YF] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1yfhz] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sxrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dxdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcf] [numeric](19, 2) NOT NULL ,
	[thc] [numeric](19, 2) NOT NULL ,
	[edi] [numeric](19, 2) NOT NULL ,
	[wjf] [numeric](19, 2) NOT NULL ,
	[exd] [numeric](19, 2) NOT NULL ,
	[djf] [numeric](19, 2) NOT NULL ,
	[fzf] [numeric](19, 2) NOT NULL ,
	[eir] [numeric](19, 2) NOT NULL ,
	[by1] [numeric](19, 2) NOT NULL ,
	[by2] [numeric](19, 2) NOT NULL ,
	[by3] [numeric](19, 2) NOT NULL ,
	[by4] [numeric](19, 2) NOT NULL ,
	[by5] [numeric](19, 2) NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[czp] [numeric](19, 2) NOT NULL ,
	[czc] [numeric](19, 2) NOT NULL ,
	[cic] [numeric](19, 2) NOT NULL ,
	[cicb] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ebs] [numeric](19, 2) NOT NULL ,
	[ebsb] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gbf] [numeric](19, 2) NOT NULL ,
	[gbfb] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ecr] [numeric](19, 2) NOT NULL ,
	[ecrb] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz1] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz3] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz4] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz5] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fydm1] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fydm2] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fydm3] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fydm4] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fydm5] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fymc1] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fymc2] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fymc3] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fymc4] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fymc5] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc1] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc2] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc3] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc4] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc5] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wjbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dff] [numeric](19, 2) NOT NULL ,
	[bdf] [numeric](19, 2) NOT NULL ,
	[f10dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxbz] [numeric](1, 0) NOT NULL ,
	[jcdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jcmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DB1YFHZ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1yflf] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qsqxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qsqxmc] [varchar] (32) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qsqxdm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qscsdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qscsmc] [varchar] (22) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qscsdm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qssfdm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qssfmc] [varchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qsdqdm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qsdqmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qxmc] [varchar] (32) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qxdm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csmc] [varchar] (22) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csdm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfdm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfmc] [varchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqdm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsdw] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsxh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[slmin] [numeric](19, 0) NOT NULL ,
	[slmax] [numeric](19, 0) NOT NULL ,
	[sfje] [numeric](19, 2) NOT NULL ,
	[sfnr] [varchar] (150) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (150) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xz] [numeric](1, 0) NOT NULL ,
	[sxrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_db1yflf] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1yq] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yq] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xz] [numeric](1, 0) NOT NULL ,
	[xh] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DB1YQ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1ztlx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_db1ztlx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1zxd] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jc] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhdbh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxd] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxr] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dh] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[twcz] [varchar] (28) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gsmc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhmdgls] [numeric](19, 2) NOT NULL ,
	[sm] [varchar] (28) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qxmc] [varchar] (32) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qxdm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csmc] [varchar] (22) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csdm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfdm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfmc] [varchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqdm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xz] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xza] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mdqydm] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mdqymc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DB1ZXD] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db1zxyq] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxyq] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DB1ZXYQ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [db2] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[khqc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxr] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dh] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[twcz] [varchar] (28) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[khdz] [varchar] (240) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yzbm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dh1] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[twcz1] [varchar] (28) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz1] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[flbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f15] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f17] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f19] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f22] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bp] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eml] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yjbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qcbj] [numeric](1, 0) NOT NULL ,
	[gxrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fkdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fkmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fkts] [numeric](19, 0) NOT NULL ,
	[fkje] [numeric](19, 2) NOT NULL ,
	[lxdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx2dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx2mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[usdzh] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[usdkhh] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rmbzh] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rmbkhh] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[khbgbm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dybj] [numeric](1, 0) NOT NULL ,
	[cpbj] [numeric](1, 0) NOT NULL ,
	[hth] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mdf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm3] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[syf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ewm] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kl] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jl] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjz] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cl] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sqlx] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[czyq] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jjsm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bkg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bk] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[op] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glrid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bkid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glra] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opa] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bka] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sja] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjrid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjzid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bmid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glraid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bkgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bkaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jlsj] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jleml] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ys] [numeric](19, 0) NOT NULL ,
	[fjrq] [numeric](2, 0) NOT NULL ,
	[gb] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cs] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csa] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sopno] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[doc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[docid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[docg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[docgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[doca] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[docaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[slr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[slrid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tjsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[slrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[slsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckn] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckp] [int] NOT NULL ,
	[lckt] [varchar] (23) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csmc] [varchar] (22) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csdm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfdm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfmc] [varchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qxmc] [varchar] (32) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qxdm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsid] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsjydz] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsyh] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nszh] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[usdyh] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fpdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fpmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nstt] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DB2] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [dbcq] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f32] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eta] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kmtr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lay1] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lay2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtdcdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtdcmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jmr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DBCQ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [dbj1] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40js1] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40js1h] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40js1s] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40js2] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40js3] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40js4] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40js5] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40js6] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40js7] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40js8] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40mz1] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40mz1h] [varchar] (13) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40mz1s] [varchar] (13) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40mz2] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40mz3] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40mz4] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40mz5] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40mz6] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40mz7] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40mz8] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40tj1] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40tj1h] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40tj1s] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40tj2] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40tj3] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40tj4] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40tj5] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40tj6] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40tj7] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40tj8] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f4031] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f4031h] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f4032] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f4032h] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f402] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40mt] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40hm] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40hmm] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f15] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f16] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f17] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f18] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f19] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f20] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f21] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f22] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f23] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dl] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jfdw] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jfr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jfrh] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jfc] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jfmz] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jfmzh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jfmzs] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jfdj] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jfdjh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jfje] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jfjeh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zdjs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zdjsh] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zdjss] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf1p] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf1c] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf2p] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf2c] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf3p] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf3c] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf4p] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf4c] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf5p] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf5c] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf6p] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf6c] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf7p] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf7c] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf8p] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf8c] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf9] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf10] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qtfy] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qtfyh] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhrzh] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shrzh] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dlrzh] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dliata] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhmz] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[clmz] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[clmzh] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zyrgj] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[khgj] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfdm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfdmh] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhqq] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pc3] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pc4] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hb] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hbh] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pod] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pc3h] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tdqfrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yfdd] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dfdd] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yfze] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[thr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qcys] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shdd] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dj] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mj] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gzh] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gzq] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gzyy] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dyrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fpbh] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kzyh] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sbjjddd] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sbjjdlxr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sbjjddh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxgq] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bm3] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bm2] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf1ph] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf1ch] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf2ph] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf2ch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf3ph] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf3ch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf4ph] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf4ch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf5ph] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf5ch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf6ph] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf6ch] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf7ph] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf7ch] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf8ph] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf8ch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf9h] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf10h] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40mts] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40hms] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40jsdw] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40jsdws] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxtk] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsdl] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[czyq] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[khdcyq] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcyq] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsyq] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxyq] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckyq] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shyq] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sbjjdcz] [varchar] (28) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yfdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dfdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[scac] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dfhm] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dfhh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jpks] [numeric](19, 0) NOT NULL ,
	[jpjs] [numeric](19, 0) NOT NULL ,
	[jpbl] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pl] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfts] [numeric](19, 0) NOT NULL ,
	[f40jz] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsfdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsfmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[czfdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[czfmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_dbj1] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [dbj2] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mdf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cddm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cycd] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shd] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhd] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhdlxr] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhdtx] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jcsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f3] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shfs] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shdtx] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[htr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjjs] [numeric](19, 2) NOT NULL ,
	[hwc] [numeric](19, 0) NOT NULL ,
	[hwk] [numeric](19, 0) NOT NULL ,
	[hwg] [numeric](19, 0) NOT NULL ,
	[tstj] [numeric](19, 3) NOT NULL ,
	[sjtj] [numeric](19, 3) NOT NULL ,
	[cgsm] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tgyy] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fgbz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fgyy] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ypcq] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f12dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f12] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz1] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz2] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzmdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzmdd] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tdz] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tdf] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tdqfd] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tdbz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cyr] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ym] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[imdg] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[unno] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wdc] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wdf] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fp] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysfsdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysfs] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jydwdm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jydw] [varchar] (76) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[myxz] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tgbz] [numeric](19, 3) NOT NULL ,
	[lctk] [varchar] (76) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[htxyh] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zjfs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pzwh] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[scno] [varchar] (18) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fcmdm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fcm] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rcbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rcf11] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rceta] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rcmv] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rcbl] [varchar] (18) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rcetd] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rc3f11] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rc3eta] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rc3mv] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rc3bl] [varchar] (18) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rc3etd] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eta] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dhr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fh] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rkrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rksj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cksj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mrdj] [numeric](19, 2) NOT NULL ,
	[sono] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bhk] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mbh] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hdrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qrrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bgks] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bgjs] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjf] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[szf] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[szmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[docrv] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zbrv] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yjjw] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjjw] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bgzl] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[szrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[djrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sgf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gqjh] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zs] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cdzs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yjsh] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yjsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxts] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxfy] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qcyscmm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qcyscm] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qcyshc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rcf11dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rc3f11dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xztdm] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xztmc] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bcetd] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bceta] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hgdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hgmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xddm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xdmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtdm1] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtmc1] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[agtdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[agtmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxmz] [numeric](19, 3) NOT NULL ,
	[bzdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzjs] [numeric](19, 0) NOT NULL ,
	[czyq] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjyl] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjbjdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjbjmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjcyfx1] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjcyfx2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gsfkdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gsfkmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kasbdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kasbmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[txcddm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[txcdmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ztxbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjend] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gsfkr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gqrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gqsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjylend] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjrqbeg] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rcf11dmh] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rcmvh] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rc3f11dmh] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rc3mvh] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fdfy] [numeric](2, 0) NOT NULL ,
	[ata] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jcfh] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[twcz] [varchar] (22) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shdd] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shddlxr] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shdddh] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shddtwcz] [varchar] (22) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhdgsmc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shdgsmc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yjjwsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjjwsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gncz] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gwcz] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cqeta] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzfsdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzfsmc] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gcbt] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgwc] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hwbtjt] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gqfp] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bgbh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsyq] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sgxdf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nlddf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsnckf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhmc] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lyf] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckzxf] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzdwdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzdwmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxjs] [numeric](19, 0) NOT NULL ,
	[zxtj] [numeric](19, 3) NOT NULL ,
	[zjfkmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zjdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zjmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jxxz] [numeric](19, 3) NOT NULL ,
	[bz3] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zjfkdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fphbl] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jmr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zsf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zssl] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hwbtjh] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz4] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cyfh] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jccyfh] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxdm2] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxgq2] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bm_hg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bm_sj] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dytdbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[udab] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cbhh] [varchar] (9) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kgrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kgsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jmsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cm] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[swfxrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[swfxsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[swfxbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xckadm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xckamc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wd] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[point] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cunno] [varchar] (9) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cdrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cdsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cdr] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cdrdh] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cdgs] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cddj] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cdbz] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckjs] [numeric](19, 0) NOT NULL ,
	[ckmz] [numeric](19, 3) NOT NULL ,
	[cktj] [numeric](19, 3) NOT NULL ,
	[docsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_dbj2] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [dbj3] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by1] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by2] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by3] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by4] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by5] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by6] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by7] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by8] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by9] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by10] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by11] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by12] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by13] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by14] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by15] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by16] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by17] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by18] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by19] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by20] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by21] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by22] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by23] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by24] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by25] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by26] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by27] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by28] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by29] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by30] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by31] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by32] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by33] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by34] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by35] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by36] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by37] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by38] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by39] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by40] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fh] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxxl] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[szh] [varchar] (65) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bgd] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mb] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jkxkz] [varchar] (65) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[peh] [varchar] (55) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qgd] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zgdbgfdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zgdbgfmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhd2] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhdlxr2] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhdtx2] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dbcddm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dbcdmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxcddm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxcdmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jdcp] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdwdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdwmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tgd] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjbh] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cydd] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wxf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fzdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fzmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dzdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dzmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fzr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dzr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hph] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shrdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shrmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kadldm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kadlmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gnkadm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gnkamc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gwkadm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gwkamc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jhh] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[txr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ch] [varchar] (45) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ddgnka] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ddgwka] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lkgwka] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjmc] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[twcz2] [varchar] (22) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shdd2] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shddlxr2] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shdddh2] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shddtwcz2] [varchar] (22) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhdgsmc2] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shdgsmc2] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fzrsj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dzrsj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ydczr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ydczsd] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[khwtr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shrbh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxyq] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shyq] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckyq] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jhslh] [varchar] (17) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzbgrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf29] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf31] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf32] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qf1] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hf1] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckh2] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shrbh2] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhjc] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shjc] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhjc2] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shjc2] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhgls] [numeric](19, 2) NOT NULL ,
	[shgls] [numeric](19, 2) NOT NULL ,
	[zhgls2] [numeric](19, 2) NOT NULL ,
	[shgls2] [numeric](19, 2) NOT NULL ,
	[bcxkrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gqzy] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtzx] [numeric](1, 0) NOT NULL ,
	[wtzxmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtbg] [numeric](1, 0) NOT NULL ,
	[wtbgmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtbx] [numeric](1, 0) NOT NULL ,
	[wtbxmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtbj] [numeric](1, 0) NOT NULL ,
	[wtbjmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gg] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cbhc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjmtbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjhmbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xgtk] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xgtkh] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtth] [numeric](1, 0) NOT NULL ,
	[wtthmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[khwtsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fjftk] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fjftkh] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fxrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fxsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cdyprq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cdypsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fgrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fgsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zcrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zcsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xcrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xcsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tdqrrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zgdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zgmc] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[amsd] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjdm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjmc2] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qydm2] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qymc2] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zgbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hzqsd] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhds] [numeric](19, 2) NOT NULL ,
	[shds] [numeric](19, 2) NOT NULL ,
	[lcmdm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lcm] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lhc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lmv] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hdrq1] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hdsj1] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hdrq2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hdsj2] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hdbj1] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hdbj2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lyf2] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1zx] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1sh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckmd] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jkmd] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cktc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jktc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckwt] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jkwt] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cksj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jkrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jksj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tdff] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jdrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jdsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zpje] [numeric](19, 2) NOT NULL ,
	[ssje] [numeric](19, 2) NOT NULL ,
	[jkje] [numeric](19, 2) NOT NULL ,
	[zdh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tdffh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tgdh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hcyq] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pcrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pcsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bgcmdm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bgcm] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bghc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bgmv] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bgmbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjcdrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjcdsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xcbjrq] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ctj] [numeric](19, 2) NOT NULL ,
	[cmz] [numeric](19, 3) NOT NULL ,
	[lqf] [numeric](19, 2) NOT NULL ,
	[shmdgls] [numeric](19, 2) NOT NULL ,
	[tjlc] [numeric](19, 2) NOT NULL ,
	[hdyf] [numeric](19, 2) NOT NULL ,
	CONSTRAINT [PK_dbj3] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [dbj4] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lc] [numeric](19, 0) NOT NULL ,
	[qj1] [numeric](19, 2) NOT NULL ,
	[dj1] [numeric](19, 2) NOT NULL ,
	[ts1] [numeric](19, 2) NOT NULL ,
	[cd2] [numeric](19, 2) NOT NULL ,
	[dw2] [numeric](19, 2) NOT NULL ,
	[dj2] [numeric](19, 2) NOT NULL ,
	[ts2] [numeric](19, 2) NOT NULL ,
	[sxqks] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sxqjs] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ts] [numeric](19, 2) NOT NULL ,
	[sl] [numeric](19, 2) NOT NULL ,
	[dj] [numeric](19, 2) NOT NULL ,
	[je] [numeric](19, 2) NOT NULL ,
	[lccj] [numeric](19, 0) NOT NULL ,
	[bcsl] [numeric](19, 0) NOT NULL ,
	[kjhls] [numeric](19, 2) NOT NULL ,
	[kjhlf] [numeric](19, 2) NOT NULL ,
	[kjyj] [numeric](19, 2) NOT NULL ,
	[xth] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hwzl] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzsl] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gysdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gsy] [numeric](19, 2) NOT NULL ,
	[gss] [numeric](19, 2) NOT NULL ,
	[zzy] [numeric](19, 2) NOT NULL ,
	[zzs] [numeric](19, 2) NOT NULL ,
	[xfy] [numeric](19, 2) NOT NULL ,
	[xfs] [numeric](19, 2) NOT NULL ,
	[fqy] [numeric](19, 2) NOT NULL ,
	[fqs] [numeric](19, 2) NOT NULL ,
	[hjy] [numeric](19, 2) NOT NULL ,
	[hjs] [numeric](19, 2) NOT NULL ,
	CONSTRAINT [PK_DBJ4] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [dbj5] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fcm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f31] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[y1] [varchar] (1200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hgbh] [varchar] (18) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hgfxzt] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[edijssj] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mthzzt] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ypcm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yphc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ypf31] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ypzt] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ypsj] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yphz] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xdcm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xdhc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xdf31] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xdjs] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xdmz] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yp_bj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yp_rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yp_sj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hg_bj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hg_rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hg_sj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh_bj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh_rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh_sj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xd_bj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xd_rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xd_sj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhf31] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cb_bj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cb_rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cb_sj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mv_bj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mv_rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mv_sj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f31_rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f31_sj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f29_rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f29_sj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ba_bj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ba_rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ba_sj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zh_bj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zh_rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zh_sj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hgba] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fx_bj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fx_rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fx_sj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bg_bj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bg_rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bg_sj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DBJ5] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [dbj6] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bf] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bfid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bfg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bfgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bfa] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bfaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bgg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bggid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bga] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bgaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yxmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yxrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yxsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tqdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tqmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhfdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhfmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsy] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsysj] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cxmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bcdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bcmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jxmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jcjs] [numeric](19, 0) NOT NULL ,
	[jcmz] [numeric](19, 3) NOT NULL ,
	[jctj] [numeric](19, 4) NOT NULL ,
	[jczxf] [numeric](19, 2) NOT NULL ,
	[qsfh] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[psbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[psjs] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yhjl] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fcjl] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cljg] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zysbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zjdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zjmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zjh] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[y1] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[y2] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysfph] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jcccf] [numeric](19, 2) NOT NULL ,
	[yhbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yhrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yhsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yxzph] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tpsl] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tpdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tpmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysf] [numeric](19, 2) NOT NULL ,
	[jcjljs] [numeric](19, 0) NOT NULL ,
	[jcts] [numeric](19, 0) NOT NULL ,
	[ys] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jhsdm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jhsmc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yfzb] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yfwc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[amsbllx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[amsblno] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[isfzt] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[isfztrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[isfztsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[isffsrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[isffssj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[amsztrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[amsztsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qhjs] [numeric](19, 0) NOT NULL ,
	[spjs] [numeric](19, 0) NOT NULL ,
	[f1hd] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1dc] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1tc] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[vgmin] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[vgmout] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_dbj6] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [dbj7] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bjy] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjcddm] [varchar] (37) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjf10] [varchar] (33) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjf12] [varchar] (33) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjgjdm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xssy] [numeric](19, 0) NOT NULL ,
	[yjdh] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yjlxr] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhrdh] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhrlxr] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxbh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsmcc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsmce] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywpm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wxfxz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jcy] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jcyid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sby] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sbyid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxsj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bjyid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wsb_js] [numeric](19, 0) NOT NULL ,
	[wsb_mz] [numeric](19, 3) NOT NULL ,
	[wsb_jz] [numeric](19, 2) NOT NULL ,
	[jzzgrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzzgsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzblrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzblsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hwqd] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[djr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cyr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bqtg] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bqdr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bqjs] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zgjs] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zgfcr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zgfxr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zgfc] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hyjg] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hyjgs] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ngr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[czr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zgckdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zgckmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cydz] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[djcy] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xbyj] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[swsfj] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bjsfy] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfcy] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cygy] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qtcm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_dbj7] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [dbjtm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tmlx] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tmmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tm] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mobkey] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DBJTM] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [dbxj] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cy] [numeric](1, 0) NOT NULL ,
	[cgsdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtdcdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtdcmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jxxz] [numeric](19, 3) NOT NULL ,
	[hyf] [numeric](19, 2) NOT NULL ,
	[caf] [numeric](19, 2) NOT NULL ,
	[baf] [numeric](19, 2) NOT NULL ,
	[czf] [numeric](19, 3) NOT NULL ,
	[pss] [numeric](19, 2) NOT NULL ,
	[qt1] [numeric](19, 2) NOT NULL ,
	[qt2] [numeric](19, 2) NOT NULL ,
	[ty] [numeric](19, 2) NOT NULL ,
	[hj] [numeric](19, 2) NOT NULL ,
	[f32] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxgq] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yxz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzg] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mh] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dgr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_dbxj] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [dcdmk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcdz] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_dcdmk] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [dcinfo] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ch] [numeric](2, 0) NOT NULL ,
	[mv] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcetd] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dceta] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f31] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysfs] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysfsn] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cyrdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cyrmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fcmdm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fcm] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DCINFO] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [dmyy] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dbf0] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm0] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc0] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dbf1] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm1] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc1] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yw] [numeric](1, 0) NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_dmyy] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [document] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zpx] [numeric](1, 0) NOT NULL ,
	[issuedate] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[amount] [numeric](12, 2) NOT NULL ,
	[hl] [numeric](19, 6) NOT NULL ,
	[applicant] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[term] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[paymt] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bank_id] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[curency] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[orderer] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[transdetail] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[inv_date] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[state] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[freight] [numeric](9, 2) NOT NULL ,
	[process] [numeric](10, 2) NOT NULL ,
	[premium] [numeric](9, 2) NOT NULL ,
	[place] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gspdate] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gspspot] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[credittype] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cmcode] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[commodity] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[factcode] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[factory] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[factphone] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[proccess] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fp] [numeric](1, 0) NOT NULL ,
	[zxd] [numeric](1, 0) NOT NULL ,
	[cdz] [numeric](1, 0) NOT NULL ,
	[cono] [varchar] (13) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cotype] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[invname] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[invco] [varchar] (150) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[invcoaddr] [varchar] (150) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[specialterm] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xyzdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xyzmc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ycdbzdm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ycdbz] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[appremark] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsl] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ddw] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[commission] [numeric](5, 2) NOT NULL ,
	[bzsl] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xdw] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gw] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nw] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tj] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sl] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsdw] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zj] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[biz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_document] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [dsk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xf1] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[y1] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zpje] [numeric](19, 2) NOT NULL ,
	[zprq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zpsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rwr] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zpr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ssje] [numeric](19, 2) NOT NULL ,
	[ssrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sssj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ssr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jkdh] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jkje] [numeric](19, 2) NOT NULL ,
	[jkrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jksj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jkr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DSK] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [dxlog] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxr] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[date_] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[time_] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zdh] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ynr] [varchar] (254) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yy] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DXLOG] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [dxx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[super_id] [numeric](19, 0) NOT NULL ,
	[send_id] [numeric](19, 0) NOT NULL ,
	[rcv_id] [numeric](19, 0) NOT NULL ,
	[title] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[msg] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[adv_id] [numeric](19, 0) NOT NULL ,
	[rdy] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[snd] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_dxx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [dzdm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (254) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkdm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gsmc] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dz] [varchar] (240) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxr] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dh] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cz] [varchar] (28) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eml] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjmc] [varchar] (32) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ctdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ctmc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[partyid] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[partyidlx] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yzbm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_DZDM] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [dzdmk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dzdm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dzmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_dzdmk] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ebk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fl] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcbh] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtfdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtf] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fkxy] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtbh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fm1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zx] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tdlx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tdmc] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhr] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shr] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzr] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jhd] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhg] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhg] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mdg] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzmdd] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ypcq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dlxx] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[js] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mz] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tj] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yftk] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cytk] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jobno] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cmhc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[etd] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gdh] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[slyj] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[slr] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[srrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[srsj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xgrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xgsj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tjsj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jssj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcsj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zt] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zsh] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ck] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jcrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jcsj] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jcbh] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cd] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxsj] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mddz] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxr] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxrdh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zbg] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[df] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fp] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zq] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xq] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wxp] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[class] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[imdg] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[point] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[unno] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lcp] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wdc] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wdf] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tgbz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qtf] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nzf] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mmf] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtfop] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zsfhr] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhgdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhgdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mdgdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywop] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzgdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xyh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz1] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zsshr] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qcyscmm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qcyscm] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qcyshc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qcys] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bcetd] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bceta] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtdm1] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtmc1] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sl] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nbts] [varchar] (240) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xtts] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tddf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsbm] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fms] [int] NOT NULL ,
	[glr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glrid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bmid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glra] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glraid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckn] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckp] [int] NOT NULL ,
	[lckt] [varchar] (23) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spwtbh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dfdd] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tdz] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sono] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[scac] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fcmdm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fcm] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qc] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hc] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zc1] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yc] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gc] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opa] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lkdm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bwmc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bwid] [varchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wd] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tddfmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bwdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bwlx] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fsdm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsdm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jhxt] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hzsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jhddm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ym] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bwfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ztdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ebk] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ebkzt] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ebkzt] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [edi] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dz] [varchar] (240) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxr] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dh] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cz] [varchar] (28) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eml] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yzbm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[acc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjmc] [varchar] (32) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ctdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ctmc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[partylx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[partyid] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[partyidlx] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfmc] [varchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_EDI] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [edi95] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xk] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bic] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_edi95] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [edicd] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[url] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_edicd] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [edics] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_EDICS] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [edidd] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwm] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywm] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ztdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gndm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zbdm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzdm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_edidd] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ediddgj] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwm] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywm] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ediddgj] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ediddgn] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwm] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywm] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ediddgn] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ediddsf] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwm] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywm] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ediddsf] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ediddzt] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwm] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywm] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ediddzt] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [edihg] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_edihg] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [edihs] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_EDIHS] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [edika] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_edika] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [edinbxx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nbs] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nbt] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kts] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ktt] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_edinbxx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ediunno] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ediunno] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ediuser] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yhdm] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yhmc] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_EDIUSER] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ediwbzlb] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_EDIWBZLB] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ediwfl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[unno] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[imdg] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ym] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yjcsh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jjznh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc2] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_EDIWFL] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ediwimdg] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_EDIWIMDG] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [edixd] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_EDIXD] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [egoods] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsbm] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwhm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mt] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hm] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gdh] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[js1] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mz1] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tj1] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_egoods] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ekpttsq] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kptt] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsid] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsjydz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsyh] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nszh] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[usdyh] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[usdzh] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fpdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fpmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_EKPTTSQ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [emlnr] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xz] [numeric](1, 0) NOT NULL ,
	[xh] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nr] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_EMLNR] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [entrydm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxdm] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_entrydm] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [exdata] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tname] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fname] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ftype] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[flen] [numeric](19, 0) NOT NULL ,
	[dlen] [numeric](1, 0) NOT NULL ,
	[fcomment] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fpxz] [numeric](1, 0) NOT NULL ,
	[fpbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcxz] [numeric](1, 0) NOT NULL ,
	[dcbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sbxz] [numeric](1, 0) NOT NULL ,
	[sbbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_exdata] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ezxd] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xf1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[y1] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[y2] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxdx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zjs] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ztj] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zmz] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzdw] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxyq] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wxp] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ldx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[soc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xztdm] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tjsj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zt] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[slr] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jssj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtnid] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[vgw] [numeric](19, 2) NOT NULL ,
	[vn] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[vt] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ve] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[va] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[vrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[vsj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[vm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ezxd] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ezxd0] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxdfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f31] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[js] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mz] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tj] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwhm] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mt] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hm] [varchar] (240) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tjsj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zt] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[slr] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jssj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ydh] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzdm0] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzmc0] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_EZXD0] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ezxd2] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sl] [numeric](19, 0) NOT NULL ,
	[soc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xztdm] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ezxd2] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ezxdmx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[y1] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[y2] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxdx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[soc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xztdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xztmc] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[js] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mz] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tj] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzdm0] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzmc0] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxdfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zt] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ezxdmx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [f_bm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wbdm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_F_BM] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [f_gs] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[km] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckn] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckp] [int] NOT NULL ,
	[lckt] [varchar] (23) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_F_GS] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [f_jz] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jdid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nf] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_F_JZ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [f_km] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[km] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zjm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kmmc] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kmmc2] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kmlxsz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kmlxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kmlxmc] [varchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kmlxmc2] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjkm] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzdm] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzmc] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzmc2] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wlhs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wbhs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[biz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[grhs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[slhs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rjz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yhz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[syf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gsdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gsmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yhs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_F_KM] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [f_kmlx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc2] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_F_KMLX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [f_kmxz] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc2] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sx2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_F_KMXZ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [f_kz] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jdid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nf] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qy] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qcks] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qcjs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pzks] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckn] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckp] [int] NOT NULL ,
	[lckt] [varchar] (23) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_F_KZ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [f_ly] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_F_LY] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [f_ms1] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lbdm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ds] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pzfld] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fls] [numeric](10, 0) NOT NULL ,
	[fl] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xtdm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xtmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lbmc] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[usertag] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_F_MS1] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [f_ms2] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hh] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zy] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[km] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[md] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[me] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mb] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysfph] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kmmc] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zydm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kmmc2] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wlhs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wbhs] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[biz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[grhs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yhs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[slhs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rjz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yhz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gr] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[grid] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[grg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[grgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gra] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[graid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sld] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[slc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ydm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ymc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yhdm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yhmc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fkdm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fkmc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pjh] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pjrq] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[swfid] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[flhb] [numeric](1, 0) NOT NULL ,
	[fjs] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hl] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hlc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kxfl] [numeric](1, 0) NOT NULL ,
	[gyhs] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hbyj] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dyl] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fl] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_F_MS2] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [f_ms3] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_F_MS3] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [f_pd] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hh] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[km] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kmmc] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kmmc2] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wlhs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wbhs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[biz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[grhs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[slhs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rjz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yhz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zydm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zy] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[md] [numeric](19, 2) NOT NULL ,
	[me] [numeric](19, 2) NOT NULL ,
	[mc] [numeric](19, 2) NOT NULL ,
	[mb] [numeric](19, 2) NOT NULL ,
	[hl] [numeric](19, 6) NOT NULL ,
	[sld] [numeric](19, 3) NOT NULL ,
	[slc] [numeric](19, 3) NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fkdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fkmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pjh] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[grid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[grg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[grgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gra] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[graid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dfkm] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yhlq] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wllq] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hlc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qcfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yhs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ydm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ymc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yhdm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yhmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysfph] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[swfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_F_PD] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [f_pz] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jdid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nf] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pz] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lsh] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lbdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lbmc] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fjs] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zd] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zdid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zdg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zdgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zda] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zdaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jz] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cn] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mds] [numeric](19, 2) NOT NULL ,
	[mcs] [numeric](19, 2) NOT NULL ,
	[mes] [numeric](19, 2) NOT NULL ,
	[mbs] [numeric](19, 2) NOT NULL ,
	[slds] [numeric](19, 3) NOT NULL ,
	[slcs] [numeric](19, 3) NOT NULL ,
	[bz] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckn] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckp] [int] NOT NULL ,
	[lckt] [varchar] (23) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_F_PZ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [f_pzlb] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_F_PZLB] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [f_qc] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jdid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nf] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[km] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kmlxsz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjkm] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kmmc] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kmmc2] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzmc] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzmc2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wlhs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wbhs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[biz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[grhs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[slhs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qc] [numeric](19, 2) NOT NULL ,
	[qd] [numeric](19, 2) NOT NULL ,
	[bz] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yhs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yhz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sl] [numeric](19, 3) NOT NULL ,
	CONSTRAINT [PK_F_QC] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [f_y] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_F_Y] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [f_ye] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jdid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nf] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[km] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[md0] [numeric](19, 2) NOT NULL ,
	[mc0] [numeric](19, 2) NOT NULL ,
	[me0] [numeric](19, 2) NOT NULL ,
	[mb0] [numeric](19, 2) NOT NULL ,
	[sld0] [numeric](19, 3) NOT NULL ,
	[slc0] [numeric](19, 3) NOT NULL ,
	[md1] [numeric](19, 2) NOT NULL ,
	[mc1] [numeric](19, 2) NOT NULL ,
	[me1] [numeric](19, 2) NOT NULL ,
	[mb1] [numeric](19, 2) NOT NULL ,
	[sld1] [numeric](19, 3) NOT NULL ,
	[slc1] [numeric](19, 3) NOT NULL ,
	[md2] [numeric](19, 2) NOT NULL ,
	[mc2] [numeric](19, 2) NOT NULL ,
	[me2] [numeric](19, 2) NOT NULL ,
	[mb2] [numeric](19, 2) NOT NULL ,
	[sld2] [numeric](19, 3) NOT NULL ,
	[slc2] [numeric](19, 3) NOT NULL ,
	CONSTRAINT [PK_F_YE] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [f_zb] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jdid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jdmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nf] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[km] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ly] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fl] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pdfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pz] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ri] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zy] [varchar] (252) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[md] [numeric](19, 2) NOT NULL ,
	[mc] [numeric](19, 2) NOT NULL ,
	[fx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fx2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ye] [numeric](19, 2) NOT NULL ,
	[yhdm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yhmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[me] [numeric](19, 2) NOT NULL ,
	[mb] [numeric](19, 2) NOT NULL ,
	[wfx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wfx2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wye] [numeric](19, 2) NOT NULL ,
	[ydm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ymc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_F_ZB] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [f_zy] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_F_ZY] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [f_zygs] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_F_ZYGS] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [fei_cnt] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ei] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxmc] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dxdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dxmc] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xztdm] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xztmc] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dw] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxyg] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[biz] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dj] [numeric](19, 2) NOT NULL ,
	[lx] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[srr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[srd] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_fei_cnt] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [fjk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxdm] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (13) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dx] [numeric](19, 0) NOT NULL ,
	[sgrq] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wjm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxbm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fjsj] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fj] [image] NOT NULL ,
	[fb] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjrid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjrg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjrgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjra] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjraid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sy] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yslx] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[klx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_fjk] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [fjk] 
) ON [fjk]
GO


CREATE TABLE [fjlx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_fjlx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [fjsftj] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_fjsftj] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [fkdmk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ts] [numeric](19, 0) NOT NULL ,
	[je] [numeric](19, 2) NOT NULL ,
	[sm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ys] [numeric](19, 0) NOT NULL ,
	[fjrq] [numeric](2, 0) NOT NULL ,
	CONSTRAINT [PK_fkdmk] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [fkfs] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_fkfs] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [fplx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_FPLX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [fwlx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_FWLX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [fydmk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fydm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fymc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fyemc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hs] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[km] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzl] [numeric](2, 0) NOT NULL ,
	[dwdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bll] [numeric](1, 0) NOT NULL ,
	[bjs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dj] [numeric](19, 2) NOT NULL ,
	[msdm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[msmc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_fydmk] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [fydw] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_fydw] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [fyfl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_FYFL] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [fyqz] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_fyqz] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ggdbf] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ggdbf] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [gjk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwm] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywm] [varchar] (32) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dhqh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjdm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_GJK] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [gjzt] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_gjzt] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [gkdbf] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkqc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkzwm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rcf11dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rcf11] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm2] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjzwm] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjywm] [varchar] (32) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[op] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opa] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mdmt] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_GKDBF] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [gkdbfa] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkqc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkzwm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm2] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjzwm] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjywm] [varchar] (32) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_GKDBFA] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [gkdbfb] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkqc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkzwm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm2] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_GKDBFB] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [gkdbfc] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkqc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkzwm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjzwm] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjywm] [varchar] (32) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_GKDBFC] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [gkdbfz] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzmc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkqc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xqjdm] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xqj] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_GKDBFZ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [goods] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ms] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sbhm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dj] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_GOODS] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [gsfkdm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_gsfkdm] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [gstt] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gstt] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqr] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key] [varchar] (19) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqr0] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key0] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqr1] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key1] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqr2] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key2] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqr3] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key3] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqr4] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key4] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqr5] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key5] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqr6] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key6] [varchar] (19) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqr7] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key7] [varchar] (19) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqr8] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key8] [varchar] (19) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqr9] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key9] [varchar] (19) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gsywtt] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gsdh] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gstwcz] [varchar] (28) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gsyzbm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gsdz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gsywdz] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gslxr] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gseml] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sysgg] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pmbj] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xtbj] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mk] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkqc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkdma] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tdqfd] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fpfrx] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_keya] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xks] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mk9] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gs] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sw] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jl] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkdmb] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ydm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ymc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lkdm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lkmm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lkzt] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tokenid] [varchar] (32) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bfdm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bfmm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kddm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kdmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_gstt] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [gysdb] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gysdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gysmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_gysdb] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [gysfl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_gysfl] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [gz] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yymm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sxh] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gh] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jbgz] [numeric](19, 2) NOT NULL ,
	[fsbt] [numeric](19, 2) NOT NULL ,
	[ct] [numeric](19, 2) NOT NULL ,
	[ylbx] [numeric](19, 2) NOT NULL ,
	[zfgjj] [numeric](19, 2) NOT NULL ,
	[jj] [numeric](19, 2) NOT NULL ,
	[jbgs] [numeric](19, 2) NOT NULL ,
	[jiabangz] [numeric](19, 2) NOT NULL ,
	[bcjgs] [numeric](19, 2) NOT NULL ,
	[bcjgz] [numeric](19, 2) NOT NULL ,
	[sjgs] [numeric](19, 2) NOT NULL ,
	[sjgz] [numeric](19, 2) NOT NULL ,
	[qtjb1] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qtgz1] [numeric](19, 2) NOT NULL ,
	[qtjb2] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qtgz2] [numeric](19, 2) NOT NULL ,
	[qtjb3] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qtgz3] [numeric](19, 2) NOT NULL ,
	[qtjb4] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qtgz4] [numeric](19, 2) NOT NULL ,
	[qtjb5] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qtgz5] [numeric](19, 2) NOT NULL ,
	[cdcs] [numeric](19, 0) NOT NULL ,
	[cdje] [numeric](19, 2) NOT NULL ,
	[fakuan] [numeric](19, 2) NOT NULL ,
	[yfgz] [numeric](19, 2) NOT NULL ,
	[sfgz] [numeric](19, 2) NOT NULL ,
	[ft] [numeric](19, 2) NOT NULL ,
	[ykgz] [numeric](19, 2) NOT NULL ,
	[gts] [numeric](19, 2) NOT NULL ,
	[gtssg] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yhzh] [varchar] (19) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yhje] [numeric](19, 2) NOT NULL ,
	[lx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_gz] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [hgdm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_hgdm] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [hgfl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_HGFL] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [hsdl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsfs] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_hsdk] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [hsfl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jc] [varchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ufdm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sino_dm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yg] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_hsfl] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [hsxj] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_hsxj] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [hsxm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_hsxm] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [hsyh] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ck] [numeric](19, 2) NOT NULL ,
	[wbdm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_HSYH] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [hwfl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_hwfl] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [hwzt] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_hwzt] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [hxdmk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_HXDMK] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [hxfl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_HXFL] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [hydm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_hydm] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ie] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ie] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [isf] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[scs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsbm] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qylx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qydm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qymc] [varchar] (35) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[idcode] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[idf] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dz] [varchar] (70) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yzbm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hzgdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hzh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_isf] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jcfl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_JCFL] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jjr] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jjr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_jjr] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fsdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fsmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yydm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yymc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[address] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[server] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dbname] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[username] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pass] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qy] [numeric](1, 0) NOT NULL ,
	CONSTRAINT [PK_JK] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jkfs] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_JKFS] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jkyy] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_JKYY] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jobgc] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxdm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tjr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tjsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tjzd] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[clr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[clrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[clsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[clzd] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cltg] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[clsc] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz3] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cur] [numeric](1, 0) NOT NULL ,
	[tj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cl] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tjrid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tjrg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tjrgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tjra] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tjraid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsrid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsrg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsrgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsra] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsraid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[clrid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[clrg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[clrgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[clra] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[clraid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz1] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_JOBGC] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [joblx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_JOBLX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jsdmht] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_JSDMHT] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jsdmjq] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_JSDMJQ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jsdmkq] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_jsdmkq] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jsdmlz] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_JSDMLZ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jsdmsb] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_JSDMSB] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jsdmwh] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_JSDMWH] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jshys] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjrfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jrrfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjsj] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jssj] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hysmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_JSHYS] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jshyslt] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fsfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fssj] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ltjl] [varchar] (2000) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_JSHYSLT] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jsltjl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fsfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sdfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zt] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ltjl] [varchar] (800) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jssj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxfldm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxflmc] [varchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fsrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fstm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kv] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[klx] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sw] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sb] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fstab] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jstab] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_JSLTJL] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jsmsnlxr] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[uid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mail] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[name] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nickname] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_JSMSNLXR] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jsxxfl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_jsxxfl] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jsy] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jtdh] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bgdh] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfzhm] [varchar] (18) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_jsy] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jsygw] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ksrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gw] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_JSYGW] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jsyh] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zt] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dlyh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dlmm] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gsxxys] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yxxys] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bmxxys] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[slfsys] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[slsdys] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hysys] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dlip] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dljsj] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_JSYH] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jsyht] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qdrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ksrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[htje] [numeric](19, 2) NOT NULL ,
	[htnx] [numeric](19, 0) NOT NULL ,
	[lxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_JSYHT] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jsyjl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gzksr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lzr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zw] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gzcs] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_jsyjl] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jsyjt] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gx] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xb] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mz] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzmm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xldm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xl] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hj] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_JSYJT] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jsykq] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fsrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sy] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sc] [numeric](19, 2) NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_JSYKQ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jsypx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ts] [numeric](19, 3) NOT NULL ,
	[zzdwdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzdw] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dd] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nr] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zs] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[je] [numeric](19, 2) NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[js] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_JSYPX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jsyqj] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lgrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ts] [numeric](19, 3) NOT NULL ,
	[qjlxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qjlxmc] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kfje] [numeric](19, 2) NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sy] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sc] [numeric](19, 0) NOT NULL ,
	[spr] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhr] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qjsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qjrq2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qjsj2] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[swh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sw_fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_JSYQJ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jsysg] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsy] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dd] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lb] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rysw] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsyz] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cbss] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sggs] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bgzgjg] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zgjg] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pcxcxz] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xcxz] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jhyjcs] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yjzh] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjyjcs] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jxyfcs] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cdcl] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gscl] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lpxmje] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xfje] [numeric](19, 2) NOT NULL ,
	[dfry] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xfrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bfje] [numeric](19, 2) NOT NULL ,
	[bfrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gsss] [numeric](19, 2) NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tb] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tbrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_JSYSG] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jsysj] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qyyl1] [numeric](19, 2) NOT NULL ,
	[qyyl2] [numeric](19, 2) NOT NULL ,
	[qyyl3] [numeric](19, 2) NOT NULL ,
	[qysy1] [numeric](19, 2) NOT NULL ,
	[qygs] [numeric](19, 3) NOT NULL ,
	[qysy2] [numeric](19, 3) NOT NULL ,
	[qygjj] [numeric](19, 2) NOT NULL ,
	[gryl1] [numeric](19, 2) NOT NULL ,
	[gryl2] [numeric](19, 2) NOT NULL ,
	[gryl3] [numeric](19, 2) NOT NULL ,
	[grsy1] [numeric](19, 2) NOT NULL ,
	[grgs] [numeric](19, 2) NOT NULL ,
	[grsy2] [numeric](19, 2) NOT NULL ,
	[grgjj] [numeric](19, 2) NOT NULL ,
	[zhbx] [numeric](19, 2) NOT NULL ,
	[qybxhj] [numeric](19, 2) NOT NULL ,
	[grbxhj] [numeric](19, 2) NOT NULL ,
	[bxhj] [numeric](19, 2) NOT NULL ,
	[jnrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxdm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sbjs] [numeric](19, 2) NOT NULL ,
	[gjjjs] [numeric](19, 2) NOT NULL ,
	CONSTRAINT [PK_jsysj] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jsyst] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sbjs] [numeric](19, 2) NOT NULL ,
	[lxdm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_JSYST] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jsywz] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[je] [numeric](19, 2) NOT NULL ,
	[fs] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qksm] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jlsj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_JSYWZ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jsyxf] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ffrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jbgz] [numeric](19, 2) NOT NULL ,
	[jbjj] [numeric](19, 2) NOT NULL ,
	[jabgz] [numeric](19, 2) NOT NULL ,
	[fdjj] [numeric](19, 2) NOT NULL ,
	[sjkc] [numeric](19, 2) NOT NULL ,
	[bjkc] [numeric](19, 2) NOT NULL ,
	[fkkc] [numeric](19, 2) NOT NULL ,
	[sbkc] [numeric](19, 2) NOT NULL ,
	[grsds] [numeric](19, 2) NOT NULL ,
	[sjffje] [numeric](19, 2) NOT NULL ,
	[yfje] [numeric](19, 2) NOT NULL ,
	[yfx1] [numeric](19, 2) NOT NULL ,
	[yfx2] [numeric](19, 2) NOT NULL ,
	[yfx3] [numeric](19, 2) NOT NULL ,
	[yfx4] [numeric](19, 2) NOT NULL ,
	[yfx5] [numeric](19, 2) NOT NULL ,
	[ykx1] [numeric](19, 2) NOT NULL ,
	[ykx2] [numeric](19, 2) NOT NULL ,
	[ykx3] [numeric](19, 2) NOT NULL ,
	[ykx4] [numeric](19, 2) NOT NULL ,
	[ykx5] [numeric](19, 2) NOT NULL ,
	CONSTRAINT [PK_JSYXF] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jsyxl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxksr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxjsr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxcs] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_jsyxl] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jsyxt] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzqxz] [numeric](19, 2) NOT NULL ,
	[tzhxz] [numeric](19, 2) NOT NULL ,
	CONSTRAINT [PK_JSYXT] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jsyxx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jszhm] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfzhm] [varchar] (18) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xb] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csny] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dz] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzlx] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yxrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[syrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fzjg] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yxrqq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dhh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jtdh] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgsr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jclxdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jclxmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jssj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[skkh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[plpx] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fypx] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gsfypx] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxqs] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[htqs] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[htjs] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sg] [numeric](3, 0) NOT NULL ,
	[mz] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjgz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hjdz] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wycd] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzmm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rdtsj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[whcd] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[byyx] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sxzy] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zcps] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rsda] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hyzk] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zw] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jg] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bysj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[txdz] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lzdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lzmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[whdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ldsckg] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[njhj] [numeric](19, 3) NOT NULL ,
	[njhd] [numeric](19, 3) NOT NULL ,
	[yf_y_0] [numeric](19, 2) NOT NULL ,
	[sf_y_0] [numeric](19, 2) NOT NULL ,
	[yf_m_0] [numeric](19, 2) NOT NULL ,
	[sf_m_0] [numeric](19, 2) NOT NULL ,
	[yf_y_1] [numeric](19, 2) NOT NULL ,
	[sf_y_1] [numeric](19, 2) NOT NULL ,
	[yf_m_1] [numeric](19, 2) NOT NULL ,
	[sf_m_1] [numeric](19, 2) NOT NULL ,
	[jtjs] [numeric](19, 2) NOT NULL ,
	[sb_y_0] [numeric](19, 2) NOT NULL ,
	[gr_y_0] [numeric](19, 2) NOT NULL ,
	[sb_m_0] [numeric](19, 2) NOT NULL ,
	[gr_m_0] [numeric](19, 2) NOT NULL ,
	[sb_y_1] [numeric](19, 2) NOT NULL ,
	[gr_y_1] [numeric](19, 2) NOT NULL ,
	[sb_m_1] [numeric](19, 2) NOT NULL ,
	[gr_m_1] [numeric](19, 2) NOT NULL ,
	[cb_y_0] [numeric](19, 2) NOT NULL ,
	[cb_m_0] [numeric](19, 2) NOT NULL ,
	[cb_y_1] [numeric](19, 2) NOT NULL ,
	[cb_m_1] [numeric](19, 2) NOT NULL ,
	[njhj_1] [numeric](19, 3) NOT NULL ,
	[njhd_1] [numeric](19, 3) NOT NULL ,
	[lckn] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckp] [int] NOT NULL ,
	[lckt] [varchar] (23) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hdgl] [numeric](19, 0) NOT NULL ,
	[ljgl] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsgl] [numeric](19, 2) NOT NULL ,
	[sbybh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bgybh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxybh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jcybh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bjybh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_JSYXX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jxfs] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_jxfs] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jydwdmk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (76) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bm_hg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bm_sj] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hgfl] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywmc] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywdz] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dh] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[twcz] [varchar] (28) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yb] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sg] [numeric](1, 0) NOT NULL ,
	CONSTRAINT [PK_jydwdmk] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [jzfs] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_JZFS] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [kdd] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kddh] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kddm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kdmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jjdw] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jjr] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jjdz] [varchar] (240) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jjdh] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jjsj] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfd] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jjid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjdw] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjr] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjdz] [varchar] (240) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjdh] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjsj] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mdd] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eml] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jz] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dyrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dysj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kdje] [numeric](19, 2) NOT NULL ,
	[zyfbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yffid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_kdd] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [khmyd] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_KHMYD] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [khxqj] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_khxqj] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [khyj] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ksrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qrbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzrq] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by9] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[by10] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[quoteno] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fwdm] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fwmc] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fwmc2] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxbz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckn] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckp] [int] NOT NULL ,
	[lckt] [varchar] (23) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_KHYJ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [khyjlx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_khyjlx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [khyjmenu] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jdid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fjd] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xm] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glrid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glz] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glzid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glra] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glraid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fwdm] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fwmc] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fwmc2] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_KHYJMENU] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [khyjmx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmmc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dj] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f20] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f42] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f45] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[biz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dw] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gxrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xsxh] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_khyjmx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [khyjtj] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[title] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxdm] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[khdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[khmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[link] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ksrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zt] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[czrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_khyjtj] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [khyjxy] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xyh] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcfdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcf] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xqjdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xqj] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkd1] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkm1] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkd2] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkm2] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkd3] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkm3] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sxrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[srrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ggbj2] [numeric](1, 0) NOT NULL ,
	[ggbj3] [numeric](1, 0) NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[quoteno] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[subno] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[priceno] [varchar] (31) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f20] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f42] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f45] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zd] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ts] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mbl] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nxz] [numeric](5, 0) NOT NULL ,
	[xh] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dj] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[biz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dw] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ggbj1] [numeric](5, 0) NOT NULL ,
	[ggbj4] [numeric](5, 0) NOT NULL ,
	[dqrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fwtk] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fkfs] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pcterm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxgq] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jg] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tybj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ty] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_KHYJXY] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [kjlx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_KJLX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [kl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jdid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fjd] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kl] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zcx] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jb] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[czfw] [varchar] (999) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (21) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ky] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dh] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[twcz] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jy] [numeric](1, 0) NOT NULL ,
	[fw] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ewxm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wlfw] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[oth] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fwlst] [varchar] (244) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wllst] [varchar] (244) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zw1] [image] NOT NULL ,
	[zw2] [image] NOT NULL ,
	[dm2] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nfg] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yslst] [varchar] (254) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yflst] [varchar] (254) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wlmsn] [numeric](19, 0) NOT NULL ,
	[sino_dm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sino_bs] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sino_dir] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eml] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[msn] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qq] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[swfw] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[swlst] [varchar] (244) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kl1] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xgfw] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xglst] [varchar] (244) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsy] [numeric](5, 0) NOT NULL ,
	[lz] [numeric](5, 0) NOT NULL ,
	[hrfw] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hrlst] [varchar] (244) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fjfw] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fjlst] [varchar] (244) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckn] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckp] [int] NOT NULL ,
	[lckt] [varchar] (23) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[msnzl] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[msnbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yjfw] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yjlst] [varchar] (244) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yxfw] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yxlst] [varchar] (244) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysfw] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yffw] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cwfw] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cwlst] [varchar] (244) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dutydm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dutymc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[aqjb] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xsbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xsmc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[m_smtp] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[m_port] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[m_user] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[m_pwd] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[m_ssl] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_kl] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [klm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kl] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_klm] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [kyfkfs] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zw] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ew] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_KYFKFS] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [kyfl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkqc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zl] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fl] [numeric](19, 2) NOT NULL ,
	[sm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_KYFL] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [kyfldm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zw] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ew] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_kyfldm] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [lcsk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cz] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zid] [numeric](3, 0) NOT NULL ,
	[station] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dzsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fcsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxsj] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[km] [numeric](8, 0) NOT NULL 
) ON [PRIMARY]
GO


CREATE TABLE [lkbwb] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bwpt] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bwdm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bwmc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bwbs] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bwsm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gldw] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwdmzd] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmczd] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bwlx] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ty] [numeric](1, 0) NOT NULL ,
	[jmlx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywlxdm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_lkbwb] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [lkbwfx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_lkbwfx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [lkbwhz] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_lkbwhz] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [lkbwlog] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wljhdm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fx] [numeric](1, 0) NOT NULL ,
	[bwbs] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bwmc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wldwdm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wldwmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsjhdm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[op] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bw] [image] NOT NULL ,
	[cs] [int] NOT NULL ,
	[jsbw] [image] NOT NULL ,
	[bwid] [varchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fsrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fssj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jssj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ztdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ztmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jhxt] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fxmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[url] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[usr] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pwd] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wjm] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cwyy] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[acdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[acmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hzdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hzmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsdwdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsdwmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fxdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cl] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[khbh] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[usercode] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ebkfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_lkbwlog] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [lkbwmsg] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ktfsdm] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ktjsdm] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[state] [int] NOT NULL ,
	[type] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[khbh] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcbh] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_lkbwmsg] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [lkbwzt] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_lkbwzt] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [lx2dm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_lx2dm] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [lxdmk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_lxdmk] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [lyftzxm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[s_je] [numeric](19, 3) NOT NULL ,
	[f_je] [numeric](19, 4) NOT NULL ,
	[bz] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_LYFTZXM] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [madditem] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[flag] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[no] [numeric](19, 2) NOT NULL ,
	[xm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xm_us] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cmd] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[msg] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[msg_us] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[skipfor] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nxz] [numeric](5, 0) NOT NULL ,
	[dyl] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_madditem] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [maillog] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[item_id] [int] NOT NULL ,
	[sbrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sbsj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[op] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zdm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fjwjm] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fjr] [varchar] (128) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[klx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kv] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fjk_fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[title] [varchar] (254) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_maillog] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [mawb] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mawb] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[srrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm3] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hkgs] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sy] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_mawb] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [menuc] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[no] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fxprq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fxpsj] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[prg] [image] NOT NULL ,
	[fxp] [image] NOT NULL ,
	[xjr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yslx] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dx] [numeric](19, 0) NOT NULL ,
	[lxdm] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lj] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxbm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nxz] [numeric](5, 0) NOT NULL ,
	CONSTRAINT [PK_MENUC] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [menuclx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_MENUCLX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [menui] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jdid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fjd] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xm_us] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mname] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[itemname] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[flag] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hotkey] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ty] [numeric](1, 0) NOT NULL ,
	[cmd] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[skipfor] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[msg] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[msg_us] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[chg_tag] [numeric](5, 0) NOT NULL ,
	[fj] [numeric](1, 0) NOT NULL ,
	CONSTRAINT [PK_MENUI] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [mffs] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_MFFS] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [msdm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_msdm] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [msncd] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[parentfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[name] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lv] [int] NOT NULL ,
	[sfzj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kjtj] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dyhs] [varchar] (2000) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mb] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zldm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_MSNCD] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [msndzxx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[func] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[args] [numeric](5, 0) NOT NULL ,
	[xxnr] [varchar] (500) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zl] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_MSNDZXX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [msnltjl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fsz] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsz] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fssj] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ltjl] [varchar] (2000) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_MSNLTJL] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [msnsession] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[msn] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[beginfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[endfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqtj] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_MSNSESSION] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [msnzdxx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxnr] [varchar] (500) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[scsj] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqsj] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxrmsn] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sffs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[link] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_msnzdxx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [mtqydm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_MTQYDM] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [netlog] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[zdm] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zdip] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dlrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dlsj] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dz] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dlm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dlxm] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mk] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_NETLOG] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [opm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_OPM] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [pcdl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc2] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_PCDL] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [pkg_cz] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_pkg_cz] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [pkg_fb] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_pkg_fb] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [pkg_yt] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_PKG_YT] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [pkgs] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jpdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qtmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ly] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_pkgs] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [podmfk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_PODMFK] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [podmfl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_PODMFL] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [podmhy] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_PODMHY] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [podmjck] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_PODMJCK] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [podmk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qc] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bh] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtk] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[htk] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f12dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f12] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qc2] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qc3] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_podmk] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [podmzt] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_PODMZT] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [poitem] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pono] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[itemno] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgbh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[itemztdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[itemzt] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shrdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shrmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shr] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gysdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gysmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gys] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsbm] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[style] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dx] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ys] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mt] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[styno] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hm] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjs] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[js] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mz] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jz] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tj] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[biz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dj] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sl] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[je] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hylxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hylx] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pc1] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f4032] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f12dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f12] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzmdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzmdd] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shddm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shdd] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yjbt] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[etd] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yqsd] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ky] [numeric](1, 0) NOT NULL ,
	CONSTRAINT [PK_poitem] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [posyk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[pono] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pofm1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[podm] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pomc] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pomc2] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[porq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[poqr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[poztdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pozt] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzbh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[myfs] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[myfn] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jckdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jck] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fkdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fkmc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mjdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mjmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mj] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tyrdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tyrmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tyr] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhddm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhdmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhd] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhddm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhdmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhd] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzrdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzrmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzr] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhxgrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zje] [numeric](19, 2) NOT NULL ,
	[bz] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxjs] [numeric](19, 0) NOT NULL ,
	[zjs] [numeric](19, 0) NOT NULL ,
	[zmz] [numeric](19, 3) NOT NULL ,
	[zjz] [numeric](19, 3) NOT NULL ,
	[ztj] [numeric](19, 3) NOT NULL ,
	[hth] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[htrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckh] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qt] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cj] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cja] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckn] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckp] [int] NOT NULL ,
	[lckt] [varchar] (23) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gxz] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gxrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xms] [numeric](19, 0) NOT NULL ,
	[glr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glrid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bmid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glra] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glraid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cs] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csa] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_POSYK] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [qxdm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (32) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qxdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_qxdm] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [qydm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_QYDM] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [qygj] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qydm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qymc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjzwm] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjywm] [varchar] (32) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_QYGJ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [register] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[khqc] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[khdz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxr] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dh] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[twcz] [varchar] (28) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eml] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dzlxr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dh1] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[twcz1] [varchar] (28) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yjbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fkmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz1] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_register] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [rl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ggl] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_rl] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [rqfl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_rqfl] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [sddm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_sddm] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [sfdm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfjc] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqdm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_SFDM] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [sfdzk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dzlx] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dzmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dzbh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[srrq] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tkhrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ggbj] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ggmc] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dybj] [numeric](1, 0) NOT NULL ,
	[bh] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[scsr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhsr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tkhsr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qsr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qsrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fb] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kjbh] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kjdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kjmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kjbht] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tgzx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bgqsrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bgqsr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bgscrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bgscr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[srsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tkhsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hgba] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tdtzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tdtzsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xf1] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[y1] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[myfs] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[myfn] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cddm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cdmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fl] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsy] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtdm2] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtmc2] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfts] [numeric](19, 0) NOT NULL ,
	CONSTRAINT [PK_SFDZK] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [sflx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_SFLX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [sgfaxsend] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[taskid] [int] NOT NULL ,
	[subtime] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sendername] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rcvername] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[faxnum] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[schsendtime] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[userid] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[filename] [varchar] (255) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[filepath] [varchar] (255) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sendfaxnum] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[status] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[transstatus] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sendtime] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[senddescribe] [varchar] (255) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[transfilename] [varchar] (255) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[transfilepath] [varchar] (255) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[filesize] [int] NOT NULL ,
	[username] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[logonname] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ex1] [varchar] (255) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ex2] [varchar] (255) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ex3] [varchar] (255) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ex4] [varchar] (255) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ex5] [varchar] (255) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_sgfaxsend] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [sinofydm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsdldm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsdlmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ygs] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ygf] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ys] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dsdf] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_sinofydm] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [sinokm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_sinokm] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [sinoswmx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bj] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sino_khdm] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mxno] [varchar] (150) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[je] [numeric](19, 2) NOT NULL ,
	[hcrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL 
) ON [PRIMARY]
GO


CREATE TABLE [sinoywlx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsfldm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[exp] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_sinoywlx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [sjcl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_SJCL] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [sjdz] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hh] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ph] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xssy] [numeric](19, 0) NOT NULL ,
	[qfdw] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzsj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_SJDZ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [sjhz] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jydw] [varchar] (76) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bm_hg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bm_sj] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jydwdm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sdrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xsys] [numeric](19, 0) NOT NULL ,
	[xsyy] [numeric](19, 0) NOT NULL ,
	[xswy] [numeric](19, 0) NOT NULL ,
	[jy] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzsj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_SJHZ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [sjlx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_SJLX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [slc] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[prod] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[forw] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[carr] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cb] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rate] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_slc] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [slv] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pol] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pod] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tmode] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[vol] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tos] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[comm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cb] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_slv] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [sm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cn] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_sm] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [sn] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zdm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sysno] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sn] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sysno5] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sn5] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sysno6] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sn6] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sysno60] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sn60] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sysno61] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sn61] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sysno62] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sn62] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjzdm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ip] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mac] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cn] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wg] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hdid] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zdmold] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sysno66] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sn66] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sysno67] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sn67] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sysno68] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sn68] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jy] [numeric](1, 0) NOT NULL ,
	[sysnocy5] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sncy5] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[updrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[updsj] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pz] [numeric](5, 0) NOT NULL ,
	[drcs] [int] NOT NULL ,
	[drsc] [numeric](19, 2) NOT NULL ,
	CONSTRAINT [PK_sn] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [snreq] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gstt] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zdm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sysno] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sn] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjzdm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ip] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mac] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cn] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wg] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hdid] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zdmold] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzjs] [numeric](1, 0) NOT NULL ,
	[msg] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_snreq] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [sob] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[orderid] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xddm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xdmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xsl] [numeric](19, 0) NOT NULL ,
	[xh] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wxl] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wxw] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wxh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rxl] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rxw] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rxh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pol] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pod1] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mandm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[manmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jcrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxbz] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[instat] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1ex] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1ref] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjtxrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yxdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yxmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f29] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f31] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f32] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f32sj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pod] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eta] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[etasj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fxjhr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxstat] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fd] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mzts] [numeric](19, 0) NOT NULL ,
	[fdts] [numeric](19, 0) NOT NULL ,
	[tbrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tbts] [numeric](19, 0) NOT NULL ,
	[tbxq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tbje] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdz] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shr] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fdstart] [numeric](1, 0) NOT NULL ,
	[rate] [numeric](19, 2) NOT NULL ,
	[dpp] [numeric](19, 2) NOT NULL ,
	[yxbt] [numeric](19, 2) NOT NULL ,
	[dsps] [numeric](19, 2) NOT NULL ,
	[zxfy] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tstk] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qydd] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kstxr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhcyr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz1] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bj_in] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bj_out] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eir] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxxl] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_sob] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [socsta] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_socsta] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [sqyx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_SQYX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [stbz] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_stbz] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [stcklx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mkey] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mval] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_STCKLX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [stdw] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc2] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc3] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_stdw] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [stinspect] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_stinspect] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [stkw] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_stkw] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [stlx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_stlx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [stlx2] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_stlx2] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [stlx3] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_stlx3] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [stsp] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qc] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jj] [numeric](19, 2) NOT NULL ,
	[sj] [numeric](19, 2) NOT NULL ,
	[jqcb] [numeric](19, 2) NOT NULL ,
	[kcs] [numeric](19, 2) NOT NULL ,
	[kcje] [numeric](19, 2) NOT NULL ,
	[aqcl] [numeric](19, 2) NOT NULL ,
	[bz1] [varchar] (249) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[syf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gg] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cd] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cs] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywm] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ms] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jcwz] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hs] [numeric](19, 0) NOT NULL ,
	[lx2dm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx2mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lmd] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[idate] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zkl] [numeric](19, 2) NOT NULL ,
	[lx3dm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx3mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zkj] [numeric](19, 2) NOT NULL ,
	[jmbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ty] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckn] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckp] [int] NOT NULL ,
	[lckt] [varchar] (23) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_STSP] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [stspmx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wldm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wlmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zy] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[djj] [numeric](19, 2) NOT NULL ,
	[djh] [numeric](19, 4) NOT NULL ,
	[slj] [numeric](19, 2) NOT NULL ,
	[jej] [numeric](19, 2) NOT NULL ,
	[jeh] [numeric](19, 2) NOT NULL ,
	[djc] [numeric](19, 2) NOT NULL ,
	[djd] [numeric](19, 4) NOT NULL ,
	[slc] [numeric](19, 2) NOT NULL ,
	[jec] [numeric](19, 2) NOT NULL ,
	[jed] [numeric](19, 2) NOT NULL ,
	[djk] [numeric](19, 2) NOT NULL ,
	[djl] [numeric](19, 4) NOT NULL ,
	[slk] [numeric](19, 2) NOT NULL ,
	[jek] [numeric](19, 2) NOT NULL ,
	[jel] [numeric](19, 2) NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ph] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yxq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[refid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_STSPMX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [stywlx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mkey] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mval] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_stywlx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [stzld] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gg] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xsdj] [numeric](19, 2) NOT NULL ,
	[xssl] [numeric](19, 0) NOT NULL ,
	[xsje] [numeric](19, 2) NOT NULL ,
	[cgdj] [numeric](19, 2) NOT NULL ,
	[cgje] [numeric](19, 2) NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[srrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ph] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yxq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywm] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cd] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxh] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jmbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zczh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zldm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zlzk] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_stzld] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [sw] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yshj1] [numeric](19, 2) NOT NULL ,
	[sshj1] [numeric](19, 2) NOT NULL ,
	[yfbfhj1] [numeric](19, 2) NOT NULL ,
	[yfsjze1] [numeric](19, 2) NOT NULL ,
	[lr1] [numeric](19, 2) NOT NULL ,
	[bizhong1] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[srbj1] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yskpbj1] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ssbj1] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz1] [varchar] (70) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yshj2] [numeric](19, 2) NOT NULL ,
	[sshj2] [numeric](19, 2) NOT NULL ,
	[yfbfhj2] [numeric](19, 2) NOT NULL ,
	[yfsjze2] [numeric](19, 2) NOT NULL ,
	[lr2] [numeric](19, 2) NOT NULL ,
	[bizhong2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[srbj2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yskpbj2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ssbj2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz2] [varchar] (70) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yshj3] [numeric](19, 2) NOT NULL ,
	[sshj3] [numeric](19, 2) NOT NULL ,
	[yfbfhj3] [numeric](19, 2) NOT NULL ,
	[yfsjze3] [numeric](19, 2) NOT NULL ,
	[lr3] [numeric](19, 2) NOT NULL ,
	[bizhong3] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[srbj3] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yskpbj3] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ssbj3] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz3] [varchar] (70) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xsje] [numeric](19, 2) NOT NULL ,
	[cgje] [numeric](19, 2) NOT NULL ,
	[mlje] [numeric](19, 2) NOT NULL ,
	[ssum] [numeric](19, 2) NOT NULL ,
	[fsum] [numeric](19, 2) NOT NULL ,
	[lsum] [numeric](19, 2) NOT NULL ,
	[sea_e_f] [numeric](19, 1) NOT NULL ,
	[sea_e_l] [numeric](19, 3) NOT NULL ,
	[sea_i_f] [numeric](19, 1) NOT NULL ,
	[sea_i_l] [numeric](19, 3) NOT NULL ,
	[air_e] [numeric](19, 1) NOT NULL ,
	[air_i] [numeric](19, 1) NOT NULL ,
	[mljh] [numeric](19, 2) NOT NULL ,
	[ysjh] [numeric](19, 2) NOT NULL ,
	[yfjh] [numeric](19, 2) NOT NULL ,
	[jhqr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jhrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsdm] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jtje] [numeric](19, 2) NOT NULL ,
	[jtrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jhzz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzzz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsfldm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsflmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jshl] [numeric](19, 2) NOT NULL ,
	[ystz1] [numeric](19, 2) NOT NULL ,
	[yftz1] [numeric](19, 2) NOT NULL ,
	[ystz2] [numeric](19, 2) NOT NULL ,
	[yftz2] [numeric](19, 2) NOT NULL ,
	[ystz3] [numeric](19, 2) NOT NULL ,
	[yftz3] [numeric](19, 2) NOT NULL ,
	[stzsum] [numeric](19, 2) NOT NULL ,
	[ftzsum] [numeric](19, 2) NOT NULL ,
	[ltzsum] [numeric](19, 2) NOT NULL ,
	[ltz1] [numeric](19, 2) NOT NULL ,
	[ltz2] [numeric](19, 2) NOT NULL ,
	[ltz3] [numeric](19, 2) NOT NULL ,
	[zt] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sinoywdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sinoywlx] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yshj4] [numeric](19, 2) NOT NULL ,
	[sshj4] [numeric](19, 2) NOT NULL ,
	[yfbfhj4] [numeric](19, 2) NOT NULL ,
	[yfsjze4] [numeric](19, 2) NOT NULL ,
	[lr4] [numeric](19, 2) NOT NULL ,
	[bizhong4] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[srbj4] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yskpbj4] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ssbj4] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz4] [varchar] (70) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ystz4] [numeric](19, 2) NOT NULL ,
	[yftz4] [numeric](19, 2) NOT NULL ,
	[ltz4] [numeric](19, 2) NOT NULL ,
	[jxs] [numeric](19, 2) NOT NULL ,
	[xxs] [numeric](19, 2) NOT NULL ,
	[yns] [numeric](19, 2) NOT NULL ,
	[jxj] [numeric](19, 2) NOT NULL ,
	[xxj] [numeric](19, 2) NOT NULL ,
	[mlj] [numeric](19, 2) NOT NULL ,
	[lx] [numeric](19, 2) NOT NULL ,
	[tx] [numeric](19, 2) NOT NULL ,
	[mlcx] [numeric](19, 2) NOT NULL ,
	[ssj] [numeric](19, 2) NOT NULL ,
	[fsj] [numeric](19, 2) NOT NULL ,
	[mljs] [numeric](19, 2) NOT NULL ,
	[xmjt] [numeric](19, 2) NOT NULL ,
	[xmrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kdf] [numeric](19, 2) NOT NULL ,
	CONSTRAINT [PK_sw] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [swagtbk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[srpdj1] [numeric](19, 3) NOT NULL ,
	[srpdj2] [numeric](19, 3) NOT NULL ,
	[srpdj3] [numeric](19, 3) NOT NULL ,
	[srpdj4] [numeric](19, 3) NOT NULL ,
	[srpdj5] [numeric](19, 3) NOT NULL ,
	[srpdj6] [numeric](19, 3) NOT NULL ,
	[srpdj7] [numeric](19, 3) NOT NULL ,
	[srpdj8] [numeric](19, 3) NOT NULL ,
	[srpje1] [numeric](19, 2) NOT NULL ,
	[srpje2] [numeric](19, 2) NOT NULL ,
	[srpje3] [numeric](19, 2) NOT NULL ,
	[srpje4] [numeric](19, 2) NOT NULL ,
	[srpje5] [numeric](19, 2) NOT NULL ,
	[srpje6] [numeric](19, 2) NOT NULL ,
	[srpje7] [numeric](19, 2) NOT NULL ,
	[srpje8] [numeric](19, 2) NOT NULL ,
	[srcdj1] [numeric](19, 3) NOT NULL ,
	[srcdj2] [numeric](19, 3) NOT NULL ,
	[srcdj3] [numeric](19, 3) NOT NULL ,
	[srcdj4] [numeric](19, 3) NOT NULL ,
	[srcdj5] [numeric](19, 3) NOT NULL ,
	[srcdj6] [numeric](19, 3) NOT NULL ,
	[srcdj7] [numeric](19, 3) NOT NULL ,
	[srcdj8] [numeric](19, 3) NOT NULL ,
	[srcje1] [numeric](19, 2) NOT NULL ,
	[srcje2] [numeric](19, 2) NOT NULL ,
	[srcje3] [numeric](19, 2) NOT NULL ,
	[srcje4] [numeric](19, 2) NOT NULL ,
	[srcje5] [numeric](19, 2) NOT NULL ,
	[srcje6] [numeric](19, 2) NOT NULL ,
	[srcje7] [numeric](19, 2) NOT NULL ,
	[srcje8] [numeric](19, 2) NOT NULL ,
	[stpdj1] [numeric](19, 3) NOT NULL ,
	[stpdj2] [numeric](19, 3) NOT NULL ,
	[stpdj3] [numeric](19, 3) NOT NULL ,
	[stpdj4] [numeric](19, 3) NOT NULL ,
	[stpdj5] [numeric](19, 3) NOT NULL ,
	[stpdj6] [numeric](19, 3) NOT NULL ,
	[stpdj7] [numeric](19, 3) NOT NULL ,
	[stpdj8] [numeric](19, 3) NOT NULL ,
	[stpje1] [numeric](19, 2) NOT NULL ,
	[stpje2] [numeric](19, 2) NOT NULL ,
	[stpje3] [numeric](19, 2) NOT NULL ,
	[stpje4] [numeric](19, 2) NOT NULL ,
	[stpje5] [numeric](19, 2) NOT NULL ,
	[stpje6] [numeric](19, 2) NOT NULL ,
	[stpje7] [numeric](19, 2) NOT NULL ,
	[stpje8] [numeric](19, 2) NOT NULL ,
	[stcdj1] [numeric](19, 3) NOT NULL ,
	[stcdj2] [numeric](19, 3) NOT NULL ,
	[stcdj3] [numeric](19, 3) NOT NULL ,
	[stcdj4] [numeric](19, 3) NOT NULL ,
	[stcdj5] [numeric](19, 3) NOT NULL ,
	[stcdj6] [numeric](19, 3) NOT NULL ,
	[stcdj7] [numeric](19, 3) NOT NULL ,
	[stcdj8] [numeric](19, 3) NOT NULL ,
	[stcje1] [numeric](19, 2) NOT NULL ,
	[stcje2] [numeric](19, 2) NOT NULL ,
	[stcje3] [numeric](19, 2) NOT NULL ,
	[stcje4] [numeric](19, 2) NOT NULL ,
	[stcje5] [numeric](19, 2) NOT NULL ,
	[stcje6] [numeric](19, 2) NOT NULL ,
	[stcje7] [numeric](19, 2) NOT NULL ,
	[stcje8] [numeric](19, 2) NOT NULL ,
	[agt1] [numeric](19, 2) NOT NULL ,
	[agt2] [numeric](19, 2) NOT NULL ,
	[agt3] [numeric](19, 2) NOT NULL ,
	[agt4] [numeric](19, 2) NOT NULL ,
	[agt5] [numeric](19, 2) NOT NULL ,
	[agt6] [numeric](19, 2) NOT NULL ,
	[agt7] [numeric](19, 2) NOT NULL ,
	[agt8] [numeric](19, 2) NOT NULL ,
	[jfsl1] [numeric](19, 3) NOT NULL ,
	[jfsl2] [numeric](19, 3) NOT NULL ,
	[jfsl3] [numeric](19, 3) NOT NULL ,
	[jfsl4] [numeric](19, 3) NOT NULL ,
	[jfsl5] [numeric](19, 3) NOT NULL ,
	[jfsl6] [numeric](19, 3) NOT NULL ,
	[jfsl7] [numeric](19, 3) NOT NULL ,
	[jfsl8] [numeric](19, 3) NOT NULL ,
	[dlbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_swagtbk] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [swchg] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bk] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bf] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kf] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zdlx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zdcd] [numeric](19, 0) NOT NULL ,
	[xsw] [numeric](19, 0) NOT NULL ,
	CONSTRAINT [PK_swchg] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [swdefa] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zs] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dmbds] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mcbds] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gddm] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[djexp] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[slexp] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[je] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[biz] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywdm] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xz] [numeric](1, 0) NOT NULL ,
	[zzl] [numeric](2, 0) NOT NULL ,
	[dwdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ch] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bm] [varchar] (9) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_swdefa] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [swex] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhid] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhr] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shid] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shr] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spid] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzbj2] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzxz2] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pzys] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pzss] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsid] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsjydz] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsyh] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nszh] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_SWEX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [swfp] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysfph] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[expid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kpex] [numeric](19, 2) NOT NULL ,
	[zzl] [numeric](2, 0) NOT NULL ,
	[zzj] [numeric](19, 2) NOT NULL ,
	[zzs] [numeric](19, 2) NOT NULL ,
	[bkzzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nstt] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsid] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsjydz] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsyh] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nstt2] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsid2] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsjydz2] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsyh2] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lbdm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spbh] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zyspmc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ssyf] [numeric](2, 0) NOT NULL ,
	[fhr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zfbz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bsbz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dybz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qdbz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[djbz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wkbz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xfbz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[syh] [numeric](19, 2) NOT NULL ,
	[sbbz] [numeric](19, 2) NOT NULL ,
	[fpdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fpmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xd] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xdjg] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fph] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ffrdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ffr] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[biz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yshj] [numeric](19, 2) NOT NULL ,
	[kpex2] [numeric](19, 2) NOT NULL ,
	[zzl2] [numeric](2, 0) NOT NULL ,
	[zzj2] [numeric](19, 2) NOT NULL ,
	[zzs2] [numeric](19, 2) NOT NULL ,
	[ffrfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pz] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fplx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fpzt] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lbdm2] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysfph2] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzdbh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zfr] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zfrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_SWFP] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [swfpms] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysfph] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f29] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f31] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kpex] [numeric](19, 2) NOT NULL ,
	[f10dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f12dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f12] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjyf] [numeric](19, 2) NOT NULL ,
	[gkmt] [numeric](19, 2) NOT NULL ,
	[qtsf] [numeric](19, 2) NOT NULL ,
	[bz] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xtts] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_swfpms] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [swfpmx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[expid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysfph] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fpmxxh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kpex] [numeric](19, 2) NOT NULL ,
	[zzl] [numeric](2, 0) NOT NULL ,
	[zzj] [numeric](19, 2) NOT NULL ,
	[zzs] [numeric](19, 2) NOT NULL ,
	[spbh] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spmc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ggxh] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jldw] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sl] [numeric](19, 2) NOT NULL ,
	[dj] [numeric](19, 2) NOT NULL ,
	[hsjbz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_swfpmx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [swjx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fsdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fsmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zdex] [numeric](19, 2) NOT NULL ,
	[f33] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sssj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ssbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qxr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jxts] [numeric](19, 0) NOT NULL ,
	[fxts] [numeric](19, 0) NOT NULL ,
	[txts] [numeric](19, 0) NOT NULL ,
	[lx] [numeric](19, 2) NOT NULL ,
	[tx] [numeric](19, 2) NOT NULL ,
	[bz] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jxper] [numeric](19, 2) NOT NULL ,
	CONSTRAINT [PK_swjx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [swly] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_swly] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [swlyftz] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmmc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[je] [numeric](19, 2) NOT NULL ,
	[bz] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_swlyftz] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [swsf] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fl] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ly] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysfph] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yshj] [numeric](19, 2) NOT NULL ,
	[yskpbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysbkxm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sshj] [numeric](19, 2) NOT NULL ,
	[sssj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ssbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ffrdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ffr] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bkzzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sd] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xz] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pzh] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (254) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hr] [numeric](19, 6) NOT NULL ,
	[sjbz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjje] [numeric](19, 2) NOT NULL ,
	[pc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmh1] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmh2] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmh3] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmh4] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmh5] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmh6] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmh7] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmh8] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfnr1] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfnr2] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfnr3] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfnr4] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfnr5] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfnr6] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfnr7] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfnr8] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dj1] [numeric](19, 3) NOT NULL ,
	[dj2] [numeric](19, 3) NOT NULL ,
	[dj3] [numeric](19, 3) NOT NULL ,
	[dj4] [numeric](19, 3) NOT NULL ,
	[dj5] [numeric](19, 3) NOT NULL ,
	[dj6] [numeric](19, 3) NOT NULL ,
	[dj7] [numeric](19, 3) NOT NULL ,
	[dj8] [numeric](19, 3) NOT NULL ,
	[sfje1] [numeric](19, 2) NOT NULL ,
	[sfje2] [numeric](19, 2) NOT NULL ,
	[sfje3] [numeric](19, 2) NOT NULL ,
	[sfje4] [numeric](19, 2) NOT NULL ,
	[sfje5] [numeric](19, 2) NOT NULL ,
	[sfje6] [numeric](19, 2) NOT NULL ,
	[sfje7] [numeric](19, 2) NOT NULL ,
	[sfje8] [numeric](19, 2) NOT NULL ,
	[bz1] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz2] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz3] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz4] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz5] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz6] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz7] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz8] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zbcopy] [numeric](1, 0) NOT NULL ,
	[zzbj] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glfdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glf] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhxgrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[biz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dzje] [numeric](19, 2) NOT NULL ,
	[sl1] [numeric](19, 4) NOT NULL ,
	[sl2] [numeric](19, 4) NOT NULL ,
	[sl3] [numeric](19, 4) NOT NULL ,
	[sl4] [numeric](19, 4) NOT NULL ,
	[sl5] [numeric](19, 4) NOT NULL ,
	[sl6] [numeric](19, 4) NOT NULL ,
	[sl7] [numeric](19, 4) NOT NULL ,
	[sl8] [numeric](19, 4) NOT NULL ,
	[ywmc] [numeric](1, 0) NOT NULL ,
	[dd] [numeric](1, 0) NOT NULL ,
	[bl] [numeric](19, 2) NOT NULL ,
	[sdr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glz] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dzh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kpbz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fkdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fkmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzxz] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fyqr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsyhdm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsyhmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsxjdm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsxjmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kphl] [numeric](19, 6) NOT NULL ,
	[km] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kpje] [numeric](19, 2) NOT NULL ,
	[hcbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glrid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glra] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glzid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glraid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hth] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[db1fkmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ddnid] [numeric](19, 0) NOT NULL ,
	[llpr] [varchar] (36) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[llpd] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lspr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lspd] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lqpr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lqpd] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lts] [numeric](19, 0) NOT NULL ,
	[xf1] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[y1] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ocl] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dxbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ddfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fpzf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kpc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qrrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kpqr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cny] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cdno] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[oth] [numeric](19, 2) NOT NULL ,
	[nxz] [numeric](5, 0) NOT NULL ,
	[zdex] [numeric](19, 2) NOT NULL ,
	[kpex] [numeric](19, 2) NOT NULL ,
	[xzex] [numeric](19, 2) NOT NULL ,
	[zzl] [numeric](2, 0) NOT NULL ,
	[zzs] [numeric](19, 2) NOT NULL ,
	[zzj] [numeric](19, 2) NOT NULL ,
	[zdhl] [numeric](19, 6) NOT NULL ,
	[zdc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[expid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fpdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fpmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwdm1] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwdm2] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwdm3] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwdm4] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwdm5] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwdm6] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwdm7] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwdm8] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc1] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc2] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc3] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc4] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc5] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc6] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc7] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc8] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sds] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kpyq] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bll] [numeric](1, 0) NOT NULL ,
	[wpxz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ffrmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fl1] [numeric](19, 3) NOT NULL ,
	[fl2] [numeric](19, 3) NOT NULL ,
	[fl3] [numeric](19, 3) NOT NULL ,
	[fl4] [numeric](19, 3) NOT NULL ,
	[fl5] [numeric](19, 3) NOT NULL ,
	[fl6] [numeric](19, 3) NOT NULL ,
	[fl7] [numeric](19, 3) NOT NULL ,
	[fl8] [numeric](19, 3) NOT NULL ,
	[fj1] [numeric](19, 2) NOT NULL ,
	[fj2] [numeric](19, 2) NOT NULL ,
	[fj3] [numeric](19, 2) NOT NULL ,
	[fj4] [numeric](19, 2) NOT NULL ,
	[fj5] [numeric](19, 2) NOT NULL ,
	[fj6] [numeric](19, 2) NOT NULL ,
	[fj7] [numeric](19, 2) NOT NULL ,
	[fj8] [numeric](19, 2) NOT NULL ,
	[sfje] [numeric](19, 2) NOT NULL ,
	[fj] [numeric](19, 2) NOT NULL ,
	[ys1] [numeric](19, 2) NOT NULL ,
	[ys2] [numeric](19, 2) NOT NULL ,
	[ys3] [numeric](19, 2) NOT NULL ,
	[ys4] [numeric](19, 2) NOT NULL ,
	[ys5] [numeric](19, 2) NOT NULL ,
	[ys6] [numeric](19, 2) NOT NULL ,
	[ys7] [numeric](19, 2) NOT NULL ,
	[ys8] [numeric](19, 2) NOT NULL ,
	[tbfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_swsf] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [swsv] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fl] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ly] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yshj] [numeric](19, 2) NOT NULL ,
	[xmh1] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[biz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[km] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsfldm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_swsv] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [tcfs] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_TCFS] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [tdff] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_TDFF] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [tdlx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_tdlx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [tdlxdz] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_tdlxdz] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [tdlxyj] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_tdlxyj] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [tidanb] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[no] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tdmc] [varchar] (28) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tdzl] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tdprg] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[faxto] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[faxexp] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qy] [numeric](1, 0) NOT NULL ,
	[jkcx] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rptrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rptsj] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[frx] [image] NOT NULL ,
	[frt] [image] NOT NULL ,
	[ccto] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ccexp] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[app_m] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dxnr] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yslx] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bbbeg] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bbprg] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cy] [numeric](1, 0) NOT NULL ,
	[dyl] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_tidanb] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [tidanbb] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bbdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bbmc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bbfile] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bblx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bbprg] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bbbeg] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_tidanbb] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [tidanft] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[no] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[faxto] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[faxexp] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_TIDANFT] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [tidangd] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[no] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tdmc] [varchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tdprg] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tdzl] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjr] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_tidangd] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [tidansql] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [int] NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sql] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gl] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[myorder] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mygroup] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zb] [int] NOT NULL ,
	CONSTRAINT [PK_tidansql] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [tidanyy] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[app_m] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_TIDANYY] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [tkhfs] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_TKHFS] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [tmlx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_TMLX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [tplx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_TPLX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [userkey] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[keylabel] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[key_] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_userkey] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [userrep] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bbdm] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bbfile] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bblx] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bbbeg] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bbprg] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jbtj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fjtj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cucw] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cview] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cclass] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ccxlx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cprep] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[id] [numeric](19, 0) NOT NULL ,
	[yhtj] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bmlst] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bbmc] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[head1] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[head2] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[head3] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[head4] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jj1] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jj2] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jj3] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jj4] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jj5] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dycs] [numeric](5, 0) NOT NULL ,
	[mschart] [numeric](5, 0) NOT NULL ,
	[sqlselect] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sqlleft] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sqlwhere] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sqlorderby] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sqlgroupby] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_USERREP] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [users1] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key5] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key6] [varchar] (19) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqr0] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key60] [varchar] (19) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqr1] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key61] [varchar] (19) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqr2] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key62] [varchar] (19) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqr3] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key63] [varchar] (19) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqr4] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key64] [varchar] (19) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqr5] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key65] [varchar] (19) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqr6] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key66] [varchar] (19) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqr7] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key67] [varchar] (19) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqr8] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key68] [varchar] (19) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqr9] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key69] [varchar] (19) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xyks] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hdver] [numeric](19, 3) NOT NULL ,
	[dqr] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mk] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ts] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gszy] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fkxy] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ztdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ztmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gmdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gmmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hydm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hymc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lydm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lymc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fwdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fwmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zjr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fwry] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rmbgsmc] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[usdgsmc] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rmbyh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[usdyh] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rmbzh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[usdzh] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nsh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zyyw] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ffts] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[regmnt] [numeric](19, 2) NOT NULL ,
	[regbz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[regrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[regxm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mydm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mymc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[djrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csmc] [varchar] (22) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csdm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfdm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfmc] [varchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qxmc] [varchar] (32) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qxdm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gszy1] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zsbh] [varchar] (18) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zsks] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zsjs] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mk9] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mk10] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqr10] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key6a] [varchar] (19) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key6b] [varchar] (19) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_keya] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zcts] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bv] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[regfr] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[regzs] [varchar] (240) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[regzt] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[reglx] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[regdj] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[regmc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[regwz] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[regsl] [numeric](5, 0) NOT NULL ,
	[regnj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[regjg] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[regqt] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[regcx] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_keykp] [varchar] (19) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ts0] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ts1] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ts2] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ts6] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ts7] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqrc] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key6c] [varchar] (19) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqrd] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key6d] [varchar] (19) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqre] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key6e] [varchar] (19) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqrf] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key6f] [varchar] (19) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fzbl] [numeric](2, 0) NOT NULL ,
	[dqrg] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key6g] [varchar] (19) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqrh] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sys_key6h] [varchar] (19) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[regdh] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[regeml] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[reghy] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[regxydm] [varchar] (18) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[reggd] [varchar] (254) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[regrz] [varchar] (254) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pwd] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nszg] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ns_beg] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ns_end] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_users1] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [wjcurr] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[c_symb] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[name] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rate] [numeric](19, 6) NOT NULL ,
	[cdmk] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rate_usd] [numeric](19, 6) NOT NULL ,
	[cdmk_usd] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[code] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yf] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rate_yf] [numeric](19, 6) NOT NULL ,
	[sino_biz] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rate_hkd] [numeric](19, 6) NOT NULL ,
	[cdmk_hkd] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_WJCURR] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [wjgods] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[code] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[name] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hzcd] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dtmk] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_wjgods] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [wjgodsa] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[code] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[name] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hzcd] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dtmk] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_wjgodsa] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [wjjgtk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[code] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[name] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_wjjgtk] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [wjmc] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_WJMC] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [wjmywk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spco] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spcn] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysfs] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysfn] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gods] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[godn] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zyfs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zyfn] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgtk] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgtn] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[myfs] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[myfn] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spot] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[spon] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mpot] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mpon] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opot] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opon] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[quan] [numeric](19, 3) NOT NULL ,
	[myfs2] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[myfn2] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[myfs3] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[myfn3] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gj0] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gj2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjrq0] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjrq2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bgks0] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bgks2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zs0] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zs2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hgcyr0] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hgcyr2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hgjs0] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hgjs2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gqjh0] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gqjh2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[djrq0] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[djrq2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhrq2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[myfs4] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[myfn4] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ldfs1] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ldfs2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ldfs3] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ldfs4] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxdf10] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxdf11] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxdf12] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtdm2] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtmc2] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jcgsdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jcgsmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fs1] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fs2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fs3] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fs4] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shjs1] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shjs2] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shmz1] [varchar] (13) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shmz2] [varchar] (13) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shtj1] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shtj2] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pcrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pcsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[phmz] [numeric](19, 1) NOT NULL ,
	[fpb] [numeric](6, 2) NOT NULL ,
	[jsmz] [numeric](19, 1) NOT NULL ,
	[phmzh] [numeric](19, 1) NOT NULL ,
	[fpbh] [numeric](6, 2) NOT NULL ,
	[jsmzh] [numeric](19, 1) NOT NULL ,
	[phmzs] [numeric](19, 1) NOT NULL ,
	[fpbs] [numeric](6, 2) NOT NULL ,
	[jsmzs] [numeric](19, 1) NOT NULL ,
	[f40jst] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40mzt] [varchar] (13) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f40tjt] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jfmzt] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[phmzt] [numeric](19, 1) NOT NULL ,
	[thxx] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wjfdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wjfmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wjbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxfldm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxflmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxfsdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxfsmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjqsrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjqssj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ddsf] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csmc] [varchar] (22) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[csdm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfdm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sfmc] [varchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bdh] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tbbf] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bbxr] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tbr] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pfdd] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zyfjg] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jhjs] [numeric](19, 0) NOT NULL ,
	[jhmz] [numeric](19, 3) NOT NULL ,
	[jhtj] [numeric](19, 3) NOT NULL ,
	[yjdz] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yjlxr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yjdh] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sb] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yap] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsdbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsdpc] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zd1js] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zd1jg] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zd2js] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zd2jg] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zd3js] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zd3jg] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hwzj] [numeric](19, 2) NOT NULL ,
	[bxysjs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxysjg] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxyfjs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxyfjg] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysqs] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cjsjh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqdm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dqmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qxmc] [varchar] (32) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qxdm2] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgsg] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fgsg] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zcsg] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxdf10dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxdf11dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxdf12dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zm1] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dj1] [numeric](19, 2) NOT NULL ,
	[bz1] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hl1] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[edibz] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bgbz] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxbz] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[djkx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjzs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bjbz] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shbz] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cdjjr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dtbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wxpfl] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yjcsh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jjznh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tfl] [numeric](19, 3) NOT NULL ,
	[bxfl] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bbxdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fdrq1] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fdsj1] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fdrq2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fdsj2] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yap2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywc2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhtcf] [numeric](19, 2) NOT NULL ,
	[shtcf] [numeric](19, 2) NOT NULL ,
	[xzhds] [numeric](19, 2) NOT NULL ,
	[xshds] [numeric](19, 2) NOT NULL ,
	[xzhss] [numeric](19, 2) NOT NULL ,
	[xshss] [numeric](19, 2) NOT NULL ,
	[fhrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysbf] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yfbf] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wgbm] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtqydm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtqymc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mdqydm] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mdqymc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwxb] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hgfl] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtqydm2] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtqymc2] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mdqydm2] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mdqymc2] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fsrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fssj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jszt] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsdm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjztdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjztmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dcmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xwdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xrdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xgdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xadm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xamc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ie] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ytdm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ytmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lydm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lymc] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gzfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jhs] [numeric](19, 0) NOT NULL ,
	[wcs] [numeric](19, 0) NOT NULL ,
	[sxts] [numeric](19, 0) NOT NULL ,
	[sxrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[vgmzz] [numeric](19, 2) NOT NULL ,
	CONSTRAINT [PK_wjmywk] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [wjport] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[code] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[name] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ename] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[country] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_wjport] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [wjporta] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[code] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[name] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ename] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[country] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_wjporta] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [wjspco] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[code] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[name] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_wjspco] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [wjtrade] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[code] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[name] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_wjtrade] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [wjtransf] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[code] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[name] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_WJTRANSF] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [wjzyfs] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[code] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[name] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[unit] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_wjzyfs] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [worker] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wrk] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wrkid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wrkg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wrkgid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wrka] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wrkaid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bfb] [numeric](19, 2) NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mobkey] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fwdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fwmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jljs] [numeric](19, 0) NOT NULL ,
	[hwc] [numeric](19, 2) NOT NULL ,
	[hwk] [numeric](19, 2) NOT NULL ,
	[hwg] [numeric](19, 2) NOT NULL ,
	[dwtj] [numeric](19, 4) NOT NULL ,
	[jstj] [numeric](19, 4) NOT NULL ,
	[dwdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qty] [numeric](19, 4) NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_WORKER] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [wsb] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[unno] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wxpfl] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ym] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[js] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ems] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfag] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mz] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jz] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[imdg] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[point] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wxpph] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzsj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_WSB] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [xg_bx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[y1] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxje] [numeric](19, 2) NOT NULL ,
	[bxfy] [numeric](19, 2) NOT NULL ,
	[bxgsdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxgs] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxqxq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxqxz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bdh] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jb] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[blrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_XG_BX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [xg_dz] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[czc] [numeric](1, 0) NOT NULL ,
	[zc_] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cef] [numeric](1, 0) NOT NULL ,
	[ef_] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[szc] [numeric](1, 0) NOT NULL ,
	[zc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sef] [numeric](1, 0) NOT NULL ,
	[ef] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cyp] [numeric](1, 0) NOT NULL ,
	[yp_] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[syp] [numeric](1, 0) NOT NULL ,
	[yp] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_xg_dz] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [xg_gz] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[y1] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eir] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ddm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dd] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dzdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dz] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ef] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xssm] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f31] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f29] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yxrdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yxr] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f12dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f12] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzsj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yp] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ytdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ytmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwhm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f32] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qcys] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bcetd] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xadm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xamc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xwdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xrdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xgdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xgf1] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_xg_gz] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [xg_tj] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_xg_tj] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [xg_xa] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_xg_xa] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [xg_xg] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_xg_xg] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [xg_xly] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_xg_xly] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [xg_xq] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxbz] [numeric](5, 0) NOT NULL ,
	[ksrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_XG_XQ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [xg_xr] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_xg_xr] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [xg_xt] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[y1] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ddh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xddm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xdjc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjjc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gcdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gcjc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxbz] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lydm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lymc] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zqks] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zqjz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxqxq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bxqxz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckn] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lckp] [int] NOT NULL ,
	[lckt] [varchar] (23) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ddm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dd] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kxz] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xrj] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ypbj] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ypf31] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ypf29] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ypf32] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[clrd1] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[clrd2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[chkd] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hotd1] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hotd2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xqks] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xqjz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fid_gz] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tjdm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tjmc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[y1y] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_xg_xt] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [xg_xw] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_xg_xw] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [xg_yt] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_xg_yt] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [xg_zq] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxbz] [numeric](5, 0) NOT NULL ,
	[ksrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[czdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[czmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[biz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dj] [numeric](19, 2) NOT NULL ,
	[sl] [numeric](19, 3) NOT NULL ,
	[dwdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[je] [numeric](19, 2) NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwhm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hth] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_XG_ZQ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [xhfs] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_XHFS] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [xj] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sw] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cj] [numeric](19, 0) NOT NULL ,
	[wtf2dm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtf2] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glrid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bmid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glra] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[glraid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yq] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkqc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mz] [numeric](19, 3) NOT NULL ,
	[tj] [numeric](19, 3) NOT NULL ,
	[xxxl] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj1dm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj1mc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj2dm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj2mc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj3dm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj3mc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywdm] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yw] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_XJ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [xmwz] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_XMWZ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [xsjs] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_XSJS] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [xtmc] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc2] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc3] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_XTMC] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [xxdmk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_xxdmk] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [xxdxk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_xxdxk] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [xxfjk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_xxfjk] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [xxpzk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[teu] [numeric](19, 2) NOT NULL ,
	[xl] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xw] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wxl] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wxw] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wxh] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cu] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cp] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ytdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ctj] [numeric](19, 2) NOT NULL ,
	[cmz] [numeric](19, 3) NOT NULL ,
	CONSTRAINT [PK_XXPZK] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [xxztk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_xxztk] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [xyzlx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc2] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_xyzlx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [xztdm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_xztdm] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ycdbz] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ycdbz] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [yftkk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yftk] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_yftkk] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [yhty] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yh] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhrgsmc] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhrdz] [varchar] (240) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhrlxr] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhrdh] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhrcz] [varchar] (28) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhreml] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shrgsmc] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shrdz] [varchar] (240) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shrlxr] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shrdh] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shrcz] [varchar] (28) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shreml] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzrgsmc] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzrdz] [varchar] (240) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzrlxr] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzrdh] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzrcz] [varchar] (28) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzreml] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzr2gsmc] [varchar] (160) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzr2dz] [varchar] (240) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzr2lxr] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzr2dh] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzr2cz] [varchar] (28) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzr2eml] [varchar] (44) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhr] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shr] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzr] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzr2] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pc1] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yftk] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fjftk] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_YHTY] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [yhzh] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[usdid] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rmbid] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[usdzhjc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rmbzhjc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[usdyh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rmbyh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[usdzh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rmbzh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[usdgsmc] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rmbgsmc] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_yhzh] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [yhzh1] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzid] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhjc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gsmc] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_yhzh1] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [yjhl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtf2dm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtf2] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[biz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ksrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjmc] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysfs] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysfn] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_yjhl] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [yjhlb] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mz1] [numeric](19, 1) NOT NULL ,
	[mz2] [numeric](19, 1) NOT NULL ,
	[aaa] [numeric](19, 2) NOT NULL ,
	[aab] [numeric](19, 2) NOT NULL ,
	[aac] [numeric](19, 2) NOT NULL ,
	[aad] [numeric](19, 2) NOT NULL ,
	[aae] [numeric](19, 2) NOT NULL ,
	[aaf] [numeric](19, 2) NOT NULL ,
	[aag] [numeric](19, 2) NOT NULL ,
	[aah] [numeric](19, 2) NOT NULL ,
	[aai] [numeric](19, 2) NOT NULL ,
	[aaj] [numeric](19, 2) NOT NULL ,
	[aak] [numeric](19, 2) NOT NULL ,
	[aal] [numeric](19, 2) NOT NULL ,
	[aam] [numeric](19, 2) NOT NULL ,
	[aan] [numeric](19, 2) NOT NULL ,
	[aao] [numeric](19, 2) NOT NULL ,
	[aap] [numeric](19, 2) NOT NULL ,
	[aaq] [numeric](19, 2) NOT NULL ,
	[aar] [numeric](19, 2) NOT NULL ,
	[aas] [numeric](19, 2) NOT NULL ,
	[aat] [numeric](19, 2) NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_YJHLB] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [yqlx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (14) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_YQLX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ysfs] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ysfs] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ywdmk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qc] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bh] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtk] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[htk] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f11] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f12dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f12] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qc2] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qc3] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsfldm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsflmc] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzl] [numeric](2, 0) NOT NULL ,
	CONSTRAINT [PK_ywdmk] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ywjzk] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywdm] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzm1] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzjc1] [varchar] (27) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bj1] [numeric](1, 0) NOT NULL ,
	[jzm2] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzjc2] [varchar] (27) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bj2] [numeric](1, 0) NOT NULL ,
	[jzm3] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzjc3] [varchar] (27) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bj3] [numeric](1, 0) NOT NULL ,
	[jzm4] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzjc4] [varchar] (27) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bj4] [numeric](1, 0) NOT NULL ,
	[jzm5] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzjc5] [varchar] (27) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bj5] [numeric](1, 0) NOT NULL ,
	[jzm6] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzjc6] [varchar] (27) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bj6] [numeric](1, 0) NOT NULL ,
	[jzm7] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzjc7] [varchar] (27) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bj7] [numeric](1, 0) NOT NULL ,
	[jzm8] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzjc8] [varchar] (27) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bj8] [numeric](1, 0) NOT NULL ,
	[jzm9] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzjc9] [varchar] (27) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bj9] [numeric](1, 0) NOT NULL ,
	[jzm12] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzm22] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzm32] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzm42] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzm52] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzm62] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzm72] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzm82] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzm92] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dyxs1] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dyxs2] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dyxs3] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dyxs4] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dyxs5] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dyxs6] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dyxs7] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dyxs8] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dyxs9] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ywjzk] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ywsj] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zt] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ms] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[kssj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jssj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pfyj] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pfr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pfrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jbr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_YWSJ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ywsy] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fm1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cs] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f10] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f12dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f12] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lay1] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lay2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f4031h] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pc2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f4032h] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mz] [numeric](19, 3) NOT NULL ,
	[zwhm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tj] [numeric](19, 2) NOT NULL ,
	CONSTRAINT [PK_YWSY] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ywzl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bh] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bflydm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bflymc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ry] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bm] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qy] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ryid] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bmid] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qyid] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ms] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bflxdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bflxmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fzr1] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq1] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yy] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fzr2] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jy] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fzr3] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq3] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zg] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rkrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yjwc] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wc] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fzr4] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq4] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgqr] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[khqr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qrrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jg] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ywzl] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ywzt] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ywzt] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [ywzy] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ok] [numeric](1, 0) NOT NULL ,
	CONSTRAINT [PK_YWZY] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [yxfs] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_yxfs] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [zcsj] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjjs] [numeric](19, 0) NOT NULL ,
	[hw] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjmz] [numeric](19, 3) NOT NULL ,
	[sjtj] [numeric](19, 4) NOT NULL ,
	[sjhc] [numeric](19, 2) NOT NULL ,
	[sjhk] [numeric](19, 2) NOT NULL ,
	[sjhg] [numeric](19, 2) NOT NULL ,
	[shsj] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ps] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tq] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[psqk] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[psjs] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzdw] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjmt] [varchar] (2000) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjhm] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gg] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cyjs] [numeric](19, 0) NOT NULL ,
	[cymz] [numeric](19, 3) NOT NULL ,
	[cytj] [numeric](19, 3) NOT NULL ,
	[ckfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hwztdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hwzt] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hwfldm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hwfl] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jljs] [numeric](19, 0) NOT NULL ,
	[wlh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pono] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[grade] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zj] [numeric](19, 0) NOT NULL ,
	CONSTRAINT [PK_ZCSJ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [zcsjc] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cksj] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckjs] [numeric](19, 0) NOT NULL ,
	[ckmz] [numeric](19, 3) NOT NULL ,
	[cktj] [numeric](19, 3) NOT NULL ,
	[thcph] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[thdw] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[thr] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxdh] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zjbh] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wlh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pono] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[grade] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zj] [numeric](19, 0) NOT NULL ,
	CONSTRAINT [PK_ZCSJC] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [zgcl] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ZGCL] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [zlbflx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ZLBFLX] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [zlbfly] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (60) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ZLBFLY] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [zxcd] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysf1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xf1] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[del] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[rq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxyq] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsy] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsydh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsy1] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gls] [numeric](19, 1) NOT NULL ,
	[hdyh] [numeric](19, 2) NOT NULL ,
	[txdwdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[txdwmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[txdbh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[txdz] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[txlxr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[txdh] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[txcz] [varchar] (22) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[txmc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[txjc] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhdwdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhdwmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhdbh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzxdz] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzxlxr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzxdh] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzxcz] [varchar] (22) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzxmc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhjc] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhdwdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhdwmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shdbh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xshdz] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xshlxr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xshdh] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xshcz] [varchar] (22) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xshmc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shjc] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ytx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yzx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ycc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yxx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sg] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cdzxrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cdzxsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cdxxrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cdxxsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ZXCD] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [zxd] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xf1] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[y1] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[y2] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxmc] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxdx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxdxm] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zjs] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ztj] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zmz] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxdno] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzdw] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxyq] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wxp] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ldx] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tg] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cg] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yxts] [numeric](19, 0) NOT NULL ,
	[mfts] [numeric](19, 0) NOT NULL ,
	[cqts] [numeric](19, 0) NOT NULL ,
	[cqdj] [numeric](19, 2) NOT NULL ,
	[cqje] [numeric](19, 2) NOT NULL ,
	[hxf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wxf] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[soc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xztdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xztmc] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gndxh] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ch] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pzh] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gwch] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzbz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cddm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cdmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gqxw] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shcddm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shcdmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tzsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sdrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sdsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shyq] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhch] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhtgch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhcjsy] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhcjsydh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shtgch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shcjsy] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shcjsydh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xyjzxrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xyjzxsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xyjshrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xyjshsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzxdz] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzxlxr] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzxdh] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzxcz] [varchar] (22) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzxmc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xshdz] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xshlxr] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xshdh] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xshcz] [varchar] (22) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xshmc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pcbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hth] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mddpcbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shdbh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhdbh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhzlrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhzlsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dlshd] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[khshd] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xydczr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xydczsd] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xqshd] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhjc] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shjc] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysf1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bcdm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bcmc] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bchc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtdm2] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtmc2] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bcetd] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bceta] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtdm1] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtmc1] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhmc] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bcxkrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fzdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fzmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fzr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dzdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dzmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dzr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hph] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jhslh] [varchar] (17) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsjg] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhcjsy1] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shcjsy1] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[teu] [numeric](19, 2) NOT NULL ,
	[refno] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yjbl] [numeric](6, 2) NOT NULL ,
	[yap] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yap2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pcbj2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywc2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sgzh] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sgsh] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm2] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shsg] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[st1] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[st2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[x1] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[x2] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhfj] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xqssj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xqsr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckkxcc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckzxjg] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckzxzc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckkxzc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jkzxxc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jkkxxc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jkzxlg] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jkkxhc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fgbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zcrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zcsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xcrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xcsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxts] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nxz] [numeric](1, 0) NOT NULL ,
	[xdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eir] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qc] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hc] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yc] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zc] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gc] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bwch] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bwhh] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bwlh] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wd] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tfl] [numeric](19, 3) NOT NULL ,
	[fdrq1] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fdsj1] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fdrq2] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fdsj2] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzhds] [numeric](19, 2) NOT NULL ,
	[xshds] [numeric](19, 2) NOT NULL ,
	[zhtcf] [numeric](19, 2) NOT NULL ,
	[shtcf] [numeric](19, 2) NOT NULL ,
	[xzhss] [numeric](19, 2) NOT NULL ,
	[xshss] [numeric](19, 2) NOT NULL ,
	[jkfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tcfsdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tcfsmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmwzdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmwzmc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yshj2] [numeric](19, 2) NOT NULL ,
	[yfhj2] [numeric](19, 2) NOT NULL ,
	[lr2] [numeric](19, 2) NOT NULL ,
	[tkr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tksj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[lxrc] [numeric](19, 0) NOT NULL ,
	[ddxx] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[y2c] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zjz] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxjs] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dbbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[djwcrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[djwcsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtqydm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtqymc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mdqydm] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mdqymc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yszdjg] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yszdjs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yfzdjg] [varchar] (120) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yfzdjs] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[y3] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtqydm2] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtqymc2] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mdqydm2] [varchar] (8) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mdqymc2] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tcfsdm2] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tcfsmc2] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wjbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wxpfl] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wtnid] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hdyh] [numeric](19, 2) NOT NULL ,
	[lqf] [numeric](19, 2) NOT NULL ,
	[shmdgls] [numeric](19, 2) NOT NULL ,
	[tjlc] [numeric](19, 2) NOT NULL ,
	[hdyf] [numeric](19, 2) NOT NULL ,
	[vgw] [numeric](19, 2) NOT NULL ,
	[vn] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[vt] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ve] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[va] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[vrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[vsj] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[vm] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_zxd] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [zxd0] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxdfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f31] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jcbh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[js] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mz] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tj] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwhm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mt] [varchar] (2000) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fh] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ydh] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fhdh0] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xsdh0] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzdm0] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzmc0] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzjs0] [numeric](19, 0) NOT NULL ,
	[cgdh0] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[erpno] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gqddh] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hwsfrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hwqsrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[js0] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mz0] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tj0] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pno] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jz] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xc] [numeric](19, 2) NOT NULL ,
	[xk] [numeric](19, 2) NOT NULL ,
	[xg] [numeric](19, 2) NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hm] [varchar] (240) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qthm] [varchar] (240) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzmc2] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzmc3] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zyl] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jyl] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hhy] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjs] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wgbm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ippb] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ippc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f17] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fbtd] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fbtm] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hwcw] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hwps] [numeric](19, 0) NOT NULL ,
	[jssj] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[czdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[czmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cs] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cz] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[doc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[docid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[op] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[opid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bk] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bkid] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cksj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[efbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[efrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[eftm] [varchar] (18) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[wpsl] [numeric](19, 0) NOT NULL ,
	[f15] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtqr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_zxd0] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [zxd1] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hsbm] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mtxh] [varchar] (2000) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwhm] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hwmc] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsl] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzsl] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nw] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gw] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tj] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[biz] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dj] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sl] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zj] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sg1] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sg2] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sg3] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xc] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xk] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xg] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xtj] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmz] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjz] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjs] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xdw] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ddw] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsdw] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[itemno] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ys] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cdbzdm] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cdbzmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjmc] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gjmc2] [varchar] (32) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jsfs] [numeric](5, 0) NOT NULL ,
	CONSTRAINT [PK_zxd1] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [zxd2] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sl] [numeric](19, 0) NOT NULL ,
	[teu] [numeric](19, 2) NOT NULL ,
	[soc] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xztdm] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mxts] [numeric](5, 0) NOT NULL ,
	[yhxs] [numeric](5, 0) NOT NULL ,
	[cqxt] [numeric](5, 0) NOT NULL ,
	[sjmx] [numeric](5, 0) NOT NULL ,
	[sjcq] [numeric](5, 0) NOT NULL ,
	[dj] [numeric](19, 2) NOT NULL ,
	[je] [numeric](19, 2) NOT NULL ,
	[ctj] [numeric](19, 2) NOT NULL ,
	[cmz] [numeric](19, 3) NOT NULL ,
	[wxpfl] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_zxd2] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [zxd3] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mt] [varchar] (2000) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwhm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[js] [numeric](19, 0) NOT NULL ,
	[mz] [numeric](19, 3) NOT NULL ,
	[tj] [numeric](19, 3) NOT NULL ,
	[jgrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cldm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[clmc] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tlgr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[refno] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ZXD3] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [zxd4] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ysf1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mt] [varchar] (2000) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwhm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[js] [numeric](19, 0) NOT NULL ,
	[mz] [numeric](19, 3) NOT NULL ,
	[mzt] [numeric](19, 3) NOT NULL ,
	[tj] [numeric](19, 3) NOT NULL ,
	[dj] [numeric](19, 2) NOT NULL ,
	[je] [numeric](19, 2) NOT NULL ,
	[fph] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shcddm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shcdmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxgq] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shdbh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhdbh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzxdz] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xshdz] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzxlxr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xshlxr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzxdh] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xshdh] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzxcz] [varchar] (22) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xshcz] [varchar] (22) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhjc] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shjc] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzxmc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xshmc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhmdgls] [numeric](19, 2) NOT NULL ,
	[shmdgls] [numeric](19, 2) NOT NULL ,
	[jzfsdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzfsmc] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjr] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[del] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[yap] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ywc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhdwdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zhdwmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhdwdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhdwmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ZXD4] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [zxd5] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[pno] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zwhm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mt] [varchar] (2000) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hm] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjs] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xmz] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xtj] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xc] [numeric](19, 2) NOT NULL ,
	[xk] [numeric](19, 2) NOT NULL ,
	[xg] [numeric](19, 2) NOT NULL ,
	[mz] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jz] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xjz] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tj] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzmc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hmqt] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ccdm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ccmc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ystj] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xtdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xtmc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mccwz] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mhwzt] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzmc2] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bzmc3] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ccmc2] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ccmc3] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ZXD5] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [zxd6] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f9] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f8] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[js] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mz] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[tj] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzyh] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jz] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bqh] [varchar] (3) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hm] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hmqt] [varchar] (200) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ssth] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sbwh] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc2] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dwmc3] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ggxh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gg] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xh] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ZXD6] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [zxd7] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[y1] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[y2] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxdx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jgsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[fgbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zcrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zcsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxgq] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhf31] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhcm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhhc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhf10] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhf11] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhf12] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhxz] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhhz] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhxd] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhjc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhcc] [varchar] (30) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xdcm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xdhc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xdy1] [varchar] (11) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xdjgsj] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xdjgdd] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xdxjy] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xdbwh] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xdbwsj] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckqc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ckdz] [varchar] (240) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jxr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cyy] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jcy] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jcrq] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jcch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ccrq] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ccch] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzfsdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jzfsmc] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[point] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[exno] [text] COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xbgcmdm] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xbgcm] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xbghc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xbgmv] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xbgmbj] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ZXD7] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [zxd8] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xdf31] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xdmz] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xdjs] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xdhgfz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xdhgsj] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xdf11dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xdch] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhf31] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhjs] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xhmz] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ZXD8] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [zxf] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xxts] [numeric](19, 0) NOT NULL ,
	[sxts] [numeric](19, 0) NOT NULL ,
	[sfdj] [numeric](19, 2) NOT NULL ,
	[sss] [numeric](19, 2) NOT NULL ,
	[sm] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_zxf] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [zxgz] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zxdfid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ddrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[ddsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dddm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dd] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[jl] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sj] [varchar] (24) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xzrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[qr] [varchar] (1) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ZXGZ] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [zxhx] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sl] [numeric](5, 0) NOT NULL ,
	[hxrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cts] [numeric](5, 0) NOT NULL ,
	[bz] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sjcq] [numeric](5, 0) NOT NULL ,
	CONSTRAINT [PK_zxhx] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [zxsh] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[f1] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xx] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sl] [numeric](5, 0) NOT NULL ,
	[shcddm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shcdmc] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shdbh] [varchar] (15) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xshdz] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xshlxr] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xshdh] [varchar] (40) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xshcz] [varchar] (22) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shjc] [varchar] (16) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xshmc] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xyjshrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[xyjshsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sdrq] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sdsj] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[shyq] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (250) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sdckd] [varchar] (6) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_zxsh] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [zyxm] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[dm] [varchar] (2) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[mc] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[sm] [varchar] (80) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[nxz] [numeric](1, 0) NOT NULL ,
	CONSTRAINT [PK_ZYXM] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


CREATE TABLE [zzflb] (
	[nid] [int] IDENTITY (1, 1) NOT NULL ,
	[fid] [varchar] (12) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsdm] [varchar] (7) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[cgsmc] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkdm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkqc] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hxdm] [varchar] (4) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[hx] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[gkszg] [varchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[szgywm] [varchar] (25) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzf20] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzf40] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzg1dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzg1] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzg1ts] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzg2dm] [varchar] (5) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzg2] [varchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[zzg2ts] [varchar] (10) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[bz] [varchar] (100) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	CONSTRAINT [PK_ZZFLB] PRIMARY KEY  CLUSTERED 
	(
		[nid]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO


 CREATE  INDEX [BM] ON [_bm]([bm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [_bm]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [a_act]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [a_act]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [a_bd]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [a_bd]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [a_bdlx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [a_bdlx]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [a_ck]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [a_ck]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [jdxh] ON [a_ck]([jdxh]) ON [PRIMARY]
GO

 CREATE  INDEX [ckjd] ON [a_ck]([ckjd]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [a_dl]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [fdm] ON [a_dl]([fdm]) ON [PRIMARY]
GO

 CREATE  INDEX [wtid] ON [a_dl]([wtid]) ON [PRIMARY]
GO

 CREATE  INDEX [dlid] ON [a_dl]([dlid]) ON [PRIMARY]
GO

 CREATE  INDEX [ksrqsj] ON [a_dl]([ksrqsj]) ON [PRIMARY]
GO

 CREATE  INDEX [jsrqsj] ON [a_dl]([jsrqsj]) ON [PRIMARY]
GO

 CREATE  INDEX [zf] ON [a_dl]([zf]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [a_duty]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [a_f]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [a_f]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fldm] ON [a_f]([fldm]) ON [PRIMARY]
GO

 CREATE  INDEX [bddm] ON [a_f]([bddm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [a_fk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [sw_fid] ON [a_fk]([sw_fid]) ON [PRIMARY]
GO

 CREATE  INDEX [swh] ON [a_fk]([swh]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [a_fkmx]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [a_flx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [a_flx]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [a_fp]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [swh] ON [a_fp]([swh]) ON [PRIMARY]
GO

 CREATE  INDEX [ysfph] ON [a_fp]([ysfph]) ON [PRIMARY]
GO

 CREATE  INDEX [expid] ON [a_fp]([expid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [a_gd]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [sw_fid] ON [a_gd]([sw_fid]) ON [PRIMARY]
GO

 CREATE  INDEX [swh] ON [a_gd]([swh]) ON [PRIMARY]
GO

 CREATE  INDEX [swsf_fid] ON [a_gd]([swsf_fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [a_jd]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [a_jd]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [xh] ON [a_jd]([xh]) ON [PRIMARY]
GO

 CREATE  INDEX [jddm] ON [a_jd]([jddm]) ON [PRIMARY]
GO

 CREATE  INDEX [gdm] ON [a_jd]([gdm]) ON [PRIMARY]
GO

 CREATE  INDEX [dutydm] ON [a_jd]([dutydm]) ON [PRIMARY]
GO

 CREATE  INDEX [kzf] ON [a_jd]([kzf]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [a_jdlx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [a_jdlx]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [a_jdzt]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [a_jdzt]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [a_jjlx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [a_jjlx]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [a_op]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [a_op]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [a_opg]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [a_opg]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [a_oplx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [a_oplx]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [a_sj]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [a_sj]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [xh] ON [a_sj]([xh]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [a_sw]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [SWH] ON [a_sw]([swh]) ON [PRIMARY]
GO

 CREATE  INDEX [kv] ON [a_sw]([kv]) ON [PRIMARY]
GO

 CREATE  INDEX [wc] ON [a_sw]([wc]) ON [PRIMARY]
GO

 CREATE  INDEX [swztdm] ON [a_sw]([swztdm]) ON [PRIMARY]
GO

 CREATE  INDEX [lckp] ON [a_sw]([lckp]) ON [PRIMARY]
GO

 CREATE  INDEX [lckt] ON [a_sw]([lckt]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [a_swzt]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [a_swzt]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [agtcost]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [autosend]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [F1] ON [autosend]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [tstatus] ON [autosend]([tstatus]) ON [PRIMARY]
GO

 CREATE  INDEX [otime] ON [autosend]([otime]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [batchg]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [LX] ON [batchg]([lx]) ON [PRIMARY]
GO

 CREATE  INDEX [xh] ON [batchg]([xh]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [bbhead]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [BBMC] ON [bbhead]([bbmc]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [bblx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [BBLX] ON [bblx]([bblx]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [bd]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [fm2] ON [bd]([fm2]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [bgd]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [F1] ON [bgd]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [hgbh] ON [bgd]([hgbh]) ON [PRIMARY]
GO


 CREATE  INDEX [F1] ON [bggd]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [bggd]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [bggs]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [bggs]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [bggs]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [bgjgtj]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [bgjgtj]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [bgjldw]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [bgjldw]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [bgsp]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [spdm] ON [bgsp]([spdm]) ON [PRIMARY]
GO

 CREATE  INDEX [spmc] ON [bgsp]([spmc]) ON [PRIMARY]
GO


 CREATE  INDEX [dm] ON [bjlx]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [bjlx]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [bsdy]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [xh] ON [bsdy]([xh]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [bsdyw]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [bsdyw]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [fhv] ON [bsdyw]([fhv]) ON [PRIMARY]
GO

 CREATE  INDEX [fydm] ON [bsdyw]([fydm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [bwlog]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [rq] ON [bwlog]([rq]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [bwlx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [bwlx]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [bxfl]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [bxfl]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [bxfs]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [bxfs]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cbdtk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [F32] ON [cbdtk]([f32]) ON [PRIMARY]
GO

 CREATE  INDEX [CMDM] ON [cbdtk]([cmdm]) ON [PRIMARY]
GO

 CREATE  INDEX [F29] ON [cbdtk]([f29]) ON [PRIMARY]
GO

 CREATE  INDEX [ZWM] ON [cbdtk]([zwm]) ON [PRIMARY]
GO

 CREATE  INDEX [cgsdm] ON [cbdtk]([cgsdm]) ON [PRIMARY]
GO

 CREATE  INDEX [HXDM] ON [cbdtk]([hxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [GG] ON [cbdtk]([gg]) ON [PRIMARY]
GO

 CREATE  INDEX [dcdm] ON [cbdtk]([dcdm]) ON [PRIMARY]
GO

 CREATE  INDEX [pod] ON [cbdtk]([pod]) ON [PRIMARY]
GO

 CREATE  INDEX [ei] ON [cbdtk]([ei]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cbhc]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f29] ON [cbhc]([f29]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cbhs]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [cbhs]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cbst]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [cbst]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [f32] ON [cbst]([f32]) ON [PRIMARY]
GO

 CREATE  INDEX [eta] ON [cbst]([eta]) ON [PRIMARY]
GO

 CREATE  INDEX [f11dm] ON [cbst]([f11dm]) ON [PRIMARY]
GO

 CREATE  INDEX [mtdm] ON [cbst]([mtdm]) ON [PRIMARY]
GO

 CREATE  INDEX [jgr] ON [cbst]([jgr]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cbsy]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [F32] ON [cbsy]([f32]) ON [PRIMARY]
GO

 CREATE  INDEX [CMDM] ON [cbsy]([cmdm]) ON [PRIMARY]
GO

 CREATE  INDEX [F29] ON [cbsy]([f29]) ON [PRIMARY]
GO

 CREATE  INDEX [ZWM] ON [cbsy]([zwm]) ON [PRIMARY]
GO

 CREATE  INDEX [cgsdm] ON [cbsy]([cgsdm]) ON [PRIMARY]
GO

 CREATE  INDEX [HXDM] ON [cbsy]([hxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [GG] ON [cbsy]([gg]) ON [PRIMARY]
GO

 CREATE  INDEX [dcdm] ON [cbsy]([dcdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ccbc]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [ccbc]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cccw]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [cccw]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cccx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [cccx]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ccdw]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [ccdw]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ccfjk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [ccfjk]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [f3] ON [ccfjk]([f3]) ON [PRIMARY]
GO

 CREATE  INDEX [cjr] ON [ccfjk]([cjr]) ON [PRIMARY]
GO

 CREATE  INDEX [klx] ON [ccfjk]([klx]) ON [PRIMARY]
GO

 CREATE  INDEX [xzbj] ON [ccfjk]([xzbj]) ON [PRIMARY]
GO

 CREATE  INDEX [tj] ON [ccfjk]([tj]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ccfp]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cchw]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [cchw]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [F3] ON [cchw]([f3]) ON [PRIMARY]
GO

 CREATE  INDEX [jcfid] ON [cchw]([jcfid]) ON [PRIMARY]
GO

 CREATE  INDEX [qr] ON [cchw]([qr]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ccjs]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [ccjs]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ccjx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [ccjx]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ccsend]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [ccsend]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [f3] ON [ccsend]([f3]) ON [PRIMARY]
GO

 CREATE  INDEX [tj] ON [ccsend]([tj]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ccsendmx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [ccsendmx]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [ccsendmx]([f1]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cctj]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [cctj]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cctq]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [cctq]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cctqyb]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [rq] ON [cctqyb]([rq]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [cctqyb]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ccyw]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [xh] ON [ccyw]([xh]) ON [PRIMARY]
GO

 CREATE  INDEX [lx] ON [ccyw]([lx]) ON [PRIMARY]
GO

 CREATE  INDEX [xz] ON [ccyw]([xz]) ON [PRIMARY]
GO

 CREATE  INDEX [ty] ON [ccyw]([ty]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ccywlx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [ccywlx]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cczj]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [cczj]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cddmk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [cddmk]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cddmlx]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [chgitem]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [bm] ON [chgitem]([bm]) ON [PRIMARY]
GO

 CREATE  INDEX [zdm] ON [chgitem]([zdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [chgrec]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [khbh] ON [chgrec]([khbh]) ON [PRIMARY]
GO

 CREATE  INDEX [F1] ON [chgrec]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [f32] ON [chgrec]([f32]) ON [PRIMARY]
GO

 CREATE  INDEX [OP] ON [chgrec]([op]) ON [PRIMARY]
GO

 CREATE  INDEX [OPG] ON [chgrec]([opg]) ON [PRIMARY]
GO

 CREATE  INDEX [slr] ON [chgrec]([slr]) ON [PRIMARY]
GO

 CREATE  INDEX [opa] ON [chgrec]([opa]) ON [PRIMARY]
GO

 CREATE  INDEX [chgpc] ON [chgrec]([chgpc]) ON [PRIMARY]
GO

 CREATE  INDEX [tj] ON [chgrec]([tj]) ON [PRIMARY]
GO

 CREATE  INDEX [sl] ON [chgrec]([sl]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ckdm]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [ckdm] ON [ckdm]([ckdm]) ON [PRIMARY]
GO


 CREATE  INDEX [FM1] ON [cksyk]([fm1]) ON [PRIMARY]
GO

 CREATE  INDEX [F1] ON [cksyk]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [F29] ON [cksyk]([f29]) ON [PRIMARY]
GO

 CREATE  INDEX [F31] ON [cksyk]([f31]) ON [PRIMARY]
GO

 CREATE  INDEX [f11dm] ON [cksyk]([f11dm]) ON [PRIMARY]
GO

 CREATE  INDEX [f9] ON [cksyk]([f9]) ON [PRIMARY]
GO

 CREATE  INDEX [wtf2dm] ON [cksyk]([wtf2dm]) ON [PRIMARY]
GO

 CREATE  INDEX [ZZYH] ON [cksyk]([zzyh]) ON [PRIMARY]
GO

 CREATE  INDEX [YWDM] ON [cksyk]([ywdm]) ON [PRIMARY]
GO

 CREATE  INDEX [YW] ON [cksyk]([yw]) ON [PRIMARY]
GO

 CREATE  INDEX [agent] ON [cksyk]([agent]) ON [PRIMARY]
GO

 CREATE  INDEX [wtdcdm] ON [cksyk]([wtdcdm]) ON [PRIMARY]
GO

 CREATE  INDEX [pc1] ON [cksyk]([pc1]) ON [PRIMARY]
GO

 CREATE  INDEX [pc2] ON [cksyk]([pc2]) ON [PRIMARY]
GO

 CREATE  INDEX [dl1] ON [cksyk]([dl1]) ON [PRIMARY]
GO

 CREATE  INDEX [f32] ON [cksyk]([f32]) ON [PRIMARY]
GO

 CREATE  INDEX [tgbz] ON [cksyk]([tgbz]) ON [PRIMARY]
GO

 CREATE  INDEX [swqr] ON [cksyk]([swqr]) ON [PRIMARY]
GO

 CREATE  INDEX [ywqr] ON [cksyk]([ywqr]) ON [PRIMARY]
GO

 CREATE  INDEX [cgsdm] ON [cksyk]([cgsdm]) ON [PRIMARY]
GO

 CREATE  INDEX [dm2] ON [cksyk]([dm2]) ON [PRIMARY]
GO

 CREATE  INDEX [dm3] ON [cksyk]([dm3]) ON [PRIMARY]
GO

 CREATE  INDEX [cbhsbj] ON [cksyk]([cbhsbj]) ON [PRIMARY]
GO

 CREATE  INDEX [GLR] ON [cksyk]([glr]) ON [PRIMARY]
GO

 CREATE  INDEX [hsbj] ON [cksyk]([hsbj]) ON [PRIMARY]
GO

 CREATE  INDEX [F31H] ON [cksyk]([f31h]) ON [PRIMARY]
GO

 CREATE  INDEX [KHBH] ON [cksyk]([khbh]) ON [PRIMARY]
GO

 CREATE  INDEX [fkdm] ON [cksyk]([fkdm]) ON [PRIMARY]
GO

 CREATE  INDEX [pc4] ON [cksyk]([pc4]) ON [PRIMARY]
GO

 CREATE  INDEX [jz1] ON [cksyk]([jz1]) ON [PRIMARY]
GO

 CREATE  INDEX [jz2] ON [cksyk]([jz2]) ON [PRIMARY]
GO

 CREATE  INDEX [jz3] ON [cksyk]([jz3]) ON [PRIMARY]
GO

 CREATE  INDEX [jz4] ON [cksyk]([jz4]) ON [PRIMARY]
GO

 CREATE  INDEX [jz5] ON [cksyk]([jz5]) ON [PRIMARY]
GO

 CREATE  INDEX [jz6] ON [cksyk]([jz6]) ON [PRIMARY]
GO

 CREATE  INDEX [jz7] ON [cksyk]([jz7]) ON [PRIMARY]
GO

 CREATE  INDEX [jz8] ON [cksyk]([jz8]) ON [PRIMARY]
GO

 CREATE  INDEX [jz9] ON [cksyk]([jz9]) ON [PRIMARY]
GO

 CREATE  INDEX [wtf1dm] ON [cksyk]([wtf1dm]) ON [PRIMARY]
GO

 CREATE  INDEX [wtf1] ON [cksyk]([wtf1]) ON [PRIMARY]
GO

 CREATE  INDEX [dcbh] ON [cksyk]([dcbh]) ON [PRIMARY]
GO

 CREATE  INDEX [tdlx] ON [cksyk]([tdlx]) ON [PRIMARY]
GO

 CREATE  INDEX [xz] ON [cksyk]([xz]) ON [PRIMARY]
GO

 CREATE  INDEX [HYLX] ON [cksyk]([hylx]) ON [PRIMARY]
GO

 CREATE  INDEX [F33] ON [cksyk]([f33]) ON [PRIMARY]
GO

 CREATE  INDEX [HDRQ] ON [cksyk]([hdrq]) ON [PRIMARY]
GO

 CREATE  INDEX [lxdm] ON [cksyk]([lxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [BM] ON [cksyk]([bm]) ON [PRIMARY]
GO

 CREATE  INDEX [OP] ON [cksyk]([op]) ON [PRIMARY]
GO

 CREATE  INDEX [OPG] ON [cksyk]([opg]) ON [PRIMARY]
GO

 CREATE  INDEX [ft40xy] ON [cksyk]([ft40xy]) ON [PRIMARY]
GO

 CREATE  INDEX [YWDMA] ON [cksyk]([ywdma]) ON [PRIMARY]
GO

 CREATE  INDEX [fm2] ON [cksyk]([fm2]) ON [PRIMARY]
GO

 CREATE  INDEX [bk] ON [cksyk]([bk]) ON [PRIMARY]
GO

 CREATE  INDEX [bkg] ON [cksyk]([bkg]) ON [PRIMARY]
GO

 CREATE  INDEX [glrid] ON [cksyk]([glrid]) ON [PRIMARY]
GO

 CREATE  INDEX [glra] ON [cksyk]([glra]) ON [PRIMARY]
GO

 CREATE  INDEX [opid] ON [cksyk]([opid]) ON [PRIMARY]
GO

 CREATE  INDEX [opa] ON [cksyk]([opa]) ON [PRIMARY]
GO

 CREATE  INDEX [bkid] ON [cksyk]([bkid]) ON [PRIMARY]
GO

 CREATE  INDEX [bka] ON [cksyk]([bka]) ON [PRIMARY]
GO

 CREATE  INDEX [bmid] ON [cksyk]([bmid]) ON [PRIMARY]
GO

 CREATE  INDEX [glraid] ON [cksyk]([glraid]) ON [PRIMARY]
GO

 CREATE  INDEX [opgid] ON [cksyk]([opgid]) ON [PRIMARY]
GO

 CREATE  INDEX [opaid] ON [cksyk]([opaid]) ON [PRIMARY]
GO

 CREATE  INDEX [bkgid] ON [cksyk]([bkgid]) ON [PRIMARY]
GO

 CREATE  INDEX [bkaid] ON [cksyk]([bkaid]) ON [PRIMARY]
GO

 CREATE  INDEX [doc] ON [cksyk]([doc]) ON [PRIMARY]
GO

 CREATE  INDEX [docg] ON [cksyk]([docg]) ON [PRIMARY]
GO

 CREATE  INDEX [doca] ON [cksyk]([doca]) ON [PRIMARY]
GO

 CREATE  INDEX [docid] ON [cksyk]([docid]) ON [PRIMARY]
GO

 CREATE  INDEX [docgid] ON [cksyk]([docgid]) ON [PRIMARY]
GO

 CREATE  INDEX [docaid] ON [cksyk]([docaid]) ON [PRIMARY]
GO

 CREATE  INDEX [ctj] ON [cksyk]([ctj]) ON [PRIMARY]
GO

 CREATE  INDEX [cl] ON [cksyk]([cl]) ON [PRIMARY]
GO

 CREATE  INDEX [ctj1] ON [cksyk]([ctj1]) ON [PRIMARY]
GO

 CREATE  INDEX [cl1] ON [cksyk]([cl1]) ON [PRIMARY]
GO

 CREATE  INDEX [atd] ON [cksyk]([atd]) ON [PRIMARY]
GO

 CREATE  INDEX [ywlxdm] ON [cksyk]([ywlxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [cklxdm] ON [cksyk]([cklxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [jmyw] ON [cksyk]([jmyw]) ON [PRIMARY]
GO

 CREATE  INDEX [dcqr] ON [cksyk]([dcqr]) ON [PRIMARY]
GO

 CREATE  INDEX [cs] ON [cksyk]([cs]) ON [PRIMARY]
GO

 CREATE  INDEX [csid] ON [cksyk]([csid]) ON [PRIMARY]
GO

 CREATE  INDEX [csg] ON [cksyk]([csg]) ON [PRIMARY]
GO

 CREATE  INDEX [csgid] ON [cksyk]([csgid]) ON [PRIMARY]
GO

 CREATE  INDEX [csa] ON [cksyk]([csa]) ON [PRIMARY]
GO

 CREATE  INDEX [csaid] ON [cksyk]([csaid]) ON [PRIMARY]
GO

 CREATE  INDEX [rqfldm] ON [cksyk]([rqfldm]) ON [PRIMARY]
GO

 CREATE  INDEX [fhdh] ON [cksyk]([fhdh]) ON [PRIMARY]
GO

 CREATE  INDEX [xsdh] ON [cksyk]([xsdh]) ON [PRIMARY]
GO

 CREATE  INDEX [cgdh] ON [cksyk]([cgdh]) ON [PRIMARY]
GO

 CREATE  INDEX [cgsdldm] ON [cksyk]([cgsdldm]) ON [PRIMARY]
GO

 CREATE  INDEX [bxgsdm] ON [cksyk]([bxgsdm]) ON [PRIMARY]
GO

 CREATE  INDEX [bxdldm] ON [cksyk]([bxdldm]) ON [PRIMARY]
GO

 CREATE  INDEX [jzdm] ON [cksyk]([jzdm]) ON [PRIMARY]
GO

 CREATE  INDEX [atdsj] ON [cksyk]([atdsj]) ON [PRIMARY]
GO

 CREATE  INDEX [czbj] ON [cksyk]([czbj]) ON [PRIMARY]
GO

 CREATE  INDEX [dcrq] ON [cksyk]([dcrq]) ON [PRIMARY]
GO

 CREATE  INDEX [ywrq] ON [cksyk]([ywrq]) ON [PRIMARY]
GO

 CREATE  INDEX [swrq] ON [cksyk]([swrq]) ON [PRIMARY]
GO

 CREATE  INDEX [hsrq] ON [cksyk]([hsrq]) ON [PRIMARY]
GO

 CREATE  INDEX [lckp] ON [cksyk]([lckp]) ON [PRIMARY]
GO

 CREATE  INDEX [lckt] ON [cksyk]([lckt]) ON [PRIMARY]
GO

 CREATE  INDEX [cwdldm] ON [cksyk]([cwdldm]) ON [PRIMARY]
GO

 CREATE  INDEX [opm] ON [cksyk]([opm]) ON [PRIMARY]
GO

 CREATE  INDEX [rkdz] ON [cksyk]([rkdz]) ON [PRIMARY]
GO

 CREATE  INDEX [ckdz] ON [cksyk]([ckdz]) ON [PRIMARY]
GO

 CREATE  INDEX [jgbj] ON [cksyk]([jgbj]) ON [PRIMARY]
GO

 CREATE  INDEX [bksh1] ON [cksyk]([bksh1]) ON [PRIMARY]
GO

 CREATE  INDEX [bksh2] ON [cksyk]([bksh2]) ON [PRIMARY]
GO

 CREATE  INDEX [bksh3] ON [cksyk]([bksh3]) ON [PRIMARY]
GO

 CREATE  INDEX [bfsh1] ON [cksyk]([bfsh1]) ON [PRIMARY]
GO

 CREATE  INDEX [bfsh2] ON [cksyk]([bfsh2]) ON [PRIMARY]
GO

 CREATE  INDEX [bfsh3] ON [cksyk]([bfsh3]) ON [PRIMARY]
GO

 CREATE  INDEX [zdbj] ON [cksyk]([zdbj]) ON [PRIMARY]
GO

 CREATE  INDEX [f32sj] ON [cksyk]([f32sj]) ON [PRIMARY]
GO

 CREATE  INDEX [pol] ON [cksyk]([pol]) ON [PRIMARY]
GO

 CREATE  INDEX [ywqr2] ON [cksyk]([ywqr2]) ON [PRIMARY]
GO

 CREATE  INDEX [zzyhid] ON [cksyk]([zzyhid]) ON [PRIMARY]
GO

 CREATE  INDEX [zzyhz] ON [cksyk]([zzyhz]) ON [PRIMARY]
GO

 CREATE  INDEX [zzyhzid] ON [cksyk]([zzyhzid]) ON [PRIMARY]
GO

 CREATE  INDEX [zzyha] ON [cksyk]([zzyha]) ON [PRIMARY]
GO

 CREATE  INDEX [zzyhaid] ON [cksyk]([zzyhaid]) ON [PRIMARY]
GO

 CREATE  INDEX [f34] ON [cksyk]([f34]) ON [PRIMARY]
GO

 CREATE  INDEX [gdtj] ON [cksyk]([gdtj]) ON [PRIMARY]
GO

 CREATE  INDEX [gdsl] ON [cksyk]([gdsl]) ON [PRIMARY]
GO

 CREATE  INDEX [bfdm] ON [cksyk]([bfdm]) ON [PRIMARY]
GO


 CREATE  INDEX [FM1] ON [ckzxl]([fm1]) ON [PRIMARY]
GO

 CREATE  INDEX [wtdcdm] ON [ckzxl]([wtdcdm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [ckzxl]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [clbb]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [clbb]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [ch] ON [clbb]([ch]) ON [PRIMARY]
GO

 CREATE  INDEX [jsyxm] ON [clbb]([jsyxm]) ON [PRIMARY]
GO

 CREATE  INDEX [fsrq] ON [clbb]([fsrq]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [clbx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [clbx]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [ch] ON [clbx]([ch]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [clbxmx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [clbxmx]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [ch] ON [clbxmx]([ch]) ON [PRIMARY]
GO

 CREATE  INDEX [cbxzdm] ON [clbxmx]([cbxzdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [clby]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [clby]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [ch] ON [clby]([ch]) ON [PRIMARY]
GO

 CREATE  INDEX [gcrq] ON [clby]([gcrq]) ON [PRIMARY]
GO

 CREATE  INDEX [jsyxm] ON [clby]([jsyxm]) ON [PRIMARY]
GO

 CREATE  INDEX [bydwdm] ON [clby]([bydwdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [clbymx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [clbymx]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [ch] ON [clbymx]([ch]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [clbymx]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cldmbx]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cldmby]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cldmcz]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cldmgc]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cldmjf]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cldmlx]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cldmrl]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cldmzt]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cljf]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [cljf]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [ch] ON [cljf]([ch]) ON [PRIMARY]
GO

 CREATE  INDEX [jfqq] ON [cljf]([jfqq]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cljsy]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [cljsy]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cllt]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [cllt]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [ch] ON [cllt]([ch]) ON [PRIMARY]
GO

 CREATE  INDEX [jsyxm] ON [cllt]([jsyxm]) ON [PRIMARY]
GO

 CREATE  INDEX [ltbh] ON [cllt]([ltbh]) ON [PRIMARY]
GO

 CREATE  INDEX [jhbfsj] ON [cllt]([jhbfsj]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [clxx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [clxx]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [lckp] ON [clxx]([lckp]) ON [PRIMARY]
GO

 CREATE  INDEX [lckt] ON [clxx]([lckt]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cmdmk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [DM] ON [cmdmk]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [MC] ON [cmdmk]([mc]) ON [PRIMARY]
GO

 CREATE  INDEX [EMC] ON [cmdmk]([emc]) ON [PRIMARY]
GO

 CREATE  INDEX [cgsdm] ON [cmdmk]([cgsdm]) ON [PRIMARY]
GO

 CREATE  INDEX [qy] ON [cmdmk]([qy]) ON [PRIMARY]
GO

 CREATE  INDEX [unno] ON [cmdmk]([unno]) ON [PRIMARY]
GO

 CREATE  INDEX [bc] ON [cmdmk]([bc]) ON [PRIMARY]
GO



 CREATE  INDEX [fid] ON [cqfl]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [cqfl]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [crfl]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [crfl]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [crxm]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [fldm] ON [crxm]([fldm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [csdm]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [csdm]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [sfdm] ON [csdm]([sfdm]) ON [PRIMARY]
GO

 CREATE  INDEX [csdm] ON [csdm]([csdm]) ON [PRIMARY]
GO


 CREATE  INDEX [name] ON [csk]([name]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [csk]([sm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [csk]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [csl]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [name] ON [csl]([name]) ON [PRIMARY]
GO

 CREATE  INDEX [sm] ON [csl]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cslx]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [f1] ON [csv]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [cgsdm] ON [csv]([cgsdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cu]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [xh] ON [cu]([xh]) ON [PRIMARY]
GO

 CREATE  INDEX [lx] ON [cu]([lx]) ON [PRIMARY]
GO

 CREATE  INDEX [ty] ON [cu]([ty]) ON [PRIMARY]
GO

 CREATE  INDEX [fl] ON [cu]([fl]) ON [PRIMARY]
GO


 CREATE  INDEX [jy] ON [culink]([jy]) ON [PRIMARY]
GO

 CREATE  INDEX [lx] ON [culink]([lx]) ON [PRIMARY]
GO

 CREATE  INDEX [mk] ON [culink]([mk]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [culink]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cupg]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [xh] ON [cupg]([xh]) ON [PRIMARY]
GO

 CREATE  INDEX [lx] ON [cupg]([lx]) ON [PRIMARY]
GO

 CREATE  INDEX [ty] ON [cupg]([ty]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [custep]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [lx] ON [custep]([lx]) ON [PRIMARY]
GO

 CREATE  INDEX [stp] ON [custep]([stp]) ON [PRIMARY]
GO

 CREATE  INDEX [xh] ON [custep]([xh]) ON [PRIMARY]
GO

 CREATE  INDEX [xz] ON [custep]([xz]) ON [PRIMARY]
GO

 CREATE  INDEX [ty] ON [custep]([ty]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [custp]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [lx] ON [custp]([lx]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cuyy]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [xh] ON [cuyy]([xh]) ON [PRIMARY]
GO

 CREATE  INDEX [lx] ON [cuyy]([lx]) ON [PRIMARY]
GO

 CREATE  INDEX [ty] ON [cuyy]([ty]) ON [PRIMARY]
GO

 CREATE  INDEX [qid] ON [cuyy]([qid]) ON [PRIMARY]
GO

 CREATE  INDEX [fz] ON [cuyy]([fz]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cuyyg]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [xz] ON [cuyyg]([xz]) ON [PRIMARY]
GO

 CREATE  INDEX [zz] ON [cuyyg]([zz]) ON [PRIMARY]
GO

 CREATE  INDEX [lx] ON [cuyyg]([lx]) ON [PRIMARY]
GO

 CREATE  INDEX [xh] ON [cuyyg]([xh]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cwbbii]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [xh] ON [cwbbii]([xh]) ON [PRIMARY]
GO

 CREATE  INDEX [lx] ON [cwbbii]([lx]) ON [PRIMARY]
GO

 CREATE  INDEX [ty] ON [cwbbii]([ty]) ON [PRIMARY]
GO

 CREATE  INDEX [qid] ON [cwbbii]([qid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cwywbb]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [xh] ON [cwywbb]([xh]) ON [PRIMARY]
GO

 CREATE  INDEX [lx] ON [cwywbb]([lx]) ON [PRIMARY]
GO

 CREATE  INDEX [ty] ON [cwywbb]([ty]) ON [PRIMARY]
GO

 CREATE  INDEX [qid] ON [cwywbb]([qid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cwywbbb]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [xh] ON [cwywbbb]([xh]) ON [PRIMARY]
GO

 CREATE  INDEX [lx] ON [cwywbbb]([lx]) ON [PRIMARY]
GO

 CREATE  INDEX [ty] ON [cwywbbb]([ty]) ON [PRIMARY]
GO

 CREATE  INDEX [qid] ON [cwywbbb]([qid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [cxdbf]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [XH] ON [cxdbf]([xh]) ON [PRIMARY]
GO

 CREATE  INDEX [lx] ON [cxdbf]([lx]) ON [PRIMARY]
GO

 CREATE  INDEX [ty] ON [cxdbf]([ty]) ON [PRIMARY]
GO

 CREATE  INDEX [qid] ON [cxdbf]([qid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f9] ON [db1]([f9]) ON [PRIMARY]
GO

 CREATE  INDEX [f8] ON [db1]([f8]) ON [PRIMARY]
GO

 CREATE  INDEX [KHQC] ON [db1]([khqc]) ON [PRIMARY]
GO

 CREATE  INDEX [GLR] ON [db1]([glr]) ON [PRIMARY]
GO

 CREATE  INDEX [OPG] ON [db1]([opg]) ON [PRIMARY]
GO

 CREATE  INDEX [LXR] ON [db1]([lxr]) ON [PRIMARY]
GO

 CREATE  INDEX [DH] ON [db1]([dh]) ON [PRIMARY]
GO

 CREATE  INDEX [TWCZ] ON [db1]([twcz]) ON [PRIMARY]
GO

 CREATE  INDEX [KHDZ] ON [db1]([khdz]) ON [PRIMARY]
GO

 CREATE  INDEX [DH1] ON [db1]([dh1]) ON [PRIMARY]
GO

 CREATE  INDEX [TWCZ1] ON [db1]([twcz1]) ON [PRIMARY]
GO

 CREATE  INDEX [flbj] ON [db1]([flbj]) ON [PRIMARY]
GO

 CREATE  INDEX [yjbj] ON [db1]([yjbj]) ON [PRIMARY]
GO

 CREATE  INDEX [qcbj] ON [db1]([qcbj]) ON [PRIMARY]
GO

 CREATE  INDEX [fkdm] ON [db1]([fkdm]) ON [PRIMARY]
GO

 CREATE  INDEX [lxdm] ON [db1]([lxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [lx2dm] ON [db1]([lx2dm]) ON [PRIMARY]
GO

 CREATE  INDEX [dybj] ON [db1]([dybj]) ON [PRIMARY]
GO

 CREATE  INDEX [cpbj] ON [db1]([cpbj]) ON [PRIMARY]
GO

 CREATE  INDEX [HTH] ON [db1]([hth]) ON [PRIMARY]
GO

 CREATE  INDEX [dm2] ON [db1]([dm2]) ON [PRIMARY]
GO

 CREATE  INDEX [dm3] ON [db1]([dm3]) ON [PRIMARY]
GO

 CREATE  INDEX [KL] ON [db1]([kl]) ON [PRIMARY]
GO

 CREATE  INDEX [JL] ON [db1]([jl]) ON [PRIMARY]
GO

 CREATE  INDEX [bkg] ON [db1]([bkg]) ON [PRIMARY]
GO

 CREATE  INDEX [bk] ON [db1]([bk]) ON [PRIMARY]
GO

 CREATE  INDEX [op] ON [db1]([op]) ON [PRIMARY]
GO

 CREATE  INDEX [bm] ON [db1]([bm]) ON [PRIMARY]
GO

 CREATE  INDEX [glrid] ON [db1]([glrid]) ON [PRIMARY]
GO

 CREATE  INDEX [glra] ON [db1]([glra]) ON [PRIMARY]
GO

 CREATE  INDEX [opid] ON [db1]([opid]) ON [PRIMARY]
GO

 CREATE  INDEX [opa] ON [db1]([opa]) ON [PRIMARY]
GO

 CREATE  INDEX [bkid] ON [db1]([bkid]) ON [PRIMARY]
GO

 CREATE  INDEX [bka] ON [db1]([bka]) ON [PRIMARY]
GO

 CREATE  INDEX [bmid] ON [db1]([bmid]) ON [PRIMARY]
GO

 CREATE  INDEX [glraid] ON [db1]([glraid]) ON [PRIMARY]
GO

 CREATE  INDEX [opgid] ON [db1]([opgid]) ON [PRIMARY]
GO

 CREATE  INDEX [opaid] ON [db1]([opaid]) ON [PRIMARY]
GO

 CREATE  INDEX [bkgid] ON [db1]([bkgid]) ON [PRIMARY]
GO

 CREATE  INDEX [bkaid] ON [db1]([bkaid]) ON [PRIMARY]
GO

 CREATE  INDEX [gb] ON [db1]([gb]) ON [PRIMARY]
GO

 CREATE  INDEX [cs] ON [db1]([cs]) ON [PRIMARY]
GO

 CREATE  INDEX [csid] ON [db1]([csid]) ON [PRIMARY]
GO

 CREATE  INDEX [csg] ON [db1]([csg]) ON [PRIMARY]
GO

 CREATE  INDEX [csgid] ON [db1]([csgid]) ON [PRIMARY]
GO

 CREATE  INDEX [csa] ON [db1]([csa]) ON [PRIMARY]
GO

 CREATE  INDEX [csaid] ON [db1]([csaid]) ON [PRIMARY]
GO

 CREATE  INDEX [SOPNO] ON [db1]([sopno]) ON [PRIMARY]
GO

 CREATE  INDEX [doc] ON [db1]([doc]) ON [PRIMARY]
GO

 CREATE  INDEX [docid] ON [db1]([docid]) ON [PRIMARY]
GO

 CREATE  INDEX [docg] ON [db1]([docg]) ON [PRIMARY]
GO

 CREATE  INDEX [docgid] ON [db1]([docgid]) ON [PRIMARY]
GO

 CREATE  INDEX [doca] ON [db1]([doca]) ON [PRIMARY]
GO

 CREATE  INDEX [docaid] ON [db1]([docaid]) ON [PRIMARY]
GO

 CREATE  INDEX [fx] ON [db1]([fx]) ON [PRIMARY]
GO

 CREATE  INDEX [lckp] ON [db1]([lckp]) ON [PRIMARY]
GO

 CREATE  INDEX [lckt] ON [db1]([lckt]) ON [PRIMARY]
GO

 CREATE  INDEX [sja] ON [db1]([sja]) ON [PRIMARY]
GO

 CREATE  INDEX [sjrid] ON [db1]([sjrid]) ON [PRIMARY]
GO

 CREATE  INDEX [sjzid] ON [db1]([sjzid]) ON [PRIMARY]
GO

 CREATE  INDEX [sjaid] ON [db1]([sjaid]) ON [PRIMARY]
GO

 CREATE  INDEX [NBDW] ON [db1]([nbdw]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1_kjqy]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f9] ON [db1_kjqy]([f9]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1c2]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1c2]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [rq] ON [db1c2]([rq]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1cc]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1cc]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [xh] ON [db1cc]([xh]) ON [PRIMARY]
GO

 CREATE  INDEX [xzrq] ON [db1cc]([xzrq]) ON [PRIMARY]
GO

 CREATE  INDEX [qr] ON [db1cc]([qr]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1cr]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1cr]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [rq] ON [db1cr]([rq]) ON [PRIMARY]
GO

 CREATE  INDEX [fldm] ON [db1cr]([fldm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1cs]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1cs]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1cx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1cx]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [rq] ON [db1cx]([rq]) ON [PRIMARY]
GO

 CREATE  INDEX [fldm] ON [db1cx]([fldm]) ON [PRIMARY]
GO

 CREATE  INDEX [xmdm] ON [db1cx]([xmdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1d1]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1d1]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [zhdbh] ON [db1d1]([zhdbh]) ON [PRIMARY]
GO

 CREATE  INDEX [f10dm] ON [db1d1]([f10dm]) ON [PRIMARY]
GO

 CREATE  INDEX [zxdm] ON [db1d1]([zxdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1d2]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1d2]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [f10dm] ON [db1d2]([f10dm]) ON [PRIMARY]
GO

 CREATE  INDEX [f11dm] ON [db1d2]([f11dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1d3]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1d3]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [f11dm] ON [db1d3]([f11dm]) ON [PRIMARY]
GO

 CREATE  INDEX [zxdm2] ON [db1d3]([zxdm2]) ON [PRIMARY]
GO

 CREATE  INDEX [shdbh] ON [db1d3]([shdbh]) ON [PRIMARY]
GO


 CREATE  INDEX [dm] ON [db1dqlx]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [db1dqlx]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1dz]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1dz]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1fj]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1fj]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [f11dm] ON [db1fj]([f11dm]) ON [PRIMARY]
GO

 CREATE  INDEX [dxdm] ON [db1fj]([dxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [xxdm] ON [db1fj]([xxdm]) ON [PRIMARY]
GO


 CREATE  INDEX [dm] ON [db1fwlx]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [db1fwlx]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1gd]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1gd]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1glr]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1glr]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [dm] ON [db1gmlx]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [db1gmlx]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1hx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1hx]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1hyf]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1hyf]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [xxbz] ON [db1hyf]([xxbz]) ON [PRIMARY]
GO

 CREATE  INDEX [sfdm] ON [db1hyf]([sfdm]) ON [PRIMARY]
GO

 CREATE  INDEX [f10dm] ON [db1hyf]([f10dm]) ON [PRIMARY]
GO

 CREATE  INDEX [f11dm] ON [db1hyf]([f11dm]) ON [PRIMARY]
GO

 CREATE  INDEX [cgsdm] ON [db1hyf]([cgsdm]) ON [PRIMARY]
GO

 CREATE  INDEX [sxrq] ON [db1hyf]([sxrq]) ON [PRIMARY]
GO

 CREATE  INDEX [xxdm] ON [db1hyf]([xxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [cqdm] ON [db1hyf]([cqdm]) ON [PRIMARY]
GO

 CREATE  INDEX [mdmt] ON [db1hyf]([mdmt]) ON [PRIMARY]
GO

 CREATE  INDEX [ty] ON [db1hyf]([ty]) ON [PRIMARY]
GO

 CREATE  INDEX [stp] ON [db1hyf]([stp]) ON [PRIMARY]
GO

 CREATE  INDEX [ffid] ON [db1hyf]([ffid]) ON [PRIMARY]
GO

 CREATE  INDEX [sfid] ON [db1hyf]([sfid]) ON [PRIMARY]
GO

 CREATE  INDEX [cshx] ON [db1hyf]([cshx]) ON [PRIMARY]
GO


 CREATE  INDEX [dm] ON [db1hylx]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [db1hylx]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1jjd]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1jjd]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1khzk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1khzk]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [f9] ON [db1khzk]([f9]) ON [PRIMARY]
GO

 CREATE  INDEX [lxdm] ON [db1khzk]([lxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [qydm] ON [db1khzk]([qydm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1kptt]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f9] ON [db1kptt]([f9]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1kptt]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [nsid] ON [db1kptt]([nsid]) ON [PRIMARY]
GO

 CREATE  INDEX [kptt] ON [db1kptt]([kptt]) ON [PRIMARY]
GO

 CREATE  INDEX [nszh] ON [db1kptt]([nszh]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1ld]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1ld]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [f10dm] ON [db1ld]([f10dm]) ON [PRIMARY]
GO

 CREATE  INDEX [f11dm] ON [db1ld]([f11dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1lxr]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f9] ON [db1lxr]([f9]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1lxr]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [FID] ON [db1lxr2]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [DB1FID] ON [db1lxr2]([db1fid]) ON [PRIMARY]
GO

 CREATE  INDEX [MK] ON [db1lxr2]([mk]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1lxrlx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [db1lxrlx]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1ly]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1ly]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [zhdbh] ON [db1ly]([zhdbh]) ON [PRIMARY]
GO

 CREATE  INDEX [shdbh] ON [db1ly]([shdbh]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1lyf]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1lyf]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [xxbz] ON [db1lyf]([xxbz]) ON [PRIMARY]
GO

 CREATE  INDEX [sfdm] ON [db1lyf]([sfdm]) ON [PRIMARY]
GO

 CREATE  INDEX [jcdm] ON [db1lyf]([jcdm]) ON [PRIMARY]
GO

 CREATE  INDEX [f10dm] ON [db1lyf]([f10dm]) ON [PRIMARY]
GO

 CREATE  INDEX [F11DM] ON [db1lyf]([f11dm]) ON [PRIMARY]
GO

 CREATE  INDEX [cyrdm] ON [db1lyf]([cyrdm]) ON [PRIMARY]
GO

 CREATE  INDEX [sxrq] ON [db1lyf]([sxrq]) ON [PRIMARY]
GO

 CREATE  INDEX [xzrq] ON [db1lyf]([xzrq]) ON [PRIMARY]
GO

 CREATE  INDEX [qr] ON [db1lyf]([qr]) ON [PRIMARY]
GO


 CREATE  INDEX [dm] ON [db1lylx]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [db1lylx]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1pro]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1pro]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1sf]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1sf]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [f10dm] ON [db1sf]([f10dm]) ON [PRIMARY]
GO

 CREATE  INDEX [f11dm] ON [db1sf]([f11dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fydm] ON [db1sf]([fydm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1shdd]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1shdd]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [f9] ON [db1shdd]([f9]) ON [PRIMARY]
GO

 CREATE  INDEX [MDQYDM] ON [db1shdd]([mdqydm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1shyq]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1shyq]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1sj]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1sj]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [jgdm] ON [db1sj]([jgdm]) ON [PRIMARY]
GO

 CREATE  INDEX [qr] ON [db1sj]([qr]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1sl]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1sl]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [sl] ON [db1sl]([sl]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1svc]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f9] ON [db1svc]([f9]) ON [PRIMARY]
GO

 CREATE  INDEX [XMDM] ON [db1svc]([xmdm]) ON [PRIMARY]
GO

 CREATE  INDEX [bz] ON [db1svc]([bz]) ON [PRIMARY]
GO

 CREATE  INDEX [xxbz] ON [db1svc]([xxbz]) ON [PRIMARY]
GO

 CREATE  INDEX [mdqydm] ON [db1svc]([mdqydm]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1svc]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1uf]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [MDQYDM] ON [db1uf]([mdqydm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1xy]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1xy]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [f9] ON [db1xy]([f9]) ON [PRIMARY]
GO

 CREATE  INDEX [xyzz] ON [db1xy]([xyzz]) ON [PRIMARY]
GO

 CREATE  INDEX [zht] ON [db1xy]([zht]) ON [PRIMARY]
GO

 CREATE  INDEX [qr] ON [db1xy]([qr]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1xy2]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1xy2]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1xy3]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1xy3]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [dm] ON [db1xylx]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [db1xylx]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [dm] ON [db1xzlx]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [db1xzlx]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1yf]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1yf]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [f10dm] ON [db1yf]([f10dm]) ON [PRIMARY]
GO

 CREATE  INDEX [f11dm] ON [db1yf]([f11dm]) ON [PRIMARY]
GO

 CREATE  INDEX [SHDBH] ON [db1yf]([shdbh]) ON [PRIMARY]
GO

 CREATE  INDEX [f4031h] ON [db1yf]([f4031h]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1yfhz]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1yfhz]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [cgsdm] ON [db1yfhz]([cgsdm]) ON [PRIMARY]
GO

 CREATE  INDEX [hxdm] ON [db1yfhz]([hxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [sxrq] ON [db1yfhz]([sxrq]) ON [PRIMARY]
GO

 CREATE  INDEX [dxdm] ON [db1yfhz]([dxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [xxdm] ON [db1yfhz]([xxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [qr] ON [db1yfhz]([qr]) ON [PRIMARY]
GO

 CREATE  INDEX [sfdm] ON [db1yfhz]([sfdm]) ON [PRIMARY]
GO

 CREATE  INDEX [wjbj] ON [db1yfhz]([wjbj]) ON [PRIMARY]
GO

 CREATE  INDEX [f10dm] ON [db1yfhz]([f10dm]) ON [PRIMARY]
GO

 CREATE  INDEX [xxbz] ON [db1yfhz]([xxbz]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1yflf]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [csdm] ON [db1yflf]([csdm]) ON [PRIMARY]
GO

 CREATE  INDEX [qxdm] ON [db1yflf]([qxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [jsxh] ON [db1yflf]([jsxh]) ON [PRIMARY]
GO

 CREATE  INDEX [qscsdm] ON [db1yflf]([qscsdm]) ON [PRIMARY]
GO

 CREATE  INDEX [qsqxdm] ON [db1yflf]([qsqxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1yflf]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1yq]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1yq]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [dm] ON [db1ztlx]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [db1ztlx]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1zxd]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f9] ON [db1zxd]([f9]) ON [PRIMARY]
GO

 CREATE  INDEX [MDQYDM] ON [db1zxd]([mdqydm]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1zxd]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db1zxyq]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [db1zxyq]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [db2]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f9] ON [db2]([f9]) ON [PRIMARY]
GO

 CREATE  INDEX [f8] ON [db2]([f8]) ON [PRIMARY]
GO

 CREATE  INDEX [KHQC] ON [db2]([khqc]) ON [PRIMARY]
GO

 CREATE  INDEX [GLR] ON [db2]([glr]) ON [PRIMARY]
GO

 CREATE  INDEX [OPG] ON [db2]([opg]) ON [PRIMARY]
GO

 CREATE  INDEX [LXR] ON [db2]([lxr]) ON [PRIMARY]
GO

 CREATE  INDEX [DH] ON [db2]([dh]) ON [PRIMARY]
GO

 CREATE  INDEX [TWCZ] ON [db2]([twcz]) ON [PRIMARY]
GO

 CREATE  INDEX [KHDZ] ON [db2]([khdz]) ON [PRIMARY]
GO

 CREATE  INDEX [DH1] ON [db2]([dh1]) ON [PRIMARY]
GO

 CREATE  INDEX [TWCZ1] ON [db2]([twcz1]) ON [PRIMARY]
GO

 CREATE  INDEX [flbj] ON [db2]([flbj]) ON [PRIMARY]
GO

 CREATE  INDEX [yjbj] ON [db2]([yjbj]) ON [PRIMARY]
GO

 CREATE  INDEX [qcbj] ON [db2]([qcbj]) ON [PRIMARY]
GO

 CREATE  INDEX [fkdm] ON [db2]([fkdm]) ON [PRIMARY]
GO

 CREATE  INDEX [lxdm] ON [db2]([lxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [lx2dm] ON [db2]([lx2dm]) ON [PRIMARY]
GO

 CREATE  INDEX [dybj] ON [db2]([dybj]) ON [PRIMARY]
GO

 CREATE  INDEX [cpbj] ON [db2]([cpbj]) ON [PRIMARY]
GO

 CREATE  INDEX [HTH] ON [db2]([hth]) ON [PRIMARY]
GO

 CREATE  INDEX [dm2] ON [db2]([dm2]) ON [PRIMARY]
GO

 CREATE  INDEX [dm3] ON [db2]([dm3]) ON [PRIMARY]
GO

 CREATE  INDEX [KL] ON [db2]([kl]) ON [PRIMARY]
GO

 CREATE  INDEX [JL] ON [db2]([jl]) ON [PRIMARY]
GO

 CREATE  INDEX [sjr] ON [db2]([sjr]) ON [PRIMARY]
GO

 CREATE  INDEX [sjz] ON [db2]([sjz]) ON [PRIMARY]
GO

 CREATE  INDEX [bm] ON [db2]([bm]) ON [PRIMARY]
GO

 CREATE  INDEX [bkg] ON [db2]([bkg]) ON [PRIMARY]
GO

 CREATE  INDEX [bk] ON [db2]([bk]) ON [PRIMARY]
GO

 CREATE  INDEX [op] ON [db2]([op]) ON [PRIMARY]
GO

 CREATE  INDEX [glrid] ON [db2]([glrid]) ON [PRIMARY]
GO

 CREATE  INDEX [opid] ON [db2]([opid]) ON [PRIMARY]
GO

 CREATE  INDEX [bkid] ON [db2]([bkid]) ON [PRIMARY]
GO

 CREATE  INDEX [glra] ON [db2]([glra]) ON [PRIMARY]
GO

 CREATE  INDEX [opa] ON [db2]([opa]) ON [PRIMARY]
GO

 CREATE  INDEX [bka] ON [db2]([bka]) ON [PRIMARY]
GO

 CREATE  INDEX [sja] ON [db2]([sja]) ON [PRIMARY]
GO

 CREATE  INDEX [sjrid] ON [db2]([sjrid]) ON [PRIMARY]
GO

 CREATE  INDEX [sjzid] ON [db2]([sjzid]) ON [PRIMARY]
GO

 CREATE  INDEX [sjaid] ON [db2]([sjaid]) ON [PRIMARY]
GO

 CREATE  INDEX [bmid] ON [db2]([bmid]) ON [PRIMARY]
GO

 CREATE  INDEX [glraid] ON [db2]([glraid]) ON [PRIMARY]
GO

 CREATE  INDEX [opgid] ON [db2]([opgid]) ON [PRIMARY]
GO

 CREATE  INDEX [opaid] ON [db2]([opaid]) ON [PRIMARY]
GO

 CREATE  INDEX [bkgid] ON [db2]([bkgid]) ON [PRIMARY]
GO

 CREATE  INDEX [bkaid] ON [db2]([bkaid]) ON [PRIMARY]
GO

 CREATE  INDEX [cs] ON [db2]([cs]) ON [PRIMARY]
GO

 CREATE  INDEX [csid] ON [db2]([csid]) ON [PRIMARY]
GO

 CREATE  INDEX [csg] ON [db2]([csg]) ON [PRIMARY]
GO

 CREATE  INDEX [csgid] ON [db2]([csgid]) ON [PRIMARY]
GO

 CREATE  INDEX [csa] ON [db2]([csa]) ON [PRIMARY]
GO

 CREATE  INDEX [csaid] ON [db2]([csaid]) ON [PRIMARY]
GO

 CREATE  INDEX [docg] ON [db2]([docg]) ON [PRIMARY]
GO

 CREATE  INDEX [docgid] ON [db2]([docgid]) ON [PRIMARY]
GO

 CREATE  INDEX [doca] ON [db2]([doca]) ON [PRIMARY]
GO

 CREATE  INDEX [docaid] ON [db2]([docaid]) ON [PRIMARY]
GO

 CREATE  INDEX [lckp] ON [db2]([lckp]) ON [PRIMARY]
GO

 CREATE  INDEX [lckt] ON [db2]([lckt]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [dbcq]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [MFID] ON [dbcq]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [f32] ON [dbcq]([f32]) ON [PRIMARY]
GO

 CREATE  INDEX [eta] ON [dbcq]([eta]) ON [PRIMARY]
GO

 CREATE  INDEX [f11dm] ON [dbcq]([f11dm]) ON [PRIMARY]
GO

 CREATE  INDEX [mtdm] ON [dbcq]([mtdm]) ON [PRIMARY]
GO

 CREATE  INDEX [jgr] ON [dbcq]([jgr]) ON [PRIMARY]
GO


 CREATE  INDEX [F1] ON [dbj1]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [ZXDM] ON [dbj1]([zxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [dfhm] ON [dbj1]([dfhm]) ON [PRIMARY]
GO

 CREATE  INDEX [dfhh] ON [dbj1]([dfhh]) ON [PRIMARY]
GO


 CREATE  INDEX [F1] ON [dbj2]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [f2] ON [dbj2]([f2]) ON [PRIMARY]
GO

 CREATE  INDEX [cddm] ON [dbj2]([cddm]) ON [PRIMARY]
GO

 CREATE  INDEX [ckdm] ON [dbj2]([ckdm]) ON [PRIMARY]
GO

 CREATE  INDEX [F3] ON [dbj2]([f3]) ON [PRIMARY]
GO

 CREATE  INDEX [YPCQ] ON [dbj2]([ypcq]) ON [PRIMARY]
GO

 CREATE  INDEX [HXDM] ON [dbj2]([hxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [f10] ON [dbj2]([f10]) ON [PRIMARY]
GO

 CREATE  INDEX [f12dm] ON [dbj2]([f12dm]) ON [PRIMARY]
GO

 CREATE  INDEX [ysfsdm] ON [dbj2]([ysfsdm]) ON [PRIMARY]
GO

 CREATE  INDEX [jydwdm] ON [dbj2]([jydwdm]) ON [PRIMARY]
GO

 CREATE  INDEX [rcbj] ON [dbj2]([rcbj]) ON [PRIMARY]
GO

 CREATE  INDEX [rkrq] ON [dbj2]([rkrq]) ON [PRIMARY]
GO

 CREATE  INDEX [rksj] ON [dbj2]([rksj]) ON [PRIMARY]
GO

 CREATE  INDEX [SONO] ON [dbj2]([sono]) ON [PRIMARY]
GO

 CREATE  INDEX [bhk] ON [dbj2]([bhk]) ON [PRIMARY]
GO

 CREATE  INDEX [sjf] ON [dbj2]([sjf]) ON [PRIMARY]
GO

 CREATE  INDEX [szf] ON [dbj2]([szf]) ON [PRIMARY]
GO

 CREATE  INDEX [docrv] ON [dbj2]([docrv]) ON [PRIMARY]
GO

 CREATE  INDEX [mtdm] ON [dbj2]([mtdm]) ON [PRIMARY]
GO

 CREATE  INDEX [xddm] ON [dbj2]([xddm]) ON [PRIMARY]
GO

 CREATE  INDEX [mtdm1] ON [dbj2]([mtdm1]) ON [PRIMARY]
GO

 CREATE  INDEX [agtdm] ON [dbj2]([agtdm]) ON [PRIMARY]
GO

 CREATE  INDEX [czyq] ON [dbj2]([czyq]) ON [PRIMARY]
GO

 CREATE  INDEX [sjbjdm] ON [dbj2]([sjbjdm]) ON [PRIMARY]
GO

 CREATE  INDEX [kasbdm] ON [dbj2]([kasbdm]) ON [PRIMARY]
GO

 CREATE  INDEX [txcddm] ON [dbj2]([txcddm]) ON [PRIMARY]
GO

 CREATE  INDEX [cgsyq] ON [dbj2]([cgsyq]) ON [PRIMARY]
GO

 CREATE  INDEX [xhdm] ON [dbj2]([xhdm]) ON [PRIMARY]
GO

 CREATE  INDEX [BZDWDM] ON [dbj2]([bzdwdm]) ON [PRIMARY]
GO

 CREATE  INDEX [zjdm] ON [dbj2]([zjdm]) ON [PRIMARY]
GO

 CREATE  INDEX [zxdm2] ON [dbj2]([zxdm2]) ON [PRIMARY]
GO

 CREATE  INDEX [xckadm] ON [dbj2]([xckadm]) ON [PRIMARY]
GO


 CREATE  INDEX [F1] ON [dbj3]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [by9] ON [dbj3]([by9]) ON [PRIMARY]
GO

 CREATE  INDEX [zgdbgfdm] ON [dbj3]([zgdbgfdm]) ON [PRIMARY]
GO

 CREATE  INDEX [dbcddm] ON [dbj3]([dbcddm]) ON [PRIMARY]
GO

 CREATE  INDEX [hxcddm] ON [dbj3]([hxcddm]) ON [PRIMARY]
GO

 CREATE  INDEX [hxdwdm] ON [dbj3]([hxdwdm]) ON [PRIMARY]
GO

 CREATE  INDEX [fzdm] ON [dbj3]([fzdm]) ON [PRIMARY]
GO

 CREATE  INDEX [dzdm] ON [dbj3]([dzdm]) ON [PRIMARY]
GO

 CREATE  INDEX [shrdm] ON [dbj3]([shrdm]) ON [PRIMARY]
GO

 CREATE  INDEX [kadldm] ON [dbj3]([kadldm]) ON [PRIMARY]
GO

 CREATE  INDEX [ckmd] ON [dbj3]([ckmd]) ON [PRIMARY]
GO

 CREATE  INDEX [jkmd] ON [dbj3]([jkmd]) ON [PRIMARY]
GO

 CREATE  INDEX [cktc] ON [dbj3]([cktc]) ON [PRIMARY]
GO

 CREATE  INDEX [jktc] ON [dbj3]([jktc]) ON [PRIMARY]
GO

 CREATE  INDEX [ckwt] ON [dbj3]([ckwt]) ON [PRIMARY]
GO

 CREATE  INDEX [jkwt] ON [dbj3]([jkwt]) ON [PRIMARY]
GO


 CREATE  INDEX [f1] ON [dbj4]([f1]) ON [PRIMARY]
GO


 CREATE  INDEX [f1] ON [dbj5]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [ypzt] ON [dbj5]([ypzt]) ON [PRIMARY]
GO

 CREATE  INDEX [yp_bj] ON [dbj5]([yp_bj]) ON [PRIMARY]
GO

 CREATE  INDEX [yp_rq] ON [dbj5]([yp_rq]) ON [PRIMARY]
GO

 CREATE  INDEX [hg_bj] ON [dbj5]([hg_bj]) ON [PRIMARY]
GO

 CREATE  INDEX [hg_rq] ON [dbj5]([hg_rq]) ON [PRIMARY]
GO

 CREATE  INDEX [xh_bj] ON [dbj5]([xh_bj]) ON [PRIMARY]
GO

 CREATE  INDEX [xh_rq] ON [dbj5]([xh_rq]) ON [PRIMARY]
GO

 CREATE  INDEX [xd_bj] ON [dbj5]([xd_bj]) ON [PRIMARY]
GO

 CREATE  INDEX [xd_rq] ON [dbj5]([xd_rq]) ON [PRIMARY]
GO

 CREATE  INDEX [yp_sj] ON [dbj5]([yp_sj]) ON [PRIMARY]
GO

 CREATE  INDEX [f31_sj] ON [dbj5]([f31_sj]) ON [PRIMARY]
GO

 CREATE  INDEX [hg_sj] ON [dbj5]([hg_sj]) ON [PRIMARY]
GO

 CREATE  INDEX [xd_sj] ON [dbj5]([xd_sj]) ON [PRIMARY]
GO

 CREATE  INDEX [cb_sj] ON [dbj5]([cb_sj]) ON [PRIMARY]
GO

 CREATE  INDEX [mv_sj] ON [dbj5]([mv_sj]) ON [PRIMARY]
GO

 CREATE  INDEX [xh_sj] ON [dbj5]([xh_sj]) ON [PRIMARY]
GO

 CREATE  INDEX [cb_bj] ON [dbj5]([cb_bj]) ON [PRIMARY]
GO

 CREATE  INDEX [cb_rq] ON [dbj5]([cb_rq]) ON [PRIMARY]
GO

 CREATE  INDEX [mv_bj] ON [dbj5]([mv_bj]) ON [PRIMARY]
GO

 CREATE  INDEX [mv_rq] ON [dbj5]([mv_rq]) ON [PRIMARY]
GO

 CREATE  INDEX [f31_rq] ON [dbj5]([f31_rq]) ON [PRIMARY]
GO

 CREATE  INDEX [f29_sj] ON [dbj5]([f29_sj]) ON [PRIMARY]
GO

 CREATE  INDEX [f29_rq] ON [dbj5]([f29_rq]) ON [PRIMARY]
GO

 CREATE  INDEX [ba_bj] ON [dbj5]([ba_bj]) ON [PRIMARY]
GO

 CREATE  INDEX [ba_rq] ON [dbj5]([ba_rq]) ON [PRIMARY]
GO

 CREATE  INDEX [ba_sj] ON [dbj5]([ba_sj]) ON [PRIMARY]
GO

 CREATE  INDEX [zh_bj] ON [dbj5]([zh_bj]) ON [PRIMARY]
GO

 CREATE  INDEX [zh_rq] ON [dbj5]([zh_rq]) ON [PRIMARY]
GO

 CREATE  INDEX [zh_sj] ON [dbj5]([zh_sj]) ON [PRIMARY]
GO


 CREATE  INDEX [f1] ON [dbj6]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [bf] ON [dbj6]([bf]) ON [PRIMARY]
GO

 CREATE  INDEX [bfid] ON [dbj6]([bfid]) ON [PRIMARY]
GO

 CREATE  INDEX [bfg] ON [dbj6]([bfg]) ON [PRIMARY]
GO

 CREATE  INDEX [bfgid] ON [dbj6]([bfgid]) ON [PRIMARY]
GO

 CREATE  INDEX [bfa] ON [dbj6]([bfa]) ON [PRIMARY]
GO

 CREATE  INDEX [bfaid] ON [dbj6]([bfaid]) ON [PRIMARY]
GO

 CREATE  INDEX [bg] ON [dbj6]([bg]) ON [PRIMARY]
GO

 CREATE  INDEX [bgid] ON [dbj6]([bgid]) ON [PRIMARY]
GO

 CREATE  INDEX [bgg] ON [dbj6]([bgg]) ON [PRIMARY]
GO

 CREATE  INDEX [bggid] ON [dbj6]([bggid]) ON [PRIMARY]
GO

 CREATE  INDEX [bga] ON [dbj6]([bga]) ON [PRIMARY]
GO

 CREATE  INDEX [bgaid] ON [dbj6]([bgaid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [dbj7]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [dbj7]([f1]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [dbjtm]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [dbjtm]([f1]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [dbxj]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [dbxj]([f1]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [dcdmk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dcdm] ON [dcdmk]([dcdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [dcinfo]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [dcinfo]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [ch] ON [dcinfo]([ch]) ON [PRIMARY]
GO

 CREATE  INDEX [mv] ON [dcinfo]([mv]) ON [PRIMARY]
GO

 CREATE  INDEX [dcetd] ON [dcinfo]([dcetd]) ON [PRIMARY]
GO

 CREATE  INDEX [dceta] ON [dcinfo]([dceta]) ON [PRIMARY]
GO

 CREATE  INDEX [cyrdm] ON [dcinfo]([cyrdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [dmyy]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dbf0] ON [dmyy]([dbf0]) ON [PRIMARY]
GO

 CREATE  INDEX [dm1] ON [dmyy]([dm1]) ON [PRIMARY]
GO

 CREATE  INDEX [dm0] ON [dmyy]([dm0]) ON [PRIMARY]
GO

 CREATE  INDEX [dbf1] ON [dmyy]([dbf1]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [dmyy]([sm]) ON [PRIMARY]
GO

 CREATE  INDEX [mc1] ON [dmyy]([mc1]) ON [PRIMARY]
GO

 CREATE  INDEX [mc0] ON [dmyy]([mc0]) ON [PRIMARY]
GO


 CREATE  INDEX [f1] ON [document]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [factcode] ON [document]([factcode]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [dsk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [dsk]([f1]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [dxlog]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [MFID] ON [dxlog]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [zzyh] ON [dxlog]([zzyh]) ON [PRIMARY]
GO

 CREATE  INDEX [date_] ON [dxlog]([date_]) ON [PRIMARY]
GO

 CREATE  INDEX [time_] ON [dxlog]([time_]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [dxlog]([f1]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [dxx]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [dzdm]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [dzdm]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [mc] ON [dzdm]([mc]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [dzdm]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [f9] ON [dzdm]([f9]) ON [PRIMARY]
GO

 CREATE  INDEX [lx] ON [dzdm]([lx]) ON [PRIMARY]
GO


 CREATE  INDEX [dzdm] ON [dzdmk]([dzdm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [dzdmk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [dzdmk]([sm]) ON [PRIMARY]
GO

 CREATE  INDEX [DZMC] ON [dzdmk]([dzmc]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ebk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [fl] ON [ebk]([fl]) ON [PRIMARY]
GO

 CREATE  INDEX [DCBH] ON [ebk]([dcbh]) ON [PRIMARY]
GO

 CREATE  INDEX [wtfdm] ON [ebk]([wtfdm]) ON [PRIMARY]
GO

 CREATE  INDEX [hxdm] ON [ebk]([hxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [cgsdm] ON [ebk]([cgsdm]) ON [PRIMARY]
GO

 CREATE  INDEX [fm1] ON [ebk]([fm1]) ON [PRIMARY]
GO

 CREATE  INDEX [tdlx] ON [ebk]([tdlx]) ON [PRIMARY]
GO

 CREATE  INDEX [jhd] ON [ebk]([jhd]) ON [PRIMARY]
GO

 CREATE  INDEX [zhg] ON [ebk]([zhg]) ON [PRIMARY]
GO

 CREATE  INDEX [xhg] ON [ebk]([xhg]) ON [PRIMARY]
GO

 CREATE  INDEX [mdg] ON [ebk]([mdg]) ON [PRIMARY]
GO

 CREATE  INDEX [ypcq] ON [ebk]([ypcq]) ON [PRIMARY]
GO

 CREATE  INDEX [tj] ON [ebk]([tj]) ON [PRIMARY]
GO

 CREATE  INDEX [jobno] ON [ebk]([jobno]) ON [PRIMARY]
GO

 CREATE  INDEX [cmhc] ON [ebk]([cmhc]) ON [PRIMARY]
GO

 CREATE  INDEX [etd] ON [ebk]([etd]) ON [PRIMARY]
GO

 CREATE  INDEX [GDH] ON [ebk]([gdh]) ON [PRIMARY]
GO

 CREATE  INDEX [srrq] ON [ebk]([srrq]) ON [PRIMARY]
GO

 CREATE  INDEX [xgrq] ON [ebk]([xgrq]) ON [PRIMARY]
GO

 CREATE  INDEX [tjrq] ON [ebk]([tjrq]) ON [PRIMARY]
GO

 CREATE  INDEX [jsrq] ON [ebk]([jsrq]) ON [PRIMARY]
GO

 CREATE  INDEX [dcrq] ON [ebk]([dcrq]) ON [PRIMARY]
GO

 CREATE  INDEX [zt] ON [ebk]([zt]) ON [PRIMARY]
GO

 CREATE  INDEX [ck] ON [ebk]([ck]) ON [PRIMARY]
GO

 CREATE  INDEX [cd] ON [ebk]([cd]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [ebk]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [sl] ON [ebk]([sl]) ON [PRIMARY]
GO

 CREATE  INDEX [glr] ON [ebk]([glr]) ON [PRIMARY]
GO

 CREATE  INDEX [glrid] ON [ebk]([glrid]) ON [PRIMARY]
GO

 CREATE  INDEX [bm] ON [ebk]([bm]) ON [PRIMARY]
GO

 CREATE  INDEX [bmid] ON [ebk]([bmid]) ON [PRIMARY]
GO

 CREATE  INDEX [glra] ON [ebk]([glra]) ON [PRIMARY]
GO

 CREATE  INDEX [glraid] ON [ebk]([glraid]) ON [PRIMARY]
GO

 CREATE  INDEX [bwfid] ON [ebk]([bwfid]) ON [PRIMARY]
GO

 CREATE  INDEX [ztdm] ON [ebk]([ztdm]) ON [PRIMARY]
GO

 CREATE  INDEX [lckp] ON [ebk]([lckp]) ON [PRIMARY]
GO

 CREATE  INDEX [lckt] ON [ebk]([lckt]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ebkzt]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [ebkzt]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [edi]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [edi]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [lx] ON [edi]([lx]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [edi95]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [edi95]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [mc] ON [edi95]([mc]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [edicd]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [edicd]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [bh] ON [edicd]([bh]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [edics]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [edics]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [edidd]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [edidd]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [zwm] ON [edidd]([zwm]) ON [PRIMARY]
GO

 CREATE  INDEX [ywm] ON [edidd]([ywm]) ON [PRIMARY]
GO

 CREATE  INDEX [sfdm] ON [edidd]([sfdm]) ON [PRIMARY]
GO

 CREATE  INDEX [gjdm] ON [edidd]([gjdm]) ON [PRIMARY]
GO

 CREATE  INDEX [ztdm] ON [edidd]([ztdm]) ON [PRIMARY]
GO

 CREATE  INDEX [gndm] ON [edidd]([gndm]) ON [PRIMARY]
GO

 CREATE  INDEX [zbdm] ON [edidd]([zbdm]) ON [PRIMARY]
GO

 CREATE  INDEX [xzdm] ON [edidd]([xzdm]) ON [PRIMARY]
GO

 CREATE  INDEX [sm] ON [edidd]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ediddgj]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [ediddgj]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [zwm] ON [ediddgj]([zwm]) ON [PRIMARY]
GO

 CREATE  INDEX [ywm] ON [ediddgj]([ywm]) ON [PRIMARY]
GO

 CREATE  INDEX [sm] ON [ediddgj]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ediddgn]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [ediddgn]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [zwm] ON [ediddgn]([zwm]) ON [PRIMARY]
GO

 CREATE  INDEX [ywm] ON [ediddgn]([ywm]) ON [PRIMARY]
GO

 CREATE  INDEX [sm] ON [ediddgn]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ediddsf]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [ediddsf]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [zwm] ON [ediddsf]([zwm]) ON [PRIMARY]
GO

 CREATE  INDEX [ywm] ON [ediddsf]([ywm]) ON [PRIMARY]
GO

 CREATE  INDEX [sm] ON [ediddsf]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ediddzt]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [ediddzt]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [zwm] ON [ediddzt]([zwm]) ON [PRIMARY]
GO

 CREATE  INDEX [ywm] ON [ediddzt]([ywm]) ON [PRIMARY]
GO

 CREATE  INDEX [sm] ON [ediddzt]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [edihg]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [edihg]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [mc] ON [edihg]([mc]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [edihs]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [edihs]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [edika]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [edika]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [edinbxx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [nbs] ON [edinbxx]([nbs]) ON [PRIMARY]
GO

 CREATE  INDEX [nbt] ON [edinbxx]([nbt]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ediunno]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [ediunno]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [mc] ON [ediunno]([mc]) ON [PRIMARY]
GO

 CREATE  INDEX [sm] ON [ediunno]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ediuser]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [yhdm] ON [ediuser]([yhdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ediwbzlb]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [ediwbzlb]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ediwfl]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [ediwfl]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ediwimdg]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [ediwimdg]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [edixd]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [DM] ON [edixd]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [MC] ON [edixd]([mc]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [egoods]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [egoods]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [hsbm] ON [egoods]([hsbm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ekpttsq]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [ekpttsq]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [kptt] ON [ekpttsq]([kptt]) ON [PRIMARY]
GO

 CREATE  INDEX [nsid] ON [ekpttsq]([nsid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [emlnr]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [entrydm]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [entrydm]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [lxdm] ON [entrydm]([lxdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [exdata]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ezxd]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [xf1] ON [ezxd]([xf1]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [ezxd]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [ezxd]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ezxd0]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [zxdfid] ON [ezxd0]([zxdfid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [ezxd0]([f1]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ezxd2]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [ezxd2]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ezxdmx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [ezxdmx]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [y1] ON [ezxdmx]([y1]) ON [PRIMARY]
GO

 CREATE  INDEX [y2] ON [ezxdmx]([y2]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [f_bm]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [f_bm]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [f_gs]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [lckp] ON [f_gs]([lckp]) ON [PRIMARY]
GO

 CREATE  INDEX [lckt] ON [f_gs]([lckt]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [f_jz]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [jdid] ON [f_jz]([jdid]) ON [PRIMARY]
GO

 CREATE  INDEX [nf] ON [f_jz]([nf]) ON [PRIMARY]
GO

 CREATE  INDEX [yf] ON [f_jz]([yf]) ON [PRIMARY]
GO

 CREATE  INDEX [jz] ON [f_jz]([jz]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [f_km]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [km] ON [f_km]([km]) ON [PRIMARY]
GO

 CREATE  INDEX [zjm] ON [f_km]([zjm]) ON [PRIMARY]
GO

 CREATE  INDEX [kmmc] ON [f_km]([kmmc]) ON [PRIMARY]
GO

 CREATE  INDEX [kmmc2] ON [f_km]([kmmc2]) ON [PRIMARY]
GO

 CREATE  INDEX [hz] ON [f_km]([hz]) ON [PRIMARY]
GO

 CREATE  INDEX [sjkm] ON [f_km]([sjkm]) ON [PRIMARY]
GO

 CREATE  INDEX [wlhs] ON [f_km]([wlhs]) ON [PRIMARY]
GO

 CREATE  INDEX [wbhs] ON [f_km]([wbhs]) ON [PRIMARY]
GO

 CREATE  INDEX [grhs] ON [f_km]([grhs]) ON [PRIMARY]
GO

 CREATE  INDEX [slhs] ON [f_km]([slhs]) ON [PRIMARY]
GO

 CREATE  INDEX [yhz] ON [f_km]([yhz]) ON [PRIMARY]
GO

 CREATE  INDEX [fc] ON [f_km]([fc]) ON [PRIMARY]
GO

 CREATE  INDEX [yhs] ON [f_km]([yhs]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [f_kmlx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [sz] ON [f_kmlx]([sz]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [f_kmlx]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [f_kmxz]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [f_kmxz]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [f_kz]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [jdid] ON [f_kz]([jdid]) ON [PRIMARY]
GO

 CREATE  INDEX [lckp] ON [f_kz]([lckp]) ON [PRIMARY]
GO

 CREATE  INDEX [lckt] ON [f_kz]([lckt]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [f_ly]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [f_ly]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [f_ms1]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [f_ms1]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [f_ms2]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [f_ms2]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [f_ms2]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [f_ms3]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [f_ms3]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [f_pd]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [f_pd]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [hh] ON [f_pd]([hh]) ON [PRIMARY]
GO

 CREATE  INDEX [km] ON [f_pd]([km]) ON [PRIMARY]
GO

 CREATE  INDEX [F9] ON [f_pd]([f9]) ON [PRIMARY]
GO

 CREATE  INDEX [fkdm] ON [f_pd]([fkdm]) ON [PRIMARY]
GO

 CREATE  INDEX [pjh] ON [f_pd]([pjh]) ON [PRIMARY]
GO

 CREATE  INDEX [gr] ON [f_pd]([gr]) ON [PRIMARY]
GO

 CREATE  INDEX [grid] ON [f_pd]([grid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [f_pd]([f1]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [f_pz]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [lx] ON [f_pz]([lx]) ON [PRIMARY]
GO

 CREATE  INDEX [jdid] ON [f_pz]([jdid]) ON [PRIMARY]
GO

 CREATE  INDEX [nf] ON [f_pz]([nf]) ON [PRIMARY]
GO

 CREATE  INDEX [yf] ON [f_pz]([yf]) ON [PRIMARY]
GO

 CREATE  INDEX [pz] ON [f_pz]([pz]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [f_pz]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [lckp] ON [f_pz]([lckp]) ON [PRIMARY]
GO

 CREATE  INDEX [lckt] ON [f_pz]([lckt]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [f_pzlb]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [f_pzlb]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [f_qc]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [jdid] ON [f_qc]([jdid]) ON [PRIMARY]
GO

 CREATE  INDEX [nf] ON [f_qc]([nf]) ON [PRIMARY]
GO

 CREATE  INDEX [yf] ON [f_qc]([yf]) ON [PRIMARY]
GO

 CREATE  INDEX [km] ON [f_qc]([km]) ON [PRIMARY]
GO

 CREATE  INDEX [sjkm] ON [f_qc]([sjkm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [f_y]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [DM] ON [f_y]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [f_ye]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [jdid] ON [f_ye]([jdid]) ON [PRIMARY]
GO

 CREATE  INDEX [nf] ON [f_ye]([nf]) ON [PRIMARY]
GO

 CREATE  INDEX [yf] ON [f_ye]([yf]) ON [PRIMARY]
GO

 CREATE  INDEX [km] ON [f_ye]([km]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [f_zb]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [jdid] ON [f_zb]([jdid]) ON [PRIMARY]
GO

 CREATE  INDEX [nf] ON [f_zb]([nf]) ON [PRIMARY]
GO

 CREATE  INDEX [yf] ON [f_zb]([yf]) ON [PRIMARY]
GO

 CREATE  INDEX [km] ON [f_zb]([km]) ON [PRIMARY]
GO

 CREATE  INDEX [ly] ON [f_zb]([ly]) ON [PRIMARY]
GO

 CREATE  INDEX [fl] ON [f_zb]([fl]) ON [PRIMARY]
GO

 CREATE  INDEX [pdfid] ON [f_zb]([pdfid]) ON [PRIMARY]
GO

 CREATE  INDEX [pz] ON [f_zb]([pz]) ON [PRIMARY]
GO

 CREATE  INDEX [rq] ON [f_zb]([rq]) ON [PRIMARY]
GO

 CREATE  INDEX [yhdm] ON [f_zb]([yhdm]) ON [PRIMARY]
GO

 CREATE  INDEX [f9] ON [f_zb]([f9]) ON [PRIMARY]
GO

 CREATE  INDEX [ydm] ON [f_zb]([ydm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [f_zy]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [f_zy]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [f_zygs]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [f_zygs]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [fei_cnt]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [fei_cnt]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [fjk]([fid]) ON [fjk]
GO

 CREATE  INDEX [F1] ON [fjk]([f1]) ON [fjk]
GO

 CREATE  INDEX [cjr] ON [fjk]([cjr]) ON [fjk]
GO

 CREATE  INDEX [fb] ON [fjk]([fb]) ON [fjk]
GO

 CREATE  INDEX [cjrid] ON [fjk]([cjrid]) ON [fjk]
GO

 CREATE  INDEX [cjrg] ON [fjk]([cjrg]) ON [fjk]
GO

 CREATE  INDEX [cjrgid] ON [fjk]([cjrgid]) ON [fjk]
GO

 CREATE  INDEX [cjra] ON [fjk]([cjra]) ON [fjk]
GO

 CREATE  INDEX [cjraid] ON [fjk]([cjraid]) ON [fjk]
GO

 CREATE  INDEX [klx] ON [fjk]([klx]) ON [fjk]
GO

 CREATE  INDEX [xzbj] ON [fjk]([xzbj]) ON [fjk]
GO


 CREATE  INDEX [fid] ON [fjlx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [fjlx]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [mc] ON [fjlx]([mc]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [fjlx]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [fjsftj]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [dm] ON [fkdmk]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [fkdmk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [MC] ON [fkdmk]([mc]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [fkdmk]([sm]) ON [PRIMARY]
GO

 CREATE  INDEX [ts] ON [fkdmk]([ts]) ON [PRIMARY]
GO


 CREATE  INDEX [dm] ON [fkfs]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [fkfs]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [fplx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [fplx]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [fwlx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [fwlx]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [fydmk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [FYDM] ON [fydmk]([fydm]) ON [PRIMARY]
GO

 CREATE  INDEX [FYMC] ON [fydmk]([fymc]) ON [PRIMARY]
GO

 CREATE  INDEX [FYEMC] ON [fydmk]([fyemc]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [fydmk]([sm]) ON [PRIMARY]
GO

 CREATE  INDEX [msdm] ON [fydmk]([msdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [fydw]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [fydw]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [fyfl]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [fyfl]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [fyqz]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [fyqz]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [dm] ON [ggdbf]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [ggdbf]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [MC] ON [ggdbf]([mc]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [ggdbf]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [gjk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [DM] ON [gjk]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [ZWM] ON [gjk]([zwm]) ON [PRIMARY]
GO

 CREATE  INDEX [YWM] ON [gjk]([ywm]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [gjk]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [gjzt]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [gjzt]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [gkdbf]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [gkdm] ON [gkdbf]([gkdm]) ON [PRIMARY]
GO

 CREATE  INDEX [GKQC] ON [gkdbf]([gkqc]) ON [PRIMARY]
GO

 CREATE  INDEX [GKZWM] ON [gkdbf]([gkzwm]) ON [PRIMARY]
GO

 CREATE  INDEX [HXDM] ON [gkdbf]([hxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [GJDM] ON [gkdbf]([gjdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [gkdbfa]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [gkdm] ON [gkdbfa]([gkdm]) ON [PRIMARY]
GO

 CREATE  INDEX [GKQC] ON [gkdbfa]([gkqc]) ON [PRIMARY]
GO

 CREATE  INDEX [GKZWM] ON [gkdbfa]([gkzwm]) ON [PRIMARY]
GO

 CREATE  INDEX [HXDM] ON [gkdbfa]([hxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [GJDM] ON [gkdbfa]([gjdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [gkdbfb]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [gkdm] ON [gkdbfb]([gkdm]) ON [PRIMARY]
GO

 CREATE  INDEX [gkqc] ON [gkdbfb]([gkqc]) ON [PRIMARY]
GO

 CREATE  INDEX [gkzwm] ON [gkdbfb]([gkzwm]) ON [PRIMARY]
GO

 CREATE  INDEX [hxdm] ON [gkdbfb]([hxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [gjdm] ON [gkdbfb]([gjdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [gkdbfc]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [gkdm] ON [gkdbfc]([gkdm]) ON [PRIMARY]
GO

 CREATE  INDEX [gkqc] ON [gkdbfc]([gkqc]) ON [PRIMARY]
GO

 CREATE  INDEX [gkzwm] ON [gkdbfc]([gkzwm]) ON [PRIMARY]
GO

 CREATE  INDEX [hxdm] ON [gkdbfc]([hxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [gjdm] ON [gkdbfc]([gjdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [gkdbfz]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [zzdm] ON [gkdbfz]([zzdm]) ON [PRIMARY]
GO

 CREATE  INDEX [gkdm] ON [gkdbfz]([gkdm]) ON [PRIMARY]
GO

 CREATE  INDEX [cgsdm] ON [gkdbfz]([cgsdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [goods]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [goods]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [xh] ON [goods]([xh]) ON [PRIMARY]
GO

 CREATE  INDEX [BZDM] ON [goods]([bzdm]) ON [PRIMARY]
GO


 CREATE  INDEX [DM] ON [gsfkdm]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [FID] ON [gsfkdm]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [gstt]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [gysdb]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [gysdb]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [lxmd] ON [gysdb]([lxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [gysmd] ON [gysdb]([gysdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [gysfl]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [gysfl]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [gz]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [dm] ON [hgdm]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [hgdm]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [hgfl]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [hgfl]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [hsdl]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [hsdl]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [hsfs] ON [hsdl]([hsfs]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [hsfl]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [hsfl]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [ufdm] ON [hsfl]([ufdm]) ON [PRIMARY]
GO

 CREATE  INDEX [sino_dm] ON [hsfl]([sino_dm]) ON [PRIMARY]
GO


 CREATE  INDEX [dm] ON [hsxj]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [hsxj]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [DM] ON [hsxm]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [hsxm]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [hsyh]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [hsyh]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [hwfl]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [DM] ON [hwfl]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [hwzt]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [hwzt]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [hxdmk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [HXDM] ON [hxdmk]([hxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [HX] ON [hxdmk]([hx]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [hxdmk]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [hxfl]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [hxfl]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [hydm]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [MC] ON [hydm]([mc]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [hydm]([sm]) ON [PRIMARY]
GO

 CREATE  INDEX [DM] ON [hydm]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ie]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [ie]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [isf]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [F1] ON [isf]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [scs] ON [isf]([scs]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jcfl]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [jcfl]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jjr]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [jjr] ON [jjr]([jjr]) ON [PRIMARY]
GO

 CREATE  INDEX [tzr] ON [jjr]([tzr]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [fsdm] ON [jk]([fsdm]) ON [PRIMARY]
GO

 CREATE  INDEX [yydm] ON [jk]([yydm]) ON [PRIMARY]
GO

 CREATE  INDEX [f9] ON [jk]([f9]) ON [PRIMARY]
GO

 CREATE  INDEX [qy] ON [jk]([qy]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jkfs]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jkyy]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jobgc]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [F1] ON [jobgc]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [lxdm] ON [jobgc]([lxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [cur] ON [jobgc]([cur]) ON [PRIMARY]
GO

 CREATE  INDEX [tj] ON [jobgc]([tj]) ON [PRIMARY]
GO

 CREATE  INDEX [cl] ON [jobgc]([cl]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [jobgc]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [joblx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [joblx]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jsdmht]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jsdmjq]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jsdmkq]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [jsdmkq]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jsdmlz]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jsdmsb]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jsdmwh]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jshys]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [cjrfid] ON [jshys]([cjrfid]) ON [PRIMARY]
GO

 CREATE  INDEX [jrrfid] ON [jshys]([jrrfid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [jshys]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jshyslt]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [jshyslt]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [fsfid] ON [jshyslt]([fsfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jsltjl]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [fsfid] ON [jsltjl]([fsfid]) ON [PRIMARY]
GO

 CREATE  INDEX [sdfid] ON [jsltjl]([sdfid]) ON [PRIMARY]
GO

 CREATE  INDEX [zt] ON [jsltjl]([zt]) ON [PRIMARY]
GO

 CREATE  INDEX [LTJL] ON [jsltjl]([ltjl]) ON [PRIMARY]
GO

 CREATE  INDEX [jsrq] ON [jsltjl]([jsrq]) ON [PRIMARY]
GO

 CREATE  INDEX [jssj] ON [jsltjl]([jssj]) ON [PRIMARY]
GO

 CREATE  INDEX [jsbj] ON [jsltjl]([jsbj]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [jsltjl]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [fsrq] ON [jsltjl]([fsrq]) ON [PRIMARY]
GO

 CREATE  INDEX [fstm] ON [jsltjl]([fstm]) ON [PRIMARY]
GO

 CREATE  INDEX [sw] ON [jsltjl]([sw]) ON [PRIMARY]
GO

 CREATE  INDEX [xs] ON [jsltjl]([xs]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jsmsnlxr]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [uid] ON [jsmsnlxr]([uid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jsxxfl]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [jsxxfl]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jsy]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [jsy]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jsygw]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [jsygw]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [xm] ON [jsygw]([xm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jsyh]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [xxfid] ON [jsyh]([xxfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jsyht]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [jsyht]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [xm] ON [jsyht]([xm]) ON [PRIMARY]
GO

 CREATE  INDEX [ksrq] ON [jsyht]([ksrq]) ON [PRIMARY]
GO

 CREATE  INDEX [jsrq] ON [jsyht]([jsrq]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jsyjl]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [jsyjl]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [xm] ON [jsyjl]([xm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jsyjt]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [jsyjt]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [xm] ON [jsyjt]([xm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jsykq]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [jsykq]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [xm] ON [jsykq]([xm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jsypx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [jsypx]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [rq] ON [jsypx]([rq]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jsyqj]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [jsyqj]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [xm] ON [jsyqj]([xm]) ON [PRIMARY]
GO

 CREATE  INDEX [lgrq] ON [jsyqj]([lgrq]) ON [PRIMARY]
GO

 CREATE  INDEX [fzrq] ON [jsyqj]([fzrq]) ON [PRIMARY]
GO

 CREATE  INDEX [qjrq] ON [jsyqj]([qjrq]) ON [PRIMARY]
GO

 CREATE  INDEX [SWH] ON [jsyqj]([swh]) ON [PRIMARY]
GO

 CREATE  INDEX [sw_fid] ON [jsyqj]([sw_fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jsysg]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [jsysg]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [bh] ON [jsysg]([bh]) ON [PRIMARY]
GO

 CREATE  INDEX [ch] ON [jsysg]([ch]) ON [PRIMARY]
GO

 CREATE  INDEX [jsy] ON [jsysg]([jsy]) ON [PRIMARY]
GO

 CREATE  INDEX [rq] ON [jsysg]([rq]) ON [PRIMARY]
GO

 CREATE  INDEX [xm] ON [jsysg]([xm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jsysj]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [jsysj]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [xm] ON [jsysj]([xm]) ON [PRIMARY]
GO

 CREATE  INDEX [jnrq] ON [jsysj]([jnrq]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jsyst]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [jsyst]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [xm] ON [jsyst]([xm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jsywz]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [jsywz]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [rq] ON [jsywz]([rq]) ON [PRIMARY]
GO

 CREATE  INDEX [xm] ON [jsywz]([xm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jsyxf]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [jsyxf]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [xm] ON [jsyxf]([xm]) ON [PRIMARY]
GO

 CREATE  INDEX [ffrq] ON [jsyxf]([ffrq]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jsyxl]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [jsyxl]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [xm] ON [jsyxl]([xm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jsyxt]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [jsyxt]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [xm] ON [jsyxt]([xm]) ON [PRIMARY]
GO

 CREATE  INDEX [tzrq] ON [jsyxt]([tzrq]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jsyxx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [jsyxx]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [lckp] ON [jsyxx]([lckp]) ON [PRIMARY]
GO

 CREATE  INDEX [lckt] ON [jsyxx]([lckt]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jxfs]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [jxfs]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jydwdmk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [jydwdmk]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [MC] ON [jydwdmk]([mc]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [jzfs]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [jzfs]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [kdd]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [kdd]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [kddh] ON [kdd]([kddh]) ON [PRIMARY]
GO

 CREATE  INDEX [jjrq] ON [kdd]([jjrq]) ON [PRIMARY]
GO

 CREATE  INDEX [kddm] ON [kdd]([kddm]) ON [PRIMARY]
GO

 CREATE  INDEX [f9] ON [kdd]([f9]) ON [PRIMARY]
GO

 CREATE  INDEX [jjid] ON [kdd]([jjid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [khmyd]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [khxqj]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [DM] ON [khxqj]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [khyj]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [xyh] ON [khyj]([xyh]) ON [PRIMARY]
GO

 CREATE  INDEX [f9] ON [khyj]([f9]) ON [PRIMARY]
GO

 CREATE  INDEX [ksrq] ON [khyj]([ksrq]) ON [PRIMARY]
GO

 CREATE  INDEX [lxdm] ON [khyj]([lxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [qrbj] ON [khyj]([qrbj]) ON [PRIMARY]
GO

 CREATE  INDEX [QUOTENO] ON [khyj]([quoteno]) ON [PRIMARY]
GO

 CREATE  INDEX [fwdm] ON [khyj]([fwdm]) ON [PRIMARY]
GO

 CREATE  INDEX [xxbz] ON [khyj]([xxbz]) ON [PRIMARY]
GO

 CREATE  INDEX [lckp] ON [khyj]([lckp]) ON [PRIMARY]
GO

 CREATE  INDEX [lckt] ON [khyj]([lckt]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [khyjlx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [khyjlx]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [khyjlx]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [khyjmenu]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [JDID] ON [khyjmenu]([jdid]) ON [PRIMARY]
GO

 CREATE  INDEX [FJD] ON [khyjmenu]([fjd]) ON [PRIMARY]
GO

 CREATE  INDEX [XM] ON [khyjmenu]([xm]) ON [PRIMARY]
GO

 CREATE  INDEX [bm] ON [khyjmenu]([bm]) ON [PRIMARY]
GO

 CREATE  INDEX [GLR] ON [khyjmenu]([glr]) ON [PRIMARY]
GO

 CREATE  INDEX [glrid] ON [khyjmenu]([glrid]) ON [PRIMARY]
GO

 CREATE  INDEX [glz] ON [khyjmenu]([glz]) ON [PRIMARY]
GO

 CREATE  INDEX [glzid] ON [khyjmenu]([glzid]) ON [PRIMARY]
GO

 CREATE  INDEX [glra] ON [khyjmenu]([glra]) ON [PRIMARY]
GO

 CREATE  INDEX [glraid] ON [khyjmenu]([glraid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [khyjmx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [khyjmx]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [xmdm] ON [khyjmx]([xmdm]) ON [PRIMARY]
GO

 CREATE  INDEX [qr] ON [khyjmx]([qr]) ON [PRIMARY]
GO

 CREATE  INDEX [xsxh] ON [khyjmx]([xsxh]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [khyjtj]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [lxdm] ON [khyjtj]([lxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [khdm] ON [khyjtj]([khdm]) ON [PRIMARY]
GO

 CREATE  INDEX [zt] ON [khyjtj]([zt]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [khyjxy]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [xyh] ON [khyjxy]([xyh]) ON [PRIMARY]
GO

 CREATE  INDEX [cgsdm] ON [khyjxy]([cgsdm]) ON [PRIMARY]
GO

 CREATE  INDEX [dcfdm] ON [khyjxy]([dcfdm]) ON [PRIMARY]
GO

 CREATE  INDEX [XQJDM] ON [khyjxy]([xqjdm]) ON [PRIMARY]
GO

 CREATE  INDEX [SXRQ] ON [khyjxy]([sxrq]) ON [PRIMARY]
GO

 CREATE  INDEX [SRRQ] ON [khyjxy]([srrq]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [khyjxy]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [QUOTENO] ON [khyjxy]([quoteno]) ON [PRIMARY]
GO

 CREATE  INDEX [subno] ON [khyjxy]([subno]) ON [PRIMARY]
GO

 CREATE  INDEX [PRICENO] ON [khyjxy]([priceno]) ON [PRIMARY]
GO

 CREATE  INDEX [gkd1] ON [khyjxy]([gkd1]) ON [PRIMARY]
GO

 CREATE  INDEX [gkd2] ON [khyjxy]([gkd2]) ON [PRIMARY]
GO

 CREATE  INDEX [gkd3] ON [khyjxy]([gkd3]) ON [PRIMARY]
GO

 CREATE  INDEX [dqrq] ON [khyjxy]([dqrq]) ON [PRIMARY]
GO

 CREATE  INDEX [zxdm] ON [khyjxy]([zxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [hxdm] ON [khyjxy]([hxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [tybj] ON [khyjxy]([tybj]) ON [PRIMARY]
GO

 CREATE  INDEX [qr] ON [khyjxy]([qr]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [kjlx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [kjlx]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [FID] ON [kl]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [zzyh] ON [kl]([zzyh]) ON [PRIMARY]
GO

 CREATE  INDEX [XM] ON [kl]([xm]) ON [PRIMARY]
GO

 CREATE  INDEX [JDID] ON [kl]([jdid]) ON [PRIMARY]
GO

 CREATE  INDEX [FJD] ON [kl]([fjd]) ON [PRIMARY]
GO

 CREATE  INDEX [KL] ON [kl]([kl]) ON [PRIMARY]
GO

 CREATE  INDEX [jy] ON [kl]([jy]) ON [PRIMARY]
GO

 CREATE  INDEX [lckp] ON [kl]([lckp]) ON [PRIMARY]
GO

 CREATE  INDEX [lckt] ON [kl]([lckt]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [klm]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [klm]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [KL] ON [klm]([kl]) ON [PRIMARY]
GO

 CREATE  INDEX [rq] ON [klm]([rq]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [kyfkfs]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [DM] ON [kyfkfs]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [kyfl]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [gkdm] ON [kyfl]([gkdm]) ON [PRIMARY]
GO

 CREATE  INDEX [GKQC] ON [kyfl]([gkqc]) ON [PRIMARY]
GO

 CREATE  INDEX [GJDM] ON [kyfl]([gjdm]) ON [PRIMARY]
GO

 CREATE  INDEX [dm2] ON [kyfl]([dm2]) ON [PRIMARY]
GO

 CREATE  INDEX [zl] ON [kyfl]([zl]) ON [PRIMARY]
GO

 CREATE  INDEX [FL] ON [kyfl]([fl]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [kyfl]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [dm] ON [kyfldm]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [kyfldm]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [nid] ON [lcsk]([nid]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [lcsk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [cz] ON [lcsk]([cz]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [lkbwb]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [bwpt] ON [lkbwb]([bwpt]) ON [PRIMARY]
GO

 CREATE  INDEX [bwdm] ON [lkbwb]([bwdm]) ON [PRIMARY]
GO

 CREATE  INDEX [bwbs] ON [lkbwb]([bwbs]) ON [PRIMARY]
GO

 CREATE  INDEX [ty] ON [lkbwb]([ty]) ON [PRIMARY]
GO

 CREATE  INDEX [jmlx] ON [lkbwb]([jmlx]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [lkbwfx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [lkbwfx]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [lkbwhz]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [lkbwhz]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [sm] ON [lkbwhz]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [lkbwlog]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [lkbwlog]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [fx] ON [lkbwlog]([fx]) ON [PRIMARY]
GO

 CREATE  INDEX [bwbs] ON [lkbwlog]([bwbs]) ON [PRIMARY]
GO

 CREATE  INDEX [wljhdm] ON [lkbwlog]([wljhdm]) ON [PRIMARY]
GO

 CREATE  INDEX [wldwdm] ON [lkbwlog]([wldwdm]) ON [PRIMARY]
GO

 CREATE  INDEX [jsjhdm] ON [lkbwlog]([jsjhdm]) ON [PRIMARY]
GO

 CREATE  INDEX [jsdwdm] ON [lkbwlog]([jsdwdm]) ON [PRIMARY]
GO

 CREATE  INDEX [jhxt] ON [lkbwlog]([jhxt]) ON [PRIMARY]
GO

 CREATE  INDEX [ztdm] ON [lkbwlog]([ztdm]) ON [PRIMARY]
GO

 CREATE  INDEX [acdm] ON [lkbwlog]([acdm]) ON [PRIMARY]
GO

 CREATE  INDEX [hzdm] ON [lkbwlog]([hzdm]) ON [PRIMARY]
GO

 CREATE  INDEX [fxdm] ON [lkbwlog]([fxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [cl] ON [lkbwlog]([cl]) ON [PRIMARY]
GO

 CREATE  INDEX [khbh] ON [lkbwlog]([khbh]) ON [PRIMARY]
GO

 CREATE  INDEX [usercode] ON [lkbwlog]([usercode]) ON [PRIMARY]
GO

 CREATE  INDEX [ebkfid] ON [lkbwlog]([ebkfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [lkbwmsg]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [lkbwmsg]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [lkbwmsg]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [ktfsdm] ON [lkbwmsg]([ktfsdm]) ON [PRIMARY]
GO

 CREATE  INDEX [ktjsdm] ON [lkbwmsg]([ktjsdm]) ON [PRIMARY]
GO

 CREATE  INDEX [state] ON [lkbwmsg]([state]) ON [PRIMARY]
GO

 CREATE  INDEX [type] ON [lkbwmsg]([type]) ON [PRIMARY]
GO

 CREATE  INDEX [khbh] ON [lkbwmsg]([khbh]) ON [PRIMARY]
GO

 CREATE  INDEX [dcbh] ON [lkbwmsg]([dcbh]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [lkbwzt]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [lkbwzt]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [dm] ON [lx2dm]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [lx2dm]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [MC] ON [lx2dm]([mc]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [lx2dm]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [dm] ON [lxdmk]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [lxdmk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mc] ON [lxdmk]([mc]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [lxdmk]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [lyftzxm]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [lyftzxm]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [madditem]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [NO] ON [madditem]([no]) ON [PRIMARY]
GO

 CREATE  INDEX [xh] ON [madditem]([xh]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [maillog]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [item_id] ON [maillog]([item_id]) ON [PRIMARY]
GO

 CREATE  INDEX [title] ON [maillog]([title]) ON [PRIMARY]
GO


 CREATE  INDEX [mawb] ON [mawb]([mawb]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [mawb]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [mawb]([f1]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [menuc]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [NO] ON [menuc]([no]) ON [PRIMARY]
GO

 CREATE  INDEX [mc] ON [menuc]([mc]) ON [PRIMARY]
GO

 CREATE  INDEX [zwmc] ON [menuc]([zwmc]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [menuclx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [menuclx]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [menui]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [jdid] ON [menui]([jdid]) ON [PRIMARY]
GO

 CREATE  INDEX [fjd] ON [menui]([fjd]) ON [PRIMARY]
GO

 CREATE  INDEX [XM] ON [menui]([xm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [mffs]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [mffs]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [msdm]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [msdm]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [msncd]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [msndzxx]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [msnltjl]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [fsz] ON [msnltjl]([fsz]) ON [PRIMARY]
GO

 CREATE  INDEX [jsz] ON [msnltjl]([jsz]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [msnsession]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [msnzdxx]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [mtqydm]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [mtqydm]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [zdm] ON [netlog]([zdm]) ON [PRIMARY]
GO

 CREATE  INDEX [f9] ON [netlog]([f9]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [opm]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [opm]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [pcdl]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [pcdl]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [MC] ON [pcdl]([mc]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [pkg_cz]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [pkg_cz]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [pkg_fb]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [pkg_fb]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [pkg_yt]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [pkgs]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [pkgs]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [podmfk]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [podmfl]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [podmhy]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [podmjck]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [podmk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [podmk]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [podmzt]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [poitem]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [pono] ON [poitem]([pono]) ON [PRIMARY]
GO

 CREATE  INDEX [itemno] ON [poitem]([itemno]) ON [PRIMARY]
GO

 CREATE  INDEX [itemztdm] ON [poitem]([itemztdm]) ON [PRIMARY]
GO

 CREATE  INDEX [shrdm] ON [poitem]([shrdm]) ON [PRIMARY]
GO

 CREATE  INDEX [gysdm] ON [poitem]([gysdm]) ON [PRIMARY]
GO

 CREATE  INDEX [HYLXDM] ON [poitem]([hylxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [f10dm] ON [poitem]([f10dm]) ON [PRIMARY]
GO

 CREATE  INDEX [f11dm] ON [poitem]([f11dm]) ON [PRIMARY]
GO

 CREATE  INDEX [f12dm] ON [poitem]([f12dm]) ON [PRIMARY]
GO


 CREATE  INDEX [pono] ON [posyk]([pono]) ON [PRIMARY]
GO

 CREATE  INDEX [porq] ON [posyk]([porq]) ON [PRIMARY]
GO

 CREATE  INDEX [poztdm] ON [posyk]([poztdm]) ON [PRIMARY]
GO

 CREATE  INDEX [myfs] ON [posyk]([myfs]) ON [PRIMARY]
GO

 CREATE  INDEX [lxdm] ON [posyk]([lxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [jckdm] ON [posyk]([jckdm]) ON [PRIMARY]
GO

 CREATE  INDEX [mjdm] ON [posyk]([mjdm]) ON [PRIMARY]
GO

 CREATE  INDEX [tyrdm] ON [posyk]([tyrdm]) ON [PRIMARY]
GO

 CREATE  INDEX [fhddm] ON [posyk]([fhddm]) ON [PRIMARY]
GO

 CREATE  INDEX [xhddm] ON [posyk]([xhddm]) ON [PRIMARY]
GO

 CREATE  INDEX [lckp] ON [posyk]([lckp]) ON [PRIMARY]
GO

 CREATE  INDEX [lckt] ON [posyk]([lckt]) ON [PRIMARY]
GO

 CREATE  INDEX [glr] ON [posyk]([glr]) ON [PRIMARY]
GO

 CREATE  INDEX [glrid] ON [posyk]([glrid]) ON [PRIMARY]
GO

 CREATE  INDEX [bm] ON [posyk]([bm]) ON [PRIMARY]
GO

 CREATE  INDEX [bmid] ON [posyk]([bmid]) ON [PRIMARY]
GO

 CREATE  INDEX [glra] ON [posyk]([glra]) ON [PRIMARY]
GO

 CREATE  INDEX [glraid] ON [posyk]([glraid]) ON [PRIMARY]
GO

 CREATE  INDEX [cjid] ON [posyk]([cjid]) ON [PRIMARY]
GO

 CREATE  INDEX [cjgid] ON [posyk]([cjgid]) ON [PRIMARY]
GO

 CREATE  INDEX [cjaid] ON [posyk]([cjaid]) ON [PRIMARY]
GO

 CREATE  INDEX [csid] ON [posyk]([csid]) ON [PRIMARY]
GO

 CREATE  INDEX [csgid] ON [posyk]([csgid]) ON [PRIMARY]
GO

 CREATE  INDEX [csaid] ON [posyk]([csaid]) ON [PRIMARY]
GO

 CREATE  INDEX [cj] ON [posyk]([cj]) ON [PRIMARY]
GO

 CREATE  INDEX [cjg] ON [posyk]([cjg]) ON [PRIMARY]
GO

 CREATE  INDEX [cja] ON [posyk]([cja]) ON [PRIMARY]
GO

 CREATE  INDEX [cs] ON [posyk]([cs]) ON [PRIMARY]
GO

 CREATE  INDEX [csg] ON [posyk]([csg]) ON [PRIMARY]
GO

 CREATE  INDEX [csa] ON [posyk]([csa]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [qxdm]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [qxdm]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [sfdm] ON [qxdm]([sfdm]) ON [PRIMARY]
GO

 CREATE  INDEX [csdm] ON [qxdm]([csdm]) ON [PRIMARY]
GO

 CREATE  INDEX [qxdm] ON [qxdm]([qxdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [qydm]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [DM] ON [qydm]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [qygj]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f9] ON [qygj]([f9]) ON [PRIMARY]
GO

 CREATE  INDEX [qydm] ON [qygj]([qydm]) ON [PRIMARY]
GO

 CREATE  INDEX [gjdm] ON [qygj]([gjdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [register]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [lxmc] ON [register]([lxmc]) ON [PRIMARY]
GO

 CREATE  INDEX [lxr] ON [register]([lxr]) ON [PRIMARY]
GO

 CREATE  INDEX [yjbj] ON [register]([yjbj]) ON [PRIMARY]
GO


 CREATE  INDEX [RQ] ON [rl]([rq]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [rl]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [rqfl]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [rqfl]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [dm] ON [sddm]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [sddm]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [sfdm]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [sfdm]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [sfdm] ON [sfdm]([sfdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [sfdzk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [F1] ON [sfdzk]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [dzlx] ON [sfdzk]([dzlx]) ON [PRIMARY]
GO

 CREATE  INDEX [DZBH] ON [sfdzk]([dzbh]) ON [PRIMARY]
GO

 CREATE  INDEX [SRRQ] ON [sfdzk]([srrq]) ON [PRIMARY]
GO

 CREATE  INDEX [fhrq] ON [sfdzk]([fhrq]) ON [PRIMARY]
GO

 CREATE  INDEX [tkhrq] ON [sfdzk]([tkhrq]) ON [PRIMARY]
GO

 CREATE  INDEX [ggbj] ON [sfdzk]([ggbj]) ON [PRIMARY]
GO

 CREATE  INDEX [bh] ON [sfdzk]([bh]) ON [PRIMARY]
GO

 CREATE  INDEX [shrq] ON [sfdzk]([shrq]) ON [PRIMARY]
GO

 CREATE  INDEX [fb] ON [sfdzk]([fb]) ON [PRIMARY]
GO

 CREATE  INDEX [kjdm] ON [sfdzk]([kjdm]) ON [PRIMARY]
GO

 CREATE  INDEX [tgzx] ON [sfdzk]([tgzx]) ON [PRIMARY]
GO

 CREATE  INDEX [xf1] ON [sfdzk]([xf1]) ON [PRIMARY]
GO

 CREATE  INDEX [myfs] ON [sfdzk]([myfs]) ON [PRIMARY]
GO

 CREATE  INDEX [fl] ON [sfdzk]([fl]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [sflx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [sflx]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [sgfaxsend]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [sgfaxsend]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [sendername] ON [sgfaxsend]([sendername]) ON [PRIMARY]
GO

 CREATE  INDEX [rcvername] ON [sgfaxsend]([rcvername]) ON [PRIMARY]
GO

 CREATE  INDEX [faxnum] ON [sgfaxsend]([faxnum]) ON [PRIMARY]
GO

 CREATE  INDEX [status] ON [sgfaxsend]([status]) ON [PRIMARY]
GO

 CREATE  INDEX [username] ON [sgfaxsend]([username]) ON [PRIMARY]
GO

 CREATE  INDEX [logonname] ON [sgfaxsend]([logonname]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [sinofydm]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [sinofydm]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [sinofydm]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [hsdldm] ON [sinofydm]([hsdldm]) ON [PRIMARY]
GO

 CREATE  INDEX [ygs] ON [sinofydm]([ygs]) ON [PRIMARY]
GO

 CREATE  INDEX [ygf] ON [sinofydm]([ygf]) ON [PRIMARY]
GO

 CREATE  INDEX [ys] ON [sinofydm]([ys]) ON [PRIMARY]
GO

 CREATE  INDEX [yf] ON [sinofydm]([yf]) ON [PRIMARY]
GO

 CREATE  INDEX [dsdf] ON [sinofydm]([dsdf]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [sinokm]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [sinokm]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [sinoswmx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [sinoswmx]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [xh] ON [sinoswmx]([xh]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [sinoswmx]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [hcrq] ON [sinoswmx]([hcrq]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [sinoywlx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [sinoywlx]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [hsfldm] ON [sinoywlx]([hsfldm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [sjcl]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [sjcl]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [sjdz]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [sjdz]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [sjh] ON [sjdz]([sjh]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [sjhz]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [sjh] ON [sjhz]([sjh]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [sjlx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [sjlx]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [slc]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [slc]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [slv]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [slv]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [sm]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [sn]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f9] ON [sn]([f9]) ON [PRIMARY]
GO

 CREATE  INDEX [rq] ON [sn]([rq]) ON [PRIMARY]
GO

 CREATE  INDEX [bz] ON [sn]([bz]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [sn]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [cn] ON [sn]([cn]) ON [PRIMARY]
GO

 CREATE  INDEX [jy] ON [sn]([jy]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [snreq]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [lx] ON [snreq]([lx]) ON [PRIMARY]
GO

 CREATE  INDEX [gstt] ON [snreq]([gstt]) ON [PRIMARY]
GO

 CREATE  INDEX [f9] ON [snreq]([f9]) ON [PRIMARY]
GO

 CREATE  INDEX [rq] ON [snreq]([rq]) ON [PRIMARY]
GO

 CREATE  INDEX [bz] ON [snreq]([bz]) ON [PRIMARY]
GO

 CREATE  INDEX [MAC] ON [snreq]([mac]) ON [PRIMARY]
GO

 CREATE  INDEX [hdid] ON [snreq]([hdid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [snreq]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [sob]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [lx] ON [sob]([lx]) ON [PRIMARY]
GO

 CREATE  INDEX [F1] ON [sob]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [F1EX] ON [sob]([f1ex]) ON [PRIMARY]
GO

 CREATE  INDEX [F1REF] ON [sob]([f1ref]) ON [PRIMARY]
GO

 CREATE  INDEX [orderid] ON [sob]([orderid]) ON [PRIMARY]
GO

 CREATE  INDEX [xh] ON [sob]([xh]) ON [PRIMARY]
GO

 CREATE  INDEX [xddm] ON [sob]([xddm]) ON [PRIMARY]
GO

 CREATE  INDEX [mandm] ON [sob]([mandm]) ON [PRIMARY]
GO

 CREATE  INDEX [dcdm] ON [sob]([dcdm]) ON [PRIMARY]
GO

 CREATE  INDEX [yxdm] ON [sob]([yxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [shdm] ON [sob]([shdm]) ON [PRIMARY]
GO

 CREATE  INDEX [bxdm] ON [sob]([bxdm]) ON [PRIMARY]
GO


 CREATE  INDEX [dm] ON [socsta]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [socsta]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [sqyx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [sqyx]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [DM] ON [stbz]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [FID] ON [stbz]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [stcklx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [stcklx]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [stdw]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [stdw]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [MC] ON [stdw]([mc]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [stdw]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [DM] ON [stinspect]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [FID] ON [stinspect]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [dm] ON [stkw]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [stkw]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mc] ON [stkw]([mc]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [stkw]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [dm] ON [stlx]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [stlx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [MC] ON [stlx]([mc]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [stlx]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [DM] ON [stlx2]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [FID] ON [stlx2]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [DM] ON [stlx3]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [FID] ON [stlx3]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [stsp]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [F9] ON [stsp]([f9]) ON [PRIMARY]
GO

 CREATE  INDEX [F8] ON [stsp]([f8]) ON [PRIMARY]
GO

 CREATE  INDEX [lxdm] ON [stsp]([lxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [HH] ON [stsp]([hh]) ON [PRIMARY]
GO

 CREATE  INDEX [XH] ON [stsp]([xh]) ON [PRIMARY]
GO

 CREATE  INDEX [dwdm] ON [stsp]([dwdm]) ON [PRIMARY]
GO

 CREATE  INDEX [jmbj] ON [stsp]([jmbj]) ON [PRIMARY]
GO

 CREATE  INDEX [ty] ON [stsp]([ty]) ON [PRIMARY]
GO

 CREATE  INDEX [lckp] ON [stsp]([lckp]) ON [PRIMARY]
GO

 CREATE  INDEX [lckt] ON [stsp]([lckt]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [stspmx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [stspmx]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [F1] ON [stspmx]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [wldm] ON [stspmx]([wldm]) ON [PRIMARY]
GO

 CREATE  INDEX [wlmc] ON [stspmx]([wlmc]) ON [PRIMARY]
GO

 CREATE  INDEX [F9] ON [stspmx]([f9]) ON [PRIMARY]
GO

 CREATE  INDEX [rq] ON [stspmx]([rq]) ON [PRIMARY]
GO

 CREATE  INDEX [ZZYH] ON [stspmx]([zzyh]) ON [PRIMARY]
GO

 CREATE  INDEX [ckdm] ON [stspmx]([ckdm]) ON [PRIMARY]
GO

 CREATE  INDEX [refid] ON [stspmx]([refid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [stywlx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [stywlx]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [stzld]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [F1] ON [stzld]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [jc] ON [stzld]([jc]) ON [PRIMARY]
GO

 CREATE  INDEX [F9] ON [stzld]([f9]) ON [PRIMARY]
GO

 CREATE  INDEX [lxdm] ON [stzld]([lxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [dwdm] ON [stzld]([dwdm]) ON [PRIMARY]
GO

 CREATE  INDEX [HH] ON [stzld]([hh]) ON [PRIMARY]
GO

 CREATE  INDEX [srrq] ON [stzld]([srrq]) ON [PRIMARY]
GO

 CREATE  INDEX [ZZYH] ON [stzld]([zzyh]) ON [PRIMARY]
GO

 CREATE  INDEX [jmbj] ON [stzld]([jmbj]) ON [PRIMARY]
GO

 CREATE  INDEX [ckdm] ON [stzld]([ckdm]) ON [PRIMARY]
GO


 CREATE  INDEX [F1] ON [sw]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [SSHJ1] ON [sw]([sshj1]) ON [PRIMARY]
GO

 CREATE  INDEX [SSHJ2] ON [sw]([sshj2]) ON [PRIMARY]
GO

 CREATE  INDEX [SSHJ3] ON [sw]([sshj3]) ON [PRIMARY]
GO

 CREATE  INDEX [jhrq] ON [sw]([jhrq]) ON [PRIMARY]
GO

 CREATE  INDEX [HSDM] ON [sw]([hsdm]) ON [PRIMARY]
GO

 CREATE  INDEX [jtrq] ON [sw]([jtrq]) ON [PRIMARY]
GO

 CREATE  INDEX [hsfldm] ON [sw]([hsfldm]) ON [PRIMARY]
GO

 CREATE  INDEX [sinoywdm] ON [sw]([sinoywdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [swagtbk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [F1] ON [swagtbk]([f1]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [swchg]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [BF] ON [swchg]([bf]) ON [PRIMARY]
GO

 CREATE  INDEX [BK] ON [swchg]([bk]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [swdefa]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [lx] ON [swdefa]([lx]) ON [PRIMARY]
GO

 CREATE  INDEX [ywdm] ON [swdefa]([ywdm]) ON [PRIMARY]
GO

 CREATE  INDEX [xz] ON [swdefa]([xz]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [swex]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [swfp]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [ysfph] ON [swfp]([ysfph]) ON [PRIMARY]
GO

 CREATE  INDEX [expid] ON [swfp]([expid]) ON [PRIMARY]
GO

 CREATE  INDEX [xd] ON [swfp]([xd]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [swfp]([f1]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [swfpms]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [swfpms]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [ysfph] ON [swfpms]([ysfph]) ON [PRIMARY]
GO

 CREATE  INDEX [ysrq] ON [swfpms]([ysrq]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [swfpms]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [f29] ON [swfpms]([f29]) ON [PRIMARY]
GO

 CREATE  INDEX [f31] ON [swfpms]([f31]) ON [PRIMARY]
GO

 CREATE  INDEX [f10dm] ON [swfpms]([f10dm]) ON [PRIMARY]
GO

 CREATE  INDEX [f11dm] ON [swfpms]([f11dm]) ON [PRIMARY]
GO

 CREATE  INDEX [f12dm] ON [swfpms]([f12dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [swfpmx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [expid] ON [swfpmx]([expid]) ON [PRIMARY]
GO

 CREATE  INDEX [ysfph] ON [swfpmx]([ysfph]) ON [PRIMARY]
GO

 CREATE  INDEX [FPMXXH] ON [swfpmx]([fpmxxh]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [swjx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [swjx]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [swjx]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [yf] ON [swjx]([yf]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [swly]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [swly]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [swlyftz]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [xmdm] ON [swlyftz]([xmdm]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [swlyftz]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [swsf]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [F1] ON [swsf]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [fl] ON [swsf]([fl]) ON [PRIMARY]
GO

 CREATE  INDEX [ly] ON [swsf]([ly]) ON [PRIMARY]
GO

 CREATE  INDEX [YSFPH] ON [swsf]([ysfph]) ON [PRIMARY]
GO

 CREATE  INDEX [yshj] ON [swsf]([yshj]) ON [PRIMARY]
GO

 CREATE  INDEX [yskpbj] ON [swsf]([yskpbj]) ON [PRIMARY]
GO

 CREATE  INDEX [YSRQ] ON [swsf]([ysrq]) ON [PRIMARY]
GO

 CREATE  INDEX [ysbkxm] ON [swsf]([ysbkxm]) ON [PRIMARY]
GO

 CREATE  INDEX [sshj] ON [swsf]([sshj]) ON [PRIMARY]
GO

 CREATE  INDEX [SSSJ] ON [swsf]([sssj]) ON [PRIMARY]
GO

 CREATE  INDEX [ssbj] ON [swsf]([ssbj]) ON [PRIMARY]
GO

 CREATE  INDEX [ffrdm] ON [swsf]([ffrdm]) ON [PRIMARY]
GO

 CREATE  INDEX [GLR] ON [swsf]([glr]) ON [PRIMARY]
GO

 CREATE  INDEX [PZH] ON [swsf]([pzh]) ON [PRIMARY]
GO

 CREATE  INDEX [BZ8] ON [swsf]([bz8]) ON [PRIMARY]
GO

 CREATE  INDEX [zbcopy] ON [swsf]([zbcopy]) ON [PRIMARY]
GO

 CREATE  INDEX [zzbj] ON [swsf]([zzbj]) ON [PRIMARY]
GO

 CREATE  INDEX [glfdm] ON [swsf]([glfdm]) ON [PRIMARY]
GO

 CREATE  INDEX [shbj] ON [swsf]([shbj]) ON [PRIMARY]
GO

 CREATE  INDEX [SHRQ] ON [swsf]([shrq]) ON [PRIMARY]
GO

 CREATE  INDEX [ZHXGRQ] ON [swsf]([zhxgrq]) ON [PRIMARY]
GO

 CREATE  INDEX [biz] ON [swsf]([biz]) ON [PRIMARY]
GO

 CREATE  INDEX [sdr] ON [swsf]([sdr]) ON [PRIMARY]
GO

 CREATE  INDEX [GLZ] ON [swsf]([glz]) ON [PRIMARY]
GO

 CREATE  INDEX [DZH] ON [swsf]([dzh]) ON [PRIMARY]
GO

 CREATE  INDEX [fkdm] ON [swsf]([fkdm]) ON [PRIMARY]
GO

 CREATE  INDEX [ZZXZ] ON [swsf]([zzxz]) ON [PRIMARY]
GO

 CREATE  INDEX [fyqr] ON [swsf]([fyqr]) ON [PRIMARY]
GO

 CREATE  INDEX [hsyhdm] ON [swsf]([hsyhdm]) ON [PRIMARY]
GO

 CREATE  INDEX [hsxjdm] ON [swsf]([hsxjdm]) ON [PRIMARY]
GO

 CREATE  INDEX [glrid] ON [swsf]([glrid]) ON [PRIMARY]
GO

 CREATE  INDEX [glra] ON [swsf]([glra]) ON [PRIMARY]
GO

 CREATE  INDEX [glzid] ON [swsf]([glzid]) ON [PRIMARY]
GO

 CREATE  INDEX [glraid] ON [swsf]([glraid]) ON [PRIMARY]
GO

 CREATE  INDEX [xf1] ON [swsf]([xf1]) ON [PRIMARY]
GO

 CREATE  INDEX [OCL] ON [swsf]([ocl]) ON [PRIMARY]
GO

 CREATE  INDEX [dxbj] ON [swsf]([dxbj]) ON [PRIMARY]
GO

 CREATE  INDEX [ddfid] ON [swsf]([ddfid]) ON [PRIMARY]
GO

 CREATE  INDEX [fpzf] ON [swsf]([fpzf]) ON [PRIMARY]
GO

 CREATE  INDEX [qrrq] ON [swsf]([qrrq]) ON [PRIMARY]
GO

 CREATE  INDEX [cdno] ON [swsf]([cdno]) ON [PRIMARY]
GO

 CREATE  INDEX [zdex] ON [swsf]([zdex]) ON [PRIMARY]
GO

 CREATE  INDEX [expid] ON [swsf]([expid]) ON [PRIMARY]
GO

 CREATE  INDEX [sds] ON [swsf]([sds]) ON [PRIMARY]
GO

 CREATE  INDEX [bll] ON [swsf]([bll]) ON [PRIMARY]
GO

 CREATE  INDEX [tbfid] ON [swsf]([tbfid]) ON [PRIMARY]
GO


 CREATE  INDEX [F1] ON [swsv]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [swsv]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [tcfs]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [tcfs]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [tdff]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [tdff]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [dm] ON [tdlx]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [tdlx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mc] ON [tdlx]([mc]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [tdlx]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [tdlxdz]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [tdlxdz]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [MC] ON [tdlxdz]([mc]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [tdlxdz]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [dm] ON [tdlxyj]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [tdlxyj]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [MC] ON [tdlxyj]([mc]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [tdlxyj]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [tidanb]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [NO] ON [tidanb]([no]) ON [PRIMARY]
GO

 CREATE  INDEX [TDMC] ON [tidanb]([tdmc]) ON [PRIMARY]
GO

 CREATE  INDEX [TDZL] ON [tidanb]([tdzl]) ON [PRIMARY]
GO

 CREATE  INDEX [TDPRG] ON [tidanb]([tdprg]) ON [PRIMARY]
GO

 CREATE  INDEX [FAXTO] ON [tidanb]([faxto]) ON [PRIMARY]
GO

 CREATE  INDEX [BZ] ON [tidanb]([bz]) ON [PRIMARY]
GO

 CREATE  INDEX [LX] ON [tidanb]([lx]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [tidanbb]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [bbdm] ON [tidanbb]([bbdm]) ON [PRIMARY]
GO

 CREATE  INDEX [BBLX] ON [tidanbb]([bblx]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [tidanft]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [no] ON [tidanft]([no]) ON [PRIMARY]
GO

 CREATE  INDEX [FAXTO] ON [tidanft]([faxto]) ON [PRIMARY]
GO

 CREATE  INDEX [FAXEXP] ON [tidanft]([faxexp]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [tidanft]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [tidangd]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [tidansql]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mc] ON [tidansql]([mc]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [tidanyy]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [app_m] ON [tidanyy]([app_m]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [tkhfs]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [tkhfs]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [tmlx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [tmlx]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [tplx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [tplx]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [userkey]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [KEYLABEL] ON [userkey]([keylabel]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [userkey]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [KEY_] ON [userkey]([key_]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [userkey]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [bbdm] ON [userrep]([bbdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [users1]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f9] ON [users1]([f9]) ON [PRIMARY]
GO

 CREATE  INDEX [f8] ON [users1]([f8]) ON [PRIMARY]
GO

 CREATE  INDEX [ztdm] ON [users1]([ztdm]) ON [PRIMARY]
GO

 CREATE  INDEX [xzdm] ON [users1]([xzdm]) ON [PRIMARY]
GO

 CREATE  INDEX [gmdm] ON [users1]([gmdm]) ON [PRIMARY]
GO

 CREATE  INDEX [hydm] ON [users1]([hydm]) ON [PRIMARY]
GO

 CREATE  INDEX [dqdm] ON [users1]([dqdm]) ON [PRIMARY]
GO

 CREATE  INDEX [lydm] ON [users1]([lydm]) ON [PRIMARY]
GO

 CREATE  INDEX [fwdm] ON [users1]([fwdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [wjcurr]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [c_symb] ON [wjcurr]([c_symb]) ON [PRIMARY]
GO

 CREATE  INDEX [NAME] ON [wjcurr]([name]) ON [PRIMARY]
GO

 CREATE  INDEX [RATE] ON [wjcurr]([rate]) ON [PRIMARY]
GO

 CREATE  INDEX [cdmk] ON [wjcurr]([cdmk]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [wjcurr]([sm]) ON [PRIMARY]
GO

 CREATE  INDEX [RATE_USD] ON [wjcurr]([rate_usd]) ON [PRIMARY]
GO

 CREATE  INDEX [cdmk_usd] ON [wjcurr]([cdmk_usd]) ON [PRIMARY]
GO

 CREATE  INDEX [code] ON [wjcurr]([code]) ON [PRIMARY]
GO

 CREATE  INDEX [YF] ON [wjcurr]([yf]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [wjgods]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [code] ON [wjgods]([code]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [wjgods]([sm]) ON [PRIMARY]
GO

 CREATE  INDEX [NAME] ON [wjgods]([name]) ON [PRIMARY]
GO


 CREATE  INDEX [code] ON [wjgodsa]([code]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [wjgodsa]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [NAME] ON [wjgodsa]([name]) ON [PRIMARY]
GO


 CREATE  INDEX [code] ON [wjjgtk]([code]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [wjjgtk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [name] ON [wjjgtk]([name]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [wjjgtk]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [wjmc]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [wjmc]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [F1] ON [wjmywk]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [YSFS] ON [wjmywk]([ysfs]) ON [PRIMARY]
GO

 CREATE  INDEX [gods] ON [wjmywk]([gods]) ON [PRIMARY]
GO

 CREATE  INDEX [mtdm2] ON [wjmywk]([mtdm2]) ON [PRIMARY]
GO

 CREATE  INDEX [jcgsdm] ON [wjmywk]([jcgsdm]) ON [PRIMARY]
GO

 CREATE  INDEX [wjfdm] ON [wjmywk]([wjfdm]) ON [PRIMARY]
GO

 CREATE  INDEX [bdh] ON [wjmywk]([bdh]) ON [PRIMARY]
GO

 CREATE  INDEX [jc] ON [wjmywk]([jc]) ON [PRIMARY]
GO

 CREATE  INDEX [YAP] ON [wjmywk]([yap]) ON [PRIMARY]
GO

 CREATE  INDEX [YWC] ON [wjmywk]([ywc]) ON [PRIMARY]
GO

 CREATE  INDEX [cjdm] ON [wjmywk]([cjdm]) ON [PRIMARY]
GO

 CREATE  INDEX [jgsg] ON [wjmywk]([jgsg]) ON [PRIMARY]
GO

 CREATE  INDEX [mtqydm] ON [wjmywk]([mtqydm]) ON [PRIMARY]
GO

 CREATE  INDEX [MDQYDM] ON [wjmywk]([mdqydm]) ON [PRIMARY]
GO

 CREATE  INDEX [mtqydm2] ON [wjmywk]([mtqydm2]) ON [PRIMARY]
GO

 CREATE  INDEX [MDQYDM2] ON [wjmywk]([mdqydm2]) ON [PRIMARY]
GO

 CREATE  INDEX [fsrq] ON [wjmywk]([fsrq]) ON [PRIMARY]
GO

 CREATE  INDEX [dcdm] ON [wjmywk]([dcdm]) ON [PRIMARY]
GO

 CREATE  INDEX [ytdm] ON [wjmywk]([ytdm]) ON [PRIMARY]
GO

 CREATE  INDEX [gzfid] ON [wjmywk]([gzfid]) ON [PRIMARY]
GO

 CREATE  INDEX [jhs] ON [wjmywk]([jhs]) ON [PRIMARY]
GO

 CREATE  INDEX [wcs] ON [wjmywk]([wcs]) ON [PRIMARY]
GO

 CREATE  INDEX [sxrq] ON [wjmywk]([sxrq]) ON [PRIMARY]
GO


 CREATE  INDEX [code] ON [wjport]([code]) ON [PRIMARY]
GO

 CREATE  INDEX [NAME] ON [wjport]([name]) ON [PRIMARY]
GO

 CREATE  INDEX [ENAME] ON [wjport]([ename]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [wjport]([sm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [wjport]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [code] ON [wjporta]([code]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [wjporta]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [country] ON [wjporta]([country]) ON [PRIMARY]
GO


 CREATE  INDEX [code] ON [wjspco]([code]) ON [PRIMARY]
GO

 CREATE  INDEX [NAME] ON [wjspco]([name]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [wjspco]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [wjspco]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [wjspco]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [CODE] ON [wjtrade]([code]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [wjtrade]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [NAME] ON [wjtrade]([name]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [wjtrade]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [wjtransf]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [CODE] ON [wjtransf]([code]) ON [PRIMARY]
GO

 CREATE  INDEX [NAME] ON [wjtransf]([name]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [wjtransf]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [code] ON [wjzyfs]([code]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [wjzyfs]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [wjzyfs]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [worker]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [worker]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [worker]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [lx] ON [worker]([lx]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [wsb]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [wsb]([f1]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [xg_bx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [xg_bx]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [y1] ON [xg_bx]([y1]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [xg_dz]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [xg_dz]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [xg_gz]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [xg_gz]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [xg_gz]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [y1] ON [xg_gz]([y1]) ON [PRIMARY]
GO

 CREATE  INDEX [rq] ON [xg_gz]([rq]) ON [PRIMARY]
GO

 CREATE  INDEX [sj] ON [xg_gz]([sj]) ON [PRIMARY]
GO

 CREATE  INDEX [DDM] ON [xg_gz]([ddm]) ON [PRIMARY]
GO

 CREATE  INDEX [dzdm] ON [xg_gz]([dzdm]) ON [PRIMARY]
GO

 CREATE  INDEX [f31] ON [xg_gz]([f31]) ON [PRIMARY]
GO

 CREATE  INDEX [f29] ON [xg_gz]([f29]) ON [PRIMARY]
GO

 CREATE  INDEX [LXDM] ON [xg_gz]([lxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [YSDM] ON [xg_gz]([ysdm]) ON [PRIMARY]
GO

 CREATE  INDEX [YTDM] ON [xg_gz]([ytdm]) ON [PRIMARY]
GO

 CREATE  INDEX [xgf1] ON [xg_gz]([xgf1]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [xg_tj]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [xg_tj]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [xg_xa]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [xg_xa]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [xg_xg]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [xg_xg]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [xg_xly]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [DM] ON [xg_xly]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [xg_xq]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [xxbz] ON [xg_xq]([xxbz]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [xg_xq]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [xg_xr]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [xg_xr]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [xg_xt]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [y1] ON [xg_xt]([y1]) ON [PRIMARY]
GO

 CREATE  INDEX [lckp] ON [xg_xt]([lckp]) ON [PRIMARY]
GO

 CREATE  INDEX [lckt] ON [xg_xt]([lckt]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [xg_xw]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [xg_xw]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [xg_yt]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [xg_yt]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [xg_zq]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [xg_zq]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [xxbz] ON [xg_zq]([xxbz]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [xhfs]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [xhfs]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [xj]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [rq] ON [xj]([rq]) ON [PRIMARY]
GO

 CREATE  INDEX [sw] ON [xj]([sw]) ON [PRIMARY]
GO

 CREATE  INDEX [wtf2dm] ON [xj]([wtf2dm]) ON [PRIMARY]
GO

 CREATE  INDEX [glr] ON [xj]([glr]) ON [PRIMARY]
GO

 CREATE  INDEX [glrid] ON [xj]([glrid]) ON [PRIMARY]
GO

 CREATE  INDEX [bm] ON [xj]([bm]) ON [PRIMARY]
GO

 CREATE  INDEX [bmid] ON [xj]([bmid]) ON [PRIMARY]
GO

 CREATE  INDEX [glra] ON [xj]([glra]) ON [PRIMARY]
GO

 CREATE  INDEX [glraid] ON [xj]([glraid]) ON [PRIMARY]
GO

 CREATE  INDEX [gkdm] ON [xj]([gkdm]) ON [PRIMARY]
GO

 CREATE  INDEX [hxdm] ON [xj]([hxdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [xmwz]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [xmwz]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [xsjs]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [xsjs]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [xtmc]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [DM] ON [xtmc]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [xxdmk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [xxdmk]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [mc] ON [xxdmk]([mc]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [xxdmk]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [dm] ON [xxdxk]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [xxdxk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mc] ON [xxdxk]([mc]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [xxdxk]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [dm] ON [xxfjk]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [xxfjk]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [xxpzk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dx] ON [xxpzk]([dx]) ON [PRIMARY]
GO

 CREATE  INDEX [xx] ON [xxpzk]([xx]) ON [PRIMARY]
GO

 CREATE  INDEX [pz] ON [xxpzk]([pz]) ON [PRIMARY]
GO

 CREATE  INDEX [TEU] ON [xxpzk]([teu]) ON [PRIMARY]
GO

 CREATE  INDEX [xl] ON [xxpzk]([xl]) ON [PRIMARY]
GO

 CREATE  INDEX [xw] ON [xxpzk]([xw]) ON [PRIMARY]
GO

 CREATE  INDEX [xh] ON [xxpzk]([xh]) ON [PRIMARY]
GO

 CREATE  INDEX [cu] ON [xxpzk]([cu]) ON [PRIMARY]
GO

 CREATE  INDEX [cp] ON [xxpzk]([cp]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [xxpzk]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [xxztk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [xxztk]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [xyzlx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [xyzlx]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [mc] ON [xyzlx]([mc]) ON [PRIMARY]
GO

 CREATE  INDEX [sm] ON [xyzlx]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [dm] ON [xztdm]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [fid] ON [xztdm]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ycdbz]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [ycdbz]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [mc] ON [ycdbz]([mc]) ON [PRIMARY]
GO

 CREATE  INDEX [sm] ON [ycdbz]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [yftkk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [YFTK] ON [yftkk]([yftk]) ON [PRIMARY]
GO

 CREATE  INDEX [SM] ON [yftkk]([sm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [yhty]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [yh] ON [yhty]([yh]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [yhzh]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [yhzh1]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [bzid] ON [yhzh1]([bzid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [yjhl]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f9] ON [yjhl]([f9]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [yjhlb]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f9] ON [yjhlb]([f9]) ON [PRIMARY]
GO

 CREATE  INDEX [lxdm] ON [yjhlb]([lxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [yjhlb]([mfid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [yqlx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [yqlx]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ysfs]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [DM] ON [ysfs]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ywdmk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [DM] ON [ywdmk]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ywjzk]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [ywdm] ON [ywjzk]([ywdm]) ON [PRIMARY]
GO

 CREATE  INDEX [JZM1] ON [ywjzk]([jzm1]) ON [PRIMARY]
GO

 CREATE  INDEX [JZM2] ON [ywjzk]([jzm2]) ON [PRIMARY]
GO

 CREATE  INDEX [JZM3] ON [ywjzk]([jzm3]) ON [PRIMARY]
GO

 CREATE  INDEX [JZM4] ON [ywjzk]([jzm4]) ON [PRIMARY]
GO

 CREATE  INDEX [JZM5] ON [ywjzk]([jzm5]) ON [PRIMARY]
GO

 CREATE  INDEX [JZM6] ON [ywjzk]([jzm6]) ON [PRIMARY]
GO

 CREATE  INDEX [JZM7] ON [ywjzk]([jzm7]) ON [PRIMARY]
GO

 CREATE  INDEX [JZM8] ON [ywjzk]([jzm8]) ON [PRIMARY]
GO

 CREATE  INDEX [JZM9] ON [ywjzk]([jzm9]) ON [PRIMARY]
GO

 CREATE  INDEX [lx] ON [ywjzk]([lx]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ywsj]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [F1] ON [ywsj]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [jgdm] ON [ywsj]([jgdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ywsy]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [fm1] ON [ywsy]([fm1]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [ywsy]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [cgsdm] ON [ywsy]([cgsdm]) ON [PRIMARY]
GO

 CREATE  INDEX [f10dm] ON [ywsy]([f10dm]) ON [PRIMARY]
GO

 CREATE  INDEX [f12dm] ON [ywsy]([f12dm]) ON [PRIMARY]
GO

 CREATE  INDEX [pc2] ON [ywsy]([pc2]) ON [PRIMARY]
GO


 CREATE  INDEX [F1] ON [ywzl]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [xh] ON [ywzl]([xh]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ywzt]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [ywzt]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [ywzy]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [ywzy]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [ywzy]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [yxfs]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [yxfs]([dm]) ON [PRIMARY]
GO

 CREATE  INDEX [mc] ON [yxfs]([mc]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [zcsj]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [F1] ON [zcsj]([f1]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [zcsjc]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [zcsjc]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [cksj] ON [zcsjc]([cksj]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [zgcl]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [zgcl]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [zlbflx]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [zlbfly]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [zxcd]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [zxcd]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [ysf1] ON [zxcd]([ysf1]) ON [PRIMARY]
GO

 CREATE  INDEX [xf1] ON [zxcd]([xf1]) ON [PRIMARY]
GO

 CREATE  INDEX [DEL] ON [zxcd]([del]) ON [PRIMARY]
GO

 CREATE  INDEX [QR] ON [zxcd]([qr]) ON [PRIMARY]
GO

 CREATE  INDEX [rq] ON [zxcd]([rq]) ON [PRIMARY]
GO

 CREATE  INDEX [lxdm] ON [zxcd]([lxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [ytx] ON [zxcd]([ytx]) ON [PRIMARY]
GO

 CREATE  INDEX [yzx] ON [zxcd]([yzx]) ON [PRIMARY]
GO

 CREATE  INDEX [ycc] ON [zxcd]([ycc]) ON [PRIMARY]
GO

 CREATE  INDEX [yxx] ON [zxcd]([yxx]) ON [PRIMARY]
GO

 CREATE  INDEX [txdwdm] ON [zxcd]([txdwdm]) ON [PRIMARY]
GO

 CREATE  INDEX [zhdwdm] ON [zxcd]([zhdwdm]) ON [PRIMARY]
GO

 CREATE  INDEX [xhdwdm] ON [zxcd]([xhdwdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [zxd]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [zxd]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [xf1] ON [zxd]([xf1]) ON [PRIMARY]
GO

 CREATE  INDEX [y1] ON [zxd]([y1]) ON [PRIMARY]
GO

 CREATE  INDEX [y2] ON [zxd]([y2]) ON [PRIMARY]
GO

 CREATE  INDEX [ZXDNO] ON [zxd]([zxdno]) ON [PRIMARY]
GO

 CREATE  INDEX [zxrq] ON [zxd]([zxrq]) ON [PRIMARY]
GO

 CREATE  INDEX [zxsj] ON [zxd]([zxsj]) ON [PRIMARY]
GO

 CREATE  INDEX [jgrq] ON [zxd]([jgrq]) ON [PRIMARY]
GO

 CREATE  INDEX [jgsj] ON [zxd]([jgsj]) ON [PRIMARY]
GO

 CREATE  INDEX [xzdm] ON [zxd]([xzdm]) ON [PRIMARY]
GO

 CREATE  INDEX [cddm] ON [zxd]([cddm]) ON [PRIMARY]
GO

 CREATE  INDEX [shcddm] ON [zxd]([shcddm]) ON [PRIMARY]
GO

 CREATE  INDEX [tzrq] ON [zxd]([tzrq]) ON [PRIMARY]
GO

 CREATE  INDEX [tzsj] ON [zxd]([tzsj]) ON [PRIMARY]
GO

 CREATE  INDEX [sdrq] ON [zxd]([sdrq]) ON [PRIMARY]
GO

 CREATE  INDEX [sdsj] ON [zxd]([sdsj]) ON [PRIMARY]
GO

 CREATE  INDEX [xyjzxrq] ON [zxd]([xyjzxrq]) ON [PRIMARY]
GO

 CREATE  INDEX [pcbj] ON [zxd]([pcbj]) ON [PRIMARY]
GO

 CREATE  INDEX [mtdm] ON [zxd]([mtdm]) ON [PRIMARY]
GO

 CREATE  INDEX [mtdm2] ON [zxd]([mtdm2]) ON [PRIMARY]
GO

 CREATE  INDEX [mtdm1] ON [zxd]([mtdm1]) ON [PRIMARY]
GO

 CREATE  INDEX [fzdm] ON [zxd]([fzdm]) ON [PRIMARY]
GO

 CREATE  INDEX [dzdm] ON [zxd]([dzdm]) ON [PRIMARY]
GO

 CREATE  INDEX [yap] ON [zxd]([yap]) ON [PRIMARY]
GO

 CREATE  INDEX [ywc] ON [zxd]([ywc]) ON [PRIMARY]
GO

 CREATE  INDEX [yap2] ON [zxd]([yap2]) ON [PRIMARY]
GO

 CREATE  INDEX [pcbj2] ON [zxd]([pcbj2]) ON [PRIMARY]
GO

 CREATE  INDEX [ywc2] ON [zxd]([ywc2]) ON [PRIMARY]
GO

 CREATE  INDEX [ckfid] ON [zxd]([ckfid]) ON [PRIMARY]
GO

 CREATE  INDEX [jkfid] ON [zxd]([jkfid]) ON [PRIMARY]
GO

 CREATE  INDEX [mtqydm] ON [zxd]([mtqydm]) ON [PRIMARY]
GO

 CREATE  INDEX [MDQYDM] ON [zxd]([mdqydm]) ON [PRIMARY]
GO

 CREATE  INDEX [yszdjs] ON [zxd]([yszdjs]) ON [PRIMARY]
GO

 CREATE  INDEX [yfzdjs] ON [zxd]([yfzdjs]) ON [PRIMARY]
GO

 CREATE  INDEX [mtqydm2] ON [zxd]([mtqydm2]) ON [PRIMARY]
GO

 CREATE  INDEX [MDQYDM2] ON [zxd]([mdqydm2]) ON [PRIMARY]
GO

 CREATE  INDEX [tcfsdm2] ON [zxd]([tcfsdm2]) ON [PRIMARY]
GO

 CREATE  INDEX [ckkxcc] ON [zxd]([ckkxcc]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [zxd0]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [zxdfid] ON [zxd0]([zxdfid]) ON [PRIMARY]
GO

 CREATE  INDEX [F1] ON [zxd0]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [F31] ON [zxd0]([f31]) ON [PRIMARY]
GO

 CREATE  INDEX [JCBH] ON [zxd0]([jcbh]) ON [PRIMARY]
GO

 CREATE  INDEX [mtqr] ON [zxd0]([mtqr]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [zxd1]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [F1] ON [zxd1]([f1]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [zxd2]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [F1] ON [zxd2]([f1]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [zxd3]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [F1] ON [zxd3]([f1]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [zxd4]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [F1] ON [zxd4]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [YSF1] ON [zxd4]([ysf1]) ON [PRIMARY]
GO

 CREATE  INDEX [shcddm] ON [zxd4]([shcddm]) ON [PRIMARY]
GO

 CREATE  INDEX [zxdm] ON [zxd4]([zxdm]) ON [PRIMARY]
GO

 CREATE  INDEX [zhdwdm] ON [zxd4]([zhdwdm]) ON [PRIMARY]
GO

 CREATE  INDEX [xhdwdm] ON [zxd4]([xhdwdm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [zxd5]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [zxd5]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [pno] ON [zxd5]([pno]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [zxd6]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [mfid] ON [zxd6]([mfid]) ON [PRIMARY]
GO

 CREATE  INDEX [F9] ON [zxd6]([f9]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [zxd7]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [zxd8]([fid]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [zxf]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dx] ON [zxf]([dx]) ON [PRIMARY]
GO

 CREATE  INDEX [xx] ON [zxf]([xx]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [zxgz]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [f1] ON [zxgz]([f1]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [zxhx]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [F1] ON [zxhx]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [dx] ON [zxhx]([dx]) ON [PRIMARY]
GO

 CREATE  INDEX [xx] ON [zxhx]([xx]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [zxsh]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [F1] ON [zxsh]([f1]) ON [PRIMARY]
GO

 CREATE  INDEX [shcddm] ON [zxsh]([shcddm]) ON [PRIMARY]
GO

 CREATE  INDEX [shdbh] ON [zxsh]([shdbh]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [zyxm]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [dm] ON [zyxm]([dm]) ON [PRIMARY]
GO


 CREATE  INDEX [fid] ON [zzflb]([fid]) ON [PRIMARY]
GO

 CREATE  INDEX [cgsdm] ON [zzflb]([cgsdm]) ON [PRIMARY]
GO

 CREATE  INDEX [cgsmc] ON [zzflb]([cgsmc]) ON [PRIMARY]
GO

 CREATE  INDEX [gkdm] ON [zzflb]([gkdm]) ON [PRIMARY]
GO

 CREATE  INDEX [GKQC] ON [zzflb]([gkqc]) ON [PRIMARY]
GO

 CREATE  INDEX [HXDM] ON [zzflb]([hxdm]) ON [PRIMARY]
GO


