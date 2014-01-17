CREATE VIEW [dbo].[ExtraStoredProcedures]
(
	  [survey]
	, [database_survey]
	, [stored_procedure]
)
AS (
	SELECT
		  [OtherSurvey].[survey]
		, [OtherSurvey].[id]
		, [OtherProcedures].[name]
	FROM [dbo].[DatabaseSurveys] AS [ReferenceSurvey]
	INNER JOIN [dbo].[DatabaseSurveys] AS [OtherSurvey]
			ON [OtherSurvey].[survey] = [ReferenceSurvey].[survey]
			AND [OtherSurvey].[is_reference_schema] = 0
	INNER JOIN [dbo].[Procedures] AS [OtherProcedures]
			ON [OtherProcedures].[database_survey] = [OtherSurvey].[id]
	LEFT OUTER JOIN [dbo].[Procedures] AS [ReferenceProcedures]
			ON [ReferenceProcedures].[database_survey] = [ReferenceSurvey].[id]
			AND [ReferenceProcedures].[name] = [OtherProcedures].[name]
	WHERE [ReferenceSurvey].[is_reference_schema] = 1
	AND [ReferenceProcedures].[name] IS NULL
)
