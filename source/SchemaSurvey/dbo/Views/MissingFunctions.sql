CREATE VIEW [dbo].[MissingFunctions]
(
	  [survey]
	, [database_survey]
	, [function]
)
AS (
	SELECT
		  [OtherSurvey].[survey]
		, [OtherSurvey].[id]
		, [ReferenceFunctions].[name]
	FROM [dbo].[DatabaseSurveys] AS [ReferenceSurvey]
	INNER JOIN [dbo].[DatabaseSurveys] AS [OtherSurvey]
			ON [OtherSurvey].[survey] = [ReferenceSurvey].[survey]
			AND [OtherSurvey].[is_reference_schema] = 0
	INNER JOIN [dbo].[Functions] AS [ReferenceFunctions]
			ON [ReferenceFunctions].[database_survey] = [ReferenceSurvey].[id]
	LEFT OUTER JOIN [dbo].[Functions] AS [OtherFunctions]
			ON [OtherFunctions].[database_survey] = [OtherSurvey].[id]
			AND [OtherFunctions].[name] = [ReferenceFunctions].[name]
	WHERE [ReferenceSurvey].[is_reference_schema] = 1
	AND [OtherFunctions].[name] IS NULL
)
