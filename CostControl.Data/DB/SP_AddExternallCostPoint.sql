IF (OBJECT_ID('dbo.SP_AddExternallCostPoint') IS NOT NULL)
	DROP PROCEDURE dbo.SP_AddExternallCostPoint;
GO

CREATE PROCEDURE dbo.SP_AddExternallCostPoint
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
					dbo.CostPoint
				WHERE
					Code = @Key)
		)
	BEGIN
		DECLARE
			@CostPointGroup NVARCHAR(50)

		SELECT
			@CostPointGroup =  Parent collate Arabic_CI_AS
		FROM
			PouyaD.dbo.FinDetCg
		WHERE
			Leaf = 'F'
			AND
			Parent IS NOT NULL
			AND
			NULLIF(LTRIM(RTRIM(Code)), '') collate Arabic_CI_AS = @Key collate Arabic_CI_AS

		EXEC dbo.SP_AddExternallCostPointGroup @CostPointGroup

		INSERT INTO
			dbo.CostPoint
				(
					Name,
					Code,
					CostPointGroupId
				)
		SELECT
			REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی'),
			NULLIF(LTRIM(RTRIM(Code)), ''),
			(SELECT TOP 1 Id FROM dbo.CostPointGroup WHERE Code collate Arabic_CI_AS = Parent collate Arabic_CI_AS)
		FROM
			PouyaD.dbo.FinDetCg
		WHERE
			Leaf = 'F'
			AND
			Parent IS NOT NULL
			AND
			NULLIF(LTRIM(RTRIM(Code)), '') collate Arabic_CI_AS = @Key collate Arabic_CI_AS
	END