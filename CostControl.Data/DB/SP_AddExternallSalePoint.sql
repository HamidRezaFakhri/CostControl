IF (OBJECT_ID('dbo.SP_AddExternallSalePoint') IS NOT NULL)
	DROP PROCEDURE dbo.SP_AddExternallSalePoint;
GO

CREATE PROCEDURE dbo.SP_AddExternallSalePoint
	@Key NVARCHAR(50)
AS
	SET NOCOUNT ON
	SET FMTONLY OFF

	IF (
			NULLIF(LTRIM(RTRIM(@Key)), '') IS NOT NULL
			AND
			NOT EXISTS(
				SELECT Code
				FROM
					dbo.SalePoint
				WHERE
					Code = @Key)
		)
	BEGIN
		INSERT INTO
			dbo.SalePoint
				(
					Name,
					Code,
					EnglishName,
					IsHall
				)
		SELECT
			REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(fNameFarsi AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی') Name,
			NULLIF(LTRIM(RTRIM(fCode)), 'Code For ' + fNameFarsi) Code,
			ISNULL(LTRIM(RTRIM(fNameEng)), 'EnglishName For ' + fNameFarsi) EnglishName,
			isHall IsHall
		FROM
			[FB970506].dbo.MFB_POS
		WHERE
			fDeleted = 0
			AND
			NULLIF(LTRIM(RTRIM(fCode)), '') = @Key
	END