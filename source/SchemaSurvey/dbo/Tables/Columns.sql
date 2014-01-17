CREATE TABLE [dbo].[Columns]
(
	[id] INT IDENTITY(1, 1) NOT NULL,
	[database_survey] INT NOT NULL,
	[object_id] INT NOT NULL,
	[name] VARCHAR(255) NULL,
	[column_id] INT NOT NULL,
	[system_type_id] TINYINT NOT NULL,
	[user_type_id] INT NOT NULL,
	[max_length] SMALLINT NOT NULL,
	[precision] TINYINT NOT NULL,
	[scale] TINYINT NOT NULL,
	[collation_name] VARCHAR(255) NULL,
	[is_nullable] BIT NULL,
	[is_ansi_padded] BIT NOT NULL,
	[is_rowguidcol] BIT NOT NULL,
	[is_identity] BIT NOT NULL,
	[is_computed] BIT NOT NULL,
	[is_filestream] BIT NOT NULL,
	[is_replicated] BIT NULL,
	[is_non_sql_subscribed] BIT NULL,
	[is_merge_published] BIT NULL,
	[is_dts_replicated] BIT NULL,
	[is_xml_document] BIT NOT NULL,
	[xml_collection_id] INT NOT NULL,
	[default_object_id] INT NOT NULL,
	[rule_object_id] INT NOT NULL,
	/*
	these columns appear in MSSQL2008
	[is_sparse] BIT NULL,
	[is_column_set] BIT NULL,
	*/
	CONSTRAINT [PK__Columns] PRIMARY KEY ([id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX__Columns__database_survey__object_id__name] ON [dbo].[Columns] ([database_survey] ASC, [object_id] ASC, [name] ASC)
GO
