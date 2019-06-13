IF (OBJECT_ID('dbo.SP_AddExternallIngredient') IS NOT NULL)
	DROP PROCEDURE dbo.SP_AddExternallIngredient;
GO

CREATE PROCEDURE dbo.SP_AddExternallIngredient
	@Key NVARCHAR(50)
AS
	SET NOCOUNT ON
	SET FMTONLY OFF

	INSERT INTO
		dbo.ConsumptionUnit
			(
				Name
			)
	SELECT
		DISTINCT
			REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(FirstCountUnit AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی')
	FROM
		PouyaD.dbo.SckGdsCg
	WHERE
		REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(FirstCountUnit AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی') collate Arabic_CI_AS
			NOT IN
				(
					SELECT Name collate Arabic_CI_AS
					FROM
						dbo.ConsumptionUnit
				)

	IF (
			NULLIF(LTRIM(RTRIM(@Key)), '') IS NOT NULL
			AND
			NOT EXISTS(
				SELECT Code
				FROM
					dbo.Ingredient
				WHERE
					Code = @Key)
		)
	BEGIN
		INSERT INTO
			dbo.Ingredient
				(
					Name,
					Code,
					EnglishName,
					UsefullRatio,
					ConsumptionUnitId
				)
			SELECT
				DISTINCT
					REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(A.Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی') Name,
					NULLIF(LTRIM(RTRIM(B.Goods)), '') Code,
					NULLIF(LTRIM(RTRIM(A.LatinName)), '') EnglishName,
					70,
					(
						SELECT Id
						FROM
							dbo.ConsumptionUnit
						WHERE
							Name collate Arabic_CI_AS =
								REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(A.FirstCountUnit AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی') collate Arabic_CI_AS) Unit
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
				Leaf = 'F'
				AND
				NULLIF(LTRIM(RTRIM(Code)), '') collate Arabic_CI_AS = @Key collate Arabic_CI_AS
	END