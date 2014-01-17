CREATE VIEW [dbo].[MissingSchemas]
(
	  [survey]
	, [database_survey]
	, [schema]
)
AS (
	SELECT
		  [OtherSurvey].[survey]
		, [OtherSurvey].[id]
		, [ReferenceSchemas].[name]
	FROM [dbo].[DatabaseSurveys] AS [ReferenceSurvey]
	INNER JOIN [dbo].[DatabaseSurveys] AS [OtherSurvey]
			ON [OtherSurvey].[survey] = [ReferenceSurvey].[survey]
			AND [OtherSurvey].[is_reference_schema] = 0
	INNER JOIN [dbo].[Schemas] AS [ReferenceSchemas]
			ON [ReferenceSchemas].[database_survey] = [ReferenceSurvey].[id]
	LEFT OUTER JOIN [dbo].[Schemas] AS [OtherSchemas]
			ON [OtherSchemas].[database_survey] = [OtherSurvey].[id]
			AND [OtherSchemas].[name] = [ReferenceSchemas].[name]
	WHERE [ReferenceSurvey].[is_reference_schema] = 1
	AND [OtherSchemas].[name] IS NULL
)
