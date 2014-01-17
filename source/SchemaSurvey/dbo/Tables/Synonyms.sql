CREATE TABLE [dbo].[Synonyms]
(
	[id] INT IDENTITY(1, 1) NOT NULL,
	[database_survey] INT NOT NULL,
	[name] VARCHAR(255) NOT NULL,
	[object_id] INT NOT NULL,
	[principal_id] INT NULL,
	[schema_id] INT NOT NULL,
	[parent_object_id] INT NOT NULL,
	[type] CHAR(2) NOT NULL,
	[type_desc] NVARCHAR(60) NULL,
	[create_date] DATETIME NOT NULL,
	[modify_date] DATETIME NOT NULL,
	[is_ms_shipped] BIT NOT NULL,
	[is_published] BIT NOT NULL,
	[is_schema_published] BIT NOT NULL,
	[base_object_name] NVARCHAR(1035) NULL,
	CONSTRAINT [PK__Synonyms] PRIMARY KEY ([id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX__Synonyms__database_survey] ON [dbo].[Synonyms] ([database_survey] ASC)
GO
