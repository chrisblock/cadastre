CREATE TABLE [dbo].[SqlModuleDefinitions]
(
	[id] INT IDENTITY(1, 1) NOT NULL,
	[database_survey] INT NOT NULL,
	[object_id] INT NOT NULL,
	[definition] VARCHAR(MAX) NULL,
	CONSTRAINT [PK__SqlModuleDefinitions] PRIMARY KEY ([id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX__SqlModuleDefinitions__database_survey] ON [dbo].[SqlModuleDefinitions] ([database_survey] ASC)
GO
