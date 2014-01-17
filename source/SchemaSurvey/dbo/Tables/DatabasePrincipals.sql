CREATE TABLE [dbo].[DatabasePrincipals]
(
	[id] INT IDENTITY(1, 1) NOT NULL,
	[database_survey] INT NOT NULL,
	[name] VARCHAR(255) NOT NULL,
	[principal_id] INT NOT NULL,
	[type] CHAR(1) NOT NULL,
	[type_desc] NVARCHAR(60) NULL,
	[default_schema_name] VARCHAR(255) NULL,
	[create_date] DATETIME NOT NULL,
	[modify_date] DATETIME NOT NULL,
	[owning_principal_id] INT NULL,
	[sid] VARBINARY(85) NULL,
	[is_fixed_role] BIT NOT NULL,
	CONSTRAINT [PK__DatabasePrincipals] PRIMARY KEY ([id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX__DatabasePrincipals__database_survey] ON [dbo].[DatabasePrincipals] ([database_survey] ASC)
GO
