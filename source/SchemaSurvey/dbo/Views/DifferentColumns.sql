CREATE VIEW [dbo].[DifferentColumns]
(
	  [survey]
	, [database_survey]
	, [table]
	, [column]
)
AS (
	SELECT
		  [OtherSurvey].[survey]
		, [OtherSurvey].[id]
		, [OtherTables].[name]
		, [OtherColumns].[name]
	FROM [dbo].[DatabaseSurveys] AS [ReferenceSurvey]
	INNER JOIN [dbo].[DatabaseSurveys] AS [OtherSurvey]
			ON [OtherSurvey].[survey] = [ReferenceSurvey].[survey]
			AND [OtherSurvey].[is_reference_schema] = 0
	INNER JOIN [dbo].[Tables] AS [ReferenceTables]
			ON [ReferenceTables].[database_survey] = [ReferenceSurvey].[id]
	INNER JOIN [dbo].[Tables] AS [OtherTables]
			ON [OtherTables].[database_survey] = [OtherSurvey].[id]
			AND [OtherTables].[name] = [ReferenceTables].[name]
	INNER JOIN [dbo].[Columns] AS [ReferenceColumns]
			ON [ReferenceColumns].[database_survey] = [ReferenceSurvey].[id]
			AND [ReferenceColumns].[object_id] = [ReferenceTables].[object_id]
	INNER JOIN [dbo].[Columns] AS [OtherColumns]
			ON [OtherColumns].[database_survey] = [OtherSurvey].[id]
			AND [OtherColumns].[object_id] = [OtherTables].[object_id]
			AND [OtherColumns].[name] = [ReferenceColumns].[name]
			AND BINARY_CHECKSUM([OtherColumns].[system_type_id], [OtherColumns].[user_type_id], [OtherColumns].[is_identity], [OtherColumns].[is_nullable], [OtherColumns].[max_length], [OtherColumns].[scale], [OtherColumns].[precision])
				!= BINARY_CHECKSUM([ReferenceColumns].[system_type_id], [ReferenceColumns].[user_type_id], [ReferenceColumns].[is_identity], [ReferenceColumns].[is_nullable], [ReferenceColumns].[max_length], [ReferenceColumns].[scale], [OtherColumns].[precision])
	WHERE [ReferenceSurvey].[is_reference_schema] = 1
)
