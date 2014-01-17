CREATE VIEW [dbo].[DuplicateIndexes]
(
	  [survey]
	, [database_survey]
	, [table]
	, [index]
	, [index_group]
)
AS
	WITH [IndexColumnSets] ([database_survey], [object_id], [index_id], [columns])
	AS (
		SELECT
			  [Indexes].[database_survey]
			, [Indexes].[object_id]
			, [Indexes].[index_id]
			, STUFF(
			(
				SELECT
						', [' + [Columns].[name] + ']'
				FROM [dbo].[IndexColumns]
				INNER JOIN [dbo].[Columns]
						ON [Columns].[database_survey] = [IndexColumns].[database_survey]
						AND [Columns].[object_id] = [IndexColumns].[object_id]
						AND [Columns].[column_id] = [IndexColumns].[column_id]
				WHERE [IndexColumns].[database_survey] = [Indexes].[database_survey]
				AND [IndexColumns].[object_id] = [Indexes].[object_id]
				AND [IndexColumns].[index_id] = [Indexes].[index_id]
				ORDER BY [Columns].[name]
				FOR XML PATH('')
			), 1, 2, '')
		FROM [dbo].[Indexes]
	),
	[DuplicateColumnSets] ([index_group_id], [database_survey], [object_id], [columns])
	AS (
		SELECT
			  ROW_NUMBER() OVER (ORDER BY [database_survey] ASC, [object_id] ASC)
			, [IndexColumnSets].[database_survey]
			, [IndexColumnSets].[object_id]
			, [IndexColumnSets].[columns]
		FROM [IndexColumnSets]
		GROUP BY
			  [IndexColumnSets].[database_survey]
			, [IndexColumnSets].[object_id]
			, [IndexColumnSets].[columns]
		HAVING COUNT(1) > 1
	)
	SELECT
		  [DatabaseSurveys].[survey]
		, [DatabaseSurveys].[id]
		, [Tables].[name]
		, [Indexes].[name]
		, [DuplicateColumnSets].[index_group_id]
	FROM [dbo].[DatabaseSurveys]
	INNER JOIN [dbo].[Tables]
			ON [Tables].[database_survey] = [DatabaseSurveys].[id]
	INNER JOIN [dbo].[Indexes]
			ON [Indexes].[database_survey] = [Tables].[database_survey]
			AND [Indexes].[object_id] = [Tables].[object_id]
	INNER JOIN [IndexColumnSets]
			ON [IndexColumnSets].[database_survey] = [Indexes].[database_survey]
			AND [IndexColumnSets].[object_id] = [Indexes].[object_id]
			AND [IndexColumnSets].[index_id] = [Indexes].[index_id]
	INNER JOIN [DuplicateColumnSets]
			ON [DuplicateColumnSets].[database_survey] = [IndexColumnSets].[database_survey]
			AND [DuplicateColumnSets].[object_id] = [IndexColumnSets].[object_id]
			AND [DuplicateColumnSets].[columns] = [IndexColumnSets].[columns]
