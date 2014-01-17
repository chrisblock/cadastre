CREATE VIEW [dbo].[ExtraViews]
(
	  [survey]
	, [database_survey]
	, [view]
)
AS (
	SELECT
		  [OtherSurvey].[survey]
		, [OtherSurvey].[id]
		, [OtherViews].[name]
	FROM [dbo].[DatabaseSurveys] AS [ReferenceSurvey]
	INNER JOIN [dbo].[DatabaseSurveys] AS [OtherSurvey]
			ON [OtherSurvey].[survey] = [ReferenceSurvey].[survey]
			AND [OtherSurvey].[is_reference_schema] = 0
	INNER JOIN [dbo].[Views] AS [OtherViews]
			ON [OtherViews].[database_survey] = [OtherSurvey].[id]
	LEFT OUTER JOIN [dbo].[Views] AS [ReferenceViews]
			ON [ReferenceViews].[database_survey] = [ReferenceSurvey].[id]
			AND [ReferenceViews].[name] = [OtherViews].[name]
	WHERE [ReferenceSurvey].[is_reference_schema] = 1
	AND [ReferenceViews].[name] IS NULL
)
