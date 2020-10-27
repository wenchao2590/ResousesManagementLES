CREATE FUNCTION [dbo].[ToLower]
(@InString NVARCHAR (512))
RETURNS NVARCHAR (512)
AS
 EXTERNAL NAME [StringHelper].[StringHelper.Convert].[ToLower]

