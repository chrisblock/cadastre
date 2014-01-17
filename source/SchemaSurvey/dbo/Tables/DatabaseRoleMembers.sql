CREATE TABLE [dbo].[DatabaseRoleMembers]
(
	[id] INT IDENTITY(1, 1) NOT NULL,
	[database_survey] INT NOT NULL,
	[role_principal_id] INT NULL,
	[member_principal_id] INT NULL,
	CONSTRAINT [PK__DatabaseRoleMembers] PRIMARY KEY ([id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX__DatabaseRoleMembers__database_survey] ON [dbo].[DatabaseRoleMembers] ([database_survey] ASC)
GO
