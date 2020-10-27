--拼箱
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[cksykpx]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[cksykpx]
GO

CREATE VIEW dbo.cksykpx  
AS  
SELECT cksyk.f1,cksyk.nid,cksyk.fm1,cksyk.fm2,cksyk.YWDM,cksyk.TGBZ,cksyk.MZ,cksyk.JS,cksyk.TJ,cksyk.F31,
	dbo.f_get_first_subjob(cksyk.f1,cksyk.fm1) as zhf1,
	CKSYK.F29,cksyk.f11,cksyk.wtf2,dbj2.ypcq,cksyk.f32,cksyk.pc4 --首个子号
FROM dbo.cksyk left join dbj2 on cksyk.f1 = dbj2.f1 
GO

--订单拼箱
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[posykpx]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[posykpx]
GO

CREATE VIEW dbo.posykpx  
AS  
SELECT posyk.pono,posyk.nid,posyk.pofm1
FROM dbo.posyk
GO


--并单
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[cksykbd]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[cksykbd]
GO

CREATE VIEW dbo.cksykbd  
AS  
SELECT f1,nid,fm2,YWDM,TGBZ,MZ,JS,TJ,F31
FROM dbo.cksyk  
--order by f1 不能使用order by
GO


--分单 暂不用
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[cksykfd]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[cksykfd]
GO

CREATE VIEW dbo.cksykfd  
AS  
SELECT f1,nid,fm3,YWDM,TGBZ,MZ,JS,TJ 
FROM dbo.cksyk  
--order by f1 不能使用order by
GO

--工资 当前月
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[gzk]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[gzk]
GO

CREATE VIEW dbo.gzk
AS
SELECT *
FROM dbo.gz
WHERE (lx = ' ')
GO

--工资 历史
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[gzkbak]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[gzkbak]
GO

CREATE VIEW dbo.gzkbak
AS
SELECT *
FROM dbo.gz
WHERE (lx = '*')
GO

--专项 一对一
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swbck]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swbck]
GO

CREATE VIEW dbo.swbck  
AS  
SELECT nid as nid, f1 AS f1, yshj3 AS YSHJ, sshj3 AS SSHJ, yfbfhj3 AS YFBFHJ, yfsjze3 AS YFSJZE,   
      bizhong3 AS BIZHONG, lr3 AS LR, srbj3 AS SRBJ, yskpbj3 AS YSKPBJ,   
      ssbj3 AS SSBJ, bz3 AS BZ, ystz3 as ystz, yftz3 as yftz, ltz3 as ltz    
FROM dbo.sw  

GO


--专项 应付
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swbckbf]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swbckbf]
GO

CREATE VIEW dbo.swbckbf  
AS  
select *, 
	 ysfph as yfbffph, yshj as yfbfje, xmh1 as xmdm, sfnr1 as yfbfxm, sshj as sfbfje, sssj as yfbfrq, 
	 ssbj as yffkbj, ffrdm as sfrdm, ffr as sfr, sl1 as sl, ysrq as dzrq, yskpbj as dzbj
FROM dbo.swsf   
WHERE (FL = '-' and LY = '3')   
GO

--专项 应收
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swbckbk]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swbckbk]
GO

CREATE VIEW dbo.swbckbk  
AS  
SELECT *  
FROM dbo.swsf 
WHERE (FL = '+' and LY = '3')  
GO

--专项应付
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swbf]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swbf]
GO

CREATE VIEW dbo.swbf  
AS  
select *, 
	 ysfph as yfbffph, yshj as yfbfje, xmh1 as xmdm, sfnr1 as yfbfxm, sshj as sfbfje, sssj as yfbfrq, 
	 ssbj as yffkbj, ffrdm as sfrdm, ffr as sfr, sl1 as sl, ysrq as dzrq, yskpbj as dzbj
FROM dbo.swsf   
WHERE (FL = '-')   
GO


--应收
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swbk]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swbk]
GO

CREATE VIEW dbo.swbk  
AS  
SELECT *  
FROM dbo.swsf 
WHERE (FL = '+')  
GO

--RMB 一对一
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swrmb]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swrmb]
GO

CREATE VIEW dbo.swrmb  
AS  
SELECT nid, f1, yshj2 AS YSHJ, sshj2 AS SSHJ, yfbfhj2 AS YFBFHJ, yfsjze2 AS YFSJZE,  
      bizhong2 AS BIZHONG, lr2 AS LR, srbj2 AS SRBJ, yskpbj2 AS YSKPBJ,  
      ssbj2 AS SSBJ, bz2 AS BZ, ystz2 as ystz, yftz2 as yftz, ltz2 as ltz   
FROM dbo.sw 

GO

--RMB 应付
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swrmbbf]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swrmbbf]
GO

CREATE VIEW dbo.swrmbbf  
AS  
select *, 
	 ysfph as yfbffph, yshj as yfbfje, xmh1 as xmdm, sfnr1 as yfbfxm, sshj as sfbfje, sssj as yfbfrq, 
	 ssbj as yffkbj, ffrdm as sfrdm, ffr as sfr, sl1 as sl, ysrq as dzrq, yskpbj as dzbj
FROM dbo.swsf   
WHERE (FL = '-' and LY = '2')   
GO

--RMB 应收
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swrmbbk]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swrmbbk]
GO

CREATE VIEW dbo.swrmbbk  
AS  
SELECT *  
FROM dbo.swsf 
WHERE (FL = '+' and LY = '2')  
GO


--设置视图 sw 自动计算部分
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swst]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swst]
GO

CREATE VIEW dbo.swst  
AS  
SELECT nid, f1, xsje, cgje, mlje, ssum, fsum, lsum, sea_e_f, sea_e_l, sea_i_f, sea_i_l, 
      air_e, air_i, mljh, ysjh, yfjh, stzsum, ftzsum, ltzsum,yskpbj1,yskpbj2,yskpbj3,yskpbj4,
      jxs, xxs, yns, jxj, xxj, mlj, lx, tx, mlcx, mljs  
FROM dbo.sw
GO

--设置View swc change part
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swc]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swc]
GO

CREATE VIEW dbo.swc  
AS  
SELECT nid, f1, bz1, bz2, bz3, jhqr, jhrq, hsdm, hsmc, jtje, jtrq, jhzz, tzzz,
	 hsfldm, hsflmc,jshl,zt,sinoywdm,sinoywlx, xmjt, xmrq 
FROM dbo.sw
GO

--设置视图 swwtf 自动计算客户部分 
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swwtf]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swwtf]
GO

CREATE VIEW dbo.swwtf  
AS  
SELECT nid, f1,  CAST(dbo.f_get_wtf(SW.f1,'SWUSDBK','YSRMB') AS numeric(19,2)) as khys1rmb, --应收USD折RMB
	CAST(dbo.f_get_wtf(SW.f1,'SWUSDBK','YSHJ') AS numeric(19,2)) as khys1, --应收金额
	CAST(dbo.f_get_wtf(SW.f1,'SWRMBBK','YSHJ') AS numeric(19,2)) as khys2,
	CAST(dbo.f_get_wtf(SW.f1,'SWBCKBK','YSHJ') AS numeric(19,2)) as khys3,
	CAST(dbo.f_get_wtf(SW.f1,'SWUSDBK','SSHJ') AS numeric(19,2)) as khss1, --实收金额
	CAST(dbo.f_get_wtf(SW.f1,'SWRMBBK','SSHJ') AS numeric(19,2)) as khss2,
	CAST(dbo.f_get_wtf(SW.f1,'SWBCKBK','SSHJ') AS numeric(19,2)) as khss3,
	dbo.f_get_wtf(SW.f1,'SWUSDBK','YSFPH') as khfph1, --实收日期
	dbo.f_get_wtf(SW.f1,'SWRMBBK','YSFPH') as khfph2,
	dbo.f_get_wtf(SW.f1,'SWBCKBK','YSFPH') as khfph3,
	dbo.f_get_wtf(SW.f1,'SWUSDBK','SSSJ') as khsssj1, --实收日期
	dbo.f_get_wtf(SW.f1,'SWRMBBK','SSSJ') as khsssj2,
	dbo.f_get_wtf(SW.f1,'SWBCKBK','SSSJ') as khsssj3,
	60 as mxts, --dbo.f_cal_rest_mxts(SW.f1)
	dbo.f_cal_rest_job(SW.f1,'000101',dbo.f_mydate1(dbo.f_getdate()),'USD') as lx1,
	dbo.f_cal_rest_job(SW.f1,'000101',dbo.f_mydate1(dbo.f_getdate()),'RMB') as lx2,
	dbo.f_cal_rest_job(SW.f1,'000101',dbo.f_mydate1(dbo.f_getdate()),'BCK') as lx3,
	dbo.f_cal_rest_job(SW.f1,'000101',dbo.f_mydate1(dbo.f_getdate()),'') as lx
FROM dbo.sw
GO

--USD 一对一
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swusd]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swusd]
GO

CREATE VIEW dbo.swusd  
AS  
SELECT nid,f1, yshj1 AS YSHJ, sshj1 AS SSHJ, yfbfhj1 AS YFBFHJ, yfsjze1 AS YFSJZE,  
      bizhong1 AS BIZHONG, lr1 AS LR, srbj1 AS SRBJ, yskpbj1 AS YSKPBJ,  
      ssbj1 AS SSBJ, bz1 AS BZ, ystz1 as ystz, yftz1 as yftz, ltz1 as ltz 
FROM dbo.sw 

GO

--USD 应付
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swusdbf]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swusdbf]
GO

CREATE VIEW dbo.swusdbf  
AS  
select *, 
	 ysfph as yfbffph, yshj as yfbfje, xmh1 as xmdm, sfnr1 as yfbfxm, sshj as sfbfje, sssj as yfbfrq, 
	 ssbj as yffkbj, ffrdm as sfrdm, ffr as sfr, sl1 as sl, ysrq as dzrq, yskpbj as dzbj
FROM dbo.swsf   
WHERE (FL = '-' and LY = '1')   
GO


--USD、RMB 应付
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swuarbf]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swuarbf]
GO

CREATE VIEW dbo.swuarbf  
AS  
select *, 
	 ysfph as yfbffph, yshj as yfbfje, xmh1 as xmdm, sfnr1 as yfbfxm, sshj as sfbfje, sssj as yfbfrq, 
	 ssbj as yffkbj, ffrdm as sfrdm, ffr as sfr, sl1 as sl, ysrq as dzrq, yskpbj as dzbj
FROM dbo.swsf   
WHERE (FL = '-' and (LY = '1' or ly='2'))   

GO

--USD 应收
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swusdbk]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swusdbk]
GO

CREATE VIEW dbo.swusdbk  
AS  
SELECT *  
FROM dbo.swsf 
WHERE (FL = '+' and LY = '1')  
GO


--USD、RMB 应收
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swuarbk]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swuarbk]
GO

CREATE VIEW dbo.swuarbk  
AS  
SELECT *  
FROM dbo.swsf 
WHERE (FL = '+' and (LY = '1' or LY='2'))  
GO

--单证 不包括停用的格式
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tidan]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[tidan]
GO
CREATE VIEW tidan AS SELECT [nid], [fid], [no], [tdmc], [tdzl], [bz], [tdprg], [faxto], [faxexp], [ccto], [ccexp], [lx], [qy],
	 [jkcx],[bbbeg],[bbprg],[rptrq],[rptsj],[app_m],[dxnr],[cy],[dyl]
	from tidanb WHERE QY=1 
GO

--单证 系统
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tidanst]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[tidanst]
GO
CREATE VIEW tidanst AS SELECT [nid], [fid], [no], [tdmc], [tdzl], [bz], [tdprg], [faxto], [faxexp], [ccto], [ccexp], [lx], [qy], 
	[jkcx],[bbbeg],[bbprg],[rptrq],[rptsj],[app_m],[dxnr],[cy],[dyl] 
	from tidanb WHERE lx='系统' and app_m='传统单证'
GO

--单证 不包括报表image的View
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tidanbn]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[tidanbn]
GO
CREATE VIEW tidanbn AS SELECT [nid], [fid], [no], [tdmc], [tdzl], [bz], [tdprg], [faxto], [faxexp], [ccto], [ccexp], [lx], [qy], 
	[jkcx],[bbbeg],[bbprg],[rptrq],[rptsj],[app_m],[yslx],[dxnr],[cy],[dyl]
	from tidanb 
GO


--单证、全部
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tidandz]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[tidandz]
GO
CREATE VIEW tidandz AS SELECT [nid], [fid], [no], [tdmc], [tdzl], [bz], [tdprg], [faxto], [faxexp], [ccto], [ccexp], [lx], [qy], 
	[jkcx],[bbbeg],[bbprg],[rptrq],[rptsj],[app_m],[dxnr],[cy],[dyl] 
	from tidanb WHERE lx='单证' and app_m='传统单证'
GO

--单证 在用、不包括停用
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tidandzu]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[tidandzu]
GO
CREATE VIEW tidandzu AS SELECT [nid], [fid], [no], [tdmc], [tdzl], [bz], [tdprg], [faxto], [faxexp], [ccto], [ccexp], [lx], [qy], 
	[jkcx],[bbbeg],[bbprg],[rptrq],[rptsj],[app_m],[dxnr],[cy],[dyl] 
	from tidanb WHERE lx='单证' and QY=1 and app_m='传统单证'
GO

--运价全部
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tidanyj]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[tidanyj]
GO
CREATE VIEW tidanyj AS SELECT [nid], [fid], [no], [tdmc], [tdzl], [bz], [tdprg], [faxto], [faxexp], [ccto], [ccexp], [lx], [qy], 
	[jkcx],[bbbeg],[bbprg],[rptrq],[rptsj],[app_m],[dxnr],[cy],[dyl] 
	from tidanb WHERE lx='运价' and app_m='传统单证'
GO

--运价在用、不包括停用
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tidanyju]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[tidanyju]
GO
CREATE VIEW tidanyju AS SELECT [nid], [fid], [no], [tdmc], [tdzl], [bz], [tdprg], [faxto], [faxexp], [ccto], [ccexp], [lx], [qy], 
	[jkcx],[bbbeg],[bbprg],[rptrq],[rptsj],[app_m],[dxnr],[cy],[dyl] 
	from tidanb WHERE lx='运价' and QY=1 and app_m='传统单证'
GO

--船期全部
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tidancq]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[tidancq]
GO
CREATE VIEW tidancq AS SELECT [nid], [fid], [no], [tdmc], [tdzl], [bz], [tdprg], [faxto], [faxexp], [ccto], [ccexp], [lx], [qy], 
	[jkcx],[bbbeg],[bbprg],[rptrq],[rptsj],[app_m],[dxnr],[cy],[dyl] 
	from tidanb WHERE lx='船期' and app_m='传统单证'
GO

--船期在用、不包括停用
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tidancqu]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[tidancqu]
GO
CREATE VIEW tidancqu AS SELECT [nid], [fid], [no], [tdmc], [tdzl], [bz], [tdprg], [faxto], [faxexp], [ccto], [ccexp], [lx], [qy], 
	[jkcx],[bbbeg],[bbprg],[rptrq],[rptsj],[app_m],[dxnr],[cy],[dyl] 
	from tidanb WHERE lx='船期' and QY=1 and app_m='传统单证'
GO

--商品全部
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tidansp]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[tidansp]
GO
CREATE VIEW tidansp AS SELECT [nid], [fid], [no], [tdmc], [tdzl], [bz], [tdprg], [faxto], [faxexp], [ccto], [ccexp], [lx], [qy], 
	[jkcx],[bbbeg],[bbprg],[rptrq],[rptsj],[app_m],[dxnr],[cy],[dyl] 
	from tidanb WHERE lx='COMMODITY' and app_m='传统单证'
GO

--商品在用、不包括停用
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tidanspu]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[tidanspu]
GO
CREATE VIEW tidanspu AS SELECT [nid], [fid], [no], [tdmc], [tdzl], [bz], [tdprg], [faxto], [faxexp], [ccto], [ccexp], [lx], [qy],
	 [jkcx],[bbbeg],[bbprg],[rptrq],[rptsj],[app_m],[dxnr],[cy],[dyl] 
	from tidanb WHERE lx='COMMODITY' and qy=1 and app_m='传统单证'
GO

--CRM全部
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tidancrm]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[tidancrm]
GO
CREATE VIEW tidancrm AS SELECT [nid], [fid], [no], [tdmc], [tdzl], [bz], [tdprg], [faxto], [faxexp], [ccto], [ccexp], [lx], [qy], 
	[jkcx],[bbbeg],[bbprg],[rptrq],[rptsj],[app_m],[dxnr],[cy],[dyl] 
	from tidanb WHERE lx='CRM' and app_m='传统单证'
GO

--CRM在用、不包括停用
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tidancrmu]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[tidancrmu]
GO
CREATE VIEW tidancrmu AS SELECT [nid], [fid], [no], [tdmc], [tdzl], [bz], [tdprg], [faxto], [faxexp], [ccto], [ccexp], [lx], [qy],
	 [jkcx],[bbbeg],[bbprg],[rptrq],[rptsj],[app_m],[dxnr],[cy],[dyl] 
	from tidanb WHERE lx='CRM' and qy=1 and app_m='传统单证'
GO

--车辆全部
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tidancl]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[tidancl]
GO
CREATE VIEW tidancl AS SELECT [nid], [fid], [no], [tdmc], [tdzl], [bz], [tdprg], [faxto], [faxexp], [ccto], [ccexp], [lx], [qy],
	 [jkcx],[bbbeg],[bbprg],[rptrq],[rptsj],[app_m],[dxnr],[cy],[dyl] 
	from tidanb WHERE lx='TRUCK' and app_m='传统单证'
GO

--车辆在用、不包括停用
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tidanclu]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[tidanclu]
GO
CREATE VIEW tidanclu AS SELECT [nid], [fid], [no], [tdmc], [tdzl], [bz], [tdprg], [faxto], [faxexp], [ccto], [ccexp], [lx], [qy], 
	[jkcx],[bbbeg],[bbprg],[rptrq],[rptsj],[app_m],[dxnr],[cy],[dyl] 
	from tidanb WHERE lx='TRUCK' and qy=1 and app_m='传统单证'
GO

--驾驶员全部
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tidanjs]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[tidanjs]
GO
CREATE VIEW tidanjs AS SELECT [nid], [fid], [no], [tdmc], [tdzl], [bz], [tdprg], [faxto], [faxexp], [ccto], [ccexp], [lx], [qy], 
	[jkcx],[bbbeg],[bbprg],[rptrq],[rptsj],[app_m],[dxnr],[cy],[dyl] 
	from tidanb WHERE lx='DRIVER' and app_m='传统单证'
GO

--驾驶员在用、不包括停用
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tidanjsu]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[tidanjsu]
GO
CREATE VIEW tidanjsu AS SELECT [nid], [fid], [no], [tdmc], [tdzl], [bz], [tdprg], [faxto], [faxexp], [ccto], [ccexp], [lx], [qy], 
	[jkcx],[bbbeg],[bbprg],[rptrq],[rptsj],[app_m],[dxnr],[cy],[dyl] 
	from tidanb WHERE lx='DRIVER' and qy=1 and app_m='传统单证'
GO

--短信视图-单证
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tidandx]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[tidandx]
GO
CREATE VIEW tidandx AS SELECT [nid], [fid], [no], [tdmc], [tdzl], [bz], [tdprg], [faxto], [faxexp], [ccto], 
	[ccexp], [lx], [qy], [jkcx],[bbbeg],[bbprg],[rptrq],[rptsj],[app_m],[dxnr],[cy],[dyl] 
	from tidanb WHERE lx='单证' AND app_m='手机短信'
go

--短信视图-单证 在用、不包括停用
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tidandxu]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[tidandxu]
GO
CREATE VIEW tidandxu AS SELECT [nid], [fid], [no], [tdmc], [tdzl], [bz], [tdprg], [faxto], [faxexp], [ccto], 
	[ccexp], [lx], [qy], [jkcx],[bbbeg],[bbprg],[rptrq],[rptsj],[app_m],[dxnr],[cy],[dyl] 
	from tidanb WHERE lx='单证' AND app_m='手机短信' and qy=1
go

--短信视图-CRM
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tidandy]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[tidandy]
GO
CREATE VIEW tidandy AS SELECT [nid], [fid], [no], [tdmc], [tdzl], [bz], [tdprg], [faxto], [faxexp], [ccto], 
	[ccexp], [lx], [qy], [jkcx],[bbbeg],[bbprg],[rptrq],[rptsj],[app_m],[dxnr],[cy],[dyl]
	from tidanb WHERE lx='CRM' AND app_m='手机短信'
go

--短信视图-CRM 在用、不包括停用
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tidandyu]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[tidandyu]
GO
CREATE VIEW tidandyu AS SELECT [nid], [fid], [no], [tdmc], [tdzl], [bz], [tdprg], [faxto], [faxexp], [ccto], 
	[ccexp], [lx], [qy], [jkcx],[bbbeg],[bbprg],[rptrq],[rptsj],[app_m],[dxnr],[cy],[dyl]
	from tidanb WHERE lx='CRM' AND app_m='手机短信' and qy=1
go

--订单、全部
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tidanpo]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[tidanpo]
GO
CREATE VIEW tidanpo AS SELECT [nid], [fid], [no], [tdmc], [tdzl], [bz], [tdprg], [faxto], [faxexp], [ccto], 
	[ccexp], [lx], [qy], [jkcx],[bbbeg],[bbprg],[rptrq],[rptsj],[app_m],[dxnr],[cy],[dyl] 
	from tidanb WHERE lx='PO' and app_m='传统单证'
GO


--订单 在用、不包括停用
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tidanpou]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[tidanpou]
GO
CREATE VIEW tidanpou AS SELECT [nid], [fid], [no], [tdmc], [tdzl], [bz], [tdprg], [faxto], [faxexp], [ccto], 
	[ccexp], [lx], [qy], [jkcx],[bbbeg],[bbprg],[rptrq],[rptsj],[app_m],[dxnr],[cy],[dyl] 
	from tidanb WHERE lx='PO' and QY=1 and app_m='传统单证'
GO

--凭证 在用、不包括停用
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tidanpzu]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[tidanpzu]
GO
CREATE VIEW tidanpzu AS SELECT [nid], [fid], [no], [tdmc], [tdzl], [bz], [tdprg], [faxto], [faxexp], [ccto], 
	[ccexp], [lx], [qy], [jkcx],[bbbeg],[bbprg],[rptrq],[rptsj],[app_m],[dxnr],[cy],[dyl] 
	from tidanb WHERE lx='凭证' and QY=1 and app_m='传统单证'
GO

--凭证全部
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tidanpz]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[tidanpz]
GO
CREATE VIEW tidanpz AS SELECT [nid], [fid], [no], [tdmc], [tdzl], [bz], [tdprg], [faxto], [faxexp], [ccto], [ccexp], [lx], [qy], 
	[jkcx],[bbbeg],[bbprg],[rptrq],[rptsj],[app_m],[dxnr],[cy],[dyl]
	from tidanb WHERE lx='凭证' and app_m='传统单证'
GO

--SOC档案表View
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[soc]') and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view [dbo].[soc]
GO

CREATE VIEW dbo.soc
AS
SELECT *
	  --nid, fid, lx, f1, orderid, xddm, xdmc, xsl, xh, dx, xx, fj, wxl, wxw, wxh, rxl, rxw, rxh, 
      --pol, pod1, pod2, mandm, manmc, dcdm, dcmc, jcrq, zxbz, instat, f1EX, f1ref, sjtxrq, 
      --yxdm, yxmc, shdm, shmc, bxdm, bxmc, f29, f31, f32, f32sj, pod, eta, etasj, hxh, hxrq, 
      --hxstat, mzts, fdts, tbrq, tbts, tbje, hxdz, hxdz1, shr, fdstart, rate, dpp, yxbt, dsps, zxfy, 
      --tstk, qydd, kstxr, zhcyr, bz, bz1, bj_in, bj_out
FROM dbo.sob
WHERE (lx = '*')
GO

--SOC箱输入箱号检查
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[socchk]') and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view [dbo].[socchk]
GO

CREATE VIEW dbo.socchk
AS
SELECT *
FROM dbo.sob
WHERE (lx = '*')
GO

--SOC业务表View 箱明细
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sod]') and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view [dbo].[sod]
GO

CREATE VIEW dbo.sod
AS
SELECT *
FROM dbo.sob
WHERE (lx = '#')
GO

--SOC业务表每一业务 定单号，业务号 一对一CKSYK
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[soe]') and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view [dbo].[soe]
GO

CREATE VIEW dbo.soe
AS
SELECT *
FROM dbo.sob
WHERE (lx = 'F')
GO

--soc箱型箱量表
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sog]') and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view [dbo].[sog]
GO

CREATE VIEW dbo.sog
AS
SELECT *
FROM dbo.sob
WHERE (lx = 'G')
GO

--口令树
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[t_kl]') and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view [dbo].[t_kl]
GO

CREATE VIEW dbo.t_kl
AS
SELECT nid,fid,zzyh,xm,
	dbo.f_get_user_group(jdid) as fjdmc,
	dbo.f_get_user_area(jdid) as rootname,
	jdid,fjd,sjid,sjmc,dutydm,dutymc,dcdm,dcmc,
	dbo.f_get_user_root(jdid) as rootid,
	dbo.f_get_user_level(jdid) as jdc,
	jsy,lz
FROM dbo.kl
GO

--运价目录树
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[t_yj]') and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view [dbo].[t_yj]
GO

CREATE VIEW dbo.t_yj
AS
SELECT nid,fid,xm,dbo.f_get_yj_group(jdid) as fjdmc,dbo.f_get_yj_area(jdid) as rootname,jdid,fjd,
	dbo.f_get_yj_root(jdid) as rootid,dbo.f_get_yj_level(jdid) as jdc
FROM dbo.khyjmenu
GO

--081201 有效登录
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[t_lg]') and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view [dbo].[t_lg]
GO

CREATE VIEW dbo.t_lg
AS
SELECT KL.NID,xm,zzyh,KL,JDID,FJD,dbo.f_get_user_level(jdid) as jdc,dbo.f_get_user_root(jdid) as rootid,NFG 
from kl with (nolock) WHERE jy = 0 and jy = 0
GO

--菜单树
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[t_menui]') and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view [dbo].[t_menui]
GO

CREATE VIEW dbo.t_menui
AS
	SELECT nid,fid,xm,jdid,fjd,flag,dbo.f_get_menu_root(jdid) as rootid,dbo.f_get_menu_level(jdid) as jdc,
		xm_us,skipfor  --dbo.f_get_user_group(jdid) as fjdmc,dbo.f_get_user_area(jdid) as rootname
		FROM dbo.menui
GO

--用户表
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[usr]') and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view [dbo].[usr]
GO

create view dbo.usr
as
	select *,dbo.f_get_fid_by_jdid(fjd) as fjdfid,dbo.f_get_fid_by_jdid(rootid) as rootfid
		from dbo.t_kl
		where jdc = '2'
go

--用户表 未离职
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ust]') and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view [dbo].[ust]
GO

create view dbo.ust
as
	select *,dbo.f_get_fid_by_jdid(fjd) as fjdfid,dbo.f_get_fid_by_jdid(rootid) as rootfid
		from dbo.t_kl
		where jdc = '2' and lz = 0
go

--用户表-申报员
if exists (select * from dbo.sysobjects where id = object_id(N'[usr_sby]') and objectproperty(id, N'IsView') = 1)
	drop view [dbo].[usr_sby]
go

create view dbo.usr_sby
as
	select t_kl.*,dbo.f_get_fid_by_jdid(fjd) as fjdfid,dbo.f_get_fid_by_jdid(rootid) as rootfid
		from dbo.t_kl left join jsyxx on t_kl.fid = jsyxx.fid
		where t_kl.jdc = '2' and jsyxx.sbybh <> ''
go

--用户表-报关员
if exists (select * from dbo.sysobjects where id = object_id(N'[usr_bgy]') and objectproperty(id, N'IsView') = 1)
	drop view [dbo].[usr_bgy]
go

create view dbo.usr_bgy
as
	select t_kl.*,dbo.f_get_fid_by_jdid(fjd) as fjdfid,dbo.f_get_fid_by_jdid(rootid) as rootfid
		from dbo.t_kl left join jsyxx on t_kl.fid = jsyxx.fid
		where t_kl.jdc = '2' and jsyxx.bgybh <> ''
go

--用户表-装箱员
if exists (select * from dbo.sysobjects where id = object_id(N'[usr_zxy]') and objectproperty(id, N'IsView') = 1)
	drop view [dbo].[usr_zxy]
go

create view dbo.usr_zxy
as
	select t_kl.*,dbo.f_get_fid_by_jdid(fjd) as fjdfid,dbo.f_get_fid_by_jdid(rootid) as rootfid
		from dbo.t_kl left join jsyxx on t_kl.fid = jsyxx.fid
		where t_kl.jdc = '2' and jsyxx.zxybh <> ''
go

--用户表-检查员
if exists (select * from dbo.sysobjects where id = object_id(N'[usr_jcy]') and objectproperty(id, N'IsView') = 1)
	drop view [dbo].[usr_jcy]
go

create view dbo.usr_jcy
as
	select t_kl.*,dbo.f_get_fid_by_jdid(fjd) as fjdfid,dbo.f_get_fid_by_jdid(rootid) as rootfid
		from dbo.t_kl left join jsyxx on t_kl.fid = jsyxx.fid
		where t_kl.jdc = '2' and jsyxx.jcybh <> ''
go

--用户表-报检员
if exists (select * from dbo.sysobjects where id = object_id(N'[usr_bjy]') and objectproperty(id, N'IsView') = 1)
	drop view [dbo].[usr_bjy]
go

create view dbo.usr_bjy
as
	select t_kl.*,dbo.f_get_fid_by_jdid(fjd) as fjdfid,dbo.f_get_fid_by_jdid(rootid) as rootfid
		from dbo.t_kl left join jsyxx on t_kl.fid = jsyxx.fid
		where t_kl.jdc = '2' and jsyxx.bjybh <> ''
go

--用户表-驾驶员
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dvr]') and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view [dbo].[dvr]
GO

create view dbo.dvr
as
	select *
		from dbo.t_kl
		where (jdc = '2' and jsy = 1)
go

--雇员表
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[employee]') and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view [dbo].[employee]
GO

create view dbo.employee
as
	select nid,fid,zzyh,xm,jdid,fjd,dh,twcz,ewxm,sj,eml,msn,qq,jsy,lz,
		sjid,sjmc,dutydm,dutymc,dcdm,dcmc,aqjb,xsbj,
		dbo.f_get_user_group(jdid) as fjdmc,
		dbo.f_get_user_area(jdid) as rootname,
		dbo.f_get_user_root(jdid) as rootid,
		dbo.f_get_user_level(jdid) as jdc,
		dbo.f_get_user_ktmsn(fid) as ktmsn
		from dbo.kl
		where (dbo.f_get_user_level(jdid) = '2' and jy = 0 and lz = 0)
go

--部门表
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dep]') and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view [dbo].[dep]
GO

create view dbo.dep
as
	select *
		from dbo.t_kl
		where (jdc = '1')
go

--区域
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[area]') and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view [dbo].[area]
GO

create view dbo.area
as
	select *
		from dbo.t_kl
		where (fjd = '0_')
go

--vfud 改用p_view_vfud
--vfun 改用p_view_vfun


--附件 不包括文件image的View 少FJK
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[fjkn]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[fjkn]
GO
CREATE VIEW fjkn AS SELECT [nid],[fid],[f1],[lxdm],[lxmc],[cjrq],[cjr],[dx],[sgrq],[wjm],[bz],[lxbm],
	[fjrq],[fjsj],[fb],[sy],[cjrid],[cjrg],[cjrgid],[cjra],[cjraid],[yslx],[klx],[xzbj] FROM [fjk]
GO

--日期 使用在function中
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[v_getdate]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[v_getdate]
GO
create view v_getdate
as
select getdate() [output]
go


--单位 06.08.08 黑名单视图
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[db1bl]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[db1bl]
GO
create view db1bl
as
select nid,fid,f9,bz2,fx from db1
go

--单位CRM出库指令单明细视图
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[crm_zld]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[crm_zld]
GO
create view crm_zld
as
	select stzld.*,cksyk.wtf2dm,cksyk.wtf2,cksyk.ywlxdm,cksyk.ywlxmc
		from stzld with (nolock) 
		left join cksyk with (nolock) on stzld.f1 = cksyk.f1
		where cksyk.opm = 'STOCK_EX' 
go


--单位 07.01.01 CRM 应收款视图
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[crm_ys]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[crm_ys]
GO
create view crm_ys
as
select s.nid,s.fid,s.ly,s.fl,0 as xz,c.ywlxdm,c.ywlxmc,c.f33,s.f1,dbo.f_ujqys(s.F1) AS SQ,dbo.f_ujqyf(s.F1) AS FQ,
	s.ffrdm,s.glr,s.biz,s.sfnr1,s.yshj,s.ysfph,s.sssj,s.sshj,s.bz,dbo.f_swsf_fkts(s.nid) as fkts,s.sd,
	c.f29,c.f31,c.f32,c.khbh,j2.f12,j3.xxxl,dbo.f_get_sfnr(s.fid) as sfnr,s.ffr,s.ysrq,c.jmyw from swsf s
					left join cksyk c on s.f1=c.f1
					left join dbj2 j2 on s.f1=j2.f1
					left join dbj3 j3 on s.f1=j3.f1
					where s.fl='+' and s.yshj<>0  --  C.ywlxdm+order by c.f32 
go


--单位 07.01.01 CRM 未收款视图
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[crm_ws]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[crm_ws]
GO
create view crm_ws
as
select s.nid,s.fid,s.ly,s.fl,0 as xz,c.ywlxdm,c.ywlxmc,c.f33,s.f1,dbo.f_ujqys(s.F1) AS SQ,dbo.f_ujqyf(s.F1) AS FQ,
	s.ffrdm,s.glr,s.biz,s.sfnr1,s.yshj,s.ysfph,s.sssj,s.sshj,s.bz,dbo.f_swsf_fkts(s.nid) as fkts,s.sd,
	c.f29,c.f31,c.f32,c.khbh,j2.f12,j3.xxxl,dbo.f_get_sfnr(s.fid) as sfnr,s.ffr,s.ysrq,c.jmyw from swsf s
					left join cksyk c on s.f1=c.f1
					left join dbj2 j2 on s.f1=j2.f1
					left join dbj3 j3 on s.f1=j3.f1
					where s.fl='+' and s.yshj<>0 and s.ssbj='' --  C.ywlxdm+order by c.f32 
go



--单位 07.01.01 CRM 应付款视图
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[crm_yf]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[crm_yf]
GO
create view crm_yf
as
select s.nid,s.fid,s.ly,s.fl,0 as xz,c.ywlxdm,c.ywlxmc,c.f33,s.f1,dbo.f_ujqys(s.F1) AS SQ,dbo.f_ujqyf(s.F1) AS FQ,
	s.ffrdm,s.glr,s.biz,s.sfnr1,s.yshj,s.ysfph,s.sssj,s.sshj,s.bz,dbo.f_swsf_fkts(s.nid) as fkts,s.sd,
	c.f29,c.f31,c.f32,c.khbh,j2.f12,j3.xxxl,dbo.f_get_sfnr(s.fid) as sfnr,s.ffr,s.ysrq,c.jmyw from swsf s
					left join cksyk c on s.f1=c.f1
					left join dbj2 j2 on s.f1=j2.f1
					left join dbj3 j3 on s.f1=j3.f1
					where s.fl='-' and s.yshj<>0  --  C.ywlxdm+order by c.f32 
go


--单位 07.01.01 CRM 未付款视图 
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[crm_wf]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[crm_wf]
GO
create view crm_wf
as
select s.nid,s.fid,s.ly,s.fl,0 as xz,c.ywlxdm,c.ywlxmc,c.f33,s.f1,dbo.f_ujqys(s.F1) AS SQ,dbo.f_ujqyf(s.F1) AS FQ,
	s.ffrdm,s.glr,s.biz,s.sfnr1,s.yshj,s.ysfph,s.sssj,s.sshj,s.bz,dbo.f_swsf_fkts(s.nid) as fkts,s.sd,
	c.f29,c.f31,c.f32,c.khbh,j2.f12,j3.xxxl,dbo.f_get_sfnr(s.fid) as sfnr,s.ffr,s.ysrq,c.jmyw from swsf s
					left join cksyk c on s.f1=c.f1
					left join dbj2 j2 on s.f1=j2.f1
					left join dbj3 j3 on s.f1=j3.f1
					where s.fl='-' and s.yshj<>0 and s.ssbj='' --  C.ywlxdm+order by c.f32 
go


--单位 07.01.15 CRM 服务视图
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[crm_fw]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[crm_fw]
GO
create view crm_fw
as
select cksyk.nid,CKSYK.F1,cksyk.zzyh,dbj2.f2,cksyk.f32,cksyk.wtf2dm,cksyk.wtf2,cksyk.lx2mc,cksyk.glr,cksyk.op,dbj2.bz1,cksyk.tdmc,dbj2.dhr,dbj2.bz2
	from cksyk left join dbj2 on dbj2.f1=cksyk.f1 where cksyk.ywdm='V' 
go

--07.04.18
--币种 单行币种
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[biz]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[biz]
GO
create view biz
as
	select * from wjcurr a where not exists(select 1 from wjcurr where nid > a.nid and c_symb = a.c_symb ) 
go

--口令 07.01.15 KLN 不包括指纹
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[kln]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[kln]
GO
create view kln
as
SELECT [nid],[fid],[zzyh],[xm],[jdid],[fjd],[kl],[zcx],[jb],[czfw],[f9],[ky],[dh],[twcz],[jy],[fw],[ewxm],[wlfw],[oth],[fwlst],
      [wllst],[dm2],[nfg],[sj],[ysfw],[yslst],[yffw],[yflst],[wlmsn],[sino_dm],[sino_bs],[sino_dir],[eml],[msn],[qq],[swfw],[swlst],
		[kl1],[xgfw],[xglst],[jsy],[lz],[hrfw],[hrlst],[fjfw],[fjlst],[msnzl],[msnbj],[yjfw],[yjlst],[yxfw],[yxlst],[cwfw],[cwlst],
		dutydm,dutymc,[sjmc],[aqjb],[xsbj],[dcdm],[dcmc],[xsmc],[m_smtp],[m_port],[m_user],[m_pwd],[m_ssl]
	FROM kl  --ZW1,ZW2
go

      
--驾驶员 07.08.27 前台按车队+驾驶员定位 同名表
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[jsyxx1]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[jsyxx1]
GO
create view jsyxx1
as
	SELECT *
	FROM jsyxx
go

--驾驶员 07.08.27 前台驾驶员定位 同名表
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[jsyxx2]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[jsyxx2]
GO
create view jsyxx2
as
	SELECT *
	FROM jsyxx
go


--菜单 07.10.01 menucn 不含文件的菜单代码
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[menucn]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[menucn]
GO
CREATE VIEW menucn AS SELECT [nid],[fid],[no],[mc],[zwmc],[sm],[fxprq],[fxpsj],xjr,xjrq,xjsj,yslx,
	dx,lxdm,lj,lxbm,lxmc,nxz
  FROM [dbo].[menuc] 
GO


--商品明细 080104 商品分批结存数view 仅有MFID、PH、结存数三列
--080122 增加商品有效期
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[stspph]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[stspph]
GO
CREATE VIEW stspph 
AS 
	select mfid,ph,sum(slj-slc) as jcsl,min(yxq) as yxq from stspmx  group by mfid,ph 
GO

--商品 080104 商品批次结存数view  商品信息+PH和结存数
--080122 增加商品有效期
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[stsppc]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[stsppc]
GO
CREATE VIEW stsppc
AS 
	select stsp.*,isnull(stspph.ph,'') as ph,isnull(stspph.jcsl,0) as jcsl,isnull(stspph.yxq,'') as yxq 
		from stsp left join stspph on stspph.mfid = stsp.fid 
GO

--商品明细 090704 商品分批结存数view 仅有MFID、CKDM、CKMC 结存数四列
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[stmxck]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[stmxck]
GO
CREATE VIEW stmxck 
AS 
	select mfid,ckdm,max(ckmc) as ckmc,sum(slj-slc) as jcsl from stspmx  group by mfid,ckdm 
GO

--商品 090704 商品分库结存数view  商品信息+仓库地点 结存数
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[stspck]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[stspck]
GO
CREATE VIEW stspck
AS 
	select stsp.*,isnull(stmxck.ckdm,'') as ckdm,isnull(stmxck.ckmc,'') as ckmc,isnull(stmxck.jcsl,0) as jcsl
		from stsp left join stmxck on stmxck.mfid = stsp.fid 
GO


--收付项目 前台按收付(SWSF)按JOBNO定位
----记录号,JOBNO,分类，来源、序号、项目代码、项目、单价、数量、金额、附加税率，附加金额，含税金额，备注
--nid,fid,mfid,f1,fl,ly,xh,xmh,sfnr,dj,sl1,dwdm,dwmc,je,fln,fjn,ysn,bz
--select * from swxm where f1 = 'GW0L00000020'  --View
--select * from dbo.f_gettab_swxm('gw0l00000020')  --表函数
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swxm]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swxm]
GO
--2011.02.22 为提速删除 ,substring(ocl,1,1) as ygocl,substring(ocl,9,1) as sjocl 两列
CREATE VIEW dbo.swxm  
AS  
	select nid,fid+'1' as fid,fid as mfid,
		f1,swsf.fl,swsf.ly,1 as xh,xmh1 as xmh,sfnr1 as sfnr,dj1 as dj,sl1 as sl1,dwdm1 as dwdm, dwmc1 as dwmc,sfje1 as je,
		fl1 as fln,fj1 as fjn,ys1 as ysn,bz1 as bz from swsf where ys1<>0--not(ys1=0 and sfnr1='')
	union
		select nid,fid+'2',fid,f1,swsf.fl,swsf.ly,2 as xh,xmh2,sfnr2,dj2,sl2,dwdm2,dwmc2,sfje2,
			fl2,fj2,ys2,bz2 from swsf where ys2<>0 --not(ys2=0 and sfnr2='')
	union
		select nid,fid+'3',fid,f1,swsf.fl,swsf.ly,3 as xh,xmh3,sfnr3,dj3,sl3,dwdm3,dwmc3,sfje3,
			fl3,fj3,ys3,bz3 from swsf where ys3<>0 --not(ys3=0 and sfnr3='')
	union
		select nid,fid+'4',fid,f1,swsf.fl,swsf.ly,4 as xh,xmh4,sfnr4,dj4,sl4,dwdm4,dwmc4,sfje4,
			fl4,fj4,ys4,bz4 from swsf where ys4<>0 --not(ys4=0 and sfnr4='')
	union
		select nid,fid+'5',fid,f1,swsf.fl,swsf.ly,5 as xh,xmh5,sfnr5,dj5,sl5,dwdm5,dwmc5,sfje5,
			fl5,fj5,ys5,bz5 from swsf where ys5<>0 --not(ys5=0 and sfnr5='')
	union
		select nid,fid+'6',fid,f1,swsf.fl,swsf.ly,6 as xh,xmh6,sfnr6,dj6,sl6,dwdm6,dwmc6,sfje6,
			fl6,fj6,ys6,bz6 from swsf where ys6<>0 --not(ys6=0 and sfnr6='')
	union
		select nid,fid+'7',fid,f1,swsf.fl,swsf.ly,7 as xh,xmh7,sfnr7,dj7,sl7,dwdm7,dwmc7,sfje7,
			fl7,fj7,ys7,bz7 from swsf where ys7<>0 --not(ys7=0 and sfnr7='')
	union
		select nid,fid+'8',fid,f1,swsf.fl,swsf.ly,8 as xh,xmh8,sfnr8,dj8,sl8,dwdm8,dwmc8,sfje8,
			fl8,fj8,ys8,bz8 from swsf where ys8<>0 --not(ys8=0 and sfnr8='')
go

--收付明细 前台按行定位
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swmx]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swmx]
GO
CREATE VIEW dbo.swmx  
AS  
	select * from swxm
go

--代理收付 收付项目明细 改进基于SWxm
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swagtxm]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swagtxm]
GO

CREATE VIEW dbo.swagtxm  
AS  
	select * from swxm where swxm.fl = '+' and swxm.ly = '3' 
go

--20150414 增加对账确认日
--应收项目 前台按收付(SWSF)行定位
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swxmsf]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swxmsf]
GO

CREATE VIEW dbo.swxmsf  
AS  	
	select swsf.nid,swxm.fid,swxm.mfid,swsf.f1,swsf.FL,swsf.ly,  --关键部分
		swxm.xh,swxm.xmh,swxm.sfnr,swxm.dj,swxm.sl1,swxm.je,
		swxm.fln,swxm.fjn,swxm.ysn,swxm.bz,swxm.dwdm,swxm.dwmc, --分项部分
		swsf.biz,swsf.cny,swsf.ffrdm,swsf.ffrmc as sfr,swsf.ffr,
		swsf.ysfph,swsf.cdno,swsf.yskpbj,swsf.ysrq,swsf.sssj,swsf.ssbj,swsf.glr,swsf.bkzzyh,swsf.sd,swsf.xz,swsf.pzh,swsf.bz as bza,swsf.hr,
		swsf.sjbz,swsf.sjje,swsf.pc,swsf.zbcopy,swsf.zzbj,swsf.glfdm,swsf.glf,swsf.shbj,swsf.shr,swsf.shrq,swsf.zhxgrq,
		swsf.dzje,swsf.ywmc,swsf.dd,swsf.bl,swsf.sdr,swsf.glz,swsf.dzh,swsf.kpbz,swsf.fkdm,swsf.fkmc,swsf.zzxz,
		swsf.fyqr,swsf.hsyhdm,swsf.hsyhmc,swsf.hsxjdm,swsf.hsxjmc,swsf.kphl,swsf.km,swsf.kpje,swsf.hcbj,swsf.glrid,swsf.glra,
		swsf.glzid,swsf.glraid,swsf.hth,swsf.db1fkmc,swsf.ddnid,swsf.llpr,swsf.llpd,swsf.lspr,swsf.lspd,swsf.lqpr,
		swsf.lqpd,swsf.lts,swsf.xf1,swsf.y1,swsf.ch,swsf.ocl,swsf.dxbj,swsf.bj,swsf.ddfid,swsf.fpzf,swsf.kpc,swsf.sjc,swsf.qrrq,swsf.kpqr,
		swsf.zzl,swsf.dqrq,
		cksyk.fm1,dbo.f_bzzh_yw(swsf.biz,swxm.je,swxm.f1,case when swxm.fl = '-' then 'F' else 'S' end) as je_rmb,
		dbo.f_bzzh_usd_yf(swsf.biz,swxm.ysn,cksyk.f33) AS je_usd,
		swxm.nid as xmnid,swxm.fid as xmfid,swxm.sfnr as xmnr,
		swxm.dj as xmdj,swxm.sl1 as xmsl,swxm.je as xmje,swxm.bz as xmbz
	from swxm left join swsf on swxm.mfid=swsf.fid 
		left join cksyk on swxm.f1 = cksyk.f1		
go


--08.06.26 
--应收项目 前台按拼码定位
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swxmsfpx]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swxmsfpx]
GO

CREATE VIEW dbo.swxmsfpx 
AS  	
	select *
	from swxmsf 
go


--07.10.12 
--应收项目 前台按收付(SWSF)行定位
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swxmbk]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swxmbk]
GO

CREATE VIEW dbo.swxmbk  
AS  	
	select *
	from swxmsf where swxmsf.fl='+' and swxmsf.je<>0 and swxmsf.ly<>'4'
go

--应付项目 前台按收付(SWSF)行定位
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swxmbf]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swxmbf]
GO


CREATE VIEW dbo.swxmbf
AS  	
	select *
	from swxmsf where swxmsf.fl='-' and swxmsf.je<>0 and swxmsf.ly<>'4'
go

--费收检查 应收项目 分项目合计
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swxmbkg]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swxmbkg]
GO

CREATE VIEW dbo.swxmbkg  
AS
	select 1 as nid,f1,biz,ffrdm,max(sfr) as sfr,xmh,max(sfnr) as sfnr,count(nid) as ncount,
		SUM(je) as je,SUM(sl1) as sl,sum(fjn) as fjn,sum(ysn) as ysn from swxmbk 
		group by f1,biz,ffrdm,xmh --order by f1,biz,ffrdm,xmh 
go


--费收检查 应付项目 分项目合计
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swxmbfg]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swxmbfg]
GO


CREATE VIEW dbo.swxmbfg
AS 
	select 1 as nid,f1,biz,ffrdm,max(sfr) as sfr,xmh,max(sfnr) as sfnr,count(nid) as ncount,
		SUM(je) as je,SUM(sl1) as sl,sum(fjn) as fjn,sum(ysn) as ysn from swxmbf 
		group by f1,biz,ffrdm,xmh --order by f1,biz,ffrdm,xmh 
go




--根据函数形成的View
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[vswrow]') and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view [dbo].[vswrow]
GO

CREATE VIEW dbo.vswrow
AS
SELECT swsf.nid,swsf.fid,swsf.f1,
	case when swsf.xf1 = '' then dbj2.ZHD else isnull(zxd.xzxdz,'') end as xzxdz,		--箱装箱地址
	case when swsf.xf1 = '' then dbj2.SHDD else isnull(zxd.xshdz,'') end as xshdz,		--箱送货地址
	case when swsf.xf1 = '' then dbj2.SHDGSMC else isnull(zxd.xshmc,'') end as xshmc,	--箱送货门点名称
	case when swsf.xf1 = '' then dbj2.sjjw else isnull(zxd.sdrq,'') end as sdrq,		--箱送抵日期
	case when swsf.xf1 = '' then '' else isnull(zxd.fhrq,'') end as fhrq,				--箱放货指令收到日
	case when swsf.xf1 = '' then dbj2.txcdmc else isnull(zxd.shcdmc,'') end as xshcdmc,	--箱送货车队(08.04.08)
	case when swsf.xf1 = '' then dbj2.yjjw else isnull(zxd.xyjshrq,'') end as xyjshrq,	--箱计划送抵(08.06.23)
	case when swsf.xf1 = '' then dbj3.ckh else isnull(zxd.zhdbh,'') end as zhdbh,		--箱装货点编号(08.09.26)
	case when swsf.xf1 = '' then dbj3.zhjc else isnull(zxd.zhjc,'') end as zhjc,		--箱装货门点简称
	case when swsf.xf1 = '' then dbj2.zhdgsmc else isnull(zxd.xzxmc,'') end as xzxmc,	--箱装货门点名称
	case when swsf.xf1 = '' then dbj3.shrbh else isnull(zxd.shdbh,'') end as shdbh,		--箱送货点编号
	case when swsf.xf1 = '' then dbj3.shjc else isnull(zxd.shjc,'') end as shjc			--箱送货门点简称
FROM dbo.swsf left join zxd on swsf.xf1 = zxd.xf1 left join DBJ2 on dbj2.f1 = swsf.f1 left join dbj3 on dbj3.f1 = swsf.f1
GO

--车队车头视图
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[clv]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[clv]
GO
CREATE VIEW clv AS SELECT NID,FID,CH,GCH,JSYXM,JSYXM1 FROM CLXX where clxx.ct = '*'
go

--车队车挂视图
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[clg]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[clg]
GO
CREATE VIEW clg AS SELECT NID,FID,CH,LXMC FROM CLXX WHERE CLXX.CT = ''
go


--箱号表
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[xhb]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[xhb]
GO

CREATE VIEW dbo.xhb  
AS  
SELECT ZXD.NID,ZXD.FID,CKSYK.F1
FROM ZXD  left join cksyk on ZXD.f1=case when cksyk.fm1='' then cksyk.f1 else cksyk.fm1 end LEFT OUTER JOIN WJMYWK ON cksyk.F1=WJMYWK.F1 
WHERE not(zxd.nid is null) 
and (dbo.f_is_consol_first_jobno(cksyk.f1) = 1 OR cksyk.fm1 = '')
GO

--未处理消息
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[msgfrom]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[msgfrom]
GO

CREATE VIEW dbo.msgfrom
AS
SELECT *
FROM dbo.jsltjl where jsbj = ''
GO

--消息记录
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[msglog]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[msglog]
GO

CREATE VIEW dbo.msglog
AS
select jsltjl.nid,jsltjl.fid,fsrq,fstm,
	substring(fsrq,1,2)+'.'+substring(fsrq,3,2)+'.'+substring(fsrq,5,2) + '' +
	' ' + substring(fstm,1,2)+':'+substring(fstm,3,2)+':'+substring(fstm,5,2) as fssj,
	jsltjl.sdfid,jsltjl.f1,jsltjl.xxfldm,jsltjl.xxflmc,
	dbo.f_get_xm_by_fid(fsfid,jsltjl.fstab) as fsxm,
	dbo.f_get_xm_by_fid(sdfid,jsltjl.jstab) as sdxm,
	cast(left(ltjl,250) as varchar(250)) as msg,
	substring(jsrq,1,2)+'.'+substring(jsrq,3,2)+'.'+substring(jsrq,5,2) as rq,
	substring(jssj,1,2)+':'+substring(jssj,3,2)+':'+substring(jssj,5,2) as sj,
	zt,kv,klx,sw,sb
	from dbo.jsltjl 
GO




--PO单位
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[podw]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[podw]
GO
--注意 union all 两个以上
CREATE VIEW dbo.podw  
AS  
	select	1 as nid,db1.fid,pono as f1,'买家' as lx ,posyk.mjdm as wldm,posyk.mjmc as wljc,
			db1.khqc,db1.eml,db1.lxr,db1.dh,db1.dh1,db1.twcz,db1.twcz1,db1.sj,'posyk.mjdm' as wlexp
		from posyk left join db1 on db1.f9=posyk.mjdm where db1.nid is not null and posyk.mjdm<>''
	union
	select	2,db1.fid,pono,'托运人',posyk.tyrdm,posyk.tyrmc,
			db1.khqc,db1.eml,db1.lxr,db1.dh,db1.dh1,db1.twcz,db1.twcz1,db1.sj,'posyk.tyrdm'
		from posyk left join db1 on db1.f9=posyk.tyrdm where db1.nid is not null and posyk.tyrdm<>''
	union
	select	3,db1.fid,pono,'发货地货代',posyk.fhddm,posyk.fhdmc,
			db1.khqc,db1.eml,db1.lxr,db1.dh,db1.dh1,db1.twcz,db1.twcz1,db1.sj,'posyk.fhddm'
		from posyk left join db1 on db1.f9=posyk.fhddm where db1.nid is not null and posyk.fhddm<>''
	union
	select	4,db1.fid,pono,'卸货地货代',posyk.xhddm,posyk.xhdmc,
			db1.khqc,db1.eml,db1.lxr,db1.dh,db1.dh1,db1.twcz,db1.twcz1,db1.sj,'posyk.xhddm'
		from posyk left join db1 on db1.f9=posyk.xhddm where db1.nid is not null and posyk.xhddm<>''
	union
	select	5,db1.fid,pono,'通知人',posyk.tzrdm,posyk.tzrmc,
			db1.khqc,db1.eml,db1.lxr,db1.dh,db1.dh1,db1.twcz,db1.twcz1,db1.sj,'posyk.tzrdm'
		from posyk left join db1 on db1.f9=posyk.tzrdm where db1.nid is not null and posyk.tzrdm<>''
	union all
	select	6,db1.fid,pono,'供应商',poitem.gysdm,poitem.gysmc,
			db1.khqc,db1.eml,db1.lxr,db1.dh,db1.dh1,db1.twcz,db1.twcz1,db1.sj,'poitem.gysdm'
		from poitem left join db1 on db1.f9=poitem.gysdm where db1.nid is not null and poitem.gysdm<>''
	union all
	select	7,db1.fid,pono,'收货人',poitem.shrdm,poitem.shrmc,
			db1.khqc,db1.eml,db1.lxr,db1.dh,db1.dh1,db1.twcz,db1.twcz1,db1.sj,'poitem.shrdm'
		from poitem left join db1 on db1.f9=poitem.shrdm where db1.nid is not null and poitem.shrdm<>''
go


--车队使用列视图
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[truck]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[truck]
GO

CREATE VIEW dbo.truck
AS
SELECT cksyk.nid,cksyk.f1,wjmywk.yap,wjmywk.ywc,dbj1.zxdm,dbj1.zxgq
FROM dbo.cksyk left join wjmywk on cksyk.f1 = wjmywk.f1 left join dbj1 on cksyk.f1 = dbj1.f1
GO

--08.08.18
--码头 ebooking
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[v_mt]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[v_mt]
GO

CREATE VIEW [dbo].[v_mt] 
AS 
	SELECT * from db1 where charindex(lxdm,dbo.f_cxvalue('MT_TYPE'))>0
GO

--船公司 ebooking
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[v_cgs]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[v_cgs]
GO

create view [dbo].[v_cgs] 
as 
	select * from db1 where charindex(lxdm,dbo.f_cxvalue('cs_type'))>0
go

----效率高,但客户变更参数CS会导致需要重新定义View才能生效
--declare @cmd varchar(8000)
--set @cmd = 'create view dbo.v_cgs ' + 
--	'as ' + 
--	'select * from db1 where charindex(lxdm,' + char(39) + dbo.f_cxvalue('CS_TYPE')+ CHAR(39) + ')>0'
--exec(@cmd)
--go


--并发港单位, 已有并发港交换代码的公司
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[v_bfg]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[v_bfg]
GO

create view [dbo].[v_bfg] 
as 
	select nid,fid,bfdm,f9,f8 from db1 where bfdm <> ''
go

--最新薪资调整视图
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[jsyxtn]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[jsyxtn]
GO

CREATE VIEW [dbo].[jsyxtn] 
AS 
	select * from jsyxt where not EXISTS(select 1 from jsyxt a where jsyxt.mfid=a.mfid and a.tzrq>jsyxt.tzrq)
GO


--最新HR合同视图
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[jsyhtn]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[jsyhtn]
GO

CREATE VIEW [dbo].[jsyhtn] 
AS 
	select * from jsyht where not EXISTS(select 1 from jsyht a where jsyht.mfid=a.mfid and a.qdrq>jsyht.qdrq)
GO

--企业成本 薪资+社保
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[jsyxfn]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[jsyxfn]
GO

CREATE VIEW [dbo].[jsyxfn] 
AS 
	select jsyxf.*,isnull(jsysj.qybxhj,0) as qybxhj,jsyxf.yfje + isnull(jsysj.qybxhj,0) as qycb 
		from jsyxf left join jsysj on jsyxf.mfid = jsysj.mfid and left(jsyxf.ffrq,4) = left(jsysj.jnrq,4)
go


--最新运价调整视图
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[khyjn]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[khyjn]
GO
CREATE VIEW [dbo].[khyjn] 
AS 
select b.* from khyjxy b left join khyj a on b.xyh = a.fid where not EXISTS(select 1 from khyj where khyj.f9=a.f9 and khyj.xyh>a.xyh)
GO

--往来单位日志
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[log_kh]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[log_kh]
GO
declare @cmd varchar(8000)
set @cmd = 'CREATE VIEW dbo.log_kh AS SELECT * FROM ['+db_name()+'log].dbo.back 
			where dbfn like ''DB1%'' OR dbfn in (''USERS1'',''SN'',''SNREQ'',''GYSDB'',''GOODS'',''FJK'') ' 
EXEC(@cmd)
go

--凭证日志
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[log_pz]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[log_pz]
GO
declare @cmd varchar(8000)
set @cmd = 'CREATE VIEW dbo.log_pz AS SELECT * from ['+db_name()+'log].dbo.back 
			where dbfn like ''f_pz%'' OR dbfn in (''f_vch'',''f_pd'',''f_pz_qc'',''f_pz_qm'') ' 
EXEC(@cmd)
go

--HR日志
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[log_hr]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[log_hr]
GO
declare @cmd varchar(8000)
set @cmd = 'CREATE VIEW dbo.log_hr AS SELECT * from ['+db_name()+'log].dbo.back 
			where (dbfn like ''jsy%'' and dbfn<>''jsy'') OR dbfn = ''kl'' ' 
EXEC(@cmd)
go

--业务日志
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[log_yw]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[log_yw]
GO
declare @cmd varchar(8000),@ilen int
select @ilen = col_length('CKSYK','F1')
select @ilen = isnull(@ilen,15)
set @cmd = 'CREATE VIEW dbo.log_yw AS SELECT * from ['+db_name()+'log].dbo.back where f1 <> '+ char(39) + Replicate('0',@ilen) + char(39) 
EXEC(@cmd)
go

--自动处理日志
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[log_auto]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[log_auto]
GO
declare @cmd varchar(8000)
set @cmd = 'CREATE VIEW dbo.log_auto AS SELECT * from ['+db_name()+'log].dbo.back where f1 = '+ char(39) + 'AUTOSEND' + char(39) 
EXEC(@cmd)
go

--上机日志
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[log_sj]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[log_sj]
GO
declare @cmd varchar(8000),@ilen int
select @ilen = col_length('CKSYK','F1')
select @ilen = isnull(@ilen,15)
set @cmd = 'CREATE VIEW dbo.log_sj AS SELECT * from ['+db_name()+'log].dbo.back where f1 = '+ char(39) + Replicate('0',@ilen) + char(39) 
EXEC(@cmd)
go

--订单日志
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[log_po]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[log_po]
GO
declare @cmd varchar(8000)
set @cmd = 'CREATE VIEW dbo.log_po AS SELECT * from ['+db_name()+'log].dbo.back 
			where dbfn in (''posyk'',''poitem'') ' 
EXEC(@cmd)
go

--箱管日志
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[log_xg]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[log_xg]
GO
declare @cmd varchar(8000)
set @cmd = 'CREATE VIEW dbo.log_xg AS SELECT * from ['+db_name()+'log].dbo.back 
			where dbfn in (''xg_xt'',''xg_gz'',''xg_bx'') ' 
EXEC(@cmd)
go

--全部日志
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[back]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[back]
GO
declare @cmd varchar(8000)
set @cmd = 'CREATE VIEW dbo.back AS SELECT * from ['+db_name()+'log].dbo.back' 
EXEC(@cmd)
go


--160909 支持独立附件数据库
----------------------------------------------------------------------------------
--中间库视图 select * from fjkd
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[fjkd]') and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view [dbo].[fjkd]
GO

declare @err int,@having_mid int
declare @dbname varchar(80) 
declare @cmd varchar(8000)
select @err = 0

--检查数据库是否存在
if @err = 0
begin
	select @dbname = db_name() + 'fjk'
	select @having_mid = dbo.f_is_database(@dbname)
end

--创建view
if @err = 0
begin
	if @having_mid = 1
		select @cmd = 'CREATE VIEW dbo.fjkd AS SELECT * FROM '+ @dbname +'.dbo.fjk '
	else
		select @cmd = 'CREATE VIEW dbo.fjkd AS SELECT * FROM fjk '
	EXEC(@cmd)
end
go

--160909 支持独立附件数据库
----------------------------------------------------------------------------------
--附件 不包括文件image的View 少FJKnd
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[fjknd]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[fjknd]
GO

declare @err int,@having_mid int
declare @dbname varchar(80) 
declare @cmd varchar(8000)
select @err = 0

--检查数据库是否存在
if @err = 0
begin
	select @dbname = db_name() + 'fjk'
	select @having_mid = dbo.f_is_database(@dbname)
end

--创建view
if @err = 0
begin
	if @having_mid = 1
		select @cmd = 'CREATE VIEW fjknd AS SELECT [nid],[fid],[f1],[lxdm],[lxmc],[cjrq],[cjr],[dx],[sgrq],[wjm],[bz],[lxbm],' + 
						'[fjrq],[fjsj],[fb],[sy],[cjrid],[cjrg],[cjrgid],[cjra],[cjraid],[yslx],[klx],[xzbj] FROM ' + @dbname + '.dbo.[fjk]'
	else
		select @cmd = 'CREATE VIEW fjknd AS SELECT [nid],[fid],[f1],[lxdm],[lxmc],[cjrq],[cjr],[dx],[sgrq],[wjm],[bz],[lxbm],' + 
						'[fjrq],[fjsj],[fb],[sy],[cjrid],[cjrg],[cjrgid],[cjra],[cjraid],[yslx],[klx],[xzbj] FROM [fjk]'
	exec(@cmd)
end
GO

----------------------------------------------------------------------------------

--081201 登录前的第一个View
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[v_lg1]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[v_lg1]
GO

CREATE VIEW dbo.v_lg1  
AS  
	SELECT TOP 1 nid,fid,GSTT,gsywtt,gsdz,gsdh,gstwcz,gseml,MK,DQR,DQR0,DQR1,DQR2,DQR3,DQR6,DQR7,DQR8,
		SYS_KEY,SYS_KEY0,SYS_KEY1,SYS_KEY2,SYS_KEY3,SYS_KEY4,SYS_KEY5,SYS_KEY6,SYS_KEY7,SYS_KEY8,SYS_KEY9,pmbj,xtbj,
		dbo.f_kg('域地域') as kgydy, dbo.f_kg('OPT_ECKF') as kgfjgr, dbo.f_cxvalue('年份') as cyear,
		dbo.f_cxvalue('起始ETD') as BegdateRow, dbo.f_cxvalue('终止ETD') as EnddateRow, --无作用，仅兼容
		dbo.f_kg('SHARECONN') as ShareConn, dbo.f_kg('MVSTGGL') as MVSTGGL,
		dbo.f_kg('SAVEPASS') as SAVEPASS,dbo.f_kg('BCK_OPT') as BCK_OPT,dbo.f_kg('REC_LOCK') as REC_LOCK,
		dbo.f_kg('DYMENU') as DYMENU,dbo.f_kg('XJ') as XJ,dbo.f_kg('TAG_SECCO') as TAG_SECCO,
		dbo.f_kg('NO_LOGIN') as NO_LOGIN,gstt.xks
		from gstt with (nolock) where fid = '000000000000'
GO

--081202 登录后的参数初始
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[v_lg2]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[v_lg2]
GO

--dbo.f_get_ywdm_by_opm('SEA_EXP')/dbo.f_cxvalue('SEA_N_F')
CREATE VIEW dbo.v_lg2  
AS  
	SELECT 1 as nid,'01' as fid,
	dbo.f_csl_cxvalue('SEA_E_F') as sea_e_f,  
	dbo.f_csl_cxvalue('SEA_E_L') as sea_e_l, 
	dbo.f_cxvalue('SEA_I_F') as sea_i_f, 
	dbo.f_cxvalue('SEA_I_L') as sea_i_l, 
	dbo.f_get_ywdm_by_opm('AGT_CK') as agt_ck, 
	dbo.f_get_ywdm_by_opm('AGT_CS') as agt_cs, 
	dbo.f_get_ywdm_by_opm('AGT_CST') as agt_bg, 
	dbo.f_get_ywdm_by_opm('AGT_EXP') as agt_e, 
	dbo.f_get_ywdm_by_opm('AGT_IMP') as agt_i, 
	dbo.f_get_ywdm_by_opm('AIR_EXP') as air_e, 
	dbo.f_get_ywdm_by_opm('AIR_IMP') as air_i, 
	dbo.f_get_ywdm_by_opm('BARGE_JOB') as barge_job,
	dbo.f_get_ywdm_by_opm('CNT_IN') as cnt_in,
	dbo.f_get_ywdm_by_opm('CNT_OUT') as cnt_out,
	dbo.f_get_ywdm_by_opm('EIR_HND') as eir_hnd,	
	dbo.f_get_ywdm_by_opm('HW_CC') as hw_cc,
	dbo.f_get_ywdm_by_opm('HW_JC') as hw_jc,
	dbo.f_get_ywdm_by_opm('IMP_MAIN') as imp_main,
	dbo.f_get_ywdm_by_opm('IMP_SUB') as imp_sub,					
	dbo.f_get_ywdm_by_opm('INN_LD') as nm_ld,
	dbo.f_get_ywdm_by_opm('INN_RAIL') as nm_tl, 
	dbo.f_get_ywdm_by_opm('INN_RIVER') as nm_river,
	dbo.f_get_ywdm_by_opm('INN_ROAD') as nm_gl, 
	dbo.f_get_ywdm_by_opm('INN_SEA') as sea_n_f,  
	dbo.f_get_ywdm_by_opm('INN_SECC') as nm_secco, 
	dbo.f_get_ywdm_by_opm('INT_EXP') as int_exp,
	dbo.f_get_ywdm_by_opm('INT_TL') as int_tl, 
	dbo.f_get_ywdm_by_opm('INT_TRADE') as mtrade,
	dbo.f_get_ywdm_by_opm('PLN_IN') as pln_in,	
	dbo.f_get_ywdm_by_opm('SEA_EXP') as sea_exp,		
	dbo.f_get_ywdm_by_opm('SEA_HP') as sea_hp, 
	dbo.f_get_ywdm_by_opm('SEA_IMP') as sea_imp,
	dbo.f_get_ywdm_by_opm('SH_EXP') as SEA_E_B, 
	dbo.f_get_ywdm_by_opm('SH_HP') as SH_HP,  
	dbo.f_get_ywdm_by_opm('SH_IMP') as SEA_I_B, 
	dbo.f_get_ywdm_by_opm('SOC_IN') as SOC_IN, --dbo.f_get_ywdm_by_opm('SOC_EX') as SOC_EX, 
	dbo.f_get_ywdm_by_opm('STOCK_BJ') as STOCK_BJ, 
	dbo.f_get_ywdm_by_opm('STOCK_EX') as STOCK_EX, 
	dbo.f_get_ywdm_by_opm('STOCK_FW') as STOCK_FW, 
	dbo.f_get_ywdm_by_opm('STOCK_IN') as STOCK_IN, 
	dbo.f_get_ywdm_by_opm('STOCK_JY') as STOCK_JY, 
	dbo.f_get_ywdm_by_opm('YS_CD') as YS_CD,
	dbo.f_get_ywdm_by_opm('SH_PH') as SH_PH, --为升级保留的，否则升级前报错
	dbo.f_cxvalue('BG_CODE') as BG_CODE, 
	dbo.f_cxvalue('CS_CODE') as CS_CODE, 
	dbo.f_cxvalue('SALESCALC') as SALESCALC, 
	COL_LENGTH('CKSYK','F1') as PF1SJCD,  COL_LENGTH('POSYK','PONO') as POSJCD,  
	dbo.f_bzhl_yf('USD',left(dbo.f_mydate1(dbo.f_getdate()),4),'S','') as mhl, 
	ver,bv from db with (nolock) where fid='000000000001'
GO

--081202 模块切换后的参数初始
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[v_lg3]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[v_lg3]
GO

CREATE VIEW dbo.v_lg3  
AS
	SELECT nid,fid,fpfrx,gkdm,gkdma,gkqc,tdqfd from gstt
GO

--081202 开关初始
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[v_lg4]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[v_lg4]
GO

CREATE VIEW dbo.v_lg4  
AS
	SELECT  1 as nid,'01' as fid,  dbo.f_kg('LCK_YW') as kg1, dbo.f_kg('LCK_SW') as kg2, dbo.f_kg('LCK_HS') as kg3, 
		dbo.f_kg('专项代收 ') as kg4, dbo.f_kg('商务无应付') as kg5, dbo.f_kg('江海模式 ') as kg6, 
		dbo.f_kg('标识黑名单 ') as kg7, dbo.f_kg('操作 ') as kg9, dbo.f_kg('单证 ') as kgA, 
		dbo.f_kg('OPT_ADMIN') as kgB, dbo.f_kg('航线 ') as kgC, 1 as kgD, 
		dbo.f_kg('YSTK_KEYIN ') as kgE, dbo.f_kg('订舱编号 ') as kgF, dbo.f_kg('订舱 ') as kgG, 
		dbo.f_kg('船名航次 ') as kgH, dbo.f_kg('海船船公司 ') as kgI, dbo.f_kg('ETDETA ') as kgJ, 
		dbo.f_kg('驳船航次 ') as kgK, dbo.f_kg('驳船ETD ') as kgL, dbo.f_kg('承运人 ') as kgM, 
		dbo.f_kg('代理提单号 ') as kgN, dbo.f_kg('输单控制 ') as kg12, dbo.f_kg('业务审批 ') as kg13, 
		dbo.f_kg('单证与业务 ') as kg14, dbo.f_kg('随附新增 ') as kg15, dbo.f_kg('CARGO_UNIT ') as kg16, 
		dbo.f_kg('OPT_BGGD ') as kg17, dbo.f_kg('加权分摊 ') as kg18, dbo.f_kg('发票抬头 ') as kg19, 
		dbo.f_kg('收付管理人 ') as kg20, dbo.f_kg('预置数量 ') as kg21, dbo.f_kg('费收新增 ') as kg23, 
		dbo.f_kg('DEL_SPGL') as kg22, dbo.f_kg('KP输入 ') as kg24, dbo.f_kg('外运模式 ') as kg25, 
		dbo.f_kg('FY不用箱量 ') as kg26, dbo.f_kg('无管理人 ') as kg27, dbo.f_kg('代码取消 ') as kg28, 
		dbo.f_kg('代垫处理 ') as kg29, dbo.f_kg('专项合计数 ') as kg30, dbo.f_kg('海船港区 ') as kg31, 
		dbo.f_kg('专项合计数 ') as kg32, dbo.f_kg('MAILATTACH ') as kg33, dbo.f_kg('操作拉提单 ') as kg34, 
		dbo.f_kg('二次拉提单 ') as kg35, dbo.f_kg('表达式检查 ') as kg36, dbo.f_kg('自动加高 ') as kg37, 
		dbo.f_cxvalue('ALERTCOLOR ') as kg38, dbo.f_cxvalue('TRADE') as kg39, dbo.f_cxvalue('AIRFEICODE') as kg40, 
		dbo.f_kg('SUBMIT_SD') as kg41, dbo.f_kg('SUBMIT_YW') as kg42, dbo.f_kg('SW_MODE') as kg43, 
		dbo.f_kg('AT') as kg44, dbo.f_kg('AGT_CD') as kg45, dbo.f_cxvalue('凭证预估 ') as kg46, 
		dbo.f_cxvalue('凭证应收 ') as kg47, dbo.f_cxvalue('凭证应付 ') as kg48, 
		dbo.f_cxvalue('凭证实收 ') as kg49, dbo.f_cxvalue('凭证实付 ') as kg50, 
		dbo.f_cxvalue('凭证应收差 ') as kg51, dbo.f_cxvalue('凭证应付差 ') as kg52, 
		dbo.f_kg('委托方控制 ') as kgZ
GO

--货物明细第一行视图
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[zxd0n]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[zxd0n]
GO

CREATE VIEW [dbo].[zxd0n] 
AS 
	select * from zxd0 where not EXISTS(select 1 from zxd0 a where zxd0.zxdfid=a.zxdfid and a.fid<zxd0.fid)
GO

--申报货物明细第一行视图
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[zxd1n]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[zxd1n]
GO

CREATE VIEW [dbo].[zxd1n] 
AS 
	select * from zxd1 where not EXISTS(select 1 from zxd1 a where zxd1.f1=a.f1 and a.fid<zxd1.fid)
GO

--装箱明细
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[zxdmx]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[zxdmx]
GO

CREATE VIEW dbo.zxdmx
AS 
SELECT zxd0.nid,zxd0.fid,zxdfid,zxd0.f1 as zxd0f1,f31,jcbh,js,mz,tj,zwhm,mt,fh,tz,ydh,fhdh0,xsdh0,bzdm0,bzmc0,bzjs0,cgdh0,erpno,gqddh,hwsfrq,hwqsrq,  
	zxd.f1,y1,y2,xx,xxmc,xxdx,xxdxm,bz,zjs,ztj,zmz,zxdno,bzdw,zxyq,wxp,ldx,tg,cg,
	zxd.fdrq1,zxd.fdsj1,zxd.zxrq,zxd.zxsj,zxd.jgrq,zxd.jgsj,zxd.ywc, --放单,装箱,进港
	zxd.zzyh,hxr,yxts,mfts, --nid,fid
	cqts,cqdj,cqje,hxf,wxf,soc,xztdm,xztmc,gndxh,ch,pzh,xzdm,xzmc,gwch,bzbz,cddm,cdmc,gqxw,xf1,shcddm,shcdmc,
	zxd.fdrq2,zxd.fdsj2,zxd.tzrq,zxd.tzsj,zxd.sdrq,zxd.sdsj,zxd.ywc2,
	shyq,zhch,zhtgch,zhcjsy,zhcjsydh,shch,shtgch,shcjsy,shcjsydh,xyjzxrq,xyjzxsj,xyjshrq,xyjshsj,xzxdz,xzxlxr,xzxdh,xzxcz,xzxmc,
	xshdz,xshlxr,xshdh,xshcz,xshmc,pcbj,hth,fhrq,mddpcbj,shdbh,zhdbh,fhzlrq,fhzlsj,
	dlshd,khshd,xydczr,xydczsd,xqshd,zhjc,shjc,
	zxd.sm,zxd.sm2,zxd.zhtcf,zxd.shtcf,zxd.ckfid,zxd.jkfid
 FROM ZXD0 left join zxd on zxd.fid=zxd0.zxdfid
go

--装箱明细,只有箱表 F1,明细表 F1、F31
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[zxd0mx]') 
	and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[zxd0mx]
GO

create view dbo.zxd0mx
as 
select zxd0.nid,zxd0.fid,zxd0.f1 as zxd0f1,zxd0.f31,zxd.xf1,zxd.f1,zxd.y1,zxd.y2
	from zxd0  left join zxd on zxd.fid=zxd0.zxdfid
go

--装箱明细,箱内货物只有一行
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[zxdmxn]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[zxdmxn]
GO
--select * from zxdmxn where f1 = 'KTCC1100001'
CREATE VIEW dbo.zxdmxn
AS
SELECT zxd0n.nid,zxd0n.fid,zxd0n.zxdfid,zxd0n.f1 as zxd0nf1,
	zxd0n.f31,zxd0n.jcbh,zxd0n.js,zxd0n.mz,zxd0n.tj,zxd0n.zwhm,zxd0n.mt,
	zxd0n.fh,zxd0n.tz,zxd0n.ydh,zxd0n.fhdh0,zxd0n.xsdh0,zxd0n.bzdm0,zxd0n.bzmc0,zxd0n.bzjs0,zxd0n.cgdh0,
	zxd0n.erpno,zxd0n.gqddh,zxd0n.hwsfrq,zxd0n.hwqsrq,  
	zxd.f1,y1,y2,xx,xxmc,xxdx,xxdxm,bz,zjs,ztj,zmz,zxdno,bzdw,zxyq,wxp,ldx,tg,cg,
	zxd.fdrq1,zxd.fdsj1,zxd.zxrq,zxd.zxsj,zxd.jgrq,zxd.jgsj,zxd.ywc, --放单,装箱,进港
	zxd.zzyh,hxr,yxts,mfts, --nid,fid
	cqts,cqdj,cqje,hxf,wxf,soc,xztdm,xztmc,gndxh,ch,pzh,xzdm,xzmc,gwch,bzbz,cddm,cdmc,gqxw,xf1,shcddm,shcdmc,
	zxd.fdrq2,zxd.fdsj2,zxd.tzrq,zxd.tzsj,zxd.sdrq,zxd.sdsj,zxd.ywc2,
	shyq,zhch,zhtgch,zhcjsy,zhcjsydh,shch,shtgch,shcjsy,shcjsydh,xyjzxrq,xyjzxsj,xyjshrq,xyjshsj,xzxdz,xzxlxr,xzxdh,xzxcz,xzxmc,
	xshdz,xshlxr,xshdh,xshcz,xshmc,pcbj,hth,fhrq,mddpcbj,shdbh,zhdbh,fhzlrq,fhzlsj,
	dlshd,khshd,xydczr,xydczsd,xqshd,zhjc,shjc,
	zxd.sm,zxd.sm2,zxd.zhtcf,zxd.shtcf,zxd.ckfid,zxd.jkfid
	from zxd0n
	left join zxd on zxd.fid=zxd0n.zxdfid 
go

--帐户余额
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[zhye]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[zhye]
GO

CREATE VIEW dbo.zhye  
AS  
SELECT f1,nid,fid,biz,sssj,hsyhdm,hsyhmc,sjbz,hr,sjje,
	case when fl='+' then sshj else 0 end as d,
	case when fl='-' then sshj else 0 end as c,
	dbo.f_get_ye(fid,sssj,'RMB') as rye,
	dbo.f_get_ye(fid,sssj,'USD') as uye,
	dbo.f_getf8byf9(ffrdm) as f9
	from swsf
	where ssbj='*'  and sssj<>'' and hsyhdm<>''
GO



--8.00.053
--箱跟踪时所要用的业务表
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[xg_gz_yw]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[xg_gz_yw]
GO

CREATE VIEW dbo.xg_gz_yw  
AS  
SELECT cksyk.nid,cksyk.f1,cksyk.f31,cksyk.f29,cksyk.wtf2dm,cksyk.wtf2,dbj2.f10dm,dbj2.f10,cksyk.f11dm,
	cksyk.f11,dbj2.f12dm,dbj2.f12,cksyk.khbh,cksyk.f32,cksyk.atd,cast(dbj1.f40hm as varchar(250)) as f40hm,
	cksyk.zwhm,dbj1.qcys,dbj2.bcetd
	from cksyk 
	left join dbj2 on cksyk.f1 = dbj2.f1
	left join dbj1 on cksyk.f1 = dbj1.f1
	left join wjmywk on cksyk.f1 = wjmywk.f1
GO


--最新箱状态视图
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[xg_gzn]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[xg_gzn]
GO

CREATE VIEW [dbo].[xg_gzn] 
AS 
	--select * from xg_gz where not EXISTS(select 1 from xg_gz a where xg_gz.mfid=a.mfid and a.nid>xg_gz.nid) --记录先后方法
	select * from xg_gz where not EXISTS(select 1 from xg_gz a where xg_gz.mfid=a.mfid and (a.rq+a.sj+a.fid)>(xg_gz.rq+xg_gz.sj+xg_gz.fid))
GO

--箱号FID视图
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[xg_xh]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[xg_xh]
GO

CREATE VIEW dbo.xg_xh  
AS  
SELECT xg_xt.nid,xg_xt.fid,xg_xt.y1	from xg_xt 
GO


--上一年度往来单位db1
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[l_db1]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[l_db1]
GO

declare @cnt int,@lastdb varchar(20),@cmd varchar(4000),@dbname varchar(20)
select @cnt = 0,@cmd='',@lastdb='',@dbname=''

select @lastdb = rtrim(dbo.f_cxvalue('LASTDB')) --取上年度
select @lastdb = case when @lastdb = '' or not exists(select * from master.dbo.sysdatabases where name = @lastdb) 
					then db_name() else @lastdb end --不存在取当前

select @cmd ='CREATE VIEW dbo.l_db1'+char(13)+char(10)+
			'AS'+char(13)+char(10)+  
			'SELECT * FROM ['+@lastdb+'].dbo.db1'
--print @cmd		
exec (@cmd)
go

--上一年度往来单位users1
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[l_users1]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[l_users1]
GO

declare @cnt int,@lastdb varchar(20),@cmd varchar(4000),@dbname varchar(20)
select @cnt = 0,@cmd='',@lastdb='',@dbname=''

select @lastdb = rtrim(dbo.f_cxvalue('LASTDB')) --取上年度
select @lastdb = case when @lastdb = '' or not exists(select * from master.dbo.sysdatabases where name = @lastdb) 
					then db_name() else @lastdb end --不存在取当前

select @cmd ='CREATE VIEW dbo.l_users1'+char(13)+char(10)+
			'AS'+char(13)+char(10)+  
			'SELECT * FROM ['+@lastdb+'].dbo.users1'
--print @cmd		
exec (@cmd)
go

--区县代码 唯一名称
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[qx]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[qx]
GO

CREATE VIEW dbo.qx 
AS  
select * from qxdm a where not exists(select 1 from qxdm where nid > a.nid and mc = a.mc ) 
go

--箱跟踪，按JOBNO为主键
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[vzxgz]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[vzxgz]
GO

CREATE VIEW dbo.vzxgz
AS
	select * from zxgz 
go
 

--拼箱的包装箱号
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[v_zxd5]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[v_zxd5]
GO

CREATE VIEW dbo.v_zxd5 
AS
	select zxd5.nid,zxd5.fid,zxd5.f1,cksyk.fm1,zxd5.pno from zxd5 left join cksyk on zxd5.f1 = cksyk.f1
go


--出口船期表
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[cbde]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[cbde]
GO

CREATE VIEW dbo.cbde 
AS
	select * from cbdtk where cbdtk.ei = 'E'
go

--进口船期表
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[cbdi]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[cbdi]
GO

CREATE VIEW dbo.cbdi 
AS
	select * from cbdtk where cbdtk.ei = 'I'
go


--出口船期
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[cbocbe]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[cbocbe]
GO

CREATE VIEW dbo.cbocbe  
AS  
select cbdtk.f29,cbdtk.nid,cbdtk.fid,cbdtk.f32 from cbdtk where cbdtk.ei = 'E'
GO


--初始数据
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[kl_dist]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[kl_dist]
GO

CREATE VIEW dbo.kl_dist  
AS  
select * from kl where xm='系统员' or FJD='50_' or xm ='系统组' or xm = '本部' 
GO

--初始数据
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[td_dist]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[td_dist]
GO

CREATE VIEW dbo.td_dist  
AS  
select * from tidanb 
	where not (no>'K02' and NO<'K99') AND
		not (no>'D02' and NO<='DZZ')
		or tidanb.no in ('K03','K05','K11','K16','K17','DC1')
		
GO

--[3gfax_SendFaxInfo]视图select * from sgfaxsend
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[3gfax_SendFaxInfo]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[3gfax_SendFaxInfo]
GO

CREATE VIEW dbo.[3gfax_SendFaxInfo ]
AS  
select nid as listno,fid,f1,taskid,subtime,sendername,rcvername,faxnum,schsendtime,userid,filename,filepath,
sendfaxnum,status,transstatus,sendtime,senddescribe,transfilename,transfilepath,filesize,username,logonname
from sgfaxsend
GO


--运价目录
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[khyjml]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[khyjml]
GO
CREATE VIEW dbo.[khyjml]
AS  
select nid,fid,'1' as ly,bm,xm,glr,glrid,glz,glzid,glra,glraid,lxdm,lx,        fwdm,fwmc,fwmc2,hxdm,hx from khyjmenu where bm <> '' union
select nid,fid,'2',      f9,f8,glr,glrid,bm, bmid, glra,glraid,'11','客户报价','','','',       hxdm,hx from db1 where f9<>''
go

--运价费用项目
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[PriceItem]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[PriceItem]
GO
CREATE VIEW dbo.[PriceItem]
AS  
--合成主运费的显示序号，费用代码、费用名称
select nid,'0'+fid as fid,priceno,'0     ' as xsxh,'OF  ' as xmdm,'Ocean Freight'+space(27) as xmmc,dj,f20,f40,f42,f45,biz,dw,bz from khyjxy union
select khyjmx.nid,'1'+khyjmx.fid,khyjxy.priceno,khyjmx.xsxh,khyjmx.xmdm,khyjmx.xmmc,
	khyjmx.dj,khyjmx.f20,khyjmx.f40,khyjmx.f42,khyjmx.f45,khyjmx.biz,khyjmx.dw,khyjmx.bz 
	from khyjmx left join khyjxy on khyjmx.mfid = khyjxy.fid
go

--主程+多程视图
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dcv]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[dcv]
GO

CREATE VIEW dbo.dcv  
AS  
select 0-cksyk.nid as nid,cksyk.f1 as fid,cksyk.f1 as f1,'0' as ch,f29 as mv,f32 as dcetd,eta as dceta,f10dm,f10,f11dm,f11,f31,dbj2.bz1 as bz,
	wjmywk.ysfs as ysfs,ysfn as ysfsn,cgsdm as cyrdm,cgsmc as cyrmc,cksyk.ywqr as qr,
	cksyk.zzyh as xzr,dbj2.f2 as xzrq,dbj2.fcmdm,dbj2.fcm,dbj2.fhc 
	from cksyk
	left join dbj2 on cksyk.f1=dbj2.f1
	left join wjmywk on cksyk.f1=wjmywk.f1
union
select nid,fid,f1,ch,mv,dcetd,dceta,f10dm,f10,f11dm,f11,f31,bz,
	ysfs,ysfsn,cyrdm,cyrmc,qr,xzr,xzrq,fcmdm,fcm,fhc from dcinfo where mv<> ''
GO

--报关单商品项目视图
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[v_bgdmx]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[v_bgdmx]
GO

CREATE VIEW dbo.v_bgdmx
AS
SELECT     fid,f1, ylrbh,1 no, xh1 xh, spbh1 spbh, spmz1 spmz, sljdw1 sljdw, zzmdm1 zzmdm, zzmdg1 zzmdg, dja1 dja, djb1 djb, zja1 zja, zjb1 zjb, bza1 bza, bzb1 bzb, zm1 zm, 
                      spbhm1 spbhm
FROM         bgd
UNION ALL
SELECT     fid,f1, ylrbh,2 no,xh2 xh, spbh2 spbh, spmz2 spmz, sljdw2 sljdw, zzmdm2 zzmdm, zzmdg2 zzmdg, dja2 dja, djb2 djb, zja2 zja, zjb2 zjb, bza2 bza, bzb2 bzb, zm2 zm, 
                      spbhm2 spbhm
FROM         bgd
UNION ALL
SELECT     fid,f1, ylrbh,3 no, xh3 xh, spbh3 spbh, spmz3 spmz, sljdw3 sljdw, zzmdm3 zzmdm, zzmdg3 zzmdg, dja3 dja, djb3 djb, zja3 zja, zjb3 zjb, bza3 bza, bzb3 bzb, zm3 zm, 
                      spbhm3 spbhm
FROM         bgd
UNION ALL
SELECT     fid,f1, ylrbh,4 no, xh4 xh, spbh4 spbh, spmz4 spmz, sljdw4 sljdw, zzmdm4 zzmdm, zzmdg4 zzmdg, dja4 dja, djb4 djb, zja4 zja, zjb4 zjb, bza4 bza, bzb4 bzb, zm4 zm, 
                      spbhm4 spbhm
FROM         bgd
UNION ALL
SELECT     fid,f1, ylrbh,5 no, xh5 xh, spbh5 spbh, spmz5 spmz, sljdw5 sljdw, zzmdm5 zzmdm, zzmdg5 zzmdg, dja5 dja, djb5 djb, zja5 zja, zjb5 zjb, bza5 bza, bzb5 bzb, zm5 zm, 
                      spbhm5 spbhm
FROM         bgd

GO


--杂项 一对一
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swtbx]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swtbx]
GO

CREATE VIEW dbo.swtbx  
AS  
SELECT nid as nid, f1 AS f1, yshj4 AS YSHJ, sshj4 AS SSHJ, yfbfhj4 AS YFBFHJ, yfsjze4 AS YFSJZE,   
      bizhong4 AS BIZHONG, lr4 AS LR, srbj4 AS SRBJ, yskpbj4 AS YSKPBJ,   
      ssbj4 AS SSBJ, bz4 AS BZ, ystz4 as ystz, yftz4 as yftz, ltz4 as ltz    
FROM dbo.sw  

GO



--特别项 应付
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swtbxbf]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swtbxbf]
GO

CREATE VIEW dbo.swtbxbf  
AS  
select *, 
	 ysfph as yfbffph, yshj as yfbfje, xmh1 as xmdm, sfnr1 as yfbfxm, sshj as sfbfje, sssj as yfbfrq, 
	 ssbj as yffkbj, ffrdm as sfrdm, ffr as sfr, sl1 as sl, ysrq as dzrq, yskpbj as dzbj
FROM dbo.swsf   
WHERE (FL = '-' and LY = '4')   
GO


--特别项 应收
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swtbxbk]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swtbxbk]
GO

CREATE VIEW dbo.swtbxbk  
AS  
SELECT *  
FROM dbo.swsf 
WHERE (FL = '+' and LY = '4')  
GO



----ccf1别名 进仓明细,只有一个JOBNO
--if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ccf1]') and OBJECTPROPERTY(id, N'IsView') = 1)
--drop view [dbo].[ccf1]
--GO

--CREATE VIEW dbo.ccf1
--AS
--select distinct f1  from cchw
--GO

--新的算法 160407
--进仓 基本view
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[cchwjc0]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[cchwjc0]
GO

CREATE VIEW dbo.cchwjc0 
AS
	select cchw.nid,cchw.fid,cchw.f1,dbj2.f3,cchw.hh,cchw.zwhm,cchw.mt,cchw.dwdm,cchw.dwmc,
		cchw.jljs,cchw.js,cchw.hwc,cchw.hwk,cchw.hwg,cchw.dwtj,cchw.jstj,cchw.dwmz,cchw.jsmz,cchw.hwcw,cchw.hwps,
		cchw.ysxcf,cchw.ysccf,cchw.f31 as cchwf31,
		dbj2.rkrq,dbj2.rksj,dbj6.fhfmc, 
		cksyk.zzyhaid,cksyk.zzyha,
		cchw.ccjs, --dbo.f_sum_cc_js(cchw.fid) as ccjs,
		cchw.ccjljs, --dbo.f_sum_cc_jljs(cchw.fid) as ccjljs 
		cchw.zcjs,
		cchw.zcjljs,
		cchw.po,
		cchw.kh
		from cchw 
		left join cksyk on cksyk.f1 = cchw.f1 
		left join dbj2 on dbj2.f1 = cchw.f1
		left join dbj6 on dbj6.f1 = cchw.f1
		where cksyk.opm = 'hw_jc'
go

--进仓,出仓件数,在仓件数 过滤在仓数量
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[cchwjc]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[cchwjc]
GO

CREATE VIEW dbo.cchwjc 
AS
	select *, --cchwjc0.js-cchwjc0.ccjs as zcjs,cchwjc0.jljs - cchwjc0.ccjljs as zcjljs,
		0 as nxz from cchwjc0 where cchwjc0.f3 <> '' and (cchwjc0.zcjs<>0 or cchwjc0.zcjljs<>0)
		--nxz 选择多个
go

--装箱指令视图（按进仓编号及预进仓编号分组）
--select * from cchw where f1 = 'YFMB13009268' order by f3,f4
--select * from cczxzl where f1 = 'YFMB13009268' order by f3
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[cczxzl]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[cczxzl]
GO

CREATE VIEW dbo.cczxzl
AS  
select min(nid) as nid,min(f1) as f1,
	case when rtrim(f3) = '' then f4 else f3 end as f3,min(hh) as hh,min(zwhm) as zwhm,
	min(mt) as mt,min(dwdm) as dwdm,min(dwmc) as dwmc,sum(js) as js,sum(jstj) as jstj,
	sum(jsmz) as jsmz,min(hwcw) as hwcw,sum(hwps) as hwps,min(f31) as f31,
	--sum(dbo.f_strtonum(js0)) as js0,sum(dbo.f_strtonum(mz)) as mz,sum(dbo.f_strtonum(tj)) as tj,
	sum(jljs) as jljs,
	min(qr) as qr,min(zzyh) as zzyh,min(xzrq) as xzrq,min(xzsj) as xzsj,max(bz) as bz,count(nid) as cnt
from cchw with(nolock) group by f1,case when rtrim(f3) = '' then f4 else f3 end
GO

--仓储业务移动设备视图-进出仓业务
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Mcc]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[Mcc]
GO
CREATE VIEW dbo.Mcc
AS
	select cksyk.f1,cksyk.opm,cksyk.wtf2,cksyk.yw,cksyk.zwhm,cksyk.bk,cksyk.ywqr,cksyk.hsbj,cksyk.f29,
	dbj1.f40js1,dbj1.f40mz1,dbj1.f40tj1,dbj1.f40mt,dbj1.zxgq,dbj1.czyq,
	dbj2.f3,dbj2.cycd,
	case when cksyk.opm = 'hw_jc' then dbj2.rkrq else dbj2.ckrq end as rkrq,
	case when cksyk.opm = 'hw_jc' then dbj2.rksj else dbj2.cksj end as rksj,dbj2.jmr,dbj2.jmsj,
	dbj6.ch,dbj6.yhbj,dbj6.yhsj,dbj6.yhrq,dbj6.yhjl,dbj6.psjs,dbj6.psbj,
	dbj6.jcjs,dbj6.jcmz,dbj6.jctj,
	dbj6.ckmc,dbj2.bz2,dbj6.dx + dbj6.xx as xxdx,dbj6.y1
	from cksyk with(nolock) 
	left join dbj1 with(nolock) on cksyk.f1 = dbj1.f1
	left join dbj2 with(nolock) on cksyk.f1 = dbj2.f1
	left join dbj6 with(nolock) on cksyk.f1 = dbj6.f1
	where cksyk.opm = 'hw_jc' or cksyk.opm = 'hw_cc'
go


--出仓业务明细记录移动设备视图
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Mcchwcc]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[Mcchwcc]
GO
CREATE VIEW dbo.Mcchwcc
AS
	select cchw.fid,cchw.f1,cchw.f3,dbo.f_get_jc_f1(cchw.jcfid) as jcF1,cchw.yhbj,cchw.yhrq,cchw.yhsj from cchw 
		left join cksyk on cchw.f1 = cksyk.f1 
		where cksyk.opm = 'HW_CC'
go


--cksyk.doc 装卸
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[wrk_cdoc]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[wrk_cdoc]
GO

CREATE VIEW dbo.wrk_cdoc  
AS  
SELECT * FROM dbo.worker where worker.lx = 'cdoc'
GO

--cksyk.op 机械
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[wrk_cop]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[wrk_cop]
GO

CREATE VIEW dbo.wrk_cop  
AS  
SELECT * FROM dbo.worker where worker.lx = 'cop'
GO

--cksyk.doc 理货
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[wrk_cbk]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[wrk_cbk]
GO

CREATE VIEW dbo.wrk_cbk  
AS  
SELECT * FROM dbo.worker where worker.lx = 'cbk'
GO

--工作量 增值服务
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[wrk_add]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[wrk_add]
GO

CREATE VIEW dbo.wrk_add
AS  
SELECT * FROM dbo.worker where worker.lx = 'add'
GO

--货物出仓 增加进仓业务的利润
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[cchw_jcml]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[cchw_jcml]
GO

CREATE VIEW dbo.cchw_jcml  
AS  
	select cchw.*,ccjc.f1 as jcf1,dbj6.jcjs,sw.lsum,
		cast(case when dbj6.jcjs = 0 then 0 else cchw.js / dbj6.jcjs * 100 end as numeric(19,2)) as bl,
		cast(sw.lsum * case when dbj6.jcjs = 0 then 0 else cchw.js / dbj6.jcjs end as numeric(19,2)) as jcml
		from cchw 
		left join cchw ccjc with (nolock) on cchw.jcfid = ccjc.fid
		left join cksyk with (nolock) on cchw.f1 = cksyk.f1
		left join dbj6 with (nolock) on ccjc.f1 = dbj6.f1
		left join sw with (nolock) on ccjc.f1 = sw.f1
		where cksyk.opm = 'HW_CC' and cchw.jcfid <> '' --AND cchw.f1 = '10'
GO

--货物进出仓人员考核平均数
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[workeravg]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[workeravg]
GO

CREATE VIEW dbo.workeravg  
AS  
	select worker.*,
	dbo.f_get_worker_count(worker.f1,lx) as wrknum,
	dbo.f_get_worker_average_cbm(worker.f1,lx) as avgcbm,
	dbo.f_get_worker_average_kgs(worker.f1,lx) as avgkgs,
	dbo.f_get_worker_average_js(worker.f1,lx) as avgjs
	from worker
	--dbo.f_get_worker_percent_cbm(worker.f1,bfb) as percbm,
	--dbo.f_get_worker_percent_kgs(worker.f1,bfb) as perkgs,
	--dbo.f_get_worker_percent_js(worker.f1,bfb) as perjs
GO

--cksyk.doc 装卸
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[wrka_cdoc]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[wrka_cdoc]
GO

CREATE VIEW dbo.wrka_cdoc  
AS  
SELECT * FROM dbo.workeravg where workeravg.lx = 'cdoc'
GO

--cksyk.op 机械
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[wrka_cop]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[wrka_cop]
GO

CREATE VIEW dbo.wrka_cop  
AS  
SELECT * FROM dbo.workeravg where workeravg.lx = 'cop'
GO

--cksyk.doc 理货
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[wrka_cbk]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[wrka_cbk]
GO

CREATE VIEW dbo.wrka_cbk  
AS  
SELECT * FROM dbo.workeravg where workeravg.lx = 'cbk'
go


----------------------------------------------------------------------------------
--进仓费模式搜索转换视图
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[db1cc_m]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[db1cc_m]
GO

CREATE VIEW dbo.db1cc_m  
AS  
SELECT *,rtrim(REPLACE(f3,'…','%')) as f3_m FROM dbo.db1cc --此处需要去右空
	where db1cc.f3 <> '…' and db1cc.f3 <> '' and db1cc.f3 <> '……' and db1cc.f3 <> '………'
GO


------------------------------------------------------------------------------
--地址_发货人
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dz_s]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[dz_s]
GO

CREATE VIEW dbo.dz_s  
AS  
SELECT * from dzdm where lx = 'S'
GO

--地址_收货人
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dz_c]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[dz_c]
GO

CREATE VIEW dbo.dz_c  
AS  
SELECT * from dzdm where lx = 'C'
GO

--地址_通知人
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dz_n]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[dz_n]
GO

CREATE VIEW dbo.dz_n  
AS  
SELECT * from dzdm where lx = 'N'
GO

--地址_同时通知
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[dz_a]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[dz_a]
GO

CREATE VIEW dbo.dz_a  
AS  
SELECT * from dzdm where lx = 'A'
GO


----------------------------------------------------------------------------------
--中间库视图 select * from ccsnd
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ccsnd]') and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view [dbo].[ccsnd]
GO

declare @err int,@having_mid int
declare @dbname varchar(80) 
declare @cmd varchar(8000)
select @err = 0

--检查数据库是否存在
if @err = 0
begin
	select @dbname = db_name() + 'cc'
	select @having_mid = dbo.f_is_database(@dbname)
end

--创建view
if @err = 0
begin
	if @having_mid = 1
		select @cmd = 'CREATE VIEW dbo.ccsnd AS SELECT * FROM '+ @dbname +'.dbo.ccsend '
	else
		select @cmd = 'CREATE VIEW dbo.ccsnd AS SELECT * FROM ccsend '
	EXEC(@cmd)
end
go


----------------------------------------------------------------------------------
--中间库视图ccsndmx select * from ccsndmx
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ccsndmx]') and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view [dbo].[ccsndmx]
GO

declare @err int,@having_mid int
declare @dbname varchar(80) 
declare @cmd varchar(8000)
select @err = 0

--检查数据库是否存在
if @err = 0
begin
	select @dbname = db_name() + 'cc'
	select @having_mid =  dbo.f_is_database(@dbname) 
end

--创建view
if @err = 0
begin
	if @having_mid = 1
		select @cmd = 'CREATE VIEW dbo.ccsndmx AS SELECT * FROM ' + @dbname + '.dbo.ccsendmx '
	else
		select @cmd = 'CREATE VIEW dbo.ccsndmx AS SELECT * FROM ccsendmx '
	EXEC(@cmd)
end
go

----------------------------------------------------------------------------------
--中间库视图 select * from ccfj
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ccfj]') and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view [dbo].[ccfj]
GO

declare @err int,@having_mid int
declare @dbname varchar(80) 
declare @cmd varchar(8000)
select @err = 0

--检查数据库是否存在
if @err = 0
begin
	select @dbname = db_name() + 'cc'
	select @having_mid = dbo.f_is_database(@dbname)
end

--创建view
if @err = 0
begin
	if @having_mid = 1
		select @cmd = 'CREATE VIEW dbo.ccfj AS SELECT * FROM '+ @dbname +'.dbo.ccfjk '
	else
		select @cmd = 'CREATE VIEW dbo.ccfj AS SELECT * FROM ccfjk '
	EXEC(@cmd)
end
go

------------------------------------------------------------------------------------
--以下财务
--期初余额
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[f_pz_qc]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[f_pz_qc]
GO

CREATE VIEW dbo.f_pz_qc  
AS  
SELECT * from f_pz where lx = '0'
GO

--期末余额
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[f_pz_qm]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[f_pz_qm]
GO

CREATE VIEW dbo.f_pz_qm  
AS  
SELECT * from f_pz where lx = '9'
GO


--凭证
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[f_vch]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[f_vch]
GO

CREATE VIEW dbo.f_vch  
AS  
SELECT * from f_pz where lx = '1'
GO

--明细科目
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[f_km_mx]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[f_km_mx]
GO

CREATE VIEW dbo.f_km_mx  
AS  
SELECT * from f_km where hz = '' and fc = ''
GO

--明细帐薄
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[f_mxz]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[f_mxz]
GO

CREATE VIEW dbo.f_mxz 
AS  
SELECT * from f_zb where fl = '1'
GO

--select * from f_mxz order by f_mxz.jdid,f_mxz.nf,f_mxz.yf,f_mxz.km,f_mxz.ly,f_mxz.rq,f_mxz.pdfid

--总帐帐薄
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[f_zz]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[f_zz]
GO

CREATE VIEW dbo.f_zz 
AS  
SELECT f_zb.* from f_zb left join f_km with (nolock) on f_zb.km = f_km.km where f_zb.fl = '1' and f_zb.ly <> '1' and f_km.jc = '1'
GO
--select * from f_zz order by f_zz.jdid,f_zz.nf,f_zz.yf,f_zz.km,f_zz.ly,f_zz.rq

--银行明细帐
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[f_yhz]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[f_yhz]
GO

CREATE VIEW dbo.f_yhz 
AS  
SELECT * from f_zb where fl = '3'
GO

--往来明细帐
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[f_wlz]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[f_wlz]
GO

CREATE VIEW dbo.f_wlz 
AS  
SELECT * from f_zb where fl = '5'
GO

--部门明细帐
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[f_bmz]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[f_bmz]
GO

CREATE VIEW dbo.f_bmz 
AS  
SELECT * from f_zb where fl = '7'
GO

--以上财务
------------------------------------------------------------------------------------

--RMB 应收 箱 用于删除时
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swrmbbkx]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swrmbbkx]
GO

CREATE VIEW dbo.swrmbbkx  
AS  
SELECT *  
FROM dbo.swsf 
WHERE (FL = '+' and LY = '2')  
GO

--RMB 应付 箱 用于删除时
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swrmbbfx]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swrmbbfx]
GO

CREATE VIEW dbo.swrmbbfx  
AS  
select *
FROM dbo.swsf   
WHERE (FL = '-' and LY = '2')   
GO



--进口转到出口上,与zxd一对一
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[zxd_tc]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[zxd_tc]
GO

CREATE VIEW dbo.zxd_tc  
AS  
select zxd.fid,zxd.xf1,zxd.f1,zxd.y1,zxd.y2,
	case when cksyk.ywlxdm = dbo.f_kgd('ys_cd_imp') then zxd.shcddm else zxd.cddm end as cddm,	--承运方
	case when cksyk.ywlxdm = dbo.f_kgd('ys_cd_imp') then zxd.shcdmc else zxd.cdmc end as cdmc,
	case when cksyk.ywlxdm = dbo.f_kgd('ys_cd_imp') then zxd.shtcf else zxd.zhtcf end as zhtcf,	--拖车费  
	case when cksyk.ywlxdm = dbo.f_kgd('ys_cd_imp') then zxd.xshds else zxd.xzhds end as xzhds,	--代收任务 
	case when cksyk.ywlxdm = dbo.f_kgd('ys_cd_imp') then zxd.fdrq2 else zxd.fdrq1 end as fdrq1,	--放单日  
	case when cksyk.ywlxdm = dbo.f_kgd('ys_cd_imp') then zxd.fdsj2 else zxd.fdsj1 end as fdsj1,	--放单时 
	case when cksyk.ywlxdm = dbo.f_kgd('ys_cd_imp') then zxd.tzrq else zxd.zxrq end as zxrq,	 --装箱/提重日
	case when cksyk.ywlxdm = dbo.f_kgd('ys_cd_imp') then zxd.tzsj else zxd.zxsj end as zxsj,
	case when cksyk.ywlxdm = dbo.f_kgd('ys_cd_imp') then zxd.sdrq else zxd.jgrq end as jgrq,	--进港/送抵日
	case when cksyk.ywlxdm = dbo.f_kgd('ys_cd_imp') then zxd.sdsj else zxd.jgsj end as jgsj,
	case when cksyk.ywlxdm = dbo.f_kgd('ys_cd_imp') then zxd.ywc2 else zxd.ywc end as ywc,		--已完成
	case when cksyk.ywlxdm = dbo.f_kgd('YS_CD_IMP') then zxd.xshdz else zxd.xzxdz end as xzxdz,	--单箱装货地址 
	case when cksyk.ywlxdm = dbo.f_kgd('YS_CD_IMP') then zxd.xshlxr else zxd.xzxdz end as xzxlxr,
	case when cksyk.ywlxdm = dbo.f_kgd('YS_CD_IMP') then zxd.shch else zxd.zhch end as zhch,		--车号
	case when cksyk.ywlxdm = dbo.f_kgd('YS_CD_IMP') then zxd.shcjsy else zxd.zhcjsy end as zhcjsy
	from dbo.zxd  
		left join cksyk on cksyk.f1 = zxd.f1 
go

--20110803 网上订舱与网上查货合并入一个视图
--20110803 网上订舱与网上查货合并入一个视图
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[etracev]') and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view [dbo].[etracev]
GO
create view dbo.etracev
as
	select cksyk.nid,cksyk.f1,dbj2.f2 ,'' as fid,'' as ydcbh,cksyk.wtf2dm,cksyk.wtf2,
			cksyk.khbh,cksyk.cgsdm,cksyk.cgsmc,cksyk.fm1,cksyk.yw,cksyk.tdlx,cksyk.tdmc,
			dbj1.shdd,dbj2.f10dm,dbj2.f10,cksyk.f11dm,cksyk.f11,dbj2.f12dm,dbj2.f12,dbj2.zzmdm,dbj2.zzmdd,
			dbj2.ypcq,cksyk.js,cksyk.mz,cksyk.tj,cksyk.f46,cksyk.f47,cksyk.f48,
			dbj1.f4032,dbj1.f4031,cksyk.f29,cksyk.f32,zzyh,dbj2.qcyscmm,dbj2.qcyscm,dbj2.qcyshc,
			dbj1.qcys,dbj2.bcetd,dbj2.bceta,dbj2.mtdm1,dbj2.mtmc1,
			cksyk.glr,cksyk.bm,cksyk.glra,cksyk.cs,cksyk.csg,cksyk.csa,cksyk.doc,cksyk.docg,cksyk.doca,
			cksyk.op,cksyk.opg,cksyk.opa,cksyk.bk,cksyk.bkg,cksyk.bka,cksyk.f31,cksyk.tdyqr,dbj3.tdqrrq,
			case when cksyk.tdyqr = '*' then '' else dbo.f_dateadd(cksyk.f32,'DAY',-1) end as zwrq,
			dbj1.khdcyq,cksyk.tgbz
	from cksyk with(nolock) 
		left join dbj1 with(nolock) on cksyk.f1 = dbj1.f1
		left join dbj2 with(nolock) on cksyk.f1 = dbj2.f1
		left join dbj3 with(nolock) on cksyk.f1 = dbj3.f1
go

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[etrace]') and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view [dbo].[etrace]
GO

create view dbo.etrace
as
	--select cast(ROW_NUMBER() OVER(order by f1) as int) as nid, * from etracev  with(nolock)
	select * from etracev  with(nolock)
go

--多级分类汇总全部
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tidangs]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[tidangs]
GO
CREATE VIEW tidangs AS SELECT [nid], [fid], [no], [tdmc], [tdzl], [bz], [tdprg], [faxto], [faxexp], [ccto], [ccexp], [lx], [qy], 
	[jkcx],[bbbeg],[bbprg],[rptrq],[rptsj],[app_m],[dxnr],[cy],[dyl]
	from tidanb WHERE lx='GUM' and app_m='传统单证'
GO

--多级分类汇总在用、不包括停用
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tidangsu]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[tidangsu]
GO
CREATE VIEW tidangsu AS SELECT [nid], [fid], [no], [tdmc], [tdzl], [bz], [tdprg], [faxto], [faxexp], [ccto], [ccexp], [lx], [qy], 
	[jkcx],[bbbeg],[bbprg],[rptrq],[rptsj],[app_m],[dxnr],[cy],[dyl]
	from tidanb WHERE lx='GUM' and qy=1 and app_m='传统单证'
GO

--JOBNO与进仓编号唯一值View all 未指定,不返回重复行
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Vf3]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[Vf3]
GO

CREATE VIEW dbo.Vf3  
AS  
	select f1,f3 from dbo.dbj2
	union --all
	select f1,f3 from dbo.cchw
GO


---OA
--最新流程流转节点
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[a_sjn]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[a_sjn]
GO
CREATE VIEW [dbo].[a_sjn] 
AS 
	select * from a_sj where not EXISTS(select 1 from a_sj a where a_sj.mfid=a.mfid and a.fid>a_sj.fid)
GO

--流程流转控制，选择节点用视图
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[a_jds]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[a_jds]
GO
CREATE VIEW [dbo].[a_jds] 
AS 
	select nid,fid,xh,mc,mfid from a_jd
GO


--列小的视图 箱号表
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[zxd_s]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[zxd_s]
GO
CREATE VIEW [dbo].[zxd_s] 
AS 
	SELECT [nid],[fid],[fid] as zxd_fid,[y1] ,[y2],[xx],[xxmc] ,[xxdx] ,[xxdxm] ,[bz] ,[zjs] ,[ztj] ,[zmz] ,
	[zxdno] ,[bzdw] ,[zxyq] ,[wxp] ,[ldx] ,[tg] ,[cg] ,[zxrq] ,[zxsj] ,[jgrq] ,[jgsj] ,[zzyh] ,
	[hxr] ,[yxts] ,[mfts] ,[cqts] ,[cqdj] ,[cqje] ,[hxf] ,
	[wxf] ,[soc] ,[xztdm] ,[xztmc] ,[gndxh] ,[ch] ,[pzh] ,[xzdm] ,[xzmc]
	from zxd
GO

--列小的视图 箱内货物表
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[zxd0_s]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[zxd0_s]
GO
CREATE VIEW [dbo].[zxd0_s] 
AS 
	SELECT [nid] ,[fid] ,[fid] as zxd0_fid,[zxdfid] ,[f1] ,[f1] as zxd0_f1,[f31] ,[jcbh] ,[js] ,[mz] ,[tj] ,[zwhm] ,[mt] ,[zzyh] ,
	[fh] ,[tz], [cgdh0] ,[jz] ,[zyl] ,[jyl] ,[hhy]
	from zxd0
GO


--分拨桩脚卡
if exists (select * from dbo.sysobjects where id = object_id(N'dbo.fbzj') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view dbo.fbzj
GO
CREATE VIEW dbo.fbzj
AS 
	select cchw.*
	  ,zxd0.fid as z_fid
	  ,zxd0.zxdfid
      ,zxd0.f31 as z_f31
      ,zxd0.jcbh
      ,zxd0.js as z_js
      ,zxd0.mz as z_mz
      ,zxd0.tj as z_tj
      ,zxd0.zwhm as z_zwhm
      ,zxd0.mt as z_mt
      ,zxd0.zzyh as z_zzyh
      ,zxd0.fh
      ,zxd0.tz
      ,zxd0.ydh
      ,zxd0.fhdh0
      ,zxd0.xsdh0
      ,zxd0.bzdm0
      ,zxd0.bzmc0
      ,zxd0.bzjs0
      ,zxd0.cgdh0
      ,zxd0.erpno
      ,zxd0.gqddh
      ,zxd0.hwsfrq
      ,zxd0.hwqsrq
      ,zxd0.xh
      ,zxd0.ckfid
      ,zxd0.js0 as z_js0
      ,zxd0.mz0
      ,zxd0.tj0
      ,zxd0.pno
      ,zxd0.jz
      ,zxd0.xzrq as z_xzrq
      ,zxd0.qr as z_qr
      ,zxd0.xc  
      ,zxd0.xk
      ,zxd0.xg
      ,zxd0.sm
      ,zxd0.hm
      ,zxd0.qthm
      ,zxd0.bzmc2
      ,zxd0.bzmc3
      ,zxd0.zyl
      ,zxd0.jyl
      ,zxd0.hhy
      ,zxd0.xjs
      ,zxd0.wgbm
      ,zxd0.ippb
      ,zxd0.ippc
      ,zxd0.f17
      ,zxd0.fbtd
      ,zxd0.fbtm
      ,zxd0.hwcw as z_hwcw
      ,zxd0.hwps as z_hwps
      ,zxd0.jssj
      ,zxd0.bzdm
      ,zxd0.bzmc
      ,zxd0.czdm
      ,zxd0.czmc
      ,zxd0.cs
      ,zxd0.cc
      ,zxd0.cz
      ,zxd0.doc
      ,zxd0.docid
      ,zxd0.op
      ,zxd0.opid
      ,zxd0.bk
      ,zxd0.bkid
	from cchw with (nolock)
	left join zxd0 on cchw.f1 = zxd0.f1
GO


--驳船船名
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[cm_bc]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[cm_bc]
GO
CREATE VIEW [dbo].[cm_bc] 
AS 
	SELECT * from cmdmk where bc = '*'
GO


---

--查验方式视图
--select * from entcyfs
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[entcyfs]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[entcyfs]
go

create view dbo.entcyfs  
as  
select * from dbo.entrydm where lxdm = 'CHKTYPE'
go

--箱号标志
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[entxhbz]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[entxhbz]
go

create view dbo.entxhbz  
as  
select * from dbo.entrydm where lxdm = 'CTNNO_CHK'
go

--箱号标志状态
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[entxzt]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[entxzt]
go

create view dbo.entxzt  
as  
select * from dbo.entrydm where lxdm = 'CTNSTATUS'
go

--报关单类型
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[enttype]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[enttype]
go

create view dbo.enttype  
as  
select * from dbo.entrydm where lxdm = 'ENTRYTYPE'
go

--监管方式
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[entjgfs]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[entjgfs]
go

create view dbo.entjgfs 
as  
select * from dbo.entrydm where lxdm = 'SERVMODE1'
go

--监管方式2
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[entjgfs2]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[entjgfs2]
go

create view dbo.entjgfs2 
as  
select * from dbo.entrydm where lxdm = 'SERVMODE2'
go

--通道标志
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[enttdbz]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[enttdbz]
go

create view dbo.enttdbz
as  
select * from dbo.entrydm where lxdm = 'SPMARK'
go

--通关性质
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[enttgxz]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[enttgxz]
go

create view dbo.enttgxz
as  
select * from dbo.entrydm where lxdm = 'TRADETYPE'
go

--商务备注，输入协议号
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[swsf_xyh]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[swsf_xyh]
go

create view dbo.swsf_xyh
as  
	select db1svc.nid,db1svc.fid,db1svc.xyh,db1svc.xmdm,db1svc.xmmc,db1svc.dwdm,db1svc.dwmc,db1svc.dj,
		db1svc.bz,db1svc.dx,db1svc.xx,db1svc.mtqydm,db1svc.mtqymc,db1svc.gg,db1svc.ksrq,db1svc.jsrq,
		db1svc.mfid,db1.f9 as f9 from db1svc
		left join db1 on db1svc.mfid = db1.fid
		--where db1svc.mfid = '000000000045'
go


--有效进箱计划号
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[plan_in]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[plan_in]
go

create view dbo.plan_in
as  --JOBNO 堆场 箱经营人 大小 箱型  计划据数据 完成数 失效天数
	select wjmywk.nid,wjmywk.dcdm,wjmywk.f1,wjmywk.dcmc,zxd.xzdm,zxd.xzmc,zxd.xxdx,zxd.xx,
		wjmywk.jhs,wjmywk.wcs,wjmywk.sxts,
		wjmywk.lxdm,wjmywk.lxmc,wjmywk.sxrq
		from cksyk
		left join dbj3 on dbj3.f1 = cksyk.f1 
		left join wjmywk on wjmywk.f1 = cksyk.f1
		left join zxd on zxd.f1 = cksyk.f1
		where cksyk.opm = 'pln_in' and dbj3.wtzx = 1 
			and cksyk.f34 <> ''
			and wjmywk.wcs < wjmywk.jhs  
			and wjmywk.sxrq >= dbo.f_mydate1(dbo.f_getdate())
		--计划 进箱 完工 完生数 < 计划数 未失效
go

--有效出箱计划号
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[plan_out]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[plan_out]
go

create view dbo.plan_out
as  
	--JOBNO 堆场 箱经营人 大小 箱型  计划据数据 完成数 失效天数
	select wjmywk.nid,wjmywk.dcdm,wjmywk.f1,wjmywk.dcmc,zxd.xzdm,zxd.xzmc,zxd.xxdx,zxd.xx,
		wjmywk.jhs,wjmywk.wcs,wjmywk.sxts,
		wjmywk.lxdm,wjmywk.lxmc,wjmywk.sxrq
		from cksyk
		left join dbj3 on dbj3.f1 = cksyk.f1 
		left join wjmywk on wjmywk.f1 = cksyk.f1
		left join zxd on zxd.f1 = cksyk.f1
		where cksyk.opm = 'pln_in' and dbj3.wtzx = 2 
			and cksyk.f34 <> ''
			and wjmywk.wcs < wjmywk.jhs  
			and wjmywk.jhs <> wjmywk.wcs
			and wjmywk.sxrq >= dbo.f_mydate1(dbo.f_getdate())
			--计划 进箱 完工 完工 完生数 < 计划数 未失效
go

--堆场出箱 选择在场箱
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].cnt_on') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].cnt_on
GO
CREATE VIEW [dbo].cnt_on 
AS	--箱号 大小 箱型 箱经营人 堆场 流向
	select xg_xt.nid,xg_xt.fid,xg_xt.y1,xg_xt.dx,xg_xt.xx,xg_xt.xjjc,xg_gzn.dd,xg_gzn.lxmc,
		xg_gzn.ddm,xg_gzn.lxdm,xg_gzn.zc,xg_xt.xjdm 
		from xg_xt with (nolock)
		left join xg_gzn with (nolock) on xg_xt.fid = xg_gzn.mfid
		where xg_gzn.zc = '*'
		--在场
go

--堆场进箱 选择将进场箱
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].cnt_off') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].cnt_off
GO
CREATE VIEW [dbo].cnt_off 
AS	--箱号 大小 箱型 箱经营人 堆场 流向
	select xg_xt.nid,xg_xt.fid,xg_xt.y1,xg_xt.dx,xg_xt.xx,xg_xt.xjjc,xg_gzn.dd,xg_gzn.lxmc,
		xg_gzn.ddm,xg_gzn.lxdm,xg_gzn.zc,xg_xt.xjdm
		from xg_xt with (nolock)
		left join xg_gzn with (nolock) on xg_xt.fid = xg_gzn.mfid
		where xg_gzn.zc = ''
		--不在场
go

--ebooking别名
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ebooking]') and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view [dbo].[ebooking]
GO

CREATE VIEW dbo.ebooking
AS
	SELECT *
	FROM dbo.ebk where fl = '1'
GO

--etemplate别名
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[etemplate]') and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view [dbo].etemplate
GO

CREATE VIEW dbo.etemplate
AS
	SELECT *
	FROM dbo.ebk where fl = '2'
GO

--ebkg别名
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ebkg]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[ebkg]
GO

CREATE VIEW dbo.ebkg
AS
SELECT *
FROM dbo.ebk where fl = '1'
GO

--ebooking根据函数形成的View
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ebk_fun]') and OBJECTPROPERTY(id, N'IsView') = 1)
	drop view [dbo].[ebk_fun]
GO

CREATE VIEW dbo.ebk_fun
AS
SELECT ebk.nid,ebk.fid,

	isnull(DB1.GLR,'')	as DGLR,	isnull(DB1.GLRID,'')	as DGLRID,
	isnull(DB1.BM,'')	AS DBM,		isnull(DB1.BMID,'')		AS DBMID,
	isnull(DB1.GLRA,'') AS DGLRA,	isnull(DB1.GLRAID,'')	AS DGLRAID,
    isnull(DB1.CS,'')	AS DCS,		isnull(DB1.CSID,'')		AS DCSID,  --客服
	isnull(DB1.CSG,'')	AS DCSG,	isnull(DB1.CSGID,'')	AS DCSGID,
	isnull(DB1.CSA,'')	AS DCSA,	isnull(DB1.CSAID,'')	AS DCSAID,
	'_'+rtrim(isnull(DB1.GLRID,''))		+isnull(DB1.CSID,'') as xsid, --销售：客服
	'_'+rtrim(isnull(DB1.BMID,''))		+isnull(DB1.CSGID,'') as xsgid,
	'_'+rtrim(isnull(DB1.GLRAID,''))	+isnull(DB1.CSAID,'') as xsaid,
	'_'+rtrim(isnull(ebk.opid,'')) as czid, --操作
	'_'+rtrim(isnull(ebk.opgid,'')) as czgid,
	'_'+rtrim(isnull(ebk.opaid,'')) as czaid,
	'_'+rtrim(isnull(DB1.GLRID,''))		+ rtrim(isnull(DB1.CSID,'')) + isnull(ebk.opid,'') as syid, --所有
	'_'+rtrim(isnull(DB1.BMID,''))		+ rtrim(isnull(DB1.CSGID,'')) + isnull(ebk.opgid,'') as sygid,
	'_'+rtrim(isnull(DB1.GLRAID,''))	+ rtrim(isnull(DB1.CSAID,'')) + isnull(ebk.opaid,'') as syaid

FROM dbo.ebk left join db1 on ebk.wtfdm = db1.f9 and ebk.wtfdm <> '' where ebk.fl = '1'
GO


--美国反恐申报ISF信息视图
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[edi_isf]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[edi_isf]
GO
CREATE VIEW dbo.edi_isf
AS
	select * from edi where lx in('F16','F18','F20','ISF')
go


--授权方
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[edi_sqf]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[edi_sqf]
GO
CREATE VIEW dbo.edi_sqf
AS
	select * from edi where lx = 'SQF'
go

--授权方
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[edi_sqf]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[edi_sqf]
GO
CREATE VIEW dbo.edi_sqf
AS
	select * from edi where lx = 'SQF'
go

--称重方
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[edi_CZF]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [dbo].[edi_CZF]
GO
CREATE VIEW dbo.edi_CZF
AS
	select * from edi where lx = 'CZF'
go


