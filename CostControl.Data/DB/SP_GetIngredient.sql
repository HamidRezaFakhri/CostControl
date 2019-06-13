IF (OBJECT_ID('dbo.SP_GetIngredient') IS NOT NULL)
	DROP PROCEDURE dbo.SP_GetIngredient;
GO

CREATE PROCEDURE dbo.SP_GetIngredient
AS
	SET NOCOUNT ON
	SET FMTONLY OFF

	SELECT
		DISTINCT
			NULLIF(LTRIM(RTRIM(B.Goods)), '') Code,
			REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(A.Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی') Name,
			A.LatinName EnglishName,
			REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(A.FirstCountUnit AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی') Unit
	FROM
		PouyaD.dbo.SckGdsCg A
		INNER JOIN
		PouyaD.dbo.SckVchBk B
	ON
		B.Goods = A.Code
		INNER JOIN
		PouyaD.dbo.SckVchLs C
	ON
		C.Serial = B.Parent
	WHERE
		A.Leaf = 'F'
		AND
		C.Class = '2'
		AND
		C.Section = '1'
		AND
		NULLIF(LTRIM(RTRIM(B.Goods)), '') collate Arabic_CI_AS
			NOT IN
				(
					SELECT Code collate Arabic_CI_AS
					FROM
						dbo.Ingredient
				)
	ORDER BY
		2
