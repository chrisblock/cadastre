CREATE TABLE [dbo].[Triggers]
(
	[id] INT IDENTITY(1, 1) NOT NULL,
	[database_survey] INT NOT NULL,
	[name] VARCHAR(255) NOT NULL,
	[object_id] INT NOT NULL,
	[parent_class] TINYINT NOT NULL,
	[parent_class_desc] NVARCHAR(60) NULL,
	[parent_id] INT NOT NULL,
	[type] CHAR(2) NOT NULL,
	[type_desc] NVARCHAR(60) NULL,
	[create_date] DATETIME NOT NULL,
	[modify_date] DATETIME NOT NULL,
	[is_ms_shipped] BIT NOT NULL,
	[is_disabled] BIT NOT NULL,
	[is_not_for_replication] BIT NOT NULL,
	[is_instead_of_trigger] BIT NOT NULL,
	CONSTRAINT [PK__Triggers] PRIMARY KEY ([id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX__Triggers__database_survey] ON [dbo].[Triggers] ([database_survey] ASC)
GO
