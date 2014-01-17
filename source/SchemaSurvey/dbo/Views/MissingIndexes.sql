CREATE VIEW [dbo].[MissingIndexes]
(
	  [survey]
	, [database_survey]
	, [table]
	, [index]
)
AS (
	SELECT
		  [OtherSurvey].[survey]
		, [OtherSurvey].[id]
		, [ReferenceTables].[name]
		, [ReferenceIndexes].[name]
	FROM [dbo].[DatabaseSurveys] AS [ReferenceSurvey]
	INNER JOIN [dbo].[DatabaseSurveys] AS [OtherSurvey]
			ON [OtherSurvey].[survey] = [ReferenceSurvey].[survey]
			AND [OtherSurvey].[is_reference_schema] = 0
	INNER JOIN [dbo].[Tables] AS [ReferenceTables]
			ON [ReferenceTables].[database_survey] = [ReferenceSurvey].[id]
	INNER JOIN [dbo].[Indexes] AS [ReferenceIndexes]
			ON [ReferenceIndexes].[database_survey] = [ReferenceSurvey].[id]
			AND [ReferenceIndexes].[object_id] = [ReferenceTables].[object_id]
	INNER JOIN [dbo].[Tables] AS [OtherTables]
			ON [OtherTables].[database_survey] = [OtherSurvey].[id]
			AND [OtherTables].[name] = [ReferenceTables].[name]
	LEFT OUTER JOIN [dbo].[Indexes] AS [OtherIndexes]
			ON [OtherIndexes].[database_survey] = [OtherSurvey].[id]
			AND [OtherIndexes].[object_id] = [OtherTables].[object_id]
			AND [OtherIndexes].[name] = [ReferenceIndexes].[name]
	WHERE [ReferenceSurvey].[is_reference_schema] = 1
	AND [OtherIndexes].[name] IS NULL
)
