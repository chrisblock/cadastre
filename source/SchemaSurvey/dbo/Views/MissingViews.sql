CREATE VIEW [dbo].[MissingViews]
(
	  [survey]
	, [database_survey]
	, [view]
)
AS (
	SELECT
		  [OtherSurvey].[survey]
		, [OtherSurvey].[id]
		, [ReferenceViews].[name]
	FROM [dbo].[DatabaseSurveys] AS [ReferenceSurvey]
	INNER JOIN [dbo].[DatabaseSurveys] AS [OtherSurvey]
			ON [OtherSurvey].[survey] = [ReferenceSurvey].[survey]
			AND [OtherSurvey].[is_reference_schema] = 0
	INNER JOIN [dbo].[Views] AS [ReferenceViews]
			ON [ReferenceViews].[database_survey] = [ReferenceSurvey].[id]
	LEFT OUTER JOIN [dbo].[Views] AS [OtherViews]
			ON [OtherViews].[database_survey] = [OtherSurvey].[id]
			AND [OtherViews].[name] = [ReferenceViews].[name]
	WHERE [ReferenceSurvey].[is_reference_schema] = 1
	AND [OtherViews].[name] IS NULL
)
