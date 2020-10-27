/*********************************************************************************/
/*   Project Name:  Foton LES System											 */
/*   Program Name:  [LES].[PROC_TWD_CALCULATE_SENDTIME]							 */
/*   Called By:     by the Page													 */
/*   Purpose:       计算供应商发单时间											 */
/*   author:        孙述霄	2017-05-22   										 */
/*********************************************************************************/
CREATE PROCEDURE [LES].[PROC_TWD_CALCULATE_SENDTIME]
(
	@PLANT VARCHAR(10),						--工厂
	@ASSEMBLYLINE VARCHAR(10),				--流水线
	@WINDOWDATE DATETIME,					--窗口时间
	@ONLINETIME INT,						--上线时间
	@SENDTIME DATETIME OUTPUT				--发单时间
)
AS
BEGIN
	SET @SENDTIME = [LES].[FN_GET_TWD_SENDTIME](@PLANT, @ASSEMBLYLINE, @WINDOWDATE, @ONLINETIME)
END