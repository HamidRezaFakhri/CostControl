IF (OBJECT_ID('dbo.SP_GetOverCostType') IS NOT NULL)
	DROP PROCEDURE dbo.SP_GetOverCostType;
GO

CREATE PROCEDURE dbo.SP_GetOverCostType
AS
	SET NOCOUNT ON
	SET FMTONLY OFF

	SELECT
		REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی') Name,
		NULLIF(LTRIM(RTRIM(Code)), 'Code For ' + Name) Code,
		(
			SELECT
				REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی')
			FROM
				PouyaD.dbo.FinAccCg
			WHERE
				Code = FACg.Parent
		) OverCostTypeGroupName
	FROM
		PouyaD.dbo.FinAccCg FACg
	WHERE
		Leaf = 'F'
		AND
		NULLIF(LTRIM(RTRIM(Code)), '') collate Arabic_CI_AS
			NOT IN
				(
					SELECT FinancialCode collate Arabic_CI_AS
					FROM
						dbo.OverCostType
				)
	ORDER BY
		3, 1