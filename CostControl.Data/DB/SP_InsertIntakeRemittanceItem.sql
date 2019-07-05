IF (OBJECT_ID('dbo.SP_InsertIntakeRemittanceItem') IS NOT NULL)
	DROP PROCEDURE dbo.SP_InsertIntakeRemittanceItem;
GO

CREATE PROCEDURE dbo.SP_InsertIntakeRemittanceItem
	@IntakeRemittanceId BIGINT
AS
	SET NOCOUNT ON
	SET FMTONLY OFF

	DECLARE
		@FromDate CHAR(8) = '13970102',
		@ToDate CHAR(8) = '13970203',
		@SaleCostPointId INT = 1

	SELECT
		@SaleCostPointId = SaleCostPointId,
		@FromDate =	FORMAT(IntakeFromDate, 'yyyy', 'fa-IR') +
								FORMAT(IntakeFromDate, 'MM', 'fa-IR') +
								FORMAT(IntakeFromDate, 'dd', 'fa-IR'),
		@ToDate = FORMAT(IntakeToDate, 'yyyy', 'fa-IR') +
							FORMAT(IntakeToDate, 'MM', 'fa-IR') +
							FORMAT(IntakeToDate, 'dd', 'fa-IR')
	FROM
		dbo.IntakeRemittance
	WHERE
		Id = @IntakeRemittanceId

	DELETE
		dbo.IntakeRemittanceItem
	WHERE
		IntakeRemittanceId = @IntakeRemittanceId

	DECLARE
		@Sales Table
			(
				Price NUMERIC(28, 2),
				ServicePrice NUMERIC(28, 2),
				Quantity NUMERIC(28, 2),
				Name NVARCHAR(50),
				Code INT
			)

	INSERT INTO
		@Sales
	EXEC
		[FB970506].dbo.[STP_MFB_RestFroosh]
			@opCode = -1,
			@Dfrom = @FromDate,
			@Dto = @ToDate,
			@PCode = 555,
			@PType = -1,
			@fCodKolB = -1,
			@fCodSarB = -1,
			@jariorbaygani = 0

	--SELECT
	--	Code,
	--	Name,
	--	Quantity,
	--	Price,
	--	ServicePrice
	--FROM
	--	@Sales

	INSERT INTO
		dbo.IntakeRemittanceItem
			(
				IntakeRemittanceID,
				IngredientId,
				Amount,
				ConsumptionUnitId
			)
	SELECT
		@IntakeRemittanceId,
		--F.Code,
		R.IngredientId,
		--I.Name + '(' + CU.Name + ')' IngredientName,
		CAST(SUM(R.Amount * S.Quantity) AS NUMERIC(28, 2)) Amount,
		I.ConsumptionUnitId
		--,
		--I.UsefullRatio
	FROM
		dbo.Food F
		INNER JOIN
		dbo.Recipe R
	ON
		R.FoodId = F.Id
		INNER JOIN
		dbo.Ingredient I
	ON
		I.Id = R.IngredientId
		INNER JOIN
		dbo.ConsumptionUnit CU
	ON
		CU.Id = I.ConsumptionUnitId
		INNER JOIN
		@Sales S
	ON
		S.Code = F.Code
	GROUP BY
		R.IngredientId,
		I.ConsumptionUnitId
