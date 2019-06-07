IF (OBJECT_ID('dbo.SP_AddExternallCostPointGroup') IS NOT NULL)
	DROP PROCEDURE dbo.SP_AddExternallCostPointGroup;
GO

CREATE PROCEDURE dbo.SP_AddExternallCostPointGroup
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
					dbo.CostPointGroup
				WHERE
					Code = @Key)
		)
	BEGIN
		INSERT INTO
			dbo.CostPointGroup
				(
					Name,
					Code
				)
		SELECT
			REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی'),
			NULLIF(LTRIM(RTRIM(Code)), '')
		FROM
			PouyaD.dbo.FinDetCg
		WHERE
			Leaf = 'T'
			AND
			Parent IS NULL
			AND
			NULLIF(LTRIM(RTRIM(Code)), '') = @Key
	END