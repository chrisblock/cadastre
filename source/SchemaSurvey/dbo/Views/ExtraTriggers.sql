CREATE VIEW [dbo].[ExtraTriggers]
(
	  [survey]
	, [database_survey]
	, [table]
	, [trigger]
)
AS (
	SELECT
		  [OtherSurvey].[survey]
		, [OtherSurvey].[id]
		, [OtherTables].[name]
		, [OtherTriggers].[name]
	FROM [dbo].[DatabaseSurveys] AS [ReferenceSurvey]
	INNER JOIN [dbo].[DatabaseSurveys] AS [OtherSurvey]
			ON [OtherSurvey].[survey] = [ReferenceSurvey].[survey]
			AND [OtherSurvey].[is_reference_schema] = 0
	INNER JOIN [dbo].[Tables] AS [OtherTables]
			ON [OtherTables].[database_survey] = [OtherSurvey].[id]
	INNER JOIN [dbo].[Triggers] AS [OtherTriggers]
			ON [OtherTriggers].[database_survey] = [OtherSurvey].[id]
			AND [OtherTriggers].[parent_id] = [OtherTables].[object_id]
	INNER JOIN [dbo].[Tables] AS [ReferenceTables]
			ON [ReferenceTables].[database_survey] = [ReferenceSurvey].[id]
			AND [ReferenceTables].[name] = [OtherTables].[name]
	LEFT OUTER JOIN [dbo].[Triggers] AS [ReferenceTriggers]
			ON [ReferenceTriggers].[database_survey] = [ReferenceSurvey].[id]
			AND [ReferenceTriggers].[parent_id] = [ReferenceTables].[object_id]
			AND [ReferenceTriggers].[name] = [OtherTriggers].[name]
	WHERE [ReferenceSurvey].[is_reference_schema] = 1
	AND [ReferenceTriggers].[name] IS NULL
)
