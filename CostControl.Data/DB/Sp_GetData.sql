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
			IF OBJECT_ID('tempdb..#Tmp') IS NOT NULL
				DROP TABLE #Tmp

			SELECT
				REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی') Name,
				MIN(Code) Code
			INTO
				#Tmp
			FROM
				PouyaD.dbo.SckGdsCg
			WHERE
				REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی')
					IN
						(
							SELECT
								REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی')
							FROM
								PouyaD.dbo.SckGdsCg
							GROUP BY
								REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی')
							HAVING
								COUNT(*) > 1
						)
			GROUP BY
				REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی')

			IF OBJECT_ID('tempdb..#Tmp1') IS NOT NULL
				DROP TABLE #Tmp1

			SELECT
				REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی') Name,
				Code
			INTO
				#Tmp1
			FROM
				PouyaD.dbo.SckGdsCg
			WHERE
				REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی')
					IN
						(
							SELECT
								REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی') Name
							FROM
								PouyaD.dbo.SckGdsCg
							GROUP BY
								REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی')
							HAVING
								COUNT(*) > 1
						)

		INSERT INTO
			dbo.Ingredient
				(
					State,
					Name,
					Code,
					EnglishName,
					Type,
					Price,
					UsefullRatio
				)
		SELECT
			1,
			REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی'),
			NULLIF(LTRIM(RTRIM(Code)), ''),
			ISNULL(LTRIM(RTRIM(LatinName)), ' '),
			1,
			0,
			70
		FROM
			PouyaD.dbo.SckGdsCg
		WHERE
			LTRIM(RTRIM(Name)) <> ''
			AND
			Leaf = 'F'
			AND
			NULLIF(LTRIM(RTRIM(Code)), '')
				NOT IN
					(
						SELECT Code
						FROM
							dbo.Ingredient
					)
			AND
			Code
				NOT IN
					(
						SELECT Code FROM #Tmp1
						EXCEPT	
						SELECT Code FROM #Tmp
					)

		UPDATE
			dbo.Ingredient
		SET
			Name = REPLACE(REPLACE(REPLACE(REPLACE(LTRIM(RTRIM(CAST(S.Name AS NVARCHAR(MAX)))), '  ', ' '), N'ك', N'ک'), N'ي', N'ی'), N'ئ', N'ی'),
			EnglishName = ISNULL(LTRIM(RTRIM(LatinName)), ' ')
		FROM
			dbo.Ingredient I
			INNER JOIN
			PouyaD.dbo.SckGdsCg S
		ON
			S.Code = I.Code

		---------------------------------------------------------------------------------------

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

