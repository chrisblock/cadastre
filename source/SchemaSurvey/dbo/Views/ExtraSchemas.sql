CREATE VIEW [dbo].[ExtraSchemas]
(
	  [survey]
	, [database_survey]
	, [schema]
)
AS (
	SELECT
		  [OtherSurvey].[survey]
		, [OtherSurvey].[id]
		, [OtherSchemas].[name]
	FROM [dbo].[DatabaseSurveys] AS [ReferenceSurvey]
	INNER JOIN [dbo].[DatabaseSurveys] AS [OtherSurvey]
			ON [OtherSurvey].[survey] = [ReferenceSurvey].[survey]
			AND [OtherSurvey].[is_reference_schema] = 0
	INNER JOIN [dbo].[Schemas] AS [OtherSchemas]
			ON [OtherSchemas].[database_survey] = [OtherSurvey].[id]
	LEFT OUTER JOIN [dbo].[Schemas] AS [ReferenceSchemas]
			ON [ReferenceSchemas].[database_survey] = [ReferenceSurvey].[id]
			AND [ReferenceSchemas].[name] = [OtherSchemas].[name]
	WHERE [ReferenceSurvey].[is_reference_schema] = 1
	AND [ReferenceSchemas].[name] IS NULL
)
