CREATE VIEW [dbo].[MissingStoredProcedures]
(
	  [survey]
	, [database_survey]
	, [stored_procedure]
)
AS (
	SELECT
		  [OtherSurvey].[survey]
		, [OtherSurvey].[id]
		, [ReferenceProcedures].[name]
	FROM [dbo].[DatabaseSurveys] AS [ReferenceSurvey]
	INNER JOIN [dbo].[DatabaseSurveys] AS [OtherSurvey]
			ON [OtherSurvey].[survey] = [ReferenceSurvey].[survey]
			AND [OtherSurvey].[is_reference_schema] = 0
	INNER JOIN [dbo].[Procedures] AS [ReferenceProcedures]
			ON [ReferenceProcedures].[database_survey] = [ReferenceSurvey].[id]
	LEFT OUTER JOIN [dbo].[Procedures] AS [OtherProcedures]
			ON [OtherProcedures].[database_survey] = [OtherSurvey].[id]
			AND [OtherProcedures].[name] = [ReferenceProcedures].[name]
	WHERE [ReferenceSurvey].[is_reference_schema] = 1
	AND [OtherProcedures].[name] IS NULL
)
