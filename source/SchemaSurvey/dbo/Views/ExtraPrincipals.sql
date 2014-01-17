CREATE VIEW [dbo].[ExtraPrincipals]
(
	  [survey]
	, [database_survey]
	, [principal]
)
AS (
	SELECT
		  [OtherSurvey].[survey]
		, [OtherSurvey].[id]
		, [OtherPrincipals].[name]
	FROM [dbo].[DatabaseSurveys] AS [ReferenceSurvey]
	INNER JOIN [dbo].[DatabaseSurveys] AS [OtherSurvey]
			ON [OtherSurvey].[survey] = [ReferenceSurvey].[survey]
			AND [OtherSurvey].[is_reference_schema] = 0
	INNER JOIN [dbo].[DatabasePrincipals] AS [OtherPrincipals]
			ON [OtherPrincipals].[database_survey] = [OtherSurvey].[id]
	LEFT OUTER JOIN [dbo].[DatabasePrincipals] AS [ReferencePrincipals]
			ON [ReferencePrincipals].[database_survey] = [ReferenceSurvey].[id]
			AND [ReferencePrincipals].[name] = [OtherPrincipals].[name]
	WHERE [ReferenceSurvey].[is_reference_schema] = 1
	AND [ReferencePrincipals].[name] IS NULL
)
