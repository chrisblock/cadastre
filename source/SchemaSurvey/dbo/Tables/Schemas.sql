CREATE TABLE [dbo].[Schemas]
(
	[id] INT IDENTITY(1, 1) NOT NULL,
	[database_survey] INT NOT NULL,
	[name] VARCHAR(255) NOT NULL,
	[schema_id] INT NOT NULL,
	[principal_id] INT NULL,
	CONSTRAINT [PK__Schemas] PRIMARY KEY ([id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX__Schemas__database_survey] ON [dbo].[Schemas] ([database_survey] ASC)
GO
