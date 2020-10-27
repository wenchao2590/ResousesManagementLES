CREATE PROCEDURE [LES].[PROC_TM_RPM_DIFFANALYSE_REPORT_INSERT]
(
	@NOTIFICATIONID bigint 
)
AS
BEGIN

	DECLARE @maxReportNo NVARCHAR(30)--差异报表编号
	DECLARE @ReportId BIGINT

	DECLARE @strDate VARCHAR(8)--获取当天日期：20150609
	SELECT @strDate=CONVERT(varchar(12) , getdate(), 112 )
	SELECT @maxReportNo=MAX(REPORT_NO) FROM Les.TM_RPM_DIFFANALYSE_REPORT WHERE REPORT_NO LIKE 'CY'+@strDate+'%'
	IF @maxReportNo IS NULL
	BEGIN
		SET @maxReportNo='CY' + @strDate + '001'
	END
	ELSE
    BEGIN
		SET @maxReportNo='CY' + CAST(CAST(REPLACE(@maxReportNo,'CY','') AS BIGINT)+1 AS NVARCHAR)
	END

	--插入差异主表;--t_head.PUBLISH_TIME
    INSERT INTO Les.TM_RPM_DIFFANALYSE_REPORT
            ( REPORT_NO ,
              NOTIFICATION_NO ,
              PLANT ,
              ASSEMBLY_LINE ,
              PLANT_ZONE ,
              WORKSHOP ,
              WM_NO ,
              ZONE_NO ,
              COUNT_TYPE ,
              PUBLISH_KEEPER ,
              PUBLISH_TIME ,
              CREATE_USER ,
              CREATE_DATE 
            )
	SELECT @maxReportNo, 
		t_head.NOTIFICATION_NO,
		t_head.PLANT,
		t_head.ASSEMBLY_LINE,
		t_head.PLANT_ZONE,
		t_head.WORKSHOP,
		t_head.WM_NO,
		t_head.ZONE_NO,
		t_head.COUNT_TYPE,
		t_head.PUBLISH_KEEPER,
		GETDATE(),
		t_head.CREATE_USER,
		t_head.CREATE_DATE
	FROM les.TM_RPM_PACKAGE_NOTIFICATION_HEAD t_head WHERE t_head.NOTIFICATION_ID=@NOTIFICATIONID;
	SELECT @ReportId=@@IDENTITY;
    
	--添加差异明细表
	INSERT INTO LES.TM_RPM_DIFFANALYSE_REPORT_DETAIL
	        ( REPORT_ID ,
	          WM_NO ,
	          PACKAGE_NO ,
	          PACKAGE_CNAME ,
	          PACKAGE_ENAME ,
	          ZONE_NO ,
	          DLOC ,
	          LOCATION ,
	          PACK_TYPE ,
	          NUM ,
	          REAL_NUM ,
	          DIFF_NUM ,
	          CREATE_USER ,
	          CREATE_DATE 
	        )
	SELECT @ReportId,
		  t_Import.WM_NO,
		  t_Import.PACKAGE_NO,
		  t_Import.PACKAGE_CNAME,
		  t_Import.PACKAGE_ENAME,
		  t_Import.ZONE_NO,
		  t_Import.DLOC,
		  t_Import.LOCATION,
		  t_Import.PACK_TYPE,
		  t_Import.NUM,
		  t_Import.REAL_NUM,
		  t_Import.NUM-t_Import.REAL_NUM,
		  t_Import.CREATE_USER,
		  t_Import.CREATE_DATE
	 FROM les.TM_RPM_PACKAGE_NOTIFICATION_DETAIL_IMPORT t_Import WHERE t_Import.NOTIFICATIONID=@NOTIFICATIONID	

	 --更新通知主表状态
	 UPDATE les.TM_RPM_PACKAGE_NOTIFICATION_HEAD SET COUNT_STATUS=5 WHERE NOTIFICATION_ID=@NOTIFICATIONID;
END