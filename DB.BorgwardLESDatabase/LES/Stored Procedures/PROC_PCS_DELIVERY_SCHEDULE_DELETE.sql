
/********************************************************************/
/*                                                                  */
/*   Project Name:  LES                                            */
/*                                                                  */
/*   Program Name:  [PROC_PCS_DELIVERY_SCHEDULE_DELETE]				 */
/*                                                                  */
/*   Called By:     web page .delete the new timewindow                  */
/*                                                                  */
/*   Purpose:       Checks to make sure the  delivery time is right  */
/*											                          */
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_PCS_DELIVERY_SCHEDULE_DELETE]
@schedule_identity int

AS

DECLARE @CurrentDate DATETIME
SET @CurrentDate=DATEADD(dd,DATEDIFF(dd,0,GETDATE()),0)

BEGIN TRAN

DECLARE @plant nvarchar(10)
DECLARE @assemblyline nvarchar(10)
DECLARE @windowsTime nvarchar(5)

SELECT @plant=plant,@assemblyline=assembly_line,@windowsTime=WINDOWS_TIME
FROM TT_PCS_DELIVERY_SCHEDULE
WHERE schedule_identity=@schedule_identity

DELETE FROM  TT_PCS_DELIVERY_SCHEDULE
WHERE PLANT=@plant and ASSEMBLY_LINE=@assemblyline and @windowsTime=WINDOWS_TIME

DELETE FROM LES.TR_PCS_SCHEDULE_BOX_PART
WHERE SCHEDULEID=@schedule_identity

--模板变更,则删除按老模板生成的第二天的投递时间,对当天已生成的投递时间无影响
--即模板改变,第二天才会生效
IF(EXISTS(SELECT * FROM TT_PCS_DELIVERY_SCHEDULE WHERE plant=@plant and assembly_line=@assemblyline and delivery_date=DATEADD(dd,1,@CurrentDate)))
BEGIN
	DELETE FROM TT_PCS_DELIVERY_SCHEDULE WHERE plant=@plant and assembly_line=@assemblyline and delivery_date=DATEADD(dd,1,@CurrentDate)
END

COMMIT TRAN