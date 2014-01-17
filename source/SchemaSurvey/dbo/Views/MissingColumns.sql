CREATE VIEW [dbo].[MissingColumns]
(
	  [survey]
	, [database_survey]
	, [table]
	, [column]
)
AS (
	SELECT
		  [OtherSurvey].[survey]
		, [OtherSurvey].[id]
		, [ReferenceTables].[name]
		, [ReferenceColumns].[name]
	FROM [dbo].[DatabaseSurveys] AS [ReferenceSurvey]
	INNER JOIN [dbo].[DatabaseSurveys] AS [OtherSurvey]
			ON [OtherSurvey].[survey] = [ReferenceSurvey].[survey]
			AND [OtherSurvey].[is_reference_schema] = 0
	INNER JOIN [dbo].[Tables] AS [ReferenceTables]
			ON [ReferenceTables].[database_survey] = [ReferenceSurvey].[id]
	INNER JOIN [dbo].[Columns] AS [ReferenceColumns]
			ON [ReferenceColumns].[database_survey] = [ReferenceSurvey].[id]
			AND [ReferenceColumns].[object_id] = [ReferenceTables].[object_id]
	INNER JOIN [dbo].[Tables] AS [OtherTables]
			ON [OtherTables].[database_survey] = [OtherSurvey].[id]
			AND [OtherTables].[name] = [ReferenceTables].[name]
	LEFT OUTER JOIN [dbo].[Columns] AS [OtherColumns]
			ON [OtherColumns].[database_survey] = [OtherSurvey].[id]
			AND [OtherColumns].[object_id] = [OtherTables].[object_id]
			AND [OtherColumns].[name] = [ReferenceColumns].[name]
	WHERE [ReferenceSurvey].[is_reference_schema] = 1
	AND [OtherColumns].[name] IS NULL
)
