CREATE VIEW [dbo].[MissingSynonyms]
(
	  [survey]
	, [database_survey]
	, [synonym]
)
AS (
	SELECT
		  [OtherSurvey].[survey]
		, [OtherSurvey].[id]
		, [ReferenceSynonyms].[name]
	FROM [dbo].[DatabaseSurveys] AS [ReferenceSurvey]
	INNER JOIN [dbo].[DatabaseSurveys] AS [OtherSurvey]
			ON [OtherSurvey].[survey] = [ReferenceSurvey].[survey]
			AND [OtherSurvey].[is_reference_schema] = 0
	INNER JOIN [dbo].[Synonyms] AS [ReferenceSynonyms]
			ON [ReferenceSynonyms].[database_survey] = [ReferenceSurvey].[id]
	LEFT OUTER JOIN [dbo].[Synonyms] AS [OtherSynonyms]
			ON [OtherSynonyms].[database_survey] = [OtherSurvey].[id]
			AND [OtherSynonyms].[name] = [ReferenceSynonyms].[name]
	WHERE [ReferenceSurvey].[is_reference_schema] = 1
	AND [OtherSynonyms].[name] IS NULL
)
