CREATE TABLE [dbo].[ForeignKeyColumns]
(
	[id] INT IDENTITY(1, 1) NOT NULL,
	[database_survey] INT NOT NULL,
	[constraint_object_id] INT NULL,
	[constraint_column_id] INT NULL,
	[parent_object_id] INT NULL,
	[parent_column_id] INT NULL,
	[referenced_object_id] INT NULL,
	[referenced_column_id] INT NULL,
	CONSTRAINT [PK__ForeignKeyColumns] PRIMARY KEY ([id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX__ForeignKeyColumns__database_survey] ON [dbo].[ForeignKeyColumns] ([database_survey] ASC)
GO
