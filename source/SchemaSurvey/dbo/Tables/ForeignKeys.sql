CREATE TABLE [dbo].[ForeignKeys]
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
	[referenced_object_id] INT NULL,
	[key_index_id] INT NULL,
	[is_disabled] BIT NOT NULL,
	[is_not_for_replication] BIT NOT NULL,
	[is_not_trusted] BIT NOT NULL,
	[delete_referential_action] TINYINT NULL,
	[delete_referential_action_desc] NVARCHAR(60) NULL,
	[update_referential_action] TINYINT NULL,
	[update_referential_action_desc] NVARCHAR(60) NULL,
	[is_system_named] BIT NOT NULL,
	CONSTRAINT [PK__ForeignKeys] PRIMARY KEY ([id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX__ForeignKeys__database_survey] ON [dbo].[ForeignKeys] ([database_survey] ASC)
GO
