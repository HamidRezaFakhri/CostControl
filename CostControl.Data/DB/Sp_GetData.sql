	SET NOCOUNT ON
	SET FMTONLY OFF

	BEGIN TRY
		BEGIN TRAN

		INSERT INTO
			dbo.SalePoint
				(
					State,
					Name,
					Code,
					EnglishName,
					IsHall
				)
			SELECT
				1,
				REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(fNameFarsi AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی'),
				NULLIF(LTRIM(RTRIM(fCode)), ''),
				ISNULL(LTRIM(RTRIM(fNameEng)), ''),
				isHall
			FROM
				[FB970506].dbo.MFB_POS
			WHERE
				fDeleted = 0
				AND
				NULLIF(LTRIM(RTRIM(fCode)), '')
					NOT IN
						(
							SELECT Code
							FROM
								dbo.SalePoint
						)

			UPDATE
				dbo.SalePoint
			SET
				Name = REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(fNameFarsi AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی'),
				EnglishName = ISNULL(LTRIM(RTRIM(fNameEng)), ''),
				IsHall = POS.isHall				
			FROM
				dbo.SalePoint SP
				INNER JOIN
				[FB970506].dbo.MFB_POS POS
			ON
				POS.fCode = SP.Code

		---------------------------------------------------------------------------------------
		INSERT INTO
			dbo.CostPointGroup
				(
					State,
					Name,
					Code
				)

		SELECT
			1,
			REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی'),
			NULLIF(LTRIM(RTRIM(Code)), '')
		FROM
			PouyaD.dbo.FinDetCg
		WHERE
			Leaf = 'T'
			AND
			Parent IS NULL
			AND
			NULLIF(LTRIM(RTRIM(Code)), '')
				NOT IN
					(
						SELECT Code
						FROM
							dbo.CostPointGroup
					)

		UPDATE
			dbo.CostPointGroup
		SET
			Name = REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(CG.Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی')
		FROM
			dbo.CostPointGroup SC
			INNER JOIN
			PouyaD.dbo.FinDetCg CG
		ON
			CG.Code = SC.Code

		---------------------------------------------------------------------------------------
		INSERT INTO
			dbo.CostPoint
				(
					State,
					Name,
					Code,
					CostPointGroupId
				)

		SELECT
			1,
			REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی') + ' - ' + NULLIF(LTRIM(RTRIM(Code)), ''),
			NULLIF(LTRIM(RTRIM(Code)), ''),
			(SELECT TOP 1 Id FROM dbo.CostPointGroup WHERE Code = Parent)
		FROM
			PouyaD.dbo.FinDetCg
		WHERE
			Leaf = 'F'
			AND
			Parent IS NOT NULL
			AND
			NULLIF(LTRIM(RTRIM(Code)), '')
				NOT IN
					(
						SELECT Code
						FROM
							dbo.CostPoint
					)

		UPDATE
			dbo.CostPoint
		SET
			Name = REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(CG.Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی') + ' - ' + NULLIF(LTRIM(RTRIM(CG.Code)), '')
		FROM
			dbo.CostPoint CP
			INNER JOIN
			PouyaD.dbo.FinDetCg CG
		ON
			CG.Code = CP.Code

		---------------------------------------------------------------------------------------
		INSERT INTO
			dbo.ConsumptionUnit
				(
					State,
					Name,
					Code
				)
		SELECT
			State,
			Name,
			Code
		FROM
			(
				SELECT
					1 State,
					REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی') Name,
					CAST(ROW_NUMBER() Over(Order By Name) AS NVARCHAR(MAX)) Code
				FROM
					PouyaD.dbo.SckUntBd
				WHERE
					Leaf = 'F'
					AND
					Parent IS NULL
			) C
		WHERE
			Code
				NOT IN
					(
						SELECT Code
						FROM
							dbo.ConsumptionUnit
					)

		UPDATE
			dbo.ConsumptionUnit
		SET
			Name = U.Name
		FROM
			dbo.ConsumptionUnit CU
			INNER JOIN
			(
				SELECT
					REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی') Name,
					CAST(ROW_NUMBER() Over(Order By Name) AS NVARCHAR(MAX)) Code
				FROM
					PouyaD.dbo.SckUntBd
				WHERE
					Leaf = 'F'
					AND
					Parent IS NULL			
			) U
		ON
			U.Code = CU.Code

		---------------------------------------------------------------------------------------
		INSERT INTO
			dbo.Ingredient
				(
					State,
					Name,
					Code,
					EnglishName,
					ConsumptionUnitId,
					UsefullRatio,
					Description
				)
		SELECT
			1,
			REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی'),
			NULLIF(LTRIM(RTRIM(Code)), ''),
			LTRIM(RTRIM(LatinName)),
			(
				SELECT Id
				FROM
					dbo.ConsumptionUnit
				WHERE
					Name = REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(FirstCountUnit AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی')
			),
			70,
			NULL
		FROM
			PouyaD.dbo.SckGdsCg
		WHERE
			NULLIF(LTRIM(RTRIM(Name)), '') IS NOT NULL
			AND
			NULLIF(LTRIM(RTRIM(Code)), '') IS NOT NULL
			AND
			LTRIM(RTRIM(Code))
				NOT IN
					(
						SELECT Code
						FROM
							dbo.Ingredient
					)
			AND
			GoodsGroup = '01'
			AND
			Leaf = 'F'
			AND
			Code LIKE '1%'
			AND
			REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی')
				NOT IN
					(
						SELECT
							REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی')
						FROM
							PouyaD.dbo.SckGdsCg
						GROUP BY
							REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی'),
							Parent
						HAVING
							COUNT(*) > 1
					)

		UPDATE
			dbo.Ingredient
		SET
			Name = REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(S.Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی'),
			EnglishName = LTRIM(RTRIM(LatinName))
		FROM
			dbo.Ingredient I
			INNER JOIN
			PouyaD.dbo.SckGdsCg S
		ON
			LTRIM(RTRIM(S.Code)) = I.Code
		WHERE
			NULLIF(LTRIM(RTRIM(S.Code)), '') IS NOT NULL

		---------------------------------------------------------------------------------------
		INSERT INTO
			dbo.Food
				(
					State,
					Name,
					SaleCostPointId,
					Code,
					EnglishName,
					FoodType
				)
		SELECT
			1,
			REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی'),
			NULL,
			NULLIF(LTRIM(RTRIM(Code)), ''),
			LTRIM(RTRIM(LatinName)),
			1
		FROM
			PouyaD.dbo.SckGdsCg
		WHERE
			NULLIF(LTRIM(RTRIM(Name)), '') IS NOT NULL
			AND
			NULLIF(LTRIM(RTRIM(Code)), '') IS NOT NULL
			AND
			LTRIM(RTRIM(Code))
				NOT IN
					(
						SELECT Code
						FROM
							dbo.Food
					)
			AND
			GoodsGroup = '01'
			AND
			Leaf = 'F'
			AND
			Code LIKE '9%'
			AND
			REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی')
				NOT IN
					(
						SELECT
							REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی')
						FROM
							PouyaD.dbo.SckGdsCg
						GROUP BY
							REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی'),
							Parent
						HAVING
							COUNT(*) > 1
					)

		UPDATE
			dbo.Food
		SET
			Name = REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(S.Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی'),
			EnglishName = LTRIM(RTRIM(LatinName))
		FROM
			dbo.Food F
			INNER JOIN
			PouyaD.dbo.SckGdsCg S
		ON
			LTRIM(RTRIM(S.Code)) = F.Code
		WHERE
			NULLIF(LTRIM(RTRIM(S.Code)), '') IS NOT NULL

		---------------------------------------------------------------------------------------
		INSERT INTO
			dbo.Inventory
				(
					State,
					Name,
					Code,
					IsWasted
				)
		SELECT
			1,
			REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی'),
			CAST(ROW_NUMBER() OVER(ORDER BY Name) AS NVARCHAR(MAX)),
			0
		FROM
			PouyaD.dbo.SckSecCg
		WHERE
			Leaf = 'F'
			AND
			[Index] = 1
			AND
			REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی')
				NOT IN
					(
						SELECT Name
						FROM
							dbo.Inventory
					)

		---------------------------------------------------------------------------------------
		--INSERT INTO
		--	dbo.Draft
		--		(
				
		--		)
		--SELECT
		--	Goods
		--FROM
		--	PouyaD.dbo.SckGd_st
		--WHERE
		--	Stock = 1

		--dbo.DraftItem
		--	(
			
		--	)
		
		COMMIT
	END TRY
	BEGIN CATCH
		ROLLBACK
	END CATCH



--EXEC [FB970506].dbo.[STP_MFB_RestFroosh]
--	@opCode = -1,
--	@Dfrom = 13970102,
--	@Dto = 13970201,
--	@PCode = 555,
--	@PType = -1,
--	@fCodKolB = -1,
--	@fCodSarB = -1,
--	@jariorbaygani = 0

--@opCode smallint                      -1 ارسال شود,    
--@Dfrom int,                                  از تاریخ ، مثلا 13970102
--@Dto int,                                       تا تاریخ ، مثلا 13970201
--@PCode INT,                                  کد مرکز فروش ، مثلا 555 ، اگر -1 باشد یعنی همه مراکز فروش
--@PType int  ,                                -1 ارسال شود
--@fCodKolB int,                             -1 ارسال شود
--@fCodSarB int  ,                           -1 ارسال شود
--@jariorbaygani smallint               0 = جاری   ///
--1 = بایگانی

/*
-- مرکز فروش
SELECT fNameFarsi, fCode, fNameEng, isHall FROM [FB970506].dbo.MFB_POS

-- گروه مرکز هزینه
SELECT Name, Code FROM PouyaD.dbo.FinDetCg WHERE Leaf = 'T' AND Parent IS NULL

-- مرکز هزینه
SELECT Name, Code FROM PouyaD.dbo.FinDetCg WHERE Leaf = 'F' AND Parent IS NOT NULL

-- ارتباط بین مرکز فروش و مرکز هزینه
/*
در برنامه
Cost Control
انجام می شود
*/

-- واحدهای مصرفی
SELECT Name, '' Code FROM PouyaD.dbo.SckUntBd WHERE Leaf = 'F' AND Parent IS NULL

-- انبارها
SELECT Name FROM PouyaD.dbo.SckSecCg WHERE Leaf = 'F' AND [Index] = 1

-- سرفصل هزینه های سربار
/*
در برنامه
Cost Control
انجام می شود
*/

-- هزینه های سربار دوره
/*
در برنامه
Cost Control
انجام می شود
*/

-- مواد خام
SELECT Name, Code, LatinName FROM PouyaD.dbo.SckGdsCg WHERE GoodsGroup = '01' AND Leaf = 'F' AND Code LIKE '1%' 

-- غذا
SELECT Name, Code, LatinName FROM PouyaD.dbo.SckGdsCg WHERE GoodsGroup = '01' AND Leaf = 'F' AND Code LIKE '9%'
/*
قیمت و مرکز هزینه و فروش آن در برنامه
Cost Control
انجام می شود
*/

-- مواد تشکیل دهنده غذا
/*
در برنامه
Cost Control
انجام می شود
*/

-- فروش
EXEC [FB970506].dbo.[STP_MFB_RestFroosh]
	@opCode = -1,
	@Dfrom = 13970102,
	@Dto = 13970201,
	@PCode = 555,
	@PType = -1,
	@fCodKolB = -1,
	@fCodSarB = -1,
	@jariorbaygani = 0

-- حواله انبار
--SELECT * FROM PouyaD.dbo.SckGd_St WHERE Stock = 1

-- حواله مصرفی
--SELECT * FROM PouyaD.dbo.SckVchPr
--SELECT * FROM PouyaD.dbo.SckVchBk

--EXEC dbo.USP_SckVchLs_Insert

DECLARE
	@FromDate DATE = '2017-08-15 00:00:00.000',
	@ToDate DATE = '2017-08-15 00:00:00.000',
	@Expence INT = 140013

SELECT
	L.[Date],
	B.Goods,
	B.FirstNumber,
	B.Expence,
	P.FirstCost,
	P.FirstCost / B.FirstNumber Fee
FROM
	PouyaD.dbo.SckVchLs L
	INNER JOIN
	PouyaD.dbo.SckVchBk B
ON
	B.Parent = L.Serial
	INNER JOIN
	PouyaD.dbo.SckVchPr P
ON
	P.Parent = B.Serial
WHERE
	--Goods = '101001008'
	Goods = '111003003'
	AND
	B.Expence = @Expence
	AND
	Date BETWEEN @FromDate AND @ToDate

SELECT * FROM PouyaD.dbo.SckGdsCg WHERE Code = '111003003'

*/