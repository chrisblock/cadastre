CREATE VIEW [dbo].[MissingServers]
(
	  [survey]
	, [database_survey]
	, [server]
)
AS (
	SELECT
		  [OtherSurvey].[survey]
		, [OtherSurvey].[id]
		, [ReferenceServers].[name]
	FROM [dbo].[DatabaseSurveys] AS [ReferenceSurvey]
	INNER JOIN [dbo].[DatabaseSurveys] AS [OtherSurvey]
			ON [OtherSurvey].[survey] = [ReferenceSurvey].[survey]
			AND [OtherSurvey].[is_reference_schema] = 0
	INNER JOIN [dbo].[Servers] AS [ReferenceServers]
			ON [ReferenceServers].[database_survey] = [ReferenceSurvey].[id]
	LEFT OUTER JOIN [dbo].[Servers] AS [OtherServers]
			ON [OtherServers].[database_survey] = [OtherSurvey].[id]
			AND [OtherServers].[name] = [ReferenceServers].[name]
	WHERE [ReferenceSurvey].[is_reference_schema] = 1
	AND [OtherServers].[name] IS NULL
)
