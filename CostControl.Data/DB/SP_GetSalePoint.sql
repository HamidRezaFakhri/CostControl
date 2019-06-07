IF (OBJECT_ID('dbo.SP_GetCostPoint') IS NOT NULL)
	DROP PROCEDURE dbo.SP_GetCostPoint;
GO

CREATE PROCEDURE dbo.SP_GetCostPoint
AS
	SET NOCOUNT ON
	SET FMTONLY OFF

	SELECT
		REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(fNameFarsi AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی') Name,
		NULLIF(LTRIM(RTRIM(fCode)), '') Code,
		ISNULL(LTRIM(RTRIM(fNameEng)), '') EnglishName,
		isHall IsHall
	FROM
		[FB970506].dbo.MFB_POS
	WHERE
		fDeleted = 0
		AND
		NULLIF(LTRIM(RTRIM(fCode)), '')
			NOT IN
				(
					SELECT Code
					FROM
						dbo.SalePoint
				)