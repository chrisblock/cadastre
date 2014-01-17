CREATE TABLE [dbo].[Procedures]
(
	[id] INT IDENTITY(1, 1) NOT NULL,
	[database_survey] INT NOT NULL,
	[name] VARCHAR(255) NULL,
	[object_id] INT NULL,
	[principal_id] INT NULL,
	[schema_id] INT NULL,
	[parent_object_id] INT NULL,
	[type] CHAR(2) NULL,
	[type_desc] NVARCHAR(60) NOT NULL,
	[create_date] DATETIME NULL,
	[modify_date] DATETIME NULL,
	[is_ms_shipped] BIT NULL,
	[is_published] BIT NULL,
	[is_schema_published] BIT NULL,
	[is_auto_executed] BIT NULL,
	[is_execution_replicated] BIT NOT NULL,
	[is_repl_serializable_only] BIT NOT NULL,
	[skips_repl_constraints] BIT NOT NULL,
	CONSTRAINT [PK__Procedures] PRIMARY KEY ([id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX__Procedures__database_survey] ON [dbo].[Procedures] ([database_survey] ASC)
GO
