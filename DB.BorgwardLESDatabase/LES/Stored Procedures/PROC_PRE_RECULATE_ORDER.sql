
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [LES].[PROC_PRE_RECULATE_ORDER]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	TRUNCATE TABLE LES.TM_ODS_ORDER_PART_RESULTS
	UPDATE [LES].[TT_BAS_PULL_ORDERS] SET [RECALCULATE_FLAG]=1
END