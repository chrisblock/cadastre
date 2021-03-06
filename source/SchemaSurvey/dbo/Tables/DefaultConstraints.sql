﻿CREATE TABLE [dbo].[DefaultConstraints]
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
	[parent_column_id] INT NOT NULL,
	[definition] NVARCHAR(MAX) NULL,
	[is_system_named] BIT NOT NULL,
	CONSTRAINT [PK__DefaultConstraints] PRIMARY KEY ([id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX__DefaultConstraints__database_survey] ON [dbo].[DefaultConstraints] ([database_survey] ASC)
GO
