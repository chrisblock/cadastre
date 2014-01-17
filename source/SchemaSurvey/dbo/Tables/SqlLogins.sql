CREATE TABLE [dbo].[SqlLogins]
(
	[id] INT IDENTITY(1, 1) NOT NULL,
	[database_survey] INT NOT NULL,
	[name] VARCHAR(255) NOT NULL,
	[principal_id] INT NOT NULL,
	[sid] VARBINARY(85) NULL,
	[type] CHAR(1) NOT NULL,
	[type_desc] NVARCHAR(60) NULL,
	[is_disabled] BIT NULL,
	[create_date] DATETIME NOT NULL,
	[modify_date] DATETIME NOT NULL,
	[default_database_name] VARCHAR(255) NULL,
	[default_language_name] VARCHAR(255) NULL,
	[credential_id] INT NULL,
	[is_policy_checked] BIT NULL,
	[is_expiration_checked] BIT NULL,
	[password_hash] VARBINARY(256) NULL,
	CONSTRAINT [PK__SqlLogins] PRIMARY KEY ([id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX__SqlLogins__database_survey] ON [dbo].[SqlLogins] ([database_survey] ASC)
GO
