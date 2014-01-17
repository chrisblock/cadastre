CREATE VIEW [dbo].[MissingPrincipals]
(
	  [survey]
	, [database_survey]
	, [principal]
)
AS (
	SELECT
		  [OtherSurvey].[survey]
		, [OtherSurvey].[id]
		, [ReferencePrincipals].[name]
	FROM [dbo].[DatabaseSurveys] AS [ReferenceSurvey]
	INNER JOIN [dbo].[DatabaseSurveys] AS [OtherSurvey]
			ON [OtherSurvey].[survey] = [ReferenceSurvey].[survey]
			AND [OtherSurvey].[is_reference_schema] = 0
	INNER JOIN [dbo].[DatabasePrincipals] AS [ReferencePrincipals]
			ON [ReferencePrincipals].[database_survey] = [ReferenceSurvey].[id]
	LEFT OUTER JOIN [dbo].[DatabasePrincipals] AS [OtherPrincipals]
			ON [OtherPrincipals].[database_survey] = [OtherSurvey].[id]
			AND [OtherPrincipals].[name] = [ReferencePrincipals].[name]
	WHERE [ReferenceSurvey].[is_reference_schema] = 1
	AND [OtherPrincipals].[name] IS NULL
)
