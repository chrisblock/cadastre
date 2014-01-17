CREATE TABLE [dbo].[LinkedLogins]
(
	[id] INT IDENTITY(1, 1) NOT NULL,
	[database_survey] INT NOT NULL,
	[server_id] INT NOT NULL,
	[local_principal_id] INT NULL,
	[uses_self_credential] BIT NOT NULL,
	[remote_name] VARCHAR(255) NULL,
	[modify_date] DATETIME NOT NULL,
	CONSTRAINT [PK__LinkedLogins] PRIMARY KEY ([id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX__LinkedLogins__database_survey] ON [dbo].[LinkedLogins] ([database_survey] ASC)
GO
