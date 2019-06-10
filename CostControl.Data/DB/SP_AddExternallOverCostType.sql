IF (OBJECT_ID('dbo.SP_AddExternallOverCostType') IS NOT NULL)
	DROP PROCEDURE dbo.SP_AddExternallOverCostType;
GO

CREATE PROCEDURE dbo.SP_AddExternallOverCostType
	@Key NVARCHAR(50)
AS
	SET NOCOUNT ON
	SET FMTONLY OFF

	IF (
			NULLIF(LTRIM(RTRIM(@Key)), '') IS NOT NULL
			AND
			NOT EXISTS(
				SELECT FinancialCode
				FROM
					dbo.OverCostType
				WHERE
					FinancialCode = @Key)
		)
	BEGIN
		INSERT INTO
			dbo.OverCostType
				(
					Name,
					FinancialCode
				)
		SELECT
			REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی') Name,
			NULLIF(LTRIM(RTRIM(Code)), 'Code For ' + Name) Code
		FROM
			PouyaD.dbo.FinAccCg
		WHERE
			Leaf = 'F'
			AND
			NULLIF(LTRIM(RTRIM(Code)), '') collate Arabic_CI_AS = @Key collate Arabic_CI_AS
	END