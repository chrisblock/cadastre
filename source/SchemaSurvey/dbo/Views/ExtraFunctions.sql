CREATE VIEW [dbo].[ExtraFunctions]
(
	  [survey]
	, [database_survey]
	, [function]
)
AS (
	SELECT
		  [OtherSurvey].[survey]
		, [OtherSurvey].[id]
		, [OtherFunctions].[name]
	FROM [dbo].[DatabaseSurveys] AS [ReferenceSurvey]
	INNER JOIN [dbo].[DatabaseSurveys] AS [OtherSurvey]
			ON [OtherSurvey].[survey] = [ReferenceSurvey].[survey]
			AND [OtherSurvey].[is_reference_schema] = 0
	INNER JOIN [dbo].[Functions] AS [OtherFunctions]
			ON [OtherFunctions].[database_survey] = [OtherSurvey].[id]
	LEFT OUTER JOIN [dbo].[Functions] AS [ReferenceFunctions]
			ON [ReferenceFunctions].[database_survey] = [ReferenceSurvey].[id]
			AND [ReferenceFunctions].[name] = [OtherFunctions].[name]
	WHERE [ReferenceSurvey].[is_reference_schema] = 1
	AND [ReferenceFunctions].[name] IS NULL
)
