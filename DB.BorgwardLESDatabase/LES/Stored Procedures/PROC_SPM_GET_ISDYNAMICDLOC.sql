/*********************************************************************************/
/*   Project Name:  Foton LES System											 */
/*   Program Name:  [LES].[PROC_SPM_GET_ISDYNAMICDLOC]							 */
/*   Called By:     by the PDA													 */
/*   Purpose:       获取是否动态库位											 */
/*   author:        孙述霄	2017-08-09   										 */
/*********************************************************************************/
CREATE PROCEDURE [LES].[PROC_SPM_GET_ISDYNAMICDLOC]
(
    @BillNo NVARCHAR(50),					--单据号
	@IsDynamicDloc BIT OUTPUT,				--是否动态库位，0-否，1-是
	@IsOutputSole BIT OUTPUT,				--是否上架，0-否，1-是
	@OverflowDloc NVARCHAR(32) OUTPUT		--溢库库位
)
AS
BEGIN
	SELECT
		@IsDynamicDloc = ISNULL(B.[IS_DYNAMIC_DLOC], 0),
		@IsOutputSole = ISNULL(B.[IS_OUTPUT_SOLE], 0),
		@OverflowDloc = ISNULL(B.[OVERFLOW_DLOC], '')
	FROM [LES].[TT_WMM_RECEIVE] A WITH (NOLOCK)
	INNER JOIN [LES].[TM_WMM_ZONES] B WITH (NOLOCK)
	ON A.[PLANT] = B.[PLANT] AND A.[WM_NO] = B.[WM_NO] AND A.[ZONE_NO] = B.[ZONE_NO]
	WHERE A.[RECEIVE_NO] = @BillNo

	SET @IsDynamicDloc = ISNULL(@IsDynamicDloc, 0)
	SET @IsOutputSole = ISNULL(@IsOutputSole, 0)
	SET @OverflowDloc = ISNULL(@OverflowDloc, '')
END