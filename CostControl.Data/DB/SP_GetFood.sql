IF (OBJECT_ID('dbo.SP_GetFood') IS NOT NULL)
	DROP PROCEDURE dbo.SP_GetFood;
GO

CREATE PROCEDURE dbo.SP_GetFood
AS
	SET NOCOUNT ON
	SET FMTONLY OFF

	SELECT
		NULLIF(LTRIM(RTRIM(I.fcode)), '') Code,
		REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(I.fName AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی') Name,
		NULLIF(LTRIM(RTRIM(I.fNameEng)), '') EnglishName
		--,
		--P.fPrice,
		--I.fUnit,
		--I.fType
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
		NULLIF(LTRIM(RTRIM(I.fcode)), '')
			NOT IN
				(
					SELECT Code
					FROM
						dbo.Food
				)
		--ORDER BY
		--	fType,
		--	REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(I.fName AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی')