CREATE TABLE [dbo].[SqlModules]
(
	[id] INT IDENTITY(1, 1),
	[database_survey] INT NOT NULL,
	[object_id] INT NOT NULL,
	[definition_length] INT NULL,
	[definition_checksum] INT NULL,
	[uses_ansi_nulls] BIT NULL,
	[uses_quoted_identifier] BIT NULL,
	[is_schema_bound] BIT NULL,
	[uses_database_collation] BIT NULL,
	[is_recompiled] BIT NULL,
	[null_on_null_input] BIT NULL,
	[execute_as_principal_id] INT NULL,
	CONSTRAINT [PK__SqlModules] PRIMARY KEY ([id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX__SqlModules__database_survey] ON [dbo].[SqlModules] ([database_survey] ASC)
GO
