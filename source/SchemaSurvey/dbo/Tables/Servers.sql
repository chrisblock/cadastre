CREATE TABLE [dbo].[Servers]
(
	[id] INT IDENTITY(1, 1) NOT NULL,
	[database_survey] INT NOT NULL,
	[server_id] INT NOT NULL,
	[name] VARCHAR(255) NOT NULL,
	[product] VARCHAR(255) NOT NULL,
	[provider] VARCHAR(255) NOT NULL,
	[data_source] NVARCHAR(4000) NULL,
	[location] NVARCHAR(4000) NULL,
	[provider_string] NVARCHAR(4000) NULL,
	[catalog] VARCHAR(255) NULL,
	[connect_timeout] INT NULL,
	[query_timeout] INT NULL,
	[is_linked] BIT NOT NULL,
	[is_remote_login_enabled] BIT NOT NULL,
	[is_rpc_out_enabled] BIT NOT NULL,
	[is_data_access_enabled] BIT NOT NULL,
	[is_collation_compatible] BIT NOT NULL,
	[uses_remote_collation] BIT NOT NULL,
	[collation_name] VARCHAR(255) NULL,
	[lazy_schema_validation] BIT NOT NULL,
	[is_system] BIT NOT NULL,
	[is_publisher] BIT NOT NULL,
	[is_subscriber] BIT NULL,
	[is_distributor] BIT NULL,
	[is_nonsql_subscriber] BIT NULL,
	[is_remote_proc_transaction_promotion_enabled] BIT NULL,
	[modify_date] DATETIME NOT NULL,
	CONSTRAINT [PK__Servers] PRIMARY KEY ([id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX__Servers__database_survey] ON [dbo].[Servers] ([database_survey] ASC)
GO
