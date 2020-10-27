
--建立人：  高升
--建立日期：2007/08/12
--修改日期：
--功能目的：返回指定日期的跟踪记录
--参数：    日期格式yyyyMMdd
CREATE FUNCTION [dbo].[ReadTrackFile] (@readDate char(8))
RETURNS @traceLog table(
[DBName]    nvarchar(50),
[TextData]  nvarchar(max),
[Duration]  DECIMAL(18,2),
[StartTime] datetime,
[EndTime] DATETIME,
[Reads] BIGINT,
[Writes] BIGINT,
[CPU] DECIMAL(18,2),
[LoginName] nvarchar(50),
[HostName]  nvarchar(256),
[ApplicationName]NVARCHAR(200))
  AS
BEGIN
  DECLARE @fileAddress nvarchar(245)  --文件地址

  SET @fileAddress = N'D:\TraceSQL\SQL' + @readDate + '.trc'

  INSERT INTO @traceLog SELECT DataBaseName,TextData,ROUND(Duration/1000000.00,3),StartTime,EndTime,Reads,Writes,ROUND(CPU/1000.00,3),LoginName,HostName,ApplicationName
    FROM fn_trace_gettable(@fileAddress, default)   --SPID,ApplicationName,EventClassselect

  RETURN
END