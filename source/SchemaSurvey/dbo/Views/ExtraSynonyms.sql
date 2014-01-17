CREATE VIEW [dbo].[ExtraSynonyms]
(
	  [survey]
	, [database_survey]
	, [synonym]
)
AS (
	SELECT
		  [OtherSurvey].[survey]
		, [OtherSurvey].[id]
		, [OtherSynonyms].[name]
	FROM [dbo].[DatabaseSurveys] AS [ReferenceSurvey]
	INNER JOIN [dbo].[DatabaseSurveys] AS [OtherSurvey]
			ON [OtherSurvey].[survey] = [ReferenceSurvey].[survey]
			AND [OtherSurvey].[is_reference_schema] = 0
	INNER JOIN [dbo].[Synonyms] AS [OtherSynonyms]
			ON [OtherSynonyms].[database_survey] = [OtherSurvey].[id]
	LEFT OUTER JOIN [dbo].[Synonyms] AS [ReferenceSynonyms]
			ON [ReferenceSynonyms].[database_survey] = [ReferenceSurvey].[id]
			AND [ReferenceSynonyms].[name] = [OtherSynonyms].[name]
	WHERE [ReferenceSurvey].[is_reference_schema] = 1
	AND [ReferenceSynonyms].[name] IS NULL
)
