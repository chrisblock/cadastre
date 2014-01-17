CREATE TABLE [dbo].[DatabasePermissions]
(
	[id] INT IDENTITY(1, 1) NOT NULL,
	[database_survey] INT NOT NULL,
	[class] TINYINT NULL,
	[class_desc] NVARCHAR(60) NOT NULL,
	[major_id] INT NULL,
	[minor_id] INT NULL,
	[grantee_principal_id] INT NULL,
	[grantor_principal_id] INT NULL,
	[type] CHAR(4) NULL,
	[permission_name] NVARCHAR(128) NOT NULL,
	[state] CHAR(1) NULL,
	[state_desc] NVARCHAR(60) NOT NULL,
	CONSTRAINT [PK__DatabasePermissions] PRIMARY KEY ([id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX__DatabasePermissions__database_survey] ON [dbo].[DatabasePermissions] ([database_survey] ASC)
GO
