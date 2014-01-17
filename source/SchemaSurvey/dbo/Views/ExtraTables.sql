CREATE VIEW [dbo].[ExtraTables]
(
	  [survey]
	, [database_survey]
	, [table]
)
AS (
	SELECT
		  [OtherSurvey].[survey]
		, [OtherSurvey].[id]
		, [OtherTables].[name]
	FROM [dbo].[DatabaseSurveys] AS [ReferenceSurvey]
	INNER JOIN [dbo].[DatabaseSurveys] AS [OtherSurvey]
			ON [OtherSurvey].[survey] = [ReferenceSurvey].[survey]
			AND [OtherSurvey].[is_reference_schema] = 0
	INNER JOIN [dbo].[Tables] AS [OtherTables]
			ON [OtherTables].[database_survey] = [OtherSurvey].[id]
	LEFT OUTER JOIN [dbo].[Tables] AS [ReferenceTables]
			ON [ReferenceTables].[database_survey] = [ReferenceSurvey].[id]
			AND [ReferenceTables].[name] = [OtherTables].[name]
	WHERE [ReferenceSurvey].[is_reference_schema] = 1
	AND [ReferenceTables].[name] IS NULL
)
