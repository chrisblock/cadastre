CREATE TABLE [dbo].[RemoteLogins]
(
	[id] INT IDENTITY(1, 1) NOT NULL,
	[database_survey] INT NOT NULL,
	[server_id] INT NOT NULL,
	[remote_name] VARCHAR(255) NULL,
	[local_principal_id] INT NULL,
	[modify_date] DATETIME NOT NULL,
	CONSTRAINT [PK__RemoteLogins] PRIMARY KEY ([id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX__RemoteLogins__database_survey] ON [dbo].[RemoteLogins] ([database_survey] ASC)
GO
