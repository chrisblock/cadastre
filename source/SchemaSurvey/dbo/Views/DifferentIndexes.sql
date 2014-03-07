CREATE VIEW [dbo].[DifferentIndexes]
(
	  [survey]
	, [database_survey]
	, [table]
	, [index]
)
AS
	WITH [IndexColumnChecksums] ([database_survey], [object_id], [index_id], [checksum])
	AS (
		SELECT
			  [Indexes].[database_survey]
			, [Indexes].[object_id]
			, [Indexes].[index_id]
			, CHECKSUM_AGG(BINARY_CHECKSUM([Columns].[name]))
		FROM [dbo].[Indexes]
		INNER JOIN [dbo].[IndexColumns]
				ON [IndexColumns].[database_survey] = [Indexes].[database_survey]
				AND [IndexColumns].[object_id] = [Indexes].[object_id]
				AND [IndexColumns].[index_id] = [Indexes].[index_id]
		INNER JOIN [dbo].[Columns]
				ON [Columns].[database_survey] = [IndexColumns].[database_survey]
				AND [Columns].[object_id] = [IndexColumns].[object_id]
				AND [Columns].[column_id] = [IndexColumns].[column_id]
		GROUP BY
			  [Indexes].[database_survey]
			, [Indexes].[object_id]
			, [Indexes].[index_id]
	)
	SELECT
		  [OtherSurvey].[survey]
		, [OtherSurvey].[id]
		, [OtherTables].[name]
		, [OtherIndexes].[name]
	FROM [dbo].[DatabaseSurveys] AS [ReferenceSurvey]
	INNER JOIN [dbo].[DatabaseSurveys] AS [OtherSurvey]
			ON [OtherSurvey].[survey] = [ReferenceSurvey].[survey]
			AND [OtherSurvey].[is_reference_schema] = 0
	INNER JOIN [dbo].[Tables] AS [ReferenceTables]
			ON [ReferenceTables].[database_survey] = [ReferenceSurvey].[id]
	INNER JOIN [dbo].[Tables] AS [OtherTables]
			ON [OtherTables].[database_survey] = [OtherSurvey].[id]
			AND [OtherTables].[name] = [ReferenceTables].[name]
	INNER JOIN [dbo].[Indexes] AS [ReferenceIndexes]
			ON [ReferenceIndexes].[database_survey] = [ReferenceSurvey].[id]
			AND [ReferenceIndexes].[object_id] = [ReferenceTables].[object_id]
	INNER JOIN [dbo].[Indexes] AS [OtherIndexes]
			ON [OtherIndexes].[database_survey] = [OtherSurvey].[id]
			AND [OtherIndexes].[object_id] = [OtherTables].[object_id]
			AND [OtherIndexes].[name] = [ReferenceIndexes].[name]
	INNER JOIN [IndexColumnChecksums] AS [ReferenceIndexColumnChecksums]
			ON [ReferenceIndexColumnChecksums].[database_survey] = [ReferenceIndexes].[database_survey]
			AND [ReferenceIndexColumnChecksums].[object_id] = [ReferenceIndexes].[object_id]
			AND [ReferenceIndexColumnChecksums].[index_id] = [ReferenceIndexes].[index_id]
	INNER JOIN [IndexColumnChecksums] AS [OtherIndexColumnChecksums]
			ON [OtherIndexColumnChecksums].[database_survey] = [OtherIndexes].[database_survey]
			AND [OtherIndexColumnChecksums].[object_id] = [OtherIndexes].[object_id]
			AND [OtherIndexColumnChecksums].[index_id] = [OtherIndexes].[index_id]
			AND [OtherIndexColumnChecksums].[checksum] != [ReferenceIndexColumnChecksums].[checksum]
	WHERE [ReferenceSurvey].[is_reference_schema] = 1
