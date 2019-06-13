IF (OBJECT_ID('dbo.SP_AddExternallFood') IS NOT NULL)
	DROP PROCEDURE dbo.SP_AddExternallFood;
GO

CREATE PROCEDURE dbo.SP_AddExternallFood
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
					dbo.Food
				WHERE
					Code = @Key)
		)
	BEGIN
		INSERT INTO
			dbo.Food
				(
					Name,
					Code,
					EnglishName,
					FoodType
				)
		SELECT
			REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(I.fName AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی') Name,
			NULLIF(LTRIM(RTRIM(I.fcode)), '') Code,
			NULLIF(LTRIM(RTRIM(I.fNameEng)), '') EnglishName,
			1
		FROM
			[FB970506].dbo.MFB_ITEMS I
			INNER JOIN
			[FB970506].dbo.MFB_POSITEMS P
		ON
			I.fcode = P.fItemCode
		WHERE
			I.fDeleted = 0
			AND
			P.fPosCode = 555
			AND
			NULLIF(LTRIM(RTRIM(I.fcode)), '') = @Key
	END