IF (OBJECT_ID('dbo.SP_GetCostPoint') IS NOT NULL)
	DROP PROCEDURE dbo.SP_GetCostPoint;
GO

CREATE PROCEDURE dbo.SP_GetCostPoint
AS
	SET NOCOUNT ON
	SET FMTONLY OFF

	SELECT
		REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی') Name,
		NULLIF(LTRIM(RTRIM(Code)), '') Code
	FROM
		PouyaD.dbo.FinDetCg
	WHERE
		Leaf = 'F'
		AND
		Parent IS NOT NULL
		AND
		NULLIF(LTRIM(RTRIM(Code)), '') collate Arabic_CI_AS
			NOT IN
				(
					SELECT Code collate Arabic_CI_AS
					FROM
						CostControl.dbo.CostPoint
				)

