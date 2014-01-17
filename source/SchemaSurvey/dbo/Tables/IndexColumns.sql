CREATE TABLE [dbo].[IndexColumns]
(
	[id] INT IDENTITY(1, 1) NOT NULL,
	[database_survey] INT NOT NULL,
	[object_id] INT NOT NULL,
	[index_id] INT NOT NULL,
	[index_column_id] INT NOT NULL,
	[column_id] INT NOT NULL,
	[key_ordinal] TINYINT NOT NULL,
	[partition_ordinal] TINYINT NOT NULL,
	[is_descending_key] BIT NULL,
	[is_included_column] BIT NULL,
	CONSTRAINT [PK__IndexColumns] PRIMARY KEY ([id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX__IndexColumns__database_survey] ON [dbo].[IndexColumns] ([database_survey] ASC)
GO
